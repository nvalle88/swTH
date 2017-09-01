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
    [Route("api/NacionalidadesIndigenas")]
    public class NacionalidadesIndigenasController : Controller
    {
        private readonly SwTHDbContext db;

        public NacionalidadesIndigenasController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/NacionalidadIndigenaes
        [HttpGet]
        [Route("ListarNacionalidadesIndigenas")]
        public async Task<List<NacionalidadIndigena>> GetNacionalidadIndigena()
        {
            try
            {
                return await db.NacionalidadIndigena.OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<NacionalidadIndigena>();
            }
        }

        // GET: api/NacionalidadIndigenaes/5
        [HttpGet("{id}")]
        public async Task<Response> GetNacionalidadIndigena([FromRoute] int id)
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

                var NacionalidadIndigena = await db.NacionalidadIndigena.SingleOrDefaultAsync(m => m.IdNacionalidadIndigena == id);

                if (NacionalidadIndigena == null)
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
                    Resultado = NacionalidadIndigena,
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

        // PUT: api/NacionalidadIndigenaes/5
        [HttpPut("{id}")]
        public async Task<Response> PutNacionalidadIndigena([FromRoute] int id, [FromBody] NacionalidadIndigena NacionalidadIndigena)
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

                var existe = Existe(NacionalidadIndigena);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Existe un registro de igual Nombre",
                    };
                }

                var NacionalidadIndigenaActualizar = await db.NacionalidadIndigena.Where(x => x.IdNacionalidadIndigena == id).FirstOrDefaultAsync();

                if (NacionalidadIndigenaActualizar != null)
                {
                    try
                    {
                        NacionalidadIndigenaActualizar.Nombre = NacionalidadIndigena.Nombre;
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

        // POST: api/NacionalidadIndigenaes
        [HttpPost]
        [Route("InsertarNacionalidadesIndigenas")]
        public async Task<Response> PostNacionalidadIndigena([FromBody] NacionalidadIndigena NacionalidadIndigena)
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

                var respuesta = Existe(NacionalidadIndigena);
                if (!respuesta.IsSuccess)
                {
                    db.NacionalidadIndigena.Add(NacionalidadIndigena);
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

        // DELETE: api/NacionalidadIndigenaes/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteNacionalidadIndigena([FromRoute] int id)
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

                var respuesta = await db.NacionalidadIndigena.SingleOrDefaultAsync(m => m.IdNacionalidadIndigena == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No existe ",
                    };
                }
                db.NacionalidadIndigena.Remove(respuesta);
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

        private Response Existe(NacionalidadIndigena NacionalidadIndigena)
        {
            var bdd = NacionalidadIndigena.Nombre;
            var NacionalidadIndigenarespuesta = db.NacionalidadIndigena.Where(p => p.Nombre == bdd).FirstOrDefault();
            if (NacionalidadIndigenarespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Existe un Nacionalidad Indigena de igual nombre",
                    Resultado = null,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = NacionalidadIndigenarespuesta,
            };
        }
    }
}