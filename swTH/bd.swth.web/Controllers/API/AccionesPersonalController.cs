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
        /// Este método es usado en el timer para actualizar los días restantes en cada movimiento de personal
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
                            && we.Activo == true
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
                    .OrderByDescending(o=>o.IdIndiceOcupacionalModalidadPartida)
                    .FirstOrDefaultAsync();

                var listaIOMPOcupados= await db.IndiceOcupacionalModalidadPartida
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
                    .OrderByDescending(o=>o.IdIndiceOcupacionalModalidadPartida)
                    .DistinctBy(d=>d.IdEmpleado)
                    .ToAsyncEnumerable()
                    .ToList();
                
                

                var modeloListaIOMPOcupados = new List<IndiceOcupacionalModalidadPartida>();

                foreach (var item in listaIOMPOcupados)
                {
                    modeloListaIOMPOcupados.Add(
                        
                        new IndiceOcupacionalModalidadPartida {
                            Empleado = new Empleado {
                                Persona = new Persona {
                                    Nombres = item.Empleado.Persona.Nombres,
                                    Apellidos = item.Empleado.Persona.Apellidos,

                                },
                            },

                            ModalidadPartida = new ModalidadPartida {
                                IdModalidadPartida = (item.ModalidadPartida != null)
                                    ? item.ModalidadPartida.IdModalidadPartida
                                    : 0
                                 ,
                                Nombre = (item.ModalidadPartida != null)
                                    ? item.ModalidadPartida.Nombre
                                    : ""
                                 ,
                            },

                            IndiceOcupacional = new IndiceOcupacional {

                                IdIndiceOcupacional = item.IdIndiceOcupacional,
                                IdManualPuesto = item.IndiceOcupacional.IdManualPuesto,
                                IdDependencia = item.IndiceOcupacional.IdDependencia,
                                IdRolPuesto = item.IndiceOcupacional.IdRolPuesto,

                                Dependencia = new Dependencia {

                                    IdDependencia = item.IndiceOcupacional.Dependencia.IdDependencia,
                                    Nombre = item.IndiceOcupacional.Dependencia.Nombre,

                                    Sucursal = new Sucursal {

                                        IdSucursal = item.IndiceOcupacional.Dependencia.Sucursal.IdSucursal,
                                        Nombre = item.IndiceOcupacional.Dependencia.Sucursal.Nombre,
                                    },
                                },

                                EscalaGrados = new EscalaGrados {
                                    IdEscalaGrados = item.IndiceOcupacional.EscalaGrados.IdEscalaGrados,
                                    Remuneracion = item.IndiceOcupacional.EscalaGrados.Remuneracion,
                                    Nombre = item.IndiceOcupacional.EscalaGrados.Nombre,
                                },
                            },

                            FondoFinanciamiento = new FondoFinanciamiento
                            {
                                IdFondoFinanciamiento = item.FondoFinanciamiento.IdFondoFinanciamiento,
                                Nombre = item.FondoFinanciamiento.Nombre
                            },

                            NumeroPartidaIndividual = item.NumeroPartidaIndividual,
                            CodigoContrato = item.CodigoContrato,
                            SalarioReal = item.SalarioReal,

                            IdIndiceOcupacionalModalidadPartida = item.IdIndiceOcupacionalModalidadPartida,

                        }    
                    );
                }

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

                    var nuevoIOMP = new IndiceOcupacionalModalidadPartida
                    {
                        Empleado = null,

                        ModalidadPartida = new ModalidadPartida
                        {
                            IdModalidadPartida = (item.ModalidadPartida != null)
                                    ? item.ModalidadPartida.IdModalidadPartida
                                    : 0
                                 ,
                            Nombre = (item.ModalidadPartida != null)
                                    ? item.ModalidadPartida.Nombre
                                    : ""
                                 ,
                        },

                        IndiceOcupacional = new IndiceOcupacional
                        {

                            IdIndiceOcupacional = item.IdIndiceOcupacional,
                            IdManualPuesto = item.IndiceOcupacional.IdManualPuesto,
                            IdDependencia = item.IndiceOcupacional.IdDependencia,
                            IdRolPuesto = item.IndiceOcupacional.IdRolPuesto,

                            Dependencia = new Dependencia
                            {

                                IdDependencia = item.IndiceOcupacional.Dependencia.IdDependencia,
                                Nombre = item.IndiceOcupacional.Dependencia.Nombre,

                                Sucursal = new Sucursal
                                {

                                    IdSucursal = item.IndiceOcupacional.Dependencia.Sucursal.IdSucursal,
                                    Nombre = item.IndiceOcupacional.Dependencia.Sucursal.Nombre,
                                },
                            },

                            EscalaGrados = new EscalaGrados
                            {
                                IdEscalaGrados = item.IndiceOcupacional.EscalaGrados.IdEscalaGrados,
                                Remuneracion = item.IndiceOcupacional.EscalaGrados.Remuneracion,
                                Nombre = item.IndiceOcupacional.EscalaGrados.Nombre,
                            },
                        },

                        FondoFinanciamiento = new FondoFinanciamiento
                        {
                            IdFondoFinanciamiento = item.FondoFinanciamiento.IdFondoFinanciamiento,
                            Nombre = item.FondoFinanciamiento.Nombre
                        },

                        NumeroPartidaIndividual = item.NumeroPartidaIndividual,
                        CodigoContrato = item.CodigoContrato,
                        SalarioReal = item.SalarioReal,

                        IdIndiceOcupacionalModalidadPartida = item.IdIndiceOcupacionalModalidadPartida,

                    };

                    var existeEnLista = modeloListaIOMPOcupados
                        .Where(w =>
                            w.NumeroPartidaIndividual == item.NumeroPartidaIndividual
                        ).FirstOrDefault();

                    if (existeEnLista == null) {

                        modeloListaIOMPOcupados.Add(nuevoIOMP);
                    }
                    
                }


                var modelo = new AccionPersonalViewModel {

                    EmpleadoMovimiento = new EmpleadoMovimiento {

                        Empleado = new Empleado {

                            IdEmpleado = empleado.IdEmpleado,

                            Persona = new Persona {
                                Nombres = indiceOcupacionalModalidadPartida.Empleado
                                .Persona.Nombres,

                                Apellidos = indiceOcupacionalModalidadPartida.Empleado
                                .Persona.Apellidos,

                                Identificacion = indiceOcupacionalModalidadPartida.Empleado
                                .Persona.Identificacion,
                            },

                            Dependencia = new Dependencia {

                                IdDependencia = indiceOcupacionalModalidadPartida.Empleado.Dependencia
                                    .IdDependencia,

                                Nombre = indiceOcupacionalModalidadPartida.Empleado.Dependencia
                                    .Nombre,

                                Sucursal = new Sucursal {
                                    IdSucursal = indiceOcupacionalModalidadPartida.Empleado.Dependencia
                                    .Sucursal.IdSucursal,

                                    Nombre = indiceOcupacionalModalidadPartida.Empleado.Dependencia
                                    .Sucursal.Nombre,
                                }
                            },
                            
                        },

                        IndiceOcupacionalModalidadPartidaDesde = new IndiceOcupacionalModalidadPartida {

                            IdIndiceOcupacionalModalidadPartida = indiceOcupacionalModalidadPartida
                             .IdIndiceOcupacionalModalidadPartida,

                            SalarioReal = indiceOcupacionalModalidadPartida.SalarioReal,

                            TipoNombramiento = new TipoNombramiento {

                                IdTipoNombramiento = indiceOcupacionalModalidadPartida
                                 .TipoNombramiento.IdTipoNombramiento,
                                
                                Nombre = indiceOcupacionalModalidadPartida
                                    .TipoNombramiento.Nombre,

                                RelacionLaboral = new RelacionLaboral {

                                    IdRelacionLaboral = indiceOcupacionalModalidadPartida
                                        .TipoNombramiento.RelacionLaboral
                                            .IdRelacionLaboral,

                                    Nombre = indiceOcupacionalModalidadPartida
                                        .TipoNombramiento.RelacionLaboral
                                            .Nombre,

                                    IdRegimenLaboral = indiceOcupacionalModalidadPartida
                                        .TipoNombramiento.RelacionLaboral
                                        .IdRegimenLaboral,

                                }
                            },

                            IndiceOcupacional = new IndiceOcupacional {
                                IdIndiceOcupacional = indiceOcupacionalModalidadPartida
                                    .IndiceOcupacional.IdIndiceOcupacional,

                                IdDependencia = indiceOcupacionalModalidadPartida
                                    .IndiceOcupacional.IdDependencia,

                                IdManualPuesto = indiceOcupacionalModalidadPartida
                                    .IndiceOcupacional.IdManualPuesto,

                                IdRolPuesto = indiceOcupacionalModalidadPartida
                                    .IndiceOcupacional.IdRolPuesto,

                                EscalaGrados = new EscalaGrados {

                                    IdEscalaGrados = indiceOcupacionalModalidadPartida
                                        .IndiceOcupacional.EscalaGrados
                                        .IdEscalaGrados,

                                    Remuneracion = indiceOcupacionalModalidadPartida
                                        .IndiceOcupacional.EscalaGrados
                                        .Remuneracion,
                                },

                                ManualPuesto = new ManualPuesto {

                                    IdManualPuesto = indiceOcupacionalModalidadPartida
                                        .IndiceOcupacional.ManualPuesto
                                        .IdManualPuesto,

                                    Nombre = indiceOcupacionalModalidadPartida
                                        .IndiceOcupacional.ManualPuesto
                                        .Nombre,
                                }
                            },
                        },
                        IdIndiceOcupacionalModalidadPartidaHasta = 0
                    },

                    ListaPuestosOcupados = modeloListaIOMPOcupados,
                    
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


            }
            catch (Exception ex)
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
                    .OrderByDescending(o => o.IdIndiceOcupacionalModalidadPartida)
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
                    .OrderByDescending(o => o.IdIndiceOcupacionalModalidadPartida)
                    .DistinctBy(d => d.IdEmpleado)
                    .ToAsyncEnumerable()
                    .ToList();


                var modeloListaIOMPOcupados = new List<IndiceOcupacionalModalidadPartida>();

                foreach (var item in listaIOMPOcupados)
                {
                    modeloListaIOMPOcupados.Add(

                        new IndiceOcupacionalModalidadPartida
                        {
                            Empleado = new Empleado
                            {
                                Persona = new Persona
                                {
                                    Nombres = item.Empleado.Persona.Nombres,
                                    Apellidos = item.Empleado.Persona.Apellidos,

                                },
                            },

                            ModalidadPartida = new ModalidadPartida
                            {
                                IdModalidadPartida = (item.ModalidadPartida != null)
                                    ? item.ModalidadPartida.IdModalidadPartida
                                    : 0
                                 ,
                                Nombre = (item.ModalidadPartida != null)
                                    ? item.ModalidadPartida.Nombre
                                    : ""
                                 ,
                            },

                            IndiceOcupacional = new IndiceOcupacional
                            {

                                IdIndiceOcupacional = item.IdIndiceOcupacional,
                                IdManualPuesto = item.IndiceOcupacional.IdManualPuesto,
                                IdDependencia = item.IndiceOcupacional.IdDependencia,
                                IdRolPuesto = item.IndiceOcupacional.IdRolPuesto,

                                Dependencia = new Dependencia
                                {

                                    IdDependencia = item.IndiceOcupacional.Dependencia.IdDependencia,
                                    Nombre = item.IndiceOcupacional.Dependencia.Nombre,

                                    Sucursal = new Sucursal
                                    {

                                        IdSucursal = item.IndiceOcupacional.Dependencia.Sucursal.IdSucursal,
                                        Nombre = item.IndiceOcupacional.Dependencia.Sucursal.Nombre,
                                    },
                                },

                                EscalaGrados = new EscalaGrados
                                {
                                    IdEscalaGrados = item.IndiceOcupacional.EscalaGrados.IdEscalaGrados,
                                    Remuneracion = item.IndiceOcupacional.EscalaGrados.Remuneracion,
                                    Nombre = item.IndiceOcupacional.EscalaGrados.Nombre,
                                },
                            },

                            FondoFinanciamiento = new FondoFinanciamiento
                            {
                                IdFondoFinanciamiento = item.FondoFinanciamiento.IdFondoFinanciamiento,
                                Nombre = item.FondoFinanciamiento.Nombre
                            },

                            NumeroPartidaIndividual = item.NumeroPartidaIndividual,
                            CodigoContrato = item.CodigoContrato,
                            SalarioReal = item.SalarioReal,

                            IdIndiceOcupacionalModalidadPartida = item.IdIndiceOcupacionalModalidadPartida,

                        }
                    );
                }


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

                    var nuevoIOMP = new IndiceOcupacionalModalidadPartida
                    {
                        Empleado = null,

                        ModalidadPartida = new ModalidadPartida
                        {
                            IdModalidadPartida = (item.ModalidadPartida != null)
                                    ? item.ModalidadPartida.IdModalidadPartida
                                    : 0
                                 ,
                            Nombre = (item.ModalidadPartida != null)
                                    ? item.ModalidadPartida.Nombre
                                    : ""
                                 ,
                        },

                        IndiceOcupacional = new IndiceOcupacional
                        {

                            IdIndiceOcupacional = item.IdIndiceOcupacional,
                            IdManualPuesto = item.IndiceOcupacional.IdManualPuesto,
                            IdDependencia = item.IndiceOcupacional.IdDependencia,
                            IdRolPuesto = item.IndiceOcupacional.IdRolPuesto,

                            Dependencia = new Dependencia
                            {

                                IdDependencia = item.IndiceOcupacional.Dependencia.IdDependencia,
                                Nombre = item.IndiceOcupacional.Dependencia.Nombre,

                                Sucursal = new Sucursal
                                {

                                    IdSucursal = item.IndiceOcupacional.Dependencia.Sucursal.IdSucursal,
                                    Nombre = item.IndiceOcupacional.Dependencia.Sucursal.Nombre,
                                },
                            },

                            EscalaGrados = new EscalaGrados
                            {
                                IdEscalaGrados = item.IndiceOcupacional.EscalaGrados.IdEscalaGrados,
                                Remuneracion = item.IndiceOcupacional.EscalaGrados.Remuneracion,
                                Nombre = item.IndiceOcupacional.EscalaGrados.Nombre,
                            },
                        },

                        FondoFinanciamiento = new FondoFinanciamiento
                        {
                            IdFondoFinanciamiento = item.FondoFinanciamiento.IdFondoFinanciamiento,
                            Nombre = item.FondoFinanciamiento.Nombre
                        },

                        NumeroPartidaIndividual = item.NumeroPartidaIndividual,
                        CodigoContrato = item.CodigoContrato,
                        SalarioReal = item.SalarioReal,

                        IdIndiceOcupacionalModalidadPartida = item.IdIndiceOcupacionalModalidadPartida,

                    };


                    var existeEnLista = modeloListaIOMPOcupados
                        .Where(w =>
                            w.NumeroPartidaIndividual == item.NumeroPartidaIndividual
                        ).FirstOrDefault();

                    if (existeEnLista == null)
                    {

                        modeloListaIOMPOcupados.Add(nuevoIOMP);
                    }
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
                    new EmpleadoMovimiento {

                        Empleado = new Empleado
                        {

                            IdEmpleado = empleado.IdEmpleado,

                            Persona = new Persona
                            {
                                Nombres = empleado.Persona.Nombres,
                                Apellidos = empleado.Persona.Apellidos,
                                Identificacion = empleado.Persona.Identificacion,
                            },

                            Dependencia = new Dependencia
                            {

                                IdDependencia = empleado.Dependencia.IdDependencia,
                                Nombre = empleado.Dependencia.Nombre,

                                Sucursal = new Sucursal
                                {
                                    IdSucursal = empleado.Dependencia.Sucursal.IdSucursal,
                                    Nombre = empleado.Dependencia.Sucursal.Nombre
                                }
                            }
                        },

                        IndiceOcupacionalModalidadPartidaDesde = new IndiceOcupacionalModalidadPartida
                        {

                            IdIndiceOcupacionalModalidadPartida = indiceOcupacionalModalidadPartida
                             .IdIndiceOcupacionalModalidadPartida,

                            SalarioReal = indiceOcupacionalModalidadPartida.SalarioReal,

                            TipoNombramiento = new TipoNombramiento
                            {

                                IdTipoNombramiento = indiceOcupacionalModalidadPartida
                                 .TipoNombramiento.IdTipoNombramiento,

                                Nombre = indiceOcupacionalModalidadPartida
                                    .TipoNombramiento.Nombre,

                                RelacionLaboral = new RelacionLaboral
                                {

                                    IdRelacionLaboral = indiceOcupacionalModalidadPartida
                                        .TipoNombramiento.RelacionLaboral
                                            .IdRelacionLaboral,

                                    Nombre = indiceOcupacionalModalidadPartida
                                        .TipoNombramiento.RelacionLaboral
                                            .Nombre,

                                    IdRegimenLaboral = indiceOcupacionalModalidadPartida
                                        .TipoNombramiento.RelacionLaboral
                                        .IdRegimenLaboral,

                                }
                            },

                            IndiceOcupacional = new IndiceOcupacional
                            {
                                IdIndiceOcupacional = indiceOcupacionalModalidadPartida
                                    .IndiceOcupacional.IdIndiceOcupacional,

                                IdDependencia = indiceOcupacionalModalidadPartida
                                    .IndiceOcupacional.IdDependencia,

                                IdManualPuesto = indiceOcupacionalModalidadPartida
                                    .IndiceOcupacional.IdManualPuesto,

                                IdRolPuesto = indiceOcupacionalModalidadPartida
                                    .IndiceOcupacional.IdRolPuesto,

                                EscalaGrados = new EscalaGrados
                                {

                                    IdEscalaGrados = indiceOcupacionalModalidadPartida
                                        .IndiceOcupacional.EscalaGrados
                                        .IdEscalaGrados,

                                    Remuneracion = indiceOcupacionalModalidadPartida
                                        .IndiceOcupacional.EscalaGrados
                                        .Remuneracion,
                                },

                                ManualPuesto = new ManualPuesto
                                {

                                    IdManualPuesto = indiceOcupacionalModalidadPartida
                                        .IndiceOcupacional.ManualPuesto
                                        .IdManualPuesto,

                                    Nombre = indiceOcupacionalModalidadPartida
                                        .IndiceOcupacional.ManualPuesto
                                        .Nombre,
                                }
                            },
                        },

                        IndiceOcupacionalModalidadPartidaHasta = null,

                        NumeroPartidaIndividual = empleadoMovimiento.NumeroPartidaIndividual,
                        CodigoContrato = empleadoMovimiento.CodigoContrato,
                        SalarioReal = empleadoMovimiento.SalarioReal,
                        EsJefe = empleadoMovimiento.EsJefe,
                        IdModalidadPartida = empleadoMovimiento.IdModalidadPartida
                    }
                    :
                    new EmpleadoMovimiento
                    {
                        Empleado = new Empleado {

                            IdEmpleado = empleado.IdEmpleado,

                            Persona = new Persona {
                                Nombres = empleado.Persona.Nombres,
                                Apellidos = empleado.Persona.Apellidos,
                                Identificacion = empleado.Persona.Identificacion,
                            },
                            
                            Dependencia = new Dependencia {

                                IdDependencia = empleado.Dependencia.IdDependencia,
                                Nombre = empleado.Dependencia.Nombre,

                                Sucursal = new Sucursal {
                                    IdSucursal = empleado.Dependencia.Sucursal.IdSucursal,
                                    Nombre = empleado.Dependencia.Sucursal.Nombre
                                }
                            }
                        },

                        IndiceOcupacionalModalidadPartidaDesde = new IndiceOcupacionalModalidadPartida
                        {

                            IdIndiceOcupacionalModalidadPartida = indiceOcupacionalModalidadPartida
                             .IdIndiceOcupacionalModalidadPartida,

                            SalarioReal = indiceOcupacionalModalidadPartida.SalarioReal,

                            TipoNombramiento = new TipoNombramiento
                            {

                                IdTipoNombramiento = indiceOcupacionalModalidadPartida
                                 .TipoNombramiento.IdTipoNombramiento,

                                Nombre = indiceOcupacionalModalidadPartida
                                    .TipoNombramiento.Nombre,

                                RelacionLaboral = new RelacionLaboral
                                {

                                    IdRelacionLaboral = indiceOcupacionalModalidadPartida
                                        .TipoNombramiento.RelacionLaboral
                                            .IdRelacionLaboral,

                                    Nombre = indiceOcupacionalModalidadPartida
                                        .TipoNombramiento.RelacionLaboral
                                            .Nombre,

                                    IdRegimenLaboral = indiceOcupacionalModalidadPartida
                                        .TipoNombramiento.RelacionLaboral
                                        .IdRegimenLaboral,

                                }
                            },

                            IndiceOcupacional = new IndiceOcupacional
                            {
                                IdIndiceOcupacional = indiceOcupacionalModalidadPartida
                                    .IndiceOcupacional.IdIndiceOcupacional,

                                IdDependencia = indiceOcupacionalModalidadPartida
                                    .IndiceOcupacional.IdDependencia,

                                IdManualPuesto = indiceOcupacionalModalidadPartida
                                    .IndiceOcupacional.IdManualPuesto,

                                IdRolPuesto = indiceOcupacionalModalidadPartida
                                    .IndiceOcupacional.IdRolPuesto,

                                EscalaGrados = new EscalaGrados
                                {

                                    IdEscalaGrados = indiceOcupacionalModalidadPartida
                                        .IndiceOcupacional.EscalaGrados
                                        .IdEscalaGrados,

                                    Remuneracion = indiceOcupacionalModalidadPartida
                                        .IndiceOcupacional.EscalaGrados
                                        .Remuneracion,
                                },

                                ManualPuesto = new ManualPuesto
                                {

                                    IdManualPuesto = indiceOcupacionalModalidadPartida
                                        .IndiceOcupacional.ManualPuesto
                                        .IdManualPuesto,

                                    Nombre = indiceOcupacionalModalidadPartida
                                        .IndiceOcupacional.ManualPuesto
                                        .Nombre,
                                }
                            },
                        },

                        IdIndiceOcupacionalModalidadPartidaHasta = 0,
                        
                    },

                    ListaPuestosOcupados = modeloListaIOMPOcupados,

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

                    var nombramientoHasta = await db.TipoNombramiento
                            .Include(i => i.RelacionLaboral)
                            .Include(i => i.RelacionLaboral.RegimenLaboral)
                            .Where(w => w.IdTipoNombramiento == (int)empleadoMovimiento.IdTipoNombramiento).FirstOrDefaultAsync();


                    modelo.EmpleadoMovimiento.IndiceOcupacionalModalidadPartidaHasta =
                        new IndiceOcupacionalModalidadPartida
                        {
                            IdIndiceOcupacional = (int)empleadoMovimiento.IdIndiceOcupacional,
                            IdFondoFinanciamiento = (int)empleadoMovimiento.IdFondoFinanciamiento,
                            IdTipoNombramiento = (int)empleadoMovimiento.IdTipoNombramiento,
                            SalarioReal = empleadoMovimiento.SalarioReal,
                            

                            TipoNombramiento = new TipoNombramiento {

                                IdTipoNombramiento = nombramientoHasta.IdTipoNombramiento,
                                Nombre = nombramientoHasta.Nombre,
                                IdRelacionLaboral = nombramientoHasta.IdRelacionLaboral,

                                RelacionLaboral = new RelacionLaboral {
                                    IdRelacionLaboral = nombramientoHasta.RelacionLaboral.IdRelacionLaboral,
                                    Nombre = nombramientoHasta.RelacionLaboral.Nombre,
                                    IdRegimenLaboral = nombramientoHasta.RelacionLaboral.IdRegimenLaboral,

                                    RegimenLaboral = new RegimenLaboral {

                                        IdRegimenLaboral = nombramientoHasta.RelacionLaboral.RegimenLaboral
                                            .IdRegimenLaboral,

                                        Nombre = nombramientoHasta.RelacionLaboral.RegimenLaboral
                                            .Nombre,
                                    },

                                },

                            },

                            IndiceOcupacional = new IndiceOcupacional
                            {

                                Dependencia = new Dependencia
                                {
                                    IdDependencia = varIO.Dependencia.IdDependencia,
                                    Nombre = varIO.Dependencia.Nombre,

                                    Sucursal = new Sucursal
                                    {
                                        IdSucursal = varIO.Dependencia.Sucursal.IdSucursal,
                                        Nombre = varIO.Dependencia.Sucursal.Nombre,
                                        IdCiudad = varIO.Dependencia.Sucursal.IdCiudad
                                    },
                                },

                                ManualPuesto = new ManualPuesto
                                {
                                    IdManualPuesto = varIO.ManualPuesto.IdManualPuesto,
                                    Nombre = varIO.ManualPuesto.Nombre
                                },

                                RolPuesto = new RolPuesto
                                {
                                    IdRolPuesto = varIO.RolPuesto.IdRolPuesto,
                                    Nombre = varIO.RolPuesto.Nombre
                                },

                                EscalaGrados = new EscalaGrados {
                                    IdEscalaGrados = varIO.EscalaGrados.IdEscalaGrados,
                                    Nombre = varIO.EscalaGrados.Nombre,
                                    Remuneracion = varIO.EscalaGrados.Remuneracion,
                                },

                                IdDependencia = empleadoMovimiento.IdIndiceOcupacional,
                                IdRolPuesto = empleadoMovimiento.IndiceOcupacional.IdRolPuesto,
                                IdManualPuesto = empleadoMovimiento.IndiceOcupacional.IdManualPuesto,
                                IdEscalaGrados = empleadoMovimiento.IndiceOcupacional.IdEscalaGrados,

                            },

                            IdDependencia = (int)varIO.IdDependencia,

                            IdIndiceOcupacionalModalidadPartida = empleadoMovimiento
                                .IdIndiceOcupacionalModalidadPartidaHasta != null
                                ? (int) empleadoMovimiento.IdIndiceOcupacionalModalidadPartidaHasta
                                :0
                                ,

                        };

                    modelo.EmpleadoMovimiento.IdIndiceOcupacionalModalidadPartidaHasta =
                        empleadoMovimiento.IdIndiceOcupacionalModalidadPartidaHasta;

                    modelo.EmpleadoMovimiento.IdFondoFinanciamiento = empleadoMovimiento.IdFondoFinanciamiento;

                    
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
        /// Este método permite visualizar el estado anterior del empleado y el movimiento de personal
        /// que se realizó
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

                var empleadoMovimiento = await db.EmpleadoMovimiento
                    .Include(i => i.IndiceOcupacionalModalidadPartidaDesde)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaDesde.TipoNombramiento)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaDesde.TipoNombramiento.RelacionLaboral)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaDesde.TipoNombramiento.RelacionLaboral.RegimenLaboral)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaDesde.IndiceOcupacional)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaDesde.IndiceOcupacional.Dependencia)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaDesde.IndiceOcupacional.Dependencia.Sucursal)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaDesde.IndiceOcupacional.ManualPuesto)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaDesde.IndiceOcupacional.RolPuesto)
                    .Include(i => i.IndiceOcupacionalModalidadPartidaDesde.IndiceOcupacional.EscalaGrados)

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


                /*
                var listaPuestos = await db.IndiceOcupacionalModalidadPartida
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
                        && w.Fecha < accionPersonal.Fecha
                    )
                    .OrderByDescending(o => o.IdIndiceOcupacionalModalidadPartida)
                    .DistinctBy(d => d.IdEmpleado)
                    .ToAsyncEnumerable()
                    .ToList();
                */
                

                var listaIOMPOcupados = new List<IndiceOcupacionalModalidadPartida>();
                var listaPuestosVacios = new List<IndiceOcupacionalModalidadPartida>();


                if (empleadoMovimiento != null)
                {
                    if (empleadoMovimiento.IdIndiceOcupacionalModalidadPartidaHasta != null) {

                        listaIOMPOcupados = await db.IndiceOcupacionalModalidadPartida
                            .Include(i => i.Empleado)
                            .Include(i => i.Empleado.Persona)
                            .Include(i => i.IndiceOcupacional)
                            .Include(i => i.IndiceOcupacional.Dependencia)
                            .Include(i => i.IndiceOcupacional.Dependencia.Sucursal)
                            .Include(i => i.IndiceOcupacional.EscalaGrados)
                            .Include(i => i.FondoFinanciamiento)
                            .Include(i => i.ModalidadPartida)
                            .Where(w =>
                                w.IdIndiceOcupacionalModalidadPartida ==
                                empleadoMovimiento.IdIndiceOcupacionalModalidadPartidaHasta
                                && w.Empleado != null
                            ).ToListAsync();


                        listaPuestosVacios = await db.IndiceOcupacionalModalidadPartida
                            .Include(i => i.IndiceOcupacional)
                            .Include(i => i.IndiceOcupacional.EscalaGrados)
                            .Include(i => i.IndiceOcupacional.Dependencia)
                            .Include(i => i.IndiceOcupacional.Dependencia.Sucursal)
                            .Include(i => i.ModalidadPartida)
                            .Include(i => i.FondoFinanciamiento)
                            .Where(w =>
                                w.IdEmpleado == null && w.NumeroPartidaIndividual != null
                                && w.IdIndiceOcupacionalModalidadPartida ==
                                empleadoMovimiento.IdIndiceOcupacionalModalidadPartidaHasta
                            ).ToListAsync();

                    }

                }
                



                var modeloListaIOMPOcupados = new List<IndiceOcupacionalModalidadPartida>();

                foreach (var item in listaIOMPOcupados)
                {
                    modeloListaIOMPOcupados.Add(

                        new IndiceOcupacionalModalidadPartida
                        {
                            Empleado = new Empleado
                            {
                                Persona = new Persona
                                {
                                    Nombres = item.Empleado.Persona.Nombres,
                                    Apellidos = item.Empleado.Persona.Apellidos,

                                },
                            },
                            
                            ModalidadPartida = new ModalidadPartida
                            {
                                IdModalidadPartida = (item.ModalidadPartida != null)
                                    ? item.ModalidadPartida.IdModalidadPartida
                                    : 0
                                 ,
                                Nombre = (item.ModalidadPartida != null)
                                    ? item.ModalidadPartida.Nombre
                                    : ""
                                 ,
                            },

                            IndiceOcupacional = new IndiceOcupacional
                            {

                                IdIndiceOcupacional = item.IdIndiceOcupacional,
                                IdManualPuesto = item.IndiceOcupacional.IdManualPuesto,
                                IdDependencia = item.IndiceOcupacional.IdDependencia,
                                IdRolPuesto = item.IndiceOcupacional.IdRolPuesto,

                                Dependencia = new Dependencia
                                {

                                    IdDependencia = item.IndiceOcupacional.Dependencia.IdDependencia,
                                    Nombre = item.IndiceOcupacional.Dependencia.Nombre,

                                    Sucursal = new Sucursal
                                    {

                                        IdSucursal = item.IndiceOcupacional.Dependencia.Sucursal.IdSucursal,
                                        Nombre = item.IndiceOcupacional.Dependencia.Sucursal.Nombre,
                                    },
                                },

                                EscalaGrados = new EscalaGrados
                                {
                                    IdEscalaGrados = item.IndiceOcupacional.EscalaGrados.IdEscalaGrados,
                                    Remuneracion = item.IndiceOcupacional.EscalaGrados.Remuneracion,
                                    Nombre = item.IndiceOcupacional.EscalaGrados.Nombre,
                                },
                            },

                            FondoFinanciamiento = new FondoFinanciamiento
                            {
                                IdFondoFinanciamiento = item.FondoFinanciamiento.IdFondoFinanciamiento,
                                Nombre = item.FondoFinanciamiento.Nombre
                            },

                            NumeroPartidaIndividual = item.NumeroPartidaIndividual,
                            CodigoContrato = item.CodigoContrato,
                            SalarioReal = item.SalarioReal,
                            
                            IdIndiceOcupacionalModalidadPartida = item.IdIndiceOcupacionalModalidadPartida,
                            
                        }
                    );
                }

                
                foreach (var item in listaPuestosVacios)
                {

                    var nuevoIOMP = new IndiceOcupacionalModalidadPartida
                    {
                        Empleado = null,

                        ModalidadPartida = new ModalidadPartida
                        {
                            IdModalidadPartida = (item.ModalidadPartida != null)
                                    ? item.ModalidadPartida.IdModalidadPartida
                                    : 0
                                 ,
                            Nombre = (item.ModalidadPartida != null)
                                    ? item.ModalidadPartida.Nombre
                                    : ""
                                 ,
                        },

                        IndiceOcupacional = new IndiceOcupacional
                        {

                            IdIndiceOcupacional = item.IdIndiceOcupacional,
                            IdManualPuesto = item.IndiceOcupacional.IdManualPuesto,
                            IdDependencia = item.IndiceOcupacional.IdDependencia,
                            IdRolPuesto = item.IndiceOcupacional.IdRolPuesto,

                            Dependencia = new Dependencia
                            {

                                IdDependencia = item.IndiceOcupacional.Dependencia.IdDependencia,
                                Nombre = item.IndiceOcupacional.Dependencia.Nombre,

                                Sucursal = new Sucursal
                                {

                                    IdSucursal = item.IndiceOcupacional.Dependencia.Sucursal.IdSucursal,
                                    Nombre = item.IndiceOcupacional.Dependencia.Sucursal.Nombre,
                                },
                            },

                            EscalaGrados = new EscalaGrados
                            {
                                IdEscalaGrados = item.IndiceOcupacional.EscalaGrados.IdEscalaGrados,
                                Remuneracion = item.IndiceOcupacional.EscalaGrados.Remuneracion,
                                Nombre = item.IndiceOcupacional.EscalaGrados.Nombre,
                            },
                        },

                        FondoFinanciamiento = new FondoFinanciamiento
                        {
                            IdFondoFinanciamiento = item.FondoFinanciamiento.IdFondoFinanciamiento,
                            Nombre = item.FondoFinanciamiento.Nombre
                        },

                        NumeroPartidaIndividual = item.NumeroPartidaIndividual,
                        CodigoContrato = item.CodigoContrato,
                        SalarioReal = item.SalarioReal,

                        IdIndiceOcupacionalModalidadPartida = item.IdIndiceOcupacionalModalidadPartida,

                    };


                    var existeEnLista = modeloListaIOMPOcupados
                        .Where(w =>
                            w.NumeroPartidaIndividual == item.NumeroPartidaIndividual
                        ).FirstOrDefault();

                    if (existeEnLista == null)
                    {

                        modeloListaIOMPOcupados.Add(nuevoIOMP);
                    }
                }
                

                var modelo = new AccionPersonalViewModel
                {

                    EmpleadoMovimiento = empleadoMovimiento != null ?
                    new EmpleadoMovimiento
                    {

                        Empleado = new Empleado
                        {

                            IdEmpleado = empleado.IdEmpleado,

                            Persona = new Persona
                            {
                                Nombres = empleado.Persona.Nombres,
                                Apellidos = empleado.Persona.Apellidos,
                                Identificacion = empleado.Persona.Identificacion,
                            },

                            Dependencia = new Dependencia
                            {

                                IdDependencia = empleadoMovimiento
                                .IndiceOcupacionalModalidadPartidaDesde
                                .IndiceOcupacional.Dependencia
                                .IdDependencia,

                                Nombre = empleadoMovimiento
                                .IndiceOcupacionalModalidadPartidaDesde
                                .IndiceOcupacional.Dependencia
                                .Nombre,

                                Sucursal = new Sucursal
                                {
                                    IdSucursal = empleadoMovimiento
                                        .IndiceOcupacionalModalidadPartidaDesde
                                        .IndiceOcupacional.Dependencia
                                        .Sucursal.IdSucursal,

                                    Nombre = empleadoMovimiento
                                        .IndiceOcupacionalModalidadPartidaDesde
                                        .IndiceOcupacional.Dependencia
                                        .Sucursal.Nombre,
                                }
                            }
                        },

                        IndiceOcupacionalModalidadPartidaDesde = new IndiceOcupacionalModalidadPartida
                        {

                            IdIndiceOcupacionalModalidadPartida = empleadoMovimiento
                             .IndiceOcupacionalModalidadPartidaDesde
                             .IdIndiceOcupacionalModalidadPartida,

                            SalarioReal = empleadoMovimiento
                                .IndiceOcupacionalModalidadPartidaDesde
                                .SalarioReal,

                            TipoNombramiento = new TipoNombramiento
                            {

                                IdTipoNombramiento = empleadoMovimiento
                                .IndiceOcupacionalModalidadPartidaDesde
                                 .TipoNombramiento.IdTipoNombramiento,

                                Nombre = empleadoMovimiento
                                    .IndiceOcupacionalModalidadPartidaDesde
                                    .TipoNombramiento.Nombre,

                                RelacionLaboral = new RelacionLaboral
                                {

                                    IdRelacionLaboral = empleadoMovimiento
                                    .IndiceOcupacionalModalidadPartidaDesde
                                        .TipoNombramiento.RelacionLaboral
                                            .IdRelacionLaboral,

                                    Nombre = empleadoMovimiento
                                .IndiceOcupacionalModalidadPartidaDesde
                                        .TipoNombramiento.RelacionLaboral
                                            .Nombre,

                                    IdRegimenLaboral = empleadoMovimiento
                                .IndiceOcupacionalModalidadPartidaDesde
                                        .TipoNombramiento.RelacionLaboral
                                        .IdRegimenLaboral,

                                }
                            },

                            IndiceOcupacional = new IndiceOcupacional
                            {
                                IdIndiceOcupacional = empleadoMovimiento
                                .IndiceOcupacionalModalidadPartidaDesde
                                    .IndiceOcupacional.IdIndiceOcupacional,

                                IdDependencia = empleadoMovimiento
                                .IndiceOcupacionalModalidadPartidaDesde
                                    .IndiceOcupacional.IdDependencia,

                                IdManualPuesto = empleadoMovimiento
                                .IndiceOcupacionalModalidadPartidaDesde
                                    .IndiceOcupacional.IdManualPuesto,

                                IdRolPuesto = empleadoMovimiento
                                .IndiceOcupacionalModalidadPartidaDesde
                                    .IndiceOcupacional.IdRolPuesto,

                                EscalaGrados = new EscalaGrados
                                {

                                    IdEscalaGrados = empleadoMovimiento
                                .IndiceOcupacionalModalidadPartidaDesde
                                        .IndiceOcupacional.EscalaGrados
                                        .IdEscalaGrados,

                                    Remuneracion = empleadoMovimiento
                                .IndiceOcupacionalModalidadPartidaDesde
                                        .IndiceOcupacional.EscalaGrados
                                        .Remuneracion,
                                },

                                ManualPuesto = new ManualPuesto
                                {

                                    IdManualPuesto = empleadoMovimiento
                                .IndiceOcupacionalModalidadPartidaDesde
                                        .IndiceOcupacional.ManualPuesto
                                        .IdManualPuesto,

                                    Nombre = empleadoMovimiento
                                .IndiceOcupacionalModalidadPartidaDesde
                                        .IndiceOcupacional.ManualPuesto
                                        .Nombre,
                                }
                            },
                        },

                        IndiceOcupacionalModalidadPartidaHasta = null,

                        NumeroPartidaIndividual = empleadoMovimiento.NumeroPartidaIndividual,
                        CodigoContrato = empleadoMovimiento.CodigoContrato,
                        SalarioReal = empleadoMovimiento.SalarioReal,
                        EsJefe = empleadoMovimiento.EsJefe,
                        IdModalidadPartida = empleadoMovimiento.IdModalidadPartida
                    }
                    :
                    new EmpleadoMovimiento
                    {
                        Empleado = new Empleado
                        {

                            IdEmpleado = empleado.IdEmpleado,

                            Persona = new Persona
                            {
                                Nombres = empleado.Persona.Nombres,
                                Apellidos = empleado.Persona.Apellidos,
                                Identificacion = empleado.Persona.Identificacion,
                            },

                            Dependencia = new Dependencia
                            {

                                IdDependencia = empleadoMovimiento
                                .IndiceOcupacionalModalidadPartidaDesde
                                .IndiceOcupacional.Dependencia
                                .IdDependencia,

                                Nombre = empleadoMovimiento
                                .IndiceOcupacionalModalidadPartidaDesde
                                .IndiceOcupacional.Dependencia
                                .Nombre,

                                Sucursal = new Sucursal
                                {
                                    IdSucursal = empleadoMovimiento
                                        .IndiceOcupacionalModalidadPartidaDesde
                                        .IndiceOcupacional.Dependencia
                                        .Sucursal.IdSucursal,

                                    Nombre = empleadoMovimiento
                                        .IndiceOcupacionalModalidadPartidaDesde
                                        .IndiceOcupacional.Dependencia
                                        .Sucursal.Nombre,
                                }
                            }
                        },

                        IndiceOcupacionalModalidadPartidaDesde = new IndiceOcupacionalModalidadPartida
                        {

                            IdIndiceOcupacionalModalidadPartida = empleadoMovimiento
                                .IndiceOcupacionalModalidadPartidaDesde
                                .IdIndiceOcupacionalModalidadPartida,

                            SalarioReal = empleadoMovimiento
                                .IndiceOcupacionalModalidadPartidaDesde
                                .SalarioReal,

                            TipoNombramiento = new TipoNombramiento
                            {

                                IdTipoNombramiento = empleadoMovimiento
                                    .IndiceOcupacionalModalidadPartidaDesde
                                    .TipoNombramiento.IdTipoNombramiento,

                                Nombre = empleadoMovimiento
                                    .IndiceOcupacionalModalidadPartidaDesde
                                    .TipoNombramiento.Nombre,

                                RelacionLaboral = new RelacionLaboral
                                {

                                    IdRelacionLaboral = empleadoMovimiento
                                        .IndiceOcupacionalModalidadPartidaDesde
                                        .TipoNombramiento.RelacionLaboral
                                            .IdRelacionLaboral,

                                    Nombre = empleadoMovimiento
                                        .IndiceOcupacionalModalidadPartidaDesde
                                        .TipoNombramiento.RelacionLaboral
                                            .Nombre,

                                    IdRegimenLaboral = empleadoMovimiento
                                        .IndiceOcupacionalModalidadPartidaDesde
                                        .TipoNombramiento.RelacionLaboral
                                        .IdRegimenLaboral,

                                }
                            },

                            IndiceOcupacional = new IndiceOcupacional
                            {
                                IdIndiceOcupacional = empleadoMovimiento
                                    .IndiceOcupacionalModalidadPartidaDesde
                                    .IndiceOcupacional.IdIndiceOcupacional,

                                IdDependencia = empleadoMovimiento
                                    .IndiceOcupacionalModalidadPartidaDesde
                                    .IndiceOcupacional.IdDependencia,

                                IdManualPuesto = empleadoMovimiento
                                    .IndiceOcupacionalModalidadPartidaDesde
                                    .IndiceOcupacional.IdManualPuesto,

                                IdRolPuesto = empleadoMovimiento
                                    .IndiceOcupacionalModalidadPartidaDesde
                                    .IndiceOcupacional.IdRolPuesto,

                                EscalaGrados = new EscalaGrados
                                {

                                    IdEscalaGrados = empleadoMovimiento
                                    .IndiceOcupacionalModalidadPartidaDesde
                                        .IndiceOcupacional.EscalaGrados
                                        .IdEscalaGrados,

                                    Remuneracion = empleadoMovimiento
                                        .IndiceOcupacionalModalidadPartidaDesde
                                        .IndiceOcupacional.EscalaGrados
                                        .Remuneracion,
                                },

                                ManualPuesto = new ManualPuesto
                                {

                                    IdManualPuesto = empleadoMovimiento
                                    .IndiceOcupacionalModalidadPartidaDesde
                                        .IndiceOcupacional.ManualPuesto
                                        .IdManualPuesto,

                                    Nombre = empleadoMovimiento
                                        .IndiceOcupacionalModalidadPartidaDesde
                                        .IndiceOcupacional.ManualPuesto
                                        .Nombre,
                                }
                            },
                        },

                        IdIndiceOcupacionalModalidadPartidaHasta = 0,

                    },

                    ListaPuestosOcupados = modeloListaIOMPOcupados,

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

                    var nombramientoHasta = await db.TipoNombramiento
                            .Include(i => i.RelacionLaboral)
                            .Include(i => i.RelacionLaboral.RegimenLaboral)
                            .Where(w => w.IdTipoNombramiento == (int)empleadoMovimiento.IdTipoNombramiento).FirstOrDefaultAsync();


                    modelo.EmpleadoMovimiento.IndiceOcupacionalModalidadPartidaHasta =
                        new IndiceOcupacionalModalidadPartida
                        {
                            IdIndiceOcupacional = (int)empleadoMovimiento.IdIndiceOcupacional,
                            IdFondoFinanciamiento = (int)empleadoMovimiento.IdFondoFinanciamiento,
                            IdTipoNombramiento = (int)empleadoMovimiento.IdTipoNombramiento,
                            SalarioReal = empleadoMovimiento.SalarioReal,

                            TipoNombramiento = new TipoNombramiento
                            {

                                IdTipoNombramiento = nombramientoHasta.IdTipoNombramiento,
                                Nombre = nombramientoHasta.Nombre,
                                IdRelacionLaboral = nombramientoHasta.IdRelacionLaboral,

                                RelacionLaboral = new RelacionLaboral
                                {
                                    IdRelacionLaboral = nombramientoHasta.RelacionLaboral.IdRelacionLaboral,
                                    Nombre = nombramientoHasta.RelacionLaboral.Nombre,
                                    IdRegimenLaboral = nombramientoHasta.RelacionLaboral.IdRegimenLaboral,

                                    RegimenLaboral = new RegimenLaboral
                                    {

                                        IdRegimenLaboral = nombramientoHasta.RelacionLaboral.RegimenLaboral
                                            .IdRegimenLaboral,

                                        Nombre = nombramientoHasta.RelacionLaboral.RegimenLaboral
                                            .Nombre,
                                    },

                                },

                            },

                            IndiceOcupacional = new IndiceOcupacional
                            {

                                Dependencia = new Dependencia
                                {
                                    IdDependencia = varIO.Dependencia.IdDependencia,
                                    Nombre = varIO.Dependencia.Nombre,

                                    Sucursal = new Sucursal
                                    {
                                        IdSucursal = varIO.Dependencia.Sucursal.IdSucursal,
                                        Nombre = varIO.Dependencia.Sucursal.Nombre,
                                        IdCiudad = varIO.Dependencia.Sucursal.IdCiudad
                                    },
                                },

                                ManualPuesto = new ManualPuesto
                                {
                                    IdManualPuesto = varIO.ManualPuesto.IdManualPuesto,
                                    Nombre = varIO.ManualPuesto.Nombre
                                },

                                RolPuesto = new RolPuesto
                                {
                                    IdRolPuesto = varIO.RolPuesto.IdRolPuesto,
                                    Nombre = varIO.RolPuesto.Nombre
                                },

                                EscalaGrados = new EscalaGrados
                                {
                                    IdEscalaGrados = varIO.EscalaGrados.IdEscalaGrados,
                                    Nombre = varIO.EscalaGrados.Nombre,
                                    Remuneracion = varIO.EscalaGrados.Remuneracion,
                                },

                                IdDependencia = empleadoMovimiento.IdIndiceOcupacional,
                                IdRolPuesto = empleadoMovimiento.IndiceOcupacional.IdRolPuesto,
                                IdManualPuesto = empleadoMovimiento.IndiceOcupacional.IdManualPuesto,
                                IdEscalaGrados = empleadoMovimiento.IndiceOcupacional.IdEscalaGrados,

                            },

                            IdDependencia = (int)varIO.IdDependencia,

                            IdIndiceOcupacionalModalidadPartida = empleadoMovimiento
                                .IdIndiceOcupacionalModalidadPartidaHasta != null
                                ? (int)empleadoMovimiento.IdIndiceOcupacionalModalidadPartidaHasta
                                : 0
                                ,

                        };

                    modelo.EmpleadoMovimiento.IdIndiceOcupacionalModalidadPartidaHasta =
                        empleadoMovimiento.IdIndiceOcupacionalModalidadPartidaHasta;

                    modelo.EmpleadoMovimiento.IdFondoFinanciamiento = empleadoMovimiento.IdFondoFinanciamiento;


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
        ///  Lista los estados de aprobación de TTHH
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
        ///  Lista los estados de aprobación del jefe de una dependencia
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

                // 1) OBTENER EL TIPO DE ACCIÓN DE PERSONAL
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



                // 2) Interpretación de datos 

                /*
                    modificaDistributivo = genera movimiento de personal (osea debe ser registrado)
                    modalidadContratacion = toma los datos del puesto ( IOMP hasta), si es falso los datos del cambio se toman de los actuales de la persona
                    definitivo = tiempo indefinido                 
                 */

                if (
                    tipoAccionPersonal.Nombre.ToString().ToUpper() == ConstantesAccionPersonal
                    .TerminacionEncargo.ToString().ToUpper()
                    )
                {
                    var encargoActual = await db.EmpleadoMovimiento
                        .Where(w =>
                            w.AccionPersonal.Ejecutado == true
                            && w.AccionPersonal.TipoAccionPersonal.Nombre.ToString().ToUpper()
                            == ConstantesAccionPersonal.Encargo.ToString().ToUpper()
                            && (w.FechaHasta != null && w.FechaHasta >= 
                                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
                            )
                        ).FirstOrDefaultAsync();

                    if (encargoActual != null)
                    {
                        modelo.FechaRige = encargoActual.FechaDesde;
                        modelo.FechaRigeHasta = (modelo.FechaRigeHasta <= encargoActual.FechaHasta)
                            ? modelo.FechaRigeHasta
                            : encargoActual.FechaHasta
                        ;
                        modelo.NoDias = ((DateTime)modelo.FechaRigeHasta - modelo.FechaRige).Days;
                        modelo.Numero = ""+modelo.NoDias;
                    }
                    else {

                        return new Response
                        {
                            IsSuccess = false,
                            Message = Mensaje.ErrorFinEncargo,
                            Resultado = accionPersonalViewModel.EmpleadoMovimiento.Empleado.Persona.Identificacion
                        };
                    }


                }

                if (
                    tipoAccionPersonal.Nombre.ToString().ToUpper() == ConstantesAccionPersonal
                    .TerminacionSubrogacion.ToString().ToUpper()
                    )
                {
                    var encargoActual = await db.EmpleadoMovimiento
                        .Where(w =>
                            w.AccionPersonal.Ejecutado == true
                            && w.AccionPersonal.TipoAccionPersonal.Nombre.ToString().ToUpper()
                            == ConstantesAccionPersonal.Subrogacion.ToString().ToUpper()
                            && (w.FechaHasta != null && w.FechaHasta >= 
                                new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day)
                            )
                        ).FirstOrDefaultAsync();

                    if (encargoActual != null)
                    {
                        modelo.FechaRige = encargoActual.FechaDesde;
                        modelo.FechaRigeHasta = (modelo.FechaRigeHasta <= encargoActual.FechaHasta)
                            ? modelo.FechaRigeHasta
                            : encargoActual.FechaHasta
                        ;

                        modelo.NoDias = ((DateTime)modelo.FechaRigeHasta - modelo.FechaRige).Days;
                        modelo.Numero = "" + modelo.NoDias;

                    }
                    else
                    {

                        return new Response
                        {
                            IsSuccess = false,
                            Message = Mensaje.ErrorFinSubrogacion,
                            Resultado = accionPersonalViewModel.EmpleadoMovimiento.Empleado.Persona.Identificacion
                        };
                    }


                }

                else if (
                    tipoAccionPersonal.ModificaDistributivo == true
                    //&& tipoAccionPersonal.ModalidadContratacion == true
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
                            .Include(i => i.Empleado)
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
                    && tipoAccionPersonal.DesactivarEmpleado == true
                )
                {
                    // Esto es una desvinculación por tanto no se registra en empleado Movimiento

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

                // 1) OBTENER EL TIPO DE ACCIÓN DE PERSONAL
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



                // 2) Interpretación de datos 

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
                    // Esto es una desvinculación por tanto no se registra en empleado Movimiento

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
                 * Obtiene el último movimiento temporal del empleado logueado que esté vigente en esta fecha
                 * y asi saber si subroga un cargo o está encargado del puesto
                */
                var empleadoLogueadoPuestoEncargado = await db.EmpleadoMovimiento
                    .Include(i => i.IndiceOcupacional)
                    .Include(i => i.AccionPersonal)
                    .Include(i => i.AccionPersonal.TipoAccionPersonal)
                    .Where(w =>
                        w.AccionPersonal.Ejecutado == true
                        && w.FechaDesde <= DateTime.Now
                        && ( 
                            (w.FechaHasta != null && w.FechaHasta >= 
                            new DateTime (DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
                            )
                            || w.FechaHasta == null
                        )
                        && w.IdEmpleado == empleadoLogueado.IdEmpleado
                    )
                    .OrderByDescending(o => o.IdEmpleadoMovimiento)
                    .FirstOrDefaultAsync();


                // Este será el indice ocupacional actual del empleado logueado
                // tomará los valores del puesto al que está subrogando o encargado si fuera el caso
                var IndiceOcupacionalUsuarioActual = new IndiceOcupacional();


                // Esta variable dirá si el empleado logueado es jefe de su dependencia
                var empleadoLogueadoJefe = false;


                if (empleadoLogueadoPuestoEncargado != null)
                {

                    if (
                        empleadoLogueadoPuestoEncargado.AccionPersonal.TipoAccionPersonal.Nombre.ToString().ToUpper()
                        == ConstantesAccionPersonal.Encargo.ToString().ToUpper()
                        )
                    {
                        var finEncargo = await db.AccionPersonal
                            .Where(w =>
                                w.Ejecutado == true
                                && w.TipoAccionPersonal.Nombre.ToString().ToUpper()
                                    == ConstantesAccionPersonal.TerminacionEncargo.ToString().ToUpper()
                                && new DateTime(w.FechaRige.Year, w.FechaRige.Month, w.FechaRige.Day)
                                    == new DateTime(
                                        empleadoLogueadoPuestoEncargado.FechaDesde.Year,
                                        empleadoLogueadoPuestoEncargado.FechaDesde.Month,
                                        empleadoLogueadoPuestoEncargado.FechaDesde.Day
                                        )

                            ).FirstOrDefaultAsync();

                        if (finEncargo != null)
                        {

                            IndiceOcupacionalUsuarioActual = empleadoLogueado.IndiceOcupacional;
                            empleadoLogueadoJefe = empleadoLogueado.Empleado.EsJefe;

                        }
                        else
                        {


                            IndiceOcupacionalUsuarioActual = empleadoLogueadoPuestoEncargado
                                .IndiceOcupacional;

                            empleadoLogueadoJefe = empleadoLogueadoPuestoEncargado
                                .EsJefe;

                        }

                    }

                    else if (
                        empleadoLogueadoPuestoEncargado.AccionPersonal.TipoAccionPersonal.Nombre.ToString().ToUpper()
                        == ConstantesAccionPersonal.Subrogacion.ToString().ToUpper()
                        )
                    {
                        var finEncargo = await db.AccionPersonal
                            .Where(w =>
                                w.Ejecutado == true
                                && w.TipoAccionPersonal.Nombre.ToString().ToUpper()
                                    == ConstantesAccionPersonal.TerminacionSubrogacion.ToString().ToUpper()
                                && new DateTime(w.FechaRige.Year, w.FechaRige.Month, w.FechaRige.Day)
                                    == new DateTime(
                                        empleadoLogueadoPuestoEncargado.FechaDesde.Year,
                                        empleadoLogueadoPuestoEncargado.FechaDesde.Month,
                                        empleadoLogueadoPuestoEncargado.FechaDesde.Day
                                        )

                            ).FirstOrDefaultAsync();

                        if (finEncargo != null)
                        {

                            IndiceOcupacionalUsuarioActual = empleadoLogueado.IndiceOcupacional;
                            empleadoLogueadoJefe = empleadoLogueado.Empleado.EsJefe;

                        }
                        else
                        {


                            IndiceOcupacionalUsuarioActual = empleadoLogueadoPuestoEncargado
                                .IndiceOcupacional;

                            empleadoLogueadoJefe = empleadoLogueadoPuestoEncargado
                                .EsJefe;

                        }

                    }
                    else {
                        IndiceOcupacionalUsuarioActual = empleadoLogueadoPuestoEncargado
                                .IndiceOcupacional;

                        empleadoLogueadoJefe = empleadoLogueadoPuestoEncargado
                            .EsJefe;
                    }

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
                    // busca si en el flujo de aprobación, el empleado logueado POR SU PUESTO puede aprobar 
                    // la acción de personal
                    var flujoAprobacionPorPuesto = await db.FlujoAprobacion
                        .Where(w =>
                            w.IdTipoAccionPersonal == item.TipoAccionPersonalViewModel.IdTipoAccionPersonal
                            && w.IdManualPuesto == IndiceOcupacionalUsuarioActual.IdManualPuesto
                        )
                        .FirstOrDefaultAsync();


                    // busca si en el flujo de aprobación, el empleado logueado por SER JEFE puede aprobar 
                    // la acción de personal
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

                        // Poner el estado de aprobación para cada persona encargada de aprobar
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
                 * Obtiene el último movimiento temporal del empleado logueado que esté vigente en esta fecha
                 * y asi saber si subroga un cargo o está encargado del puesto
                */
                var empleadoLogueadoPuestoEncargado = await db.EmpleadoMovimiento
                    .Include(i => i.IndiceOcupacional)
                    .Include(i => i.AccionPersonal)
                    .Include(i => i.AccionPersonal.TipoAccionPersonal)
                    .Where(w =>
                        w.AccionPersonal.Ejecutado == true
                        && w.FechaDesde <= DateTime.Now
                        && (
                            (w.FechaHasta != null && w.FechaHasta >=
                            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
                            )
                            || w.FechaHasta == null
                        )
                        && w.IdEmpleado == empleadoLogueado.IdEmpleado
                    )
                    .OrderByDescending(o => o.IdEmpleadoMovimiento)
                    .FirstOrDefaultAsync();


                // Este será el indice ocupacional actual del empleado logueado
                // tomará los valores del puesto al que está subrogando o encargado si fuera el caso
                var IndiceOcupacionalUsuarioActual = new IndiceOcupacional();


                // Esta variable dirá si el empleado logueado es jefe de su dependencia
                var empleadoLogueadoJefe = false;


                if (empleadoLogueadoPuestoEncargado != null)
                {

                    if (
                        empleadoLogueadoPuestoEncargado.AccionPersonal.TipoAccionPersonal.Nombre.ToString().ToUpper()
                        == ConstantesAccionPersonal.Encargo.ToString().ToUpper()
                        )
                    {
                        var finEncargo = await db.AccionPersonal
                            .Where(w =>
                                w.Ejecutado == true
                                && w.TipoAccionPersonal.Nombre.ToString().ToUpper()
                                    == ConstantesAccionPersonal.TerminacionEncargo.ToString().ToUpper()
                                && new DateTime(w.FechaRige.Year, w.FechaRige.Month, w.FechaRige.Day)
                                    == new DateTime(
                                        empleadoLogueadoPuestoEncargado.FechaDesde.Year,
                                        empleadoLogueadoPuestoEncargado.FechaDesde.Month,
                                        empleadoLogueadoPuestoEncargado.FechaDesde.Day
                                        )

                            ).FirstOrDefaultAsync();

                        if (finEncargo != null)
                        {

                            IndiceOcupacionalUsuarioActual = empleadoLogueado.IndiceOcupacional;
                            empleadoLogueadoJefe = empleadoLogueado.Empleado.EsJefe;

                        }
                        else
                        {


                            IndiceOcupacionalUsuarioActual = empleadoLogueadoPuestoEncargado
                                .IndiceOcupacional;

                            empleadoLogueadoJefe = empleadoLogueadoPuestoEncargado
                                .EsJefe;

                        }

                    }

                    else if (
                        empleadoLogueadoPuestoEncargado.AccionPersonal.TipoAccionPersonal.Nombre.ToString().ToUpper()
                        == ConstantesAccionPersonal.Subrogacion.ToString().ToUpper()
                        )
                    {
                        var finEncargo = await db.AccionPersonal
                            .Where(w =>
                                w.Ejecutado == true
                                && w.TipoAccionPersonal.Nombre.ToString().ToUpper()
                                    == ConstantesAccionPersonal.TerminacionSubrogacion.ToString().ToUpper()
                                && new DateTime(w.FechaRige.Year, w.FechaRige.Month, w.FechaRige.Day)
                                    == new DateTime(
                                        empleadoLogueadoPuestoEncargado.FechaDesde.Year,
                                        empleadoLogueadoPuestoEncargado.FechaDesde.Month,
                                        empleadoLogueadoPuestoEncargado.FechaDesde.Day
                                        )

                            ).FirstOrDefaultAsync();

                        if (finEncargo != null)
                        {

                            IndiceOcupacionalUsuarioActual = empleadoLogueado.IndiceOcupacional;
                            empleadoLogueadoJefe = empleadoLogueado.Empleado.EsJefe;

                        }
                        else
                        {


                            IndiceOcupacionalUsuarioActual = empleadoLogueadoPuestoEncargado
                                .IndiceOcupacional;

                            empleadoLogueadoJefe = empleadoLogueadoPuestoEncargado
                                .EsJefe;

                        }

                    }
                    else
                    {
                        IndiceOcupacionalUsuarioActual = empleadoLogueadoPuestoEncargado
                                .IndiceOcupacional;

                        empleadoLogueadoJefe = empleadoLogueadoPuestoEncargado
                            .EsJefe;
                    }


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

                        
                        // busca si en el flujo de aprobación, el empleado logueado POR SU PUESTO puede aprobar 
                        // la acción de personal
                        var flujoAprobacionPorPuesto = await db.FlujoAprobacion
                            .Where(w =>
                                w.IdTipoAccionPersonal == accionPersonalActualizar.IdTipoAccionPersonal
                                && w.IdManualPuesto == IndiceOcupacionalUsuarioActual.IdManualPuesto
                            )
                            .FirstOrDefaultAsync();


                        // busca si en el flujo de aprobación, el empleado logueado por SER JEFE puede aprobar 
                        // la acción de personal
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
                                // Si ya existe el registro, el único campo que se actualiza es el estado
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
                                    // Si ya existe el registro, el único campo que se actualiza es el estado
                                    firmado2.Estado = accionPersonalViewModel.Estado;

                                    db.AprobacionAccionPersonal.Update(firmado2);
                                    await db.SaveChangesAsync();
                                }

                            }

                        }
                        
                        


                        // obtener las firmas necesarias para realizar la comparación
                        var firmasTotales = await db.FlujoAprobacion
                            .Where(w =>
                                w.IdTipoAccionPersonal == accionPersonalActualizar.IdTipoAccionPersonal
                            ).ToListAsync();

                        // obtener las firmas que están realizadas
                        var firmasRealizadas = await db.AprobacionAccionPersonal
                            .Where(w =>
                                w.IdAccionPersonal == accionPersonalViewModel.IdAccionPersonal
                            ).ToListAsync();

                        // Obtener las firmas que están aprobadas
                        var firmasAprobadas = firmasRealizadas.Where(w => w.Estado == 5).ToList();


                        // si todas las firmas necesarias están aprobadas, el estado va a aprobado
                        // si todas las firmas se han hecho pero almenos una está negada el estado va negado
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

                        

                        // Cuando todo se ha realizado correctamente y todas las firmas están aprobadas
                        // se ejecuta la acción de personal
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

        
        /// <summary>
        /// Este método se encarga de ejecutar todas las acciones de personal de acuerdo a lo 
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
                    0 = n/a                                    desactivarEmpleado = false, modalidadContratación = false
                    1 = Modalidad contratación                 desactivarEmpleado = false, modalidadContratación = true
                    2 = desactivar empleado                    desactivarEmpleado = true, modalidadContratación = false
                    
                */


                var accion = await db.AccionPersonal
                    .Include(i => i.TipoAccionPersonal)
                    .Where(w => w.IdAccionPersonal == idAccionPersonal)
                    .FirstOrDefaultAsync();

                var empleadoAfectado = await db.Empleado
                    .Where(w => w.IdEmpleado == accion.IdEmpleado)
                    .FirstOrDefaultAsync();

                var IOMPActual = await db.IndiceOcupacionalModalidadPartida
                    .Include(i=>i.Empleado)
                    .Include(i=>i.TipoNombramiento)
                    .Include(i => i.TipoNombramiento.RelacionLaboral)
                    .Where(w => w.IdEmpleado == empleadoAfectado.IdEmpleado)
                    .OrderByDescending(o=>o.IdIndiceOcupacionalModalidadPartida)
                    .FirstOrDefaultAsync();

                var modalidadPartidaVacante = await db.ModalidadPartida
                            .Where(w => w.Nombre == Constantes.PartidaVacante)
                            .FirstOrDefaultAsync();

                // ModificaDistributivo = se relizan cambios
                if (accion.TipoAccionPersonal.ModificaDistributivo == true)
                {

                    if (accion.TipoAccionPersonal.DesactivarEmpleado == true)
                    {

                        await DesvincularEmpleadoPorFecha(empleadoAfectado.IdEmpleado,accion.FechaRige.Year,accion.FechaRige);
                    }

                    else if (
                        accion.TipoAccionPersonal.DesactivarEmpleado == false
                        && accion.TipoAccionPersonal.ModalidadContratacion == true
                    )
                    {
                        await RealizarMovimientoPersonal(accion.IdAccionPersonal);

                    }

                    if (accion.TipoAccionPersonal.DesactivarCargo == true) {
                        
                        var iompActual = await db.IndiceOcupacionalModalidadPartida
                            .Where(w => 
                                w.IdEmpleado == empleadoAfectado.IdEmpleado
                                && w.IdModalidadPartida != null
                            )
                            .OrderByDescending(o=>o.IdIndiceOcupacionalModalidadPartida)
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
                            //iompActual.IdModalidadPartida = idModalidadPartidaSuprimida.IdModalidadPartida;
                            // se debe crear un a nueva iomp con partida suprimida

                        }

                    }
                }

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


        public async Task DesvincularEmpleadoPorFecha(int IdEmpleado, int yearDesvinculacion, DateTime FechaDesvinculacion)
        {

            try {

                if (
                    FechaDesvinculacion.Day <= DateTime.Now.Day
                    && FechaDesvinculacion.Month <= DateTime.Now.Month
                    && FechaDesvinculacion.Year <= DateTime.Now.Year
                    )
                {

                    var empleadoAfectado = await db.Empleado
                    .Where(w => w.IdEmpleado == IdEmpleado)
                    .FirstOrDefaultAsync();

                    var IOMPActual = await db.IndiceOcupacionalModalidadPartida
                        .Include(i => i.Empleado)
                        .Include(i => i.TipoNombramiento)
                        .Include(i => i.TipoNombramiento.RelacionLaboral)
                        .Where(w => w.IdEmpleado == empleadoAfectado.IdEmpleado)
                        .OrderByDescending(o => o.Fecha)
                        .FirstOrDefaultAsync();

                    var modalidadPartidaVacante = await db.ModalidadPartida
                                .Where(w => w.Nombre == Constantes.PartidaVacante)
                                .FirstOrDefaultAsync();


                    empleadoAfectado.Activo = false;
                    empleadoAfectado.AnoDesvinculacion = yearDesvinculacion;
                    empleadoAfectado.TipoRelacion = null;
                    empleadoAfectado.IdDependencia = null;

                    db.Empleado.Update(empleadoAfectado);



                    // Se crea un indice ocupacional modalidad partida vacío
                    // para el historial
                    var modelo = new IndiceOcupacionalModalidadPartida
                    {
                        IdIndiceOcupacional = IOMPActual.IdIndiceOcupacional,
                        IdEmpleado = null,
                        IdFondoFinanciamiento = IOMPActual.IdFondoFinanciamiento,
                        IdTipoNombramiento = null,
                        Fecha = DateTime.Now,
                        SalarioReal = null,
                        CodigoContrato = null,
                        NumeroPartidaIndividual = null,
                        IdModalidadPartida = null
                    };

                    // si es por nombramiento se mantiene el numero de partida, y la modalidad pasa a vacante
                    if (
                        IOMPActual.TipoNombramiento.RelacionLaboral.Nombre.ToString().ToUpper()
                        == ConstantesTipoRelacion.Nombramiento.ToString().ToUpper()
                        )
                    {
                        modelo.NumeroPartidaIndividual = IOMPActual.NumeroPartidaIndividual;
                        modelo.IdModalidadPartida = modalidadPartidaVacante.IdModalidadPartida;
                        modelo.IdTipoNombramiento = IOMPActual.IdTipoNombramiento;

                        db.IndiceOcupacionalModalidadPartida.Add(modelo);
                    }

                    

                    await db.SaveChangesAsync();
                    
                }

                

            }
            catch (Exception ex) {
                
                throw;
                
            }
        }

        public async Task RealizarMovimientoPersonal(int idAccionPersonal)
        {
           
            
            var accion = await db.AccionPersonal
                    .Include(i => i.TipoAccionPersonal)
                    .Where(w => w.IdAccionPersonal == idAccionPersonal)
                    .FirstOrDefaultAsync();

            if (accion.FechaRige <= DateTime.Now) {


                var empleadoAfectado = await db.Empleado
                .Where(w => w.IdEmpleado == accion.IdEmpleado)
                .FirstOrDefaultAsync();


                var modalidadPartidaVacante = await db.ModalidadPartida
                            .Where(w => w.Nombre == Constantes.PartidaVacante)
                            .FirstOrDefaultAsync();

                var empleadoMovimiento = await db.EmpleadoMovimiento
                                .Include(i => i.IndiceOcupacional)
                                .Include(i => i.IndiceOcupacional.Dependencia)
                                .Where(w => w.IdAccionPersonal == accion.IdAccionPersonal)
                                .FirstOrDefaultAsync();

                var modeloIOMP = new IndiceOcupacionalModalidadPartida
                {

                    IdIndiceOcupacional = (int)empleadoMovimiento.IdIndiceOcupacional,
                    IdEmpleado = (int)empleadoMovimiento.IdEmpleado,
                    IdFondoFinanciamiento = (int)empleadoMovimiento.IdFondoFinanciamiento,
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
                    .Include(i => i.RelacionLaboral)
                    .Where(w => w.IdTipoNombramiento == modeloIOMP.IdTipoNombramiento)
                    .FirstOrDefaultAsync();

                empleadoAfectado.IdDependencia = empleadoMovimiento.IndiceOcupacional.IdDependencia;
                empleadoAfectado.TipoRelacion = tipoRelacion.RelacionLaboral.Nombre;

                db.Empleado.Update(empleadoAfectado);


                if (!String.IsNullOrEmpty(modeloIOMP.NumeroPartidaIndividual))
                {
                    var puestoAnterior = await db.IndiceOcupacionalModalidadPartida
                        .Where(w =>
                            w.IdIndiceOcupacionalModalidadPartida == empleadoMovimiento
                            .IdIndiceOcupacionalModalidadPartidaDesde
                        ).FirstOrDefaultAsync();

                    var partidaVacante = await db.ModalidadPartida.
                        Where(w => w.Nombre == Constantes.PartidaVacante)
                        .FirstOrDefaultAsync();


                    //** Se crea un indice ocupacional modalidad partida vacío
                    // para el historial
                    var modelo = new IndiceOcupacionalModalidadPartida
                    {
                        IdIndiceOcupacional = puestoAnterior.IdIndiceOcupacional,
                        IdEmpleado = null,
                        IdFondoFinanciamiento = puestoAnterior.IdFondoFinanciamiento,
                        IdTipoNombramiento = null,
                        Fecha = DateTime.Now,
                        SalarioReal = null,
                        CodigoContrato = null,
                        NumeroPartidaIndividual = null,
                        IdModalidadPartida = null
                    };

                    // si es por nombramiento se mantiene el numero de partida, y la modalidad pasa a vacante
                    if (
                        puestoAnterior.TipoNombramiento.RelacionLaboral.Nombre.ToString().ToUpper()
                        == ConstantesTipoRelacion.Nombramiento.ToString().ToUpper()
                        )
                    {
                        modelo.NumeroPartidaIndividual = puestoAnterior.NumeroPartidaIndividual;
                        modelo.IdModalidadPartida = modalidadPartidaVacante.IdModalidadPartida;
                        modelo.IdTipoNombramiento = puestoAnterior.IdTipoNombramiento;

                        db.IndiceOcupacionalModalidadPartida.Add(modelo);
                    }


                    await db.SaveChangesAsync();
                    

                }


            }

            
        }


        // comentario cambio rama

    }
}