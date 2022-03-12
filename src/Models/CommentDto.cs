namespace Totter.Models {
    public class CommentDto {
        public string Content { get; set; }
        public long AuthorId { get; set; }
        public long TweetId { get; set; }
    }
}
