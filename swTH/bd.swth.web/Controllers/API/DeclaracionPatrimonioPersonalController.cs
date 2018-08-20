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
        

        // post: api/DeclaracionPatrimonioPersonal
        [HttpPost]
        [Route("ObtenerHistoricoDeclaracionPatrimonioPersonalPorIdEmpleado")]
        public async Task<DeclaracionPatrimonioPersonalHistoricoViewModel> ObtenerHistoricoDeclaracionPatrimonioPersonalPorIdEmpleado([FromBody] int idEmpleado)
        {

            var modelo = new DeclaracionPatrimonioPersonalHistoricoViewModel {

                NombrePersona = "",
                ApellidoPersona = "",

                ListaDeclaracionPatrimonioPersonal = new List<DeclaracionPatrimonioPersonal>()
                 
            };

            try
            {
                var empleado = await db.Empleado
                    .Include(i => i.Persona)
                    .Where(w => w.IdEmpleado == idEmpleado)
                    .FirstOrDefaultAsync();

                var lista0 = await db.DeclaracionPatrimonioPersonal
                    .Where(w => w.IdEmpleado == idEmpleado)
                    .ToListAsync();

                var lista = await db.OtroIngreso
                    .Include(i=>i.DeclaracionPatrimonioPersonal)
                    .Where(w => 
                        w.DeclaracionPatrimonioPersonal.IdEmpleado == idEmpleado
                    )
                    .Select(s=>new DeclaracionPatrimonioPersonal
                        {
                            IdDeclaracionPatrimonioPersonal = s.IdDeclaracionPatrimonioPersonal,
                            FechaDeclaracion = s.DeclaracionPatrimonioPersonal.FechaDeclaracion,
                            IdEmpleado = s.DeclaracionPatrimonioPersonal.IdEmpleado,
                            TotalEfectivo = s.DeclaracionPatrimonioPersonal.TotalEfectivo,
                            TotalPasivo = s.DeclaracionPatrimonioPersonal.TotalPasivo,
                            TotalPatrimonio = s.DeclaracionPatrimonioPersonal.TotalPatrimonio,
                            Comparativo =  lista0
                            .Where(w=>w.FechaDeclaracion.Year == (s.DeclaracionPatrimonioPersonal.FechaDeclaracion.Year-1) )
                            .OrderByDescending(o => o.FechaDeclaracion)
                            .OrderByDescending(w=>w.IdDeclaracionPatrimonioPersonal)
                            .FirstOrDefault() != null
                            ? s.DeclaracionPatrimonioPersonal.TotalPatrimonio - lista0
                            .Where(w => w.FechaDeclaracion.Year == (s.DeclaracionPatrimonioPersonal.FechaDeclaracion.Year - 1))
                            .OrderByDescending(o => o.FechaDeclaracion)
                            .OrderByDescending(w => w.IdDeclaracionPatrimonioPersonal)
                            .FirstOrDefault()
                            .TotalPatrimonio
                            : s.DeclaracionPatrimonioPersonal.TotalPatrimonio
                        }
                    )
                    .ToListAsync();

                modelo.NombrePersona = empleado.Persona.Nombres;
                modelo.ApellidoPersona = empleado.Persona.Apellidos;
                modelo.ListaDeclaracionPatrimonioPersonal = lista;
                modelo.IdEmpleado = empleado.IdEmpleado;

                return modelo;

            }
            catch (Exception ex)
            {

                return modelo;
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



        // POST: api/DeclaracionPatrimonioPersonal
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



        // POST: api/DeclaracionPatrimonioPersonal
        [HttpPost]
        [Route("AddDeclaracionPatrimonioPersonal")]
        public async Task<Response> AddDeclaracionPatrimonioPersonal([FromBody] ViewModelDeclaracionPatrimonioPersonal viewModelDeclaracionPatrimonioPersonal)
        {
            try {

                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ModeloInvalido
                    };
                }


                var modeloActual = new DeclaracionPatrimonioPersonal
                {

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

                    DescripcionOtros = (viewModelDeclaracionPatrimonioPersonal.OtroIngresoActual.DescripcionOtros != null) ?
                    viewModelDeclaracionPatrimonioPersonal.OtroIngresoActual.DescripcionOtros.ToString().ToUpper() :  null,

                    Total = viewModelDeclaracionPatrimonioPersonal.OtroIngresoActual.Total,

                    Observaciones = (viewModelDeclaracionPatrimonioPersonal.OtroIngresoActual.Observaciones != null )
                    ?viewModelDeclaracionPatrimonioPersonal.OtroIngresoActual.Observaciones.ToString().ToUpper()
                    :null
                };

                using (var transaction = await db.Database.BeginTransactionAsync())
                {
                    await db.DeclaracionPatrimonioPersonal.AddAsync(modeloActual);
                    
                    modeloOtroIngreso.IdDeclaracionPatrimonioPersonal = modeloActual.IdDeclaracionPatrimonioPersonal;
                    await db.OtroIngreso.AddAsync(modeloOtroIngreso);


                    await db.DeclaracionPatrimonioPersonal.AddAsync(modeloPasado);
                    await db.SaveChangesAsync();

                    transaction.Commit();
                }


                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.GuardadoSatisfactorio
                };


            }
            catch (Exception ex) {

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };
            }

        }


        
        // POST: api/DeclaracionPatrimonioPersonal
        [HttpPost]
        [Route("ObtenerDeclaracionPatrimonioPersonal")]
        public async Task<Response> ObtenerDeclaracionPatrimonioPersonal([FromBody] int idDeclaracionPatrimonio)
        {

            try
            {

                var modeloActual = await db.DeclaracionPatrimonioPersonal
                    .Where(w => w.IdDeclaracionPatrimonioPersonal == idDeclaracionPatrimonio)
                    .FirstOrDefaultAsync();

                var modeloOtrosIngresos = await db.OtroIngreso
                    .Where(w => w.IdDeclaracionPatrimonioPersonal == idDeclaracionPatrimonio)
                    .FirstOrDefaultAsync();

                var modeloAnterior = await db.DeclaracionPatrimonioPersonal
                    .Where(w => 
                        w.FechaDeclaracion.Year == (modeloActual.FechaDeclaracion.Year - 1)
                        && w.IdEmpleado == modeloActual.IdEmpleado
                    )
                    .OrderByDescending(o => o.FechaDeclaracion)
                    .OrderByDescending(o=>o.IdDeclaracionPatrimonioPersonal)
                    .FirstOrDefaultAsync();

                if (modeloAnterior == null) {

                    modeloAnterior = new DeclaracionPatrimonioPersonal
                    {
                        IdDeclaracionPatrimonioPersonal = 0,

                        IdEmpleado = modeloActual.IdEmpleado,

                        FechaDeclaracion = new DateTime(modeloActual.FechaDeclaracion.Year-1,1,1),

                        TotalBienInmueble = 0,

                        TotalBienMueble = 0,

                        TotalEfectivo = 0,

                        TotalPasivo = 0,

                        TotalPatrimonio = 0
                    };
                }

                if (modeloOtrosIngresos == null) {

                    modeloOtrosIngresos = new OtroIngreso
                    {
                        IdOtroIngreso = 0,
                        IdDeclaracionPatrimonioPersonal = 0,
                        IngresoConyuge = 0,
                        IngresoArriendos = 0,
                        IngresoNegocioParticular = 0,
                        IngresoRentasFinancieras = 0,
                        OtrosIngresos = 0,
                        DescripcionOtros = null,
                        Total = 0,
                        Observaciones = null
                    };

                }


                var modelo = new ViewModelDeclaracionPatrimonioPersonal
                {
                    IdEmpleado = modeloActual.IdEmpleado,

                    DeclaracionPatrimonioPersonalActual = new DeclaracionPatrimonioPersonal {

                        IdDeclaracionPatrimonioPersonal = modeloActual.IdDeclaracionPatrimonioPersonal,

                        IdEmpleado = modeloActual.IdEmpleado,

                        FechaDeclaracion = modeloActual.FechaDeclaracion,

                        TotalBienInmueble = modeloActual.TotalBienInmueble,

                        TotalBienMueble = modeloActual.TotalBienMueble,

                        TotalEfectivo = modeloActual.TotalEfectivo,

                        TotalPasivo = modeloActual.TotalPasivo,

                        TotalPatrimonio = modeloActual.TotalPatrimonio

                    },


                    DeclaracionPatrimonioPersonalPasado = new DeclaracionPatrimonioPersonal
                    {

                        IdDeclaracionPatrimonioPersonal = modeloAnterior.IdDeclaracionPatrimonioPersonal,

                        IdEmpleado = modeloAnterior.IdEmpleado,

                        FechaDeclaracion = modeloAnterior.FechaDeclaracion,

                        TotalBienInmueble = modeloAnterior.TotalBienInmueble,

                        TotalBienMueble = modeloAnterior.TotalBienMueble,

                        TotalEfectivo = modeloAnterior.TotalEfectivo,

                        TotalPasivo = modeloAnterior.TotalPasivo,

                        TotalPatrimonio = modeloAnterior.TotalPatrimonio

                    },

                    

                    OtroIngresoActual = new OtroIngreso {
                        
                        IdOtroIngreso = modeloOtrosIngresos.IdOtroIngreso,
                        IdDeclaracionPatrimonioPersonal = modeloOtrosIngresos.IdDeclaracionPatrimonioPersonal,
                        IngresoConyuge = modeloOtrosIngresos.IngresoConyuge,
                        IngresoArriendos = modeloOtrosIngresos.IngresoArriendos,
                        IngresoNegocioParticular = modeloOtrosIngresos.IngresoNegocioParticular,
                        IngresoRentasFinancieras = modeloOtrosIngresos.IngresoRentasFinancieras,
                        OtrosIngresos = modeloOtrosIngresos.OtrosIngresos,
                        DescripcionOtros = modeloOtrosIngresos.DescripcionOtros,
                        Total = modeloOtrosIngresos.Total,
                        Observaciones = modeloOtrosIngresos.Observaciones

                    }

                    
                };

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.GuardadoSatisfactorio,
                    Resultado = modelo
                };


            }
            catch (Exception ex) {

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };
            }

        }


        // POST: api/DeclaracionPatrimonioPersonal
        [HttpPost]
        [Route("EditarDeclaracionPatrimonioPersonal")]
        public async Task<Response> EditarDeclaracionPatrimonioPersonal([FromBody] ViewModelDeclaracionPatrimonioPersonal viewModelDeclaracionPatrimonioPersonal) {

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

                

                if (
                    viewModelDeclaracionPatrimonioPersonal
                    .OtroIngresoActual
                    .DescripcionOtros != null
                    )
                {
                    viewModelDeclaracionPatrimonioPersonal.OtroIngresoActual
                        .DescripcionOtros = viewModelDeclaracionPatrimonioPersonal.OtroIngresoActual
                        .DescripcionOtros.ToString().ToUpper();
                }

                if (
                    viewModelDeclaracionPatrimonioPersonal
                    .OtroIngresoActual
                    .Observaciones != null
                    )
                {
                    viewModelDeclaracionPatrimonioPersonal.OtroIngresoActual
                        .Observaciones = viewModelDeclaracionPatrimonioPersonal.OtroIngresoActual
                        .Observaciones.ToString().ToUpper();
                }


                var modeloActual = await db.DeclaracionPatrimonioPersonal
                    .Where(w =>
                        w.IdDeclaracionPatrimonioPersonal == viewModelDeclaracionPatrimonioPersonal
                        .DeclaracionPatrimonioPersonalActual
                        .IdDeclaracionPatrimonioPersonal
                    ).FirstOrDefaultAsync();


                if (modeloActual == null) {

                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado
                    };

                }


                using (var transaction = await db.Database.BeginTransactionAsync())
                {

                    modeloActual.TotalEfectivo = viewModelDeclaracionPatrimonioPersonal
                            .DeclaracionPatrimonioPersonalActual
                            .TotalEfectivo;

                    modeloActual.TotalPasivo = viewModelDeclaracionPatrimonioPersonal
                            .DeclaracionPatrimonioPersonalActual
                            .TotalPasivo;

                    modeloActual.TotalPatrimonio = viewModelDeclaracionPatrimonioPersonal
                            .DeclaracionPatrimonioPersonalActual
                            .TotalPatrimonio;

                    modeloActual.FechaDeclaracion = viewModelDeclaracionPatrimonioPersonal
                            .DeclaracionPatrimonioPersonalActual
                            .FechaDeclaracion;

                    db.DeclaracionPatrimonioPersonal.Update(modeloActual);

                    await db.SaveChangesAsync();


                    if (
                        viewModelDeclaracionPatrimonioPersonal
                        .DeclaracionPatrimonioPersonalPasado
                        .IdDeclaracionPatrimonioPersonal
                        > 0
                        )
                    {
                        var modeloPasado = await db.DeclaracionPatrimonioPersonal
                            .Where(w =>
                                w.IdDeclaracionPatrimonioPersonal == viewModelDeclaracionPatrimonioPersonal
                                .DeclaracionPatrimonioPersonalPasado
                                .IdDeclaracionPatrimonioPersonal
                            ).FirstOrDefaultAsync();

                        
                        modeloPasado.TotalEfectivo = viewModelDeclaracionPatrimonioPersonal
                                .DeclaracionPatrimonioPersonalPasado
                                .TotalEfectivo;

                        modeloPasado.TotalPasivo = viewModelDeclaracionPatrimonioPersonal
                                .DeclaracionPatrimonioPersonalPasado
                                .TotalPasivo;

                        modeloPasado.TotalPatrimonio = viewModelDeclaracionPatrimonioPersonal
                                .DeclaracionPatrimonioPersonalPasado
                                .TotalPatrimonio;

                        modeloPasado.FechaDeclaracion = viewModelDeclaracionPatrimonioPersonal
                                .DeclaracionPatrimonioPersonalPasado
                                .FechaDeclaracion;

                        db.DeclaracionPatrimonioPersonal.Update(modeloPasado);

                        await db.SaveChangesAsync();

                    }
                    else {

                        var modeloPasado = new DeclaracionPatrimonioPersonal {

                            FechaDeclaracion = viewModelDeclaracionPatrimonioPersonal
                                .DeclaracionPatrimonioPersonalPasado
                                .FechaDeclaracion,

                            TotalEfectivo = viewModelDeclaracionPatrimonioPersonal
                                .DeclaracionPatrimonioPersonalPasado
                                .TotalEfectivo,

                            TotalBienInmueble = viewModelDeclaracionPatrimonioPersonal
                                .DeclaracionPatrimonioPersonalPasado
                                .TotalBienInmueble,

                            TotalBienMueble = viewModelDeclaracionPatrimonioPersonal
                                .DeclaracionPatrimonioPersonalPasado
                                .TotalBienMueble,

                            TotalPasivo = viewModelDeclaracionPatrimonioPersonal
                                .DeclaracionPatrimonioPersonalPasado
                                .TotalPasivo,

                            IdEmpleado = modeloActual.IdEmpleado

                        };

                        await db.DeclaracionPatrimonioPersonal.AddAsync(modeloPasado);
                        await db.AddAsync(modeloPasado);
                    }


                    if (
                        viewModelDeclaracionPatrimonioPersonal
                        .OtroIngresoActual
                        .IdOtroIngreso
                        > 0
                        )
                    {
                        var modeloOtroIngreso = await db.OtroIngreso
                            .Where(w =>
                                w.IdOtroIngreso == viewModelDeclaracionPatrimonioPersonal
                                .OtroIngresoActual
                                .IdOtroIngreso
                            ).FirstOrDefaultAsync();


                        modeloOtroIngreso.IngresoArriendos = viewModelDeclaracionPatrimonioPersonal
                            .OtroIngresoActual
                            .IngresoArriendos;

                        modeloOtroIngreso.IngresoConyuge = viewModelDeclaracionPatrimonioPersonal
                            .OtroIngresoActual
                            .IngresoConyuge;

                        modeloOtroIngreso.IngresoNegocioParticular = viewModelDeclaracionPatrimonioPersonal
                            .OtroIngresoActual
                            .IngresoNegocioParticular;

                        modeloOtroIngreso.IngresoRentasFinancieras = viewModelDeclaracionPatrimonioPersonal
                            .OtroIngresoActual
                            .IngresoRentasFinancieras;

                        modeloOtroIngreso.OtrosIngresos = viewModelDeclaracionPatrimonioPersonal
                            .OtroIngresoActual
                            .OtrosIngresos;

                        modeloOtroIngreso.DescripcionOtros = viewModelDeclaracionPatrimonioPersonal
                            .OtroIngresoActual
                            .DescripcionOtros;

                        modeloOtroIngreso.Total = viewModelDeclaracionPatrimonioPersonal
                            .OtroIngresoActual
                            .Total;

                        modeloOtroIngreso.Observaciones = viewModelDeclaracionPatrimonioPersonal
                            .OtroIngresoActual
                            .Observaciones;

                        db.OtroIngreso.Update(modeloOtroIngreso);
                        await db.SaveChangesAsync();

                    }
                    else {

                        var modeloOtrosIngresos = new OtroIngreso
                        {
                            IdDeclaracionPatrimonioPersonal = modeloActual.IdDeclaracionPatrimonioPersonal,

                            IngresoArriendos = viewModelDeclaracionPatrimonioPersonal
                            .OtroIngresoActual
                            .IngresoArriendos,

                            IngresoConyuge = viewModelDeclaracionPatrimonioPersonal
                            .OtroIngresoActual
                            .IngresoConyuge,

                            IngresoNegocioParticular = viewModelDeclaracionPatrimonioPersonal
                            .OtroIngresoActual
                            .IngresoNegocioParticular,

                            IngresoRentasFinancieras = viewModelDeclaracionPatrimonioPersonal
                            .OtroIngresoActual
                            .IngresoRentasFinancieras,

                            OtrosIngresos = viewModelDeclaracionPatrimonioPersonal
                            .OtroIngresoActual
                            .OtrosIngresos,

                            DescripcionOtros = viewModelDeclaracionPatrimonioPersonal
                            .OtroIngresoActual
                            .DescripcionOtros,

                            Total = viewModelDeclaracionPatrimonioPersonal
                            .OtroIngresoActual
                            .Total,

                            Observaciones = viewModelDeclaracionPatrimonioPersonal
                            .OtroIngresoActual
                            .Observaciones,
                        };

                        await db.OtroIngreso.AddAsync(modeloOtrosIngresos);
                        await db.SaveChangesAsync();
                    }

                    transaction.Commit();
                }


                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.GuardadoSatisfactorio
                };


            } catch (Exception ex) {

                return new Response {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
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
        public async Task<Response> DeleteDeclaracionPatrimonioPersonal([FromRoute] string id)
        {
            try
            {
                var ids = Convert.ToInt32(id);

                var modeloPatrimonio = await db.DeclaracionPatrimonioPersonal
                    .Where(w => w.IdDeclaracionPatrimonioPersonal == ids)
                    .FirstOrDefaultAsync();

                if (modeloPatrimonio == null) {

                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado
                    };
                }

                var modeloOtroIngreso = await db.OtroIngreso
                    .Where(w => w.IdDeclaracionPatrimonioPersonal == modeloPatrimonio.IdDeclaracionPatrimonioPersonal)
                    .FirstOrDefaultAsync();


                using (var transaction = await db.Database.BeginTransactionAsync())
                {

                    if (modeloOtroIngreso != null)
                    {
                        db.OtroIngreso.Remove(modeloOtroIngreso);
                        
                    }


                    db.DeclaracionPatrimonioPersonal.Remove(modeloPatrimonio);
                    db.SaveChanges();

                    transaction.Commit();
                }
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.BorradoSatisfactorio,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.BorradoNoSatisfactorio,
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