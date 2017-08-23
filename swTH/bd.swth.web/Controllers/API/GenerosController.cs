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
    [Route("api/Generos")]
    public class GenerosController : Controller
    {
        private readonly SwTHDbContext _context;

        public GenerosController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/Generos
        [HttpGet]
        public IEnumerable<Genero> GetGenero()
        {
            return _context.Genero;
        }

        // GET: api/Generos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenero([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genero = await _context.Genero.SingleOrDefaultAsync(m => m.IdGenero == id);

            if (genero == null)
            {
                return NotFound();
            }

            return Ok(genero);
        }

        // PUT: api/Generos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenero([FromRoute] int id, [FromBody] Genero genero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != genero.IdGenero)
            {
                return BadRequest();
            }

            _context.Entry(genero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneroExists(id))
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

        // POST: api/Generos
        [HttpPost]
        public async Task<IActionResult> PostGenero([FromBody] Genero genero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Genero.Add(genero);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGenero", new { id = genero.IdGenero }, genero);
        }

        // DELETE: api/Generos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenero([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genero = await _context.Genero.SingleOrDefaultAsync(m => m.IdGenero == id);
            if (genero == null)
            {
                return NotFound();
            }

            _context.Genero.Remove(genero);
            await _context.SaveChangesAsync();

            return Ok(genero);
        }

        private bool GeneroExists(int id)
        {
            return _context.Genero.Any(e => e.IdGenero == id);
        }
    }
}