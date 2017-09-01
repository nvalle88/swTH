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
using bd.log.guardar.ObjectTranfer;
using bd.log.guardar.Servicios;
using bd.log.guardar.Enumeradores;
using bd.swth.entidades.Utils;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/NivelesDesarrollo")]
    public class NivelesDesarrolloController : Controller
    {
        private readonly SwTHDbContext db;

        public NivelesDesarrolloController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/NivelDesarrolloes
        [HttpGet]
        [Route("ListarNivelesDesarrollo")]
        public async Task<List<NivelDesarrollo>> GetNivelDesarrollo()
        {
            try
            {
                return await db.NivelDesarrollo.OrderBy(x => x.Nombre).ToListAsync();
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = "Se ha producido una excepción",
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new List<NivelDesarrollo>();
            }
        }

        // GET: api/NivelDesarrolloes/5
        [HttpGet("{id}")]
        public async Task<Response> GetNivelDesarrollo([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Módelo no válido",
                    };
                }

                var NivelDesarrollo = await db.NivelDesarrollo.SingleOrDefaultAsync(m => m.IdNivelDesarrollo == id);

                if (NivelDesarrollo == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No encontrado",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Resultado = NivelDesarrollo,
                };
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = "Se ha producido una excepción",
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new Response
                {
                    IsSuccess = false,
                    Message = "Error ",
                };
            }
        }

        // PUT: api/NivelDesarrolloes/5
        [HttpPut("{id}")]
        public async Task<Response> PutNivelDesarrollo([FromRoute] int id, [FromBody] NivelDesarrollo NivelDesarrollo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Módelo inválido"
                    };
                }

                var existe = Existe(NivelDesarrollo);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Existe un registro de igual Nombre",
                    };
                }

                var NivelDesarrolloActualizar = await db.NivelDesarrollo.Where(x => x.IdNivelDesarrollo == id).FirstOrDefaultAsync();

                if (NivelDesarrolloActualizar != null)
                {
                    try
                    {
                        NivelDesarrolloActualizar.Nombre = NivelDesarrollo.Nombre;
                        await db.SaveChangesAsync();

                        return new Response
                        {
                            IsSuccess = true,
                            Message = "Ok",
                        };

                    }
                    catch (Exception ex)
                    {
                        await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                        {
                            ApplicationName = Convert.ToString(Aplicacion.SwTH),
                            ExceptionTrace = ex,
                            Message = "Se ha producido una excepción",
                            LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                            LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                            UserName = "",

                        });
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "Error ",
                        };
                    }
                }




                return new Response
                {
                    IsSuccess = false,
                    Message = "Existe"
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Excepción"
                };
            }
        }

        // POST: api/NivelDesarrolloes
        [HttpPost]
        [Route("InsertarNivelesDesarrollo")]
        public async Task<Response> PostNivelDesarrollo([FromBody] NivelDesarrollo NivelDesarrollo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Módelo inválido"
                    };
                }

                var respuesta = Existe(NivelDesarrollo);
                if (!respuesta.IsSuccess)
                {
                    db.NivelDesarrollo.Add(NivelDesarrollo);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = "OK"
                    };
                }

                return new Response
                {
                    IsSuccess = false,
                    Message = "Existe un registro de igual Nombre..."
                };

            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = "Se ha producido una excepción",
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new Response
                {
                    IsSuccess = false,
                    Message = "Error ",
                };
            }
        }

        // DELETE: api/NivelDesarrolloes/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteNivelDesarrollo([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Módelo no válido ",
                    };
                }

                var respuesta = await db.NivelDesarrollo.SingleOrDefaultAsync(m => m.IdNivelDesarrollo == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No existe ",
                    };
                }
                db.NivelDesarrollo.Remove(respuesta);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = "Eliminado ",
                };
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = "Se ha producido una excepción",
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new Response
                {
                    IsSuccess = false,
                    Message = "Error ",
                };
            }
        }

        private Response Existe(NivelDesarrollo NivelDesarrollo)
        {
            var bdd = NivelDesarrollo.Nombre;
            var NivelDesarrollorespuesta = db.NivelDesarrollo.Where(p => p.Nombre == bdd).FirstOrDefault();
            if (NivelDesarrollorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Existe un Nivel de Desarrollo de igual nombre",
                    Resultado = null,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = NivelDesarrollorespuesta,
            };
        }

    }
}