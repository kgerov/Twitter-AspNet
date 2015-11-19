using System;
using System.Web.Mvc;
using Twitter.Data.UnitOfWork;
using Twitter.Models;
using Twitter.Web.Models;

namespace Twitter.Web.Controllers
{
    public class TweetController : BaseController
    {
        public TweetController(ITwitterData data)
            : base(data)
        {
        }

        // GET: Tweets
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Compose()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Compose(TweetViewModel tweet)
        {
            Tweet newTweet = new Tweet()
            {
                Content = tweet.Content,
                DatePublished = DateTime.Now,
                UserId = this.UserProfile.Id,
                User = null
            };
            

            this.Data.Tweets.Add(newTweet);
            this.Data.SaveChanges();

            return RedirectToAction("Profile", "Users", new { username = this.UserProfile.UserName });
        }
    }
}