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


        [HttpPost]
        [Route("ListarNacionalidadesIndigenasPorEtnias")]
        public async Task<List<NacionalidadIndigena>> ListarNacionalidadesIndigenasPorEtnias([FromBody] Etnia etnia)
        {
            try
            {
                return await db.NacionalidadIndigena.Where(x => x.IdEtnia==etnia.IdEtnia).OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<NacionalidadIndigena>();
            }
        }


        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarNacionalidadesIndigenas")]
        public async Task<List<NacionalidadIndigena>> GetNacionalidadesIndigenas()
        {
            try
            {
                return await db.NacionalidadIndigena.Include(x => x.Etnia).OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<NacionalidadIndigena>();
            }
        }

        // GET: api/BasesDatos/5
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

                var NacionalidadIndigena = await db.NacionalidadIndigena.SingleOrDefaultAsync(m => m.IdNacionalidadIndigena == id);

                if (NacionalidadIndigena == null)
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
                    Resultado = NacionalidadIndigena,
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
        public async Task<Response> PutNacionalidadIndigena([FromRoute] int id, [FromBody] NacionalidadIndigena NacionalidadIndigena)
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

                var existe = Existe(NacionalidadIndigena);
                var NacionalidadIndigenaActualizar = (NacionalidadIndigena)existe.Resultado;
                if (existe.IsSuccess)
                {
                    if (NacionalidadIndigenaActualizar.IdNacionalidadIndigena == NacionalidadIndigena.IdNacionalidadIndigena)
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
                var nacionalidadindigena = db.NacionalidadIndigena.Find(NacionalidadIndigena.IdNacionalidadIndigena);

                nacionalidadindigena.IdEtnia = NacionalidadIndigena.IdEtnia;
                nacionalidadindigena.Nombre = NacionalidadIndigena.Nombre;
                db.NacionalidadIndigena.Update(nacionalidadindigena);
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

        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarNacionalidadIndigena")]
        public async Task<Response> PostNacionalidadIndigena([FromBody] NacionalidadIndigena NacionalidadIndigena)
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

                var respuesta = Existe(NacionalidadIndigena);
                if (!respuesta.IsSuccess)
                {
                    db.NacionalidadIndigena.Add(NacionalidadIndigena);
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

        // DELETE: api/BasesDatos/5
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

        private Response Existe(NacionalidadIndigena NacionalidadIndigena)
        {
            var bdd = NacionalidadIndigena.Nombre.ToUpper().TrimEnd().TrimStart();
            var NacionalidadIndigenarespuesta = db.NacionalidadIndigena.Where(p => p.Nombre.ToUpper().TrimStart().TrimEnd() == bdd && p.IdEtnia == NacionalidadIndigena.IdEtnia).FirstOrDefault();
            if (NacionalidadIndigenarespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = NacionalidadIndigenarespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = NacionalidadIndigenarespuesta,
            };
        }
    }
}