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
    [Route("api/PersonasEstudios")]
    public class PersonasEstudiosController : Controller
    {
        private readonly SwTHDbContext db;

        public PersonasEstudiosController(SwTHDbContext db)
        {
            this.db = db;
        }


        // GET: api/PersonaEstudio
        [HttpGet]
        [Route("ListarPersonasEstudios")]
        public async Task<List<PersonaEstudio>> GetPersonasEstudios()
        {
            try
            {
                return await db.PersonaEstudio.Include(x => x.Titulo).Include(x => x.Persona).OrderBy(x => x.FechaGraduado).ToListAsync();
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
                return new List<PersonaEstudio>();
            }
        }

        // GET: api/PersonaEstudio
        [HttpPost]
        [Route("ListarEstudiosporEmpleado")]
        public async Task<List<PersonaEstudio>> GetEstudiosById([FromBody] Empleado empleado)
        {
            try
            {
                return await db.PersonaEstudio.Where(x=>x.IdPersona == empleado.IdPersona).Include(x => x.Persona).Include(x => x.Titulo.AreaConocimiento).Include(x => x.Titulo.Estudio).OrderBy(x => x.FechaGraduado).ToListAsync();
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
                return new List<PersonaEstudio>();
            }
        }

        // GET: api/PersonaEstudio/5
        [HttpGet("{id}")]
        public async Task<Response> GetPersonaEstudio([FromRoute] int id)
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

                var PersonaEstudio = await db.PersonaEstudio.SingleOrDefaultAsync(m => m.IdPersonaEstudio == id);

                if (PersonaEstudio == null)
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
                    Resultado = PersonaEstudio,
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

        // PUT: api/PersonaEstudio/5
        [HttpPut("{id}")]
        public async Task<Response> PutPersonaEstudio([FromRoute] int id, [FromBody] PersonaEstudio personaEstudio)
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

                var existe = Existe(personaEstudio);
                var PersonaEstudioActualizar = (PersonaEstudio)existe.Resultado;
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }
                var PersonaEstudio = await db.PersonaEstudio.Where(x => x.IdPersonaEstudio == personaEstudio.IdPersonaEstudio).FirstOrDefaultAsync();
                
                PersonaEstudio.FechaGraduado = personaEstudio.FechaGraduado;
                PersonaEstudio.Observaciones = personaEstudio.Observaciones;
                PersonaEstudio.IdTitulo = personaEstudio.IdTitulo;
                PersonaEstudio.IdPersona = personaEstudio.IdPersona;
                PersonaEstudio.NoSenescyt = personaEstudio.NoSenescyt;
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

        // POST: api/PersonaEstudio
        [HttpPost]
        [Route("InsertarPersonaEstudio")]
        public async Task<Response> PostPersonaEstudio([FromBody] PersonaEstudio PersonaEstudio)
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

                var respuesta = Existe(PersonaEstudio);
                if (!respuesta.IsSuccess)
                {
                    db.PersonaEstudio.Add(PersonaEstudio);
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

        // DELETE: api/PersonaEstudio/5
        [HttpDelete("{id}")]
        public async Task<Response> DeletePersonaEstudio([FromRoute] int id)
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

                var respuesta = await db.PersonaEstudio.SingleOrDefaultAsync(m => m.IdPersonaEstudio == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.PersonaEstudio.Remove(respuesta);
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

        private Response Existe(PersonaEstudio PersonaEstudio)
        {
            var fechaGraduado = PersonaEstudio.FechaGraduado;
            var PersonaEstudiorespuesta = db.PersonaEstudio.Where(p => p.FechaGraduado == fechaGraduado 
            && p.IdPersona == PersonaEstudio.IdPersona 
            && p.IdTitulo == PersonaEstudio.IdTitulo
            && p.Observaciones == PersonaEstudio.Observaciones
            && p.NoSenescyt == PersonaEstudio.NoSenescyt).FirstOrDefault();
            if (PersonaEstudiorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = PersonaEstudiorespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = PersonaEstudiorespuesta,
            };
        }
    }
}
