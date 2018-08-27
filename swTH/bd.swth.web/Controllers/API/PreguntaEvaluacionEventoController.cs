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
    [Route("api/PreguntaEvaluacionEvento")]
    public class PreguntaEvaluacionEventoController : Controller
    {
        private readonly SwTHDbContext db;

        public PreguntaEvaluacionEventoController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/Pais
        [HttpGet]
        [Route("ListarFacilitador")]
        public async Task<List<PreguntaEvaluacionEvento>> ListarFacilitador()
        {
            try
            {
                var lista = await db.PreguntaEvaluacionEvento.Where(x=>x.Facilitador== true).OrderBy(x => x.Descripcion).ToListAsync();
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
                return new List<PreguntaEvaluacionEvento>();
            }
        }

        [HttpGet]
        [Route("ListarOrganizador")]
        public async Task<List<PreguntaEvaluacionEvento>> ListarOrganizador()
        {
            try
            {
                return await db.PreguntaEvaluacionEvento.Where(x => x.Organizador == true).OrderBy(x => x.Descripcion).ToListAsync();
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
                return new List<PreguntaEvaluacionEvento>();
            }
        }
        [HttpGet]
        [Route("ListarConocimiento")]
        public async Task<List<PreguntaEvaluacionEvento>> ListarConocimiento()
        {
            try
            {
                return await db.PreguntaEvaluacionEvento.Where(x => x.ConocimientoObtenidos == true).OrderBy(x => x.Descripcion).ToListAsync();
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
                return new List<PreguntaEvaluacionEvento>();
            }
        }
        // GET: api/Pais/5
        [HttpGet("{id}")]
        public async Task<Response> GetPreguntaEvaluacionEvento([FromRoute] int id)
        {
            try
            {
                
                var pais = await db.PreguntaEvaluacionEvento.SingleOrDefaultAsync(m => m.IdPreguntaEvaluacionEvento == id);

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
        public async Task<Response> PutPreguntaEvaluacionEvento([FromRoute] int id, [FromBody] PreguntaEvaluacionEvento preguntaEvaluacionEvento)
        {
            try
            {
                var generalCapacitacionsActualizar = await db.PreguntaEvaluacionEvento.Where(x => x.IdPreguntaEvaluacionEvento == id).FirstOrDefaultAsync();
                if (generalCapacitacionsActualizar != null)
                {
                    try
                    {
                        var  respuesta = Existe(preguntaEvaluacionEvento);
                        if (!respuesta.IsSuccess)
                        {
                            generalCapacitacionsActualizar.Descripcion = preguntaEvaluacionEvento.Descripcion;
                            generalCapacitacionsActualizar.Facilitador = preguntaEvaluacionEvento.Facilitador;
                            generalCapacitacionsActualizar.Organizador = preguntaEvaluacionEvento.Organizador;
                            generalCapacitacionsActualizar.ConocimientoObtenidos = preguntaEvaluacionEvento.ConocimientoObtenidos;
                            db.PreguntaEvaluacionEvento.Update(generalCapacitacionsActualizar);
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
        [Route("InsertarPreguntas")]
        public async Task<Response> PostGeneralCapacitacion([FromBody] PreguntaEvaluacionEvento preguntaEvaluacionEvento)
        {
            try
            {
                var respuesta = Existe(preguntaEvaluacionEvento);
                if (!respuesta.IsSuccess)
                {
                    db.PreguntaEvaluacionEvento.Add(preguntaEvaluacionEvento);
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
                
                var respuesta = await db.PreguntaEvaluacionEvento.SingleOrDefaultAsync(m => m.IdPreguntaEvaluacionEvento == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.PreguntaEvaluacionEvento.Remove(respuesta);
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

        public Response Existe(PreguntaEvaluacionEvento generalCapacitacion)
        {
            var bdd = generalCapacitacion.IdPreguntaEvaluacionEvento;
            var bdd1 = generalCapacitacion.Descripcion.ToUpper().TrimEnd().TrimStart();
            var bdd2= generalCapacitacion.Facilitador;
            var bdd3 = generalCapacitacion.Organizador;
            var bdd4 = generalCapacitacion.ConocimientoObtenidos;
            var loglevelrespuesta = db.PreguntaEvaluacionEvento.Where(p => p.Descripcion == bdd1 && p.Facilitador == bdd2 && p.Organizador == bdd3 && p.ConocimientoObtenidos == bdd4).FirstOrDefault();
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