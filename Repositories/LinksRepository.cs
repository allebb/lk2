using LK2.Models;
using LK2.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LK2.Repositories
{
    public class LinksRepository : ILinksRepository
    {

        private DatabaseContext db;

        public LinksRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public Link CreateShortUrl(string url)
        {
            Link link = new Link();
            link.Redir = url;
            db.Links.Add(link);
            db.SaveChanges();
            // Now we generate a Base36 string for the DB id:
            link.Hash = Base62.Encode(link.Id);
            db.SaveChanges();
            return link;
        }

        public Link ReadShortUrl(string hash)
        {
            Link link = db.Links.First(s => s.Hash == hash);
            return link;
        }

        public void IncrementUrlClickCount(Link link)
        {

            // Incrememnt our click counter by 1.
            link.Clicks++;

            // Inform EF that the model has been updated and requires saving to our database.
            db.Entry(link).State = EntityState.Modified;

            // Persist/save our changes to the database.
            db.SaveChanges();
        }

        public int GetLinkStatistics()
        {
            return db.Links.Count();
        }
    }
}
