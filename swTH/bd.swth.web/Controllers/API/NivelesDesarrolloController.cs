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
    [Route("api/NivelesDesarrollo")]
    public class NivelesDesarrolloController : Controller
    {
        private readonly SwTHDbContext _context;

        public NivelesDesarrolloController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/NivelesDesarrollo
        [HttpGet]
        public IEnumerable<NivelDesarrollo> GetNivelDesarrollo()
        {
            return _context.NivelDesarrollo;
        }

        // GET: api/NivelesDesarrollo/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNivelDesarrollo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nivelDesarrollo = await _context.NivelDesarrollo.SingleOrDefaultAsync(m => m.IdNivelDesarrollo == id);

            if (nivelDesarrollo == null)
            {
                return NotFound();
            }

            return Ok(nivelDesarrollo);
        }

        // PUT: api/NivelesDesarrollo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNivelDesarrollo([FromRoute] int id, [FromBody] NivelDesarrollo nivelDesarrollo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nivelDesarrollo.IdNivelDesarrollo)
            {
                return BadRequest();
            }

            _context.Entry(nivelDesarrollo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NivelDesarrolloExists(id))
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

        // POST: api/NivelesDesarrollo
        [HttpPost]
        public async Task<IActionResult> PostNivelDesarrollo([FromBody] NivelDesarrollo nivelDesarrollo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.NivelDesarrollo.Add(nivelDesarrollo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNivelDesarrollo", new { id = nivelDesarrollo.IdNivelDesarrollo }, nivelDesarrollo);
        }

        // DELETE: api/NivelesDesarrollo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNivelDesarrollo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nivelDesarrollo = await _context.NivelDesarrollo.SingleOrDefaultAsync(m => m.IdNivelDesarrollo == id);
            if (nivelDesarrollo == null)
            {
                return NotFound();
            }

            _context.NivelDesarrollo.Remove(nivelDesarrollo);
            await _context.SaveChangesAsync();

            return Ok(nivelDesarrollo);
        }

        private bool NivelDesarrolloExists(int id)
        {
            return _context.NivelDesarrollo.Any(e => e.IdNivelDesarrollo == id);
        }
    }
}