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
    [Route("api/IngresoEgresoRMU")]
    public class IngresoEgresoRMUController : Controller
    {
        private readonly SwTHDbContext db;

        public IngresoEgresoRMUController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarIngresoEgresoRMU")]
        public async Task<List<IngresoEgresoRMU>> GetCapacitacionesTemarios()
        {
            try
            {
                return await db.IngresoEgresoRMU.Include(x => x.FormulasRMU).OrderBy(x => x.Descripcion).ToListAsync();
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex.Message,
                    Message = Mensaje.Excepcion,
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new List<IngresoEgresoRMU>();
            }
        }

        // GET: api/BasesDatos/5
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
                        Message = Mensaje.ModeloInvalido,
                    };
                }

                var IngresoEgresoRMU = await db.IngresoEgresoRMU.SingleOrDefaultAsync(m => m.IdIngresoEgresoRMU == id);

                if (IngresoEgresoRMU == null)
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
                    Resultado = IngresoEgresoRMU,
                };
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex.Message,
                    Message = Mensaje.Excepcion,
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

        // PUT: api/BasesDatos/5
        [HttpPut("{id}")]
        public async Task<Response> PutIngresoEgresoRMU([FromRoute] int id, [FromBody] IngresoEgresoRMU IngresoEgresoRMU)
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

                var existe = Existe(IngresoEgresoRMU);
                var IngresoEgresoRMUActualizar = (IngresoEgresoRMU)existe.Resultado;
                if (existe.IsSuccess)
                {
                    //if (IngresoEgresoRMUActualizar.IdIngresoEgresoRMU == IngresoEgresoRMU.IdIngresoEgresoRMU)
                    //{
                    //    return new Response
                    //    {
                    //        IsSuccess = true,
                    //    };
                    //}
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }
                var ingresoegresormu = db.IngresoEgresoRMU.Find(IngresoEgresoRMU.IdIngresoEgresoRMU);

                ingresoegresormu.IdFormulaRMU = IngresoEgresoRMU.IdFormulaRMU;
                ingresoegresormu.Descripcion = IngresoEgresoRMU.Descripcion;
                ingresoegresormu.CuentaContable = IngresoEgresoRMU.CuentaContable;
                db.IngresoEgresoRMU.Update(ingresoegresormu);
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
                    ExceptionTrace = ex.Message,
                    Message = Mensaje.Excepcion,
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Excepcion,
                };
            }

        }

        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarIngresoEgresoRMU")]
        public async Task<Response> PostIngresoEgresoRMU([FromBody] IngresoEgresoRMU IngresoEgresoRMU)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = ""
                    };
                }

                var respuesta = Existe(IngresoEgresoRMU);
                if (!respuesta.IsSuccess)
                {
                    db.IngresoEgresoRMU.Add(IngresoEgresoRMU);
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
                    Message = Mensaje.ExisteRegistro,
                };

            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex.Message,
                    Message = Mensaje.Excepcion,
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

        // DELETE: api/BasesDatos/5
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
                        Message = Mensaje.ModeloInvalido,
                    };
                }

                var respuesta = await db.IngresoEgresoRMU.SingleOrDefaultAsync(m => m.IdIngresoEgresoRMU == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.IngresoEgresoRMU.Remove(respuesta);
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
                    ExceptionTrace = ex.Message,
                    Message = Mensaje.Excepcion,
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

        private Response Existe(IngresoEgresoRMU IngresoEgresoRMU)
        {
            var bdd = IngresoEgresoRMU.Descripcion.ToUpper().TrimEnd().TrimStart();
            var bdd1 = IngresoEgresoRMU.IdIngresoEgresoRMU;
            var cuenta = IngresoEgresoRMU.CuentaContable;
            var IngresoEgresoRMUrespuesta = db.IngresoEgresoRMU.Where(p => p.Descripcion.ToUpper().TrimStart().TrimEnd() == bdd && p.IdFormulaRMU == bdd1  && p.CuentaContable==cuenta).FirstOrDefault();
            if (IngresoEgresoRMUrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = IngresoEgresoRMUrespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = IngresoEgresoRMUrespuesta,
            };
        }

    }
}