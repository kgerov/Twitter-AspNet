using System.Data.Entity;
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
                .Include(x => x.User)
                .Include(x => x.Favorites)
                .Include(x => x.Retweets)
                .Select(TweetViewModel.ViewModel)
                .ToList();

            HomeViewModel homeModel = new HomeViewModel()
            {
                Tweets = tweets
            };

            if (this.Request.IsAuthenticated)
            {
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