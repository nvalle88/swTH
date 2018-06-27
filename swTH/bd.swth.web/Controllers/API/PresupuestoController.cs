using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.Utils;
using bd.swth.entidades.ViewModels;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Presupuesto")]
    public class PresupuestoController : Controller
    {
        private readonly SwTHDbContext db;

        public PresupuestoController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarPresupuesto")]
        public async Task<List<Presupuesto>> GetPresupuesto()
        {
            try
            {
                return await db.Presupuesto.Where(x => x.IdSucursal == null).OrderBy(x => x.Fecha).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<Presupuesto>();
            }
        }
        [HttpGet]
        [Route("ListarPresupuestoCapacitaciones")]
        public async Task<List<ViewModelPresupuesto>> ListarPresupuestoCapacitaciones()
        {
            try
            {
                var ListaPresupuestoCapacitaciones =  await db.Presupuesto.Select(
                    x => new ViewModelPresupuesto
                    {
                        IdSucursal = x.IdSucursal,
                        NombreSucursal = x.Sucursal.Nombre,
                        NumeroPartidaPresupuestaria = x.NumeroPartidaPresupuestaria,
                        IdPresupuesto = x.IdPresupuesto,
                        Fecha = x.Fecha,
                        Valor = x.Valor
                                      
                    }).Where(x => x.IdSucursal != null).ToListAsync();

                return ListaPresupuestoCapacitaciones;
            }
            catch (Exception ex)
            {
                return new List<ViewModelPresupuesto>();
            }
        }

        [HttpPost]
        [Route("ObtenerPresupuesto")]
        public async Task<Response> ObtenerPresupuesto([FromBody] ViewModelsSolicitudViaticos viewModelsSolicitudViaticos)
        {
            try
            {
                var a = await db.Presupuesto.Where(x => x.IdPresupuesto == viewModelsSolicitudViaticos.IdPresupuesto && x.IdSucursal == null).OrderBy(x => x.Fecha).FirstOrDefaultAsync();
                if (a != null)
                {
                    var b = db.DetallePresupuesto.Where(x => x.IdPresupuesto == viewModelsSolicitudViaticos.IdPresupuesto).ToListAsync().Result.Sum(x => x.Valor);
                    var valor = b + Convert.ToDouble(viewModelsSolicitudViaticos.ValorEstimado);
                    if (valor <= a.Valor)
                    {

                        var detalle = new DetallePresupuesto
                        {
                            IdPresupuesto = a.IdPresupuesto,
                            IdSolicitudViatico = viewModelsSolicitudViaticos.IdSolicitudViatico,
                            Valor = Convert.ToDouble(viewModelsSolicitudViaticos.ValorEstimado),
                            Fecha = DateTime.Now

                        };
                        db.DetallePresupuesto.Add(detalle);
                        await db.SaveChangesAsync();
                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.Satisfactorio
                        };
                    }
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No Contiene Fondos"
                    };

                }
                return new Response
                {
                    IsSuccess = false
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false
                };
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetPresupuesto([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ModeloInvalido,
                    };
                }

                var Etnia = await db.Presupuesto.SingleOrDefaultAsync(m => m.IdPresupuesto == id);

                if (Etnia == null)
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
                    Resultado = Etnia,
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

        // PUT: api/BasesDatos/5
        [HttpPut("{id}")]
        public async Task<Response> PutPresupuesto([FromRoute] int id, [FromBody] Presupuesto presupuesto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ModeloInvalido
                    };
                }

                var existe = Existe(presupuesto);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var PresupuestoActualizar = await db.Presupuesto.Where(x => x.IdPresupuesto == id).FirstOrDefaultAsync();
                if (PresupuestoActualizar != null)
                {
                    try
                    {
                        PresupuestoActualizar.NumeroPartidaPresupuestaria = presupuesto.NumeroPartidaPresupuestaria;
                        PresupuestoActualizar.NumeroPartidaPresupuestaria = presupuesto.NumeroPartidaPresupuestaria;
                        PresupuestoActualizar.Fecha = presupuesto.Fecha;
                        PresupuestoActualizar.IdSucursal = presupuesto.IdSucursal;
                        db.Presupuesto.Update(PresupuestoActualizar);
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
                    Message = Mensaje.Excepcion
                };
            }
        }

        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarPresupuesto")]
        public async Task<Response> PostPresupuesto([FromBody] Presupuesto presupuesto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = ""
                    };
                }

                var respuesta = Existe(presupuesto);
                if (!respuesta.IsSuccess)
                {
                    db.Presupuesto.Add(presupuesto);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio
                    };
                }

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ExisteRegistro
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

        // DELETE: api/BasesDatos/5
        [HttpDelete("{id}")]
        public async Task<Response> DeletePresupuesto([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ModeloInvalido,
                    };
                }

                var respuesta = await db.Presupuesto.SingleOrDefaultAsync(m => m.IdPresupuesto == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.Presupuesto.Remove(respuesta);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                };
            }
            catch (Exception ex)
            {

                if (ex.InnerException.Message.Contains("REFERENCE"))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No se puede eliminar",
                    };
                }
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }

        private Response Existe(Presupuesto presupuesto)
        {
            var bdd1 = presupuesto.NumeroPartidaPresupuestaria;
            var presupuestorespuesta = db.Presupuesto.Where(p =>p.NumeroPartidaPresupuestaria == bdd1).FirstOrDefault();
            if (presupuestorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = null,
                };

            }
            return new Response
            {
                IsSuccess = false,
                Resultado = presupuestorespuesta,
            };
        }
    }
}