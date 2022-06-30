namespace Totter.Replies;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Totter.Models;

[Route("api/replies")]
[ApiController]
public class RepliesController : ControllerBase {
    private readonly AppDbContext db;

    public RepliesController(AppDbContext db) {
        this.db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reply>>> GetReplies() {
        return NotFound();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Reply>> GetReply(long id) {
        return NotFound();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutReply(long id, Reply reply) {
        if (id != reply.Id) {
            return BadRequest();
        }

        db.Entry(reply).State = EntityState.Modified;

        try {
            await db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) {
            if (!ReplyExists(id)) {
                return NotFound();
            }
            else {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Reply>> PostReply(Reply reply) {
        var comment = await db.Comments.FindAsync(reply.CommentId);
        if (comment is null) {
            return BadRequest();
        }
        var author = await db.Users.FindAsync(reply.AuthorId);
        if (author is null) {
            return BadRequest();
        }
        var replyTo = await db.Users.FindAsync(reply.ReplyToId);

        var r = new Reply {
            Content = reply.Content,
            LastUpdated = DateTime.Now,
            Author = author,
            CommentId = comment.Id,
            ReplyTo = replyTo
        };
        comment.Replies.Add(r);
        await db.SaveChangesAsync();

        return CreatedAtAction("GetReply", new { id = r.Id }, new { id = r.Id });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReply(long id) {
        return NoContent();
    }

    private bool ReplyExists(long id) {
        return false;
    }
}
