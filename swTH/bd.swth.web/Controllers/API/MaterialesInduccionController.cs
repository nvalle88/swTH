using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.swth.servicios.Interfaces;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.swth.entidades.Enumeradores;
using bd.swth.entidades.Utils;
using bd.log.guardar.Enumeradores;
using bd.swth.entidades.ObjectTransfer;
using System.IO;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/MaterialesInduccion")]
    public class MaterialesInduccionController : Controller
    {
        private readonly IUploadFileService uploadFileService;
        private readonly SwTHDbContext db;



        public MaterialesInduccionController(SwTHDbContext db, IUploadFileService uploadFileService)
        {
            this.uploadFileService = uploadFileService;
            this.db = db;
        }


        // Get: api/BasesDatos
        [HttpGet]
        [Route("ListarMaterialesInduccionTTHH")]
        public async Task<List<MaterialInduccion>> ListarMaterialesInduccionTTHH()
        {
            var lista = new List<MaterialInduccion>();

            try
            {
                return await db.MaterialInduccion.OrderBy(x => x.Titulo).ToListAsync();
            }
            catch (Exception ex)
            {
                return lista;
            }
        }



        // Post: api/BasesDatos
        [HttpPost]
        [Route("ListarMaterialesInduccion")]
        public async Task<Response> ListarMaterialesInduccion([FromBody] Induccion induccion)
        {
            try
            {
                var induccionRealizada = await db.Induccion.Where(y=>y.IdEmpleado == induccion.IdEmpleado).ToListAsync();

                if (induccionRealizada.Count > 0)
                {
                    // Si existe registro de inducción realizada por ese empleado

                    return new Response
                    {
                        IsSuccess = true
                    };

                }
                else {
                    // Si no existe registro de inducción realizada por ese empleado

                    var lista = await db.MaterialInduccion.OrderBy(x => x.Titulo).ToListAsync();

                    return new Response
                    {
                        IsSuccess = false,
                        Resultado = lista
                    };
                }

                

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };
            }
        }


        [HttpPost]
        [Route("UploadFiles")]
        public async Task<Response> Post([FromBody] DocumentoInstitucionalTransfer documentoInstitucionalTransfer)
        {
            MaterialInduccion seleccionado = new MaterialInduccion();
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

                var documentoInstitucional = new MaterialInduccion
                {
                    Titulo = documentoInstitucionalTransfer.Nombre,
                    Descripcion = documentoInstitucionalTransfer.Descripcion
                };

                var respuesta = ExisteMaterialInduccion(documentoInstitucional);
                if (!respuesta.IsSuccess)
                {
                    if (documentoInstitucionalTransfer.Url==null)
                    {
                        documentoInstitucional = await InsertarMaterialInduccion(documentoInstitucional);

                        await uploadFileService.UploadFile(documentoInstitucionalTransfer.Fichero, "MaterialInduccion", Convert.ToString(documentoInstitucional.IdMaterialInduccion), documentoInstitucionalTransfer.Extension);


                        seleccionado = db.MaterialInduccion.Find(documentoInstitucional.IdMaterialInduccion);
                        seleccionado.Url = string.Format("{0}/{1}{2}", "MaterialInduccion", Convert.ToString(documentoInstitucional.IdMaterialInduccion), documentoInstitucionalTransfer.Extension);
                        db.MaterialInduccion.Update(seleccionado);
                        db.SaveChanges();
                    }
                    else
                    {
                        documentoInstitucional = await InsertarMaterialInduccion(documentoInstitucional);
                        seleccionado = db.MaterialInduccion.Find(documentoInstitucional.IdMaterialInduccion);
                        seleccionado.Url = documentoInstitucionalTransfer.Url;
                        db.MaterialInduccion.Update(seleccionado);
                        db.SaveChanges();
                    }
                    
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                        Resultado = documentoInstitucional

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
        private async Task<MaterialInduccion> InsertarMaterialInduccion(MaterialInduccion MaterialInduccion)
        {

            db.MaterialInduccion.Add(MaterialInduccion);
            await db.SaveChangesAsync();
            return MaterialInduccion;

        }

        // POST: api/BasesDatos
        [HttpPost]
        [Route("IngresarInduccionEmpleado")]
        public async Task<Response> IngresarInduccionEmpleado([FromBody] Induccion induccion)
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

                var respuesta = ExisteInduccion(induccion);
                if (!respuesta.IsSuccess)
                {
                    db.Induccion.Add(induccion);
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



        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetMaterialInduccion([FromRoute] int id)
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

                var MaterialInduccion = await db.MaterialInduccion.SingleOrDefaultAsync(m => m.IdMaterialInduccion == id);

                if (MaterialInduccion == null)
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
                    Resultado = MaterialInduccion,
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

        // GET: api/BasesDatos/5
        [HttpPost]
        [Route("GetFile")]
        public async Task<Response> GetFile([FromBody] MaterialInduccion documentoInformacionInstitucional)
        {
            var ext = Path.GetExtension(documentoInformacionInstitucional.Url);
            try
            {

                var respuestaFile = uploadFileService.GetFile("MaterialInduccion", Convert.ToString(documentoInformacionInstitucional.IdMaterialInduccion), ext);

                var documentoIstitucional = await db.MaterialInduccion.Where(x => x.IdMaterialInduccion == documentoInformacionInstitucional.IdMaterialInduccion).FirstOrDefaultAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = documentoIstitucional.Titulo,
                    Resultado = respuestaFile,
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
        public async Task<Response> PutMaterialInduccion([FromRoute] int id, [FromBody] MaterialInduccion documentoInformacionInstitucional)
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

                var existe = ExisteMaterialInduccion(documentoInformacionInstitucional);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var documentoInformacionInstitucionalActualizar = await db.MaterialInduccion.Where(x => x.IdMaterialInduccion == id).FirstOrDefaultAsync();

                if (documentoInformacionInstitucionalActualizar != null)
                {
                    try
                    {
                        documentoInformacionInstitucionalActualizar.Titulo = documentoInformacionInstitucional.Titulo;
                        documentoInformacionInstitucionalActualizar.Descripcion = documentoInformacionInstitucional.Descripcion;
                        await db.SaveChangesAsync();

                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.Satisfactorio,
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
        public async Task<Response> DeleteMaterialInduccion([FromRoute] int id)
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
                

                var respuesta = await db.MaterialInduccion.SingleOrDefaultAsync(m => m.IdMaterialInduccion == id);

                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }

                DeleteFile(respuesta);
                
                db.MaterialInduccion.Remove(respuesta);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
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

        private Response ExisteInduccion(Induccion induccion)
        {
            var bdd = induccion.IdEmpleado;
            var Induccionrespuesta = db.Induccion.Where(p => p.IdEmpleado == bdd).FirstOrDefault();
            if (Induccionrespuesta != null)
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
                Resultado = Induccionrespuesta,
            };
        }

        private Response ExisteMaterialInduccion(MaterialInduccion materialInduccion)
        {

            var MaterialInduccionrespuesta = db.MaterialInduccion
                .Where(p =>
                    p.Titulo.ToUpper().TrimStart().TrimEnd() == materialInduccion.Titulo.ToUpper().TrimStart().TrimEnd() &&
                    p.Descripcion.ToUpper().TrimStart().TrimEnd() == materialInduccion.Descripcion.ToUpper().TrimStart().TrimEnd()
                ).FirstOrDefault();
            if (MaterialInduccionrespuesta != null)
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
                Resultado = MaterialInduccionrespuesta,
            };
        }


        [HttpPost]
        [Route("DeleteFile")]
        public async Task<Response> DeleteFile(MaterialInduccion modelo)
        {

            try
            {

                var ext = Path.GetExtension(modelo.Url);
                
                var respuestaFile = uploadFileService.DeleteFile("MaterialInduccion", Convert.ToString(modelo.IdMaterialInduccion), ext);
                
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.BorradoSatisfactorio,
                    Resultado = respuestaFile,
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
}