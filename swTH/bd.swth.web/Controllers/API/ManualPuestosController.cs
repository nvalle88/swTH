using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.Utils;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.swth.entidades.Enumeradores;
using bd.log.guardar.Enumeradores;
using MoreLinq;
using bd.swth.entidades.ViewModels;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/ManualPuestos")]
    public class ManualPuestosController : Controller
    {
        private readonly SwTHDbContext db;

        public ManualPuestosController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarManualPuestos")]
        public async Task<List<ManualPuesto>> GetManualPuestos()
        {
            try
            {
                return await db.ManualPuesto.Include(x=>x.RelacionesInternasExternas).OrderBy(x => x.Descripcion).ToListAsync();
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
                return new List<ManualPuesto>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetManualPuesto([FromRoute] int id)
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

                var ManualPuesto = await db.ManualPuesto.SingleOrDefaultAsync(m => m.IdManualPuesto == id);

                if (ManualPuesto == null)
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
                    Resultado = ManualPuesto,
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

        private async Task Actualizar(ManualPuesto ManualPuesto)
        {
            var manualpuesto = db.ManualPuesto.Find(ManualPuesto.IdManualPuesto);

            manualpuesto.Nombre = ManualPuesto.Nombre;
            manualpuesto.Descripcion = ManualPuesto.Descripcion;
            manualpuesto.Mision = ManualPuesto.Mision;
            manualpuesto.IdRelacionesInternasExternas = ManualPuesto.IdRelacionesInternasExternas;
            db.ManualPuesto.Update(manualpuesto);
            await db.SaveChangesAsync();
        }

        // PUT: api/BasesDatos/5
        [HttpPut("{id}")]
        public async Task<Response> PutManualPuesto([FromRoute] int id, [FromBody] ManualPuesto ManualPuesto)
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
                
                var existe = Existe(ManualPuesto);
                var ManualPuestoActualizar = (ManualPuesto)existe.Resultado;

                if (existe.IsSuccess)
                {


                    if (ManualPuestoActualizar.IdManualPuesto == ManualPuesto.IdManualPuesto)
                    {
                        if (ManualPuesto.Nombre == ManualPuestoActualizar.Nombre &&
                        ManualPuesto.Descripcion == ManualPuestoActualizar.Descripcion)
                        {
                            return new Response
                            {
                                IsSuccess = true,
                            };
                        }

                        await Actualizar(ManualPuesto);
                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.Satisfactorio,
                        };
                    }
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                await Actualizar(ManualPuesto);
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
        [Route("InsertarManualPuesto")]
        public async Task<Response> PostManualPuesto([FromBody] ManualPuesto ManualPuesto)
        {
            try
            {

                var respuesta = Existe(ManualPuesto);
                if (!respuesta.IsSuccess)
                {
                    db.ManualPuesto.Add(ManualPuesto);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio
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
        public async Task<Response> DeleteManualPuesto([FromRoute] int id)
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

                var respuesta = await db.ManualPuesto.SingleOrDefaultAsync(m => m.IdManualPuesto == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.ManualPuesto.Remove(respuesta);
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

        private Response Existe(ManualPuesto ManualPuesto)
        {
            var bdd = ManualPuesto.Nombre;
            var bdd1 = ManualPuesto.Descripcion;
            var bdd2 = ManualPuesto.Mision;
            var bdd3 = ManualPuesto.IdRelacionesInternasExternas;
            var ManualPuestorespuesta = db.ManualPuesto.Where(p => p.Nombre == bdd && p.Descripcion ==bdd1 && p.Mision == bdd2 && p.IdRelacionesInternasExternas == bdd3 ).FirstOrDefault();
            if (ManualPuestorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                   // Resultado = ManualPuestorespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = ManualPuestorespuesta,
            };
        }


        // POST: api/ManualPuestos
        [HttpPost]
        [Route("ListarManualPuestoPorDependencia")]
        public async Task<List<ManualPuesto>> ListarManualPuestoPorDependencia([FromBody] Dependencia dependencia)
        {
            try
            {

                var listaRoles = db.IndiceOcupacional
                    .Where(w => w.IdDependencia == dependencia.IdDependencia)
                    .Select(s => new ManualPuesto
                    {
                        IdManualPuesto = (int)s.IdManualPuesto,
                        Nombre = s.ManualPuesto.Nombre
                    }).DistinctBy(x => x.IdManualPuesto).ToList();



                return listaRoles;

            }
            catch (Exception ex)
            {
                return new List<ManualPuesto>();
            }
        }


        // POST: api/ManualPuestos
        [HttpPost]
        [Route("ListarManualPuestoPorSucursal")]
        public async Task<List<ManualPuesto>> ListarManualPuestoPorSucursal([FromBody] IdFiltrosViewModel Filtro)
        {
            try
            {

                var lista = db.IndiceOcupacionalModalidadPartida
                    .Where(w => w.IndiceOcupacional.Dependencia.IdSucursal == Filtro.IdSucursal)
                    .Select(s => new ManualPuesto
                    {
                        IdManualPuesto = (int)s.IndiceOcupacional.IdManualPuesto,
                        Nombre = s.IndiceOcupacional.ManualPuesto.Nombre
                    }).DistinctBy(x => x.IdManualPuesto).ToList();

                

                return lista;

            }
            catch (Exception ex)
            {
                return new List<ManualPuesto>();
            }
        }


    }
}