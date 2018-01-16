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
                return await db.ConfiguracionViatico.Include(x => x.RolPuesto).ToListAsync();

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
                        Message = Mensaje.ModeloInvalido,
                    };
                }

                var ConfiguracionViatico = await db.ConfiguracionViatico.SingleOrDefaultAsync(m => m.IdConfiguracionViatico == id);

                if (ConfiguracionViatico == null)
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
                    Resultado = ConfiguracionViatico,
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

        private async Task Actualizar(ConfiguracionViatico configuracionViatico)
        {
            var escalaevatotal = db.ConfiguracionViatico.Find(configuracionViatico.IdConfiguracionViatico);

            escalaevatotal.IdRolPuesto = configuracionViatico.IdRolPuesto;
            escalaevatotal.ValorEntregadoPorDia = configuracionViatico.ValorEntregadoPorDia;
            escalaevatotal.PorCientoAjustificar = configuracionViatico.PorCientoAjustificar;
            db.ConfiguracionViatico.Update(escalaevatotal);
            await db.SaveChangesAsync();
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
                        Message = Mensaje.ModeloInvalido
                    };
                }


                var existe = Existe(ConfiguracionViatico);
                var ConfiguracionViaticoActualizar = (ConfiguracionViatico)existe.Resultado;

                if (existe.IsSuccess)
                {


                    if (ConfiguracionViaticoActualizar.IdConfiguracionViatico == ConfiguracionViatico.IdConfiguracionViatico)
                    {
                        if (ConfiguracionViatico.IdRolPuesto == ConfiguracionViaticoActualizar.IdRolPuesto &&
                        ConfiguracionViatico.ValorEntregadoPorDia == ConfiguracionViaticoActualizar.ValorEntregadoPorDia &&
                        ConfiguracionViatico.PorCientoAjustificar == ConfiguracionViaticoActualizar.PorCientoAjustificar)
                        {
                            return new Response
                            {
                                IsSuccess = true,
                            };
                        }

                        await Actualizar(ConfiguracionViatico);
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

                await Actualizar(ConfiguracionViatico);
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
                        Message = ""
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
        public async Task<Response> DeleteConfiguracionViatico([FromRoute] int id)
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

                var respuesta = await db.ConfiguracionViatico.SingleOrDefaultAsync(m => m.IdConfiguracionViatico == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.ConfiguracionViatico.Remove(respuesta);
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

        private Response Existe(ConfiguracionViatico ConfiguracionViatico)
        {
            var ConfiguracionViaticorespuesta = db.ConfiguracionViatico.Where(p => p.IdRolPuesto ==ConfiguracionViatico.IdRolPuesto
                                                && p.PorCientoAjustificar ==ConfiguracionViatico.PorCientoAjustificar
                                                && p.ValorEntregadoPorDia == ConfiguracionViatico.ValorEntregadoPorDia).FirstOrDefault();
            if (ConfiguracionViaticorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = ConfiguracionViaticorespuesta,
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