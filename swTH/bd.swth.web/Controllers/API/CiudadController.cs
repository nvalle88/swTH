using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.log.guardar.Servicios;
using bd.log.guardar.Enumeradores;
using Microsoft.EntityFrameworkCore;
using bd.log.guardar.ObjectTranfer;
using bd.swth.entidades.Enumeradores;
using bd.swth.entidades.Utils;

namespace bd.swrm.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Ciudad")]
    public class CiudadController : Controller
    {
        private readonly SwTHDbContext db;

        public CiudadController(SwTHDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        [Route("ListarCiudadPorPais")]
        public async Task<List<Ciudad>> ListarCiudadPorPais([FromBody] Pais pais)
        {
            try
            {
                if (pais==null)
                {
                    return new List<Ciudad>();
                }

                var listaSalida = new List<Ciudad>();
                var provincias =await db.Provincia.Where(x => x.IdPais == pais.IdPais).ToListAsync();

                foreach (var item in provincias)
                {
                  var listaCiudad=await db.Ciudad.Include(x=> x.Provincia).ThenInclude(x=> x.Pais).Where(x => x.IdProvincia == item.IdProvincia).ToListAsync();
                    listaSalida.AddRange(listaCiudad); 
                }

                return  listaSalida;
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
                return new List<Ciudad>();
            }
        }

        [HttpPost]
        [Route("ListarCiudadPorProvincia")]
        public async Task<List<Ciudad>> GetCiudad([FromBody] Provincia provincia)
        {
            try
            {
                if (provincia==null)
                {
                    return new List<Ciudad>();
                }
                return await db.Ciudad.Include(x => x.Provincia).ThenInclude(x => x.Pais).Where(x => x.IdProvincia == provincia.IdProvincia).OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<Ciudad>();
            }
        }


        // GET: api/Ciudad
        [HttpGet]
        [Route("ListarCiudad")]
        public async Task<List<Ciudad>> GetCiudad()
        {
            try
            {
                return await db.Ciudad.Include(x => x.Provincia).ThenInclude(x => x.Pais).OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<Ciudad>();
            }
        }

        // GET: api/Ciudad/5
        [HttpGet("{id}")]
        public async Task<Response> GetCiudad([FromRoute] int id)
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

                var ciudad = await db.Ciudad.Include(c=> c.Provincia).ThenInclude(c=> c.Pais).SingleOrDefaultAsync(m => m.IdCiudad == id);

                if (ciudad == null)
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
                    Resultado = ciudad,
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

        // PUT: api/Ciudad/5
        [HttpPut("{id}")]
        public async Task<Response> PutCiudad([FromRoute] int id, [FromBody] Ciudad ciudad)
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
                var existe = Existe(ciudad);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }
                var ciudadActualizar = await db.Ciudad.Where(x => x.IdCiudad == id).FirstOrDefaultAsync();
                if (ciudadActualizar != null)
                {
                    try
                    {
                        ciudadActualizar.Nombre = ciudad.Nombre;
                        db.Ciudad.Update(ciudadActualizar);
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

        // POST: api/Ciudad
        [HttpPost]
        [Route("InsertarCiudad")]
        public async Task<Response> PostCiudad([FromBody] Ciudad ciudad)
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

                var respuesta = Existe(ciudad);
                if (!respuesta.IsSuccess)
                {
                    db.Ciudad.Add(ciudad);
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

        // DELETE: api/Ciudad/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteCiudad([FromRoute] int id)
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

                var respuesta = await db.Ciudad.SingleOrDefaultAsync(m => m.IdCiudad == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.Ciudad.Remove(respuesta);
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

        private bool CiudadExists(string nombre)
        {
            return db.Ciudad.Any(e => e.Nombre == nombre);
        }

        public Response Existe(Ciudad ciudad)
        {
            var bdd = ciudad.Nombre.ToUpper().TrimEnd().TrimStart();
            var bdd1 = ciudad.IdProvincia;
            var loglevelrespuesta = db.Ciudad.Where(p => p.Nombre.ToUpper().TrimStart().TrimEnd() == bdd && p.IdProvincia == bdd1).FirstOrDefault();
            if (loglevelrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.BorradoNoSatisfactorio,
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