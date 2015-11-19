using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
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

        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Profile(string username)
        {
            var userProfile = this.Data.Users.All()
               .Include(x => x.Tweets)
               .Where(x => x.UserName == username)
               .Select(UserViewModel.ViewModel)
               .FirstOrDefault();

            return View(userProfile);
        }
    }
}