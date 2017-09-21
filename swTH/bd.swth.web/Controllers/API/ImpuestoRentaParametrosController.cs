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
                    Message = Mensaje.Excepcion,
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
                        Message = Mensaje.ModeloInvalido,
                    };
                }

                var ImpuestoRentaParametros = await db.ImpuestoRentaParametros.SingleOrDefaultAsync(m => m.IdImpuestoRentaParametros == id);

                if (ImpuestoRentaParametros == null)
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
                    Resultado = ImpuestoRentaParametros,
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

        private async Task Actualizar(ImpuestoRentaParametros ImpuestoRentaParametros)
        {
            var impuestorentaparametros = db.ImpuestoRentaParametros.Find(ImpuestoRentaParametros.IdImpuestoRentaParametros);
           
            impuestorentaparametros.ExcesoHasta = ImpuestoRentaParametros.ExcesoHasta;
            impuestorentaparametros.FraccionBasica = ImpuestoRentaParametros.FraccionBasica;
            impuestorentaparametros.ImpuestoFraccionBasica = ImpuestoRentaParametros.ImpuestoFraccionBasica;
            impuestorentaparametros.PorcentajeImpuestoFraccionExcedente = ImpuestoRentaParametros.PorcentajeImpuestoFraccionExcedente;
            db.ImpuestoRentaParametros.Update(impuestorentaparametros);
            await db.SaveChangesAsync();
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
                        Message = Mensaje.ModeloInvalido
                    };
                }
                //fraccionBasica, ExcesoHasta, ImpuestoFraccionBasica, PorcentajeImpuestoFraccionExcedente

                if (ImpuestoRentaParametros.FraccionBasica > ImpuestoRentaParametros.ExcesoHasta || ImpuestoRentaParametros.FraccionBasica == ImpuestoRentaParametros.ExcesoHasta)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fracción básica no puede ser mayor o igual a exceso hasta"
                    };
                }

                var existe = Existe(ImpuestoRentaParametros);
                var ImpuestoRentaParametrosActualizar = (ImpuestoRentaParametros)existe.Resultado;

                if (existe.IsSuccess)
                {
                    //fraccionBasica, ExcesoHasta, ImpuestoFraccionBasica, PorcentajeImpuestoFraccionExcedente


                    if (ImpuestoRentaParametrosActualizar.IdImpuestoRentaParametros == ImpuestoRentaParametros.IdImpuestoRentaParametros)
                    {
                        if (ImpuestoRentaParametros.ExcesoHasta == ImpuestoRentaParametrosActualizar.ExcesoHasta &&
                        ImpuestoRentaParametros.FraccionBasica == ImpuestoRentaParametrosActualizar.FraccionBasica &&
                        ImpuestoRentaParametros.ImpuestoFraccionBasica == ImpuestoRentaParametrosActualizar.ImpuestoFraccionBasica &&
                        ImpuestoRentaParametros.PorcentajeImpuestoFraccionExcedente == ImpuestoRentaParametrosActualizar.PorcentajeImpuestoFraccionExcedente)
                        {
                            return new Response
                            {
                                IsSuccess = true,
                            };
                        }

                        await Actualizar(ImpuestoRentaParametros);
                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.Satisfactorio,
                        };
                    }
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                await Actualizar(ImpuestoRentaParametros);
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
                    IsSuccess = true,
                    Message = Mensaje.Excepcion,
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
                //fraccionBasica, ExcesoHasta, ImpuestoFraccionBasica, PorcentajeImpuestoFraccionExcedente

                if (ImpuestoRentaParametros.FraccionBasica > ImpuestoRentaParametros.ExcesoHasta || ImpuestoRentaParametros.FraccionBasica == ImpuestoRentaParametros.ExcesoHasta)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fracción básica  no puede ser mayor o igual al exceso hasta"
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
                        Message = Mensaje.Satisfactorio
                    };
                }

                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = ""
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

        private Response Existe(ImpuestoRentaParametros ImpuestoRentaParametros)
        {
            var bdd = ImpuestoRentaParametros.FraccionBasica;
            var ImpuestoRentaParametrosrespuesta = db.ImpuestoRentaParametros.Where(p => p.FraccionBasica == bdd).FirstOrDefault();
            if (ImpuestoRentaParametrosrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = ImpuestoRentaParametrosrespuesta,
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