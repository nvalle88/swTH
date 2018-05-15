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
    [Route("api/ReliquidacionViaticos")]
    public class ReliquidacionViaticosController : Controller
    {
        private readonly SwTHDbContext db;

        public ReliquidacionViaticosController(SwTHDbContext db)
        {
            this.db = db;
        }
        [HttpPost]
        [Route("ListarReliquidaciones")]
        public async Task<List<ReliquidacionViatico>> ListarReliquidaciones([FromBody] InformeViatico informeViatico)
        {

            try
            {
                return await db.ReliquidacionViatico.Where(x => x.IdItinerarioViatico == informeViatico.IdItinerarioViatico).Include(x => x.TipoTransporte).Include(x => x.CiudadOrigen).Include(x => x.CiudadDestino).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<ReliquidacionViatico>();
            }
        }


        [HttpPost]
        [Route("ActualizarEstadoReliquidacion")]
        public async Task<Response> ActualizarEstadoReliquidacion([FromBody] SolicitudViatico solicitudViatico)
        {

            var informeViaticoActualizar = await db.SolicitudViatico.Where(x => x.IdSolicitudViatico == solicitudViatico.IdSolicitudViatico).FirstOrDefaultAsync();

            if (informeViaticoActualizar != null)
            {
                try
                {

                    informeViaticoActualizar.Estado = 4;
                    db.SolicitudViatico.Update(informeViaticoActualizar);
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
                Message = Mensaje.RegistroNoEncontrado,
            };

        }

        [HttpPost]
        [Route("InsertarReliquidacionViatico")]
        public async Task<Response> InsertarReliquidacionViatico([FromBody] ReliquidacionViatico reliquidacionViatico)
        {
            try
            {
                db.ReliquidacionViatico.Add(reliquidacionViatico);
                await db.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio
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
        [HttpGet("{id}")]
        public async Task<Response> GetInformeViatico([FromRoute] int id)
        {
            try
            {

                var ItinerarioViatico = await db.ReliquidacionViatico.SingleOrDefaultAsync(m => m.IdReliquidacionViatico == id);

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
        [HttpPut("{id}")]
        public async Task<Response> ActualizarReliquidacion([FromRoute] int id, [FromBody] ReliquidacionViatico reliquidacionViatico)
        {

            var informeViaticoActualizar = await db.ReliquidacionViatico.Where(x => x.IdReliquidacionViatico == id).FirstOrDefaultAsync();

            if (informeViaticoActualizar != null)
            {
                try
                {
                    var existe = Existe(reliquidacionViatico);
                    if (!existe.IsSuccess)
                    {
                        informeViaticoActualizar.IdItinerarioViatico = reliquidacionViatico.IdItinerarioViatico;
                        informeViaticoActualizar.IdTipoTransporte = reliquidacionViatico.IdTipoTransporte;
                        informeViaticoActualizar.NombreTransporte = reliquidacionViatico.NombreTransporte;
                        informeViaticoActualizar.IdCiudadDestino = reliquidacionViatico.IdCiudadDestino;
                        informeViaticoActualizar.IdCiudadOrigen = reliquidacionViatico.IdCiudadOrigen;
                        informeViaticoActualizar.FechaSalida = reliquidacionViatico.FechaSalida;
                        informeViaticoActualizar.FechaLlegada = reliquidacionViatico.FechaLlegada;
                        informeViaticoActualizar.HoraSalida = reliquidacionViatico.HoraSalida;
                        informeViaticoActualizar.HoraLlegada = reliquidacionViatico.HoraLlegada;
                        informeViaticoActualizar.Descripcion = reliquidacionViatico.Descripcion;
                        informeViaticoActualizar.ValorEstimado = reliquidacionViatico.ValorEstimado;
                        db.ReliquidacionViatico.Update(reliquidacionViatico);
                        await db.SaveChangesAsync();
                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.Satisfactorio,
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
            else
            {
                informeViaticoActualizar.IdItinerarioViatico = reliquidacionViatico.IdItinerarioViatico;
                informeViaticoActualizar.IdTipoTransporte = reliquidacionViatico.IdTipoTransporte;
                informeViaticoActualizar.NombreTransporte = reliquidacionViatico.NombreTransporte;
                informeViaticoActualizar.IdCiudadDestino = reliquidacionViatico.IdCiudadDestino;
                informeViaticoActualizar.IdCiudadOrigen = reliquidacionViatico.IdCiudadOrigen;
                informeViaticoActualizar.FechaSalida = reliquidacionViatico.FechaSalida;
                informeViaticoActualizar.FechaLlegada = reliquidacionViatico.FechaLlegada;
                informeViaticoActualizar.HoraSalida = reliquidacionViatico.HoraSalida;
                informeViaticoActualizar.HoraLlegada = reliquidacionViatico.HoraLlegada;
                informeViaticoActualizar.Descripcion = reliquidacionViatico.Descripcion;
                informeViaticoActualizar.ValorEstimado = reliquidacionViatico.ValorEstimado;
                db.ReliquidacionViatico.Add(reliquidacionViatico);
                await db.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                };
            }


        }
        [HttpDelete("{id}")]
        public async Task<Response> DeleteReliquidacion([FromRoute] int id)
        {
            try
            {

                var respuesta = await db.ReliquidacionViatico.SingleOrDefaultAsync(m => m.IdReliquidacionViatico == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.ReliquidacionViatico.Remove(respuesta);
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

        private Response Existe(ReliquidacionViatico reliquidacionViatico)
        {
            var bdd1 = reliquidacionViatico.IdReliquidacionViatico;
            var bdd2 = reliquidacionViatico.IdTipoTransporte;
            var bdd3 = reliquidacionViatico.NombreTransporte;
            var bdd4 = reliquidacionViatico.IdCiudadOrigen;
            var bdd5 = reliquidacionViatico.IdCiudadDestino;
            var bdd6 = reliquidacionViatico.FechaSalida;
            var bdd7 = reliquidacionViatico.HoraSalida;
            var bdd8 = reliquidacionViatico.FechaLlegada;
            var bdd9 = reliquidacionViatico.HoraLlegada;
            var bdd10 = reliquidacionViatico.IdItinerarioViatico;
            var bdd11 = reliquidacionViatico.ValorEstimado;
            var informeViaticos = db.ReliquidacionViatico.Where(p => p.IdReliquidacionViatico == bdd1
            && p.IdTipoTransporte == bdd2
            && p.NombreTransporte == bdd3
            && p.IdCiudadOrigen == bdd4
            && p.IdCiudadDestino == bdd5
             && p.FechaSalida == bdd6
            && p.HoraSalida == bdd7
             && p.FechaSalida == bdd8
            && p.HoraSalida == bdd9
            && p.IdItinerarioViatico == bdd10
            && p.ValorEstimado == bdd11).FirstOrDefault();
            if (informeViaticos != null)
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
                Resultado = informeViaticos,
            };
        }
    }
}