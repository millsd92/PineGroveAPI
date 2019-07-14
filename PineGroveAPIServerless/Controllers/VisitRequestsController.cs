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
    public class VisitRequestsController : ControllerBase
    {
        private readonly PineGroveDatabaseContext _context;

        public VisitRequestsController(PineGroveDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/VisitRequests
        [HttpGet]
        public IEnumerable<VisitRequest> GetVisitRequest()
        {
            return _context.VisitRequest;
        }

        // GET: api/VisitRequests/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVisitRequest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var visitRequest = await _context.VisitRequest.FindAsync(id);

            if (visitRequest == null)
            {
                return NotFound();
            }

            return Ok(visitRequest);
        }

        // PUT: api/VisitRequests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisitRequest([FromRoute] int id, [FromBody] VisitRequest visitRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != visitRequest.VisitId)
            {
                return BadRequest();
            }

            _context.Entry(visitRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitRequestExists(id))
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

        // POST: api/VisitRequests
        [HttpPost]
        public async Task<IActionResult> PostVisitRequest([FromBody] VisitRequest visitRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.VisitRequest.Add(visitRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVisitRequest", new { id = visitRequest.VisitId }, visitRequest);
        }

        // DELETE: api/VisitRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitRequest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var visitRequest = await _context.VisitRequest.FindAsync(id);
            if (visitRequest == null)
            {
                return NotFound();
            }

            _context.VisitRequest.Remove(visitRequest);
            await _context.SaveChangesAsync();

            return Ok(visitRequest);
        }

        private bool VisitRequestExists(int id)
        {
            return _context.VisitRequest.Any(e => e.VisitId == id);
        }
    }
}