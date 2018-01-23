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
    [Route("api/ConfiguracionFeriados")]
    public class ConfiguracionFeriadosController : Controller
    {
        private readonly SwTHDbContext db;

        public ConfiguracionFeriadosController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarConfiguracionesFeriados")]
        public async Task<List<ConfiguracionFeriados>> GetConfiguracionFeriados()
        {
            try
            {
                return await db.ConfiguracionFeriados.OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<ConfiguracionFeriados>();
            }
        }


        [HttpGet("{id}")]
        public async Task<Response> GetConfiguracionFeriados([FromRoute] int id)
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

                var ConfiguracionFeriados = await db.ConfiguracionFeriados.SingleOrDefaultAsync(m => m.IdConfiguracionFeriado == id);

                if (ConfiguracionFeriados == null)
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
                    Resultado = ConfiguracionFeriados,
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
        public async Task<Response> PutConfiguracionFeriados([FromRoute] int id, [FromBody] ConfiguracionFeriados configuracionFeriados)
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

                if (configuracionFeriados.FechaDesde > configuracionFeriados.FechaHasta )
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha desde no debe ser mayor que la fecha hasta"
                    };
                }

                var existe = Existe(configuracionFeriados);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var configuracionFeriadosActualizar = await db.ConfiguracionFeriados.Where(x => x.IdConfiguracionFeriado == id).FirstOrDefaultAsync();

                if (configuracionFeriadosActualizar != null)
                {
                    try
                    {
                        configuracionFeriadosActualizar.Nombre = configuracionFeriados.Nombre;
                        configuracionFeriadosActualizar.FechaDesde = configuracionFeriados.FechaDesde;
                        configuracionFeriadosActualizar.FechaHasta = configuracionFeriados.FechaDesde;
                        configuracionFeriadosActualizar.Descripcion = configuracionFeriados.Descripcion;
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
        [Route("InsertarConfiguracionFeriado")]
        public async Task<Response> PostConfiguracionFeriado([FromBody] ConfiguracionFeriados configuracionFeriados)
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


                if (configuracionFeriados.FechaDesde > configuracionFeriados.FechaHasta)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha desde no debe ser mayor que la fecha hasta"
                    };
                }

                var respuesta = Existe(configuracionFeriados);
                if (!respuesta.IsSuccess)
                {
                    db.ConfiguracionFeriados.Add(configuracionFeriados);
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

        // DELETE: api/BasesDatos/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteConfiguracionFeriados([FromRoute] int id)
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

                var respuesta = await db.ConfiguracionFeriados.SingleOrDefaultAsync(m => m.IdConfiguracionFeriado == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.ConfiguracionFeriados.Remove(respuesta);
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

        private Response Existe(ConfiguracionFeriados configuracionFeriados)
        {
            var bdd1 = configuracionFeriados.Nombre.ToUpper().TrimEnd().TrimStart();
            var bdd2 = configuracionFeriados.FechaDesde;
            var bdd3 = configuracionFeriados.FechaHasta;
            var bdd4 = configuracionFeriados.Descripcion.ToUpper().TrimEnd().TrimStart();
            var configuracionferiadorespuesta = db.ConfiguracionFeriados.Where(p => p.Nombre.ToUpper().TrimStart().TrimEnd() == bdd1 && p.FechaDesde == bdd2 && p.FechaHasta == bdd3 && p.Descripcion.TrimStart().ToUpper().TrimEnd() == bdd4).FirstOrDefault();
            if (configuracionferiadorespuesta != null)
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
                Resultado = configuracionferiadorespuesta,
            };
        }
    }
}