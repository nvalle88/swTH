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

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/TiposAccionesPersonales")]
    public class TiposAccionesPersonalesController : Controller
    {
        private readonly SwTHDbContext db;

        public TiposAccionesPersonalesController(SwTHDbContext db)
        {
            this.db = db;
        }


        [HttpPost]
        [Route("ListarTiposAccionesPersonalesPorEstado")]
        public async Task<List<TipoAccionPersonal>> ListarTiposAccionesPersonalesPorEstado([FromBody] EstadoTipoAccionPersonal estadoTipoAccionPersonal)
        {
            try
            {
                return await db.TipoAccionPersonal.Where(x => x.IdEstadoTipoAccionPersonal == estadoTipoAccionPersonal.IdEstadoTipoAccionPersonal).OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<TipoAccionPersonal>();
            }
        }


        // GET: api/TipoAccionPersonal
        [HttpGet]
        [Route("ListarTiposAccionesPersonales")]
        public async Task<List<TipoAccionPersonal>> GetTiposAccionesPersonales()
        {
            try
            {
                return await db.TipoAccionPersonal.Include(x => x.EstadoTipoAccionPersonal).OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<TipoAccionPersonal>();
            }
        }

        // GET: api/TipoAccionPersonal/5
        [HttpGet("{id}")]
        public async Task<Response> GetTipoAccionPersonal([FromRoute] int id)
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

                var TipoAccionPersonal = await db.TipoAccionPersonal.SingleOrDefaultAsync(m => m.IdTipoAccionPersonal == id);

                if (TipoAccionPersonal == null)
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
                    Resultado = TipoAccionPersonal,
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

        // PUT: api/TipoAccionPersonal/5
        [HttpPut("{id}")]
        public async Task<Response> PutTipoAccionPersonal([FromRoute] int id, [FromBody] TipoAccionPersonal tipoAccionPersonal)
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

               
                var TipoAccionPersonal = db.TipoAccionPersonal.Find(tipoAccionPersonal.IdTipoAccionPersonal);
                
                TipoAccionPersonal.Nombre = tipoAccionPersonal.Nombre;
                TipoAccionPersonal.NDiasMaximo = tipoAccionPersonal.NDiasMaximo;
                TipoAccionPersonal.NDiasMinimo = tipoAccionPersonal.NDiasMinimo;
                TipoAccionPersonal.NHorasMaximo = tipoAccionPersonal.NHorasMaximo;
                TipoAccionPersonal.NHorasMinimo = tipoAccionPersonal.NHorasMinimo;
                TipoAccionPersonal.DiasHabiles = tipoAccionPersonal.DiasHabiles;
                TipoAccionPersonal.ImputableVacaciones = tipoAccionPersonal.ImputableVacaciones;
                TipoAccionPersonal.ProcesoNomina = tipoAccionPersonal.ProcesoNomina;
                TipoAccionPersonal.EsResponsableTH = tipoAccionPersonal.EsResponsableTH;
                TipoAccionPersonal.Matriz = tipoAccionPersonal.Matriz;
                TipoAccionPersonal.Descripcion = TipoAccionPersonal.Descripcion;
                TipoAccionPersonal.GeneraAccionPersonal = tipoAccionPersonal.GeneraAccionPersonal;
                TipoAccionPersonal.ModificaDistributivo = tipoAccionPersonal.ModificaDistributivo;
                TipoAccionPersonal.IdEstadoTipoAccionPersonal = tipoAccionPersonal.IdEstadoTipoAccionPersonal;
                db.TipoAccionPersonal.Update(TipoAccionPersonal);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                    Resultado = TipoAccionPersonal,
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

        // POST: api/TipoAccionPersonal
        [HttpPost]
        [Route("InsertarTipoAccionPersonal")]
        public async Task<Response> PostTipoAccionPersonal([FromBody] TipoAccionPersonal TipoAccionPersonal)
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

                var respuesta = Existe(TipoAccionPersonal);
                if (!respuesta.IsSuccess)
                {
                    db.TipoAccionPersonal.Add(TipoAccionPersonal);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                        Resultado = TipoAccionPersonal
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

        // DELETE: api/TipoAccionPersonal/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteTipoAccionPersonal([FromRoute] int id)
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

                var respuesta = await db.TipoAccionPersonal.SingleOrDefaultAsync(m => m.IdTipoAccionPersonal == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.TipoAccionPersonal.Remove(respuesta);
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

        private Response Existe(TipoAccionPersonal TipoAccionPersonal)
        {
            var nombre = TipoAccionPersonal.Nombre.ToUpper().TrimEnd().TrimStart();
            var TipoAccionPersonalrespuesta = db.TipoAccionPersonal.Where(p => p.Nombre.ToUpper().TrimStart().TrimEnd() == nombre && p.IdEstadoTipoAccionPersonal == TipoAccionPersonal.IdEstadoTipoAccionPersonal).FirstOrDefault();

            if (TipoAccionPersonalrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = TipoAccionPersonalrespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = TipoAccionPersonalrespuesta,
            };
        }
    }
}
