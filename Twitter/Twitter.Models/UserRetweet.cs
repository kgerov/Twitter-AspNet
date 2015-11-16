using System;
using System.ComponentModel.DataAnnotations;

namespace Twitter.Models
{
    public class UserRetweet
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public int TweetId { get; set; }

        public string Comment { get; set; }

        public virtual Tweet Tweet { get; set; }

        [Required]
        public DateTime DateRetweeted { get; set; }
    }
}
