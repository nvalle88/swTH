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
using bd.swth.entidades.ViewModels;
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
            catch (Exception ex)
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

           var rowsAfected= db.Database
                .ExecuteSqlCommand("sp_SalarioEmpleado @idCalculoNomina = {0}"
                , calculoNomina.IdCalculoNomina);

            return new Response {IsSuccess=true,Message=Convert.ToString(rowsAfected) };
        }

        [HttpPost]
        [Route("LimpiarDiasLaborados")]
        public async Task<Response> LimpiarDiasLaborados([FromBody] CalculoNomina calculoNomina)
        {
            try
            {
                var listadoBorrar = await db.DiasLaboradosNomina.Where(x => x.IdCalculoNomina == calculoNomina.IdCalculoNomina).ToListAsync();
                db.DiasLaboradosNomina.RemoveRange(listadoBorrar);
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

        [HttpPost]
        [Route("LimpiarHorasExtras")]
        public async Task<Response> LimpiarHorasExtras([FromBody] CalculoNomina calculoNomina)
        {
            try
            {
                var listadoBorrar = await db.HorasExtrasNomina.Where(x => x.IdCalculoNomina == calculoNomina.IdCalculoNomina).ToListAsync();
                db.HorasExtrasNomina.RemoveRange(listadoBorrar);
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


        [HttpGet]
        [HttpPost("EditarDiasLaborados")]
        public async Task<Response> EditarDiasLaborados([FromBody] DiasLaboradosNomina laboradosNomina)
        {
            try
            {
                var diasLaborados = await db.DiasLaboradosNomina.Where(x => x.IdDiasLaboradosNomina == laboradosNomina.IdDiasLaboradosNomina).FirstOrDefaultAsync();


                if (diasLaborados == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }

                diasLaborados.CantidadDias = laboradosNomina.CantidadDias;
                db.DiasLaboradosNomina.Update(diasLaborados);
                await db.SaveChangesAsync();
              
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                    Resultado = diasLaborados,
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

        [HttpGet]
        [HttpPost("ObtenerDiasLaborados")]
        public async Task<Response> ObtenerDiasLaborados([FromBody] DiasLaboradosNomina laboradosNomina)
        {
            try
            {
                var diasLaborados = await db.DiasLaboradosNomina.Where(x=>x.IdDiasLaboradosNomina==laboradosNomina.IdDiasLaboradosNomina).FirstOrDefaultAsync();


                if (diasLaborados == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }

                diasLaborados.Nombres =  db.Empleado.Where(x => x.IdEmpleado == diasLaborados.IdEmpleado).Include(x=>x.Persona).FirstOrDefault().Persona.Nombres;
                diasLaborados.Apellidos = db.Empleado.Where(x => x.IdEmpleado == diasLaborados.IdEmpleado).Include(x => x.Persona).FirstOrDefault().Persona.Apellidos;

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                    Resultado = diasLaborados,
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
                CalculoNominaActualizar.FechaFinDecimoCuarto = CalculoNomina.FechaFinDecimoCuarto;
                CalculoNominaActualizar.FechaFinDecimoTercero = CalculoNomina.FechaFinDecimoTercero;
                CalculoNominaActualizar.FechaInicioDecimoCuarto = CalculoNomina.FechaInicioDecimoCuarto;
                CalculoNominaActualizar.FechaInicioDecimoTercero = CalculoNomina.FechaInicioDecimoTercero;
                CalculoNominaActualizar.IdPeriodo = CalculoNomina.IdPeriodo;
                CalculoNominaActualizar.IdProceso = CalculoNomina.IdProceso;
                CalculoNominaActualizar.DecimoTercerSueldo = CalculoNomina.DecimoTercerSueldo;
                CalculoNominaActualizar.DecimoCuartoSueldo = CalculoNomina.DecimoCuartoSueldo;
                CalculoNominaActualizar.EmpleadoActivo = CalculoNomina.EmpleadoActivo;
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
                return await  db.ReportadoNomina.Where(x => x.IdCalculoNomina == CalculoNomina.IdCalculoNomina)
                    .Select(x=> new ReportadoNomina
                    {
                        IdCalculoNomina=x.IdCalculoNomina,
                        Cantidad=x.Cantidad,
                        Importe=x.Importe,
                        CodigoConcepto=x.CodigoConcepto,
                        IdentificacionEmpleado=x.IdentificacionEmpleado,
                        NombreEmpleado=x.NombreEmpleado,
                        IdReportadoNomina=x.IdReportadoNomina,
                        DescripcionConcepto= db.ConceptoNomina.Where(c=>c.Codigo==x.CodigoConcepto).FirstOrDefault().Descripcion,
                        
                    }) .ToListAsync();
            }
            catch (Exception)
            {
                return new List<ReportadoNomina>();
            }
        }


        [HttpPost]
        [Route("ListarDiasLaborados")]
        public async Task<List<DiasLaboradosNomina>> ListarDiasLaborados([FromBody] CalculoNomina CalculoNomina)
        {
            try
            {
                return await db.DiasLaboradosNomina.Where(x => x.IdCalculoNomina == CalculoNomina.IdCalculoNomina).
                    Select(x => new DiasLaboradosNomina
                    {
                        CantidadDias = x.CantidadDias,
                        Nombres = db.Persona.Where(y => y.Identificacion == x.IdentificacionEmpleado).FirstOrDefault().Nombres,
                        Apellidos = db.Persona.Where(y => y.Identificacion == x.IdentificacionEmpleado).FirstOrDefault().Apellidos,
                        IdentificacionEmpleado = x.IdentificacionEmpleado,
                        IdCalculoNomina=x.IdCalculoNomina,
                        IdDiasLaboradosNomina=x.IdDiasLaboradosNomina,
                        IdEmpleado=x.IdEmpleado,

                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<DiasLaboradosNomina>();
            }
        }


        [HttpPost]
        [Route("ListarHorasExtras")]
        public async Task<List<HorasExtrasNomina>> ListarHorasExtras([FromBody] CalculoNomina CalculoNomina)
        {
            try
            {
                return await db.HorasExtrasNomina.Where(x => x.IdCalculoNomina == CalculoNomina.IdCalculoNomina).
                    Select(x => new HorasExtrasNomina
                    {
                        CantidadHoras = x.CantidadHoras,
                        Nombres = db.Persona.Where(y => y.Identificacion == x.IdentificacionEmpleado).FirstOrDefault().Nombres,
                        Apellidos = db.Persona.Where(y => y.Identificacion == x.IdentificacionEmpleado).FirstOrDefault().Apellidos,
                        EsExtraordinaria = x.EsExtraordinaria,
                        IdentificacionEmpleado = x.IdentificacionEmpleado,
                        IdHorasExtrasNomina = x.IdHorasExtrasNomina,
                        EsCienPorciento = x.EsCienPorciento,

                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<HorasExtrasNomina>();
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