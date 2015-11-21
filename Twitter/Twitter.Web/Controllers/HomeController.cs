using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Twitter.Data.UnitOfWork;
using Twitter.Models;
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
            ICollection<TweetViewModel> tweets = null;

            if (this.Request.IsAuthenticated)
            {
                tweets = this.Data.Tweets.All()
                    .Include(x => x.User)
                    .Include(x => x.Favorites)
                    .Include(x => x.Retweets)
                    .Select(x => new TweetViewModel()
                    {
                        Id = x.Id,
                        Content = x.Content,
                        DatePublished = x.DatePublished,
                        Retweets = x.Retweets.Count,
                        FavoritesCount = x.Favorites.Count,
                        Name = x.User.FullName,
                        UserName = x.User.UserName,
                        Image = x.User.Image.Photo,
                        IsLikedByUser =
                            (x.Favorites.FirstOrDefault(u => u.UserName == this.UserProfile.UserName) != null)
                    })
                    .ToList();
            }
            else
            {
                tweets = this.Data.Tweets.All()
                    .Include(x => x.User)
                    .Include(x => x.Favorites)
                    .Include(x => x.Retweets)
                    .Select(x => new TweetViewModel()
                    {
                        Id = x.Id,
                        Content = x.Content,
                        DatePublished = x.DatePublished,
                        Retweets = x.Retweets.Count,
                        FavoritesCount = x.Favorites.Count,
                        Name = x.User.FullName,
                        UserName = x.User.UserName,
                        Image = x.User.Image.Photo,
                        IsLikedByUser = false
                    })
                    .ToList();
            }

            HomeViewModel homeModel = new HomeViewModel()
            {
                Tweets = tweets
            };


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