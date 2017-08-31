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
    [Route("api/ConfiguracionesViaticos")]
    public class ConfiguracionesViaticosController : Controller
    {
        private readonly SwTHDbContext db;

        public ConfiguracionesViaticosController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarConfiguracionesViaticos")]
        public async Task<List<ConfiguracionViatico>> GetConfiguracionesViaticos()
        {
            try
            {
                return await db.ConfiguracionViatico.OrderBy(x => x.PorCientoAJustificar).ToListAsync();
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = "Se ha producido una excepción",
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new List<ConfiguracionViatico>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetConfiguracionViatico([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Módelo no válido",
                    };
                }

                var ConfiguracionViatico = await db.ConfiguracionViatico.SingleOrDefaultAsync(m => m.IdConfiguracionViatico == id);

                if (ConfiguracionViatico == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No encontrado",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Resultado = ConfiguracionViatico,
                };
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = "Se ha producido una excepción",
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new Response
                {
                    IsSuccess = false,
                    Message = "Error ",
                };
            }
        }

        // PUT: api/BasesDatos/5
        [HttpPut("{id}")]
        public async Task<Response> PutConfiguracionViatico([FromRoute] int id, [FromBody] ConfiguracionViatico ConfiguracionViatico)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Módelo inválido"
                    };
                }

                var ConfiguracionViaticoActualizar = await db.ConfiguracionViatico.Where(x => x.IdConfiguracionViatico == id).FirstOrDefaultAsync();
                if (ConfiguracionViaticoActualizar != null)
                {
                    try
                    {

                        ConfiguracionViaticoActualizar.IdDependencia = ConfiguracionViatico.IdDependencia;
                        ConfiguracionViaticoActualizar.PorCientoAJustificar= ConfiguracionViatico.PorCientoAJustificar;
                        ConfiguracionViaticoActualizar.ValorEntregadoPorDia = ConfiguracionViatico.ValorEntregadoPorDia;
                        db.ConfiguracionViatico.Update(ConfiguracionViaticoActualizar);
                        await db.SaveChangesAsync();

                        return new Response
                        {
                            IsSuccess = true,
                            Message = "Ok",
                        };

                    }
                    catch (Exception ex)
                    {
                        await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                        {
                            ApplicationName = Convert.ToString(Aplicacion.SwTH),
                            ExceptionTrace = ex,
                            Message = "Se ha producido una excepción",
                            LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                            LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                            UserName = "",

                        });
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "Error ",
                        };
                    }
                }




                return new Response
                {
                    IsSuccess = false,
                    Message = "Existe"
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Excepción"
                };
            }
        }

        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarConfiguracionViatico")]
        public async Task<Response> PostConfiguracionViatico([FromBody] ConfiguracionViatico ConfiguracionViatico)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Módelo inválido"
                    };
                }

                var respuesta = Existe(ConfiguracionViatico);
                if (!respuesta.IsSuccess)
                {
                    db.ConfiguracionViatico.Add(ConfiguracionViatico);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = "OK"
                    };
                }

                return new Response
                {
                    IsSuccess = false,
                    Message = "Existe una configuración de viático con igual información"
                };

            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = "Se ha producido una excepción",
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new Response
                {
                    IsSuccess = false,
                    Message = "Error ",
                };
            }
        }

        // DELETE: api/BasesDatos/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteConfiguracionViatico([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Módelo no válido ",
                    };
                }

                var respuesta = await db.ConfiguracionViatico.SingleOrDefaultAsync(m => m.IdConfiguracionViatico == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No existe ",
                    };
                }
                db.ConfiguracionViatico.Remove(respuesta);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = "Eliminado ",
                };
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = "Se ha producido una excepción",
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new Response
                {
                    IsSuccess = false,
                    Message = "Error ",
                };
            }
        }

        private Response Existe(ConfiguracionViatico ConfiguracionViatico)
        {
            var ConfiguracionViaticorespuesta = db.ConfiguracionViatico.Where(p => p.IdDependencia ==ConfiguracionViatico.IdDependencia 
                                                && p.PorCientoAJustificar ==ConfiguracionViatico.PorCientoAJustificar 
                                                && p.ValorEntregadoPorDia == ConfiguracionViatico.ValorEntregadoPorDia).FirstOrDefault();
            if (ConfiguracionViaticorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Existe una configuración de viático con igual información",
                    Resultado = null,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = ConfiguracionViaticorespuesta,
            };
        }
    }
}