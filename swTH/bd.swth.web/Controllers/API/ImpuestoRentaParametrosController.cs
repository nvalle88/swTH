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
using bd.swth.entidades.Negocio;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/ImpuestoRentaParametros")]
    public class ImpuestoRentaParametrosController : Controller
    {
        private readonly SwTHDbContext db;

        public ImpuestoRentaParametrosController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/ImpuestoRentaParametross
        [HttpGet]
        [Route("ListarImpuestoRentaParametros")]
        public async Task<List<ImpuestoRentaParametros>> GetImpuestoRentaParametros()
        {
            try
            {
                return await db.ImpuestoRentaParametros.OrderBy(x => x.FraccionBasica).ToListAsync();
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = "Se ha producido una exepción",
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new List<ImpuestoRentaParametros>();
            }
        }

        // GET: api/ImpuestoRentaParametross/5
        [HttpGet("{id}")]
        public async Task<Response> GetImpuestoRentaParametros([FromRoute] int id)
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

                var adscbdd = await db.ImpuestoRentaParametros.SingleOrDefaultAsync(m => m.IdImpuestoRentaParametros == id);

                if (adscbdd == null)
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
                    Resultado = adscbdd,
                };
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = "Se ha producido una exepción",
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

        // PUT: api/ImpuestoRentaParametross/5
        [HttpPut("{id}")]
        public async Task<Response> PutImpuestoRentaParametros([FromRoute] int id, [FromBody] ImpuestoRentaParametros ImpuestoRentaParametros)
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


                try
                {
                    var entidad = await db.ImpuestoRentaParametros.Where(x => x.IdImpuestoRentaParametros == id).FirstOrDefaultAsync();

                    if (entidad == null)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "No existe información acerca del ImpuestoRentaParametros ",
                        };

                    }
                    else
                    {

                        entidad.FraccionBasica = ImpuestoRentaParametros.FraccionBasica;
                        entidad.ExcesoHasta = ImpuestoRentaParametros.ExcesoHasta;
                        entidad.ImpuestoFraccionBasica = ImpuestoRentaParametros.ImpuestoFraccionBasica;
                        entidad.PorcentajeImpuestoFraccionExcedente = ImpuestoRentaParametros.PorcentajeImpuestoFraccionExcedente;
                        db.ImpuestoRentaParametros.Update(entidad);
                        await db.SaveChangesAsync();
                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.Satisfactorio,
                        };
                    }


                }
                catch (Exception ex)
                {
                    await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                    {
                        ApplicationName = Convert.ToString(Aplicacion.SwTH),
                        ExceptionTrace = ex,
                        Message = "Se ha producido una exepción",
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
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                     Message = Mensaje.Excepcion
                };
            }
        }

        // POST: api/ImpuestoRentaParametross
        [HttpPost]
        [Route("InsertarImpuestoRentaParametros")]
        public async Task<Response> PostImpuestoRentaParametros([FromBody] ImpuestoRentaParametros ImpuestoRentaParametros)
        {
            try
            {

                var respuesta = Existe(ImpuestoRentaParametros.FraccionBasica);
                if (!respuesta.IsSuccess)
                {
                    db.ImpuestoRentaParametros.Add(ImpuestoRentaParametros);
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
                    Message = Mensaje.Satisfactorio
                };

            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = "Se ha producido una exepción",
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

        // DELETE: api/ImpuestoRentaParametross/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteImpuestoRentaParametros([FromRoute] int id)
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

                var respuesta = await db.ImpuestoRentaParametros.SingleOrDefaultAsync(m => m.IdImpuestoRentaParametros == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.ImpuestoRentaParametros.Remove(respuesta);
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
                    Message = "Se ha producido una exepción",
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

        private bool ImpuestoRentaParametrosExists(int id)
        {
            return db.ImpuestoRentaParametros.Any(e => e.IdImpuestoRentaParametros == id);
        }


        public Response Existe(decimal fraccionbasica)
        {

            var loglevelrespuesta = db.ImpuestoRentaParametros.Where(p => p.FraccionBasica == fraccionbasica).FirstOrDefault();
            if (loglevelrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Existe un sistema de igual nombre",
                    Resultado = null,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = loglevelrespuesta,
            };
        }
    }
}