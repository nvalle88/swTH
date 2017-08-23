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
    [Route("api/Parentescos")]
    public class ParentescosController : Controller
    {
        private readonly SwTHDbContext _context;

        public ParentescosController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/Parentescos
        [HttpGet]
        public IEnumerable<Parentesco> GetParentesco()
        {
            return _context.Parentesco;
        }

        // GET: api/Parentescos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetParentesco([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parentesco = await _context.Parentesco.SingleOrDefaultAsync(m => m.IdParentesco == id);

            if (parentesco == null)
            {
                return NotFound();
            }

            return Ok(parentesco);
        }

        // PUT: api/Parentescos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParentesco([FromRoute] int id, [FromBody] Parentesco parentesco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parentesco.IdParentesco)
            {
                return BadRequest();
            }

            _context.Entry(parentesco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParentescoExists(id))
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

        // POST: api/Parentescos
        [HttpPost]
        public async Task<IActionResult> PostParentesco([FromBody] Parentesco parentesco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Parentesco.Add(parentesco);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParentesco", new { id = parentesco.IdParentesco }, parentesco);
        }

        // DELETE: api/Parentescos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParentesco([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parentesco = await _context.Parentesco.SingleOrDefaultAsync(m => m.IdParentesco == id);
            if (parentesco == null)
            {
                return NotFound();
            }

            _context.Parentesco.Remove(parentesco);
            await _context.SaveChangesAsync();

            return Ok(parentesco);
        }

        private bool ParentescoExists(int id)
        {
            return _context.Parentesco.Any(e => e.IdParentesco == id);
        }
    }
}