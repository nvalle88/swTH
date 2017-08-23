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
    [Route("api/FormularioCapacitacions")]
    public class FormularioCapacitacionsController : Controller
    {
        private readonly SwTHDbContext _context;

        public FormularioCapacitacionsController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/FormularioCapacitacions
        [HttpGet]
        public IEnumerable<FormularioCapacitacion> GetFormularioCapacitacion()
        {
            return _context.FormularioCapacitacion;
        }

        // GET: api/FormularioCapacitacions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFormularioCapacitacion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var formularioCapacitacion = await _context.FormularioCapacitacion.SingleOrDefaultAsync(m => m.IdFormularioCapacitacion == id);

            if (formularioCapacitacion == null)
            {
                return NotFound();
            }

            return Ok(formularioCapacitacion);
        }

        // PUT: api/FormularioCapacitacions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormularioCapacitacion([FromRoute] int id, [FromBody] FormularioCapacitacion formularioCapacitacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != formularioCapacitacion.IdFormularioCapacitacion)
            {
                return BadRequest();
            }

            _context.Entry(formularioCapacitacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormularioCapacitacionExists(id))
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

        // POST: api/FormularioCapacitacions
        [HttpPost]
        public async Task<IActionResult> PostFormularioCapacitacion([FromBody] FormularioCapacitacion formularioCapacitacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FormularioCapacitacion.Add(formularioCapacitacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFormularioCapacitacion", new { id = formularioCapacitacion.IdFormularioCapacitacion }, formularioCapacitacion);
        }

        // DELETE: api/FormularioCapacitacions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormularioCapacitacion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var formularioCapacitacion = await _context.FormularioCapacitacion.SingleOrDefaultAsync(m => m.IdFormularioCapacitacion == id);
            if (formularioCapacitacion == null)
            {
                return NotFound();
            }

            _context.FormularioCapacitacion.Remove(formularioCapacitacion);
            await _context.SaveChangesAsync();

            return Ok(formularioCapacitacion);
        }

        private bool FormularioCapacitacionExists(int id)
        {
            return _context.FormularioCapacitacion.Any(e => e.IdFormularioCapacitacion == id);
        }
    }
}