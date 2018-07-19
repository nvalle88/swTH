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
    [Route("api/InstitucionesFinancieras")]
    public class InstitucionesFinancierasController : Controller
    {
        private readonly SwTHDbContext db;

        public InstitucionesFinancierasController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarInstitucionesFinancieras")]
        public async Task<List<InstitucionFinanciera>> GetInstitucionesFinancieras()
        {
            try
            {
                return await db.InstitucionFinanciera.OrderBy(x => x.Nombre).ToListAsync();
            }
            catch (Exception ex)
            {
                
                return new List<InstitucionFinanciera>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetInstitucionFinanciera([FromRoute] int id)
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

                var InstitucionFinanciera = await db.InstitucionFinanciera.SingleOrDefaultAsync(m => m.IdInstitucionFinanciera == id);

                if (InstitucionFinanciera == null)
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
                    Resultado = InstitucionFinanciera,
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

        private async Task Actualizar(InstitucionFinanciera InstitucionFinanciera)
        {
            var institucionfinanciera = db.InstitucionFinanciera.Find(InstitucionFinanciera.IdInstitucionFinanciera);

            institucionfinanciera.Nombre = InstitucionFinanciera.Nombre;
            institucionfinanciera.SPI = InstitucionFinanciera.SPI;
            db.InstitucionFinanciera.Update(institucionfinanciera);
            await db.SaveChangesAsync();
        }

        // PUT: api/BasesDatos/5
        [HttpPut("{id}")]
        public async Task<Response> PutInstitucionFinanciera([FromRoute] int id, [FromBody] InstitucionFinanciera InstitucionFinanciera)
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


                
                var existe = db.InstitucionFinanciera
                    .Where(w=>
                        w.IdInstitucionFinanciera != id
                        && (
                        w.Nombre.ToString().ToUpper() == InstitucionFinanciera.Nombre.ToString().ToUpper()
                        || w.SPI == InstitucionFinanciera.SPI
                        )
                    )
                    .ToList()
                ;
                

                if 
                (
                    existe.Count > 0 
                )
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                // convertir a mayúsculas
                InstitucionFinanciera.Nombre = InstitucionFinanciera.Nombre.ToString().ToUpper();

                await Actualizar(InstitucionFinanciera);
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.GuardadoSatisfactorio,
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
        [Route("InsertarInstitucionFinanciera")]
        public async Task<Response> PostInstitucionFinanciera([FromBody] InstitucionFinanciera InstitucionFinanciera)
        {
            try
            {

              
                var respuesta = Existe(InstitucionFinanciera);
                if (!respuesta.IsSuccess)
                {

                    // convertir a mayúsculas
                    InstitucionFinanciera.Nombre = InstitucionFinanciera.Nombre.ToString().ToUpper();

                    db.InstitucionFinanciera.Add(InstitucionFinanciera);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.GuardadoSatisfactorio
                    };
                }

                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = ""
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
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }

        // DELETE: api/BasesDatos/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteInstitucionFinanciera([FromRoute] int id)
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

                var respuesta = await db.InstitucionFinanciera.SingleOrDefaultAsync(m => m.IdInstitucionFinanciera == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.InstitucionFinanciera.Remove(respuesta);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.BorradoSatisfactorio,
                };
            }
            catch (Exception ex)
            {
               
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.BorradoNoSatisfactorio,
                };
            }
        }

        private Response Existe(InstitucionFinanciera InstitucionFinanciera)
        {
            var bdd = InstitucionFinanciera.Nombre;
            var bdd1 = InstitucionFinanciera.SPI;

            var InstitucionFinancierarespuesta = db.InstitucionFinanciera
                .Where(p =>
                    p.Nombre.ToString().ToUpper() == bdd.ToString().ToUpper()
                    || p.SPI == bdd1
                ).FirstOrDefault();
            

            if (InstitucionFinancierarespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = InstitucionFinancierarespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = InstitucionFinancierarespuesta,
            };
        }
    }
}