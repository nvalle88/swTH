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

                // índice ocupacional modalidad partida: IOMP

                // Obtiene la lista de empleados registrados en el IOMP
                // filtrando: los repetidos, por la ultima fecha de registro, y activos
                var empleados = await db.IndiceOcupacionalModalidadPartida
                    .Include(i=>i.TipoNombramiento).ThenInclude(t=>t.RelacionLaboral)
                    .Include(i2=>i2.Empleado)
                    .Where(w=>w.Empleado.Activo == true)
                    .OrderByDescending(o=>o.Fecha)
                    .ToAsyncEnumerable()
                    .Distinct(d => d.IdEmpleado)
                    .ToList();

                


                //**** Realizar las acciones por cada empleado ****
                foreach (var item2 in empleados) {

                    await CalcularYRegistrarVacacionesPorEmpleado(item2.IdEmpleado);

                }// end forEach
                

            } catch (Exception ex) {
                var a = ex;
            }
        }


        /// <summary>
        /// Calcula y [crea o edita(en caso de ya existir registro) en VacacionesEmpleado] las vacaciones del IdEmpleado ingresado
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <returns></returns>
        public async Task CalcularYRegistrarVacacionesPorEmpleado(int idEmpleado) {

            // ********* Creación de varibles para usar en los posteriores cálculos ************************
            //----------------------------------------------------------------------------------------------

            // ** obtiene el registro de vacaciones o null si no existe
            var registroVacaciones = await db.VacacionesEmpleado
                .Where(w =>
                    w.PeriodoFiscal == DateTime.Now.Year
                    && w.IdEmpleado == idEmpleado
                )
                .FirstOrDefaultAsync();


            // ** Variable para guardar el total de vacaciones
            int totalDiasVacacionesEstePeriodoFiscal = 0;


            // ** Historial de registros en IOMP por empleado
            var listaRegistrosIOMP = await db.IndiceOcupacionalModalidadPartida
                .Include(i => i.TipoNombramiento)
                .Where(w => w.IdEmpleado == idEmpleado)
                .OrderBy(o => o.Fecha)
                .ToListAsync();

            // obtener registros IOMP de este año
            var listaRegistrosIOMPPeriodoFiscalActual = listaRegistrosIOMP
                .Where(w => w.Fecha.Year == DateTime.Now.Year)
                .ToList();


            // ** Lista de acciones generadas para el empleado
            var listaAccionesEmpleado = await db.AccionPersonal
                .Include(i => i.TipoAccionPersonal)
                .Where(w =>
                    w.IdEmpleado == idEmpleado
                )
                .ToListAsync();



            // ** Obtiene una lista de desvinculaciones del empleado
            var listaDesvinculacionesEmpleado = listaAccionesEmpleado
                .Where(w =>
                    w.TipoAccionPersonal.Definitivo == true
                    && w.TipoAccionPersonal.DesactivarEmpleado == true
                )
                .OrderByDescending(o => o.FechaRige);



            // ** Obtiene la lista de solicitudes de vacaciones aprobadas para el empleado este Período fiscal
            var listaVacacionesAprobadasEmpleado = await db.SolicitudVacaciones
                .Where(w =>
                    w.IdEmpleado == idEmpleado
                    && w.Estado == 6
                    && w.FechaDesde.Year == DateTime.Now.Year
                ).ToListAsync();

            //------------------------------------------------------------------------------------------------






            // *********** Obtener la última fecha de ingreso del empleado a la institución ******************
            // Variable: FechaInicio
            //------------------------------------------------------------------------------------------------
            DateTime FechaInicioFunciones = listaRegistrosIOMP
                .OrderBy(o => o.Fecha)
                .FirstOrDefault()
                .Fecha;

            if (listaDesvinculacionesEmpleado.Count() > 0)
            {
                var UltimaDesvinculacion = listaDesvinculacionesEmpleado
                    .OrderByDescending(o => o.FechaRige)
                    .FirstOrDefault()
                    .Fecha;

                FechaInicioFunciones = listaRegistrosIOMP
                    .Where(w =>
                        w.Fecha > UltimaDesvinculacion
                     )
                     .OrderBy(o => o.Fecha)
                     .FirstOrDefault().Fecha;
            }

            //-----------------------------------------------------------------------------------------------




            // **************** Cálculo de los días totales de vacaciones por Período fiscal *****************
            //Variable: totalDiasVacacionesEstePeriodoFiscal
            //------------------------------------------------------------------------------------------------
            //// Si no hay movimientos este año se debe calcular normalmente las vacaciones dependiendo de la 
            //// relación laboral actual del empleado
            if (listaRegistrosIOMPPeriodoFiscalActual.Count() == 0)
            {

                var IOMPActual = listaRegistrosIOMP
                    .OrderByDescending(o => o.Fecha)
                    .FirstOrDefault();

                // Obtiene el total de vacaciones desde la fecha de ingreso hasta la fecha actual según
                // el tipo de relacion laboral actual
                totalDiasVacacionesEstePeriodoFiscal = (int)await CalcularVacacionesPorFechas(
                    IOMPActual.TipoNombramiento.IdRelacionLaboral,
                    FechaInicioFunciones,
                    DateTime.Now
                 );
            }
            else
            {

                Double diasPorPeriodo = 0;
                var Fecha1 = FechaInicioFunciones;
                var Fecha2 = DateTime.Now;

                // Obtención de los días de vacaciones de este período, hasta la fecha actual,
                // tomando en cuenta los diferentes registros de IOMP
                for (int i = 0; i < listaRegistrosIOMPPeriodoFiscalActual.Count(); i++)
                {



                    if ((i + 1) < listaRegistrosIOMPPeriodoFiscalActual.Count())
                    {
                        Fecha2 = listaRegistrosIOMPPeriodoFiscalActual.ElementAt(i + 1).Fecha;
                        Fecha2 = Fecha2.AddDays(-1);

                    }
                    else
                    {
                        Fecha2 = DateTime.Now;
                    }

                    diasPorPeriodo = diasPorPeriodo + await CalcularVacacionesPorFechas(
                            listaRegistrosIOMPPeriodoFiscalActual.ElementAt(i).TipoNombramiento.IdRelacionLaboral,
                            Fecha1,
                            Fecha2
                        );

                    if ((i + 1) < listaRegistrosIOMPPeriodoFiscalActual.Count())
                    {
                        Fecha1 = listaRegistrosIOMPPeriodoFiscalActual.ElementAt(i + 1).Fecha;

                    }
                }

                totalDiasVacacionesEstePeriodoFiscal = (int)diasPorPeriodo;

            }

            //-------------------------------------------------------------------------------------------------




            // **** Restar los días de vacaciones solicitados aprobados y los movimientos imputables de vacaciones ******
            // Variable: totalDiasVacacionesEstePeriodoFiscal
            // NOTA: Si algún proceso resta o aumenta vacaciones se debe agregar en esta sección
            //------------------------------------------------------------------------------------------------------------



            var diasVacacionesAprobadasPeriodoFiscalActual = 0;
            var diasImputablesVacaciones = 0;
            var sumatoriaDiasVacacionesUsados = 0;

            // Obtención de los días de vacaciones aprobados
            // Variable: diasVacacionesAprobadasPeriodoFiscalActual
            foreach (var itemSolicitudes in listaVacacionesAprobadasEmpleado)
            {
                diasVacacionesAprobadasPeriodoFiscalActual =
                    diasVacacionesAprobadasPeriodoFiscalActual
                    + (itemSolicitudes.FechaHasta - itemSolicitudes.FechaDesde).Days;
            }
            //-----------------------------------------------------



            // ** Obtención de los días imputables a vacaciones
            var listaImputableVacaciones = listaAccionesEmpleado
                .Where(w => w.TipoAccionPersonal.ImputableVacaciones == true)
                .ToList();

            foreach (var itemImputablesVacaciones in listaImputableVacaciones)
            {
                diasImputablesVacaciones = diasImputablesVacaciones + (int)itemImputablesVacaciones.NoDias;

            }
            //----------------------------------------------------



            // ** Sumatoria de días de vacaciones usados
            sumatoriaDiasVacacionesUsados =
                diasVacacionesAprobadasPeriodoFiscalActual
                + diasImputablesVacaciones
            ;



            // ** Si no existe registro para este período fiscal, debe agregar uno nuevo
            if (registroVacaciones == null)
            {

                var modelo = new VacacionesEmpleado()
                {
                    IdEmpleado = idEmpleado,
                    PeriodoFiscal = DateTime.Now.Year,
                    VacacionesGozadas = sumatoriaDiasVacacionesUsados,
                    VacacionesNoGozadas = totalDiasVacacionesEstePeriodoFiscal
                };

                db.VacacionesEmpleado.Add(modelo);
                await db.SaveChangesAsync();
            }
            else
            {

                // ** Si existe el registro, se actualiza

                registroVacaciones.VacacionesNoGozadas = totalDiasVacacionesEstePeriodoFiscal;
                registroVacaciones.VacacionesGozadas = sumatoriaDiasVacacionesUsados;

                db.VacacionesEmpleado.Update(registroVacaciones);
                await db.SaveChangesAsync();
            }

        }


        
        /// <summary>
        /// Obtiene la cantidad de dias de vacaciones desde una fecha de este período fiscal, hasta la fecha fin,
        /// de este mismo período fiscal
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private async Task<Double> CalcularVacacionesPorFechas(int IdRelacionLaboral,DateTime FechaInicio,DateTime FechaFin)
        {


            // Obtención del tiempo trabajado en años
            var WorkingTimeInYears = FechaFin.Year - FechaInicio.Year;

            // Obtención de las reglas por tipo de contrato
            var reglas = await db.VacacionRelacionLaboral
                .Where(w => w.IdRelacionLaboral == IdRelacionLaboral)
                .FirstOrDefaultAsync();

            // Variable de almacenamiento del total de vacaciones
            Double totalDiasVacaciones = 0;


            Double diasVacacionesPorMes = (Double)0;

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
                    int mesActual = FechaFin.Month;//DateTime.Now.Month;

                    // Obtención de los dias del mes actual
                    var totalDiasMes = DateTime.DaysInMonth(FechaFin.Year, FechaFin.Month);

                    if (mesActual == 1)
                    {
                        diasVacacionesHastaHoy = ((double)FechaFin.Day * (Double)diasVacacionesPorMes) / (Double)totalDiasMes;
                        totalDiasVacaciones = diasVacacionesHastaHoy;
                    }
                    else
                    {
                        diasVacacionesHastaHoy = (FechaFin.Day * diasVacacionesPorMes) / totalDiasMes;
                        diasVacacionesHastaHoy = diasVacacionesHastaHoy + ((mesActual - 1) * diasVacacionesPorMes);

                        totalDiasVacaciones = diasVacacionesHastaHoy;
                    }


                }
                else if (WorkingTimeInYears < 1)
                {

                    diasVacacionesPorMes = (Double)reglas.MinAcumulacionDias / (Double)12;
                    var mesActual = FechaFin.Month;

                    // Obtención del tiempo trabajado en Meses, +1 porque cuenta el més de ingreso
                    var workingTimeInMonths = mesActual - FechaInicio.Month + 1;

                    // Obtención de los dias que tiene el mes de ingreso
                    var totalDiasMesIngreso = DateTime.DaysInMonth(FechaInicio.Year, FechaInicio.Month);

                    // Obtención del tiempo trabajado en días
                    var workingTimeInDays = 0;

                    // ** Si estamos en el mismo mes del ingreso, se calcula a partir de la fecha
                    // actual, en lugar de todo el mes
                    if (workingTimeInMonths > 1)
                    {
                        // +1 porque cuenta el día de ingreso como día trabajado
                        workingTimeInDays = totalDiasMesIngreso - (FechaInicio.Day) + 1;
                    }
                    else
                    {
                        // +1 porque cuenta el día de ingreso como día trabajado
                        workingTimeInDays = FechaFin.Day - FechaInicio.Day + 1;

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
                    totalDiasVacaciones = diasVacacionesAcumulados;


                    return totalDiasVacaciones;

                }
                else
                {

                    diasVacacionesPorMes = (Double)reglas.MinAcumulacionDias / (Double)12;

                    Double diasVacacionesHastaHoy = 0;
                    int mesActual = FechaFin.Month;

                    // Obtención de los dias del mes actual
                    var totalDiasMes = DateTime.DaysInMonth(FechaFin.Year, mesActual);

                    if (mesActual == 1)
                    {
                        diasVacacionesHastaHoy = ((double)FechaFin.Day * (Double)diasVacacionesPorMes) / (Double)totalDiasMes;
                        totalDiasVacaciones = diasVacacionesHastaHoy;
                    }
                    else
                    {
                        diasVacacionesHastaHoy = (FechaFin.Day * diasVacacionesPorMes) / totalDiasMes;
                        diasVacacionesHastaHoy = diasVacacionesHastaHoy + ((mesActual - 1) * diasVacacionesPorMes);

                        totalDiasVacaciones = diasVacacionesHastaHoy;
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
                    vacacionesAcumuladas = vacacionesAcumuladas + item.VacacionesNoGozadas;
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
                var usuario = await db.Empleado.Include(i=>i.Persona)
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

                var hoy = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

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

                        FechaDesde = hoy,
                        FechaHasta = hoy,
                        FechaSolicitud = hoy,

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
        public async Task<Response> InsertarSolicitudPlanificacionVacaciones([FromBody] SolicitudPlanificacionVacacionesViewModel solicitudPlanificacionVacacionesViewModel)
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

                var modelo = new SolicitudPlanificacionVacaciones {
                    Estado = solicitudPlanificacionVacacionesViewModel.Estado,
                    FechaDesde = solicitudPlanificacionVacacionesViewModel.FechaDesde,
                    FechaHasta = solicitudPlanificacionVacacionesViewModel.FechaHasta,
                    FechaSolicitud = DateTime.Now,
                    IdEmpleado = solicitudPlanificacionVacacionesViewModel.DatosBasicosEmpleadoViewModel.IdEmpleado,
                    Observaciones = ""
                    
                };

                db.SolicitudPlanificacionVacaciones.Add(modelo);
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


        // POST: api/SolicitudPlanificacionVacaciones
        [HttpPost]
        [Route("ObtenerSolicitudPlanificacionVacacionesViewModel")]
        public async Task<Response> ObtenerSolicitudPlanificacionVacacionesViewModel([FromBody] string id) {

            try {
                var idSolicitud = Convert.ToInt32(id);

                var estado = ConstantesEstadosVacaciones.ListaEstadosVacaciones;

                var vacacionesAcumuladas = 0;
                
                var modelo = await db.SolicitudPlanificacionVacaciones
                    .Where(w => w.IdSolicitudPlanificacionVacaciones == idSolicitud)
                    .Select(s =>  new SolicitudPlanificacionVacacionesViewModel
                        {
                            DatosBasicosEmpleadoViewModel = new DatosBasicosEmpleadoViewModel
                                {
                                    Nombres = s.Empleado.Persona.Nombres,
                                    Apellidos = s.Empleado.Persona.Apellidos,
                                    IdEmpleado = s.IdEmpleado,
                                    IdPersona = s.Empleado.Persona.IdPersona,
                                    Identificacion = s.Empleado.Persona.Identificacion
                                },

                            IdSolicitudPlanificacionVacaciones = s.IdSolicitudPlanificacionVacaciones,

                            FechaDesde = s.FechaDesde,
                            FechaHasta = s.FechaHasta,
                            FechaSolicitud = s.FechaSolicitud,

                            Observaciones = s.Observaciones,

                            Estado = s.Estado,

                            NombreEstado = estado.Where(est=>est.ValorEstado == s.Estado).FirstOrDefault().NombreEstado,

                            VacacionesAcumuladas = vacacionesAcumuladas

                        }
                    )
                    .FirstOrDefaultAsync();

                var numeroEstado = estado.Where(w => w.ValorEstado == modelo.Estado).FirstOrDefault();

                if (numeroEstado == null) {
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


                if (modelo != null) {
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


                } catch (Exception ex) {

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };
            }
        }


        /// <summary>
        /// Permite editar los datos de ingreso del empleado para planificación de vacaciones,
        /// Necesario: IdSolicitudPlanificacionVacaciones
        /// </summary>
        /// <param name="solicitudPlanificacionVacacionesViewModel"></param>
        /// <returns></returns>
        // POST: api/SolicitudPlanificacionVacaciones
        [HttpPost]
        [Route("EditarSolicitudPlanificacionVacaciones")]
        public async Task<Response> EditarSolicitudPlanificacionVacaciones([FromBody] SolicitudPlanificacionVacacionesViewModel solicitudPlanificacionVacacionesViewModel)
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

                var modelo = await db.SolicitudPlanificacionVacaciones
                    .Where(w => w.IdSolicitudPlanificacionVacaciones == solicitudPlanificacionVacacionesViewModel.IdSolicitudPlanificacionVacaciones)
                    .FirstOrDefaultAsync();

                modelo.Estado = solicitudPlanificacionVacacionesViewModel.Estado;
                modelo.FechaDesde = solicitudPlanificacionVacacionesViewModel.FechaDesde;
                modelo.FechaHasta = solicitudPlanificacionVacacionesViewModel.FechaHasta;
                modelo.FechaSolicitud = DateTime.Now;
                modelo.IdEmpleado = solicitudPlanificacionVacacionesViewModel.DatosBasicosEmpleadoViewModel.IdEmpleado;
                
                db.SolicitudPlanificacionVacaciones.Update(modelo);
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
        /// Permite editar el estado y las observaciones de la planificación del empleado,
        /// Necesario: IdSolicitudPlanificacionVacaciones
        /// </summary>
        /// <param name="solicitudPlanificacionVacacionesViewModel"></param>
        /// <returns></returns>
        // POST: api/SolicitudPlanificacionVacaciones
        [HttpPost]
        [Route("AprobarSolicitudPlanificacionVacaciones")]
        public async Task<Response> AprobarSolicitudPlanificacionVacaciones([FromBody] SolicitudPlanificacionVacacionesViewModel solicitudPlanificacionVacacionesViewModel)
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

                var modelo = await db.SolicitudPlanificacionVacaciones
                    .Where(w => w.IdSolicitudPlanificacionVacaciones == solicitudPlanificacionVacacionesViewModel.IdSolicitudPlanificacionVacaciones)
                    .FirstOrDefaultAsync();

                modelo.Estado = solicitudPlanificacionVacacionesViewModel.Estado;
                modelo.Observaciones = solicitudPlanificacionVacacionesViewModel.Observaciones;

                db.SolicitudPlanificacionVacaciones.Update(modelo);
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
        ///  Devuelve la lista de solicitudes de planificación de vacaciones hechas por el idEmpleado,
        ///  validando que el nombreUsuario sea jefe y esté activado
        ///  Nota: No valida que el usuario sea jefe de la unidad Administrativa a la que pertenece el empleado
        ///  Necesario: IdEmpleado(del que se desea ver las solicitudes), NombreUsuario (logueado)
        /// </summary>
        /// <param name="idFiltrosViewModel"></param>
        /// <returns></returns>
        // POST: api/SolicitudPlanificacionVacaciones
        [HttpPost]
        [Route("ObtenerListaSolicitudPlanificacionVacacionesViewModelPorEmpleado")]
        public async Task<Response> ObtenerListaSolicitudPlanificacionVacacionesViewModelPorEmpleado([FromBody] IdFiltrosViewModel idFiltrosViewModel)
        {

            try
            {
                var usuario = await db.Empleado.Where(w => w.NombreUsuario == idFiltrosViewModel.NombreUsuario && w.Activo == true).FirstOrDefaultAsync();

                if (usuario.EsJefe == true)
                {

                    var listaEstados = ConstantesEstadosVacaciones.ListaEstadosVacaciones;

                    var vacaciones = await db.VacacionesEmpleado
                        .Where(w => w.IdEmpleado == idFiltrosViewModel.IdEmpleado)
                        .ToListAsync();

                    var vacacionesAcumuladas = 0;

                    foreach (var item in vacaciones)
                    {
                        vacacionesAcumuladas = vacacionesAcumuladas + item.VacacionesNoGozadas;
                    }


                    var lista = await db.SolicitudPlanificacionVacaciones
                        .Where(w => w.IdEmpleado == idFiltrosViewModel.IdEmpleado)
                        .Select(s=> new SolicitudPlanificacionVacacionesViewModel
                            {
                            DatosBasicosEmpleadoViewModel = new DatosBasicosEmpleadoViewModel
                            {
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
                            NombreEstado = listaEstados.Where(w1 => w1.ValorEstado == s.Estado).FirstOrDefault().NombreEstado,

                            VacacionesAcumuladas = vacacionesAcumuladas
                        }
                        )
                        .ToListAsync();

                    return new Response
                    {
                        IsSuccess = true,
                        Resultado = lista
                    };
                }
                else {
                    
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.AccesoNoAutorizado
                    };
                }
                

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

        // Get: api/SolicitudPlanificacionVacaciones
        [HttpGet]
        [Route("ListarEstadosAprobador")]
        public async Task<List<EstadoVacacionesViewModel>> ListarEstadosAprobador()
        {
            var listaEstados = ConstantesEstadosVacaciones.ListaEstadosVacaciones
                .Where(w=>w.GrupoAprobacion == 1)
                .ToList();

            return listaEstados;
        }


    }
}