using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bd.swth.datos;
using bd.swth.entidades.Constantes;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/EvaluacionDesempeno")]
    public class EvaluacionDesempenoController : Controller
    {
        private readonly SwTHDbContext db;

        public EvaluacionDesempenoController(SwTHDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("ListarEmpleados")]
        public async Task<List<ViewModelEvaluacionDesempeno>> GetEmpleados()
        {
            try
            {
                //var lista = await db.Empleado.Include(x => x.Persona).Include(x => x.Dependencia).Include(x=>x.DatosBancarios).Include(x => x.IndiceOcupacionalModalidadPartida).ThenInclude(x => x.IndiceOcupacional).ThenInclude(x => x.RolPuesto).OrderBy(x => x.FechaIngreso).ToListAsync();

                //var empleado = await db.ModalidadPartida.
                var DatosBasicos = new List<ViewModelEvaluacionDesempeno>();
                var a = await db.IndiceOcupacionalModalidadPartida.ToListAsync();
                foreach (var item in a)
                {
                    var lista = await db.Empleado
                                    .Where(x => x.IdEmpleado == item.IdEmpleado && x.Activo==true)
                                    .Select(x => new ViewModelEvaluacionDesempeno
                                    {
                                        IdEmpleado = x.IdEmpleado,
                                        IdPersona = x.Persona.IdPersona,
                                        NombreApellido = string.Format("{0} {1}", x.Persona.Nombres, x.Persona.Apellidos),
                                        Dependencia = x.Dependencia == null ? "No asignado" : x.Dependencia.Nombre,
                                        IdDependencia = x.Dependencia.IdDependencia,
                                        Identificacion = x.Persona.Identificacion,
                                    })
                                    .FirstOrDefaultAsync();
                    if (lista != null)
                    {
                        var jefe = db.Empleado.Where(x => x.Activo==true && x.EsJefe == true && x.IdDependencia == lista.IdDependencia).FirstOrDefault();
                        if (jefe != null)
                        {
                            var dato = db.Persona.Where(x => x.IdPersona == jefe.IdPersona).FirstOrDefault();
                            lista.DatosJefe = dato.Nombres +""+dato.Apellidos;
                        }
                        var ModalidadPartida = await db.ModalidadPartida.Where(x => x.Nombre == Constantes.PartidaOcupada).FirstOrDefaultAsync();
                        var b = db.IndiceOcupacional.Where(y => y.IdIndiceOcupacional == item.IdIndiceOcupacional && y.IdModalidadPartida == ModalidadPartida.IdModalidadPartida).FirstOrDefault();
                        if (b != null)
                        {
                            
                            lista.ManualPuesto = db.ManualPuesto.Where(y => y.IdManualPuesto == b.IdManualPuesto).FirstOrDefault().Nombre;
                            DatosBasicos.Add(lista);
                        }
                    }
                }
                return DatosBasicos;

            }
            catch (Exception ex)
            {
                return new List<ViewModelEvaluacionDesempeno>();
            }
        }
    }
}