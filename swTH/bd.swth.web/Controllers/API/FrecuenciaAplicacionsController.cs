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
    [Route("api/FrecuenciaAplicacions")]
    public class FrecuenciaAplicacionsController : Controller
    {
        private readonly SwTHDbContext _context;

        public FrecuenciaAplicacionsController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/FrecuenciaAplicacions
        [HttpGet]
        public IEnumerable<FrecuenciaAplicacion> GetFrecuenciaAplicacion()
        {
            return _context.FrecuenciaAplicacion;
        }

        // GET: api/FrecuenciaAplicacions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFrecuenciaAplicacion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var frecuenciaAplicacion = await _context.FrecuenciaAplicacion.SingleOrDefaultAsync(m => m.IdFrecuenciaAplicacion == id);

            if (frecuenciaAplicacion == null)
            {
                return NotFound();
            }

            return Ok(frecuenciaAplicacion);
        }

        // PUT: api/FrecuenciaAplicacions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFrecuenciaAplicacion([FromRoute] int id, [FromBody] FrecuenciaAplicacion frecuenciaAplicacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != frecuenciaAplicacion.IdFrecuenciaAplicacion)
            {
                return BadRequest();
            }

            _context.Entry(frecuenciaAplicacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FrecuenciaAplicacionExists(id))
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

        // POST: api/FrecuenciaAplicacions
        [HttpPost]
        public async Task<IActionResult> PostFrecuenciaAplicacion([FromBody] FrecuenciaAplicacion frecuenciaAplicacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FrecuenciaAplicacion.Add(frecuenciaAplicacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFrecuenciaAplicacion", new { id = frecuenciaAplicacion.IdFrecuenciaAplicacion }, frecuenciaAplicacion);
        }

        // DELETE: api/FrecuenciaAplicacions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFrecuenciaAplicacion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var frecuenciaAplicacion = await _context.FrecuenciaAplicacion.SingleOrDefaultAsync(m => m.IdFrecuenciaAplicacion == id);
            if (frecuenciaAplicacion == null)
            {
                return NotFound();
            }

            _context.FrecuenciaAplicacion.Remove(frecuenciaAplicacion);
            await _context.SaveChangesAsync();

            return Ok(frecuenciaAplicacion);
        }

        private bool FrecuenciaAplicacionExists(int id)
        {
            return _context.FrecuenciaAplicacion.Any(e => e.IdFrecuenciaAplicacion == id);
        }
    }
}