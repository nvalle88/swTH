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
    [Route("api/EscalasGrados")]
    public class EscalasGradosController : Controller
    {
        private readonly SwTHDbContext db;

        public EscalasGradosController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarEscalasGrados")]
        public async Task<List<EscalaGrados>> GetEscalasGrados()
        {
            try
            {
                return await db.EscalaGrados.Include(x => x.GrupoOcupacional).OrderBy(x => x.Grado).ToListAsync();

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
                return new List<EscalaGrados>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetEscalaGrados([FromRoute] int id)
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

                var EscalaGrados = await db.EscalaGrados.SingleOrDefaultAsync(m => m.IdEscalaGrados == id);

                if (EscalaGrados == null)
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
                    Resultado = EscalaGrados,
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


        private async Task Actualizar(EscalaGrados escalaGrados)
        {
            var escalaevatotal = db.EscalaGrados.Find(escalaGrados.IdEscalaGrados);

            escalaevatotal.IdGrupoOcupacional = escalaGrados.IdGrupoOcupacional;
            escalaevatotal.Grado = escalaGrados.Grado;
            escalaevatotal.Remuneracion = escalaGrados.Remuneracion;
            escalaevatotal.Nombre = escalaGrados.Nombre;
            db.EscalaGrados.Update(escalaevatotal);
            await db.SaveChangesAsync();
        }

        // PUT: api/BasesDatos/5
        [HttpPut("{id}")]
        public async Task<Response> PutEscalaGrados([FromRoute] int id, [FromBody] EscalaGrados EscalaGrados)
        {
           
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ModeloInvalido
                    };
                }

            var existe = Existe(EscalaGrados);
            var EscalaGradosActualizar = (EscalaGrados)existe.Resultado;

            if (existe.IsSuccess)
            {


                //if (EscalaGradosActualizar.IdEscalaGrados == EscalaGrados.IdEscalaGrados)
                //{
                //    if (EscalaGrados.IdGrupoOcupacional == EscalaGradosActualizar.IdGrupoOcupacional &&
                //    EscalaGrados.Grado == EscalaGradosActualizar.Grado &&
                //    EscalaGrados.Remuneracion == EscalaGradosActualizar.Remuneracion &&
                //    EscalaGrados.Nombre == EscalaGradosActualizar.Nombre)
                //    {
                //        return new Response
                //        {
                //            IsSuccess = true,
                //            Message=Mensaje.ExisteRegistro,
                //        };
                //    }

                //    await Actualizar(EscalaGrados);
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

            await Actualizar(EscalaGrados);
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.Satisfactorio,
            };

        }

        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarEscalaGrados")]
        public async Task<Response> PostEscalaGrados([FromBody] EscalaGrados EscalaGrados)
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

                var respuesta = Existe(EscalaGrados);
                if (!respuesta.IsSuccess)
                {
                    db.EscalaGrados.Add(EscalaGrados);
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
        public async Task<Response> DeleteEscalaGrados([FromRoute] int id)
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

                var respuesta = await db.EscalaGrados.SingleOrDefaultAsync(m => m.IdEscalaGrados == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.EscalaGrados.Remove(respuesta);
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

        private Response Existe(EscalaGrados EscalaGrados)
        {
            var bdd = EscalaGrados.Grado;
            var bbd1 = EscalaGrados.Remuneracion;
            var bbd2 = EscalaGrados.IdGrupoOcupacional;
            var bbd3 = EscalaGrados.Nombre;

            var EscalaGradosrespuesta = db.EscalaGrados.Where(p => p.Grado == bdd && p.Remuneracion== bbd1 && p.IdGrupoOcupacional == bbd2 && p.Nombre == bbd3).FirstOrDefault();
            if (EscalaGradosrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = EscalaGradosrespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = EscalaGradosrespuesta,
            };
        }

        [HttpPost]
        [Route("ListarEscalasGradosPorGrupoOcupacional")]
        public async Task<List<EscalaGrados>> GetSucursalbyCity([FromBody] GrupoOcupacional grupoocupacional)
        {
            try
            {
                return await db.EscalaGrados.Include(c => c.GrupoOcupacional).Where(x => x.IdGrupoOcupacional == grupoocupacional.IdGrupoOcupacional).OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<EscalaGrados>();
            }
        }
    }
}