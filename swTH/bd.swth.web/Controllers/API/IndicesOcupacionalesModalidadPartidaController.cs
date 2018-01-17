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
    [Route("api/IndicesOcupacionalesModalidadPartida")]
    public class IndicesOcupacionalesModalidadPartidaController : Controller
    {
        private readonly SwTHDbContext db;

        public IndicesOcupacionalesModalidadPartidaController(SwTHDbContext db)
        {
            this.db = db;
        }


        // GET: api/IndiceOcupacionalModalidadPartida
        [HttpGet]
        [Route("ListarIndicesOcupacionalesModalidadPartida")]
        public async Task<List<IndiceOcupacionalModalidadPartida>> GetIndicesOcupacionalesModalidadPartida()
        {
            try
            {
                return await db.IndiceOcupacionalModalidadPartida.Include(x => x.IndiceOcupacional).Include(x => x.Empleado).Include(x => x.FondoFinanciamiento).Include(x => x.ModalidadPartida).Include(x => x.TipoNombramiento).OrderBy(x => x.Fecha).ToListAsync();
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
                return new List<IndiceOcupacionalModalidadPartida>();
            }
        }

        [HttpPost]
        [Route("IndiceOcupacionalModalidadPartidaPorIdEmpleado")]
        public async Task<Response> IndiceOcupacionalModalidadPartidaPorIdEmpleado([FromBody] IndiceOcupacionalModalidadPartida indiceOcupacionalModalidadPartida)
        {
            try
            {
                var IndiceOcupacionalModalidadPartida = await db.IndiceOcupacionalModalidadPartida.SingleOrDefaultAsync(m => m.IdEmpleado == indiceOcupacionalModalidadPartida.IdEmpleado);

                var response = new Response
                {
                    IsSuccess = true,
                    Resultado = IndiceOcupacionalModalidadPartida,
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



        // GET: api/IndiceOcupacionalModalidadPartida/5
        [HttpGet("{id}")]
        public async Task<Response> GetIndiceOcupacionalModalidadPartida([FromRoute] int id)
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

                var IndiceOcupacionalModalidadPartida = await db.IndiceOcupacionalModalidadPartida.SingleOrDefaultAsync(m => m.IdIndiceOcupacionalModalidadPartida == id);

                if (IndiceOcupacionalModalidadPartida == null)
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
                    Resultado = IndiceOcupacionalModalidadPartida,
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

        // PUT: api/IndiceOcupacionalModalidadPartida/5
        [HttpPut("{id}")]
        public async Task<Response> PutIndiceOcupacionalModalidadPartida([FromRoute] int id, [FromBody] IndiceOcupacionalModalidadPartida indiceOcupacionalModalidadPartida)
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

                var existe = Existe(indiceOcupacionalModalidadPartida);
                var IndiceOcupacionalModalidadPartidaActualizar = (IndiceOcupacionalModalidadPartida)existe.Resultado;
                if (existe.IsSuccess)
                {
                    if (IndiceOcupacionalModalidadPartidaActualizar.IdIndiceOcupacionalModalidadPartida == indiceOcupacionalModalidadPartida.IdIndiceOcupacionalModalidadPartida)
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

                var IndiceOcupacionalModalidadPartida = db.IndiceOcupacionalModalidadPartida.Find(indiceOcupacionalModalidadPartida.IdIndiceOcupacionalModalidadPartida);

                IndiceOcupacionalModalidadPartida.IdIndiceOcupacional = indiceOcupacionalModalidadPartida.IdIndiceOcupacional;
                IndiceOcupacionalModalidadPartida.IdEmpleado = indiceOcupacionalModalidadPartida.IdEmpleado;
                IndiceOcupacionalModalidadPartida.IdFondoFinanciamiento = indiceOcupacionalModalidadPartida.IdFondoFinanciamiento;
                IndiceOcupacionalModalidadPartida.IdModalidadPartida = indiceOcupacionalModalidadPartida.IdModalidadPartida;
                IndiceOcupacionalModalidadPartida.IdTipoNombramiento = indiceOcupacionalModalidadPartida.IdTipoNombramiento;
                IndiceOcupacionalModalidadPartida.Fecha = indiceOcupacionalModalidadPartida.Fecha;
                IndiceOcupacionalModalidadPartida.SalarioReal = indiceOcupacionalModalidadPartida.SalarioReal;

                db.IndiceOcupacionalModalidadPartida.Update(IndiceOcupacionalModalidadPartida);
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

        // POST: api/IndiceOcupacionalModalidadPartida
        [HttpPost]
        [Route("InsertarIndiceOcupacionalModalidadPartida")]
        public async Task<Response> PostIndiceOcupacionalModalidadPartida([FromBody] IndiceOcupacionalModalidadPartida IndiceOcupacionalModalidadPartida)
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

                var respuesta = Existe(IndiceOcupacionalModalidadPartida);
                if (!respuesta.IsSuccess)
                {
                    db.IndiceOcupacionalModalidadPartida.Add(IndiceOcupacionalModalidadPartida);
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

        // DELETE: api/IndiceOcupacionalModalidadPartida/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteIndiceOcupacionalModalidadPartida([FromRoute] int id)
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

                var respuesta = await db.IndiceOcupacionalModalidadPartida.SingleOrDefaultAsync(m => m.IdIndiceOcupacionalModalidadPartida == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.IndiceOcupacionalModalidadPartida.Remove(respuesta);
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

        private Response Existe(IndiceOcupacionalModalidadPartida IndiceOcupacionalModalidadPartida)
        {
            var fecha = IndiceOcupacionalModalidadPartida.Fecha;
            var salarioReal = IndiceOcupacionalModalidadPartida.SalarioReal;
            var IndiceOcupacionalModalidadPartidarespuesta = db.IndiceOcupacionalModalidadPartida.Where(p => p.Fecha == fecha && p.SalarioReal == salarioReal && p.IdIndiceOcupacional == IndiceOcupacionalModalidadPartida.IdIndiceOcupacional && p.IdEmpleado == IndiceOcupacionalModalidadPartida.IdEmpleado && p.IdFondoFinanciamiento == IndiceOcupacionalModalidadPartida.IdFondoFinanciamiento && p.IdModalidadPartida == IndiceOcupacionalModalidadPartida.IdModalidadPartida && p.IdTipoNombramiento == IndiceOcupacionalModalidadPartida.IdTipoNombramiento).FirstOrDefault();
            if (IndiceOcupacionalModalidadPartidarespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = IndiceOcupacionalModalidadPartidarespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = IndiceOcupacionalModalidadPartidarespuesta,
            };
        }

    }
}
