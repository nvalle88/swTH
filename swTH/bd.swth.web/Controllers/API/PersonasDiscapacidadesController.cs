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
    [Route("api/PersonasDiscapacidades")]
    public class PersonasDiscapacidadesController : Controller
    {
        private readonly SwTHDbContext db;

        public PersonasDiscapacidadesController(SwTHDbContext db)
        {
            this.db = db;
        }


        // GET: api/PersonaDiscapacidad
        [HttpGet]
        [Route("ListarPersonasDiscapacidades")]
        public async Task<List<PersonaDiscapacidad>> GetPersonaDiscapacidad()
        {
            try
            {
                return await db.PersonaDiscapacidad.Include(x => x.TipoDiscapacidad).Include(x => x.Persona).OrderBy(x => x.NumeroCarnet).ToListAsync();
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
                return new List<PersonaDiscapacidad>();
            }
        }

        // GET: api/PersonaDiscapacidad/5
        [HttpGet("{id}")]
        public async Task<Response> GetPersonaDiscapacidad([FromRoute] int id)
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

                var PersonaDiscapacidad = await db.PersonaDiscapacidad.SingleOrDefaultAsync(m => m.IdPersonaDiscapacidad == id);

                if (PersonaDiscapacidad == null)
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
                    Resultado = PersonaDiscapacidad,
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

        // PUT: api/PersonaDiscapacidad/5
        [HttpPut("{id}")]
        public async Task<Response> PutPersonaDiscapacidad([FromRoute] int id, [FromBody] PersonaDiscapacidad personaDiscapacidad)
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

                var existe = Existe(personaDiscapacidad);
                var PersonaDiscapacidadActualizar = (PersonaDiscapacidad)existe.Resultado;
                if (existe.IsSuccess)
                {
                    if (PersonaDiscapacidadActualizar.IdPersonaDiscapacidad == personaDiscapacidad.IdPersonaDiscapacidad)
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

                var PersonaDiscapacidad = db.PersonaDiscapacidad.Find(personaDiscapacidad.IdPersonaDiscapacidad);

                PersonaDiscapacidad.IdTipoDiscapacidad = PersonaDiscapacidad.IdTipoDiscapacidad;
                PersonaDiscapacidad.IdPersona = PersonaDiscapacidad.IdPersona;
                PersonaDiscapacidad.NumeroCarnet = PersonaDiscapacidad.NumeroCarnet;
                PersonaDiscapacidad.Porciento = PersonaDiscapacidad.Porciento;
                db.PersonaDiscapacidad.Update(PersonaDiscapacidad);

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

        // POST: api/PersonaDiscapacidad
        [HttpPost]
        [Route("InsertarPersonaDiscapacidad")]
        public async Task<Response> PostPersonaDiscapacidad([FromBody] PersonaDiscapacidad PersonaDiscapacidad)
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

                var respuesta = Existe(PersonaDiscapacidad);
                if (!respuesta.IsSuccess)
                {
                    db.PersonaDiscapacidad.Add(PersonaDiscapacidad);
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

        // DELETE: api/PersonaDiscapacidad/5
        [HttpDelete("{id}")]
        public async Task<Response> DeletePersonaDiscapacidad([FromRoute] int id)
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

                var respuesta = await db.PersonaDiscapacidad.SingleOrDefaultAsync(m => m.IdPersonaDiscapacidad == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.PersonaDiscapacidad.Remove(respuesta);
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

        private Response Existe(PersonaDiscapacidad PersonaDiscapacidad)
        {
            var numeroCarnet = PersonaDiscapacidad.NumeroCarnet;
            var PersonaDiscapacidadrespuesta = db.PersonaDiscapacidad.Where(p => p.NumeroCarnet == numeroCarnet && p.IdPersona == PersonaDiscapacidad.IdPersona).FirstOrDefault();

            if (PersonaDiscapacidadrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = PersonaDiscapacidadrespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = PersonaDiscapacidadrespuesta,
            };
        }
    }
}
