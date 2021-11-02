using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using abcApi.Models;
using Microsoft.AspNetCore.Cors;

namespace abcApi.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly abcDBContext _context;

        public UsersController(abcDBContext context)
        {
            _context = context;
        }

     

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblUser>>> GetTblUsers()
        {
            return await _context.TblUsers.ToListAsync();
        }
        public bool validateUser(string email, string password, string role)
        {
            List<TblUser> users = _context.TblUsers.ToList();
            return users.Exists(x => x.Email == email && x.Password == password && x.Role == role);
        }
        [HttpGet("login/{email}/{password}/{role}")]
        public TblUser login(string email, string password,string role)
        {
            TblUser user = null;
            
            if (validateUser(email,password,role))
            {
                user = _context.TblUsers.ToList().Single(x => x.Email == email);
            }
            

            return user;
        }
        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblUser>> GetTblUser(int id)
        {
            var tblUser = await _context.TblUsers.FindAsync(id);

            if (tblUser == null)
            {
                return NotFound();
            }

            return tblUser;
        }
        [HttpGet("email/{email}")]
        public async Task<ActionResult<TblUser>> GetTblUser(string email)
        {
            var user = await _context.TblUsers.SingleOrDefaultAsync(x => x.Email == email);


            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblUser(int id, TblUser tblUser)
        {
            if (id != tblUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblUserExists(id))
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblUser>> PostTblUser(TblUser tblUser)
        {
            _context.TblUsers.Add(tblUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblUser", new { id = tblUser.Id }, tblUser);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblUser>> DeleteTblUser(int id)
        {
            var tblUser = await _context.TblUsers.FindAsync(id);
            if (tblUser == null)
            {
                return NotFound();
            }

            _context.TblUsers.Remove(tblUser);
            await _context.SaveChangesAsync();

            return tblUser;
        }

        private bool TblUserExists(int id)
        {
            return _context.TblUsers.Any(e => e.Id == id);
        }
    }
}
