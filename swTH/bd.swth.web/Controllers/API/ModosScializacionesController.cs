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
    [Route("api/ModosScializaciones")]
    public class ModosScializacionesController : Controller
    {
        private readonly SwTHDbContext _context;

        public ModosScializacionesController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/ModosScializaciones
        [HttpGet]
        public IEnumerable<ModosScializacion> GetModosScializacion()
        {
            return _context.ModosScializacion;
        }

        // GET: api/ModosScializaciones/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModosScializacion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modosScializacion = await _context.ModosScializacion.SingleOrDefaultAsync(m => m.IdModosScializacion == id);

            if (modosScializacion == null)
            {
                return NotFound();
            }

            return Ok(modosScializacion);
        }

        // PUT: api/ModosScializaciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModosScializacion([FromRoute] int id, [FromBody] ModosScializacion modosScializacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modosScializacion.IdModosScializacion)
            {
                return BadRequest();
            }

            _context.Entry(modosScializacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModosScializacionExists(id))
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

        // POST: api/ModosScializaciones
        [HttpPost]
        public async Task<IActionResult> PostModosScializacion([FromBody] ModosScializacion modosScializacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ModosScializacion.Add(modosScializacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModosScializacion", new { id = modosScializacion.IdModosScializacion }, modosScializacion);
        }

        // DELETE: api/ModosScializaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModosScializacion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modosScializacion = await _context.ModosScializacion.SingleOrDefaultAsync(m => m.IdModosScializacion == id);
            if (modosScializacion == null)
            {
                return NotFound();
            }

            _context.ModosScializacion.Remove(modosScializacion);
            await _context.SaveChangesAsync();

            return Ok(modosScializacion);
        }

        private bool ModosScializacionExists(int id)
        {
            return _context.ModosScializacion.Any(e => e.IdModosScializacion == id);
        }
    }
}