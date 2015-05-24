using System;
using System.Linq;
using System.Web.Routing;
using Twitter.Data;
using Twitter.Data.UnitOfWork;
using Twitter.Models;
using WebGrease;

namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;

    public abstract class BaseController : Controller
    {
        private ITwitterData data;
        private User userProfile;

        protected BaseController(ITwitterData data)
        {
            this.Data = data;
        }

        protected BaseController(ITwitterData data, User userProfile)
            : this(data)
        {
            this.UserProfile = userProfile;
        }

        public ITwitterData Data { get; private set; }

        public User UserProfile { get; private set; }

        protected override IAsyncResult BeginExecute(RequestContext request, AsyncCallback callback, object state)
        {
            if (request.HttpContext.User.Identity.IsAuthenticated)
            {
                var username = request.HttpContext.User.Identity.Name;
                var user = this.Data.Users.All().FirstOrDefault(x => x.UserName == username);
                this.UserProfile = user;
            }

            return base.BeginExecute(request, callback, state);
        }
    }
}