using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using LK2.Models;
using Microsoft.EntityFrameworkCore;
using LK2.Repositories;
using Microsoft.Extensions.Configuration;

namespace LK2
{
    public class Startup
    {

        /// <summary>
        /// Application configuration.
        /// </summary>
        public IConfigurationRoot Configuration { get; set; }

        /// <summary>
        /// Configure application environment variables.
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Add our database context into the IoC container.
            var driver = Configuration["DbDriver"];
            // Use SQLite?
            if (driver == "sqlite")
            {
                services.AddDbContext<DatabaseContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            }
            // Use MSSQL?
            if (driver == "mssql")
            {
                services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            }

            // IoC bindings.
            services.AddScoped<ILinksRepository, LinksRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, DatabaseContext db)
        {

            loggerFactory.AddConsole();

            // Enable development environment exception page.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enables automatic execution of Entity Framework migrations at application startup.
            db.Database.Migrate();

            // Enable static files (assets to be served etc.)
            app.UseStaticFiles();

            app.UseMvc(
                 routes =>
                 {
                     // GET / - The homepage.
                     routes.MapRoute("homepage", "", defaults: new { controller = "Home", action = "Index" });
                     // POST / - Generates a new short url.
                     routes.MapRoute("generate", "", defaults: new { controller = "Home", action = "Create" });
                     // GET /docs/ - The documentation page.
                     routes.MapRoute("documentation", "docs/", defaults: new { controller = "Documentation", action = "Index" });
                     // GET /api/v1/ping - A simple JSON endpoint for service monitoring.
                     routes.MapRoute("api-ping", "api/v1/ping", defaults: new { controller = "Api", action = "Ping" });
                     // GET /api/v1/create - An endpoint that accepts a JSON payload to generate a short url.
                     routes.MapRoute("api-create", "api/v1/create", defaults: new { controller = "Api", action = "Generate" });
                     // GET /{hash} - The retrieval URL
                     routes.MapRoute("retrieve", "{*hash}", defaults: new { controller = "Home", action = "Retrieve" });
                 }
            );

        }
    }
}
