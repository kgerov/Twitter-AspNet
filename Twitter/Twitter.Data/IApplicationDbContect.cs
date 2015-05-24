using System.Data.Entity;
using Twitter.Models;

namespace Twitter.Data
{
    public interface IApplicationDbContect
    {
        IDbSet<Message> Messages { get; set; }

        IDbSet<AdministrationLog> AdministrationLogs { get; set; }

        IDbSet<Report> Reports { get; set; }

        IDbSet<Tweet> Tweets { get; set; }

        int SaveChanges();
    }
}
