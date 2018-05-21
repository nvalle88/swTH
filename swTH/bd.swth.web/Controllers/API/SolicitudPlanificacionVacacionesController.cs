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
    [Route("api/SolicitudPlanificacionVacaciones")]
    public class SolicitudPlanificacionVacacionesController : Controller
    {
        private readonly SwTHDbContext db;

        public SolicitudPlanificacionVacacionesController(SwTHDbContext db)
        {
            this.db = db;
        }


        /// <summary>
        /// Este método crea automáticamente registros de vacaciones para los empleados por período fiscal
        /// usando el temporizador
        /// </summary>
        /// <returns></returns>
        public async Task CrearRegistroVacacionesEmpleados() {
            try {

                // Obtiene la lista de empleados registrados en el indiceOcupacionalModalidadPartida
                // filtrando: los repetidos, por la ultima fecha de registro, y activos
                var empleados = await db.IndiceOcupacionalModalidadPartida
                    .Include(i=>i.TipoNombramiento).ThenInclude(t=>t.RelacionLaboral)
                    .Include(i2=>i2.Empleado)
                    .Where(w=>w.Empleado.Activo == true)
                    .OrderByDescending(o=>o.Fecha)
                    .ToAsyncEnumerable()
                    .Distinct(d => d.IdEmpleado)
                    .ToList();


                foreach (var item in empleados) {

                    var registro = await db.VacacionesEmpleado
                        .Where(w =>
                            w.PeriodoFiscal == DateTime.Now.Year
                            && w.IdEmpleado == item.IdEmpleado
                        )
                        .FirstOrDefaultAsync();

                    if (item.IdEmpleado == 2101)
                    {
                        var aadsd = 0;
                    }

                    var totalDiasVacaciones = await CalcularVacacionesPorPeriodoFiscal(item);

                    
                    // Si no existe registro para este período fiscal, debe agregar uno nuevo
                    if (registro == null)
                    {
                        var modelo = new VacacionesEmpleado()
                        {
                            IdEmpleado = item.IdEmpleado,
                            PeriodoFiscal = DateTime.Now.Year,
                            VacacionesGozadas = 0,
                            VacacionesNoGozadas = totalDiasVacaciones
                        };

                        db.VacacionesEmpleado.Add(modelo);
                        await db.SaveChangesAsync();
                    }
                    else {

                        var sumaVacacionesRegistro = registro.VacacionesNoGozadas + registro.VacacionesGozadas;

                        if (totalDiasVacaciones > sumaVacacionesRegistro) {

                            registro.VacacionesNoGozadas = (totalDiasVacaciones - sumaVacacionesRegistro) + registro.VacacionesNoGozadas;

                            db.VacacionesEmpleado.Update(registro);
                            await db.SaveChangesAsync();

                            
                        }
                        
                    }
                    
                }// end forEach
                

            } catch (Exception ex) {
                var a = ex;
            }
        }

        

        /// <summary>
        /// Obtiene la cantidad de dias de vacaciones de este período fiscal, hasta la fecha actual,
        /// tomando en cuenta las reglas para vacaciones
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private async Task<int> CalcularVacacionesPorPeriodoFiscal(IndiceOcupacionalModalidadPartida item) {
            
            // Obtención del tiempo trabajado en años
            var WorkingTimeInYears = DateTime.Now.Year - item.Fecha.Year;

            // Obtención de las reglas por tipo de contrato
            var reglas = await db.VacacionRelacionLaboral
                .Where(w => w.IdRelacionLaboral == item.TipoNombramiento.RelacionLaboral.IdRelacionLaboral)
                .FirstOrDefaultAsync();

            // Variable de almacenamiento del total de vacaciones
            var totalDiasVacaciones = 0;
            

            Double diasVacacionesPorMes = (Double) 0;

            // Si hay reglas para calcular las vacaciones empieza el proceso, caso contrario el resultado será 0
            if (reglas != null)
            {
                totalDiasVacaciones = 0;

                // Lógica para la obtención de días que el empleado va a tener este período fiscal
                if (WorkingTimeInYears >= reglas.IncrementoApartirPeriodoFiscal)
                {

                    var maxDiasEmpleado = 1 + (WorkingTimeInYears - reglas.IncrementoApartirPeriodoFiscal) * reglas.IncrementoDiasPorPeriodoFiscal + reglas.MinAcumulacionDias;

                    if (maxDiasEmpleado > reglas.MaxAcumulacionDias)
                    {
                        maxDiasEmpleado = reglas.MaxAcumulacionDias;
                    }


                    diasVacacionesPorMes = (Double)maxDiasEmpleado / (Double)12;

                    Double diasVacacionesHastaHoy = 0;
                    int mesActual = 1;//DateTime.Now.Month;

                    // Obtención de los dias del mes actual
                    var totalDiasMes = DateTime.DaysInMonth(DateTime.Now.Year, mesActual);

                    if (mesActual == 1)
                    {
                        diasVacacionesHastaHoy = ((double)DateTime.Now.Day * (Double)diasVacacionesPorMes) / (Double)totalDiasMes;
                        totalDiasVacaciones = (int)diasVacacionesHastaHoy;
                    }
                    else
                    {
                        diasVacacionesHastaHoy = (DateTime.Now.Day * diasVacacionesPorMes) / totalDiasMes;
                        diasVacacionesHastaHoy = diasVacacionesHastaHoy + ((mesActual - 1) * diasVacacionesPorMes);

                        totalDiasVacaciones = (int)diasVacacionesHastaHoy;
                    }


                }
                else if (WorkingTimeInYears < 1)
                {

                    diasVacacionesPorMes = (Double)reglas.MinAcumulacionDias / (Double)12;
                    var mesActual = DateTime.Now.Month;

                    // Obtención del tiempo trabajado en Meses, +1 porque cuenta el més de ingreso
                    var workingTimeInMonths = mesActual - item.Fecha.Month + 1;

                    // Obtención de los dias que tiene el mes de ingreso
                    var totalDiasMesIngreso = DateTime.DaysInMonth(item.Fecha.Year, item.Fecha.Month);

                    // Obtención del tiempo trabajado en días
                    var workingTimeInDays = 0;

                    // ** Si estamos en el mismo mes del ingreso, se calcula a partir de la fecha
                    // actual, en lugar de todo el mes
                    if (workingTimeInMonths > 1)
                    {
                        // +1 porque cuenta el día de ingreso como día trabajado
                        workingTimeInDays = totalDiasMesIngreso - (item.Fecha.Day) + 1;
                    }
                    else
                    {
                        // +1 porque cuenta el día de ingreso como día trabajado
                        workingTimeInDays = DateTime.Now.Day - item.Fecha.Day + 1;

                        // Validación si hay números negativos por errores de fecha:
                        //Ejemplo: alguien ingresa una fecha de ingreso mayor a la fecha actual
                        if (workingTimeInDays < 0)
                        {
                            workingTimeInDays = 0;
                        }
                    }


                    // Cálculo de los días de vacaciones, por los días trabajados el mes de ingreso
                    var diasVacacionesMesIngreso = (workingTimeInDays * diasVacacionesPorMes)
                            / totalDiasMesIngreso;


                    // Se resta un mes porque ya se tiene el valor de los días de vacaciones del 
                    // mes de ingreso
                    if (workingTimeInMonths > 0)
                    {
                        workingTimeInMonths = workingTimeInMonths - 1;
                    }


                    // suma de los dias de vacaciones del mes de ingreso mas los dias de vacaciones de los
                    // otros meses
                    var diasVacacionesAcumulados = ((Double)diasVacacionesMesIngreso + (Double)(workingTimeInMonths * diasVacacionesPorMes));

                    // El total de días de vacaciones es igual a la parte entera del acumulado total
                    totalDiasVacaciones = (int)diasVacacionesAcumulados;


                    return totalDiasVacaciones;

                }
                else {

                    diasVacacionesPorMes = (Double)reglas.MinAcumulacionDias / (Double)12;

                    Double diasVacacionesHastaHoy = 0;
                    int mesActual = DateTime.Now.Month;

                    // Obtención de los dias del mes actual
                    var totalDiasMes = DateTime.DaysInMonth(DateTime.Now.Year, mesActual);

                    if (mesActual == 1)
                    {
                        diasVacacionesHastaHoy = ((double)DateTime.Now.Day * (Double)diasVacacionesPorMes) / (Double)totalDiasMes;
                        totalDiasVacaciones = (int)diasVacacionesHastaHoy;
                    }
                    else
                    {
                        diasVacacionesHastaHoy = (DateTime.Now.Day * diasVacacionesPorMes) / totalDiasMes;
                        diasVacacionesHastaHoy = diasVacacionesHastaHoy + ((mesActual - 1) * diasVacacionesPorMes);

                        totalDiasVacaciones = (int)diasVacacionesHastaHoy;
                    }

                }

            } // end if
            

            return totalDiasVacaciones;
        }


        /// <summary>
        /// Obtiene la lista de solicitudes planificadas por el empleado, requerido: NombreUsuario
        /// </summary>
        /// <param name="idFiltrosViewModel"></param>
        /// <returns></returns>
        // Post: api/SolicitudPlanificacionVacaciones
        [HttpPost]
        [Route("ListarSolicitudesPlanificacionesVacaciones")]
        public async Task<List<SolicitudPlanificacionVacacionesViewModel>> ListarSolicitudesPlanificacionesVacaciones([FromBody]IdFiltrosViewModel idFiltrosViewModel)
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

                foreach (var item in vacaciones) {
                    vacacionesAcumuladas = vacacionesAcumuladas + item.VacacionesNoGozadas - item.VacacionesGozadas;
                }


                var modelo = await db.SolicitudPlanificacionVacaciones
                    .Where(w=>w.IdEmpleado == usuario.IdEmpleado)
                    .Select(s => new SolicitudPlanificacionVacacionesViewModel
                    {
                        DatosBasicosEmpleadoViewModel = new DatosBasicosEmpleadoViewModel {
                            Nombres = s.Empleado.Persona.Nombres,
                            Apellidos = s.Empleado.Persona.Apellidos,
                            Identificacion = s.Empleado.Persona.Identificacion,
                            IdEmpleado = s.IdEmpleado,
                            IdPersona = s.Empleado.Persona.IdPersona
                        },

                        IdSolicitudPlanificacionVacaciones = s.IdSolicitudPlanificacionVacaciones,

                        FechaDesde = s.FechaDesde,
                        FechaHasta = s.FechaHasta,
                        FechaSolicitud = s.FechaSolicitud,
                        
                        Observaciones = s.Observaciones,

                        Estado = s.Estado,
                        NombreEstado = listaEstados.Where(w1=>w1.ValorEstado == s.Estado).FirstOrDefault().NombreEstado,
                        
                        VacacionesAcumuladas = vacacionesAcumuladas
                        
                    }
                    ).ToListAsync();


                return modelo;
            }
            catch (Exception ex)
            {
                
                return new List<SolicitudPlanificacionVacacionesViewModel>();
            }
        }


        /// <summary>
        /// Obtiene información para poder crear la solicitudPlanificacionVacaciones, requerido: NombreUsuario
        /// </summary>
        /// <param name="idFiltrosViewModel"></param>
        /// <returns></returns>
        // Post: api/SolicitudPlanificacionVacaciones
        [HttpPost]
        [Route("CrearSolicitudesPlanificacionesVacaciones")]
        public async Task<SolicitudPlanificacionVacacionesViewModel> CrearSolicitudesPlanificacionesVacaciones([FromBody]IdFiltrosViewModel idFiltrosViewModel)
        {

            try
            {
                var usuario = await db.Empleado
                    .Where(w => w.NombreUsuario == idFiltrosViewModel.NombreUsuario).FirstOrDefaultAsync();

                var IOMPEmpleado = await db.IndiceOcupacionalModalidadPartida
                    .Include(i=>i.TipoNombramiento)
                    .Where(w => w.IdEmpleado == usuario.IdEmpleado)
                    .OrderByDescending(o=>o.Fecha)
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


                var estado = ConstantesEstadosVacaciones.ListaEstadosVacaciones.Where(w=>w.GrupoAprobacion == 2).FirstOrDefault();


                foreach (var item in vacaciones)
                {
                    vacacionesAcumuladas = vacacionesAcumuladas + item.VacacionesNoGozadas - item.VacacionesGozadas;
                }


                var modelo = new SolicitudPlanificacionVacacionesViewModel
                    {
                        DatosBasicosEmpleadoViewModel = new DatosBasicosEmpleadoViewModel
                        {
                            Nombres = usuario.Persona.Nombres,
                            Apellidos = usuario.Persona.Apellidos,
                            Identificacion = usuario.Persona.Identificacion,
                            IdEmpleado = usuario.IdEmpleado,
                            IdPersona = usuario.Persona.IdPersona
                        },

                        IdSolicitudPlanificacionVacaciones = 0,

                        FechaDesde = DateTime.Now,
                        FechaHasta = DateTime.Now,
                        FechaSolicitud = DateTime.Now,

                        Observaciones = "",

                        Estado = (estado != null) ? estado.ValorEstado : 0,

                        NombreEstado = (estado != null)?estado.NombreEstado:"Sin estados",

                        VacacionesAcumuladas = vacacionesAcumuladas

                    };


                return modelo;
            }
            catch (Exception ex)
            {

                return new SolicitudPlanificacionVacacionesViewModel();
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

      

    }
}