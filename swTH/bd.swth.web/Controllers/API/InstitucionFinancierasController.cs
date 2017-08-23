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
    [Route("api/InstitucionFinancieras")]
    public class InstitucionFinancierasController : Controller
    {
        private readonly SwTHDbContext _context;

        public InstitucionFinancierasController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/InstitucionFinancieras
        [HttpGet]
        public IEnumerable<InstitucionFinanciera> GetInstitucionFinanciera()
        {
            return _context.InstitucionFinanciera;
        }

        // GET: api/InstitucionFinancieras/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstitucionFinanciera([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var institucionFinanciera = await _context.InstitucionFinanciera.SingleOrDefaultAsync(m => m.IdInstitucionFinanciera == id);

            if (institucionFinanciera == null)
            {
                return NotFound();
            }

            return Ok(institucionFinanciera);
        }

        // PUT: api/InstitucionFinancieras/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstitucionFinanciera([FromRoute] int id, [FromBody] InstitucionFinanciera institucionFinanciera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != institucionFinanciera.IdInstitucionFinanciera)
            {
                return BadRequest();
            }

            _context.Entry(institucionFinanciera).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstitucionFinancieraExists(id))
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

        // POST: api/InstitucionFinancieras
        [HttpPost]
        public async Task<IActionResult> PostInstitucionFinanciera([FromBody] InstitucionFinanciera institucionFinanciera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.InstitucionFinanciera.Add(institucionFinanciera);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstitucionFinanciera", new { id = institucionFinanciera.IdInstitucionFinanciera }, institucionFinanciera);
        }

        // DELETE: api/InstitucionFinancieras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstitucionFinanciera([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var institucionFinanciera = await _context.InstitucionFinanciera.SingleOrDefaultAsync(m => m.IdInstitucionFinanciera == id);
            if (institucionFinanciera == null)
            {
                return NotFound();
            }

            _context.InstitucionFinanciera.Remove(institucionFinanciera);
            await _context.SaveChangesAsync();

            return Ok(institucionFinanciera);
        }

        private bool InstitucionFinancieraExists(int id)
        {
            return _context.InstitucionFinanciera.Any(e => e.IdInstitucionFinanciera == id);
        }
    }
}