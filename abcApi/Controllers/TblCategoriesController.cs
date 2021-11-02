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
    public class TblCategoriesController : ControllerBase
    {
        private readonly abcDBContext _context;

        public TblCategoriesController(abcDBContext context)
        {
            _context = context;
        }

     

        // GET: api/TblCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblCategory>>> GetTblCategories()
        {
            return await _context.TblCategories.ToListAsync();
        }

        // GET: api/TblCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblCategory>> GetTblCategory(int id)
        {
            var tblCategory = await _context.TblCategories.FindAsync(id);

            if (tblCategory == null)
            {
                return NotFound();
            }

            return tblCategory;
        }

        // PUT: api/TblCategories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblCategory(int id, TblCategory tblCategory)
        {
            if (id != tblCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblCategoryExists(id))
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

        // POST: api/TblCategories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblCategory>> PostTblCategory(TblCategory tblCategory)
        {
            _context.TblCategories.Add(tblCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblCategory", new { id = tblCategory.Id }, tblCategory);
        }

        // DELETE: api/TblCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblCategory>> DeleteTblCategory(int id)
        {
            var tblCategory = await _context.TblCategories.FindAsync(id);
            if (tblCategory == null)
            {
                return NotFound();
            }

            _context.TblCategories.Remove(tblCategory);
            await _context.SaveChangesAsync();

            return tblCategory;
        }

        private bool TblCategoryExists(int id)
        {
            return _context.TblCategories.Any(e => e.Id == id);
        }
    }
}
