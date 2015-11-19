using System.Collections.Generic;

namespace Twitter.Web.Models
{
    public class HomeViewModel
    {
        public bool isLoggedIn { get; set; }

        public string UserName { get; set; }

        public int UserFollowers { get; set; }

        public int UserFollowings { get; set; }

        public int NumberOfTweets { get; set; }

        public ICollection<TweetViewModel> Tweets { get; set; }
    }
}