using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using System;
using bd.swth.entidades.Enumeradores;
using bd.swth.entidades.Utils;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.log.guardar.Enumeradores;
using bd.swth.entidades.ViewModels;
using bd.swth.entidades.Constantes;
using MoreLinq;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/AccionesPersonal")]
    public class AccionesPersonalController : Controller
    {
        private readonly SwTHDbContext db;

        public AccionesPersonalController(SwTHDbContext db)
        {
            this.db = db;
        }


        /// <summary>
        /// Este m�todo es usado en el timer para actualizar los d�as restantes en cada movimiento de personal
        /// </summary>
        /// <returns></returns>
        public async Task ActualizarDiasRestantesAccionPersonal()
        {

            try
            {

                var hoy = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                var lista = await db.AccionPersonal.Where(w => w.Fecha.Year == hoy.Year).ToListAsync();

                foreach (var item in lista)
                {

                    if (item.FechaRige < hoy)
                    {

                        TimeSpan tiempo = (TimeSpan)(item.FechaRigeHasta - hoy);
                        var diasRestantes = (int)tiempo.TotalDays;

                        if (diasRestantes < 1)
                        {
                            diasRestantes = 0;
                        }

                        if (Convert.ToInt32(item.Numero) > diasRestantes && Convert.ToInt32(item.Numero) > 0)
                        {

                            var registro = db.AccionPersonal.Find(item.IdAccionPersonal);
                            registro.Numero = diasRestantes + "";
                            db.AccionPersonal.Update(registro);
                            await db.SaveChangesAsync();

                        }

                    }
                }


            }
            catch (Exception ex)
            {
            }

        }


        // POST: api/AccionesPersonal
        [HttpPost]
        [Route("ListarAccionesPersonalPorEmpleado")]
        public async Task<AccionesPersonalPorEmpleadoViewModel> ListarAccionesPersonalPorEmpleado([FromBody] AccionesPersonalPorEmpleadoViewModel accionesPersonalPorEmpleadoViewModel)
        {
            var modelo = new AccionesPersonalPorEmpleadoViewModel
            {
                ListaAccionPersonal = new List<AccionPersonalViewModel>(),
                DatosBasicosEmpleadoViewModel = new DatosBasicosEmpleadoViewModel()
            };

            try
            {

                var empleadoActual = db.Empleado.Include(d => d.Dependencia)
                    .Where(x => x.NombreUsuario == accionesPersonalPorEmpleadoViewModel.NombreUsuarioActual)
                    .FirstOrDefault()
                ;

                var datosEmpleado = db.Empleado
                    .Include(me => me.Persona)
                    .Include(md => md.Dependencia)
                        .Where(we =>
                            we.Persona.Identificacion
                            == accionesPersonalPorEmpleadoViewModel.DatosBasicosEmpleadoViewModel.Identificacion
                            && we.Dependencia.IdSucursal == empleadoActual.Dependencia.IdSucursal
                            )
                            .Select(se => new DatosBasicosEmpleadoViewModel
                            {
                                IdEmpleado = se.IdEmpleado,
                                Nombres = se.Persona.Nombres + " " + se.Persona.Apellidos,

                            }
                        ).FirstOrDefault();

                var ListaEstados = ConstantesEstadosAprobacionMovimientoInterno.ListaEstadosAprobacionMovimientoInterno;


                var lista = await db.AccionPersonal.Include(map => map.TipoAccionPersonal)
                    .Where(w => w.IdEmpleado == datosEmpleado.IdEmpleado)
                    .Select(s => new AccionPersonalViewModel
                    {
                        IdAccionPersonal = s.IdAccionPersonal,
                        Fecha = (DateTime)s.Fecha,
                        Numero = s.Numero,
                        Solicitud = s.Solicitud,
                        Explicacion = s.Explicacion,
                        FechaRige = (DateTime)s.FechaRige,
                        FechaRigeHasta = s.FechaRigeHasta,
                        NoDias = (int)s.NoDias,
                        Estado = s.Estado,
                        EstadoDirector = (ListaEstados.Count > 0)
                            ?
                                ListaEstados.Where(wle =>
                                    (wle.GrupoAprobacion == 0 || wle.GrupoAprobacion == 2)
                                    && wle.ValorEstado == s.Estado
                                ).FirstOrDefault().NombreEstado
                            : "",
                        EstadoValidacionTTHH = (ListaEstados.Count > 0)
                            ?
                                ListaEstados.Where(wle =>
                                    wle.GrupoAprobacion == 1
                                    && wle.ValorEstado == s.Estado
                                ).FirstOrDefault().NombreEstado
                            : "",


                        TipoAccionPersonalViewModel = db.TipoAccionPersonal
                            .Where(tapw => tapw.IdTipoAccionPersonal == s.IdTipoAccionPersonal)
                            .Select(st => new TipoAccionesPersonalViewModel
                            {
                                IdTipoAccionPersonal = st.IdTipoAccionPersonal,
                                Nombre = st.Nombre,
                                NDiasMinimo = st.NDiasMinimo,
                                NDiasMaximo = st.NDiasMaximo,
                                NHorasMinimo = st.NHorasMinimo,
                                NHorasMaximo = st.NHorasMaximo,
                                DiasHabiles = st.DiasHabiles,
                                ImputableVacaciones = st.ImputableVacaciones,
                                ProcesoNomina = st.ProcesoNomina,
                                EsResponsableTH = st.EsResponsableTH,
                                Matriz = st.Matriz,
                                Descripcion = st.Descripcion,
                                GeneraAccionPersonal = st.GeneraAccionPersonal,
                                ModificaDistributivo = st.ModificaDistributivo

                            }
                            )
                            .FirstOrDefault()
                    }

                    ).ToListAsync();

                modelo.DatosBasicosEmpleadoViewModel = datosEmpleado;
                modelo.ListaAccionPersonal = lista;

                return modelo;


            }
            catch (Exception ex)
            {
                return modelo;
            }
        }


        // POST: api/AccionesPersonal
        [HttpPost]
        [Route("ObtenerNuevaAccionPersonalViewModel")]
        public async Task<Response> ObtenerNuevaAccionPersonalViewModel([FromBody] int IdEmpleado)
        {
            try {
                

                var empleado = await db.Empleado
                    .Include(i=>i.Persona)
                    .Include(i=>i.Dependencia)
                    .Include(i=>i.Dependencia.Sucursal)
                    .Where(w => 
                        w.IdEmpleado == IdEmpleado
                        && w.Activo == true
                    )
                    .FirstOrDefaultAsync();


                if (empleado == null)
                {

                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.EmpleadoNoExiste

                    };
                }


                var indiceOcupacionalModalidadPartida = await db.IndiceOcupacionalModalidadPartida
                    .Include(i=>i.IndiceOcupacional)
                    .Include(i=>i.IndiceOcupacional.EscalaGrados)
                    .Include(i => i.IndiceOcupacional.ManualPuesto)
                    .Include(i=> i.TipoNombramiento)
                    .Include(i => i.TipoNombramiento.RelacionLaboral)
                    .Where(w=>w.IdEmpleado == IdEmpleado)
                    .OrderByDescending(o=>o.Fecha)
                    .FirstOrDefaultAsync();

                var listaIOMPOcupados = await db.IndiceOcupacionalModalidadPartida
                    .Include(i=>i.Empleado)
                    .Include(i => i.Empleado.Persona)
                    .Include(i => i.IndiceOcupacional)
                    .Include(i => i.IndiceOcupacional.Dependencia)
                    .Include(i => i.IndiceOcupacional.Dependencia.Sucursal)
                    .Include(i=>i.IndiceOcupacional.EscalaGrados)
                    .Include(i => i.FondoFinanciamiento)
                    .Include(i => i.ModalidadPartida)
                    .Where(w => 
                        w.IdEmpleado != null 
                        && w.Empleado.Activo == true
                    )
                    .OrderByDescending(o=>o.Fecha)
                    .DistinctBy(d=>d.IdEmpleado)
                    .ToAsyncEnumerable()
                    .ToList();

                var listaPuestosVacios = await db.IndiceOcupacionalModalidadPartida
                    .Include(i => i.IndiceOcupacional)
                    .Include(i => i.IndiceOcupacional.EscalaGrados)
                    .Include(i => i.IndiceOcupacional.Dependencia)
                    .Include(i => i.IndiceOcupacional.Dependencia.Sucursal)
                    .Include(i => i.ModalidadPartida)
                    .Include(i=>i.FondoFinanciamiento)
                    .Where(w =>
                        w.IdEmpleado == null && w.NumeroPartidaIndividual != null
                    ).ToListAsync();
                

                foreach (var item in listaPuestosVacios) {
                    listaIOMPOcupados.Add(item);
                }


                var modelo = new AccionPersonalViewModel {

                    EmpleadoMovimiento = new EmpleadoMovimiento {
                        Empleado = empleado,
                        IndiceOcupacionalModalidadPartidaDesde = indiceOcupacionalModalidadPartida,
                        IdIndiceOcupacionalModalidadPartidaHasta = 0
                    },

                    ListaPuestosOcupados = listaIOMPOcupados,
                    
                    Numero = "0",
                    Fecha = DateTime.Now,
                    FechaRige = DateTime.Now,
                    FechaRigeHasta = DateTime.Now,
                    GeneraMovimientoPersonal = false,
                    ConfigurarPuesto = false
                };


                return new Response
                {
                    IsSuccess = true,
                    Resultado = modelo
                };


            } catch (Exception ex)
            {
                return new Response {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }
        }


        // POST: api/AccionesPersonal
        [HttpPost]
        [Route("ObtenerAccionPersonalViewModelParaEditar")]
        public async Task<Response> ObtenerAccionPersonalViewModelParaEditar([FromBody] int IdAccionPersonal)
        {
            try
            {
                var accionPersonal = await db.AccionPersonal
                    .Include(i=>i.TipoAccionPersonal)
                    .Where(w => w.IdAccionPersonal == IdAccionPersonal)
                    .FirstOrDefaultAsync();


                if (accionPersonal == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado
                    };
                }

                var empleado = await db.Empleado
                    .Include(i => i.Persona)
                    .Include(i => i.Dependencia)
                    .Include(i => i.Dependencia.Sucursal)
                    .Where(w =>
                        w.IdEmpleado == accionPersonal.IdEmpleado
                        && w.Activo == true
                    )
                    .FirstOrDefaultAsync();


                if (empleado == null)
                {

                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.EmpleadoNoExiste

                    };
                }


                var indiceOcupacionalModalidadPartida = await db.IndiceOcupacionalModalidadPartida
                    .Include(i => i.IndiceOcupacional)
                    .Include(i => i.IndiceOcupacional.EscalaGrados)
                    .Include(i => i.IndiceOcupacional.ManualPuesto)
                    .Include(i => i.TipoNombramiento)
                    .Include(i => i.TipoNombramiento.RelacionLaboral)
                    .Where(w => w.IdEmpleado == accionPersonal.IdEmpleado)
                    .OrderByDescending(o => o.Fecha)
                    .FirstOrDefaultAsync();

                var listaIOMPOcupados = await db.IndiceOcupacionalModalidadPartida
                    .Include(i => i.Empleado)
                    .Include(i => i.Empleado.Persona)
                    .Include(i => i.IndiceOcupacional)
                    .Include(i => i.IndiceOcupacional.Dependencia)
                    .Include(i => i.IndiceOcupacional.Dependencia.Sucursal)
                    .Include(i => i.IndiceOcupacional.EscalaGrados)
                    .Include(i => i.FondoFinanciamiento)
                    .Include(i => i.ModalidadPartida)
                    .Where(w =>
                        w.IdEmpleado != null
                        && w.Empleado.Activo == true
                    )
                    .OrderByDescending(o => o.Fecha)
                    .DistinctBy(d => d.IdEmpleado)
                    .ToAsyncEnumerable()
                    .ToList();

                var listaPuestosVacios = await db.IndiceOcupacionalModalidadPartida
                    .Include(i => i.IndiceOcupacional)
                    .Include(i => i.IndiceOcupacional.EscalaGrados)
                    .Include(i => i.IndiceOcupacional.Dependencia)
                    .Include(i => i.IndiceOcupacional.Dependencia.Sucursal)
                    .Include(i => i.ModalidadPartida)
                    .Include(i => i.FondoFinanciamiento)
                    .Where(w =>
                        w.IdEmpleado == null && w.NumeroPartidaIndividual != null
                    ).ToListAsync();


                foreach (var item in listaPuestosVacios)
                {
                    listaIOMPOcupados.Add(item);
                }



                var empleadoMovimiento = await db.EmpleadoMovimiento
                    .Include(i=>i.IndiceOcupacionalModalidadPartidaDesde)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaDesde.TipoNombramiento)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaDesde.TipoNombramiento.RelacionLaboral)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaDesde.TipoNombramiento.RelacionLaboral.RegimenLaboral)


                    .Include(i=>i.IndiceOcupacionalModalidadPartidaHasta)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaHasta.TipoNombramiento)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaHasta.TipoNombramiento.RelacionLaboral)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaHasta.TipoNombramiento.RelacionLaboral.RegimenLaboral)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaHasta.IndiceOcupacional)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaHasta.IndiceOcupacional.Dependencia)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaHasta.IndiceOcupacional.Dependencia.Sucursal)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaHasta.IndiceOcupacional.ManualPuesto)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaHasta.IndiceOcupacional.RolPuesto)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaHasta.IndiceOcupacional.EscalaGrados)
                    .Where(w => w.IdAccionPersonal == accionPersonal.IdAccionPersonal)
                    .FirstOrDefaultAsync();

                

                var modelo = new AccionPersonalViewModel
                {

                    EmpleadoMovimiento = empleadoMovimiento != null?
                    empleadoMovimiento
                    :
                    new EmpleadoMovimiento
                    {
                        Empleado = empleado,
                        IndiceOcupacionalModalidadPartidaDesde = indiceOcupacionalModalidadPartida,
                        IdIndiceOcupacionalModalidadPartidaHasta = 0
                    },

                    ListaPuestosOcupados = listaIOMPOcupados,

                    IdAccionPersonal = accionPersonal.IdAccionPersonal,
                    Solicitud = accionPersonal.Solicitud,
                    Explicacion = accionPersonal.Explicacion,

                    NoDias = accionPersonal.NoDias,
                    Numero = accionPersonal.Numero,
                    Fecha = accionPersonal.Fecha,
                    FechaRige = accionPersonal.FechaRige,
                    FechaRigeHasta = accionPersonal.FechaRigeHasta,


                    GeneraMovimientoPersonal = (empleadoMovimiento != null) ? true:false,
                    ConfigurarPuesto = (empleadoMovimiento != null 
                    && empleadoMovimiento.IdIndiceOcupacionalModalidadPartidaHasta == null)
                    ? true:false,

                    TipoAccionPersonalViewModel = new TipoAccionesPersonalViewModel {
                        IdTipoAccionPersonal = accionPersonal.TipoAccionPersonal.IdTipoAccionPersonal,
                    },

                    Estado = accionPersonal.Estado

                };

                if (
                    empleadoMovimiento != null 
                    && modelo.EmpleadoMovimiento.IndiceOcupacionalModalidadPartidaHasta == null
                    )
                {

                    var varIO = await db.IndiceOcupacional
                            .Include(i => i.Dependencia)
                            .Include(i => i.Dependencia.Sucursal)
                            .Include(i => i.ManualPuesto)
                            .Include(i => i.RolPuesto)
                            .Include(i => i.EscalaGrados)
                            .Where(w => w.IdIndiceOcupacional == (int)empleadoMovimiento.IdIndiceOcupacional)
                            .FirstOrDefaultAsync();

                    modelo.EmpleadoMovimiento.IndiceOcupacionalModalidadPartidaHasta =
                        new IndiceOcupacionalModalidadPartida
                        {
                            IdIndiceOcupacional = (int)empleadoMovimiento.IdIndiceOcupacional,
                            IdFondoFinanciamiento = (int)empleadoMovimiento.IdFondoFinanciamiento,
                            IdTipoNombramiento = (int)empleadoMovimiento.IdTipoNombramiento,
                            SalarioReal = empleadoMovimiento.SalarioReal,

                            TipoNombramiento = await db.TipoNombramiento
                            .Include(i => i.RelacionLaboral)
                            .Include(i => i.RelacionLaboral.RegimenLaboral)
                            .Where(w => w.IdTipoNombramiento == (int)empleadoMovimiento.IdTipoNombramiento).FirstOrDefaultAsync(),

                            IndiceOcupacional = varIO,

                            IdDependencia = (int)varIO.IdDependencia,
                            
                        };
                    
                }

                modelo.EmpleadoMovimiento.NumeroPartidaIndividual = modelo.EmpleadoMovimiento.NumeroPartidaIndividual + modelo.EmpleadoMovimiento.CodigoContrato;

                


                return new Response
                {
                    IsSuccess = true,
                    Resultado = modelo
                };


            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }
        }


        /// <summary>
        /// Este m�todo permite visualizar el estado anterior del empleado y el movimiento de personal
        /// que se realiz�
        /// </summary>
        /// <param name="IdAccionPersonal"></param>
        /// <returns></returns>
        // POST: api/AccionesPersonal
        [HttpPost]
        [Route("ObtenerAccionPersonalViewModelParaVisualizar")]
        public async Task<Response> ObtenerAccionPersonalViewModelParaVisualizar([FromBody] int IdAccionPersonal)
        {
            try
            {
                var accionPersonal = await db.AccionPersonal
                    .Include(i => i.TipoAccionPersonal)
                    .Where(w => w.IdAccionPersonal == IdAccionPersonal)
                    .FirstOrDefaultAsync();


                if (accionPersonal == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado
                    };
                }

                

                var indiceOcupacionalModalidadPartida = await db.IndiceOcupacionalModalidadPartida
                    .Include(i=>i.Empleado)
                    
                    .Include(i => i.IndiceOcupacional)
                    .Include(i => i.IndiceOcupacional.Dependencia)
                    .Include(i => i.IndiceOcupacional.Dependencia.Sucursal)

                    .Include(i => i.IndiceOcupacional.EscalaGrados)
                    .Include(i => i.IndiceOcupacional.ManualPuesto)
                    .Include(i => i.TipoNombramiento)
                    .Include(i => i.TipoNombramiento.RelacionLaboral)
                    .Where(w => 
                        w.IdEmpleado == accionPersonal.IdEmpleado
                        && w.Fecha <= accionPersonal.Fecha
                    )
                    .OrderByDescending(o => o.Fecha)
                    .FirstOrDefaultAsync();

                var listaIOMPOcupados = await db.IndiceOcupacionalModalidadPartida
                    .Include(i => i.Empleado)
                    .Include(i => i.Empleado.Persona)
                    .Include(i => i.IndiceOcupacional)
                    .Include(i => i.IndiceOcupacional.Dependencia)
                    .Include(i => i.IndiceOcupacional.Dependencia.Sucursal)
                    .Include(i => i.IndiceOcupacional.EscalaGrados)
                    .Include(i => i.FondoFinanciamiento)
                    .Include(i => i.ModalidadPartida)
                    .Where(w =>
                        w.IdEmpleado != null
                        && w.Empleado.Activo == true
                        && w.Fecha <= accionPersonal.Fecha
                    )
                    .OrderByDescending(o => o.Fecha)
                    .DistinctBy(d => d.IdEmpleado)
                    .ToAsyncEnumerable()
                    .ToList();

                var listaPuestosVacios = await db.IndiceOcupacionalModalidadPartida
                    .Include(i => i.IndiceOcupacional)
                    .Include(i => i.IndiceOcupacional.EscalaGrados)
                    .Include(i => i.IndiceOcupacional.Dependencia)
                    .Include(i => i.IndiceOcupacional.Dependencia.Sucursal)
                    .Include(i => i.ModalidadPartida)
                    .Include(i => i.FondoFinanciamiento)
                    .Where(w =>
                        w.IdEmpleado == null && w.NumeroPartidaIndividual != null
                        && w.Fecha <= accionPersonal.Fecha
                    ).ToListAsync();


                foreach (var item in listaPuestosVacios)
                {
                    listaIOMPOcupados.Add(item);
                }

                var empleado = await db.Empleado
                    .Include(i => i.Persona)
                    .Where(w =>
                        w.IdEmpleado == accionPersonal.IdEmpleado
                        //&& w.Activo == true
                    )
                    .FirstOrDefaultAsync();
                


                if (empleado == null)
                {

                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.EmpleadoNoExiste

                    };
                }



                var empleadoMovimiento = await db.EmpleadoMovimiento
                    .Include(i => i.IndiceOcupacionalModalidadPartidaDesde)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaDesde.TipoNombramiento)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaDesde.TipoNombramiento.RelacionLaboral)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaDesde.TipoNombramiento.RelacionLaboral.RegimenLaboral)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaDesde.IndiceOcupacional)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaDesde.IndiceOcupacional.ManualPuesto)

                    .Include(i => i.IndiceOcupacionalModalidadPartidaHasta)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaHasta.TipoNombramiento)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaHasta.TipoNombramiento.RelacionLaboral)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaHasta.TipoNombramiento.RelacionLaboral.RegimenLaboral)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaHasta.IndiceOcupacional)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaHasta.IndiceOcupacional.Dependencia)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaHasta.IndiceOcupacional.Dependencia.Sucursal)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaHasta.IndiceOcupacional.ManualPuesto)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaHasta.IndiceOcupacional.RolPuesto)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaHasta.IndiceOcupacional.EscalaGrados)
                    .Where(w => w.IdAccionPersonal == accionPersonal.IdAccionPersonal)
                    .FirstOrDefaultAsync();



                var modelo = new AccionPersonalViewModel
                {

                    EmpleadoMovimiento = empleadoMovimiento != null ?
                    empleadoMovimiento
                    :
                    new EmpleadoMovimiento
                    {
                        Empleado = empleado,
                        IndiceOcupacionalModalidadPartidaDesde = indiceOcupacionalModalidadPartida,
                        IdIndiceOcupacionalModalidadPartidaHasta = 0
                    },

                    ListaPuestosOcupados = listaIOMPOcupados,

                    IdAccionPersonal = accionPersonal.IdAccionPersonal,
                    Solicitud = accionPersonal.Solicitud,
                    Explicacion = accionPersonal.Explicacion,

                    NoDias = accionPersonal.NoDias,
                    Numero = accionPersonal.Numero,
                    Fecha = accionPersonal.Fecha,
                    FechaRige = accionPersonal.FechaRige,
                    FechaRigeHasta = accionPersonal.FechaRigeHasta,


                    GeneraMovimientoPersonal = (empleadoMovimiento != null) ? true : false,
                    ConfigurarPuesto = (empleadoMovimiento != null
                    && empleadoMovimiento.IdIndiceOcupacionalModalidadPartidaHasta == null)
                    ? true : false,

                    TipoAccionPersonalViewModel = new TipoAccionesPersonalViewModel
                    {
                        IdTipoAccionPersonal = accionPersonal.TipoAccionPersonal.IdTipoAccionPersonal,
                    },

                    Estado = accionPersonal.Estado

                };

                modelo.EmpleadoMovimiento.Empleado.Dependencia = modelo.EmpleadoMovimiento
                    .IndiceOcupacionalModalidadPartidaDesde.IndiceOcupacional.Dependencia;

                modelo.EmpleadoMovimiento.Empleado.Dependencia.Sucursal = modelo.EmpleadoMovimiento
                    .IndiceOcupacionalModalidadPartidaDesde.IndiceOcupacional.Dependencia.Sucursal;



                if (
                    empleadoMovimiento != null
                    && modelo.EmpleadoMovimiento.IndiceOcupacionalModalidadPartidaHasta == null
                    )
                {

                    var varIO = await db.IndiceOcupacional
                            .Include(i => i.Dependencia)
                            .Include(i => i.Dependencia.Sucursal)
                            .Include(i => i.ManualPuesto)
                            .Include(i => i.RolPuesto)
                            .Include(i => i.EscalaGrados)
                            .Where(w => w.IdIndiceOcupacional == (int)empleadoMovimiento.IdIndiceOcupacional)
                            .FirstOrDefaultAsync();

                    modelo.EmpleadoMovimiento.IndiceOcupacionalModalidadPartidaHasta =
                        new IndiceOcupacionalModalidadPartida
                        {
                            IdIndiceOcupacional = (int)empleadoMovimiento.IdIndiceOcupacional,
                            IdFondoFinanciamiento = (int)empleadoMovimiento.IdFondoFinanciamiento,
                            IdTipoNombramiento = (int)empleadoMovimiento.IdTipoNombramiento,
                            SalarioReal = empleadoMovimiento.SalarioReal,

                            TipoNombramiento = await db.TipoNombramiento
                            .Include(i => i.RelacionLaboral)
                            .Include(i => i.RelacionLaboral.RegimenLaboral)
                            .Where(w => w.IdTipoNombramiento == (int)empleadoMovimiento.IdTipoNombramiento).FirstOrDefaultAsync(),

                            IndiceOcupacional = varIO,

                            IdDependencia = (int)varIO.IdDependencia,

                        };

                }

                modelo.EmpleadoMovimiento.NumeroPartidaIndividual = modelo.EmpleadoMovimiento.NumeroPartidaIndividual + modelo.EmpleadoMovimiento.CodigoContrato;




                return new Response
                {
                    IsSuccess = true,
                    Resultado = modelo
                };


            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }
        }


        /// <summary>
        ///  Lista los estados de aprobaci�n de TTHH
        /// </summary>
        /// <returns></returns>
        // GET: api/AccionesPersonal
        [HttpGet]
        [Route("ListarEstadosAprobacionTTHH")]
        public async Task<List<AprobacionMovimientoInternoViewModel>> ListarEstadosAprobacionTTHH()
        {

            try
            {
                var lista = ConstantesEstadosAprobacionMovimientoInterno
                    .ListaEstadosAprobacionMovimientoInterno
                    .Where(w => w.GrupoAprobacion == 1)
                    .ToList();

                return lista;

            }
            catch (Exception)
            {
                return new List<AprobacionMovimientoInternoViewModel>();
            }
        }


        /// <summary>
        ///  Lista los estados de aprobaci�n del jefe de una dependencia
        /// </summary>
        /// <returns></returns>
        // GET: api/AccionesPersonal
        [HttpGet]
        [Route("ListarEstadosAprobacionAprobador")]
        public async Task<List<AprobacionMovimientoInternoViewModel>> ListarEstadosAprobacion()
        {

            try
            {
                var lista = ConstantesEstadosAprobacionMovimientoInterno.ListaEstadosAprobacionMovimientoInterno.Where(w => w.GrupoAprobacion == 0).ToList();

                return lista;

            }
            catch (Exception)
            {
                return new List<AprobacionMovimientoInternoViewModel>();
            }
        }


        private Response Existe(AccionPersonal AccionPersonal)
        {
            var registro = db.AccionPersonal
                .Where(p =>
                    p.IdEmpleado == AccionPersonal.IdEmpleado
                    && p.Solicitud.ToString().ToUpper().TrimEnd().TrimStart() == AccionPersonal.Solicitud.ToString().ToUpper().TrimEnd().TrimStart()
                && p.FechaRige == AccionPersonal.FechaRige
                && p.IdTipoAccionPersonal == AccionPersonal.IdTipoAccionPersonal
                )
                .FirstOrDefault();

            if (registro != null)
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
                Resultado = registro,
            };
        }


        // POST: api/AccionesPersonal
        [HttpPost]
        [Route("InsertarAccionPersonal")]
        public async Task<Response> InsertarAccionPersonal([FromBody] AccionPersonalViewModel accionPersonalViewModel)
        {


            try
            {

                var modelo = new AccionPersonal
                {
                    IdEmpleado = accionPersonalViewModel.EmpleadoMovimiento.Empleado.IdEmpleado,
                    IdTipoAccionPersonal = accionPersonalViewModel.TipoAccionPersonalViewModel.IdTipoAccionPersonal,
                    Fecha = accionPersonalViewModel.Fecha,
                    NoDias = accionPersonalViewModel.NoDias,
                    Numero = accionPersonalViewModel.Numero,
                    Solicitud = accionPersonalViewModel.Solicitud.ToString().ToUpper(),
                    Explicacion = accionPersonalViewModel.Explicacion.ToString().ToUpper(),
                    FechaRige = accionPersonalViewModel.FechaRige,
                    FechaRigeHasta = accionPersonalViewModel.FechaRigeHasta,
                    Estado = accionPersonalViewModel.Estado
                };


                if (modelo.FechaRigeHasta < modelo.FechaRige)
                {
                    modelo.FechaRigeHasta = modelo.FechaRige;
                }
                
                
                var tipoNombramiento = await db.TipoNombramiento
                    .Include(i=>i.RelacionLaboral)
                    .ToListAsync();

                // 1) OBTENER EL TIPO DE ACCI�N DE PERSONAL
                var tipoAccionPersonal = await db.TipoAccionPersonal
                    .Where(w => w.IdTipoAccionPersonal == accionPersonalViewModel.TipoAccionPersonalViewModel.IdTipoAccionPersonal)
                    .FirstOrDefaultAsync();



                if (tipoAccionPersonal.Definitivo == true)
                {
                    modelo.FechaRigeHasta = null;

                    modelo.NoDias = 0;
                    modelo.Numero = "0";
                }
                else {
                    
                    modelo.NoDias = ((DateTime)modelo.FechaRigeHasta - modelo.FechaRige).Days;
                    
                    
                    if (modelo.NoDias < 0)
                    {
                        modelo.NoDias = 0;
                        modelo.Numero = "0";
                    }
                    else
                    {
                        modelo.Numero = modelo.NoDias + "";
                    }

                }




                // Si el movimiento existe no se registra
                var existe = Existe(modelo);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                        Resultado = accionPersonalViewModel.EmpleadoMovimiento.Empleado.Persona.Identificacion
                    };
                }



                // 2) Interpretaci�n de datos 

                /*
                    modificaDistributivo = genera movimiento de personal (osea debe ser registrado)
                    modalidadContratacion = toma los datos del puesto ( IOMP hasta), si es falso los datos del cambio se toman de los actuales de la persona
                    definitivo = tiempo indefinido                 
                 */


                if (
                    tipoAccionPersonal.ModificaDistributivo == true
                    && tipoAccionPersonal.ModalidadContratacion == true
                    && tipoAccionPersonal.DesactivarEmpleado == false
                )
                {
                    // se registra en MovimientoEmpleado, pero 
                    // Se deben buscar los valores que deben asignarse


                    if (accionPersonalViewModel.ConfigurarPuesto == true)
                    {

                        // tomar datos del modelo de entrada: accionPersonalViewModel.empleadoMovimiento

                        using (var transaction = await db.Database.BeginTransactionAsync())
                        {
                            await db.AccionPersonal.AddAsync(modelo);

                            var modeloEmpleadoMovimiento = new EmpleadoMovimiento
                            {
                                IdEmpleado = modelo.IdEmpleado,
                                FechaDesde = modelo.FechaRige,
                                FechaHasta = modelo.FechaRigeHasta,

                                EsJefe = accionPersonalViewModel.EmpleadoMovimiento.EsJefe,

                                IdIndiceOcupacionalModalidadPartidaDesde =
                                    accionPersonalViewModel.EmpleadoMovimiento
                                    .IndiceOcupacionalModalidadPartidaDesde
                                    .IdIndiceOcupacionalModalidadPartida,

                                IdIndiceOcupacionalModalidadPartidaHasta = null,

                                IdAccionPersonal = modelo.IdAccionPersonal,

                                IdIndiceOcupacional =
                                    accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .IndiceOcupacionalModalidadPartidaHasta
                                    .IdIndiceOcupacional,

                                IdFondoFinanciamiento =
                                    accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .IdFondoFinanciamiento,

                                IdTipoNombramiento =
                                    accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .IdTipoNombramiento,

                                SalarioReal =
                                    accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .SalarioReal

                            };

                            var contratacion = tipoNombramiento
                                .Where(w =>
                                    w.IdTipoNombramiento == modeloEmpleadoMovimiento.IdTipoNombramiento
                                ).FirstOrDefault();


                            if (
                                contratacion.RelacionLaboral.Nombre.ToString().ToUpper() ==
                                ConstantesTipoRelacion.Contrato.ToString().ToUpper()
                                )
                            {

                                modeloEmpleadoMovimiento.CodigoContrato = accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .NumeroPartidaIndividual;

                            }

                            else if (
                                contratacion.RelacionLaboral.Nombre.ToString().ToUpper() ==
                                ConstantesTipoRelacion.Nombramiento.ToString().ToUpper()
                                )
                            {


                                modeloEmpleadoMovimiento.NumeroPartidaIndividual = accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .NumeroPartidaIndividual;

                                modeloEmpleadoMovimiento.IdModalidadPartida = accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .IdModalidadPartida;
                            }
                            else {

                                return new Response {
                                    IsSuccess = false,
                                    Resultado = accionPersonalViewModel.EmpleadoMovimiento.Empleado.Persona.Identificacion,
                                    Message = Mensaje.ErrorSeleccionContratacionNoDefinida
                                };
                            }


                            await db.EmpleadoMovimiento.AddAsync(modeloEmpleadoMovimiento);

                            await db.SaveChangesAsync();

                            transaction.Commit();
                        }


                    }
                    else
                    {
                        // tomar el IdIOMPhasta del modelo de entrada accionPersonalViewModel

                        var iompHasta = await db.IndiceOcupacionalModalidadPartida
                            .Where(w =>
                                w.IdIndiceOcupacionalModalidadPartida ==
                                 accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .IdIndiceOcupacionalModalidadPartidaHasta
                            ).FirstOrDefaultAsync();


                        if (iompHasta.IdTipoNombramiento == null) {

                            var idNombramiento = tipoNombramiento
                                .Where(w =>
                                    w.RelacionLaboral.Nombre.ToString().ToUpper() ==
                                    ConstantesTipoRelacion.Nombramiento.ToString().ToUpper()
                                ).FirstOrDefault().IdTipoNombramiento;

                            if (iompHasta.NumeroPartidaIndividual != null) {

                                iompHasta.IdTipoNombramiento = idNombramiento;

                            }
                        }


                        using (var transaction = await db.Database.BeginTransactionAsync())
                        {

                            await db.AccionPersonal.AddAsync(modelo);

                            var modeloEmpleadoMovimiento = new EmpleadoMovimiento
                            {
                                IdEmpleado = modelo.IdEmpleado,
                                FechaDesde = modelo.FechaRige,
                                FechaHasta = modelo.FechaRigeHasta,
                                
                                IdIndiceOcupacionalModalidadPartidaDesde =
                                    accionPersonalViewModel.EmpleadoMovimiento
                                    .IndiceOcupacionalModalidadPartidaDesde
                                    .IdIndiceOcupacionalModalidadPartida,

                                IdIndiceOcupacionalModalidadPartidaHasta =
                                    accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .IdIndiceOcupacionalModalidadPartidaHasta,

                                IdAccionPersonal = modelo.IdAccionPersonal,

                                IdIndiceOcupacional = iompHasta.IdIndiceOcupacional,
                                IdFondoFinanciamiento = iompHasta.IdFondoFinanciamiento,
                                IdTipoNombramiento = iompHasta.IdTipoNombramiento,
                                SalarioReal = iompHasta.SalarioReal,
                                CodigoContrato = iompHasta.CodigoContrato,
                                NumeroPartidaIndividual = iompHasta.NumeroPartidaIndividual,
                                IdModalidadPartida = iompHasta.IdModalidadPartida
                            };

                            if (iompHasta.IdEmpleado != null)
                            {
                                modeloEmpleadoMovimiento.EsJefe = iompHasta.Empleado.EsJefe;
                            }
                            else
                            {
                                modeloEmpleadoMovimiento.EsJefe = accionPersonalViewModel.EmpleadoMovimiento.EsJefe;
                            }

                            await db.EmpleadoMovimiento.AddAsync(modeloEmpleadoMovimiento);


                            await db.SaveChangesAsync();

                            transaction.Commit();
                        }
                    }


                }

                else if (
                    tipoAccionPersonal.ModificaDistributivo == true
                    && tipoAccionPersonal.ModalidadContratacion == false
                    && tipoAccionPersonal.DesactivarEmpleado == false
                )
                {
                    // se registra en MovimientoEmpleado, cambia el puesto, pero se mantienen los 
                    // datos del iomp actual (fondoFinanciamiento, modalidadPartida,etc)

                    using (var transaction = await db.Database.BeginTransactionAsync())
                    {

                        await db.AccionPersonal.AddAsync(modelo);

                        var iompActual = await db.IndiceOcupacionalModalidadPartida
                            .Where(w =>
                                w.IdIndiceOcupacionalModalidadPartida ==
                                accionPersonalViewModel.EmpleadoMovimiento
                                .IndiceOcupacionalModalidadPartidaDesde
                                .IdIndiceOcupacionalModalidadPartida
                            ).FirstOrDefaultAsync();


                        var modeloEmpleadoMovimiento = new EmpleadoMovimiento
                        {
                            IdEmpleado = modelo.IdEmpleado,
                            FechaDesde = modelo.FechaRige,
                            FechaHasta = modelo.FechaRigeHasta,

                            IdIndiceOcupacionalModalidadPartidaDesde =
                                        accionPersonalViewModel.EmpleadoMovimiento
                                        .IndiceOcupacionalModalidadPartidaDesde
                                        .IdIndiceOcupacionalModalidadPartida,

                            IdAccionPersonal = modelo.IdAccionPersonal,
                            
                        };


                        if (accionPersonalViewModel.ConfigurarPuesto == true)
                        {
                            modeloEmpleadoMovimiento.EsJefe = accionPersonalViewModel
                                .EmpleadoMovimiento
                                .EsJefe;

                            modeloEmpleadoMovimiento.IdIndiceOcupacionalModalidadPartidaHasta = null;

                            modeloEmpleadoMovimiento.IdIndiceOcupacional =
                                accionPersonalViewModel
                                .EmpleadoMovimiento
                                .IndiceOcupacionalModalidadPartidaHasta
                                .IdIndiceOcupacional;

                            modeloEmpleadoMovimiento.IdFondoFinanciamiento =
                                accionPersonalViewModel
                                .EmpleadoMovimiento
                                .IdFondoFinanciamiento;

                            modeloEmpleadoMovimiento.IdTipoNombramiento =
                                accionPersonalViewModel
                                .EmpleadoMovimiento
                                .IdTipoNombramiento;

                            modeloEmpleadoMovimiento.SalarioReal =
                                accionPersonalViewModel
                                .EmpleadoMovimiento
                                .SalarioReal;


                            var contratacion = tipoNombramiento
                                .Where(w => w.IdTipoNombramiento == modeloEmpleadoMovimiento.IdTipoNombramiento)
                                .FirstOrDefault().RelacionLaboral.Nombre;

                            if (
                                contratacion.ToString().ToUpper() ==
                                ConstantesTipoRelacion.Contrato.ToString().ToUpper()
                                )
                            {
                                modeloEmpleadoMovimiento.CodigoContrato =
                                    accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .NumeroPartidaIndividual;
                            }

                            else if (
                                contratacion.ToString().ToUpper() ==
                                ConstantesTipoRelacion.Nombramiento.ToString().ToUpper()
                                )
                            {
                                modeloEmpleadoMovimiento.NumeroPartidaIndividual =
                                    accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .NumeroPartidaIndividual;

                                modeloEmpleadoMovimiento.IdModalidadPartida =
                                    accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .IdModalidadPartida;
                            }



                            await db.EmpleadoMovimiento.AddAsync(modeloEmpleadoMovimiento);

                            await db.SaveChangesAsync();

                            transaction.Commit();



                        }
                        else
                        {

                            var iompHasta = await db.IndiceOcupacionalModalidadPartida
                                .Include(i=>i.Empleado)
                                .Where(w =>
                                    w.IdIndiceOcupacionalModalidadPartida ==
                                    accionPersonalViewModel.EmpleadoMovimiento
                                    .IdIndiceOcupacionalModalidadPartidaHasta
                                ).FirstOrDefaultAsync();

                            if (iompHasta.IdEmpleado != null)
                            {
                                modeloEmpleadoMovimiento.EsJefe = iompHasta.Empleado.EsJefe;
                            }
                            else {
                                modeloEmpleadoMovimiento.EsJefe = accionPersonalViewModel.EmpleadoMovimiento.EsJefe;
                            }

                            modeloEmpleadoMovimiento.IdIndiceOcupacionalModalidadPartidaHasta =
                                iompHasta.IdIndiceOcupacionalModalidadPartida;

                            modeloEmpleadoMovimiento.IdIndiceOcupacional =
                                iompHasta.IdIndiceOcupacional;

                            modeloEmpleadoMovimiento.IdFondoFinanciamiento = iompActual.IdFondoFinanciamiento;

                            modeloEmpleadoMovimiento.IdTipoNombramiento = iompActual.IdTipoNombramiento;

                            modeloEmpleadoMovimiento.SalarioReal = iompActual.SalarioReal;



                            var contratacion = tipoNombramiento
                                .Where(w => w.IdTipoNombramiento == modeloEmpleadoMovimiento.IdTipoNombramiento)
                                .FirstOrDefault().RelacionLaboral.Nombre;

                            if (
                                contratacion.ToString().ToUpper() ==
                                ConstantesTipoRelacion.Contrato.ToString().ToUpper()
                                )
                            {
                                modeloEmpleadoMovimiento.CodigoContrato =
                                    iompActual.CodigoContrato;
                            }

                            else if (
                                contratacion.ToString().ToUpper() ==
                                ConstantesTipoRelacion.Nombramiento.ToString().ToUpper()
                                )
                            {
                                modeloEmpleadoMovimiento.NumeroPartidaIndividual =
                                    iompActual.NumeroPartidaIndividual;

                                modeloEmpleadoMovimiento.IdModalidadPartida =
                                    iompActual.IdModalidadPartida;
                            }


                            await db.EmpleadoMovimiento.AddAsync(modeloEmpleadoMovimiento);

                            await db.SaveChangesAsync();

                            transaction.Commit();

                        }


                    }

                }

                else if (
                    tipoAccionPersonal.ModificaDistributivo == true
                    && tipoAccionPersonal.DesactivarEmpleado == true
                )
                {
                    // Esto es una desvinculaci�n por tanto no se registra en empleado Movimiento
                    
                    await db.AccionPersonal.AddAsync(modelo);
                    await db.SaveChangesAsync();

                }

                else if (
                    tipoAccionPersonal.ModificaDistributivo == false
                )
                {
                    // No se registra en MovimientoEmpleado

                    await db.AccionPersonal.AddAsync(modelo);
                    await db.SaveChangesAsync();
                }

                return new Response {
                    IsSuccess = true,
                    Message = Mensaje.GuardadoSatisfactorio,
                    Resultado = accionPersonalViewModel.EmpleadoMovimiento.Empleado.Persona.Identificacion
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



        // POST: api/AccionesPersonal
        [HttpPost]
        [Route("EditarAccionPersonal")]
        public async Task<Response> EditarAccionPersonal([FromBody] AccionPersonalViewModel accionPersonalViewModel)
        {


            try
            {

                var modelo = await db.AccionPersonal
                    .Where(w=>w.IdAccionPersonal == accionPersonalViewModel.IdAccionPersonal)
                    .FirstOrDefaultAsync();

                modelo.IdEmpleado = accionPersonalViewModel.EmpleadoMovimiento.Empleado.IdEmpleado;
                modelo.IdTipoAccionPersonal = accionPersonalViewModel.TipoAccionPersonalViewModel.IdTipoAccionPersonal;
                modelo.Fecha = accionPersonalViewModel.Fecha;
                modelo.NoDias = accionPersonalViewModel.NoDias;
                modelo.Numero = accionPersonalViewModel.Numero;
                modelo.Solicitud = accionPersonalViewModel.Solicitud.ToString().ToUpper();
                modelo.Explicacion = accionPersonalViewModel.Explicacion.ToString().ToUpper();
                modelo.FechaRige = accionPersonalViewModel.FechaRige;
                modelo.FechaRigeHasta = accionPersonalViewModel.FechaRigeHasta;
                modelo.Estado = accionPersonalViewModel.Estado;
                


                if (modelo.FechaRigeHasta < modelo.FechaRige)
                {
                    modelo.FechaRigeHasta = modelo.FechaRige;
                }


                var tipoNombramiento = await db.TipoNombramiento
                    .Include(i => i.RelacionLaboral)
                    .ToListAsync();

                // 1) OBTENER EL TIPO DE ACCI�N DE PERSONAL
                var tipoAccionPersonal = await db.TipoAccionPersonal
                    .Where(w => w.IdTipoAccionPersonal == accionPersonalViewModel.TipoAccionPersonalViewModel.IdTipoAccionPersonal)
                    .FirstOrDefaultAsync();



                if (tipoAccionPersonal.Definitivo == true)
                {
                    modelo.FechaRigeHasta = null;

                    modelo.NoDias = 0;
                    modelo.Numero = "0";
                }
                else
                {

                    modelo.NoDias = ((DateTime)modelo.FechaRigeHasta - modelo.FechaRige).Days;


                    if (modelo.NoDias < 0)
                    {
                        modelo.NoDias = 0;
                        modelo.Numero = "0";
                    }
                    else
                    {
                        modelo.Numero = modelo.NoDias + "";
                    }

                }




                //// Si el movimiento existe no se registra
                //var existe = Existe(modelo);
                //if (existe.IsSuccess)
                //{
                //    return new Response
                //    {
                //        IsSuccess = false,
                //        Message = Mensaje.ExisteRegistro,
                //        Resultado = accionPersonalViewModel.EmpleadoMovimiento.Empleado.Persona.Identificacion
                //    };
                //}



                // 2) Interpretaci�n de datos 

                /*
                    modificaDistributivo = genera movimiento de personal (osea debe ser registrado)
                    modalidadContratacion = toma los datos del puesto ( IOMP hasta), si es falso los datos del cambio se toman de los actuales de la persona
                    definitivo = tiempo indefinido                 
                 */


                if (
                    tipoAccionPersonal.ModificaDistributivo == true
                    && tipoAccionPersonal.ModalidadContratacion == true
                    && tipoAccionPersonal.DesactivarEmpleado == false
                )
                {
                    // se registra en MovimientoEmpleado, pero 
                    // Se deben buscar los valores que deben asignarse


                    if (accionPersonalViewModel.ConfigurarPuesto == true)
                    {
                        // tomar datos del modelo de entrada: accionPersonalViewModel.empleadoMovimiento

                        using (var transaction = await db.Database.BeginTransactionAsync())
                        {

                            
                            db.AccionPersonal.Update(modelo);



                            var modeloEmpleadoMovimiento = new EmpleadoMovimiento
                            {
                                IdEmpleado = modelo.IdEmpleado,
                                FechaDesde = modelo.FechaRige,
                                FechaHasta = modelo.FechaRigeHasta,

                                EsJefe = accionPersonalViewModel.EmpleadoMovimiento.EsJefe,

                                IdIndiceOcupacionalModalidadPartidaDesde =
                                    accionPersonalViewModel.EmpleadoMovimiento
                                    .IndiceOcupacionalModalidadPartidaDesde
                                    .IdIndiceOcupacionalModalidadPartida,

                                IdIndiceOcupacionalModalidadPartidaHasta = null,

                                IdAccionPersonal = modelo.IdAccionPersonal,

                                IdIndiceOcupacional =
                                    accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .IndiceOcupacionalModalidadPartidaHasta
                                    .IdIndiceOcupacional,

                                IdFondoFinanciamiento =
                                    accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .IdFondoFinanciamiento,

                                IdTipoNombramiento =
                                    accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .IdTipoNombramiento,

                                SalarioReal =
                                    accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .SalarioReal

                            };

                            var contratacion = tipoNombramiento
                                .Where(w =>
                                    w.IdTipoNombramiento == modeloEmpleadoMovimiento.IdTipoNombramiento
                                ).FirstOrDefault();


                            if (
                                contratacion.RelacionLaboral.Nombre.ToString().ToUpper() ==
                                ConstantesTipoRelacion.Contrato.ToString().ToUpper()
                                )
                            {

                                modeloEmpleadoMovimiento.CodigoContrato = accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .NumeroPartidaIndividual;

                                modeloEmpleadoMovimiento.NumeroPartidaIndividual = null;
                                modeloEmpleadoMovimiento.TipoNombramiento = null;

                            }

                            else if (
                                contratacion.RelacionLaboral.Nombre.ToString().ToUpper() ==
                                ConstantesTipoRelacion.Nombramiento.ToString().ToUpper()
                                )
                            {


                                modeloEmpleadoMovimiento.NumeroPartidaIndividual = accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .NumeroPartidaIndividual;

                                modeloEmpleadoMovimiento.IdModalidadPartida = accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .IdModalidadPartida;

                                modeloEmpleadoMovimiento.CodigoContrato = null;
                            }
                            else
                            {

                                return new Response
                                {
                                    IsSuccess = false,
                                    Resultado = accionPersonalViewModel.EmpleadoMovimiento.Empleado.Persona.Identificacion,
                                    Message = Mensaje.ErrorSeleccionContratacionNoDefinida
                                };
                            }


                            var existe = await db.EmpleadoMovimiento
                                .Where(w => w.IdAccionPersonal == modelo.IdAccionPersonal)
                                .FirstOrDefaultAsync();

                            if (existe == null)
                            {
                                await db.EmpleadoMovimiento.AddAsync(modeloEmpleadoMovimiento);
                            }
                            else
                            {

                                existe.IdIndiceOcupacionalModalidadPartidaDesde = modeloEmpleadoMovimiento
                                    .IdIndiceOcupacionalModalidadPartidaDesde;

                                existe.IdIndiceOcupacionalModalidadPartidaHasta = modeloEmpleadoMovimiento
                                    .IdIndiceOcupacionalModalidadPartidaHasta;

                                existe.FechaDesde = modeloEmpleadoMovimiento.FechaDesde;

                                existe.FechaHasta = modeloEmpleadoMovimiento.FechaHasta;

                                existe.IdAccionPersonal = modeloEmpleadoMovimiento.IdAccionPersonal;

                                existe.IdIndiceOcupacional = modeloEmpleadoMovimiento.IdIndiceOcupacional;

                                existe.IdFondoFinanciamiento = modeloEmpleadoMovimiento.IdFondoFinanciamiento;

                                existe.IdTipoNombramiento = modeloEmpleadoMovimiento.IdTipoNombramiento;

                                existe.SalarioReal = modeloEmpleadoMovimiento.SalarioReal;

                                existe.CodigoContrato = modeloEmpleadoMovimiento.CodigoContrato;

                                existe.NumeroPartidaIndividual = modeloEmpleadoMovimiento.NumeroPartidaIndividual;

                                existe.IdModalidadPartida = modeloEmpleadoMovimiento.IdModalidadPartida;

                                existe.EsJefe = modeloEmpleadoMovimiento.EsJefe;

                                db.EmpleadoMovimiento.Update(existe);
                            }

                            

                            await db.SaveChangesAsync();

                            transaction.Commit();
                        }


                    }
                    else
                    {
                        // tomar el IdIOMPhasta del modelo de entrada accionPersonalViewModel

                        var iompHasta = await db.IndiceOcupacionalModalidadPartida
                            .Include(i=>i.Empleado)
                            .Where(w =>
                                w.IdIndiceOcupacionalModalidadPartida ==
                                 accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .IdIndiceOcupacionalModalidadPartidaHasta
                            ).FirstOrDefaultAsync();


                        if (iompHasta.IdTipoNombramiento == null)
                        {

                            var idNombramiento = tipoNombramiento
                                .Where(w =>
                                    w.RelacionLaboral.Nombre.ToString().ToUpper() ==
                                    ConstantesTipoRelacion.Nombramiento.ToString().ToUpper()
                                ).FirstOrDefault().IdTipoNombramiento;

                            if (iompHasta.NumeroPartidaIndividual != null)
                            {

                                iompHasta.IdTipoNombramiento = idNombramiento;

                            }
                        }


                        using (var transaction = await db.Database.BeginTransactionAsync())
                        {

                            db.AccionPersonal.Update(modelo);

                            var modeloEmpleadoMovimiento = new EmpleadoMovimiento
                            {
                                IdEmpleado = modelo.IdEmpleado,
                                FechaDesde = modelo.FechaRige,
                                FechaHasta = modelo.FechaRigeHasta,

                                IdIndiceOcupacionalModalidadPartidaDesde =
                                    accionPersonalViewModel.EmpleadoMovimiento
                                    .IndiceOcupacionalModalidadPartidaDesde
                                    .IdIndiceOcupacionalModalidadPartida,

                                IdIndiceOcupacionalModalidadPartidaHasta =
                                    accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .IdIndiceOcupacionalModalidadPartidaHasta,

                                IdAccionPersonal = modelo.IdAccionPersonal,

                                IdIndiceOcupacional = iompHasta.IdIndiceOcupacional,
                                IdFondoFinanciamiento = iompHasta.IdFondoFinanciamiento,
                                IdTipoNombramiento = iompHasta.IdTipoNombramiento,
                                SalarioReal = iompHasta.SalarioReal,
                                CodigoContrato = iompHasta.CodigoContrato,
                                NumeroPartidaIndividual = iompHasta.NumeroPartidaIndividual,
                                IdModalidadPartida = iompHasta.IdModalidadPartida
                            };

                            if (iompHasta.IdEmpleado != null)
                            {
                                modeloEmpleadoMovimiento.EsJefe = iompHasta.Empleado.EsJefe;
                            }
                            else
                            {
                                modeloEmpleadoMovimiento.EsJefe = accionPersonalViewModel.EmpleadoMovimiento.EsJefe;
                            }


                            var existe = await db.EmpleadoMovimiento
                                .Where(w => w.IdAccionPersonal == modelo.IdAccionPersonal)
                                .FirstOrDefaultAsync();


                            if (existe == null)
                            {
                                await db.EmpleadoMovimiento.AddAsync(modeloEmpleadoMovimiento);
                            }
                            else
                            {

                                existe.IdIndiceOcupacionalModalidadPartidaDesde = modeloEmpleadoMovimiento
                                    .IdIndiceOcupacionalModalidadPartidaDesde;

                                existe.IdIndiceOcupacionalModalidadPartidaHasta = modeloEmpleadoMovimiento
                                    .IdIndiceOcupacionalModalidadPartidaHasta;

                                existe.FechaDesde = modeloEmpleadoMovimiento.FechaDesde;

                                existe.FechaHasta = modeloEmpleadoMovimiento.FechaHasta;

                                existe.IdAccionPersonal = modeloEmpleadoMovimiento.IdAccionPersonal;

                                existe.IdIndiceOcupacional = modeloEmpleadoMovimiento.IdIndiceOcupacional;

                                existe.IdFondoFinanciamiento = modeloEmpleadoMovimiento.IdFondoFinanciamiento;

                                existe.IdTipoNombramiento = modeloEmpleadoMovimiento.IdTipoNombramiento;

                                existe.SalarioReal = modeloEmpleadoMovimiento.SalarioReal;

                                existe.CodigoContrato = modeloEmpleadoMovimiento.CodigoContrato;

                                existe.NumeroPartidaIndividual = modeloEmpleadoMovimiento.NumeroPartidaIndividual;

                                existe.IdModalidadPartida = modeloEmpleadoMovimiento.IdModalidadPartida;

                                existe.EsJefe = modeloEmpleadoMovimiento.EsJefe;

                                db.EmpleadoMovimiento.Update(existe);
                            }



                            await db.SaveChangesAsync();

                            transaction.Commit();
                        }
                    }


                }

                else if (
                    tipoAccionPersonal.ModificaDistributivo == true
                    && tipoAccionPersonal.ModalidadContratacion == false
                    && tipoAccionPersonal.DesactivarEmpleado == false
                )
                {
                    // se registra en MovimientoEmpleado, cambia el puesto, pero se mantienen los 
                    // datos del iomp actual (fondoFinanciamiento, modalidadPartida,etc)

                    using (var transaction = await db.Database.BeginTransactionAsync())
                    {

                        db.AccionPersonal.Update(modelo);

                        var iompActual = await db.IndiceOcupacionalModalidadPartida
                            .Include(i=>i.Empleado)
                            .Where(w =>
                                w.IdIndiceOcupacionalModalidadPartida ==
                                accionPersonalViewModel.EmpleadoMovimiento
                                .IndiceOcupacionalModalidadPartidaDesde
                                .IdIndiceOcupacionalModalidadPartida
                            ).FirstOrDefaultAsync();


                        var modeloEmpleadoMovimiento = new EmpleadoMovimiento
                        {
                            IdEmpleado = modelo.IdEmpleado,
                            FechaDesde = modelo.FechaRige,
                            FechaHasta = modelo.FechaRigeHasta,

                            IdIndiceOcupacionalModalidadPartidaDesde =
                                        accionPersonalViewModel.EmpleadoMovimiento
                                        .IndiceOcupacionalModalidadPartidaDesde
                                        .IdIndiceOcupacionalModalidadPartida,

                            IdAccionPersonal = modelo.IdAccionPersonal


                        };


                        if (accionPersonalViewModel.ConfigurarPuesto == true)
                        {

                            modeloEmpleadoMovimiento.IdIndiceOcupacionalModalidadPartidaHasta =
                                null;

                            modeloEmpleadoMovimiento.IdIndiceOcupacional =
                                accionPersonalViewModel
                                .EmpleadoMovimiento
                                .IndiceOcupacionalModalidadPartidaHasta
                                .IdIndiceOcupacional;

                            modeloEmpleadoMovimiento.IdFondoFinanciamiento =
                                accionPersonalViewModel
                                .EmpleadoMovimiento
                                .IdFondoFinanciamiento;

                            modeloEmpleadoMovimiento.IdTipoNombramiento =
                                accionPersonalViewModel
                                .EmpleadoMovimiento
                                .IdTipoNombramiento;

                            modeloEmpleadoMovimiento.SalarioReal =
                                accionPersonalViewModel
                                .EmpleadoMovimiento
                                .SalarioReal;


                            var contratacion = tipoNombramiento
                                .Where(w => w.IdTipoNombramiento == modeloEmpleadoMovimiento.IdTipoNombramiento)
                                .FirstOrDefault().RelacionLaboral.Nombre;

                            if (
                                contratacion.ToString().ToUpper() ==
                                ConstantesTipoRelacion.Contrato.ToString().ToUpper()
                                )
                            {
                                modeloEmpleadoMovimiento.CodigoContrato =
                                    accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .NumeroPartidaIndividual;
                            }

                            else if (
                                contratacion.ToString().ToUpper() ==
                                ConstantesTipoRelacion.Nombramiento.ToString().ToUpper()
                                )
                            {
                                modeloEmpleadoMovimiento.NumeroPartidaIndividual =
                                    accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .NumeroPartidaIndividual;

                                modeloEmpleadoMovimiento.IdModalidadPartida =
                                    accionPersonalViewModel
                                    .EmpleadoMovimiento
                                    .IdModalidadPartida;
                            }



                            var existe = await db.EmpleadoMovimiento
                                .Where(w => w.IdAccionPersonal == modelo.IdAccionPersonal)
                                .FirstOrDefaultAsync();


                            if (existe == null)
                            {
                                await db.EmpleadoMovimiento.AddAsync(modeloEmpleadoMovimiento);
                            }
                            else
                            {

                                existe.IdIndiceOcupacionalModalidadPartidaDesde = modeloEmpleadoMovimiento
                                    .IdIndiceOcupacionalModalidadPartidaDesde;

                                existe.IdIndiceOcupacionalModalidadPartidaHasta = modeloEmpleadoMovimiento
                                    .IdIndiceOcupacionalModalidadPartidaHasta;

                                existe.FechaDesde = modeloEmpleadoMovimiento.FechaDesde;

                                existe.FechaHasta = modeloEmpleadoMovimiento.FechaHasta;

                                existe.IdAccionPersonal = modeloEmpleadoMovimiento.IdAccionPersonal;

                                existe.IdIndiceOcupacional = modeloEmpleadoMovimiento.IdIndiceOcupacional;

                                existe.IdFondoFinanciamiento = modeloEmpleadoMovimiento.IdFondoFinanciamiento;

                                existe.IdTipoNombramiento = modeloEmpleadoMovimiento.IdTipoNombramiento;

                                existe.SalarioReal = modeloEmpleadoMovimiento.SalarioReal;

                                existe.CodigoContrato = modeloEmpleadoMovimiento.CodigoContrato;

                                existe.NumeroPartidaIndividual = modeloEmpleadoMovimiento.NumeroPartidaIndividual;

                                existe.IdModalidadPartida = modeloEmpleadoMovimiento.IdModalidadPartida;

                                existe.EsJefe = modeloEmpleadoMovimiento.EsJefe;

                                db.EmpleadoMovimiento.Update(existe);
                            }



                            await db.SaveChangesAsync();

                            transaction.Commit();



                        }
                        else
                        {

                            var iompHasta = await db.IndiceOcupacionalModalidadPartida
                                .Where(w =>
                                    w.IdIndiceOcupacionalModalidadPartida ==
                                    accionPersonalViewModel.EmpleadoMovimiento
                                    .IdIndiceOcupacionalModalidadPartidaHasta
                                ).FirstOrDefaultAsync();


                            if (iompHasta.IdEmpleado != null)
                            {
                                modeloEmpleadoMovimiento.EsJefe = iompHasta.Empleado.EsJefe;
                            }
                            else
                            {
                                modeloEmpleadoMovimiento.EsJefe = accionPersonalViewModel.EmpleadoMovimiento.EsJefe;
                            }


                            modeloEmpleadoMovimiento.IdIndiceOcupacionalModalidadPartidaHasta =
                                iompHasta.IdIndiceOcupacionalModalidadPartida;

                            modeloEmpleadoMovimiento.IdIndiceOcupacional =
                                iompHasta.IdIndiceOcupacional;

                            modeloEmpleadoMovimiento.IdFondoFinanciamiento = iompActual.IdFondoFinanciamiento;

                            modeloEmpleadoMovimiento.IdTipoNombramiento = iompActual.IdTipoNombramiento;

                            modeloEmpleadoMovimiento.SalarioReal = iompActual.SalarioReal;



                            var contratacion = tipoNombramiento
                                .Where(w => w.IdTipoNombramiento == modeloEmpleadoMovimiento.IdTipoNombramiento)
                                .FirstOrDefault().RelacionLaboral.Nombre;

                            if (
                                contratacion.ToString().ToUpper() ==
                                ConstantesTipoRelacion.Contrato.ToString().ToUpper()
                                )
                            {
                                modeloEmpleadoMovimiento.CodigoContrato =
                                    iompActual.CodigoContrato;

                                modeloEmpleadoMovimiento.NumeroPartidaIndividual = null;

                                modeloEmpleadoMovimiento.IdModalidadPartida = null;
                            }

                            else if (
                                contratacion.ToString().ToUpper() ==
                                ConstantesTipoRelacion.Nombramiento.ToString().ToUpper()
                                )
                            {
                                modeloEmpleadoMovimiento.CodigoContrato = null;

                                modeloEmpleadoMovimiento.NumeroPartidaIndividual =
                                    iompActual.NumeroPartidaIndividual;

                                modeloEmpleadoMovimiento.IdModalidadPartida =
                                    iompActual.IdModalidadPartida;
                            }


                            var existe = await db.EmpleadoMovimiento
                                   .Where(w => w.IdAccionPersonal == modelo.IdAccionPersonal)
                                   .FirstOrDefaultAsync();

                            if (existe == null)
                            {
                                await db.EmpleadoMovimiento.AddAsync(modeloEmpleadoMovimiento);
                            }
                            else
                            {

                                existe.IdIndiceOcupacionalModalidadPartidaDesde = modeloEmpleadoMovimiento
                                    .IdIndiceOcupacionalModalidadPartidaDesde;

                                existe.IdIndiceOcupacionalModalidadPartidaHasta = modeloEmpleadoMovimiento
                                    .IdIndiceOcupacionalModalidadPartidaHasta;

                                existe.FechaDesde = modeloEmpleadoMovimiento.FechaDesde;

                                existe.FechaHasta = modeloEmpleadoMovimiento.FechaHasta;

                                existe.IdAccionPersonal = modeloEmpleadoMovimiento.IdAccionPersonal;

                                existe.IdIndiceOcupacional = modeloEmpleadoMovimiento.IdIndiceOcupacional;

                                existe.IdFondoFinanciamiento = modeloEmpleadoMovimiento.IdFondoFinanciamiento;

                                existe.IdTipoNombramiento = modeloEmpleadoMovimiento.IdTipoNombramiento;

                                existe.SalarioReal = modeloEmpleadoMovimiento.SalarioReal;

                                existe.CodigoContrato = modeloEmpleadoMovimiento.CodigoContrato;

                                existe.NumeroPartidaIndividual = modeloEmpleadoMovimiento.NumeroPartidaIndividual;

                                existe.IdModalidadPartida = modeloEmpleadoMovimiento.IdModalidadPartida;

                                existe.EsJefe = modeloEmpleadoMovimiento.EsJefe;

                                db.EmpleadoMovimiento.Update(existe);
                            }



                            await db.SaveChangesAsync();

                            transaction.Commit();

                        }


                    }

                }

                else if (
                    tipoAccionPersonal.ModificaDistributivo == true
                    && tipoAccionPersonal.DesactivarEmpleado == true
                )
                {
                    // Esto es una desvinculaci�n por tanto no se registra en empleado Movimiento

                    using (var transaction = await db.Database.BeginTransactionAsync())
                    {
                        db.AccionPersonal.Update(modelo);

                        var existe = await db.EmpleadoMovimiento
                                .Where(w => w.IdAccionPersonal == modelo.IdAccionPersonal)
                                .FirstOrDefaultAsync();


                        if (existe != null)
                        {
                            db.EmpleadoMovimiento.Remove(existe);
                        }

                        

                        await db.SaveChangesAsync();

                        transaction.Commit();

                    }

                }

                else if (
                    tipoAccionPersonal.ModificaDistributivo == false
                )
                {
                    // No se registra en MovimientoEmpleado

                    using (var transaction = await db.Database.BeginTransactionAsync())
                    {
                        db.AccionPersonal.Update(modelo);


                        var existe = await db.EmpleadoMovimiento
                                .Where(w => w.IdAccionPersonal == modelo.IdAccionPersonal)
                                .FirstOrDefaultAsync();


                        if (existe != null)
                        {
                            db.EmpleadoMovimiento.Remove(existe);
                        }

                        
                        await db.SaveChangesAsync();

                        transaction.Commit();
                    }
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.GuardadoSatisfactorio,
                    Resultado = accionPersonalViewModel.EmpleadoMovimiento.Empleado.Persona.Identificacion
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



        // POST: api/AccionesPersonal
        [HttpPost]
        [Route("ListarEmpleadosConAccionPersonal")]
        public async Task<List<AccionPersonalViewModel>> ListarEmpleadosConAccionPersonal([FromBody] AccionesPersonalPorEmpleadoViewModel accionesPersonalPorEmpleadoViewModel)
        {

            try
            {

                var listaDevolver = new List<AccionPersonalViewModel>();


                var empleadoLogueado = await db.IndiceOcupacionalModalidadPartida
                    .Include(i => i.Empleado)
                    .Include(i => i.Empleado.Dependencia)
                    .Include(i => i.IndiceOcupacional)
                    .Include(i => i.IndiceOcupacional.ManualPuesto)
                    .Where(w =>
                        w.Empleado.NombreUsuario == accionesPersonalPorEmpleadoViewModel.NombreUsuarioActual
                        && w.Empleado.Activo == true
                    )
                    .OrderByDescending(o => o.Fecha)
                    .FirstOrDefaultAsync();

                /* 
                 * Obtiene el �ltimo movimiento temporal del empleado logueado que est� vigente en esta fecha
                 * y asi saber si subroga un cargo o est� encargado del puesto
                */
                var empleadoLogueadoPuestoEncargado = await db.EmpleadoMovimiento
                    .Include(i => i.IndiceOcupacional)
                    .Where(w =>
                        w.AccionPersonal.Ejecutado == true
                        && w.FechaDesde <= DateTime.Now
                        && (w.FechaHasta != null && w.FechaHasta >= DateTime.Now)
                    )
                    .OrderByDescending(o => new { o.FechaDesde, o.FechaHasta })
                    .FirstOrDefaultAsync();


                // Este ser� el indice ocupacional actual del empleado logueado
                // tomar� los valores del puesto al que est� subrogando o encargado si fuera el caso
                var IndiceOcupacionalUsuarioActual = new IndiceOcupacional();


                // Esta variable dir� si el empleado logueado es jefe de su dependencia
                var empleadoLogueadoJefe = false;


                if (empleadoLogueadoPuestoEncargado != null)
                {
                    IndiceOcupacionalUsuarioActual = empleadoLogueadoPuestoEncargado
                        .IndiceOcupacional;

                    empleadoLogueadoJefe = empleadoLogueadoPuestoEncargado
                        .EsJefe;
                }
                else
                {
                    IndiceOcupacionalUsuarioActual = empleadoLogueado.IndiceOcupacional;
                    empleadoLogueadoJefe = empleadoLogueado.Empleado.EsJefe;
                }


                var ListaEstados = ConstantesEstadosAprobacionMovimientoInterno.ListaEstadosAprobacionMovimientoInterno;


                var lista = await db.AccionPersonal
                    .Where(w => w.Estado != 2)
                    .Select(s => new AccionPersonalViewModel
                    {
                        EmpleadoMovimiento = new EmpleadoMovimiento
                        {
                            Empleado = new Empleado {
                                IdEmpleado = s.IdEmpleado,
                                Persona = s.Empleado.Persona,
                                Dependencia = s.Empleado.Dependencia
                            }
                        },


                        TipoAccionPersonalViewModel = new TipoAccionesPersonalViewModel
                        {
                            IdTipoAccionPersonal = s.TipoAccionPersonal.IdTipoAccionPersonal,
                            Nombre = s.TipoAccionPersonal.Nombre,
                            NDiasMinimo = s.TipoAccionPersonal.NDiasMinimo,
                            NDiasMaximo = s.TipoAccionPersonal.NDiasMaximo,
                            NHorasMinimo = s.TipoAccionPersonal.NHorasMinimo,
                            NHorasMaximo = s.TipoAccionPersonal.NHorasMaximo,
                            DiasHabiles = s.TipoAccionPersonal.DiasHabiles,
                            ImputableVacaciones = s.TipoAccionPersonal.ImputableVacaciones,
                            ProcesoNomina = s.TipoAccionPersonal.ProcesoNomina,
                            EsResponsableTH = s.TipoAccionPersonal.EsResponsableTH,
                            Matriz = s.TipoAccionPersonal.Matriz,
                            Descripcion = s.TipoAccionPersonal.Descripcion,
                            GeneraAccionPersonal = s.TipoAccionPersonal.GeneraAccionPersonal,
                            ModificaDistributivo = s.TipoAccionPersonal.ModificaDistributivo,
                            MesesMaximo = s.TipoAccionPersonal.MesesMaximo,
                            YearsMaximo = s.TipoAccionPersonal.YearsMaximo,
                            DesactivarCargo = s.TipoAccionPersonal.DesactivarCargo,
                            Definitivo = s.TipoAccionPersonal.Definitivo,
                            DesactivarEmpleado = s.TipoAccionPersonal.DesactivarEmpleado,
                            ModalidadContratacion = s.TipoAccionPersonal.ModalidadContratacion

                        },

                        IdAccionPersonal = s.IdAccionPersonal,
                        Estado = s.Estado,
                        Explicacion = s.Explicacion,
                        Fecha = s.Fecha,
                        FechaRige = s.FechaRige,
                        FechaRigeHasta = s.FechaRigeHasta,
                        NoDias = s.NoDias,
                        Numero = s.Numero,
                        Solicitud = s.Solicitud,
                        Bloquear = s.Bloquear,
                        Ejecutado = s.Ejecutado,

                        EstadoDirector = (ListaEstados.Count > 0)
                            ?
                                ListaEstados.Where(wle =>
                                    //wle.GrupoAprobacion == 0 &&
                                    wle.ValorEstado == s.Estado
                                ).FirstOrDefault().NombreEstado
                            : "",

                        EstadoValidacionTTHH = (ListaEstados.Count > 0)
                            ?
                                ListaEstados.Where(wle =>
                                    wle.GrupoAprobacion == 1
                                    && wle.ValorEstado == s.Estado
                                ).FirstOrDefault().NombreEstado
                            : ""


                    }
                    )
                    .ToListAsync();

                

                foreach (var item in lista)
                {
                    // busca si en el flujo de aprobaci�n, el empleado logueado POR SU PUESTO puede aprobar 
                    // la acci�n de personal
                    var flujoAprobacionPorPuesto = await db.FlujoAprobacion
                        .Where(w =>
                            w.IdTipoAccionPersonal == item.TipoAccionPersonalViewModel.IdTipoAccionPersonal
                            && w.IdManualPuesto == IndiceOcupacionalUsuarioActual.IdManualPuesto
                        )
                        .FirstOrDefaultAsync();


                    // busca si en el flujo de aprobaci�n, el empleado logueado por SER JEFE puede aprobar 
                    // la acci�n de personal
                    var flujoAprobacionJefe = await db.FlujoAprobacion
                        .Where(w =>
                            w.IdTipoAccionPersonal == item.TipoAccionPersonalViewModel.IdTipoAccionPersonal
                            && w.ApruebaJefe == true
                        )
                        .FirstOrDefaultAsync();

                    var permitirAprobacionPorSerJefe = false;


                    if (flujoAprobacionJefe != null)
                    {
                        // TENGO QUE REVISAR SI EL EMPLEADO LOGUEADO ES EL JEFE DEL EMPLEADO QUE HACE LA ACCION DE PERSONAL

                        var datosEmpleadoItem = await db.Empleado
                            .Where(w => w.IdEmpleado == item.EmpleadoMovimiento.Empleado.IdEmpleado)
                            .FirstOrDefaultAsync();

                        if (
                            datosEmpleadoItem != null
                            && datosEmpleadoItem.IdDependencia == IndiceOcupacionalUsuarioActual.IdDependencia
                            && empleadoLogueadoJefe == true
                            )
                        {
                            permitirAprobacionPorSerJefe = true;
                        }

                    }


                    if (flujoAprobacionPorPuesto != null || permitirAprobacionPorSerJefe == true)
                    {

                        // Poner el estado de aprobaci�n para cada persona encargada de aprobar
                        var miFirma = await db.AprobacionAccionPersonal
                            .Where(w =>
                                w.IdAccionPersonal == item.IdAccionPersonal
                                && w.IdEmpleadoAprobador == empleadoLogueado.IdEmpleado
                            )
                            .FirstOrDefaultAsync();
                        

                        if (miFirma != null)
                        {
                            item.Estado = miFirma.Estado;

                        }

                        item.EstadoDirector = (ListaEstados.Count > 0)
                            ?
                                ListaEstados.Where(wle =>
                                    (wle.GrupoAprobacion == 0 || wle.GrupoAprobacion == 2)
                                    && wle.ValorEstado == item.Estado
                                )
                                .FirstOrDefault() != null
                                ? 
                                    ListaEstados.Where(wle =>
                                        (wle.GrupoAprobacion == 0 || wle.GrupoAprobacion == 2)
                                        && wle.ValorEstado == item.Estado
                                     ).FirstOrDefault().NombreEstado
                                : ""
                            : ""
                        ;

                        listaDevolver.Add(item);
                    }


                }



                return listaDevolver;

            }
            catch (Exception ex)
            {

                return new List<AccionPersonalViewModel>();
            }
        }


        // POST: api/AccionesPersonal
        [HttpPost]
        [Route("AprobarAccionPersonal")]
        public async Task<Response> AprobarAccionPersonal([FromBody] AccionPersonalViewModel accionPersonalViewModel)
        {


            try
            {
                
                var accionPersonalActualizar = await db.AccionPersonal
                    .Where(w => 
                        w.IdAccionPersonal == accionPersonalViewModel.IdAccionPersonal
                    )
                    .FirstOrDefaultAsync();
                

                var empleadoLogueado = await db.IndiceOcupacionalModalidadPartida
                    .Include(i => i.Empleado)
                    .Include(i => i.Empleado.Dependencia)
                    .Include(i => i.IndiceOcupacional)
                    .Include(i => i.IndiceOcupacional.ManualPuesto)
                    .Where(w =>
                        w.Empleado.NombreUsuario == accionPersonalViewModel.NombreUsuarioAprobador
                        && w.Empleado.Activo == true
                    )
                    .OrderByDescending(o => o.Fecha)
                    .FirstOrDefaultAsync();


                /* 
                 * Obtiene el �ltimo movimiento temporal del empleado logueado que est� vigente en esta fecha
                 * y asi saber si subroga un cargo o est� encargado del puesto
                */
                var empleadoLogueadoPuestoEncargado = await db.EmpleadoMovimiento
                    .Include(i => i.IndiceOcupacional)
                    .Where(w =>
                        w.AccionPersonal.Ejecutado == true
                        && w.FechaDesde <= DateTime.Now
                        && (w.FechaHasta != null && w.FechaHasta >= DateTime.Now)
                    )
                    .OrderByDescending(o => new { o.FechaDesde, o.FechaHasta })
                    .FirstOrDefaultAsync();


                // Este ser� el indice ocupacional actual del empleado logueado
                // tomar� los valores del puesto al que est� subrogando o encargado si fuera el caso
                var IndiceOcupacionalUsuarioActual = new IndiceOcupacional();


                // Esta variable dir� si el empleado logueado es jefe de su dependencia
                var empleadoLogueadoJefe = false;


                if (empleadoLogueadoPuestoEncargado != null)
                {
                    IndiceOcupacionalUsuarioActual = empleadoLogueadoPuestoEncargado
                        .IndiceOcupacional;

                    empleadoLogueadoJefe = empleadoLogueadoPuestoEncargado
                        .EsJefe;
                }
                else
                {
                    IndiceOcupacionalUsuarioActual = empleadoLogueado.IndiceOcupacional;
                    empleadoLogueadoJefe = empleadoLogueado.Empleado.EsJefe;
                }

                

                if (
                        accionPersonalActualizar != null
                        && accionPersonalActualizar.Bloquear == false
                    )
                {

                    using (var transaction = await db.Database.BeginTransactionAsync())
                    {

                        
                        // busca si en el flujo de aprobaci�n, el empleado logueado POR SU PUESTO puede aprobar 
                        // la acci�n de personal
                        var flujoAprobacionPorPuesto = await db.FlujoAprobacion
                            .Where(w =>
                                w.IdTipoAccionPersonal == accionPersonalActualizar.IdTipoAccionPersonal
                                && w.IdManualPuesto == IndiceOcupacionalUsuarioActual.IdManualPuesto
                            )
                            .FirstOrDefaultAsync();


                        // busca si en el flujo de aprobaci�n, el empleado logueado por SER JEFE puede aprobar 
                        // la acci�n de personal
                        var flujoAprobacionJefe = await db.FlujoAprobacion
                            .Where(w =>
                                w.IdTipoAccionPersonal == accionPersonalActualizar.IdTipoAccionPersonal
                                && w.ApruebaJefe == true
                            )
                            .FirstOrDefaultAsync();

                        
                        if (flujoAprobacionPorPuesto != null) {

                            var modeloAprobacion = new AprobacionAccionPersonal
                            {
                                IdAccionPersonal = accionPersonalActualizar.IdAccionPersonal,
                                IdEmpleadoAprobador = (int)empleadoLogueado.IdEmpleado,
                                IdFlujoAprobacion = 0,
                                Estado = accionPersonalViewModel.Estado,
                                FechaAprobacion = DateTime.Now
                            };

                            modeloAprobacion.IdFlujoAprobacion = flujoAprobacionPorPuesto.IdFlujoAprobacion;

                            var firmado = await db.AprobacionAccionPersonal
                                .Where(w =>
                                    w.IdAccionPersonal == modeloAprobacion.IdAccionPersonal
                                    && w.IdEmpleadoAprobador == modeloAprobacion.IdEmpleadoAprobador
                                    && w.IdFlujoAprobacion == modeloAprobacion.IdFlujoAprobacion
                                ).FirstOrDefaultAsync();


                            // agregar registro si no existe
                            if (firmado == null)
                            {
                                db.AprobacionAccionPersonal.Add(modeloAprobacion);
                                await db.SaveChangesAsync();
                            }
                            else
                            {
                                // Si ya existe el registro, el �nico campo que se actualiza es el estado
                                firmado.Estado = accionPersonalViewModel.Estado;

                                db.AprobacionAccionPersonal.Update(firmado);
                                await db.SaveChangesAsync();
                            }

                        }


                        if (flujoAprobacionJefe != null)
                        {
                            // REVISA SI EL EMPLEADO LOGUEADO ES EL JEFE DEL EMPLEADO QUE HACE LA ACCION DE PERSONAL

                            var datosEmpleadoItem = await db.Empleado
                                .Where(w => w.IdEmpleado == accionPersonalActualizar.IdEmpleado)
                                .FirstOrDefaultAsync();

                            if (
                                datosEmpleadoItem != null
                                && datosEmpleadoItem.IdDependencia == IndiceOcupacionalUsuarioActual.IdDependencia
                                && empleadoLogueadoJefe == true
                                )
                            {
                                var modeloAprobacion2 = new AprobacionAccionPersonal
                                {
                                    IdAccionPersonal = accionPersonalActualizar.IdAccionPersonal,
                                    IdEmpleadoAprobador = (int)empleadoLogueado.IdEmpleado,
                                    IdFlujoAprobacion = 0,
                                    Estado = accionPersonalViewModel.Estado,
                                    FechaAprobacion = DateTime.Now
                                };

                                modeloAprobacion2.IdFlujoAprobacion = flujoAprobacionJefe.IdFlujoAprobacion;

                                var firmado2 = await db.AprobacionAccionPersonal
                                    .Where(w =>
                                        w.IdAccionPersonal == modeloAprobacion2.IdAccionPersonal
                                        && w.IdEmpleadoAprobador == modeloAprobacion2.IdEmpleadoAprobador
                                        && w.IdFlujoAprobacion == modeloAprobacion2.IdFlujoAprobacion
                                    ).FirstOrDefaultAsync();


                                // agregar registro si no existe
                                if (firmado2 == null)
                                {
                                    db.AprobacionAccionPersonal.Add(modeloAprobacion2);
                                    await db.SaveChangesAsync();
                                }
                                else
                                {
                                    // Si ya existe el registro, el �nico campo que se actualiza es el estado
                                    firmado2.Estado = accionPersonalViewModel.Estado;

                                    db.AprobacionAccionPersonal.Update(firmado2);
                                    await db.SaveChangesAsync();
                                }

                            }

                        }
                        
                        


                        // obtener las firmas necesarias para realizar la comparaci�n
                        var firmasTotales = await db.FlujoAprobacion
                            .Where(w =>
                                w.IdTipoAccionPersonal == accionPersonalActualizar.IdTipoAccionPersonal
                            ).ToListAsync();

                        // obtener las firmas que est�n realizadas
                        var firmasRealizadas = await db.AprobacionAccionPersonal
                            .Where(w =>
                                w.IdAccionPersonal == accionPersonalViewModel.IdAccionPersonal
                            ).ToListAsync();

                        // Obtener las firmas que est�n aprobadas
                        var firmasAprobadas = firmasRealizadas.Where(w => w.Estado == 5).ToList();


                        // si todas las firmas necesarias est�n aprobadas, el estado va a aprobado
                        // si todas las firmas se han hecho pero almenos una est� negada el estado va negado
                        if (firmasRealizadas.Count == firmasTotales.Count())
                        {
                            if (firmasAprobadas.Count == firmasTotales.Count())
                            {
                                accionPersonalActualizar.Estado = 5;
                                accionPersonalActualizar.Bloquear = true;
                            }
                            else
                            {
                                accionPersonalActualizar.Estado = 4;
                            }

                        } else if (
                            firmasRealizadas.Count > 0
                            && firmasRealizadas.Count < firmasTotales.Count() 
                        )
                        {
                            accionPersonalActualizar.Estado = 6;
                        }

                        

                        // Cuando todo se ha realizado correctamente y todas las firmas est�n aprobadas
                        // se ejecuta la acci�n de personal
                        if (accionPersonalActualizar.Estado == 5)
                        {

                            await EjecutarAccionesPersonal(accionPersonalActualizar.IdAccionPersonal);
                            accionPersonalActualizar.Ejecutado = true;
                        }

                        // se vuelven a guardar los cambios por el cambio de estado en accionPersonal
                        await db.SaveChangesAsync();

                        transaction.Commit();

                    } // end transaction

                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.GuardadoSatisfactorio,
                    };


                }

                else if (
                        accionPersonalActualizar != null
                        && accionPersonalActualizar.Bloquear == true
                        )
                {
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.AccionPersonalBloqueada,
                    };
                }

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
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


        /*

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarAccionesPersonal")]
        public async Task<List<AccionPersonal>> GetAccionPersonal()
        {
            try
            {
                return await db.AccionPersonal.OrderBy(x => x.IdTipoAccionPersonal).ToListAsync();
            }
            catch (Exception ex)
            {

                return new List<AccionPersonal>();
            }
        }




        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetAccionPersonal([FromRoute] int id)
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

                var AccionPersonal = await db.AccionPersonal.SingleOrDefaultAsync(m => m.IdEmpleado == id);

                if (AccionPersonal == null)
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
                    Resultado = AccionPersonal,
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


        // PUT: api/BasesDatos/5
        [HttpPut("{id}")]
        public async Task<Response> PutAccionPersonal([FromRoute] int id, [FromBody] AccionPersonal accionPersonal)
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

                //var existe = Existe(accionPersonal);
                //if (existe.IsSuccess)
                //{
                //    return new Response
                //    {
                //        IsSuccess = false,
                //        Message = Mensaje.ExisteRegistro,
                //    };
                //}

                var accionPersonalActualizar = await db.AccionPersonal.Where(x => x.IdAccionPersonal == accionPersonal.IdAccionPersonal).FirstOrDefaultAsync();

                if (accionPersonalActualizar != null)
                {
                    try
                    {
                        accionPersonalActualizar.IdEmpleado = accionPersonal.IdEmpleado;
                        accionPersonalActualizar.IdTipoAccionPersonal = accionPersonal.IdTipoAccionPersonal;
                        accionPersonalActualizar.Fecha = accionPersonal.Fecha;
                        accionPersonalActualizar.Numero = accionPersonal.Numero;
                        accionPersonalActualizar.Solicitud = accionPersonal.Solicitud;
                        accionPersonalActualizar.Explicacion = accionPersonal.Explicacion;
                        accionPersonalActualizar.FechaRige = accionPersonal.FechaRige;
                        accionPersonalActualizar.FechaRigeHasta = accionPersonal.FechaRigeHasta;
                        accionPersonalActualizar.NoDias = accionPersonal.NoDias;
                        accionPersonalActualizar.Estado = accionPersonal.Estado;

                        db.AccionPersonal.Update(accionPersonalActualizar);
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




                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ExisteRegistro
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };
            }
        }



        



        // DELETE: api/BasesDatos/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteAccionPersonal([FromRoute] int id)
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

                var respuesta = await db.AccionPersonal.SingleOrDefaultAsync(m => m.IdAccionPersonal == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.AccionPersonal.Remove(respuesta);
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


        


        


        


        


        // POST: api/AccionesPersonal
        /// <summary>
        /// Necesario: IdAccionPersonal
        /// </summary>
        /// <param name="accionPersonalViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ObtenerAccionPersonalViewModel")]
        public async Task<Response> ObtenerAccionPersonalViewModel([FromBody] AccionPersonalViewModel accionPersonalViewModel)
        {
            try
            {

                var empleadoMovimientoRegistro = await db.EmpleadoMovimiento.Where(sw => sw.IdAccionPersonal == accionPersonalViewModel.IdAccionPersonal).FirstOrDefaultAsync();

                var empleadoAprobador = await db.Empleado
                    .Where(w => w.NombreUsuario == accionPersonalViewModel.NombreUsuarioAprobador)
                    .FirstOrDefaultAsync();

                var indicePropuesto = 0;

                if (empleadoMovimientoRegistro != null)
                {
                    indicePropuesto = (int)empleadoMovimientoRegistro.IdIndiceOcupacionalModalidadPartidaHasta;
                }

                var modelo = await db.AccionPersonal
                    .Where(w => w.IdAccionPersonal == accionPersonalViewModel.IdAccionPersonal)
                    .Select(s => new AccionPersonalViewModel
                    {
                        IdAccionPersonal = s.IdAccionPersonal,
                        Solicitud = s.Solicitud,
                        Estado = s.Estado,
                        Explicacion = s.Explicacion,
                        Fecha = (DateTime)s.Fecha,
                        FechaRige = (DateTime)s.FechaRige,
                        FechaRigeHasta = (DateTime)s.FechaRigeHasta,
                        NoDias = (int)s.NoDias,
                        Numero = s.Numero,

                        IdIndiceOcupacionalModalidadPartidaPropuesta = indicePropuesto,

                        GeneraMovimientoPersonal = (empleadoMovimientoRegistro != null) ? true : false,

                        TipoAccionPersonalViewModel = db.TipoAccionPersonal
                           .Where(tapw => tapw.IdTipoAccionPersonal == s.IdTipoAccionPersonal)
                           .Select(st => new TipoAccionesPersonalViewModel
                           {
                               IdTipoAccionPersonal = st.IdTipoAccionPersonal,
                               Nombre = st.Nombre,
                               NDiasMinimo = st.NDiasMinimo,
                               NDiasMaximo = st.NDiasMaximo,
                               NHorasMinimo = st.NHorasMinimo,
                               NHorasMaximo = st.NHorasMaximo,
                               DiasHabiles = st.DiasHabiles,
                               ImputableVacaciones = st.ImputableVacaciones,
                               ProcesoNomina = st.ProcesoNomina,
                               EsResponsableTH = st.EsResponsableTH,
                               Matriz = st.Matriz,
                               Descripcion = st.Descripcion,
                               GeneraAccionPersonal = st.GeneraAccionPersonal,
                               ModificaDistributivo = st.ModificaDistributivo

                           }
                           )
                           .FirstOrDefault(),

                        DatosBasicosEmpleadoViewModel = db.Empleado
                                  .Where(e => e.IdEmpleado == s.IdEmpleado)
                                  .Select(x => new DatosBasicosEmpleadoViewModel
                                  {

                                      FechaNacimiento = x.Persona.FechaNacimiento.Value.Date,
                                      IdSexo = Convert.ToInt32(x.Persona.IdSexo),
                                      IdTipoIdentificacion = Convert.ToInt32(x.Persona.IdTipoIdentificacion),
                                      IdEstadoCivil = Convert.ToInt32(x.Persona.IdEstadoCivil),
                                      IdGenero = Convert.ToInt32(x.Persona.IdGenero),
                                      IdNacionalidad = Convert.ToInt32(x.Persona.IdNacionalidad),
                                      IdTipoSangre = Convert.ToInt32(x.Persona.IdTipoSangre),
                                      IdEtnia = Convert.ToInt32(x.Persona.IdEtnia),
                                      Identificacion = x.Persona.Identificacion,
                                      Nombres = x.Persona.Nombres,
                                      Apellidos = x.Persona.Apellidos,
                                      TelefonoPrivado = x.Persona.TelefonoPrivado,
                                      TelefonoCasa = x.Persona.TelefonoCasa,
                                      CorreoPrivado = x.Persona.CorreoPrivado,
                                      LugarTrabajo = x.Persona.LugarTrabajo,
                                      IdNacionalidadIndigena = x.Persona.IdNacionalidadIndigena,
                                      CallePrincipal = x.Persona.CallePrincipal,
                                      CalleSecundaria = x.Persona.CalleSecundaria,
                                      Referencia = x.Persona.Referencia,
                                      Numero = x.Persona.Numero,
                                      IdParroquia = Convert.ToInt32(x.Persona.IdParroquia),
                                      Ocupacion = x.Persona.Ocupacion,
                                      IdEmpleado = x.IdEmpleado,
                                      IdProvinciaLugarSufragio = Convert.ToInt32(x.IdProvinciaLugarSufragio),
                                      IdPaisLugarNacimiento = x.CiudadNacimiento.Provincia.Pais.IdPais,
                                      IdCiudadLugarNacimiento = x.IdCiudadLugarNacimiento,
                                      IdPaisLugarSufragio = x.ProvinciaSufragio.Pais.IdPais,
                                      IdPaisLugarPersona = x.Persona.Parroquia.Ciudad.Provincia.Pais.IdPais,
                                      IdCiudadLugarPersona = x.Persona.Parroquia.Ciudad.IdCiudad,
                                      IdProvinciaLugarPersona = x.Persona.Parroquia.Ciudad.Provincia.IdProvincia,

                                  }
                                  ).FirstOrDefault()

                    }
                    )
                    .FirstOrDefaultAsync();

                // Poner el estado de aprobaci�n para cada persona encargada de aprobar
                var miFirma = await db.AprobacionAccionPersonal
                    .Where(w =>
                        w.IdAccionPersonal == accionPersonalViewModel.IdAccionPersonal
                        && w.IdEmpleadoAprobador == empleadoAprobador.IdEmpleado
                    )
                    .FirstOrDefaultAsync();

                if (miFirma != null)
                {
                    modelo.Estado = miFirma.Estado;

                }


                return new Response
                {
                    IsSuccess = true,
                    Resultado = modelo
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion + " " + ex
                };
            }
        }



        // POST: api/AccionesPersonal
        [HttpPost]
        [Route("EditarAccionPersonalTTHH")]
        public async Task<Response> EditarAccionPersonalTTHH([FromBody] AccionPersonalViewModel accionPersonalViewModel)
        {
            try
            {

                var empleado = await db.Empleado.Include(ie => ie.Persona)
                    .Where(w => w.IdEmpleado == accionPersonalViewModel.DatosBasicosEmpleadoViewModel.IdEmpleado)
                    .FirstOrDefaultAsync()
                ;



                var tipoAccion = await db.TipoAccionPersonal
                    .Where(w => w.IdTipoAccionPersonal == accionPersonalViewModel.TipoAccionPersonalViewModel.IdTipoAccionPersonal)
                    .FirstOrDefaultAsync();


                if (tipoAccion.Definitivo == true)
                {
                    accionPersonalViewModel.FechaRigeHasta = accionPersonalViewModel.FechaRige;
                    accionPersonalViewModel.NoDias = 0;
                    accionPersonalViewModel.Numero = "0";
                }




                var accionPersonalActualizar = await db.AccionPersonal
                    .Where(w => w.IdAccionPersonal == accionPersonalViewModel.IdAccionPersonal).FirstOrDefaultAsync();

                var coincidencia = await db.AccionPersonal
                    .Where(w =>
                            w.IdEmpleado == accionPersonalViewModel.DatosBasicosEmpleadoViewModel.IdEmpleado
                            && w.Solicitud.ToString().ToUpper().TrimEnd().TrimStart() == accionPersonalViewModel.Solicitud.ToString().ToUpper().TrimEnd().TrimStart()
                            && w.Fecha == accionPersonalViewModel.Fecha
                            && w.IdTipoAccionPersonal == accionPersonalViewModel.TipoAccionPersonalViewModel.IdTipoAccionPersonal
                        )
                    .FirstOrDefaultAsync();

                var modificarMovimiento = await db.EmpleadoMovimiento.Where(w => w.IdAccionPersonal == accionPersonalViewModel.IdAccionPersonal).FirstOrDefaultAsync();


                if (accionPersonalViewModel.GeneraMovimientoPersonal == true)
                {

                    if (modificarMovimiento == null)
                    {
                        using (var transaction = await db.Database.BeginTransactionAsync())
                        {

                            //Modificacion del registro de acci�nPersonal

                            if (
                               coincidencia != null && coincidencia.IdAccionPersonal == accionPersonalViewModel.IdAccionPersonal ||
                               coincidencia == null && accionPersonalActualizar != null
                            )
                            {

                                accionPersonalActualizar.IdEmpleado = accionPersonalViewModel.DatosBasicosEmpleadoViewModel.IdEmpleado;
                                accionPersonalActualizar.IdTipoAccionPersonal = accionPersonalViewModel.TipoAccionPersonalViewModel.IdTipoAccionPersonal;
                                accionPersonalActualizar.Fecha = accionPersonalViewModel.Fecha;
                                accionPersonalActualizar.Numero = accionPersonalViewModel.Numero;
                                accionPersonalActualizar.Solicitud = accionPersonalViewModel.Solicitud;
                                accionPersonalActualizar.Explicacion = accionPersonalViewModel.Explicacion;
                                accionPersonalActualizar.FechaRige = accionPersonalViewModel.FechaRige;
                                accionPersonalActualizar.FechaRigeHasta = accionPersonalViewModel.FechaRigeHasta;
                                accionPersonalActualizar.NoDias = accionPersonalViewModel.NoDias;
                                accionPersonalActualizar.Estado = accionPersonalViewModel.Estado;

                                db.AccionPersonal.Update(accionPersonalActualizar);
                                await db.SaveChangesAsync();

                            }
                            else
                            {

                                return new Response
                                {
                                    IsSuccess = false,
                                    Message = Mensaje.ExisteRegistro,
                                    Resultado = empleado.Persona.Identificacion
                                };
                            }


                            // **** Creaci�n del registro de movimiento de personal ******

                            var modalidadPartidaActual = ObtenerIndice(accionPersonalViewModel.DatosBasicosEmpleadoViewModel.IdEmpleado);

                            // Modelo del movimiento de personal
                            var modeloEmpleadoMovimiento = new EmpleadoMovimiento
                            {
                                IdEmpleado = accionPersonalViewModel.DatosBasicosEmpleadoViewModel.IdEmpleado,
                                FechaDesde = accionPersonalViewModel.FechaRige,
                                FechaHasta = accionPersonalViewModel.FechaRigeHasta,
                                IdIndiceOcupacionalModalidadPartidaDesde = modalidadPartidaActual.IdIndiceOcupacionalModalidadPartida,
                                IdIndiceOcupacionalModalidadPartidaHasta = accionPersonalViewModel.IdIndiceOcupacionalModalidadPartidaPropuesta,
                                IdAccionPersonal = accionPersonalViewModel.IdAccionPersonal

                            };

                            // Guardando registro del movimiento del personal
                            db.EmpleadoMovimiento.Add(modeloEmpleadoMovimiento);
                            await db.SaveChangesAsync();

                            transaction.Commit();

                            return new Response
                            {
                                IsSuccess = true,
                                Message = Mensaje.Satisfactorio,
                                Resultado = empleado.Persona.Identificacion
                            };


                        } // fin transaction

                    }
                    else
                    {
                        using (var transaction = await db.Database.BeginTransactionAsync())
                        {

                            //Modificacion del registro de acci�nPersonal

                            if (
                               coincidencia != null && coincidencia.IdAccionPersonal == accionPersonalViewModel.IdAccionPersonal ||
                               coincidencia == null && accionPersonalActualizar != null
                            )
                            {

                                accionPersonalActualizar.IdEmpleado = accionPersonalViewModel.DatosBasicosEmpleadoViewModel.IdEmpleado;
                                accionPersonalActualizar.IdTipoAccionPersonal = accionPersonalViewModel.TipoAccionPersonalViewModel.IdTipoAccionPersonal;
                                accionPersonalActualizar.Fecha = accionPersonalViewModel.Fecha;
                                accionPersonalActualizar.Numero = accionPersonalViewModel.Numero;
                                accionPersonalActualizar.Solicitud = accionPersonalViewModel.Solicitud;
                                accionPersonalActualizar.Explicacion = accionPersonalViewModel.Explicacion;
                                accionPersonalActualizar.FechaRige = accionPersonalViewModel.FechaRige;
                                accionPersonalActualizar.FechaRigeHasta = accionPersonalViewModel.FechaRigeHasta;
                                accionPersonalActualizar.NoDias = accionPersonalViewModel.NoDias;
                                accionPersonalActualizar.Estado = accionPersonalViewModel.Estado;

                                db.AccionPersonal.Update(accionPersonalActualizar);
                                await db.SaveChangesAsync();

                            }
                            else
                            {

                                return new Response
                                {
                                    IsSuccess = false,
                                    Message = Mensaje.ExisteRegistro,
                                    Resultado = empleado.Persona.Identificacion
                                };
                            }


                            // Modificaci�n del registro de movimiento de personal

                            modificarMovimiento.IdIndiceOcupacionalModalidadPartidaHasta = accionPersonalViewModel.IdIndiceOcupacionalModalidadPartidaPropuesta;

                            modificarMovimiento.FechaDesde = accionPersonalViewModel.FechaRige;
                            modificarMovimiento.FechaHasta = accionPersonalViewModel.FechaRigeHasta;


                            await db.SaveChangesAsync();

                            transaction.Commit();

                            return new Response
                            {
                                IsSuccess = true,
                                Message = Mensaje.Satisfactorio,
                                Resultado = empleado.Persona.Identificacion
                            };


                        } // fin transaction

                    }


                }
                else
                {

                    if (
                       coincidencia != null && coincidencia.IdAccionPersonal == accionPersonalViewModel.IdAccionPersonal ||
                       coincidencia == null && accionPersonalActualizar != null
                   )
                    {

                        accionPersonalActualizar.IdEmpleado = accionPersonalViewModel.DatosBasicosEmpleadoViewModel.IdEmpleado;
                        accionPersonalActualizar.IdTipoAccionPersonal = accionPersonalViewModel.TipoAccionPersonalViewModel.IdTipoAccionPersonal;
                        accionPersonalActualizar.Fecha = accionPersonalViewModel.Fecha;
                        accionPersonalActualizar.Numero = accionPersonalViewModel.Numero;
                        accionPersonalActualizar.Solicitud = accionPersonalViewModel.Solicitud;
                        accionPersonalActualizar.Explicacion = accionPersonalViewModel.Explicacion;
                        accionPersonalActualizar.FechaRige = accionPersonalViewModel.FechaRige;
                        accionPersonalActualizar.FechaRigeHasta = accionPersonalViewModel.FechaRigeHasta;
                        accionPersonalActualizar.NoDias = accionPersonalViewModel.NoDias;
                        accionPersonalActualizar.Estado = accionPersonalViewModel.Estado;

                        db.AccionPersonal.Update(accionPersonalActualizar);
                        await db.SaveChangesAsync();

                        if (modificarMovimiento != null)
                        {

                            db.EmpleadoMovimiento.Remove(modificarMovimiento);
                            await db.SaveChangesAsync();
                        }


                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.Satisfactorio,
                            Resultado = empleado.Persona.Identificacion
                        };

                    }

                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                        Resultado = empleado.Persona.Identificacion
                    };
                }




            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion + " " + ex,
                };
            }
        }

        // POST: api/AccionesPersonal
        [HttpPost]
        [Route("EditarAccionPersonal")]
        public async Task<Response> EditarAccionPersonal([FromBody] AccionPersonal accionPersonal)
        {
            try
            {

                var empleado = await db.Empleado.Include(ie => ie.Persona)
                    .Where(w => w.IdEmpleado == accionPersonal.IdEmpleado)
                    .FirstOrDefaultAsync()
                ;

                var empleadoAprobador = await db.IndiceOcupacionalModalidadPartida
                    .Include(i => i.IndiceOcupacional).ThenInclude(t => t.ManualPuesto)
                    .Where(w => w.Empleado.NombreUsuario == accionPersonal.NombreUsuario)
                    .OrderByDescending(o => o.IdIndiceOcupacionalModalidadPartida)
                    .FirstOrDefaultAsync();



                var accionPersonalActualizar = await db.AccionPersonal
                    .Where(w => w.IdAccionPersonal == accionPersonal.IdAccionPersonal).FirstOrDefaultAsync();



                if (
                        accionPersonalActualizar != null
                        && accionPersonalActualizar.Bloquear == false
                    )
                {

                    using (var transaction = await db.Database.BeginTransactionAsync())
                    {

                        var flujoAprobacion = await db.FlujoAprobacion
                            .Where(w =>
                                w.IdTipoAccionPersonal == accionPersonalActualizar.IdTipoAccionPersonal
                                && w.IdManualPuesto == empleadoAprobador.IndiceOcupacional.IdManualPuesto
                            )
                            .FirstOrDefaultAsync();



                        var aprobacion = new AprobacionAccionPersonal
                        {
                            IdAccionPersonal = accionPersonalActualizar.IdAccionPersonal,
                            IdEmpleadoAprobador = (int)empleadoAprobador.IdEmpleado,
                            IdFlujoAprobacion = flujoAprobacion.IdFlujoAprobacion,
                            Estado = accionPersonal.Estado,
                            FechaAprobacion = DateTime.Now
                        };

                        var firmado = await db.AprobacionAccionPersonal
                            .Where(w =>
                                w.IdAccionPersonal == accionPersonal.IdAccionPersonal
                                && w.IdEmpleadoAprobador == empleadoAprobador.IdEmpleado
                            ).FirstOrDefaultAsync();

                        // agregar registro si no existe
                        if (firmado == null)
                        {
                            db.AprobacionAccionPersonal.Add(aprobacion);
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            // Si ya existe el registro, el �nico campo que se actualiza es el estado
                            firmado.Estado = accionPersonal.Estado;

                            db.AprobacionAccionPersonal.Update(firmado);
                            await db.SaveChangesAsync();
                        }


                        // obtener las firmas necesarias para realizar la comparaci�n
                        var firmasTotales = await db.FlujoAprobacion
                            .Where(w =>
                                w.IdTipoAccionPersonal == accionPersonalActualizar.IdTipoAccionPersonal
                            ).ToListAsync();

                        // obtener las firmas que est�n realizadas
                        var firmasRealizadas = await db.AprobacionAccionPersonal
                            .Where(w =>
                                w.IdAccionPersonal == accionPersonal.IdAccionPersonal
                            ).ToListAsync();

                        // Obtener las firmas que est�n aprobadas
                        var firmasAprobadas = firmasRealizadas.Where(w => w.Estado == 5).ToList();


                        // si todas las firmas necesarias est�n aprobadas, el estado va a aprobado
                        // si todas las firmas se han hecho pero almenos una est� negada el estado va negado
                        if (firmasRealizadas.Count == firmasTotales.Count())
                        {
                            if (firmasAprobadas.Count == firmasTotales.Count())
                            {
                                accionPersonalActualizar.Estado = 5;
                                accionPersonalActualizar.Bloquear = true;
                            }
                            else
                            {
                                accionPersonalActualizar.Estado = 4;
                            }

                        }



                        await db.SaveChangesAsync();


                        // Cuando todo se ha realizado correctamente y todas las firmas est�n aprobadas
                        // se ejecuta la acci�n de personal
                        if (accionPersonalActualizar.Estado == 5)
                        {

                            await EjecutarAccionesPersonal(accionPersonalActualizar.IdAccionPersonal);
                        }


                        transaction.Commit();

                    } // end transaction

                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                        Resultado = empleado.Persona.Identificacion
                    };


                }

                else if (
                        accionPersonalActualizar != null
                        && accionPersonalActualizar.Bloquear == true
                        )
                {
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.AccionPersonalBloqueada,
                        Resultado = empleado.Persona.Identificacion
                    };
                }

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                    Resultado = new AccionesPersonalPorEmpleadoViewModel()
                };


            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion + " " + ex,
                };
            }
        }



        


        private Response CumpleRequerimientos(AccionPersonalViewModel modelo)
        {

            try
            {


                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio
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

        private IndiceOcupacionalModalidadPartida ObtenerIndice(int IdEmpleado)
        {
            try
            {
                var modelo = db.IndiceOcupacionalModalidadPartida.Where(w => w.IdEmpleado == IdEmpleado).FirstOrDefault();

                return modelo;

            }
            catch (Exception ex)
            {
                return new IndiceOcupacionalModalidadPartida();
            }
        }

        //private IndiceOcupacionalModalidadPartida ObtenerIndicePropuesto(int Id)
        //{
        //    try
        //    {
        //        var modelo = db.IndiceOcupacionalModalidadPartida.Where(w => w.IdEmpleado == IdEmpleado).FirstOrDefault();

        //        return modelo;

        //    }
        //    catch (Exception ex)
        //    {
        //        return new IndiceOcupacionalModalidadPartida();
        //    }
        //}

        */


        /// <summary>
        /// Este m�todo se encarga de ejecutar todas las acciones de personal de acuerdo a lo 
        /// que diga la tabla tipoAccionPersonal
        /// </summary>
        /// <param name="idAccionPersonal"></param>
        /// <returns></returns>
        private async Task EjecutarAccionesPersonal(int idAccionPersonal)
        {
            try
            {
                // Estados de cambio del empleado
                /*  
                    0 = n/a                                    desactivarEmpleado = false, modalidadContrataci�n = false
                    1 = Modalidad contrataci�n                 desactivarEmpleado = false, modalidadContrataci�n = true
                    2 = desactivar empleado                    desactivarEmpleado = true, modalidadContrataci�n = false
                    
                */


                var accion = await db.AccionPersonal
                    .Include(i => i.TipoAccionPersonal)
                    .Where(w => w.IdAccionPersonal == idAccionPersonal)
                    .FirstOrDefaultAsync();

                var empleadoAfectado = await db.Empleado
                    .Where(w => w.IdEmpleado == accion.IdEmpleado)
                    .FirstOrDefaultAsync();

                // ModificaDistributivo = se relizan cambios
                if (accion.TipoAccionPersonal.ModificaDistributivo == true)
                {

                    if (accion.TipoAccionPersonal.DesactivarEmpleado == true)
                    {
                        empleadoAfectado.Activo = false;
                    }

                    else if (
                        accion.TipoAccionPersonal.DesactivarEmpleado == false
                    )
                    {
                        var empleadoMovimiento = await db.EmpleadoMovimiento
                            .Include(i=>i.IndiceOcupacional)
                            .Include(i => i.IndiceOcupacional.Dependencia)
                            .Where(w => w.IdAccionPersonal == accion.IdAccionPersonal)
                            .FirstOrDefaultAsync();

                        var modeloIOMP = new IndiceOcupacionalModalidadPartida {

                            IdIndiceOcupacional = (int)empleadoMovimiento.IdIndiceOcupacional,
                            IdEmpleado = (int)empleadoMovimiento.IdEmpleado,
                            IdFondoFinanciamiento = (int) empleadoMovimiento.IdFondoFinanciamiento,
                            IdTipoNombramiento = empleadoMovimiento.IdTipoNombramiento,
                            Fecha = empleadoMovimiento.FechaDesde,
                            SalarioReal = empleadoMovimiento.SalarioReal,
                            CodigoContrato = empleadoMovimiento.CodigoContrato,
                            NumeroPartidaIndividual = empleadoMovimiento.NumeroPartidaIndividual,
                            IdModalidadPartida = empleadoMovimiento.IdModalidadPartida,
                            FechaFin = empleadoMovimiento.FechaHasta

                        };
                        
                        await db.IndiceOcupacionalModalidadPartida.AddAsync(modeloIOMP);

                        var tipoRelacion = await db.TipoNombramiento
                            .Include(i=>i.RelacionLaboral)
                            .Where(w => w.IdTipoNombramiento == modeloIOMP.IdTipoNombramiento)
                            .FirstOrDefaultAsync();

                        empleadoAfectado.IdDependencia = empleadoMovimiento.IndiceOcupacional.IdDependencia;
                        empleadoAfectado.TipoRelacion = tipoRelacion.RelacionLaboral.Nombre;

                        db.Empleado.Update(empleadoAfectado);
                        
                        if (!String.IsNullOrEmpty(modeloIOMP.NumeroPartidaIndividual))
                        {
                            var puestoAnterior = await db.IndiceOcupacionalModalidadPartida
                                .Where(w =>
                                    w.IdIndiceOcupacionalModalidadPartida == empleadoMovimiento.IdIndiceOcupacionalModalidadPartidaDesde
                                ).FirstOrDefaultAsync();

                            var partidaVacante = await db.ModalidadPartida.
                                Where(w => w.Nombre == Constantes.PartidaVacante)
                                .FirstOrDefaultAsync();

                            puestoAnterior.IdModalidadPartida = partidaVacante.IdModalidadPartida;

                            db.IndiceOcupacionalModalidadPartida.Update(puestoAnterior);

                        }

                    }

                    if (accion.TipoAccionPersonal.DesactivarCargo == true) {
                        
                        var iompActual = await db.IndiceOcupacionalModalidadPartida
                            .Where(w => 
                                w.IdEmpleado == empleadoAfectado.IdEmpleado
                                && w.IdModalidadPartida != null
                            )
                            .OrderByDescending(o=>o.Fecha)
                            .FirstOrDefaultAsync();


                        var idModalidadPartidaSuprimida = await db.ModalidadPartida
                            .Where(w => w.Nombre == Constantes.PartidaSuprimida.ToString().ToUpper())
                            .FirstOrDefaultAsync();


                        if (idModalidadPartidaSuprimida == null)
                        {
                            var modeloPartida = new ModalidadPartida {
                                Nombre = Constantes.PartidaSuprimida.ToString().ToUpper()
                            };

                            await db.ModalidadPartida.AddAsync(modeloPartida);

                            idModalidadPartidaSuprimida = modeloPartida;
                        }

                        if (iompActual != null)
                        {
                            iompActual.IdModalidadPartida = idModalidadPartidaSuprimida.IdModalidadPartida;
                            empleadoAfectado.AnoDesvinculacion = accion.FechaRige.Year;
                        }

                    }
                }

                //hay que agregar un campo con estado activo, desactivado en el indiceOcupacional
                //para el movimiento de supresion de partida o poner un estado de partida en indiceOcupacional

                // calcula las vacaciones si es imputable a vacaiones 
                //(en calcular se toma ya en cuenta los movimientos imputables a vacaciones)
                if (accion.TipoAccionPersonal.ImputableVacaciones == true)
                {
                    var ctrl = new SolicitudPlanificacionVacacionesController(db);
                    await ctrl.CalcularYRegistrarVacacionesPorEmpleado(accion.IdEmpleado);
                }

                

            }
            catch (Exception ex)
            {

                throw;
            }


        }


    }
}