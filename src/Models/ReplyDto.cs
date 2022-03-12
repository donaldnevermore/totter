namespace Totter.Models {
    public class ReplyDto {
        public string Content { get; set; }
        public long AuthorId { get; set; }
        public long CommentId { get; set; }
        public long? ReplyToUserId { get; set; }
    }
}
