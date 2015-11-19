using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Twitter.Models
{
    public class Tweet
    {
        private ICollection<UserRetweet> userretweet;
        private ICollection<User> favorites;
        private ICollection<Discussion> discussions;
 
        public Tweet()
        {
            this.userretweet = new HashSet<UserRetweet>();
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


// Self relation

//public int? MainTweetId { get; set; }

//public virtual Tweet MainTweet { get; set; }

//[InverseProperty("MainTweet")] 
//public virtual ICollection<Tweet> Replies
//{
//    get { return this.replies; }
//    set { this.replies = value; }
//}