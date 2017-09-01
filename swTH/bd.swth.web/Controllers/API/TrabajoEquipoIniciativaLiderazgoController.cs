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

namespace bd.swrm.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/TrabajoEquipoIniciativaLiderazgo")]
    public class TrabajoEquipoIniciativaLiderazgoController : Controller
    {
        private readonly SwTHDbContext db;

        public TrabajoEquipoIniciativaLiderazgoController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/TrabajoEquipoIniciativaLiderazgo
        [HttpGet]
        [Route("ListarTrabajoEquipoIniciativaLiderazgo")]
        public async Task<List<TrabajoEquipoIniciativaLiderazgo>> GetTrabajoEquipoIniciativaLiderazgo()
        {
            try
            {
                return await db.TrabajoEquipoIniciativaLiderazgo.OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<TrabajoEquipoIniciativaLiderazgo>();
            }
        }

        // GET: api/TrabajoEquipoIniciativaLiderazgo/5
        [HttpGet("{id}")]
        public async Task<Response> GetTrabajoEquipoIniciativaLiderazgo([FromRoute] int id)
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

                var trabajoEquipoIniciativaLiderazgo = await db.TrabajoEquipoIniciativaLiderazgo.SingleOrDefaultAsync(m => m.IdTrabajoEquipoIniciativaLiderazgo == id);

                if (trabajoEquipoIniciativaLiderazgo == null)
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
                    Resultado = trabajoEquipoIniciativaLiderazgo,
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

        // PUT: api/TrabajoEquipoIniciativaLiderazgo/5
        [HttpPut("{id}")]
        public async Task<Response> PutTrabajoEquipoIniciativaLiderazgo([FromRoute] int id, [FromBody] TrabajoEquipoIniciativaLiderazgo trabajoEquipoIniciativaLiderazgo)
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

                var trabajoEquipoIniciativaLiderazgoActualizar = await db.TrabajoEquipoIniciativaLiderazgo.Where(x => x.IdTrabajoEquipoIniciativaLiderazgo == id).FirstOrDefaultAsync();
                if (trabajoEquipoIniciativaLiderazgoActualizar != null)
                {
                    try
                    {
                        trabajoEquipoIniciativaLiderazgoActualizar.Nombre = trabajoEquipoIniciativaLiderazgo.Nombre;
                        db.TrabajoEquipoIniciativaLiderazgo.Update(trabajoEquipoIniciativaLiderazgoActualizar);
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
                return new Response
                {
                    IsSuccess = false,
                    Message=Mensaje.ExisteRegistro
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

        // POST: api/TrabajoEquipoIniciativaLiderazgo
        [HttpPost]
        [Route("InsertarTrabajoEquipoIniciativaLiderazgo")]
        public async Task<Response> PostTrabajoEquipoIniciativaLiderazgo([FromBody] TrabajoEquipoIniciativaLiderazgo trabajoEquipoIniciativaLiderazgo)
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

                var respuesta = Existe(trabajoEquipoIniciativaLiderazgo);
                if (!respuesta.IsSuccess)
                {
                    db.TrabajoEquipoIniciativaLiderazgo.Add(trabajoEquipoIniciativaLiderazgo);
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

        // DELETE: api/TrabajoEquipoIniciativaLiderazgo/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteTrabajoEquipoIniciativaLiderazgo([FromRoute] int id)
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

                var respuesta = await db.TrabajoEquipoIniciativaLiderazgo.SingleOrDefaultAsync(m => m.IdTrabajoEquipoIniciativaLiderazgo == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.TrabajoEquipoIniciativaLiderazgo.Remove(respuesta);
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

        private bool TrabajoEquipoIniciativaLiderazgoExists(string nombre)
        {
            return db.TrabajoEquipoIniciativaLiderazgo.Any(e => e.Nombre == nombre);
        }

        public Response Existe(TrabajoEquipoIniciativaLiderazgo trabajoEquipoIniciativaLiderazgo)
        {
            var bdd = trabajoEquipoIniciativaLiderazgo.Nombre.ToUpper().TrimEnd().TrimStart();
            var loglevelrespuesta = db.TrabajoEquipoIniciativaLiderazgo.Where(p => p.Nombre.ToUpper().TrimStart().TrimEnd() == bdd).FirstOrDefault();
            if (loglevelrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Existe un Trabajo Equipo Iniciativa Liderazgo de igual nombre",
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