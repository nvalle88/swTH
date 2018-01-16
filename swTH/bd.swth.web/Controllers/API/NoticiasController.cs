using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.log.guardar.Enumeradores;
using bd.swth.entidades.Enumeradores;
using bd.swth.entidades.Utils;
using System.IO;
using bd.swth.entidades.ObjectTransfer;
using bd.swth.servicios.Interfaces;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Noticias")]
    public class NoticiasController : Controller
    {
        private readonly IUploadFileService uploadFileService;
        private readonly SwTHDbContext db;
        
        public NoticiasController(SwTHDbContext db, IUploadFileService uploadFileService)
        {
            this.uploadFileService = uploadFileService;
            this.db = db;
        }

        // GET: api/Noticias
        [HttpGet]
        [Route("ListarNoticias")]
        public async Task<List<Noticia>> GetNoticias()
        {
            try
            {
                return await db.Noticia.OrderByDescending(x => x.Fecha).ToListAsync();
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
                return new List<Noticia>();
            }
        }
        
        [HttpPost]
        [Route("UploadFiles")]
        public async Task<Response> Post([FromBody] NoticiaTransfer noticiaTransfer)
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

                var noticia = new Noticia
                {
                    Titulo = noticiaTransfer.Titulo,
                    Fecha = noticiaTransfer.Fecha,
                    Descripcion = noticiaTransfer.Descripcion,
                  
                };
                
                var respuesta = Existe(noticia);
                if (!respuesta.IsSuccess)
                {

                    Noticia objetoNoticia = await InsertarNoticia(noticia);

                    await uploadFileService.UploadFile(noticiaTransfer.Fichero, "Noticias", Convert.ToString(objetoNoticia.IdNoticia), "jpg");
                    
                    var seleccionado = db.Noticia.Find(objetoNoticia.IdNoticia);
                    seleccionado.Foto = string.Format("{0}/{1}.{2}", "Noticias", Convert.ToString(objetoNoticia.IdNoticia), "jpg");
                    db.Noticia.Update(seleccionado);
                    db.SaveChanges();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                        Resultado= objetoNoticia
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

        [HttpPost]
        [Route("UploadFilesActualizar")]
        public async Task<Response> UploadFilesActualizar([FromBody] NoticiaTransfer noticiaTransfer)
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

                var noticia = new Noticia
                {
                    IdNoticia = noticiaTransfer.Id,
                    Titulo = noticiaTransfer.Titulo,
                    Fecha = noticiaTransfer.Fecha,
                    Descripcion = noticiaTransfer.Descripcion,
                };

               
                    noticia = await ActualizarNoticia(noticia);

                    await uploadFileService.UploadFile(noticiaTransfer.Fichero, "Noticias", Convert.ToString(noticia.IdNoticia), "jpg");

                    var seleccionado = db.Noticia.Find(noticia.IdNoticia);
                    seleccionado.Foto = string.Format("{0}/{1}.{2}", "Noticias", Convert.ToString(noticia.IdNoticia), "jpg");
                    db.Noticia.Update(seleccionado);
                    db.SaveChanges();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                        Resultado=noticia
                    };
                
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

        // POST: api/Noticia
        private async Task<Noticia> InsertarNoticia(Noticia Noticia)
        {
            Noticia noticia = new Noticia();
            try
            {
                db.Noticia.Add(Noticia);
                await db.SaveChangesAsync();
                return Noticia;
            }
            catch (Exception ex)
            {
                noticia = new Noticia();
            }
            return Noticia;
        }



        // GET: api/Noticia/5
        [HttpGet("{id}")]
        public async Task<Response> GetNoticia([FromRoute] int id)
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

                var Noticia = await db.Noticia.SingleOrDefaultAsync(m => m.IdNoticia == id);

                if (Noticia == null)
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
                    Resultado = Noticia,
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

        // GET: api/Noticia/5
        [HttpPost]
        [Route("GetFile")]
        public async Task<Response> GetFile([FromBody] Noticia Noticia)
        {

            try
            {
                var respuestaFile = uploadFileService.GetFile("Noticias", Convert.ToString(Noticia.IdNoticia), "jpg");

                var documentoIstitucional = await db.Noticia.Where(x => x.IdNoticia == Noticia.IdNoticia).FirstOrDefaultAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Noticia.Titulo,
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


        
        public async Task<Noticia> ActualizarNoticia(Noticia Noticia)
        {
            Noticia oNoticia = new Noticia();
            try
            {
                var NoticiaActualizar = await db.Noticia.Where(x => x.IdNoticia == Noticia.IdNoticia).FirstOrDefaultAsync();

                if (NoticiaActualizar != null)
                {
                    try
                    {
                        NoticiaActualizar.Titulo = Noticia.Titulo;
                        NoticiaActualizar.Fecha = Noticia.Fecha;
                        NoticiaActualizar.Descripcion = Noticia.Descripcion;
                        db.Noticia.Update(NoticiaActualizar);
                        await db.SaveChangesAsync();

                        return NoticiaActualizar;

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
                        
                    }
                }

                return oNoticia;
            }
            catch (Exception ex)
            {
                
                return oNoticia;
            }
        }

        // DELETE: api/Noticia/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteNoticia([FromRoute] int id)
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


                var respuestaFile = uploadFileService.DeleteFile("Noticias", Convert.ToString(id), "jpg");

                var respuesta = await db.Noticia.SingleOrDefaultAsync(m => m.IdNoticia == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.Noticia.Remove(respuesta);
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

        private Response Existe(Noticia noticia)
        {
            string oTitulo = noticia.Titulo.ToUpper().TrimEnd().TrimStart();
            var Noticiarespuesta = db.Noticia.Where(p => p.Titulo.ToUpper().TrimStart().TrimEnd().Equals(oTitulo)).FirstOrDefault();

            if (Noticiarespuesta != null)
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
                Resultado = Noticiarespuesta,
            };
        }

    }
}