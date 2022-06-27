namespace Totter.Controllers;

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
        return await db.Replies.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Reply>> GetReply(long id) {
        var reply = await db.Replies.FindAsync(id);

        if (reply == null) {
            return NotFound();
        }

        return reply;
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
        db.Replies.Add(reply);
        await db.SaveChangesAsync();

        return CreatedAtAction("GetReply", new { id = reply.Id }, reply);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReply(long id) {
        var reply = await db.Replies.FindAsync(id);
        if (reply == null) {
            return NotFound();
        }

        db.Replies.Remove(reply);
        await db.SaveChangesAsync();

        return NoContent();
    }

    private bool ReplyExists(long id) {
        return db.Replies.Any(e => e.Id == id);
    }
}
