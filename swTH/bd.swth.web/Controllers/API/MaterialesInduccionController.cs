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


        #region Métodos para mantenimiento de los materiales de inducción


        #region Consulta y descarga

        /// <summary>
        /// Obtiene la lista de materiales de inducción
        /// </summary>
        /// <returns></returns>
        // Get: api/MaterialesInduccion
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


        /// <summary>
        /// Obtiene el archivo de material inducción a partir del id y url,
        /// usado para el download
        /// </summary>
        /// <param name="documentoInformacionInstitucional"></param>
        /// <returns></returns>
        // POST: api/MaterialesInduccion
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

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }
        

        #endregion

        #region Ingreso de material de inducción

        /// <summary>
        /// Permite registrar el material de inducción (datos y archivos)
        /// </summary>
        /// <param name="documentoInstitucionalTransfer"></param>
        /// <returns></returns>
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
                    Titulo = documentoInstitucionalTransfer.Nombre.ToString().ToUpper(),
                    Descripcion = documentoInstitucionalTransfer.Descripcion.ToString().ToUpper()
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
                        Message = Mensaje.GuardadoSatisfactorio,
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
                
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }


        }

        
        /// <summary>
        /// Registra los datos del material de inducción, (no guarda archivos)
        /// </summary>
        /// <param name="MaterialInduccion"></param>
        /// <returns></returns>
        // POST: api/MaterialesInduccion
        private async Task<MaterialInduccion> InsertarMaterialInduccion(MaterialInduccion MaterialInduccion)
        {
            MaterialInduccion.Descripcion.ToString().ToUpper();
            MaterialInduccion.Titulo.ToString().ToUpper();

            db.MaterialInduccion.Add(MaterialInduccion);
            await db.SaveChangesAsync();
            return MaterialInduccion;

        }


        /// <summary>
        ///  Verifica si existe un material de inducción
        /// </summary>
        /// <param name="materialInduccion"></param>
        /// <returns></returns>
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

        #endregion

        #region Edición
        // GET: api/MaterialesInduccion
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
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }

        /// <summary>
        /// Edita los datos del material de inducción (no modifica archivo)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="documentoInformacionInstitucional"></param>
        /// <returns></returns>
        // PUT: api/MaterialesInduccion
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

                var existe = await db.MaterialInduccion
                .Where(p =>
                   p.Titulo.ToUpper().TrimStart().TrimEnd() ==
                   documentoInformacionInstitucional.Titulo.ToUpper().TrimStart().TrimEnd()
                   && p.Descripcion.ToUpper().TrimStart().TrimEnd() ==
                   documentoInformacionInstitucional.Descripcion.ToUpper().TrimStart().TrimEnd()
               ).FirstOrDefaultAsync();

                if (existe != null && existe.IdMaterialInduccion != documentoInformacionInstitucional.IdMaterialInduccion)
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
                        documentoInformacionInstitucionalActualizar.Titulo = documentoInformacionInstitucional.Titulo
                            .ToString().ToUpper();
                        documentoInformacionInstitucionalActualizar.Descripcion = documentoInformacionInstitucional.Descripcion
                            .ToString().ToUpper();

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

        #endregion
        
        #region Eliminar

        /// <summary>
        /// Elimina el registro y el archivo de material Inducción
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/MaterialesInduccion
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

                await DeleteFile(respuesta);

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

        /// <summary>
        /// Elimina un archivo de materialInducción, no elimina registro de la base de datos
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
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

        #endregion
        
        #endregion


        #region Métodos para que el empleado realize su proceso de inducción

        /// <summary>
        /// Devuelve la lista de materiales de inducción si el empleado no ha realizado el proceso de inducción,
        /// caso contrario devuelve un valor TRUE para que acceda luego al reporte
        /// </summary>
        /// <param name="induccion"></param>
        /// <returns></returns>
        // Post: api/MaterialesInduccion
        [HttpPost]
        [Route("ListarMaterialesInduccion")]
        public async Task<Response> ListarMaterialesInduccion([FromBody] Induccion induccion)
        {
            try
            {
                var induccionRealizada = await db.Induccion.Where(y => y.IdEmpleado == induccion.IdEmpleado).ToListAsync();

                if (induccionRealizada.Count > 0)
                {
                    // Si existe registro de inducción realizada por ese empleado

                    return new Response
                    {
                        IsSuccess = true
                    };

                }
                else
                {
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


        /// <summary>
        /// Registra que el empleado ha realizado la inducción
        /// </summary>
        /// <param name="induccion"></param>
        /// <returns></returns>
        // POST: api/MaterialesInduccion
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

        #endregion

        
    }
}