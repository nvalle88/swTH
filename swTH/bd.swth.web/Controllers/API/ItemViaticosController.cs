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
    [Route("api/ItemViaticos")]
    public class ItemViaticosController : Controller
    {
        private readonly SwTHDbContext _context;

        public ItemViaticosController(SwTHDbContext context)
        {
            _context = context;
        }

        // GET: api/ItemViaticos
        [HttpGet]
        public IEnumerable<ItemViatico> GetItemViatico()
        {
            return _context.ItemViatico;
        }

        // GET: api/ItemViaticos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemViatico([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemViatico = await _context.ItemViatico.SingleOrDefaultAsync(m => m.IdItemViatico == id);

            if (itemViatico == null)
            {
                return NotFound();
            }

            return Ok(itemViatico);
        }

        // PUT: api/ItemViaticos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemViatico([FromRoute] int id, [FromBody] ItemViatico itemViatico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itemViatico.IdItemViatico)
            {
                return BadRequest();
            }

            _context.Entry(itemViatico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemViaticoExists(id))
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

        // POST: api/ItemViaticos
        [HttpPost]
        public async Task<IActionResult> PostItemViatico([FromBody] ItemViatico itemViatico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ItemViatico.Add(itemViatico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemViatico", new { id = itemViatico.IdItemViatico }, itemViatico);
        }

        // DELETE: api/ItemViaticos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemViatico([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemViatico = await _context.ItemViatico.SingleOrDefaultAsync(m => m.IdItemViatico == id);
            if (itemViatico == null)
            {
                return NotFound();
            }

            _context.ItemViatico.Remove(itemViatico);
            await _context.SaveChangesAsync();

            return Ok(itemViatico);
        }

        private bool ItemViaticoExists(int id)
        {
            return _context.ItemViatico.Any(e => e.IdItemViatico == id);
        }
    }
}