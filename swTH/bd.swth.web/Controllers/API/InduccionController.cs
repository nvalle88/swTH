using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.swth.servicios.Interfaces;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.swth.entidades.Enumeradores;
using bd.swth.entidades.Utils;
using bd.log.guardar.Enumeradores;
using bd.swth.entidades.ObjectTransfer;
using System.IO;
using bd.swth.entidades.ViewModels;
using bd.swth.entidades.Constantes;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Induccion")]
    public class InduccionController : Controller
    {
        private readonly SwTHDbContext db;



        public InduccionController(SwTHDbContext db)
        {
            this.db = db;
        }


        // Get: api/Induccion
        [HttpGet]
        [Route("ListarEstadoInduccionEmpleados")]
        public async Task<List<InduccionViewModel>> ListarEstadoInduccionEmpleados()
        {
            var lista = new List<InduccionViewModel>();
            
            try
            {
                lista = await db.Empleado
                    .Select(x => new InduccionViewModel
                        {
                            IdEmpleado = x.IdEmpleado,
                            Nombres = x.Persona.Nombres + " " + x.Persona.Apellidos,
                            FechaIngreso = x.FechaIngreso.Date,

                            EstadoInduccion = (db.Induccion.Any(w=>w.IdEmpleado == x.IdEmpleado))
                                ? ConstantesEstadoInduccion.InduccionFinalizada 
                                : ConstantesEstadoInduccion.InduccionNoFinalizada
                            ,

                            ValorCompletado = ConstantesEstadoInduccion.InduccionFinalizada
                    } 
                    )
                    .ToListAsync();

                return lista;
            }
            catch (Exception ex)
            {
                return lista;
            }
        }


        


    }
}