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
    [Route("api/ConceptoConjuntoNomina")]
    public class ConceptoConjuntoNominaController : Controller
    {

        private readonly SwTHDbContext db;

        public ConceptoConjuntoNominaController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarConceptoConjuntoNomina")]
        public async Task<List<ConceptoConjuntoNomina>> ListarConceptoConjuntoNomina()
        {
            try
            {
                return await db.ConceptoConjuntoNomina.Include(x=>x.Concepto).Include(x => x.Conjunto).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<ConceptoConjuntoNomina>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet]
        [HttpPost("ObtenerConceptoConjuntoNomina")]
        public async Task<Response> ObtenerConceptoConjuntoNomina([FromBody] ConceptoConjuntoNomina ConceptoConjuntoNomina)
        {
            try
            {
                var conceptoConjuntoNomina = await db.ConceptoConjuntoNomina.SingleOrDefaultAsync(m => m.IdConceptoConjunto == ConceptoConjuntoNomina.IdConceptoConjunto);

                if (conceptoConjuntoNomina == null)
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
                    Resultado = conceptoConjuntoNomina,
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
        [Route("EditarConceptoConjuntoNomina")]
        public async Task<Response> EditarConceptoConjuntoNomina([FromBody] ConceptoConjuntoNomina ConceptoConjuntoNomina)
        {
            try
            {
                if (await Existe(ConceptoConjuntoNomina))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var ConceptoConjuntoNominaActualizar = await db.ConceptoConjuntoNomina.Where(x => x.IdConceptoConjunto == ConceptoConjuntoNomina.IdConceptoConjunto).FirstOrDefaultAsync();
                if (ConceptoConjuntoNominaActualizar == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                    };

                }

                ConceptoConjuntoNominaActualizar.IdConcepto = ConceptoConjuntoNomina.IdConcepto;
                ConceptoConjuntoNominaActualizar.IdConjunto = ConceptoConjuntoNomina.IdConjunto;
                ConceptoConjuntoNominaActualizar.Suma = ConceptoConjuntoNomina.Suma;
                ConceptoConjuntoNominaActualizar.Resta = ConceptoConjuntoNomina.Resta;
                db.ConceptoConjuntoNomina.Update(ConceptoConjuntoNominaActualizar);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Resultado = ConceptoConjuntoNominaActualizar
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
        [Route("InsertarConceptoConjuntoNomina")]
        public async Task<Response> PostConceptoConjuntoNomina([FromBody] ConceptoConjuntoNomina ConceptoConjuntoNomina)
        {
            try
            {

                if (!await Existe(ConceptoConjuntoNomina))
                {
                    db.ConceptoConjuntoNomina.Add(ConceptoConjuntoNomina);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                        Resultado = ConceptoConjuntoNomina,
                    };
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
                    Message = Mensaje.Error,
                };
            }
        }

        // DELETE: api/BasesDatos/5
        [HttpPost]
        [Route("EliminarConceptoConjuntoNomina")]
        public async Task<Response> EliminarConceptoConjuntoNomina([FromBody]ConceptoConjuntoNomina ConceptoConjuntoNomina)
        {
            try
            {
                var respuesta = await db.ConceptoConjuntoNomina.Where(m => m.IdConceptoConjunto == ConceptoConjuntoNomina.IdConceptoConjunto).FirstOrDefaultAsync();
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.ConceptoConjuntoNomina.Remove(respuesta);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                };
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("FK"))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.BorradoNoSatisfactorio,
                    };
                }
                else
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = ex.InnerException.InnerException.Message,
                    };
                }

            }
        }

        private async Task<bool> Existe(ConceptoConjuntoNomina ConceptoConjuntoNomina)
        {
            var conjunto = ConceptoConjuntoNomina.IdConjunto;
            var concepto = ConceptoConjuntoNomina.IdConcepto;
            var ConceptoConjuntoNominarespuesta = await db.ConceptoConjuntoNomina.Where(p => p.IdConcepto ==concepto && p.IdConjunto==conjunto).FirstOrDefaultAsync();

            if (ConceptoConjuntoNominarespuesta == null || ConceptoConjuntoNominarespuesta.IdConceptoConjunto == ConceptoConjuntoNomina.IdConceptoConjunto)
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