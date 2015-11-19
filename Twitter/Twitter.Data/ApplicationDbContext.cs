using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Twitter.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Twitter.Data.Migrations;

namespace Twitter.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public IDbSet<Message> Messages { get; set; }

        public IDbSet<AdministrationLog> AdministrationLogs { get; set; }

        public IDbSet<Report> Reports { get; set; }

        public IDbSet<Tweet> Tweets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(t => t.Followers)
                .WithMany(t => t.Following)
                .Map(m =>
                {
                    m.ToTable("UserFollowing");
                    m.MapLeftKey("FollowerId");
                    m.MapRightKey("FollowingId");
                });

            modelBuilder.Entity<Tweet>()
                .HasMany(t => t.Favorites)
                .WithMany(t => t.Favorties)
                .Map(m =>
                {
                    m.ToTable("TweetFavorties");
                    m.MapLeftKey("TweetId");
                    m.MapRightKey("UserId");
                });

            //modelBuilder.Entity<Message>()
            //    .HasRequired(m => m.Recipient)
            //    .WithMany(u => u.MessagesInbox)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Message>()
            //    .HasRequired(m => m.Sender)
            //    .WithMany(u => u.MessagesSend)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Message>()
            //    .HasRequired(m => m.Sender)
            //    .WithOptional
            //    .WillCascadeOnDelete(false);


            modelBuilder.Entity<Tweet>()
                .HasRequired(m => m.User)
                .WithMany(u => u.Tweets)
                .WillCascadeOnDelete(false);

           base.OnModelCreating(modelBuilder);
        }
    }
}
