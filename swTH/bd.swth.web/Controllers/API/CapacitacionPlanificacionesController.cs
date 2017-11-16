using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using bd.swth.datos;
using bd.swth.entidades.ObjectTransfer;
using Microsoft.EntityFrameworkCore;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/CapacitacionPlanificaciones")]
    public class CapacitacionPlanificacionesController : Controller
    {

        private readonly SwTHDbContext db;

        public CapacitacionPlanificacionesController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/AccionesPersonal
        [HttpPost]
        [Route("ListarCapacitacionPlanificaciones")]
        public async Task<IEnumerable<CapacitacionPlanificacionViewModel>> CapacitacionPlanificacionesAsync(CapacitacioPlanificacionParametros capacitacioPlanificacionParametros )
        {
            var lista =await db.CapacitacionPlanificacion
                .Where(x => x.Fecha.Year == capacitacioPlanificacionParametros.Fecha.Year
                       && x.CapacitacionTemario.IdCapacitacionTemario == capacitacioPlanificacionParametros.IdCapacitacionTemario)
                .OrderBy(x=>x.Fecha).ThenBy(x=>x.CapacitacionTemario.Tema)
                .ToListAsync();

            var listaSalida = new List<CapacitacionPlanificacionViewModel>();

            foreach (var item in lista)
            {
                listaSalida.Add(new CapacitacionPlanificacionViewModel
                {
                 CapacitacionModalidad=item.CapacitacionModalidad.Descripcion,
                 CapacitacionTemario=item.CapacitacionTemario.Tema,
                 Dependencia=item.Empleado.Dependencia.Nombre,
                 Fecha=item.Fecha,
                 IdCapacitacionPlanificacion=item.IdCapacitacionPlanificacion,
                 IdentificacionEmpleado=item.Empleado.Persona.Identificacion,
                 NombreApellido=string.Format("{0} {1}",item.Empleado.Persona.Nombres, item.Empleado.Persona.Apellidos),
                 NumeroHoras=item.NumeroHoras,
                 Presupuesto=item.Presupuesto,
                 Sucurzal=item.Empleado.Dependencia.Sucursal.Nombre,
                });
                
            }

            return listaSalida;
        }
    }
}