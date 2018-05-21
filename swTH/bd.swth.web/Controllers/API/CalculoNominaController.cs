using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using bd.swth.datos;
using bd.swth.entidades.Constantes;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/CalculoNomina")]
    public class CalculoNominaController : Controller
    {

        private readonly SwTHDbContext db;
        private readonly SwTHDbContext db_1;

        public CalculoNominaController(SwTHDbContext db, SwTHDbContext db_1)
        {
            this.db = db;
            this.db_1 = db_1;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarCalculoNomina")]
        public async Task<List<CalculoNomina>> ListarCalculoNomina()
        {
            try
            {
                return await db.CalculoNomina.Include(x => x.PeriodoNomina).Include(x => x.ProcesoNomina).ToListAsync();
            }
            catch (Exception)
            {
                return new List<CalculoNomina>();
            }
        }


        private async Task<List<EmpleadoMovimiento>> ListarMovimientosAprobadosPorPeriodo(DateTime fechaInicio)
        {
            var lista =await db.EmpleadoMovimiento.Where(x => x.FechaDesde >= fechaInicio && x.AccionPersonal.Estado==ConstantesEstadosAprobacionMovimientoInterno.ListaEstadosAprobacionMovimientoInterno[3].ValorEstado).ToListAsync();
            return lista;
        }

        private async Task<CalculoNomina> ObtenerCalculoNominaDetalle(CalculoNomina calculoNomina)
        {
            var calculoNominaRequest = await db.CalculoNomina.Where(x => x.IdCalculoNomina == calculoNomina.IdCalculoNomina).Include(x => x.PeriodoNomina).Include(y => y.ProcesoNomina).ThenInclude(y => y.ConceptoNomina).FirstOrDefaultAsync();
            return calculoNominaRequest;
        }

        private async Task<List<ReportadoNomina>> ListarReportadoNomina(CalculoNomina calculoNomina)
        {
                var listaReportado = await db.ReportadoNomina.Where(x => x.IdCalculoNomina == calculoNomina.IdCalculoNomina).ToListAsync();

            return listaReportado;
        }

        private async Task<List<Empleado>> ListarEmpleadosNomina(bool activo)
        {
            var listaEmpleados = await db.Empleado.Where(x => x.Activo == activo).Include(x=>x.Persona).Include(y=>y.IndiceOcupacionalModalidadPartida).ThenInclude(y=>y.IndiceOcupacional.EscalaGrados).ToListAsync();
            return listaEmpleados;
        }

        [HttpPost]
        [Route("CalcularDetalleNomina")]
        public async Task<Response> CalcularDetalleNomina([FromBody] CalculoNomina calculoNomina)
        {
            try
            {
                ///Variables necesarias para el calculo de la nómina
                var ListaReportados = await ListarReportadoNomina(calculoNomina);
                var CalculoNomina = await ObtenerCalculoNominaDetalle(calculoNomina);
                var ListaMovimietosEmpleado = await ListarMovimientosAprobadosPorPeriodo(CalculoNomina.PeriodoNomina.FechaInicio);
                var listaEmpleados = await ListarEmpleadosNomina(true);
                string scapeConst = "#";
                string scapeFunct = "@";


                var conn = db.Database.GetDbConnection();

                 conn.Open();
                using (var command = conn.CreateCommand())
                {
                    string query = "Select Empleado.IdEmpleado from Empleado";
                    command.CommandText = query;
                    DbDataReader reader =  command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while ( reader.Read())
                        {
                            var row = new Empleado { IdEmpleado = reader.GetInt32(0)};
                           
                        }
                    }
                    reader.Dispose();
                }

                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var empleado in listaEmpleados)
                        {
                            ///Insertar Cabecera del Calculo de la nómina
                            var cabeceraNomina = new CabeceraNomina { IdEmpleado = empleado.IdEmpleado, IdCalculoNomina = calculoNomina.IdCalculoNomina };

                            await db.CabeceraNomina.AddAsync(cabeceraNomina);
                            await db.SaveChangesAsync();

                            //var SalarioMensual = empleado.IndiceOcupacionalModalidadPartida.OrderByDescending(x => x.Fecha).FirstOrDefault().SalarioReal == null  
                            //                     ? empleado.IndiceOcupacionalModalidadPartida.OrderByDescending(x => x.Fecha).FirstOrDefault().IndiceOcupacional.EscalaGrados.Remuneracion 
                            //                     : empleado.IndiceOcupacionalModalidadPartida.OrderByDescending(x => x.Fecha).FirstOrDefault().SalarioReal;

                            //var detalleNomi = db.DetalleNomina.AddAsync(new DetalleNomina { IdCabeceraNomina = cabeceraNomina.IdCabeceraNomina, IdConceptoNomina = 10, Valor = Convert.ToDouble(SalarioMensual) });


                            List<Empleado> modelList = new List<Empleado>();

                            modelList = db.Empleado.FromSql("Select * from Empleado").ToList();

                           


                        }

                        transaction.Commit();
                        return new Response { IsSuccess = true };
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
               
            }
            catch (Exception ex)
            {
                
                return new Response
                {
                    IsSuccess = false,
                };
            }
        }

        [HttpPost]
        [Route("LimpiarReportados")]
        public async Task<Response> LimpiarReportados([FromBody] CalculoNomina calculoNomina)
        {
            try
            {
                var listadoBorrar = await db.ReportadoNomina.Where(x => x.IdCalculoNomina == calculoNomina.IdCalculoNomina).ToListAsync();
                db.ReportadoNomina.RemoveRange(listadoBorrar);
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
        [HttpPost("ObtenerCalculoNomina")]
        public async Task<Response> ObtenerCalculoNomina([FromBody] CalculoNomina CalculoNomina)
        {
            try
            {
                var calculoNomina = await db.CalculoNomina.SingleOrDefaultAsync(m => m.IdCalculoNomina == CalculoNomina.IdCalculoNomina);

                if (calculoNomina == null)
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
                    Resultado = calculoNomina,
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
        public async Task<List<ReportadoNomina>> ListarReportados([FromBody] CalculoNomina CalculoNomina)
        {
            try
            {
                return await db.ReportadoNomina.Where(x => x.IdCalculoNomina == CalculoNomina.IdCalculoNomina).ToListAsync();
            }
            catch (Exception)
            {
                return new List<ReportadoNomina>();
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
    }
}