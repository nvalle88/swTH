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
using bd.swth.entidades.Constantes;

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
                        SolicitudVacacionesActualizar.Observaciones = SolicitudVacaciones.Observaciones;
                        SolicitudVacacionesActualizar.Estado = SolicitudVacaciones.Estado;
                        SolicitudVacacionesActualizar.FechaRespuesta = SolicitudVacaciones.FechaRespuesta;

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
                
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }



        /// <summary>
        /// Este método guarda datos, creando un nuevo registro de Solicitud Vacaciones
        /// </summary>
        /// <param name="SolicitudVacacionesViewModel"></param>
        /// <returns></returns>
        // POST: api/SolicitudVacaciones
        [HttpPost]
        [Route("InsertarSolicitudVacaciones")]
        public async Task<Response> InsertarSolicitudVacaciones([FromBody] SolicitudVacacionesViewModel SolicitudVacacionesViewModel)
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

                DateTime fechaHasta;
                DateTime fechaDesde;

                if (SolicitudVacacionesViewModel.PlanAnual == false)
                {

                    fechaDesde = SolicitudVacacionesViewModel.FechaDesde;
                    fechaHasta = SolicitudVacacionesViewModel.FechaHasta;
                }
                else
                {

                    var plan = await db.SolicitudPlanificacionVacaciones
                        .Where(w => w.IdSolicitudPlanificacionVacaciones == SolicitudVacacionesViewModel.IdSolicitudPlanificacionVacaciones)
                        .FirstOrDefaultAsync();

                    fechaDesde = plan.FechaDesde;
                    fechaHasta = plan.FechaHasta;

                }

                var modelo = new SolicitudVacaciones
                {
                    FechaDesde = fechaDesde,
                    FechaHasta = fechaHasta,
                    FechaSolicitud = DateTime.Now,
                    IdEmpleado = SolicitudVacacionesViewModel.DatosBasicosEmpleadoViewModel.IdEmpleado,
                    Estado = SolicitudVacacionesViewModel.Estado,
                    Observaciones = "",
                    PlanAnual = SolicitudVacacionesViewModel.PlanAnual

                };

                db.SolicitudVacaciones.Add(modelo);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.GuardadoSatisfactorio
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


        /// <summary>
        /// Obtiene el formato y datos que se mostrarán en la vista, (Este método no guarda datos)
        /// </summary>
        /// <param name="idFiltrosViewModel"></param>
        /// <returns></returns>
        // Post: api/SolicitudVacaciones
        [HttpPost]
        [Route("CrearSolicitudesVacaciones")]
        public async Task<SolicitudVacacionesViewModel> CrearSolicitudesVacaciones([FromBody]IdFiltrosViewModel idFiltrosViewModel)
        {

            try
            {
                var usuario = await db.Empleado.Include(i => i.Persona)
                    .Where(w => w.NombreUsuario == idFiltrosViewModel.NombreUsuario).FirstOrDefaultAsync();

                var IOMPEmpleado = await db.IndiceOcupacionalModalidadPartida
                    .Include(i => i.TipoNombramiento)
                    .Where(w => w.IdEmpleado == usuario.IdEmpleado)
                    .OrderByDescending(o => o.Fecha)
                    .FirstOrDefaultAsync();


                var vacacionesAcumuladas = 0;

                var vacaciones = await db.VacacionesEmpleado
                    .Where(w => w.IdEmpleado == usuario.IdEmpleado)
                    .ToListAsync();

                foreach (var item in vacaciones)
                {
                    vacacionesAcumuladas = vacacionesAcumuladas + item.VacacionesNoGozadas;
                }

                //var WorkingTimeInYears = IOMPEmpleado.Fecha.Month.
                var fechaIngreso = new TimeSpan(IOMPEmpleado.Fecha.Year, IOMPEmpleado.Fecha.Month, IOMPEmpleado.Fecha.Day);


                var estado = ConstantesEstadosVacaciones.ListaEstadosVacaciones.Where(w => w.GrupoAprobacion == 2).FirstOrDefault();

                var hoy = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                var modelo = new SolicitudVacacionesViewModel
                {
                    DatosBasicosEmpleadoViewModel = new DatosBasicosEmpleadoViewModel
                    {
                        Nombres = usuario.Persona.Nombres,
                        Apellidos = usuario.Persona.Apellidos,
                        Identificacion = usuario.Persona.Identificacion,
                        IdEmpleado = usuario.IdEmpleado,
                        IdPersona = usuario.Persona.IdPersona
                    },

                    IdSolicitudVacaciones = 0,

                    FechaDesde = hoy,
                    FechaHasta = hoy,
                    FechaSolicitud = hoy,

                    Observaciones = "",

                    Estado = (estado != null) ? estado.ValorEstado : 0,

                    NombreEstado = (estado != null) ? estado.NombreEstado : "Sin estados",

                    VacacionesAcumuladas = vacacionesAcumuladas,

                    PlanAnual = true,

                    ListaPLanificacionVacaciones = await db.SolicitudPlanificacionVacaciones
                    .Where(w=>
                        w.Estado == 6
                        && w.FechaDesde.Year == DateTime.Now.Year
                    )
                    .ToListAsync()

                };


                return modelo;
            }
            catch (Exception ex)
            {

                return new SolicitudVacacionesViewModel();
            }
        }


        /// <summary>
        /// Obtiene la lista de solicitudes de vacaciones del empleado, requerido: NombreUsuario
        /// </summary>
        /// <param name="idFiltrosViewModel"></param>
        /// <returns></returns>
        // Post: api/SolicitudVacaciones
        [HttpPost]
        [Route("ListarSolicitudesVacacionesViewModel")]
        public async Task<List<SolicitudVacacionesViewModel>> ListarSolicitudesVacacionesViewModel([FromBody]IdFiltrosViewModel idFiltrosViewModel)
        {

            try
            {
                var usuario = await db.Empleado
                    .Where(w => w.NombreUsuario == idFiltrosViewModel.NombreUsuario).FirstOrDefaultAsync();

                var listaEstados = ConstantesEstadosVacaciones.ListaEstadosVacaciones;

                var vacaciones = await db.VacacionesEmpleado
                    .Where(w => w.IdEmpleado == usuario.IdEmpleado)
                    .ToListAsync();

                var vacacionesAcumuladas = 0;

                foreach (var item in vacaciones)
                {
                    vacacionesAcumuladas = vacacionesAcumuladas + item.VacacionesNoGozadas;
                }


                var modelo = await db.SolicitudVacaciones
                    .Where(w => w.IdEmpleado == usuario.IdEmpleado)
                    .Select(s => new SolicitudVacacionesViewModel
                    {
                        IdSolicitudVacaciones =s.IdSolicitudVacaciones,

                        
                        DatosBasicosEmpleadoViewModel = new DatosBasicosEmpleadoViewModel
                        {
                            Nombres = s.Empleado.Persona.Nombres,
                            Apellidos = s.Empleado.Persona.Apellidos,
                            Identificacion = s.Empleado.Persona.Identificacion,
                            IdEmpleado = s.IdEmpleado,
                            IdPersona = s.Empleado.Persona.IdPersona
                        },
                        
                        FechaDesde = s.FechaDesde,
                        FechaHasta = s.FechaHasta,
                        FechaSolicitud = s.FechaSolicitud,
                        FechaRespuesta = (s.FechaRespuesta != null)?(DateTime)s.FechaRespuesta:new DateTime(0001,1,1),
                        
                        Observaciones = s.Observaciones,
                        
                        Estado = s.Estado,
                        NombreEstado = listaEstados.Where(w1 => w1.ValorEstado == s.Estado).FirstOrDefault().NombreEstado,

                        VacacionesAcumuladas = vacacionesAcumuladas,
                        
                        PlanAnual = s.PlanAnual
                        

                    }
                    ).ToListAsync();


                return modelo;
            }
            catch (Exception ex)
            {

                return new List<SolicitudVacacionesViewModel>();
            }
        }


        /// <summary>
        /// Obtiene un solicitudVacacionesViewModel a partir del id (id de solicitudVacaciones)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: api/SolicitudVacaciones
        [HttpPost]
        [Route("ObtenerSolicitudVacacionesViewModel")]
        public async Task<Response> ObtenerSolicitudVacacionesViewModel([FromBody] string id)
        {

            try
            {
                var idSolicitud = Convert.ToInt32(id);

                var estado = ConstantesEstadosVacaciones.ListaEstadosVacaciones.ToList();

                var vacacionesAcumuladas = 0;


                var modelo = await db.SolicitudVacaciones
                    .Where(w => w.IdSolicitudVacaciones == idSolicitud)
                    .Select(s => new SolicitudVacacionesViewModel
                    {
                        DatosBasicosEmpleadoViewModel = new DatosBasicosEmpleadoViewModel
                        {
                            Nombres = s.Empleado.Persona.Nombres,
                            Apellidos = s.Empleado.Persona.Apellidos,
                            Identificacion = s.Empleado.Persona.Identificacion,
                            IdEmpleado = s.Empleado.IdEmpleado,
                            IdPersona = s.Empleado.Persona.IdPersona
                        },

                        IdSolicitudVacaciones = s.IdSolicitudVacaciones,

                        FechaDesde = s.FechaDesde,
                        FechaHasta = s.FechaHasta,
                        FechaSolicitud = s.FechaSolicitud,
                        FechaRespuesta = ((DateTime)s.FechaRespuesta != null) ? (DateTime)s.FechaRespuesta : new DateTime(0001, 1, 1),

                        Observaciones = s.Observaciones,

                        Estado = (estado != null) ? s.Estado : 0,

                        NombreEstado = (estado != null) ? estado
                            .Where(w => w.ValorEstado == s.Estado).FirstOrDefault().NombreEstado
                            : "Sin estados",

                        VacacionesAcumuladas = vacacionesAcumuladas,

                        PlanAnual = s.PlanAnual,

                        IdSolicitudPlanificacionVacaciones = (s.PlanAnual == false) ? 0 
                        : db.SolicitudPlanificacionVacaciones
                            .Where(wp=>
                            wp.FechaDesde == s.FechaDesde
                            && wp.FechaHasta == s.FechaHasta
                            ).OrderByDescending(op=>op.FechaSolicitud)
                            .FirstOrDefault().IdSolicitudPlanificacionVacaciones
                        ,

                        ListaPLanificacionVacaciones = db.SolicitudPlanificacionVacaciones
                    .Where(w =>
                        w.Estado == 6
                        && w.FechaDesde.Year == DateTime.Now.Year
                    )
                    .ToList()
                    

                    }
                    )
                    .FirstOrDefaultAsync();

                var numeroEstado = estado.Where(w => w.ValorEstado == modelo.Estado).FirstOrDefault();

                if (numeroEstado == null)
                {
                    modelo.Estado = estado.Where(w => w.GrupoAprobacion == 2).FirstOrDefault().ValorEstado;
                    modelo.NombreEstado = estado.Where(w => w.GrupoAprobacion == 2).FirstOrDefault().NombreEstado;
                }

                var vacaciones = await db.VacacionesEmpleado
                    .Where(w => w.IdEmpleado == modelo.DatosBasicosEmpleadoViewModel.IdEmpleado)
                    .ToListAsync();

                // Suma de vacaciones no gozadas
                foreach (var item in vacaciones)
                {
                    vacacionesAcumuladas = vacacionesAcumuladas + item.VacacionesNoGozadas;
                }

                // Se añaden las vacaciones acumuladas al modelo
                modelo.VacacionesAcumuladas = vacacionesAcumuladas;


                if (modelo != null)
                {
                    return new Response
                    {
                        IsSuccess = true,
                        Resultado = modelo
                    };
                }

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.RegistroNoEncontrado
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


        /// <summary>
        ///  Edita el registro a partir del SolicitudVacacionesViewModel ingresado
        /// </summary>
        /// <param name="solicitudVacacionesViewModel"></param>
        /// <returns></returns>
        // POST: api/SolicitudVacaciones
        [HttpPost]
        [Route("EditarSolicitudVacaciones")]
        public async Task<Response> EditarSolicitudVacaciones([FromBody] SolicitudVacacionesViewModel solicitudVacacionesViewModel)
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

                var modelo = await db.SolicitudVacaciones
                    .Where(w => w.IdSolicitudVacaciones == solicitudVacacionesViewModel.IdSolicitudVacaciones)
                    .FirstOrDefaultAsync();

                DateTime fechaDesde;
                DateTime fechaHasta;

                if (solicitudVacacionesViewModel.PlanAnual == true)
                {
                    var plan = await db.SolicitudPlanificacionVacaciones
                        .Where(w => w.IdSolicitudPlanificacionVacaciones == solicitudVacacionesViewModel.IdSolicitudPlanificacionVacaciones)
                        .FirstOrDefaultAsync();

                    fechaDesde = plan.FechaDesde;
                    fechaHasta = plan.FechaHasta;

                }
                else {
                    fechaDesde = solicitudVacacionesViewModel.FechaDesde;
                    fechaHasta = solicitudVacacionesViewModel.FechaHasta;
                }

                modelo.Estado = solicitudVacacionesViewModel.Estado;
                modelo.FechaDesde = fechaDesde;
                modelo.FechaHasta = fechaHasta;
                modelo.FechaSolicitud = DateTime.Now;
                modelo.IdEmpleado = solicitudVacacionesViewModel.DatosBasicosEmpleadoViewModel.IdEmpleado;
                modelo.PlanAnual = solicitudVacacionesViewModel.PlanAnual;
                
                db.SolicitudVacaciones.Update(modelo);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.GuardadoSatisfactorio
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


        /// <summary>
        /// Devuelve la lista de SolicitudesVacaciones por IdEmpleado
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ListarSolicitudesVacaciones")]
        public async Task<List<SolicitudVacaciones>> ObtenerEmpleadoLogueado([FromBody]Empleado empleado)
        {
            try
            {

                var SolicitudVacaciones = await db.SolicitudVacaciones
                                   .Where(e => e.IdEmpleado == empleado.IdEmpleado).ToListAsync();


                return SolicitudVacaciones;
            }
            catch (Exception ex)
            {
                return new List<SolicitudVacaciones>();
            }
        }

    }
}