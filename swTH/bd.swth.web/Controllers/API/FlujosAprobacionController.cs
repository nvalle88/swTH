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
    [Route("api/FlujosAprobacion")]
    public class FlujosAprobacionController : Controller
    {
        private readonly SwTHDbContext db;

        public FlujosAprobacionController(SwTHDbContext db)
        {
            this.db = db;
        }

        

        // GET: api/FlujoAprobacion
        [HttpGet]
        [Route("ListarFlujosAprobacion")]
        public async Task<List<FlujoAprobacion>> GetFlujosAprobacion()
        {
            try
            {
                return await db.FlujoAprobacion
                    .Include(i=>i.Sucursal)
                    .Include(i=>i.TipoAccionPersonal)
                    .Include(i=>i.ManualPuesto)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                
                return new List<FlujoAprobacion>();
            }
        }

        
        // GET: api/FlujoAprobacion/5
        [HttpGet("{id}")]
        public async Task<Response> GetFlujoAprobacion([FromRoute] int id)
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

                var FlujoAprobacion = await db.FlujoAprobacion
                    .Include(i=>i.TipoAccionPersonal)
                    .SingleOrDefaultAsync(m => m.IdFlujoAprobacion == id);

                if (FlujoAprobacion == null)
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
                    Resultado = FlujoAprobacion,
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



        // PUT: api/FlujoAprobacion/5
        [HttpPut("{id}")]
        public async Task<Response> PutFlujoAprobacion([FromRoute] int id, [FromBody] FlujoAprobacion flujoAprobacion)
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

                var modelo = new FlujoAprobacion
                {
                    IdFlujoAprobacion = flujoAprobacion.IdFlujoAprobacion,
                    IdTipoAccionPersonal = flujoAprobacion.IdTipoAccionPersonal,
                    IdSucursal = flujoAprobacion.IdSucursal,
                    IdManualPuesto = flujoAprobacion.IdManualPuesto,
                    ApruebaJefe = flujoAprobacion.ApruebaJefe

                };

                if (modelo.ApruebaJefe == true)
                {
                    modelo.IdManualPuesto = null;
                }
                else if
                    (
                        modelo.ApruebaJefe == false
                        &&
                        (modelo.IdManualPuesto == null || modelo.IdManualPuesto < 1)

                    )
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ErrorFlujoAprobacionSeleccion
                    };
                }



                var existe = await db.FlujoAprobacion
                    .Where(w => 
                        w.IdSucursal == modelo.IdSucursal
                        && w.IdManualPuesto == modelo.IdManualPuesto
                        && w.IdTipoAccionPersonal == modelo.IdTipoAccionPersonal
                        && w.ApruebaJefe == modelo.ApruebaJefe
                ).FirstOrDefaultAsync();


                if (existe == null || existe != null && existe.IdFlujoAprobacion == flujoAprobacion.IdFlujoAprobacion)
                {

                    var modeloFlujoAprobacion = await db.FlujoAprobacion
                        .Where(w => w.IdFlujoAprobacion == flujoAprobacion.IdFlujoAprobacion)
                        .FirstOrDefaultAsync();

                    modeloFlujoAprobacion.IdTipoAccionPersonal = modelo.IdTipoAccionPersonal;
                    modeloFlujoAprobacion.IdSucursal = modelo.IdSucursal;
                    modeloFlujoAprobacion.IdManualPuesto = modelo.IdManualPuesto;
                    modeloFlujoAprobacion.ApruebaJefe = modelo.ApruebaJefe;

                    db.FlujoAprobacion.Update(modeloFlujoAprobacion);
                    await db.SaveChangesAsync();

                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.GuardadoSatisfactorio,
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

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Excepcion,
                };
            }

        }

        
        // POST: api/FlujoAprobacion
        [HttpPost]
        [Route("InsertarFlujoAprobacion")]
        public async Task<Response> PostFlujoAprobacion([FromBody] FlujoAprobacion FlujoAprobacion)
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

                var modelo = new FlujoAprobacion {
                    IdFlujoAprobacion = FlujoAprobacion.IdFlujoAprobacion,
                    IdTipoAccionPersonal = FlujoAprobacion.IdTipoAccionPersonal,
                    IdSucursal = FlujoAprobacion.IdSucursal,
                    IdManualPuesto = FlujoAprobacion.IdManualPuesto,
                    ApruebaJefe = FlujoAprobacion.ApruebaJefe

                };

                if (modelo.ApruebaJefe == true)
                {
                    modelo.IdManualPuesto = null;
                }
                else if
                    (
                        modelo.ApruebaJefe == false
                        && 
                        (modelo.IdManualPuesto == null || modelo.IdManualPuesto < 1)

                    )
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ErrorFlujoAprobacionSeleccion
                    };
                }


                var existe = await db.FlujoAprobacion
                    .Where(w =>
                        w.IdTipoAccionPersonal == modelo.IdTipoAccionPersonal
                        && w.IdSucursal == modelo.IdSucursal
                        && w.IdManualPuesto == modelo.IdManualPuesto
                        && w.ApruebaJefe == modelo.ApruebaJefe
                     )
                     .FirstOrDefaultAsync();

                if (existe == null)
                {
                    db.FlujoAprobacion.Add(modelo);
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

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }


        
        // DELETE: api/FlujoAprobacion/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteFlujoAprobacion([FromRoute] int id)
        {
            try
            {

                var respuesta = await db.FlujoAprobacion.SingleOrDefaultAsync(m => m.IdFlujoAprobacion == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }

                db.FlujoAprobacion.Remove(respuesta);
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
}