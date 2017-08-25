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
    [Route("api/RelacionesInternasExternas")]
    public class RelacionesInternasExternasController : Controller
    {
        private readonly SwTHDbContext db;

        public RelacionesInternasExternasController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/RelacionesInternasExternas
        [HttpGet]
        [Route("ListarRelacionesInternasExternas")]
        public async Task<List<RelacionesInternasExternas>> GetRelacionesInternasExternas()
        {
            try
            {
                return await db.RelacionesInternasExternas.OrderBy(x => x.Descripcion).ToListAsync();
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
                return new List<RelacionesInternasExternas>();
            }
        }

        // GET: api/RelacionesInternasExternas/5
        [HttpGet("{id}")]
        public async Task<Response> GetRelacionesInternasExternas([FromRoute] int id)
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

                var relacionesInternasExternas = await db.RelacionesInternasExternas.SingleOrDefaultAsync(m => m.IdRelacionesInternasExternas == id);

                if (relacionesInternasExternas == null)
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
                    Resultado = relacionesInternasExternas,
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

        // POST: api/RelacionesInternasExternas
        [HttpPost]
        [Route("InsertarRelacionesInternasExternas")]
        public async Task<Response> PostRelacionesInternasExternas([FromBody] RelacionesInternasExternas relacionesInternasExternas)
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

                var respuesta = Existe(relacionesInternasExternas);
                if (!respuesta.IsSuccess)
                {
                    db.RelacionesInternasExternas.Add(relacionesInternasExternas);
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
                    Message = "OK"
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

        // PUT: api/RelacionesInternasExternas/5
        [HttpPut("{id}")]
        public async Task<Response> PutRelacionesInternasExternas([FromRoute] int id, [FromBody] RelacionesInternasExternas relacionesInternasExternas)
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

                var RelacionesInternasExternasActualizar = await db.RelacionesInternasExternas.Where(x => x.IdRelacionesInternasExternas == id).FirstOrDefaultAsync();
                if (RelacionesInternasExternasActualizar != null)
                {
                    try
                    {

                        RelacionesInternasExternasActualizar.Descripcion = relacionesInternasExternas.Descripcion;
                        db.RelacionesInternasExternas.Update(RelacionesInternasExternasActualizar);
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

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteRelacionesInternasExternas([FromRoute] int id)
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

                var respuesta = await db.RelacionesInternasExternas.SingleOrDefaultAsync(m => m.IdRelacionesInternasExternas == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No existe ",
                    };
                }
                db.RelacionesInternasExternas.Remove(respuesta);
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

        private Response Existe(RelacionesInternasExternas relacionesInternasExternas)
        {
            var bdd = relacionesInternasExternas.Descripcion.ToUpper().TrimEnd().TrimStart();
            var RelacionesInternasExternasrespuesta = db.RelacionesInternasExternas.Where(p => p.Descripcion.ToUpper().TrimStart().TrimEnd() == bdd).FirstOrDefault();
            if (RelacionesInternasExternasrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Existe una relación interna y externa de igual descripción",
                    Resultado = null,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = RelacionesInternasExternasrespuesta,
            };
        }
    }
}
