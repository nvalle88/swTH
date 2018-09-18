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
using MoreLinq;

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
        [HttpPost]
        [Route("ListarEstadoInduccionEmpleados")]
        public async Task<List<InduccionViewModel>> ListarEstadoInduccionEmpleados(UsuarioViewModel usuario)
        {
            var lista = new List<InduccionViewModel>();
            
            try
            {
                var empleadoActual = db.Empleado.Include(d => d.Dependencia)
                    .Where(x => x.NombreUsuario == usuario.NombreUsuarioActual)
                    .FirstOrDefault()
                ;

                DistributivosController ctrlDistributivo = new DistributivosController(db);

                var distributivo = await ctrlDistributivo.ObtenerDistributivoFormal();
                
                lista = distributivo
                    .Select(s => new InduccionViewModel
                    {
                        IdEmpleado = s.IdEmpleado,
                        Nombres = s.Empleado.Persona.Nombres + " " + s.Empleado.Persona.Apellidos,
                        FechaIngreso = s.FechaInicio,
                        EstadoInduccion = (db.Induccion.Any(w => w.IdEmpleado == s.IdEmpleado))
                                ? ConstantesEstadoInduccion.InduccionFinalizada
                                : ConstantesEstadoInduccion.InduccionNoFinalizada
                            ,
                        ValorCompletado = ConstantesEstadoInduccion.InduccionFinalizada,
                        NombreDependencia = s.IndiceOcupacionalModalidadPartida.Dependencia.Nombre,
                        NombreRol = s.IndiceOcupacionalModalidadPartida.IndiceOcupacional.RolPuesto.Nombre
                    }
                    )
                    .OrderByDescending(o=>o.FechaIngreso)
                    .ToList();

                return lista;
            }
            catch (Exception ex)
            {
                return lista;
            }
        }


        


    }
}