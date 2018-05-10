using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/FacturaViatico")]
    public class FacturaViaticoController : Controller
    {
        private readonly SwTHDbContext db;

        public FacturaViaticoController(SwTHDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        [Route("ListarFacturas")]
        public async Task<List<FacturaViatico>> ListarFacturas([FromBody]FacturaViatico facturaViatico)
        {
            //Persona persona = new Persona();
            try
            {

                return await db.FacturaViatico.Where(x => x.IdItinerarioViatico == facturaViatico.IdItinerarioViatico).ToListAsync();

            }
            catch (Exception ex)
            {
                
                return new List<FacturaViatico>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetFacturaViatico([FromRoute] int id)
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
               
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }

        // PUT: api/BasesDatos/5
        [HttpPut("{id}")]
        public async Task<Response> PutFacturaViatico([FromRoute] int id, [FromBody] FacturaViatico facturaViatico)
        {
            try
            {
                var existe = Existe(facturaViatico);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var facturaViaticoActualizar = await db.FacturaViatico.Where(x => x.IdFacturaViatico == id).FirstOrDefaultAsync();

                if (facturaViaticoActualizar != null)
                {
                    try
                    {
                        facturaViaticoActualizar.IdFacturaViatico = facturaViatico.IdFacturaViatico;
                        facturaViaticoActualizar.NumeroFactura = facturaViatico.NumeroFactura;
                        facturaViaticoActualizar.FechaFactura = facturaViatico.FechaFactura;
                        facturaViaticoActualizar.ValorTotalAprobacion = facturaViatico.ValorTotalAprobacion;
                        facturaViaticoActualizar.FechaAprobacion = facturaViatico.FechaAprobacion;
                        facturaViaticoActualizar.ValorTotalAprobacion = facturaViatico.ValorTotalAprobacion;
                        facturaViaticoActualizar.Observaciones = facturaViatico.Observaciones;
                        facturaViaticoActualizar.IdItemViatico = facturaViatico.IdItemViatico;
                        facturaViaticoActualizar.IdItinerarioViatico = facturaViatico.IdItinerarioViatico;


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
        [Route("InsertarItinerarioViatico")]
        public async Task<Response> PostFacturaViatico([FromBody] FacturaViatico facturaViatico)
        {
            try
            {
                var respuesta = Existe(facturaViatico);
                if (!respuesta.IsSuccess)
                {
                    db.FacturaViatico.Add(facturaViatico);
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
        public async Task<Response> DeleteFacturaViatico([FromRoute] int id)
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
               
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }

        private Response Existe(FacturaViatico facturaViatico)
        {
            var bdd1 = facturaViatico.IdFacturaViatico;
            //var bdd2 = ItinerarioViatico.IdTipoTransporte;
            //var bdd3 = ItinerarioViatico.Descripcion;
            //var bdd4 = ItinerarioViatico.FechaDesde;
            //var bdd5 = ItinerarioViatico.FechaHasta;
            //var bdd6 = ItinerarioViatico.Valor;
            //var bdd7 = ItinerarioViatico.HoraSalida;
            //var bdd8 = ItinerarioViatico.HoraLlegada;
            var facturaViaticorespuesta = db.FacturaViatico.Where(p => p.IdFacturaViatico == bdd1
            //&& p.IdTipoTransporte == bdd2
            //&& p.Descripcion == bdd3
            //&& p.FechaHasta == bdd4
            //&& p.FechaHasta == bdd5
            //&& p.Valor == bdd6
            //&& p.HoraSalida == bdd7
            //&& p.HoraLlegada == bdd8
            ).FirstOrDefault();
            if (facturaViaticorespuesta != null)
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
                Resultado = facturaViaticorespuesta,
            };
        }
    } 
}