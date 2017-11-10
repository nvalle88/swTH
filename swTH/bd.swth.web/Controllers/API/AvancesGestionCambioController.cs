using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.Enumeradores;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.log.guardar.Enumeradores;
using bd.swth.entidades.Utils;
using bd.swth.entidades.ViewModels;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/AvancesGestionCambio")]
    public class AvancesGestionCambioController : Controller
    {
        private readonly SwTHDbContext db;

        public AvancesGestionCambioController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/AvanceGestionCambio
        [HttpGet]
        [Route("ListarAvancesGestionCambio")]
        public async Task<List<AvanceGestionCambio>> GetAvanceGestionCambio()
        {
            try
            {
                return await db.AvanceGestionCambio.OrderBy(x => x.Fecha).ToListAsync();
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
                return new List<AvanceGestionCambio>();
            }
        }
        //int IdPlanGestionCambio - ListarAvanceGestionCambioconIdPlan
        // POST: api/AvanceGestionCambio
        [HttpPost]
        [Route("ListarAvanceGestionCambioconIdActividad")]
        public async Task<List<AvanceGestionCambio>> ListarAvanceGestionCambioconIdActividad([FromBody] AvanceGestionCambio AvanceGestionCambio)
        {
            try
            {
                return await db.AvanceGestionCambio.Where(m => m.IdActividadesGestionCambio == AvanceGestionCambio.IdActividadesGestionCambio).ToListAsync();
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
                return new List<AvanceGestionCambio>();
            }
        }


        //int IdActividadesGestionCambio - ActividadesGestionCambioconIdActividad
        // POST: api/ActividadesGestionCambio
        [HttpPost]
        [Route("AvanceGestionCambioSumaIndicadorReal")]
        public async Task<Response> AvanceGestionCambioSumaIndicadorReal([FromBody] ActividadesGestionCambio actividadesGestionCambio)
        {
            try
            {
                
                var sumaAvance = new AvanceGestionCambioModel
                {
                    Suma = db.AvanceGestionCambio.Where(m => m.IdActividadesGestionCambio == actividadesGestionCambio.IdActividadesGestionCambio).Sum(m => m.Indicadorreal)
                };

                var response = new Response
                {
                    IsSuccess = true,
                    Resultado = sumaAvance,
                };

                return response;

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
                return new Response { };
            }
        }



        // GET: api/AvanceGestionCambio/5
        [HttpGet("{id}")]
        public async Task<Response> GetAvanceGestionCambio([FromRoute] int id)
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

                var AvanceGestionCambio = await db.AvanceGestionCambio.SingleOrDefaultAsync(m => m.IdAvanceGestionCambio == id);

                if (AvanceGestionCambio == null)
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
                    Resultado = AvanceGestionCambio,
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




        // PUT: api/AvanceGestionCambio/5
        [HttpPut("{id}")]
        public async Task<Response> PutAvanceGestionCambio([FromRoute] int id, [FromBody] AvanceGestionCambio avanceGestionCambio)
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



                if (avanceGestionCambio.Indicadorreal == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "El indicador real no puede ser cero"
                    };
                }


                if (avanceGestionCambio.Fecha <= DateTime.Today)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha de inicio no puede ser menor o igual que la fecha de hoy"
                    };
                }

                string fecha = avanceGestionCambio.Fecha.DayOfWeek.ToString();

                if (fecha.Equals("Saturday") || fecha.Equals("Sunday"))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha de inicio no puede ser fin de semana"
                    };
                }


                ActividadesGestionCambio Actividades = db.ActividadesGestionCambio.Find(avanceGestionCambio.IdActividadesGestionCambio);

                if (Actividades.FechaInicio > avanceGestionCambio.Fecha)
                {

                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha del avance no puede ser menor a la fecha inicio de las actividades"
                    };
                }

                var existe = Existe(avanceGestionCambio);
                var AvanceGestionCambioActualizar = (AvanceGestionCambio)existe.Resultado;
                if (existe.IsSuccess)
                {
                    if (AvanceGestionCambioActualizar.IdAvanceGestionCambio == avanceGestionCambio.IdAvanceGestionCambio)
                    {
                        return new Response
                        {
                            IsSuccess = true,
                        };
                    }
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }
                var AvanceGestionCambio = db.AvanceGestionCambio.Find(avanceGestionCambio.IdAvanceGestionCambio);

                AvanceGestionCambio.Fecha = avanceGestionCambio.Fecha;
                AvanceGestionCambio.Indicadorreal = avanceGestionCambio.Indicadorreal;
                AvanceGestionCambio.IdActividadesGestionCambio = avanceGestionCambio.IdActividadesGestionCambio;

                db.AvanceGestionCambio.Update(AvanceGestionCambio);
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
                    IsSuccess = true,
                    Message = Mensaje.Excepcion,
                };
            }

        }

        // POST: api/AvanceGestionCambio
        [HttpPost]
        [Route("InsertarAvanceGestionCambio")]
        public async Task<Response> PostAvanceGestionCambio([FromBody] AvanceGestionCambio AvanceGestionCambio)
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


                if (AvanceGestionCambio.Indicadorreal == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "El indicador real no puede ser cero"
                    };
                }


                if (AvanceGestionCambio.Fecha <= DateTime.Today)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha de inicio no puede ser menor o igual que la fecha de hoy"
                    };
                }
               
                string fecha = AvanceGestionCambio.Fecha.DayOfWeek.ToString();

                if (fecha.Equals("Saturday") || fecha.Equals("Sunday"))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha de inicio no puede ser fin de semana"
                    };
                }

                ActividadesGestionCambio Actividades = db.ActividadesGestionCambio.Find(AvanceGestionCambio.IdActividadesGestionCambio);

                if (Actividades.FechaInicio > AvanceGestionCambio.Fecha)
                {

                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La fecha del avance no puede ser menor a la fecha inicio de las actividades"
                    };
                }

                var respuesta = Existe(AvanceGestionCambio);
                if (!respuesta.IsSuccess)
                {
                    db.AvanceGestionCambio.Add(AvanceGestionCambio);
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
                    Message = Mensaje.ExisteRegistro,
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

        // DELETE: api/AvanceGestionCambio/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteAvanceGestionCambio([FromRoute] int id)
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

                var respuesta = await db.AvanceGestionCambio.SingleOrDefaultAsync(m => m.IdAvanceGestionCambio == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.AvanceGestionCambio.Remove(respuesta);
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

        private Response Existe(AvanceGestionCambio AvanceGestionCambio)
        {

            var AvanceGestionCambiorespuesta = db.AvanceGestionCambio.Where(p => p.Fecha == AvanceGestionCambio.Fecha &&
                                                                                      p.Indicadorreal == AvanceGestionCambio.Indicadorreal &&
                                                                                      p.IdActividadesGestionCambio == AvanceGestionCambio.IdActividadesGestionCambio
                                                                                      ).FirstOrDefault();
            if (AvanceGestionCambiorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = AvanceGestionCambiorespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = AvanceGestionCambiorespuesta,
            };
        }
        
    }
}