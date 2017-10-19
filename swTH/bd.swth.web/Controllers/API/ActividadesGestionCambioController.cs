using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.Enumeradores;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.log.guardar.Enumeradores;
using bd.swth.entidades.Utils;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/ActividadesGestionCambio")]
    public class ActividadesGestionCambioController : Controller
    {
        private readonly SwTHDbContext db;

        public ActividadesGestionCambioController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/ActividadesGestionCambio
        [HttpGet]
        [Route("ListarActividadesGestionCambio")]
        public async Task<List<ActividadesGestionCambio>> GetActividadesGestionCambio()
        {
            try
            {
                return await db.ActividadesGestionCambio.OrderBy(x => x.FechaInicio).ToListAsync();
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
                return new List<ActividadesGestionCambio>();
            }
        }
        //int IdPlanGestionCambio - ListarActividadesGestionCambioconIdPlan
        // GET: api/ActividadesGestionCambio
        [HttpGet]
        [Route("ListarActividadesGestionCambioconIdPlan")]
        public async Task<List<ActividadesGestionCambio>> ListarActividadesGestionCambioconIdPlan(int IdPlanGestionCambio)
        {
            try
            {
                return await db.ActividadesGestionCambio.Where(m => m.IdPlanGestionCambio == IdPlanGestionCambio).ToListAsync();
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
                return new List<ActividadesGestionCambio>();
            }
        }

        // GET: api/ActividadesGestionCambio/5
        [HttpGet("{id}")]
        public async Task<Response> GetActividadesGestionCambio([FromRoute] int id)
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

                var ActividadesGestionCambio = await db.ActividadesGestionCambio.SingleOrDefaultAsync(m => m.IdActividadesGestionCambio == id);

                if (ActividadesGestionCambio == null)
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
                    Resultado = ActividadesGestionCambio,
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

        // PUT: api/ActividadesGestionCambio/5
        [HttpPut("{id}")]
        public async Task<Response> PutActividadesGestionCambio([FromRoute] int id, [FromBody] ActividadesGestionCambio actividadesGestionCambio)
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

                if (actividadesGestionCambio.FechaInicio > actividadesGestionCambio.FechaFin)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "la fecha de inicio no puede ser mayor que la fecha fin"
                    };
                }

                var existe = Existe(actividadesGestionCambio);
                var ActividadesGestionCambioActualizar = (ActividadesGestionCambio)existe.Resultado;
                if (existe.IsSuccess)
                {
                    if (ActividadesGestionCambioActualizar.IdActividadesGestionCambio == actividadesGestionCambio.IdActividadesGestionCambio)
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
                var ActividadesGestionCambio = db.ActividadesGestionCambio.Find(actividadesGestionCambio.IdActividadesGestionCambio);

                ActividadesGestionCambio.FechaInicio = ActividadesGestionCambio.FechaInicio;
                ActividadesGestionCambio.FechaFin = ActividadesGestionCambio.FechaFin;
                ActividadesGestionCambio.Indicador = ActividadesGestionCambio.Indicador;
                ActividadesGestionCambio.Porciento = ActividadesGestionCambio.Porciento;
                ActividadesGestionCambio.Descripcion = ActividadesGestionCambio.Descripcion;

                db.ActividadesGestionCambio.Update(ActividadesGestionCambio);
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

        // POST: api/ActividadesGestionCambio
        [HttpPost]
        [Route("InsertarActividadesGestionCambio")]
        public async Task<Response> PostActividadesGestionCambio([FromBody] ActividadesGestionCambio ActividadesGestionCambio)
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

                if (ActividadesGestionCambio.FechaInicio > ActividadesGestionCambio.FechaFin)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "la fecha de inicio no puede ser mayor que la fecha fin"
                    };
                }

                var respuesta = Existe(ActividadesGestionCambio);
                if (!respuesta.IsSuccess)
                {
                    db.ActividadesGestionCambio.Add(ActividadesGestionCambio);
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

        // DELETE: api/ActividadesGestionCambio/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteActividadesGestionCambio([FromRoute] int id)
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

                var respuesta = await db.ActividadesGestionCambio.SingleOrDefaultAsync(m => m.IdActividadesGestionCambio == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.ActividadesGestionCambio.Remove(respuesta);
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

        private Response Existe(ActividadesGestionCambio ActividadesGestionCambio)
        {
            var bdd = ActividadesGestionCambio.FechaInicio;
            var ActividadesGestionCambiorespuesta = db.ActividadesGestionCambio.Where(p => p.FechaInicio == bdd).FirstOrDefault();
            if (ActividadesGestionCambiorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = ActividadesGestionCambiorespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = ActividadesGestionCambiorespuesta,
            };
        }
    }
}