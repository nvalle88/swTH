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
using bd.swth.entidades.Utils;
using bd.log.guardar.Enumeradores;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/IndiceOcupacionalCapacitaciones")]
    public class IndiceOcupacionalCapacitacionesController : Controller
    {
        private readonly SwTHDbContext db;

        public IndiceOcupacionalCapacitacionesController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarIndiceOcupacionalCapacitaciones")]
        public async Task<List<IndiceOcupacionalCapacitaciones>> GetIndiceOcupacionalCapacitaciones()
        {
            try
            {
                return await db.IndiceOcupacionalCapacitaciones.Include(x=> x.IndiceOcupacional).Include(x => x.Capacitacion).OrderBy(x => x.IdIndiceOcupacionalCapacitaciones).ToListAsync();
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = Mensaje.Excepcion,
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new List<IndiceOcupacionalCapacitaciones>();
            }
        }

        [HttpPost]
        [Route("ListaFiltradaCapacitaciones")]
        public async Task<List<Capacitacion>> GetListaFiltradaCapacitaciones([FromBody] IndiceOcupacional indiceOcupacional)
        {
            var ListaResult = new List<Capacitacion>();
            try
            {
               

                var listaCapacitaciones = await db.Capacitacion.ToListAsync();
                var listaIndiceCapacitacion = await db.IndiceOcupacionalCapacitaciones.Where(x=>x.IdIndiceOcupacional==indiceOcupacional.IdIndiceOcupacional).ToListAsync();

                if (listaIndiceCapacitacion.Count!=0)
                {
                foreach (var item in listaCapacitaciones)
                {
                    foreach (var item1 in listaIndiceCapacitacion)
                    {
                        if (item.IdCapacitacion!=item1.IdCapacitacion)
                        {
                            ListaResult.Add(item);
                        }
                    }
                }
                    return ListaResult;
                }

                return listaCapacitaciones;
                
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = Mensaje.Excepcion,
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return ListaResult;
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetIndiceOcupacionalCapacitaciones([FromRoute] int id)
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

                var IndiceOcupacionalCapacitaciones = await db.IndiceOcupacionalCapacitaciones.SingleOrDefaultAsync(m => m.IdIndiceOcupacionalCapacitaciones == id);

                if (IndiceOcupacionalCapacitaciones == null)
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
                    Resultado = IndiceOcupacionalCapacitaciones,
                };
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = Mensaje.Excepcion,
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }

        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarIndiceOcupacionalCapacitaciones")]
        public async Task<Response> PostIndiceOcupacionalCapacitaciones([FromBody] IndiceOcupacionalCapacitaciones IndiceOcupacionalCapacitaciones)
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

                var respuesta = Existe(IndiceOcupacionalCapacitaciones);
                if (!respuesta.IsSuccess)
                {
                    db.IndiceOcupacionalCapacitaciones.Add(IndiceOcupacionalCapacitaciones);
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
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = Mensaje.Excepcion,
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }

        // DELETE: api/BasesDatos/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteIndiceOcupacionalCapacitaciones([FromRoute] int id)
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

                var respuesta = await db.IndiceOcupacionalCapacitaciones.SingleOrDefaultAsync(m => m.IdIndiceOcupacionalCapacitaciones == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.IndiceOcupacionalCapacitaciones.Remove(respuesta);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                };
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex,
                    Message = Mensaje.Excepcion,
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }

        private Response Existe(IndiceOcupacionalCapacitaciones IndiceOcupacionalCapacitaciones)
        {
            var IndiceOcupacionalCapacitacionesrespuesta = db.IndiceOcupacionalCapacitaciones.Where(p => p.IdIndiceOcupacional== IndiceOcupacionalCapacitaciones.IdIndiceOcupacional && p.IdCapacitacion== IndiceOcupacionalCapacitaciones.IdCapacitacion).FirstOrDefault();
            if (IndiceOcupacionalCapacitacionesrespuesta != null)
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
                Resultado = IndiceOcupacionalCapacitacionesrespuesta,
            };
        }
    }
}