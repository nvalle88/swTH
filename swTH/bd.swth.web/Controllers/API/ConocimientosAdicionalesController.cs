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
    [Route("api/ConocimientosAdicionales")]
    public class ConocimientosAdicionalesController : Controller
    {
        private readonly SwTHDbContext db;

        public ConocimientosAdicionalesController(SwTHDbContext db)
        {
            this.db = db;
        }


        [HttpPost]
        [Route("ListarConocimientosAdicionalesPorIndiceOcupacional")]
        public async Task<List<ConocimientosAdicionalesViewModel>> ListarConocimientosAdicionalesPorIndiceOcupacional([FromBody]IndiceOcupacional indiceOcupacional)
        {

            var ListaConocimientosAdicionales = await db.IndiceOcupacionalConocimientosAdicionales
                                                .Join(db.IndiceOcupacional
                                                , indiceConocimiento => indiceConocimiento.IdIndiceOcupacional, indice => indice.IdIndiceOcupacional,
                                                (indiceConocimiento, indice) => new { IndiceOcupacionalConocimientosAdicionales = indiceConocimiento, IndiceOcupacional = indice })
                                                .Join(db.ConocimientosAdicionales
                                                , indice_1 => indice_1.IndiceOcupacionalConocimientosAdicionales.IdConocimientosAdicionales, conocimientoAdicional => conocimientoAdicional.IdConocimientosAdicionales,
                                                (indice_1, conocimientoAdicional) => new { ca = indice_1, io = conocimientoAdicional })
                                                .Where(ds => ds.ca.IndiceOcupacional.IdIndiceOcupacional ==indiceOcupacional.IdIndiceOcupacional)
                                                .Select(t => new ConocimientosAdicionalesViewModel
                                                {
                                                    IdConocimientosAdicionales = t.io.IdConocimientosAdicionales,
                                                    Descripcion = t.io.Descripcion,
                                                    IdIndiceOcupacional=t.ca.IndiceOcupacional.IdIndiceOcupacional,
                                                })
                                                .ToListAsync();

            if (ListaConocimientosAdicionales.Count == 0)
            {
                ListaConocimientosAdicionales.Add(new ConocimientosAdicionalesViewModel { IdIndiceOcupacional = indiceOcupacional.IdIndiceOcupacional, IdConocimientosAdicionales = -1 });
            }

            return ListaConocimientosAdicionales;

        }


        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarConocimientosAdicionales")]
        public async Task<List<ConocimientosAdicionales>> GetConocimientosAdicionales()
        {
            try
            {
                return await db.ConocimientosAdicionales.OrderBy(x => x.Descripcion).ToListAsync();
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
                return new List<ConocimientosAdicionales>();
            }
        }


        [HttpPost]
        [Route("ListarConocimientosAdicionalesNoAsignadasIndiceOcupacional")]
        public async Task<List<ConocimientosAdicionales>> ListarConocimientosAdicionalesNoAsignadasIndiceOcupacional([FromBody]IndiceOcupacional indiceOcupacional)
        {
            try
            {
                var Lista = await db.ConocimientosAdicionales
                                   .Where(ac => !db.IndiceOcupacionalConocimientosAdicionales
                                                   .Where(a => a.IndiceOcupacional.IdIndiceOcupacional == indiceOcupacional.IdIndiceOcupacional)
                                                   .Select(ioac => ioac.IdConocimientosAdicionales)
                                                   .Contains(ac.IdConocimientosAdicionales))
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
                return new List<ConocimientosAdicionales>();
            }
        }

        [HttpPost]
        [Route("EliminarIncideOcupacionalConocimientosAdicionales")]
        public async Task<Response> EliminarIncideOcupacionalConocimientosAdicionales([FromBody] IndiceOcupacionalConocimientosAdicionales indiceOcupacionalConocimientosAdicionales)
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

                var respuesta = await db.IndiceOcupacionalConocimientosAdicionales.SingleOrDefaultAsync(m => m.IdConocimientosAdicionales == indiceOcupacionalConocimientosAdicionales.IdConocimientosAdicionales
                                      && m.IdIndiceOcupacional == indiceOcupacionalConocimientosAdicionales.IdIndiceOcupacional);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.IndiceOcupacionalConocimientosAdicionales.Remove(respuesta);
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
        public async Task<Response> GetConocimientosAdicionales([FromRoute] int id)
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

                var ConocimientosAdicionales = await db.ConocimientosAdicionales.SingleOrDefaultAsync(m => m.IdConocimientosAdicionales == id);

                if (ConocimientosAdicionales == null)
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
                    Resultado = ConocimientosAdicionales,
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

        // PUT: api/BasesDatos/5
        [HttpPut("{id}")]
        public async Task<Response> PutConocimientosAdicionales([FromRoute] int id, [FromBody] ConocimientosAdicionales ConocimientosAdicionales)
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


                var existe = Existe(ConocimientosAdicionales);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var ConocimientosAdicionalesActualizar = await db.ConocimientosAdicionales.Where(x => x.IdConocimientosAdicionales == id).FirstOrDefaultAsync();
                if (ConocimientosAdicionalesActualizar != null)
                {
                    try
                    {

                        ConocimientosAdicionalesActualizar.Descripcion = ConocimientosAdicionales.Descripcion;
                        db.ConocimientosAdicionales.Update(ConocimientosAdicionalesActualizar);
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
        [Route("InsertarConocimientosAdicionales")]
        public async Task<Response> PostConocimientosAdicionales([FromBody] ConocimientosAdicionales ConocimientosAdicionales)
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

                var respuesta = Existe(ConocimientosAdicionales);
                if (!respuesta.IsSuccess)
                {
                    db.ConocimientosAdicionales.Add(ConocimientosAdicionales);
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
        public async Task<Response> DeleteConocimientosAdicionales([FromRoute] int id)
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

                var respuesta = await db.ConocimientosAdicionales.SingleOrDefaultAsync(m => m.IdConocimientosAdicionales == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.ConocimientosAdicionales.Remove(respuesta);
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

        private Response Existe(ConocimientosAdicionales ConocimientosAdicionales)
        {
            var bdd = ConocimientosAdicionales.Descripcion.ToUpper().TrimEnd().TrimStart();
            var ConocimientosAdicionalesrespuesta = db.ConocimientosAdicionales.Where(p => p.Descripcion.ToUpper().TrimStart().TrimEnd() == bdd).FirstOrDefault();
            if (ConocimientosAdicionalesrespuesta != null)
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
                Resultado = ConocimientosAdicionalesrespuesta,
            };
        }
    }
}