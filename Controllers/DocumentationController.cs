using Microsoft.AspNetCore.Mvc;

namespace LK2.Controllers
{
    public class DocumentationController : Controller
    {
        /// <summary>
        /// Displays a simple documentation page.
        /// </summary>
        /// <returns></returns>
        // GET: /docs
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.BaseUrl = Url.RouteUrl("homepage", null, Request.Scheme)
                .TrimEnd("/".ToCharArray());
            ViewBag.Host = Request.Host;
            return View();
        }
    }
}
