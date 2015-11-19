namespace Twitter.Web.Models
{
    using System;
    using System.Linq.Expressions;
    using Twitter.Models;

    public class TweetViewModel
    {
        public static Expression<Func<Tweet, TweetViewModel>> ViewModel
        {
            get
            {
                return x => new TweetViewModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    DatePublished = x.DatePublished
                };
            }
        }

        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime DatePublished { get; set; }
    }
}