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
    [Route("api/TipoConjuntoNomina")]
    public class TipoConjuntoNominaController : Controller
    {

        private readonly SwTHDbContext db;

        public TipoConjuntoNominaController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarTipoConjuntoNomina")]
        public async Task<List<TipoConjuntoNomina>> ListarTipoConjuntoNomina()
        {
            try
            {
                return await db.TipoConjuntoNomina.OrderBy(x => x.Codigo).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<TipoConjuntoNomina>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet]
        [HttpPost("ObtenerTipoConjuntoNomina")]
        public async Task<Response> ObtenerTipoConjuntoNomina([FromBody] TipoConjuntoNomina tipoConjuntoNomina)
        {
            try
            {
                var TipoConjuntoNomina = await db.TipoConjuntoNomina.SingleOrDefaultAsync(m => m.IdTipoConjunto == tipoConjuntoNomina.IdTipoConjunto);

                if (TipoConjuntoNomina == null)
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
                    Resultado = TipoConjuntoNomina,
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
        [Route("EditarTipoConjuntoNomina")]
        public async Task<Response> EditarTipoConjuntoNomina([FromBody] TipoConjuntoNomina TipoConjuntoNomina)
        {
            try
            {
                if (await Existe(TipoConjuntoNomina))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var TipoConjuntoNominaActualizar = await db.TipoConjuntoNomina.Where(x => x.IdTipoConjunto == TipoConjuntoNomina.IdTipoConjunto).FirstOrDefaultAsync();
                if (TipoConjuntoNominaActualizar == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                    };

                }

                TipoConjuntoNominaActualizar.Codigo = TipoConjuntoNomina.Codigo;
                TipoConjuntoNominaActualizar.Descripcion = TipoConjuntoNomina.Descripcion;
                db.TipoConjuntoNomina.Update(TipoConjuntoNominaActualizar);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Resultado=TipoConjuntoNominaActualizar
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
        [Route("InsertarTipoConjuntoNomina")]
        public async Task<Response> PostTipoConjuntoNomina([FromBody] TipoConjuntoNomina TipoConjuntoNomina)
        {
            try
            {
               
                if (!await Existe(TipoConjuntoNomina))
                {
                    db.TipoConjuntoNomina.Add(TipoConjuntoNomina);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                        Resultado = TipoConjuntoNomina,
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
        [Route("EliminarTipoConjuntoNomina")]
        public async Task<Response> EliminarTipoConjuntoNomina([FromBody]TipoConjuntoNomina tipoConjuntoNomina)
        {
            try
            {
                var respuesta = await db.TipoConjuntoNomina.Where(m => m.IdTipoConjunto == tipoConjuntoNomina.IdTipoConjunto).FirstOrDefaultAsync();
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.TipoConjuntoNomina.Remove(respuesta);
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

        private async Task<bool> Existe(TipoConjuntoNomina TipoConjuntoNomina)
        {
            var bdd = TipoConjuntoNomina.Codigo.ToUpper().TrimEnd().TrimStart();
            var TipoConjuntoNominarespuesta = await db.TipoConjuntoNomina.Where(p => p.Codigo.ToUpper().TrimStart().TrimEnd() == bdd).FirstOrDefaultAsync();

            if (TipoConjuntoNominarespuesta == null || TipoConjuntoNominarespuesta.IdTipoConjunto == TipoConjuntoNomina.IdTipoConjunto)
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