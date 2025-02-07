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
    public class EquityTabsController : ControllerBase
    {
        private readonly ProjectContext _context;

        public EquityTabsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/EquityTabs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquityTab>>> GetEquityTabs()
        {
            return await _context.EquityTabs.ToListAsync();
        }

        // GET: api/EquityTabs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EquityTab>> GetEquityTab(int id)
        {
            var equityTab = await _context.EquityTabs.FindAsync(id);

            if (equityTab == null)
            {
                return NotFound();
            }

            return equityTab;
        }

        // PUT: api/EquityTabs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquityTab(int id, EquityTab equityTab)
        {
            if (id != equityTab.TabId)
            {
                return BadRequest();
            }

            _context.Entry(equityTab).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquityTabExists(id))
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

        // POST: api/EquityTabs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EquityTab>> PostEquityTab(EquityTab equityTab)
        {
            _context.EquityTabs.Add(equityTab);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEquityTab", new { id = equityTab.TabId }, equityTab);
        }

        // DELETE: api/EquityTabs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquityTab(int id)
        {
            var equityTab = await _context.EquityTabs.FindAsync(id);
            if (equityTab == null)
            {
                return NotFound();
            }

            _context.EquityTabs.Remove(equityTab);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EquityTabExists(int id)
        {
            return _context.EquityTabs.Any(e => e.TabId == id);
        }
    }
}
