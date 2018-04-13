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
    [Route("api/AntecedentesFamiliares")]
    public class AntecedentesFamiliaresController : Controller
    {
        private readonly SwTHDbContext db;

        public AntecedentesFamiliaresController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/AntecedentesFamiliares
        [HttpGet]
        [Route("ListarAntecedentesFamiliares")]
        public async Task<List<AntecedentesFamiliares>> GetAntecedentesFamiliares()
        {

            try
            {
                return await db.AntecedentesFamiliares.Include( x => x.FichaMedica).OrderBy(x => x.IdAntecedentesFamiliares).ToListAsync();
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
                return new List<AntecedentesFamiliares>();
            }
        }



        // GET: api/AntecedentesFamiliares/5
        [HttpGet("{id}")]
        public async Task<Response> GetAntecedentesFamiliares([FromRoute] int id)
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

                var AntecedentesFamiliares = await db.AntecedentesFamiliares.SingleOrDefaultAsync(m => m.IdAntecedentesFamiliares == id);


                if (AntecedentesFamiliares == null)
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
                    Resultado = AntecedentesFamiliares,
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

        // PUT: api/AntecedentesFamiliares/5
        [HttpPut("{id}")]
        public async Task<Response> PutAntecedentesFamiliares([FromRoute] int id, [FromBody] AntecedentesFamiliares AntecedentesFamiliares)
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

                var existe = Existe(AntecedentesFamiliares);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var Actualizar = await db.AntecedentesFamiliares.Where(x => x.IdAntecedentesFamiliares == id).FirstOrDefaultAsync();
                if (Actualizar != null)
                {
                    try
                    {

                        Actualizar.Parentesco = AntecedentesFamiliares.Parentesco;
                        Actualizar.Enfermedad = AntecedentesFamiliares.Enfermedad;
                        Actualizar.Observaciones = AntecedentesFamiliares.Observaciones;
                        Actualizar.IdFichaMedica = AntecedentesFamiliares.IdFichaMedica;

                        db.AntecedentesFamiliares.Update(Actualizar);

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


                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ExisteRegistro,
                };


            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };
            }

        }

        // POST: api/AntecedentesFamiliares
        [HttpPost]
        [Route("InsertarAntecedentesFamiliares")]
        public async Task<Response> InsertarAntecedentesFamiliares([FromBody] AntecedentesFamiliares AntecedentesFamiliares)
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

                var respuesta = Existe(AntecedentesFamiliares);
                if (!respuesta.IsSuccess)
                {
                    db.AntecedentesFamiliares.Add(AntecedentesFamiliares);
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



        // Post: api/AntecedentesFamiliares
        [HttpPost]
        [Route("ListarAntecedentesFamiliaresPorFicha")]
        public async Task<Response> GetAntecedentesLaboralesPorFicha([FromBody] int idFicha)
        {

            Response response = new entidades.Utils.Response();

            try
            {
                var lista = await db.AntecedentesFamiliares.Where(x => x.IdFichaMedica == idFicha).OrderBy(x => x.IdAntecedentesFamiliares).ToListAsync();


                return new Response { IsSuccess = true, Resultado = lista };

            }
            catch (Exception ex)
            {

                return new Response { IsSuccess = false, Message = Mensaje.Excepcion };
            }


        }




        // DELETE: api/AntecedentesFamiliares/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteAntecedentesFamiliares([FromRoute] int id)
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

                var respuesta = await db.AntecedentesFamiliares.SingleOrDefaultAsync(m => m.IdAntecedentesFamiliares == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.AntecedentesFamiliares.Remove(respuesta);
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
                    Message = Mensaje.BorradoNoSatisfactorio,
                };
            }

        }




        private Response Existe(AntecedentesFamiliares AntecedentesFamiliares)
        {
            var afp = AntecedentesFamiliares.Parentesco.ToUpper().TrimEnd().TrimStart();
            var afe = AntecedentesFamiliares.Enfermedad.ToUpper().TrimEnd().TrimStart();
            var afo = AntecedentesFamiliares.Observaciones.ToUpper().TrimEnd().TrimStart();
            var afi = AntecedentesFamiliares.IdFichaMedica;


            var Respuesta = db.AntecedentesFamiliares.Where(

                    p => p.Parentesco.ToUpper().TrimEnd().TrimStart() == afp
                    && p.Enfermedad.ToUpper().TrimEnd().TrimStart() == afe
                    && p.Observaciones.ToUpper().TrimEnd().TrimStart() == afo
                    && p.IdFichaMedica == afi

                ).FirstOrDefault();

            if (Respuesta != null)
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
                Resultado = Respuesta,
            };
        }

    }
}