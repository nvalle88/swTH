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
    [Route("api/IndicesOcupacionalesDeEstudio")]
    public class IndicesOcupacionalesDeEstudioController : Controller
    {
        private readonly SwTHDbContext db;

        public IndicesOcupacionalesDeEstudioController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/IndiceOcupacionalEstudio
        [HttpGet]
        [Route("ListarIndicesOcupacionalesDeEstudio")]
        public async Task<List<IndiceOcupacionalEstudio>> GetIndicesOcupacionalesDeEstudio()
        {
            try
            {
                return await db.IndiceOcupacionalEstudio.Include(x => x.IndiceOcupacional).Include(x => x.Estudio).OrderBy(x => x.IndiceOcupacional).ToListAsync();
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
                return new List<IndiceOcupacionalEstudio>();
            }
        }

        // GET: api/IndiceOcupacionalEstudio
        [HttpGet]
        [Route("ListarIndicesOcupacionalesDeEstudioConId")]
        public async Task<List<IndiceOcupacionalEstudio>> GetIndicesOcupacionalesDeEstudioConId(int codigoIndiceOcupacional)
        {
            try
            {
                return await db.IndiceOcupacionalEstudio.Include(x => x.IndiceOcupacional).Include(x => x.Estudio).OrderBy(x => x.IndiceOcupacional).Where(x=>x.IdIndiceOcupacional== codigoIndiceOcupacional).ToListAsync();
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
                return new List<IndiceOcupacionalEstudio>();
            }
        }



        // GET: api/IndiceOcupacionalEstudio/5
        [HttpGet("{id}")]
        public async Task<Response> GetIndiceOcupacionalEstudio([FromRoute] int id)
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

                var IndiceOcupacionalEstudio = await db.IndiceOcupacionalEstudio.SingleOrDefaultAsync(m => m.IdIndiceOcupacionalEstudio == id);

                if (IndiceOcupacionalEstudio == null)
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
                    Resultado = IndiceOcupacionalEstudio,
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

        // PUT: api/IndiceOcupacionalEstudio/5
        [HttpPut("{id}")]
        public async Task<Response> PutIndiceOcupacionalEstudio([FromRoute] int id, [FromBody] IndiceOcupacionalEstudio IndiceOcupacionalEstudio)
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

                var existe = Existe(IndiceOcupacionalEstudio);
                var IndiceOcupacionalEstudioActualizar = (IndiceOcupacionalEstudio)existe.Resultado;
                if (existe.IsSuccess)
                {
                    if (IndiceOcupacionalEstudioActualizar.IdIndiceOcupacionalEstudio == IndiceOcupacionalEstudio.IdIndiceOcupacionalEstudio)
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
                var indiceOcupacionalEstudio = db.IndiceOcupacionalEstudio.Find(IndiceOcupacionalEstudio.IdIndiceOcupacionalEstudio);

                indiceOcupacionalEstudio.IdIndiceOcupacional = IndiceOcupacionalEstudio.IdIndiceOcupacional;
                indiceOcupacionalEstudio.IdEstudio = IndiceOcupacionalEstudio.IdEstudio;
                db.IndiceOcupacionalEstudio.Update(indiceOcupacionalEstudio);
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

        // POST: api/IndiceOcupacionalEstudio
        [HttpPost]
        [Route("InsertarIndiceOcupacionalEstudio")]
        public async Task<Response> PostIndiceOcupacionalEstudio([FromBody] IndiceOcupacionalEstudio IndiceOcupacionalEstudio)
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

                var respuesta = Existe(IndiceOcupacionalEstudio);
                if (!respuesta.IsSuccess)
                {
                    db.IndiceOcupacionalEstudio.Add(IndiceOcupacionalEstudio);
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

        // DELETE: api/IndiceOcupacionalEstudio/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteIndiceOcupacionalEstudio([FromRoute] int id)
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

                var respuesta = await db.IndiceOcupacionalEstudio.SingleOrDefaultAsync(m => m.IdIndiceOcupacionalEstudio == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.IndiceOcupacionalEstudio.Remove(respuesta);
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

        private Response Existe(IndiceOcupacionalEstudio IndiceOcupacionalEstudio)
        {

            var IndiceOcupacionalEstudiorespuesta = db.IndiceOcupacionalEstudio.Where(p => p.IdIndiceOcupacional == IndiceOcupacionalEstudio.IdIndiceOcupacional &&p.IdEstudio==IndiceOcupacionalEstudio.IdEstudio).FirstOrDefault();
            if (IndiceOcupacionalEstudiorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = IndiceOcupacionalEstudiorespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = IndiceOcupacionalEstudiorespuesta,
            };
        }
    }
}