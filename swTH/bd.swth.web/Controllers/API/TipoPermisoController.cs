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
using bd.log.guardar.Utiles;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/TipoPermiso")]
    public class TipoPermisoController : Controller
    {
        private readonly SwTHDbContext db;

        public TipoPermisoController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/ListarTipoPermiso
        [HttpGet]
        [Route("ListarTipoPermiso")]
        public async Task<List<TipoPermiso>> GetTipoPermiso()
        {
            try
            {
                return await db.TipoPermiso.OrderBy(x => x.Nombre).ToListAsync();
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = "Se ha producido una exepci�n",
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new List<TipoPermiso>();
            }
        }


        // GET: api/TipoPermiso/5
        [HttpGet("{id}")]
        public async Task<Response> GetTipoPermiso([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "M�delo no v�lido",
                    };
                }

                var TipoPermiso = await db.TipoPermiso.SingleOrDefaultAsync(m => m.IdTipoPermiso == id);

                if (TipoPermiso == null)
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
                    Resultado = TipoPermiso,
                };
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = "Se ha producido una exepci�n",
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


        // PUT: api/TipoPermiso/5
        [HttpPut("{id}")]
        public async Task<Response> PutTipoPermiso([FromRoute] int id, [FromBody] TipoPermiso TipoPermiso)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "M�delo inv�lido"
                    };
                }

                var TipoPermisoActualizar = await db.TipoPermiso.Where(x => x.IdTipoPermiso == id).FirstOrDefaultAsync();
                if (TipoPermisoActualizar != null)
                {
                    try
                    {
                        TipoPermiso.Nombre = TipoPermiso.Nombre;
                        db.TipoPermiso.Update(TipoPermisoActualizar);
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
                            Message = "Se ha producido una exepci�n",
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
                    Message = "Excepci�n"
                };
            }
        }

        // POST: api/TipoPermiso
        [HttpPost]
        [Route("InsertarTipoPermiso")]
        public async Task<Response> PostTipoPermiso([FromBody] TipoPermiso TipoPermiso)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "M�delo inv�lido"
                    };
                }

                var respuesta = Existe(TipoPermiso);
                if (!respuesta.IsSuccess)
                {
                    db.TipoPermiso.Add(TipoPermiso);
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
                    Message = "Se ha producido una exepci�n",
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

        // DELETE: api/TipoPermiso/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteTipoPermiso([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "M�delo no v�lido ",
                    };
                }

                var respuesta = await db.TipoPermiso.SingleOrDefaultAsync(m => m.IdTipoPermiso == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No existe ",
                    };
                }
                db.TipoPermiso.Remove(respuesta);
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
                    Message = "Se ha producido una exepci�n",
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

        private bool TipoPermisoExists(string nombre)
        {
            return db.TipoPermiso.Any(e => e.Nombre == nombre);
        }

        public Response Existe(TipoPermiso TipoPermiso)
        {
            var bdd = TipoPermiso.Nombre.ToUpper().TrimEnd().TrimStart();
            var loglevelrespuesta = db.TipoPermiso.Where(p => p.Nombre.ToUpper().TrimStart().TrimEnd() == bdd).FirstOrDefault();
            if (loglevelrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Existe un permiso de igual nombre",
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
