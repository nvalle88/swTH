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


        // GET: api/Empleados
        [HttpGet]
        [Route("ListarEmpleados")]
        public async Task<List<ListaEmpleadoViewModel>> GetEmpleados()
        {
            try
            {
                //var lista = await db.Empleado.Include(x => x.Persona).Include(x => x.Dependencia).Include(x=>x.DatosBancarios).Include(x => x.IndiceOcupacionalModalidadPartida).ThenInclude(x => x.IndiceOcupacional).ThenInclude(x => x.RolPuesto).OrderBy(x => x.FechaIngreso).ToListAsync();
                var lista = await db.Empleado.Include(x => x.Persona).Include(x => x.Dependencia).OrderBy(x => x.FechaIngreso).ToListAsync();
                var listaSalida = new List<ListaEmpleadoViewModel>();
                foreach (var item in lista)
                {
                   
                        listaSalida.Add(new ListaEmpleadoViewModel
                        {
                            IdEmpleado = item.IdEmpleado,
                            NombreApellido = string.Format("{0} {1}", item.Persona.Nombres, item.Persona.Apellidos),
                            Identificacion = item.Persona.Identificacion,
                            TelefonoPrivado = item.Persona.TelefonoPrivado,
                            CorreoPrivado = item.Persona.CorreoPrivado,
                            Dependencia = item.Dependencia.Nombre,


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

        [HttpGet]
        [Route("ListarEmpleadoconAccionPersonalPendiente")]
        public async Task<List<ListaEmpleadoViewModel>> ListarEmpleadoconAccionPersonalPendiente()
        {
            try
            {
                //var lista = await db.Empleado.Include(x => x.Persona).Include(x => x.Dependencia).Include(x => x.IndiceOcupacionalModalidadPartida).ThenInclude(x => x.IndiceOcupacional).ThenInclude(x => x.RolPuesto).OrderBy(x => x.FechaIngreso).ToListAsync();
                var lista = await db.Empleado.Include(x => x.Persona).Include(x => x.Dependencia).Include(x => x.AccionPersonal).OrderBy(x => x.FechaIngreso).ToListAsync();
                var listaSalida = new List<ListaEmpleadoViewModel>();
                foreach (var item in lista)
                {
                    foreach (var item2 in item.AccionPersonal)
                    {
                        if(item2.Estado == 0)
                        {
                            listaSalida.Add(new ListaEmpleadoViewModel
                            {
                                Dependencia = item.Dependencia.Nombre,
                                IdEmpleado = item.IdEmpleado,
                                NombreApellido = string.Format("{0} {1}", item.Persona.Nombres, item.Persona.Apellidos),
                                Identificacion = item.Persona.Identificacion,
                                TelefonoPrivado = item.Persona.TelefonoPrivado,
                                CorreoPrivado = item.Persona.CorreoPrivado

                            });
                        }
                    }
              

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

                var Empleado = await db.Empleado
                                                .Include(x => x.Persona)
                                                .Include(x => x.Dependencia)
                                                .SingleOrDefaultAsync(m => m.IdEmpleado == id);

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
        [Route("EmpleadosAsuCargoSolicitudPermiso")]
        public async Task <List<ListaEmpleadoViewModel>> ListaEmpleadosAsuCargo([FromBody] Empleado empleado)
        {
            try
            {

                var listaEmpleado = await db.Empleado
                                  .Include(x => x.Persona)
                                  .Include(x => x.SolicitudPermiso)
                                  .Where(x => x.IdDependencia == empleado.IdDependencia && x.EsJefe != empleado.EsJefe)
                                  .OrderBy(x => x.Persona.Apellidos)
                                  .ToListAsync();

                var listaSolicitudPermiso = await db.SolicitudPermiso
                                  .OrderBy(x => x.IdEmpleado)
                                  .ToListAsync();

                var listaSalida = new List<ListaEmpleadoViewModel>();


                foreach (var item in listaEmpleado)
                {

                    bool existeConsulta = listaSolicitudPermiso.Exists(x => ( x.IdEmpleado == item.IdEmpleado) && (x.Estado==0 || x.Estado==-1));
                    bool existeListaSalida = listaSalida.Exists(x => x.IdEmpleado == item.IdEmpleado);

                    if (existeConsulta)
                    {
                        if (!existeListaSalida)
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
                    }

                }

                
                return listaSalida;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("ObtenerDatosEmpleadoActualizar")]
        public async Task<EmpleadoViewModel> ObtenerEmpleadoActualizar([FromBody] int idEmpleado)
        {
            try
            {

                IndiceOcupacionalModalidadPartida indice = await db.IndiceOcupacionalModalidadPartida
                                .Where(x=>x.IdEmpleado==idEmpleado)
                                .Include(x=>x.TipoNombramiento).ThenInclude(x=>x.RelacionLaboral)
                                .Include(x=>x.ModalidadPartida).ThenInclude(x=>x.RelacionLaboral)
                                .SingleOrDefaultAsync();
                
                Empleado oEmpleado = await db.Empleado
                                  .Where(x => x.IdEmpleado == idEmpleado)
                                  .Include(x=>x.ProvinciaSufragio)
                                  .Include(x=>x.CiudadNacimiento)
                                  .SingleOrDefaultAsync();

                Persona persona = await db.Persona
                                         .Where(m => m.IdPersona == oEmpleado.IdPersona)
                                         .Include(x => x.Parroquia)
                                         .ThenInclude(x => x.Ciudad)
                                         .ThenInclude(x => x.Provincia)
                                         .ThenInclude(x => x.Pais)
                                         .FirstOrDefaultAsync();
                
                DatosBancarios datosBancarios = await db.DatosBancarios
                                  .Where(x => x.IdEmpleado == idEmpleado)
                                  .SingleOrDefaultAsync();

                EmpleadoContactoEmergencia contactoEmergencia = await db.EmpleadoContactoEmergencia
                                  .Include(x => x.Persona)
                                 .Where(x => x.IdEmpleado == idEmpleado)
                                 .SingleOrDefaultAsync();


                EmpleadoViewModel item = new EmpleadoViewModel
                {
                    IndiceOcupacionalModalidadPartida = indice,
                    Persona = persona,
                    Empleado = oEmpleado,
                    DatosBancarios = datosBancarios,
                    EmpleadoContactoEmergencia = contactoEmergencia,
               
                };

                return item;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("ObtenerTrayectoriaLaboralEmpleado")]
        public async Task<EmpleadoViewModel> ObtenerTrayectoriaLaboralEmpleado([FromBody] int idEmpleado)
        {
            try
            {

                
                Empleado oEmpleado = await db.Empleado
                                  .Where(x => x.IdEmpleado == idEmpleado)
                                  .SingleOrDefaultAsync();

                Persona persona = await db.Persona
                                         .Where(m => m.IdPersona == oEmpleado.IdPersona)
                                         .Include(x => x.Parroquia)
                                         .ThenInclude(x => x.Ciudad)
                                         .ThenInclude(x => x.Provincia)
                                         .ThenInclude(x => x.Pais)
                                         .FirstOrDefaultAsync();

                List<TrayectoriaLaboral> trayectoriaLaboral = await db.TrayectoriaLaboral
                .Where(x => x.IdPersona == oEmpleado.IdPersona)
                .ToListAsync();


                EmpleadoViewModel item = new EmpleadoViewModel
                {
                    Persona = persona,
                    Empleado = oEmpleado,
                    TrayectoriaLaboral= trayectoriaLaboral
                };

                return item;

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        [Route("ObtenerPersonaEstudioEmpleado")]
        public async Task<List<PersonaEstudioViewModel>> ObtenerPersonaEstudioEmpleado([FromBody] int idEmpleado)
        {
            try
            {


                Empleado oEmpleado = await db.Empleado
                                  .Where(x => x.IdEmpleado == idEmpleado)
                                  .SingleOrDefaultAsync();

                Persona persona = await db.Persona
                                         .Where(m => m.IdPersona == oEmpleado.IdPersona)
                                         .Include(x => x.Parroquia)
                                         .ThenInclude(x => x.Ciudad)
                                         .ThenInclude(x => x.Provincia)
                                         .ThenInclude(x => x.Pais)
                                         .FirstOrDefaultAsync();

                List<PersonaEstudio> personaEstudio = await db.PersonaEstudio
                                .Where(x => x.IdPersona == oEmpleado.IdPersona)
                                .Include(x => x.Titulo).ThenInclude(x => x.Estudio)
                                .Include(x => x.Titulo).ThenInclude(x => x.AreaConocimiento)
                                .ToListAsync();

                List<PersonaEstudioViewModel> listaPersonaEstudio = new List<PersonaEstudioViewModel>();


                foreach (PersonaEstudio item in personaEstudio)
                {

                    PersonaEstudioViewModel objetoPersonaEstudio = new PersonaEstudioViewModel
                    {
                        estudio = item.Titulo.Estudio.Nombre,
                        titulo = item.Titulo.Nombre,
                        areaConocimiento = item.Titulo.AreaConocimiento.Descripcion,
                        fechaGraduado = String.Format(item.FechaGraduado.ToString(), "dd/mm/aaaa")
                    };

                    listaPersonaEstudio.Add(objetoPersonaEstudio);
                }

                return listaPersonaEstudio;

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        [Route("ObtenerEmpleadoFamiliarEmpleado")]
        public async Task<EmpleadoViewModel> ObtenerEmpleadoFamiliarEmpleado([FromBody] int idEmpleado)
        {
            try
            {


                Empleado oEmpleado = await db.Empleado
                                  .Where(x => x.IdEmpleado == idEmpleado)
                                  .SingleOrDefaultAsync();

                Persona persona = await db.Persona
                                         .Where(m => m.IdPersona == oEmpleado.IdPersona)
                                         .Include(x => x.Parroquia)
                                         .ThenInclude(x => x.Ciudad)
                                         .ThenInclude(x => x.Provincia)
                                         .ThenInclude(x => x.Pais)
                                         .FirstOrDefaultAsync();

                List<EmpleadoFamiliar> empleadoFamiliar = await db.EmpleadoFamiliar
                                 .Where(x => x.IdEmpleado == oEmpleado.IdEmpleado)
                                 .ToListAsync();


                EmpleadoViewModel item = new EmpleadoViewModel
                {
                    Persona = persona,
                    Empleado = oEmpleado,
                    EmpleadoFamiliar = empleadoFamiliar
                };

                return item;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("ObtenerPersonaDiscapacidadEmpleado")]
        public async Task<EmpleadoViewModel> ObtenerPersonaDiscapacidadEmpleado([FromBody] int idEmpleado)
        {
            try
            {

                Empleado oEmpleado = await db.Empleado
                                  .Where(x => x.IdEmpleado == idEmpleado)
                                  .SingleOrDefaultAsync();

                Persona persona = await db.Persona
                                         .Where(m => m.IdPersona == oEmpleado.IdPersona)
                                         .Include(x => x.Parroquia)
                                         .ThenInclude(x => x.Ciudad)
                                         .ThenInclude(x => x.Provincia)
                                         .ThenInclude(x => x.Pais)
                                         .FirstOrDefaultAsync();

                List<PersonaDiscapacidad> personaDiscapacidad = await db.PersonaDiscapacidad
                                  .Where(x => x.IdPersona == oEmpleado.IdPersona)
                                  .ToListAsync();


                EmpleadoViewModel item = new EmpleadoViewModel
                {
                    Persona = persona,
                    Empleado = oEmpleado,
                    PersonaDiscapacidad = personaDiscapacidad
                };

                return item;

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        [Route("ObtenerPersonaEnfermedadEmpleado")]
        public async Task<EmpleadoViewModel> ObtenerPersonaEnfermedadEmpleado([FromBody] int idEmpleado)
        {
            try
            {

                Empleado oEmpleado = await db.Empleado
                                  .Where(x => x.IdEmpleado == idEmpleado)
                                  .SingleOrDefaultAsync();

                Persona persona = await db.Persona
                                         .Where(m => m.IdPersona == oEmpleado.IdPersona)
                                         .Include(x => x.Parroquia)
                                         .ThenInclude(x => x.Ciudad)
                                         .ThenInclude(x => x.Provincia)
                                         .ThenInclude(x => x.Pais)
                                         .FirstOrDefaultAsync();

                List<PersonaEnfermedad> personaEnfermedad = await db.PersonaEnfermedad
                  .Where(x => x.IdPersona == oEmpleado.IdPersona)
                  .ToListAsync();

                EmpleadoViewModel item = new EmpleadoViewModel
                {
                    Persona = persona,
                    Empleado = oEmpleado,
                    PersonaEnfermedad = personaEnfermedad
                };

                return item;

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        [Route("ObtenerDiscapacidadSustitutoEmpleado")]
        public async Task<EmpleadoViewModel> ObtenerDiscapacidadSustitutoEmpleado([FromBody] int idEmpleado)
        {
            try
            {

                Empleado oEmpleado = await db.Empleado
                                  .Where(x => x.IdEmpleado == idEmpleado)
                                  .SingleOrDefaultAsync();

                Persona persona = await db.Persona
                                         .Where(m => m.IdPersona == oEmpleado.IdPersona)
                                         .Include(x => x.Parroquia)
                                         .ThenInclude(x => x.Ciudad)
                                         .ThenInclude(x => x.Provincia)
                                         .ThenInclude(x => x.Pais)
                                         .FirstOrDefaultAsync();

                PersonaSustituto personaSustituto = await db.PersonaSustituto
                                 .Where(x => x.IdPersona == oEmpleado.IdPersona)
                                 .SingleOrDefaultAsync();

                List<DiscapacidadSustituto> discapacidadSustituto = await db.DiscapacidadSustituto
                                  .Where(x => x.IdPersonaSustituto == personaSustituto.IdPersonaSustituto)
                                  .ToListAsync();

                EmpleadoViewModel item = new EmpleadoViewModel
                {
                    Persona = persona,
                    Empleado = oEmpleado,
                    DiscapacidadSustituto = discapacidadSustituto
                };

                return item;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("ObtenerEnfermedadSustitutoEmpleado")]
        public async Task<EmpleadoViewModel> ObtenerEnfermedadSustitutoEmpleado([FromBody] int idEmpleado)
        {
            try
            {

                Empleado oEmpleado = await db.Empleado
                                  .Where(x => x.IdEmpleado == idEmpleado)
                                  .SingleOrDefaultAsync();

                Persona persona = await db.Persona
                                         .Where(m => m.IdPersona == oEmpleado.IdPersona)
                                         .Include(x => x.Parroquia)
                                         .ThenInclude(x => x.Ciudad)
                                         .ThenInclude(x => x.Provincia)
                                         .ThenInclude(x => x.Pais)
                                         .FirstOrDefaultAsync();

                PersonaSustituto personaSustituto = await db.PersonaSustituto
                                 .Where(x => x.IdPersona == oEmpleado.IdPersona)
                                 .SingleOrDefaultAsync();

                List<EnfermedadSustituto> enfermedadSustituto = await db.EnfermedadSustituto
                  .Where(x => x.IdPersonaSustituto == personaSustituto.IdPersonaSustituto)
                  .ToListAsync();


                EmpleadoViewModel item = new EmpleadoViewModel
                {
                    Persona = persona,
                    Empleado = oEmpleado,
                    EnfermedadSustituto = enfermedadSustituto
                };

                return item;

            }
            catch (Exception ex)
            {
                throw;
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
        [Route("ObtenerDatosCompletosEmpleado")]
        public async Task<ListaEmpleadoViewModel> ObtenerDatosCompletosEmpleado([FromBody]Empleado empleado)
        {
            try
            {

                var empleadoObtenido = await db.Empleado.Where(x => x.NombreUsuario == empleado.NombreUsuario)
                    .Include(x => x.Persona).Include(x => x.Dependencia).Include(x=>x.IndiceOcupacionalModalidadPartida).ThenInclude(x=>x.FondoFinanciamiento)
                    .Include(x => x.IndiceOcupacionalModalidadPartida).ThenInclude(x => x.IndiceOcupacional).ThenInclude(x => x.RolPuesto).ThenInclude(x=>x.ConfiguracionViatico)
                    .Include(x => x.DatosBancarios).ThenInclude(x=>x.InstitucionFinanciera)
                    .FirstOrDefaultAsync();

                var empleadoEnviar = new ListaEmpleadoViewModel();
                
                    
                        var empleados = new ListaEmpleadoViewModel
                        {
                            IdEmpleado = empleadoObtenido.IdEmpleado,
                            NombreApellido = string.Format("{0} {1}", empleadoObtenido.Persona.Nombres, empleadoObtenido.Persona.Apellidos),
                            Identificacion = empleadoObtenido.Persona.Identificacion,
                            TelefonoPrivado = empleadoObtenido.Persona.TelefonoPrivado,
                            CorreoPrivado = empleadoObtenido.Persona.CorreoPrivado,
                            Dependencia = empleadoObtenido.Dependencia.Nombre,
                            InstitucionBancaria = empleadoObtenido.DatosBancarios.FirstOrDefault().InstitucionFinanciera.Nombre,
                            NoCuenta = empleadoObtenido.DatosBancarios.FirstOrDefault().NumeroCuenta,
                            TipoCuenta = empleadoObtenido.DatosBancarios.FirstOrDefault().Ahorros,
                            RolPuesto = empleadoObtenido.IndiceOcupacionalModalidadPartida.FirstOrDefault().IndiceOcupacional.RolPuesto.Nombre,
                            FondoFinanciamiento = empleadoObtenido.IndiceOcupacionalModalidadPartida.FirstOrDefault().FondoFinanciamiento.Nombre,
                            IdConfiguracionViatico = empleadoObtenido.IndiceOcupacionalModalidadPartida.FirstOrDefault().IndiceOcupacional.RolPuesto.ConfiguracionViatico.FirstOrDefault().IdConfiguracionViatico
                            
                                                                                                 
                        };
                    empleadoEnviar = empleados;
                        //listaEmpleado.Add(empleados);



               

                return empleadoEnviar;



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
                return new ListaEmpleadoViewModel();
            }
        }


        [HttpPost]
        [Route("ListarEmpleadosdeDependenciaSinCesacion")]
        public async Task<List<EmpleadoSolicitudViewModel>> ListarEmpleadosdeDependencia([FromBody]Empleado empleado)
        {
            try
            {
          
                    var listaEmpleados = await db.Empleado.Where(x => x.IdDependencia == empleado.IdDependencia && x.Activo==true).Include(x => x.Persona).ToListAsync();

                    var listaEmpleado = new List<EmpleadoSolicitudViewModel>();
                    foreach (var item in listaEmpleados)
                    {
                  
                        var empleados = new EmpleadoSolicitudViewModel
                        {
                            NombreApellido = item.Persona.Nombres + " " + item.Persona.Apellidos,
                            Identificacion = item.Persona.Identificacion,
                            IdEmpleado = item.IdEmpleado,                     
                        };

                        listaEmpleado.Add(empleados);
                    }

                    return listaEmpleado;
                

              
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
                return new List<EmpleadoSolicitudViewModel>();
            }
        }


        [HttpPost]
        [Route("ObtenerDatosEmpleadoSeleccionado")]
        public async Task<EmpleadoSolicitudViewModel> ObtenerDatosEmpleadoSeleccionado([FromBody]Empleado empleado)
        {
            try
            {

                var Empleado = await db.Empleado.Include(x=>x.Persona).SingleOrDefaultAsync(m => m.IdEmpleado == empleado.IdEmpleado);


                    var empleadoObtenido = new EmpleadoSolicitudViewModel
                    {
                        NombreApellido = Empleado.Persona.Nombres + " " + Empleado.Persona.Apellidos,
                        Identificacion = Empleado.Persona.Identificacion,
                        IdEmpleado = Empleado.IdEmpleado,
                    };

                

                return empleadoObtenido;



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
                return new EmpleadoSolicitudViewModel();
            }
        }




        [HttpPost]
        [Route("ListarEmpleadosdeJefe")]
        public async Task<List<EmpleadoSolicitudViewModel>> ListarEmpleadosdeJefe([FromBody]Empleado empleado)
        {
            try
            {
                var EmpleadoJefe = await db.Empleado
                                   .Where(e => e.NombreUsuario == empleado.NombreUsuario && e.EsJefe == true).FirstOrDefaultAsync();

                if (EmpleadoJefe != null)
                {

                    var listaSubordinados = await db.Empleado.Where(x => x.IdDependencia == EmpleadoJefe.IdDependencia && x.EsJefe == false).Include(x => x.Persona).Include(x => x.SolicitudPlanificacionVacaciones).ToListAsync();

                    var listaEmpleado = new List<EmpleadoSolicitudViewModel>();
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




                        var empleadoSolicitud = new EmpleadoSolicitudViewModel
                        {
                            NombreApellido = item.Persona.Nombres +" " + item.Persona.Apellidos,
                            Identificacion = item.Persona.Identificacion,
                            Aprobado = aprobado,
                            IdEmpleado = item.IdEmpleado,
                            HaSolicitado = haSolicitado,
                        };

                        listaEmpleado.Add(empleadoSolicitud);
                    }

                    return listaEmpleado;
                }

                return new List<EmpleadoSolicitudViewModel>();
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

                    // var lista = new List<EmpleadoSolicitudViewModel>();


                    //foreach (var item in listaSalida)
                    //{
                    //    var a = new EmpleadoSolicitudViewModel { Apellidos = item.Apellidos, Nombres = item.Nombres, Identificacion = item.Identificacion, Empleado = item.Empleado,Aprobado=true };
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
                return new List<EmpleadoSolicitudViewModel>();
            }
        }

        [HttpPost]
        [Route("ListarEmpleadosdeJefeconSolucitudesVacaciones")]
        public async Task<List<EmpleadoSolicitudViewModel>> ListarEmpleadosdeJefeconSolucitudesVacaciones([FromBody]Empleado empleado)
        {
            try
            {
                var EmpleadoJefe = await db.Empleado
                                   .Where(e => e.NombreUsuario == empleado.NombreUsuario && e.EsJefe == true).FirstOrDefaultAsync();

                if (EmpleadoJefe != null)
                {

                    var listaSubordinados = await db.Empleado.Where(x => x.IdDependencia == EmpleadoJefe.IdDependencia && x.EsJefe == false).Include(x => x.Persona).Include(x => x.SolicitudVacaciones).ToListAsync();

                    var listaEmpleado = new List<EmpleadoSolicitudViewModel>();
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




                        var empleadoSolicitud = new EmpleadoSolicitudViewModel
                        {
                            NombreApellido = item.Persona.Nombres + " " + item.Persona.Apellidos,
                            Identificacion = item.Persona.Identificacion,
                            Aprobado = aprobado,
                            IdEmpleado = item.IdEmpleado,
                            HaSolicitado = haSolicitado,
                        };

                        listaEmpleado.Add(empleadoSolicitud);
                    }

                    return listaEmpleado;
                }

                return new List<EmpleadoSolicitudViewModel>();
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
                return new List<EmpleadoSolicitudViewModel>();
            }
        }

        [HttpPost]
        [Route("ListarEmpleadosdeJefeconSolucitudesViaticos")]
        public async Task<List<EmpleadoSolicitudViewModel>> ListarEmpleadosdeJefeconSolucitudesViaticos([FromBody]Empleado empleado)
        {
            try
            {
                var EmpleadoJefe = await db.Empleado
                                   .Where(e => e.NombreUsuario == empleado.NombreUsuario && e.EsJefe == true).FirstOrDefaultAsync();

                if (EmpleadoJefe != null)
                {

                    var listaSubordinados = await db.Empleado.Where(x => x.IdDependencia == EmpleadoJefe.IdDependencia && x.EsJefe == false).Include(x => x.Persona).Include(x => x.SolicitudViatico).ToListAsync();

                    var listaEmpleado = new List<EmpleadoSolicitudViewModel>();
                    foreach (var item in listaSubordinados)
                    {
                        var haSolicitado = false;
                        var aprobado = true;

                        if (item.SolicitudViatico.Count == 0)
                        {
                            haSolicitado = false;
                            aprobado = false;
                        }
                        else
                        {
                            foreach (var item1 in item.SolicitudViatico)
                            {

                                if (item1.Estado == 0)
                                {
                                    haSolicitado = true;
                                    aprobado = false;
                                    break;
                                }
                            }
                        }




                        var empleadoSolicitud = new EmpleadoSolicitudViewModel
                        {
                            NombreApellido = item.Persona.Nombres + " " + item.Persona.Apellidos,
                            Identificacion = item.Persona.Identificacion,
                            Aprobado = aprobado,
                            IdEmpleado = item.IdEmpleado,
                            HaSolicitado = haSolicitado,
                        };

                        listaEmpleado.Add(empleadoSolicitud);
                    }

                    return listaEmpleado;
                }

                return new List<EmpleadoSolicitudViewModel>();
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
                return new List<EmpleadoSolicitudViewModel>();
            }
        }

        [HttpPost]
        [Route("ListarEmpleadosdeJefeconHorasExtra")]
        public async Task<List<EmpleadoSolicitudViewModel>> ListarEmpleadosdeJefeconHorasExtra([FromBody]Empleado empleado)
        {
            try
            {
                var EmpleadoJefe = await db.Empleado
                                   .Where(e => e.NombreUsuario == empleado.NombreUsuario && e.EsJefe == true).FirstOrDefaultAsync();

                if (EmpleadoJefe != null)
                {

                    var listaSubordinados = await db.Empleado.Where(x => x.IdDependencia == EmpleadoJefe.IdDependencia && x.EsJefe == false).Include(x => x.Persona).Include(x => x.SolicitudHorasExtras).ToListAsync();

                    var listaEmpleado = new List<EmpleadoSolicitudViewModel>();
                    foreach (var item in listaSubordinados)
                    {
                        var haSolicitado = false;
                        var aprobado = true;

                        if (item.SolicitudHorasExtras.Count == 0)
                        {
                            haSolicitado = false;
                            aprobado = false;
                        }
                        else
                        {
                            foreach (var item1 in item.SolicitudHorasExtras)
                            {

                                if (item1.Estado == 0)
                                {
                                    haSolicitado = true;
                                    aprobado = false;
                                    break;
                                }
                            }
                        }




                        var empleadoSolicitud = new EmpleadoSolicitudViewModel
                        {
                            NombreApellido = item.Persona.Nombres + " " + item.Persona.Apellidos,
                            Identificacion = item.Persona.Identificacion,
                            Aprobado = aprobado,
                            IdEmpleado = item.IdEmpleado,
                            HaSolicitado = haSolicitado,
                        };

                        listaEmpleado.Add(empleadoSolicitud);
                    }

                    return listaEmpleado;
                }

                return new List<EmpleadoSolicitudViewModel>();
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
                return new List<EmpleadoSolicitudViewModel>();
            }
        }


        [HttpPost]
        [Route("EmpleadoSegunNombreUsuario")]
        public async Task<Empleado> EmpleadoSegunNombreUsuario([FromBody] string nombreUsuario)
        {
            try
            {

                var empleadoSegunNombre = await db.Empleado
                                  .Include(x => x.Persona)
                                  .Where(x => x.NombreUsuario == nombreUsuario)
                                  .SingleOrDefaultAsync();

                
                return empleadoSegunNombre;

            }
            catch (Exception ex)
            {
                throw;
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

        // PUT: api/Empleados/5
        [HttpPut("{id}")]
        public async Task<Response> PutEmpleado([FromRoute] int id, [FromBody] EmpleadoViewModel empleadoViewModel)
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

                //1. Tabla Empleado
                var empleado = db.Empleado.Find(empleadoViewModel.Empleado.IdEmpleado);
                empleado.IdPersona = empleadoViewModel.Empleado.IdPersona;
                empleado.IdCiudadLugarNacimiento = empleadoViewModel.Empleado.IdCiudadLugarNacimiento;
                empleado.IdProvinciaLugarSufragio = empleadoViewModel.Empleado.IdProvinciaLugarSufragio;
                empleado.IdDependencia = empleadoViewModel.Empleado.IdDependencia;
                empleado.FechaIngreso = empleadoViewModel.Empleado.FechaIngreso;
                empleado.FechaIngresoSectorPublico = empleadoViewModel.Empleado.FechaIngresoSectorPublico;
                empleado.EsJefe = empleadoViewModel.Empleado.EsJefe;
                empleado.TrabajoSuperintendenciaBanco = empleadoViewModel.Empleado.TrabajoSuperintendenciaBanco;
                empleado.DeclaracionJurada = empleadoViewModel.Empleado.DeclaracionJurada;
                empleado.IngresosOtraActividad = empleadoViewModel.Empleado.IngresosOtraActividad;
                empleado.MesesImposiciones = empleadoViewModel.Empleado.MesesImposiciones;
                empleado.DiasImposiciones = empleadoViewModel.Empleado.DiasImposiciones;
                empleado.FondosReservas = empleadoViewModel.Empleado.FondosReservas;
                empleado.NombreUsuario = empleadoViewModel.Empleado.NombreUsuario;
                empleado.Activo = empleadoViewModel.Empleado.Activo;
                db.Empleado.Update(empleado);
                await db.SaveChangesAsync();

                //2. Tabla Persona
                var persona = db.Persona.Find(empleadoViewModel.Persona.IdPersona);
                persona.FechaNacimiento = empleadoViewModel.Persona.FechaNacimiento;
                persona.IdSexo = empleadoViewModel.Persona.IdSexo;
                persona.IdTipoIdentificacion = empleadoViewModel.Persona.IdTipoIdentificacion;
                persona.IdEstadoCivil = empleadoViewModel.Persona.IdEstadoCivil;
                persona.IdGenero = empleadoViewModel.Persona.IdGenero;
                persona.IdNacionalidad = empleadoViewModel.Persona.IdNacionalidad;
                persona.IdTipoSangre = empleadoViewModel.Persona.IdTipoSangre;
                persona.IdEtnia = empleadoViewModel.Persona.IdEtnia;
                persona.Identificacion = empleadoViewModel.Persona.Identificacion;
                persona.Nombres = empleadoViewModel.Persona.Nombres;
                persona.Apellidos = empleadoViewModel.Persona.Apellidos;
                persona.TelefonoPrivado = empleadoViewModel.Persona.TelefonoPrivado;
                persona.TelefonoCasa = empleadoViewModel.Persona.TelefonoCasa;
                persona.CorreoPrivado = empleadoViewModel.Persona.CorreoPrivado;
                persona.LugarTrabajo = empleadoViewModel.Persona.LugarTrabajo;
                persona.IdNacionalidadIndigena = empleadoViewModel.Persona.IdNacionalidadIndigena;
                persona.CallePrincipal = empleadoViewModel.Persona.CallePrincipal;
                persona.CalleSecundaria = empleadoViewModel.Persona.CalleSecundaria;
                persona.Referencia = empleadoViewModel.Persona.Referencia;
                persona.Numero = empleadoViewModel.Persona.Numero;
                persona.IdParroquia = empleadoViewModel.Persona.IdParroquia;
                persona.Ocupacion = empleadoViewModel.Persona.Ocupacion;
                db.Persona.Update(persona);
                await db.SaveChangesAsync();

                //3. Tabla Datos Bancarios
                var datosbancarios = db.DatosBancarios.Find(empleadoViewModel.DatosBancarios.IdDatosBancarios);
                datosbancarios.IdEmpleado = empleadoViewModel.DatosBancarios.IdEmpleado;
                datosbancarios.IdInstitucionFinanciera = empleadoViewModel.DatosBancarios.IdInstitucionFinanciera;
                datosbancarios.NumeroCuenta = empleadoViewModel.DatosBancarios.NumeroCuenta;
                datosbancarios.Ahorros = empleadoViewModel.DatosBancarios.Ahorros;
                db.DatosBancarios.Update(datosbancarios);
                await db.SaveChangesAsync();

                //4. Tabla Empleado Contacto Emergencia
                var empleadoContactoEmergencia = db.EmpleadoContactoEmergencia.Find(empleadoViewModel.EmpleadoContactoEmergencia.IdEmpleadoContactoEmergencia);
                empleadoContactoEmergencia.IdPersona = empleadoViewModel.EmpleadoContactoEmergencia.IdPersona;
                empleadoContactoEmergencia.IdEmpleado = empleadoViewModel.EmpleadoContactoEmergencia.IdEmpleado;
                empleadoContactoEmergencia.IdParentesco = empleadoViewModel.EmpleadoContactoEmergencia.IdParentesco;
                db.EmpleadoContactoEmergencia.Update(empleadoContactoEmergencia);
                await db.SaveChangesAsync();

                //5. Tabla Indice Ocupacional Modalidad Partida
                var indiceOcupacionalModalidadPartida = db.IndiceOcupacionalModalidadPartida.Find(empleadoViewModel.IndiceOcupacionalModalidadPartida.IdIndiceOcupacionalModalidadPartida);
                indiceOcupacionalModalidadPartida.IdIndiceOcupacional = empleadoViewModel.IndiceOcupacionalModalidadPartida.IdIndiceOcupacional;
                indiceOcupacionalModalidadPartida.IdEmpleado = empleadoViewModel.IndiceOcupacionalModalidadPartida.IdEmpleado;
                indiceOcupacionalModalidadPartida.IdFondoFinanciamiento = empleadoViewModel.IndiceOcupacionalModalidadPartida.IdFondoFinanciamiento;
                indiceOcupacionalModalidadPartida.IdModalidadPartida = empleadoViewModel.IndiceOcupacionalModalidadPartida.IdModalidadPartida;
                indiceOcupacionalModalidadPartida.IdTipoNombramiento = empleadoViewModel.IndiceOcupacionalModalidadPartida.IdTipoNombramiento;
                indiceOcupacionalModalidadPartida.Fecha = empleadoViewModel.IndiceOcupacionalModalidadPartida.Fecha;
                indiceOcupacionalModalidadPartida.SalarioReal = empleadoViewModel.IndiceOcupacionalModalidadPartida.SalarioReal;
                db.IndiceOcupacionalModalidadPartida.Update(indiceOcupacionalModalidadPartida);
                await db.SaveChangesAsync();

                //6. Persona Estudio
                foreach (PersonaEstudio itemPersonaEstudio in empleadoViewModel.PersonaEstudio.Where(x => x.IdPersona == empleadoViewModel.Persona.IdPersona).ToList())
                {
                    var personaEstudio = db.PersonaEstudio.Find(itemPersonaEstudio.IdPersonaEstudio);
                    personaEstudio.FechaGraduado = itemPersonaEstudio.FechaGraduado;
                    personaEstudio.Observaciones = itemPersonaEstudio.Observaciones;
                    personaEstudio.IdTitulo = itemPersonaEstudio.IdTitulo;
                    personaEstudio.IdPersona = itemPersonaEstudio.IdPersona;
                    personaEstudio.NoSenescyt = itemPersonaEstudio.NoSenescyt;
                    db.PersonaEstudio.Update(personaEstudio);
                    await db.SaveChangesAsync();
                }

                //7. Trayectoria Laboral
                foreach (TrayectoriaLaboral itemTrayectoriaLaboral in empleadoViewModel.TrayectoriaLaboral.Where(x => x.IdPersona == empleadoViewModel.Persona.IdPersona).ToList())
                {
                    var trayectoriaLaboral = db.TrayectoriaLaboral.Find(itemTrayectoriaLaboral.IdTrayectoriaLaboral);
                    trayectoriaLaboral.IdPersona = itemTrayectoriaLaboral.IdPersona;
                    trayectoriaLaboral.FechaInicio = itemTrayectoriaLaboral.FechaInicio;
                    trayectoriaLaboral.FechaFin = itemTrayectoriaLaboral.FechaFin;
                    trayectoriaLaboral.Empresa = itemTrayectoriaLaboral.Empresa;
                    trayectoriaLaboral.PuestoTrabajo = itemTrayectoriaLaboral.PuestoTrabajo;
                    trayectoriaLaboral.DescripcionFunciones = itemTrayectoriaLaboral.DescripcionFunciones;
                    db.TrayectoriaLaboral.Update(trayectoriaLaboral);
                    await db.SaveChangesAsync();
                }


                //8. Persona Discapacidad
                foreach (PersonaDiscapacidad itemPersonaDiscapacidad in empleadoViewModel.PersonaDiscapacidad.Where(x => x.IdPersona == empleadoViewModel.Persona.IdPersona).ToList())
                {
                    var personaDiscapacidad = db.PersonaDiscapacidad.Find(itemPersonaDiscapacidad.IdPersonaDiscapacidad);
                    personaDiscapacidad.IdTipoDiscapacidad = itemPersonaDiscapacidad.IdTipoDiscapacidad;
                    personaDiscapacidad.IdPersona = itemPersonaDiscapacidad.IdPersona;
                    personaDiscapacidad.NumeroCarnet = itemPersonaDiscapacidad.NumeroCarnet;
                    personaDiscapacidad.Porciento = itemPersonaDiscapacidad.Porciento;
                    db.PersonaDiscapacidad.Update(personaDiscapacidad);
                    await db.SaveChangesAsync();
                }

                //9. Persona Enfermedad
                foreach (PersonaEnfermedad itemPersonaEnfermedad in empleadoViewModel.PersonaEnfermedad.Where(x => x.IdPersona == empleadoViewModel.Persona.IdPersona).ToList())
                {
                    var personaEnfermedad = db.PersonaEnfermedad.Find(itemPersonaEnfermedad.IdPersonaEnfermedad);
                    personaEnfermedad.IdTipoEnfermedad = itemPersonaEnfermedad.IdTipoEnfermedad;
                    personaEnfermedad.IdPersona = itemPersonaEnfermedad.IdPersona;
                    personaEnfermedad.InstitucionEmite = itemPersonaEnfermedad.InstitucionEmite;
                    db.PersonaEnfermedad.Update(personaEnfermedad);
                    await db.SaveChangesAsync();
                }

                //10. Persona Sustituto
                var personaSustituto = db.PersonaSustituto.Find(empleadoViewModel.PersonaSustituto.IdPersonaSustituto);
                personaSustituto.IdParentesco = empleadoViewModel.PersonaSustituto.IdParentesco;
                personaSustituto.IdPersona = empleadoViewModel.PersonaSustituto.IdPersona;
                personaSustituto.IdPersonaDiscapacidad = empleadoViewModel.PersonaSustituto.IdPersonaDiscapacidad;
                db.PersonaSustituto.Update(personaSustituto);
                await db.SaveChangesAsync();
                
                //11. Discapacidad Sustituto
                foreach (DiscapacidadSustituto itemDiscapacidadSustituto in empleadoViewModel.DiscapacidadSustituto.Where(x => x.IdPersonaSustituto == empleadoViewModel.PersonaSustituto.IdPersonaSustituto).ToList())
                {
                    var discapacidadSustituto = db.DiscapacidadSustituto.Find(itemDiscapacidadSustituto.IdDiscapacidadSustituto);
                    discapacidadSustituto.IdTipoDiscapacidad = itemDiscapacidadSustituto.IdTipoDiscapacidad;
                    discapacidadSustituto.PorcentajeDiscapacidad = itemDiscapacidadSustituto.PorcentajeDiscapacidad;
                    discapacidadSustituto.NumeroCarnet = itemDiscapacidadSustituto.NumeroCarnet;
                    discapacidadSustituto.IdPersonaSustituto = itemDiscapacidadSustituto.IdPersonaSustituto;
                    db.DiscapacidadSustituto.Update(discapacidadSustituto);
                    await db.SaveChangesAsync();
                }

              
                //12. Enfermedad Sustituto
                foreach (EnfermedadSustituto itemEnfermedadSustituto in empleadoViewModel.EnfermedadSustituto.Where(x => x.IdPersonaSustituto == empleadoViewModel.PersonaSustituto.IdPersonaSustituto).ToList())
                {
                    var enfermedadSustituto = db.EnfermedadSustituto.Find(itemEnfermedadSustituto.IdEnfermedadSustituto);
                    enfermedadSustituto.IdTipoEnfermedad = itemEnfermedadSustituto.IdTipoEnfermedad;
                    enfermedadSustituto.InstitucionEmite = itemEnfermedadSustituto.InstitucionEmite;
                    enfermedadSustituto.IdPersonaSustituto = itemEnfermedadSustituto.IdPersonaSustituto;
                    db.EnfermedadSustituto.Update(enfermedadSustituto);
                    await db.SaveChangesAsync();
                }

                //13. Empleado Familiar 
                foreach (EmpleadoFamiliar itemEmpleadoFamiliar in empleadoViewModel.EmpleadoFamiliar.Where(x => x.IdEmpleado == empleadoViewModel.Empleado.IdEmpleado).ToList())
                {
                    var empleadoFamiliar = db.EmpleadoFamiliar.Find(itemEmpleadoFamiliar.IdEmpleadoFamiliar);
                    empleadoFamiliar.IdPersona = itemEmpleadoFamiliar.IdPersona;
                    empleadoFamiliar.IdEmpleado = itemEmpleadoFamiliar.IdEmpleado;
                    empleadoFamiliar.IdParentesco = itemEmpleadoFamiliar.IdParentesco;
                    db.EmpleadoFamiliar.Update(empleadoFamiliar);
                    await db.SaveChangesAsync();
                }


                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                    Resultado= empleadoViewModel
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


        // PUT: api/BasesDatos/5
        //[HttpPut("{id}")]
        //public async Task<Response> PutEmpleado([FromRoute] int id, [FromBody] Empleado empleado)
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


        //        var Empleado = db.Empleado.Find(empleado.IdEmpleado);

        //        Empleado.IdPersona = 5;
        //        Empleado.IdDependencia = 1;
        //        Empleado.IdCiudadLugarNacimiento = 1;
        //        Empleado.IdProvinciaLugarSufragio = 1;


        //        Empleado.FechaIngreso = empleado.FechaIngreso;
        //        Empleado.FechaIngresoSectorPublico = empleado.FechaIngresoSectorPublico;
        //        Empleado.TrabajoSuperintendenciaBanco = empleado.TrabajoSuperintendenciaBanco;
        //        Empleado.DeclaracionJurada = empleado.DeclaracionJurada;
        //        Empleado.IngresosOtraActividad = empleado.IngresosOtraActividad;
        //        Empleado.MesesImposiciones = empleado.MesesImposiciones;
        //        Empleado.DiasImposiciones = empleado.DiasImposiciones;
        //        Empleado.FondosReservas = empleado.FondosReservas;

        //        db.Empleado.Update(Empleado);
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

        // PUT: api/BasesDatos/5
        [HttpPut("{id}")]
        public async Task<Response> PutEstadoEmpleado([FromRoute] int id, [FromBody] Empleado empleado)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return new Response
                //    {
                //        IsSuccess = false,
                //        Message = Mensaje.ModeloInvalido
                //    };
                //}

                var empleadoActualizar = await db.Empleado.Where(x => x.IdEmpleado == id).FirstOrDefaultAsync();

                if (empleadoActualizar != null)
                {
                    try
                    {
                        empleadoActualizar.Activo = false;
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

    }
}