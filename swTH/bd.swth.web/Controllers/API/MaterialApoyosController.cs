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
    [Route("api/MaterialApoyos")]
    public class MaterialApoyosController : Controller
    {
        private readonly SwTHDbContext _context;

        public MaterialApoyosController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/MaterialApoyos
        [HttpGet]
        public IEnumerable<MaterialApoyo> GetMaterialApoyo()
        {
            return _context.MaterialApoyo;
        }

        // GET: api/MaterialApoyos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaterialApoyo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var materialApoyo = await _context.MaterialApoyo.SingleOrDefaultAsync(m => m.IdMaterialApoyo == id);

            if (materialApoyo == null)
            {
                return NotFound();
            }

            return Ok(materialApoyo);
        }

        // PUT: api/MaterialApoyos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterialApoyo([FromRoute] int id, [FromBody] MaterialApoyo materialApoyo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != materialApoyo.IdMaterialApoyo)
            {
                return BadRequest();
            }

            _context.Entry(materialApoyo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialApoyoExists(id))
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

        // POST: api/MaterialApoyos
        [HttpPost]
        public async Task<IActionResult> PostMaterialApoyo([FromBody] MaterialApoyo materialApoyo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MaterialApoyo.Add(materialApoyo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaterialApoyo", new { id = materialApoyo.IdMaterialApoyo }, materialApoyo);
        }

        // DELETE: api/MaterialApoyos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterialApoyo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var materialApoyo = await _context.MaterialApoyo.SingleOrDefaultAsync(m => m.IdMaterialApoyo == id);
            if (materialApoyo == null)
            {
                return NotFound();
            }

            _context.MaterialApoyo.Remove(materialApoyo);
            await _context.SaveChangesAsync();

            return Ok(materialApoyo);
        }

        private bool MaterialApoyoExists(int id)
        {
            return _context.MaterialApoyo.Any(e => e.IdMaterialApoyo == id);
        }
    }
}