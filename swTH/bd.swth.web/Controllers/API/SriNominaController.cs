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
    [Route("api/SriNomina")]
    public class SriNominaController : Controller
    {
        private readonly SwTHDbContext db;

        public SriNominaController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarSriNomina")]
        public async Task<List<SriNomina>> ListarSriNomina()
        {
            try
            {
                return await db.SriNomina.ToListAsync();
            }
            catch (Exception )
            {
                return new List<SriNomina>();
            }
        }

        [HttpPost]
        [Route("ListarSriDetalle")]
        public async Task<List<SriDetalle>> ListarSriDetalle([FromBody] SriNomina sriNomina)
        {
            try
            {
                return await db.SriDetalle.Where(x=>x.IdSri==sriNomina.IdSri).Include(x => x.SriNomina).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<SriDetalle>();
            }
        }

        [HttpPost]
        [HttpPost("DesactivarSriNomina")]
        public async Task<Response> DesactivarSriNomina([FromBody] SriNomina sriNomina)
        {
            try
            {
                var lista = await db.SriNomina.Where(x => x.Activo == true).ToListAsync();
                foreach (var item in lista)
                {
                    item.Activo = false;
                }
                db.SriNomina.UpdateRange(lista);
                await db.SaveChangesAsync();

                var sriNominaActivar = await db.SriNomina.Where(x => x.IdSri == sriNomina.IdSri).FirstOrDefaultAsync();

                sriNominaActivar.Activo = false;
                await db.SaveChangesAsync();

                if (sriNominaActivar == null)
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
                    Resultado = sriNominaActivar,
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
        [HttpPost("ActivarSriNomina")]
        public async Task<Response> ActivarSriNomina([FromBody] SriNomina sriNomina)
        {
            try
            {
                var lista =await db.SriNomina.Where(x => x.Activo == true).ToListAsync();
                foreach (var item in lista)
                {
                    item.Activo = false;
                }
                db.SriNomina.UpdateRange(lista);
                await db.SaveChangesAsync();

                var sriNominaActivar = await db.SriNomina.Where(x => x.IdSri == sriNomina.IdSri).FirstOrDefaultAsync();

                sriNominaActivar.Activo = true;
                await db.SaveChangesAsync();

                if (sriNominaActivar == null)
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
                    Resultado = sriNominaActivar,
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

        [HttpGet]
        [HttpPost("ObtenerSriDetalle")]
        public async Task<Response> ObtenerSriDetalle([FromBody] SriDetalle sriDetalle)
        {
            try
            {
                var sriDetalleActualizar = await db.SriDetalle.SingleOrDefaultAsync(m => m.IdSriDetalle == sriDetalle.IdSriDetalle);

                if (sriDetalleActualizar == null)
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
                    Resultado = sriDetalleActualizar,
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
        // GET: api/BasesDatos/5
        [HttpGet]
        [HttpPost("ObtenerSriNomina")]
        public async Task<Response> ObtenerSriNomina([FromBody] SriNomina SriNomina)
        {
            try
            {
                var sriNomina = await db.SriNomina.SingleOrDefaultAsync(m => m.IdSri == SriNomina.IdSri);

                if (sriNomina == null)
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
                    Resultado = sriNomina,
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
        [Route("EditarSriDetalle")]
        public async Task<Response> EditarSriDetalle([FromBody] SriDetalle sriDetalle)
        {
            try
            {
                if (await Existe(sriDetalle))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var SriDetalleActualizar = await db.SriDetalle.Where(x => x.IdSriDetalle == sriDetalle.IdSriDetalle).FirstOrDefaultAsync();
                if (SriDetalleActualizar == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                    };

                }

                SriDetalleActualizar.FraccionBasica = sriDetalle.FraccionBasica;
                SriDetalleActualizar.ExcesoHasta = sriDetalle.ExcesoHasta;
                SriDetalleActualizar.ImpFranccionBasica = sriDetalle.ImpFranccionBasica;
                SriDetalleActualizar.PorcientoImpFraccExced = sriDetalle.PorcientoImpFraccExced;
                db.SriDetalle.Update(SriDetalleActualizar);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Resultado = SriDetalleActualizar
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
        [Route("EliminarSriDetalle")]
        public async Task<Response> EliminarSriDetalle([FromBody] SriDetalle sriDetalle)
        {
            try
            {
                var SriDetalleEliminar = await db.SriDetalle.Where(x => x.IdSriDetalle == sriDetalle.IdSriDetalle).FirstOrDefaultAsync();
                if (SriDetalleEliminar == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                    };

                }
                db.SriDetalle.Remove(SriDetalleEliminar);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Resultado = SriDetalleEliminar
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

        // PUT: api/BasesDatos/5
        [HttpPost]
        [Route("EditarSriNomina")]
        public async Task<Response> EditarSriNomina([FromBody] SriNomina SriNomina)
        {
            try
            {
                if (await Existe(SriNomina))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var SriNominaActualizar = await db.SriNomina.Where(x => x.IdSri == SriNomina.IdSri).FirstOrDefaultAsync();
                if (SriNominaActualizar == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                    };

                }

                SriNominaActualizar.Descripcion = SriNomina.Descripcion;
                db.SriNomina.Update(SriNominaActualizar);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Resultado = SriNominaActualizar
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
        [Route("InsertarSriDetalle")]
        public async Task<Response> InsertarSriNomina([FromBody] SriDetalle sriDetalle)
        {
            try
            {

                if (!await Existe(sriDetalle))
                {
                    db.SriDetalle.Add(sriDetalle);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                        Resultado = sriDetalle,
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

        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarSriNomina")]
        public async Task<Response> InsertarSriNomina([FromBody] SriNomina SriNomina)
        {
            try
            {

                if (!await Existe(SriNomina))
                {
                    db.SriNomina.Add(SriNomina);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                        Resultado = SriNomina,
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

        private async Task<bool> Existe(SriDetalle sriDetalle)
        {
            var basica = sriDetalle.FraccionBasica;
            var exceso = sriDetalle.ExcesoHasta;


            var SriNominarespuesta = await db.SriDetalle.Where(p => p.FraccionBasica == basica && p.ExcesoHasta==exceso && p.IdSri==sriDetalle.IdSri).FirstOrDefaultAsync();

            if (SriNominarespuesta == null || SriNominarespuesta.IdSriDetalle == sriDetalle.IdSriDetalle)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        private async Task<bool> Existe(SriNomina SriNomina)
        {
            var descripcion = SriNomina.Descripcion;
            var SriNominarespuesta = await db.SriNomina.Where(p => p.Descripcion == descripcion).FirstOrDefaultAsync();

            if (SriNominarespuesta == null || SriNominarespuesta.IdSri == SriNomina.IdSri)
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