using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab1A.Models;

namespace Lab1A.Controllers
{
    [Produces("application/json")]
    [Route("api/DealershipApi")]
    public class DealershipApiController : Controller
    {
        private readonly CarContext _context;

        public DealershipApiController(CarContext context)
        {
            _context = context;
        }

        // GET: api/DealershipApi
        [HttpGet]
        public IEnumerable<Dealership> GetDealership()
        {
            return _context.Dealership;
        }

        // GET: api/DealershipApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDealership([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dealership = await _context.Dealership.SingleOrDefaultAsync(m => m.ID == id);

            if (dealership == null)
            {
                return NotFound();
            }

            return Ok(dealership);
        }

        // PUT: api/DealershipApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDealership([FromRoute] int id, [FromBody] Dealership dealership)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dealership.ID)
            {
                return BadRequest();
            }

            _context.Entry(dealership).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DealershipExists(id))
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

        // POST: api/DealershipApi
        [HttpPost]
        public async Task<IActionResult> PostDealership([FromBody] Dealership dealership)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Dealership.Add(dealership);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDealership", new { id = dealership.ID }, dealership);
        }

        // DELETE: api/DealershipApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDealership([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dealership = await _context.Dealership.SingleOrDefaultAsync(m => m.ID == id);
            if (dealership == null)
            {
                return NotFound();
            }

            _context.Dealership.Remove(dealership);
            await _context.SaveChangesAsync();

            return Ok(dealership);
        }

        private bool DealershipExists(int id)
        {
            return _context.Dealership.Any(e => e.ID == id);
        }
    }
}