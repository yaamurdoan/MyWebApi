using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PublisherController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PublisherController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Publisher
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
    {
        return await _context.Publishers.ToListAsync();
    }

    // GET: api/Publisher/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Publisher>> GetPublisher(int id)
    {
        var publisher = await _context.Publishers.FindAsync(id);

        if (publisher == null)
        {
            return NotFound();
        }

        return publisher;
    }

    // POST: api/Publisher
    [HttpPost]
    public async Task<ActionResult<Publisher>> PostPublisher(Publisher publisher)
    {
        _context.Publishers.Add(publisher);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPublisher), new { id = publisher.Id }, publisher);
    }

    // PUT: api/Publisher/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPublisher(int id, Publisher publisher)
    {
        if (id != publisher.Id)
        {
            return BadRequest();
        }

        _context.Entry(publisher).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PublisherExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Publisher/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePublisher(int id)
    {
        var publisher = await _context.Publishers.FindAsync(id);
        if (publisher == null)
        {
            return NotFound();
        }

        _context.Publishers.Remove(publisher);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PublisherExists(int id)
    {
        return _context.Publishers.Any(e => e.Id == id);
    }
}

