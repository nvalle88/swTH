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
    [Route("api/Dependencias")]
    public class DependenciasController : Controller
    {
        private readonly SwTHDbContext db;

        public DependenciasController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/Dependencias
        [HttpGet]
        [Route("ListarDependencias")]
        public async Task<List<Dependencia>> GetDependencia()
        {
            try
            {
                return await db.Dependencia.OrderBy(x => x.Sucursal.Nombre)
                                                    .ThenBy(x => x.DependenciaPadre.Nombre)
                                                    .ThenBy(x => x.Nombre).Include(x => x.Sucursal).Include(x => x.DependenciaPadre)
                                                    .ToListAsync();
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
                return new List<Dependencia>();
            }
        }


        [HttpPost]
        [Route("ListarDependenciaporSucursal")]
        public async Task<List<Dependencia>> GetDependenciabySucursal([FromBody] Sucursal sucursal)
        {
            try
            {
                return await db.Dependencia.Include(c => c.Sucursal).Where(x => x.IdSucursal == sucursal.IdSucursal).OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<Dependencia>();
            }
        }

        // GET: api/Dependencias/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDependencia([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dependencia = await db.Dependencia.SingleOrDefaultAsync(m => m.IdDependencia == id);

            if (dependencia == null)
            {
                return NotFound();
            }

            return Ok(dependencia);
        }

        // PUT: api/Dependencias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDependencia([FromRoute] int id, [FromBody] Dependencia dependencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dependencia.IdDependencia)
            {
                return BadRequest();
            }

            db.Entry(dependencia).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;

            }

            return NoContent();
        }

        

             [HttpPost]
        [Route("ListarPadresPorSucursal")]
        public async Task<List<Dependencia>> ListarPadresPorSucursal([FromBody] Dependencia dependencia)
        {
            try
            {
                return await db.Dependencia.Where(x => x.IdSucursal == dependencia.IdSucursal).OrderBy(x => x.Nombre).ToListAsync();
                                                    
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
                return new List<Dependencia>();
            }
        }

        // POST: api/Dependencias
        [HttpPost]
        [Route("InsertarDependencia")]
        public async Task<Response> PostDependencia([FromBody] Dependencia dependencia)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message =Mensaje.ModeloInvalido,
                    };
                }

                var respuesta = Existe(dependencia);
                if (!respuesta.IsSuccess)
                {
                    db.Dependencia.Add(dependencia);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio
                    };
                }

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ExisteRegistro
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

        // DELETE: api/Dependencias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDependencia([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dependencia = await db.Dependencia.SingleOrDefaultAsync(m => m.IdDependencia == id);
            if (dependencia == null)
            {
                return NotFound();
            }

            db.Dependencia.Remove(dependencia);
            await db.SaveChangesAsync();

            return Ok(dependencia);
        }

        private Response Existe(Dependencia dependencia)
        {
            var nombre = dependencia.Nombre.ToUpper().TrimEnd().TrimStart();
            var EscalaGradosrespuesta = db.Dependencia.Where(p => p.Nombre.ToUpper().TrimEnd().TrimStart()==nombre
                                                             && p.IdDependenciaPadre==dependencia.IdDependenciaPadre
                                                             && p.IdSucursal==dependencia.IdSucursal).FirstOrDefault();
            if (EscalaGradosrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = EscalaGradosrespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = EscalaGradosrespuesta,
            };
        }
    }
}