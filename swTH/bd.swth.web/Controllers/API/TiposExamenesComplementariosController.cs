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
    [Route("api/TiposExamenesComplementarios")]
    public class TiposExamenesComplementariosController : Controller
    {
        private readonly SwTHDbContext db;

        public TiposExamenesComplementariosController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/TiposExamenesComplementarios
        [HttpGet]
        [Route("ListarTiposExamenesComplementarios")]
        public async Task<List<TipoExamenComplementario>> GetTipoExamenComplementario()
        {

            try
            {
                return await db.TipoExamenComplementario.OrderBy(x => x.IdTipoExamenComplementario).ToListAsync();
            }
            catch (Exception ex)
            {
                
                return new List<TipoExamenComplementario>();
            }
        }



        // GET: api/TiposExamenesComplementarios/5
        [HttpGet("{id}")]
        public async Task<Response> GetTipoExamenComplementario([FromRoute] int id)
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

                var TipoExamenComplementario = await db.TipoExamenComplementario.SingleOrDefaultAsync(m => m.IdTipoExamenComplementario == id);


                if (TipoExamenComplementario == null)
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
                    Resultado = TipoExamenComplementario,
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

        // PUT: api/TiposExamenesComplementarios/5
        [HttpPut("{id}")]
        public async Task<Response> PutTipoExamenComplementario([FromRoute] int id, [FromBody] TipoExamenComplementario TipoExamenComplementario)
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

                var existe = Existe(TipoExamenComplementario);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var Actualizar = await db.TipoExamenComplementario.Where(x => x.IdTipoExamenComplementario == id).FirstOrDefaultAsync();
                if (Actualizar != null)
                {
                    try
                    {

                        Actualizar.Nombre = TipoExamenComplementario.Nombre;
                        

                        db.TipoExamenComplementario.Update(Actualizar);

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

        // POST: api/TiposExamenesComplementarios
        [HttpPost]
        [Route("InsertarTiposExamenesComplementarios")]
        public async Task<Response> PostTipoExamenComplementario([FromBody] TipoExamenComplementario TipoExamenComplementario)
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

                var respuesta = Existe(TipoExamenComplementario);
                if (!respuesta.IsSuccess)
                {
                    db.TipoExamenComplementario.Add(TipoExamenComplementario);
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

        // DELETE: api/TiposExamenesComplementarios/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteTipoExamenComplementario([FromRoute] int id)
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

                var respuesta = await db.TipoExamenComplementario.SingleOrDefaultAsync(m => m.IdTipoExamenComplementario == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.TipoExamenComplementario.Remove(respuesta);
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
                    Message = Mensaje.BorradoNoSatisfactorio,
                };
            }

        }




        private Response Existe(TipoExamenComplementario TipoExamenComplementario)
        {
            var tcn = TipoExamenComplementario.Nombre.ToUpper().TrimEnd().TrimStart();


            var Respuesta = db.TipoExamenComplementario.Where(

                    p => p.Nombre.ToUpper().TrimEnd().TrimStart() == tcn

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