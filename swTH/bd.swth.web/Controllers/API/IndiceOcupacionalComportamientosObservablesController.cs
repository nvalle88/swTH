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
    [Route("api/IndiceOcupacionalComportamientosObservables")]
    public class IndiceOcupacionalComportamientosObservablesController : Controller
    {
        private readonly SwTHDbContext db;

        public IndiceOcupacionalComportamientosObservablesController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarIndiceOcupacionalComportamientosObservables")]
        public async Task<List<IndiceOcupacionalComportamientoObservable>> GetIndiceOcupacionalComportamientoObservable()
        {
            try
            {
                return await db.IndiceOcupacionalComportamientoObservable.Include(x => x.IndiceOcupacional).Include(x => x.ComportamientoObservable).OrderBy(x => x.IdIndiceOcupacionalComportamientoObservable).ToListAsync();
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex.Message,
                    Message = Mensaje.Excepcion,
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new List<IndiceOcupacionalComportamientoObservable>();
            }
        }

        [HttpPost]
        [Route("ListaFiltradaComportamientosObservables")]
        public async Task<List<ComportamientoObservable>> GetListaFiltradaComportamientosObservables([FromBody] IndiceOcupacional indiceOcupacional)
        {
            var ListaResult = new List<ComportamientoObservable>();
            try
            {


                var listaComportamientoObservablees = await db.ComportamientoObservable.ToListAsync();
                var listaIndiceComportamientoObservable = await db.IndiceOcupacionalComportamientoObservable.Where(x => x.IdIndiceOcupacional == indiceOcupacional.IdIndiceOcupacional).ToListAsync();

                if (listaIndiceComportamientoObservable.Count != 0)
                {
                    foreach (var item in listaComportamientoObservablees)
                    {
                        foreach (var item1 in listaIndiceComportamientoObservable)
                        {
                            if (item.IdComportamientoObservable != item1.IdComportamientoObservable)
                            {
                                ListaResult.Add(item);
                            }
                        }
                    }
                    return ListaResult;
                }

                return listaComportamientoObservablees;

            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex.Message,
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
        public async Task<Response> GetIndiceOcupacionalComportamientoObservable([FromRoute] int id)
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

                var IndiceOcupacionalComportamientoObservable = await db.IndiceOcupacionalComportamientoObservable.SingleOrDefaultAsync(m => m.IdIndiceOcupacionalComportamientoObservable == id);

                if (IndiceOcupacionalComportamientoObservable == null)
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
                    Resultado = IndiceOcupacionalComportamientoObservable,
                };
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex.Message,
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
        [Route("InsertarIndiceOcupacionalComportamientoObservable")]
        public async Task<Response> PostIndiceOcupacionalComportamientoObservable([FromBody] IndiceOcupacionalComportamientoObservable IndiceOcupacionalComportamientoObservable)
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

                var respuesta = Existe(IndiceOcupacionalComportamientoObservable);
                if (!respuesta.IsSuccess)
                {
                    db.IndiceOcupacionalComportamientoObservable.Add(IndiceOcupacionalComportamientoObservable);
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
                    ExceptionTrace = ex.Message,
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
        public async Task<Response> DeleteIndiceOcupacionalComportamientoObservable([FromRoute] int id)
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

                var respuesta = await db.IndiceOcupacionalComportamientoObservable.SingleOrDefaultAsync(m => m.IdIndiceOcupacionalComportamientoObservable == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.IndiceOcupacionalComportamientoObservable.Remove(respuesta);
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
                    ExceptionTrace = ex.Message,
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

        private Response Existe(IndiceOcupacionalComportamientoObservable IndiceOcupacionalComportamientoObservable)
        {
            var IndiceOcupacionalComportamientoObservablerespuesta = db.IndiceOcupacionalComportamientoObservable.Where(p => p.IdIndiceOcupacional == IndiceOcupacionalComportamientoObservable.IdIndiceOcupacional && p.IdComportamientoObservable == IndiceOcupacionalComportamientoObservable.IdComportamientoObservable).FirstOrDefault();
            if (IndiceOcupacionalComportamientoObservablerespuesta != null)
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
                Resultado = IndiceOcupacionalComportamientoObservablerespuesta,
            };
        }
    }
}