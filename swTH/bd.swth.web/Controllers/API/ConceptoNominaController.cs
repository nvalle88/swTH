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
    [Route("api/ConceptoNomina")]
    public class ConceptoNominaController : Controller
    {
        private readonly SwTHDbContext db;

        public ConceptoNominaController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarConceptoNomina")]
        public async Task<List<ConceptoNomina>> ListarConceptoNomina()
        {
            try
            {
                return await db.ConceptoNomina.Include(x => x.ProcesoNomina).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<ConceptoNomina>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet]
        [HttpPost("ObtenerConceptoNomina")]
        public async Task<Response> ObtenerConceptoNomina([FromBody] ConceptoNomina ConceptoNomina)
        {
            try
            {
                var conceptoNomina = await db.ConceptoNomina.SingleOrDefaultAsync(m => m.IdConcepto == ConceptoNomina.IdConcepto);

                if (conceptoNomina == null)
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
                    Resultado = conceptoNomina,
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

        [HttpPost]
        [Route("EditarFormula")]
        public async Task<Response> EditarFormula([FromBody] ConceptoNomina ConceptoNomina)
        {
            try
            {
                if (await Existe(ConceptoNomina))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var ConceptoNominaActualizar = await db.ConceptoNomina.Where(x => x.IdConcepto == ConceptoNomina.IdConcepto).FirstOrDefaultAsync();
                if (ConceptoNominaActualizar == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                    };

                }

                ConceptoNominaActualizar.FormulaCalculo = ConceptoNomina.FormulaCalculo;
                db.ConceptoNomina.Update(ConceptoNominaActualizar);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Resultado = ConceptoNominaActualizar
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

        // PUT: api/BasesDatos/5
        [HttpPost]
        [Route("EditarConceptoNomina")]
        public async Task<Response> EditarConceptoNomina([FromBody] ConceptoNomina ConceptoNomina)
        {
            try
            {
                if (await Existe(ConceptoNomina))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var ConceptoNominaActualizar = await db.ConceptoNomina.Where(x => x.IdConcepto == ConceptoNomina.IdConcepto).FirstOrDefaultAsync();
                if (ConceptoNominaActualizar == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                    };

                }

                ConceptoNominaActualizar.IdProceso = ConceptoNomina.IdProceso;
                ConceptoNominaActualizar.Codigo = ConceptoNomina.Codigo;
                ConceptoNominaActualizar.Descripcion = ConceptoNomina.Descripcion;
                ConceptoNominaActualizar.TipoConcepto = ConceptoNomina.TipoConcepto;
                ConceptoNominaActualizar.TipoCalculo = ConceptoNomina.TipoCalculo;
                ConceptoNominaActualizar.NivelAcumulacion = ConceptoNomina.NivelAcumulacion;
                ConceptoNominaActualizar.RegistroEn = ConceptoNomina.RegistroEn;
                ConceptoNominaActualizar.Estatus = ConceptoNomina.Estatus;
                ConceptoNominaActualizar.Abreviatura = ConceptoNomina.Abreviatura;
                ConceptoNominaActualizar.FormulaCalculo = ConceptoNomina.FormulaCalculo;

                db.ConceptoNomina.Update(ConceptoNominaActualizar);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Resultado = ConceptoNominaActualizar
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
        [Route("InsertarConceptoNomina")]
        public async Task<Response> PostConceptoNomina([FromBody] ConceptoNomina ConceptoNomina)
        {
            try
            {

                if (!await Existe(ConceptoNomina))
                {
                    db.ConceptoNomina.Add(ConceptoNomina);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                        Resultado = ConceptoNomina,
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
        [Route("EliminarConceptoNomina")]
        public async Task<Response> EliminarConceptoNomina([FromBody]ConceptoNomina ConceptoNomina)
        {
            try
            {
                var respuesta = await db.ConceptoNomina.Where(m => m.IdConcepto == ConceptoNomina.IdConcepto).FirstOrDefaultAsync();
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.ConceptoNomina.Remove(respuesta);
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

        private async Task<bool> Existe(ConceptoNomina ConceptoNomina)
        {
            var codigo = ConceptoNomina.Codigo;
            var ConceptoNominarespuesta = await db.ConceptoNomina.Where(p => p.Codigo == codigo).FirstOrDefaultAsync();

            if (ConceptoNominarespuesta == null || ConceptoNominarespuesta.IdConcepto == ConceptoNomina.IdConcepto)
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