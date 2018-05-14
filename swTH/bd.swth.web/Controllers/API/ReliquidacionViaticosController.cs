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
        [Route("Actividades")]
        public async Task<Response> Actividades([FromBody] InformeViatico informeViatico)
        {

            var informeViaticoActualizar = await db.InformeActividadViatico.Where(x => x.IdItinerarioViatico == informeViatico.IdItinerarioViatico).FirstOrDefaultAsync();

            if (informeViaticoActualizar != null)
            {
                try
                {

                    informeViaticoActualizar.Descripcion = informeViatico.Descripcion;
                    db.InformeActividadViatico.Update(informeViaticoActualizar);
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
            else
            {
                try
                {
                    var actividad = new InformeActividadViatico
                    {
                        IdItinerarioViatico = informeViatico.IdItinerarioViatico,
                        Descripcion = informeViatico.Descripcion,
                    };
                    db.InformeActividadViatico.Add(actividad);
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
        }

        [HttpPost]
        [Route("ActualizarEstadoInforme")]
        public async Task<Response> ActualizarEstadoInforme([FromBody] SolicitudViatico solicitudViatico)
        {

            var informeViaticoActualizar = await db.SolicitudViatico.Where(x => x.IdSolicitudViatico == solicitudViatico.IdSolicitudViatico).FirstOrDefaultAsync();

            if (informeViaticoActualizar != null)
            {
                try
                {

                    informeViaticoActualizar.Estado = 3;
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
        [Route("ObtenerActividades")]
        public async Task<InformeActividadViatico> ObtenerActividades([FromBody] InformeViatico informeViatico)
        {

            var informeViaticoActualizar = await db.InformeActividadViatico.Where(x => x.IdItinerarioViatico == informeViatico.IdItinerarioViatico).FirstOrDefaultAsync();

            if (informeViaticoActualizar != null)
            {

                return informeViaticoActualizar;
            }

            return informeViaticoActualizar;

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

                var ItinerarioViatico = await db.InformeViatico.SingleOrDefaultAsync(m => m.IdInformeViatico == id);

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
        public async Task<Response> ActualizarInformeViatico([FromRoute] int id, [FromBody] InformeViatico informeViatico)
        {

            var informeViaticoActualizar = await db.InformeViatico.Where(x => x.IdInformeViatico == id).FirstOrDefaultAsync();

            if (informeViaticoActualizar != null)
            {
                try
                {
                    var existe = Existe(informeViatico);
                    if (!existe.IsSuccess)
                    {
                        informeViaticoActualizar.IdItinerarioViatico = informeViatico.IdItinerarioViatico;
                        informeViaticoActualizar.IdTipoTransporte = informeViatico.IdTipoTransporte;
                        informeViaticoActualizar.NombreTransporte = informeViatico.NombreTransporte;
                        informeViaticoActualizar.IdCiudadDestino = informeViatico.IdCiudadDestino;
                        informeViaticoActualizar.IdCiudadOrigen = informeViatico.IdCiudadOrigen;
                        informeViaticoActualizar.FechaSalida = informeViatico.FechaSalida;
                        informeViaticoActualizar.FechaLlegada = informeViatico.FechaLlegada;
                        informeViaticoActualizar.HoraSalida = informeViatico.HoraSalida;
                        informeViaticoActualizar.HoraLlegada = informeViatico.HoraLlegada;
                        informeViaticoActualizar.Descripcion = informeViatico.Descripcion;
                        informeViaticoActualizar.ValorEstimado = informeViatico.ValorEstimado;
                        db.InformeViatico.Update(informeViaticoActualizar);
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
                informeViaticoActualizar.IdItinerarioViatico = informeViatico.IdItinerarioViatico;
                informeViaticoActualizar.IdTipoTransporte = informeViatico.IdTipoTransporte;
                informeViaticoActualizar.NombreTransporte = informeViatico.NombreTransporte;
                informeViaticoActualizar.IdCiudadDestino = informeViatico.IdCiudadDestino;
                informeViaticoActualizar.IdCiudadOrigen = informeViatico.IdCiudadOrigen;
                informeViaticoActualizar.FechaSalida = informeViatico.FechaSalida;
                informeViaticoActualizar.FechaLlegada = informeViatico.FechaLlegada;
                informeViaticoActualizar.HoraSalida = informeViatico.HoraSalida;
                informeViaticoActualizar.HoraLlegada = informeViatico.HoraLlegada;
                informeViaticoActualizar.Descripcion = informeViatico.Descripcion;
                informeViaticoActualizar.ValorEstimado = informeViatico.ValorEstimado;
                db.InformeViatico.Add(informeViaticoActualizar);
                await db.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                };
            }


        }
        [HttpDelete("{id}")]
        public async Task<Response> DeleteInformeViaticos([FromRoute] int id)
        {
            try
            {

                var respuesta = await db.InformeViatico.SingleOrDefaultAsync(m => m.IdInformeViatico == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.InformeViatico.Remove(respuesta);
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

        private Response Existe(InformeViatico informeViatico)
        {
            var bdd1 = informeViatico.IdInformeViatico;
            var bdd2 = informeViatico.IdTipoTransporte;
            var bdd3 = informeViatico.NombreTransporte;
            var bdd4 = informeViatico.IdCiudadOrigen;
            var bdd5 = informeViatico.IdCiudadDestino;
            var bdd6 = informeViatico.FechaSalida;
            var bdd7 = informeViatico.HoraSalida;
            var bdd8 = informeViatico.FechaLlegada;
            var bdd9 = informeViatico.HoraLlegada;
            var bdd10 = informeViatico.IdItinerarioViatico;
            var bdd11 = informeViatico.ValorEstimado;
            var informeViaticos = db.InformeViatico.Where(p => p.IdInformeViatico == bdd1
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