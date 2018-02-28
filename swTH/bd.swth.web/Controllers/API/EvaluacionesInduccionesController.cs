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
    [Route("api/EvaluacionesInducciones")]
    public class EvaluacionesInduccionesController : Controller
    {
        private readonly SwTHDbContext db;

        public EvaluacionesInduccionesController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarEvaluacionesInducciones")]
        public async Task<List<EvaluacionInducion>> GetEvaluacionesInducciones()
        {
            try
            {
                return await db.EvaluacionInducion.OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<EvaluacionInducion>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetEvaluacionInducion([FromRoute] int id)
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

                var EvaluacionInducion = await db.EvaluacionInducion.SingleOrDefaultAsync(m => m.IdEvaluacionInduccion == id);

                if (EvaluacionInducion == null)
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
                    Resultado = EvaluacionInducion,
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


        private async Task Actualizar(EvaluacionInducion evaluacionInducion)
        {
            var escalaevatotal = db.EvaluacionInducion.Find(evaluacionInducion.IdEvaluacionInduccion);

            escalaevatotal.MinimoAprobar = evaluacionInducion.MinimoAprobar;
            escalaevatotal.MaximoPuntos = evaluacionInducion.MaximoPuntos;
            escalaevatotal.Nombre = evaluacionInducion.Nombre;
            db.EvaluacionInducion.Update(escalaevatotal);
            await db.SaveChangesAsync();
        }

        // PUT: api/BasesDatos/5
        [HttpPut("{id}")]
        public async Task<Response> PutEvaluacionInducion([FromRoute] int id, [FromBody] EvaluacionInducion EvaluacionInducion)
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

                if (EvaluacionInducion.MinimoAprobar > EvaluacionInducion.MaximoPuntos || EvaluacionInducion.MinimoAprobar == EvaluacionInducion.MaximoPuntos)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "El mínimo de puntos a aprobar no puede ser mayor o igual que el máximo de puntos a aprobar"
                    };
                }

                var existe = Existe(EvaluacionInducion);
                var EvaluacionInducionActualizar = (EvaluacionInducion)existe.Resultado;

                if (existe.IsSuccess)
                {


                    if (EvaluacionInducionActualizar.IdEvaluacionInduccion == EvaluacionInducion.IdEvaluacionInduccion)
                    {
                        if (EvaluacionInducion.MinimoAprobar == EvaluacionInducionActualizar.MinimoAprobar &&
                        EvaluacionInducion.MaximoPuntos == EvaluacionInducionActualizar.MaximoPuntos &&
                        EvaluacionInducion.Nombre == EvaluacionInducionActualizar.Nombre)
                        {
                            return new Response
                            {
                                IsSuccess = true,
                            };
                        }

                        await Actualizar(EvaluacionInducion);
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

                await Actualizar(EvaluacionInducion);
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
        [Route("InsertarEvaluacionInducion")]
        public async Task<Response> PostEvaluacionInducion([FromBody] EvaluacionInducion EvaluacionInducion)
        {
            try
            {
                if (EvaluacionInducion.MinimoAprobar > EvaluacionInducion.MaximoPuntos || EvaluacionInducion.MinimoAprobar > EvaluacionInducion.MaximoPuntos)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "El mínimo de puntos a aprobar no puede ser mayor o igual que el máximo de puntos a aprobar"
                    };
                }
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ModeloInvalido
                    };
                }

                var respuesta = Existe(EvaluacionInducion);
                if (!respuesta.IsSuccess)
                {
                    db.EvaluacionInducion.Add(EvaluacionInducion);
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
                    Message = Mensaje.Satisfactorio
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
        public async Task<Response> DeleteEvaluacionInducion([FromRoute] int id)
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

                var respuesta = await db.EvaluacionInducion.SingleOrDefaultAsync(m => m.IdEvaluacionInduccion == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.EvaluacionInducion.Remove(respuesta);
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

        private Response Existe(EvaluacionInducion EvaluacionInducion)
        {
            var bdd = EvaluacionInducion.Nombre.ToUpper().TrimEnd().TrimStart();
            //var bdd1 = EvaluacionInducion.MinimoAprobar;
            //var bdd2 = EvaluacionInducion.MinimoAprobar;
            var EvaluacionInducionrespuesta = db.EvaluacionInducion.Where(p => p.Nombre.ToUpper().TrimStart().TrimEnd() == bdd ).FirstOrDefault();
            if (EvaluacionInducionrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = EvaluacionInducionrespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = EvaluacionInducionrespuesta,
            };
        }
    }
}