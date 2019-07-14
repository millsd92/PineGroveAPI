using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PineGroveAPIServerless.Models;

namespace PineGroveAPIServerless.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementRequestsController : ControllerBase
    {
        private readonly PineGroveDatabaseContext _context;

        public AnnouncementRequestsController(PineGroveDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/AnnouncementRequests
        [HttpGet]
        public IEnumerable<AnnouncementRequest> GetAnnouncementRequest()
        {
            return _context.AnnouncementRequest;
        }

        // GET: api/AnnouncementRequests/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnnouncementRequest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var announcementRequest = await _context.AnnouncementRequest.FindAsync(id);

            if (announcementRequest == null)
            {
                return NotFound();
            }

            return Ok(announcementRequest);
        }

        // PUT: api/AnnouncementRequests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnouncementRequest([FromRoute] int id, [FromBody] AnnouncementRequest announcementRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != announcementRequest.AnnouncementId)
            {
                return BadRequest();
            }

            _context.Entry(announcementRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnouncementRequestExists(id))
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

        // POST: api/AnnouncementRequests
        [HttpPost]
        public async Task<IActionResult> PostAnnouncementRequest([FromBody] AnnouncementRequest announcementRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AnnouncementRequest.Add(announcementRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnnouncementRequest", new { id = announcementRequest.AnnouncementId }, announcementRequest);
        }

        // DELETE: api/AnnouncementRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncementRequest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var announcementRequest = await _context.AnnouncementRequest.FindAsync(id);
            if (announcementRequest == null)
            {
                return NotFound();
            }

            _context.AnnouncementRequest.Remove(announcementRequest);
            await _context.SaveChangesAsync();

            return Ok(announcementRequest);
        }

        private bool AnnouncementRequestExists(int id)
        {
            return _context.AnnouncementRequest.Any(e => e.AnnouncementId == id);
        }
    }
}