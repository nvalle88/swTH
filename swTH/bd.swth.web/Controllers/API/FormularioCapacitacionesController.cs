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
    [Route("api/FormularioCapacitaciones")]
    public class FormularioCapacitacionesController : Controller
    {
        private readonly SwTHDbContext db;

        public FormularioCapacitacionesController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/FormularioCapacitaciones
        [HttpGet]
        [Route("ListarFormularioCapacitaciones")]
        public async Task<List<FormularioCapacitacion>> GetFormularioCapacitacion()
        {
            try
            {
                return await db.FormularioCapacitacion.OrderBy(x => x.Descripcion).ToListAsync();
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
                return new List<FormularioCapacitacion>();
            }
        }

        // GET: api/FormularioCapacitaciones/5
        [HttpGet("{id}")]
        public async Task<Response> GetFormularioCapacitacion([FromRoute] int id)
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

                var adscbdd = await db.FormularioCapacitacion.SingleOrDefaultAsync(m => m.IdFormularioCapacitacion == id);

                if (adscbdd == null)
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
                    Message = "Error ",
                };
            }
        }

        // PUT: api/FormularioCapacitaciones/5
        [HttpPut("{id}")]
        public async Task<Response> PutFormularioCapacitacion([FromRoute] int id, [FromBody] FormularioCapacitacion formularioCapacitacion)
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

                var existeFormularioCapacitacion = Existe(formularioCapacitacion.Descripcion);
                if (existeFormularioCapacitacion.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Existe un FormularioCapacitacion de igual descripción",
                    };
                }
                try
                {
                    var entidad = await  db.FormularioCapacitacion.Where(x => x.IdFormularioCapacitacion == id).FirstOrDefaultAsync();

                    if (entidad == null)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "No existe información acerca de el Formulario de Capcitación ",
                        };

                    }
                    else
                    {

                        entidad.Descripcion = formularioCapacitacion.Descripcion;
                        db.FormularioCapacitacion.Update(entidad);
                        await db.SaveChangesAsync();
                        return new Response
                        {
                            IsSuccess = true,
                            Message = "Ok",
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
                        Message = "Error ",
                    };
                }


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

        // POST: api/FormularioCapacitaciones
        [HttpPost]
        [Route("InsertarFormularioCapacitacion")]
        public async Task<Response> PostFormularioCapacitacion([FromBody] FormularioCapacitacion formularioCapacitacion)
        {
            try
            {

                var respuesta = Existe(formularioCapacitacion.Descripcion);
                if (!respuesta.IsSuccess)
                {
                    db.FormularioCapacitacion.Add(formularioCapacitacion);
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
                    Message = "Se ha producido una exepción",
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

        // DELETE: api/FormularioCapacitaciones/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteFormularioCapacitacion([FromRoute] int id)
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

                var respuesta = await db.FormularioCapacitacion.SingleOrDefaultAsync(m => m.IdFormularioCapacitacion == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No existe ",
                    };
                }
                db.FormularioCapacitacion.Remove(respuesta);
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
                    Message = "Se ha producido una exepción",
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

        private bool FormularioCapacitacionExists(int id)
        {
            return db.FormularioCapacitacion.Any(e => e.IdFormularioCapacitacion == id);
        }


        public Response Existe(string nombreFormularioCapacitacion)
        {

            var loglevelrespuesta = db.FormularioCapacitacion.Where(p => p.Descripcion.ToUpper().TrimStart().TrimEnd() == nombreFormularioCapacitacion).FirstOrDefault();
            if (loglevelrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Existe un sistema de igual descripción",
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