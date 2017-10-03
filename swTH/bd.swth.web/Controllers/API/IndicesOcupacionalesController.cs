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
using bd.swth.entidades.ObjectTransfer;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/IndicesOcupacionales")]
    public class IndicesOcupacionalesController : Controller
    {
        private readonly SwTHDbContext db;

        public IndicesOcupacionalesController(SwTHDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("ListarIndicesOcupaciones")]
        public async Task<List<IndiceOcupacional>> GetIndicesOcupacionales()
        {
            try
            {
                //Escala de grados
                //Include(x => x.Dependencia.Nombre).Include(x => x.Dependencia.IdDependencia).Include(x => x.ManualPuesto.Nombre).Include(x => x.RolPuesto)
                var lista=await db.IndiceOcupacional.ToListAsync();
                var lista1 = new List<IndiceOcupacional>();

                foreach (var item in lista)
                {
                    var escalaGrados =await db.EscalaGrados.Where(x => x.IdEscalaGrados == item.IdEscalaGrados).FirstOrDefaultAsync();
                    var dependencia = await db.Dependencia.Where(x => x.IdDependencia == item.IdDependencia).FirstOrDefaultAsync();
                    var manualPuesto = await db.ManualPuesto.Where(x => x.IdManualPuesto == item.IdManualPuesto).FirstOrDefaultAsync();
                    var rolPuesto = await db.RolPuesto.Where(x => x.IdRolPuesto == item.IdRolPuesto).FirstOrDefaultAsync();


                    var grados = new EscalaGrados
                    {
                        Grado = escalaGrados.Grado,
                        GrupoOcupacional = escalaGrados.GrupoOcupacional,
                        Remuneracion=escalaGrados.Remuneracion,
                    };

                    var dependencia1 = new Dependencia
                    {
                        Nombre =dependencia.Nombre,
                       DependenciaPadre=dependencia.DependenciaPadre,
                    };


                    var manual = new ManualPuesto
                    {
                        Nombre = manualPuesto.Nombre,
                        Descripcion=manualPuesto.Descripcion,
                       
                    };

                    var rol = new RolPuesto
                    {
                        Nombre = rolPuesto.Nombre,
                    };

                    item.EscalaGrados = grados;
                    item.Dependencia = dependencia1;
                    item.ManualPuesto = manual;
                    item.RolPuesto = rol;
                    lista1.Add(item);
                }

                return lista1;
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
                return new List<IndiceOcupacional>();
            }
        }

        // GET: api/IndicesOcupacionales
        [HttpGet]
        [Route("ListarActividadesEsenciales")]
        public async Task<List<IndiceOcupacional>> GetIndicesOcupacionales(IndiceOcupacional indiceOcupacional)
        {
            try
            {
                return await db.IndiceOcupacional.Where(x=>x.IdIndiceOcupacional==indiceOcupacional.IdIndiceOcupacional).Include(x=>x.Dependencia).Include(x=>x.ManualPuesto).Include(x=>x.RolPuesto)
                             .Include(x=>x.EscalaGrados).ThenInclude(x=>x.GrupoOcupacional).Include(x=>x.EscalaGrados).ThenInclude(x=>x.Remuneracion).ToListAsync();
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
                return new List<IndiceOcupacional>();
            }
        }

        // GET: api/IndicesOcupacionales/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIndiceOcupacional([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var indiceOcupacional = await db.IndiceOcupacional.SingleOrDefaultAsync(m => m.IdIndiceOcupacional == id);

            if (indiceOcupacional == null)
            {
                return NotFound();
            }

            return Ok(indiceOcupacional);
        }

        // PUT: api/IndicesOcupacionales/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIndiceOcupacional([FromRoute] int id, [FromBody] IndiceOcupacional indiceOcupacional)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != indiceOcupacional.IdIndiceOcupacional)
            {
                return BadRequest();
            }

            db.Entry(indiceOcupacional).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IndiceOcupacionalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        [Route("DetalleIndiceOcupacional")]
        public async Task<IndiceOcupacionalDetalle> DetalleIndiceOcupacional([FromBody] IndiceOcupacionalDetalle indiceOcupacionalDetalle)
        {
            try
            {

                //public IndiceOcupacional IndiceOcupacional { get; set; }



                //public List<ComportamientoObservable> ListaComportamientoObservables { get; set; }

                var IndiceOcupacional1 =await db.IndiceOcupacional.Where(x => x.IdIndiceOcupacional == indiceOcupacionalDetalle.IndiceOcupacional.IdIndiceOcupacional)
                                      .Include(x => x.Dependencia)
                                      .Include(x=>x.ManualPuesto)
                                      .Include(x=>x.RolPuesto)
                                      .Include(x=>x.EscalaGrados.GrupoOcupacional)
                                      .FirstOrDefaultAsync();




                //var ListaExperienciaLaboralRequeridas = await db.ExperienciaLaboralRequerida.Where(x => x.IdIndiceOcupacionalCapacitaciones == indiceOcupacionalDetalle.IndiceOcupacional.IdIndiceOcupacional).Include(x => x.IdExperienciaLaboralRequerida).ToListAsync();

                var ListaExperienciaLaboralRequeridas = await db.IndiceOcupacionalExperienciaLaboralRequerida
                                                    .Join(db.IndiceOcupacional
                                                    , rta => rta.IdIndiceOcupacional, ind => ind.IdIndiceOcupacional,
                                                    (rta, ind) => new { hm = rta, gh = ind })
                                                    .Join(db.ExperienciaLaboralRequerida
                                                    , ind_1 => ind_1.hm.ExperienciaLaboralRequerida.IdExperienciaLaboralRequerida, valor => valor.IdExperienciaLaboralRequerida,
                                                    (ind_1, valor) => new { ca = ind_1, rt = valor })
                                                    .Where(ds => ds.ca.hm.IdIndiceOcupacional == indiceOcupacionalDetalle.IndiceOcupacional.IdIndiceOcupacional)
                                                    .Select(t => new ExperienciaLaboralRequerida
                                                    {
                                                        IdExperienciaLaboralRequerida = t.rt.IdExperienciaLaboralRequerida,
                                                        Estudio=t.rt.Estudio,
                                                        AnoExperiencia=t.rt.AnoExperiencia,
                                                        EspecificidadExperiencia=t.rt.EspecificidadExperiencia,
                                                    })
                                                    .ToListAsync();



                var ListaRelacionesInternasExternas = await db.RelacionesInternasExternasIndiceOcupacional
                                                    .Join(db.IndiceOcupacional
                                                    , rta => rta.IdIndiceOcupacional, ind => ind.IdIndiceOcupacional,
                                                    (rta, ind) => new { hm = rta, gh = ind })
                                                    .Join(db.RelacionesInternasExternas
                                                    , ind_1 => ind_1.hm.RelacionesInternasExternas.IdRelacionesInternasExternas, valor => valor.IdRelacionesInternasExternas,
                                                    (ind_1, valor) => new { ca = ind_1, rt = valor })
                                                    .Where(ds => ds.ca.hm.IdIndiceOcupacional == indiceOcupacionalDetalle.IndiceOcupacional.IdIndiceOcupacional)
                                                    .Select(t => new RelacionesInternasExternas
                                                    {
                                                        IdRelacionesInternasExternas = t.rt.IdRelacionesInternasExternas,
                                                        Descripcion = t.rt.Descripcion,
                                                    })
                                                    .ToListAsync();



                var listaAreasConocimiento = await db.IndiceOcupacionalAreaConocimiento
                                                     .Join(db.IndiceOcupacional
                                                     , rta => rta.IdIndiceOcupacional, ind => ind.IdIndiceOcupacional,
                                                     (rta, ind) => new { hm = rta, gh = ind })
                                                     .Join(db.AreaConocimiento
                                                     , ind_1 => ind_1.hm.AreaConocimiento.IdAreaConocimiento, valor => valor.IdAreaConocimiento,
                                                     (ind_1, valor) => new { ca = ind_1, rt = valor })
                                                     .Where(ds => ds.ca.hm.IdIndiceOcupacional == indiceOcupacionalDetalle.IndiceOcupacional.IdIndiceOcupacional)
                                                     .Select(t => new AreaConocimiento
                                                     {
                                                         IdAreaConocimiento = t.rt.IdAreaConocimiento,
                                                         Descripcion = t.rt.Descripcion,
                                                     })
                                                     .ToListAsync();




                var listaMisiones = await db.MisionIndiceOcupacional
                                                     .Join(db.IndiceOcupacional
                                                     , rta => rta.IdIndiceOcupacional, ind => ind.IdIndiceOcupacional,
                                                     (rta, ind) => new { hm = rta, gh = ind })
                                                     .Join(db.Mision
                                                     , ind_1 => ind_1.hm.Mision.IdMision, valor => valor.IdMision,
                                                     (ind_1, valor) => new { ca = ind_1, rt = valor })
                                                     .Where(ds => ds.ca.hm.IdIndiceOcupacional == indiceOcupacionalDetalle.IndiceOcupacional.IdIndiceOcupacional)
                                                     .Select(t => new Mision
                                                     {
                                                         IdMision = t.rt.IdMision,
                                                         Descripcion = t.rt.Descripcion,
                                                     })
                                                     .ToListAsync();


                var ListaEstudios = await db.IndiceOcupacionalEstudio
                                                     .Join(db.IndiceOcupacional
                                                     , rta => rta.IdIndiceOcupacional, ind => ind.IdIndiceOcupacional,
                                                     (rta, ind) => new { hm = rta, gh = ind })
                                                     .Join(db.Estudio
                                                     , ind_1 => ind_1.hm.Estudio.IdEstudio, valor => valor.IdEstudio,
                                                     (ind_1, valor) => new { ca = ind_1, rt = valor })
                                                     .Where(ds => ds.ca.hm.IdIndiceOcupacional == indiceOcupacionalDetalle.IndiceOcupacional.IdIndiceOcupacional)
                                                     .Select(t => new Estudio
                                                     {
                                                         IdEstudio = t.rt.IdEstudio,
                                                         Nombre = t.rt.Nombre,
                                                     })
                                                     .ToListAsync();



                var ListaCapacitaciones = await db.IndiceOcupacionalCapacitaciones
                                                    .Join(db.IndiceOcupacional
                                                    , indiceCapacitaciones=> indiceCapacitaciones.IdIndiceOcupacional, indice => indice.IdIndiceOcupacional,
                                                    (indiceActEsenciales, indice) => new { IndiceOcupacionalActividadesEsenciales = indiceActEsenciales, IndiceOcupacional = indice })
                                                    .Join(db.Capacitacion
                                                    , indice_1 => indice_1.IndiceOcupacionalActividadesEsenciales.IdCapacitacion, capacitacion => capacitacion.IdCapacitacion,
                                                    (indice_1, capacitacion) => new { ca = indice_1, rt = capacitacion })
                                                    .Where(ds => ds.ca.IndiceOcupacional.IdIndiceOcupacional == indiceOcupacionalDetalle.IndiceOcupacional.IdIndiceOcupacional)
                                                    .Select(t => new Capacitacion
                                                    {
                                                       IdCapacitacion=t.rt.IdCapacitacion,
                                                       Nombre=t.rt.Nombre,
                                                    })
                                                    .ToListAsync();




                //Seleccionar Actividades esenciales......

                var listaActividadesEsenciales =await db.IndiceOcupacionalActividadesEsenciales
                                                        .Join(db.IndiceOcupacional
                                                        ,indice => indice.IdIndiceOcupacional, ocupacional => ocupacional.IdIndiceOcupacional,
                                                        (indice, ocupacional) => new { IndiceOcupacionalActividadesEsenciales = indice, IndiceOcupacional = ocupacional })
                                                        .Join(db.ActividadesEsenciales
                                                        ,indice1 => indice1.IndiceOcupacionalActividadesEsenciales.IdActividadesEsenciales, a => a.IdActividadesEsenciales,
                                                        (indice1, a) => new { z = indice1, s = a })
                                                        .Where(ds => ds.z.IndiceOcupacional.IdIndiceOcupacional == indiceOcupacionalDetalle.IndiceOcupacional.IdIndiceOcupacional)
                                                        .Select(t => new ActividadesEsenciales
                                                                      {
                                                                          IdActividadesEsenciales=t.s.IdActividadesEsenciales,
                                                                          Descripcion=t.s.Descripcion,
                                                                      })
                                                        .ToListAsync();



                var ListaConocimientosAdicionales = await db.IndiceOcupacionalConocimientosAdicionales
                                                    .Join(db.IndiceOcupacional
                                                    , indiceConocimiento => indiceConocimiento.IdIndiceOcupacional, indice => indice.IdIndiceOcupacional,
                                                    (indiceConocimiento, indice) => new { IndiceOcupacionalConocimientosAdicionales = indiceConocimiento, IndiceOcupacional = indice })
                                                    .Join(db.ConocimientosAdicionales
                                                    , indice_1 => indice_1.IndiceOcupacionalConocimientosAdicionales.IdConocimientosAdicionales, conocimientoAdicional => conocimientoAdicional.IdConocimientosAdicionales,
                                                    (indice_1, conocimientoAdicional) => new { ca = indice_1, io = conocimientoAdicional })
                                                    .Where(ds => ds.ca.IndiceOcupacional.IdIndiceOcupacional== indiceOcupacionalDetalle.IndiceOcupacional.IdIndiceOcupacional)
                                                    .Select(t => new ConocimientosAdicionales
                                                    {
                                                        IdConocimientosAdicionales=t.io.IdConocimientosAdicionales,
                                                        Descripcion=t.io.Descripcion,
                                                    })
                                                    .ToListAsync();

                var ListaComportamientoObservables = await db.IndiceOcupacionalComportamientoObservable
                                                    .Join(db.IndiceOcupacional
                                                    , indiceComportamiento => indiceComportamiento.IdIndiceOcupacional, indice => indice.IdIndiceOcupacional,
                                                    (indiceConocimiento, indice) => new { IndiceOcupacionalComportamientoObservable = indiceConocimiento, IndiceOcupacional = indice })
                                                    .Join(db.ComportamientoObservable
                                                    , indice_1 => indice_1.IndiceOcupacionalComportamientoObservable.IdComportamientoObservable, comportamientoObservable => comportamientoObservable.IdComportamientoObservable,
                                                    (indice_1, comportamientoObservable) => new { ca = indice_1, rt = comportamientoObservable })
                                                    .Where(ds => ds.ca.IndiceOcupacional.IdIndiceOcupacional == indiceOcupacionalDetalle.IndiceOcupacional.IdIndiceOcupacional)
                                                    .Select(t => new ComportamientoObservable
                                                    {
                                                        IdComportamientoObservable=t.rt.IdComportamientoObservable,
                                                        Descripcion=t.rt.Descripcion,
                                                        Nivel=t.rt.Nivel,
                                                        DenominacionCompetencia=t.rt.DenominacionCompetencia,
                                                    })
                                                    .ToListAsync();






                var detalle = new IndiceOcupacionalDetalle
                {
                    IndiceOcupacional= IndiceOcupacional1,
                    ListaActividadesEsenciales=listaActividadesEsenciales,
                    ListaConocimientosAdicionales=ListaConocimientosAdicionales,
                    ListaComportamientoObservables=ListaComportamientoObservables,
                    ListaRelacionesInternasExternas=ListaRelacionesInternasExternas,
                    ListaAreaConocimientos=listaAreasConocimiento,
                    ListaCapacitaciones=ListaCapacitaciones,
                    ListaEstudios=ListaEstudios,
                    ListaMisiones=listaMisiones,
                    ListaExperienciaLaboralRequeridas=ListaExperienciaLaboralRequeridas,
                  


                };


                return detalle;
               
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        [Route("InsertarActividadesEsenciales")]
        public async Task<Response> InsertarActividadesEsenciales([FromBody] IndiceOcupacionalActividadesEsenciales indiceOcupacionalActividadesEsenciales)
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
                db.IndiceOcupacionalActividadesEsenciales.Add(indiceOcupacionalActividadesEsenciales);
                await db.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio
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
        [Route("InsertarAreaConocimiento")]
        public async Task<Response> InsertarAreaConocimiento([FromBody] IndiceOcupacionalAreaConocimiento indiceOcupacionalAreaConocimiento)
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
                    db.IndiceOcupacionalAreaConocimiento.Add(indiceOcupacionalAreaConocimiento);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio
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
        [Route("InsertarCapacitacion")]
        public async Task<Response> InsertarCapacitacion([FromBody] IndiceOcupacionalCapacitaciones indiceOcupacionalCapacitaciones)
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
                db.IndiceOcupacionalCapacitaciones.Add(indiceOcupacionalCapacitaciones);
                await db.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio
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
        [Route("InsertarComportamientoObservable")]
        public async Task<Response> InsertarComportamientoObservable([FromBody] IndiceOcupacionalComportamientoObservable indiceOcupacionalComportamientoObservable)
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
                db.IndiceOcupacionalComportamientoObservable.Add(indiceOcupacionalComportamientoObservable);
                await db.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio
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
        [Route("InsertarConocimientoAdicional")]
        public async Task<Response> InsertarConocimientoAdicional([FromBody] IndiceOcupacionalConocimientosAdicionales indiceOcupacionalConocimientosAdicionales)
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
                db.IndiceOcupacionalConocimientosAdicionales.Add(indiceOcupacionalConocimientosAdicionales);
                await db.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio
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

        // POST: api/IndicesOcupacionales
        [HttpPost]
        [Route("InsertarIndiceOcupacional")]
        public async Task<Response> PostEscalaGrados([FromBody] IndiceOcupacional indiceOcupacional)
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

                var respuesta = Existe(indiceOcupacional);
                if (!respuesta.IsSuccess)
                {
                    db.IndiceOcupacional.Add(indiceOcupacional);
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

        // DELETE: api/IndicesOcupacionales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIndiceOcupacional([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var indiceOcupacional = await db.IndiceOcupacional.SingleOrDefaultAsync(m => m.IdIndiceOcupacional == id);
            if (indiceOcupacional == null)
            {
                return NotFound();
            }

            db.IndiceOcupacional.Remove(indiceOcupacional);
            await db.SaveChangesAsync();

            return Ok(indiceOcupacional);
        }

        private bool IndiceOcupacionalExists(int id)
        {
            return db.IndiceOcupacional.Any(e => e.IdIndiceOcupacional == id);
        }




        private Response Existe(IndiceOcupacional indiceOcupacional)
        {
            
            var indiceOcupacionalReturn = db.IndiceOcupacional.Where(p => p.IdDependencia == indiceOcupacional.IdDependencia && 
                                                                      p.IdEscalaGrados==indiceOcupacional.IdEscalaGrados &&
                                                                      p.IdManualPuesto==indiceOcupacional.IdManualPuesto && 
                                                                      p.IdRolPuesto==indiceOcupacional.IdRolPuesto).FirstOrDefault();
            if (indiceOcupacionalReturn != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = indiceOcupacionalReturn,
                };
            }

            return new Response
            {
                IsSuccess = false,
                Resultado = indiceOcupacionalReturn,
            };
        }
    }
}