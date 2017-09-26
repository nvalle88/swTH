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
    [Route("api/InstitucionesFinancieras")]
    public class InstitucionesFinancierasController : Controller
    {
        private readonly SwTHDbContext db;

        public InstitucionesFinancierasController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarInstitucionesFinancieras")]
        public async Task<List<InstitucionFinanciera>> GetInstitucionesFinancieras()
        {
            try
            {
                return await db.InstitucionFinanciera.OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<InstitucionFinanciera>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetInstitucionFinanciera([FromRoute] int id)
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

                var InstitucionFinanciera = await db.InstitucionFinanciera.SingleOrDefaultAsync(m => m.IdInstitucionFinanciera == id);

                if (InstitucionFinanciera == null)
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
                    Resultado = InstitucionFinanciera,
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

        private async Task Actualizar(InstitucionFinanciera InstitucionFinanciera)
        {
            var institucionfinanciera = db.InstitucionFinanciera.Find(InstitucionFinanciera.IdInstitucionFinanciera);

            institucionfinanciera.Nombre = InstitucionFinanciera.Nombre;
            institucionfinanciera.SPI = InstitucionFinanciera.SPI;
            db.InstitucionFinanciera.Update(institucionfinanciera);
            await db.SaveChangesAsync();
        }

        // PUT: api/BasesDatos/5
        [HttpPut("{id}")]
        public async Task<Response> PutInstitucionFinanciera([FromRoute] int id, [FromBody] InstitucionFinanciera InstitucionFinanciera)
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


                
                var existe = Existe(InstitucionFinanciera);
                var InstitucionFinancieraActualizar = (InstitucionFinanciera)existe.Resultado;

                if (existe.IsSuccess)
                {


                    if (InstitucionFinancieraActualizar.IdInstitucionFinanciera == InstitucionFinanciera.IdInstitucionFinanciera)
                    {
                        if (
                        InstitucionFinanciera.Nombre == InstitucionFinancieraActualizar.Nombre &&
                        InstitucionFinanciera.SPI == InstitucionFinancieraActualizar.SPI)
                        {
                            return new Response
                            {
                                IsSuccess = true,
                            };
                        }

                        await Actualizar(InstitucionFinanciera);
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

                await Actualizar(InstitucionFinanciera);
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
        [Route("InsertarInstitucionFinanciera")]
        public async Task<Response> PostInstitucionFinanciera([FromBody] InstitucionFinanciera InstitucionFinanciera)
        {
            try
            {

              
                var respuesta = Existe(InstitucionFinanciera);
                if (!respuesta.IsSuccess)
                {
                    db.InstitucionFinanciera.Add(InstitucionFinanciera);
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
        public async Task<Response> DeleteInstitucionFinanciera([FromRoute] int id)
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

                var respuesta = await db.InstitucionFinanciera.SingleOrDefaultAsync(m => m.IdInstitucionFinanciera == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.InstitucionFinanciera.Remove(respuesta);
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

        private Response Existe(InstitucionFinanciera InstitucionFinanciera)
        {
            var bdd = InstitucionFinanciera.Nombre;
            var InstitucionFinancierarespuesta = db.InstitucionFinanciera.Where(p => p.Nombre == bdd).FirstOrDefault();
            if (InstitucionFinancierarespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = InstitucionFinancierarespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = InstitucionFinancierarespuesta,
            };
        }
    }
}