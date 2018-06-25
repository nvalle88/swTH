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
    [Route("api/InformeViaticos")]
    public class InformeViaticosController : Controller
    {
        private readonly SwTHDbContext db;

        public InformeViaticosController(SwTHDbContext db)
        {
            this.db = db;
        }
        [HttpPost]
        [Route("ListarInformeViaticos")]
        public async Task<List<InformeViatico>> ListarInformeViaticos([FromBody] InformeViatico informeViatico)
        {

            try
            {
                return await db.InformeViatico.Where(x => x.IdSolicitudViatico == informeViatico.IdSolicitudViatico).Include(x => x.TipoTransporte).Include(x => x.CiudadOrigen).Include(x => x.CiudadDestino).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<InformeViatico>();
            }
        }
        [HttpPost]
        [Route("Actividades")]
        public async Task<Response> Actividades([FromBody] InformeActividadViatico informeActividadViatico)
        {

            var informeActividadesViatico = await db.InformeActividadViatico.Where(x => x.IdSolicitudViatico == informeActividadViatico.IdSolicitudViatico).FirstOrDefaultAsync();

            if (informeActividadesViatico != null)
            {
                try
                {

                    informeActividadesViatico.Descripcion = informeActividadViatico.Descripcion;
                    informeActividadesViatico.Observacion = informeActividadViatico.Observacion;
                    db.InformeActividadViatico.Update(informeActividadesViatico);
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
                        IdSolicitudViatico = informeActividadViatico.IdSolicitudViatico,
                        Descripcion = informeActividadViatico.Descripcion,
                        Observacion = informeActividadViatico.Observacion
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
        [Route("ObtenerActividades")]
        public async Task<InformeActividadViatico> ObtenerActividades([FromBody] InformeViatico informeViatico)
        {

            try
            {
                var informeViaticoActualizar = await db.InformeActividadViatico.Where(x => x.IdSolicitudViatico == informeViatico.IdSolicitudViatico).FirstOrDefaultAsync();
                                

                return informeViaticoActualizar;
            }
            catch (Exception ex)
            {

                return new InformeActividadViatico();
            }

        }
        [HttpPost]
        [Route("InsertarInformeViatico")]
        public async Task<Response> InsertarInformeViatico([FromBody] InformeViatico informeViatico)
        {
            try
            {

                db.InformeViatico.Add(informeViatico);
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
                        informeViaticoActualizar.IdSolicitudViatico = informeViatico.IdSolicitudViatico;
                        informeViaticoActualizar.IdTipoTransporte = informeViatico.IdTipoTransporte;
                        informeViaticoActualizar.NombreTransporte = informeViatico.NombreTransporte;
                        informeViaticoActualizar.IdCiudadDestino = informeViatico.IdCiudadDestino;
                        informeViaticoActualizar.IdCiudadOrigen = informeViatico.IdCiudadOrigen;
                        informeViaticoActualizar.FechaSalida = informeViatico.FechaSalida;
                        informeViaticoActualizar.FechaLlegada = informeViatico.FechaLlegada;
                        informeViaticoActualizar.HoraSalida = informeViatico.HoraSalida;
                        informeViaticoActualizar.HoraLlegada = informeViatico.HoraLlegada;
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
                informeViaticoActualizar.IdSolicitudViatico = informeViatico.IdSolicitudViatico;
                informeViaticoActualizar.IdTipoTransporte = informeViatico.IdTipoTransporte;
                informeViaticoActualizar.NombreTransporte = informeViatico.NombreTransporte;
                informeViaticoActualizar.IdCiudadDestino = informeViatico.IdCiudadDestino;
                informeViaticoActualizar.IdCiudadOrigen = informeViatico.IdCiudadOrigen;
                informeViaticoActualizar.FechaSalida = informeViatico.FechaSalida;
                informeViaticoActualizar.FechaLlegada = informeViatico.FechaLlegada;
                informeViaticoActualizar.HoraSalida = informeViatico.HoraSalida;
                informeViaticoActualizar.HoraLlegada = informeViatico.HoraLlegada;
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
                        Resultado = respuesta,
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
            var bdd10 = informeViatico.IdSolicitudViatico;
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
            && p.IdSolicitudViatico == bdd10
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