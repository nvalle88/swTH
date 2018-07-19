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
    [Route("api/TiposDiscapacidades")]
    public class TiposDiscapacidadesController : Controller
    {
        private readonly SwTHDbContext db;

        public TiposDiscapacidadesController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarTiposDiscapacidades")]
        public async Task<List<TipoDiscapacidad>> GetTipoDiscapacidad()
        {
            try
            {
                return await db.TipoDiscapacidad.OrderBy(x => x.Nombre).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<TipoDiscapacidad>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetTipoDiscapacidad([FromRoute] int id)
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

                var TipoDiscapacidad = await db.TipoDiscapacidad.SingleOrDefaultAsync(m => m.IdTipoDiscapacidad == id);

                if (TipoDiscapacidad == null)
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
                    Resultado = TipoDiscapacidad,
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
        public async Task<Response> PutTipoDiscapacidad([FromRoute] int id, [FromBody] TipoDiscapacidad TipoDiscapacidad)
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

                var existe = await db.TipoDiscapacidad
                    .Where(p => 
                        p.Nombre == TipoDiscapacidad.Nombre
                    )
                    .FirstOrDefaultAsync();

                if (existe != null && existe.IdTipoDiscapacidad != id)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var TipoDiscapacidadActualizar = await db.TipoDiscapacidad.Where(x => x.IdTipoDiscapacidad == id).FirstOrDefaultAsync();

                if (TipoDiscapacidadActualizar != null)
                {
                    try
                    {
                        // Convertir a mayúsculas
                        TipoDiscapacidadActualizar.Nombre = TipoDiscapacidad.Nombre.ToString().ToUpper();
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
        [Route("InsertarTipoDiscapacidad")]
        public async Task<Response> PostTipoDiscapacidad([FromBody] TipoDiscapacidad TipoDiscapacidad)
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

                var respuesta = Existe(TipoDiscapacidad);
                if (!respuesta.IsSuccess)
                {
                    // Convertir a mayúsculas
                    TipoDiscapacidad.Nombre = TipoDiscapacidad.Nombre.ToString().ToUpper();

                    db.TipoDiscapacidad.Add(TipoDiscapacidad);
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
        public async Task<Response> DeleteTipoDiscapacidad([FromRoute] int id)
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

                var respuesta = await db.TipoDiscapacidad.SingleOrDefaultAsync(m => m.IdTipoDiscapacidad == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.TipoDiscapacidad.Remove(respuesta);
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
                    Message = Mensaje.Error,
                };
            }
        }

        private Response Existe(TipoDiscapacidad TipoDiscapacidad)
        {
            var bdd = TipoDiscapacidad.Nombre;
            var TipoDiscapacidadrespuesta = db.TipoDiscapacidad.Where(p => p.Nombre == bdd).FirstOrDefault();
            if (TipoDiscapacidadrespuesta != null)
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
                Resultado = TipoDiscapacidadrespuesta,
            };
        }

    }
}