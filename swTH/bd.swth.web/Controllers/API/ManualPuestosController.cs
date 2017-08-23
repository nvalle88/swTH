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
    [Route("api/ManualPuestos")]
    public class ManualPuestosController : Controller
    {
        private readonly SwTHDbContext _context;

        public ManualPuestosController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/ManualPuestos
        [HttpGet]
        public IEnumerable<ManualPuesto> GetManualPuesto()
        {
            return _context.ManualPuesto;
        }

        // GET: api/ManualPuestos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetManualPuesto([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var manualPuesto = await _context.ManualPuesto.SingleOrDefaultAsync(m => m.IdManualPuesto == id);

            if (manualPuesto == null)
            {
                return NotFound();
            }

            return Ok(manualPuesto);
        }

        // PUT: api/ManualPuestos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManualPuesto([FromRoute] int id, [FromBody] ManualPuesto manualPuesto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != manualPuesto.IdManualPuesto)
            {
                return BadRequest();
            }

            _context.Entry(manualPuesto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManualPuestoExists(id))
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

        // POST: api/ManualPuestos
        [HttpPost]
        public async Task<IActionResult> PostManualPuesto([FromBody] ManualPuesto manualPuesto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ManualPuesto.Add(manualPuesto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetManualPuesto", new { id = manualPuesto.IdManualPuesto }, manualPuesto);
        }

        // DELETE: api/ManualPuestos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManualPuesto([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var manualPuesto = await _context.ManualPuesto.SingleOrDefaultAsync(m => m.IdManualPuesto == id);
            if (manualPuesto == null)
            {
                return NotFound();
            }

            _context.ManualPuesto.Remove(manualPuesto);
            await _context.SaveChangesAsync();

            return Ok(manualPuesto);
        }

        private bool ManualPuestoExists(int id)
        {
            return _context.ManualPuesto.Any(e => e.IdManualPuesto == id);
        }
    }
}