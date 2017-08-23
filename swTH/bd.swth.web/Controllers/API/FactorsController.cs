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
    [Route("api/Factors")]
    public class FactorsController : Controller
    {
        private readonly SwTHDbContext _context;

        public FactorsController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/Factors
        [HttpGet]
        public IEnumerable<Factor> GetFactor()
        {
            return _context.Factor;
        }

        // GET: api/Factors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFactor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var factor = await _context.Factor.SingleOrDefaultAsync(m => m.IdFactor == id);

            if (factor == null)
            {
                return NotFound();
            }

            return Ok(factor);
        }

        // PUT: api/Factors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactor([FromRoute] int id, [FromBody] Factor factor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != factor.IdFactor)
            {
                return BadRequest();
            }

            _context.Entry(factor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FactorExists(id))
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

        // POST: api/Factors
        [HttpPost]
        public async Task<IActionResult> PostFactor([FromBody] Factor factor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Factor.Add(factor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFactor", new { id = factor.IdFactor }, factor);
        }

        // DELETE: api/Factors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var factor = await _context.Factor.SingleOrDefaultAsync(m => m.IdFactor == id);
            if (factor == null)
            {
                return NotFound();
            }

            _context.Factor.Remove(factor);
            await _context.SaveChangesAsync();

            return Ok(factor);
        }

        private bool FactorExists(int id)
        {
            return _context.Factor.Any(e => e.IdFactor == id);
        }
    }
}