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
    [Route("api/Titulos")]
    public class TitulosController : Controller
    {
        private readonly SwTHDbContext db;

        public TitulosController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarTitulos")]
        public async Task<List<Titulo>> GetTitulos()
        {
            try
            {
                return await db.Titulo.Include(x => x.AreaConocimiento).Include(x => x.Estudio).OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<Titulo>();
            }
        }

        // GET: api/BasesDatos
        [HttpPost]
        [Route("ListarAreasConocimientoyEstudiosporTitulo")]
        public async Task<List<Titulo>> ListarAreasConocimientoyEstudiosporTitulo([FromBody] Titulo titulo)
        {
            try
            {
                return await db.Titulo.Where(x => x.IdTitulo == titulo.IdTitulo).Include(x => x.AreaConocimiento).Include(x => x.Estudio).OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<Titulo>();
            }
        }


        [HttpPost]
        [Route("ListarTitulosporAreaConocimiento")]
        public async Task<List<Titulo>> ListarTitulosporAreaConocimiento([FromBody]Titulo titulo)
        {
            try
            {
                var Lista = await db.Titulo
                                   .Where(ac => db.Titulo
                                                   .Where(a => a.IdAreaConocimiento == titulo.IdAreaConocimiento && a.IdEstudio == titulo.IdEstudio)
                                                   .Select(ioac => ioac.IdTitulo)
                                                   .Contains(ac.IdTitulo))
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
                return new List<Titulo>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetTitulo([FromRoute] int id)
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

                var Titulo = await db.Titulo.SingleOrDefaultAsync(m => m.IdTitulo == id);

                if (Titulo == null)
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
                    Resultado = Titulo,
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
        public async Task<Response> PutTitulo([FromRoute] int id, [FromBody] Titulo Titulo)
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

                var existe = Existe(Titulo);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Existe una brigada de salud y seguridad ocupacional con igual rol",
                    };
                }

                var TituloActualizar = await db.Titulo.Where(x => x.IdTitulo == id).FirstOrDefaultAsync();
                if (TituloActualizar != null)
                {
                    try
                    {

                        TituloActualizar.Nombre = Titulo.Nombre;
                        TituloActualizar.IdAreaConocimiento = Titulo.IdAreaConocimiento;
                        TituloActualizar.IdEstudio = Titulo.IdEstudio;
                        db.Titulo.Update(TituloActualizar);
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
                    Message = "Existe una brigada de salud y seguridad ocupacional con igual rol",
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
        [Route("InsertarTitulo")]
        public async Task<Response> PostTitulo([FromBody] Titulo Titulo)
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

                var respuesta = Existe(Titulo);
                if (!respuesta.IsSuccess)
                {
                    db.Titulo.Add(Titulo);
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
                    Message = "Existe una brigada de salud y seguridad ocupacional con igual rol"
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
        public async Task<Response> DeleteTitulo([FromRoute] int id)
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

                var respuesta = await db.Titulo.SingleOrDefaultAsync(m => m.IdTitulo == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.Titulo.Remove(respuesta);
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
                    Message = Mensaje.BorradoNoSatisfactorio,
                };
            }
        }

        private Response Existe(Titulo Titulo)
        {
            var bdd = Titulo.Nombre.ToUpper().TrimEnd().TrimStart();
            var ada = Titulo.IdEstudio;
            var iac = Titulo.IdAreaConocimiento;

            var Titulorespuesta = db.Titulo.Where(
                    p => p.Nombre.ToUpper().TrimStart().TrimEnd() == bdd 
                    && p.IdEstudio == ada
                    && p.IdAreaConocimiento == iac

                ).FirstOrDefault();
            if (Titulorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Existe una brigada de salud y seguridad ocupacional con igual rol",
                    Resultado = null,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = Titulorespuesta,
            };
        }
    }
}