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
    [Route("api/PaquetesInformaticos")]
    public class PaquetesInformaticosController : Controller
    {
        private readonly SwTHDbContext _context;

        public PaquetesInformaticosController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/PaquetesInformaticos
        [HttpGet]
        public IEnumerable<PaquetesInformaticos> GetPaquetesInformaticos()
        {
            return _context.PaquetesInformaticos;
        }

        // GET: api/PaquetesInformaticos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaquetesInformaticos([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paquetesInformaticos = await _context.PaquetesInformaticos.SingleOrDefaultAsync(m => m.IdPaquetesInformaticos == id);

            if (paquetesInformaticos == null)
            {
                return NotFound();
            }

            return Ok(paquetesInformaticos);
        }

        // PUT: api/PaquetesInformaticos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaquetesInformaticos([FromRoute] int id, [FromBody] PaquetesInformaticos paquetesInformaticos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paquetesInformaticos.IdPaquetesInformaticos)
            {
                return BadRequest();
            }

            _context.Entry(paquetesInformaticos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaquetesInformaticosExists(id))
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

        // POST: api/PaquetesInformaticos
        [HttpPost]
        public async Task<IActionResult> PostPaquetesInformaticos([FromBody] PaquetesInformaticos paquetesInformaticos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PaquetesInformaticos.Add(paquetesInformaticos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaquetesInformaticos", new { id = paquetesInformaticos.IdPaquetesInformaticos }, paquetesInformaticos);
        }

        // DELETE: api/PaquetesInformaticos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaquetesInformaticos([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paquetesInformaticos = await _context.PaquetesInformaticos.SingleOrDefaultAsync(m => m.IdPaquetesInformaticos == id);
            if (paquetesInformaticos == null)
            {
                return NotFound();
            }

            _context.PaquetesInformaticos.Remove(paquetesInformaticos);
            await _context.SaveChangesAsync();

            return Ok(paquetesInformaticos);
        }

        private bool PaquetesInformaticosExists(int id)
        {
            return _context.PaquetesInformaticos.Any(e => e.IdPaquetesInformaticos == id);
        }
    }
}