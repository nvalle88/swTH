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
    [Route("api/NacionalidadesIndigenas")]
    public class NacionalidadesIndigenasController : Controller
    {
        private readonly SwTHDbContext db;

        public NacionalidadesIndigenasController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/NacionalidadIndigenas
        [HttpGet]
        [Route("ListarNacionalidadIndigenas")]
        public async Task<List<NacionalidadIndigena>> GetNacionalidadIndigena()
        {
            try
            {
                return await db.NacionalidadIndigena.OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<NacionalidadIndigena>();
            }
        }

        // GET: api/NacionalidadIndigenas/5
        [HttpGet("{id}")]
        public async Task<Response> GetNacionalidadIndigena([FromRoute] int id)
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

                var adscbdd = await db.NacionalidadIndigena.SingleOrDefaultAsync(m => m.IdNacionalidadIndigena == id);

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

        // PUT: api/NacionalidadIndigenas/5
        [HttpPut("{id}")]
        public async Task<Response> PutNacionalidadIndigena([FromRoute] int id, [FromBody] NacionalidadIndigena nacionalidadIndigena)
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
                    var entidad = await db.NacionalidadIndigena.Where(x => x.IdNacionalidadIndigena == id).FirstOrDefaultAsync();

                    if (entidad == null)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "No existe información acerca del NacionalidadIndigena ",
                        };

                    }
                    else
                    {

                        entidad.Nombre = nacionalidadIndigena.Nombre;
                        entidad.IdEtnia = nacionalidadIndigena.IdEtnia;
                        db.NacionalidadIndigena.Update(entidad);
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

        // POST: api/NacionalidadIndigenas
        [HttpPost]
        [Route("InsertarNacionalidadesIndigenas")]
        public async Task<Response> PostNacionalidadIndigena([FromBody] NacionalidadIndigena nacionalidadIndigena)
        {
            try
            {

                var respuesta = Existe(nacionalidadIndigena.Nombre);
                if (!respuesta.IsSuccess)
                {
                    db.NacionalidadIndigena.Add(nacionalidadIndigena);
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

        // DELETE: api/NacionalidadIndigenas/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteNacionalidadIndigena([FromRoute] int id)
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

                var respuesta = await db.NacionalidadIndigena.SingleOrDefaultAsync(m => m.IdNacionalidadIndigena == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.NacionalidadIndigena.Remove(respuesta);
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

        private bool NacionalidadIndigenaExists(int id)
        {
            return db.NacionalidadIndigena.Any(e => e.IdNacionalidadIndigena == id);
        }


        public Response Existe(string nombreNacionalidadIndigena)
        {

            var loglevelrespuesta = db.NacionalidadIndigena.Where(p => p.Nombre.ToUpper().TrimStart().TrimEnd() == nombreNacionalidadIndigena).FirstOrDefault();
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