using System;
using Totter.Users;

namespace Totter.Tweets {
    public class Tweet {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public DateTime LastUpdated { get; set; }

        public long AuthorId { get; set; }
        public User Author { get; set; }
    }
}
