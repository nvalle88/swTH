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
    [Route("api/NivelesConocimiento")]
    public class NivelesConocimientoController : Controller
    {
        private readonly SwTHDbContext _context;

        public NivelesConocimientoController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/NivelesConocimiento
        [HttpGet]
        public IEnumerable<NivelConocimiento> GetNivelConocimiento()
        {
            return _context.NivelConocimiento;
        }

        // GET: api/NivelesConocimiento/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNivelConocimiento([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nivelConocimiento = await _context.NivelConocimiento.SingleOrDefaultAsync(m => m.IdNivelConocimiento == id);

            if (nivelConocimiento == null)
            {
                return NotFound();
            }

            return Ok(nivelConocimiento);
        }

        // PUT: api/NivelesConocimiento/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNivelConocimiento([FromRoute] int id, [FromBody] NivelConocimiento nivelConocimiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nivelConocimiento.IdNivelConocimiento)
            {
                return BadRequest();
            }

            _context.Entry(nivelConocimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NivelConocimientoExists(id))
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

        // POST: api/NivelesConocimiento
        [HttpPost]
        public async Task<IActionResult> PostNivelConocimiento([FromBody] NivelConocimiento nivelConocimiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.NivelConocimiento.Add(nivelConocimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNivelConocimiento", new { id = nivelConocimiento.IdNivelConocimiento }, nivelConocimiento);
        }

        // DELETE: api/NivelesConocimiento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNivelConocimiento([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nivelConocimiento = await _context.NivelConocimiento.SingleOrDefaultAsync(m => m.IdNivelConocimiento == id);
            if (nivelConocimiento == null)
            {
                return NotFound();
            }

            _context.NivelConocimiento.Remove(nivelConocimiento);
            await _context.SaveChangesAsync();

            return Ok(nivelConocimiento);
        }

        private bool NivelConocimientoExists(int id)
        {
            return _context.NivelConocimiento.Any(e => e.IdNivelConocimiento == id);
        }
    }
}