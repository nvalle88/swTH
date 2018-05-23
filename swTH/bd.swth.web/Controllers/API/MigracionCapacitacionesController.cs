using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/MigracionCapacitaciones")]
    public class MigracionCapacitacionesController : Controller
    {

        private readonly SwTHDbContext db;

        public MigracionCapacitacionesController(SwTHDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        [Route("VerificarExcel")]
        public async Task<List<PlanCapacitacion>> ExisteConceptoPorCodigo([FromBody] List<PlanCapacitacion> lista)
        {
            try
            {
                var result = lista
                .GroupBy(x => x.NombreCiudad)
                .Select(g => new
                {
                    Nombreciudad = g.Key,
                    Total = g.Sum(x => x.PresupuestoIndividual)
                });
                foreach (var item in result)
                {
                    var a = await ExitePresupuesto(item.Nombreciudad, item.Total);

                }
                foreach (var item in lista)
                {

                    var empleado = await db.Empleado.Where(x => x.Activo == true && x.Persona.Identificacion == item.Cedula).FirstOrDefaultAsync();
                    if (empleado == null)
                    {
                        item.Valido = false;
                        item.MensajeError = Mensaje.EmpleadoNoExiste;
                    }else 
                    {
                        item.Valido = true;
                    }

                }
               

                return lista;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarGestionPlanCapacitaciones")]
        public async Task<List<GestionPlanCapacitacion>> ListarGestionPlanCapacitaciones()
        {
            try
            {
                return await db.GestionPlanCapacitacion.ToListAsync();
            }
            catch (Exception)
            {
                return new List<GestionPlanCapacitacion>();
            }
        }

        [HttpPost]
        [Route("LimpiarReportados")]
        public async Task<Response> LimpiarReportados([FromBody] GestionPlanCapacitacion gestionPlanCapacitacion)
        {
            try
            {
                var listadoBorrar = await db.PlanCapacitacion.Where(x => x.IdGestionPlanCapacitacion == gestionPlanCapacitacion.IdGestionPlanCapacitacion).ToListAsync();
                db.PlanCapacitacion.RemoveRange(listadoBorrar);
                await db.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                };
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet]
        [HttpPost("ObtenerDatosPlanCapacitaciones")]
        public async Task<Response> ObtenerDatosPlanCapacitaciones([FromBody] PlanCapacitacion planCapacitacion)
        {
            try
            {
                var capacitacion = await db.PlanCapacitacion.Where(x=>x.IdPlanCapacitacion == planCapacitacion.IdPlanCapacitacion).FirstOrDefaultAsync();
                var datos = await db.Empleado.Where(x => x.Persona.Identificacion == capacitacion.Cedula).FirstOrDefaultAsync();
                var presupuesto = await db.Presupuesto.Where(x => x.NumeroPartidaPresupuestaria == capacitacion.NumeroPartidaPresupuestaria).FirstOrDefaultAsync();

                if (presupuesto !=null )
                {
                    var datos2 = await db.IndiceOcupacionalModalidadPartida.Where(x => x.IdEmpleado == datos.IdEmpleado).OrderByDescending(x => x.Fecha).Select(y => new PlanCapacitacion
                    {
                        IdPresupuesto = presupuesto.IdPresupuesto,
                        IdEmpleado = y.IdEmpleado,
                        Institucion = capacitacion.Institucion,
                        NivelDesconcentracion = capacitacion.NivelDesconcentracion,
                        Pais = y.IndiceOcupacional.Dependencia.Sucursal.Ciudad.Provincia.Pais.Nombre,
                        Provincia = y.IndiceOcupacional.Dependencia.Sucursal.Ciudad.Provincia.Nombre,
                        NombreCiudad = y.IndiceOcupacional.Dependencia.Sucursal.Ciudad.Nombre,
                        Cedula = y.Empleado.Persona.Identificacion,
                        ApellidoNombre = y.Empleado.Persona.Nombres + " " + y.Empleado.Persona.Apellidos,
                        Sexo = y.Empleado.Persona.Sexo.Nombre,
                        GrupoOcupacional = y.IndiceOcupacional.EscalaGrados.GrupoOcupacional.TipoEscala,
                        ModalidadLaboral = y.TipoNombramiento.Nombre,
                        RegimenLaboral = y.TipoNombramiento.RelacionLaboral.Nombre,
                        DenominacionPuesto = y.IndiceOcupacional.ManualPuesto.Nombre,
                        ProductoFinal = capacitacion.ProductoFinal,
                        TemaCapacitacion = capacitacion.TemaCapacitacion,
                        ClasificacionTema = capacitacion.ClasificacionTema,
                        Modalidad = capacitacion.Modalidad,
                        Duracion = capacitacion.Duracion,
                        PresupuestoIndividual = capacitacion.PresupuestoIndividual,
                        FechaCapacitacionPlanificada = capacitacion.FechaCapacitacionPlanificada,
                        TipoCapacitacion = capacitacion.TipoCapacitacion,
                        EstadoEvento = capacitacion.EstadoEvento,
                        UnidadAdministrativa = y.IndiceOcupacional.Dependencia.Nombre

                    }).FirstOrDefaultAsync();
                    if (datos2 == null)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = Mensaje.RegistroNoEncontrado,
                        };
                    }

                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                        Resultado = datos2,
                    };
                }
               
                return new Response
                {
                    IsSuccess = false,
                    Message = "Verificar Presupuesto no Existe",
                    
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }
        [HttpPost]
        [Route("InsertarReportadoPlanificacion")]
        public async Task<Response> InsertarReportadoNomina([FromBody] List<PlanCapacitacion> listaSalvar)
        {
            try
            {
               var listadoBorrar = await db.PlanCapacitacion.Where(x => x.IdGestionPlanCapacitacion == listaSalvar.FirstOrDefault().IdGestionPlanCapacitacion).ToListAsync();

                db.PlanCapacitacion.RemoveRange(listadoBorrar);
                await db.SaveChangesAsync();
                await db.PlanCapacitacion.AddRangeAsync(listaSalvar);
                await db.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                };
            }
        }
        // PUT: api/BasesDatos/5
        [HttpPost]
        [Route("EditarCalculoNomina")]
        public async Task<Response> EditarCalculoNomina([FromBody] CalculoNomina CalculoNomina)
        {
            try
            {
                if (await Existe(CalculoNomina))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var CalculoNominaActualizar = await db.CalculoNomina.Where(x => x.IdCalculoNomina == CalculoNomina.IdCalculoNomina).FirstOrDefaultAsync();
                if (CalculoNominaActualizar == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                    };

                }
                CalculoNominaActualizar.Descripcion = CalculoNomina.Descripcion;
                CalculoNominaActualizar.IdPeriodo = CalculoNomina.IdPeriodo;
                CalculoNominaActualizar.IdProceso = CalculoNomina.IdProceso;
                CalculoNominaActualizar.Automatico = CalculoNomina.Automatico;
                CalculoNominaActualizar.Reportado = CalculoNomina.Reportado;
                CalculoNominaActualizar.EmpleadoActivo = CalculoNomina.EmpleadoActivo;
                CalculoNominaActualizar.EmpleadoPasivo = CalculoNomina.EmpleadoPasivo;
                db.CalculoNomina.Update(CalculoNominaActualizar);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Resultado = CalculoNominaActualizar
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };
            }
        }

        [HttpPost]
        [Route("ListarReportados")]
        public async Task<List<PlanCapacitacion>> ListarReportados([FromBody] GestionPlanCapacitacion gestionPlanCapacitacion)
        {
            try
            {
                var lista= await db.PlanCapacitacion.Where(x => x.IdGestionPlanCapacitacion == gestionPlanCapacitacion.IdGestionPlanCapacitacion).ToListAsync();
                return lista;
            }
            catch (Exception)
            {
                return new List<PlanCapacitacion>();
            }
        }

        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarCalculoNomina")]
        public async Task<Response> PostCalculoNomina([FromBody] CalculoNomina CalculoNomina)
        {
            try
            {

                if (!await Existe(CalculoNomina))
                {
                    db.CalculoNomina.Add(CalculoNomina);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                        Resultado = CalculoNomina,
                    };
                }

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ExisteRegistro
                };

            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }

        // DELETE: api/BasesDatos/5
        [HttpPost]
        [Route("EliminarCalculoNomina")]
        public async Task<Response> EliminarCalculoNomina([FromBody]CalculoNomina CalculoNomina)
        {
            try
            {
                var respuesta = await db.CalculoNomina.Where(m => m.IdCalculoNomina == CalculoNomina.IdCalculoNomina).FirstOrDefaultAsync();
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.CalculoNomina.Remove(respuesta);
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
                    Message = Mensaje.BorradoNoSatisfactorio,
                };

            }
        }

        private async Task<bool> Existe(CalculoNomina CalculoNomina)
        {
            var periodo = CalculoNomina.IdPeriodo;
            var proceso = CalculoNomina.IdProceso;
            var CalculoNominarespuesta = await db.CalculoNomina.Where(p => p.IdProceso == proceso && p.IdPeriodo == periodo).FirstOrDefaultAsync();

            if (CalculoNominarespuesta == null || CalculoNominarespuesta.IdCalculoNomina == CalculoNomina.IdCalculoNomina)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        private async Task<bool> ExitePresupuesto(string ciudadresive, decimal? valorresive)
        {
            var datosenvia = new Presupuesto();
            var ciudad = await db.Ciudad.Where(x => x.Nombre == ciudadresive).FirstOrDefaultAsync();
            if (ciudad != null)
            {
                var sucursal = await db.Sucursal.Where(x => x.IdCiudad == ciudad.IdCiudad).FirstOrDefaultAsync();
                if (sucursal != null)
                {
                    var presupuesto = await db.Presupuesto.Where(x => x.IdSucursal == sucursal.IdSucursal).FirstOrDefaultAsync();
                    if (presupuesto != null)
                    {
                        var b = db.DetallePresupuesto.Where(x => x.IdPresupuesto == presupuesto.IdPresupuesto).ToListAsync().Result.Sum(x => x.Valor);
                        var valor = b + Convert.ToDouble(valorresive);
                        if (valor <= presupuesto.Valor)
                        {
                            return true;
                        }
                    }
                }

            }
            return false;
        }
    }
}