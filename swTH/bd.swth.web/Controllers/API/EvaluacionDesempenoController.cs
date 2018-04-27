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

        [HttpPost]
        [Route("ListarEmpleadosJefes")]
        public async Task<List<ViewModelEvaluacionDesempeno>> ListarEmpleadosPorJefes([FromBody] ViewModelEvaluacionDesempeno viewModelEvaluacionDesempeno)
        {
            var DatosBasicos = new List<ViewModelEvaluacionDesempeno>();
            try
            {
                var jefe = db.Empleado.Where(x => x.Activo == true && x.EsJefe == true && x.NombreUsuario == viewModelEvaluacionDesempeno.NombreUsuario).FirstOrDefault();
                if (jefe != null)
                {

                    var a = await db.IndiceOcupacionalModalidadPartida.ToListAsync();
                    foreach (var item in a)
                    {
                        var lista = await db.Empleado
                                        .Where(x => x.IdEmpleado == item.IdEmpleado && x.Activo == true && x.IdDependencia == jefe.IdDependencia)
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
                            var ModalidadPartida = await db.ModalidadPartida.Where(x => x.Nombre == Constantes.PartidaOcupada).FirstOrDefaultAsync();
                            var b = db.IndiceOcupacional.Where(y => y.IdIndiceOcupacional == item.IdIndiceOcupacional && y.IdModalidadPartida == ModalidadPartida.IdModalidadPartida).FirstOrDefault();
                            if (b != null)
                            {

                                lista.ManualPuesto = db.ManualPuesto.Where(y => y.IdManualPuesto == b.IdManualPuesto).FirstOrDefault().Nombre;
                                DatosBasicos.Add(lista);
                            }
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
        [HttpPost]
        [Route("Evaluar")]
        public async Task<ViewModelEvaluador> Evaluar([FromBody] ViewModelEvaluador viewModelEvaluador)
        {
            var DatosBasicos = new ViewModelEvaluador();
            var DatosBasicos1 = new ViewModelEvaluador();
            try
            {
                var jefe = db.Empleado.Where(x => x.Activo == true && x.EsJefe == true && x.NombreUsuario == viewModelEvaluador.NombreUsuario).FirstOrDefault();
                var persona = db.Persona.Where(x => x.IdPersona == jefe.IdPersona).FirstOrDefault();
                var titulo = db.PersonaEstudio.Include(x => x.Titulo).Where(x => x.IdPersona == jefe.IdPersona).FirstOrDefault();
                if (true)
                {

                    var a = await db.IndiceOcupacionalModalidadPartida.Where(x => x.IdEmpleado == viewModelEvaluador.IdEmpleado).FirstOrDefaultAsync();

                    var lista = await db.Empleado
                                    .Where(x => x.IdEmpleado == a.IdEmpleado && x.Activo == true)
                                    .Select(x => new ViewModelEvaluador
                                    {
                                        IdEmpleado = x.IdEmpleado,
                                        NombreApellido = string.Format("{0} {1}", x.Persona.Nombres, x.Persona.Apellidos)

                                    })
                                    .FirstOrDefaultAsync();
                    if (lista != null)
                    {
                        var ModalidadPartida = await db.ModalidadPartida.Where(x => x.Nombre == Constantes.PartidaOcupada).FirstOrDefaultAsync();
                        var b = db.IndiceOcupacional.Where(y => y.IdIndiceOcupacional == a.IdIndiceOcupacional && y.IdModalidadPartida == ModalidadPartida.IdModalidadPartida).FirstOrDefault();
                        if (b != null)
                        {
                            // busca las actividades del puesto
                            var Lista = await db.ActividadesEsenciales
                                   .Where(act => db.IndiceOcupacionalActividadesEsenciales
                                                   .Where(y => y.IndiceOcupacional.IdIndiceOcupacional == b.IdIndiceOcupacional)
                                                   .Select(ioac => ioac.IdActividadesEsenciales)
                                                   .Contains(act.IdActividadesEsenciales))
                                          .ToListAsync();

                            lista.Puesto = db.ManualPuesto.Where(y => y.IdManualPuesto == b.IdManualPuesto).FirstOrDefault().Nombre;

                            var datos = jefe.Persona.Nombres;
                            lista.DatosJefe = persona.Nombres + "" + persona.Apellidos;
                            lista.Titulo = titulo.Titulo.Nombre;
                            lista.ListaActividad = Lista;
                            lista.IdIndiceOcupacional = a.IdIndiceOcupacional;
                        }
                        
                    }
                    DatosBasicos1 = lista;
                }

                return DatosBasicos = DatosBasicos1;


            }
            catch (Exception ex)
            {
                return new ViewModelEvaluador();
            }
        }
    }
}