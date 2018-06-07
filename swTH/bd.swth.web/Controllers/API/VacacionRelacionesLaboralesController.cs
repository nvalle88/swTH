using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using System;
using bd.swth.entidades.Enumeradores;
using bd.swth.entidades.Utils;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.log.guardar.Enumeradores;
using bd.swth.entidades.ViewModels;
using bd.swth.entidades.Constantes;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/VacacionRelacionesLaborales")]
    public class VacacionRelacionesLaboralesController : Controller
    {
        private readonly SwTHDbContext db;

        public VacacionRelacionesLaboralesController(SwTHDbContext db)
        {
            this.db = db;
        }


        // GET: api/VacacionRelacionesLaborales
        [HttpGet]
        [Route("ListarVacacionRelacionesLaborales")]
        public async Task<List<VacacionRelacionLaboral>> ListarVacacionRelacionesLaborales()
        {
            try
            {
                return await db.VacacionRelacionLaboral.Include(i=>i.RelacionLaboral).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<VacacionRelacionLaboral>();
            }
        }


        // Post: api/VacacionRelacionesLaborales
        [HttpPost]
        [Route("ObtenerVacacionRelacionLaboral")]
        public async Task<VacacionRelacionLaboral> ObtenerVacacionRelacionLaboral([FromBody] VacacionRelacionLaboral vacacionRelacionLaboral)
        {
            try
            {

                var modelo = await db.VacacionRelacionLaboral
                    .Where(w => w.IdVacacionRelacionLaboral == vacacionRelacionLaboral.IdVacacionRelacionLaboral)
                    .FirstOrDefaultAsync();

                return modelo;

            }
            catch (Exception ex)
            {
                return new VacacionRelacionLaboral();
            }
        }


        // Post: api/VacacionRelacionesLaborales
        [HttpPost]
        [Route("InsertarVacacionesrelacionesLaborales")]
        public async Task<Response> InsertarVacacionesrelacionesLaborales([FromBody] VacacionRelacionLaboral vacacionRelacionLaboral)
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


                var existe = await Existe(vacacionRelacionLaboral);


                if (existe.IsSuccess == true) {

                    return new Response
                    {
                       IsSuccess = false,
                       Message = Mensaje.ExisteVacacionRelacionLaboral
                    };

                }


                if (existe.IsSuccess == false && Convert.ToInt32(existe.Resultado) == 0)
                {
                    db.VacacionRelacionLaboral.Add(vacacionRelacionLaboral);
                    await db.SaveChangesAsync();

                    return new Response {
                        IsSuccess = true,
                        Message = Mensaje.GuardadoSatisfactorio
                    };
                }

                else {

                    return existe;
                }
                

            }
            catch (Exception ex)
            {
                return new Response {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };
            }
        }


        // Post: api/VacacionRelacionesLaborales
        [HttpPost]
        [Route("EditarVacacionesrelacionesLaborales")]
        public async Task<Response> EditarVacacionesrelacionesLaborales([FromBody] VacacionRelacionLaboral vacacionRelacionLaboral)
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


                var existe = await db.VacacionRelacionLaboral
                    .Where(w=>w.IdVacacionRelacionLaboral == vacacionRelacionLaboral.IdVacacionRelacionLaboral)
                    .FirstOrDefaultAsync();

                var IdRelacionLaboralModel = await db.VacacionRelacionLaboral
                    .Where(w => w.IdRelacionLaboral == vacacionRelacionLaboral.IdRelacionLaboral)
                    .FirstOrDefaultAsync();


                if (existe == null) {

                    return new Response {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado
                    };
                }

                else if (
                        IdRelacionLaboralModel == null 
                        || IdRelacionLaboralModel != null && IdRelacionLaboralModel.IdVacacionRelacionLaboral == vacacionRelacionLaboral.IdVacacionRelacionLaboral
                      ) {

                    existe.IncrementoApartirPeriodoFiscal = vacacionRelacionLaboral.IncrementoApartirPeriodoFiscal;
                    existe.IncrementoDiasPorPeriodoFiscal = vacacionRelacionLaboral.IncrementoDiasPorPeriodoFiscal;
                    existe.MaxAcumulacionDias = vacacionRelacionLaboral.MaxAcumulacionDias;
                    existe.MinAcumulacionDias = vacacionRelacionLaboral.MinAcumulacionDias;
                    existe.IdRelacionLaboral = existe.IdRelacionLaboral;


                    db.VacacionRelacionLaboral.Update(existe);
                    await db.SaveChangesAsync();

                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.GuardadoSatisfactorio
                    };

                }

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ExisteVacacionRelacionLaboral
                };
                
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };
            }
        }


        // DELETE: api/VacacionRelacionesLaborales
        [HttpPost]
        [Route("BorrarVacacionRelacionLaboral")]
        public async Task<Response> BorrarVacacionRelacionLaboral([FromBody] string id)
        {
            try
            {
                var ids = Convert.ToInt32(id);

                var modelo = await db.VacacionRelacionLaboral
                    .Where(w => w.IdVacacionRelacionLaboral == ids)
                    .FirstOrDefaultAsync();

                db.VacacionRelacionLaboral.Remove(modelo);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.BorradoSatisfactorio
                };


            }
            catch (Exception)
            {
                return new Response {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };
            }
        }

        public async Task<Response> Existe( VacacionRelacionLaboral vacacionRelacionLaboral) {

            try{

                var registro = await db.VacacionRelacionLaboral
                    .Where(w => 
                        w.IdRelacionLaboral == vacacionRelacionLaboral.IdRelacionLaboral
                        )
                    .FirstOrDefaultAsync();

                if (registro == null) {

                    return new Response {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                        Resultado = 0
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = registro
                };

            }catch (Exception) {

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };
            }

        }

        
    }
}