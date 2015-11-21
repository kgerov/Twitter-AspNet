using PagedList;

namespace Twitter.Web.Models
{
    public class HomeViewModel
    {
        public int UserFollowers { get; set; }

        public int UserFollowings { get; set; }

        public int NumberOfTweets { get; set; }

        public IPagedList<TweetViewModel> Tweets { get; set; }
    }
}