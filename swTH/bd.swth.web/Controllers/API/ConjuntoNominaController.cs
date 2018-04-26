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
    [Route("api/ConjuntoNomina")]
    public class ConjuntoNominaController : Controller
    {
        
        private readonly SwTHDbContext db;

        public ConjuntoNominaController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarConjuntoNomina")]
        public async Task<List<ConjuntoNomina>> ListarConjuntoNomina()
        {
            try
            {
                return await db.ConjuntoNomina.OrderBy(x => x.Codigo).Include(x=>x.TipoConjunto).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<ConjuntoNomina>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet]
        [HttpPost("ObtenerConjuntoNomina")]
        public async Task<Response> ObtenerConjuntoNomina([FromBody] ConjuntoNomina ConjuntoNomina)
        {
            try
            {
                var conjuntoNomina = await db.ConjuntoNomina.SingleOrDefaultAsync(m => m.IdConjunto == ConjuntoNomina.IdConjunto);

                if (conjuntoNomina == null)
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
                    Resultado = conjuntoNomina,
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
        [Route("EditarConjuntoNomina")]
        public async Task<Response> EditarConjuntoNomina([FromBody] ConjuntoNomina ConjuntoNomina)
        {
            try
            {
                if (await Existe(ConjuntoNomina))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var ConjuntoNominaActualizar = await db.ConjuntoNomina.Where(x => x.IdConjunto == ConjuntoNomina.IdConjunto).FirstOrDefaultAsync();
                if (ConjuntoNominaActualizar == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                    };

                }

                ConjuntoNominaActualizar.Codigo = ConjuntoNomina.Codigo;
                ConjuntoNominaActualizar.Descripcion = ConjuntoNomina.Descripcion;
                ConjuntoNominaActualizar.IdTipoConjunto = ConjuntoNomina.IdTipoConjunto;
                db.ConjuntoNomina.Update(ConjuntoNominaActualizar);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Resultado = ConjuntoNominaActualizar
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
        [Route("InsertarConjuntoNomina")]
        public async Task<Response> PostConjuntoNomina([FromBody] ConjuntoNomina ConjuntoNomina)
        {
            try
            {

                if (!await Existe(ConjuntoNomina))
                {
                    db.ConjuntoNomina.Add(ConjuntoNomina);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                        Resultado = ConjuntoNomina,
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
        [Route("EliminarConjuntoNomina")]
        public async Task<Response> EliminarConjuntoNomina([FromBody]ConjuntoNomina ConjuntoNomina)
        {
            try
            {
                var respuesta = await db.ConjuntoNomina.Where(m => m.IdConjunto == ConjuntoNomina.IdConjunto).FirstOrDefaultAsync();
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.ConjuntoNomina.Remove(respuesta);
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

        private async Task<bool> Existe(ConjuntoNomina ConjuntoNomina)
        {
            var bdd = ConjuntoNomina.Codigo.ToUpper().TrimEnd().TrimStart();
            var ConjuntoNominarespuesta = await db.ConjuntoNomina.Where(p => p.Codigo.ToUpper().TrimStart().TrimEnd() == bdd).FirstOrDefaultAsync();

            if (ConjuntoNominarespuesta == null || ConjuntoNominarespuesta.IdConjunto == ConjuntoNomina.IdConjunto)
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