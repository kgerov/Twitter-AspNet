using Microsoft.AspNet.Identity;
using Twitter.Data.Repositories;
using Twitter.Models;

namespace Twitter.Data.UnitOfWork
{
    public interface ITwitterData
    {
        IRepository<User> Users { get; }

        IRepository<Tweet> Tweets { get; }

        IRepository<Message> Messages { get; }

        IRepository<AdministrationLog> AdministrationLogs { get; }

        IRepository<Report> Reports { get; }

        IRepository<Image> Images { get; }

        int SaveChanges();
    }
}
