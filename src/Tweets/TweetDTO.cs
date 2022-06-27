namespace Totter.Tweets;

public class TweetDTO {
    public long AuthorId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
