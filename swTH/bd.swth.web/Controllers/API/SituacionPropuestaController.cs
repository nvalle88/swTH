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
using bd.webappth.entidades.ObjectTransfer;
using bd.swth.servicios.Interfaces;
using bd.webappth.entidades.ViewModels;
using bd.swth.entidades.ViewModels;
using System.Diagnostics;
using bd.swth.entidades.Constantes;
using EnviarCorreo;
using SendMails.methods;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/SituacionPropuesta")]
    public class SituacionPropuestaController : Controller
    {
        private readonly SwTHDbContext db;

        public SituacionPropuestaController(SwTHDbContext db)
        {
            this.db = db;
        }


        #region Métodos para el mantenimiento del proceso de Situación propuesta

        /// <summary>
        /// Se requiere un NombreUsuario,
        /// Permite obtener en response->respuesta el modelo index de situación propuesta si el NombreUsuario existe y es jefe,
        /// Caso contrario devuelve un Response con: mensaje y estado false
        /// </summary>
        /// <param name="requerimientoRolPorDependenciaViewModel"></param>
        /// <returns></returns>
        // POST: api/SituacionActual
        [HttpPost]
        [Route("ObtenerRequerimientoRolPorDependencia")]
        public async Task<Response> ObtenerRequerimientoRolPorDependencia([FromBody] RequerimientoRolPorDependenciaViewModel requerimientoRolPorDependenciaViewModel)
        {
            var modelo = new RequerimientoRolPorDependenciaViewModel();
            modelo.RolesNivelJerarquicoSuperior = new RequerimientoRolPorGrupoOcupacionalViewModel();
            modelo.RolesNivelOperativo = new RequerimientoRolPorGrupoOcupacionalViewModel();
            modelo.RolesNivelJerarquicoSuperior.ListaRolesRequeridos = new List<RequerimientoRolViewModel>();
            modelo.RolesNivelOperativo.ListaRolesRequeridos = new List<RequerimientoRolViewModel>();

            try
            {
                var userResponseSend = new Response { Resultado = requerimientoRolPorDependenciaViewModel.NombreUsuario };

                Response acceso = await VerificarAcceso(userResponseSend);

                if (acceso.IsSuccess)
                {

                    requerimientoRolPorDependenciaViewModel.IdDependencia = Convert.ToInt32(acceso.Resultado);

                    // Obtención datos dependencia
                    var dependencia = await db.Dependencia
                        .Where(x => x.IdDependencia == requerimientoRolPorDependenciaViewModel.IdDependencia)
                        .FirstOrDefaultAsync();

                    // Obtención de datos para saber si el documento está habilitado
                    var actualYear = DateTime.Now.Year;

                    var procesoDatos = await db.ActivarPersonalTalentoHumano
                        .Where(x =>
                            x.IdDependencia == dependencia.IdDependencia
                            && x.Fecha.Year == actualYear
                        )
                        .FirstOrDefaultAsync();

                    if (procesoDatos != null)
                    {
                        if (procesoDatos.Estado == Convert.ToInt32(Constantes.ActivacionPersonalValorActivado))
                        {
                            // Ingresa a este proceso cuando existe un documento y está habilitado

                            // Obtención de los datos actuales por dependencia

                            var grupoOcupacionalSuperior = await db.GrupoOcupacional
                                .Where(x => x.TipoEscala == ConstantesGrupoOcupacional.GrupoOcupacionalNivelSuperior)
                                .FirstOrDefaultAsync();

                            var grupoOcupacionalOperativo = await db.GrupoOcupacional
                                .Where(x => x.TipoEscala == ConstantesGrupoOcupacional.GrupoOcupacionalNivelOperativo)
                                .FirstOrDefaultAsync();

                            modelo.IdDependencia = requerimientoRolPorDependenciaViewModel.IdDependencia;
                            modelo.NombreDependencia = dependencia.Nombre;

                            modelo.RolesNivelJerarquicoSuperior = new RequerimientoRolPorGrupoOcupacionalViewModel
                            {
                                IdGrupoOcupacional = grupoOcupacionalSuperior.IdGrupoOcupacional,
                                NombreGrupoOcupacional = grupoOcupacionalSuperior.TipoEscala,

                                ListaRolesRequeridos = await db.SituacionPropuesta
                                .Where(x =>
                                    x.IdGrupoOcupacional == grupoOcupacionalSuperior.IdGrupoOcupacional
                                    && x.IdDependencia == dependencia.IdDependencia
                                )
                                .Select(y => new RequerimientoRolViewModel
                                {
                                    IdRolPuesto = y.IdRolPuesto,
                                    NombreRolPuesto = y.RolPuesto.Nombre,
                                    Cantidad = y.Cantidad,
                                    Descripcion = y.Descripcion
                                }
                                )
                                .ToListAsync(),

                            };

                            modelo.RolesNivelOperativo = new RequerimientoRolPorGrupoOcupacionalViewModel
                            {
                                IdGrupoOcupacional = grupoOcupacionalOperativo.IdGrupoOcupacional,
                                NombreGrupoOcupacional = grupoOcupacionalOperativo.TipoEscala,

                                ListaRolesRequeridos = await db.SituacionPropuesta
                                .Where(x =>
                                    x.IdGrupoOcupacional == grupoOcupacionalOperativo.IdGrupoOcupacional
                                    && x.IdDependencia == dependencia.IdDependencia
                                )
                                .Select(y => new RequerimientoRolViewModel
                                {
                                    IdRolPuesto = y.IdRolPuesto,
                                    NombreRolPuesto = y.RolPuesto.Nombre,
                                    Cantidad = y.Cantidad,
                                    Descripcion = y.Descripcion
                                }
                                )
                                .ToListAsync()
                            };

                            return new Response
                            {
                                IsSuccess = true,
                                Resultado = modelo
                            };

                        }
                    }

                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ProcesoDesactivado

                    };


                }
                else
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = acceso.Message

                    };
                }

                

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



        /// <summary>
        /// Recibe el nombre de usuario en el result del response para para validar si es jefe,
        /// si es jefe da como resultado el IdDependencia y true como IsSuccess
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        // POST: api/SituacionActual
        [HttpPost]
        [Route("VerificarAcceso")]
        public async Task<Response> VerificarAcceso([FromBody] Response response)
        {
            try
            {
                var ctrlDistributivo = new DistributivosController(db);
                
                var distributivo = await ctrlDistributivo.ObtenerDistributivoReal();

                var registro = distributivo
                    .Where(w => w.Empleado.NombreUsuario == response.Resultado.ToString())
                    .FirstOrDefault();
                

                if (
                        registro != null
                        && registro.Empleado.EsJefe == true
                    )
                {
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.RegistroNoEncontrado,
                        Resultado = registro.IndiceOcupacionalModalidadPartida.Dependencia.IdDependencia
                    };
                }

                if (
                    registro != null
                    && registro.Empleado.EsJefe == false
                ) {

                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.AccesoNoAutorizado
                    };
                }

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.RegistroNoEncontrado
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

        


        /// <summary>
        /// Se requiere un IdDependencia
        /// </summary>
        /// <param name="requerimientoRolPorDependenciaViewModel"></param>
        /// <returns></returns>
        // POST: api/SituacionActual
        [HttpPost]
        [Route("CrearRequerimientoRolPorDependencia")]
        public async Task<Response> CrearRequerimientoRolPorDependencia([FromBody] RequerimientoRolPorDependenciaViewModel requerimientoRolPorDependenciaViewModel)
        {

            
            var requerimiento = new RequerimientoRolPorDependenciaViewModel();
            
            try
            {


                var userResponseSend = new Response { Resultado = requerimientoRolPorDependenciaViewModel.NombreUsuario };

                Response acceso = await VerificarAcceso(userResponseSend);

                if (acceso.IsSuccess)
                {
                    // Se busca y se asigna la dependencia
                    requerimientoRolPorDependenciaViewModel.IdDependencia = Convert.ToInt32(acceso.Resultado);
                    

                    var registro = await db.ActivarPersonalTalentoHumano
                    .Where(x =>
                        x.IdDependencia == requerimientoRolPorDependenciaViewModel.IdDependencia
                        && x.Fecha.Year == DateTime.Now.Year
                     ).FirstOrDefaultAsync();

                    if (registro != null)
                    {

                        var estado = registro.Estado;

                        if (estado == Convert.ToInt32(Constantes.ActivacionPersonalValorActivado))
                        {
                            var query = db.SituacionPropuesta;

                            var dependencia = await db.Dependencia
                                .Where(x => x.IdDependencia == requerimientoRolPorDependenciaViewModel.IdDependencia)
                                .FirstOrDefaultAsync();

                            requerimiento.IdDependencia = dependencia.IdDependencia;
                            requerimiento.NombreDependencia = dependencia.Nombre;

                            var grupoOcupacionalSuperior = await db.GrupoOcupacional.Where(x => x.TipoEscala == ConstantesGrupoOcupacional.GrupoOcupacionalNivelSuperior).FirstOrDefaultAsync();

                            var grupoOcupacionalOperativo = await db.GrupoOcupacional.Where(x => x.TipoEscala == ConstantesGrupoOcupacional.GrupoOcupacionalNivelOperativo).FirstOrDefaultAsync();

                            requerimiento = new RequerimientoRolPorDependenciaViewModel
                            {
                                IdDependencia = dependencia.IdDependencia,
                                NombreDependencia = dependencia.Nombre,
                                RolesNivelJerarquicoSuperior = new RequerimientoRolPorGrupoOcupacionalViewModel
                                {
                                    IdGrupoOcupacional = grupoOcupacionalSuperior.IdGrupoOcupacional,
                                    NombreGrupoOcupacional = grupoOcupacionalSuperior.TipoEscala,

                                    ListaRolesRequeridos = await db.RolPuesto
                                     .Where(x => x.IndiceOcupacional.
                                       Any(y => Convert.ToInt32(y.EscalaGrados.IdGrupoOcupacional) == grupoOcupacionalSuperior.IdGrupoOcupacional)
                                      )

                                     .Select(
                                         rol => new RequerimientoRolViewModel
                                         {
                                             IdRolPuesto = rol.IdRolPuesto,
                                             NombreRolPuesto = rol.Nombre,
                                             Cantidad = (query
                                                    .Where(s =>
                                                        s.IdDependencia == dependencia.IdDependencia
                                                        && s.IdRolPuesto == rol.IdRolPuesto
                                                    ).FirstOrDefault() != null
                                                    ? query
                                                    .Where(s =>
                                                        s.IdDependencia == dependencia.IdDependencia
                                                        && s.IdRolPuesto == rol.IdRolPuesto
                                                    ).FirstOrDefault().Cantidad
                                                    : 0
                                             ),
                                             Descripcion = (query
                                                    .Where(s =>
                                                        s.IdDependencia == dependencia.IdDependencia
                                                        && s.IdRolPuesto == rol.IdRolPuesto
                                                    ).FirstOrDefault() != null
                                                    ? query
                                                    .Where(s =>
                                                        s.IdDependencia == dependencia.IdDependencia
                                                        && s.IdRolPuesto == rol.IdRolPuesto
                                                    ).FirstOrDefault().Descripcion
                                                    : ""
                                             ),
                                         }
                                     )
                                     .ToListAsync()
                                },

                                RolesNivelOperativo = new RequerimientoRolPorGrupoOcupacionalViewModel
                                {
                                    IdGrupoOcupacional = grupoOcupacionalOperativo.IdGrupoOcupacional,
                                    NombreGrupoOcupacional = grupoOcupacionalOperativo.TipoEscala,

                                    ListaRolesRequeridos = await db.RolPuesto
                                     .Where(x => x.IndiceOcupacional.
                                       Any(y => Convert.ToInt32(y.EscalaGrados.IdGrupoOcupacional) == grupoOcupacionalOperativo.IdGrupoOcupacional)
                                      )

                                     .Select(
                                         rol => new RequerimientoRolViewModel
                                         {
                                             IdRolPuesto = rol.IdRolPuesto,
                                             NombreRolPuesto = rol.Nombre,
                                             Cantidad = (query
                                                    .Where(s =>
                                                        s.IdDependencia == dependencia.IdDependencia
                                                        && s.IdRolPuesto == rol.IdRolPuesto
                                                    ).FirstOrDefault() != null
                                                    ? query
                                                    .Where(s =>
                                                        s.IdDependencia == dependencia.IdDependencia
                                                        && s.IdRolPuesto == rol.IdRolPuesto
                                                    ).FirstOrDefault().Cantidad
                                                    : 0
                                             ),
                                             Descripcion = (query
                                                    .Where(s =>
                                                        s.IdDependencia == dependencia.IdDependencia
                                                        && s.IdRolPuesto == rol.IdRolPuesto
                                                    ).FirstOrDefault() != null
                                                    ? query
                                                    .Where(s =>
                                                        s.IdDependencia == dependencia.IdDependencia
                                                        && s.IdRolPuesto == rol.IdRolPuesto
                                                    ).FirstOrDefault().Descripcion
                                                    : ""
                                            )
                                         }
                                     )
                                     .ToListAsync()
                                },

                            };


                            return new Response
                            {
                                IsSuccess = true,
                                Resultado = requerimiento
                            };

                        }

                    }


                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ProcesoDesactivado
                    };
                }
                else
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = acceso.Message

                    };
                }
                

            }
            catch (Exception ex)
            {

                return new Response {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };
            }

        }


        // POST: api/SituacionActual
        [HttpPost]
        [Route("InsertarNivelesOperativos")]
        public async Task<Response> InsertarNivelesOperativos([FromBody] RequerimientoRolPorDependenciaViewModel requerimientoRolPorDependenciaViewModel)
        {
            try
            {
                var varProceso = db.Dependencia
                    .Where(x => x.IdDependencia == requerimientoRolPorDependenciaViewModel.IdDependencia)
                    .Select(s => new Proceso
                    {
                        IdProceso = s.IdProceso,
                        Nombre = s.Nombre
                    }
                    )
                    .FirstOrDefaultAsync();


                foreach (var item in requerimientoRolPorDependenciaViewModel.RolesNivelOperativo.ListaRolesRequeridos)
                {
                    

                    if (item.Cantidad > 0)
                    {

                        var modelo = new SituacionPropuesta
                        {
                            IdDependencia = requerimientoRolPorDependenciaViewModel.IdDependencia,
                            IdGrupoOcupacional = requerimientoRolPorDependenciaViewModel.RolesNivelOperativo.IdGrupoOcupacional,
                            IdProceso = varProceso.Result.IdProceso,
                            IdRolPuesto = item.IdRolPuesto,
                            Cantidad = item.Cantidad,
                            Descripcion = (item.Descripcion!= null)?item.Descripcion.ToString().ToUpper():item.Descripcion,
                        };

                        var existe = Existe(modelo);

                        if (existe.IsSuccess)
                        {
                            var objeto = await db.SituacionPropuesta
                            .Where(x =>
                                x.IdDependencia == requerimientoRolPorDependenciaViewModel.IdDependencia
                                && x.IdGrupoOcupacional == requerimientoRolPorDependenciaViewModel.RolesNivelOperativo.IdGrupoOcupacional
                                && x.IdRolPuesto == item.IdRolPuesto
                            ).FirstOrDefaultAsync();

                            objeto.Cantidad = item.Cantidad;
                            objeto.Descripcion = (item.Descripcion != null) ? item.Descripcion.ToString().ToUpper() : item.Descripcion;


                            db.SituacionPropuesta.Update(objeto);

                        }
                        else
                        {
                            db.SituacionPropuesta.Add(modelo);
                        }

                        await db.SaveChangesAsync();
                    }


                }




                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.GuardadoSatisfactorio
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


        public Response Existe(SituacionPropuesta situacionPropuesta)
        {

            try
            {
                var objeto = db.SituacionPropuesta
                    .Where(x =>
                        x.IdDependencia == situacionPropuesta.IdDependencia
                        && x.IdGrupoOcupacional == situacionPropuesta.IdGrupoOcupacional
                        && x.IdRolPuesto == situacionPropuesta.IdRolPuesto
                    ).ToList();

                if (objeto.Count > 0)
                {
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.ExisteRegistro,
                        Resultado = objeto.FirstOrDefault()
                    };
                }

                return new Response
                {
                    IsSuccess = false
                };


            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion,
                    Resultado = ex
                };
            }

        }


        // POST: api/SituacionActual
        [HttpPost]
        [Route("InsertarNivelesJerarquicos")]
        public async Task<Response> InsertarNivelesJerarquicos([FromBody] RequerimientoRolPorDependenciaViewModel requerimientoRolPorDependenciaViewModel)
        {
            try
            {
                var varProceso = db.Dependencia
                    .Where(x => x.IdDependencia == requerimientoRolPorDependenciaViewModel.IdDependencia)
                    .Select(s => new Proceso
                    {
                        IdProceso = s.IdProceso,
                        Nombre = s.Nombre
                    }
                    )
                    .FirstOrDefaultAsync();


                foreach (var item in requerimientoRolPorDependenciaViewModel.RolesNivelJerarquicoSuperior.ListaRolesRequeridos)
                {


                    if (item.Cantidad > 0)
                    {

                        var modelo = new SituacionPropuesta
                        {
                            IdDependencia = requerimientoRolPorDependenciaViewModel.IdDependencia,
                            IdGrupoOcupacional = requerimientoRolPorDependenciaViewModel.RolesNivelJerarquicoSuperior.IdGrupoOcupacional,
                            IdProceso = varProceso.Result.IdProceso,
                            IdRolPuesto = item.IdRolPuesto,
                            Cantidad = item.Cantidad,
                            Descripcion = (item.Descripcion != null) ? item.Descripcion.ToString().ToUpper() : item.Descripcion,
                        };

                        var existe = Existe(modelo);

                        if (existe.IsSuccess)
                        {
                            var objeto = await db.SituacionPropuesta
                            .Where(x =>
                                x.IdDependencia == requerimientoRolPorDependenciaViewModel.IdDependencia
                                && x.IdGrupoOcupacional == requerimientoRolPorDependenciaViewModel.RolesNivelJerarquicoSuperior.IdGrupoOcupacional
                                && x.IdRolPuesto == item.IdRolPuesto
                            ).FirstOrDefaultAsync();

                            objeto.Cantidad = item.Cantidad;
                            objeto.Descripcion = (item.Descripcion != null) ? item.Descripcion.ToString().ToUpper() : item.Descripcion;


                            db.SituacionPropuesta.Update(objeto);

                        }
                        else
                        {
                            db.SituacionPropuesta.Add(modelo);
                        }

                        await db.SaveChangesAsync();
                    }


                }




                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.GuardadoSatisfactorio
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



        /// <summary>
        /// Necesita idRolPuesto e idDependencia
        /// </summary>
        /// <param name="requerimientoRolPorDependenciaViewModel"></param>
        /// <returns></returns>
        // POST: api/SituacionActual
        [HttpPost]
        [Route("EliminarRolPorDependencia")]
        public async Task<Response> EliminarRolPorDependencia([FromBody] RequerimientoRolPorDependenciaViewModel requerimientoRolPorDependenciaViewModel)
        {
            try
            {
                var idRol = 0;

                if (requerimientoRolPorDependenciaViewModel.RolesNivelJerarquicoSuperior != null)
                {
                    idRol = requerimientoRolPorDependenciaViewModel.RolesNivelJerarquicoSuperior.ListaRolesRequeridos.FirstOrDefault().IdRolPuesto;
                }

                else if (requerimientoRolPorDependenciaViewModel.RolesNivelOperativo != null)
                {
                    idRol = requerimientoRolPorDependenciaViewModel.RolesNivelOperativo.ListaRolesRequeridos.FirstOrDefault().IdRolPuesto;
                }

                else
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado
                    };
                }

                var modelo = await db.SituacionPropuesta
                    .Where(x =>
                        x.IdDependencia == requerimientoRolPorDependenciaViewModel.IdDependencia
                        && (x.IdRolPuesto == idRol)
                     ).FirstOrDefaultAsync();



                db.SituacionPropuesta.Remove(modelo);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.BorradoSatisfactorio
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



        /// <summary>
        /// Requiere IdDependencia
        /// </summary>
        /// <param name="requerimientoRolPorDependenciaViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CerrarProcesoRequerimientoPersona")]
        public async Task<Response> CerrarProcesoRequerimientoPersona([FromBody] RequerimientoRolPorDependenciaViewModel requerimientoRolPorDependenciaViewModel)
        {
            try
            {

                var registro = await db.ActivarPersonalTalentoHumano
                    .Where(x =>
                        x.IdDependencia == requerimientoRolPorDependenciaViewModel.IdDependencia
                        && x.Fecha.Year == DateTime.Now.Year
                     ).FirstOrDefaultAsync();

                if (registro != null)
                {

                    registro.Estado = Convert.ToInt32(Constantes.ActivacionPersonalValorDesactivado);
                    db.ActivarPersonalTalentoHumano.Update(registro);
                    await db.SaveChangesAsync();
                }


                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ProcesoFinalizadoSatisfactorio
                };

            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion + ex
                };

            }
        }

        #endregion


        #region Métodos usados para la aprobación de situación propuesta

        [HttpPost]
        [Route("ObtenerRequerimientoRolPorIdDependencia")]
        public async Task<Response> ObtenerRequerimientoRolPorIdDependencia([FromBody] RequerimientoRolPorDependenciaViewModel requerimientoRolPorDependenciaViewModel)
        {
            try {

                // Obtención datos dependencia
                var dependencia = await db.Dependencia.Where(x => x.IdDependencia == requerimientoRolPorDependenciaViewModel.IdDependencia).FirstOrDefaultAsync();

                // Obtención de datos para saber si el documento está habilitado
                var actualYear = DateTime.Now.Year;

                var procesoDatos = await db.ActivarPersonalTalentoHumano
                    .Where(x =>
                        x.IdDependencia == dependencia.IdDependencia
                        && x.Fecha.Year == actualYear
                    )

                    .FirstOrDefaultAsync();

                if (procesoDatos != null)
                {
                    if (true)
                    {
                        var modelo = new RequerimientoRolPorDependenciaViewModel();
                        modelo.RolesNivelJerarquicoSuperior = new RequerimientoRolPorGrupoOcupacionalViewModel();
                        modelo.RolesNivelOperativo = new RequerimientoRolPorGrupoOcupacionalViewModel();
                        modelo.RolesNivelJerarquicoSuperior.ListaRolesRequeridos = new List<RequerimientoRolViewModel>();
                        modelo.RolesNivelOperativo.ListaRolesRequeridos = new List<RequerimientoRolViewModel>();

                        // Obtención de los datos actuales por dependencia

                        var grupoOcupacionalSuperior = await db.GrupoOcupacional.Where(x => x.TipoEscala == ConstantesGrupoOcupacional.GrupoOcupacionalNivelSuperior).FirstOrDefaultAsync();

                        var grupoOcupacionalOperativo = await db.GrupoOcupacional.Where(x => x.TipoEscala == ConstantesGrupoOcupacional.GrupoOcupacionalNivelOperativo).FirstOrDefaultAsync();

                        modelo.IdDependencia = requerimientoRolPorDependenciaViewModel.IdDependencia;
                        modelo.NombreDependencia = dependencia.Nombre;

                        modelo.RolesNivelJerarquicoSuperior = new RequerimientoRolPorGrupoOcupacionalViewModel
                        {
                            IdGrupoOcupacional = grupoOcupacionalSuperior.IdGrupoOcupacional,
                            NombreGrupoOcupacional = grupoOcupacionalSuperior.TipoEscala,

                            ListaRolesRequeridos = await db.SituacionPropuesta
                            .Where(x =>
                                x.IdGrupoOcupacional == grupoOcupacionalSuperior.IdGrupoOcupacional
                                && x.IdDependencia == dependencia.IdDependencia
                            )
                            .Select(y => new RequerimientoRolViewModel
                            {
                                IdRolPuesto = y.IdRolPuesto,
                                NombreRolPuesto = y.RolPuesto.Nombre,
                                Cantidad = y.Cantidad,
                                Descripcion = y.Descripcion
                            }
                            )
                            .ToListAsync(),

                        };

                        modelo.RolesNivelOperativo = new RequerimientoRolPorGrupoOcupacionalViewModel
                        {
                            IdGrupoOcupacional = grupoOcupacionalOperativo.IdGrupoOcupacional,
                            NombreGrupoOcupacional = grupoOcupacionalOperativo.TipoEscala,

                            ListaRolesRequeridos = await db.SituacionPropuesta
                            .Where(x =>
                                x.IdGrupoOcupacional == grupoOcupacionalOperativo.IdGrupoOcupacional
                                && x.IdDependencia == dependencia.IdDependencia
                            )
                            .Select(y => new RequerimientoRolViewModel
                            {
                                IdRolPuesto = y.IdRolPuesto,
                                NombreRolPuesto = y.RolPuesto.Nombre,
                                Cantidad = y.Cantidad,
                                Descripcion = y.Descripcion
                            }
                            )
                            .ToListAsync()
                        };

                        return new Response
                        {
                            IsSuccess = true,
                            Resultado = modelo
                        };

                    }
                }

                return new Response
                {
                    IsSuccess = true,
                    Resultado = Mensaje.RegistroNoEncontrado
                };

            } catch (Exception ex) {

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion

                };
            }

        }




        #endregion





        /*
        
        


        


        
        public async Task<Dependencia> ObtenerDependenciaActualPorIdEmpleado(int idEmpleado)
        {

            Dependencia dependencia = new Dependencia();

            try
            {

                var empleadoActualIOMP = await db.IndiceOcupacionalModalidadPartida
                    .Include(i => i.Empleado)
                    .Include(i => i.Empleado.Dependencia)
                    .Include(i => i.Empleado.Persona)
                    .Include(i => i.IndiceOcupacional)
                    .Include(i => i.IndiceOcupacional.ManualPuesto)
                    .Where(w =>
                        w.Empleado.IdEmpleado == idEmpleado
                        && w.Empleado.Activo == true
                        && w.Empleado.EsJefe == true
                    )
                    .OrderByDescending(o => o.IdIndiceOcupacionalModalidadPartida)
                    .FirstOrDefaultAsync();

                 
                //Obtiene el último movimiento temporal del empleado logueado que esté vigente en esta fecha
                //y asi saber si subroga un cargo o está encargado del puesto
                
                var empleadoPuestoEncargado = await db.EmpleadoMovimiento
                    .Include(i => i.IndiceOcupacional)
                    .Include(i => i.IndiceOcupacional.Dependencia)
                    .Include(i => i.AccionPersonal)
                    .Include(i => i.AccionPersonal.TipoAccionPersonal)
                    .Include(i => i.Empleado)
                    .Include(i => i.Empleado.Dependencia)
                    .Include(i => i.Empleado.Persona)
                    .Where(w =>
                        w.AccionPersonal.Ejecutado == true
                        && w.FechaDesde <= DateTime.Now
                        && (
                            (w.FechaHasta != null && w.FechaHasta >=
                                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
                            )
                            || w.FechaHasta == null
                        )
                        && w.IdEmpleado == idEmpleado
                        && w.Empleado.Activo == true
                    )
                    .OrderByDescending(o => o.IdEmpleadoMovimiento)
                    .FirstOrDefaultAsync();


                // Este será el indice ocupacional actual del empleado logueado
                // tomará los valores del puesto al que está subrogando o encargado si fuera el caso
                var IndiceOcupacionalUsuarioActual = new IndiceOcupacional();
                var empleadoJefe = false;



                if (empleadoPuestoEncargado != null)
                {

                    if (
                        empleadoPuestoEncargado.AccionPersonal.TipoAccionPersonal.Nombre.ToString().ToUpper()
                        == ConstantesAccionPersonal.Encargo.ToString().ToUpper()
                        )
                    {
                        var finEncargo = await db.AccionPersonal
                            .Where(w =>
                                w.Ejecutado == true
                                && w.TipoAccionPersonal.Nombre.ToString().ToUpper()
                                    == ConstantesAccionPersonal.TerminacionEncargo.ToString().ToUpper()
                                && new DateTime(w.FechaRige.Year, w.FechaRige.Month, w.FechaRige.Day)
                                    == new DateTime(
                                        empleadoPuestoEncargado.FechaDesde.Year,
                                        empleadoPuestoEncargado.FechaDesde.Month,
                                        empleadoPuestoEncargado.FechaDesde.Day
                                        )
                            ).FirstOrDefaultAsync();

                        if (finEncargo != null)
                        {

                            IndiceOcupacionalUsuarioActual = empleadoActualIOMP.IndiceOcupacional;
                            empleadoJefe = empleadoActualIOMP.Empleado.EsJefe;
                            dependencia = empleadoActualIOMP.IndiceOcupacional.Dependencia;
                        }
                        else
                        {


                            IndiceOcupacionalUsuarioActual = empleadoPuestoEncargado.IndiceOcupacional;
                            empleadoJefe = empleadoPuestoEncargado.EsJefe;
                            dependencia = empleadoPuestoEncargado.IndiceOcupacional.Dependencia;

                        }

                    }

                    else if (
                        empleadoPuestoEncargado.AccionPersonal.TipoAccionPersonal.Nombre.ToString().ToUpper()
                        == ConstantesAccionPersonal.Subrogacion.ToString().ToUpper()
                        )
                    {
                        var finEncargo = await db.AccionPersonal
                            .Where(w =>
                                w.Ejecutado == true
                                && w.TipoAccionPersonal.Nombre.ToString().ToUpper()
                                    == ConstantesAccionPersonal.TerminacionSubrogacion.ToString().ToUpper()
                                && new DateTime(w.FechaRige.Year, w.FechaRige.Month, w.FechaRige.Day)
                                    == new DateTime(
                                        empleadoPuestoEncargado.FechaDesde.Year,
                                        empleadoPuestoEncargado.FechaDesde.Month,
                                        empleadoPuestoEncargado.FechaDesde.Day
                                        )

                            ).FirstOrDefaultAsync();

                        if (finEncargo != null)
                        {

                            IndiceOcupacionalUsuarioActual = empleadoActualIOMP.IndiceOcupacional;
                            empleadoJefe = empleadoActualIOMP.Empleado.EsJefe;
                            dependencia = empleadoActualIOMP.IndiceOcupacional.Dependencia;
                        }
                        else
                        {


                            IndiceOcupacionalUsuarioActual = empleadoPuestoEncargado.IndiceOcupacional;
                            empleadoJefe = empleadoPuestoEncargado.EsJefe;
                            dependencia = empleadoPuestoEncargado.IndiceOcupacional.Dependencia;

                        }

                    }
                    else
                    {
                        IndiceOcupacionalUsuarioActual = empleadoPuestoEncargado.IndiceOcupacional;
                        empleadoJefe = empleadoPuestoEncargado.EsJefe;
                        dependencia = empleadoPuestoEncargado.IndiceOcupacional.Dependencia;
                    }

                }
                else
                {
                    IndiceOcupacionalUsuarioActual = empleadoActualIOMP.IndiceOcupacional;
                    empleadoJefe = empleadoActualIOMP.Empleado.EsJefe;
                    dependencia = empleadoActualIOMP.IndiceOcupacional.Dependencia;
                }



                return dependencia;
            }
            catch (Exception ex)
            {

                return dependencia;
            }

        }



        


        
        */
    }
}