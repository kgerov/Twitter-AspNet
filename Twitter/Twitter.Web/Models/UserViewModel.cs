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
                    Tweets = x.Tweets.AsQueryable().Select(TweetViewModel.ViewModel)
                };
            }
        }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Full name")]
        public string FullName { get; set; }

        public string Email { get; set; }

        public IEnumerable<TweetViewModel> Tweets { get; set; }
    }
}