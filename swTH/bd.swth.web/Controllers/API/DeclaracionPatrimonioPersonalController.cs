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

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/DeclaracionPatrimonioPersonal")]
    public class DeclaracionPatrimonioPersonalController : Controller
    {
        private readonly SwTHDbContext db;

        public DeclaracionPatrimonioPersonalController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarDeclaracionesPatrimonioPersonal")]
        public async Task<List<DeclaracionPatrimonioPersonal>> GetDeclaracionPatrimonioPersonal()
        {
            try
            {
                return await db.DeclaracionPatrimonioPersonal.OrderBy(x => x.FechaDeclaracion).ToListAsync();
            }
            catch (Exception ex)
            {
                
                return new List<DeclaracionPatrimonioPersonal>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetDeclaracionPatrimonioPersonal([FromRoute] int id)
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

                var DeclaracionPatrimonioPersonal = await db.DeclaracionPatrimonioPersonal.SingleOrDefaultAsync(m => m.IdDeclaracionPatrimonioPersonal == id);

                if (DeclaracionPatrimonioPersonal == null)
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
                    Resultado = DeclaracionPatrimonioPersonal,
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



        // POST: api/DeclaracionPatrimonioPersonal
        [HttpPost]
        [Route("ObtenerDeclaracionPatrimonial")]
        public async Task<Response> ObtenerDeclaracionPatrimonial([FromBody] ViewModelDeclaracionPatrimonioPersonal viewModelDeclaracionPatrimonioPersonal)
        {
            try {

                var modelo = new ViewModelDeclaracionPatrimonioPersonal();

                modelo.DeclaracionPatrimonioPersonalActual = new DeclaracionPatrimonioPersonal {
                    TotalBienInmueble = 0,
                    TotalBienMueble = 0,
                    TotalPatrimonio = 0,
                    TotalEfectivo = 0,
                    TotalPasivo = 0,
                    FechaDeclaracion = DateTime.Now
                };

                modelo.DeclaracionPatrimonioPersonalPasado = new DeclaracionPatrimonioPersonal {
                    TotalBienInmueble = 0,
                    TotalBienMueble = 0,
                    TotalPatrimonio = 0,
                    TotalEfectivo = 0,
                    TotalPasivo = 0,
                    FechaDeclaracion = new DateTime(DateTime.Now.Year - 1 ,1,1)
                };


                modelo.OtroIngresoActual = new OtroIngreso {
                    IngresoArriendos = 0,
                    IngresoConyuge = 0,
                    IngresoNegocioParticular = 0,
                    IngresoRentasFinancieras = 0,
                    OtrosIngresos = 0,
                    IdDeclaracionPatrimonioPersonal = 0,
                    DescripcionOtros = ""
                };
                
                
                var datosActual = await db.DeclaracionPatrimonioPersonal
                    .Where(w => 
                        w.IdEmpleado == viewModelDeclaracionPatrimonioPersonal.IdEmpleado
                        && w.FechaDeclaracion.Year == DateTime.Now.Year
                    )
                    .FirstOrDefaultAsync();

                var datosPasado = await db.DeclaracionPatrimonioPersonal
                    .Where(w =>
                        w.IdEmpleado == viewModelDeclaracionPatrimonioPersonal.IdEmpleado
                        && w.FechaDeclaracion.Year == (DateTime.Now.Year - 1)
                    )
                    .FirstOrDefaultAsync();




                if (datosActual != null) {
                    modelo.DeclaracionPatrimonioPersonalActual = datosActual;

                    var datosOtrosIngresos = await db.OtroIngreso
                        .Where(w => w.IdDeclaracionPatrimonioPersonal == datosActual.IdDeclaracionPatrimonioPersonal)
                        .FirstOrDefaultAsync();

                    if (datosOtrosIngresos != null) {

                        modelo.OtroIngresoActual = datosOtrosIngresos;

                    }

                }

                if (datosPasado != null)
                {
                    modelo.DeclaracionPatrimonioPersonalPasado = datosPasado;
                }


                return new Response {

                    IsSuccess = true,
                    Resultado = modelo,

                };


            } catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };
            }

        }



        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarDeclaracionPatrimonioPersonal")]
        public async Task<Response> PostDeclaracionPatrimonioPersonal([FromBody] ViewModelDeclaracionPatrimonioPersonal viewModelDeclaracionPatrimonioPersonal)
        {
            try
            {

                if (!ModelState.IsValid) {
                    return new Response {
                        IsSuccess = false,
                        Message = Mensaje.ModeloInvalido
                    };
                }


                var modeloActual = new DeclaracionPatrimonioPersonal {

                    IdEmpleado = viewModelDeclaracionPatrimonioPersonal.IdEmpleado,

                    FechaDeclaracion = viewModelDeclaracionPatrimonioPersonal.DeclaracionPatrimonioPersonalActual.FechaDeclaracion,

                    TotalBienInmueble = viewModelDeclaracionPatrimonioPersonal.DeclaracionPatrimonioPersonalActual.TotalBienInmueble,

                    TotalBienMueble = viewModelDeclaracionPatrimonioPersonal.DeclaracionPatrimonioPersonalActual.TotalBienMueble,

                    TotalEfectivo = viewModelDeclaracionPatrimonioPersonal.DeclaracionPatrimonioPersonalActual.TotalEfectivo,

                    TotalPasivo = viewModelDeclaracionPatrimonioPersonal.DeclaracionPatrimonioPersonalActual.TotalPasivo,

                    TotalPatrimonio = viewModelDeclaracionPatrimonioPersonal.DeclaracionPatrimonioPersonalActual.TotalPatrimonio
                };

                var modeloPasado = new DeclaracionPatrimonioPersonal
                {

                    IdEmpleado = viewModelDeclaracionPatrimonioPersonal.IdEmpleado,

                    FechaDeclaracion = viewModelDeclaracionPatrimonioPersonal.DeclaracionPatrimonioPersonalPasado.FechaDeclaracion,

                    TotalBienInmueble = viewModelDeclaracionPatrimonioPersonal.DeclaracionPatrimonioPersonalPasado.TotalBienInmueble,

                    TotalBienMueble = viewModelDeclaracionPatrimonioPersonal.DeclaracionPatrimonioPersonalPasado.TotalBienMueble,

                    TotalEfectivo = viewModelDeclaracionPatrimonioPersonal.DeclaracionPatrimonioPersonalPasado.TotalEfectivo,

                    TotalPasivo = viewModelDeclaracionPatrimonioPersonal.DeclaracionPatrimonioPersonalPasado.TotalPasivo,

                    TotalPatrimonio = viewModelDeclaracionPatrimonioPersonal.DeclaracionPatrimonioPersonalPasado.TotalPatrimonio
                };

                var modeloOtroIngreso = new OtroIngreso
                {

                    IdDeclaracionPatrimonioPersonal = viewModelDeclaracionPatrimonioPersonal.OtroIngresoActual.IdDeclaracionPatrimonioPersonal,

                    IngresoConyuge = viewModelDeclaracionPatrimonioPersonal.OtroIngresoActual.IngresoConyuge,

                    IngresoArriendos = viewModelDeclaracionPatrimonioPersonal.OtroIngresoActual.IngresoArriendos,

                    IngresoNegocioParticular = viewModelDeclaracionPatrimonioPersonal.OtroIngresoActual.IngresoNegocioParticular,

                    IngresoRentasFinancieras = viewModelDeclaracionPatrimonioPersonal.OtroIngresoActual.IngresoRentasFinancieras,

                    OtrosIngresos = viewModelDeclaracionPatrimonioPersonal.OtroIngresoActual.OtrosIngresos,

                    DescripcionOtros = (viewModelDeclaracionPatrimonioPersonal.OtroIngresoActual.DescripcionOtros != null)?
                    viewModelDeclaracionPatrimonioPersonal.OtroIngresoActual.DescripcionOtros.ToString().ToUpper():"",

                    Total = viewModelDeclaracionPatrimonioPersonal.OtroIngresoActual.Total
                };


                if (
                    modeloPasado.FechaDeclaracion > modeloActual.FechaDeclaracion
                    || modeloActual.FechaDeclaracion.Year > DateTime.Now.Year
                )
                {
                    return new Response {
                        IsSuccess = false,
                        Message = Mensaje.ErrorRevisarFechas

                    };
                }


                if (viewModelDeclaracionPatrimonioPersonal.DeclaracionPatrimonioPersonalActual.IdDeclaracionPatrimonioPersonal > 0)
                {

                    var registroActual = await db.DeclaracionPatrimonioPersonal
                        .Where(w => w.IdDeclaracionPatrimonioPersonal == viewModelDeclaracionPatrimonioPersonal.DeclaracionPatrimonioPersonalActual.IdDeclaracionPatrimonioPersonal)
                        .FirstOrDefaultAsync();

                    registroActual.IdEmpleado = modeloActual.IdEmpleado;
                    registroActual.FechaDeclaracion = modeloActual.FechaDeclaracion;
                    registroActual.TotalBienInmueble = modeloActual.TotalBienInmueble;
                    registroActual.TotalBienMueble = modeloActual.TotalBienMueble;
                    registroActual.TotalEfectivo = modeloActual.TotalEfectivo;
                    registroActual.TotalPasivo = modeloActual.TotalPasivo;
                    registroActual.TotalPatrimonio = modeloActual.TotalPatrimonio;

                    db.DeclaracionPatrimonioPersonal.Update(registroActual);
                    await db.SaveChangesAsync();
                }
                else {
                    db.DeclaracionPatrimonioPersonal.Add(modeloActual);
                }


                if (viewModelDeclaracionPatrimonioPersonal.DeclaracionPatrimonioPersonalPasado.IdDeclaracionPatrimonioPersonal > 0)
                {

                    var registroPasado = await db.DeclaracionPatrimonioPersonal
                        .Where(w => w.IdDeclaracionPatrimonioPersonal == viewModelDeclaracionPatrimonioPersonal.DeclaracionPatrimonioPersonalPasado.IdDeclaracionPatrimonioPersonal)
                        .FirstOrDefaultAsync();

                    registroPasado.IdEmpleado = modeloPasado.IdEmpleado;
                    registroPasado.FechaDeclaracion = modeloPasado.FechaDeclaracion;
                    registroPasado.TotalBienInmueble = modeloPasado.TotalBienInmueble;
                    registroPasado.TotalBienMueble = modeloPasado.TotalBienMueble;
                    registroPasado.TotalEfectivo = modeloPasado.TotalEfectivo;
                    registroPasado.TotalPasivo = modeloPasado.TotalPasivo;
                    registroPasado.TotalPatrimonio = modeloPasado.TotalPatrimonio;

                    db.DeclaracionPatrimonioPersonal.Update(registroPasado);
                    await db.SaveChangesAsync();
                }
                else
                {
                    db.DeclaracionPatrimonioPersonal.Add(modeloPasado);
                }

                if (viewModelDeclaracionPatrimonioPersonal.OtroIngresoActual.IdOtroIngreso > 0)
                {

                    var registroOtroIngreso = await db.OtroIngreso
                        .Where(w => w.IdOtroIngreso == viewModelDeclaracionPatrimonioPersonal.OtroIngresoActual.IdOtroIngreso)
                        .FirstOrDefaultAsync();

                    registroOtroIngreso.IngresoConyuge = modeloOtroIngreso.IngresoConyuge;
                    registroOtroIngreso.IngresoArriendos = modeloOtroIngreso.IngresoArriendos;
                    registroOtroIngreso.IngresoNegocioParticular = modeloOtroIngreso.IngresoNegocioParticular;
                    registroOtroIngreso.IngresoRentasFinancieras = modeloOtroIngreso.IngresoRentasFinancieras;
                    registroOtroIngreso.OtrosIngresos = modeloOtroIngreso.OtrosIngresos;
                    registroOtroIngreso.DescripcionOtros = modeloOtroIngreso.DescripcionOtros;
                    registroOtroIngreso.Total = modeloOtroIngreso.Total;

                }
                else {
                    modeloOtroIngreso.IdDeclaracionPatrimonioPersonal = modeloActual.IdDeclaracionPatrimonioPersonal;
                    db.OtroIngreso.Add(modeloOtroIngreso);
                    
                }

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

        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarOtrosIngresos")]
        public async Task<Response> PostOtrosIngresos([FromBody] OtroIngreso otroIngreso)
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

                var respuesta = ExisteOtroIngreso(otroIngreso);
                if (!respuesta.IsSuccess)
                {
                    db.OtroIngreso.Add(otroIngreso);
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
                    Message = Mensaje.ExisteRegistro
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

        // DELETE: api/BasesDatos/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteDeclaracionPatrimonioPersonal([FromRoute] int id)
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

                var respuesta = await db.DeclaracionPatrimonioPersonal.SingleOrDefaultAsync(m => m.IdDeclaracionPatrimonioPersonal == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.DeclaracionPatrimonioPersonal.Remove(respuesta);
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

        private Response ExisteDeclaracionPatrimonioPersonal(ViewModelDeclaracionPatrimonioPersonal viewModelDeclaracionPatrimonioPersonal)
        {
            var bdd = viewModelDeclaracionPatrimonioPersonal.DeclaracionPatrimonioPersonalActual.IdEmpleado;
            var bdd2 = viewModelDeclaracionPatrimonioPersonal.DeclaracionPatrimonioPersonalActual.FechaDeclaracion.Date.Year;
            var declaracionrespuesta = db.DeclaracionPatrimonioPersonal.Where(p => p.IdEmpleado == bdd && p.FechaDeclaracion.Date.Year == bdd2).FirstOrDefault();
            if (declaracionrespuesta != null || bdd2 > DateTime.Now.Year)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "No puede realizar nuevamente una Declaración de Patrimonio Personal del mismo año o superior",
                    Resultado = null,
                };

            }
            

            return new Response
            {
                IsSuccess = false,
                Resultado = declaracionrespuesta,
            };
        }

        private Response ExisteOtroIngreso(OtroIngreso otroIngreso)
        {
            var bdd = otroIngreso.IdDeclaracionPatrimonioPersonal;
            var otroingresorespuesta = db.OtroIngreso.Where(p => p.IdDeclaracionPatrimonioPersonal == bdd).FirstOrDefault();
            if (otroingresorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = null,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = otroIngreso,
            };
        }



    }
}