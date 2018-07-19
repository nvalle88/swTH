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
    [Route("api/SolicitudViaticos")]
    public class SolicitudViaticosController : Controller
    {
        private readonly SwTHDbContext db;

        public SolicitudViaticosController(SwTHDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        [Route("ObtenerSolicitudesViaticosporId")]

        public async Task<ViewModelsSolicitudViaticos> ObtenerSolicitudesViaticosporId([FromBody]ViewModelsSolicitudViaticos viewModelsSolicitudViaticos)
        {
            try
            {
                var NuevoviewModelsSolicitudViaticos = new ViewModelsSolicitudViaticos();

                NuevoviewModelsSolicitudViaticos = await db.SolicitudViatico.Select(x => new ViewModelsSolicitudViaticos
                {
                    FechaSolicitud = x.FechaSolicitud,
                    IdEmpleado = x.IdEmpleado,
                    FondoFinanciamiento = x.FondoFinanciamiento.Nombre,
                    IdSolicitudViatico = x.IdSolicitudViatico,
                    FechaLlegada = x.FechaLlegada,
                    HoraLlegada = x.HoraLlegada,
                    FechaSalida = x.FechaSalida,
                    HoraSalida = x.HoraSalida,
                    Ciudad = x.Ciudad.Nombre,
                    Provincia = x.Ciudad.Provincia.Nombre,
                    Pais = x.Ciudad.Provincia.Pais.Nombre,
                    Servidor = x.Empleado.Persona.Nombres + " " + x.Empleado.Persona.Nombres,
                    Dependencia = x.Empleado.Dependencia.Nombre,
                    Descripcion = x.Descripcion,
                    Observacion = x.Observacion,
                    Estado = x.Estado,
                    ValorEstimado = x.ValorEstimado
                }
                 ).SingleOrDefaultAsync(m => m.IdSolicitudViatico == viewModelsSolicitudViaticos.IdSolicitudViatico);
                /// busca el puesto del empleado 
                var modalidadpartida = await db.IndiceOcupacionalModalidadPartida.OrderByDescending(x => x.Fecha).Where(m => m.IdEmpleado == NuevoviewModelsSolicitudViaticos.IdEmpleado)
                    .Select(y => new ManualPuesto
                    {
                        Nombre = y.IndiceOcupacional.ManualPuesto.Nombre
                    }
                ).FirstOrDefaultAsync();

                //lista los itinerio de la solicitud
                var ListaItinerario = await db.ItinerarioViatico.Where(m => m.IdSolicitudViatico == NuevoviewModelsSolicitudViaticos.IdSolicitudViatico)
                    .Select(m => new ViewModelsItinerarioViaticos
                    {
                        IdItinerarioViatico=m.IdItinerarioViatico,
                        IdCiudadDestino = m.IdCiudadDestino,
                        IdCiudadOrigen =m.IdCiudadOrigen,
                        Transporte = m.TipoTransporte.Descripcion,
                        Ruta = m.CiudadOrigen.Nombre +"-"+ m.CiudadDestino.Nombre,
                        FechaDesde = m.FechaDesde,
                        HoraSalida = m.HoraSalida,
                        FechaHasta = m.FechaHasta,
                        HoraLlegada = m.HoraLlegada,
                    })
                    .ToListAsync();

                NuevoviewModelsSolicitudViaticos.Puesto = modalidadpartida.Nombre;
                NuevoviewModelsSolicitudViaticos.ListaItinerarioViatico = ListaItinerario;

                return NuevoviewModelsSolicitudViaticos;

            }
            catch (Exception ex)
            {
                return new ViewModelsSolicitudViaticos();
            }
        }
        [HttpGet]
        [Route("ListarSolicitudesViaticos")]
        public async Task<List<SolicitudViatico>> GetSolicitudViatico()
        {
            try
            {
                return await db.SolicitudViatico.OrderBy(x => x.FechaSolicitud).ToListAsync();
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
                return new List<SolicitudViatico>();
            }
        }

        [HttpPost]
        [Route("ListarSolicitudesViaticosPorEmpleado")]
        public async Task<List<SolicitudViatico>> ListarSolicitudesViaticosPorEmpleado([FromBody]Empleado empleado)
        {
            try
            {
                var SolicitudViaticos = new List<SolicitudViatico>();

                SolicitudViaticos = await db.SolicitudViatico
                                    .Where(e => e.IdEmpleado == empleado.IdEmpleado).ToListAsync();

                return SolicitudViaticos;


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
                return new List<SolicitudViatico>();
            }
        }
        [HttpPost]
        [Route("ListarSolicitudesViaticosPorId")]
        public async Task<SolicitudViatico> ListarSolicitudesViaticosPorId([FromBody] SolicitudViatico solicitudViatico)
        {
            try
            {
                var ItinerarioViatico = db.ItinerarioViatico.Where(x => x.IdSolicitudViatico == solicitudViatico.IdSolicitudViatico).ToList();

                var SolicitudViatico = new SolicitudViatico();

                SolicitudViatico = await db.SolicitudViatico.SingleOrDefaultAsync(m => m.IdSolicitudViatico == solicitudViatico.IdSolicitudViatico);

                return SolicitudViatico;

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return new SolicitudViatico();
        }
        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetSolicitudViatico([FromRoute] int id)
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

                var SolicitudViatico = await db.SolicitudViatico.SingleOrDefaultAsync(m => m.IdSolicitudViatico == id);

                if (SolicitudViatico == null)
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
                    Resultado = SolicitudViatico,
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
        public async Task<Response> PutSolicitudViatico([FromRoute] int id, [FromBody] SolicitudViatico solicitudViatico)
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

                var existe = Existe(solicitudViatico);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var solicitudViaticoActualizar = await db.SolicitudViatico.Where(x => x.IdSolicitudViatico == id).FirstOrDefaultAsync();

                if (solicitudViaticoActualizar != null)
                {
                    try
                    {
                        solicitudViaticoActualizar.IdEmpleado = solicitudViatico.IdEmpleado;
                        solicitudViaticoActualizar.IdCiudad = solicitudViatico.IdCiudad;
                        solicitudViaticoActualizar.IdConfiguracionViatico = solicitudViatico.IdConfiguracionViatico;
                        solicitudViaticoActualizar.FechaSolicitud = solicitudViatico.FechaSolicitud;
                        solicitudViaticoActualizar.Descripcion = solicitudViatico.Descripcion;
                        solicitudViaticoActualizar.ValorEstimado = solicitudViatico.ValorEstimado;
                        solicitudViaticoActualizar.FechaLlegada = solicitudViatico.FechaLlegada;
                        solicitudViaticoActualizar.FechaSalida = solicitudViatico.FechaSalida;
                        solicitudViaticoActualizar.Observacion = solicitudViatico.Observacion;
                        solicitudViaticoActualizar.Estado = solicitudViatico.Estado;
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
        [Route("InsertarSolicitudViatico")]
        public async Task<Response> PostSolicitudViatico([FromBody] ViewModelsSolicitudViaticos viewModelsSolicitudViaticos)
        {
            try
            {
                var empleado = await db.Empleado.Where(x => x.IdEmpleado == viewModelsSolicitudViaticos.IdEmpleado).FirstOrDefaultAsync();
                var dias = viewModelsSolicitudViaticos.FechaLlegada.Day - viewModelsSolicitudViaticos.FechaSalida.Day;

                if (empleado.EsJefe == true)
                {
                    if (dias == 0)
                    {
                        dias = 1;
                        viewModelsSolicitudViaticos.ValorEstimado = Convert.ToDecimal(ConstantesViaticos.Jefe) * dias;
                    }
                    viewModelsSolicitudViaticos.ValorEstimado = Convert.ToDecimal(ConstantesViaticos.Jefe) * dias;
                }
                else
                {
                    if (dias == 0)
                    {
                        dias = 1;
                        viewModelsSolicitudViaticos.ValorEstimado = Convert.ToDecimal(ConstantesViaticos.Operativo) * dias;
                    }
                    viewModelsSolicitudViaticos.ValorEstimado = Convert.ToDecimal(ConstantesViaticos.Operativo) * dias;
                }
                var datos = new SolicitudViatico()
                {
                    IdEmpleado = viewModelsSolicitudViaticos.IdEmpleado,
                    IdCiudad = viewModelsSolicitudViaticos.IdCiudad,
                    IdConfiguracionViatico = viewModelsSolicitudViaticos.IdConfiguracionViatico,
                    FechaSolicitud = viewModelsSolicitudViaticos.FechaSolicitud,
                    Descripcion = viewModelsSolicitudViaticos.Descripcion,
                    ValorEstimado = viewModelsSolicitudViaticos.ValorEstimado,
                    FechaLlegada = viewModelsSolicitudViaticos.FechaLlegada,
                    FechaSalida = viewModelsSolicitudViaticos.FechaSalida,
                    Observacion = viewModelsSolicitudViaticos.Observacion,
                    Estado = 0,
                    HoraSalida = viewModelsSolicitudViaticos.HoraSalida,
                    HoraLlegada = viewModelsSolicitudViaticos.HoraLlegada,
                    IdFondoFinanciamiento = viewModelsSolicitudViaticos.IdFondoFinanciamiento
                };

                var respuesta = Existe(datos);
                if (!respuesta.IsSuccess)
                {
                    var solicitudViatico = db.SolicitudViatico.Add(datos);

                    var solTipoViatico = new SolicitudTipoViatico
                    {
                        IdSolicitudViatico = solicitudViatico.Entity.IdSolicitudViatico,
                        IdTipoViatico = viewModelsSolicitudViaticos.IdConfiguracionViatico
                    };
                    db.SolicitudTipoViatico.Add(solTipoViatico);

                    await db.SaveChangesAsync();

                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                        Resultado = solicitudViatico.Entity

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
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }


        [HttpPost]
        [Route("ActualizarEstadoSolicitudViatico")]
        public async Task<Response> ActualizarEstadoSolicitudViatico([FromBody] SolicitudViatico solicitudViatico)
        {
            try
            {
                var solicitudViaticoActualizar = await db.SolicitudViatico.Where(x => x.IdSolicitudViatico == solicitudViatico.IdSolicitudViatico).FirstOrDefaultAsync();

                if (solicitudViaticoActualizar != null)
                {
                    try
                    {

                        solicitudViaticoActualizar.Estado = solicitudViatico.Estado;
                        solicitudViaticoActualizar.IdEmpleadoAprobador = solicitudViatico.IdEmpleadoAprobador;

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


        // DELETE: api/BasesDatos/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteSolicitudViatico([FromRoute] int id)
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
                var respuesta1 = await db.SolicitudTipoViatico.SingleOrDefaultAsync(m => m.IdSolicitudViatico == id);
                if (respuesta1 == null)
                {
                    var respuesta2 = await db.SolicitudViatico.SingleOrDefaultAsync(m => m.IdSolicitudViatico == id);
                    if (respuesta2 == null)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = Mensaje.RegistroNoEncontrado,
                        };
                    }
                    db.SolicitudViatico.Remove(respuesta2);
                    await db.SaveChangesAsync();
                }
                else
                {
                    db.SolicitudTipoViatico.Remove(respuesta1);
                    var respuesta = await db.SolicitudViatico.SingleOrDefaultAsync(m => m.IdSolicitudViatico == id);
                    if (respuesta == null)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = Mensaje.RegistroNoEncontrado,
                        };
                    }
                    db.SolicitudViatico.Remove(respuesta);
                    await db.SaveChangesAsync();
                }
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

        private Response Existe(SolicitudViatico SolicitudViatico)
        {
            var bdd1 = SolicitudViatico.IdEmpleado;
            var bdd4 = SolicitudViatico.IdCiudad;
            var bdd5 = SolicitudViatico.IdConfiguracionViatico;
            var bdd6 = SolicitudViatico.FechaSolicitud;
            //var bdd7 = SolicitudViatico.Descripcion;
            var bdd8 = SolicitudViatico.ValorEstimado;
            var bdd9 = SolicitudViatico.FechaLlegada;
            var bdd10 = SolicitudViatico.FechaSalida;
            var bdd11 = SolicitudViatico.Observacion;
            var bdd12 = SolicitudViatico.Estado;
            var bdd13 = SolicitudViatico.HoraSalida;
            var bdd14 = SolicitudViatico.HoraLlegada;
            var solicitudviaticorespuesta = db.SolicitudViatico.Where(p => p.IdEmpleado == bdd1
            && p.IdCiudad == bdd4
            && p.IdConfiguracionViatico == bdd5
            && p.FechaSolicitud == bdd6
            //&& p.Descripcion == bdd7
            && p.ValorEstimado == bdd8
            && p.FechaLlegada == bdd9
            && p.FechaSalida == bdd10
            && p.Observacion == bdd11
            && p.Estado == bdd12
            && p.HoraSalida == bdd13
            && p.HoraLlegada == bdd14).FirstOrDefault();
            if (solicitudviaticorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = null,
                };

            }

            return new Response
            {
                IsSuccess = false,
            };
        }
    }
}