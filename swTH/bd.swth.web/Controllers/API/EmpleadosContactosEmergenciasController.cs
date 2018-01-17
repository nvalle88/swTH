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
    [Route("api/EmpleadosContactosEmergencias")]
    public class EmpleadosContactosEmergenciasController : Controller
    {
        private readonly SwTHDbContext db;

        public EmpleadosContactosEmergenciasController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/EmpleadoContactoEmergencia
        [HttpGet]
        [Route("ListarEmpleadosContactosEmergencias")]
        public async Task<List<EmpleadoContactoEmergencia>> GetEmpleadosContactosEmergencias()
        {
            try
            {
                return await db.EmpleadoContactoEmergencia.Include(x => x.Persona).Include(x => x.Empleado).Include(x => x.Parentesco).OrderBy(x => x.IdEmpleadoContactoEmergencia).ToListAsync();
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
                return new List<EmpleadoContactoEmergencia>();
            }
        }


        [HttpPost]
        [Route("EmpleadosContactosEmergenciasPorIdEmpleado")]
        public async Task<Response> EmpleadosContactosEmergenciasPorIdEmpleado([FromBody] EmpleadoContactoEmergencia empleadoContactoEmergencia)
        {
            try
            {
                var EmpleadoContactoEmergencia = await db.EmpleadoContactoEmergencia.SingleOrDefaultAsync(m => m.IdEmpleado == empleadoContactoEmergencia.IdEmpleado);

                var response = new Response
                {
                    IsSuccess = true,
                    Resultado = EmpleadoContactoEmergencia,
                };

                return response;

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

                return new Response { };
            }
        }



        // GET: api/EmpleadoContactoEmergencia/5
        [HttpGet("{id}")]
        public async Task<Response> GetEmpleadoContactoEmergencia([FromRoute] int id)
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

                var EmpleadoContactoEmergencia = await db.EmpleadoContactoEmergencia.SingleOrDefaultAsync(m => m.IdEmpleadoContactoEmergencia == id);

                if (EmpleadoContactoEmergencia == null)
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
                    Resultado = EmpleadoContactoEmergencia,
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

        // PUT: api/EmpleadoContactoEmergencia/5
        [HttpPut("{id}")]
        public async Task<Response> PutEmpleadoContactoEmergencia([FromRoute] int id, [FromBody] EmpleadoContactoEmergencia empleadoContactoEmergencia)
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


                var existe = Existe(empleadoContactoEmergencia);
                var EmpleadoContactoEmergenciaActualizar = (EmpleadoContactoEmergencia)existe.Resultado;
                if (existe.IsSuccess)
                {
                    if (EmpleadoContactoEmergenciaActualizar.IdEmpleado == id)
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
                var EmpleadoContactoEmergencia = db.EmpleadoContactoEmergencia.Find(empleadoContactoEmergencia.IdEmpleadoContactoEmergencia);

                EmpleadoContactoEmergencia.IdPersona = empleadoContactoEmergencia.IdPersona;
                EmpleadoContactoEmergencia.IdEmpleado = id;
                EmpleadoContactoEmergencia.IdParentesco = empleadoContactoEmergencia.IdParentesco;
                db.EmpleadoContactoEmergencia.Update(EmpleadoContactoEmergencia);
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

        // POST: api/EmpleadoContactoEmergencia
        [HttpPost]
        [Route("InsertarEmpleadoContactoEmergencia")]
        public async Task<Response> PostEmpleadoContactoEmergencia([FromBody] EmpleadoContactoEmergencia EmpleadoContactoEmergencia)
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

                var respuesta = Existe(EmpleadoContactoEmergencia);
                if (!respuesta.IsSuccess)
                {
                    db.EmpleadoContactoEmergencia.Add(EmpleadoContactoEmergencia);
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

        // DELETE: api/EmpleadoContactoEmergencia/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteEmpleadoContactoEmergencia([FromRoute] int id)
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

                var respuesta = await db.EmpleadoContactoEmergencia.SingleOrDefaultAsync(m => m.IdEmpleadoContactoEmergencia == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.EmpleadoContactoEmergencia.Remove(respuesta);
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

        private Response Existe(EmpleadoContactoEmergencia EmpleadoContactoEmergencia)
        {
           
            var EmpleadoContactoEmergenciarespuesta = db.EmpleadoContactoEmergencia.Where(p => p.IdPersona == EmpleadoContactoEmergencia.IdPersona && p.IdEmpleado==EmpleadoContactoEmergencia.IdEmpleado&&p.IdParentesco== EmpleadoContactoEmergencia.IdParentesco).FirstOrDefault();
            if (EmpleadoContactoEmergenciarespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = EmpleadoContactoEmergenciarespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = EmpleadoContactoEmergenciarespuesta,
            };
        }
    }
}
