using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.Enumeradores;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.log.guardar.Enumeradores;
using bd.swth.entidades.Utils;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/FlujosAprobacion")]
    public class FlujosAprobacionController : Controller
    {
        private readonly SwTHDbContext db;

        public FlujosAprobacionController(SwTHDbContext db)
        {
            this.db = db;
        }

        

        // GET: api/FlujoAprobacion
        [HttpGet]
        [Route("ListarFlujosAprobacion")]
        public async Task<List<FlujoAprobacion>> GetFlujosAprobacion()
        {
            try
            {
                return await db.FlujoAprobacion
                    .Include(i=>i.Sucursal)
                    .Include(i=>i.TipoAccionPersonal)
                    .Include(i=>i.ManualPuesto)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                
                return new List<FlujoAprobacion>();
            }
        }

        // GET: api/FlujoAprobacion/5
        [HttpGet("{id}")]
        public async Task<Response> GetFlujoAprobacion([FromRoute] int id)
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

                var FlujoAprobacion = await db.FlujoAprobacion
                    .Include(i=>i.TipoAccionPersonal)
                    .SingleOrDefaultAsync(m => m.IdFlujoAprobacion == id);

                if (FlujoAprobacion == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                    Resultado = FlujoAprobacion,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }


        /*
        // PUT: api/FlujoAprobacion/5
        [HttpPut("{id}")]
        public async Task<Response> PutFlujoAprobacion([FromRoute] int id, [FromBody] FlujoAprobacion flujoAprobacion)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ModeloInvalido
                    };
                }

                var existe = Existe(flujoAprobacion);
                var FlujoAprobacionActualizar = (FlujoAprobacion)existe.Resultado;
                if (existe.IsSuccess)
                {
                    //if (FlujoAprobacionActualizar.IdFlujoAprobacion == flujoAprobacion.IdFlujoAprobacion)
                    //{
                    //    return new Response
                    //    {
                    //        IsSuccess = true,
                    //    };
                    //}
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }
                var FlujoAprobacion = db.FlujoAprobacion.Find(flujoAprobacion.IdFlujoAprobacion);

                FlujoAprobacion.IdTipoAccionPersonal = flujoAprobacion.IdTipoAccionPersonal;
                FlujoAprobacion.IdEmpleado = flujoAprobacion.IdEmpleado;
                db.FlujoAprobacion.Update(FlujoAprobacion);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                };

            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Excepcion,
                };
            }

        }


        /*
        

        

        // POST: api/FlujoAprobacion
        [HttpPost]
        [Route("InsertarFlujoAprobacion")]
        public async Task<Response> PostFlujoAprobacion([FromBody] FlujoAprobacion FlujoAprobacion)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = ""
                    };
                }

                var respuesta = Existe(FlujoAprobacion);
                if (!respuesta.IsSuccess)
                {
                    db.FlujoAprobacion.Add(FlujoAprobacion);
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
                    Message = Mensaje.ExisteRegistro,
                };

            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex.Message,
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

        // DELETE: api/FlujoAprobacion/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteFlujoAprobacion([FromRoute] int id)
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

                var respuesta = await db.FlujoAprobacion.SingleOrDefaultAsync(m => m.IdFlujoAprobacion == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.FlujoAprobacion.Remove(respuesta);
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
                    ExceptionTrace = ex.Message,
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

        private Response Existe(FlujoAprobacion FlujoAprobacion)
        {
            var FlujoAprobacionrespuesta = db.FlujoAprobacion.Where(p =>p.IdTipoAccionPersonal == FlujoAprobacion.IdTipoAccionPersonal&& p.IdEmpleado == FlujoAprobacion.IdEmpleado).FirstOrDefault();
            if (FlujoAprobacionrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = FlujoAprobacionrespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = FlujoAprobacionrespuesta,
            };
        }

    */
    }
}