using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PineGroveAPIServerless.Models;

namespace PineGroveAPIServerless.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrayerRequestsController : ControllerBase
    {
        private readonly PineGroveDatabaseContext _context;

        public PrayerRequestsController(PineGroveDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/PrayerRequests
        [HttpGet]
        public IEnumerable<PrayerRequest> GetPrayerRequest()
        {
            return _context.PrayerRequest;
        }

        // GET: api/PrayerRequests/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrayerRequest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prayerRequest = await _context.PrayerRequest.FindAsync(id);

            if (prayerRequest == null)
            {
                return NotFound();
            }

            return Ok(prayerRequest);
        }

        // PUT: api/PrayerRequests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrayerRequest([FromRoute] int id, [FromBody] PrayerRequest prayerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prayerRequest.PrayerId)
            {
                return BadRequest();
            }

            _context.Entry(prayerRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrayerRequestExists(id))
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

        // POST: api/PrayerRequests
        [HttpPost]
        public async Task<IActionResult> PostPrayerRequest([FromBody] PrayerRequest prayerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PrayerRequest.Add(prayerRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrayerRequest", new { id = prayerRequest.PrayerId }, prayerRequest);
        }

        // DELETE: api/PrayerRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrayerRequest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prayerRequest = await _context.PrayerRequest.FindAsync(id);
            if (prayerRequest == null)
            {
                return NotFound();
            }

            _context.PrayerRequest.Remove(prayerRequest);
            await _context.SaveChangesAsync();

            return Ok(prayerRequest);
        }

        private bool PrayerRequestExists(int id)
        {
            return _context.PrayerRequest.Any(e => e.PrayerId == id);
        }
    }
}