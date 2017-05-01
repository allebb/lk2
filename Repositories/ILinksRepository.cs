using LK2.Models;

namespace LK2.Repositories
{
    public interface ILinksRepository
    {
        Link CreateShortUrl(string url);
        int GetLinkStatistics();
        Link ReadShortUrl(string hash);
        void IncrementUrlClickCount(Link link);
    }
}