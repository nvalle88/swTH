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
    [Route("api/DenominacionesCompetencias")]
    public class DenominacionesCompetenciasController : Controller
    {
        private readonly SwTHDbContext db;

        public DenominacionesCompetenciasController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarDenominacionesCompetencias")]
        public async Task<List<DenominacionCompetencia>> GetDenominacionesCompetencias()
        {
            try
            {
                return await db.DenominacionCompetencia.OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<DenominacionCompetencia>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetDenominacionCompetencia([FromRoute] int id)
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

                var DenominacionCompetencia = await db.DenominacionCompetencia.SingleOrDefaultAsync(m => m.IdDenominacionCompetencia == id);

                if (DenominacionCompetencia == null)
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
                    Resultado = DenominacionCompetencia,
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
        private async Task Actualizar(DenominacionCompetencia DenominacionCompetencia)
        {
            var capacitacionac = db.DenominacionCompetencia.Find(DenominacionCompetencia.IdDenominacionCompetencia);

            capacitacionac.CompetenciaTecnica = DenominacionCompetencia.CompetenciaTecnica;
            capacitacionac.Definicion = DenominacionCompetencia.Definicion;
            capacitacionac.Nombre = DenominacionCompetencia.Nombre;
            db.DenominacionCompetencia.Update(capacitacionac);
            await db.SaveChangesAsync();
        }

        // PUT: api/BasesDatos/5
        [HttpPut("{id}")]
        public async Task<Response> PutDenominacionCompetencia([FromRoute] int id, [FromBody] DenominacionCompetencia DenominacionCompetencia)
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

                var existe = Existe(DenominacionCompetencia);
                var DenominacionCompetenciaActualizar = (DenominacionCompetencia)existe.Resultado;

                if (existe.IsSuccess)
                {


                    //if (DenominacionCompetenciaActualizar.IdDenominacionCompetencia == DenominacionCompetencia.IdDenominacionCompetencia)
                    //{
                    //    if (DenominacionCompetencia.Nombre == DenominacionCompetenciaActualizar.Nombre &&
                    //    DenominacionCompetencia.Definicion == DenominacionCompetenciaActualizar.Definicion &&
                    //    DenominacionCompetencia.CompetenciaTecnica == DenominacionCompetenciaActualizar.CompetenciaTecnica)
                    //    {
                    //        return new Response
                    //        {
                    //            IsSuccess = true,
                    //        };
                    //    }

                    //    await Actualizar(DenominacionCompetencia);
                    //    return new Response
                    //    {
                    //        IsSuccess = true,
                    //        Message = Mensaje.Satisfactorio,
                    //    };
                    //}
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                await Actualizar(DenominacionCompetencia);
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
                    IsSuccess = true,
                    Message = Mensaje.Excepcion,
                };
            }
        }

        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarDenominacionCompetencia")]
        public async Task<Response> PostDenominacionCompetencia([FromBody] DenominacionCompetencia DenominacionCompetencia)
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

                var respuesta = Existe(DenominacionCompetencia);
                if (!respuesta.IsSuccess)
                {
                    db.DenominacionCompetencia.Add(DenominacionCompetencia);
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
        public async Task<Response> DeleteDenominacionCompetencia([FromRoute] int id)
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

                var respuesta = await db.DenominacionCompetencia.SingleOrDefaultAsync(m => m.IdDenominacionCompetencia == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.DenominacionCompetencia.Remove(respuesta);
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

        private Response Existe(DenominacionCompetencia DenominacionCompetencia)
        {
            var bdd = DenominacionCompetencia.Nombre.ToUpper().TrimEnd().TrimStart();
            var DenominacionCompetenciarespuesta = db.DenominacionCompetencia.Where(p => p.Nombre.ToUpper().TrimStart().TrimEnd() == bdd).FirstOrDefault();
            if (DenominacionCompetenciarespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = DenominacionCompetenciarespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = DenominacionCompetenciarespuesta,
            };
        }
    }
}