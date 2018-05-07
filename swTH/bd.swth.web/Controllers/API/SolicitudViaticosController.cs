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
    [Route("api/SolicitudViaticos")]
    public class SolicitudViaticosController : Controller
    {
        private readonly SwTHDbContext db;

        public SolicitudViaticosController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
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
            //Persona persona = new Persona();
            try
            {
                var SolicitudViaticos = new List<SolicitudViatico>();
                //var SolicitudViaticos1 = new List<SolicitudViatico>();
                //var emple = await db.Empleado.Where(x => x.IdEmpleado == empleado.IdEmpleado && x.EsJefe == true && x.Activo == true).FirstOrDefaultAsync();
                //if (emple != null)
                //{

                //    var emplea = await db.Empleado.Where(x => x.IdDependencia == emple.IdDependencia && x.Activo == true).ToListAsync();
                //    foreach (var item in emplea)
                //    {
                //        SolicitudViaticos1 = await db.SolicitudViatico
                //                                       .Where(e => e.IdEmpleado == item.IdEmpleado).ToListAsync();
                //        if (SolicitudViaticos1.Count != 0) {

                //            SolicitudViaticos.AddRange(SolicitudViaticos1);
                //        }

                //    }
                //}
                //else
                //{
                    SolicitudViaticos = await db.SolicitudViatico
                                        .Where(e => e.IdEmpleado == empleado.IdEmpleado).ToListAsync();
                    //var empl = new Empleado { IdEmpleado = Empleado.IdEmpleado };
                //}
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
                        solicitudViaticoActualizar.IdPais = solicitudViatico.IdPais;
                        solicitudViaticoActualizar.IdProvincia = solicitudViatico.IdProvincia;
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
        public async Task<Response> PostSolicitudViatico([FromBody] SolicitudViaticoViewModel solicitudViaticoViewModel)
        {
            try
            {
                var respuesta = Existe(solicitudViaticoViewModel.SolicitudViatico);
                if (!respuesta.IsSuccess)
                {
                    var solicitudViatico = db.SolicitudViatico.Add(solicitudViaticoViewModel.SolicitudViatico);
                    await db.SaveChangesAsync();

                    //solicitudViaticoViewModel.SolicitudTipoViatico. = solicitudViatico.Entity.IdSolicitudViatico;


                    foreach (var item in solicitudViaticoViewModel.ViaticosSeleccionados)
                    {


                        var solTipoViatico = new SolicitudTipoViatico
                        {
                            IdSolicitudViatico = solicitudViatico.Entity.IdSolicitudViatico,
                            IdTipoViatico = Convert.ToInt32(item)
                        };
                        db.SolicitudTipoViatico.Add(solTipoViatico);
                    }

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

        [HttpPost]
        [Route("ActualizarValorTotalViatico")]
        public async Task<Response> PutValorTotalViatico([FromBody] SolicitudViatico solicitudViatico)
        {
            Decimal valor = new Decimal();
            try
            {


                var solicitudViaticoActualizar = await db.SolicitudViatico.Where(x => x.IdSolicitudViatico == solicitudViatico.IdSolicitudViatico).FirstOrDefaultAsync();

                if (solicitudViaticoActualizar != null)
                {

                    var listaItinerarios = await db.ItinerarioViatico.Where(x => x.IdSolicitudViatico == solicitudViatico.IdSolicitudViatico).ToListAsync();


                    foreach (var item in listaItinerarios)
                    {
                        valor = valor + item.Valor;
                    }


                    try
                    {
                        solicitudViaticoActualizar.ValorEstimado = valor;
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

        [HttpPost]
        [Route("ActualizarEstadoSolicitudViatico")]
        public async Task<Response> ActualizarEstadoSolicitudViatico([FromBody] SolicitudViaticoViewModel solicitudViaticoViewModel)
        {
            try
            {


                var solicitudViaticoActualizar = await db.SolicitudViatico.Where(x => x.IdSolicitudViatico == solicitudViaticoViewModel.SolicitudViatico.IdSolicitudViatico).FirstOrDefaultAsync();

                if (solicitudViaticoActualizar != null)
                {
                    try
                    {

                        solicitudViaticoActualizar.Estado = solicitudViaticoViewModel.SolicitudViatico.Estado;
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
            var bdd2 = SolicitudViatico.IdPais;
            var bdd3 = SolicitudViatico.IdProvincia;
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
            && p.IdPais == bdd2
            && p.IdProvincia == bdd3
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
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = null,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = solicitudviaticorespuesta,
            };
        }
    }
}