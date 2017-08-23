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
    [Route("api/NacionalidadIndigenas")]
    public class NacionalidadIndigenasController : Controller
    {
        private readonly SwTHDbContext _context;

        public NacionalidadIndigenasController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/NacionalidadIndigenas
        [HttpGet]
        public IEnumerable<NacionalidadIndigena> GetNacionalidadIndigena()
        {
            return _context.NacionalidadIndigena;
        }

        // GET: api/NacionalidadIndigenas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNacionalidadIndigena([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nacionalidadIndigena = await _context.NacionalidadIndigena.SingleOrDefaultAsync(m => m.IdNacionalidadIndigena == id);

            if (nacionalidadIndigena == null)
            {
                return NotFound();
            }

            return Ok(nacionalidadIndigena);
        }

        // PUT: api/NacionalidadIndigenas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNacionalidadIndigena([FromRoute] int id, [FromBody] NacionalidadIndigena nacionalidadIndigena)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nacionalidadIndigena.IdNacionalidadIndigena)
            {
                return BadRequest();
            }

            _context.Entry(nacionalidadIndigena).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NacionalidadIndigenaExists(id))
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

        // POST: api/NacionalidadIndigenas
        [HttpPost]
        public async Task<IActionResult> PostNacionalidadIndigena([FromBody] NacionalidadIndigena nacionalidadIndigena)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.NacionalidadIndigena.Add(nacionalidadIndigena);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNacionalidadIndigena", new { id = nacionalidadIndigena.IdNacionalidadIndigena }, nacionalidadIndigena);
        }

        // DELETE: api/NacionalidadIndigenas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNacionalidadIndigena([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nacionalidadIndigena = await _context.NacionalidadIndigena.SingleOrDefaultAsync(m => m.IdNacionalidadIndigena == id);
            if (nacionalidadIndigena == null)
            {
                return NotFound();
            }

            _context.NacionalidadIndigena.Remove(nacionalidadIndigena);
            await _context.SaveChangesAsync();

            return Ok(nacionalidadIndigena);
        }

        private bool NacionalidadIndigenaExists(int id)
        {
            return _context.NacionalidadIndigena.Any(e => e.IdNacionalidadIndigena == id);
        }
    }
}