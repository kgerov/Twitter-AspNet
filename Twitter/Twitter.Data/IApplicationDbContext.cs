using System.Data.Entity;
using Twitter.Models;

namespace Twitter.Data
{
    public interface IApplicationDbContext
    {
        IDbSet<Message> Messages { get; set; }

        IDbSet<AdministrationLog> AdministrationLogs { get; set; }

        IDbSet<Report> Reports { get; set; }

        IDbSet<Tweet> Tweets { get; set; }

        IDbSet<Image> Images { get; set; }

        int SaveChanges();
    }
}
