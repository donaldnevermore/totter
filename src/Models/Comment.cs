using System;
using Totter.Users;
using Totter.Tweets;

namespace Totter.Models {
    public class Comment {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public DateTime LastUpdated { get; set; }

        public long AuthorId { get; set; }
        public Tweet Tweet { get; set; }
        public User Author { get; set; }
        public long TweetId { get; set; }
    }
}
