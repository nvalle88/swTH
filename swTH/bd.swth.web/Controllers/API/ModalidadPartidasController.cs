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
    [Route("api/ModalidadPartidas")]
    public class ModalidadPartidasController : Controller
    {
        private readonly SwTHDbContext _context;

        public ModalidadPartidasController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/ModalidadPartidas
        [HttpGet]
        public IEnumerable<ModalidadPartida> GetModalidadPartida()
        {
            return _context.ModalidadPartida;
        }

        // GET: api/ModalidadPartidas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModalidadPartida([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modalidadPartida = await _context.ModalidadPartida.SingleOrDefaultAsync(m => m.IdModalidadPartida == id);

            if (modalidadPartida == null)
            {
                return NotFound();
            }

            return Ok(modalidadPartida);
        }

        // PUT: api/ModalidadPartidas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModalidadPartida([FromRoute] int id, [FromBody] ModalidadPartida modalidadPartida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modalidadPartida.IdModalidadPartida)
            {
                return BadRequest();
            }

            _context.Entry(modalidadPartida).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModalidadPartidaExists(id))
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

        // POST: api/ModalidadPartidas
        [HttpPost]
        public async Task<IActionResult> PostModalidadPartida([FromBody] ModalidadPartida modalidadPartida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ModalidadPartida.Add(modalidadPartida);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModalidadPartida", new { id = modalidadPartida.IdModalidadPartida }, modalidadPartida);
        }

        // DELETE: api/ModalidadPartidas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModalidadPartida([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modalidadPartida = await _context.ModalidadPartida.SingleOrDefaultAsync(m => m.IdModalidadPartida == id);
            if (modalidadPartida == null)
            {
                return NotFound();
            }

            _context.ModalidadPartida.Remove(modalidadPartida);
            await _context.SaveChangesAsync();

            return Ok(modalidadPartida);
        }

        private bool ModalidadPartidaExists(int id)
        {
            return _context.ModalidadPartida.Any(e => e.IdModalidadPartida == id);
        }
    }
}