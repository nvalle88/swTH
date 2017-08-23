using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Misions")]
    public class MisionsController : Controller
    {
        private readonly SwTHDbContext _context;

        public MisionsController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/Misions
        [HttpGet]
        public IEnumerable<Mision> GetMision()
        {
            return _context.Mision;
        }

        // GET: api/Misions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMision([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mision = await _context.Mision.SingleOrDefaultAsync(m => m.IdMision == id);

            if (mision == null)
            {
                return NotFound();
            }

            return Ok(mision);
        }

        // PUT: api/Misions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMision([FromRoute] int id, [FromBody] Mision mision)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mision.IdMision)
            {
                return BadRequest();
            }

            _context.Entry(mision).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MisionExists(id))
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

        // POST: api/Misions
        [HttpPost]
        public async Task<IActionResult> PostMision([FromBody] Mision mision)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Mision.Add(mision);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMision", new { id = mision.IdMision }, mision);
        }

        // DELETE: api/Misions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMision([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mision = await _context.Mision.SingleOrDefaultAsync(m => m.IdMision == id);
            if (mision == null)
            {
                return NotFound();
            }

            _context.Mision.Remove(mision);
            await _context.SaveChangesAsync();

            return Ok(mision);
        }

        private bool MisionExists(int id)
        {
            return _context.Mision.Any(e => e.IdMision == id);
        }
    }
}