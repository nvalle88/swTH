using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.Enumeradores;
using bd.swth.entidades.Utils;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.log.guardar.Enumeradores;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/TrayectoriasLaborales")]
    public class TrayectoriasLaboralesController : Controller
    {
        private readonly SwTHDbContext db;

        public TrayectoriasLaboralesController(SwTHDbContext db)
        {
            this.db = db;
        }

        
        // GET: api/TrayectoriaLaboral
        [HttpGet]
        [Route("ListarTrayectoriasLaborales")]
        public async Task<List<TrayectoriaLaboral>> GetTrayectoriasLaborales()
        {
            try
            {
                return await db.TrayectoriaLaboral.Include(x => x.Persona).OrderBy(x => x.Empresa).ToListAsync();
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
                return new List<TrayectoriaLaboral>();
            }
        }

        // GET: api/PersonaEstudio
        [HttpPost]
        [Route("ListarTrayectoriasLaboralesporEmpleado")]
        public async Task<List<TrayectoriaLaboral>> GetTrayectoriasLaboralesById([FromBody] Empleado empleado)
        {
            try
            {
                var lista = await db.TrayectoriaLaboral
                    .Where(x => x.IdPersona == empleado.IdPersona)
                    .Select(s=> new TrayectoriaLaboral {
                        IdTrayectoriaLaboral = s.IdTrayectoriaLaboral,
                        IdPersona = s.IdPersona,
                        FechaInicio = s.FechaInicio,
                        FechaFin = s.FechaFin,
                        Empresa = s.Empresa,
                        PuestoTrabajo = s.PuestoTrabajo,
                        TipoInstitucion = s.TipoInstitucion,
                        FormaIngreso = s.FormaIngreso,
                        MotivoSalida = s.MotivoSalida,
                        AreaAsignada = s.AreaAsignada,
                        DescripcionFunciones = s.DescripcionFunciones,

                        NumeroDias = ObtenerDiferenciaFechasEnDias(s.FechaInicio, s.FechaFin),

                        TiempoTexto = ObtenerDiferenciaFechasEnTexto(s.FechaInicio,s.FechaFin),
                            

                    })
                    .OrderBy(x => x.FechaInicio)
                    .ToListAsync();

                return lista;
            }
            catch (Exception ex)
            {
                return new List<TrayectoriaLaboral>();
            }
        }

        private int ObtenerDiferenciaFechasEnDias(DateTime fechaInicio, DateTime fechaFin) {
            var days = 0;

            if (fechaFin < fechaInicio) {
                return days;
            }

            // Se añade + 1 porque cuenta también el día de ingreso
            days = (fechaFin - fechaInicio).Days + 1;

            return days;
        }


        private string ObtenerDiferenciaFechasEnTexto(DateTime fechaInicio, DateTime fechaFin) {
            string respuesta="";

            int year = 0;
            int month = 0;
            int days = 0;

            // ---- Cálculo de años ----
            year = fechaFin.Year - fechaInicio.Year;

            //  No calcular si la fechafin > fecha inicio
            if (fechaFin < fechaInicio) {

                respuesta = Mensaje.ErrorFechaFinMenorFechaInicio.ToString().ToUpper();
                return respuesta;
            }

            if (year > 0)
            {
                // se resta 1 año porque ya se calculan los meses del año de ingreso
                year = year - 1;

                // ---- Cálculo de meses ----
                var monthToEndYear = (12 - fechaInicio.Month);
                var thisMonth = fechaFin.Month;
                

                month = monthToEndYear + thisMonth;



                if (month / 12 > 1)
                {
                    year = year + ((int)month / 12);
                    month = month % 12;
                } else if (month == 12) {
                    month = 0;
                }


                // resto 1 mes porque ya se calculan los días del mes de ingreso
                month = month - 1;


                // ----Cálculo de días----
                var daysFirstMonth = DateTime.DaysInMonth(fechaInicio.Year, fechaInicio.Month);

                // Se añade 1 día porque se toma en cuenta también el día de ingreso
                var daysToEndMonth = daysFirstMonth - fechaInicio.Day + 1;
                days = daysToEndMonth + fechaFin.Day;


                if (days / daysFirstMonth >=1) {

                    month = month + (days / daysFirstMonth);
                    days = (days % daysFirstMonth);
                }


            }
            else if (year == 0)
            {
                month = fechaFin.Month - fechaInicio.Month;

                if (month > 0)
                {
                    // ---- Cálculo de días ----
                    var diasMesInicio = DateTime.DaysInMonth(fechaInicio.Year, fechaInicio.Month);

                    // Se añade un día porque también cuenta el día de ingreso
                    var diasHastaFinMes = diasMesInicio - fechaInicio.Day + 1;
                    days = diasHastaFinMes + fechaFin.Day;


                    // ---- Cálculo de meses ---

                    // Se resta 1 porque ya se calcularon los días del primer mes
                    month = fechaFin.Month - fechaInicio.Month - 1;

                    if (days / diasHastaFinMes >= 1)
                    {

                        month = month + (days / diasMesInicio);
                        days = (days % diasMesInicio);
                    }

                }
                else
                {
                    var diasMesInicio = DateTime.DaysInMonth(fechaInicio.Year, fechaInicio.Month);

                    // Se añade un día porque también cuenta el día de ingreso
                    days = fechaFin.Day - fechaInicio.Day + 1;

                    if (days / diasMesInicio >= 1)
                    {

                        month = month + (days / diasMesInicio);
                        days = (days % diasMesInicio);
                    }

                }
            }


            switch (year) {
                case 0:
                    respuesta = "";
                break;case 1:
                    respuesta = year + " año,";
                break;default:
                    respuesta = year + " años,";
                break;
            }

            switch (month)
            {
                case 0:
                    respuesta = respuesta + " " + month + " meses";
                break;case 1:
                    respuesta = respuesta +" "+ month + " mes";

                break;default:
                    respuesta = respuesta + " " + month + " meses";
                break;
            }

            switch (days)
            {
                case 0:
                    respuesta = respuesta + " y " + days + " días";
                    break;
                case 1:
                    respuesta = respuesta + " y " + days + " día";

                    break;
                default:
                    respuesta = respuesta + " y " + days + " días";
                    break;
            }

            return respuesta.ToString().ToUpper();
        }

        // GET: api/TrayectoriaLaboral/5
        [HttpGet("{id}")]
        public async Task<Response> GetTrayectoriaLaboral([FromRoute] int id)
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

                var TrayectoriaLaboral = await db.TrayectoriaLaboral.SingleOrDefaultAsync(m => m.IdTrayectoriaLaboral == id);

                if (TrayectoriaLaboral == null)
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
                    Resultado = TrayectoriaLaboral,
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

        // PUT: api/TrayectoriaLaboral/5
        [HttpPut("{id}")]
        public async Task<Response> PutTrayectoriaLaboral([FromRoute] int id, [FromBody] TrayectoriaLaboral trayectoriaLaboral)
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

                var existe = Existe(trayectoriaLaboral);
                var TrayectoriaLaboralActualizar = (TrayectoriaLaboral)existe.Resultado;


                if (existe.IsSuccess && TrayectoriaLaboralActualizar.IdTrayectoriaLaboral != id)
                {
                  
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }
                var TrayectoriaLaboral = await db.TrayectoriaLaboral.Where(x => x.IdTrayectoriaLaboral == trayectoriaLaboral.IdTrayectoriaLaboral).FirstOrDefaultAsync();

                TrayectoriaLaboral.IdPersona = trayectoriaLaboral.IdPersona;
                TrayectoriaLaboral.FechaInicio = trayectoriaLaboral.FechaInicio;
                TrayectoriaLaboral.FechaFin = trayectoriaLaboral.FechaFin;
                TrayectoriaLaboral.Empresa = trayectoriaLaboral.Empresa.ToString().ToUpper();
                TrayectoriaLaboral.PuestoTrabajo = trayectoriaLaboral.PuestoTrabajo.ToString().ToUpper();
                TrayectoriaLaboral.DescripcionFunciones = trayectoriaLaboral.DescripcionFunciones.ToString().ToUpper();

                TrayectoriaLaboral.AreaAsignada = trayectoriaLaboral.AreaAsignada.ToString().ToUpper();
                TrayectoriaLaboral.FormaIngreso = trayectoriaLaboral.FormaIngreso.ToString().ToUpper();
                TrayectoriaLaboral.MotivoSalida = trayectoriaLaboral.MotivoSalida.ToString().ToUpper();
                TrayectoriaLaboral.TipoInstitucion = trayectoriaLaboral.TipoInstitucion.ToString().ToUpper();
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
                    IsSuccess = true,
                    Message = Mensaje.Excepcion,
                };
            }

        }

        // POST: api/TrayectoriaLaboral
        [HttpPost]
        [Route("InsertarTrayectoriaLaboral")]
        public async Task<Response> PostTrayectoriaLaboral([FromBody] TrayectoriaLaboral TrayectoriaLaboral)
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

                var respuesta = Existe(TrayectoriaLaboral);
                if (!respuesta.IsSuccess)
                {
                    // Convertir a mayúsculas
                    TrayectoriaLaboral.Empresa = TrayectoriaLaboral.Empresa.ToString().ToUpper();
                    TrayectoriaLaboral.PuestoTrabajo = TrayectoriaLaboral.PuestoTrabajo.ToString().ToUpper();
                    TrayectoriaLaboral.AreaAsignada = TrayectoriaLaboral.AreaAsignada.ToString().ToUpper();
                    TrayectoriaLaboral.FormaIngreso = TrayectoriaLaboral.FormaIngreso.ToString().ToUpper();
                    TrayectoriaLaboral.MotivoSalida = TrayectoriaLaboral.MotivoSalida.ToString().ToUpper();
                    TrayectoriaLaboral.DescripcionFunciones = TrayectoriaLaboral.ToString().ToUpper();
                    TrayectoriaLaboral.TipoInstitucion = TrayectoriaLaboral.TipoInstitucion.ToString().ToUpper();

                    db.TrayectoriaLaboral.Add(TrayectoriaLaboral);
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

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }

        // DELETE: api/TrayectoriaLaboral/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteTrayectoriaLaboral([FromRoute] int id)
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

                var respuesta = await db.TrayectoriaLaboral.SingleOrDefaultAsync(m => m.IdTrayectoriaLaboral == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.TrayectoriaLaboral.Remove(respuesta);
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

        private Response Existe(TrayectoriaLaboral TrayectoriaLaboral)
        {
            var fechaInicio = TrayectoriaLaboral.FechaInicio;
            var fechaFin = TrayectoriaLaboral.FechaFin;
            var Empresa = TrayectoriaLaboral.Empresa;
            var PuestoTrabajo = TrayectoriaLaboral.PuestoTrabajo.ToString().ToUpper();
            var DescripcionFunciones = TrayectoriaLaboral.DescripcionFunciones.ToString().ToUpper();
            var tipoInstitucion = TrayectoriaLaboral.TipoInstitucion.ToString().ToUpper();
            var areaAsignada = TrayectoriaLaboral.AreaAsignada.ToString().ToUpper();
            var formaIngreso = TrayectoriaLaboral.FormaIngreso.ToString().ToUpper();
            var motivoSalida = TrayectoriaLaboral.MotivoSalida.ToString().ToUpper();

            var TrayectoriaLaboralrespuesta = db.TrayectoriaLaboral
                .Where(p => 
                p.FechaInicio == fechaInicio 
                && p.FechaFin == fechaFin
                && p.Empresa == Empresa.ToString().ToUpper()
                && p.PuestoTrabajo == PuestoTrabajo.ToString().ToUpper()
                && p.TipoInstitucion == tipoInstitucion.ToString().ToUpper()
                && p.AreaAsignada == areaAsignada.ToString().ToUpper()
                && p.FormaIngreso == formaIngreso.ToString().ToUpper()
                && p.MotivoSalida == motivoSalida.ToString().ToUpper()
                && p.DescripcionFunciones == DescripcionFunciones.ToString().ToUpper()
                ).FirstOrDefault();

            if (TrayectoriaLaboralrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = TrayectoriaLaboralrespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = TrayectoriaLaboralrespuesta,
            };
        }
    }
}
