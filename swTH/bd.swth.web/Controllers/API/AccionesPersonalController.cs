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

        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarAccionPersonal")]
        public async Task<Response> PostAccionPersonal([FromBody] AccionPersonal AccionPersonal)
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

                var respuesta = Existe(AccionPersonal);
                if (!respuesta.IsSuccess)
                {
                    db.AccionPersonal.Add(AccionPersonal);
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
            var bdd = AccionPersonal.IdEmpleado;
            var estadocivilrespuesta = db.AccionPersonal.Where(p => p.IdEmpleado == bdd).FirstOrDefault();
            if (estadocivilrespuesta != null)
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
                Resultado = estadocivilrespuesta,
            };
        }


        // POST: api/AccionesPersonal
        [HttpPost]
        [Route("ListarAccionesPersonalPorEmpleado")]
        public async Task<AccionesPersonalPorEmpleadoViewModel> ListarAccionesPersonalPorEmpleado(AccionesPersonalPorEmpleadoViewModel accionesPersonalPorEmpleadoViewModel)
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

                var lista = await db.AccionPersonal.Include(map => map.TipoAccionPersonal)
                    .Where(w => w.IdEmpleado == datosEmpleado.IdEmpleado)
                    .Select(s => new AccionPersonalViewModel
                    {
                        IdAccionPersonal = s.IdAccionPersonal,
                        Fecha = s.Fecha,
                        Numero = s.Numero,
                        Solicitud = s.Solicitud,
                        Explicacion = s.Explicacion,
                        FechaRige = s.FechaRige,
                        FechaRigeHasta = s.FechaRigeHasta,
                        NoDias = s.NoDias,
                        Estado = s.Estado,

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


            } catch (Exception) {
                return modelo;
            }
        }


    }
}