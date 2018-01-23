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
    [Route("api/SolicitudPlanificacionVacaciones")]
    public class SolicitudPlanificacionVacacionesController : Controller
    {
        private readonly SwTHDbContext db;

        public SolicitudPlanificacionVacacionesController(SwTHDbContext db)
        {
            this.db = db;
        }


        //// GET: api/SolicitudPlanificacionVacaciones
        //[HttpGet]
        //[Route("ListarSolicitudesPlanificacionesVacaciones")]
        //public async Task<List<SolicitudPlanificacionVacaciones>> GetSolicitudPlanificacionVacaciones()
        //{
        //    try
        //    {
        //        return await db.SolicitudPlanificacionVacaciones.Include(x => x.Empleado).OrderBy(x => x.FechaSolicitud).ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        await GuardarLogService.SaveLogEntry(new LogEntryTranfer
        //        {
        //            ApplicationName = Convert.ToString(Aplicacion.SwTH),
        //            ExceptionTrace = ex,
        //            Message = Mensaje.Excepcion,
        //            LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
        //            LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
        //            UserName = "",

        //        });
        //        return new List<SolicitudPlanificacionVacaciones>();
        //    }
        //}

        [HttpPost]
        [Route("ListarSolicitudesPlanificacionesVacaciones")]
        public async Task<List<SolicitudPlanificacionVacaciones>> ObtenerEmpleadoLogueado([FromBody]Empleado empleado)
        {
            //Persona persona = new Persona();
            try
            {

                var SolicitudPlanificacionVacaciones = await db.SolicitudPlanificacionVacaciones
                                   .Where(e => e.IdEmpleado == empleado.IdEmpleado).ToListAsync();
                //var empl = new Empleado { IdEmpleado = Empleado.IdEmpleado };


                return SolicitudPlanificacionVacaciones;
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
                return new List<SolicitudPlanificacionVacaciones>();
            }
        }

        // GET: api/SolicitudPlanificacionVacaciones/5
        [HttpGet("{id}")]
        public async Task<Response> GetSolicitudPlanificacionVacaciones([FromRoute] int id)
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

                var SolicitudPlanificacionVacaciones = await db.SolicitudPlanificacionVacaciones.SingleOrDefaultAsync(m => m.IdSolicitudPlanificacionVacaciones == id);

                if (SolicitudPlanificacionVacaciones == null)
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
                    Resultado = SolicitudPlanificacionVacaciones,
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

        // PUT: api/SolicitudPlanificacionVacaciones/5
        [HttpPut("{id}")]
        public async Task<Response> PutSolicitudPlanificacionVacaciones([FromRoute] int id, [FromBody] SolicitudPlanificacionVacaciones SolicitudPlanificacionVacaciones)
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

                var SolicitudPlanificacionVacacionesActualizar = await db.SolicitudPlanificacionVacaciones.Where(x => x.IdSolicitudPlanificacionVacaciones == SolicitudPlanificacionVacaciones.IdSolicitudPlanificacionVacaciones).FirstOrDefaultAsync();
                if (SolicitudPlanificacionVacacionesActualizar != null)
                {
                    try
                    {
                        SolicitudPlanificacionVacacionesActualizar.IdEmpleado = SolicitudPlanificacionVacaciones.IdEmpleado;
                        SolicitudPlanificacionVacacionesActualizar.FechaSolicitud = SolicitudPlanificacionVacaciones.FechaSolicitud;
                        SolicitudPlanificacionVacacionesActualizar.FechaDesde = SolicitudPlanificacionVacaciones.FechaDesde;
                        SolicitudPlanificacionVacacionesActualizar.FechaHasta = SolicitudPlanificacionVacaciones.FechaHasta;
                        SolicitudPlanificacionVacacionesActualizar.Observaciones = SolicitudPlanificacionVacaciones.Observaciones;
                        SolicitudPlanificacionVacacionesActualizar.Estado = SolicitudPlanificacionVacaciones.Estado;
                        
                        db.SolicitudPlanificacionVacaciones.Update(SolicitudPlanificacionVacacionesActualizar);
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

        // POST: api/SolicitudPlanificacionVacaciones
        [HttpPost]
        [Route("InsertarSolicitudPlanificacionVacaciones")]
        public async Task<Response> PostSolicitudPlanificacionVacaciones([FromBody] SolicitudPlanificacionVacaciones SolicitudPlanificacionVacaciones)
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

                //var respuesta = Existe(SolicitudPlanificacionVacaciones);
                //if (!respuesta.IsSuccess)
                //{
                    db.SolicitudPlanificacionVacaciones.Add(SolicitudPlanificacionVacaciones);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                        Resultado = SolicitudPlanificacionVacaciones
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

        // DELETE: api/SolicitudPlanificacionVacaciones/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteSolicitudPlanificacionVacaciones([FromRoute] int id)
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

                var respuesta = await db.SolicitudPlanificacionVacaciones.SingleOrDefaultAsync(m => m.IdSolicitudPlanificacionVacaciones == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.SolicitudPlanificacionVacaciones.Remove(respuesta);
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

      

        //public Response Existe(SolicitudPlanificacionVacaciones SolicitudPlanificacionVacaciones)
        //{
        //    var bdd1 = SolicitudPlanificacionVacaciones.IdEmpleado;
        //    var bdd2 = SolicitudPlanificacionVacaciones.FechaSolicitud;
        //    var loglevelrespuesta = db.SolicitudPlanificacionVacaciones.Where(p => p.IdEmpleado == bdd1 && p.FechaSolicitud == bdd2).FirstOrDefault();
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