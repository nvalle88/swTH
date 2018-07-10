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
using bd.swth.entidades.ViewModels;
using MoreLinq;
using bd.swth.entidades.Constantes;

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


        // GET: api/IndicesOcupacionalesModalidadPartida
        [HttpGet]
        [Route("ListarIndicesOcupacionalesModalidadPartida")]
        public async Task<List<IndiceOcupacionalModalidadPartida>> ListarIndicesOcupacionalesModalidadPartida()
        {
            try
            {
                return await db.IndiceOcupacionalModalidadPartida
                    .Include(x => x.IndiceOcupacional)
                    .Include(x => x.Empleado)
                    .Include(x => x.FondoFinanciamiento)
                    .Include(x => x.TipoNombramiento)
                    .Include(x => x.ModalidadPartida)
                    .OrderByDescending(x => x.Fecha)
                    .DistinctBy(d => d.IndiceOcupacional.ManualPuesto.Nombre)
                    .ToAsyncEnumerable().ToList();

            }
            catch (Exception ex)
            {
                return new List<IndiceOcupacionalModalidadPartida>();
            }
        }


        // GET: api/IndicesOcupacionalesModalidadPartida
        [HttpGet]
        [Route("MostrarDistributivo")]
        public async Task<List<IndiceOcupacionalModalidadPartida>> MostrarDistributivo()
        {
            try
            {
                return await db.IndiceOcupacionalModalidadPartida
                    .Include(i => i.IndiceOcupacional)
                        .Include(i=> i.IndiceOcupacional.Dependencia)
                        .Include(i => i.IndiceOcupacional.ManualPuesto)
                        .Include(i => i.IndiceOcupacional.RolPuesto)
                        .Include(i => i.IndiceOcupacional.Ambito)
                        .Include(i => i.IndiceOcupacional.EscalaGrados)

                    .Include(i => i.Empleado)
                        .Include(i=>i.Empleado.Persona)

                    .Include(i => i.FondoFinanciamiento)
                    .Include(i => i.TipoNombramiento)
                    .Include(i => i.ModalidadPartida)
                    .Where(w=>w.Empleado.Activo == true)
                    .OrderByDescending(x => x.Fecha)
                    .DistinctBy(d => d.IdEmpleado)
                    .ToAsyncEnumerable().ToList();

            }
            catch (Exception ex)
            {
                return new List<IndiceOcupacionalModalidadPartida>();
            }
        }

        /// <summary>
        /// Este método obtiene el salario de la escala de grados y si existe un salario real,
        /// envía el salario real
        /// </summary>
        /// <returns></returns>
        // GET: api/IndicesOcupacionalesModalidadPartida
        [HttpGet]
        [Route("ListarIndicesOcupacionalesModalidadPartidaViewModel")]
        public async Task<List<IndicesOcupacionalesModalidadPartidaViewModel>> ListarIndicesOcupacionalesModalidadPartidaViewModel()
        {

            var lista = new List<IndicesOcupacionalesModalidadPartidaViewModel>();


            try
            {
                var listaIOMP = await db.IndiceOcupacionalModalidadPartida
                    .Include(i => i.IndiceOcupacional)
                        .ThenInclude(t=>t.EscalaGrados)

                        .Include(i=>i.IndiceOcupacional.Dependencia)
                            .ThenInclude(t=>t.Sucursal)

                        .Include(i=>i.IndiceOcupacional.ManualPuesto)
                            .ThenInclude(t=>t.RelacionesInternasExternas)

                        .Include(i=>i.IndiceOcupacional.RolPuesto)
                        
                        .Include(i=>i.IndiceOcupacional.Ambito)

                        .Include(i=>i.IndiceOcupacional.PartidaGeneral)

                    .Include(i => i.ModalidadPartida)
                    .Include(i=>i.FondoFinanciamiento)

                    .Include(i=>i.TipoNombramiento)
                        .ThenInclude(t=>t.RelacionLaboral)

                    .OrderByDescending(o=>o.Fecha)
                    .DistinctBy(d => d.IndiceOcupacional.ManualPuesto.Nombre)
                    .ToAsyncEnumerable().ToList();



                lista = listaIOMP.Select(
                    s=> new IndicesOcupacionalesModalidadPartidaViewModel
                        {
                            IdIndiceOcupacionalModalidadPartida = s.IdIndiceOcupacionalModalidadPartida,
                            Fecha = s.Fecha,

                            SalarioActual  = 
                                s.SalarioReal> 0
                                ? s.SalarioReal
                                : s.IndiceOcupacional.EscalaGrados.Remuneracion
                            ,
                            
                            IdIndiceOcupacional = s.IdIndiceOcupacional,
                            IdEmpleado = s.IdEmpleado,
                            IdFondoFinanciamiento  = s.IdFondoFinanciamiento,
                            IdTipoNombramiento = s.IdTipoNombramiento,
                            CodigoContrato = s.CodigoContrato,
                            IdModalidadPartida = s.IdModalidadPartida,
                            NumeroPartidaIndividual = s.NumeroPartidaIndividual,
                            //FechaFin = s.FechaFin,

        IndiceOcupacionalViewModel = new IndiceOcupacionalViewModel {

                                IdIndiceOcupacional = s.IndiceOcupacional.IdIndiceOcupacional,
                                IdDependencia = s.IndiceOcupacional.IdDependencia,
                                IdManualPuesto = s.IndiceOcupacional.IdManualPuesto,
                                IdRolPuesto = s.IndiceOcupacional.IdRolPuesto,
                                IdEscalaGrados = s.IndiceOcupacional.IdEscalaGrados,
                                IdPartidaGeneral = s.IndiceOcupacional.IdPartidaGeneral,
                                IdAmbito = s.IndiceOcupacional.IdAmbito,
                                Nivel = s.IndiceOcupacional.Nivel,

                                
                                NombreDependencia = s.IndiceOcupacional.Dependencia.Nombre,
                                CodigoDependencia = s.IndiceOcupacional.Dependencia.Codigo,

                                IdSucursal = s.IndiceOcupacional.Dependencia.Sucursal.IdSucursal,
                                NombreSucursal = s.IndiceOcupacional.Dependencia.Sucursal.Nombre,
                                
                                NombreManualPuesto = s.IndiceOcupacional.ManualPuesto.Nombre,
                                DescripcionManualPuesto = s.IndiceOcupacional.ManualPuesto.Descripcion,
                                MisionManualPuesto = s.IndiceOcupacional.ManualPuesto.Mision,
                                
                                IdRelacionesInternasExternas =  
                                    s.IndiceOcupacional.ManualPuesto.RelacionesInternasExternas.IdRelacionesInternasExternas,
                                NombreRelacionesInternasExternas = 
                                    s.IndiceOcupacional.ManualPuesto.RelacionesInternasExternas.Nombre,
                                DescripcionRelacionesInternasExternas = 
                                    s.IndiceOcupacional.ManualPuesto.RelacionesInternasExternas.Descripcion,
                                

                                NombreRolPuesto = s.IndiceOcupacional.RolPuesto.Nombre,


                                NombreEscalaGrados = s.IndiceOcupacional.EscalaGrados.Nombre,
                                Remuneracion = s.IndiceOcupacional.EscalaGrados.Remuneracion,
                                Grado = s.IndiceOcupacional.EscalaGrados.Grado,

                                NumeroPartidaGeneral = 
                                (s.IndiceOcupacional.PartidaGeneral == null)
                                ?""
                                :s.IndiceOcupacional.PartidaGeneral.NumeroPartida,

                                NombreAmbito = s.IndiceOcupacional.Ambito.Nombre
                                
                            }
                            
                            ,

                            NombreFondoFinanciamiento = (s.FondoFinanciamiento != null)?s.FondoFinanciamiento.Nombre:"",
                            NombreTipoNombramiento = (s.TipoNombramiento != null)?s.TipoNombramiento.Nombre:"",
                            IdRelacionLaboral = (s.TipoNombramiento != null)? s.TipoNombramiento.RelacionLaboral.IdRelacionLaboral:0,
                            NombreRelacionLaboral = (s.TipoNombramiento != null) ? s.TipoNombramiento.RelacionLaboral.Nombre:"",
                            NombreModalidadPartida = (s.ModalidadPartida != null) ? s.ModalidadPartida.Nombre:""
                            
                        }
                    
                    ).ToList();

                return lista;

            }
            catch (Exception ex)
            {
                return lista;
            }
        }

        /// <summary>
        /// Este método obtiene el salario de la escala de grados y si existe un salario real,
        /// envía el salario real, discrimina por Fecha, numeroPartida y codigotrabajo
        /// </summary>
        /// <returns></returns>
        // GET: api/IndicesOcupacionalesModalidadPartida
        [HttpGet]
        [Route("ListarIOMPVMCodigosDiferentes")]
        public async Task<List<IndicesOcupacionalesModalidadPartidaViewModel>> ListarIOMPVMCodigosDiferentes()
        {

            var lista = new List<IndicesOcupacionalesModalidadPartidaViewModel>();


            try
            {
                var listaIOMP = await db.IndiceOcupacionalModalidadPartida
                    .Include(i => i.IndiceOcupacional)
                        .ThenInclude(t => t.EscalaGrados)

                        .Include(i => i.IndiceOcupacional.Dependencia)
                            .ThenInclude(t => t.Sucursal)

                        .Include(i => i.IndiceOcupacional.ManualPuesto)
                            .ThenInclude(t => t.RelacionesInternasExternas)

                        .Include(i => i.IndiceOcupacional.RolPuesto)

                        .Include(i => i.IndiceOcupacional.Ambito)

                        .Include(i => i.IndiceOcupacional.PartidaGeneral)

                    .Include(i => i.ModalidadPartida)
                    .Include(i => i.FondoFinanciamiento)

                    .Include(i => i.TipoNombramiento)
                        .ThenInclude(t => t.RelacionLaboral)

                    .OrderByDescending(o => o.Fecha)
                    .DistinctBy(d => new { d.IndiceOcupacional.ManualPuesto.Nombre, d.NumeroPartidaIndividual,d.CodigoContrato})
                    .ToAsyncEnumerable().ToList();



                lista = listaIOMP.Select(
                    s => new IndicesOcupacionalesModalidadPartidaViewModel
                    {
                        IdIndiceOcupacionalModalidadPartida = s.IdIndiceOcupacionalModalidadPartida,
                        Fecha = s.Fecha,

                        SalarioActual =
                                s.SalarioReal > 0
                                ? s.SalarioReal
                                : s.IndiceOcupacional.EscalaGrados.Remuneracion
                            ,

                        IdIndiceOcupacional = s.IdIndiceOcupacional,
                        IdEmpleado = s.IdEmpleado,
                        IdFondoFinanciamiento = s.IdFondoFinanciamiento,
                        IdTipoNombramiento = s.IdTipoNombramiento,
                        CodigoContrato = s.CodigoContrato,
                        IdModalidadPartida = s.IdModalidadPartida,
                        NumeroPartidaIndividual = s.NumeroPartidaIndividual,
                        FechaFin = s.FechaFin,

                        IndiceOcupacionalViewModel = new IndiceOcupacionalViewModel
                        {

                            IdIndiceOcupacional = s.IndiceOcupacional.IdIndiceOcupacional,
                            IdDependencia = s.IndiceOcupacional.IdDependencia,
                            IdManualPuesto = s.IndiceOcupacional.IdManualPuesto,
                            IdRolPuesto = s.IndiceOcupacional.IdRolPuesto,
                            IdEscalaGrados = s.IndiceOcupacional.IdEscalaGrados,
                            IdPartidaGeneral = s.IndiceOcupacional.IdPartidaGeneral,
                            IdAmbito = s.IndiceOcupacional.IdAmbito,
                            Nivel = s.IndiceOcupacional.Nivel,


                            NombreDependencia = s.IndiceOcupacional.Dependencia.Nombre,
                            CodigoDependencia = s.IndiceOcupacional.Dependencia.Codigo,

                            IdSucursal = s.IndiceOcupacional.Dependencia.Sucursal.IdSucursal,
                            NombreSucursal = s.IndiceOcupacional.Dependencia.Sucursal.Nombre,

                            NombreManualPuesto = s.IndiceOcupacional.ManualPuesto.Nombre,
                            DescripcionManualPuesto = s.IndiceOcupacional.ManualPuesto.Descripcion,
                            MisionManualPuesto = s.IndiceOcupacional.ManualPuesto.Mision,

                            IdRelacionesInternasExternas =
                                    s.IndiceOcupacional.ManualPuesto.RelacionesInternasExternas.IdRelacionesInternasExternas,
                            NombreRelacionesInternasExternas =
                                    s.IndiceOcupacional.ManualPuesto.RelacionesInternasExternas.Nombre,
                            DescripcionRelacionesInternasExternas =
                                    s.IndiceOcupacional.ManualPuesto.RelacionesInternasExternas.Descripcion,


                            NombreRolPuesto = s.IndiceOcupacional.RolPuesto.Nombre,


                            NombreEscalaGrados = s.IndiceOcupacional.EscalaGrados.Nombre,
                            Remuneracion = s.IndiceOcupacional.EscalaGrados.Remuneracion,
                            Grado = s.IndiceOcupacional.EscalaGrados.Grado,

                            NumeroPartidaGeneral =
                                (s.IndiceOcupacional.PartidaGeneral == null)
                                ? ""
                                : s.IndiceOcupacional.PartidaGeneral.NumeroPartida,

                            NombreAmbito = s.IndiceOcupacional.Ambito.Nombre

                        }

                            ,

                        NombreFondoFinanciamiento = (s.FondoFinanciamiento != null) ? s.FondoFinanciamiento.Nombre : "",
                        NombreTipoNombramiento = (s.TipoNombramiento != null) ? s.TipoNombramiento.Nombre : "",
                        IdRelacionLaboral = (s.TipoNombramiento != null) ? s.TipoNombramiento.RelacionLaboral.IdRelacionLaboral : 0,
                        NombreRelacionLaboral = (s.TipoNombramiento != null) ? s.TipoNombramiento.RelacionLaboral.Nombre : "",
                        NombreModalidadPartida = (s.ModalidadPartida != null) ? s.ModalidadPartida.Nombre : ""

                    }

                    ).ToList();

                return lista;

            }
            catch (Exception ex)
            {
                return lista;
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
                    ExceptionTrace = ex.Message,
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
                //IndiceOcupacionalModalidadPartida.IdModalidadPartida = indiceOcupacionalModalidadPartida.IdModalidadPartida;
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



        // POST: api/IndiceOcupacionalModalidadPartida
        [HttpPost]
        [Route("InsertarIndiceOcupacionalModalidadPartida")]
        public async Task<Response> PostIndiceOcupacionalModalidadPartida([FromBody] IndiceOcupacionalModalidadPartida IndiceOcupacionalModalidadPartida)
        {
            
            using (var transaction = await db.Database.BeginTransactionAsync())
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

                    if (String.IsNullOrEmpty(IndiceOcupacionalModalidadPartida.NumeroPartidaIndividual) )
                    {
                        return new Response {
                            IsSuccess = false,
                            Message = Mensaje.NumeroPartidaObligatorio
                        };
                    }
                    

                    // Se obtienen los diferentes tipos de relación laboral
                    var nombramiento = await db.TipoNombramiento
                        .Include(i=>i.RelacionLaboral)
                        .Where(w => w.IdTipoNombramiento == IndiceOcupacionalModalidadPartida.IdTipoNombramiento)
                        .FirstOrDefaultAsync();

                    
                    // Se crea el modelo para guardar
                    var modelo = new IndiceOcupacionalModalidadPartida
                    {

                        IdIndiceOcupacional = IndiceOcupacionalModalidadPartida.IdIndiceOcupacional,
                        IdFondoFinanciamiento = IndiceOcupacionalModalidadPartida.IdFondoFinanciamiento,
                        IdTipoNombramiento = IndiceOcupacionalModalidadPartida.IdTipoNombramiento,
                        SalarioReal = IndiceOcupacionalModalidadPartida.SalarioReal,
                        Fecha = IndiceOcupacionalModalidadPartida.Fecha,
                        IdEmpleado = IndiceOcupacionalModalidadPartida.IdEmpleado

                    };



                    if (
                        nombramiento != null
                        && nombramiento.RelacionLaboral.Nombre.ToUpper() == ConstantesTipoRelacion.Contrato.ToUpper()
                        )
                    {
                        modelo.CodigoContrato = IndiceOcupacionalModalidadPartida.NumeroPartidaIndividual;
                        modelo.FechaFin = IndiceOcupacionalModalidadPartida.FechaFin;

                    }

                    else if (
                        nombramiento != null
                        && nombramiento.RelacionLaboral.Nombre.ToUpper() == ConstantesTipoRelacion.Nombramiento.ToUpper()
                        )
                    {
                        modelo.NumeroPartidaIndividual = IndiceOcupacionalModalidadPartida.NumeroPartidaIndividual;
                        modelo.IdModalidadPartida = IndiceOcupacionalModalidadPartida.IdModalidadPartida;

                    }



                        if (IndiceOcupacionalModalidadPartida.IdIndiceOcupacionalModalidadPartida < 1)
                    {

                        


                        // ** Si no existe un registro se crea uno nuevo
                        db.IndiceOcupacionalModalidadPartida.Add(modelo);
                        await db.SaveChangesAsync();

                        // ** Se agrega la dependencia y el estado del empleado cambia a activo
                        var empleado = await db.Empleado.Where(x => x.IdEmpleado == modelo.IdEmpleado).FirstOrDefaultAsync();

                        empleado.IdDependencia = IndiceOcupacionalModalidadPartida.IdDependencia;
                        empleado.Activo = true;

                        db.Empleado.Update(empleado);
                        await db.SaveChangesAsync();

                        transaction.Commit();

                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.GuardadoSatisfactorio
                        };

                    }
                    else
                    {
                        // En caso de existir un registro se edita

                        var actualizar = await db.IndiceOcupacionalModalidadPartida
                            .Where(w => w.IdIndiceOcupacionalModalidadPartida == IndiceOcupacionalModalidadPartida.IdIndiceOcupacionalModalidadPartida)
                            .FirstOrDefaultAsync();

                        actualizar.IdIndiceOcupacional = modelo.IdIndiceOcupacional;
                        actualizar.IdFondoFinanciamiento = modelo.IdFondoFinanciamiento;
                        actualizar.IdTipoNombramiento = modelo.IdTipoNombramiento;
                        actualizar.SalarioReal = modelo.SalarioReal;
                        actualizar.Fecha = modelo.Fecha;
                        actualizar.IdEmpleado = modelo.IdEmpleado;
                        actualizar.CodigoContrato = modelo.CodigoContrato;
                        actualizar.IdModalidadPartida = modelo.IdModalidadPartida;
                        actualizar.NumeroPartidaIndividual = modelo.NumeroPartidaIndividual;
                        actualizar.FechaFin = modelo.FechaFin;
        
                        db.IndiceOcupacionalModalidadPartida.Update(actualizar);
                        await db.SaveChangesAsync();

                        transaction.Commit();

                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.GuardadoSatisfactorio
                        };
                    }


                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.Error,
                    };
                }
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

        private Response Existe(IndiceOcupacionalModalidadPartida IndiceOcupacionalModalidadPartida)
        {
            var fecha = IndiceOcupacionalModalidadPartida.Fecha;
            var salarioReal = IndiceOcupacionalModalidadPartida.SalarioReal;
            //var IndiceOcupacionalModalidadPartidarespuesta = db.IndiceOcupacionalModalidadPartida.Where(p => p.Fecha == fecha && p.SalarioReal == salarioReal && p.IdIndiceOcupacional == IndiceOcupacionalModalidadPartida.IdIndiceOcupacional && p.IdEmpleado == IndiceOcupacionalModalidadPartida.IdEmpleado && p.IdFondoFinanciamiento == IndiceOcupacionalModalidadPartida.IdFondoFinanciamiento && p.IdModalidadPartida == IndiceOcupacionalModalidadPartida.IdModalidadPartida && p.IdTipoNombramiento == IndiceOcupacionalModalidadPartida.IdTipoNombramiento).FirstOrDefault();
            var IndiceOcupacionalModalidadPartidarespuesta = db.IndiceOcupacionalModalidadPartida.Where(p => p.Fecha == fecha && p.SalarioReal == salarioReal && p.IdIndiceOcupacional == IndiceOcupacionalModalidadPartida.IdIndiceOcupacional && p.IdEmpleado == IndiceOcupacionalModalidadPartida.IdEmpleado && p.IdFondoFinanciamiento == IndiceOcupacionalModalidadPartida.IdFondoFinanciamiento && p.IdTipoNombramiento == IndiceOcupacionalModalidadPartida.IdTipoNombramiento).FirstOrDefault();
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
