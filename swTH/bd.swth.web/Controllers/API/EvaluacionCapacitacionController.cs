using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using Microsoft.EntityFrameworkCore;
using bd.swth.entidades.Utils;
using bd.swth.entidades.ViewModels;
using bd.swth.entidades.Constantes;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/EvaluacionCapacitacion")]
    public class EvaluacionCapacitacionController : Controller
    {
        private readonly SwTHDbContext db;

        public EvaluacionCapacitacionController(SwTHDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        [Route("ObtenerEvaluacionesEmpleado")]
        public async Task<ViewModelEvaluacionCapacitaciones> ObtenerEvaluacionesEmpleado([FromBody] ViewModelEvaluacionCapacitaciones viewModelEvaluacionDesempeno)
        {
            var DatosBasicos = new ViewModelEvaluacionCapacitaciones();
            try
            {

                var EmpleadoDetalle = await db.Empleado
                                 .Where(x => x.NombreUsuario == viewModelEvaluacionDesempeno.NombreUsuario && x.Activo == true)
                                 .Select(x => new ViewModelEvaluacionCapacitaciones
                                 {
                                     IdEmpleado = x.IdEmpleado,
                                     Identificacion = x.Persona.Identificacion,
                                 })
                                 .FirstOrDefaultAsync();
                var Plan = await db.PlanCapacitacion.Where(x => x.Cedula == EmpleadoDetalle.Identificacion && (x.Estado == ConstantesCapacitacion.EstadoTerminado || x.Estado == ConstantesCapacitacion.EstadoEvaluado)).ToListAsync();
                if (Plan != null)
                {

                    DatosBasicos.ListaPlanCapacitacion = Plan;
                }
                return DatosBasicos;

            }
            catch (Exception ex)
            {
                return new ViewModelEvaluacionCapacitaciones();
            }
        }

        [HttpPost]
        [Route("ObtenerDatosEvaluacion")]
        public async Task<ViewModelEvaluacionCapacitaciones> ObtenerDatosEvaluacion([FromBody] ViewModelEvaluacionCapacitaciones viewModelEvaluacionDesempeno)
        {
            var DatosBasicos = new ViewModelEvaluacionCapacitaciones();
            try
            {

                var Plan = await db.PlanCapacitacion.Where(x => x.IdPlanCapacitacion == viewModelEvaluacionDesempeno.IdPlanCapacitacion && x.Estado == ConstantesCapacitacion.EstadoTerminado).FirstOrDefaultAsync();
                if (Plan != null)
                {
                    var PreguntasEvaluacion = await db.PreguntaEvaluacionEvento.ToListAsync();
                    DatosBasicos.ListaPreguntaEvaluacionEvento = PreguntasEvaluacion;
                    DatosBasicos.NombreEvento = Plan.NombreEvento;
                    DatosBasicos.Institucion = Plan.Institucion;
                    DatosBasicos.LugarFecha = Plan.Ubicacion + " del " + Plan.FechaInicio + " al " + Plan.FechaFin;
                }
                return DatosBasicos;

            }
            catch (Exception ex)
            {
                return new ViewModelEvaluacionCapacitaciones();
            }
        }
        [HttpPost]
        [Route("ObtenerDatosEvaluacionEvento")]
        public async Task<ViewModelEvaluacionCapacitaciones> ObtenerDatosEvaluacionEvento([FromBody] ViewModelEvaluacionCapacitaciones viewModelEvaluacionDesempeno)
        {
            var DatosBasicos = new ViewModelEvaluacionCapacitaciones();
            try
            {

                var Plan = await db.PlanCapacitacion.Where(x => x.IdPlanCapacitacion == viewModelEvaluacionDesempeno.IdPlanCapacitacion && x.Estado == ConstantesCapacitacion.EstadoEvaluado).FirstOrDefaultAsync();
                var evaluacion = await db.EvaluacionEvento.Where(x => x.IdPlanCapacitacion == Plan.IdPlanCapacitacion).FirstOrDefaultAsync();

                var Facilitador = await db.DetalleEvaluacionEvento.Where(x => x.IdEvaluacionEvento == evaluacion.IdEvaluacionEvento && x.Conocimiento == null && x.PreguntasEvaluacionEvento.Facilitador == true)
                    .Select(x => new ViewModelEvaluacionEventoDetalle
                    {
                        Pregunta = x.PreguntasEvaluacionEvento.Descripcion,
                        Calificacion = x.Calificacion
                    }).ToListAsync();
                var Organizador = await db.DetalleEvaluacionEvento.Where(x => x.IdEvaluacionEvento == evaluacion.IdEvaluacionEvento && x.Conocimiento == null && x.PreguntasEvaluacionEvento.Organizador == true)
                    .Select(x => new ViewModelEvaluacionEventoDetalle
                    {
                        Pregunta = x.PreguntasEvaluacionEvento.Descripcion,
                        Calificacion = x.Calificacion
                    }).ToListAsync();
                var Conocimientos = await db.DetalleEvaluacionEvento.Where(x => x.IdEvaluacionEvento == evaluacion.IdEvaluacionEvento && x.Calificacion == null && x.PreguntasEvaluacionEvento.Conocimiento != null)
                    .Select(x => new ViewModelEvaluacionEventoDetalle
                    {
                        Pregunta = x.PreguntasEvaluacionEvento.Descripcion,
                        Calificacion = x.Calificacion
                    }).ToListAsync();

                if (Plan != null)
                {
                    DatosBasicos.ListaPreguntaEvaluacionFacilitadorDetalle = Facilitador;
                    DatosBasicos.ListaPreguntaOrganizadorDetalle = Organizador;
                    DatosBasicos.ListaPreguntaEvaluacionConocimientoDetalle = Conocimientos;
                    DatosBasicos.NombreEvento = Plan.NombreEvento;
                    DatosBasicos.Institucion = Plan.Institucion;
                    DatosBasicos.LugarFecha = Plan.Ubicacion + " del " + Plan.FechaInicio + " al " + Plan.FechaFin;
                }
                return DatosBasicos;

            }
            catch (Exception ex)
            {
                return new ViewModelEvaluacionCapacitaciones();
            }
        }
        [HttpPost]
        [Route("InsertarEvaluacion")]
        public async Task<Response> InsertarEvaluacion([FromBody] ViewModelEvaluacionCapacitaciones viewModelEvaluacionCapacitaciones)
        {
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {
                    var Evaluacion = new EvaluacionEvento
                    {
                        IdPlanCapacitacion = viewModelEvaluacionCapacitaciones.IdPlanCapacitacion,
                        Sugerencias = viewModelEvaluacionCapacitaciones.ComentarioSugerencia

                    };
                    var EvaluacionInsertarda = await db.EvaluacionEvento.AddAsync(Evaluacion);


                    var lista = new List<DetalleEvaluacionEvento>();

                    foreach (var item in viewModelEvaluacionCapacitaciones.ListaPreguntaEvaluacionFacilitador)
                    {
                        lista.Add(new DetalleEvaluacionEvento
                        {
                            IdPreguntasEvaluacionEvento = item.IdPreguntaEvaluacionEvento,
                            IdEvaluacionEvento = EvaluacionInsertarda.Entity.IdEvaluacionEvento,
                            Calificacion = item.Calificacion,
                            Conocimiento = null,

                        });
                    }

                    foreach (var item in viewModelEvaluacionCapacitaciones.ListaPreguntaOrganizador)
                    {
                        lista.Add(new DetalleEvaluacionEvento
                        {
                            IdPreguntasEvaluacionEvento = item.IdPreguntaEvaluacionEvento,
                            IdEvaluacionEvento = EvaluacionInsertarda.Entity.IdEvaluacionEvento,
                            Calificacion = item.Calificacion,
                            Conocimiento = null,

                        });

                    }
                    foreach (var item in viewModelEvaluacionCapacitaciones.ListaPreguntaEvaluacionConocimiento)
                    {
                        lista.Add(new DetalleEvaluacionEvento
                        {
                            IdPreguntasEvaluacionEvento = item.IdPreguntaEvaluacionEvento,
                            IdEvaluacionEvento = EvaluacionInsertarda.Entity.IdEvaluacionEvento,
                            Calificacion = null,
                            Conocimiento = item.Conocimiento,

                        });

                    }
                    var planCapacitacionActualizar = await db.PlanCapacitacion.Where(x => x.IdPlanCapacitacion == viewModelEvaluacionCapacitaciones.IdPlanCapacitacion).FirstOrDefaultAsync();
                    planCapacitacionActualizar.Estado = ConstantesCapacitacion.EstadoEvaluado;
                    db.PlanCapacitacion.Update(planCapacitacionActualizar);

                    await db.DetalleEvaluacionEvento.AddRangeAsync(lista);
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio
                    };

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.Error,
                    };
                }
            }
        }
    }
}