namespace Totter.Comments;

using Totter.Users;

public class CommentDTO {
    public long Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;
    public DateTime LastUpdated { get; set; }

    public long AuthorId { get; set; }
    public User? Author { get; set; }
    public long TweetId { get; set; }
}
