using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/AccionesPersonal")]
    public class AccionesPersonalController : Controller
    {
        private readonly SwTHDbContext _context;

        public AccionesPersonalController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/AccionesPersonal
        [HttpGet]
        public async Task<IEnumerable<AccionPersonal>> GetAccionPersonalAsync()
        {
           

            return _context.AccionPersonal;
        }

        // GET: api/AccionesPersonal/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccionPersonal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accionPersonal = await _context.AccionPersonal.SingleOrDefaultAsync(m => m.IdAccionPersonal == id);

            if (accionPersonal == null)
            {
                return NotFound();
            }

            return Ok(accionPersonal);
        }

        // PUT: api/AccionesPersonal/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccionPersonal([FromRoute] int id, [FromBody] AccionPersonal accionPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accionPersonal.IdAccionPersonal)
            {
                return BadRequest();
            }

            _context.Entry(accionPersonal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccionPersonalExists(id))
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

        // POST: api/AccionesPersonal
        [HttpPost]
        public async Task<IActionResult> PostAccionPersonal([FromBody] AccionPersonal accionPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AccionPersonal.Add(accionPersonal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccionPersonal", new { id = accionPersonal.IdAccionPersonal }, accionPersonal);
        }

        // DELETE: api/AccionesPersonal/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccionPersonal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accionPersonal = await _context.AccionPersonal.SingleOrDefaultAsync(m => m.IdAccionPersonal == id);
            if (accionPersonal == null)
            {
                return NotFound();
            }

            _context.AccionPersonal.Remove(accionPersonal);
            await _context.SaveChangesAsync();

            return Ok(accionPersonal);
        }

        private bool AccionPersonalExists(int id)
        {
            return _context.AccionPersonal.Any(e => e.IdAccionPersonal == id);
        }
    }
}