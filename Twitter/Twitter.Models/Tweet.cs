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

        public Tweet()
        {
            this.userretweet = new HashSet<UserRetweet>();
            this.favorites = new HashSet<User>();
        }

        public int Id { get; set; }

        [MaxLength(200), MinLength(1)]
        public string Content { get; set; }

        [Required]
        public DateTime DatePublished { get; set; }

        public bool IsReply { get; set; }

        [Required]
        public string PublisherId { get; set; }

        public virtual User Publisher { get; set; }

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
