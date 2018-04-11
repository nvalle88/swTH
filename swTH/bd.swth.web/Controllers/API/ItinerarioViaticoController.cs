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
    [Route("api/ItinerarioViatico")]
    public class ItinerarioViaticoController : Controller
    {
        private readonly SwTHDbContext db;

        public ItinerarioViaticoController(SwTHDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        [Route("ListarItinerariosViaticos")]
        public async Task<List<ItinerarioViatico>> ListarItinerariosViaticos([FromBody]ItinerarioViatico itinerarioViatico)
        {
            //Persona persona = new Persona();
            try
            {

                return await db.ItinerarioViatico.Where(x=>x.IdSolicitudViatico == itinerarioViatico.IdSolicitudViatico).Include(x => x.TipoTransporte).OrderBy(x => x.FechaDesde).ToListAsync();

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
                return new List<ItinerarioViatico>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetItinerarioViatico([FromRoute] int id)
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

                var ItinerarioViatico = await db.ItinerarioViatico.SingleOrDefaultAsync(m => m.IdItinerarioViatico == id);

                if (ItinerarioViatico == null)
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
                    Resultado = ItinerarioViatico,
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
        public async Task<Response> PutItinerarioViatico([FromRoute] int id, [FromBody] ItinerarioViatico itinerarioViatico)
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

                var existe = Existe(itinerarioViatico);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var itinerarioViaticoActualizar = await db.ItinerarioViatico.Where(x => x.IdItinerarioViatico == id).FirstOrDefaultAsync();

                if (itinerarioViaticoActualizar != null)
                {
                    try
                    {
                        itinerarioViaticoActualizar.IdTipoTransporte = itinerarioViatico.IdTipoTransporte;
                        itinerarioViaticoActualizar.Descripcion = itinerarioViatico.Descripcion;
                        itinerarioViaticoActualizar.FechaDesde = itinerarioViatico.FechaDesde;
                        itinerarioViaticoActualizar.FechaHasta = itinerarioViatico.FechaHasta;
                        itinerarioViaticoActualizar.Valor = itinerarioViatico.Valor;
                        itinerarioViaticoActualizar.HoraSalida = itinerarioViatico.HoraSalida;
                        itinerarioViaticoActualizar.HoraLlegada = itinerarioViatico.HoraLlegada;


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
        [Route("InsertarItinerarioViatico")]
        public async Task<Response> PostItinerarioViatico([FromBody] ItinerarioViatico ItinerarioViatico)
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

                var respuesta = Existe(ItinerarioViatico);
                if (!respuesta.IsSuccess)
                {
                    db.ItinerarioViatico.Add(ItinerarioViatico);
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
        public async Task<Response> DeleteItinerarioViatico([FromRoute] int id)
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

                var respuesta = await db.ItinerarioViatico.SingleOrDefaultAsync(m => m.IdItinerarioViatico == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.ItinerarioViatico.Remove(respuesta);
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

        private Response Existe(ItinerarioViatico ItinerarioViatico)
        {
            var bdd1 = ItinerarioViatico.IdSolicitudViatico;
            var bdd2 = ItinerarioViatico.IdTipoTransporte;
            var bdd3 = ItinerarioViatico.Descripcion;
            var bdd4 = ItinerarioViatico.FechaDesde;
            var bdd5 = ItinerarioViatico.FechaHasta;
            var bdd6 = ItinerarioViatico.Valor;
            var bdd7 = ItinerarioViatico.HoraSalida;
            var bdd8 = ItinerarioViatico.HoraLlegada;
            var itinerarioviaticorespuesta = db.ItinerarioViatico.Where(p => p.IdSolicitudViatico== bdd1 
            && p.IdTipoTransporte ==bdd2 
            && p.Descripcion == bdd3
            && p.FechaHasta == bdd4
            && p.FechaHasta == bdd5
            && p.Valor == bdd6
            && p.HoraSalida == bdd7
            && p.HoraLlegada == bdd8).FirstOrDefault();
            if (itinerarioviaticorespuesta != null)
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
                Resultado = itinerarioviaticorespuesta,
            };
        }
    }
}