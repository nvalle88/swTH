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
                    var aumento = viewModelEvaluador.PorcentajeAumento;
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
                        evaluacionActividadesPuestoTrabajo.PorcetajeCumplimiento = Convert.ToInt32(viewModelEvaluador.PorcentajeCumplido[cont]);
                        evaluacionActividadesPuestoTrabajo.NivelCumplimiento = Convert.ToInt32(viewModelEvaluador.NivelCumplimiento[cont]);
                        evaluacionActividadesPuestoTrabajo.Aumento = Convert.ToInt32(viewModelEvaluador.PorcentajeAumento);
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
        [Route("InsertarObservacion")]
        public async Task<Response> InsertarObservacion([FromBody] ViewModelEvaluador viewModelEvaluador)
        {
            try
            {
                var eval001 = await db.Eval001.Where(x => x.IdEval001 == viewModelEvaluador.IdEval001).FirstOrDefaultAsync();
                if (eval001 != null)
                {

                    eval001.Observaciones = viewModelEvaluador.Observaciones;
                    db.Eval001.Update(eval001);
                    await db.SaveChangesAsync();

                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                    };
                }
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Error,
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }
        [HttpPost]
        [Route("ObtenerObservacion")]
        public async Task<Response> ObtenerObservacion([FromBody] ViewModelEvaluador viewModelEvaluador)
        {
            try
            {
                var eval = new Eval001()
                {
                    IdEval001 = viewModelEvaluador.IdEval001,
                };
                if (eval.IdEval001 != 0)
                {
                    var existe = Existe(eval);
                    if (existe.IsSuccess)
                    {
                        return new Response
                        {
                            IsSuccess = true,
                            Resultado = existe.Resultado,
                            Message = Mensaje.Satisfactorio,
                        };
                    }
                }
                return new Response
                {
                    Resultado = null,
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
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
                        var evaluacionCompetenciasUniversales = new EvaluacionCompetenciasUniversales();
                        evaluacionCompetenciasUniversales.IdComportamientoObservable = Convert.ToInt32(viewModelEvaluador.IdComportamientoObervable[cont]);
                        evaluacionCompetenciasUniversales.IdFrecuenciaAplicacion = Convert.ToInt32(viewModelEvaluador.IdFrecuenciaAplicaciones[cont]);
                        evaluacionCompetenciasUniversales.IdEval001 = viewModelEvaluador.IdEval001;
                        listaEvaluador1.Add(evaluacionCompetenciasUniversales);
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
        [HttpPost]
        [Route("InsertarEquipoLiderazgo")]
        public async Task<Response> InsertarEquipoLiderazgo([FromBody] ViewModelEvaluador viewModelEvaluador)
        {
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {
                    var listaEvaluador1 = new List<EvaluacionTrabajoEquipoIniciativaLiderazgo>();
                    var cont = 0;
                    foreach (var item in viewModelEvaluador.IdComportamientoObervable)
                    {
                        var evaluacionCompetenciasLiderazgo = new EvaluacionTrabajoEquipoIniciativaLiderazgo();
                        evaluacionCompetenciasLiderazgo.IdComportamientoObservable = Convert.ToInt32(viewModelEvaluador.IdComportamientoObervable[cont]);
                        evaluacionCompetenciasLiderazgo.IdFrecuenciaAplicacion = Convert.ToInt32(viewModelEvaluador.IdFrecuenciaAplicaciones[cont]);
                        evaluacionCompetenciasLiderazgo.IdEval001 = viewModelEvaluador.IdEval001;
                        listaEvaluador1.Add(evaluacionCompetenciasLiderazgo);
                        cont++;
                    }
                    await db.EvaluacionTrabajoEquipoIniciativaLiderazgo.AddRangeAsync(listaEvaluador1);
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
        [Route("CalcularTotales")]
        public async Task<ViewModelEvaluador> CalcularTotales([FromBody] ViewModelEvaluador viewModelEvaluador)
        {
            try
            {
                var totales = new ViewModelEvaluador();
                var eval = new Eval001()
                {
                    IdEval001 = viewModelEvaluador.IdEval001,
                };
                var actividades = await db.EvaluacionActividadesPuestoTrabajo.Where(x => x.IdEval001 == eval.IdEval001).ToListAsync();
                if (actividades != null)
                {
                    double valor = 0, total = 0;
                    foreach (var item in actividades)
                    {
                        valor = Convert.ToDouble(item.PorcetajeCumplimiento) + Convert.ToDouble(item.Aumento);
                        total = total + valor;
                        totales.ActividadesTotal = total;
                    }
                }
                else
                {
                    totales.ActividadesTotal = 0;
                }
                var conocimiento = await db.EvaluacionConocimiento.Where(x => x.IdEval001 == eval.IdEval001).ToListAsync();
                if (conocimiento.Count!=0)
                {
                    //double valor = 0, total = 0;
                    double valor = 0, total = 0;
                    foreach (var item in conocimiento)
                    {
                        var a = await db.NivelConocimiento.Where(x => x.IdNivelConocimiento == item.IdNivelConocimiento).FirstOrDefaultAsync();
                        var b = a.Nombre;
                        
                        if (b == EvaluacionDesempeño.Sobresaliente) {
                             valor = 8;
                        }
                        if (b == EvaluacionDesempeño.MuyBueno)
                        {
                             valor = 6;
                        }
                        if (b == EvaluacionDesempeño.Bueno)
                        {
                             valor = 4;
                        }
                        if (b == EvaluacionDesempeño.Regular)
                        {
                             valor = 2;
                        }
                        if (b == EvaluacionDesempeño.Insuficiente)
                        {
                             valor = 0;
                        }
                        total = total + valor;
                    }
                    total = total / conocimiento.Count;
                    totales.TotalConocimientos = total;
                }
                else
                {
                    totales.TotalConocimientos = 0;
                }
                var competenciasTecnicas = await db.EvaluacionCompetenciasTecnicasPuesto.Where(x => x.IdEval001 == eval.IdEval001).ToListAsync();
                if (competenciasTecnicas.Count != 0)
                {
                    //double valor = 0, total = 0;
                    double valor = 0, total = 0;
                    foreach (var item in competenciasTecnicas)
                    {
                        var a = await db.NivelDesarrollo.Where(x => x.IdNivelDesarrollo == item.IdNivelDesarrollo).FirstOrDefaultAsync();
                        var b = a.Nombre;

                        if (b == EvaluacionDesempeño.AltamenteDesarrollada)
                        {
                            valor = 8;
                        }
                        if (b == EvaluacionDesempeño.Desarrollada)
                        {
                            valor = 6;
                        }
                        if (b == EvaluacionDesempeño.MedianamenteDesarrollada)
                        {
                            valor = 4;
                        }
                        if (b == EvaluacionDesempeño.PocoDesarrollada)
                        {
                            valor = 2;
                        }
                        if (b == EvaluacionDesempeño.NoDesarrollada)
                        {
                            valor = 0;
                        }
                        total = total + valor;
                    }
                    total = total / competenciasTecnicas.Count;
                    totales.TotalCompetenciasTecnicas = total;
                }
                else
                {
                    totales.TotalCompetenciasTecnicas = 0;
                }
                var competenciasUniversales = await db.EvaluacionCompetenciasUniversales.Where(x => x.IdEval001 == eval.IdEval001).ToListAsync();
                if (competenciasUniversales.Count != 0)
                {
                    //double valor = 0, total = 0;
                    double valor = 0, total = 0;
                    foreach (var item in competenciasUniversales)
                    {
                        var a = await db.FrecuenciaAplicacion.Where(x => x.IdFrecuenciaAplicacion == item.IdFrecuenciaAplicacion).FirstOrDefaultAsync();
                        var b = a.Nombre;

                        if (b == EvaluacionDesempeño.Siempre)
                        {
                            valor = 1.3;
                        }
                        if (b == EvaluacionDesempeño.Frecuentemente)
                        {
                            valor = 1;
                        }
                        if (b == EvaluacionDesempeño.Algunavez)
                        {
                            valor = 0.7;
                        }
                        if (b == EvaluacionDesempeño.Raravez)
                        {
                            valor = 0.3;
                        }
                        if (b == EvaluacionDesempeño.Nunca)
                        {
                            valor = 0;
                        }
                        total = total + valor;
                    }
                    total = total / competenciasUniversales.Count;
                    totales.TotalCompetenciasUniversales = total;
                }
                else {
                    totales.TotalCompetenciasUniversales = 0;
                }
                var TrabajoLiderazgo = await db.EvaluacionTrabajoEquipoIniciativaLiderazgo.Where(x => x.IdEval001 == eval.IdEval001).ToListAsync();
                if (TrabajoLiderazgo.Count != 0)
                {
                    //double valor = 0, total = 0;
                    double valor = 0, total = 0;
                    foreach (var item in TrabajoLiderazgo)
                    {
                        var a = await db.FrecuenciaAplicacion.Where(x => x.IdFrecuenciaAplicacion == item.IdFrecuenciaAplicacion).FirstOrDefaultAsync();
                        var b = a.Nombre;

                        if (b == EvaluacionDesempeño.Siempre)
                        {
                            valor = 8;
                        }
                        if (b == EvaluacionDesempeño.Frecuentemente)
                        {
                            valor = 6;
                        }
                        if (b == EvaluacionDesempeño.Algunavez)
                        {
                            valor = 4;
                        }
                        if (b == EvaluacionDesempeño.Raravez)
                        {
                            valor = 2;
                        }
                        if (b == EvaluacionDesempeño.Nunca)
                        {
                            valor = 0;
                        }
                        total = total + valor;
                    }
                    total = total / TrabajoLiderazgo.Count;
                    totales.TotalTrabajoLiderazgo = total;
                }
                else
                {
                    totales.TotalTrabajoLiderazgo = 0;
                }
                var TotalEvaluacion = totales.TotalCompetenciasTecnicas + totales.TotalCompetenciasUniversales + totales.TotalConocimientos + totales.TotalTrabajoLiderazgo + totales.ActividadesTotal;
                totales.TotalEvaluacion = TotalEvaluacion;
                return totales;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return new ViewModelEvaluador();
            }
        }
        public Response Existe(Eval001 eval001)
        {
            var bdd1 = eval001.IdEval001;
            var loglevelrespuesta = db.Eval001.Where(p => p.IdEval001 == bdd1).FirstOrDefault();
            if (loglevelrespuesta.Observaciones != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Resultado = loglevelrespuesta,
                };

            }
            return new Response
            {
                IsSuccess = false,
            };
        }
    }
}