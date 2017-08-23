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
    [Route("api/IngresoEgresoRMUs")]
    public class IngresoEgresoRMUsController : Controller
    {
        private readonly SwTHDbContext _context;

        public IngresoEgresoRMUsController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/IngresoEgresoRMUs
        [HttpGet]
        public IEnumerable<IngresoEgresoRMU> GetIngresoEgresoRMU()
        {
            return _context.IngresoEgresoRMU;
        }

        // GET: api/IngresoEgresoRMUs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngresoEgresoRMU([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ingresoEgresoRMU = await _context.IngresoEgresoRMU.SingleOrDefaultAsync(m => m.IdIngresoEgresoRMU == id);

            if (ingresoEgresoRMU == null)
            {
                return NotFound();
            }

            return Ok(ingresoEgresoRMU);
        }

        // PUT: api/IngresoEgresoRMUs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngresoEgresoRMU([FromRoute] int id, [FromBody] IngresoEgresoRMU ingresoEgresoRMU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ingresoEgresoRMU.IdIngresoEgresoRMU)
            {
                return BadRequest();
            }

            _context.Entry(ingresoEgresoRMU).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngresoEgresoRMUExists(id))
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

        // POST: api/IngresoEgresoRMUs
        [HttpPost]
        public async Task<IActionResult> PostIngresoEgresoRMU([FromBody] IngresoEgresoRMU ingresoEgresoRMU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.IngresoEgresoRMU.Add(ingresoEgresoRMU);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngresoEgresoRMU", new { id = ingresoEgresoRMU.IdIngresoEgresoRMU }, ingresoEgresoRMU);
        }

        // DELETE: api/IngresoEgresoRMUs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngresoEgresoRMU([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ingresoEgresoRMU = await _context.IngresoEgresoRMU.SingleOrDefaultAsync(m => m.IdIngresoEgresoRMU == id);
            if (ingresoEgresoRMU == null)
            {
                return NotFound();
            }

            _context.IngresoEgresoRMU.Remove(ingresoEgresoRMU);
            await _context.SaveChangesAsync();

            return Ok(ingresoEgresoRMU);
        }

        private bool IngresoEgresoRMUExists(int id)
        {
            return _context.IngresoEgresoRMU.Any(e => e.IdIngresoEgresoRMU == id);
        }
    }
}