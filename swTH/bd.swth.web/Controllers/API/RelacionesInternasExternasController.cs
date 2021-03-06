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
    [Route("api/RelacionesInternasExternas")]
    public class RelacionesInternasExternasController : Controller
    {
        private readonly SwTHDbContext db;

        public RelacionesInternasExternasController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarRelacionesInternasExternas")]
        public async Task<List<RelacionesInternasExternas>> GetRelacionesInternasExternas()
        {
            try
            {
                return await db.RelacionesInternasExternas.OrderBy(x => x.Descripcion).ToListAsync();
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
                return new List<RelacionesInternasExternas>();
            }
        }


        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetRelacionesInternasExternas([FromRoute] int id)
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

                var RelacionesInternasExternas = await db.RelacionesInternasExternas.SingleOrDefaultAsync(m => m.IdRelacionesInternasExternas == id);

                if (RelacionesInternasExternas == null)
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
                    Resultado = RelacionesInternasExternas,
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
        public async Task<Response> PutRelacionesInternasExternas([FromRoute] int id, [FromBody] RelacionesInternasExternas RelacionesInternasExternas)
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

                var existe = Existe(RelacionesInternasExternas);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var RelacionesInternasExternasActualizar = await db.RelacionesInternasExternas.Where(x => x.IdRelacionesInternasExternas == id).FirstOrDefaultAsync();

                if (RelacionesInternasExternasActualizar != null)
                {
                    try
                    {

                        RelacionesInternasExternasActualizar.Nombre = RelacionesInternasExternas.Nombre.ToString().ToUpper();
                        RelacionesInternasExternasActualizar.Descripcion = RelacionesInternasExternas.Descripcion.ToString().ToUpper();

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

        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarRelacionesInternasExternas")]
        public async Task<Response> PostRelacionesInternasExternas([FromBody] RelacionesInternasExternas RelacionesInternasExternas)
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

                var respuesta = Existe(RelacionesInternasExternas);
                if (!respuesta.IsSuccess)
                {
                    RelacionesInternasExternas.Nombre = RelacionesInternasExternas.Nombre.ToString().ToUpper();
                    RelacionesInternasExternas.Descripcion = RelacionesInternasExternas.Descripcion.ToString().ToUpper();

                    db.RelacionesInternasExternas.Add(RelacionesInternasExternas);
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

        // DELETE: api/BasesDatos/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteRelacionesInternasExternas([FromRoute] int id)
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

                var respuesta = await db.RelacionesInternasExternas.SingleOrDefaultAsync(m => m.IdRelacionesInternasExternas == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.RelacionesInternasExternas.Remove(respuesta);
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

        private Response Existe(RelacionesInternasExternas RelacionesInternasExternas)
        {
            var bdd = RelacionesInternasExternas.Descripcion;
            var RelacionesInternasExternasrespuesta = db.RelacionesInternasExternas.Where(p => p.Descripcion == bdd).FirstOrDefault();
            if (RelacionesInternasExternasrespuesta != null)
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
                Resultado = RelacionesInternasExternasrespuesta,
            };
        }

    }
}
