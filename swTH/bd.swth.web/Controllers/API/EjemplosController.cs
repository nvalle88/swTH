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
    [Route("api/Ejemplos")]
    public class EjemplosController : Controller
    {
        private readonly SwTHDbContext db;

        public EjemplosController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/Ejemplos

        [HttpGet]
        [Route("ListarEjemplos")]
        public async Task<List<Ejemplo>> ListarEjemplos()
        {
            return await db.Ejemplo.Where(x=>x.Edad>=18).ToListAsync();
        }

        // GET: api/Ejemplos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEjemplo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ejemplo = await db.Ejemplo.SingleOrDefaultAsync(m => m.Id == id);

            if (ejemplo == null)
            {
                return NotFound();
            }

            return Ok(ejemplo);
        }

        // PUT: api/Ejemplos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEjemplo([FromRoute] int id, [FromBody] Ejemplo ejemplo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ejemplo.Id)
            {
                return BadRequest();
            }

            db.Entry(ejemplo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EjemploExists(id))
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

        // POST: api/Ejemplos
        [HttpPost]
        public async Task<IActionResult> PostEjemplo([FromBody] Ejemplo ejemplo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ejemplo.Add(ejemplo);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetEjemplo", new { id = ejemplo.Id }, ejemplo);
        }

        // DELETE: api/Ejemplos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEjemplo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ejemplo = await db.Ejemplo.SingleOrDefaultAsync(m => m.Id == id);
            if (ejemplo == null)
            {
                return NotFound();
            }

            db.Ejemplo.Remove(ejemplo);
            await db.SaveChangesAsync();

            return Ok(ejemplo);
        }

        private bool EjemploExists(int id)
        {
            return db.Ejemplo.Any(e => e.Id == id);
        }
    }
}