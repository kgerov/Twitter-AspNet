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

        public ActionResult Profile(string username)
        {
            var user = Data.Users.All().FirstOrDefault(x => x.UserName == username);

            if (user != null)
            {
                var userViewModel = new UserViewModel()
                {
                    UserName = user.UserName,
                    Email = user.Email
                };

                return View(userViewModel);
            }

            return this.HttpNotFound("User does not exist");
        }
    }
}