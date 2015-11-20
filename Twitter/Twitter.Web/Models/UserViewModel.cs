using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using Twitter.Models;

namespace Twitter.Web.Models
{
    public class UserViewModel
    {
        public static Expression<Func<User, UserViewModel>> ViewModel
        {
            get
            {
                return x => new UserViewModel
                {
                    UserName = x.UserName,
                    Email = x.Email,
                    FullName = x.FullName,
                    HomeTown = x.HomeTown,
                    WebSite = x.WebSite,
                    JoinDate = x.JoinDate,
                    Image = x.Image.Photo,
                    Tweets = x.Tweets.AsQueryable().Select(TweetViewModel.ViewModel)
                };
            }
        }

        public static Expression<Func<User, UserViewModel>> EditModel
        {
            get
            {
                return x => new UserViewModel
                {
                    FullName = x.FullName,
                    HomeTown = x.HomeTown,
                    WebSite = x.WebSite,
                    Image = x.Image.Photo
                };
            }
        }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Full name")]
        public string FullName { get; set; }

        public string Email { get; set; }

        public string HomeTown { get; set; }

        public string WebSite { get; set; }

        public DateTime JoinDate { get; set; }

        public byte[] Image { get; set; }

        public IEnumerable<TweetViewModel> Tweets { get; set; }
    }
}