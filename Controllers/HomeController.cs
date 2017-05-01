using LK2.Models;
using LK2.Repositories;
using LK2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LK2.Controllers
{
    public class HomeController : Controller
    {

        /// <summary>
        /// Our Links Repostory implementation.
        /// </summary>
        private ILinksRepository repo;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="linksRepository">IoC resolution for our Repository class.</param>
        public HomeController(ILinksRepository linksRepository)
        {
            repo = linksRepository;
        }

        /// <summary>
        /// Our Homepage/Landing page.
        /// </summary>
        /// <returns></returns>
        // GET: /
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.RequestScheme = HttpContext.Request.Scheme;
            return View();
        }

        /// <summary>
        /// Resolves the short URL and redirects to the original url.
        /// </summary>
        /// <returns></returns>
        /// GET /{hash}
        [HttpGet]
        public IActionResult Retrieve(string hash)
        {
            try
            {
                Link link = repo.ReadShortUrl(hash);
                repo.IncrementUrlClickCount(link);
                return Redirect(link.Redir);
            }
            catch (Exception)
            {
                // If the short url is not found we'll redirect the user to the homepage by default!!
                return RedirectToRoute("homepage");
            }

        }

        /// <summary>
        /// Generates a new short URL from the HTML form.
        /// </summary>
        /// <returns></returns>
        /// POST /
        [HttpPost]
        public IActionResult Create()
        {
            string url = Request.Form["url"];
            bool isValidUrl = Uri.IsWellFormedUriString(url, UriKind.Absolute);
            if (isValidUrl)
            {
                Link GeneratedLink = repo.CreateShortUrl(url);
                return Json(new UrlViewModel(GeneratedLink, Url.RouteUrl("homepage", null, Request.Scheme)));
            }
            return BadRequest();
        }
    }
}
