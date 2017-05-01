using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LK2.Models
{
    public class Link
    {
        /// <summary>
        /// The database managed auto index.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The short URL hash.
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// The original URL (to be redirected to)
        /// </summary>
        public string Redir { get; set; }

        /// <summary>
        /// Number of clicks/visits to the short url.
        /// </summary>
        public int Clicks { get; set; }
    }
}
