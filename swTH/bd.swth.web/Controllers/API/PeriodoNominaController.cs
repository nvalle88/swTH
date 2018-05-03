using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/PeriodoNomina")]
    public class PeriodoNominaController : Controller
    {
        private readonly SwTHDbContext db;

        public PeriodoNominaController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarPeriodoNomina")]
        public async Task<List<PeriodoNomina>> ListarPeriodoNomina()
        {
            try
            {
                return await db.PeriodoNomina.Include(x => x.ProcesoNomina).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<PeriodoNomina>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet]
        [HttpPost("ObtenerPeriodoNomina")]
        public async Task<Response> ObtenerPeriodoNomina([FromBody] PeriodoNomina PeriodoNomina)
        {
            try
            {
                var periodoNomina = await db.PeriodoNomina.SingleOrDefaultAsync(m => m.IdPeriodo == PeriodoNomina.IdPeriodo);

                if (periodoNomina == null)
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
                    Resultado = periodoNomina,
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
        [Route("EditarPeriodoNomina")]
        public async Task<Response> EditarPeriodoNomina([FromBody] PeriodoNomina PeriodoNomina)
        {
            try
            {
                if (await Existe(PeriodoNomina))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var PeriodoNominaActualizar = await db.PeriodoNomina.Where(x => x.IdPeriodo == PeriodoNomina.IdPeriodo).FirstOrDefaultAsync();
                if (PeriodoNominaActualizar == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                    };

                }

                PeriodoNominaActualizar.IdProceso = PeriodoNomina.IdProceso;
                PeriodoNominaActualizar.FechaInicio = PeriodoNomina.FechaInicio;
                PeriodoNominaActualizar.FechaFin = PeriodoNomina.FechaFin;
                PeriodoNominaActualizar.Descripcion = PeriodoNomina.Descripcion;
                PeriodoNominaActualizar.Abierto = PeriodoNomina.Abierto;
                db.PeriodoNomina.Update(PeriodoNominaActualizar);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Resultado = PeriodoNominaActualizar
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
        [Route("InsertarPeriodoNomina")]
        public async Task<Response> PostPeriodoNomina([FromBody] PeriodoNomina PeriodoNomina)
        {
            try
            {

                if (!await Existe(PeriodoNomina))
                {
                    db.PeriodoNomina.Add(PeriodoNomina);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                        Resultado = PeriodoNomina,
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
        [Route("EliminarPeriodoNomina")]
        public async Task<Response> EliminarPeriodoNomina([FromBody]PeriodoNomina PeriodoNomina)
        {
            try
            {
                var respuesta = await db.PeriodoNomina.Where(m => m.IdPeriodo == PeriodoNomina.IdPeriodo).FirstOrDefaultAsync();
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.PeriodoNomina.Remove(respuesta);
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

        private async Task<bool> Existe(PeriodoNomina PeriodoNomina)
        {
            var descripcion = PeriodoNomina.Descripcion;
            var PeriodoNominarespuesta = await db.PeriodoNomina.Where(p => p.Descripcion == descripcion).FirstOrDefaultAsync();

            if (PeriodoNominarespuesta == null || PeriodoNominarespuesta.IdPeriodo == PeriodoNomina.IdPeriodo)
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