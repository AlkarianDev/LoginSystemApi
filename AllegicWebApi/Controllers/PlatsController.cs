using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AllegicWebApi.Data;
using AllegicWebApi.Entities;
using Microsoft.AspNetCore.Authorization;

namespace AllegicWebApi.Controllers
{
    
    public class PlatsController : BaseApiController
    {
        private readonly ApplicationContext _context;

        public PlatsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Plats
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plat>>> GetPlats()
        {
          if (_context.Plats == null)
          {
              return NotFound();
          }
            return await _context.Plats.ToListAsync();
        }

        // GET: api/Plats/5
        [Authorize(Roles = "Member")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Plat>> GetPlat(int id)
        {
          if (_context.Plats == null)
          {
              return NotFound();
          }
            var plat = await _context.Plats.FindAsync(id);

            if (plat == null)
            {
                return NotFound();
            }

            return plat;
        }

        // PUT: api/Plats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlat(int id, Plat plat)
        {
            if (id != plat.Id)
            {
                return BadRequest();
            }

            _context.Entry(plat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatExists(id))
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

        // POST: api/Plats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Plat>> PostPlat(Plat plat)
        {
          if (_context.Plats == null)
          {
              return Problem("Entity set 'ApplicationContext.Plats'  is null.");
          }
            _context.Plats.Add(plat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlat", new { id = plat.Id }, plat);
        }

        // DELETE: api/Plats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlat(int id)
        {
            if (_context.Plats == null)
            {
                return NotFound();
            }
            var plat = await _context.Plats.FindAsync(id);
            if (plat == null)
            {
                return NotFound();
            }

            _context.Plats.Remove(plat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlatExists(int id)
        {
            return (_context.Plats?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
