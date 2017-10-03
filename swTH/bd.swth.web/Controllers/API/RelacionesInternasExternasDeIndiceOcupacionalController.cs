using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.Enumeradores;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.log.guardar.Enumeradores;
using bd.swth.entidades.Utils;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/RelacionesInternasExternasDeIndiceOcupacional")]
    public class RelacionesInternasExternasDeIndiceOcupacionalController : Controller
    {
        private readonly SwTHDbContext db;

        public RelacionesInternasExternasDeIndiceOcupacionalController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/RelacionesInternasExternasIndiceOcupacional
        [HttpGet]
        [Route("ListarRelacionesInternasExternasDeIndiceOcupacional")]
        public async Task<List<RelacionesInternasExternasIndiceOcupacional>> GetRelacionesInternasExternasDeIndiceOcupacional()
        {
            try
            {
                return await db.RelacionesInternasExternasIndiceOcupacional.Include(x => x.RelacionesInternasExternas).Include(x => x.IndiceOcupacional).OrderBy(x => x.IdIndiceOcupacional).ToListAsync();
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
                return new List<RelacionesInternasExternasIndiceOcupacional>();
            }
        }

        // GET: api/RelacionesInternasExternasIndiceOcupacional
        [HttpGet]
        [Route("ListarRelacionesInternasExternasDeIndiceOcupacionalConId")]
        public async Task<List<RelacionesInternasExternasIndiceOcupacional>> GetRelacionesInternasExternasDeIndiceOcupacionalConId(int codigoIndiceOcupacional)
        {
            try
            {
                return await db.RelacionesInternasExternasIndiceOcupacional.Include(x => x.RelacionesInternasExternas).Include(x => x.IndiceOcupacional).Where(x=>x.IdIndiceOcupacional== codigoIndiceOcupacional).OrderBy(x => x.IdIndiceOcupacional).ToListAsync();
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
                return new List<RelacionesInternasExternasIndiceOcupacional>();
            }
        }


        // GET: api/RelacionesInternasExternasIndiceOcupacional/5
        [HttpGet("{id}")]
        public async Task<Response> GetRelacionesInternasExternasIndiceOcupacional([FromRoute] int id)
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

                var RelacionesInternasExternasIndiceOcupacional = await db.RelacionesInternasExternasIndiceOcupacional.SingleOrDefaultAsync(m => m.IdRelacionesInternasExternasIndiceOcupacional == id);

                if (RelacionesInternasExternasIndiceOcupacional == null)
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
                    Resultado = RelacionesInternasExternasIndiceOcupacional,
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

        // PUT: api/RelacionesInternasExternasIndiceOcupacional/5
        [HttpPut("{id}")]
        public async Task<Response> PutRelacionesInternasExternasIndiceOcupacional([FromRoute] int id, [FromBody] RelacionesInternasExternasIndiceOcupacional RelacionesInternasExternasIndiceOcupacional)
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

                var existe = Existe(RelacionesInternasExternasIndiceOcupacional);
                var RelacionesInternasExternasIndiceOcupacionalActualizar = (RelacionesInternasExternasIndiceOcupacional)existe.Resultado;
                if (existe.IsSuccess)
                {
                    if (RelacionesInternasExternasIndiceOcupacionalActualizar.IdRelacionesInternasExternasIndiceOcupacional == RelacionesInternasExternasIndiceOcupacional.IdRelacionesInternasExternasIndiceOcupacional)
                    {
                        return new Response
                        {
                            IsSuccess = true,
                        };
                    }
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }
                var relacionesInternasExternasIndiceOcupacional = db.RelacionesInternasExternasIndiceOcupacional.Find(RelacionesInternasExternasIndiceOcupacional.IdRelacionesInternasExternasIndiceOcupacional);

                relacionesInternasExternasIndiceOcupacional.IdRelacionesInternasExternas = RelacionesInternasExternasIndiceOcupacional.IdRelacionesInternasExternas;
                relacionesInternasExternasIndiceOcupacional.IdIndiceOcupacional = RelacionesInternasExternasIndiceOcupacional.IdIndiceOcupacional;
                db.RelacionesInternasExternasIndiceOcupacional.Update(RelacionesInternasExternasIndiceOcupacional);
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
                    IsSuccess = true,
                    Message = Mensaje.Excepcion,
                };
            }

        }

        // POST: api/RelacionesInternasExternasIndiceOcupacional
        [HttpPost]
        [Route("InsertarRelacionesInternasExternasIndiceOcupacional")]
        public async Task<Response> PostRelacionesInternasExternasIndiceOcupacional([FromBody] RelacionesInternasExternasIndiceOcupacional RelacionesInternasExternasIndiceOcupacional)
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

                var respuesta = Existe(RelacionesInternasExternasIndiceOcupacional);
                if (!respuesta.IsSuccess)
                {
                    db.RelacionesInternasExternasIndiceOcupacional.Add(RelacionesInternasExternasIndiceOcupacional);
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
                    Message = Mensaje.ExisteRegistro,
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

        // DELETE: api/RelacionesInternasExternasIndiceOcupacional/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteRelacionesInternasExternasIndiceOcupacional([FromRoute] int id)
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

                var respuesta = await db.RelacionesInternasExternasIndiceOcupacional.SingleOrDefaultAsync(m => m.IdRelacionesInternasExternasIndiceOcupacional == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.RelacionesInternasExternasIndiceOcupacional.Remove(respuesta);
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

        private Response Existe(RelacionesInternasExternasIndiceOcupacional RelacionesInternasExternasIndiceOcupacional)
        {
  
            var RelacionesInternasExternasIndiceOcupacionalrespuesta = db.RelacionesInternasExternasIndiceOcupacional.Where(p=> p.IdRelacionesInternasExternas == RelacionesInternasExternasIndiceOcupacional.IdRelacionesInternasExternas && p.IdIndiceOcupacional== RelacionesInternasExternasIndiceOcupacional.IdIndiceOcupacional).FirstOrDefault();
            if (RelacionesInternasExternasIndiceOcupacionalrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = RelacionesInternasExternasIndiceOcupacionalrespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = RelacionesInternasExternasIndiceOcupacionalrespuesta,
            };
        }

    }
}