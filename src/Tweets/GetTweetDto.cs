namespace Totter.Tweets;

using Totter.Users;

public class GetTweetDto {
    public long Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;
    public DateTime LastUpdated { get; set; }

    public long AuthorId { get; set; }
    public GetUserDto? Author { get; set; }
}
