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
    [Route("api/GestionCapacitaciones")]
    public class GestionCapacitacionesController : Controller
    {

        private readonly SwTHDbContext db;

        public GestionCapacitacionesController(SwTHDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("ListarGestionPlanCapacitaciones")]
        public async Task<List<GestionPlanCapacitacion>> ListarGestionPlanCapacitaciones()
        {
            try
            {
                return await db.GestionPlanCapacitacion.ToListAsync();
            }
            catch (Exception)
            {
                return new List<GestionPlanCapacitacion>();
            }
        }
        // GET: api/BasesDatos/5
        [HttpGet]
        [HttpPost("ObtenerCalculoNomina")]
        public async Task<Response> ObtenerCalculoNomina([FromBody] GestionPlanCapacitacion gestionPlanCapacitacion)
        {
            try
            {
                var response = await db.GestionPlanCapacitacion.SingleOrDefaultAsync(m => m.IdGestionPlanCapacitacion == gestionPlanCapacitacion.IdGestionPlanCapacitacion);

                if (response == null)
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
                    Resultado = response,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }
        // PUT: api/BasesDatos/5
        [HttpPost]
        [Route("EditarCalculoNomina")]
        public async Task<Response> EditarCalculoNomina([FromBody] GestionPlanCapacitacion gestionPlanCapacitacion)
        {
            try
            {
                if (await Existe(gestionPlanCapacitacion))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var Actualizar = await db.GestionPlanCapacitacion.Where(x => x.IdGestionPlanCapacitacion == gestionPlanCapacitacion.IdGestionPlanCapacitacion).FirstOrDefaultAsync();
                if (Actualizar == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                    };

                }
                Actualizar.Nombre = gestionPlanCapacitacion.Nombre;
                Actualizar.Descripcion = gestionPlanCapacitacion.Descripcion;
                Actualizar.Anio = gestionPlanCapacitacion.Anio;
                Actualizar.Estado = gestionPlanCapacitacion.Estado;
               
                db.GestionPlanCapacitacion.Update(Actualizar);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Resultado = Actualizar
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
        [Route("InsertarGestionCapacitaciones")]
        public async Task<Response> PostCalculoNomina([FromBody] GestionPlanCapacitacion gestionPlanCapacitacion)
        {
            try
            {

                if (!await Existe(gestionPlanCapacitacion))
                {
                    db.GestionPlanCapacitacion.Add(gestionPlanCapacitacion);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                        Resultado = gestionPlanCapacitacion,
                    };
                }

                return new Response
                {
                    IsSuccess = false,
                    Message = "Solo se puede crear un plan anual",
                };

            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }

        // DELETE: api/BasesDatos/5
        [HttpPost]
        [Route("EliminarCalculoNomina")]
        public async Task<Response> EliminarCalculoNomina([FromBody]GestionPlanCapacitacion gestionPlanCapacitacion)
        {
            try
            {
                var respuesta = await db.GestionPlanCapacitacion.Where(m => m.IdGestionPlanCapacitacion == gestionPlanCapacitacion.IdGestionPlanCapacitacion).FirstOrDefaultAsync();
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.GestionPlanCapacitacion.Remove(respuesta);
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
                    Message = Mensaje.BorradoNoSatisfactorio,
                };

            }
        }

        private async Task<bool> Existe(GestionPlanCapacitacion gestionPlanCapacitacion)
        {
            var anio = gestionPlanCapacitacion.Anio;
            var respuesta = await db.GestionPlanCapacitacion.Where(p => p.Anio == anio).FirstOrDefaultAsync();

            if (respuesta == null )
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}