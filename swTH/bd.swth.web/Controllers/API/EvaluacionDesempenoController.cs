using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.log.guardar.Servicios;
using bd.log.guardar.Enumeradores;
using Microsoft.EntityFrameworkCore;
using bd.log.guardar.ObjectTranfer;
using bd.swth.entidades.Enumeradores;
using bd.swth.entidades.Utils;
using bd.swth.entidades.ViewModels;
using bd.swth.entidades.Constantes;

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
                            lista.IdJefe = jefe.IdEmpleado;
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

        [HttpPost]
        [Route("Actividades")]
        public async Task<ViewModelEvaluador> Actividades([FromBody] ViewModelEvaluador viewModelEvaluador)
        {
            try
            {
                var DatosBasicos = new ViewModelEvaluador();
                // busca las actividades del puesto
                var Lista = await db.ActividadesEsenciales
                       .Where(act => db.IndiceOcupacionalActividadesEsenciales
                                       .Where(y => y.IndiceOcupacional.IdIndiceOcupacional == viewModelEvaluador.IdIndiceOcupacional)
                                       .Select(ioac => ioac.IdActividadesEsenciales)
                                       .Contains(act.IdActividadesEsenciales))
                              .ToListAsync();
                DatosBasicos.ListaActividad = Lista;
                return DatosBasicos;
            }
            catch (Exception)
            {
                return new ViewModelEvaluador();
            }


        }
        [HttpPost]
        [Route("Insertarctividades")]
        public async Task<Response> Insertarctividades([FromBody] ViewModelEvaluador viewModelEvaluador)
        {
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {
                    var listaEvaluador1 = new List<EvaluacionActividadesPuestoTrabajo>();
                    var cont = 0;
                    foreach (var item in viewModelEvaluador.ListaIndicadores)
                    {
                        var indicador = new Indicador
                        {
                            Nombre = item

                        };
                        var IndicadoresInsertarda = await db.Indicador.AddAsync(indicador);
                        await db.SaveChangesAsync();

                        var evaluacionActividadesPuestoTrabajo = new EvaluacionActividadesPuestoTrabajo();
                        evaluacionActividadesPuestoTrabajo.MetaPeriodo = Convert.ToInt32(viewModelEvaluador.ListaMetaPeriodo[cont]);
                        evaluacionActividadesPuestoTrabajo.ActividadesCumplidas = Convert.ToInt32(viewModelEvaluador.ListaActividadescumplidos[cont]);
                        evaluacionActividadesPuestoTrabajo.IdActividadesEsenciales = Convert.ToInt32(viewModelEvaluador.ListaActividades[cont]);
                        evaluacionActividadesPuestoTrabajo.IdIndicador = IndicadoresInsertarda.Entity.IdIndicador;
                        evaluacionActividadesPuestoTrabajo.IdEval001 = viewModelEvaluador.IdEval001;
                        evaluacionActividadesPuestoTrabajo.DescripcionActividad = "";
                        listaEvaluador1.Add(evaluacionActividadesPuestoTrabajo);
                        cont++;
                    }
                    await db.EvaluacionActividadesPuestoTrabajo.AddRangeAsync(listaEvaluador1);
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio
                    };

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.Error,
                    };
                }
            }
        }
        [HttpPost]
        [Route("InsertarEval001")]
        public async Task<Response> InsertarEval001([FromBody] ViewModelEvaluador viewModelEvaluador)
        {
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {
                    var a = await db.Evaluador.Where(x => x.IdEmpleado == viewModelEvaluador.IdJefe).FirstOrDefaultAsync();

                    if (a != null)
                    {
                        var eval001 = new Eval001
                        {
                            IdEmpleadoEvaluado = viewModelEvaluador.IdEmpleado,
                            IdEvaluador = a.IdEmpleado,
                            FechaEvaluacionDesde = viewModelEvaluador.Desde,
                            FechaEvaluacionHasta = viewModelEvaluador.Hasta,
                            FechaRegistro = DateTime.Now
                        };
                        var eval001Insertarda = await db.Eval001.AddAsync(eval001);
                        await db.SaveChangesAsync();
                        var Ideval001 = eval001Insertarda.Entity.IdEval001;
                        transaction.Commit();
                        return new Response
                        {
                            Resultado = Ideval001,
                            IsSuccess = true,
                            Message = Mensaje.Satisfactorio
                        };
                    }
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.Error
                    };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.Error,
                    };
                }
            }
        }
        [HttpPost]
        [Route("InsertarConocimientos")]
        public async Task<Response> InsertarConocimientos([FromBody] ViewModelEvaluador viewModelEvaluador)
        {
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {
                    var listaEvaluador1 = new List<EvaluacionConocimiento>();
                    var cont = 0;
                    foreach (var item in viewModelEvaluador.ConocimientosEsenciales)
                    {
                        var evaluacionConocimiento = new EvaluacionConocimiento();
                        evaluacionConocimiento.IdNivelConocimiento = Convert.ToInt32(item);
                        evaluacionConocimiento.IdAreaConocimiento = Convert.ToInt32(viewModelEvaluador.IdAreaConocimiento[cont]);
                        evaluacionConocimiento.IdEval001 = viewModelEvaluador.IdEval001;
                        listaEvaluador1.Add(evaluacionConocimiento);
                        cont++;

                    }
                    await db.EvaluacionConocimiento.AddRangeAsync(listaEvaluador1);
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio
                    };

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.Error,
                    };
                }
            }
        }
        [HttpPost]
        [Route("InsertarCompetenciasTecnicas")]
        public async Task<Response> InsertarCompetenciasTecnicas([FromBody] ViewModelEvaluador viewModelEvaluador)
        {
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {
                    var listaEvaluador1 = new List<EvaluacionCompetenciasTecnicasPuesto>();
                    var cont = 0;
                    foreach (var item in viewModelEvaluador.IdComportamientoObervable)
                    {
                        var evaluacionCompetenciasTecnicasPuesto = new EvaluacionCompetenciasTecnicasPuesto();
                        evaluacionCompetenciasTecnicasPuesto.IdComportamientoObservable = Convert.ToInt32(viewModelEvaluador.IdComportamientoObervable[cont]);
                        evaluacionCompetenciasTecnicasPuesto.IdNivelDesarrollo = Convert.ToInt32(viewModelEvaluador.IdNivelDesarrollos[cont]);
                        evaluacionCompetenciasTecnicasPuesto.IdEval001 = viewModelEvaluador.IdEval001;
                        listaEvaluador1.Add(evaluacionCompetenciasTecnicasPuesto);
                        cont++;
                    }
                    await db.EvaluacionCompetenciasTecnicasPuesto.AddRangeAsync(listaEvaluador1);
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio
                    };

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.Error,
                    };
                }
            }
        }
        [HttpPost]
        [Route("InsertarCompetenciasUniversales")]
        public async Task<Response> InsertarCompetenciasUniversales([FromBody] ViewModelEvaluador viewModelEvaluador)
        {
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {
                    var listaEvaluador1 = new List<EvaluacionCompetenciasUniversales>();
                    var cont = 0;
                    foreach (var item in viewModelEvaluador.IdComportamientoObervable)
                    {
                        var evaluacionCompetenciasTecnicasPuesto = new EvaluacionCompetenciasUniversales();
                        evaluacionCompetenciasTecnicasPuesto.IdComportamientoObservable = Convert.ToInt32(viewModelEvaluador.IdComportamientoObervable[cont]);
                        evaluacionCompetenciasTecnicasPuesto.IdFrecuenciaAplicacion = Convert.ToInt32(viewModelEvaluador.IdNivelDesarrollos[cont]);
                        evaluacionCompetenciasTecnicasPuesto.IdEval001 = viewModelEvaluador.IdEval001;
                        listaEvaluador1.Add(evaluacionCompetenciasTecnicasPuesto);
                        cont++;
                    }
                    await db.EvaluacionCompetenciasUniversales.AddRangeAsync(listaEvaluador1);
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio
                    };

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.Error,
                    };
                }
            }
        }
    }
}