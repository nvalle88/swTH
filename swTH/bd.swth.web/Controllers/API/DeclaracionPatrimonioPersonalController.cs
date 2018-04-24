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
using bd.swth.entidades.Utils;
using bd.log.guardar.Enumeradores;
using bd.swth.entidades.ViewModels;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/DeclaracionPatrimonioPersonal")]
    public class DeclaracionPatrimonioPersonalController : Controller
    {
        private readonly SwTHDbContext db;

        public DeclaracionPatrimonioPersonalController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarDeclaracionesPatrimonioPersonal")]
        public async Task<List<DeclaracionPatrimonioPersonal>> GetDeclaracionPatrimonioPersonal()
        {
            try
            {
                return await db.DeclaracionPatrimonioPersonal.OrderBy(x => x.FechaDeclaracion).ToListAsync();
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
                return new List<DeclaracionPatrimonioPersonal>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetDeclaracionPatrimonioPersonal([FromRoute] int id)
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

                var DeclaracionPatrimonioPersonal = await db.DeclaracionPatrimonioPersonal.SingleOrDefaultAsync(m => m.IdDeclaracionPatrimonioPersonal == id);

                if (DeclaracionPatrimonioPersonal == null)
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
                    Resultado = DeclaracionPatrimonioPersonal,
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

        //// PUT: api/BasesDatos/5
        //[HttpPut("{id}")]
        //public async Task<Response> PutDeclaracionPatrimonioPersonal([FromRoute] int id, [FromBody] DeclaracionPatrimonioPersonal declaracionPatrimonioPersonal)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = Mensaje.ModeloInvalido
        //            };
        //        }

        //        var existe = ExisteDeclaracionPatrimonioPersonal(declaracionPatrimonioPersonal);
        //        if (existe.IsSuccess)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = Mensaje.ExisteRegistro,
        //            };
        //        }

        //        var declaracionPatrimonioPersonalActualizar = await db.DeclaracionPatrimonioPersonal.Where(x => x.IdDeclaracionPatrimonioPersonal == id).FirstOrDefaultAsync();

        //        if (declaracionPatrimonioPersonalActualizar != null)
        //        {
        //            try
        //            {
        //                declaracionPatrimonioPersonalActualizar.IdEmpleado = declaracionPatrimonioPersonal.IdEmpleado;
        //                declaracionPatrimonioPersonalActualizar.FechaDeclaracion = declaracionPatrimonioPersonal.FechaDeclaracion;
        //                declaracionPatrimonioPersonalActualizar.TotalEfectivo = declaracionPatrimonioPersonal.TotalEfectivo;
        //                declaracionPatrimonioPersonalActualizar.TotalBienInmueble = declaracionPatrimonioPersonal.TotalBienInmueble;
        //                declaracionPatrimonioPersonalActualizar.TotalBienMueble = declaracionPatrimonioPersonal.TotalBienMueble;
        //                declaracionPatrimonioPersonalActualizar.TotalPasivo = declaracionPatrimonioPersonal.TotalPasivo;
        //                declaracionPatrimonioPersonalActualizar.TotalPatrimonio = declaracionPatrimonioPersonal.TotalPatrimonio;
        //                await db.SaveChangesAsync();

        //                return new Response
        //                {
        //                    IsSuccess = true,
        //                    Message = Mensaje.Satisfactorio,
        //                };

        //            }
        //            catch (Exception ex)
        //            {
        //                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
        //                {
        //                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
        //                    ExceptionTrace = ex.Message,
        //                    Message = Mensaje.Excepcion,
        //                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
        //                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
        //                    UserName = "",

        //                });
        //                return new Response
        //                {
        //                    IsSuccess = false,
        //                    Message = Mensaje.Error,
        //                };
        //            }
        //        }




        //        return new Response
        //        {
        //            IsSuccess = false,
        //            Message = Mensaje.ExisteRegistro
        //        };
        //    }
        //    catch (Exception)
        //    {
        //        return new Response
        //        {
        //            IsSuccess = false,
        //            Message = Mensaje.Excepcion
        //        };
        //    }
        //}

        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarDeclaracionPatrimonioPersonal")]
        public async Task<Response> PostDeclaracionPatrimonioPersonal([FromBody] ViewModelDeclaracionPatrimonioPersonal viewModelDeclaracionPatrimonioPersonal)
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

                var respuesta = ExisteDeclaracionPatrimonioPersonal(viewModelDeclaracionPatrimonioPersonal);
                if (!respuesta.IsSuccess)
                {
                    var declaracionpersonal = db.DeclaracionPatrimonioPersonal.Add(viewModelDeclaracionPatrimonioPersonal.DeclaracionPatrimonioPersonal);
                    await db.SaveChangesAsync();

                    viewModelDeclaracionPatrimonioPersonal.OtroIngreso.IdDeclaracionPatrimonioPersonal = declaracionpersonal.Entity.IdDeclaracionPatrimonioPersonal;
                    var otrosingresos = await db.OtroIngreso.AddAsync(viewModelDeclaracionPatrimonioPersonal.OtroIngreso);
                    await db.SaveChangesAsync();

                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                        
                    };
                }
                

                return new Response
                {
                    IsSuccess = false,
                    Message = "No puede realizar una Declaración de Patrimonio Personal del mismo año o superior"
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }

        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarOtrosIngresos")]
        public async Task<Response> PostOtrosIngresos([FromBody] OtroIngreso otroIngreso)
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

                var respuesta = ExisteOtroIngreso(otroIngreso);
                if (!respuesta.IsSuccess)
                {
                    db.OtroIngreso.Add(otroIngreso);
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
                    Message = Mensaje.ExisteRegistro
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
        public async Task<Response> DeleteDeclaracionPatrimonioPersonal([FromRoute] int id)
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

                var respuesta = await db.DeclaracionPatrimonioPersonal.SingleOrDefaultAsync(m => m.IdDeclaracionPatrimonioPersonal == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.DeclaracionPatrimonioPersonal.Remove(respuesta);
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

        private Response ExisteDeclaracionPatrimonioPersonal(ViewModelDeclaracionPatrimonioPersonal viewModelDeclaracionPatrimonioPersonal)
        {
            var bdd = viewModelDeclaracionPatrimonioPersonal.DeclaracionPatrimonioPersonal.IdEmpleado;
            var bdd2 = viewModelDeclaracionPatrimonioPersonal.DeclaracionPatrimonioPersonal.FechaDeclaracion.Date.Year;
            var declaracionrespuesta = db.DeclaracionPatrimonioPersonal.Where(p => p.IdEmpleado == bdd && p.FechaDeclaracion.Date.Year == bdd2).FirstOrDefault();
            if (declaracionrespuesta != null || bdd2 > DateTime.Now.Year)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "No puede realizar nuevamente una Declaración de Patrimonio Personal del mismo año o superior",
                    Resultado = null,
                };

            }
            

            return new Response
            {
                IsSuccess = false,
                Resultado = declaracionrespuesta,
            };
        }

        private Response ExisteOtroIngreso(OtroIngreso otroIngreso)
        {
            var bdd = otroIngreso.IdDeclaracionPatrimonioPersonal;
            var otroingresorespuesta = db.OtroIngreso.Where(p => p.IdDeclaracionPatrimonioPersonal == bdd).FirstOrDefault();
            if (otroingresorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = null,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = otroIngreso,
            };
        }
    }
}