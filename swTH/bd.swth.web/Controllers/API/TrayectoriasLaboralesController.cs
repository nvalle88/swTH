using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.Enumeradores;
using bd.swth.entidades.Utils;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.log.guardar.Enumeradores;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/TrayectoriasLaborales")]
    public class TrayectoriasLaboralesController : Controller
    {
        private readonly SwTHDbContext db;

        public TrayectoriasLaboralesController(SwTHDbContext db)
        {
            this.db = db;
        }

        
        // GET: api/TrayectoriaLaboral
        [HttpGet]
        [Route("ListarTrayectoriasLaborales")]
        public async Task<List<TrayectoriaLaboral>> GetTrayectoriasLaborales()
        {
            try
            {
                return await db.TrayectoriaLaboral.Include(x => x.Persona).OrderBy(x => x.Empresa).ToListAsync();
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
                return new List<TrayectoriaLaboral>();
            }
        }

        // GET: api/PersonaEstudio
        [HttpPost]
        [Route("ListarTrayectoriasLaboralesporEmpleado")]
        public async Task<List<TrayectoriaLaboral>> GetTrayectoriasLaboralesById([FromBody] Empleado empleado)
        {
            try
            {
                return await db.TrayectoriaLaboral.Where(x => x.IdPersona == empleado.IdPersona).OrderBy(x => x.FechaInicio).ToListAsync();
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
                return new List<TrayectoriaLaboral>();
            }
        }

        // GET: api/TrayectoriaLaboral/5
        [HttpGet("{id}")]
        public async Task<Response> GetTrayectoriaLaboral([FromRoute] int id)
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

                var TrayectoriaLaboral = await db.TrayectoriaLaboral.SingleOrDefaultAsync(m => m.IdTrayectoriaLaboral == id);

                if (TrayectoriaLaboral == null)
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
                    Resultado = TrayectoriaLaboral,
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

        // PUT: api/TrayectoriaLaboral/5
        [HttpPut("{id}")]
        public async Task<Response> PutTrayectoriaLaboral([FromRoute] int id, [FromBody] TrayectoriaLaboral trayectoriaLaboral)
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

                var existe = Existe(trayectoriaLaboral);
                var TrayectoriaLaboralActualizar = (TrayectoriaLaboral)existe.Resultado;
                if (existe.IsSuccess)
                {
                  
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }
                var TrayectoriaLaboral = await db.TrayectoriaLaboral.Where(x => x.IdTrayectoriaLaboral == trayectoriaLaboral.IdTrayectoriaLaboral).FirstOrDefaultAsync();

                TrayectoriaLaboral.IdPersona = trayectoriaLaboral.IdPersona;
                TrayectoriaLaboral.FechaInicio = trayectoriaLaboral.FechaInicio;
                TrayectoriaLaboral.FechaFin = trayectoriaLaboral.FechaFin;
                TrayectoriaLaboral.Empresa = trayectoriaLaboral.Empresa;
                TrayectoriaLaboral.PuestoTrabajo = trayectoriaLaboral.PuestoTrabajo;
                TrayectoriaLaboral.DescripcionFunciones = trayectoriaLaboral.DescripcionFunciones;

                TrayectoriaLaboral.AreaAsignada = trayectoriaLaboral.AreaAsignada;
                TrayectoriaLaboral.FormaIngreso = trayectoriaLaboral.FormaIngreso;
                TrayectoriaLaboral.MotivoSalida = trayectoriaLaboral.MotivoSalida;
                TrayectoriaLaboral.TipoInstitucion = trayectoriaLaboral.TipoInstitucion;
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

        // POST: api/TrayectoriaLaboral
        [HttpPost]
        [Route("InsertarTrayectoriaLaboral")]
        public async Task<Response> PostTrayectoriaLaboral([FromBody] TrayectoriaLaboral TrayectoriaLaboral)
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

                var respuesta = Existe(TrayectoriaLaboral);
                if (!respuesta.IsSuccess)
                {
                    db.TrayectoriaLaboral.Add(TrayectoriaLaboral);
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

        // DELETE: api/TrayectoriaLaboral/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteTrayectoriaLaboral([FromRoute] int id)
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

                var respuesta = await db.TrayectoriaLaboral.SingleOrDefaultAsync(m => m.IdTrayectoriaLaboral == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.TrayectoriaLaboral.Remove(respuesta);
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

        private Response Existe(TrayectoriaLaboral TrayectoriaLaboral)
        {
            var fechaInicio = TrayectoriaLaboral.FechaInicio;
            var fechaFin = TrayectoriaLaboral.FechaFin;
            var Empresa = TrayectoriaLaboral.Empresa;
            var PuestoTrabajo = TrayectoriaLaboral.PuestoTrabajo;
            var DescripcionFunciones = TrayectoriaLaboral.DescripcionFunciones;
            var tipoInstitucion = TrayectoriaLaboral.TipoInstitucion;
            var areaAsignada = TrayectoriaLaboral.AreaAsignada;
            var formaIngreso = TrayectoriaLaboral.FormaIngreso;
            var motivoSalida = TrayectoriaLaboral.MotivoSalida;

            var TrayectoriaLaboralrespuesta = db.TrayectoriaLaboral.Where(p => p.FechaInicio == fechaInicio 
            && p.FechaFin == fechaFin
            && p.Empresa == Empresa
            && p.PuestoTrabajo == PuestoTrabajo
            && p.TipoInstitucion == tipoInstitucion
            && p.AreaAsignada == areaAsignada
            && p.FormaIngreso == formaIngreso
            && p.MotivoSalida == motivoSalida
            && p.DescripcionFunciones == DescripcionFunciones).FirstOrDefault();

            if (TrayectoriaLaboralrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = TrayectoriaLaboralrespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = TrayectoriaLaboralrespuesta,
            };
        }
    }
}
