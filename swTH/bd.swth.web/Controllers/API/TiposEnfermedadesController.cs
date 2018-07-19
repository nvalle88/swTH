using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using Microsoft.EntityFrameworkCore;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.swth.entidades.Enumeradores;
using bd.log.guardar.Enumeradores;
using bd.swth.entidades.Utils;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/TiposEnfermedades")]
    public class TiposEnfermedadesController : Controller
    {
        private readonly SwTHDbContext db;

        public TiposEnfermedadesController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarTiposEnfermedades")]
        public async Task<List<TipoEnfermedad>> GetTipoEnfermedad()
        {
            try
            {
                return await db.TipoEnfermedad.OrderBy(x => x.Nombre).ToListAsync();
            }
            catch (Exception ex)
            {
                
                return new List<TipoEnfermedad>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetTipoEnfermedad([FromRoute] int id)
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

                var TipoEnfermedad = await db.TipoEnfermedad.SingleOrDefaultAsync(m => m.IdTipoEnfermedad == id);

                if (TipoEnfermedad == null)
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
                    Resultado = TipoEnfermedad,
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
        public async Task<Response> PutTipoEnfermedad([FromRoute] int id, [FromBody] TipoEnfermedad TipoEnfermedad)
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

                var existe = await db.TipoEnfermedad
                    .Where(w =>
                        w.Nombre.ToString().ToUpper() == TipoEnfermedad.Nombre.ToString().ToUpper()
                    )
                    .FirstOrDefaultAsync();

                if (existe != null && existe.IdTipoEnfermedad != id)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var TipoEnfermedadActualizar = await db.TipoEnfermedad.Where(x => x.IdTipoEnfermedad == id).FirstOrDefaultAsync();

                if (TipoEnfermedadActualizar != null)
                {
                    try
                    {
                        TipoEnfermedadActualizar.Nombre = TipoEnfermedad.Nombre.ToString().ToUpper();
                        await db.SaveChangesAsync();

                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.GuardadoSatisfactorio,
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
        [Route("InsertarTipoEnfermedad")]
        public async Task<Response> PostTipoEnfermedad([FromBody] TipoEnfermedad TipoEnfermedad)
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

                var respuesta = Existe(TipoEnfermedad);
                if (!respuesta.IsSuccess)
                {

                    // Converir a mayúsculas
                    TipoEnfermedad.Nombre = TipoEnfermedad.Nombre.ToString().ToUpper();

                    db.TipoEnfermedad.Add(TipoEnfermedad);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.GuardadoSatisfactorio
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
        public async Task<Response> DeleteTipoEnfermedad([FromRoute] int id)
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

                var respuesta = await db.TipoEnfermedad.SingleOrDefaultAsync(m => m.IdTipoEnfermedad == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.TipoEnfermedad.Remove(respuesta);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.BorradoSatisfactorio,
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

        private Response Existe(TipoEnfermedad TipoEnfermedad)
        {
            var bdd = TipoEnfermedad.Nombre;
            var TipoEnfermedadrespuesta = db.TipoEnfermedad
                .Where(p => p.Nombre.ToString().ToUpper() == bdd.ToString().ToUpper())
                .FirstOrDefault();


            if (TipoEnfermedadrespuesta != null)
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
                Resultado = TipoEnfermedadrespuesta,
            };
        }

    }
}