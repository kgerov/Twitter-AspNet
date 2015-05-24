using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using Microsoft.AspNet.Identity;
using Twitter.Data.Repositories;
using Twitter.Models;

namespace Twitter.Data.UnitOfWork
{
    public class TwitterData : ITwitterData
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IDictionary<Type, object> repositories;

        public TwitterData()
            : this(new ApplicationDbContext())
        {
        }

        public TwitterData(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IRepository<Tweet> Tweets
        {
            get { return this.GetRepository<Tweet>();; }
        }

        public IRepository<Message> Messages
        {
            get { return this.GetRepository<Message>(); }
        }

        public IRepository<AdministrationLog> AdministrationLogs
        {
            get { return this.GetRepository<AdministrationLog>(); }
        }

        public IRepository<Report> Reports
        {
            get { return this.GetRepository<Report>(); }
        }

        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof (T);
            if (!this.repositories.ContainsKey(type))
            {
                var typeOfRepository = typeof(GenericRepository<T>);
                //if (type.IsAssignableFrom(typeof(Game)))
                //{
                //    typeOfRepository = typeof(Game);
                //}

                var repository = Activator.CreateInstance(typeOfRepository, this.dbContext);
                this.repositories.Add(type, repository);
            }

            return (IRepository<T>)this.repositories[type];
        }
    }
}
