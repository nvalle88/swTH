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
    [Route("api/FormulariosDevengacion")]
    public class FormulariosDevengacionController : Controller
    {
        private readonly SwTHDbContext db;

        public FormulariosDevengacionController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/FormularioDevengaciones
        [HttpGet]
        [Route("ListarFormulariosDevengacion")]
        public async Task<List<FormularioDevengacion>> GetFormularioDevengacion()
        {
            try
            {
                // return await db.FormularioDevengacion.Include(x => x.ModosScializacion).Include(x => x.Empleado).Include(x => x.AnalistaDesarrolloInstitucional).Include(x => x.ResponsableArea).OrderBy(x => x.ModoSocial).ToListAsync();
                return await db.FormularioDevengacion.Include(x => x.ModosScializacion).OrderBy(x => x.ModoSocial).ToListAsync();
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex.Message,
                    Message = "Se ha producido una excepción",
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new List<FormularioDevengacion>();
            }
        }

        // GET: api/FormularioDevengaciones/5
        [HttpGet("{id}")]
        public async Task<Response> GetFormularioDevengacion([FromRoute] int id)
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

                var FormularioDevengacion = await db.FormularioDevengacion.SingleOrDefaultAsync(m => m.IdFormularioDevengacion == id);

                if (FormularioDevengacion == null)
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
                    Resultado = FormularioDevengacion,
                };
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex.Message,
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

        // PUT: api/FormularioDevengaciones/5
        [HttpPut("{id}")]
        public async Task<Response> PutFormularioDevengacion([FromRoute] int id, [FromBody] FormularioDevengacion FormularioDevengacion)
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

                var existe = Existe(FormularioDevengacion);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Existe un registro de igual modo social",
                    };
                }

                var FormularioDevengacionActualizar = await db.FormularioDevengacion.Where(x => x.IdFormularioDevengacion == id).FirstOrDefaultAsync();

                if (FormularioDevengacionActualizar != null)
                {
                    try
                    {
                        FormularioDevengacionActualizar.ModoSocial = FormularioDevengacion.ModoSocial;
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
                            ExceptionTrace = ex.Message,
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

        // POST: api/FormularioDevengaciones
        [HttpPost]
        [Route("InsertarFormulariosDevengacion")]
        public async Task<Response> PostFormularioDevengacion([FromBody] FormularioDevengacion FormularioDevengacion)
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

                var respuesta = Existe(FormularioDevengacion);
                if (!respuesta.IsSuccess)
                {
                    db.FormularioDevengacion.Add(FormularioDevengacion);
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
                    Message = "Existe un registro de igual modo social..."
                };

            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex.Message,
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

        // DELETE: api/FormularioDevengaciones/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteFormularioDevengacion([FromRoute] int id)
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

                var respuesta = await db.FormularioDevengacion.SingleOrDefaultAsync(m => m.IdFormularioDevengacion == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No existe ",
                    };
                }
                db.FormularioDevengacion.Remove(respuesta);
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
                    ExceptionTrace = ex.Message,
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

        private Response Existe(FormularioDevengacion FormularioDevengacion)
        {
            var bdd = FormularioDevengacion.ModoSocial;
            var FormularioDevengacionrespuesta = db.FormularioDevengacion.Where(p => p.ModoSocial == bdd).FirstOrDefault();
            if (FormularioDevengacionrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Existe un formulario devengación de igual modo social",
                    Resultado = null,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = FormularioDevengacionrespuesta,
            };
        }

    }
}