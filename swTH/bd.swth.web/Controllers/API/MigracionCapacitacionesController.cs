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
    [Route("api/MigracionCapacitaciones")]
    public class MigracionCapacitacionesController : Controller
    {

        private readonly SwTHDbContext db;

        public MigracionCapacitacionesController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarCalculoNomina")]
        public async Task<List<CalculoNomina>> ListarCalculoNomina()
        {
            try
            {
                return await db.CalculoNomina.Include(x => x.PeriodoNomina).Include(x => x.ProcesoNomina).ToListAsync();
            }
            catch (Exception)
            {
                return new List<CalculoNomina>();
            }
        }

        [HttpPost]
        [Route("LimpiarReportados")]
        public async Task<Response> LimpiarReportados([FromBody] CalculoNomina calculoNomina)
        {
            try
            {
                var listadoBorrar = await db.ReportadoNomina.Where(x => x.IdCalculoNomina == calculoNomina.IdCalculoNomina).ToListAsync();
                db.ReportadoNomina.RemoveRange(listadoBorrar);
                await db.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                };
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet]
        [HttpPost("ObtenerCalculoNomina")]
        public async Task<Response> ObtenerCalculoNomina([FromBody] CalculoNomina CalculoNomina)
        {
            try
            {
                var calculoNomina = await db.CalculoNomina.SingleOrDefaultAsync(m => m.IdCalculoNomina == CalculoNomina.IdCalculoNomina);

                if (calculoNomina == null)
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
                    Resultado = calculoNomina,
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
        public async Task<Response> EditarCalculoNomina([FromBody] CalculoNomina CalculoNomina)
        {
            try
            {
                if (await Existe(CalculoNomina))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var CalculoNominaActualizar = await db.CalculoNomina.Where(x => x.IdCalculoNomina == CalculoNomina.IdCalculoNomina).FirstOrDefaultAsync();
                if (CalculoNominaActualizar == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                    };

                }
                CalculoNominaActualizar.Descripcion = CalculoNomina.Descripcion;
                CalculoNominaActualizar.IdPeriodo = CalculoNomina.IdPeriodo;
                CalculoNominaActualizar.IdProceso = CalculoNomina.IdProceso;
                CalculoNominaActualizar.Automatico = CalculoNomina.Automatico;
                CalculoNominaActualizar.Reportado = CalculoNomina.Reportado;
                CalculoNominaActualizar.EmpleadoActivo = CalculoNomina.EmpleadoActivo;
                CalculoNominaActualizar.EmpleadoPasivo = CalculoNomina.EmpleadoPasivo;
                db.CalculoNomina.Update(CalculoNominaActualizar);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Resultado = CalculoNominaActualizar
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

        [HttpPost]
        [Route("ListarReportados")]
        public async Task<List<ReportadoNomina>> ListarReportados([FromBody] CalculoNomina CalculoNomina)
        {
            try
            {
                return await db.ReportadoNomina.Where(x => x.IdCalculoNomina == CalculoNomina.IdCalculoNomina).ToListAsync();
            }
            catch (Exception)
            {
                return new List<ReportadoNomina>();
            }
        }

        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarCalculoNomina")]
        public async Task<Response> PostCalculoNomina([FromBody] CalculoNomina CalculoNomina)
        {
            try
            {

                if (!await Existe(CalculoNomina))
                {
                    db.CalculoNomina.Add(CalculoNomina);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                        Resultado = CalculoNomina,
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
        [Route("EliminarCalculoNomina")]
        public async Task<Response> EliminarCalculoNomina([FromBody]CalculoNomina CalculoNomina)
        {
            try
            {
                var respuesta = await db.CalculoNomina.Where(m => m.IdCalculoNomina == CalculoNomina.IdCalculoNomina).FirstOrDefaultAsync();
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.CalculoNomina.Remove(respuesta);
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

        private async Task<bool> Existe(CalculoNomina CalculoNomina)
        {
            var periodo = CalculoNomina.IdPeriodo;
            var proceso = CalculoNomina.IdProceso;
            var CalculoNominarespuesta = await db.CalculoNomina.Where(p => p.IdProceso == proceso && p.IdPeriodo == periodo).FirstOrDefaultAsync();

            if (CalculoNominarespuesta == null || CalculoNominarespuesta.IdCalculoNomina == CalculoNomina.IdCalculoNomina)
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