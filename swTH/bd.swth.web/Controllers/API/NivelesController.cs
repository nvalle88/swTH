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
    [Route("api/Niveles")]
    public class NivelesController : Controller
    {
        private readonly SwTHDbContext _context;

        public NivelesController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/Niveles
        [HttpGet]
        public IEnumerable<Nivel> GetNivel()
        {
            return _context.Nivel;
        }

        // GET: api/Niveles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNivel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nivel = await _context.Nivel.SingleOrDefaultAsync(m => m.IdNivel == id);

            if (nivel == null)
            {
                return NotFound();
            }

            return Ok(nivel);
        }

        // PUT: api/Niveles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNivel([FromRoute] int id, [FromBody] Nivel nivel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nivel.IdNivel)
            {
                return BadRequest();
            }

            _context.Entry(nivel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NivelExists(id))
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

        // POST: api/Niveles
        [HttpPost]
        public async Task<IActionResult> PostNivel([FromBody] Nivel nivel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Nivel.Add(nivel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNivel", new { id = nivel.IdNivel }, nivel);
        }

        // DELETE: api/Niveles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNivel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nivel = await _context.Nivel.SingleOrDefaultAsync(m => m.IdNivel == id);
            if (nivel == null)
            {
                return NotFound();
            }

            _context.Nivel.Remove(nivel);
            await _context.SaveChangesAsync();

            return Ok(nivel);
        }

        private bool NivelExists(int id)
        {
            return _context.Nivel.Any(e => e.IdNivel == id);
        }
    }
}