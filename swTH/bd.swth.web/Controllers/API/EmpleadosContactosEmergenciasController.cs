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
using bd.webappth.entidades.ViewModels;

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
                var EmpleadoContactoEmergencia = await db.EmpleadoContactoEmergencia.Include(x => x.Persona).Include(x => x.Empleado).Include(x => x.Parentesco).SingleOrDefaultAsync(m => m.IdEmpleado == empleadoContactoEmergencia.IdEmpleado);

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

                var EmpleadoContactoEmergencia = await db.EmpleadoContactoEmergencia.Include(x=>x.Persona).Include(x=>x.Parentesco).SingleOrDefaultAsync(m => m.IdPersona == id);

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

        // PUT: api/BasesDatos/5
        [HttpPut("{id}")]
        public async Task<Response> PutEmpleadoContactoEmergencia([FromRoute] int id, [FromBody] EmpleadoFamiliarViewModel empleadoFamiliarViewModel)
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

                var PersonaActual = await db.Persona.Where(x => x.IdPersona == empleadoFamiliarViewModel.IdPersona).FirstOrDefaultAsync();
                
                    var existe = Existe(empleadoFamiliarViewModel);
                    if (existe.IsSuccess)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = Mensaje.ExisteRegistro,
                        };
                    }
                    else
                    {
                        using (var transaction = await db.Database.BeginTransactionAsync())
                        {
                            try
                            {
                                if (empleadoFamiliarViewModel.IdNacionalidadIndigena == 0)
                                {
                                    empleadoFamiliarViewModel.IdNacionalidadIndigena = null;
                                }
                                //1. Actualizar Persona 
                                PersonaActual.Nombres = empleadoFamiliarViewModel.Nombres;
                                PersonaActual.Apellidos = empleadoFamiliarViewModel.Apellidos;
                                PersonaActual.TelefonoPrivado = empleadoFamiliarViewModel.TelefonoPrivado;
                                PersonaActual.TelefonoCasa = empleadoFamiliarViewModel.TelefonoCasa;
                                PersonaActual.CorreoPrivado = empleadoFamiliarViewModel.CorreoPrivado;


                                //2. Actualizar EmpleadoContactoEmergencia
                                var EmpleadoContactoEmergenciaActualizar = await db.EmpleadoContactoEmergencia.Where(x => x.IdPersona == empleadoFamiliarViewModel.IdPersona).FirstOrDefaultAsync();

                                EmpleadoContactoEmergenciaActualizar.IdPersona = empleadoFamiliarViewModel.IdPersona;
                                EmpleadoContactoEmergenciaActualizar.IdEmpleado = empleadoFamiliarViewModel.IdEmpleado;
                                EmpleadoContactoEmergenciaActualizar.IdParentesco = empleadoFamiliarViewModel.IdParentesco;

                                await db.SaveChangesAsync();


                                transaction.Commit();

                                return new Response
                                {
                                    IsSuccess = true,
                                    Message = Mensaje.Satisfactorio,
                                };

                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
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
                    

                }
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


        [HttpPost]
        [Route("InsertarEmpleadoContactoEmergencia")]
        public async Task<Response> InsertarEmpleadoContactoEmergencia([FromBody] EmpleadoFamiliarViewModel empleadoFamiliarViewModel)
        {
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {

                    var respuesta = Existe(empleadoFamiliarViewModel);
                    if (empleadoFamiliarViewModel.IdNacionalidadIndigena == 0)
                    {
                        empleadoFamiliarViewModel.IdNacionalidadIndigena = null;
                    }
                    if (!respuesta.IsSuccess)
                    {

                        var persona = new Persona()
                        {

                            Nombres = empleadoFamiliarViewModel.Nombres,
                            Apellidos = empleadoFamiliarViewModel.Apellidos,
                            TelefonoPrivado = empleadoFamiliarViewModel.TelefonoPrivado,
                            TelefonoCasa = empleadoFamiliarViewModel.TelefonoCasa,
                        };

                        //1. Insertar Persona 

                        var personaInsertarda = await db.Persona.AddAsync(persona);
                        await db.SaveChangesAsync();

                        //2. Insertar EmpleadoFamiliar
                        var empleadoContactoEmergencia = new EmpleadoContactoEmergencia()
                        {
                            IdPersona = personaInsertarda.Entity.IdPersona,
                            IdEmpleado = empleadoFamiliarViewModel.IdEmpleado,
                            IdParentesco = empleadoFamiliarViewModel.IdParentesco
                        };
                        var empleadoContactoEmergenciaInsertado = await db.EmpleadoContactoEmergencia.AddAsync(empleadoContactoEmergencia);
                        await db.SaveChangesAsync();

                        transaction.Commit();

                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.Satisfactorio,
                            Resultado = empleadoContactoEmergenciaInsertado.Entity
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
                    transaction.Rollback();
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

        }
        // DELETE: api/BasesDatos/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteEmpleadoContactoEmergencia([FromRoute] int id)
        {
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {


                    //Eliminar Persona
                    var respuestaEmpleadoContactoEmergencia = await db.EmpleadoContactoEmergencia.SingleOrDefaultAsync(m => m.IdPersona == id);
                    var respuestaPersona = await db.Persona.SingleOrDefaultAsync(m => m.IdPersona == id);
                    if (respuestaPersona == null)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = Mensaje.RegistroNoEncontrado,
                        };
                    }
                    db.EmpleadoContactoEmergencia.Remove(respuestaEmpleadoContactoEmergencia);
                    db.Persona.Remove(respuestaPersona);
                    await db.SaveChangesAsync();
                    transaction.Commit();


                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                    };
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
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
        }


        private Response Existe(EmpleadoFamiliarViewModel empleadoFamiliarViewModel)
        {
            var nombre = empleadoFamiliarViewModel.Nombres;
            var apellido = empleadoFamiliarViewModel.Apellidos;
            var celular = empleadoFamiliarViewModel.TelefonoPrivado;
            var telefono = empleadoFamiliarViewModel.TelefonoCasa;
            var idparentesco = empleadoFamiliarViewModel.IdParentesco;
            var Empleadorespuesta = db.Persona.Include(x=>x.EmpleadoContactoEmergencia).ThenInclude(x=>x.Parentesco).Where(p => p.Nombres == nombre
            && p.Apellidos == apellido
            && p.TelefonoPrivado == celular
            && p.TelefonoCasa == telefono
            && p.EmpleadoContactoEmergencia.FirstOrDefault().IdParentesco == idparentesco).FirstOrDefault();
            if (Empleadorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                };

            }

            return new Response
            {
                IsSuccess = false,
            };
        }
    }
}
