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
    public class TblOrdersController : ControllerBase
    {
        private readonly abcDBContext _context;

        public TblOrdersController(abcDBContext context)
        {
            _context = context;
        }

        // GET: api/TblOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblOrder>>> GetTblOrder()
        {
            return await _context.TblOrder.ToListAsync();
        }
        [HttpGet("cart")]
        public ActionResult<IEnumerable<TblOrder>> GetTblOrderCart()
        {
            return _context.TblOrder.ToList().FindAll(x => x.Ordered == false);
        }
        // GET: api/TblOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblOrder>> GetTblOrder(int id)
        {
            var tblOrder = await _context.TblOrder.FindAsync(id);

            if (tblOrder == null)
            {
                return NotFound();
            }

            return tblOrder;
        }

        // PUT: api/TblOrders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblOrder(int id, TblOrder tblOrder)
        {
            if (id != tblOrder.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblOrderExists(id))
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

            List<TblOrder> orders = _context.TblOrder.ToList().FindAll(x => x.Cust== id && x.Ordered == false);
            foreach (TblOrder o in orders)
            {
                o.Ordered = true;
            }
            _context.TblOrder.UpdateRange(orders);


            return NoContent();
        }
        // POST: api/TblOrders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblOrder>> PostTblOrder(TblOrder tblOrder)
        {
            _context.TblOrder.Add(tblOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblOrder", new { id = tblOrder.Id }, tblOrder);
        }

        // DELETE: api/TblOrders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblOrder>> DeleteTblOrder(int id)
        {
            var tblOrder = await _context.TblOrder.FindAsync(id);
            if (tblOrder == null)
            {
                return NotFound();
            }

            _context.TblOrder.Remove(tblOrder);
            await _context.SaveChangesAsync();

            return tblOrder;
        }

        private bool TblOrderExists(int id)
        {
            return _context.TblOrder.Any(e => e.Id == id);
        }
    }
}
