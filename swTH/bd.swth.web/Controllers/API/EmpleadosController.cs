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
using bd.swth.entidades.ViewModels;

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

        //public class listaEmpleadoViewModel
        //{
        //    public int IdEmpleado { get; set; }
        //    public string NombreApellido { get; set;}
        //}

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarEmpleados")]
        public async Task<List<ListaEmpleadoViewModel>> GetEmpleados()
        {
            try
            {
                //return await db.Empleado.Include(x => x.Persona).Include(x => x.CiudadNacimiento).Include(x => x.ProvinciaSufragio).Include(x => x.Dependencia).OrderBy(x => x.FechaIngreso).ToListAsync();
                var lista= await db.Empleado.Include(x => x.Persona).OrderBy(x => x.FechaIngreso).ToListAsync();
                var listaSalida = new List<ListaEmpleadoViewModel>();
                foreach (var item in lista)
                {
                    listaSalida.Add(new ListaEmpleadoViewModel
                    {
                        IdEmpleado=item.IdEmpleado,
                        NombreApellido=string.Format("{0} {1}",item.Persona.Nombres,item.Persona.Apellidos),
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

        [HttpPost]
        [Route("ObtenerEmpleadoLogueado")]
        public async Task<Empleado> ObtenerEmpleadoLogueado([FromBody]Empleado empleado)
        {
            //Persona persona = new Persona();
            try
            {
                
                var Empleado = await db.Empleado
                                   .Where(e => e.NombreUsuario == empleado.NombreUsuario).FirstOrDefaultAsync();
                var empl = new Empleado { IdEmpleado = Empleado.IdEmpleado };


                return empl;
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
                return new Empleado();
            }
        }

        

        [HttpPost]
        [Route("ListarEmpleadosdeJefe")]
        public async Task<List<EmpleadoSolicitudVacacionesViewModel>> ListarEmpleadosdeJefe([FromBody]Empleado empleado)
        {
            try
            {
                var EmpleadoJefe = await db.Empleado
                                   .Where(e => e.NombreUsuario == empleado.NombreUsuario && e.EsJefe == true).FirstOrDefaultAsync();

                if (EmpleadoJefe != null)
                {

                    var listaSubordinados = await db.Empleado.Where(x => x.IdDependencia == EmpleadoJefe.IdDependencia && x.EsJefe == false).Include(x => x.Persona).Include(x => x.SolicitudPlanificacionVacaciones).ToListAsync();

                    var listaEmpleado = new List<EmpleadoSolicitudVacacionesViewModel>();
                    foreach (var item in listaSubordinados)
                    {
                        var haSolicitado = false;
                        var aprobado = true;

                        if (item.SolicitudPlanificacionVacaciones.Count==0)
                        {
                            haSolicitado = false;
                            aprobado = false;
                        }
                        else
                        {
                            foreach (var item1 in item.SolicitudPlanificacionVacaciones)
                            {
                                if (item1.Estado == 0)
                                {
                                    haSolicitado = true;
                                    aprobado = false;
                                    break;
                                }
                            }
                        }




                        var empleadoSolicitud = new EmpleadoSolicitudVacacionesViewModel
                        {
                            NombreApellido = item.Persona.Nombres +" " + item.Persona.Apellidos,
                            Identificacion = item.Persona.Identificacion,
                            Aprobado = aprobado,
                            IdEmpleado = item.IdEmpleado,
                            HaSolicitadoVacaciones = haSolicitado,
                        };

                        listaEmpleado.Add(empleadoSolicitud);
                    }

                    return listaEmpleado;
                }

                return new List<EmpleadoSolicitudVacacionesViewModel>();
                //var ListadoEmpleados = await (from p in db.Persona
                //                              join e in db.Empleado
                //                             on p.IdPersona equals e.IdPersona
                //                              join d in db.Dependencia
                //                              on e.IdDependencia equals d.IdDependencia
                //                              where e.IdDependencia == EmpleadoJefe.IdDependencia && e.EsJefe == false 
                //                             //group s by s.AdstDescripcion into pg
                //                             select new Persona
                //                             {
                //                                 Nombres = p.Nombres,
                //                                 Apellidos = p.Apellidos,
                //                                 Identificacion = p.Identificacion,
                //                                 Empleado=p.Empleado,
                //                             }).ToListAsync();

                //    var listaSalida = new List<Persona>();
                //    foreach (var item3 in ListadoEmpleados)
                //    {
                //        listaSalida.Add(new Persona
                //        {
                //            Nombres = item3.Nombres,
                //            Apellidos = item3.Apellidos,
                //            Identificacion = item3.Identificacion
                //        });
                //    }

                    // var lista = new List<EmpleadoSolicitudVacacionesViewModel>();


                    //foreach (var item in listaSalida)
                    //{
                    //    var a = new EmpleadoSolicitudVacacionesViewModel { Apellidos = item.Apellidos, Nombres = item.Nombres, Identificacion = item.Identificacion, Empleado = item.Empleado,Aprobado=true };
                    //    //var solicitudes =await db.SolicitudPlanificacionVacaciones.Where(x => x.IdEmpleado == item.Empleado.FirstOrDefault().IdEmpleado).ToListAsync();
                    //    var solicitudes = new List<SolicitudPlanificacionVacaciones>();
                    //    solicitudes.Add( (from s in db.SolicitudPlanificacionVacaciones
                    //                           join e in db.Empleado
                    //                          on s.IdEmpleado equals e.IdEmpleado
                    //                           join p in db.Persona
                    //                          on e.IdPersona equals p.IdPersona
                    //                           where p.IdPersona == item.IdPersona
                    //                           //group s by s.AdstDescripcion into pg
                    //                           select new SolicitudPlanificacionVacaciones
                    //                           {
                    //                               IdSolicitudPlanificacionVacaciones = s.IdSolicitudPlanificacionVacaciones,
                    //                               IdEmpleado = s.IdEmpleado,
                    //                               FechaDesde = s.FechaDesde,
                    //                               FechaHasta = s.FechaDesde,
                    //                               Aprobado = s.Aprobado,
                    //                               Observaciones = s.Observaciones
                    //                           }));

                    //    foreach (var item1 in solicitudes)
                    //    {
                    //        if (item1.Aprobado==false)
                    //        {
                    //            a.Aprobado = false; 

                    //        }
                    //    }

                        
                    //    lista.Add(a);

                    //}

                //var listaSalida = new List<Persona>();
                //foreach (var item in ListadoEmpleados)
                //{
                //    listaSalida.Add(new Persona
                //    {
                //        Nombres = item.Nombres,
                //        Apellidos = item.Apellidos,
                //        Identificacion = item.Identificacion
                //    });
                // var ListadoSolicitudesVacacionesEmpleados = new List<SolicitudPlanificacionVacaciones>();
                // ListadoSolicitudesVacacionesEmpleados.Add( await (from s in db.SolicitudPlanificacionVacaciones
                //                                                           join e in db.Empleado
                //                                                          on s.IdEmpleado equals e.IdEmpleado
                //                                                           join p in db.Persona
                //                                                          on e.IdPersona equals p.IdPersona
                //                                                           where p.IdPersona == item.IdPersona
                //                                                           //group s by s.AdstDescripcion into pg
                //                                                           select new <List<SolicitudPlanificacionVacaciones>>
                //                                                           {
                //                                                               IdSolicitudPlanificacionVacaciones = s.IdSolicitudPlanificacionVacaciones,
                //                                                               IdEmpleado = s.IdEmpleado,
                //                                                               FechaDesde = s.FechaDesde,
                //                                                               FechaHasta = s.FechaDesde,
                //                                                               Aprobado = s.Aprobado,
                //                                                               Observaciones = s.Observaciones
                //                                                           }).ToListAsync();
                //    }   

                


                //    return listaSalida;
                //}

                //return null;
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
                return new List<EmpleadoSolicitudVacacionesViewModel>();
            }
        }

        [HttpPost]
        [Route("ListarEmpleadosdeJefeconSolucitudesVacaciones")]
        public async Task<List<EmpleadoSolicitudVacacionesViewModel>> ListarEmpleadosdeJefeconSolucitudesVacaciones([FromBody]Empleado empleado)
        {
            try
            {
                var EmpleadoJefe = await db.Empleado
                                   .Where(e => e.NombreUsuario == empleado.NombreUsuario && e.EsJefe == true).FirstOrDefaultAsync();

                if (EmpleadoJefe != null)
                {

                    var listaSubordinados = await db.Empleado.Where(x => x.IdDependencia == EmpleadoJefe.IdDependencia && x.EsJefe == false).Include(x => x.Persona).Include(x => x.SolicitudVacaciones).ToListAsync();

                    var listaEmpleado = new List<EmpleadoSolicitudVacacionesViewModel>();
                    foreach (var item in listaSubordinados)
                    {
                        var haSolicitado = false;
                        var aprobado = true;

                        if (item.SolicitudVacaciones.Count == 0)
                        {
                            haSolicitado = false;
                            aprobado = false;
                        }
                        else
                        {
                            foreach (var item1 in item.SolicitudVacaciones)
                            {
                             
                                if (item1.Estado == 0)
                                {
                                    haSolicitado = true;
                                    aprobado = false;
                                    break;
                                }
                            }
                        }




                        var empleadoSolicitud = new EmpleadoSolicitudVacacionesViewModel
                        {
                            NombreApellido = item.Persona.Nombres + " " + item.Persona.Apellidos,
                            Identificacion = item.Persona.Identificacion,
                            Aprobado = aprobado,
                            IdEmpleado = item.IdEmpleado,
                            HaSolicitadoVacaciones = haSolicitado,
                        };

                        listaEmpleado.Add(empleadoSolicitud);
                    }

                    return listaEmpleado;
                }

                return new List<EmpleadoSolicitudVacacionesViewModel>();
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
                return new List<EmpleadoSolicitudVacacionesViewModel>();
            }
        }

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

        //        var existe = Existe(empleadoViewModel);
        //        if (existe.IsSuccess)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = Mensaje.ExisteRegistro,
        //            };
        //        }

        //        var empleadoActualizar = await db.Empleado.Where(x => x.IdEmpleado == id).FirstOrDefaultAsync();
        //        var personaActualizar = await db.Persona.Where(x => x.IdPersona == id).FirstOrDefaultAsync();

        //        if (empleadoActualizar != null)
        //        {
        //            try
        //            {
        //                //empleadoActualizar.Nombre = empleadoViewModel.Nombre;
        //                await db.SaveChangesAsync();

        //                return new Response
        //                {
        //                    IsSuccess = true,
        //                    Message = Mensaje.Satisfactorio,
        //                };

        //            }
        //            catch (Exception ex)
        //            {
        //                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
        //                {
        //                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
        //                    ExceptionTrace = ex,
        //                    Message = Mensaje.Excepcion,
        //                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
        //                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
        //                    UserName = "",

        //                });
        //                return new Response
        //                {
        //                    IsSuccess = false,
        //                    Message = Mensaje.Error,
        //                };
        //            }
        //        }




        //        return new Response
        //        {
        //            IsSuccess = false,
        //            Message = Mensaje.ExisteRegistro
        //        };
        //    }
        //    catch (Exception)
        //    {
        //        return new Response
        //        {
        //            IsSuccess = false,
        //            Message = Mensaje.Excepcion
        //        };
        //    }
        //}


        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarEmpleado")]
        public async Task<Response> PostEmpleado([FromBody] EmpleadoViewModel empleadoViewModel)
        {

            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
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
                        IsSuccess=true,
                        Message=Mensaje.Satisfactorio,
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

        //private Response Existe(EmpleadoViewModel empleadoViewModel)
        //{
        //    var bdd = Empleado.IdPersona;
        //    var Empleadorespuesta = db.Empleado.Where(p => p.IdPersona == bdd).FirstOrDefault();
        //    if (Empleadorespuesta != null)
        //    {
        //        return new Response
        //        {
        //            IsSuccess = true,
        //            Message = Mensaje.ExisteRegistro,
        //            Resultado = Empleadorespuesta,
        //        };

        //    }

        //    return new Response
        //    {
        //        IsSuccess = false,
        //        Resultado = Empleadorespuesta,
        //    };
        //}

        //Empleado-Contacto-Emergencia

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
        [Route("EliminarEmpleadoContactoEmergencia")]
        public async Task<Response> EliminarEmpleadoContactoEmergencia([FromBody] EmpleadoContactoEmergencia empleadoContactoEmergencia)
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

                var respuesta = await db.EmpleadoContactoEmergencia.SingleOrDefaultAsync(m => m.IdEmpleadoContactoEmergencia == empleadoContactoEmergencia.IdEmpleadoContactoEmergencia && m.IdEmpleado == empleadoContactoEmergencia.IdEmpleado);

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

        //Èmpleado-Familiar

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
        [Route("EliminarEmpleadoFamiliar")]
        public async Task<Response> EliminarEmpleadoFamiliar([FromBody] EmpleadoFamiliar empleadoFamiliar)
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

                var respuesta = await db.EmpleadoFamiliar.SingleOrDefaultAsync(m => m.IdEmpleadoFamiliar == empleadoFamiliar.IdEmpleadoFamiliar && m.IdEmpleado == empleadoFamiliar.IdEmpleado);

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

        //Èmpleado-Nepotismo

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
        [Route("EliminarEmpleadoNepotismo")]
        public async Task<Response> EliminarEmpleadoNepotismo([FromBody] EmpleadoNepotismo empleadoNepotismo)
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

                var respuesta = await db.EmpleadoNepotismo.SingleOrDefaultAsync(m => m.IdEmpleadoNepotismo == empleadoNepotismo.IdEmpleadoNepotismo && m.IdEmpleado == empleadoNepotismo.IdEmpleado);

                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.EmpleadoNepotismo.Remove(respuesta);
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


        //Èmpleado-Datos-Bancarios

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

        [HttpPost]
        [Route("EliminarEmpleadoDatosBancarios")]
        public async Task<Response> EliminarEmpleadoDatosBancarios([FromBody] DatosBancarios datosBancarios)
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

                var respuesta = await db.DatosBancarios.SingleOrDefaultAsync(m => m.IdDatosBancarios == datosBancarios.IdDatosBancarios && m.IdEmpleado == datosBancarios.IdEmpleado);

                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.DatosBancarios.Remove(respuesta);
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

    }
}