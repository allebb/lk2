using LK2.Models;

namespace LK2.ViewModels
{
    public class UrlViewModel
    {

        public string hash { get; }
        public string shortened { get; }
        public string redirect { get; }

        /// <summary>
        /// Create a new instance of the URL (API response) view model.
        /// </summary>
        /// <param name="link">The Link model</param>
        /// <param name="baseUrl">The base URL to our site</param>
        public UrlViewModel(Link link, string baseUrl)
        {
            this.hash = link.Hash;
            this.shortened = baseUrl + link.Hash;
            this.redirect = link.Redir;
        }
    }
}
