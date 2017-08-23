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
    [Route("api/GrupoOcupacionals")]
    public class GrupoOcupacionalsController : Controller
    {
        private readonly SwTHDbContext _context;

        public GrupoOcupacionalsController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/GrupoOcupacionals
        [HttpGet]
        public IEnumerable<GrupoOcupacional> GetGrupoOcupacional()
        {
            return _context.GrupoOcupacional;
        }

        // GET: api/GrupoOcupacionals/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGrupoOcupacional([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var grupoOcupacional = await _context.GrupoOcupacional.SingleOrDefaultAsync(m => m.IdGrupoOcupacional == id);

            if (grupoOcupacional == null)
            {
                return NotFound();
            }

            return Ok(grupoOcupacional);
        }

        // PUT: api/GrupoOcupacionals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrupoOcupacional([FromRoute] int id, [FromBody] GrupoOcupacional grupoOcupacional)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grupoOcupacional.IdGrupoOcupacional)
            {
                return BadRequest();
            }

            _context.Entry(grupoOcupacional).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GrupoOcupacionalExists(id))
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

        // POST: api/GrupoOcupacionals
        [HttpPost]
        public async Task<IActionResult> PostGrupoOcupacional([FromBody] GrupoOcupacional grupoOcupacional)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.GrupoOcupacional.Add(grupoOcupacional);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGrupoOcupacional", new { id = grupoOcupacional.IdGrupoOcupacional }, grupoOcupacional);
        }

        // DELETE: api/GrupoOcupacionals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrupoOcupacional([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var grupoOcupacional = await _context.GrupoOcupacional.SingleOrDefaultAsync(m => m.IdGrupoOcupacional == id);
            if (grupoOcupacional == null)
            {
                return NotFound();
            }

            _context.GrupoOcupacional.Remove(grupoOcupacional);
            await _context.SaveChangesAsync();

            return Ok(grupoOcupacional);
        }

        private bool GrupoOcupacionalExists(int id)
        {
            return _context.GrupoOcupacional.Any(e => e.IdGrupoOcupacional == id);
        }
    }
}