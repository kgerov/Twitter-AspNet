﻿using System.Collections.Generic;
using System.Linq;

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
                    DatePublished = x.DatePublished,
                    Retweets = x.Retweets.Count,
                    FavoritesCount = x.Favorites.Count,
                    Name = x.User.FullName,
                    UserName = x.User.UserName,
                    Image = x.User.Image.Photo
                };
            }
        }

        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime DatePublished { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public int FavoritesCount { get; set; }

        public int Retweets { get; set; }

        public bool IsLikedByUser { get; set; }
    }
}