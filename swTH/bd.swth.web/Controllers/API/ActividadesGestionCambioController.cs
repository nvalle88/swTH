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
using bd.swth.entidades.ViewModels;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/ActividadesGestionCambio")]
    public class ActividadesGestionCambioController : Controller
    {
        private readonly SwTHDbContext db;

        public ActividadesGestionCambioController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/ActividadesGestionCambio
        [HttpGet]
        [Route("ListarActividadesGestionCambio")]
        public async Task<List<ActividadesGestionCambio>> GetActividadesGestionCambio()
        {
            try
            {
                return await db.ActividadesGestionCambio.OrderBy(x => x.FechaInicio).ToListAsync();
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
                return new List<ActividadesGestionCambio>();
            }
        }
        //int IdPlanGestionCambio - ListarActividadesGestionCambioconIdPlan
        // POST: api/ActividadesGestionCambio
        [HttpPost]
        [Route("ListarActividadesGestionCambioconIdPlan")]
        public async Task<List<ActividadesGestionCambioIndex>> ListarActividadesGestionCambioconIdPlan([FromBody] ActividadesGestionCambio actividadesGestionCambio)
        {
            try
            {


                List<ActividadesGestionCambioIndex> listaActividadesGestionCambioTotal = new List<ActividadesGestionCambioIndex>();

                List<ActividadesGestionCambioIndex> ListaActividadGestionCambioAvance = await db.ActividadesGestionCambio
                                                   .Join(db.PlanGestionCambio
                                                   , actividades => actividades.IdPlanGestionCambio, planes => planes.IdPlanGestionCambio,
                                                   (actividades, planes) => new { ActividadesGestionCambio = actividades, PlanGestionCambio = planes })
                                                   .Join(db.AvanceGestionCambio
                                                   , actividades => actividades.ActividadesGestionCambio.IdActividadesGestionCambio, avance => avance.IdActividadesGestionCambio,
                                                   (actividades, avance) => new { ActividadesGestion = actividades, AvanceGestionCambio = avance })
                                                   .GroupBy(
                                                       x =>
                                                       new
                                                       {
                                                           x.ActividadesGestion.ActividadesGestionCambio.IdPlanGestionCambio,
                                                           x.ActividadesGestion.ActividadesGestionCambio.IdActividadesGestionCambio,
                                                           x.ActividadesGestion.ActividadesGestionCambio.FechaInicio,
                                                           x.ActividadesGestion.ActividadesGestionCambio.FechaFin,
                                                           x.ActividadesGestion.ActividadesGestionCambio.Indicador,
                                                           x.ActividadesGestion.ActividadesGestionCambio.Porciento,
                                                           x.ActividadesGestion.ActividadesGestionCambio.Descripcion
                                                       })
                                                       .Select(index => new ActividadesGestionCambioIndex
                                                       {
                                                           IdPlanGestionCambio = index.Key.IdPlanGestionCambio,
                                                           IdActividadesGestionCambio = index.Key.IdActividadesGestionCambio,
                                                           FechaInicio = index.Key.FechaInicio,
                                                           FechaFin = index.Key.FechaFin,
                                                           Indicador = index.Key.Indicador,
                                                           Porciento = index.Key.Porciento,
                                                           Descripcion = index.Key.Descripcion,
                                                           Suma = ((decimal?)index.Sum(actividades => actividades.AvanceGestionCambio.Indicadorreal)) ?? 0,
                                                           Porcentaje = ((decimal?)(index.Sum(actividades => actividades.AvanceGestionCambio.Indicadorreal) * 100) / index.Key.Indicador) ?? 0

                                                       })
                                                       .Where(x => x.IdPlanGestionCambio == actividadesGestionCambio.IdPlanGestionCambio)
                                                       .ToListAsync();

                var ListaActividadGestionCambioPlan = db.ActividadesGestionCambio
                                                           .Where(x => x.IdPlanGestionCambio == actividadesGestionCambio.IdPlanGestionCambio);

                if (ListaActividadGestionCambioAvance.Count != 0)
                {

                    foreach (var elementoPlan in ListaActividadGestionCambioPlan)
                    {

                        foreach (var elementoAvance in ListaActividadGestionCambioAvance)
                        {
                            bool existeConsulta = ListaActividadGestionCambioAvance.Exists(x => x.IdActividadesGestionCambio == elementoPlan.IdActividadesGestionCambio);

                            bool existeLista = listaActividadesGestionCambioTotal.Exists(x => x.IdActividadesGestionCambio == elementoPlan.IdActividadesGestionCambio);

                            if (!existeConsulta)
                            {
                                if (!existeLista)
                                {
                                    ActividadesGestionCambioIndex actividad = new ActividadesGestionCambioIndex();
                                    actividad.IdPlanGestionCambio = elementoPlan.IdPlanGestionCambio;
                                    actividad.IdActividadesGestionCambio = elementoPlan.IdActividadesGestionCambio;
                                    actividad.FechaInicio = elementoPlan.FechaInicio;
                                    actividad.FechaFin = elementoPlan.FechaFin;
                                    actividad.Indicador = elementoPlan.Indicador;
                                    actividad.Porciento = elementoPlan.Porciento;
                                    actividad.Descripcion = elementoPlan.Descripcion;
                                    actividad.Suma = 0;
                                    actividad.Porcentaje = 0;
                                    listaActividadesGestionCambioTotal.Add(actividad);

                                }
                            }
                            else
                            {
                                if (!existeLista)
                                {
                                    if (elementoAvance.IdActividadesGestionCambio == elementoPlan.IdActividadesGestionCambio)
                                    {
                                        ActividadesGestionCambioIndex actividad = new ActividadesGestionCambioIndex();
                                        actividad.IdPlanGestionCambio = elementoAvance.IdPlanGestionCambio;
                                        actividad.IdActividadesGestionCambio = elementoAvance.IdActividadesGestionCambio;
                                        actividad.FechaInicio = elementoAvance.FechaInicio;
                                        actividad.FechaFin = elementoAvance.FechaFin;
                                        actividad.Indicador = elementoAvance.Indicador;
                                        actividad.Porciento = elementoAvance.Porciento;
                                        actividad.Descripcion = elementoAvance.Descripcion;
                                        actividad.Suma = elementoAvance.Suma;
                                        actividad.Porcentaje = elementoAvance.Porcentaje;

                                        listaActividadesGestionCambioTotal.Add(actividad);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (ListaActividadGestionCambioAvance.Count == 0)
                    {
                        foreach (var elementoPlan in ListaActividadGestionCambioPlan)
                        {
                            ActividadesGestionCambioIndex actividad = new ActividadesGestionCambioIndex();
                            actividad.IdPlanGestionCambio = elementoPlan.IdPlanGestionCambio;
                            actividad.IdActividadesGestionCambio = elementoPlan.IdActividadesGestionCambio;
                            actividad.FechaInicio = elementoPlan.FechaInicio;
                            actividad.FechaFin = elementoPlan.FechaFin;
                            actividad.Indicador = elementoPlan.Indicador;
                            actividad.Porciento = elementoPlan.Porciento;
                            actividad.Descripcion = elementoPlan.Descripcion;
                            actividad.Suma = 0;
                            actividad.Porcentaje = 0;
                            listaActividadesGestionCambioTotal.Add(actividad);
                        }
                    }
                }
                    return listaActividadesGestionCambioTotal;

                
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
                return new List<ActividadesGestionCambioIndex>();
            }
        }

        //int IdActividadesGestionCambio - ActividadesGestionCambioconIdActividad
        // POST: api/ActividadesGestionCambio
        [HttpPost]
        [Route("ActividadesGestionCambioconIdActividad")]
        public async Task<Response> ActividadesGestionCambioconIdActividad([FromBody] ActividadesGestionCambio actividadesGestionCambio)
        {
            try
            {

                var actividadesGestionCambioResultado = await db.ActividadesGestionCambio.SingleOrDefaultAsync(m => m.IdActividadesGestionCambio == actividadesGestionCambio.IdActividadesGestionCambio);

                var response = new Response
                {
                    IsSuccess = true,
                    Resultado = actividadesGestionCambioResultado,
                };

                return response;

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
                return new Response { };
            }
        }


        // GET: api/ActividadesGestionCambio/5
        [HttpGet("{id}")]
        public async Task<Response> GetActividadesGestionCambio([FromRoute] int id)
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

                var ActividadesGestionCambio = await db.ActividadesGestionCambio.SingleOrDefaultAsync(m => m.IdActividadesGestionCambio == id);

                if (ActividadesGestionCambio == null)
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
                    Resultado = ActividadesGestionCambio,
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

        // PUT: api/ActividadesGestionCambio/5
        [HttpPut("{id}")]
        public async Task<Response> PutActividadesGestionCambio([FromRoute] int id, [FromBody] ActividadesGestionCambio actividadesGestionCambio)
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

                if (actividadesGestionCambio.FechaInicio <= DateTime.Today)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha de inicio no puede ser menor o igual que la fecha de hoy"
                    };
                }

                if (actividadesGestionCambio.Indicador == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "El indicador no puede ser cero"
                    };
                }

                if (actividadesGestionCambio.FechaFin <= DateTime.Today)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha fin no puede ser menor o igual que la fecha de hoy"
                    };
                }

                if (actividadesGestionCambio.FechaInicio > actividadesGestionCambio.FechaFin)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha de inicio no puede ser mayor que la fecha fin"
                    };
                }

                string fechaInicio = actividadesGestionCambio.FechaInicio.DayOfWeek.ToString();

                if (fechaInicio.Equals("Saturday") || fechaInicio.Equals("Sunday"))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha de inicio no puede ser fin de semana"
                    };
                }


                string fechaFin = actividadesGestionCambio.FechaFin.DayOfWeek.ToString();

                if (fechaFin.Equals("Saturday") || fechaFin.Equals("Sunday"))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha fin no puede ser fin de semana"
                    };
                }


                PlanGestionCambio Planes = db.PlanGestionCambio.Find(actividadesGestionCambio.IdPlanGestionCambio);

                if (Planes.FechaInicio > actividadesGestionCambio.FechaInicio)
                {

                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha inicio del plan no puede ser mayor a la fecha inicio de actividades"
                    };
                }
                var existe = Existe(actividadesGestionCambio);
                var ActividadesGestionCambioActualizar = (ActividadesGestionCambio)existe.Resultado;
                if (existe.IsSuccess)
                {
                    if (ActividadesGestionCambioActualizar.IdActividadesGestionCambio == actividadesGestionCambio.IdActividadesGestionCambio)
                    {
                        return new Response
                        {
                            IsSuccess = true,
                        };
                    }
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }
                var ActividadesGestionCambio = db.ActividadesGestionCambio.Find(actividadesGestionCambio.IdActividadesGestionCambio);

                ActividadesGestionCambio.FechaInicio = actividadesGestionCambio.FechaInicio;
                ActividadesGestionCambio.FechaFin = actividadesGestionCambio.FechaFin;
                ActividadesGestionCambio.Indicador = actividadesGestionCambio.Indicador;
                ActividadesGestionCambio.Porciento = actividadesGestionCambio.Porciento;
                ActividadesGestionCambio.Descripcion = actividadesGestionCambio.Descripcion;

                db.ActividadesGestionCambio.Update(ActividadesGestionCambio);
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

        // POST: api/ActividadesGestionCambio
        [HttpPost]
        [Route("InsertarActividadesGestionCambio")]
        public async Task<Response> PostActividadesGestionCambio([FromBody] ActividadesGestionCambio ActividadesGestionCambio)
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

                if (ActividadesGestionCambio.FechaInicio <= DateTime.Today)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha de inicio no puede ser menor o igual que la fecha de hoy"
                    };
                }


                if (ActividadesGestionCambio.Indicador == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "El indicador no puede ser cero"
                    };
                }



                if (ActividadesGestionCambio.FechaFin <= DateTime.Today)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha fin no puede ser menor o igual que la fecha de hoy"
                    };
                }

                if (ActividadesGestionCambio.FechaInicio > ActividadesGestionCambio.FechaFin)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha de inicio no puede ser mayor que la fecha fin"
                    };
                }

                string fechaInicio = ActividadesGestionCambio.FechaInicio.DayOfWeek.ToString();

                if (fechaInicio.Equals("Saturday") || fechaInicio.Equals("Sunday"))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha de inicio no puede ser fin de semana"
                    };
                }


                string fechaFin = ActividadesGestionCambio.FechaFin.DayOfWeek.ToString();

                if (fechaFin.Equals("Saturday") || fechaFin.Equals("Sunday"))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha fin no puede ser fin de semana"
                    };
                }
                
               
                PlanGestionCambio Planes = db.PlanGestionCambio.Find(ActividadesGestionCambio.IdPlanGestionCambio);

                if (Planes.FechaInicio > ActividadesGestionCambio.FechaInicio)
                {

                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha inicio del plan no puede ser mayor a la fecha inicio de actividades"
                    };
                }

                var respuesta = Existe(ActividadesGestionCambio);
                if (!respuesta.IsSuccess)
                {
                    db.ActividadesGestionCambio.Add(ActividadesGestionCambio);
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

        // DELETE: api/ActividadesGestionCambio/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteActividadesGestionCambio([FromRoute] int id)
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

                var respuesta = await db.ActividadesGestionCambio.SingleOrDefaultAsync(m => m.IdActividadesGestionCambio == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.ActividadesGestionCambio.Remove(respuesta);
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

        private Response Existe(ActividadesGestionCambio ActividadesGestionCambio)
        {
            var bdd = ActividadesGestionCambio.FechaInicio;
            var ActividadesGestionCambiorespuesta = db.ActividadesGestionCambio.Where(p => p.FechaInicio == ActividadesGestionCambio.FechaInicio &&
                                                                                      p.FechaFin == ActividadesGestionCambio.FechaFin &&
                                                                                      p.Descripcion == ActividadesGestionCambio.Descripcion &&
                                                                                      p.IdPlanGestionCambio == ActividadesGestionCambio.IdPlanGestionCambio
                                                                                      ).FirstOrDefault();
            if (ActividadesGestionCambiorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = ActividadesGestionCambiorespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = ActividadesGestionCambiorespuesta,
            };
        }


    }
}