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
using bd.swth.entidades.ViewModels;
using bd.swth.entidades.Constantes;

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



        #region M�todos mantenimiento IndicesOcupacionales

        /// <summary>
        /// Devuelve la lista de IndicesOcupacionales en modelo: IndiceOcupacionalViewModel
        /// </summary>
        /// <returns></returns>
        // api/IndicesOcupacionales
        [HttpGet]
        [Route("ListarIndicesOcupacionesViewModel")]
        public async Task<List<IndiceOcupacionalViewModel>> ListarIndicesOcupacionesViewModel()
        {
            try
            {
                var lista = await db.IndiceOcupacional
                    .Select(s => new IndiceOcupacionalViewModel
                    {
                        IdIndiceOcupacional = s.IdIndiceOcupacional,
                        DenominacionPuesto = s.DenominacionPuesto, 
                        UnidadAdministrativa = s.UnidadAdministrativa,
                        IdRolPuesto = s.IdRolPuesto,
                        IdEscalaGrados = s.IdEscalaGrados, 
                        IdPartidaGeneral = s.IdPartidaGeneral,
                        IdAmbito = s.IdAmbito,
                        Nivel = s.Nivel,
                        Mision = s.Mision,
                        RelacionesInternasExternas = s.RelacionesInternasExternas,
                        SinClasificar = s.SinClasificar,
                        RmusinClasificar = s.RmusinClasificar,
                        Activo = s.Activo,

                        NombreRolPuesto = s.RolPuesto.Nombre,
                        NombreEscalaGrados = s.EscalaGrados.Nombre,
                        Remuneracion = s.EscalaGrados.Remuneracion,
                        Grado = s.EscalaGrados.Grado,

                        NombreAmbito = s.Ambito.Nombre

                    })
                    .ToListAsync();

                return lista;
            }
            catch (Exception ex)
            {
                return new List<IndiceOcupacionalViewModel>();
            }
        }


        // POST: api/IndicesOcupacionales
        [HttpPost]
        [Route("InsertarIndiceOcupacional")]
        public async Task<Response> InsertarIndiceOcupacional([FromBody] IndiceOcupacional indiceOcupacional)
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

                var existe = await db.IndiceOcupacional
                    .Where(w =>
                        w.DenominacionPuesto.ToString().Trim().ToUpper()
                        == indiceOcupacional.DenominacionPuesto.ToString().Trim().ToUpper()
                    )
                    .FirstOrDefaultAsync();


                if (existe == null)
                {
                    using (var transaction = await db.Database.BeginTransactionAsync())
                    {
                        // Se crea el �ndice ocupacional

                        indiceOcupacional.DenominacionPuesto = indiceOcupacional.DenominacionPuesto.ToString().ToUpper();
                        indiceOcupacional.UnidadAdministrativa = indiceOcupacional.UnidadAdministrativa.ToString().ToUpper();
                        indiceOcupacional.Mision = indiceOcupacional.Mision.ToString().ToUpper();
                        indiceOcupacional.RelacionesInternasExternas = indiceOcupacional.RelacionesInternasExternas.ToString().ToUpper();
                        indiceOcupacional.Activo = true;
                        indiceOcupacional.SinClasificar = false;

                        db.IndiceOcupacional.Add(indiceOcupacional);
                        
                        await db.SaveChangesAsync();
                        transaction.Commit();

                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.GuardadoSatisfactorio
                        };


                    } // end transaction


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
                    Message = Mensaje.Excepcion,
                };
            }

            
        }


        // DELETE: api/IndicesOcupacionales/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteIndiceOcupacional([FromRoute] int id)
        {
            var desactivar = false;
            var eliminar = false;

            try {

                var modelo = await db.IndiceOcupacional
                    .Where(w => w.IdIndiceOcupacional == id)
                    .FirstOrDefaultAsync();

                if (modelo == null) {

                    return new Response {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado
                    };
                }



                // si hay registros de modalidad partida no debe permitir borrar
                var iomp = await db.IndiceOcupacionalModalidadPartida
                    .Where(w => w.IdIndiceOcupacional == id)
                    .FirstOrDefaultAsync();

                // si no hay registros que usen este �ndice ocupacional, se permite eliminar
                if (iomp == null)
                {
                    eliminar = true;
                }
                else {

                    // Se debe buscar si hay partidas activas  que usen este Indice ocupacional

                    var iompActivos = await db.IndiceOcupacionalModalidadPartida
                    .Where(w => 
                        w.IdIndiceOcupacional == id
                        && w.Activo == true
                    )
                    .ToListAsync();

                    // Si no hay partidas activas que usen este �ndice ocupacional, se permite desactivarlo
                    if (iompActivos.Count < 1) {
                        desactivar = true;  
                    }

                }


                if (eliminar == true)
                {

                    db.IndiceOcupacional.Remove(modelo);
                    await db.SaveChangesAsync();

                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.BorradoSatisfactorio
                    };
                }

                else if (desactivar == true)
                {

                    modelo.Activo = false;

                    db.IndiceOcupacional.Update(modelo);
                    await db.SaveChangesAsync();

                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.BorradoSatisfactorio
                    };
                }

                else {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.BorradoNoSatisfactorio
                    };

                }
                


            } catch (Exception ex) {

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };

            }
            
        }


        /// <summary>
        /// Necesario IdIndiceOcupacional
        /// </summary>
        /// <param name="indiceOcupacional"></param>
        /// <returns></returns>
        // POST: api/IndicesOcupacionales
        [HttpPost]
        [Route("ObtenerIndiceOcupacional")]
        public async Task<IndiceOcupacional> ObtenerIndiceOcupacional([FromBody] IndiceOcupacional indiceOcupacional) {

            try {

                var modelo = await db.IndiceOcupacional
                    .Select(s=>new IndiceOcupacional {

                        IdIndiceOcupacional = s.IdIndiceOcupacional,
                        DenominacionPuesto = s.DenominacionPuesto,
                        UnidadAdministrativa = s.UnidadAdministrativa,
                        IdRolPuesto = s.IdRolPuesto,
                        IdEscalaGrados = s.IdEscalaGrados,
                        IdPartidaGeneral = s.IdPartidaGeneral,
                        IdAmbito = s.IdAmbito,
                        Nivel = s.Nivel,
                        Mision = s.Mision,
                        RelacionesInternasExternas = s.RelacionesInternasExternas,
                        SinClasificar = s.SinClasificar,
                        RmusinClasificar = s.RmusinClasificar,
                        Activo = s.Activo,
                   

                        EscalaGrados = new EscalaGrados {

                            IdEscalaGrados = s.EscalaGrados.IdEscalaGrados,
                            IdGrupoOcupacional = s.EscalaGrados.IdGrupoOcupacional,
                            Grado = s.EscalaGrados.Grado,
                            Remuneracion = s.EscalaGrados.Remuneracion,
                            Nombre = s.EscalaGrados.Nombre
                        },

                    })
                    .Where(w => w.IdIndiceOcupacional == indiceOcupacional.IdIndiceOcupacional)
                    .FirstOrDefaultAsync();

                return modelo;

            } catch (Exception ex) {

                return new IndiceOcupacional();
            }
        }



        // POST: api/IndicesOcupacionales
        [HttpPost]
        [Route("EditarIndiceOcupacional")]
        public async Task<Response> EditarIndiceOcupacional([FromBody] IndiceOcupacional indiceOcupacional)
        {
            try
            {

                var modelo = await db.IndiceOcupacional
                    .Where(w => w.IdIndiceOcupacional == indiceOcupacional.IdIndiceOcupacional)
                    .FirstOrDefaultAsync();

                if (modelo == null)
                {

                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado
                    };
                }


                //var distributivoHistorico = await db.DistributivoHistorico
                //  aqui se debe validar el editar y la re-estructuraci�n de puestos

                
                var distributivoFormal = await db.DistributivoHistorico
                    .Include(i=>i.IndiceOcupacionalModalidadPartida)
                    .Include(i=>i.IndiceOcupacionalModalidadPartida.IndiceOcupacional)
                    .ToListAsync()
                    ;

                var distributivosDependientes = distributivoFormal
                    .Where(w => 
                        w.IndiceOcupacionalModalidadPartida.IdIndiceOcupacional == modelo.IdIndiceOcupacional
                    )
                    .ToList();

                var distributivosDependientesActivos = distributivosDependientes
                    .Where(w => w.Activo == true)
                    .ToList();

                var distributivosDependientesInactivos = distributivosDependientes
                    .Where(w => w.Activo == false)
                    .ToList();




                if (distributivosDependientes.Count > 0)
                {

                    // Si hay distributivos dependientes, ya no se debe editar porque afecta al hist�rico, se debe: 
                    // 1) Crear un nuevo perfil con las nuevas caracter�sticas configuradas, y adquiriendo los valores de las
                    //    CARACTER�STICAS DEL PERFIL ANTERIOR
                    // 2) agregar los activos al nuevo perfil
                    // 3) mantener los inactivos en el perfil anterior

                    #region 1) Crear un nuevo perfil



                    #endregion

                    return new Response
                    {

                        IsSuccess = true,
                        Message = "Revisa la edici�n de indicesOcupacionales en swth, !!!!!"
                    };

                }

                else if (distributivosDependientes.Count == 0)
                {
                    // Si no hay registros que dependen de este, puede ser editado sin problemas

                    modelo.DenominacionPuesto = indiceOcupacional.DenominacionPuesto.Trim().ToString().ToUpper();
                    modelo.UnidadAdministrativa = indiceOcupacional.UnidadAdministrativa.Trim().ToString().ToUpper();
                    modelo.IdRolPuesto = indiceOcupacional.IdRolPuesto;
                    modelo.IdEscalaGrados = indiceOcupacional.IdEscalaGrados;
                    modelo.IdAmbito = indiceOcupacional.IdAmbito;
                    modelo.Nivel = indiceOcupacional.Nivel;
                    modelo.Mision = indiceOcupacional.Mision;
                    modelo.RelacionesInternasExternas = indiceOcupacional.RelacionesInternasExternas.ToString().ToUpper();

                    db.IndiceOcupacional.Update(modelo);
                    await db.SaveChangesAsync();

                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.GuardadoSatisfactorio
                    };

                }
                else {

                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.Error
                    };

                }


            } catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };

            }
        }

        #endregion


        /*

        [HttpGet]
        [Route("ListarIndicesOcupaciones")]
        public async Task<List<IndiceOcupacionalViewModel>> ListarIndicesOcupaciones()
        {
            try
            {
                var lista = await db.IndiceOcupacional.Select(x => new IndiceOcupacionalViewModel
                {
                    CodigoDependencia = x.Dependencia.Codigo,
                    NombreDependencia = x.Dependencia.Nombre,
                    NombreEscalaGrados = x.EscalaGrados.Nombre,
                    IdIndiceOcupacional = x.IdIndiceOcupacional,
                    NombreManualPuesto = x.ManualPuesto.Nombre,
                    Remuneracion = Convert.ToDecimal(x.EscalaGrados.Remuneracion),
                    NumeroPartidaGeneral = (x.PartidaGeneral != null)?x.PartidaGeneral.NumeroPartida:null,
                    NombreRolPuesto = x.RolPuesto.Nombre,
                    NombreAmbito = x.Ambito.Nombre,
                    Nivel = x.Nivel,
                    
                }
                ).ToListAsync();


                return lista;
            }
            catch (Exception ex)
            {
                return new List<IndiceOcupacionalViewModel>();
            }
        }


        



        // GET: api/IndicesOcupacionales
        [HttpGet]
        [Route("ListarActividadesEsenciales")]
        public async Task<List<IndiceOcupacional>> GetIndicesOcupacionales(IndiceOcupacional indiceOcupacional)
        {
            try
            {
                return await db.IndiceOcupacional.Where(x => x.IdIndiceOcupacional == indiceOcupacional.IdIndiceOcupacional).Include(x => x.Dependencia).Include(x => x.ManualPuesto).Include(x => x.RolPuesto)
                             .Include(x => x.EscalaGrados).ThenInclude(x => x.GrupoOcupacional).Include(x => x.EscalaGrados).ThenInclude(x => x.Remuneracion).ToListAsync();
            }
            catch (Exception ex)
            {
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
        [Route("InformacionBasicaIndiceOcupacional")]
        public async Task<IndiceOcupacionalViewModel> InformacionBasicaIndiceOcupacional([FromBody] IndiceOcupacional indiceOcupacionalDetalle)
        {
            try
            {

                var modelo = await db.IndiceOcupacional
                    .Where(w=>w.IdIndiceOcupacional == indiceOcupacionalDetalle.IdIndiceOcupacional)
                    .Select(
                     s => new IndiceOcupacionalViewModel
                     {

                         IdIndiceOcupacional = s.IdIndiceOcupacional,
                         IdDependencia = s.IdDependencia,
                         IdManualPuesto = s.IdManualPuesto,
                         IdRolPuesto = s.IdRolPuesto,
                         IdEscalaGrados = s.IdEscalaGrados,
                         IdPartidaGeneral = s.IdPartidaGeneral,
                         IdAmbito = s.IdAmbito,
                         Nivel = s.Nivel,


                         NombreDependencia = s.Dependencia.Nombre,
                         CodigoDependencia = s.Dependencia.Codigo,

                         IdSucursal = s.Dependencia.Sucursal.IdSucursal,
                         NombreSucursal = s.Dependencia.Sucursal.Nombre,

                         NombreManualPuesto = s.ManualPuesto.Nombre,
                         DescripcionManualPuesto = s.ManualPuesto.Descripcion,
                         MisionManualPuesto = s.ManualPuesto.Mision,

                         IdRelacionesInternasExternas =
                                     s.ManualPuesto.RelacionesInternasExternas.IdRelacionesInternasExternas,
                         NombreRelacionesInternasExternas =
                                     s.ManualPuesto.RelacionesInternasExternas.Nombre,
                         DescripcionRelacionesInternasExternas =
                                     s.ManualPuesto.RelacionesInternasExternas.Descripcion,


                         NombreRolPuesto = s.RolPuesto.Nombre,


                         NombreEscalaGrados = s.EscalaGrados.Nombre,
                         Remuneracion = s.EscalaGrados.Remuneracion,
                         Grado = s.EscalaGrados.Grado,

                         NumeroPartidaGeneral =
                                 (s.PartidaGeneral == null)
                                 ? ""
                                 : s.PartidaGeneral.NumeroPartida,

                         NombreAmbito = s.Ambito.Nombre

                     }).FirstOrDefaultAsync();

                return modelo;
            }
            catch (Exception ex)
            {

                throw;
            }
        }




        //[HttpPost]
        //[Route("DetalleIndiceOcupacional")]
        //public async Task<IndiceOcupacionalDetalle> DetalleIndiceOcupacional([FromBody] IndiceOcupacionalDetalle indiceOcupacionalDetalle)
        //{
        //    try
        //    {

        //        //public IndiceOcupacional IndiceOcupacional { get; set; }



        //        //public List<ComportamientoObservable> ListaComportamientoObservables { get; set; }





        //        //var ListaExperienciaLaboralRequeridas = await db.ExperienciaLaboralRequerida.Where(x => x.IdIndiceOcupacionalCapacitaciones == indiceOcupacionalDetalle.IndiceOcupacional.IdIndiceOcupacional).Include(x => x.IdExperienciaLaboralRequerida).ToListAsync();

        //        var ListaExperienciaLaboralRequeridas = await db.IndiceOcupacionalExperienciaLaboralRequerida
        //                                            .Join(db.IndiceOcupacional
        //                                            , rta => rta.IdIndiceOcupacional, ind => ind.IdIndiceOcupacional,
        //                                            (rta, ind) => new { hm = rta, gh = ind })
        //                                            .Join(db.ExperienciaLaboralRequerida
        //                                            , ind_1 => ind_1.hm.ExperienciaLaboralRequerida.IdExperienciaLaboralRequerida, valor => valor.IdExperienciaLaboralRequerida,
        //                                            (ind_1, valor) => new { ca = ind_1, rt = valor })
        //                                            .Where(ds => ds.ca.hm.IdIndiceOcupacional == indiceOcupacionalDetalle.IndiceOcupacional.IdIndiceOcupacional)
        //                                            .Select(t => new ExperienciaLaboralRequerida
        //                                            {
        //                                                IdExperienciaLaboralRequerida = t.rt.IdExperienciaLaboralRequerida,
        //                                                Estudio=t.rt.Estudio,
        //                                                EspecificidadExperiencia=t.rt.EspecificidadExperiencia,
        //                                            })
        //                                            .ToListAsync();



        //        //var ListaRelacionesInternasExternas = await db.RelacionesInternasExternasIndiceOcupacional
        //        //                                    .Join(db.IndiceOcupacional
        //        //                                    , rta => rta.IdIndiceOcupacional, ind => ind.IdIndiceOcupacional,
        //        //                                    (rta, ind) => new { hm = rta, gh = ind })
        //        //                                    .Join(db.RelacionesInternasExternas
        //        //                                    , ind_1 => ind_1.hm.RelacionesInternasExternas.IdRelacionesInternasExternas, valor => valor.IdRelacionesInternasExternas,
        //        //                                    (ind_1, valor) => new { ca = ind_1, rt = valor })
        //        //                                    .Where(ds => ds.ca.hm.IdIndiceOcupacional == indiceOcupacionalDetalle.IndiceOcupacional.IdIndiceOcupacional)
        //        //                                    .Select(t => new RelacionesInternasExternas
        //        //                                    {
        //        //                                        IdRelacionesInternasExternas = t.rt.IdRelacionesInternasExternas,
        //        //                                        Descripcion = t.rt.Descripcion,
        //        //                                    })
        //        //                                    .ToListAsync();



        //        var listaAreasConocimiento = await db.IndiceOcupacionalAreaConocimiento
        //                                             .Join(db.IndiceOcupacional
        //                                             , rta => rta.IdIndiceOcupacional, ind => ind.IdIndiceOcupacional,
        //                                             (rta, ind) => new { hm = rta, gh = ind })
        //                                             .Join(db.AreaConocimiento
        //                                             , ind_1 => ind_1.hm.AreaConocimiento.IdAreaConocimiento, valor => valor.IdAreaConocimiento,
        //                                             (ind_1, valor) => new { ca = ind_1, rt = valor })
        //                                             .Where(ds => ds.ca.hm.IdIndiceOcupacional == indiceOcupacionalDetalle.IndiceOcupacional.IdIndiceOcupacional)
        //                                             .Select(t => new AreaConocimiento
        //                                             {
        //                                                 IdAreaConocimiento = t.rt.IdAreaConocimiento,
        //                                                 Descripcion = t.rt.Descripcion,
        //                                             })
        //                                             .ToListAsync();




        //        //var listaMisiones = await db.MisionIndiceOcupacional
        //        //                                     .Join(db.IndiceOcupacional
        //        //                                     , rta => rta.IdIndiceOcupacional, ind => ind.IdIndiceOcupacional,
        //        //                                     (rta, ind) => new { hm = rta, gh = ind })
        //        //                                     .Join(db.Mision
        //        //                                     , ind_1 => ind_1.hm.Mision.IdMision, valor => valor.IdMision,
        //        //                                     (ind_1, valor) => new { ca = ind_1, rt = valor })
        //        //                                     .Where(ds => ds.ca.hm.IdIndiceOcupacional == indiceOcupacionalDetalle.IndiceOcupacional.IdIndiceOcupacional)
        //        //                                     .Select(t => new Mision
        //        //                                     {
        //        //                                         IdMision = t.rt.IdMision,
        //        //                                         Descripcion = t.rt.Descripcion,
        //        //                                     })
        //        //                                     .ToListAsync();


        //        var ListaEstudios = await db.IndiceOcupacionalEstudio
        //                                             .Join(db.IndiceOcupacional
        //                                             , rta => rta.IdIndiceOcupacional, ind => ind.IdIndiceOcupacional,
        //                                             (rta, ind) => new { hm = rta, gh = ind })
        //                                             .Join(db.Estudio
        //                                             , ind_1 => ind_1.hm.Estudio.IdEstudio, valor => valor.IdEstudio,
        //                                             (ind_1, valor) => new { ca = ind_1, rt = valor })
        //                                             .Where(ds => ds.ca.hm.IdIndiceOcupacional == indiceOcupacionalDetalle.IndiceOcupacional.IdIndiceOcupacional)
        //                                             .Select(t => new Estudio
        //                                             {
        //                                                 IdEstudio = t.rt.IdEstudio,
        //                                                 Nombre = t.rt.Nombre,
        //                                             })
        //                                             .ToListAsync();



        //        var ListaCapacitaciones = await db.IndiceOcupacionalCapacitaciones
        //                                            .Join(db.IndiceOcupacional
        //                                            , indiceCapacitaciones=> indiceCapacitaciones.IdIndiceOcupacional, indice => indice.IdIndiceOcupacional,
        //                                            (indiceActEsenciales, indice) => new { IndiceOcupacionalActividadesEsenciales = indiceActEsenciales, IndiceOcupacional = indice })
        //                                            .Join(db.Capacitacion
        //                                            , indice_1 => indice_1.IndiceOcupacionalActividadesEsenciales.IdCapacitacion, capacitacion => capacitacion.IdCapacitacion,
        //                                            (indice_1, capacitacion) => new { ca = indice_1, rt = capacitacion })
        //                                            .Where(ds => ds.ca.IndiceOcupacional.IdIndiceOcupacional == indiceOcupacionalDetalle.IndiceOcupacional.IdIndiceOcupacional)
        //                                            .Select(t => new Capacitacion
        //                                            {
        //                                               IdCapacitacion=t.rt.IdCapacitacion,
        //                                               Nombre=t.rt.Nombre,
        //                                            })
        //                                            .ToListAsync();




        //        //Seleccionar Actividades esenciales......

        //        var listaActividadesEsenciales =await db.IndiceOcupacionalActividadesEsenciales
        //                                                .Join(db.IndiceOcupacional
        //                                                ,indice => indice.IdIndiceOcupacional, ocupacional => ocupacional.IdIndiceOcupacional,
        //                                                (indice, ocupacional) => new { IndiceOcupacionalActividadesEsenciales = indice, IndiceOcupacional = ocupacional })
        //                                                .Join(db.ActividadesEsenciales
        //                                                ,indice1 => indice1.IndiceOcupacionalActividadesEsenciales.IdActividadesEsenciales, a => a.IdActividadesEsenciales,
        //                                                (indice1, a) => new { z = indice1, s = a })
        //                                                .Where(ds => ds.z.IndiceOcupacional.IdIndiceOcupacional == indiceOcupacionalDetalle.IndiceOcupacional.IdIndiceOcupacional)
        //                                                .Select(t => new ActividadesEsenciales
        //                                                              {
        //                                                                  IdActividadesEsenciales=t.s.IdActividadesEsenciales,
        //                                                                  Descripcion=t.s.Descripcion,
        //                                                              })
        //                                                .ToListAsync();



        //        var ListaConocimientosAdicionales = await db.IndiceOcupacionalConocimientosAdicionales
        //                                            .Join(db.IndiceOcupacional
        //                                            , indiceConocimiento => indiceConocimiento.IdIndiceOcupacional, indice => indice.IdIndiceOcupacional,
        //                                            (indiceConocimiento, indice) => new { IndiceOcupacionalConocimientosAdicionales = indiceConocimiento, IndiceOcupacional = indice })
        //                                            .Join(db.ConocimientosAdicionales
        //                                            , indice_1 => indice_1.IndiceOcupacionalConocimientosAdicionales.IdConocimientosAdicionales, conocimientoAdicional => conocimientoAdicional.IdConocimientosAdicionales,
        //                                            (indice_1, conocimientoAdicional) => new { ca = indice_1, io = conocimientoAdicional })
        //                                            .Where(ds => ds.ca.IndiceOcupacional.IdIndiceOcupacional== indiceOcupacionalDetalle.IndiceOcupacional.IdIndiceOcupacional)
        //                                            .Select(t => new ConocimientosAdicionales
        //                                            {
        //                                                IdConocimientosAdicionales=t.io.IdConocimientosAdicionales,
        //                                                Descripcion=t.io.Descripcion,
        //                                            })
        //                                            .ToListAsync();

        //        var ListaComportamientoObservables = await db.IndiceOcupacionalComportamientoObservable
        //                                            .Join(db.IndiceOcupacional
        //                                            , indiceComportamiento => indiceComportamiento.IdIndiceOcupacional, indice => indice.IdIndiceOcupacional,
        //                                            (indiceConocimiento, indice) => new { IndiceOcupacionalComportamientoObservable = indiceConocimiento, IndiceOcupacional = indice })
        //                                            .Join(db.ComportamientoObservable
        //                                            , indice_1 => indice_1.IndiceOcupacionalComportamientoObservable.IdComportamientoObservable, comportamientoObservable => comportamientoObservable.IdComportamientoObservable,
        //                                            (indice_1, comportamientoObservable) => new { ca = indice_1, rt = comportamientoObservable })
        //                                            .Where(ds => ds.ca.IndiceOcupacional.IdIndiceOcupacional == indiceOcupacionalDetalle.IndiceOcupacional.IdIndiceOcupacional)
        //                                            .Select(t => new ComportamientoObservable
        //                                            {
        //                                                IdComportamientoObservable=t.rt.IdComportamientoObservable,
        //                                                Descripcion=t.rt.Descripcion,
        //                                                Nivel=t.rt.Nivel,
        //                                                DenominacionCompetencia=t.rt.DenominacionCompetencia,
        //                                            })
        //                                            .ToListAsync();






        //        var detalle = new IndiceOcupacionalDetalle
        //        {
        //            IndiceOcupacional= IndiceOcupacional1,
        //            ListaActividadesEsenciales=listaActividadesEsenciales,
        //            ListaConocimientosAdicionales=ListaConocimientosAdicionales,
        //            ListaComportamientoObservables=ListaComportamientoObservables,
        //            //ListaRelacionesInternasExternas=ListaRelacionesInternasExternas,
        //            ListaAreaConocimientos=listaAreasConocimiento,
        //            ListaCapacitaciones=ListaCapacitaciones,
        //            ListaEstudios=ListaEstudios,
        //            //ListaMisiones=listaMisiones,
        //            ListaExperienciaLaboralRequeridas=ListaExperienciaLaboralRequeridas,



        //        };


        //        return detalle;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}


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


        [HttpPost]
        [Route("InsertarEstudio")]
        public async Task<Response> InsertarEstudio([FromBody] IndiceOcupacionalEstudio indiceOcupacionalEstudio)
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
                db.IndiceOcupacionalEstudio.Add(indiceOcupacionalEstudio);
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



        [HttpPost]
        [Route("InsertarExperienciaLaboralRequerida")]
        public async Task<Response> InsertarExperienciaLaboralRequerida([FromBody] IndiceOcupacionalExperienciaLaboralRequerida indiceOcupacionalExperienciaLaboralRequerida)
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
                db.IndiceOcupacionalExperienciaLaboralRequerida.Add(indiceOcupacionalExperienciaLaboralRequerida);
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


        //[HttpPost]
        //[Route("InsertarMision")]
        //public async Task<Response> InsertarMision([FromBody] MisionIndiceOcupacional misionIndiceOcupacional)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = Mensaje.ModeloInvalido,
        //            };
        //        }
        //        db.MisionIndiceOcupacional.Add(misionIndiceOcupacional);
        //        await db.SaveChangesAsync();
        //        return new Response
        //        {
        //            IsSuccess = true,
        //            Message = Mensaje.Satisfactorio
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        await GuardarLogService.SaveLogEntry(new LogEntryTranfer
        //        {
        //            ApplicationName = Convert.ToString(Aplicacion.SwTH),
        //            ExceptionTrace = ex.Message,
        //            Message = Mensaje.Excepcion,
        //            LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
        //            LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
        //            UserName = "",

        //        });
        //        return new Response
        //        {
        //            IsSuccess = false,
        //            Message = Mensaje.Error,
        //        };
        //    }
        //}




        //[HttpPost]
        //[Route("InsertarRelacionesInternasExternas")]
        //public async Task<Response> InsertarRelacionesInternasExternas([FromBody] RelacionesInternasExternasIndiceOcupacional relacionesInternasExternasIndiceOcupacional)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = Mensaje.ModeloInvalido,
        //            };
        //        }
        //        db.RelacionesInternasExternasIndiceOcupacional.Add(relacionesInternasExternasIndiceOcupacional);
        //        await db.SaveChangesAsync();
        //        return new Response
        //        {
        //            IsSuccess = true,
        //            Message = Mensaje.Satisfactorio
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        await GuardarLogService.SaveLogEntry(new LogEntryTranfer
        //        {
        //            ApplicationName = Convert.ToString(Aplicacion.SwTH),
        //            ExceptionTrace = ex.Message,
        //            Message = Mensaje.Excepcion,
        //            LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
        //            LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
        //            UserName = "",

        //        });
        //        return new Response
        //        {
        //            IsSuccess = false,
        //            Message = Mensaje.Error,
        //        };
        //    }
        //}

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
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }

        



        



        private bool IndiceOcupacionalExists(int id)
        {
            return db.IndiceOcupacional.Any(e => e.IdIndiceOcupacional == id);
        }




        private Response Existe(IndiceOcupacional indiceOcupacional)
        {

            var indiceOcupacionalReturn = db.IndiceOcupacional
                .Where(p => 
                    p.IdManualPuesto == indiceOcupacional.IdManualPuesto
                ).FirstOrDefault();

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

        // POST: api/BasesDatos
        [HttpPost]
        [Route("ObtenerIndiceOcupacional")]
        public async Task<Response> PostEstadoCivil([FromBody] IndiceOcupacional indiceOcupacional)
        {
            try
            {

                IndiceOcupacional IndiceOcupacional = new IndiceOcupacional();
                if (indiceOcupacional.IdEscalaGrados != 0)
                {
                    IndiceOcupacional = await db.IndiceOcupacional.Where(m => m.IdDependencia == indiceOcupacional.IdDependencia
                    && m.IdManualPuesto == indiceOcupacional.IdManualPuesto
                    && m.IdRolPuesto == indiceOcupacional.IdRolPuesto
                    && m.IdEscalaGrados == indiceOcupacional.IdEscalaGrados).FirstOrDefaultAsync();
                }
                else
                {
                    IndiceOcupacional = await db.IndiceOcupacional.SingleOrDefaultAsync(m => m.IdDependencia == indiceOcupacional.IdDependencia
                                        && m.IdManualPuesto == indiceOcupacional.IdManualPuesto
                                        && m.IdRolPuesto == indiceOcupacional.IdRolPuesto);
                }

                if (IndiceOcupacional == null)
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
                    Resultado = IndiceOcupacional,
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

        
        //[HttpPost]
        //[Route("ActualizarModalidadPartida")]
        //public async Task<Response> PutModalidadPartidaIndiceOcupacion([FromBody] IndiceOcupacional indiceOcupacional)
        //{ 
        //    try
        //    {


        //        var indiceOcupacionalActualizar = await db.IndiceOcupacional.Where(x => x.IdIndiceOcupacional == indiceOcupacional.IdIndiceOcupacional).FirstOrDefaultAsync();

        //        if (indiceOcupacionalActualizar != null)
        //        {
        //            try
        //            {
        //                indiceOcupacionalActualizar.IdModalidadPartida = indiceOcupacional.IdModalidadPartida;
        //                await db.SaveChangesAsync();

        //                return new Response
        //                {
        //                    IsSuccess = true,
        //                    Message = Mensaje.Satisfactorio,
        //                };

        //            }
        //            catch (Exception ex)
        //            {
        //                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
        //                {
        //                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
        //                    ExceptionTrace = ex.Message,
        //                    Message = Mensaje.Excepcion,
        //                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
        //                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
        //                    UserName = "",

        //                });
        //                return new Response
        //                {
        //                    IsSuccess = false,
        //                    Message = Mensaje.Error,
        //                };
        //            }
        //        }




        //        return new Response
        //        {
        //            IsSuccess = false,
        //            Message = Mensaje.ExisteRegistro
        //        };
        //    }
        //    catch (Exception)
        //    {
        //        return new Response
        //        {
        //            IsSuccess = false,
        //            Message = Mensaje.Excepcion
        //        };
        //    }
        //}
        

        
        //[HttpPost]
        //[Route("ObtenerPrimerIndicePorDatos")]
        //public async Task<IndiceOcupacional> ObtenerPrimerIndicePorDatos([FromBody] IdFiltrosViewModel filtro)
        //{
        //    try
        //    {

        //        var lista = await db.IndiceOcupacional
        //            .Include(i => i.ModalidadPartida)
        //            .Where(w =>
        //                w.IdDependencia == filtro.IdDependencia
        //                && w.IdManualPuesto == filtro.IdManualPuesto
        //                && w.IdRolPuesto == filtro.IdRolPuesto
        //                && w.IdEscalaGrados == filtro.IdEscalaGrados
        //            )
        //            .ToListAsync();


        //        var relacionLaboral = await db.RelacionLaboral
        //            .Where(w =>
        //               w.IdRelacionLaboral == filtro.IdTipoRelacion
        //            ).FirstOrDefaultAsync();

        //        var enviar = new IndiceOcupacional();

        //        // Cuando es un contrato
        //        if (relacionLaboral.Nombre.ToUpper() == ConstantesTipoRelacion.Contrato.ToUpper())
        //        {
        //            enviar = lista.Where(w => w.NumeroPartidaIndividual == null).FirstOrDefault();
        //        }

        //        // Cuando es un nombramiento
        //        else if (relacionLaboral.Nombre.ToUpper() == ConstantesTipoRelacion.Nombramiento.ToUpper())
        //        {
        //            enviar = lista
        //                .Where(w =>
        //                    w.NumeroPartidaIndividual != null
        //                    && w.ModalidadPartida.Nombre.ToUpper() == Constantes.PartidaVacante.ToUpper()
        //                )
        //                .FirstOrDefault();
        //        }


        //        return enviar;

        //    }
        //    catch (Exception ex)
        //    {
        //        return new IndiceOcupacional();
        //    }
        //}
        

    */


    }
}