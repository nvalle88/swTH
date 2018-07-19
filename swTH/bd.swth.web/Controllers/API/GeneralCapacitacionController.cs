using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.log.guardar.Servicios;
using bd.log.guardar.Enumeradores;
using Microsoft.EntityFrameworkCore;
using bd.log.guardar.ObjectTranfer;
using bd.swth.entidades.Enumeradores;
using bd.swth.entidades.Utils;
using bd.swth.entidades.Constantes;

namespace bd.swrm.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/GeneralCapacitacion")]
    public class GeneralCapacitacionController : Controller
    {
        private readonly SwTHDbContext db;

        public GeneralCapacitacionController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/Pais
        [HttpGet]
        [Route("ListarGeneralCapacitacionTipoCapacitacion")]
        public async Task<List<GeneralCapacitacion>> ListarGeneralCapacitacionTipoCapacitacion()
        {
            try
            {
                var lista = await db.GeneralCapacitacion.Where(x=>x.Tipo== PlanificacionCapacitacion.TipoCapacitacion).OrderBy(x => x.Nombre).ToListAsync();
                return lista;
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
                return new List<GeneralCapacitacion>();
            }
        }

        [HttpGet]
        [Route("ListarGeneralCapacitacionEstadoEvento")]
        public async Task<List<GeneralCapacitacion>> ListarGeneralCapacitacionEstadoEvento()
        {
            try
            {
                return await db.GeneralCapacitacion.Where(x => x.Tipo == PlanificacionCapacitacion.EstadoEvento).OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<GeneralCapacitacion>();
            }
        }
        [HttpGet]
        [Route("ListarGeneralCapacitacionAmbitoCapacitacion")]
        public async Task<List<GeneralCapacitacion>> ListarGeneralCapacitacionAmbitoCapacitacion()
        {
            try
            {
                return await db.GeneralCapacitacion.Where(x => x.Tipo == PlanificacionCapacitacion.Ambitovento).OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<GeneralCapacitacion>();
            }
        }
        [HttpGet]
        [Route("ListarGeneralCapacitacionNombreEvento")]
        public async Task<List<GeneralCapacitacion>> ListarGeneralCapacitacionNombreEvento()
        {
            try
            {
                return await db.GeneralCapacitacion.Where(x => x.Tipo == PlanificacionCapacitacion.NombreEvento).OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<GeneralCapacitacion>();
            }
        }
        [HttpGet]
        [Route("ListarGeneralCapacitacionTipoEvento")]
        public async Task<List<GeneralCapacitacion>> ListarGeneralCapacitacionTipoEvento()
        {
            try
            {
                var lista =await db.GeneralCapacitacion.Where(x => x.Tipo == PlanificacionCapacitacion.TipoEvento).OrderBy(x => x.Nombre).ToListAsync();
                return lista;
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
                return new List<GeneralCapacitacion>();
            }
        }
        [HttpGet]
        [Route("ListarGeneralCapacitacionTipoEvaluacion")]
        public async Task<List<GeneralCapacitacion>> ListarGeneralCapacitacionTipoEvaluacion()
        {
            try
            {
                return await db.GeneralCapacitacion.Where(x => x.Tipo == PlanificacionCapacitacion.TipoEvaluacion).OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<GeneralCapacitacion>();
            }
        }
        // GET: api/Pais/5
        [HttpGet("{id}")]
        public async Task<Response> GetGeneralCapacitacion([FromRoute] int id)
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

                var pais = await db.GeneralCapacitacion.SingleOrDefaultAsync(m => m.IdGeneralCapacitacion == id);

                if (pais == null)
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
                    Resultado = pais,
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

        // PUT: api/Pais/5
        [HttpPut("{id}")]
        public async Task<Response> PutGeneralCapacitacion([FromRoute] int id, [FromBody] GeneralCapacitacion generalCapacitacion)
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

                var generalCapacitacionsActualizar = await db.GeneralCapacitacion.Where(x => x.IdGeneralCapacitacion == id).FirstOrDefaultAsync();
                if (generalCapacitacionsActualizar != null)
                {
                    try
                    {
                        var  respuesta = Existe(generalCapacitacion);
                        if (!respuesta.IsSuccess)
                        {
                            generalCapacitacionsActualizar.Nombre = generalCapacitacion.Nombre;
                            db.GeneralCapacitacion.Update(generalCapacitacionsActualizar);
                            await db.SaveChangesAsync();

                            return new Response
                            {
                                IsSuccess = true,
                                Message = Mensaje.Satisfactorio,
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

        // POST: api/Pais
        [HttpPost]
        [Route("InsertarGeneralCapacitacion")]
        public async Task<Response> PostGeneralCapacitacion([FromBody] GeneralCapacitacion generalCapacitacion)
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

                var respuesta = Existe(generalCapacitacion);
                if (!respuesta.IsSuccess)
                {
                    db.GeneralCapacitacion.Add(generalCapacitacion);
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

        // DELETE: api/Pais/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteGeneralCapacitacion([FromRoute] int id)
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

                var respuesta = await db.GeneralCapacitacion.SingleOrDefaultAsync(m => m.IdGeneralCapacitacion == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.GeneralCapacitacion.Remove(respuesta);
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

        private bool GeneralCapacitacionExists(string nombre)
        {
            return db.Pais.Any(e => e.Nombre == nombre);
        }

        public Response Existe(GeneralCapacitacion generalCapacitacion)
        {
            var bdd = generalCapacitacion.Nombre.ToUpper().TrimEnd().TrimStart();
            var loglevelrespuesta = db.GeneralCapacitacion.Where(p => p.Nombre.ToUpper().TrimStart().TrimEnd() == bdd).FirstOrDefault();
            if (loglevelrespuesta != null)
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
                Resultado = loglevelrespuesta,
            };
        }

    }
}