using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.swth.entidades.Enumeradores;
using bd.swth.entidades.Utils;
using bd.log.guardar.Enumeradores;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/PersonaEnfermedades")]
    public class PersonaEnfermedadesController : Controller
    {
        private readonly SwTHDbContext db;

        public PersonaEnfermedadesController(SwTHDbContext db)
        {
            this.db = db;
        }


        // GET: api/PersonaEnfermedad
        [HttpGet]
        [Route("ListarPersonasEnfermedades")]
        public async Task<List<PersonaEnfermedad>> GetPersonaEnfermedad()
        {
            try
            {
                return await db.PersonaEnfermedad.Include(x => x.TipoEnfermedad).Include(x => x.Persona).OrderBy(x => x.InstitucionEmite).ToListAsync();
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
                return new List<PersonaEnfermedad>();
            }
        }

        [HttpPost]
        [Route("ListarEnfermedadesEmpleadoPorId")]
        public async Task<List<PersonaEnfermedad>> ListarEnfermedadesEmpleadoPorId([FromBody] Empleado empleado)
        {
            try
            {
                return await db.PersonaEnfermedad.Where(x => x.IdPersona == empleado.IdPersona).Include(x => x.TipoEnfermedad).Include(x => x.Persona).OrderBy(x => x.InstitucionEmite).ToListAsync();
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
                return new List<PersonaEnfermedad>();
            }
        }

        // GET: api/PersonaEnfermedad/5
        [HttpGet("{id}")]
        public async Task<Response> GetPersonaEnfermedad([FromRoute] int id)
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

                var PersonaEnfermedad = await db.PersonaEnfermedad.SingleOrDefaultAsync(m => m.IdPersonaEnfermedad == id);

                if (PersonaEnfermedad == null)
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
                    Resultado = PersonaEnfermedad,
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

        // PUT: api/PersonaEnfermedad/5
        [HttpPut("{id}")]
        public async Task<Response> PutPersonaEnfermedad([FromRoute] int id, [FromBody] PersonaEnfermedad PersonaEnfermedad)
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

                var existe = Existe(PersonaEnfermedad);
                var PersonaEnfermedadActualizar = (PersonaEnfermedad)existe.Resultado;
                if (
                    existe.IsSuccess 
                    && PersonaEnfermedadActualizar.IdPersonaEnfermedad != PersonaEnfermedad.IdPersonaEnfermedad
                    )
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var PersonaEnfermedadAct = await db.PersonaEnfermedad.Where(x => x.IdPersonaEnfermedad == PersonaEnfermedad.IdPersonaEnfermedad).FirstOrDefaultAsync();

                PersonaEnfermedadAct.IdTipoEnfermedad = PersonaEnfermedad.IdTipoEnfermedad;
                PersonaEnfermedadAct.IdPersona = PersonaEnfermedad.IdPersona;

                if (!String.IsNullOrEmpty(PersonaEnfermedad.InstitucionEmite)) {
                    PersonaEnfermedadAct.InstitucionEmite = PersonaEnfermedad.InstitucionEmite.ToString().ToUpper();
                }
                

                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.GuardadoSatisfactorio,
                };

            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Excepcion,
                };
            }

        }

        // POST: api/PersonaEnfermedad
        [HttpPost]
        [Route("InsertarPersonaEnfermedad")]
        public async Task<Response> PostPersonaEnfermedad([FromBody] PersonaEnfermedad PersonaEnfermedad)
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

                var respuesta = Existe(PersonaEnfermedad);
                if (!respuesta.IsSuccess)
                {

                    if (!string.IsNullOrEmpty(PersonaEnfermedad.InstitucionEmite))
                    {
                        PersonaEnfermedad.InstitucionEmite = PersonaEnfermedad.InstitucionEmite.ToString().ToUpper();
                    }

                    db.PersonaEnfermedad.Add(PersonaEnfermedad);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.GuardadoSatisfactorio
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
               
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }

        // DELETE: api/PersonaEnfermedad/5
        [HttpDelete("{id}")]
        public async Task<Response> DeletePersonaEnfermedad([FromRoute] int id)
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

                var respuesta = await db.PersonaEnfermedad.SingleOrDefaultAsync(m => m.IdPersonaEnfermedad == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.PersonaEnfermedad.Remove(respuesta);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.BorradoSatisfactorio,
                };
            }
            catch (Exception ex)
            {
                
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.BorradoNoSatisfactorio,
                };
            }
        }

        private Response Existe(PersonaEnfermedad PersonaEnfermedad)
        {
            var institucionemite = PersonaEnfermedad.InstitucionEmite.ToString().ToUpper();
            var idtipoenfermedad = PersonaEnfermedad.IdTipoEnfermedad;
            var idpersona = PersonaEnfermedad.IdPersona;

            var PersonaEnfermedadrespuesta = db.PersonaEnfermedad.Where(p => p.InstitucionEmite == institucionemite
            && p.IdTipoEnfermedad == idtipoenfermedad
            && p.IdPersona == idpersona).FirstOrDefault();

            if (PersonaEnfermedadrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = PersonaEnfermedadrespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = PersonaEnfermedadrespuesta,
            };
        }
    }
}