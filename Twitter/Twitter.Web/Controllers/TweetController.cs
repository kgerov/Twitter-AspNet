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
            // To do go to register form
            if (!this.Request.IsAuthenticated)
            {
                return this.JavaScript("");
            }

            Tweet tweet = this.Data.Tweets.All().FirstOrDefault(x => x.Id == id);
            
            if (tweet != null)
            {
                var user = this.UserProfile;
                user.Favorties.Add(tweet);

                this.Data.SaveChanges();
            }

            string response = @"<a data-ajax='true' href='/Tweet/UnfavoriteTweet?id={0}'
                                   data-ajax-method='POST' data-ajax-mode='replace'
                                   data-ajax-update='#tweet-favs-box-{0}'><img src='{1}' alt='' /></a>
                                   <span id='favs-count-{0}'>{2}</span>";

            return this.Content(String.Format(response, id, Resources.General.RedHeart, tweet.Favorites.Count));
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

            string response = @"<a data-ajax='true' href='/Tweet/FavoriteTweet?id={0}'
                                   data-ajax-method='POST' data-ajax-mode='replace'
                                   data-ajax-update='#tweet-favs-box-{0}'><img src='{1}' alt='' /></a>
                                   <span id='favs-count-{0}'>{2}</span>";

            return this.Content(String.Format(response, id, Resources.General.BlackHeart, tweet.Favorites.Count));
        }

        [HttpGet]
        public ActionResult Report(int id)
        {
            TweetViewModel reportedTweet = this.Data.Tweets.All()
                .Select(TweetViewModel.ViewModel)
                .FirstOrDefault(x => x.Id == id);

            ReportViewModel report = new ReportViewModel()
            {
                Tweet = reportedTweet
            };

            return this.View(report);
        }


        [HttpPost]
        public ActionResult Report(Report reportView)
        {
            int reportedId = reportView.ReportedId;

            Twitter.Models.Report report = new Report()
            {
                Content = reportView.Content,
                Reporter = null,
                Reported = null,
                ReportedId = reportView.ReportedId,
                ReporterId = this.UserProfile.Id
            };

            this.Data.Reports.Add(report);
            this.Data.SaveChanges();

            return this.RedirectToAction("Index", "Home");
        }
    }
}