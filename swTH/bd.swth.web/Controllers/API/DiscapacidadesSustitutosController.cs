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

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/DiscapacidadesSustitutos")]
    public class DiscapacidadesSustitutosController : Controller
    {
        private readonly SwTHDbContext db;

        public DiscapacidadesSustitutosController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarDiscapacidadesSustituto")]
        public async Task<List<DiscapacidadSustituto>> GetDiscapacidadSustituto()
        {
            try
            {
                return await db.DiscapacidadSustituto.OrderBy(x => x.IdTipoDiscapacidad).ToListAsync();
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
                return new List<DiscapacidadSustituto>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetDiscapacidadSustituto([FromRoute] int id)
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

                var DiscapacidadSustituto = await db.DiscapacidadSustituto.SingleOrDefaultAsync(m => m.IdDiscapacidadSustituto == id);

                if (DiscapacidadSustituto == null)
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
                    Resultado = DiscapacidadSustituto,
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
        public async Task<Response> PutDiscapacidadSustituto([FromRoute] int id, [FromBody] DiscapacidadSustituto DiscapacidadSustituto)
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

                var existe = Existe(DiscapacidadSustituto);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var DiscapacidadSustitutoActualizar = await db.DiscapacidadSustituto.Where(x => x.IdDiscapacidadSustituto == id).FirstOrDefaultAsync();

                if (DiscapacidadSustitutoActualizar != null)
                {
                    try
                    {
                        DiscapacidadSustitutoActualizar.IdTipoDiscapacidad = DiscapacidadSustituto.IdTipoDiscapacidad;
                        DiscapacidadSustitutoActualizar.PorcentajeDiscapacidad = DiscapacidadSustituto.PorcentajeDiscapacidad;
                        DiscapacidadSustitutoActualizar.NumeroCarnet = DiscapacidadSustituto.NumeroCarnet;
                        DiscapacidadSustitutoActualizar.IdPersonaSustituto = DiscapacidadSustituto.IdPersonaSustituto;
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
        [Route("InsertarDiscapacidadSustituto")]
        public async Task<Response> PostDiscapacidadSustituto([FromBody] DiscapacidadSustituto DiscapacidadSustituto)
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

                var respuesta = Existe(DiscapacidadSustituto);
                if (!respuesta.IsSuccess)
                {
                    db.DiscapacidadSustituto.Add(DiscapacidadSustituto);
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
        public async Task<Response> DeleteDiscapacidadSustituto([FromRoute] int id)
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

                var respuesta = await db.DiscapacidadSustituto.SingleOrDefaultAsync(m => m.IdDiscapacidadSustituto == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.DiscapacidadSustituto.Remove(respuesta);
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

        private Response Existe(DiscapacidadSustituto DiscapacidadSustituto)
        {
            var bdd1 = DiscapacidadSustituto.IdTipoDiscapacidad;
            var bdd2 = DiscapacidadSustituto.PorcentajeDiscapacidad;
            var bdd3 = DiscapacidadSustituto.NumeroCarnet;
            var bdd4 = DiscapacidadSustituto.IdPersonaSustituto;
            var DiscapacidadSustitutorespuesta = db.DiscapacidadSustituto.Where(p => p.IdTipoDiscapacidad== bdd1
            && p.PorcentajeDiscapacidad == bdd2
            && p.NumeroCarnet == bdd3
            && p.IdPersonaSustituto == bdd4).FirstOrDefault();
            if (DiscapacidadSustitutorespuesta != null)
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
                Resultado = DiscapacidadSustitutorespuesta,
            };
        }
    }
}