using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Totter.Models;

namespace Totter.Tweets {
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

        [HttpPost("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTweet(long id, TweetDTO tweetInfo) {
            var tweet = await db.Tweets.FindAsync(id);
            if (tweet is null) {
                return NotFound();
            }

            var author = await db.Users.FindAsync(tweetInfo.AuthorId);
            if (author is null) {
                return BadRequest(new {
                    error = "Invalid parameters.", message = $"User ID {tweetInfo.AuthorId} not found."
                });
            }

            tweet.Title = tweetInfo.Title;
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddTweet(TweetDTO tweetInfo) {
            var author = await db.Users.FindAsync(tweetInfo.AuthorId);
            if (author is null) {
                return BadRequest(new {
                    error = "Invalid parameters.", message = $"User Id {tweetInfo.AuthorId} not found."
                });
            }

            var tweet = new Tweet {
                Content = tweetInfo.Content,
                Title = tweetInfo.Title,
                Author = author,
                LastUpdated = DateTime.Now
            };

            db.Tweets.Add(tweet);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTweet), new { id = tweet.Id }, new { id = tweet.Id });
        }

        [HttpDelete("{id}")]
        [Authorize]
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
}
