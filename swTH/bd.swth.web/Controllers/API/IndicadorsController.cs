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
    [Route("api/Indicadors")]
    public class IndicadorsController : Controller
    {
        private readonly SwTHDbContext _context;

        public IndicadorsController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/Indicadors
        [HttpGet]
        public IEnumerable<Indicador> GetIndicador()
        {
            return _context.Indicador;
        }

        // GET: api/Indicadors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIndicador([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var indicador = await _context.Indicador.SingleOrDefaultAsync(m => m.IdIndicador == id);

            if (indicador == null)
            {
                return NotFound();
            }

            return Ok(indicador);
        }

        // PUT: api/Indicadors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIndicador([FromRoute] int id, [FromBody] Indicador indicador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != indicador.IdIndicador)
            {
                return BadRequest();
            }

            _context.Entry(indicador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IndicadorExists(id))
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

        // POST: api/Indicadors
        [HttpPost]
        public async Task<IActionResult> PostIndicador([FromBody] Indicador indicador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Indicador.Add(indicador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIndicador", new { id = indicador.IdIndicador }, indicador);
        }

        // DELETE: api/Indicadors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIndicador([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var indicador = await _context.Indicador.SingleOrDefaultAsync(m => m.IdIndicador == id);
            if (indicador == null)
            {
                return NotFound();
            }

            _context.Indicador.Remove(indicador);
            await _context.SaveChangesAsync();

            return Ok(indicador);
        }

        private bool IndicadorExists(int id)
        {
            return _context.Indicador.Any(e => e.IdIndicador == id);
        }
    }
}