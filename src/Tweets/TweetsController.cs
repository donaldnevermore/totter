namespace Totter.Tweets;

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Totter.Models;

[Route("api/tweets")]
[ApiController]
public class TweetsController : ControllerBase {
    private readonly AppDbContext db;
    private readonly IMapper mapper;

    public TweetsController(AppDbContext db, IMapper mapper) {
        this.db = db;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetTweetDTO>>> GetTweets() {
        var tweets = await db.Tweets.Include(t => t.Author).ToListAsync();
        var dto = mapper.Map<List<GetTweetDTO>>(tweets);
        return dto;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetTweetDTO>> GetTweet(long id) {
        var tweet = await db.Tweets.Where(t => t.Id == id)
            .Include(t => t.Author)
            .SingleOrDefaultAsync();

        if (tweet is null) {
            return NotFound();
        }

        var dto = mapper.Map<GetTweetDTO>(tweet);
        return dto;
    }

    // [Authorize]
    [HttpPost("{id}")]
    public async Task<IActionResult> UpdateTweet(long id, TweetDTO tweetInfo) {
        var tweet = await db.Tweets.FindAsync(id);
        if (tweet is null) {
            return NotFound();
        }

        var author = await db.Users.FindAsync(tweetInfo.AuthorId);
        if (author is null) {
            return BadRequest(new {
                error = "Invalid parameters.",
                message = $"User ID {tweetInfo.AuthorId} not found."
            });
        }

        tweet.Content = tweetInfo.Content;
        tweet.LastUpdated = DateTime.Now;

        try {
            await db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!TweetExists(id)) {
            return NotFound();
        }

        return NoContent();
    }

    // [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddTweet(TweetDTO tweetInfo) {
        var author = await db.Users.FindAsync(tweetInfo.AuthorId);
        if (author is null) {
            return BadRequest(new {
                error = "Invalid parameters.",
                message = $"User Id {tweetInfo.AuthorId} not found."
            });
        }

        var tweet = new Tweet {
            Content = tweetInfo.Content,
            Author = author,
            LastUpdated = DateTime.Now
        };

        db.Tweets.Add(tweet);
        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTweet), new { id = tweet.Id }, new { id = tweet.Id });
    }

    // [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTweet(long id) {
        var tweet = await db.Tweets.FindAsync(id);
        if (tweet is null) {
            return NotFound();
        }

        db.Tweets.Remove(tweet);
        await db.SaveChangesAsync();

        return NoContent();
    }

    private bool TweetExists(long id) {
        return db.Tweets.Any(e => e.Id == id);
    }
}
