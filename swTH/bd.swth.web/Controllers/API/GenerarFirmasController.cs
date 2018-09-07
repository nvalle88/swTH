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
using MoreLinq;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/GenerarFirmas")]
    public class GenerarFirmasController : Controller
    {
        private readonly SwTHDbContext db;

        public GenerarFirmasController(SwTHDbContext db)
        {
            this.db = db;
        }

        /*

        // Post: api/GenerarFirmas
        [HttpPost]
        [Route("ObtenerDependenciasPorNumeroFirmas")]
        public async Task<List<Dependencia>> ObtenerDependenciasPorNumeroFirmas([FromBody] IdFiltrosViewModel filtro)
        {
            var lista = new List<Dependencia>();
            
            try
            {
               lista = await db.Dependencia.Where(w => w.IdSucursal == filtro.IdSucursal).ToListAsync();
                    
                return lista;
            }
            catch (Exception ex)
            {
                return lista;
            }
        }

        // Post: api/GenerarFirmas
        [HttpPost]
        [Route("ObtenerEmpleadosPorDependencias")]
        public async Task<List<IndiceOcupacionalModalidadPartida>> ObtenerEmpleadosPorDependencias([FromBody] IdFiltrosViewModel filtro)
        {
            var lista = new List<IndiceOcupacionalModalidadPartida>();

            try
            {
                lista = await db.IndiceOcupacionalModalidadPartida
                    .Include(i=>i.IndiceOcupacional).ThenInclude(t=>t.ManualPuesto)
                    .Include(t=>t.IndiceOcupacional).ThenInclude(t=>t.Dependencia)
                    .Include(i=>i.Empleado).ThenInclude(t=>t.Persona)
                    .Where(w => 
                        w.IndiceOcupacional.IdDependencia == filtro.IdDependencia
                        && w.Empleado.Activo == true
                    )
                    .OrderByDescending(o=>o.Fecha)
                    .DistinctBy(d=>d.IdEmpleado)
                    .ToAsyncEnumerable().ToList();

                return lista;
            }
            catch (Exception ex)
            {
                return lista;
            }
        }

        */
    }
}