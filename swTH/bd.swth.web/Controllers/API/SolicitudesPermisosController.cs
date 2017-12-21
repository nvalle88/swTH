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
using System;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/SolicitudesPermisos")]
    public class SolicitudesPermisosController : Controller
    {
        private readonly SwTHDbContext db;

        public SolicitudesPermisosController(SwTHDbContext db)
        {
            this.db = db;
        }

        
        // GET: api/SolicitudesPermisos
        [HttpGet]
        [Route("ListarSolicitudesPermisos")]
        public async Task<List<SolicitudPermiso>> GetNacionalidadesIndigenas()
        {
            try
            {
                return await db.SolicitudPermiso.Include(x => x.TipoPermiso).OrderBy(x => x.TipoPermiso).ToListAsync();
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
                return new List<SolicitudPermiso>();
            }
        }

        // GET: api/SolicitudesPermisos/5
        [HttpGet("{id}")]
        public async Task<Response> GetSolicitudPermiso([FromRoute] int id)
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

                var SolicitudPermiso = await db.SolicitudPermiso.SingleOrDefaultAsync(m => m.IdSolicitudPermiso == id);

                if (SolicitudPermiso == null)
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
                    Resultado = SolicitudPermiso,
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

        // PUT: api/SolicitudesPermisos/5
        [HttpPut("{id}")]
        public async Task<Response> PutSolicitudPermiso([FromRoute] int id, [FromBody] SolicitudPermiso solicitudPermiso)
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

                var existe = Existe(solicitudPermiso);
                var SolicitudPermisoActualizar = (SolicitudPermiso)existe.Resultado;
                if (existe.IsSuccess)
                {
                    if (SolicitudPermisoActualizar.IdSolicitudPermiso == solicitudPermiso.IdSolicitudPermiso)
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
                var SolicitudPermiso = db.SolicitudPermiso.Find(solicitudPermiso.IdSolicitudPermiso);
                
                SolicitudPermiso.IdEmpleado = solicitudPermiso.IdEmpleado;
                SolicitudPermiso.FechaSolicitud = solicitudPermiso.FechaSolicitud;
                SolicitudPermiso.HoraDesde = solicitudPermiso.HoraDesde;
                SolicitudPermiso.HoraHasta = solicitudPermiso.HoraHasta;
                SolicitudPermiso.FechaDesde = solicitudPermiso.FechaDesde;
                SolicitudPermiso.FechaHasta = solicitudPermiso.FechaHasta;
                SolicitudPermiso.Motivo = solicitudPermiso.Motivo;
                SolicitudPermiso.Estado = solicitudPermiso.Estado;
                SolicitudPermiso.IdTipoPermiso = solicitudPermiso.IdTipoPermiso;
                SolicitudPermiso.FechaAprobado = solicitudPermiso.FechaAprobado;
                db.SolicitudPermiso.Update(SolicitudPermiso);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                    Resultado= SolicitudPermiso
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

        // POST: api/SolicitudesPermisos
        [HttpPost]
        [Route("InsertarSolicitudPermiso")]
        public async Task<Response> PostSolicitudPermiso([FromBody] SolicitudPermiso SolicitudPermiso)
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

                var respuesta = Existe(SolicitudPermiso);
                if (!respuesta.IsSuccess)
                {
                    db.SolicitudPermiso.Add(SolicitudPermiso);
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

        // DELETE: api/SolicitudesPermisos/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteSolicitudPermiso([FromRoute] int id)
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

                var respuesta = await db.SolicitudPermiso.SingleOrDefaultAsync(m => m.IdSolicitudPermiso == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.SolicitudPermiso.Remove(respuesta);
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

        private Response Existe(SolicitudPermiso SolicitudPermiso)
        {
            var fechaDesde = SolicitudPermiso.FechaDesde;
            var fechaHasta = SolicitudPermiso.FechaHasta;
            var horaDesde = SolicitudPermiso.HoraDesde;
            var horaHasta = SolicitudPermiso.HoraHasta;

            var SolicitudPermisorespuesta = db.SolicitudPermiso.Where(p => p.FechaDesde == fechaDesde  && p.FechaHasta == fechaHasta && p.HoraDesde == horaDesde && p.HoraHasta == horaHasta && p.IdEmpleado == SolicitudPermiso.IdEmpleado && p.IdTipoPermiso==SolicitudPermiso.IdTipoPermiso).FirstOrDefault();

            if (SolicitudPermisorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = SolicitudPermisorespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = SolicitudPermisorespuesta,
            };
        }

    }
}