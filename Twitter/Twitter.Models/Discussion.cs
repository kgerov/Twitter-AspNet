using System;
using System.Collections.Generic;

namespace Twitter.Models
{
    public class Discussion
    {
        private ICollection<Tweet> replies;

        public Discussion()
        {
            this.replies = new HashSet<Tweet>();
        }

        public int Id { get; set; }

        public int TweetId { get; set; }

        public virtual Tweet Tweet { get; set; }

        public DateTime DateStarted { get; set; }

        public virtual ICollection<Tweet> Replies
        {
            get { return this.replies; }
            set { this.replies = value; }
        }
    }
}