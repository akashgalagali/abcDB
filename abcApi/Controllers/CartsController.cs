using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using abcApi.Models;

namespace abcApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly abcDBContext _context;

        public CartsController(abcDBContext context)
        {
            _context = context;
        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetTblCart()
        {
            return await _context.Cart.ToListAsync();
        }
        [HttpGet("cartDetails/{id}")]
        public ActionResult<IEnumerable<Cart>> GetTblOrderCart(int id)
        {
            return _context.Cart.ToList().FindAll(x => x.Ordered == false && x.Cust==id);
        }
        [HttpGet("orderedDetails/{id}")]
        public ActionResult<IEnumerable<Cart>> GetPrevOrderCart(int id)
        {
            return _context.Cart.ToList().FindAll(x => x.Ordered == true && x.Cust == id);
        }
        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetTblCart(int id)
        {
            var Cart = await _context.Cart.FindAsync(id);

            if (Cart == null)
            {
                return NotFound();
            }

            return Cart;
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblCart(int id, Cart Cart)
        {
            if (id != Cart.Id)
            {
                return BadRequest();
            }

            _context.Entry(Cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblCartExists(id))
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
        [HttpPut("order/{id}")]
        public IActionResult PlaceOrder(int id)
        {

            List<Cart> orders = _context.Cart.ToList().FindAll(x => x.Cust == id && x.Ordered == false);
            foreach (Cart o in orders)
            {
                o.Ordered = true;
            }
            _context.Cart.UpdateRange(orders);
            _context.SaveChanges();

            return NoContent();
        }
        // POST: api/Carts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Cart>> PostTblCart(Cart Cart)
        {
            _context.Cart.Add(Cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblCart", new { id = Cart.Id }, Cart);
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cart>> DeleteTblCart(int id)
        {
            var Cart = await _context.Cart.FindAsync(id);
            if (Cart == null)
            {
                return NotFound();
            }

            _context.Cart.Remove(Cart);
            await _context.SaveChangesAsync();

            return Cart;
        }

        private bool TblCartExists(int id)
        {
            return _context.Cart.Any(e => e.Id == id);
        }
    }
}
