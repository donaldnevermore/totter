using System;
using Totter.Users;

namespace Totter.Models {
    public class Reply {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public DateTime LastUpdated { get; set; }

        public long? ReplyToId { get; set; }
        public User? ReplyTo { get; set; }

        public long AuthorId { get; set; }
        public User Author { get; set; }
        public long CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
