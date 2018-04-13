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

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/CapacitacionesTemarios")]
    public class CapacitacionesTemariosController : Controller
    {
         private readonly SwTHDbContext db;

        public CapacitacionesTemariosController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarCapacitacionesTemarios")]
        public async Task<List<CapacitacionTemario>> GetCapacitacionesTemarios()
        {
            try
            {
                return await db.CapacitacionTemario.Include(x=>x.CapacitacionAreaConocimiento).OrderBy(x => x.Tema).ToListAsync();
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex.Message,
                                       Message = Mensaje.Excepcion,
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new List<CapacitacionTemario>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetCapacitacionTemario([FromRoute] int id)
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

                var CapacitacionTemario = await db.CapacitacionTemario.SingleOrDefaultAsync(m => m.IdCapacitacionTemario == id);

                if (CapacitacionTemario == null)
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
                    Resultado = CapacitacionTemario,
                };
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex.Message,
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
        public async Task<Response> PutCapacitacionTemario([FromRoute] int id, [FromBody] CapacitacionTemario CapacitacionTemario)
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

                var existe = Existe(CapacitacionTemario);
                var CapacitacionTemarioActualizar = (CapacitacionTemario)existe.Resultado;
                if (existe.IsSuccess)
                {
                    //if (CapacitacionTemarioActualizar.IdCapacitacionTemario == CapacitacionTemario.IdCapacitacionTemario && CapacitacionTemarioActualizar.Tema == CapacitacionTemario.Tema)
                    //{
                    //    return new Response
                    //    {
                    //        IsSuccess = true,
                    //    };
                    //}
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }
                var capacitacionac = db.CapacitacionTemario.Find(CapacitacionTemario.IdCapacitacionTemario);

                capacitacionac.IdCapacitacionAreaConocimiento = CapacitacionTemario.IdCapacitacionAreaConocimiento;
                capacitacionac.Tema = CapacitacionTemario.Tema;
                db.CapacitacionTemario.Update(capacitacionac);
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
                    ExceptionTrace = ex.Message,
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
        [Route("InsertarCapacitacionTemario")]
        public async Task<Response> PostCapacitacionTemario([FromBody] CapacitacionTemario CapacitacionTemario)
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

                var respuesta = Existe(CapacitacionTemario);
                if (!respuesta.IsSuccess)
                {
                    db.CapacitacionTemario.Add(CapacitacionTemario);
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
                    ExceptionTrace = ex.Message,
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
        public async Task<Response> DeleteCapacitacionTemario([FromRoute] int id)
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

                var respuesta = await db.CapacitacionTemario.SingleOrDefaultAsync(m => m.IdCapacitacionTemario == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.CapacitacionTemario.Remove(respuesta);
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
                    ExceptionTrace = ex.Message,
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

        private Response Existe(CapacitacionTemario CapacitacionTemario)
        {
            var bdd = CapacitacionTemario.Tema.ToUpper().TrimEnd().TrimStart();
            var bdd1 = CapacitacionTemario.IdCapacitacionAreaConocimiento;
            var CapacitacionTemariorespuesta = db.CapacitacionTemario.Where(p => p.Tema.ToUpper().TrimStart().TrimEnd() == bdd && p.IdCapacitacionAreaConocimiento== bdd1).FirstOrDefault();
            if (CapacitacionTemariorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = CapacitacionTemariorespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = CapacitacionTemariorespuesta,
            };
        }
    }
}