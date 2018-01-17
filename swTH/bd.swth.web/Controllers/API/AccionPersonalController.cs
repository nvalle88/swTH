using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using bd.swth.entidades.Negocio;
using bd.swth.datos;
using Microsoft.EntityFrameworkCore;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.swth.entidades.Enumeradores;
using bd.swth.entidades.Utils;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/AccionPersonal")]
    public class AccionPersonalController : Controller
    {
        private readonly SwTHDbContext db;

        public AccionPersonalController(SwTHDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("ListarAccionPersonal")]
        public async Task<List<AccionPersonal>> ListarAccionPersonal()
        {
            try
            {
                var DiaActual = DateTime.Now;
                var lista= await db.AccionPersonal
                            .Where(x => x.Fecha.Year == DiaActual.Year && x.Fecha.Month==DiaActual.Month)
                            .Include(x => x.Empleado).ThenInclude(x => x.Persona)
                            .Include(x => x.TipoAccionPersonal)
                            .ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
               return new List<AccionPersonal>();
            }
        }

        [HttpPost]
        [Route("CrearAccionPersonal")]
        public async Task<Response> CrearAccionPersonal([FromBody] AccionPersonal accionPersonal)
        {
            try
            {
                accionPersonal.NoDias = Convert.ToInt32(accionPersonal.FechaRige.Subtract(accionPersonal.FechaRigeHasta));
                db.AccionPersonal.Add(accionPersonal);
                await db.SaveChangesAsync();
                return new Response { IsSuccess = true, Resultado = accionPersonal };
            }
            catch (Exception)
            {
                return new Response { IsSuccess = false };
            }
        }
    }
}