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


        [HttpPost]
        [Route("ObtenerEnfermedadSustitutos")]
        public async Task<Response> ObtenerEnfermedadSustitutos([FromBody] EnfermedadSustitutoRequest viewModel)
        {
            try
            {
                var enfermedadSustituto = await db.EnfermedadSustituto
                                                       .Where(y => y.IdEnfermedadSustituto == viewModel.IdEnfermedadSustituto)
                                                       .Select(c => new EnfermedadSustitutoRequest
                                                       {
                                                           IdEnfermedadSustituto=c.IdEnfermedadSustituto,
                                                           IdPersonaSustituto=Convert.ToInt32(c.IdPersonaSustituto),
                                                           IdTipoEnfermedad=Convert.ToInt32(c.IdTipoEnfermedad),
                                                           InstitucionEmite=c.InstitucionEmite,
                                                           NombreTipoEnfermedad=c.TipoEnfermedad.Nombre,
                                                       })
                                 .FirstOrDefaultAsync();

                if (enfermedadSustituto != null)
                {
                    return new Response { IsSuccess = true, Resultado = enfermedadSustituto };
                }

                return new Response { IsSuccess = false, Message = Mensaje.RegistroNoEncontrado };

            }
            catch (Exception ex)
            {
                return new Response { IsSuccess = false, Message = Mensaje.Excepcion };
            }
        }

        [HttpPost]
        [Route("ObtenerDiscapacidadSustitutos")]
        public async Task<Response> ObtenerDiscapacidadSustitutos([FromBody] DiscapacidadSustitutoRequest viewModel)
        {
            try
            {
                var discapacidadSustituto = await db.DiscapacidadSustituto
                                                       .Where(y => y.IdDiscapacidadSustituto == viewModel.IdDiscapacidadSustituto)
                                                       .Select(c => new DiscapacidadSustitutoRequest
                                                       {
                                                           IdDiscapacidadSustituto=c.IdDiscapacidadSustituto,
                                                           IdPersonaSustituto=Convert.ToInt32(c.IdPersonaSustituto),
                                                           IdTipoDiscapacidad=Convert.ToInt32(c.IdTipoDiscapacidad),
                                                           NombreTipoDiscapacidad=c.TipoDiscapacidad.Nombre,
                                                           NumeroCarnet=c.NumeroCarnet,
                                                           PorcentajeDiscapacidad=c.PorcentajeDiscapacidad,
                                                       })
                                 .FirstOrDefaultAsync();

                if (discapacidadSustituto != null)
                {
                    return new Response { IsSuccess = true, Resultado = discapacidadSustituto };
                }

                return new Response { IsSuccess = false, Message = Mensaje.RegistroNoEncontrado };

            }
            catch (Exception ex)
            {
                return new Response { IsSuccess = false, Message = Mensaje.Excepcion };
            }
        }


        [HttpPost]
        [Route("ObtenerPersonasSustitutos")]
        public async Task<Response> ObtenerPersonasSustitutos([FromBody] ViewModelEmpleadoSustituto viewModel)
        {
            try
            {
                var personaSustituto = await db.PersonaSustituto
                                                       .Where(y => y.IdPersonaSustituto == viewModel.IdPersonaSustituto)
                                                       .Select(c => new ViewModelEmpleadoSustituto
                                                       {

                                                           Nombre = c.Persona.Nombres,
                                                           Apellido = c.Persona.Apellidos,
                                                           TelefonoPrivado = c.Persona.TelefonoPrivado,
                                                           TelefonoCasa = c.Persona.TelefonoCasa,
                                                           IdPersona = c.IdPersona,
                                                           IdParentesco = c.Parentesco.IdParentesco,
                                                           IdEmpleado = c.IdEmpleado,
                                                           IdPersonaSustituto=c.IdPersonaSustituto,
                                                       })
                                 .FirstOrDefaultAsync();

                if (personaSustituto != null)
                {
                    return new Response { IsSuccess = true, Resultado = personaSustituto };
                }

                return new Response { IsSuccess = false, Message = Mensaje.RegistroNoEncontrado };

            }
            catch (Exception ex)
            {
                return new Response { IsSuccess = false, Message = Mensaje.Excepcion };
            }
        }

        [HttpPost]
        [Route("EliminarPersonasSustitutos")]
        public async Task<Response> EliminarPersonasSustitutos([FromBody] ViewModelEmpleadoSustituto viewModel)
        {
            try
            {
                var personaSustitutoEliminar = await db.PersonaSustituto.Where(y => y.IdPersonaSustituto == viewModel.IdPersonaSustituto)
                                 .FirstOrDefaultAsync();

                if (personaSustitutoEliminar != null)
                {
                    db.PersonaSustituto.Remove(personaSustitutoEliminar);
                    await db.SaveChangesAsync();
                    return new Response { IsSuccess = true, Resultado = personaSustitutoEliminar };
                }
                return new Response { IsSuccess = false, Message = Mensaje.RegistroNoEncontrado };

            }
            catch (Exception ex)
            {
                return new Response { IsSuccess = false, Message = Mensaje.Excepcion };
            }
        }

        [HttpPost]
        [Route("EliminarDiscapacidadSustituto")]
        public async Task<Response> EliminarDiscapacidadSustituto([FromBody] DiscapacidadSustitutoRequest viewModel)
        {
            try
            {
                if (viewModel.IdDiscapacidadSustituto <= 0)
                {
                    return new Response { IsSuccess = false };
                }

                var discapacidadEliminar = await db.DiscapacidadSustituto.Where(x => x.IdDiscapacidadSustituto == viewModel.IdDiscapacidadSustituto).FirstOrDefaultAsync();

                if (discapacidadEliminar != null)
                {
                    db.DiscapacidadSustituto.Remove(discapacidadEliminar);
                    await db.SaveChangesAsync();

                    return new Response
                    {
                        IsSuccess = true,
                        Resultado = discapacidadEliminar,
                    };
                }

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.RegistroNoEncontrado,
                };

            }
            catch (Exception ex)
            {
                return new Response { IsSuccess = false, Message = Mensaje.Excepcion };
            }
        }

        [HttpPost]
        [Route("EliminarEnfermedadeSustituto")]
        public async Task<Response> EliminarEnfermedadeSustituto([FromBody] EnfermedadSustitutoRequest viewModel)
        {
            try
            {
                if (viewModel.IdEnfermedadSustituto <= 0)
                {
                    return new Response { IsSuccess = false };
                }

                var enfermedadEliminar = await db.EnfermedadSustituto.Where(x => x.IdEnfermedadSustituto == viewModel.IdEnfermedadSustituto).FirstOrDefaultAsync();

                if (enfermedadEliminar!=null)
                {
                    db.EnfermedadSustituto.Remove(enfermedadEliminar);
                    await db.SaveChangesAsync();

                    return new Response
                    {
                        IsSuccess = true,
                        Resultado = enfermedadEliminar,
                    };
                }

                return new Response
                {
                    IsSuccess = false,
                    Message=Mensaje.RegistroNoEncontrado,
                };

            }
            catch (Exception ex)
            {
                return new Response { IsSuccess = false, Message = Mensaje.Excepcion };
            }
        }

        [HttpPost]
        [Route("EditarEnfermedadeSustituto")]
        public async Task<Response> EditarEnfermedadeSustituto([FromBody] EnfermedadSustitutoRequest viewModel)
        {
            try
            {
                if (viewModel.IdPersonaSustituto <= 0)
                {
                    return new Response { IsSuccess = false };
                }

                var enfermedadActualizar =await db.EnfermedadSustituto.Where(x => x.IdEnfermedadSustituto == viewModel.IdEnfermedadSustituto).FirstOrDefaultAsync();


                if (enfermedadActualizar!=null)
                {
                    enfermedadActualizar.IdTipoEnfermedad = viewModel.IdTipoEnfermedad;
                    enfermedadActualizar.InstitucionEmite = viewModel.InstitucionEmite;


                    //convertir a mayúsculas 
                    enfermedadActualizar.InstitucionEmite = enfermedadActualizar.InstitucionEmite.ToString().ToUpper();

                    db.EnfermedadSustituto.Update(enfermedadActualizar);
                    await db.SaveChangesAsync();

                    return new Response
                    {
                        IsSuccess = true,
                        Resultado = enfermedadActualizar,
                    };

                }

                return new Response
                {
                    IsSuccess = true,
                    Message=Mensaje.RegistroNoEncontrado,
                };

            }
            catch (Exception ex)
            {
                return new Response { IsSuccess = false, Message = Mensaje.Excepcion };
            }
        }

        [HttpPost]
        [Route("EditarDiscapacidadSustituto")]
        public async Task<Response> EditarDiscapacidadSustituto([FromBody] DiscapacidadSustitutoRequest viewModel)
        {
            try
            {
                if (viewModel.IdDiscapacidadSustituto <= 0)
                {
                    return new Response { IsSuccess = false };
                }

                var discapacidadActualizar = await db.DiscapacidadSustituto.Where(x => x.IdDiscapacidadSustituto == viewModel.IdDiscapacidadSustituto).FirstOrDefaultAsync();


                if (discapacidadActualizar != null)
                {
                    discapacidadActualizar.PorcentajeDiscapacidad = viewModel.PorcentajeDiscapacidad;
                    discapacidadActualizar.NumeroCarnet = viewModel.NumeroCarnet;
                    discapacidadActualizar.IdTipoDiscapacidad = viewModel.IdTipoDiscapacidad;

                    db.DiscapacidadSustituto.Update(discapacidadActualizar);
                    await db.SaveChangesAsync();

                    return new Response
                    {
                        IsSuccess = true,
                        Resultado = discapacidadActualizar,
                    };

                }

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.RegistroNoEncontrado,
                };

            }
            catch (Exception ex)
            {
                return new Response { IsSuccess = false, Message = Mensaje.Excepcion };
            }
        }

        [HttpPost]
        [Route("InsertarEnfermedadeSustituto")]
        public async Task<Response> InsertarEnfermedadeSustituto([FromBody] EnfermedadSustitutoRequest viewModel)
        {
            try
            {
                if (viewModel.IdPersonaSustituto <= 0)
                {
                    return new Response { IsSuccess = false };
                }

                var enfermedadSustituto = new EnfermedadSustituto
                {
                    IdPersonaSustituto=viewModel.IdPersonaSustituto,
                    IdTipoEnfermedad=viewModel.IdTipoEnfermedad,
                    InstitucionEmite=viewModel.InstitucionEmite,
                };

                // convertir a mayúsculas
                enfermedadSustituto.InstitucionEmite = enfermedadSustituto.InstitucionEmite.ToString().ToUpper();


                await db.EnfermedadSustituto.AddAsync(enfermedadSustituto);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Resultado = enfermedadSustituto,
                };

            }
            catch (Exception ex)
            {
                return new Response { IsSuccess = false, Message = Mensaje.Excepcion };
            }
        }



        [HttpPost]
        [Route("ListarEnfermedadesSustitutos")]
        public async Task<Response> GetPersonaSustituto([FromBody] EnfermedadSustitutoRequest viewModel)
        {
            try
            {
                if (viewModel.IdPersonaSustituto<=0)
                {
                    return new Response { IsSuccess=false};
                }
                var listaEnfermedades= await db.EnfermedadSustituto.Where(y => y.IdPersonaSustituto==viewModel.IdPersonaSustituto)
                                .Select(x => new ViewModelEnfermedadSustituto
                                {
                                   IdEnfermedadSustituto=x.IdEnfermedadSustituto,
                                   IdPersonaSustituto=Convert.ToInt32(x.IdPersonaSustituto),
                                   IdTipoEnfermedad=Convert.ToInt32(x.IdTipoEnfermedad),
                                   InstitucionEmite=x.InstitucionEmite,
                                   NombreTipoEnfermedad=x.TipoEnfermedad.Nombre,

                                }).ToListAsync();



                return new Response
                { IsSuccess=true,
                 Resultado= new EnfermedadSustitutoRequest
                                {
                                    IdPersonaSustituto = viewModel.IdPersonaSustituto,
                                    ListaEnfermedadesSustitutos = listaEnfermedades
                                },
                };

            }
            catch (Exception ex)
            {
                return new Response { IsSuccess = false,Message=Mensaje.Excepcion };
            }
        }


        [HttpPost]
        [Route("InsertarDiscapacidadSustituto")]
        public async Task<Response> InsertarDiscapacidadSustituto([FromBody] DiscapacidadSustitutoRequest viewModel)
        {
            try
            {
                if (viewModel.IdPersonaSustituto <= 0)
                {
                    return new Response { IsSuccess = false };
                }

                var discapacidadSustituto = new DiscapacidadSustituto
                {
                    IdPersonaSustituto = viewModel.IdPersonaSustituto,
                    PorcentajeDiscapacidad=viewModel.PorcentajeDiscapacidad,
                    NumeroCarnet=viewModel.NumeroCarnet,
                    IdTipoDiscapacidad=viewModel.IdTipoDiscapacidad,
                    IdDiscapacidadSustituto=viewModel.IdDiscapacidadSustituto,
                };

                await db.DiscapacidadSustituto.AddAsync(discapacidadSustituto);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Resultado = discapacidadSustituto,
                };

            }
            catch (Exception ex)
            {
                return new Response { IsSuccess = false, Message = Mensaje.Excepcion };
            }
        }

        [HttpPost]
        [Route("ListarDiscapacidadSustitutos")]
        public async Task<Response> ListarDiscapacidadSustitutos([FromBody] DiscapacidadSustitutoRequest viewModel)
        {
            try
            {
                if (viewModel.IdPersonaSustituto <= 0)
                {
                    return new Response { IsSuccess = false };
                }
                var listaDiscapacidades = await db.DiscapacidadSustituto.Where(y => y.IdPersonaSustituto == viewModel.IdPersonaSustituto)
                                .Select(x => new ViewModelDiscapacidadSustituto
                                {
                                    IdDiscapacidadSustituto=x.IdDiscapacidadSustituto,
                                    IdTipoDiscapacidad=x.IdTipoDiscapacidad,
                                    NombreTipoDiscapacidad=x.TipoDiscapacidad.Nombre,
                                    NumeroCarnet=x.NumeroCarnet,
                                    PorcentajeDiscapacidad=x.PorcentajeDiscapacidad,

                                }).ToListAsync();



                return new Response
                {
                    IsSuccess = true,
                    Resultado = new DiscapacidadSustitutoRequest
                    {
                        IdPersonaSustituto = viewModel.IdPersonaSustituto,
                        ListaDiscapacidadSustitutos = listaDiscapacidades
                    },
                };

            }
            catch (Exception ex)
            {
                return new Response { IsSuccess = false, Message = Mensaje.Excepcion };
            }
        }


        // GET: api/BasesDatos
        [HttpPost]
        [Route("ListarPersonasSustitutos")]
        public async Task<List<ViewModelEmpleadoSustituto>> GetPersonaSustituto([FromBody] ViewModelEmpleadoSustituto viewModel)
        {
            try
            {
                var enfermedades = await db.EnfermedadSustituto
                .Include(i => i.TipoEnfermedad)
                .ToListAsync();

                var discapacidades = await db.DiscapacidadSustituto
                    .Include(i => i.TipoDiscapacidad)
                    .ToListAsync();

                var lista = await db.PersonaSustituto.Where(y => y.IdEmpleado == viewModel.IdEmpleado)
                                .Select(x => new ViewModelEmpleadoSustituto
                                {
                                    IdPersona = x.IdPersona,
                                    IdParentesco = x.IdParentesco,
                                    Apellido = x.Persona.Apellidos,
                                    Nombre = x.Persona.Nombres,
                                    IdEmpleado = x.IdEmpleado,
                                    TelefonoCasa = x.Persona.TelefonoCasa,
                                    TelefonoPrivado = x.Persona.TelefonoPrivado,
                                    NombreParentesco = x.Parentesco.Nombre,
                                    IdPersonaSustituto = x.IdPersonaSustituto,

                                    Enfermedades = EnfermedadesPersonaSustitutoTexto(x.IdPersonaSustituto, enfermedades),
                                    Discapacidades = DiscapacidadesPersonaSustitutoTexto(x.IdPersonaSustituto, discapacidades)

                                }).ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                return new List<ViewModelEmpleadoSustituto>();
            }
        }

        private string EnfermedadesPersonaSustitutoTexto(int id, List<EnfermedadSustituto> ListasEnfermedades) {
            var texto = "";

            var lista = new List<EnfermedadSustituto>(ListasEnfermedades);

            lista = lista.Where(w => w.IdPersonaSustituto == id).ToList();

            foreach (var item in lista) {
                texto = texto + item.TipoEnfermedad.Nombre + ", ";
            }

            texto = texto.TrimEnd(' ');
            texto = texto.TrimEnd(',');
            return texto;
        }

        private string DiscapacidadesPersonaSustitutoTexto(int id, List<DiscapacidadSustituto> ListaDiscapacidades)
        {
            var texto = "";

            var lista = new List<DiscapacidadSustituto>(ListaDiscapacidades);

            lista = lista.Where(w => w.IdPersonaSustituto == id).ToList();

            foreach (var item in lista)
            {
                texto = texto + item.TipoDiscapacidad.Nombre + ", ";
            }
            texto = texto.TrimEnd(' ');
            texto = texto.TrimEnd(',');
            return texto;
        }


        [HttpPost]
        [Route("ObtenerPersonaSustitutosPorId")]
        public async Task<Response> ObtenerPersonaSustitutosPorId([FromBody] Empleado empleado)
        {
            try
            {
                var PersonaSustituto = await db.PersonaSustituto.Include(x => x.Persona).Include(x => x.Parentesco).Include(x => x.DiscapacidadSustituto).Include(x => x.EnfermedadSustituto).SingleOrDefaultAsync(x => x.IdPersona == empleado.IdPersona);
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
                    ExceptionTrace = ex.Message,
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
                    .Include(x => x.DiscapacidadSustituto).Include(x => x.EnfermedadSustituto)

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
                            //PersonaSustitutoActualizar.IdPersonaDiscapacidad = empleadoSustitutoViewModel.IdPersonaDiscapacidad;

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
                    var existe = new Response { IsSuccess = true };
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
                                //PersonaSustitutoActualizar.IdPersonaDiscapacidad = empleadoSustitutoViewModel.IdPersonaDiscapacidad;

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
        [Route("InsertarPersonaSustituto")]
        public async Task<Response> InsertarPersonaSustituto([FromBody] ViewModelEmpleadoSustituto viewModelEmpleadoSustituto)
        {
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {



                    //1. Insertar Persona

                    var persona = new Persona()
                    {
                        Nombres = viewModelEmpleadoSustituto.Nombre,
                        Apellidos = viewModelEmpleadoSustituto.Apellido,
                        TelefonoPrivado = viewModelEmpleadoSustituto.TelefonoPrivado,
                        TelefonoCasa = viewModelEmpleadoSustituto.TelefonoCasa,
                        IdTipoIdentificacion = null
                    };


                    // cambiar a mayúsculas
                    persona.Nombres = persona.Nombres.ToString().ToUpper();
                    persona.Apellidos = persona.Apellidos.ToString().ToUpper();


                    var personaInsertarda = await db.Persona.AddAsync(persona);
                    await db.SaveChangesAsync();

                    //2. Insertar PersonaDiscapacidad
                    var personaSustituto = new PersonaSustituto()
                    {
                        IdPersona = personaInsertarda.Entity.IdPersona,
                        IdParentesco = viewModelEmpleadoSustituto.IdParentesco,
                        IdEmpleado = viewModelEmpleadoSustituto.IdEmpleado,
                    };
                    var PersonaDiscapacidadInsertado = await db.PersonaSustituto.AddAsync(personaSustituto);
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

        [HttpPost]
        [Route("EditarPersonaSustituto")]
        public async Task<Response> EditarPersonaSustituto([FromBody] ViewModelEmpleadoSustituto viewModelEmpleadoSustituto)
        {
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {


                    var personaSustitutoActualizar = await db.PersonaSustituto.Where(y => y.IdPersonaSustituto == viewModelEmpleadoSustituto.IdPersonaSustituto)
                              .FirstOrDefaultAsync();
                    personaSustitutoActualizar.IdParentesco = viewModelEmpleadoSustituto.IdParentesco;

                    db.PersonaSustituto.Update(personaSustitutoActualizar);


                    var personaActualizar = await db.Persona.Where(y => y.IdPersona == personaSustitutoActualizar.IdPersona)
                              .FirstOrDefaultAsync();
                    //2. Insertar PersonaDiscapacidad
                    personaActualizar.Nombres = viewModelEmpleadoSustituto.Nombre.ToString().ToUpper();
                    personaActualizar.Apellidos = viewModelEmpleadoSustituto.Apellido.ToString().ToUpper();
                    personaActualizar.TelefonoPrivado = viewModelEmpleadoSustituto.TelefonoPrivado;
                    personaActualizar.TelefonoCasa = viewModelEmpleadoSustituto.TelefonoCasa;
                    db.Persona.Update(personaActualizar);

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
        // DELETE: api/BasesDatos/5
        [HttpDelete("{id}")]
        public async Task<Response> DeletePersonaSustituto([FromRoute] int id)
        {
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {
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


        //private Response Existe(ViewModelEmpleadoSustituto empleadoSustitutoViewModel)
        //{
        //    var identificacion = empleadoSustitutoViewModel.Identificacion.ToUpper().TrimEnd().TrimStart();
        //    var Empleadorespuesta = db.Persona.Where(p => p.Identificacion == identificacion).FirstOrDefault();
        //    if (Empleadorespuesta != null)
        //    {
        //        return new Response
        //        {
        //            IsSuccess = true,
        //            Message = Mensaje.ExisteRegistro,
        //        };

        //    }

        //    return new Response
        //    {
        //        IsSuccess = false,
        //    };
        //}
    }
}