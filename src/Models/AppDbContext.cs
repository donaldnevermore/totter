namespace Totter.Models;

using Microsoft.EntityFrameworkCore;
using Totter.Tweets;
using Totter.Users;
using Totter.Comments;
using Totter.Replies;

public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Tweet> Tweets { get; set; }
    public DbSet<Comment> Comments { get; set; }
}
