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

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarMaterialesInduccion")]
        public async Task<List<MaterialInduccion>> GetMaterialesInduccion()
        {
            try
            {
                return await db.MaterialInduccion.OrderBy(x => x.Titulo).ToListAsync();
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
                return new List<MaterialInduccion>();
            }
        }


        [HttpPost]
        [Route("UploadFiles")]
        public async Task<Response> Post([FromBody] DocumentoInstitucionalTransfer documentoInstitucionalTransfer)
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

                var documentoInstitucional = new MaterialInduccion
                {
                    Titulo = documentoInstitucionalTransfer.Nombre,
                    Descripcion = documentoInstitucionalTransfer.Descripcion
                };

                var respuesta = Existe(documentoInstitucional.Titulo);
                if (!respuesta.IsSuccess)
                {

                    documentoInstitucional = await InsertarMaterialInduccion(documentoInstitucional);

                    await uploadFileService.UploadFile(documentoInstitucionalTransfer.Fichero, "MaterialInduccion", Convert.ToString(documentoInstitucional.IdMaterialInduccion), documentoInstitucionalTransfer.Extension);


                    var seleccionado = db.MaterialInduccion.Find(documentoInstitucional.IdMaterialInduccion);
                    seleccionado.Url = string.Format("{0}/{1}{2}", "MaterialInduccion", Convert.ToString(documentoInstitucional.IdMaterialInduccion), documentoInstitucionalTransfer.Extension);
                    db.MaterialInduccion.Update(seleccionado);
                    db.SaveChanges();
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

        // POST: api/BasesDatos
        private async Task<MaterialInduccion> InsertarMaterialInduccion(MaterialInduccion MaterialInduccion)
        {

            db.MaterialInduccion.Add(MaterialInduccion);
            await db.SaveChangesAsync();
            return MaterialInduccion;

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

                var existe = Existe(documentoInformacionInstitucional.Titulo);
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


                var respuestaFile = uploadFileService.DeleteFile("MaterialInduccion", Convert.ToString(id), "pdf");

                var respuesta = await db.MaterialInduccion.SingleOrDefaultAsync(m => m.IdMaterialInduccion == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
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

        private Response Existe(String nombre)
        {
            var bdd = nombre.ToUpper().TrimEnd().TrimStart();
            var MaterialInduccionrespuesta = db.MaterialInduccion.Where(p => p.Titulo.ToUpper().TrimStart().TrimEnd() == bdd).FirstOrDefault();
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
    }
}