using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using System;
using bd.swth.entidades.Enumeradores;
using bd.swth.entidades.Utils;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.log.guardar.Enumeradores;
using bd.swth.entidades.ViewModels;
using bd.swth.entidades.Constantes;
using MoreLinq;
using bd.swth.entidades.ObjectTransfer;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Distributivos")]
    public class DistributivosController : Controller
    {
        private readonly SwTHDbContext db;

        public DistributivosController(SwTHDbContext db)
        {
            this.db = db;
        }

        // Distributivo Real: Es la situación actual de los empleados en el banco (El distributivo con los movimientos temporales del empleado)

        // Distributivo Formal: Es la situación oficial de los empleados en el banco (El distributivo propiamente)
        


        /// <summary>
        /// Obtiene el distributivo real, (escala de grados, grupo ocupacional y remuneración) están 
        /// validados si es sobrevalorado
        /// </summary>
        /// <returns></returns>
        // GET: api/Distributivos
        [HttpGet]
        [Route("ObtenerDistributivoReal")]
        public async Task<List<DistributivoSituacionActual>> ObtenerDistributivoReal()
        {
            var modelo = new List<DistributivoSituacionActual>();

            try {

                var distributivo = await db.DistributivoSituacionActual
                    .Include(i=>i.Empleado)
                    .Include(i=>i.Empleado.Persona)
                    .Include(i=>i.IndiceOcupacionalModalidadPartida)

                    .Include(i=>i.IndiceOcupacionalModalidadPartida.Dependencia)
                    .Include(i => i.IndiceOcupacionalModalidadPartida.Dependencia.Sucursal)

                    .Include(i=>i.IndiceOcupacionalModalidadPartida.EscalaGradosSobrevalorado)

                    .Include(i => i.IndiceOcupacionalModalidadPartida.GrupoOcupacionalSobrevalorado)

                    .Include(i => i.IndiceOcupacionalModalidadPartida.IndiceOcupacional)
                    .Include(i => i.IndiceOcupacionalModalidadPartida.IndiceOcupacional.RolPuesto)
                    .Include(i => i.IndiceOcupacionalModalidadPartida.IndiceOcupacional.EscalaGrados)
                    .Include(i => i.IndiceOcupacionalModalidadPartida.IndiceOcupacional.EscalaGrados.GrupoOcupacional)

                    .Include(i=>i.TipoNombramiento)
                    .Include(i => i.TipoNombramiento.RelacionLaboral)
                    .Include(i => i.TipoNombramiento.RelacionLaboral.RegimenLaboral)
                    
                    .ToListAsync();

                modelo = distributivo
                    .Select(s => new DistributivoSituacionActual
                    {
                        IdDistributivoSituacionActual = s.IdDistributivoSituacionActual,
                        IdIndiceOcupacionalModalidadPartida = s.IdIndiceOcupacionalModalidadPartida,
                        IdEmpleado = s.IdEmpleado,
                        IdFondoFinanciamiento = s.IdFondoFinanciamiento,
                        IdTipoNombramiento = s.IdTipoNombramiento,
                        Observacion = s.Observacion,
                        IdEmpleadoMovimiento = s.IdEmpleadoMovimiento,
                        
                        Empleado = new Empleado {
                            IdEmpleado = s.IdEmpleado,
                            IdPersona = s.Empleado.IdPersona,
                            IdCiudadLugarNacimiento = s.Empleado.IdCiudadLugarNacimiento,
                            IdProvinciaLugarSufragio = s.Empleado.IdProvinciaLugarSufragio,
                            IdDependencia = s.Empleado.IdDependencia,
                            IdBrigadaSSORol = s.Empleado.IdBrigadaSSORol,
                            FechaIngreso = s.Empleado.FechaIngreso,
                            FechaIngresoSectorPublico = s.Empleado.FechaIngresoSectorPublico,
                            NombreUsuario = s.Empleado.NombreUsuario,

                            Persona = new Persona {
                                IdPersona = s.Empleado.Persona.IdPersona,
                                FechaNacimiento = s.Empleado.Persona.FechaNacimiento,
                                IdSexo = s.Empleado.Persona.IdSexo,
                                IdTipoIdentificacion = s.Empleado.Persona.IdTipoIdentificacion,
                                IdEstadoCivil = s.Empleado.Persona.IdEstadoCivil,
                                Identificacion = s.Empleado.Persona.Identificacion,
                                Nombres = s.Empleado.Persona.Nombres,
                                Apellidos = s.Empleado.Persona.Apellidos,
                                TelefonoCasa = s.Empleado.Persona.TelefonoCasa,
                                TelefonoPrivado = s.Empleado.Persona.TelefonoPrivado,
                            },

                        },

                        
                        IndiceOcupacionalModalidadPartida = new IndiceOcupacionalModalidadPartida {

                            IdIndiceOcupacionalModalidadPartida = s.IndiceOcupacionalModalidadPartida.IdIndiceOcupacionalModalidadPartida,
                            IdRelacionLaboral = s.IndiceOcupacionalModalidadPartida.IdRelacionLaboral,
                            CodigoContrato = s.IndiceOcupacionalModalidadPartida.CodigoContrato,
                            NumeroPartidaIndividual = s.IndiceOcupacionalModalidadPartida.NumeroPartidaIndividual,
                            IdDependencia = s.IndiceOcupacionalModalidadPartida.IdDependencia,
                            IdIndiceOcupacional = s.IndiceOcupacionalModalidadPartida.IdIndiceOcupacional,
                            IdModalidadPartida = s.IndiceOcupacionalModalidadPartida.IdModalidadPartida,
                            IdGrupoOcupacionalSobrevalorado = s.IndiceOcupacionalModalidadPartida.IdGrupoOcupacionalSobrevalorado,
                            IdEscalaGradosSobrevalorado = s.IndiceOcupacionalModalidadPartida.IdEscalaGradosSobrevalorado,
                            Rmusobrevalorado = s.IndiceOcupacionalModalidadPartida.Rmusobrevalorado,
                            Activo = s.IndiceOcupacionalModalidadPartida.Activo,
                            EsJefe = s.IndiceOcupacionalModalidadPartida.EsJefe,
                            
                            Dependencia = new Dependencia {
                                IdDependencia = s.IndiceOcupacionalModalidadPartida.Dependencia.IdDependencia,
                                Nombre = s.IndiceOcupacionalModalidadPartida.Dependencia.Nombre,
                                IdSucursal = s.IndiceOcupacionalModalidadPartida.Dependencia.IdSucursal,
                                IdDependenciaPadre = s.IndiceOcupacionalModalidadPartida.Dependencia.IdDependenciaPadre,
                                Codigo = s.IndiceOcupacionalModalidadPartida.Dependencia.Codigo,

                                Sucursal = new Sucursal {
                                    IdSucursal = s.IndiceOcupacionalModalidadPartida.Dependencia.Sucursal.IdSucursal,
                                    Nombre = s.IndiceOcupacionalModalidadPartida.Dependencia.Sucursal.Nombre,
                                }
                            },

                            

                            IndiceOcupacional = new IndiceOcupacional {
                                IdIndiceOcupacional = s.IndiceOcupacionalModalidadPartida.IndiceOcupacional.IdIndiceOcupacional,
                                DenominacionPuesto = s.IndiceOcupacionalModalidadPartida.IndiceOcupacional.DenominacionPuesto,
                                UnidadAdministrativa = s.IndiceOcupacionalModalidadPartida.IndiceOcupacional.UnidadAdministrativa,
                                IdRolPuesto = s.IndiceOcupacionalModalidadPartida.IndiceOcupacional.IdRolPuesto,
                                IdEscalaGrados = s.IndiceOcupacionalModalidadPartida.IndiceOcupacional.IdEscalaGrados,
                                Activo = s.IndiceOcupacionalModalidadPartida.IndiceOcupacional.Activo,

                                
                                RolPuesto = new RolPuesto {
                                    IdRolPuesto = s.IndiceOcupacionalModalidadPartida.IndiceOcupacional.RolPuesto.IdRolPuesto,
                                    Nombre = s.IndiceOcupacionalModalidadPartida.IndiceOcupacional.RolPuesto.Nombre,
                                },

                                
                                EscalaGrados = new EscalaGrados {

                                    // Escala de grados con validacion si existe sobrevalorado

                                    IdEscalaGrados = 
                                    (s.IndiceOcupacionalModalidadPartida.IdEscalaGradosSobrevalorado != null)
                                    ? s.IndiceOcupacionalModalidadPartida.EscalaGradosSobrevalorado.IdEscalaGrados
                                    : s.IndiceOcupacionalModalidadPartida.IndiceOcupacional.EscalaGrados.IdEscalaGrados,

                                    IdGrupoOcupacional =
                                    (s.IndiceOcupacionalModalidadPartida.IdEscalaGradosSobrevalorado != null)
                                    ? s.IndiceOcupacionalModalidadPartida.EscalaGradosSobrevalorado.IdGrupoOcupacional
                                    : s.IndiceOcupacionalModalidadPartida.IndiceOcupacional.EscalaGrados.IdGrupoOcupacional,

                                    Grado =
                                    (s.IndiceOcupacionalModalidadPartida.IdEscalaGradosSobrevalorado != null)
                                    ? s.IndiceOcupacionalModalidadPartida.EscalaGradosSobrevalorado.Grado
                                    : s.IndiceOcupacionalModalidadPartida.IndiceOcupacional.EscalaGrados.Grado,


                                    // Remuneración con validación si es sobrevalorado
                                    Remuneracion =
                                    (s.IndiceOcupacionalModalidadPartida.IdEscalaGradosSobrevalorado != null)
                                    ? s.IndiceOcupacionalModalidadPartida.Rmusobrevalorado
                                    : s.IndiceOcupacionalModalidadPartida.IndiceOcupacional.EscalaGrados.Remuneracion,


                                    Nombre =
                                    (s.IndiceOcupacionalModalidadPartida.IdEscalaGradosSobrevalorado != null)
                                    ? s.IndiceOcupacionalModalidadPartida.EscalaGradosSobrevalorado.Nombre
                                    : s.IndiceOcupacionalModalidadPartida.IndiceOcupacional.EscalaGrados.Nombre,

                                    // Grupo ocupacional con validación si es sobrevalorado
                                    GrupoOcupacional = new GrupoOcupacional {

                                        IdGrupoOcupacional  =
                                        (s.IndiceOcupacionalModalidadPartida.IdGrupoOcupacionalSobrevalorado != null)
                                        ? s.IndiceOcupacionalModalidadPartida.GrupoOcupacionalSobrevalorado
                                        .IdGrupoOcupacional
                                        : s.IndiceOcupacionalModalidadPartida.IndiceOcupacional
                                        .EscalaGrados.GrupoOcupacional.IdGrupoOcupacional
                                        ,

                                        TipoEscala =
                                        (s.IndiceOcupacionalModalidadPartida.IdGrupoOcupacionalSobrevalorado != null)
                                        ? s.IndiceOcupacionalModalidadPartida.GrupoOcupacionalSobrevalorado
                                        .TipoEscala
                                        : s.IndiceOcupacionalModalidadPartida.IndiceOcupacional
                                        .EscalaGrados.GrupoOcupacional.TipoEscala
                                        ,
                                    },

                                },
                                

                            },
                            
                        },
                        

                        TipoNombramiento = new TipoNombramiento {
                            IdTipoNombramiento = s.TipoNombramiento.IdTipoNombramiento,
                            IdRelacionLaboral = s.TipoNombramiento.IdRelacionLaboral,
                            Nombre = s.TipoNombramiento.Nombre,

                            RelacionLaboral = new RelacionLaboral {
                                IdRelacionLaboral = s.TipoNombramiento.RelacionLaboral.IdRelacionLaboral,
                                IdRegimenLaboral = s.TipoNombramiento.RelacionLaboral.IdRegimenLaboral,
                                Nombre = s.TipoNombramiento.RelacionLaboral.Nombre,

                                RegimenLaboral = new RegimenLaboral {
                                    IdRegimenLaboral = s.TipoNombramiento.RelacionLaboral.RegimenLaboral.IdRegimenLaboral,
                                    Nombre = s.TipoNombramiento.RelacionLaboral.RegimenLaboral.Nombre,
                                },

                            },

                        },

                        
                    }
                    ).ToList();

                return modelo;

            } catch (Exception ex)
            {
                return modelo;
            }
        }
        

    }
}