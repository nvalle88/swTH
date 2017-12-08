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
using bd.swth.entidades.ObjectTransfer;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Empleados")]
    public class EmpleadosController : Controller
    {
        private readonly SwTHDbContext db;

        public EmpleadosController(SwTHDbContext db)
        {
            this.db = db;
        }


        // GET: api/Empleados
        [HttpGet]
        [Route("ListarEmpleados")]
        public async Task<List<ListaEmpleadoViewModel>> GetEmpleados()
        {
            try
            {
                var lista = await db.Empleado.Include(x => x.Persona).OrderBy(x => x.FechaIngreso).ToListAsync();
                var listaSalida = new List<ListaEmpleadoViewModel>();
                foreach (var item in lista)
                {
                    listaSalida.Add(new ListaEmpleadoViewModel
                    {
                        IdEmpleado = item.IdEmpleado,
                        IdPersona = item.IdPersona,
                        NombreApellido = string.Format("{0} {1}", item.Persona.Nombres, item.Persona.Apellidos),
                        Identificacion = item.Persona.Identificacion,
                        TelefonoPrivado = item.Persona.TelefonoPrivado,
                        CorreoPrivado = item.Persona.CorreoPrivado

                    });
                }
                return listaSalida;

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
                return new List<ListaEmpleadoViewModel>();
            }
        }

        // GET: api/Empleados/5
        [HttpGet("{id}")]
        public async Task<Response> GetEmpleado([FromRoute] int id)
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

                var Empleado = await db.Empleado.SingleOrDefaultAsync(m => m.IdEmpleado == id);

                if (Empleado == null)
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
                    Resultado = Empleado,
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


        // POST: api/Empleados
        [HttpPost]
        [Route("InsertarEmpleado")]
        public async Task<Response> PostEmpleado([FromBody] EmpleadoViewModel empleadoViewModel)
        {

            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                    try
                    {
                    
                        string fechaIndiceOcupacional = empleadoViewModel.IndiceOcupacionalModalidadPartida.Fecha.DayOfWeek.ToString();

                        if (fechaIndiceOcupacional.Equals("Saturday") || fechaIndiceOcupacional.Equals("Sunday"))
                        {
                            return new Response
                            {
                                IsSuccess = false,
                                Message = "La fecha de tipo de nombramiento no puede ser fin de semana"
                            };
                        }

                        if (empleadoViewModel.IndiceOcupacionalModalidadPartida.Fecha > DateTime.Today)
                        {
                            return new Response
                            {
                                IsSuccess = false,
                                Message = "La fecha de tipo de nombramiento no puede ser mayor que la fecha de hoy"
                            };
                        }

                        string fechaIngresoEmpleado = empleadoViewModel.Empleado.FechaIngreso.DayOfWeek.ToString();

                        if (fechaIngresoEmpleado.Equals("Saturday") || fechaIngresoEmpleado.Equals("Sunday"))
                        {
                            return new Response
                            {
                                IsSuccess = false,
                                Message = "La fecha de ingreso del empleado no puede ser fin de semana"
                            };
                        }

                        if (empleadoViewModel.Empleado.FechaIngreso > DateTime.Today)
                        {
                            return new Response
                            {
                                IsSuccess = false,
                                Message = "La fecha de ingreso del empleado no puede ser mayor que la fecha de hoy"
                            };
                        }

                        string fechaIngresoSectorPublico = empleadoViewModel.Empleado.FechaIngresoSectorPublico.DayOfWeek.ToString();

                        if (fechaIngresoSectorPublico.Equals("Saturday") || fechaIngresoSectorPublico.Equals("Sunday"))
                        {
                            return new Response
                            {
                                IsSuccess = false,
                                Message = "La fecha de ingreso al sector público del empleado  no puede ser fin de semana"
                            };
                        }

                        if (empleadoViewModel.Empleado.FechaIngresoSectorPublico > empleadoViewModel.Empleado.FechaIngreso)
                        {
                            return new Response
                            {
                                IsSuccess = false,
                                Message = "La fecha de ingreso del sector público del empleado no puede ser mayor que la fecha de ingreso"
                            };
                        }


                        if (empleadoViewModel.Empleado.FechaIngresoSectorPublico > DateTime.Today)
                        {
                            return new Response
                            {
                                IsSuccess = false,
                                Message = "La fecha de ingreso del sector público del empleado no puede ser mayor que la fecha de hoy"
                            };
                        }

                        if (empleadoViewModel.Empleado.DiasImposiciones<=0 || empleadoViewModel.Empleado.DiasImposiciones>31)
                        {
                            return new Response
                            {
                                IsSuccess = false,
                                Message = "Los días de imposición no pueden ser menor o igual a cero o mayor a treinta y un días"
                            };
                        }

                        if (empleadoViewModel.Empleado.MesesImposiciones <= 0 || empleadoViewModel.Empleado.MesesImposiciones > 12)
                        {
                            return new Response
                            {
                                IsSuccess = false,
                                Message = "Los meses de imposición no pueden ser menor o igual a cero o mayor a doce meses"
                            };
                        }

                        foreach (var trayectoria in empleadoViewModel.TrayectoriaLaboral)
                        {

                            if (trayectoria.FechaInicio > DateTime.Today)
                            {
                                return new Response
                                {
                                    IsSuccess = false,
                                    Message = "La fecha de inicio de la trayectoria laboral del empleado no puede ser mayor que la fecha de hoy"
                                };
                            }

                            if (trayectoria.FechaFin > DateTime.Today)
                            {
                                return new Response
                                {
                                    IsSuccess = false,
                                    Message = "La fecha fin de la trayectoria laboral del empleado no puede ser mayor que la fecha de hoy"
                                };
                            }


                            if (trayectoria.FechaInicio > trayectoria.FechaFin)
                            {
                                return new Response
                                {
                                    IsSuccess = false,
                                    Message = "La fecha de inicio de la trayectoria laboral no puede ser mayor que la fecha fin"
                                };
                            }

                            string fechaInicio = trayectoria.FechaInicio.DayOfWeek.ToString();

                            if (fechaInicio.Equals("Saturday") || fechaInicio.Equals("Sunday"))
                            {
                                return new Response
                                {
                                    IsSuccess = false,
                                    Message = "La fecha de inicio de la trayectoria laboral no puede ser fin de semana"
                                };
                            }


                            string fechaFin = trayectoria.FechaFin.DayOfWeek.ToString();

                            if (fechaFin.Equals("Saturday") || fechaFin.Equals("Sunday"))
                            {
                                return new Response
                                {
                                    IsSuccess = false,
                                    Message = "La fecha fin de la trayectoria laboral no puede ser fin de semana"
                                };
                            }
                        }

                    var respuesta = Existe(empleadoViewModel);
                    if (!respuesta.IsSuccess)
                    {
          
                        //1. Insertar Persona 
                        var persona = await db.Persona.AddAsync(empleadoViewModel.Persona);
                        await db.SaveChangesAsync();

                        //2. Insertar Empleado (Inicializado : IdPersona, IdDependencia)
                        empleadoViewModel.Empleado.IdPersona = persona.Entity.IdPersona;
                        empleadoViewModel.Empleado.IdDependencia = 1;
                        var empleado = await db.Empleado.AddAsync(empleadoViewModel.Empleado);
                        await db.SaveChangesAsync();

                        //3. Insertar Datos Bancarios (Inicializado : IdEmpleado)
                        empleadoViewModel.DatosBancarios.IdEmpleado = empleado.Entity.IdEmpleado;
                        await db.DatosBancarios.AddAsync(empleadoViewModel.DatosBancarios);
                        await db.SaveChangesAsync();

                        //4. Insertar Empleado Contacto Emergencia  (Inicializado : IdPersona, IdEmpleado)
                        empleadoViewModel.EmpleadoContactoEmergencia.IdPersona = persona.Entity.IdPersona;
                        empleadoViewModel.EmpleadoContactoEmergencia.IdEmpleado = empleado.Entity.IdEmpleado;
                        await db.EmpleadoContactoEmergencia.AddAsync(empleadoViewModel.EmpleadoContactoEmergencia);
                        await db.SaveChangesAsync();

                        //5. Insertar Indice Ocupacional Modalidad Partida  (Inicializado : IdIndiceOcupacional, IdEmpleado)
                        empleadoViewModel.IndiceOcupacionalModalidadPartida.IdIndiceOcupacional = 1;
                        empleadoViewModel.IndiceOcupacionalModalidadPartida.IdEmpleado = 1;
                        await db.IndiceOcupacionalModalidadPartida.AddAsync(empleadoViewModel.IndiceOcupacionalModalidadPartida);
                        await db.SaveChangesAsync();

                        //6. Insertar Persona Estudio (Inicializado : IdPersona)
                        foreach (var personaEstudio in empleadoViewModel.PersonaEstudio)
                        {
                            personaEstudio.IdPersona = persona.Entity.IdPersona;
                            await db.PersonaEstudio.AddAsync(personaEstudio);
                            await db.SaveChangesAsync();
                        }

                        // 7. Insertar Trayectoria Laboral (Inicializado : IdPersona)
                        foreach (var trayectoriaLaboral in empleadoViewModel.TrayectoriaLaboral)
                        {
                            trayectoriaLaboral.IdPersona = persona.Entity.IdPersona;
                            await db.TrayectoriaLaboral.AddAsync(trayectoriaLaboral);
                            await db.SaveChangesAsync();
                        }

                        // 8. Insertar Persona Discapacidad (Inicializado : IdPersona)
                        foreach (var personaDiscapacidad in empleadoViewModel.PersonaDiscapacidad)
                        {
                            personaDiscapacidad.IdPersona = persona.Entity.IdPersona;
                            await db.PersonaDiscapacidad.AddAsync(personaDiscapacidad);
                            await db.SaveChangesAsync();
                        }

                        // 9. Insertar Persona Enfermedad (Inicializado : IdPersona)
                        foreach (var personaEnfermedad in empleadoViewModel.PersonaEnfermedad)
                        {
                            personaEnfermedad.IdPersona = persona.Entity.IdPersona;
                            await db.PersonaEnfermedad.AddAsync(personaEnfermedad);
                            await db.SaveChangesAsync();
                        }

                        // 10. Insertar Persona Sustituto (Inicializado : IdPersona, IdPersonaDiscapacidad)
                        //10.1. Insertar Persona Discapacidad
                        var objetoPersonaSustitutoDiscapacidad = await db.Persona.AddAsync(empleadoViewModel.PersonaSustituto.PersonaDiscapacidad);
                        await db.SaveChangesAsync();

                        // 10.2. Insertar Persona Sustituto
                        empleadoViewModel.PersonaSustituto.IdPersona = persona.Entity.IdPersona;
                        empleadoViewModel.PersonaSustituto.IdPersonaDiscapacidad = objetoPersonaSustitutoDiscapacidad.Entity.IdPersona;
                        var personaSustituto = empleadoViewModel.PersonaSustituto;
                        await db.PersonaSustituto.AddAsync(empleadoViewModel.PersonaSustituto);
                        await db.SaveChangesAsync();


                        // 10.3. Insertar Discapacidad Sustituto (Inicializado : IdPersonaSustituto)
                        foreach (var discapacidadSustituto in empleadoViewModel.DiscapacidadSustituto)
                        {
                            discapacidadSustituto.IdPersonaSustituto = personaSustituto.IdPersonaSustituto;
                            await db.DiscapacidadSustituto.AddAsync(discapacidadSustituto);
                            await db.SaveChangesAsync();
                        }

                        //10.4. Insertar Enfermedad Sustituto(Inicializado : IdPersonaSustituto)
                        foreach (var enfermedadSustituto in empleadoViewModel.EnfermedadSustituto)
                        {
                            enfermedadSustituto.IdPersonaSustituto = enfermedadSustituto.IdPersonaSustituto;
                            await db.EnfermedadSustituto.AddAsync(enfermedadSustituto);
                            await db.SaveChangesAsync();
                        }

                        // 11.  Insertar Familiares Empleado
                        foreach (var empleadoFamiliar in empleadoViewModel.EmpleadoFamiliar)
                        {
                            //11.1. Insertar Persona 
                            var personafamiliar = await db.Persona.AddAsync(empleadoFamiliar.Persona);
                            await db.SaveChangesAsync();

                            //11.2.  Insertar Empleado Familiar (I.icializado : IdPersona, IdEmpleado)
                            empleadoFamiliar.IdEmpleado = empleado.Entity.IdEmpleado;
                            empleadoFamiliar.IdPersona = personafamiliar.Entity.IdPersona;
                            await db.EmpleadoFamiliar.AddAsync(empleadoFamiliar);
                            await db.SaveChangesAsync();

                        }
                    
                        transaction.Commit();

                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.Satisfactorio,
                            Resultado = empleadoViewModel
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

        //// PUT: api/Empleados/5
        //[HttpPut("{id}")]
        //public async Task<Response> PutEmpleado([FromRoute] int id, [FromBody] EmpleadoViewModel empleadoViewModel)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = Mensaje.ModeloInvalido
        //            };
        //        }

        //        //1. Tabla Empleado

        //        var empleado = db.Empleado.Find(empleadoViewModel.Empleado.IdEmpleado);

        //        empleado.IdPersona = empleadoViewModel.Empleado.IdPersona;
        //        empleado.IdCiudadLugarNacimiento = empleadoViewModel.Empleado.IdCiudadLugarNacimiento;
        //        empleado.IdProvinciaLugarSufragio = empleadoViewModel.Empleado.IdProvinciaLugarSufragio;
        //        empleado.IdDependencia = empleadoViewModel.Empleado.IdDependencia;
        //        empleado.FechaIngreso = empleadoViewModel.Empleado.FechaIngreso;
        //        empleado.FechaIngresoSectorPublico = empleadoViewModel.Empleado.FechaIngresoSectorPublico;
        //        empleado.TrabajoSuperintendenciaBanco = empleadoViewModel.Empleado.TrabajoSuperintendenciaBanco;
        //        empleado.DeclaracionJurada = empleadoViewModel.Empleado.DeclaracionJurada;
        //        empleado.IngresosOtraActividad = empleadoViewModel.Empleado.IngresosOtraActividad;
        //        empleado.MesesImposiciones = empleadoViewModel.Empleado.MesesImposiciones;
        //        empleado.DiasImposiciones = empleadoViewModel.Empleado.DiasImposiciones;
        //        empleado.FondosReservas = empleadoViewModel.Empleado.FondosReservas;

        //        db.Empleado.Update(empleado);

        //        //2. Tabla Persona
        //        var persona = db.Persona.Find(empleadoViewModel.Persona.IdPersona);

        //        persona.FechaNacimiento = empleadoViewModel.Persona.FechaNacimiento;
        //        persona.IdSexo = empleadoViewModel.Persona.IdSexo;
        //        persona.IdTipoIdentificacion = empleadoViewModel.Persona.IdTipoIdentificacion;
        //        persona.IdEstadoCivil = empleadoViewModel.Persona.IdEstadoCivil;
        //        persona.IdGenero = empleadoViewModel.Persona.IdGenero;
        //        persona.IdNacionalidad = empleadoViewModel.Persona.IdNacionalidad;
        //        persona.IdTipoSangre = empleadoViewModel.Persona.IdTipoSangre;
        //        persona.IdEtnia = empleadoViewModel.Persona.IdEtnia;
        //        persona.Identificacion = empleadoViewModel.Persona.Identificacion;
        //        persona.Nombres = empleadoViewModel.Persona.Nombres;
        //        persona.Apellidos = empleadoViewModel.Persona.Apellidos;
        //        persona.TelefonoPrivado = empleadoViewModel.Persona.TelefonoPrivado;
        //        persona.TelefonoCasa = empleadoViewModel.Persona.TelefonoCasa;
        //        persona.CorreoPrivado = empleadoViewModel.Persona.CorreoPrivado;
        //        persona.LugarTrabajo = empleadoViewModel.Persona.LugarTrabajo;
        //        persona.IdNacionalidadIndigena = empleadoViewModel.Persona.IdNacionalidadIndigena;
        //        persona.CallePrincipal = empleadoViewModel.Persona.CallePrincipal;
        //        persona.CalleSecundaria = empleadoViewModel.Persona.CalleSecundaria;
        //        persona.Referencia = empleadoViewModel.Persona.Referencia;
        //        persona.Numero = empleadoViewModel.Persona.Numero;
        //        persona.IdParroquia = empleadoViewModel.Persona.IdParroquia;
        //        persona.Ocupacion = empleadoViewModel.Persona.Ocupacion;

        //        db.Persona.Update(persona);

        //        //3. Tabla Datos Bancarios

        //        var datosbancarios = db.DatosBancarios.Find(empleadoViewModel.DatosBancarios.IdDatosBancarios);

        //        datosbancarios.IdEmpleado = empleadoViewModel.DatosBancarios.IdEmpleado;
        //        datosbancarios.IdInstitucionFinanciera = empleadoViewModel.DatosBancarios.IdInstitucionFinanciera;
        //        datosbancarios.NumeroCuenta = empleadoViewModel.DatosBancarios.NumeroCuenta;
        //        datosbancarios.Ahorros = empleadoViewModel.DatosBancarios.Ahorros;

        //        db.DatosBancarios.Update(datosbancarios);

        //        //4. Tabla Empleado Contacto Emergencia
        //        var empleadoContactoEmergencia = db.EmpleadoContactoEmergencia.Find(empleadoViewModel.EmpleadoContactoEmergencia.IdEmpleadoContactoEmergencia);

        //        empleadoContactoEmergencia.IdPersona = empleadoViewModel.EmpleadoContactoEmergencia.IdPersona;
        //        empleadoContactoEmergencia.IdEmpleado = empleadoViewModel.EmpleadoContactoEmergencia.IdEmpleado;
        //        empleadoContactoEmergencia.IdParentesco = empleadoViewModel.EmpleadoContactoEmergencia.IdParentesco;

        //        db.EmpleadoContactoEmergencia.Update(empleadoContactoEmergencia);

        //        //5. Tabla Indice Ocupacional Modalidad Partida
        //        var indiceOcupacionalModalidadPartida = db.IndiceOcupacionalModalidadPartida.Find(empleadoViewModel.IndiceOcupacionalModalidadPartida.IdIndiceOcupacionalModalidadPartida);

        //        indiceOcupacionalModalidadPartida.IdIndiceOcupacional = empleadoViewModel.IndiceOcupacionalModalidadPartida.IdIndiceOcupacional;
        //        indiceOcupacionalModalidadPartida.IdEmpleado = empleadoViewModel.IndiceOcupacionalModalidadPartida.IdEmpleado;
        //        indiceOcupacionalModalidadPartida.IdFondoFinanciamiento = empleadoViewModel.IndiceOcupacionalModalidadPartida.IdFondoFinanciamiento;
        //        indiceOcupacionalModalidadPartida.IdModalidadPartida = empleadoViewModel.IndiceOcupacionalModalidadPartida.IdModalidadPartida;
        //        indiceOcupacionalModalidadPartida.IdTipoNombramiento = empleadoViewModel.IndiceOcupacionalModalidadPartida.IdTipoNombramiento;
        //        indiceOcupacionalModalidadPartida.Fecha = empleadoViewModel.IndiceOcupacionalModalidadPartida.Fecha;
        //        indiceOcupacionalModalidadPartida.SalarioReal = empleadoViewModel.IndiceOcupacionalModalidadPartida.SalarioReal;

        //        db.IndiceOcupacionalModalidadPartida.Update(indiceOcupacionalModalidadPartida);

        //        await db.SaveChangesAsync();

        //        return new Response
        //        {
        //            IsSuccess = true,
        //            Message = Mensaje.Satisfactorio,
        //        };

        //    }
        //    catch (Exception ex)
        //    {
        //        await GuardarLogService.SaveLogEntry(new LogEntryTranfer
        //        {
        //            ApplicationName = Convert.ToString(Aplicacion.SwTH),
        //            ExceptionTrace = ex,
        //            Message = Mensaje.Excepcion,
        //            LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
        //            LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
        //            UserName = "",

        //        });

        //        return new Response
        //        {
        //            IsSuccess = true,
        //            Message = Mensaje.Excepcion,
        //        };
        //    }

        //}


        // PUT: api/BasesDatos/5
        [HttpPut("{id}")]
        public async Task<Response> PutEmpleado([FromRoute] int id, [FromBody] Empleado empleado)
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

               
                var Empleado = db.Empleado.Find(empleado.IdEmpleado);

                Empleado.IdPersona = 5;
                Empleado.IdDependencia = 1;
                Empleado.IdCiudadLugarNacimiento = 1;
                Empleado.IdProvinciaLugarSufragio = 1;


                Empleado.FechaIngreso = empleado.FechaIngreso;
                Empleado.FechaIngresoSectorPublico = empleado.FechaIngresoSectorPublico;
                Empleado.TrabajoSuperintendenciaBanco = empleado.TrabajoSuperintendenciaBanco;
                Empleado.DeclaracionJurada = empleado.DeclaracionJurada;
                Empleado.IngresosOtraActividad = empleado.IngresosOtraActividad;
                Empleado.MesesImposiciones = empleado.MesesImposiciones;
                Empleado.DiasImposiciones = empleado.DiasImposiciones;
                Empleado.FondosReservas = empleado.FondosReservas;

                db.Empleado.Update(Empleado);
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




        // DELETE: api/BasesDatos/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteEmpleado([FromRoute] int id)
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

                var respuesta = await db.Empleado.SingleOrDefaultAsync(m => m.IdEmpleado == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.Empleado.Remove(respuesta);
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
        
        [HttpPost]
        [Route("InsertarEmpleadoContactoEmergencia")]
        public async Task<Response> InsertarEmpleadoContactoEmergencia([FromBody] EmpleadoContactoEmergencia empleadoContactoEmergencia)
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
                db.EmpleadoContactoEmergencia.Add(empleadoContactoEmergencia);
                await db.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio
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

       
        [HttpPost]
        [Route("InsertarEmpleadoFamiliar")]
        public async Task<Response> InsertarEmpleadoFamiliar([FromBody] EmpleadoFamiliar empleadoFamiliar)
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
                db.EmpleadoFamiliar.Add(empleadoFamiliar);
                await db.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio
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

        
 
        [HttpPost]
        [Route("InsertarEmpleadoNepotismo")]
        public async Task<Response> InsertarEmpleadoNepotismo([FromBody] EmpleadoNepotismo empleadoNepotismo)
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
                db.EmpleadoNepotismo.Add(empleadoNepotismo);
                await db.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio
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

        

        [HttpPost]
        [Route("InsertarEmpleadoDatosBancarios")]
        public async Task<Response> InsertarEmpleadoDatosBancarios([FromBody] DatosBancarios datosBancarios)
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
                db.DatosBancarios.Add(datosBancarios);
                await db.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio
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

        private Response Existe(EmpleadoViewModel empleadoViewModel)
        {
            var identificacion = empleadoViewModel.Persona.Identificacion.ToUpper().TrimEnd().TrimStart();
            var nombres = empleadoViewModel.Persona.Nombres.ToUpper().TrimEnd().TrimStart();
            var apellidos = empleadoViewModel.Persona.Apellidos.ToUpper().TrimEnd().TrimStart();
            var Empleadorespuesta = db.Persona.Where(p => p.Nombres.ToUpper().TrimStart().TrimEnd() == nombres && p.Apellidos == apellidos && p.Identificacion == identificacion).FirstOrDefault();
            if (Empleadorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = Empleadorespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = Empleadorespuesta,
            };
        }




    }
}