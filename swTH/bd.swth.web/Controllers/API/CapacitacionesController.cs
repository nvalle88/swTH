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
using bd.log.guardar.Enumeradores;
using bd.swth.entidades.Utils;
using bd.swth.entidades.ViewModels;

namespace bd.swth.web.Controllers
{
    [Produces("application/json")]
    [Route("api/Capacitaciones")]
    public class CapacitacionesController : Controller
    {
        private readonly SwTHDbContext db;

        public CapacitacionesController(SwTHDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        [Route("ListarCapacitacionesPorIndiceOcupacional")]
        public async Task<List<CapacitacionViewModel>> ListarCapacitacionesPorIndiceOcupacional([FromBody] IndiceOcupacional indiceOcupacional)
        {
            var ListaCapacitaciones = await db.IndiceOcupacionalCapacitaciones
                                                .Join(db.IndiceOcupacional
                                                , indiceCapacitaciones => indiceCapacitaciones.IdIndiceOcupacional, indice => indice.IdIndiceOcupacional,
                                                (indiceActEsenciales, indice) => new { IndiceOcupacionalActividadesEsenciales = indiceActEsenciales, IndiceOcupacional = indice })
                                                .Join(db.Capacitacion
                                                , indice_1 => indice_1.IndiceOcupacionalActividadesEsenciales.IdCapacitacion, capacitacion => capacitacion.IdCapacitacion,
                                                (indice_1, capacitacion) => new { ca = indice_1, rt = capacitacion })
                                                .Where(ds => ds.ca.IndiceOcupacional.IdIndiceOcupacional == indiceOcupacional.IdIndiceOcupacional)
                                                .Select(t => new CapacitacionViewModel
                                                {
                                                    IdCapacitacion = t.rt.IdCapacitacion,
                                                    Nombre = t.rt.Nombre,
                                                    IdIndiceOcupacional=indiceOcupacional.IdIndiceOcupacional,
                                                })
                                                .ToListAsync();
            if (ListaCapacitaciones.Count == 0)
            {
                ListaCapacitaciones.Add(new CapacitacionViewModel { IdIndiceOcupacional = indiceOcupacional.IdIndiceOcupacional, IdCapacitacion = -1 });
            }

            return ListaCapacitaciones;

        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarCapacitaciones")]
        public async Task<List<Capacitacion>> GetCapacitaciones()
        {
            try
            {
                return await db.Capacitacion.OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<Capacitacion>();
            }
        }

        [HttpPost]
        [Route("ListarCapacitacionesNoAsignadasIndiceOcupacional")]
        public async Task<List<CapacitacionViewModel>> ListarCapacitacionesNoAsignadasIndiceOcupacional([FromBody]IndiceOcupacional indiceOcupacional)
        {

                var Lista = await db.Capacitacion
                                   .Where(ac => !db.IndiceOcupacionalCapacitaciones
                                                   .Where(a => a.IndiceOcupacional.IdIndiceOcupacional == indiceOcupacional.IdIndiceOcupacional)
                                                   .Select(ioac => ioac.IdCapacitacion)
                                                   .Contains(ac.IdCapacitacion))
                                          .ToListAsync();
                var listaSalida = new List<CapacitacionViewModel>();

                if (Lista.Count == 0)
                {
                    listaSalida.Add(new CapacitacionViewModel { IdIndiceOcupacional = indiceOcupacional.IdIndiceOcupacional, IdCapacitacion = -1 });
                }

                else
                {
                    foreach (var item in Lista)
                    {
                        listaSalida.Add(new CapacitacionViewModel
                        {
                            Nombre = item.Nombre,
                            IdCapacitacion = item.IdCapacitacion,
                            IdIndiceOcupacional = indiceOcupacional.IdIndiceOcupacional,
                        });
                    }
                }

                return listaSalida;
          
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetCapacitacion([FromRoute] int id)
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

                var Capacitacion = await db.Capacitacion.SingleOrDefaultAsync(m => m.IdCapacitacion == id);

                if (Capacitacion == null)
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
                    Resultado = Capacitacion,
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
        public async Task<Response> PutCapacitacion([FromRoute] int id, [FromBody] Capacitacion Capacitacion)
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

                var existe = Existe(Capacitacion);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var CapacitacionActualizar = await db.Capacitacion.Where(x => x.IdCapacitacion == id).FirstOrDefaultAsync();
                if (CapacitacionActualizar != null)
                {
                    try
                    {

                        CapacitacionActualizar.Nombre = Capacitacion.Nombre;
                        db.Capacitacion.Update(CapacitacionActualizar);
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
        [Route("InsertarCapacitacion")]
        public async Task<Response> PostCapacitacion([FromBody] Capacitacion Capacitacion)
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

                var respuesta = Existe(Capacitacion);
                if (!respuesta.IsSuccess)
                {
                    db.Capacitacion.Add(Capacitacion);
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
                    Message = "Existe una capacitación de igual nombre"
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
        [HttpPost]
        [Route("EliminarIncideOcupacionalCapacitaciones")]
        public async Task<Response> EliminarIndiceOcupacionalCapacitaciones([FromBody] IndiceOcupacionalCapacitaciones indiceOcupacionalCapacitaciones)
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

                var respuesta = await db.IndiceOcupacionalCapacitaciones.SingleOrDefaultAsync(m => m.IdCapacitacion == indiceOcupacionalCapacitaciones.IdCapacitacion && m.IdIndiceOcupacional == indiceOcupacionalCapacitaciones.IdIndiceOcupacional);

                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.IndiceOcupacionalCapacitaciones.Remove(respuesta);
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

        // DELETE: api/BasesDatos/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteCapacitacion([FromRoute] int id)
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

                var respuesta = await db.Capacitacion.SingleOrDefaultAsync(m => m.IdCapacitacion == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.Capacitacion.Remove(respuesta);
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

        private Response Existe(Capacitacion Capacitacion)
        {
            var bdd = Capacitacion.Nombre.ToUpper().TrimEnd().TrimStart();
            var Capacitacionrespuesta = db.Capacitacion.Where(p => p.Nombre.ToUpper().TrimStart().TrimEnd() == bdd).FirstOrDefault();
            if (Capacitacionrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Existe una capacitación de igual nombre",
                    Resultado = null,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = Capacitacionrespuesta,
            };
        }
    }
}