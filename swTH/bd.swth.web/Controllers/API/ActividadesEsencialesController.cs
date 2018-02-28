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

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/ActividadesEsenciales")]
    public class ActividadesEsencialesController : Controller
    {
        private readonly SwTHDbContext db;

        public ActividadesEsencialesController(SwTHDbContext db)
        {
            this.db = db;
        }


        [HttpPost]
        [Route("ListarActividadesEsencialesPorIndiceOcupacional")]
        public async Task<List<ActividadesEsencialesViewModel>> ListarActividadesEsencialesIndiceOcupacional([FromBody] IndiceOcupacional indiceOcupacional)
        {
            var listaActividadesEsenciales = await db.IndiceOcupacionalActividadesEsenciales
                                                        .Join(db.IndiceOcupacional
                                                        , indice => indice.IdIndiceOcupacional, ocupacional => ocupacional.IdIndiceOcupacional,
                                                        (indice, ocupacional) => new { IndiceOcupacionalActividadesEsenciales = indice, IndiceOcupacional = ocupacional })
                                                        .Join(db.ActividadesEsenciales
                                                        , indice1 => indice1.IndiceOcupacionalActividadesEsenciales.IdActividadesEsenciales, a => a.IdActividadesEsenciales,
                                                        (indice1, a) => new { z = indice1, s = a })
                                                        .Where(ds => ds.z.IndiceOcupacional.IdIndiceOcupacional == indiceOcupacional.IdIndiceOcupacional)
                                                        .Select(t => new ActividadesEsencialesViewModel
                                                        {
                                                            IdActividadesEsenciales = t.s.IdActividadesEsenciales,
                                                            Descripcion = t.s.Descripcion,
                                                            IdIndiceOcupacional=indiceOcupacional.IdIndiceOcupacional,
                                                        })
                                                        .ToListAsync();

            if (listaActividadesEsenciales.Count == 0)
            {
                listaActividadesEsenciales.Add(new ActividadesEsencialesViewModel { IdIndiceOcupacional = indiceOcupacional.IdIndiceOcupacional,IdActividadesEsenciales=-1 });
            }

            return listaActividadesEsenciales;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarActividadesEsenciales")]
        public async Task<List<ActividadesEsenciales>> GetActividadesEsenciales()
        {
            try
            {
                return await db.ActividadesEsenciales.OrderBy(x => x.Descripcion).ToListAsync();
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = Mensaje.Excepcion,
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new List<ActividadesEsenciales>();
            }
        }

        [HttpPost]
        [Route("ListarActividedesEsencialesNoAsignadasIndiceOcupacional")]
        public async Task<List<ActividadesEsenciales>> ListarActividedesEsencialesNoAsignadasIndiceOcupacional([FromBody]IndiceOcupacional indiceOcupacional)
        {
            try
            {
                var Lista = await db.ActividadesEsenciales
                                   .Where(ac => !db.IndiceOcupacionalActividadesEsenciales
                                                   .Where(a => a.IndiceOcupacional.IdIndiceOcupacional == indiceOcupacional.IdIndiceOcupacional)
                                                   .Select(ioac => ioac.IdActividadesEsenciales)
                                                   .Contains(ac.IdActividadesEsenciales))
                                          .ToListAsync();
                return Lista;
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = Mensaje.Excepcion,
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new List<ActividadesEsenciales>();
            }
        }


        [HttpPost]
        [Route("EliminarIncideOcupacionalActividadesEsenciales")]
        public async Task<Response> EliminarIncideOcupacionalActividadesEsenciales([FromBody] IndiceOcupacionalActividadesEsenciales indiceOcupacionalActividadesEsenciales)
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

                var respuesta = await db.IndiceOcupacionalActividadesEsenciales.SingleOrDefaultAsync(m => m.IdActividadesEsenciales == indiceOcupacionalActividadesEsenciales.IdActividadesEsenciales
                                      && m.IdIndiceOcupacional == indiceOcupacionalActividadesEsenciales.IdIndiceOcupacional);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.IndiceOcupacionalActividadesEsenciales.Remove(respuesta);
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
                    ExceptionTrace = ex,
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


        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetActividadesEsenciales([FromRoute] int id)
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

                var ActividadesEsenciales = await db.ActividadesEsenciales.SingleOrDefaultAsync(m => m.IdActividadesEsenciales == id);

                if (ActividadesEsenciales == null)
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
                    Resultado = ActividadesEsenciales,
                };
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = Mensaje.Excepcion,
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Satisfactorio,
                };


            }

        }
        // PUT: api/BasesDatos/5
        [HttpPut("{id}")]
        public async Task<Response> PutActividadesEsenciales([FromRoute] int id, [FromBody] ActividadesEsenciales ActividadesEsenciales)
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


                var existe = Existe(ActividadesEsenciales);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var ActividadesEsencialesActualizar = await db.ActividadesEsenciales.Where(x => x.IdActividadesEsenciales == id).FirstOrDefaultAsync();
                if (ActividadesEsencialesActualizar != null)
                {
                    try
                    {

                        ActividadesEsencialesActualizar.Descripcion = ActividadesEsenciales.Descripcion;
                        db.ActividadesEsenciales.Update(ActividadesEsencialesActualizar);
                        await db.SaveChangesAsync();

                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.Satisfactorio,
                            Resultado = ActividadesEsencialesActualizar
                        };

                    }
                    catch (Exception ex)
                    {
                        await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                        {
                            ApplicationName = Convert.ToString(Aplicacion.SwTH),
                            ExceptionTrace = ex,
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
        [Route("InsertarActividadesEsenciales")]
        public async Task<Response> PostActividadesEsenciales([FromBody] ActividadesEsenciales ActividadesEsenciales)
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

                var respuesta = Existe(ActividadesEsenciales);
                if (!respuesta.IsSuccess)
                {
                    db.ActividadesEsenciales.Add(ActividadesEsenciales);
                    await db.SaveChangesAsync();


                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                        Resultado= ActividadesEsenciales
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
                    ExceptionTrace = ex,
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
        public async Task<Response> DeleteActividadesEsenciales([FromRoute] int id)
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

                var respuesta = await db.ActividadesEsenciales.SingleOrDefaultAsync(m => m.IdActividadesEsenciales == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.ActividadesEsenciales.Remove(respuesta);
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
                    ExceptionTrace = ex,
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
        [Route("EliminarIndiceOcupacionalActividadesEsenciales")]
        public async Task<Response> EliminarIndiceOcupacionalActividadesEsenciales([FromBody] IndiceOcupacionalActividadesEsenciales indiceOcupacionalActividadesEsenciales)
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

                var respuesta = await db.IndiceOcupacionalActividadesEsenciales.SingleOrDefaultAsync(m => m.IdActividadesEsenciales == indiceOcupacionalActividadesEsenciales.IdActividadesEsenciales && m.IdIndiceOcupacional == indiceOcupacionalActividadesEsenciales.IdIndiceOcupacional);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.IndiceOcupacionalActividadesEsenciales.Remove(respuesta);
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
                    ExceptionTrace = ex,
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

        private Response Existe(ActividadesEsenciales ActividadesEsenciales)
        {
            var bdd = ActividadesEsenciales.Descripcion;
            var ActividadesEsencialesrespuesta = db.ActividadesEsenciales.Where(p => p.Descripcion.Equals(bdd)).FirstOrDefault();
            if (ActividadesEsencialesrespuesta != null)
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
                Resultado = ActividadesEsencialesrespuesta,
            };
        }
    }
}