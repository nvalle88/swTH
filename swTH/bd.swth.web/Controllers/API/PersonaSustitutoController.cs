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
using bd.webappth.entidades.ViewModels;
using bd.swth.entidades.ViewModels;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/PersonaSustituto")]
    public class PersonaSustitutoController : Controller
    {
        private readonly SwTHDbContext db;

        public PersonaSustitutoController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarPersonasSustitutos")]
        public async Task<List<PersonaSustituto>> GetPersonaSustituto()
        {
            try
            {
                return await db.PersonaSustituto.Include(x => x.Persona).Include(x => x.Parentesco).OrderBy(x => x.IdPersona).ToListAsync();
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
                return new List<PersonaSustituto>();
            }
        }

       
        [HttpPost]
        [Route("ObtenerPersonaSustitutosPorId")]
        public async Task<Response> ObtenerPersonaSustitutosPorId([FromBody] Empleado empleado)
        {
            try
            {
                var PersonaSustituto = await db.PersonaSustituto.Include(x => x.Persona).Include(x => x.Parentesco).Include(x => x.PersonaDiscapacidad).Include(x => x.DiscapacidadSustituto).Include(x => x.EnfermedadSustituto).SingleOrDefaultAsync(x => x.IdPersona == empleado.IdPersona);
                var response = new Response
                {
                    IsSuccess = true,
                    Resultado = PersonaSustituto,
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

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetPersonaSustituto([FromRoute] int id)
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

                var PersonaSustituto = await db.PersonaSustituto.Include(x => x.Parentesco)
                    .Include(x=>x.DiscapacidadSustituto).Include(x=>x.EnfermedadSustituto)
                    .Include(x=>x.PersonaDiscapacidad).ThenInclude(x=>x.Persona)
                    .SingleOrDefaultAsync(m => m.IdPersonaSustituto == id);

                if (PersonaSustituto == null)
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
                    Resultado = PersonaSustituto,
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
        public async Task<Response> PutPersonaSustituto([FromRoute] int id, [FromBody] EmpleadoSustitutoViewModel empleadoSustitutoViewModel)
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

                var PersonaActual = await db.Persona.Where(x => x.IdPersona == empleadoSustitutoViewModel.IdPersona).FirstOrDefaultAsync();
                if (PersonaActual.Identificacion == empleadoSustitutoViewModel.Identificacion)
                {
                    using (var transaction = await db.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            if (empleadoSustitutoViewModel.IdNacionalidadIndigena == 0)
                            {
                                empleadoSustitutoViewModel.IdNacionalidadIndigena = null;
                            }
                            //1. Actualizar Persona 

                            PersonaActual.FechaNacimiento = empleadoSustitutoViewModel.FechaNacimiento;
                            PersonaActual.IdSexo = empleadoSustitutoViewModel.IdSexo;
                            PersonaActual.IdTipoIdentificacion = empleadoSustitutoViewModel.IdTipoIdentificacion;
                            PersonaActual.IdEstadoCivil = empleadoSustitutoViewModel.IdEstadoCivil;
                            PersonaActual.IdGenero = empleadoSustitutoViewModel.IdGenero;
                            PersonaActual.IdNacionalidad = empleadoSustitutoViewModel.IdNacionalidad;
                            PersonaActual.IdTipoSangre = empleadoSustitutoViewModel.IdTipoSangre;
                            PersonaActual.IdEtnia = empleadoSustitutoViewModel.IdEtnia;
                            PersonaActual.Identificacion = empleadoSustitutoViewModel.Identificacion;
                            PersonaActual.Nombres = empleadoSustitutoViewModel.Nombres;
                            PersonaActual.Apellidos = empleadoSustitutoViewModel.Apellidos;
                            PersonaActual.TelefonoPrivado = empleadoSustitutoViewModel.TelefonoPrivado;
                            PersonaActual.TelefonoCasa = empleadoSustitutoViewModel.TelefonoCasa;
                            PersonaActual.CorreoPrivado = empleadoSustitutoViewModel.CorreoPrivado;
                            PersonaActual.LugarTrabajo = empleadoSustitutoViewModel.LugarTrabajo;
                            PersonaActual.IdNacionalidadIndigena = empleadoSustitutoViewModel.IdNacionalidadIndigena;
                            PersonaActual.CallePrincipal = empleadoSustitutoViewModel.CallePrincipal;
                            PersonaActual.CalleSecundaria = empleadoSustitutoViewModel.CalleSecundaria;
                            PersonaActual.Referencia = empleadoSustitutoViewModel.Referencia;
                            PersonaActual.Numero = empleadoSustitutoViewModel.Numero;
                            PersonaActual.IdParroquia = empleadoSustitutoViewModel.IdParroquia;
                            PersonaActual.Ocupacion = empleadoSustitutoViewModel.Ocupacion;

                            //2. Actualizar PersonaDiscapacidad
                            var PersonaDiscapacidadActualizar = await db.PersonaDiscapacidad.Where(x => x.IdPersona == empleadoSustitutoViewModel.IdPersona).FirstOrDefaultAsync();

                            PersonaDiscapacidadActualizar.IdPersona = empleadoSustitutoViewModel.IdPersona;
                            PersonaDiscapacidadActualizar.IdTipoDiscapacidad = empleadoSustitutoViewModel.IdTipoDiscapacidad;
                            PersonaDiscapacidadActualizar.NumeroCarnet = empleadoSustitutoViewModel.NumeroCarnet;
                            PersonaDiscapacidadActualizar.Porciento = empleadoSustitutoViewModel.Porciento;


                            //2. Actualizar PersonaSustituto
                            var PersonaSustitutoActualizar = await db.PersonaSustituto.Where(x => x.IdPersona == empleadoSustitutoViewModel.IdPersona).FirstOrDefaultAsync();

                            PersonaSustitutoActualizar.IdPersona = empleadoSustitutoViewModel.IdPersona;
                            PersonaSustitutoActualizar.IdParentesco = empleadoSustitutoViewModel.IdParentesco;
                            PersonaSustitutoActualizar.IdPersonaDiscapacidad = empleadoSustitutoViewModel.IdPersonaDiscapacidad;

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
                else
                {
                    var existe = Existe(empleadoSustitutoViewModel);
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
                                if (empleadoSustitutoViewModel.IdNacionalidadIndigena == 0)
                                {
                                    empleadoSustitutoViewModel.IdNacionalidadIndigena = null;
                                }
                                //1. Actualizar Persona 

                                PersonaActual.FechaNacimiento = empleadoSustitutoViewModel.FechaNacimiento;
                                PersonaActual.IdSexo = empleadoSustitutoViewModel.IdSexo;
                                PersonaActual.IdTipoIdentificacion = empleadoSustitutoViewModel.IdTipoIdentificacion;
                                PersonaActual.IdEstadoCivil = empleadoSustitutoViewModel.IdEstadoCivil;
                                PersonaActual.IdGenero = empleadoSustitutoViewModel.IdGenero;
                                PersonaActual.IdNacionalidad = empleadoSustitutoViewModel.IdNacionalidad;
                                PersonaActual.IdTipoSangre = empleadoSustitutoViewModel.IdTipoSangre;
                                PersonaActual.IdEtnia = empleadoSustitutoViewModel.IdEtnia;
                                PersonaActual.Identificacion = empleadoSustitutoViewModel.Identificacion;
                                PersonaActual.Nombres = empleadoSustitutoViewModel.Nombres;
                                PersonaActual.Apellidos = empleadoSustitutoViewModel.Apellidos;
                                PersonaActual.TelefonoPrivado = empleadoSustitutoViewModel.TelefonoPrivado;
                                PersonaActual.TelefonoCasa = empleadoSustitutoViewModel.TelefonoCasa;
                                PersonaActual.CorreoPrivado = empleadoSustitutoViewModel.CorreoPrivado;
                                PersonaActual.LugarTrabajo = empleadoSustitutoViewModel.LugarTrabajo;
                                PersonaActual.IdNacionalidadIndigena = empleadoSustitutoViewModel.IdNacionalidadIndigena;
                                PersonaActual.CallePrincipal = empleadoSustitutoViewModel.CallePrincipal;
                                PersonaActual.CalleSecundaria = empleadoSustitutoViewModel.CalleSecundaria;
                                PersonaActual.Referencia = empleadoSustitutoViewModel.Referencia;
                                PersonaActual.Numero = empleadoSustitutoViewModel.Numero;
                                PersonaActual.IdParroquia = empleadoSustitutoViewModel.IdParroquia;
                                PersonaActual.Ocupacion = empleadoSustitutoViewModel.Ocupacion;

                                //2. Actualizar PersonaDiscapacidad
                                var PersonaDiscapacidadActualizar = await db.PersonaDiscapacidad.Where(x => x.IdPersona == empleadoSustitutoViewModel.IdPersona).FirstOrDefaultAsync();

                                PersonaDiscapacidadActualizar.IdPersona = empleadoSustitutoViewModel.IdPersona;
                                PersonaDiscapacidadActualizar.IdTipoDiscapacidad = empleadoSustitutoViewModel.IdTipoDiscapacidad;
                                PersonaDiscapacidadActualizar.NumeroCarnet = empleadoSustitutoViewModel.NumeroCarnet;
                                PersonaDiscapacidadActualizar.Porciento = empleadoSustitutoViewModel.Porciento;


                                //3. Actualizar PersonaSustituto
                                var PersonaSustitutoActualizar = await db.PersonaSustituto.Where(x => x.IdPersona == empleadoSustitutoViewModel.IdPersona).FirstOrDefaultAsync();

                                PersonaSustitutoActualizar.IdPersona = empleadoSustitutoViewModel.IdPersona;
                                PersonaSustitutoActualizar.IdParentesco = empleadoSustitutoViewModel.IdParentesco;
                                PersonaSustitutoActualizar.IdPersonaDiscapacidad = empleadoSustitutoViewModel.IdPersonaDiscapacidad;

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
        [Route("InsertarPersonaSustituto")]
        public async Task<Response> InsertarPersonaSustituto([FromBody] EmpleadoSustitutoViewModel empleadoSustitutoViewModel)
        {
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {

                    var respuesta = Existe(empleadoSustitutoViewModel);
                    if (empleadoSustitutoViewModel.IdNacionalidadIndigena == 0)
                    {
                        empleadoSustitutoViewModel.IdNacionalidadIndigena = null;
                    }
                    if (!respuesta.IsSuccess)
                    {

                        //1. Insertar Persona
                        
                        var persona = new Persona()
                        {
                            FechaNacimiento = empleadoSustitutoViewModel.FechaNacimiento,
                            IdSexo = empleadoSustitutoViewModel.IdSexo,
                            IdTipoIdentificacion = empleadoSustitutoViewModel.IdTipoIdentificacion,
                            IdEstadoCivil = empleadoSustitutoViewModel.IdEstadoCivil,
                            IdGenero = empleadoSustitutoViewModel.IdGenero,
                            IdNacionalidad = empleadoSustitutoViewModel.IdNacionalidad,
                            IdTipoSangre = empleadoSustitutoViewModel.IdTipoSangre,
                            IdEtnia = empleadoSustitutoViewModel.IdEtnia,
                            Identificacion = empleadoSustitutoViewModel.Identificacion,
                            Nombres = empleadoSustitutoViewModel.Nombres,
                            Apellidos = empleadoSustitutoViewModel.Apellidos,
                            TelefonoPrivado = empleadoSustitutoViewModel.TelefonoPrivado,
                            TelefonoCasa = empleadoSustitutoViewModel.TelefonoCasa,
                            CorreoPrivado = empleadoSustitutoViewModel.CorreoPrivado,
                            LugarTrabajo = empleadoSustitutoViewModel.LugarTrabajo,
                            IdNacionalidadIndigena = empleadoSustitutoViewModel.IdNacionalidadIndigena,
                            CallePrincipal = empleadoSustitutoViewModel.CallePrincipal,
                            CalleSecundaria = empleadoSustitutoViewModel.CalleSecundaria,
                            Referencia = empleadoSustitutoViewModel.Referencia,
                            Numero = empleadoSustitutoViewModel.Numero,
                            IdParroquia = empleadoSustitutoViewModel.IdParroquia,
                            Ocupacion = empleadoSustitutoViewModel.Ocupacion

                        };

                        var personaInsertarda = await db.Persona.AddAsync(persona);
                        await db.SaveChangesAsync();

                        //2. Insertar PersonaDiscapacidad
                        var PersonaDiscapacidad = new PersonaDiscapacidad()
                        {
                            IdPersona = personaInsertarda.Entity.IdPersona,
                            IdTipoDiscapacidad = empleadoSustitutoViewModel.IdTipoDiscapacidad,
                            NumeroCarnet = empleadoSustitutoViewModel.NumeroCarnet,
                            Porciento = empleadoSustitutoViewModel.Porciento
                        };
                        var PersonaDiscapacidadInsertado = await db.PersonaDiscapacidad.AddAsync(PersonaDiscapacidad);
                        await db.SaveChangesAsync();

                        //3. Insertar PersonaSustituto
                        var PersonaSustituto = new PersonaSustituto()
                        {
                            IdPersona = empleadoSustitutoViewModel.IdEmpleado,
                            IdParentesco= empleadoSustitutoViewModel.IdParentesco,
                            IdPersonaDiscapacidad= PersonaDiscapacidadInsertado.Entity.IdPersonaDiscapacidad
                        };
                        var PersonaSustitutoInsertado = await db.PersonaSustituto.AddAsync(PersonaSustituto);
                        await db.SaveChangesAsync();

                        transaction.Commit();

                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.Satisfactorio,
                            Resultado = PersonaSustitutoInsertado.Entity
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
        public async Task<Response> DeletePersonaSustituto([FromRoute] int id)
        {
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {


                    //Eliminar Persona
                    var respuestaPersonaSustituto = await db.PersonaSustituto.SingleOrDefaultAsync(m => m.IdPersona == id);
                    var respuestaPersona = await db.Persona.SingleOrDefaultAsync(m => m.IdPersona == id);
                    var respuestaPersonaDiscapacidad = await db.PersonaDiscapacidad.SingleOrDefaultAsync(m => m.IdPersona == id);
                    if (respuestaPersona == null)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = Mensaje.RegistroNoEncontrado,
                        };
                    }
                    db.PersonaSustituto.Remove(respuestaPersonaSustituto);
                    db.Persona.Remove(respuestaPersona);
                    db.PersonaDiscapacidad.Remove(respuestaPersonaDiscapacidad);
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


        private Response Existe(EmpleadoSustitutoViewModel empleadoSustitutoViewModel)
        {
            var identificacion = empleadoSustitutoViewModel.Identificacion.ToUpper().TrimEnd().TrimStart();
            var Empleadorespuesta = db.Persona.Where(p => p.Identificacion == identificacion).FirstOrDefault();
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