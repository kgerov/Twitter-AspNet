using System.Linq;
using System.Web.Mvc;
using Twitter.Data.UnitOfWork;
using Twitter.Web.Models;

namespace Twitter.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ITwitterData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var tweets = this.Data.Tweets.All()
                .Select(TweetViewModel.ViewModel)
                .ToList();

            HomeViewModel homeModel = new HomeViewModel()
            {
                Tweets = tweets
            };

            if (this.UserProfile != null)
            {
                homeModel.isLoggedIn = true;
                homeModel.UserName = this.UserProfile.UserName;
            }
            else
            {
                homeModel.isLoggedIn = false;
            }

            return this.View(homeModel);
        }

        public ActionResult About()
        {
            return this.View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}