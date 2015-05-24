using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Twitter.Models
{
    public class User : IdentityUser
    {
        private ICollection<Tweet> tweets;
        private ICollection<User> followers;
        private ICollection<User> following;
        private ICollection<Message> messagesInbox;
        private ICollection<Message> messagesSend;
        private ICollection<Tweet> favorties;
        private ICollection<UserRetweet> userretweet;

        public User()
        {
            this.tweets = new HashSet<Tweet>();
            this.followers = new HashSet<User>();
            this.following = new HashSet<User>();
            this.messagesInbox = new HashSet<Message>();
            this.messagesSend = new HashSet<Message>();
            this.favorties = new HashSet<Tweet>();
            this.userretweet = new HashSet<UserRetweet>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<Tweet> Tweets
        {
            get { return this.tweets; }
            set { this.tweets = value; }
        }

        public virtual ICollection<User> Followers
        {
            get { return this.followers; }
            set { this.followers = value; }
        }

        public virtual ICollection<User> Following
        {
            get { return this.following; }
            set { this.following = value; }
        }

        [InverseProperty("Recipient")]
        public virtual ICollection<Message> MessagesInbox
        {
            get { return this.messagesInbox; }
            set { this.messagesInbox = value; }
        }

        [InverseProperty("Sender")]
        public virtual ICollection<Message> MessagesSend
        {
            get { return this.messagesSend; }
            set { this.messagesSend = value; }
        }

        [InverseProperty("Favorites")]
        public virtual ICollection<Tweet> Favorties
        {
            get { return this.favorties; }
            set { this.favorties = value; }
        }

        public virtual ICollection<UserRetweet> UserRetweet
        {
            get { return this.userretweet; }
            set { this.userretweet = value; }
        }
    }
}