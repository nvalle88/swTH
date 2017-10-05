using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.Utils;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.swth.entidades.Enumeradores;
using bd.log.guardar.Enumeradores;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/ExperienciaLaboralRequeridas")]
    public class ExperienciaLaboralRequeridasController : Controller
    {
        private readonly SwTHDbContext db;

        public ExperienciaLaboralRequeridasController(SwTHDbContext db)
        {
            this.db = db;
        }


        

        [HttpPost]
        [Route("EliminarIncideOcupacionalExperienciaLaboralRequeridas")]
        public async Task<Response> EliminarIncideOcupacionalExperienciaLaboralRequeridas([FromBody] IndiceOcupacionalExperienciaLaboralRequerida indiceOcupacionalExperienciaLaboralRequerida)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ModeloInvalido,
                    };
                }

                var respuesta = await db.IndiceOcupacionalExperienciaLaboralRequerida.SingleOrDefaultAsync(m => m.IdExperienciaLaboralRequerida == indiceOcupacionalExperienciaLaboralRequerida.IdExperienciaLaboralRequerida
                                      && m.IdIndiceOcupacional == indiceOcupacionalExperienciaLaboralRequerida.IdIndiceOcupacional);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.IndiceOcupacionalExperienciaLaboralRequerida.Remove(respuesta);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                };
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = Mensaje.Excepcion,
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }

        [HttpPost]
        [Route("ListarExperienciaLaboralRequeridaNoAsignadasIndiceOcupacional")]
        public async Task<List<ExperienciaLaboralRequerida>> ListarExperienciaLaboralRequeridaNoAsignadasIndiceOcupacional([FromBody]IndiceOcupacional indiceOcupacional)
        {
            try
            {

                var ListaExperienciaLaboralRequerida = await db.ExperienciaLaboralRequerida
                                   .Where(m => !db.IndiceOcupacionalExperienciaLaboralRequerida
                                                   .Where(a => a.IndiceOcupacional.IdIndiceOcupacional == indiceOcupacional.IdIndiceOcupacional)
                                                   .Select(iom => iom.IdExperienciaLaboralRequerida)
                                                   .Contains(m.IdExperienciaLaboralRequerida))
                                          .ToListAsync();



                return ListaExperienciaLaboralRequerida;

            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = Mensaje.Excepcion,
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new List<ExperienciaLaboralRequerida>();
            }
        }



        // GET: api/ExperienciaLaboralRequeridas
        [HttpGet]
        public IEnumerable<ExperienciaLaboralRequerida> GetExperienciaLaboralRequerida()
        {
            return db.ExperienciaLaboralRequerida;
        }

        // GET: api/ExperienciaLaboralRequeridas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExperienciaLaboralRequerida([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var experienciaLaboralRequerida = await db.ExperienciaLaboralRequerida.SingleOrDefaultAsync(m => m.IdExperienciaLaboralRequerida == id);

            if (experienciaLaboralRequerida == null)
            {
                return NotFound();
            }

            return Ok(experienciaLaboralRequerida);
        }

        // PUT: api/ExperienciaLaboralRequeridas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExperienciaLaboralRequerida([FromRoute] int id, [FromBody] ExperienciaLaboralRequerida experienciaLaboralRequerida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != experienciaLaboralRequerida.IdExperienciaLaboralRequerida)
            {
                return BadRequest();
            }

            db.Entry(experienciaLaboralRequerida).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExperienciaLaboralRequeridaExists(id))
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

        // POST: api/ExperienciaLaboralRequeridas
        [HttpPost]
        public async Task<IActionResult> PostExperienciaLaboralRequerida([FromBody] ExperienciaLaboralRequerida experienciaLaboralRequerida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ExperienciaLaboralRequerida.Add(experienciaLaboralRequerida);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetExperienciaLaboralRequerida", new { id = experienciaLaboralRequerida.IdExperienciaLaboralRequerida }, experienciaLaboralRequerida);
        }

        // DELETE: api/ExperienciaLaboralRequeridas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperienciaLaboralRequerida([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var experienciaLaboralRequerida = await db.ExperienciaLaboralRequerida.SingleOrDefaultAsync(m => m.IdExperienciaLaboralRequerida == id);
            if (experienciaLaboralRequerida == null)
            {
                return NotFound();
            }

            db.ExperienciaLaboralRequerida.Remove(experienciaLaboralRequerida);
            await db.SaveChangesAsync();

            return Ok(experienciaLaboralRequerida);
        }

        private bool ExperienciaLaboralRequeridaExists(int id)
        {
            return db.ExperienciaLaboralRequerida.Any(e => e.IdExperienciaLaboralRequerida == id);
        }
    }
}