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

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/ExamenesComplementarios")]
    public class ExamenesComplementariosController : Controller
    {

        private readonly SwTHDbContext db;

        public ExamenesComplementariosController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/ExamenesComplementarios
        [HttpGet]
        [Route("ListarExamenesComplementarios")]
        public async Task<List<ExamenComplementario>> GetExamenComplementario()
        {

            try
            {
                return await db.ExamenComplementario.Include( x => x.TipoExamenComplementario).OrderBy(x => x.IdExamenComplementario).ToListAsync();
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
                        Actualizar.IdTipoExamenComplementario= ExamenComplementario.IdTipoExamenComplementario;
                        Actualizar.IdFichaMedica= ExamenComplementario.IdFichaMedica;
                        

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

    }
}