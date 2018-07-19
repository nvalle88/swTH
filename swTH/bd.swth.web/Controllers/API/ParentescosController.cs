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
using bd.log.guardar.Enumeradores;
using bd.swth.entidades.Utils;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Parentescos")]
    public class ParentescosController : Controller
    {
        private readonly SwTHDbContext db;

        public ParentescosController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/Parentescoes
        [HttpGet]
        [Route("ListarParentescos")]
        public async Task<List<Parentesco>> GetParentesco()
        {
            try
            {
                return await db.Parentesco.OrderBy(x => x.Nombre).ToListAsync();
            }
            catch (Exception ex)
            {
                
                return new List<Parentesco>();
            }
        }

        // GET: api/Parentescoes/5
        [HttpGet("{id}")]
        public async Task<Response> GetParentesco([FromRoute] int id)
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

                var Parentesco = await db.Parentesco.SingleOrDefaultAsync(m => m.IdParentesco == id);

                if (Parentesco == null)
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
                    Resultado = Parentesco,
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

        // PUT: api/Parentescoes/5
        [HttpPut("{id}")]
        public async Task<Response> PutParentesco([FromRoute] int id, [FromBody] Parentesco Parentesco)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message =Mensaje.ModeloInvalido
                    };
                }

                var existe = Existe(Parentesco);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var ParentescoActualizar = await db.Parentesco.Where(x => x.IdParentesco == id).FirstOrDefaultAsync();

                if (ParentescoActualizar != null)
                {
                    try
                    {

                        ParentescoActualizar.Nombre = Parentesco.Nombre.ToString().ToUpper();
                        await db.SaveChangesAsync();

                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.GuardadoSatisfactorio,
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

        // POST: api/Parentescoes
        [HttpPost]
        [Route("InsertarParentescos")]
        public async Task<Response> PostParentesco([FromBody] Parentesco Parentesco)
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

                var respuesta = Existe(Parentesco);
                if (!respuesta.IsSuccess)
                {
                    // Convertir a mayúsculas
                    Parentesco.Nombre = Parentesco.Nombre.ToString().ToUpper();

                    db.Parentesco.Add(Parentesco);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.GuardadoSatisfactorio
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

        // DELETE: api/Parentescoes/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteParentesco([FromRoute] int id)
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

                var respuesta = await db.Parentesco.SingleOrDefaultAsync(m => m.IdParentesco == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.Parentesco.Remove(respuesta);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.BorradoSatisfactorio,
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

        private Response Existe(Parentesco Parentesco)
        {
            var bdd = Parentesco.Nombre;
            var Parentescorespuesta = db.Parentesco
                .Where(p =>
                    p.Nombre.ToString().ToUpper() == bdd.ToString().ToUpper()
                )
                .FirstOrDefault();

            if (Parentescorespuesta != null)
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
                Resultado = Parentescorespuesta,
            };
        }

    }
}