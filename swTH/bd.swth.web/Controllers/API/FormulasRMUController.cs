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
    [Route("api/FormulasRMU")]
    public class FormulasRMUController : Controller
    {
        private readonly SwTHDbContext db;

        public FormulasRMUController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/FormulasRMU
        [HttpGet]
        [Route("ListarFormulasRmu")]
        public async Task<List<FormulasRMU>> GetFormulasRmu()
        {
            try
            {
                return await db.FormulasRMU.OrderBy(x => x.Formula).ToListAsync();
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
                return new List<FormulasRMU>();
            }
        }

        // GET: api/FormulasRmus/5
        [HttpGet("{id}")]
        public async Task<Response> GetFormulasRmu([FromRoute] int id)
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

                var adscbdd = await db.FormulasRMU.SingleOrDefaultAsync(m => m.IdFormulaRMU == id);

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

        // PUT: api/FormulasRmus/5
        [HttpPut("{id}")]
        public async Task<Response> PutFormulasRmu([FromRoute] int id, [FromBody] FormulasRMU formulasRmu)
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

                var existeFormulasRmu = Existe(formulasRmu.Formula);
                if (existeFormulasRmu.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Existe un FormulasRmu de igual fórmula",
                    };
                }
                try
                {
                    var entidad = await db.FormulasRMU.Where(x => x.IdFormulaRMU == id).FirstOrDefaultAsync();

                    if (entidad == null)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "No existe información acerca de el Formulario RMU ",
                        };

                    }
                    else
                    {

                        entidad.Formula = formulasRmu.Formula;
                        db.FormulasRMU.Update(entidad);
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

        // api/FormulasRMU
        [HttpPost]
        [Route("InsertarFormulasRmu")]
        public async Task<Response> PostFormulasRmu([FromBody] FormulasRMU FormulasRmu)
        {
            try
            {

                var respuesta = Existe(FormulasRmu.Formula);
                if (!respuesta.IsSuccess)
                {
                    db.FormulasRMU.Add(FormulasRmu);
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

        // DELETE: api/FormulasRMU/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteFormulasRmu([FromRoute] int id)
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

                var respuesta = await db.FormulasRMU.SingleOrDefaultAsync(m => m.IdFormulaRMU == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No existe ",
                    };
                }
                db.FormulasRMU.Remove(respuesta);
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

        private bool FormulasRmuExists(int id)
        {
            return db.FormulasRMU.Any(e => e.IdFormulaRMU == id);
        }


        public Response Existe(string nombreFormulasRmu)
        {

            var loglevelrespuesta = db.FormulasRMU.Where(p => p.Formula.ToUpper().TrimStart().TrimEnd() == nombreFormulasRmu).FirstOrDefault();
            if (loglevelrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Existe un sistema de igual formula",
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