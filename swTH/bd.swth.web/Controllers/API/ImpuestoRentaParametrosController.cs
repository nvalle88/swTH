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

        // GET: api/BasesDatos
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
                    Message = "Se ha producido una excepción",
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new List<ImpuestoRentaParametros>();
            }
        }

        // GET: api/BasesDatos/5
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
                        Message = "Módelo no válido",
                    };
                }

                var ImpuestoRentaParametros = await db.ImpuestoRentaParametros.SingleOrDefaultAsync(m => m.IdImpuestoRentaParametros == id);

                if (ImpuestoRentaParametros == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No encontrado",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Resultado = ImpuestoRentaParametros,
                };
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = "Se ha producido una excepción",
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new Response
                {
                    IsSuccess = false,
                    Message = "Error ",
                };
            }
        }

        // PUT: api/BasesDatos/5
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
                        Message = "Módelo inválido"
                    };
                }

                var existe = Existe(ImpuestoRentaParametros);
                if (existe.IsSuccess)
                {
                    var impustoRenta = (ImpuestoRentaParametros)existe.Resultado;

                    if (id==impustoRenta.IdImpuestoRentaParametros)
                    {
                        var ImpuestoRentaParametrosActualizar = await db.ImpuestoRentaParametros.Where(x => x.IdImpuestoRentaParametros == id).FirstOrDefaultAsync();

                        if (ImpuestoRentaParametrosActualizar != null)
                        {
                            try
                            {
                                ImpuestoRentaParametrosActualizar.FraccionBasica = ImpuestoRentaParametros.FraccionBasica;
                                ImpuestoRentaParametrosActualizar.ExcesoHasta = ImpuestoRentaParametros.ExcesoHasta;
                                ImpuestoRentaParametrosActualizar.ImpuestoFraccionBasica = ImpuestoRentaParametros.ImpuestoFraccionBasica;
                                ImpuestoRentaParametrosActualizar.PorcentajeImpuestoFraccionExcedente = ImpuestoRentaParametros.PorcentajeImpuestoFraccionExcedente;
                                await db.SaveChangesAsync();

                                return new Response
                                {
                                    IsSuccess = true,
                                    Message = "Ok",
                                };

                            }
                            catch (Exception ex)
                            {
                                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                                {
                                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                                    ExceptionTrace = ex,
                                    Message = "Se ha producido una excepción",
                                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                                    UserName = "",

                                });
                                return new Response
                                {
                                    IsSuccess = false,
                                    Message = "Error ",
                                };
                            }
                        }

                    }
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Existe un registro de igual Fracción Básica",
                    };
                }
                

                return new Response
                {
                    IsSuccess = false,
                    Message = "Existe"
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Excepción"
                };
            }
        }

        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarImpuestoRentaParametros")]
        public async Task<Response> PostImpuestoRentaParametros([FromBody] ImpuestoRentaParametros ImpuestoRentaParametros)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Módelo inválido"
                    };
                }

                var respuesta = Existe(ImpuestoRentaParametros);
                if (!respuesta.IsSuccess)
                {
                    db.ImpuestoRentaParametros.Add(ImpuestoRentaParametros);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = "OK"
                    };
                }

                return new Response
                {
                    IsSuccess = false,
                    Message = "Existe un registro de igual Fracción Básica..."
                };

            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = "Se ha producido una excepción",
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new Response
                {
                    IsSuccess = false,
                    Message = "Error ",
                };
            }
        }

        // DELETE: api/BasesDatos/5
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
                        Message = "Módelo no válido ",
                    };
                }

                var respuesta = await db.ImpuestoRentaParametros.SingleOrDefaultAsync(m => m.IdImpuestoRentaParametros == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No existe ",
                    };
                }
                db.ImpuestoRentaParametros.Remove(respuesta);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = "Eliminado ",
                };
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = "Se ha producido una excepción",
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new Response
                {
                    IsSuccess = false,
                    Message = "Error ",
                };
            }
        }

        private Response Existe(ImpuestoRentaParametros ImpuestoRentaParametros)
        {
            var bdd = ImpuestoRentaParametros.FraccionBasica;
            var ImpuestoRentaParametrosrespuesta = db.ImpuestoRentaParametros.Where(p => p.FraccionBasica == bdd).FirstOrDefault();
            if (ImpuestoRentaParametrosrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Existe un Impuesto Renta Parámetros de igual nombre",
                    Resultado = ImpuestoRentaParametros,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = ImpuestoRentaParametrosrespuesta,
            };
        }


    }
}