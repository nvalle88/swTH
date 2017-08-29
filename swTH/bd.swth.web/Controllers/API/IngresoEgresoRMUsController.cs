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
    [Route("api/IngresoEgresoRMUs")]
    public class IngresoEgresoRMUsController : Controller
    {
        private readonly SwTHDbContext db;

        public IngresoEgresoRMUsController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/IngresoEgresoRMUs
        [HttpGet]
        [Route("ListarIngresoEgresoRMUs")]
        public async Task<List<IngresoEgresoRMU>> GetIngresoEgresoRMU()
        {
            try
            {
                return await db.IngresoEgresoRMU.OrderBy(x => x.CuentaContable).ToListAsync();
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
                return new List<IngresoEgresoRMU>();
            }
        }

        // GET: api/IngresoEgresoRMUs/5
        [HttpGet("{id}")]
        public async Task<Response> GetIngresoEgresoRMU([FromRoute] int id)
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

                var adscbdd = await db.IngresoEgresoRMU.SingleOrDefaultAsync(m => m.IdIngresoEgresoRMU == id);

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

        // PUT: api/IngresoEgresoRMUs/5
        [HttpPut("{id}")]
        public async Task<Response> PutIngresoEgresoRMU([FromRoute] int id, [FromBody] IngresoEgresoRMU ingresoEgresoRMU)
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


                try
                {
                    var entidad = await db.IngresoEgresoRMU.Where(x => x.IdIngresoEgresoRMU == id).FirstOrDefaultAsync();

                    if (entidad == null)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "No existe información acerca del Ingreso Egreso RMU ",
                        };

                    }
                    else
                    {

                        entidad.CuentaContable = ingresoEgresoRMU.CuentaContable;
                        //entidad.Descripcion = ingresoEgresoRMU.Descripcion;
                        //entidad.IdFormulaRMU = ingresoEgresoRMU.IdFormulaRMU;
                        //entidad.FormulasRMU = ingresoEgresoRMU.FormulasRMU;
                        db.IngresoEgresoRMU.Update(entidad);
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

        // POST: api/IngresoEgresoRMUs
        [HttpPost]
        [Route("InsertarIngresoEgresoRMU")]
        public async Task<Response> PostIngresoEgresoRMU([FromBody] IngresoEgresoRMU ingresoEgresoRMU)
        {
            try
            {

                var respuesta = Existe(ingresoEgresoRMU.CuentaContable);
                if (!respuesta.IsSuccess)
                {
                    db.IngresoEgresoRMU.Add(ingresoEgresoRMU);
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

        // DELETE: api/IngresoEgresoRMUs/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteIngresoEgresoRMU([FromRoute] int id)
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

                var respuesta = await db.IngresoEgresoRMU.SingleOrDefaultAsync(m => m.IdIngresoEgresoRMU == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No existe ",
                    };
                }
                db.IngresoEgresoRMU.Remove(respuesta);
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

        private bool IngresoEgresoRMUExists(int id)
        {
            return db.IngresoEgresoRMU.Any(e => e.IdIngresoEgresoRMU == id);
        }


        public Response Existe(string nombreIngresoEgresoRMU)
        {

            var loglevelrespuesta = db.IngresoEgresoRMU.Where(p => p.CuentaContable.ToUpper().TrimStart().TrimEnd() == nombreIngresoEgresoRMU).FirstOrDefault();
            if (loglevelrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Existe un sistema de igual cuenta contable",
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