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
    [Route("api/ProcesoNomina")]
    public class ProcesoNominaController : Controller
    {
        private readonly SwTHDbContext db;

        public ProcesoNominaController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarProcesoNomina")]
        public async Task<List<ProcesoNomina>> ListarProcesoNomina()
        {
            try
            {
                return await db.ProcesoNomina.OrderBy(x => x.Codigo).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<ProcesoNomina>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet]
        [HttpPost("ObtenerProcesoNomina")]
        public async Task<Response> ObtenerProcesoNomina([FromBody] ProcesoNomina ProcesoNomina)
        {
            try
            {
                var procesoNomina = await db.ProcesoNomina.SingleOrDefaultAsync(m => m.IdProceso == ProcesoNomina.IdProceso);

                if (ProcesoNomina == null)
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
                    Resultado = procesoNomina,
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
        [Route("EditarProcesoNomina")]
        public async Task<Response> EditarProcesoNomina([FromBody] ProcesoNomina ProcesoNomina)
        {
            try
            {
                if (await Existe(ProcesoNomina))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var ProcesoNominaActualizar = await db.ProcesoNomina.Where(x => x.IdProceso == ProcesoNomina.IdProceso).FirstOrDefaultAsync();
                if (ProcesoNominaActualizar == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                    };

                }

                ProcesoNominaActualizar.Codigo = ProcesoNomina.Codigo;
                ProcesoNominaActualizar.Descripcion = ProcesoNomina.Descripcion;
                db.ProcesoNomina.Update(ProcesoNominaActualizar);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Resultado = ProcesoNominaActualizar
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
        [Route("InsertarProcesoNomina")]
        public async Task<Response> PostProcesoNomina([FromBody] ProcesoNomina ProcesoNomina)
        {
            try
            {

                if (!await Existe(ProcesoNomina))
                {
                    db.ProcesoNomina.Add(ProcesoNomina);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                        Resultado = ProcesoNomina,
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
        [Route("EliminarProcesoNomina")]
        public async Task<Response> EliminarProcesoNomina([FromBody]ProcesoNomina ProcesoNomina)
        {
            try
            {
                var respuesta = await db.ProcesoNomina.Where(m => m.IdProceso == ProcesoNomina.IdProceso).FirstOrDefaultAsync();
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.ProcesoNomina.Remove(respuesta);
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

        private async Task<bool> Existe(ProcesoNomina ProcesoNomina)
        {
            var bdd = ProcesoNomina.Codigo.ToUpper().TrimEnd().TrimStart();
            var ProcesoNominarespuesta = await db.ProcesoNomina.Where(p => p.Codigo.ToUpper().TrimStart().TrimEnd() == bdd).FirstOrDefaultAsync();

            if (ProcesoNominarespuesta == null || ProcesoNominarespuesta.IdProceso == ProcesoNomina.IdProceso)
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