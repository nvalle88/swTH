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
    [Route("api/Personas")]
    public class PersonasController : Controller
    {
        private readonly SwTHDbContext db;

        public PersonasController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarPersonas")]
        public async Task<List<Persona>> GetCapacitacionesTemarios()
        {
            try
            {
                return await db.Persona.Include(x => x.Sexo).Include(x=>x.TipoIdentificacion).Include(x => x.EstadoCivil).Include(x => x.Genero).Include(x => x.Nacionalidad).Include(x => x.TipoSangre).Include(x => x.Etnia).OrderBy(x => x.Nombres).ToListAsync();
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
                return new List<Persona>();
            }
        }

        // GET: api/Personas/5
        [HttpGet("{id}")]
        public async Task<Response> GetPersona([FromRoute] int id)
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

                var Persona = await db.Persona.SingleOrDefaultAsync(m => m.IdPersona == id);

                if (Persona == null)
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
                    Resultado = Persona,
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

        // PUT: api/Personas/5
        [HttpPut("{id}")]
        public async Task<Response> PutPersona([FromRoute] int id, [FromBody] Persona Persona)
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

                var existe = Existe(Persona);
                var PersonaActualizar = (Persona)existe.Resultado;
                if (existe.IsSuccess)
                {
                    if (PersonaActualizar.IdPersona == Persona.IdPersona)
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
                var persona = db.Persona.Find(Persona.IdPersona);

                persona.Nombres = Persona.Nombres;
                persona.FechaNacimiento = Persona.FechaNacimiento;
                persona.IdSexo = Persona.IdSexo;
                persona.IdTipoIdentificacion = Persona.IdTipoIdentificacion;
                persona.IdEstadoCivil = Persona.IdEstadoCivil;
                persona.IdGenero = Persona.IdGenero;
                persona.IdNacionalidad = Persona.IdNacionalidad;
                persona.IdTipoSangre = Persona.IdTipoSangre;
                persona.IdEtnia = Persona.IdEtnia;
                persona.Identificacion = Persona.Identificacion;
                persona.Nombres = Persona.Nombres;
                persona.Apellidos = Persona.Apellidos;
                persona.TelefonoPrivado = Persona.TelefonoPrivado;
                persona.TelefonoCasa = Persona.TelefonoCasa;
                persona.CorreoPrivado = Persona.CorreoPrivado;
                persona.LugarTrabajo = Persona.LugarTrabajo;
 
                db.Persona.Update(persona);
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
        [Route("InsertarPersona")]
        public async Task<Response> PostPersona([FromBody] Persona Persona)
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

                var respuesta = Existe(Persona);
                if (!respuesta.IsSuccess)
                {
                    db.Persona.Add(Persona);
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
        public async Task<Response> DeletePersona([FromRoute] int id)
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

                var respuesta = await db.Persona.SingleOrDefaultAsync(m => m.IdPersona == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.Persona.Remove(respuesta);
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

        private Response Existe(Persona Persona)
        {
            var bdd = Persona.Nombres.ToUpper().TrimEnd().TrimStart();
            var Personarespuesta = db.Persona.Where(p => p.Nombres.ToUpper().TrimStart().TrimEnd() == bdd && p.IdSexo == Persona.IdSexo && p.IdTipoIdentificacion == Persona.IdTipoIdentificacion && p.IdEstadoCivil == Persona.IdEstadoCivil && p.IdGenero == Persona.IdGenero && p.IdNacionalidad == Persona.IdNacionalidad && p.IdTipoSangre == Persona.IdTipoSangre && p.IdEtnia == Persona.IdEtnia).FirstOrDefault();
            if (Personarespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = Personarespuesta,
                };

         }

            return new Response
            {
                IsSuccess = false,
                Resultado = Personarespuesta,
            };
        }
    }
}