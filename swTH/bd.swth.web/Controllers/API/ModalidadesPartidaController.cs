using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.log.guardar.ObjectTranfer;
using bd.log.guardar.Servicios;
using bd.swth.entidades.Enumeradores;
using bd.log.guardar.Enumeradores;
using bd.swth.entidades.Utils;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/ModalidadesPartida")]
    public class ModalidadesPartidaController : Controller
    {
        private readonly SwTHDbContext db;

        public ModalidadesPartidaController(SwTHDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        [Route("ListarModalidadesPartidaPorRelacionLaboral")]
        public async Task<List<ModalidadPartida>> ListarModalidadesPartidaPorRelacionLaboral([FromBody] RelacionLaboral relacionLaboral)
        {
            try
            {
                //return await db.ModalidadPartida.Where(x => x.IdRelacionLaboral==relacionLaboral.IdRelacionLaboral).OrderBy(x => x.Nombre).ToListAsync();
                return await db.ModalidadPartida.OrderBy(x => x.Nombre).ToListAsync();

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
                return new List<ModalidadPartida>();
            }
        }


        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarModalidadesPartida")]
        public async Task<List<ModalidadPartida>> GetCapacitacionesTemarios()
        {
            try
            {
                //return await db.ModalidadPartida.Include(x => x.RelacionLaboral).OrderBy(x => x.Nombre).ToListAsync();
                return await db.ModalidadPartida.OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<ModalidadPartida>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetModalidadPartida([FromRoute] int id)
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

                var ModalidadPartida = await db.ModalidadPartida.SingleOrDefaultAsync(m => m.IdModalidadPartida == id);

                if (ModalidadPartida == null)
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
                    Resultado = ModalidadPartida,
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
        public async Task<Response> PutModalidadPartida([FromRoute] int id, [FromBody] ModalidadPartida ModalidadPartida)
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

                var existe = Existe(ModalidadPartida);
                var ModalidadPartidaActualizar = (ModalidadPartida)existe.Resultado;
                if (existe.IsSuccess)
                {
                    if (ModalidadPartidaActualizar.IdModalidadPartida == ModalidadPartida.IdModalidadPartida)
                    {
                        return new Response
                        {
                            IsSuccess = true,
                        };
                    }
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }
                var modalidadpartida = db.ModalidadPartida.Find(ModalidadPartida.IdModalidadPartida);

                //modalidadpartida.IdRelacionLaboral = ModalidadPartida.IdRelacionLaboral;
                modalidadpartida.Nombre = ModalidadPartida.Nombre;
                db.ModalidadPartida.Update(modalidadpartida);
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
                    IsSuccess = true,
                    Message = Mensaje.Excepcion,
                };
            }

        }

        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarModalidadPartida")]
        public async Task<Response> PostModalidadPartida([FromBody] ModalidadPartida ModalidadPartida)
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

                var respuesta = Existe(ModalidadPartida);
                if (!respuesta.IsSuccess)
                {
                    db.ModalidadPartida.Add(ModalidadPartida);
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
                    Message = Mensaje.ExisteRegistro,
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

        // DELETE: api/BasesDatos/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteModalidadPartida([FromRoute] int id)
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

                var respuesta = await db.ModalidadPartida.SingleOrDefaultAsync(m => m.IdModalidadPartida == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.ModalidadPartida.Remove(respuesta);
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

        private Response Existe(ModalidadPartida ModalidadPartida)
        {
            var bdd = ModalidadPartida.Nombre.ToUpper().TrimEnd().TrimStart();
            //var ModalidadPartidarespuesta = db.ModalidadPartida.Where(p => p.Nombre.ToUpper().TrimStart().TrimEnd() == bdd && p.IdRelacionLaboral == ModalidadPartida.IdRelacionLaboral).FirstOrDefault();
            var ModalidadPartidarespuesta = db.ModalidadPartida.Where(p => p.Nombre.ToUpper().TrimStart().TrimEnd() == bdd).FirstOrDefault();
            if (ModalidadPartidarespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = ModalidadPartidarespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = ModalidadPartidarespuesta,
            };
        }

    }
}