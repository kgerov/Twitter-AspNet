using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Twitter.Data.UnitOfWork;
using PagedList;
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

        public ActionResult Index(int? page)
        {
            IOrderedQueryable<TweetViewModel> tweets;

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
                    .OrderByDescending(x => x.DatePublished);
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
                    .OrderByDescending(x => x.DatePublished);
            }

            int pageSize = Int32.Parse(Resources.General.PageSize);
            int pageNumber = (page ?? 1);

            HomeViewModel homeModel = new HomeViewModel()
            {
                Tweets = tweets.ToPagedList(pageNumber, pageSize)
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