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

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/MisionesDeIndiceOcupacional")]
    public class MisionesDeIndiceOcupacionalController : Controller
    {
        private readonly SwTHDbContext db;

        public MisionesDeIndiceOcupacionalController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/MisionIndiceOcupacional
        [HttpGet]
        [Route("ListarMisionesDeIndiceOcupacional")]
        public async Task<List<MisionIndiceOcupacional>> GetMisionesDeIndiceOcupacional()
        {
            try
            {
                return await db.MisionIndiceOcupacional.Include(x => x.Mision).Include(x => x.IndiceOcupacional).OrderBy(x => x.IdIndiceOcupacional).ToListAsync();
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
                return new List<MisionIndiceOcupacional>();
            }
        }


        // GET: api/MisionIndiceOcupacional
        [HttpGet]
        [Route("ListarMisionesDeIndiceOcupacionalConId")]
        public async Task<List<MisionIndiceOcupacional>> GetMisionesDeIndiceOcupacionalConId(int codigoIndiceOcupacional)
        {
            try
            {
                return await db.MisionIndiceOcupacional.Include(x => x.Mision).Include(x => x.IndiceOcupacional).Where(x=>x.IdIndiceOcupacional==codigoIndiceOcupacional).OrderBy(x => x.IdIndiceOcupacional).ToListAsync();
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
                return new List<MisionIndiceOcupacional>();
            }
        }


        // GET: api/MisionIndiceOcupacional/5
        [HttpGet("{id}")]
        public async Task<Response> GetMisionIndiceOcupacional([FromRoute] int id)
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

                var MisionIndiceOcupacional = await db.MisionIndiceOcupacional.SingleOrDefaultAsync(m => m.IdMisionIndiceOcupacional == id);

                if (MisionIndiceOcupacional == null)
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
                    Resultado = MisionIndiceOcupacional,
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

        // PUT: api/MisionIndiceOcupacional/5
        [HttpPut("{id}")]
        public async Task<Response> PutMisionIndiceOcupacional([FromRoute] int id, [FromBody] MisionIndiceOcupacional MisionIndiceOcupacional)
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

                var existe = Existe(MisionIndiceOcupacional);
                var MisionIndiceOcupacionalActualizar = (MisionIndiceOcupacional)existe.Resultado;
                if (existe.IsSuccess)
                {
                    if (MisionIndiceOcupacionalActualizar.IdMisionIndiceOcupacional == MisionIndiceOcupacional.IdMisionIndiceOcupacional)
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
                var misionIndiceOcupacional = db.MisionIndiceOcupacional.Find(MisionIndiceOcupacional.IdMisionIndiceOcupacional);

                misionIndiceOcupacional.IdMision = MisionIndiceOcupacional.IdMision;
                misionIndiceOcupacional.IdIndiceOcupacional = MisionIndiceOcupacional.IdIndiceOcupacional;
                db.MisionIndiceOcupacional.Update(MisionIndiceOcupacional);
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

        // POST: api/MisionIndiceOcupacional
        [HttpPost]
        [Route("InsertarMisionIndiceOcupacional")]
        public async Task<Response> PostMisionIndiceOcupacional([FromBody] MisionIndiceOcupacional MisionIndiceOcupacional)
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

                var respuesta = Existe(MisionIndiceOcupacional);
                if (!respuesta.IsSuccess)
                {
                    db.MisionIndiceOcupacional.Add(MisionIndiceOcupacional);
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

        // DELETE: api/MisionIndiceOcupacional/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteMisionIndiceOcupacional([FromRoute] int id)
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

                var respuesta = await db.MisionIndiceOcupacional.SingleOrDefaultAsync(m => m.IdMisionIndiceOcupacional == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.MisionIndiceOcupacional.Remove(respuesta);
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

        private Response Existe(MisionIndiceOcupacional MisionIndiceOcupacional)
        {
 
            var MisionIndiceOcupacionalrespuesta = db.MisionIndiceOcupacional.Where(p => p.IdMision == MisionIndiceOcupacional.IdMision && p.IdIndiceOcupacional == MisionIndiceOcupacional.IdIndiceOcupacional).FirstOrDefault();
            if (MisionIndiceOcupacionalrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = MisionIndiceOcupacionalrespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = MisionIndiceOcupacionalrespuesta,
            };
        }

    }
}