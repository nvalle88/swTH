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
                return await db.EmpleadoFamiliar.OrderBy(x => x.IdPersona).ToListAsync();
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

                var EmpleadoFamiliar = await db.EmpleadoFamiliar.SingleOrDefaultAsync(m => m.IdEmpleadoFamiliar == id);

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

                var existe = Existe(empleadoFamiliarViewModel);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var EmpleadoFamiliarActualizar = await db.EmpleadoFamiliar.Where(x => x.IdEmpleadoFamiliar == id).FirstOrDefaultAsync();

                if (EmpleadoFamiliarActualizar != null)
                {
                    try
                    {
                        //EmpleadoFamiliarActualizar.IdPersona = EmpleadoFamiliar.IdPersona;
                        //EmpleadoFamiliarActualizar.IdEmpleado = EmpleadoFamiliar.IdEmpleado;
                        //EmpleadoFamiliarActualizar.IdParentesco = EmpleadoFamiliar.IdParentesco;
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




                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ExisteRegistro
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
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
                    if (empleadoFamiliarViewModel.IdNacionalidadIndigena == 0)
                    {
                        empleadoFamiliarViewModel.IdNacionalidadIndigena = null;
                    }
                    if (!respuesta.IsSuccess)
                    {

                        var persona = new Persona()
                        {
                            FechaNacimiento = empleadoFamiliarViewModel.FechaNacimiento,
                            IdSexo = empleadoFamiliarViewModel.IdSexo,
                            IdTipoIdentificacion = empleadoFamiliarViewModel.IdTipoIdentificacion,
                            IdEstadoCivil = empleadoFamiliarViewModel.IdEstadoCivil,
                            IdGenero = empleadoFamiliarViewModel.IdGenero,
                            IdNacionalidad = empleadoFamiliarViewModel.IdNacionalidad,
                            IdTipoSangre = empleadoFamiliarViewModel.IdTipoSangre,
                            IdEtnia = empleadoFamiliarViewModel.IdEtnia,
                            Identificacion = empleadoFamiliarViewModel.Identificacion,
                            Nombres = empleadoFamiliarViewModel.Nombres,
                            Apellidos = empleadoFamiliarViewModel.Apellidos,
                            TelefonoPrivado = empleadoFamiliarViewModel.TelefonoPrivado,
                            TelefonoCasa = empleadoFamiliarViewModel.TelefonoCasa,
                            CorreoPrivado = empleadoFamiliarViewModel.CorreoPrivado,
                            LugarTrabajo = empleadoFamiliarViewModel.LugarTrabajo,
                            IdNacionalidadIndigena = empleadoFamiliarViewModel.IdNacionalidadIndigena,
                            CallePrincipal = empleadoFamiliarViewModel.CallePrincipal,
                            CalleSecundaria = empleadoFamiliarViewModel.CalleSecundaria,
                            Referencia = empleadoFamiliarViewModel.Referencia,
                            Numero = empleadoFamiliarViewModel.Numero,
                            IdParroquia = empleadoFamiliarViewModel.IdParroquia,
                            Ocupacion = empleadoFamiliarViewModel.Ocupacion

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
        public async Task<Response> DeleteEmpleadoFamiliar([FromRoute] int id)
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

                var respuesta = await db.EmpleadoFamiliar.SingleOrDefaultAsync(m => m.IdEmpleadoFamiliar == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.EmpleadoFamiliar.Remove(respuesta);
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