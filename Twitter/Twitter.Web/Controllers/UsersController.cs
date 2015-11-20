using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Twitter.Data.UnitOfWork;
using Twitter.Web.Models;

namespace Twitter.Web.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController(ITwitterData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Profile(string username)
        {
            var userProfile = this.Data.Users.All()
               .Include(x => x.Tweets)
               .Include(x => x.Image)
               .Where(x => x.UserName == username)
               .Select(UserViewModel.ViewModel)
               .FirstOrDefault();

            return View(userProfile);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Settings()
        {
            var userProfile = this.Data.Users.All()
               .Include(x => x.Image)
               .Where(x => x.UserName == this.UserProfile.UserName)
               .Select(UserViewModel.EditModel)
               .FirstOrDefault();

            return this.View(userProfile);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Settings(UserViewModel user)
        {
            var currentUser = this.UserProfile;

            currentUser.FullName = user.FullName;
            currentUser.HomeTown = user.HomeTown;
            currentUser.WebSite = user.WebSite;

            this.Data.Users.Update(currentUser);
            this.Data.SaveChanges();

            return this.RedirectToAction("Profile", "Users", new { username = this.UserProfile.UserName });
        }
    }
}