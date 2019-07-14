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
    public class EventRegistrationsController : ControllerBase
    {
        private readonly PineGroveDatabaseContext _context;

        public EventRegistrationsController(PineGroveDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/EventRegistrations
        [HttpGet]
        public IEnumerable<EventRegistration> GetEventRegistration()
        {
            return _context.EventRegistration;
        }

        // GET: api/EventRegistrations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventRegistration([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventRegistration = await _context.EventRegistration.FindAsync(id);

            if (eventRegistration == null)
            {
                return NotFound();
            }

            return Ok(eventRegistration);
        }

        // PUT: api/EventRegistrations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventRegistration([FromRoute] int id, [FromBody] EventRegistration eventRegistration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventRegistration.EventRegistrationId)
            {
                return BadRequest();
            }

            _context.Entry(eventRegistration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventRegistrationExists(id))
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

        // POST: api/EventRegistrations
        [HttpPost]
        public async Task<IActionResult> PostEventRegistration([FromBody] EventRegistration eventRegistration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.EventRegistration.Add(eventRegistration);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventRegistration", new { id = eventRegistration.EventRegistrationId }, eventRegistration);
        }

        // DELETE: api/EventRegistrations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventRegistration([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventRegistration = await _context.EventRegistration.FindAsync(id);
            if (eventRegistration == null)
            {
                return NotFound();
            }

            _context.EventRegistration.Remove(eventRegistration);
            await _context.SaveChangesAsync();

            return Ok(eventRegistration);
        }

        private bool EventRegistrationExists(int id)
        {
            return _context.EventRegistration.Any(e => e.EventRegistrationId == id);
        }
    }
}