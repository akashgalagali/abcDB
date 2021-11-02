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
    public class MedicinesController : ControllerBase
    {
        private readonly abcDBContext _context;

        public MedicinesController(abcDBContext context)
        {
            _context = context;
        }
        [HttpGet("aMedicines")]
        public ActionResult<IEnumerable<TblMedicine>> availableMedicines()
        {
            return _context.TblMedicines.ToList().FindAll(x => x.Available == true);
        }
        // GET: api/Medicines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblMedicine>>> GetTblMedicines()
        {
            return await _context.TblMedicines.ToListAsync();
        }
        [HttpGet("categories")]
        public ActionResult<IEnumerable<TblMedicine>> GroupMedicinesCategory()
        {
            IEnumerable<TblMedicine> medWithCat = _context.TblMedicines.OrderBy(x => x.CidId);
            return medWithCat.ToList();
        }
        // GET: api/Medicines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblMedicine>> GetTblMedicine(int id)
        {
            var tblMedicine = await _context.TblMedicines.FindAsync(id);

            if (tblMedicine == null)
            {
                return NotFound();
            }

            return tblMedicine;
        }  
        // GET: api/Medicines/name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<TblMedicine>> GetTblMedicine(string name)
        {
            var medi = await _context.TblMedicines.SingleOrDefaultAsync(x=>x.Name==name);
            

            if (medi == null)
            {
                return NotFound();
            }

            return medi;
        }

        // PUT: api/Medicines/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblMedicine(int id, TblMedicine tblMedicine)
        {
            if (id != tblMedicine.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblMedicine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblMedicineExists(id))
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

        // POST: api/Medicines
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblMedicine>> PostTblMedicine(TblMedicine tblMedicine)
        {
            _context.TblMedicines.Add(tblMedicine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblMedicine", new { id = tblMedicine.Id }, tblMedicine);
        }

        // DELETE: api/Medicines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblMedicine>> DeleteTblMedicine(int id)
        {
            var tblMedicine = await _context.TblMedicines.FindAsync(id);
            if (tblMedicine == null)
            {
                return NotFound();
            }

            _context.TblMedicines.Remove(tblMedicine);
            await _context.SaveChangesAsync();

            return tblMedicine;
        }

        private bool TblMedicineExists(int id)
        {
            return _context.TblMedicines.Any(e => e.Id == id);
        }
    }
}
