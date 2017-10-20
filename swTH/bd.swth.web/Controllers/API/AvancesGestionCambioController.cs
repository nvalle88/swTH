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
    [Route("api/AvancesGestionCambio")]
    public class AvancesGestionCambioController : Controller
    {
        private readonly SwTHDbContext db;

        public AvancesGestionCambioController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/AvancesGestionCambio
        [HttpGet]
        [Route("ListarAvancesGestionCambio")]
        public async Task<List<AvanceGestionCambio>> GetAvancesGestionCambio()
        {
            try
            {
                return await db.AvanceGestionCambio.OrderBy(x => x.Fecha).ToListAsync();
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
                return new List<AvanceGestionCambio>();
            }
        }


        // GET: api/AvancesGestionCambio
        [HttpGet]
        [Route("ListarAvancesGestionCambioconIdActividad")]
        public async Task<List<AvanceGestionCambio>> GetAvancesGestionCambioconIdActividad(int idActividadesGestionCambio)
        {
            try
            {
                return await db.AvanceGestionCambio.Where(m => m.IdActividadesGestionCambio == idActividadesGestionCambio).ToListAsync();
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
                return new List<AvanceGestionCambio>();
            }
        }


        // GET: api/AvancesGestionCambio/5
        [HttpGet("{id}")]
        public async Task<Response> GetAvanceGestionCambio([FromRoute] int id)
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

                var AvanceGestionCambio = await db.AvanceGestionCambio.SingleOrDefaultAsync(m => m.IdAvanceGestionCambio == id);

                if (AvanceGestionCambio == null)
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
                    Resultado = AvanceGestionCambio,
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

        // PUT: api/AvancesGestionCambio/5
        [HttpPut("{id}")]
        public async Task<Response> PutAvanceGestionCambio([FromRoute] int id, [FromBody] AvanceGestionCambio avanceGestionCambio)
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

                var existe = Existe(avanceGestionCambio);
                var AvanceGestionCambioActualizar = (AvanceGestionCambio)existe.Resultado;
                if (existe.IsSuccess)
                {
                    if (AvanceGestionCambioActualizar.IdAvanceGestionCambio == avanceGestionCambio.IdAvanceGestionCambio)
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
                var AvanceGestionCambio = db.AvanceGestionCambio.Find(avanceGestionCambio.IdAvanceGestionCambio);

                AvanceGestionCambio.Fecha = AvanceGestionCambio.Fecha;
                AvanceGestionCambio.Indicadorreal = AvanceGestionCambio.Indicadorreal;
                AvanceGestionCambio.IdActividadesGestionCambio = AvanceGestionCambio.IdActividadesGestionCambio;
                db.AvanceGestionCambio.Update(AvanceGestionCambio);
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
        [Route("InsertarAvanceGestionCambio")]
        public async Task<Response> PostAvanceGestionCambio([FromBody] AvanceGestionCambio AvanceGestionCambio)
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

                var respuesta = Existe(AvanceGestionCambio);
                if (!respuesta.IsSuccess)
                {
                    db.AvanceGestionCambio.Add(AvanceGestionCambio);
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

        // DELETE: api/AvancesGestionCambio/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteAvanceGestionCambio([FromRoute] int id)
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

                var respuesta = await db.AvanceGestionCambio.SingleOrDefaultAsync(m => m.IdAvanceGestionCambio == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.AvanceGestionCambio.Remove(respuesta);
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

        private Response Existe(AvanceGestionCambio AvanceGestionCambio)
        {
            var bdd = AvanceGestionCambio.Fecha;
            var AvanceGestionCambiorespuesta = db.AvanceGestionCambio.Where(p => p.Fecha== bdd && p.IdActividadesGestionCambio == AvanceGestionCambio.IdActividadesGestionCambio).FirstOrDefault();
            if (AvanceGestionCambiorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = AvanceGestionCambiorespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = AvanceGestionCambiorespuesta,
            };
        }
    }
}