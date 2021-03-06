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
using bd.swth.entidades.ViewModels;
using bd.webappth.entidades.ViewModels;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/EmpleadoFamiliares")]
    public class EmpleadoFamiliaresController : Controller
    {
        private readonly SwTHDbContext db;

        public EmpleadoFamiliaresController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarEmpleadoFamiliares")]
        public async Task<List<EmpleadoFamiliar>> GetEmpleadoFamiliar()
        {
            try
            {
                return await db.EmpleadoFamiliar.Include(x=>x.Persona).Include(x=>x.Parentesco).OrderBy(x => x.IdPersona).ToListAsync();
            }
            catch (Exception ex)
            {
                
                return new List<EmpleadoFamiliar>();
            }
        }

        [HttpPost]
        [Route("ListarEmpleadosFamiliaresPorId")]
        public async Task<List<EmpleadoFamiliar>> ListarEmpleadosFamiliaresPorId([FromBody] Empleado empleado)
        {
            try
            {
                return await db.EmpleadoFamiliar.Where(x => x.IdEmpleado == empleado.IdEmpleado).Include(x=>x.Persona).Include(x=>x.Parentesco).OrderBy(x => x.IdPersona).ToListAsync();
            }
            catch (Exception ex)
            {
               
                return new List<EmpleadoFamiliar>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetEmpleadoFamiliar([FromRoute] int id)
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

                var EmpleadoFamiliar = await db.EmpleadoFamiliar.Include(x=>x.Persona).Include(x=>x.Parentesco).SingleOrDefaultAsync(m => m.IdEmpleadoFamiliar == id);

                if (EmpleadoFamiliar == null)
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
                    Resultado = EmpleadoFamiliar,
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

        // PUT: api/BasesDatos/5
        [HttpPut("{id}")]
        public async Task<Response> PutEmpleadoFamiliar([FromRoute] int id, [FromBody] EmpleadoFamiliarViewModel empleadoFamiliarViewModel)
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
                if (PersonaActual.Identificacion == empleadoFamiliarViewModel.Identificacion)
                {
                    using (var transaction = await db.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            //1. Actualizar Persona 

                            PersonaActual.FechaNacimiento = empleadoFamiliarViewModel.FechaNacimiento;
                            PersonaActual.IdSexo = empleadoFamiliarViewModel.IdSexo;
                            PersonaActual.IdTipoIdentificacion = empleadoFamiliarViewModel.IdTipoIdentificacion;
                            PersonaActual.IdEstadoCivil = empleadoFamiliarViewModel.IdEstadoCivil;
                            PersonaActual.IdGenero = empleadoFamiliarViewModel.IdGenero;
                            PersonaActual.Identificacion = empleadoFamiliarViewModel.Identificacion;
                            PersonaActual.Nombres = empleadoFamiliarViewModel.Nombres;
                            PersonaActual.Apellidos = empleadoFamiliarViewModel.Apellidos;
                            PersonaActual.TelefonoPrivado = empleadoFamiliarViewModel.TelefonoPrivado;
                            PersonaActual.LugarTrabajo = empleadoFamiliarViewModel.LugarTrabajo;
                            PersonaActual.Ocupacion = empleadoFamiliarViewModel.Ocupacion;
                            PersonaActual.TelefonoCasa = empleadoFamiliarViewModel.TelefonoCasa;
                            
                            //await db.SaveChangesAsync();


                            //2. Actualizar EmpleadoFamiliar
                            var EmpleadoFamiliarActualizar = await db.EmpleadoFamiliar.Where(x => x.IdPersona == empleadoFamiliarViewModel.IdPersona).FirstOrDefaultAsync();

                            EmpleadoFamiliarActualizar.IdPersona = empleadoFamiliarViewModel.IdPersona;
                            EmpleadoFamiliarActualizar.IdEmpleado = empleadoFamiliarViewModel.IdEmpleado;
                            EmpleadoFamiliarActualizar.IdParentesco = empleadoFamiliarViewModel.IdParentesco;

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
                }
                else
                {
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
                                //1. Actualizar Persona 

                                PersonaActual.FechaNacimiento = empleadoFamiliarViewModel.FechaNacimiento;
                                PersonaActual.IdSexo = empleadoFamiliarViewModel.IdSexo;
                                PersonaActual.IdTipoIdentificacion = empleadoFamiliarViewModel.IdTipoIdentificacion;
                                PersonaActual.IdEstadoCivil = empleadoFamiliarViewModel.IdEstadoCivil;
                                PersonaActual.IdGenero = empleadoFamiliarViewModel.IdGenero;
                                PersonaActual.Identificacion = empleadoFamiliarViewModel.Identificacion;
                                PersonaActual.Nombres = empleadoFamiliarViewModel.Nombres;
                                PersonaActual.Apellidos = empleadoFamiliarViewModel.Apellidos;
                                PersonaActual.TelefonoPrivado = empleadoFamiliarViewModel.TelefonoPrivado;
                                PersonaActual.LugarTrabajo = empleadoFamiliarViewModel.LugarTrabajo;
                                PersonaActual.Ocupacion = empleadoFamiliarViewModel.Ocupacion;

                                //await db.SaveChangesAsync();

                                //2. Actualizar EmpleadoFamiliar
                                var EmpleadoFamiliarActualizar = await db.EmpleadoFamiliar.Where(x => x.IdPersona == empleadoFamiliarViewModel.IdPersona).FirstOrDefaultAsync();

                                EmpleadoFamiliarActualizar.IdPersona = empleadoFamiliarViewModel.IdPersona;
                                EmpleadoFamiliarActualizar.IdEmpleado = empleadoFamiliarViewModel.IdEmpleado;
                                EmpleadoFamiliarActualizar.IdParentesco = empleadoFamiliarViewModel.IdParentesco;

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
        [Route("InsertarEmpleadoFamiliar")]
        public async Task<Response> InsertarEmpleadoFamiliar([FromBody] EmpleadoFamiliarViewModel empleadoFamiliarViewModel)
        {
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {

                    var respuesta = Existe(empleadoFamiliarViewModel);
                    if (!respuesta.IsSuccess)
                    {

                        var persona = new Persona()
                        {
                            FechaNacimiento = empleadoFamiliarViewModel.FechaNacimiento,
                            IdSexo = empleadoFamiliarViewModel.IdSexo,
                            IdTipoIdentificacion = empleadoFamiliarViewModel.IdTipoIdentificacion,
                            IdEstadoCivil = empleadoFamiliarViewModel.IdEstadoCivil,
                            IdGenero = empleadoFamiliarViewModel.IdGenero,
                            Identificacion = empleadoFamiliarViewModel.Identificacion,
                            Nombres = empleadoFamiliarViewModel.Nombres,
                            Apellidos = empleadoFamiliarViewModel.Apellidos,
                            TelefonoPrivado = empleadoFamiliarViewModel.TelefonoPrivado,
                            LugarTrabajo = empleadoFamiliarViewModel.LugarTrabajo,
                            Ocupacion = empleadoFamiliarViewModel.Ocupacion,
                            TelefonoCasa=empleadoFamiliarViewModel.TelefonoCasa,

                        };

                        //1. Insertar Persona 

                        var personaInsertarda = await db.Persona.AddAsync(persona);
                        await db.SaveChangesAsync();

                        //2. Insertar EmpleadoFamiliar
                        var empleadoFamiliar = new EmpleadoFamiliar()
                        {
                            IdPersona = personaInsertarda.Entity.IdPersona,
                            IdEmpleado = empleadoFamiliarViewModel.IdEmpleado,
                            IdParentesco = empleadoFamiliarViewModel.IdParentesco
                        };
                        var empleadoFamiliarInsertado = await db.EmpleadoFamiliar.AddAsync(empleadoFamiliar);
                        await db.SaveChangesAsync();

                        transaction.Commit();

                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.Satisfactorio,
                            Resultado = empleadoFamiliarInsertado.Entity
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

        }
        // DELETE: api/BasesDatos/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteEmpleadoFamiliar([FromRoute] int id)
        {
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {


                    //Eliminar Persona
                    var respuestaEmpleadoFamiliar = await db.EmpleadoFamiliar.SingleOrDefaultAsync(m => m.IdPersona == id);
                    var respuestaPersona = await db.Persona.SingleOrDefaultAsync(m => m.IdPersona == id);
                    if (respuestaPersona == null)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = Mensaje.RegistroNoEncontrado,
                        };
                    }
                    db.EmpleadoFamiliar.Remove(respuestaEmpleadoFamiliar);
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
            var identificacion = empleadoFamiliarViewModel.Identificacion.ToUpper().TrimEnd().TrimStart();
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