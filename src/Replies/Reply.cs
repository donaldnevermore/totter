namespace Totter.Replies;

using Totter.Users;
using Totter.Comments;

public class Reply {
    public long Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public long CommentId { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public DateTime LastUpdated { get; set; }

    public long AuthorId { get; set; }
    public User? Author { get; set; }

    public long ReplyToId { get; set; }
    public User? ReplyTo { get; set; }
}
