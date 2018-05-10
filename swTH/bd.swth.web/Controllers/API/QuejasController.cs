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
    [Route("api/Quejas")]
    public class QuejasController : Controller
    {
        private readonly SwTHDbContext db;

        public QuejasController(SwTHDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("ListarEmpleados")]
        public async Task<List<ViewModelEvaluacionDesempeno>> ListarEmpleados()
        {
            var DatosBasicos = new List<ViewModelEvaluacionDesempeno>();
            try
            {
                    var a = await db.Eval001.ToListAsync();
                    foreach (var item in a)
                    {
                        var lista = await db.Empleado
                                        .Where(x => x.IdEmpleado == item.IdEmpleadoEvaluado && x.Activo == true)
                                        .Select(x => new ViewModelEvaluacionDesempeno
                                        {
                                            IdEmpleado = x.IdEmpleado,
                                            NombreApellido = string.Format("{0} {1}", x.Persona.Nombres, x.Persona.Apellidos),
                                            Identificacion = x.Persona.Identificacion,
                                            IdEval001 =item.IdEval001,
                                            FechaRegistro = item.FechaRegistro,
                                            FechaDesde = item.FechaEvaluacionDesde,
                                            FechaHasta = item.FechaEvaluacionHasta

                                        })
                                        .FirstOrDefaultAsync();
                        if (lista != null)
                        {
                            DatosBasicos.Add(lista);
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
        [Route("ListaQuejas")]
        public async Task<List<ViewModelQuejas>> Evaluar([FromBody] ViewModelQuejas viewModelQuejas)
        {
            try
            {
                var lista = new List<ViewModelQuejas>();

                lista = await db.Quejas.Where
                   (x => x.IdEmpleado == viewModelQuejas.IdEmpleado && x.IdEval001 == viewModelQuejas.IdEval001)
                   .Select(y => new ViewModelQuejas
                   {
                       IdQuejas = y.IdQueja,
                       IdEmpleado = Convert.ToInt32(y.IdEmpleado),
                       IdEval001 = Convert.ToInt32( y.IdEval001),
                       NombreCiudadano = y.Nombre,
                       ApellidoCiudadano = y.Apellido,
                       Descripcion = y.Descripcion,
                       NumeroFormulario = Convert.ToInt32( y.NumeroFormulario),
                       AplicaDescuento = Convert.ToBoolean( y.AplicaDescuento)
                    }).ToListAsync();

                //quejas.Add(lista);
                return lista;
            }
            catch (Exception ex)
            {
                return new List<ViewModelQuejas>();
                throw;
            }
        }

       
        [HttpPost]
        [Route("InsertarQuejas")]
        public async Task<Response> InsertarQuejas([FromBody] ViewModelQuejas viewModelQuejas)
        {
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {
                        var quejas = new Quejas
                        {
                            IdEmpleado = viewModelQuejas.IdEmpleado,
                            IdEval001 = viewModelQuejas.IdEval001,
                            Nombre = viewModelQuejas.NombreCiudadano,
                            Apellido = viewModelQuejas.ApellidoCiudadano,
                            Descripcion = viewModelQuejas.Descripcion,
                            NumeroFormulario = Convert.ToString( viewModelQuejas.NumeroFormulario),
                            AplicaDescuento = viewModelQuejas.AplicaDescuento
                        };
                        await db.Quejas.AddAsync(quejas);
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
        public async Task<ViewModelQuejas> CalcularTotales([FromBody] ViewModelQuejas  viewModelQuejas)
        {
            try
            {
                var totales = new ViewModelQuejas();
                var eval = new Eval001()
                {
                    IdEval001 = viewModelQuejas.IdEval001,
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
                
                var Quejas = await db.Quejas.Where(x => x.IdEval001 == eval.IdEval001 && x.AplicaDescuento== true).ToListAsync();
                if (Quejas.Count != 0)
                {
                    //double valor = 0, total = 0;
                    double valor = 0, total = 0;
                    foreach (var item in Quejas)
                    {
                        valor = 4;                        
                        total = total + valor;
                    }
                    //total = total / Quejas.Count;
                    totales.TotalQuejas = total;
                }
                else
                {
                    totales.TotalQuejas = 0;
                }
                var TotalEvaluacion = totales.TotalCompetenciasTecnicas + totales.TotalCompetenciasUniversales + totales.TotalConocimientos + totales.TotalTrabajoLiderazgo + totales.ActividadesTotal;
                var totalConQuejas = TotalEvaluacion - totales.TotalQuejas;
                totales.TotalEvaluacion = totalConQuejas;
                return totales;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return new ViewModelQuejas();
            }
        }
        // DELETE: api/Pais/5
        [HttpDelete("{id}")]
        public async Task<Response> Delete([FromRoute] int id)
        {
            try
            {

                var respuesta = await db.Quejas.SingleOrDefaultAsync(m => m.IdQueja == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.Quejas.Remove(respuesta);
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
                    Message = Mensaje.Error,
                };
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