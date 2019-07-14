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
    public class UsersController : ControllerBase
    {
        private readonly PineGroveDatabaseContext _context;

        public UsersController(PineGroveDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/Users/GetNames?firstName=David&lastName=Mills
        [HttpGet("GetNames")]
        public async Task<ActionResult<IEnumerable<User>>> GetNames([FromQuery]string firstName = "", [FromQuery]string lastName = "")
        {
            List<User> users = new List<User>();

            if (firstName != null && firstName.Length != 0)
            {
                if (lastName != null && lastName.Length != 0)
                {
                    foreach (User user in await _context.User.ToListAsync())
                    {
                        if (user.FirstName.Equals(firstName) && user.LastName.Equals(lastName))
                            users.Add(user);
                    }
                    if (users.Count > 0)
                        return users;
                    else
                        return NotFound();
                }
                
                foreach (User user in await _context.User.ToListAsync())
                {
                    if (user.FirstName.Equals(firstName))
                        users.Add(user);
                }
                if (users.Count > 0)
                    return users;
                else
                    return NotFound();
            }
            if (lastName != null && lastName.Length != 0)
            {
                foreach (User user in await _context.User.ToListAsync())
                {
                    if (user.LastName.Equals(lastName))
                        users.Add(user);
                }
                if (users.Count > 0)
                    return users;
                else
                    return NotFound();
            }
            return users;
        }

        // GET: api/Users/MILDAV_01
        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetUser(string username)
        {
            var user = await _context.User.FirstOrDefaultAsync(e => e.UserName.Equals(username));

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserId == id);
        }
    }
}
