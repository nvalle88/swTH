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
    [Route("api/Exepciones")]
    public class ExepcionesController : Controller
    {
        private readonly SwTHDbContext _context;

        public ExepcionesController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/Exepciones
        [HttpGet]
        public IEnumerable<Exepciones> GetExepciones()
        {
            return _context.Exepciones;
        }

        // GET: api/Exepciones/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExepciones([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exepciones = await _context.Exepciones.SingleOrDefaultAsync(m => m.IdExepciones == id);

            if (exepciones == null)
            {
                return NotFound();
            }

            return Ok(exepciones);
        }

        // PUT: api/Exepciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExepciones([FromRoute] int id, [FromBody] Exepciones exepciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != exepciones.IdExepciones)
            {
                return BadRequest();
            }

            _context.Entry(exepciones).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExepcionesExists(id))
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

        // POST: api/Exepciones
        [HttpPost]
        public async Task<IActionResult> PostExepciones([FromBody] Exepciones exepciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Exepciones.Add(exepciones);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExepciones", new { id = exepciones.IdExepciones }, exepciones);
        }

        // DELETE: api/Exepciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExepciones([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exepciones = await _context.Exepciones.SingleOrDefaultAsync(m => m.IdExepciones == id);
            if (exepciones == null)
            {
                return NotFound();
            }

            _context.Exepciones.Remove(exepciones);
            await _context.SaveChangesAsync();

            return Ok(exepciones);
        }

        private bool ExepcionesExists(int id)
        {
            return _context.Exepciones.Any(e => e.IdExepciones == id);
        }
    }
}