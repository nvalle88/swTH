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
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex.Message,
                    Message = Mensaje.Excepcion,
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
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
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };
            }
        }

        // POST: api/AccionesPersonal
        [HttpPost]
        [Route("InsertarAccionPersonal")]
        public async Task<Response> InsertarAccionPersonal([FromBody] AccionPersonalViewModel accionPersonalViewModel)
        {
            try
            {

                var empleado = db.Empleado.Include(ie=>ie.Persona)
                    .Where(w=>w.IdEmpleado == accionPersonalViewModel.DatosBasicosEmpleadoViewModel.IdEmpleado)
                    .FirstOrDefault()
                ;


                var modelo = new AccionPersonal
                {
                    IdEmpleado = accionPersonalViewModel.DatosBasicosEmpleadoViewModel.IdEmpleado,
                    Fecha = accionPersonalViewModel.Fecha,
                    FechaRige = accionPersonalViewModel.FechaRige,
                    FechaRigeHasta = accionPersonalViewModel.FechaRigeHasta,
                    Estado = accionPersonalViewModel.Estado,
                    Explicacion = accionPersonalViewModel.Explicacion,
                    NoDias = accionPersonalViewModel.NoDias,
                    Numero = accionPersonalViewModel.Numero,
                    Solicitud = accionPersonalViewModel.Solicitud,
                    IdTipoAccionPersonal = accionPersonalViewModel.TipoAccionPersonalViewModel.IdTipoAccionPersonal

                };

                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = ""
                    };
                }

                var respuesta = Existe(modelo);

                if (!respuesta.IsSuccess)
                {

                    using (var transaction = await db.Database.BeginTransactionAsync())
                    {

                        // Validación del movimiento de personal
                        var estadoValidacion = CumpleRequerimientos(accionPersonalViewModel);

                        if (!estadoValidacion.IsSuccess)
                        {
                            return new Response
                            {
                                IsSuccess = false,
                                Message = estadoValidacion.Message,
                                Resultado = empleado.Persona.Identificacion
                            };
                        }

                        if (accionPersonalViewModel.GeneraMovimientoPersonal == true)
                        {

                            if (accionPersonalViewModel.IdIndiceOcupacionalModalidadPartidaPropuesta < 1)
                            {
                                return new Response
                                {
                                    IsSuccess = false,
                                    Message = Mensaje.SeleccioneIndice,
                                    Resultado = empleado.Persona.Identificacion
                                };
                            }
                            

                            // Guardando registro de la acción de Personal
                            db.AccionPersonal.Add(modelo);
                            await db.SaveChangesAsync();


                            var modalidadPartidaActual = ObtenerIndice(accionPersonalViewModel.DatosBasicosEmpleadoViewModel.IdEmpleado);

                            // Modelo del movimiento de personal
                            var modeloEmpleadoMovimiento = new EmpleadoMovimiento
                            {
                                IdEmpleado = accionPersonalViewModel.DatosBasicosEmpleadoViewModel.IdEmpleado,
                                FechaDesde = accionPersonalViewModel.FechaRige,
                                FechaHasta = accionPersonalViewModel.FechaRigeHasta,
                                IdIndiceOcupacionalModalidadPartidaDesde = modalidadPartidaActual.IdIndiceOcupacionalModalidadPartida,
                                IdIndiceOcupacionalModalidadPartidaHasta = accionPersonalViewModel.IdIndiceOcupacionalModalidadPartidaPropuesta,
                                IdAccionPersonal = modelo.IdAccionPersonal

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

                        }

                        // ****Si la accion no genera movimiento de personal ****
                        
                        // Registro de la acción de Personal
                        db.AccionPersonal.Add(modelo);
                        await db.SaveChangesAsync();
                        transaction.Commit();

                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.GuardadoSatisfactorio,
                            Resultado = empleado.Persona.Identificacion
                        };
                    } // fin transaction

                        
                }

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = empleado.Persona.Identificacion
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
        [Route("ListarAccionesPersonalPorEmpleado")]
        public async Task<AccionesPersonalPorEmpleadoViewModel> ListarAccionesPersonalPorEmpleado([FromBody] AccionesPersonalPorEmpleadoViewModel accionesPersonalPorEmpleadoViewModel)
        {
            var modelo = new AccionesPersonalPorEmpleadoViewModel {
                    ListaAccionPersonal = new List<AccionPersonalViewModel>(),
                    DatosBasicosEmpleadoViewModel = new DatosBasicosEmpleadoViewModel()
            };

            try {

                var empleadoActual = db.Empleado.Include(d => d.Dependencia)
                    .Where(x => x.NombreUsuario == accionesPersonalPorEmpleadoViewModel.NombreUsuarioActual)
                    .FirstOrDefault()
                ;
                
                var datosEmpleado = db.Empleado
                    .Include(me => me.Persona)
                    .Include(md=>md.Dependencia)
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
                        Fecha = (DateTime) s.Fecha,
                        Numero = s.Numero,
                        Solicitud = s.Solicitud,
                        Explicacion = s.Explicacion,
                        FechaRige = (DateTime)s.FechaRige,
                        FechaRigeHasta = (DateTime)s.FechaRigeHasta,
                        NoDias = (int) s.NoDias,
                        Estado = s.Estado,
                        EstadoDirector = (ListaEstados.Count > 0)
                            ? 
                                ListaEstados.Where(wle=>
                                    wle.GrupoAprobacion == 0
                                    && wle.ValorEstado == s.Estado
                                ).FirstOrDefault().NombreEstado
                            :"",
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
                                    ModificaDistributivo = st.ModificaDistributivo,
                                    IdEstadoTipoAccionPersonal = st.IdEstadoTipoAccionPersonal

                                }
                            )
                            .FirstOrDefault()
                    }

                    ).ToListAsync();

                modelo.DatosBasicosEmpleadoViewModel = datosEmpleado;
                modelo.ListaAccionPersonal = lista;

                return modelo;


            } catch (Exception ex) {
                return modelo;
            }
        }

        /// <summary>
        ///  Lista los estados de aprobación de TTHH
        /// </summary>
        /// <returns></returns>
        // GET: api/AccionesPersonal
        [HttpGet]
        [Route("ListarEstadosAprobacionTTHH")]
        public async Task<List<AprobacionMovimientoInternoViewModel>> ListarEstadosAprobacionTTHH() {

            try {
                var lista = ConstantesEstadosAprobacionMovimientoInterno.ListaEstadosAprobacionMovimientoInterno.Where(w=>w.GrupoAprobacion == 1).ToList();

                return lista;

            } catch(Exception){
                return new List<AprobacionMovimientoInternoViewModel>();
            }
        }

        /// <summary>
        ///  Lista los estados de aprobación del jefe de una dependencia
        /// </summary>
        /// <returns></returns>
        // GET: api/AccionesPersonal
        [HttpGet]
        [Route("ListarEstadosAprobacion")]
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


        // POST: api/AccionesPersonal
        /// <summary>
        /// Necesario: IdAccionPersonal
        /// </summary>
        /// <param name="accionPersonalViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ObtenerAccionPersonalViewModel")]
        public async Task<Response> ObtenerAccionPersonalViewModel([FromBody] AccionPersonalViewModel accionPersonalViewModel) {
            try
            {

                var empleadoMovimientoRegistro = db.EmpleadoMovimiento.Where(sw => sw.IdAccionPersonal == accionPersonalViewModel.IdAccionPersonal).FirstOrDefault();

                var indicePropuesto = 0;

                if (empleadoMovimientoRegistro != null) {
                    indicePropuesto = (int) empleadoMovimientoRegistro.IdIndiceOcupacionalModalidadPartidaHasta;
                }

                var modelo = db.AccionPersonal
                    .Where(w => w.IdAccionPersonal == accionPersonalViewModel.IdAccionPersonal)
                    .Select( s=> new AccionPersonalViewModel
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
                            
                            GeneraMovimientoPersonal = (empleadoMovimientoRegistro != null) ? true:false,

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
                                ModificaDistributivo = st.ModificaDistributivo,
                                IdEstadoTipoAccionPersonal = st.IdEstadoTipoAccionPersonal

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
                    .FirstOrDefault();



                return new Response {
                    IsSuccess = true,
                    Resultado = modelo
                };

            }
            catch (Exception ex)
            {
                return new Response {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion +" "+ ex
                };
            }
        }



        // POST: api/AccionesPersonal
        [HttpPost]
        [Route("EditarAccionPersonalTTHH")]
        public async Task<Response> EditarAccionPersonalTTHH([FromBody] AccionPersonalViewModel accionPersonalViewModel)
        {
            try {

                var empleado = await db.Empleado.Include(ie=>ie.Persona)
                    .Where(w=>w.IdEmpleado == accionPersonalViewModel.DatosBasicosEmpleadoViewModel.IdEmpleado)
                    .FirstOrDefaultAsync()
                    ;

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

                var modificarMovimiento = await db.EmpleadoMovimiento.Where(w=>w.IdAccionPersonal == accionPersonalViewModel.IdAccionPersonal).FirstOrDefaultAsync();


                if (accionPersonalViewModel.GeneraMovimientoPersonal == true)
                {

                    if (modificarMovimiento == null)
                    {
                        using (var transaction = await db.Database.BeginTransactionAsync())
                        {

                            //Modificacion del registro de acciónPersonal

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


                                await db.SaveChangesAsync();

                            }
                            else {

                                return new Response
                                {
                                    IsSuccess = false,
                                    Message = Mensaje.ExisteRegistro,
                                    Resultado = empleado.Persona.Identificacion
                                };
                            }


                            // **** Creación del registro de movimiento de personal ******

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

                            //Modificacion del registro de acciónPersonal

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


                                await db.SaveChangesAsync();

                            }
                            else {

                                return new Response
                                {
                                    IsSuccess = false,
                                    Message = Mensaje.ExisteRegistro,
                                    Resultado = empleado.Persona.Identificacion
                                };
                            }


                            // Modificación del registro de movimiento de personal

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
                else {

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


                        await db.SaveChangesAsync();

                        if (modificarMovimiento != null) {
                            
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
                

                

            } catch (Exception ex) {

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion +" "+ ex,
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

                var accionPersonalActualizar = await db.AccionPersonal
                    .Where(w => w.IdAccionPersonal == accionPersonal.IdAccionPersonal).FirstOrDefaultAsync();

                

                if (
                        accionPersonalActualizar != null
                    )
                {
                    
                    accionPersonalActualizar.Estado = accionPersonal.Estado;
                    await db.SaveChangesAsync();

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
        [Route("ListarEmpleadosConAccionPersonal")]
        public async Task<List<AccionPersonalViewModel>> ListarEmpleadosConAccionPersonal([FromBody] AccionesPersonalPorEmpleadoViewModel accionesPersonalPorEmpleadoViewModel)
        {

            try {

                var empleadoActual = db.Empleado.Include(d => d.Dependencia)
                    .Where(x => x.NombreUsuario == accionesPersonalPorEmpleadoViewModel.NombreUsuarioActual)
                    .FirstOrDefault()
                ;

                var ListaEstados = ConstantesEstadosAprobacionMovimientoInterno.ListaEstadosAprobacionMovimientoInterno;

                var lista = await db.AccionPersonal
                    .Where(w => w.Empleado.Dependencia.IdSucursal == empleadoActual.Dependencia.IdSucursal)
                    .Select(s=> new AccionPersonalViewModel
                        {
                            DatosBasicosEmpleadoViewModel = new DatosBasicosEmpleadoViewModel {
                                Nombres = s.Empleado.Persona.Nombres + " " + s.Empleado.Persona.Apellidos,
                                Identificacion = s.Empleado.Persona.Identificacion
                            },
                            

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
                                    ModificaDistributivo = st.ModificaDistributivo,
                                    IdEstadoTipoAccionPersonal = st.IdEstadoTipoAccionPersonal

                                }
                                )
                                .FirstOrDefault(),

                            IdAccionPersonal = s.IdAccionPersonal,
                            Estado = s.Estado,
                            Explicacion = s.Explicacion,
                            Fecha = (DateTime)s.Fecha,
                            FechaRige = (DateTime)s.FechaRige,
                            FechaRigeHasta = (DateTime)s.FechaRigeHasta,
                            NoDias = (int)s.NoDias,
                            Numero = s.Numero,
                            Solicitud = s.Solicitud,

                        EstadoDirector = (ListaEstados.Count > 0)
                            ?
                                ListaEstados.Where(wle =>
                                    wle.GrupoAprobacion == 0
                                    && wle.ValorEstado == s.Estado
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

                return lista;

            }
            catch (Exception ex)
            {

                return new List<AccionPersonalViewModel>();
            }
        }


        private Response CumpleRequerimientos(AccionPersonalViewModel modelo) {

            try {


                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio
                };


            } catch (Exception ex)
            {
                return new Response {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };
            }
        }

        private IndiceOcupacionalModalidadPartida ObtenerIndice(int IdEmpleado) {
            try {
                var modelo = db.IndiceOcupacionalModalidadPartida.Where(w => w.IdEmpleado == IdEmpleado).FirstOrDefault();

                return modelo;

            } catch (Exception ex) {
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

        public async Task ActualizarDiasRestantesAccionPersonal() {

            try {

                var hoy = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                var lista = await db.AccionPersonal.Where(w => w.Fecha.Year == hoy.Year).ToListAsync();

                foreach (var item in lista) {

                    if (item.FechaRige < hoy) {

                        TimeSpan tiempo = (TimeSpan) (item.FechaRigeHasta - hoy);
                        var diasRestantes = (int) tiempo.TotalDays;

                        if ( Convert.ToInt32(item.Numero) > diasRestantes ) {

                            var registro = db.AccionPersonal.Find(item.IdAccionPersonal);
                            registro.Numero = diasRestantes + "";
                            db.AccionPersonal.Update(registro);
                            await db.SaveChangesAsync();

                        }

                    }
                }


            } catch (Exception ex)
            {
            }

        }

    }
}