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
using bd.webappth.entidades.ObjectTransfer;
using bd.swth.servicios.Interfaces;
using bd.swth.entidades.ViewModels;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/ExamenesComplementarios")]
    public class ExamenesComplementariosController : Controller
    {
        private readonly IUploadFileService uploadFileService;
        private readonly SwTHDbContext db;

        public ExamenesComplementariosController(SwTHDbContext db, IUploadFileService uploadFileService)
        {
            this.uploadFileService = uploadFileService;
            this.db = db;
        }

        // GET: api/ExamenesComplementarios
        [HttpGet]
        [Route("ListarExamenesComplementarios")]
        public async Task<List<ExamenComplementario>> GetExamenComplementario()
        {

            try
            {
                return await db.ExamenComplementario.Include(x => x.TipoExamenComplementario).OrderBy(x => x.IdExamenComplementario).ToListAsync();
            }
            catch (Exception ex)
            {

                return new List<ExamenComplementario>();
            }
        }



        // GET: api/ExamenesComplementarios/5
        [HttpGet("{id}")]
        public async Task<Response> GetExamenComplementario([FromRoute] int id)
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

                var ExamenComplementario = await db.ExamenComplementario.SingleOrDefaultAsync(m => m.IdExamenComplementario == id);


                if (ExamenComplementario == null)
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
                    Resultado = ExamenComplementario,
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

        // PUT: api/ExamenesComplementarios/5
        [HttpPut("{id}")]
        public async Task<Response> PutExamenComplementario([FromRoute] int id, [FromBody] ExamenComplementario ExamenComplementario)
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

                var existe = Existe(ExamenComplementario);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var Actualizar = await db.ExamenComplementario.Where(x => x.IdExamenComplementario == id).FirstOrDefaultAsync();
                if (Actualizar != null)
                {
                    try
                    {

                        Actualizar.Fecha = ExamenComplementario.Fecha;
                        Actualizar.Resultado = ExamenComplementario.Resultado;
                        Actualizar.IdTipoExamenComplementario = ExamenComplementario.IdTipoExamenComplementario;
                        Actualizar.IdFichaMedica = ExamenComplementario.IdFichaMedica;


                        db.ExamenComplementario.Update(Actualizar);

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
                    Message = Mensaje.ExisteRegistro,
                };


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

     

        // POST: api/ExamenesComplementarios
        [HttpPost]
        [Route("InsertarExamenesComplementarios")]
        public async Task<Response> PostExamenComplementario([FromBody] ExamenComplementario ExamenComplementario)
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

                var respuesta = Existe(ExamenComplementario);
                if (!respuesta.IsSuccess)
                {
                    db.ExamenComplementario.Add(ExamenComplementario);
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
                
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }


        // POST: api/ExamenesComplementarios
        [HttpPost]
        [Route("UploadFiles")]
        public async Task<Response> Post([FromBody] ExamenComplementarioTransfer examenComplementarioTransfer)
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

                var examenComplementario = new ExamenComplementario
                {
                    IdExamenComplementario = examenComplementarioTransfer.IdExamenComplementario,
                    Fecha = examenComplementarioTransfer.Fecha,
                    Resultado = examenComplementarioTransfer.Resultado,
                    IdTipoExamenComplementario = examenComplementarioTransfer.IdTipoExamenComplementario,
                    IdFichaMedica = examenComplementarioTransfer.IdFichaMedica,
                    
                };

                var respuesta = Existe(examenComplementario);
                if (!respuesta.IsSuccess)
                {
                    db.ExamenComplementario.Add(examenComplementario);
                    await db.SaveChangesAsync();

                    var id = examenComplementario.IdExamenComplementario;

                    await uploadFileService.UploadFile(examenComplementarioTransfer.Fichero, "ExamenesComplementariosDocumentos", Convert.ToString(id), ".pdf");


                    var seleccionado = db.ExamenComplementario.Find(examenComplementario.IdExamenComplementario);
                    seleccionado.Url = string.Format("{0}/{1}.{2}", "ExamenesComplementariosDocumentos", Convert.ToString(id), "pdf");
                    db.ExamenComplementario.Update(seleccionado);
                    db.SaveChanges();
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
                
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }


        }


        // POST: api/ExamenesComplementarios
        [HttpPost]
        [Route("UpdateFiles")]
        public async Task<Response> PostUpdateFiles([FromBody] ExamenComplementarioTransfer examenComplementarioTransfer)
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


                var Actualizar = await db.ExamenComplementario.Where(x => x.IdExamenComplementario == examenComplementarioTransfer.IdExamenComplementario).FirstOrDefaultAsync();

                Actualizar.Fecha = examenComplementarioTransfer.Fecha;
                Actualizar.Resultado = examenComplementarioTransfer.Resultado;
                Actualizar.IdTipoExamenComplementario = examenComplementarioTransfer.IdTipoExamenComplementario;
                Actualizar.IdFichaMedica = examenComplementarioTransfer.IdFichaMedica;

                /*borrar fichero*/

                try
                {
                    ExamenComplementario excm = new ExamenComplementario();

                    excm.IdExamenComplementario = examenComplementarioTransfer.IdExamenComplementario;

                    await DeleteFile(excm);
                }
                catch (Exception ex0) { }

                /* Crear nuevo fichero */

                var id = examenComplementarioTransfer.IdExamenComplementario;

                await uploadFileService.UploadFile(examenComplementarioTransfer.Fichero, "ExamenesComplementariosDocumentos", Convert.ToString(id), ".pdf");


                /* Edito la nueva Url */

                //var seleccionado = db.ExamenComplementario.Find(examenComplementario.IdExamenComplementario);
                Actualizar.Url = string.Format("{0}/{1}.{2}", "ExamenesComplementariosDocumentos", Convert.ToString(id), "pdf");

                db.ExamenComplementario.Update(Actualizar);
                db.SaveChanges();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio
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
        


        // Post: api/ExamenesComplementarios
        [HttpPost]
        [Route("ListarExamenesComplementariosPorFicha")]
        public async Task<Response> GetExamenesComplementariosPorFicha([FromBody] int idFicha)
        {

            Response response = new entidades.Utils.Response();

            try
            {
                var lista = await db.ExamenComplementario.Include(x => x.TipoExamenComplementario).Where(x => x.IdFichaMedica == idFicha).OrderBy(x => x.IdExamenComplementario ).ToListAsync();


                return new Response { IsSuccess = true, Resultado = lista };

            }
            catch (Exception ex)
            {

                return new Response { IsSuccess = false, Message = Mensaje.Excepcion };
            }


        }




        // DELETE: api/ExamenesComplementarios/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteExamenComplementario([FromRoute] int id)
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

                var respuesta = await db.ExamenComplementario.SingleOrDefaultAsync(m => m.IdExamenComplementario == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.ExamenComplementario.Remove(respuesta);
                await db.SaveChangesAsync();


                /*borrar fichero*/

                try
                {
                    ExamenComplementario excm = new ExamenComplementario();

                    excm.IdExamenComplementario = id;

                    await DeleteFile(excm);
                }
                catch (Exception ex0) { }

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
                    Message = Mensaje.BorradoNoSatisfactorio,
                };
            }

        }




        private Response Existe(ExamenComplementario ExamenComplementario)
        {
            
            var ecf = ExamenComplementario.Fecha;
            var ecr = ExamenComplementario.Resultado.ToUpper().TrimEnd().TrimStart();
            var ecit = ExamenComplementario.IdTipoExamenComplementario;
            var ecif = ExamenComplementario.IdFichaMedica;


            var Respuesta = db.ExamenComplementario.Where(

                    p => p.Fecha == ecf
                    && p.Resultado.ToUpper().TrimEnd().TrimStart() == ecr
                    && p.IdTipoExamenComplementario == ecit
                    && p.IdFichaMedica == ecif

                ).FirstOrDefault();

            if (Respuesta != null)
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
                Resultado = Respuesta,
            };
        }



        // DELETE: api/ExamenesComplementarios/5
        [HttpPost]
        [Route("DeleteFile")]
        public async Task<Response> DeleteFile([FromBody] ExamenComplementario examenComplementario)
        {

            try
            {
                var respuestaFile = uploadFileService.DeleteFile("ExamenesComplementariosDocumentos", Convert.ToString(examenComplementario.IdExamenComplementario), ".pdf");

                var dato = await db.ExamenComplementario.Where(x => x.IdExamenComplementario == examenComplementario.IdExamenComplementario).FirstOrDefaultAsync();

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



        // GET: api/ExamenesComplementarios/5
        [HttpPost]
        [Route("GetFile")]
        public async Task<Response> GetFile([FromBody] ExamenComplementario examenComplementario)
        {

            try
            {
                var respuestaFile = uploadFileService.GetFile("ExamenesComplementariosDocumentos", Convert.ToString(examenComplementario.IdExamenComplementario), ".pdf");

                var dato = await db.ExamenComplementario.Where(x => x.IdExamenComplementario == examenComplementario.IdExamenComplementario).FirstOrDefaultAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = "Exámen complementario #" + dato.IdExamenComplementario + ", archivo adjunto",
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


        // POST: api/ExamenesComplementarios
        [HttpPost]
        [Route("CrearFicheroOdontologicoPdf")]
        public async Task<Response> CrearFicheroOdontologicoPdf([FromBody] FichaOdontologicaViewModel fichaOdontologicaViewModel)
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
                

                /* Crear nuevo fichero */

                var id = fichaOdontologicaViewModel.IdPersona;

                await uploadFileService.UploadFile(fichaOdontologicaViewModel.Fichero, "FichasOdontologicasDocumentos", Convert.ToString(id), ".pdf");
                

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio
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