namespace Totter.Replies;

public class ReplyDTO {
    public string Content { get; set; } = string.Empty;
    public long AuthorId { get; set; }
    public long CommentId { get; set; }
    public long ReplyToUserId { get; set; }
}
