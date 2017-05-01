using LK2.Entity;
using LK2.Models;
using LK2.Repositories;
using Microsoft.AspNetCore.Mvc;
using LK2.ViewModels;

namespace LK2.Controllers
{
    public class ApiController : Controller
    {

        private ILinksRepository repo;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="linksRepository">IoC resolution for our Repository class.</param>
        public ApiController(ILinksRepository linksRepository)
        {
            repo = linksRepository;
        }

        /// <summary>
        /// Provides an endpoint that can be 'pinged' for service monitoring purposes.
        /// </summary>
        /// <returns></returns>
        // GET: /api/v1/ping
        [HttpGet]
        public IActionResult Ping()
        {
            // Retrieve the total number of links that we have stored in the database.
            int totalLinks = repo.GetLinkStatistics();
            // Build and return a JSON response.
            return Json(new {
                status = "online",
                links = totalLinks,
            });
        }

        /// <summary>
        /// Provides an endpoint that that will generate a short URL.
        /// </summary>
        /// <returns></returns>
        // GET: /api/v1/create
        [HttpPost]
        public IActionResult Generate([FromBody] UrlCreationEntity url)
        {
            Link GeneratedLink = repo.CreateShortUrl(url.url);
            return Json(new UrlViewModel(GeneratedLink, Url.RouteUrl("homepage", null, Request.Scheme)));
        }
    }
}
