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
    [Route("api/PlanesGestionCambio")]
    public class PlanesGestionCambioController : Controller
    {
        private readonly SwTHDbContext db;

        public PlanesGestionCambioController(SwTHDbContext db)
        {
            this.db = db;
            
        }

        // GET: api/PlanesGestionCambio
        [HttpGet]
        [Route("ListarPlanesGestionCambio")]
        public async Task<List<PlanGestionCambio>> GetNacionalidadesIndigenas()
        {
            try
            {
                return await db.PlanGestionCambio.Include(x => x.EmpleadoRealizadoPor.Persona).Include(x => x.EmpleadoAprobadoPor.Persona).OrderBy(x => x.Titulo).ToListAsync();
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
                return new List<PlanGestionCambio>();
            }
        }

        // GET: api/PlanesGestionCambio/5
        [HttpGet("{id}")]
        public async Task<Response> GetPlanGestionCambio([FromRoute] int id)
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

                var PlanGestionCambio = await db.PlanGestionCambio.SingleOrDefaultAsync(m => m.IdPlanGestionCambio == id);

                if (PlanGestionCambio == null)
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
                    Resultado = PlanGestionCambio,
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

        // PUT: api/PlanesGestionCambio/5
        [HttpPut("{id}")]
        public async Task<Response> PutPlanGestionCambio([FromRoute] int id, [FromBody] PlanGestionCambio planGestionCambio)
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

                if (planGestionCambio.FechaInicio <= DateTime.Today)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha de inicio no puede ser menor o igual que la fecha de hoy"
                    };
                }

                if (planGestionCambio.FechaFin <= DateTime.Today)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha fin no puede ser menor o igual que la fecha de hoy"
                    };
                }

                if (planGestionCambio.FechaInicio >= planGestionCambio.FechaFin)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha de inicio no puede ser mayor o igual que la fecha fin"
                    };
                }

                string fechaInicio = planGestionCambio.FechaInicio.DayOfWeek.ToString();

                if (fechaInicio.Equals("Saturday") || fechaInicio.Equals("Sunday"))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha de inicio no puede ser fin de semana"
                    };
                }


                string fechaFin = planGestionCambio.FechaFin.DayOfWeek.ToString();

                if (fechaFin.Equals("Saturday") || fechaFin.Equals("Sunday"))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha fin no puede ser fin de semana"
                    };
                }

                

                var existe = Existe(planGestionCambio);
                var PlanGestionCambioActualizar = (PlanGestionCambio)existe.Resultado;
                if (existe.IsSuccess)
                {
                    if (PlanGestionCambioActualizar.IdPlanGestionCambio == planGestionCambio.IdPlanGestionCambio)
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
                var PlanGestionCambio = db.PlanGestionCambio.Find(planGestionCambio.IdPlanGestionCambio);

                PlanGestionCambio.Titulo = planGestionCambio.Titulo;
                PlanGestionCambio.Descripcion = planGestionCambio.Descripcion;
                PlanGestionCambio.FechaInicio = planGestionCambio.FechaInicio;
                PlanGestionCambio.FechaFin = planGestionCambio.FechaFin;
                PlanGestionCambio.RealizadoPor = planGestionCambio.RealizadoPor;
                PlanGestionCambio.AprobadoPor = planGestionCambio.AprobadoPor;
                db.PlanGestionCambio.Update(PlanGestionCambio);
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
                    IsSuccess = true,
                    Message = Mensaje.Excepcion,
                };
            }

        }

        // POST: api/PlanesGestionCambio
        [HttpPost]
        [Route("InsertarPlanGestionCambio")]
        public async Task<Response> PostPlanGestionCambio([FromBody] PlanGestionCambio PlanGestionCambio)
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

                if (PlanGestionCambio.FechaInicio <= DateTime.Today )
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha de inicio no puede ser menor o igual que la fecha de hoy"
                    };
                }

                if (PlanGestionCambio.FechaFin <= DateTime.Today)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha fin no puede ser menor o igual que la fecha de hoy"
                    };
                }

                if (PlanGestionCambio.FechaInicio >= PlanGestionCambio.FechaFin)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha de inicio no puede ser mayor o igual que la fecha fin"
                    };
                }

                string fechaInicio=PlanGestionCambio.FechaInicio.DayOfWeek.ToString();

                if (fechaInicio.Equals("Saturday") || fechaInicio.Equals("Sunday"))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha de inicio no puede ser fin de semana"
                    };
                }


                string fechaFin = PlanGestionCambio.FechaFin.DayOfWeek.ToString();

                if (fechaFin.Equals("Saturday") || fechaFin.Equals("Sunday"))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha fin no puede ser fin de semana"
                    };
                }


                var respuesta = Existe(PlanGestionCambio);
                if (!respuesta.IsSuccess)
                {
                    db.PlanGestionCambio.Add(PlanGestionCambio);
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

        // DELETE: api/PlanesGestionCambio/5
        [HttpDelete("{id}")]
        public async Task<Response> DeletePlanGestionCambio([FromRoute] int id)
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

                var respuesta = await db.PlanGestionCambio.SingleOrDefaultAsync(m => m.IdPlanGestionCambio == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.PlanGestionCambio.Remove(respuesta);
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

        private Response Existe(PlanGestionCambio PlanGestionCambio)
        {
            var bdd = PlanGestionCambio.Titulo.ToUpper().TrimEnd().TrimStart();
            var PlanGestionCambiorespuesta = db.PlanGestionCambio.Where(p => p.Titulo.ToUpper().TrimStart().TrimEnd() == bdd && p.FechaInicio == PlanGestionCambio.FechaInicio && p.FechaFin == PlanGestionCambio.FechaFin &&  p.RealizadoPor == PlanGestionCambio.RealizadoPor  && p.AprobadoPor == PlanGestionCambio.AprobadoPor).FirstOrDefault();
            if (PlanGestionCambiorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = PlanGestionCambiorespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = PlanGestionCambiorespuesta,
            };
        }
    }
}