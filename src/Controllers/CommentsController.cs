using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Totter.Models;

namespace Totter.Controllers {
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase {
        private readonly AppDbContext db;

        public CommentsController(AppDbContext db) {
            this.db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments() {
            return await db.Comments.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(long id) {
            var comment = await db.Comments.FindAsync(id);

            if (comment == null) {
                return NotFound();
            }

            return comment;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(long id, Comment comment) {
            if (id != comment.Id) {
                return BadRequest();
            }

            db.Entry(comment).State = EntityState.Modified;

            try {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!CommentExists(id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(CommentDto commentDto) {
            var tweet = await db.Tweets.FindAsync(commentDto.TweetId);
            if (tweet is null) {
                return BadRequest();
            }

            var author = await db.Users.FindAsync(commentDto.AuthorId);
            if (author is null) {
                return BadRequest();
            }

            var comment = new Comment {
                Content = commentDto.Content, LastUpdated = DateTime.Now, Author = author, Tweet = tweet
            };
            db.Comments.Add(comment);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(long id) {
            var comment = await db.Comments.FindAsync(id);
            if (comment == null) {
                return NotFound();
            }

            db.Comments.Remove(comment);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(long id) {
            return db.Comments.Any(e => e.Id == id);
        }
    }
}
