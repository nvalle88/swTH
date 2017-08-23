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
    [Route("api/FormulasRMUs")]
    public class FormulasRMUsController : Controller
    {
        private readonly SwTHDbContext _context;

        public FormulasRMUsController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/FormulasRMUs
        [HttpGet]
        public IEnumerable<FormulasRMU> GetFormulasRMU()
        {
            return _context.FormulasRMU;
        }

        // GET: api/FormulasRMUs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFormulasRMU([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var formulasRMU = await _context.FormulasRMU.SingleOrDefaultAsync(m => m.IdFormulaRMU == id);

            if (formulasRMU == null)
            {
                return NotFound();
            }

            return Ok(formulasRMU);
        }

        // PUT: api/FormulasRMUs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormulasRMU([FromRoute] int id, [FromBody] FormulasRMU formulasRMU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != formulasRMU.IdFormulaRMU)
            {
                return BadRequest();
            }

            _context.Entry(formulasRMU).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormulasRMUExists(id))
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

        // POST: api/FormulasRMUs
        [HttpPost]
        public async Task<IActionResult> PostFormulasRMU([FromBody] FormulasRMU formulasRMU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FormulasRMU.Add(formulasRMU);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFormulasRMU", new { id = formulasRMU.IdFormulaRMU }, formulasRMU);
        }

        // DELETE: api/FormulasRMUs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormulasRMU([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var formulasRMU = await _context.FormulasRMU.SingleOrDefaultAsync(m => m.IdFormulaRMU == id);
            if (formulasRMU == null)
            {
                return NotFound();
            }

            _context.FormulasRMU.Remove(formulasRMU);
            await _context.SaveChangesAsync();

            return Ok(formulasRMU);
        }

        private bool FormulasRMUExists(int id)
        {
            return _context.FormulasRMU.Any(e => e.IdFormulaRMU == id);
        }
    }
}