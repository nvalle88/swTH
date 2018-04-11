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
using bd.swth.entidades.ViewModels;

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
        [Route("ListarSolicitudesPermisosEmpleado")]
        public async Task<List<SolicitudPermiso>> GetListarSolicitudesPermisos()
        {
            try
            {
                return await db.SolicitudPermiso
                            .Include(x => x.Empleado)
                            .ThenInclude(x=>x.Persona)
                            .Include(x => x.TipoPermiso)
                            .OrderBy(x => x.TipoPermiso)
                            .ToListAsync();
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
                return new List<SolicitudPermiso>();
            }
        }
      
        [HttpPost]
        [Route("ListarInformacionEmpleado")]
        public async Task<List<SolicitudPermiso>> ListarInformacionEmpleado([FromBody] SolicitudPermiso solicitudPermiso)
        {
            try
            {

                return await db.SolicitudPermiso
                                  .Include(x => x.Empleado)
                                  .ThenInclude(x => x.Persona)
                                  .Include(x => x.TipoPermiso)
                                  .Where(x => (x.IdEmpleado == solicitudPermiso.IdEmpleado) && (x.Estado == 0 || x.Estado == -1))
                                  .ToListAsync();
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
                return new List<SolicitudPermiso>();
            }
        }

        [HttpPost]
        [Route("ObtenerInformacionSolicitudPermiso")]
        public async Task<SolicitudPermisoViewModel> ObtenerInformacionSolicitudPermiso([FromBody] int idSolicitudPermiso)
        {
            try
            {

                SolicitudPermiso solicitudPermiso = await db.SolicitudPermiso
                                  .Include(x => x.Empleado)
                                  .ThenInclude(x => x.Persona)
                                  .Include(x => x.TipoPermiso)
                                  .Where(x => x.IdSolicitudPermiso == idSolicitudPermiso)
                                  .SingleOrDefaultAsync();
                
                Empleado empleado = await db.Empleado
                    .Include(x => x.Persona)
                    .Include(x => x.Dependencia)
                    .SingleOrDefaultAsync(m => m.IdEmpleado == solicitudPermiso.Empleado.IdEmpleado);

                var solicitudPermisoViewModel = new SolicitudPermisoViewModel
                {
                    NombresApellidosEmpleado = empleado.Persona.Nombres + " " + empleado.Persona.Apellidos,
                    NombreDependencia = empleado.Dependencia.Nombre,
                    SolicitudPermiso = solicitudPermiso
                };

                return solicitudPermisoViewModel;

            }
            catch (Exception ex)
            {
                throw;
            }
        }



        [HttpPost]
        [Route("ListarSolicitudesPermisosEmpleado")]
        public async Task<List<SolicitudPermiso>> ListarSolicitudesPermisosEmpleado([FromBody] Empleado empleado)
        {
            try
            {

                return await db.SolicitudPermiso
                                  .Include(x => x.Empleado)
                                  .ThenInclude(x => x.Persona)
                                  .Include(x => x.TipoPermiso)
                                  .Where(x => x.IdEmpleado == empleado.IdEmpleado)
                                  .ToListAsync();
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
                return new List<SolicitudPermiso>();
            }
        }


        [HttpPost]
        [Route("ListarSolicitudesPermisosPendientesJefe")]
        public async Task<List<SolicitudPermiso>> ListarSolicitudesPermisosPendientesJefe([FromBody] Empleado empleado)
        {
            try
            {

                return await db.SolicitudPermiso
                                  .Include(x => x.Empleado)
                                  .ThenInclude(x => x.Persona)
                                  .Include(x => x.TipoPermiso)
                                  .Where(x => x.IdEmpleado == empleado.IdEmpleado && x.Estado == 0 || x.Estado == -1)
                                  .Distinct()
                                  .ToListAsync();
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
                
                var SolicitudPermiso = db.SolicitudPermiso.Find(solicitudPermiso.IdSolicitudPermiso);
                
                
                SolicitudPermiso.HoraDesde = solicitudPermiso.HoraDesde;
                SolicitudPermiso.HoraHasta = solicitudPermiso.HoraHasta;
                SolicitudPermiso.FechaDesde = solicitudPermiso.FechaDesde;
                SolicitudPermiso.FechaHasta = solicitudPermiso.FechaHasta;
                SolicitudPermiso.Motivo = solicitudPermiso.Motivo;
                SolicitudPermiso.IdTipoPermiso = solicitudPermiso.IdTipoPermiso;
                SolicitudPermiso.Estado = solicitudPermiso.Estado;
                SolicitudPermiso.FechaAprobado = DateTime.Now;
                SolicitudPermiso.Observacion = solicitudPermiso.Observacion;
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