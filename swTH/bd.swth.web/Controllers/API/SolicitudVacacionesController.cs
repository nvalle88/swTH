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
    [Route("api/SolicitudVacaciones")]
    public class SolicitudVacacionesController : Controller
    {
        private readonly SwTHDbContext db;

        public SolicitudVacacionesController(SwTHDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        [Route("ListarSolicitudVacaciones")]
        public async Task<List<SolicitudVacaciones>> ListarSolicitudVacaciones([FromBody]Empleado empleado)
        {
            //Persona persona = new Persona();
            try
            {

                var SolicitudVacaciones = await db.SolicitudVacaciones
                                   .Where(e => e.IdEmpleado == empleado.IdEmpleado).ToListAsync();
                //var empl = new Empleado { IdEmpleado = Empleado.IdEmpleado };


                return SolicitudVacaciones;
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = Mensaje.Excepcion,
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new List<SolicitudVacaciones>();
            }
        }

        [HttpPost]
        [Route("ListarSolicitudesVacaciones")]
        public async Task<List<SolicitudVacaciones>> ObtenerEmpleadoLogueado([FromBody]Empleado empleado)
        {
            //Persona persona = new Persona();
            try
            {

                var SolicitudVacaciones = await db.SolicitudVacaciones
                                   .Where(e => e.IdEmpleado == empleado.IdEmpleado).ToListAsync();
                //var empl = new Empleado { IdEmpleado = Empleado.IdEmpleado };


                return SolicitudVacaciones;
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = Mensaje.Excepcion,
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new List<SolicitudVacaciones>();
            }
        }

        // GET: api/SolicitudVacaciones/5
        [HttpGet("{id}")]
        public async Task<Response> GetSolicitudVacaciones([FromRoute] int id)
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

                var SolicitudVacaciones = await db.SolicitudVacaciones.SingleOrDefaultAsync(m => m.IdSolicitudVacaciones == id);

                if (SolicitudVacaciones == null)
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
                    Resultado = SolicitudVacaciones,
                };
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
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

        // PUT: api/SolicitudVacaciones/5
        [HttpPut("{id}")]
        public async Task<Response> PutSolicitudVacaciones([FromRoute] int id, [FromBody] SolicitudVacaciones SolicitudVacaciones)
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

                var SolicitudVacacionesActualizar = await db.SolicitudVacaciones.Where(x => x.IdSolicitudVacaciones == SolicitudVacaciones.IdSolicitudVacaciones).FirstOrDefaultAsync();
                if (SolicitudVacacionesActualizar != null)
                {
                    try
                    {
                        SolicitudVacacionesActualizar.IdEmpleado = SolicitudVacaciones.IdEmpleado;
                        SolicitudVacacionesActualizar.FechaSolicitud = SolicitudVacaciones.FechaSolicitud;
                        SolicitudVacacionesActualizar.FechaDesde = SolicitudVacaciones.FechaDesde;
                        SolicitudVacacionesActualizar.FechaHasta = SolicitudVacaciones.FechaHasta;
                        SolicitudVacacionesActualizar.Observaciones = SolicitudVacaciones.Observaciones;
                        SolicitudVacacionesActualizar.Estado = SolicitudVacaciones.Estado;

                        db.SolicitudVacaciones.Update(SolicitudVacacionesActualizar);
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
                            ExceptionTrace = ex,
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

        // POST: api/SolicitudVacaciones
        [HttpPost]
        [Route("InsertarSolicitudVacaciones")]
        public async Task<Response> PostSolicitudVacaciones([FromBody] SolicitudVacaciones SolicitudVacaciones)
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

                //var respuesta = Existe(SolicitudVacaciones);
                //if (!respuesta.IsSuccess)
                //{
                db.SolicitudVacaciones.Add(SolicitudVacaciones);
                await db.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                    Resultado = SolicitudVacaciones
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
                    ExceptionTrace = ex,
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

        // DELETE: api/SolicitudVacaciones/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteSolicitudVacaciones([FromRoute] int id)
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

                var respuesta = await db.SolicitudVacaciones.SingleOrDefaultAsync(m => m.IdSolicitudVacaciones == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.SolicitudVacaciones.Remove(respuesta);
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
                    ExceptionTrace = ex,
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



        //public Response Existe(SolicitudVacaciones SolicitudVacaciones)
        //{
        //    var bdd1 = SolicitudVacaciones.IdEmpleado;
        //    var bdd2 = SolicitudVacaciones.FechaSolicitud;
        //    var loglevelrespuesta = db.SolicitudVacaciones.Where(p => p.IdEmpleado == bdd1 && p.FechaSolicitud == bdd2).FirstOrDefault();
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