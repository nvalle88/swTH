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
    [Route("api/Nacionalidades")]
    public class NacionalidadesController : Controller
    {
        private readonly SwTHDbContext _context;

        public NacionalidadesController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/Nacionalidades
        [HttpGet]
        public IEnumerable<Nacionalidad> GetNacionalidad()
        {
            return _context.Nacionalidad;
        }

        // GET: api/Nacionalidades/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNacionalidad([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nacionalidad = await _context.Nacionalidad.SingleOrDefaultAsync(m => m.IdNacionalidad == id);

            if (nacionalidad == null)
            {
                return NotFound();
            }

            return Ok(nacionalidad);
        }

        // PUT: api/Nacionalidades/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNacionalidad([FromRoute] int id, [FromBody] Nacionalidad nacionalidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nacionalidad.IdNacionalidad)
            {
                return BadRequest();
            }

            _context.Entry(nacionalidad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NacionalidadExists(id))
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

        // POST: api/Nacionalidades
        [HttpPost]
        public async Task<IActionResult> PostNacionalidad([FromBody] Nacionalidad nacionalidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Nacionalidad.Add(nacionalidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNacionalidad", new { id = nacionalidad.IdNacionalidad }, nacionalidad);
        }

        // DELETE: api/Nacionalidades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNacionalidad([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nacionalidad = await _context.Nacionalidad.SingleOrDefaultAsync(m => m.IdNacionalidad == id);
            if (nacionalidad == null)
            {
                return NotFound();
            }

            _context.Nacionalidad.Remove(nacionalidad);
            await _context.SaveChangesAsync();

            return Ok(nacionalidad);
        }

        private bool NacionalidadExists(int id)
        {
            return _context.Nacionalidad.Any(e => e.IdNacionalidad == id);
        }
    }
}