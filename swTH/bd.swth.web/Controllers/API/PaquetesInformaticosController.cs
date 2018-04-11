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
    [Route("api/PaquetesInformaticos")]
    public class PaquetesInformaticosController : Controller
    {
        private readonly SwTHDbContext db;

        public PaquetesInformaticosController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarPaquetesInformaticos")]
        public async Task<List<PaquetesInformaticos>> GetPaquetesInformaticos()
        {
            try
            {
                return await db.PaquetesInformaticos.OrderBy(x => x.Descripcion).ToListAsync();
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
                return new List<PaquetesInformaticos>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetPaquetesInformaticos([FromRoute] int id)
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

                var PaquetesInformaticos = await db.PaquetesInformaticos.SingleOrDefaultAsync(m => m.IdPaquetesInformaticos == id);

                if (PaquetesInformaticos == null)
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
                    Resultado = PaquetesInformaticos,
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

        private async Task Actualizar(PaquetesInformaticos PaquetesInformaticos)
        {
            var paqueteinformatico = db.PaquetesInformaticos.Find(PaquetesInformaticos.IdPaquetesInformaticos);

            paqueteinformatico.Nombre = PaquetesInformaticos.Nombre;
            paqueteinformatico.Descripcion = PaquetesInformaticos.Descripcion;
            db.PaquetesInformaticos.Update(paqueteinformatico);
            await db.SaveChangesAsync();
        }

        // PUT: api/BasesDatos/5
        [HttpPut("{id}")]
        public async Task<Response> PutPaquetesInformaticos([FromRoute] int id, [FromBody] PaquetesInformaticos PaquetesInformaticos)
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
                

                var existe = Existe(PaquetesInformaticos);
                var PaquetesInformaticosActualizar = (PaquetesInformaticos)existe.Resultado;

                if (existe.IsSuccess)
                {


                    if (PaquetesInformaticosActualizar.IdPaquetesInformaticos == PaquetesInformaticos.IdPaquetesInformaticos)
                    {
                        if (PaquetesInformaticos.Nombre == PaquetesInformaticosActualizar.Nombre &&
                        PaquetesInformaticos.Descripcion == PaquetesInformaticosActualizar.Descripcion )
                        {
                            return new Response
                            {
                                IsSuccess = true,
                            };
                        }

                        await Actualizar(PaquetesInformaticos);
                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.Satisfactorio,
                        };
                    }
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                await Actualizar(PaquetesInformaticos);
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
                    IsSuccess = true,
                    Message = Mensaje.Excepcion,
                };
            }
        }

        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarPaqueteInformatico")]
        public async Task<Response> PostPaquetesInformaticos([FromBody] PaquetesInformaticos PaquetesInformaticos)
        {
            try
            {

                var respuesta = Existe(PaquetesInformaticos);
                if (!respuesta.IsSuccess)
                {
                    db.PaquetesInformaticos.Add(PaquetesInformaticos);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio
                    };
                }

                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = ""
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

        // DELETE: api/BasesDatos/5
        [HttpDelete("{id}")]
        public async Task<Response> DeletePaquetesInformaticos([FromRoute] int id)
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

                var respuesta = await db.PaquetesInformaticos.SingleOrDefaultAsync(m => m.IdPaquetesInformaticos == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.PaquetesInformaticos.Remove(respuesta);
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

        private Response Existe(PaquetesInformaticos PaquetesInformaticos)
        {
            var bdd = PaquetesInformaticos.Nombre;
            var PaquetesInformaticosrespuesta = db.PaquetesInformaticos.Where(p => p.Nombre == bdd).FirstOrDefault();
            if (PaquetesInformaticosrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = PaquetesInformaticosrespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = PaquetesInformaticosrespuesta,
            };
        }
    }
}