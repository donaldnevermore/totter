namespace Totter.Comments;

using Totter.Users;
using Totter.Tweets;
using Totter.Replies;

public class Comment {
    public long Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;
    public DateTime LastUpdated { get; set; }

    public long AuthorId { get; set; }
    public User? Author { get; set; }
    public long TweetId { get; set; }
    public Tweet? Tweet { get; set; }
    public List<Reply> Replies { get; set; } = new();
}
