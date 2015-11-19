using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Twitter.Models
{
    public class Tweet
    {
        private ICollection<UserRetweet> userretweet;
        private ICollection<User> favorites;
        private ICollection<Tweet> replies;
 
        public Tweet()
        {
            this.userretweet = new HashSet<UserRetweet>();
            this.favorites = new HashSet<User>();
            this.replies = new HashSet<Tweet>();
        }

        public int Id { get; set; }

        [MaxLength(200), MinLength(1)]
        public string Content { get; set; }

        [Required]
        public DateTime DatePublished { get; set; }

        public bool IsReply { get; set; }

        public int? MainTweetId { get; set; }

        public virtual Tweet MainTweet { get; set; }

        [InverseProperty("MainTweet")] 
        public virtual ICollection<Tweet> Replies
        {
            get { return this.replies; }
            set { this.replies = value; }
        }

        public int? CommentTweetId { get; set; }

        public virtual Tweet CommentTweet { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<User> Favorites
        {
            get { return this.favorites; }
            set { this.favorites = value; }
        }

        public virtual ICollection<UserRetweet> UserRetweet
        {
            get { return this.userretweet; }
            set { this.userretweet = value; }
        }
    }
}
