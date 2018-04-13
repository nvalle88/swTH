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
    [Route("api/ConfirmacionesLecturas")]
    public class ConfirmacionesLecturasController : Controller
    {
         private readonly SwTHDbContext db;

        public ConfirmacionesLecturasController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarConfirmacionesLecturas")]
        public async Task<List<ConfirmacionLectura>> GetConfirmacionesLecturas()
        {
            try
            {
                return await db.ConfirmacionLectura.Include(x => x.Empleado).OrderBy(x => x.IdEmpleado).ToListAsync();

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
                return new List<ConfirmacionLectura>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetConfirmacionLectura([FromRoute] int id)
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

                var ConfirmacionLectura = await db.ConfirmacionLectura.SingleOrDefaultAsync(m => m.IdConfirmacionLectura == id);

                if (ConfirmacionLectura == null)
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
                    Resultado = ConfirmacionLectura,
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
        [Route("InsertarConfirmacionLectura")]
        public async Task<Response> PostConfirmacionLectura([FromBody] ConfirmacionLectura ConfirmacionLectura)
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

                var respuesta = Existe(ConfirmacionLectura);
                if (!respuesta.IsSuccess)
                {
                    db.ConfirmacionLectura.Add(ConfirmacionLectura);
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
                    Message = "Existe una confirmación de lectura de igual información"
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


        private Response Existe(ConfirmacionLectura ConfirmacionLectura)
        {
            var bdd = ConfirmacionLectura.IdEmpleado;
            var ConfirmacionLecturarespuesta = db.ConfirmacionLectura.Where(p => p.IdEmpleado == bdd).FirstOrDefault();
            if (ConfirmacionLecturarespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Existe una confirmación de lecutra de igual información",
                    Resultado = null,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = ConfirmacionLecturarespuesta,
            };
        }
    }
}