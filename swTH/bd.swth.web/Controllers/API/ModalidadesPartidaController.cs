using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.log.guardar.ObjectTranfer;
using bd.log.guardar.Servicios;
using bd.swth.entidades.Enumeradores;
using bd.log.guardar.Enumeradores;
using bd.swth.entidades.Utils;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/ModalidadPartidas")]
    public class ModalidadesPartidaController : Controller
    {
        private readonly SwTHDbContext db;

        public ModalidadesPartidaController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/ModalidadPartidas
        [HttpGet]
        [Route("ListarModalidadesPartida")]
        public async Task<List<ModalidadPartida>> GetModalidadPartida()
        {
            try
            {
                return await db.ModalidadPartida.OrderBy(x => x.Nombre).ToListAsync();
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = "Se ha producido una exepción",
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new List<ModalidadPartida>();
            }
        }

        // GET: api/ModalidadPartidas/5
        [HttpGet("{id}")]
        public async Task<Response> GetModalidadPartida([FromRoute] int id)
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

                var adscbdd = await db.ModalidadPartida.SingleOrDefaultAsync(m => m.IdModalidadPartida == id);

                if (adscbdd == null)
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
                    Resultado = adscbdd,
                };
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = "Se ha producido una exepción",
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

        // PUT: api/ModalidadPartidas/5
        [HttpPut("{id}")]
        public async Task<Response> PutModalidadPartida([FromRoute] int id, [FromBody] ModalidadPartida ModalidadPartida)
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


                try
                {
                    var entidad = await db.ModalidadPartida.Where(x => x.IdModalidadPartida == id).FirstOrDefaultAsync();

                    if (entidad == null)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "No existe información acerca del Grupo Ocupacional ",
                        };

                    }
                    else
                    {

                        entidad.Nombre = ModalidadPartida.Nombre;
                        entidad.IdRelacionLaboral = ModalidadPartida.IdRelacionLaboral;
                        db.ModalidadPartida.Update(entidad);
                        await db.SaveChangesAsync();
                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.Satisfactorio,
                        };
                    }


                }
                catch (Exception ex)
                {
                    await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                    {
                        ApplicationName = Convert.ToString(Aplicacion.SwTH),
                        ExceptionTrace = ex,
                        Message = "Se ha producido una exepción",
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
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                     Message = Mensaje.Excepcion
                };
            }
        }

        // POST: api/ModalidadPartidas
        [HttpPost]
        [Route("InsertarModalidadPartida")]
        public async Task<Response> PostModalidadPartida([FromBody] ModalidadPartida ModalidadPartida)
        {
            try
            {

                var respuesta = Existe(ModalidadPartida.Nombre);
                if (!respuesta.IsSuccess)
                {
                    db.ModalidadPartida.Add(ModalidadPartida);
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
                    Message = Mensaje.Satisfactorio
                };

            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = "Se ha producido una exepción",
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

        // DELETE: api/ModalidadPartidas/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteModalidadPartida([FromRoute] int id)
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

                var respuesta = await db.ModalidadPartida.SingleOrDefaultAsync(m => m.IdModalidadPartida == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.ModalidadPartida.Remove(respuesta);
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
                    Message = "Se ha producido una exepción",
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

        private bool ModalidadPartidaExists(int id)
        {
            return db.ModalidadPartida.Any(e => e.IdModalidadPartida == id);
        }


        public Response Existe(string nombreModalidadPartida)
        {

            var loglevelrespuesta = db.ModalidadPartida.Where(p => p.Nombre.ToUpper().TrimStart().TrimEnd() == nombreModalidadPartida).FirstOrDefault();
            if (loglevelrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Existe un sistema de igual nombre",
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