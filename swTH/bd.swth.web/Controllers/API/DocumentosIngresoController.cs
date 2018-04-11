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
    [Route("api/DocumentosIngreso")]
    public class DocumentosIngresoController : Controller
    {
        private readonly SwTHDbContext db;

        public DocumentosIngresoController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarDocumentosIngreso")]
        public async Task<List<DocumentosIngreso>> GetDocumentosIngreso()
        {
            try
            {
                return await db.DocumentosIngreso.OrderBy(x => x.Descripcion).ToListAsync();
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
                return new List<DocumentosIngreso>();
            }
        }

        // GET: api/BasesDatos
        [HttpPost]
        [Route("ListarDocumentosIngresoEmpleado")]
        public async Task<List<DocumentosIngresoEmpleado>> GetDocumentosIngresoEmpleado([FromBody] Empleado empleado)
        {
            try
            {
                return await db.DocumentosIngresoEmpleado.Where(x=>x.IdEmpleado == empleado.IdEmpleado).ToListAsync();
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
                return new List<DocumentosIngresoEmpleado>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetDocumentosIngreso([FromRoute] int id)
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

                var DocumentosIngreso = await db.DocumentosIngreso.SingleOrDefaultAsync(m => m.IdDocumentosIngreso == id);

                if (DocumentosIngreso == null)
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
                    Resultado = DocumentosIngreso,
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


        // POST: api/BasesDatos
        [HttpPost]
        [Route("GetDocumentoIngresoEmpleado")]
        public async Task<Response> GetDocumentoIngresoEmpleado([FromBody] Empleado empleado)
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

                var DocumentosIngresoEmpleado = await db.DocumentosIngresoEmpleado.SingleOrDefaultAsync(m => m.IdEmpleado == empleado.IdEmpleado);

                if (DocumentosIngresoEmpleado == null)
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
                    Resultado = DocumentosIngresoEmpleado,
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
        public async Task<Response> PutDocumentosIngreso([FromRoute] int id, [FromBody] DocumentosIngreso documentosIngreso)
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

                var existe = Existe(documentosIngreso);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var documentosIngresoActualizar = await db.DocumentosIngreso.Where(x => x.IdDocumentosIngreso == id).FirstOrDefaultAsync();

                if (documentosIngresoActualizar != null)
                {
                    try
                    {
                        documentosIngresoActualizar.Descripcion = documentosIngreso.Descripcion;
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

        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarDocumentosIngreso")]
        public async Task<Response> PostDocumentosIngreso([FromBody] DocumentosIngreso DocumentosIngreso)
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

                var respuesta = Existe(DocumentosIngreso);
                if (!respuesta.IsSuccess)
                {
                    db.DocumentosIngreso.Add(DocumentosIngreso);
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
                    Message = Mensaje.ExisteRegistro
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

        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarDocumentosIngresoEmpleado")]
        public async Task<Response> PostDocumentosIngresoEmpleado([FromBody] ViewModelDocumentoIngresoEmpleado viewModelDocumentoIngresoEmpleado)
        {
            try
            {
                foreach (var item in viewModelDocumentoIngresoEmpleado.DocumentosSeleccionados)
                {


                    var documentosIngresoEmpleado = new DocumentosIngresoEmpleado
                    {
                        IdEmpleado = viewModelDocumentoIngresoEmpleado.empleadoViewModel.IdEmpleado,
                        IdDocumentosIngreso = Convert.ToInt32(item),
                        Entregado = true
                    };
                    db.DocumentosIngresoEmpleado.Add(documentosIngresoEmpleado);
                }

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
        [HttpPost]
        [Route("EditarCheckListDocumentos")]
        public async Task<Response> PutCheckListDocumentosIngreso([FromBody] ViewModelDocumentoIngresoEmpleado viewModelDocumentoIngresoEmpleado)
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


                var documentosIngresoActualizar = await db.DocumentosIngresoEmpleado.Where(x => x.IdEmpleado == viewModelDocumentoIngresoEmpleado.empleadoViewModel.IdEmpleado).ToListAsync();

                if (documentosIngresoActualizar != null)
                {
                    try
                    {
                        foreach(var item1 in viewModelDocumentoIngresoEmpleado.DocumentosSeleccionados)
                          {
                            var bandera = true;
                              foreach(var item2 in documentosIngresoActualizar )
                           {

                                if(Convert.ToInt32(item1) == item2.IdDocumentosIngreso)
                                {                                            
                                  bandera = false;
                                }
                           }
                                if (bandera)
                                    {
                                var documentoIngresoEmpleado = new DocumentosIngresoEmpleado
                                {
                                    IdDocumentosIngreso = Convert.ToInt32(item1),
                                    IdEmpleado = viewModelDocumentoIngresoEmpleado.empleadoViewModel.IdEmpleado,
                                    Entregado = true
                                };
                                db.DocumentosIngresoEmpleado.Add(documentoIngresoEmpleado);
                                }
                            }

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

        // DELETE: api/BasesDatos/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteDocumentosIngreso([FromRoute] int id)
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

                var respuesta = await db.DocumentosIngreso.SingleOrDefaultAsync(m => m.IdDocumentosIngreso == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.DocumentosIngreso.Remove(respuesta);
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



        private Response Existe(DocumentosIngreso DocumentosIngreso)
        {
            var bdd = DocumentosIngreso.Descripcion.ToUpper().TrimEnd().TrimStart();
            var documentoingresorespuesta = db.DocumentosIngreso.Where(p => p.Descripcion.ToUpper().TrimStart().TrimEnd() == bdd).FirstOrDefault();
            if (documentoingresorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = null,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = documentoingresorespuesta,
            };
        }
    }
}