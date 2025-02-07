using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquityAttributesController : ControllerBase
    {
        private readonly ProjectContext _context;

        public EquityAttributesController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/EquityAttributes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquityAttribute>>> GetEquityAttributes()
        {
            return await _context.EquityAttributes.ToListAsync();
        }

        // GET: api/EquityAttributes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EquityAttribute>> GetEquityAttribute(int id)
        {
            var equityAttribute = await _context.EquityAttributes.FindAsync(id);

            if (equityAttribute == null)
            {
                return NotFound();
            }

            return equityAttribute;
        }

        // PUT: api/EquityAttributes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquityAttribute(int id, EquityAttribute equityAttribute)
        {
            if (id != equityAttribute.Aid)
            {
                return BadRequest();
            }

            _context.Entry(equityAttribute).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquityAttributeExists(id))
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

        // POST: api/EquityAttributes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EquityAttribute>> PostEquityAttribute(EquityAttribute equityAttribute)
        {
            _context.EquityAttributes.Add(equityAttribute);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEquityAttribute", new { id = equityAttribute.Aid }, equityAttribute);
        }

        // DELETE: api/EquityAttributes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquityAttribute(int id)
        {
            var equityAttribute = await _context.EquityAttributes.FindAsync(id);
            if (equityAttribute == null)
            {
                return NotFound();
            }

            _context.EquityAttributes.Remove(equityAttribute);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EquityAttributeExists(int id)
        {
            return _context.EquityAttributes.Any(e => e.Aid == id);
        }
    }
}
