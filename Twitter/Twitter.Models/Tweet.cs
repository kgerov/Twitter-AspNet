using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Twitter.Models
{
    public class Tweet
    {
        private ICollection<Tweet> retweets;
        private ICollection<User> favorites;
        private ICollection<Discussion> discussions;
 
        public Tweet()
        {
            this.retweets = new HashSet<Tweet>();
            this.favorites = new HashSet<User>();
            this.discussions = new HashSet<Discussion>();
        }

        public int Id { get; set; }

        [MaxLength(200), MinLength(1)]
        public string Content { get; set; }

        [Required]
        public DateTime DatePublished { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public bool IsReply { get; set; }

        public int? DiscussionId { get; set; }

        public virtual Discussion Discussion { get; set; }

        public virtual ICollection<Discussion> Discussions
        {
            get { return this.discussions; }
            set { this.discussions = value; }
        }

        public int? RetweetedTweetId { get; set; }

        public virtual Tweet RetweetedTweet { get; set; }

        [InverseProperty("RetweetedTweet")]
        public virtual ICollection<Tweet> Retweets
        {
            get { return this.retweets; }
            set { this.retweets = value; }
        }

        public virtual ICollection<User> Favorites
        {
            get { return this.favorites; }
            set { this.favorites = value; }
        }
    }
}