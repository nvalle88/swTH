using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.swth.entidades.Enumeradores;
using bd.log.guardar.Enumeradores;
using bd.swth.entidades.Utils;

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
                return await db.Presupuesto.OrderBy(x => x.Fecha).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<Presupuesto>();
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

                var EtniaActualizar = await db.Presupuesto.Where(x => x.IdPresupuesto == id).FirstOrDefaultAsync();
                if (EtniaActualizar != null)
                {
                    try
                    {
                        EtniaActualizar.NumeroPartidaPresupuestaria = presupuesto.NumeroPartidaPresupuestaria;
                        EtniaActualizar.NumeroPartidaPresupuestaria = presupuesto.NumeroPartidaPresupuestaria;
                        EtniaActualizar.Fecha = presupuesto.Fecha;
                        db.Presupuesto.Update(EtniaActualizar);
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
            var bdd = presupuesto.NumeroPartidaPresupuestaria;
            var bdd1 = presupuesto.Valor;
            var bdd2 = presupuesto.Fecha;
            var Etniarespuesta = db.Presupuesto.Where(p => p.NumeroPartidaPresupuestaria == bdd 
            && p.Valor == bdd1
            && p.Fecha == bdd2).FirstOrDefault();
            if (Etniarespuesta != null)
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
                Resultado = Etniarespuesta,
            };
        }
    }
}