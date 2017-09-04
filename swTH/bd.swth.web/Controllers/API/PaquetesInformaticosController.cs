using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.swth.entidades.Enumeradores;
using bd.log.guardar.Enumeradores;
using bd.swth.entidades.Utils;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/PaquetesInformaticos")]
    public class PaquetesInformaticosController : Controller
    {
        private readonly SwTHDbContext db;

        public PaquetesInformaticosController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/PaquetesInformaticoses
        [HttpGet]
        [Route("ListarPaquetesInformaticos")]
        public async Task<List<PaquetesInformaticos>> GetPaquetesInformaticos()
        {
            try
            {
                return await db.PaquetesInformaticos.OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<PaquetesInformaticos>();
            }
        }

        // GET: api/PaquetesInformaticoses/5
        [HttpGet("{id}")]
        public async Task<Response> GetPaquetesInformaticos([FromRoute] int id)
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

                var PaquetesInformaticos = await db.PaquetesInformaticos.SingleOrDefaultAsync(m => m.IdPaquetesInformaticos == id);

                if (PaquetesInformaticos == null)
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
                    Resultado = PaquetesInformaticos,
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

        // PUT: api/PaquetesInformaticoses/5
        [HttpPut("{id}")]
        public async Task<Response> PutPaquetesInformaticos([FromRoute] int id, [FromBody] PaquetesInformaticos PaquetesInformaticos)
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

                var existe = Existe(PaquetesInformaticos);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Existe un registro de igual Nombre",
                    };
                }

                var PaquetesInformaticosActualizar = await db.PaquetesInformaticos.Where(x => x.IdPaquetesInformaticos == id).FirstOrDefaultAsync();

                if (PaquetesInformaticosActualizar != null)
                {
                    try
                    {
                        PaquetesInformaticosActualizar.Nombre = PaquetesInformaticos.Nombre;
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

        // POST: api/PaquetesInformaticoses
        [HttpPost]
        [Route("InsertarPaquetesInformaticos")]
        public async Task<Response> PostPaquetesInformaticos([FromBody] PaquetesInformaticos PaquetesInformaticos)
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

                var respuesta = Existe(PaquetesInformaticos);
                if (!respuesta.IsSuccess)
                {
                    db.PaquetesInformaticos.Add(PaquetesInformaticos);
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

        // DELETE: api/PaquetesInformaticoses/5
        [HttpDelete("{id}")]
        public async Task<Response> DeletePaquetesInformaticos([FromRoute] int id)
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

                var respuesta = await db.PaquetesInformaticos.SingleOrDefaultAsync(m => m.IdPaquetesInformaticos == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No existe ",
                    };
                }
                db.PaquetesInformaticos.Remove(respuesta);
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

        private Response Existe(PaquetesInformaticos PaquetesInformaticos)
        {
            var bdd = PaquetesInformaticos.Nombre;
            var PaquetesInformaticosrespuesta = db.PaquetesInformaticos.Where(p => p.Nombre == bdd).FirstOrDefault();
            if (PaquetesInformaticosrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Existe un Paquetes Informáticos de igual nombre",
                    Resultado = null,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = PaquetesInformaticosrespuesta,
            };
        }

    }
}