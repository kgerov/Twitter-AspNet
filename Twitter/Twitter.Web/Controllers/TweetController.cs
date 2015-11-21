using System;
using System.Linq;
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

        [HttpPost]
        public ActionResult FavoriteTweet(int id)
        {
            Tweet tweet = this.Data.Tweets.All().FirstOrDefault(x => x.Id == id);
            
            if (tweet != null)
            {
                var user = this.UserProfile;
                user.Favorties.Add(tweet);

                this.Data.SaveChanges();
            }

            string response = @"<a data-ajax='true' href='/Tweet/UnfavoriteTweet?id=@Model.Id'
                                   data-ajax-method='POST' data-ajax-mode='replace'
                                   data-ajax-update='#tweet-favs-box-@Model.Id'><img src='~/Images/heart-red-icon.png' alt='' /></a>
                                   <span id='favs-count-@Model.Id'>{0}</span>";

            return this.Content(String.Format(response, tweet.Favorites.Count));
        }

        [HttpPost]
        public ActionResult UnfavoriteTweet(int id)
        {
            Tweet tweet = this.Data.Tweets.All().FirstOrDefault(x => x.Id == id);

            if (tweet != null)
            {
                var user = this.UserProfile;
                user.Favorties.Remove(tweet);

                this.Data.SaveChanges();
            }

            string response = @"<a data-ajax='true' href='/Tweet/FavoriteTweet?id=@Model.Id'
                                   data-ajax-method='POST' data-ajax-mode='replace'
                                   data-ajax-update='#tweet-favs-box-@Model.Id'><img src='~/Images/heart-icon.png' alt='' /></a>
                                   <span id='favs-count-@Model.Id'>{0}</span>";

            return this.Content(String.Format(response, tweet.Favorites.Count));
        }
    }
}