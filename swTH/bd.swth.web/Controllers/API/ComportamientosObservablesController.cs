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
using bd.swth.entidades.ViewModels;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/ComportamientosObservables")]
    public class ComportamientosObservablesController : Controller
    {
        private readonly SwTHDbContext db;

        public ComportamientosObservablesController(SwTHDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        [Route("ListarComportamientosObservablesPorIndiceOcupacional")]
        public async Task<List<ComportamientoObservableViewModel>> ListarComportamientosObservablesPorIndiceOcupacional([FromBody]IndiceOcupacional indiceOcupacional)
        {
            var ListaComportamientoObservables = await db.IndiceOcupacionalComportamientoObservable
                                                .Join(db.IndiceOcupacional
                                                , indiceComportamiento => indiceComportamiento.IdIndiceOcupacional, indice => indice.IdIndiceOcupacional,
                                                (indiceConocimiento, indice) => new { IndiceOcupacionalComportamientoObservable = indiceConocimiento, IndiceOcupacional = indice })
                                                .Join(db.ComportamientoObservable
                                                , indice_1 => indice_1.IndiceOcupacionalComportamientoObservable.IdComportamientoObservable, comportamientoObservable => comportamientoObservable.IdComportamientoObservable,
                                                (indice_1, comportamientoObservable) => new { ca = indice_1, rt = comportamientoObservable })
                                                .Where(ds => ds.ca.IndiceOcupacional.IdIndiceOcupacional == indiceOcupacional.IdIndiceOcupacional)
                                                .Select(t => new ComportamientoObservableViewModel
                                                {
                                                    IdComportamientoObservable = t.rt.IdComportamientoObservable,
                                                    Descripcion = t.rt.Descripcion,
                                                    CompetenciaTecnicaDenominacionCompetencia=t.rt.DenominacionCompetencia.CompetenciaTecnica,
                                                    DefinicionDenominacionCompetencia=t.rt.DenominacionCompetencia.Definicion,
                                                    IdDenominacionCompetencia=Convert.ToInt32(t.rt.IdDenominacionCompetencia),
                                                    IdIndiceOcupacional=t.ca.IndiceOcupacional.IdIndiceOcupacional,
                                                    IdNivel=Convert.ToInt32( t.rt.IdNivel),
                                                    NombreDenominacionCompetencia=t.rt.DenominacionCompetencia.Nombre,
                                                    NombreNivel=t.rt.Nivel.Nombre,
                                                   
                                                })
                                                .ToListAsync();


            if (ListaComportamientoObservables.Count == 0)
            {
                ListaComportamientoObservables.Add(new ComportamientoObservableViewModel { IdIndiceOcupacional = indiceOcupacional.IdIndiceOcupacional, IdComportamientoObservable = -1 });
            }

            return ListaComportamientoObservables;
        }


        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarComportamientosObservables")]
        public async Task<List<ComportamientoObservable>> GetComportamientosObservables()
        {
            try
            {
                return await db.ComportamientoObservable.Include(x => x.Nivel).Include(x => x.DenominacionCompetencia).OrderBy(x => x.Descripcion).ToListAsync();
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
                return new List<ComportamientoObservable>();
            }
        }


        [HttpPost]
        [Route("ListarComportamientosObservablesNoAsignadasIndiceOcupacional")]
        public async Task<List<ComportamientoObservableViewModel>> ListarActividedesEsencialesNoAsignadasIndiceOcupacional([FromBody]IndiceOcupacional indiceOcupacional)
        {

                var Lista = await db.ComportamientoObservable
                                   .Where(ac => !db.IndiceOcupacionalComportamientoObservable
                                                   .Where(a => a.IndiceOcupacional.IdIndiceOcupacional == indiceOcupacional.IdIndiceOcupacional)
                                                   .Select(ioac => ioac.IdComportamientoObservable)
                                                   .Contains(ac.IdComportamientoObservable)).Include(x=>x.Nivel).Include(x=>x.DenominacionCompetencia)
                                          .ToListAsync();

            var listaSalida = new List<ComportamientoObservableViewModel>();

            if (Lista.Count == 0)
            {
                listaSalida.Add(new ComportamientoObservableViewModel { IdIndiceOcupacional = indiceOcupacional.IdIndiceOcupacional, IdComportamientoObservable = -1 });
            }

            else
            {
                foreach (var item in Lista)
                {
                    listaSalida.Add(new ComportamientoObservableViewModel
                    {
                        NombreNivel=item.Nivel.Nombre,
                        Descripcion=item.Descripcion,
                        CompetenciaTecnicaDenominacionCompetencia=item.DenominacionCompetencia.CompetenciaTecnica,
                        DefinicionDenominacionCompetencia=item.DenominacionCompetencia.Definicion,
                        NombreDenominacionCompetencia=item.DenominacionCompetencia.Nombre,
                        IdComportamientoObservable = item.IdComportamientoObservable,
                        IdIndiceOcupacional = indiceOcupacional.IdIndiceOcupacional,
                    });
                }
            }

            return listaSalida;

        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetComportamientoObservable([FromRoute] int id)
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

                var ComportamientoObservable = await db.ComportamientoObservable.SingleOrDefaultAsync(m => m.IdComportamientoObservable == id);

                if (ComportamientoObservable == null)
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
                    Resultado = ComportamientoObservable,
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

        [HttpPost]
        [Route("EliminarIndiceOcupacionalComportamiemtoObservable")]
        public async Task<Response> EliminarIndiceOcupacionalComportamiemtoObservable([FromBody] IndiceOcupacionalComportamientoObservable indiceOcupacionalComportamientoObservable)
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

                var respuesta = await db.IndiceOcupacionalComportamientoObservable.SingleOrDefaultAsync(m => m.IdComportamientoObservable == indiceOcupacionalComportamientoObservable.IdComportamientoObservable
                                      && m.IdIndiceOcupacional == indiceOcupacionalComportamientoObservable.IdIndiceOcupacional);
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



        // PUT: api/BasesDatos/5
        [HttpPut("{id}")]
        public async Task<Response> PutComportamientoObservable([FromRoute] int id, [FromBody] ComportamientoObservable ComportamientoObservable)
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

                var existe = Existe(ComportamientoObservable);
                var ComportamientoObservableActualizar = (ComportamientoObservable)existe.Resultado;
                if (existe.IsSuccess)
                {
                    //if (ComportamientoObservableActualizar.IdComportamientoObservable == ComportamientoObservable.IdComportamientoObservable)
                    //{
                    //    return new Response
                    //    {
                    //        IsSuccess = true,
                    //    };
                    //}
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }
                var comportamiento = db.ComportamientoObservable.Find(ComportamientoObservable.IdComportamientoObservable);

                comportamiento.Descripcion = ComportamientoObservable.Descripcion;
                comportamiento.IdNivel = ComportamientoObservable.IdNivel;
                comportamiento.IdDenominacionCompetencia = ComportamientoObservable.IdDenominacionCompetencia;
                db.ComportamientoObservable.Update(comportamiento);
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

        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarComportamientoObservable")]
        public async Task<Response> PostComportamientoObservable([FromBody] ComportamientoObservable ComportamientoObservable)
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

                var respuesta = Existe(ComportamientoObservable);
                if (!respuesta.IsSuccess)
                {
                    db.ComportamientoObservable.Add(ComportamientoObservable);
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
        public async Task<Response> DeleteComportamientoObservable([FromRoute] int id)
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

                var respuesta = await db.ComportamientoObservable.SingleOrDefaultAsync(m => m.IdComportamientoObservable == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.ComportamientoObservable.Remove(respuesta);
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


        [HttpPost]
        [Route("EliminarIndiceOcupacionalComportamientosObservables")]
        public async Task<Response> EliminarIndiceOcupacionalComportamientosObservables([FromBody] IndiceOcupacionalComportamientoObservable indiceOcupacionalComportamientoObservable)
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

                var respuesta = await db.IndiceOcupacionalComportamientoObservable.SingleOrDefaultAsync(m => m.IdComportamientoObservable == indiceOcupacionalComportamientoObservable.IdComportamientoObservable && m.IdIndiceOcupacional == indiceOcupacionalComportamientoObservable.IdIndiceOcupacional);
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

        private Response Existe(ComportamientoObservable ComportamientoObservable)
        {
            var bdd = ComportamientoObservable.Descripcion.ToUpper().TrimEnd().TrimStart();
            var bdd1 = ComportamientoObservable.IdDenominacionCompetencia;
            var bdd2 = ComportamientoObservable.IdNivel;
            var ComportamientoObservablerespuesta = db.ComportamientoObservable.Where(p => p.Descripcion.ToUpper().TrimStart().TrimEnd() == bdd && p.IdNivel == bdd2 && p.IdDenominacionCompetencia == bdd1).FirstOrDefault();
            if (ComportamientoObservablerespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = ComportamientoObservablerespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = ComportamientoObservablerespuesta,
            };
        }
    }
}