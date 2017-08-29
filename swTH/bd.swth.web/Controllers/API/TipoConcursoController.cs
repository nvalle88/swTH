using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using bd.log.guardar.Utiles;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using Microsoft.EntityFrameworkCore;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.swth.entidades.Enumeradores;
using bd.log.guardar.Enumeradores;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/TipoConcurso")]
    public class TipoConcursoController : Controller
    {
        private readonly SwTHDbContext db;

        public TipoConcursoController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarTipoConcurso")]
        public async Task<List<TipoConcurso>> GetTipoConcurso()
        {
            try
            {
                return await db.TipoConcurso.OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<TipoConcurso>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetTipoConcurso([FromRoute] int id)
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

                var TipoConcurso = await db.TipoConcurso.SingleOrDefaultAsync(m => m.IdTipoConcurso == id);

                if (TipoConcurso == null)
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
                    Resultado = TipoConcurso,
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
        public async Task<Response> PutTipoConcurso([FromRoute] int id, [FromBody] TipoConcurso TipoConcurso)
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

                var TipoConcursoActualizar = await db.TipoConcurso.Where(x => x.IdTipoConcurso == id).FirstOrDefaultAsync();
                if (TipoConcursoActualizar != null)
                {
                    try
                    {
                        TipoConcursoActualizar.Nombre = TipoConcurso.Nombre;
                        TipoConcursoActualizar.Descripcion = TipoConcurso.Descripcion;
                        TipoConcursoActualizar.FaseConcurso = TipoConcurso.FaseConcurso;
                        
                        db.TipoConcurso.Update(TipoConcursoActualizar);
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
        [Route("InsertarTipoConcurso")]
        public async Task<Response> PostTipoConcurso([FromBody] TipoConcurso TipoConcurso)
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

                var respuesta = Existe(TipoConcurso);
                if (!respuesta.IsSuccess)
                {
                    db.TipoConcurso.Add(TipoConcurso);
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
                    Message = "OK"
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
        public async Task<Response> DeleteTipoConcurso([FromRoute] int id)
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

                var respuesta = await db.TipoConcurso.SingleOrDefaultAsync(m => m.IdTipoConcurso == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No existe ",
                    };
                }
                db.TipoConcurso.Remove(respuesta);
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

        private Response Existe(TipoConcurso TipoConcurso)
        {
            var bdd = TipoConcurso.Nombre.ToUpper().TrimEnd().TrimStart();
            var TipoConcursorespuesta = db.TipoConcurso.Where(p => p.Nombre.ToUpper().TrimStart().TrimEnd() == bdd).FirstOrDefault();
            if (TipoConcursorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = String.Format("Ya existe un Tipo de Concurso con el nombre {0}", TipoConcurso.Nombre),
                    Resultado = null,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = TipoConcursorespuesta,
            };
        }
    }
}