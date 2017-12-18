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
    [Route("api/EstadosTiposAccionPersonal")]
    public class EstadosTiposAccionPersonalController : Controller
    {
        private readonly SwTHDbContext db;

        public EstadosTiposAccionPersonalController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/EstadoTipoAccionPersonal
        [HttpGet]
        [Route("ListarEstadosTiposAccionPersonal")]
        public async Task<List<EstadoTipoAccionPersonal>> GetEstadoTipoAccionPersonal()
        {
            try
            {
                return await db.EstadoTipoAccionPersonal.OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<EstadoTipoAccionPersonal>();
            }
        }

        // GET: api/EstadoTipoAccionPersonal/5
        [HttpGet("{id}")]
        public async Task<Response> GetEstadoTipoAccionPersonal([FromRoute] int id)
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

                var EstadoTipoAccionPersonal = await db.EstadoTipoAccionPersonal.SingleOrDefaultAsync(m => m.IdEstadoTipoAccionPersonal == id);

                if (EstadoTipoAccionPersonal == null)
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
                    Resultado = EstadoTipoAccionPersonal,
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

        // PUT: api/EstadoTipoAccionPersonal/5
        [HttpPut("{id}")]
        public async Task<Response> PutEstadoTipoAccionPersonal([FromRoute] int id, [FromBody] EstadoTipoAccionPersonal EstadoTipoAccionPersonal)
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

                var existe = Existe(EstadoTipoAccionPersonal);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var EstadoTipoAccionPersonalActualizar = await db.EstadoTipoAccionPersonal.Where(x => x.IdEstadoTipoAccionPersonal == id).FirstOrDefaultAsync();
                if (EstadoTipoAccionPersonalActualizar != null)
                {
                    try
                    {

                        EstadoTipoAccionPersonalActualizar.Nombre = EstadoTipoAccionPersonal.Nombre;
                        db.EstadoTipoAccionPersonal.Update(EstadoTipoAccionPersonalActualizar);
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




                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ExisteRegistro
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };
            }
        }

        // POST: api/EstadoTipoAccionPersonal
        [HttpPost]
        [Route("InsertarEstadoTipoAccionPersonal")]
        public async Task<Response> PostEstadoTipoAccionPersonal([FromBody] EstadoTipoAccionPersonal EstadoTipoAccionPersonal)
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

                var respuesta = Existe(EstadoTipoAccionPersonal);
                if (!respuesta.IsSuccess)
                {
                    db.EstadoTipoAccionPersonal.Add(EstadoTipoAccionPersonal);
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

        // DELETE: api/EstadoTipoAccionPersonal/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteEstadoTipoAccionPersonal([FromRoute] int id)
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

                var respuesta = await db.EstadoTipoAccionPersonal.SingleOrDefaultAsync(m => m.IdEstadoTipoAccionPersonal == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.EstadoTipoAccionPersonal.Remove(respuesta);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                };
            }
            catch (Exception ex)
            {

                if (ex.InnerException.Message.Contains("REFERENCE"))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No se puede eliminar",
                    };
                }
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

        private Response Existe(EstadoTipoAccionPersonal EstadoTipoAccionPersonal)
        {
            var bdd = EstadoTipoAccionPersonal.Nombre.ToUpper().TrimEnd().TrimStart();
            var EstadoTipoAccionPersonalrespuesta = db.EstadoTipoAccionPersonal.Where(p => p.Nombre.ToUpper().TrimStart().TrimEnd() == bdd).FirstOrDefault();
            if (EstadoTipoAccionPersonalrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = null,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = EstadoTipoAccionPersonalrespuesta,
            };
        }
    }
}