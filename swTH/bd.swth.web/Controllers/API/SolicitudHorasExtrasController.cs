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

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/SolicitudHorasExtras")]
    public class SolicitudHorasExtrasController : Controller
    {
        private readonly SwTHDbContext db;

        public SolicitudHorasExtrasController(SwTHDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        [Route("ListarSolicitudesHorasExtra")]
        public async Task<List<SolicitudHorasExtras>> ListarSolicitudHorasExtras([FromBody]Empleado empleado)
        {
            //Persona persona = new Persona();
            try
            {

                var SolicitudHorasExtras = await db.SolicitudHorasExtras
                                   .Where(e => e.IdEmpleado == empleado.IdEmpleado).ToListAsync();
                //var empl = new Empleado { IdEmpleado = Empleado.IdEmpleado };


                return SolicitudHorasExtras;
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
                return new List<SolicitudHorasExtras>();
            }
        }

        // GET: api/SolicitudHorasExtras/5
        [HttpGet("{id}")]
        public async Task<Response> GetSolicitudHorasExtras([FromRoute] int id)
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

                var SolicitudHorasExtras = await db.SolicitudHorasExtras.SingleOrDefaultAsync(m => m.IdSolicitudHorasExtras == id);

                if (SolicitudHorasExtras == null)
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
                    Resultado = SolicitudHorasExtras,
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

        // PUT: api/SolicitudHorasExtras/5
        [HttpPut("{id}")]
        public async Task<Response> PutSolicitudHorasExtras([FromRoute] int id, [FromBody] SolicitudHorasExtras SolicitudHorasExtras)
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

                var SolicitudHorasExtrasActualizar = await db.SolicitudHorasExtras.Where(x => x.IdSolicitudHorasExtras == SolicitudHorasExtras.IdSolicitudHorasExtras).FirstOrDefaultAsync();
                if (SolicitudHorasExtrasActualizar != null)
                {
                    try
                    {
                        
                        SolicitudHorasExtrasActualizar.IdEmpleado = id;
                        SolicitudHorasExtrasActualizar.Fecha = SolicitudHorasExtras.Fecha;
                        SolicitudHorasExtrasActualizar.CantidadHoras = SolicitudHorasExtras.CantidadHoras;
                        SolicitudHorasExtrasActualizar.Estado = 0;
                        SolicitudHorasExtrasActualizar.Observaciones = SolicitudHorasExtras.Observaciones;
                        SolicitudHorasExtrasActualizar.FechaSolicitud = SolicitudHorasExtras.FechaSolicitud;
                        SolicitudHorasExtrasActualizar.FechaAprobado = SolicitudHorasExtras.FechaAprobado;
                        SolicitudHorasExtrasActualizar.EsExtraordinaria = SolicitudHorasExtras.EsExtraordinaria;
                        db.SolicitudHorasExtras.Update(SolicitudHorasExtrasActualizar);
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
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ExisteRegistro
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };
            }
        }

        // POST: api/SolicitudHorasExtras
        [HttpPost]
        [Route("InsertarSolicitudHorasExtras")]
        public async Task<Response> PostSolicitudHorasExtras([FromBody] SolicitudHorasExtras SolicitudHorasExtras)
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

                //var respuesta = Existe(SolicitudHorasExtras);
                //if (!respuesta.IsSuccess)
                //{
                SolicitudHorasExtras.Estado = 0;
                db.SolicitudHorasExtras.Add(SolicitudHorasExtras);
                await db.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                    Resultado = SolicitudHorasExtras
                };
                //}

                //return new Response
                //{
                //    IsSuccess = false,
                //    Message = Mensaje.Satisfactorio
                //};

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

        [HttpPost]
        [Route("EsExtraordinaria")]
        public async Task<Response> EsExtraordinaria([FromBody] SolicitudHorasExtras SolicitudHorasExtras)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return new Response
                //    {
                //        IsSuccess = false,
                //        Message = Mensaje.ModeloInvalido
                //    };
                //}

                var diaSemana = (int)SolicitudHorasExtras.Fecha.Date.DayOfWeek;

                if (diaSemana < 1 || diaSemana > 5)
                {
                    return new Response { IsSuccess = true };
                }


                var feriadoAno =await db.ConfiguracionFeriados.Where(x => x.FechaHasta.Year == DateTime.Now.Year).ToListAsync();

                foreach (var item in feriadoAno)
                {
                    if (item.FechaDesde.Date <=SolicitudHorasExtras.Fecha.Date && item.FechaHasta.Date>=SolicitudHorasExtras.Fecha.Date )
                    {
                        return new Response { IsSuccess = true };
                    }
                };



                return new Response { IsSuccess = false };

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

        // DELETE: api/SolicitudHorasExtras/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteSolicitudHorasExtras([FromRoute] int id)
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

                var respuesta = await db.SolicitudHorasExtras.SingleOrDefaultAsync(m => m.IdSolicitudHorasExtras == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.SolicitudHorasExtras.Remove(respuesta);
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



        //public Response Existe(SolicitudHorasExtras SolicitudHorasExtras)
        //{
        //    var bdd1 = SolicitudHorasExtras.IdEmpleado;
        //    var bdd2 = SolicitudHorasExtras.FechaSolicitud;
        //    var loglevelrespuesta = db.SolicitudHorasExtras.Where(p => p.IdEmpleado == bdd1 && p.FechaSolicitud == bdd2).FirstOrDefault();
        //    if (loglevelrespuesta != null)
        //    {
        //        return new Response
        //        {
        //            IsSuccess = true,
        //            Message = Mensaje.BorradoNoSatisfactorio,
        //            Resultado = null,
        //        };

        //    }
        //    return new Response
        //    {
        //        IsSuccess = false,
        //        Resultado = loglevelrespuesta,
        //    };
        //}
    }
}