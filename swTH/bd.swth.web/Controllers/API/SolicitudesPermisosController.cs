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
using System;
using bd.swth.entidades.ViewModels;
using bd.swth.entidades.Constantes;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/SolicitudesPermisos")]
    public class SolicitudesPermisosController : Controller
    {
        private readonly SwTHDbContext db;

        public SolicitudesPermisosController(SwTHDbContext db)
        {
            this.db = db;
        }

        
        // POST: api/SolicitudesPermisos
        /// <summary>
        ///  Devuelve una lista de solicitudesPermiso filtradas por dependencia
        /// </summary>
        /// <param name="dependencia"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ListarSolicitudesPermisosPorDependenciaViewModel")]
        public async Task<List<SolicitudPermisoViewModel>> ListarSolicitudesPermisosPorDependenciaViewModel([FromBody] Dependencia dependencia)
        {
            try
            {
                var lista = await db.SolicitudPermiso.
                    Where(w=>
                        w.Empleado.IdDependencia == dependencia.IdDependencia
                        //&& w.Empleado.EsJefe == false
                    )
                    .Select(s => new SolicitudPermisoViewModel
                    {
                        NombreDependencia = s.Empleado.Dependencia.Nombre,
                        NombresApellidosEmpleado = String.Format("{0} {1}", s.Empleado.Persona.Nombres, s.Empleado.Persona.Apellidos),

                        SolicitudPermiso = new SolicitudPermiso
                        {
                            FechaAprobado = s.FechaAprobado,
                            FechaDesde = s.FechaDesde,
                            FechaHasta = s.FechaHasta,
                            FechaSolicitud = s.FechaSolicitud,
                            HoraDesde = s.HoraDesde,
                            HoraHasta = s.HoraHasta,
                            IdEmpleado = s.IdEmpleado,
                            IdSolicitudPermiso = s.IdSolicitudPermiso,
                            IdTipoPermiso = s.IdTipoPermiso,
                            Estado = s.Estado,
                            Motivo = s.Motivo,
                            Observacion = s.Observacion,
                        },

                        NombreTipoPermiso = s.TipoPermiso.Nombre,

                        NombreEstadoAprobacion = !String.IsNullOrEmpty(ConstantesEstadosAprobacionMovimientoInterno
                                      .ListaEstadosAprobacionMovimientoInterno.Where(cw => cw.ValorEstado == s.Estado)
                                      .FirstOrDefault().NombreEstado) ? ConstantesEstadosAprobacionMovimientoInterno
                                      .ListaEstadosAprobacionMovimientoInterno.Where(cw => cw.ValorEstado == s.Estado)
                                      .FirstOrDefault().NombreEstado : Mensaje.ErrorValorEstadoMovimientoInterno

                    }
               )
               .ToListAsync();

                return lista;
            }
            catch (Exception ex)
            {
                return new List<SolicitudPermisoViewModel>();
            }
        }
      
        [HttpPost]
        [Route("ListarInformacionEmpleado")]
        public async Task<List<SolicitudPermiso>> ListarInformacionEmpleado([FromBody] SolicitudPermiso solicitudPermiso)
        {
            try
            {

                return await db.SolicitudPermiso
                                  .Include(x => x.Empleado)
                                  .ThenInclude(x => x.Persona)
                                  .Include(x => x.TipoPermiso)
                                  .Where(x => (x.IdEmpleado == solicitudPermiso.IdEmpleado) && (x.Estado == 0 || x.Estado == -1))
                                  .ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<SolicitudPermiso>();
            }
        }

        /// <summary>
        /// Obtiene el viewModel del registro de solicitudPermiso a partir de su ID
        /// </summary>
        /// <param name="idSolicitudPermiso"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ObtenerInformacionSolicitudPermiso")]
        public async Task<SolicitudPermisoViewModel> ObtenerInformacionSolicitudPermiso([FromBody] int idSolicitudPermiso)
        {
            try
            {

                SolicitudPermiso solicitudPermiso = await db.SolicitudPermiso
                                  .Include(x => x.Empleado)
                                  .ThenInclude(x => x.Persona)
                                  .Include(x => x.TipoPermiso)
                                  .Where(x => x.IdSolicitudPermiso == idSolicitudPermiso)
                                  .SingleOrDefaultAsync();
                
                Empleado empleado = await db.Empleado
                    .Include(x => x.Persona)
                    .Include(x => x.Dependencia)
                    .SingleOrDefaultAsync(m => m.IdEmpleado == solicitudPermiso.Empleado.IdEmpleado);

                var solicitudPermisoViewModel = new SolicitudPermisoViewModel
                {
                    NombresApellidosEmpleado = empleado.Persona.Nombres + " " + empleado.Persona.Apellidos,
                    NombreDependencia = empleado.Dependencia.Nombre,
                    SolicitudPermiso = solicitudPermiso
                };

                return solicitudPermisoViewModel;

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// Obtiene el viewModel del registro de solicitudPermiso a partir del idEmpleado 
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ListarSolicitudesPermisosEmpleado")]
        public async Task<List<SolicitudPermisoViewModel>> ListarSolicitudesPermisosEmpleado([FromBody] Empleado empleado)
        {
            try
            {

                var lista =  await db.SolicitudPermiso
                                  .Where(x => x.IdEmpleado == empleado.IdEmpleado)
                                  .Select(s=>new SolicitudPermisoViewModel
                                    {
                                      NombreDependencia = s.Empleado.Dependencia.Nombre,
                                      NombresApellidosEmpleado = String.Format("{0} {1}",s.Empleado.Persona.Nombres,s.Empleado.Persona.Apellidos),

                                      SolicitudPermiso = new SolicitudPermiso {
                                        FechaAprobado = s.FechaAprobado,
                                        FechaDesde = s.FechaDesde,
                                        FechaHasta = s.FechaHasta,
                                        FechaSolicitud =s.FechaSolicitud,
                                        HoraDesde = s.HoraDesde,
                                        HoraHasta = s.HoraHasta,
                                        IdEmpleado = s.IdEmpleado,
                                        IdSolicitudPermiso = s.IdSolicitudPermiso,
                                        IdTipoPermiso = s.IdTipoPermiso,
                                        Estado = s.Estado,
                                        Motivo = s.Motivo,
                                        Observacion = s.Observacion,
                                      },

                                      NombreTipoPermiso = s.TipoPermiso.Nombre,

                                      NombreEstadoAprobacion = !String.IsNullOrEmpty(ConstantesEstadosAprobacionMovimientoInterno
                                      .ListaEstadosAprobacionMovimientoInterno.Where(cw=>cw.ValorEstado == s.Estado)
                                      .FirstOrDefault().NombreEstado)? ConstantesEstadosAprobacionMovimientoInterno
                                      .ListaEstadosAprobacionMovimientoInterno.Where(cw => cw.ValorEstado == s.Estado)
                                      .FirstOrDefault().NombreEstado:Mensaje.ErrorValorEstadoMovimientoInterno 

                                  }
                                  )
                                  .ToListAsync();

                return lista;
            }
            catch (Exception ex)
            {
                return new List<SolicitudPermisoViewModel>();
            }
        }


        [HttpPost]
        [Route("ListarSolicitudesPermisosPendientesJefe")]
        public async Task<List<SolicitudPermiso>> ListarSolicitudesPermisosPendientesJefe([FromBody] Empleado empleado)
        {
            try
            {

                return await db.SolicitudPermiso
                                  .Include(x => x.Empleado)
                                  .ThenInclude(x => x.Persona)
                                  .Include(x => x.TipoPermiso)
                                  .Where(x => x.IdEmpleado == empleado.IdEmpleado && x.Estado == 0 || x.Estado == -1)
                                  .Distinct()
                                  .ToListAsync();
            }
            catch (Exception ex)
            {
                
                return new List<SolicitudPermiso>();
            }
        }
        

        // GET: api/SolicitudesPermisos/5
        [HttpGet("{id}")]
        public async Task<Response> GetSolicitudPermiso([FromRoute] int id)
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

                var SolicitudPermiso = await db.SolicitudPermiso.SingleOrDefaultAsync(m => m.IdSolicitudPermiso == id);

                if (SolicitudPermiso == null)
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
                    Resultado = SolicitudPermiso,
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


        /// <summary>
        ///  Permite la edición de SolicitudPermiso a partir de su id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="solicitudPermiso"></param>
        /// <returns></returns>
        // PUT: api/SolicitudesPermisos/5
        [HttpPut("{id}")]
        public async Task<Response> PutSolicitudPermiso([FromRoute] int id, [FromBody] SolicitudPermiso solicitudPermiso)
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
                
                var SolicitudPermiso = db.SolicitudPermiso.Find(solicitudPermiso.IdSolicitudPermiso);
                
                
                SolicitudPermiso.HoraDesde = solicitudPermiso.HoraDesde;
                SolicitudPermiso.HoraHasta = solicitudPermiso.HoraHasta;
                SolicitudPermiso.FechaDesde = solicitudPermiso.FechaDesde;
                SolicitudPermiso.FechaHasta = solicitudPermiso.FechaHasta;
                SolicitudPermiso.Motivo = solicitudPermiso.Motivo;
                SolicitudPermiso.IdTipoPermiso = solicitudPermiso.IdTipoPermiso;
                SolicitudPermiso.Estado = solicitudPermiso.Estado;
                SolicitudPermiso.FechaAprobado = DateTime.Now;
                SolicitudPermiso.Observacion = solicitudPermiso.Observacion;
                SolicitudPermiso.CargoVacaciones = solicitudPermiso.CargoVacaciones;

                db.SolicitudPermiso.Update(SolicitudPermiso);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.GuardadoSatisfactorio,
                    Resultado= SolicitudPermiso
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Excepcion,
                };
            }

        }

        // POST: api/SolicitudesPermisos
        [HttpPost]
        [Route("InsertarSolicitudPermiso")]
        public async Task<Response> PostSolicitudPermiso([FromBody] SolicitudPermiso SolicitudPermiso)
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

                var respuesta = Existe(SolicitudPermiso);
                if (!respuesta.IsSuccess)
                {
                    db.SolicitudPermiso.Add(SolicitudPermiso);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.GuardadoSatisfactorio
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
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }


        // DELETE: api/SolicitudesPermisos/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteSolicitudPermiso([FromRoute] int id)
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

                var respuesta = await db.SolicitudPermiso.SingleOrDefaultAsync(m => m.IdSolicitudPermiso == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.SolicitudPermiso.Remove(respuesta);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
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


        private Response Existe(SolicitudPermiso SolicitudPermiso)
        {
            var fechaDesde = SolicitudPermiso.FechaDesde;
            var fechaHasta = SolicitudPermiso.FechaHasta;
            var horaDesde = SolicitudPermiso.HoraDesde;
            var horaHasta = SolicitudPermiso.HoraHasta;

            var SolicitudPermisorespuesta = db.SolicitudPermiso.Where(p => p.FechaDesde == fechaDesde  && p.FechaHasta == fechaHasta && p.HoraDesde == horaDesde && p.HoraHasta == horaHasta && p.IdEmpleado == SolicitudPermiso.IdEmpleado && p.IdTipoPermiso==SolicitudPermiso.IdTipoPermiso).FirstOrDefault();

            if (SolicitudPermisorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = SolicitudPermisorespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = SolicitudPermisorespuesta,
            };
        }


        // GET: api/SolicitudesPermisos
        [HttpGet]
        [Route("ListarEstadosAprobacionEmpleado")]
        public async Task<List<AprobacionMovimientoInternoViewModel>> ListarEstadosAprobacionEmpleado()
        {

            try
            {
                var lista = ConstantesEstadosAprobacionMovimientoInterno.ListaEstadosAprobacionMovimientoInterno.Where(w => w.GrupoAprobacion == 2).ToList();

                return lista;

            }
            catch (Exception)
            {
                return new List<AprobacionMovimientoInternoViewModel>();
            }
        }


        // POST: api/AccionesPersonal
        [HttpPost]
        [Route("EditarEstadoSolicitudPermiso")]
        public async Task<Response> EditarEstadoSolicitudPermiso([FromBody] SolicitudPermisoViewModel solicitudPermisoViewModel)
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

                var SolicitudPermiso = await db.SolicitudPermiso.FindAsync(solicitudPermisoViewModel.SolicitudPermiso.IdSolicitudPermiso);
                
                SolicitudPermiso.Estado = solicitudPermisoViewModel.SolicitudPermiso.Estado;
                db.SolicitudPermiso.Update(SolicitudPermiso);
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
                    Message = Mensaje.Excepcion,
                };
            }
        }


        // POST: api/SolicitudesPermisos
        /// <summary>
        ///  Devuelve una lista de solicitudesPermiso, se filtra por sucursal pero recibe la dependencia del usuario de TTHH
        /// </summary>
        /// <param name="dependencia"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ListarSolicitudesPermisosTTHHViewModel")]
        public async Task<List<SolicitudPermisoViewModel>> ListarSolicitudesPermisosTTHHViewModel([FromBody] Dependencia dependencia)
        {
            try
            {
                var idSucursal = db.Dependencia.Where(w => w.IdDependencia == dependencia.IdDependencia).FirstOrDefault().IdSucursal;

                var lista = await db.SolicitudPermiso.
                    Where(w =>
                        w.Empleado.Dependencia.Sucursal.IdSucursal == idSucursal
                    )
                    .Select(s => new SolicitudPermisoViewModel
                    {
                        NombreDependencia = s.Empleado.Dependencia.Nombre,
                        NombresApellidosEmpleado = String.Format("{0} {1}", s.Empleado.Persona.Nombres, s.Empleado.Persona.Apellidos),

                        SolicitudPermiso = new SolicitudPermiso
                        {
                            FechaAprobado = s.FechaAprobado,
                            FechaDesde = s.FechaDesde,
                            FechaHasta = s.FechaHasta,
                            FechaSolicitud = s.FechaSolicitud,
                            HoraDesde = s.HoraDesde,
                            HoraHasta = s.HoraHasta,
                            IdEmpleado = s.IdEmpleado,
                            IdSolicitudPermiso = s.IdSolicitudPermiso,
                            IdTipoPermiso = s.IdTipoPermiso,
                            Estado = s.Estado,
                            Motivo = s.Motivo,
                            Observacion = s.Observacion,
                        },

                        NombreTipoPermiso = s.TipoPermiso.Nombre,

                        NombreEstadoAprobacion = !String.IsNullOrEmpty(ConstantesEstadosAprobacionMovimientoInterno
                                      .ListaEstadosAprobacionMovimientoInterno.Where(cw => cw.ValorEstado == s.Estado)
                                      .FirstOrDefault().NombreEstado) ? ConstantesEstadosAprobacionMovimientoInterno
                                      .ListaEstadosAprobacionMovimientoInterno.Where(cw => cw.ValorEstado == s.Estado)
                                      .FirstOrDefault().NombreEstado : Mensaje.ErrorValorEstadoMovimientoInterno

                    }
               )
               .ToListAsync();

                return lista;
            }
            catch (Exception ex)
            {
                return new List<SolicitudPermisoViewModel>();
            }
        }


        /// <summary>
        ///  Devuelve una lista de solicitudesPermiso de los jefes, se filtra por sucursal pero recibe la dependencia del usuario de TTHH
        /// </summary>
        /// <param name="dependencia"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ListarSolicitudesPermisosJefesTTHHViewModel")]
        public async Task<List<SolicitudPermisoViewModel>> ListarSolicitudesPermisosJefesTTHHViewModel([FromBody] Dependencia dependencia)
        {
            try
            {
                var idSucursal = db.Dependencia.Where(w => w.IdDependencia == dependencia.IdDependencia).FirstOrDefault().IdSucursal;

                var lista = await db.SolicitudPermiso.
                    Where(w =>
                        w.Empleado.Dependencia.Sucursal.IdSucursal == idSucursal
                        && w.Empleado.EsJefe == true
                    )
                    .Select(s => new SolicitudPermisoViewModel
                    {
                        NombreDependencia = s.Empleado.Dependencia.Nombre,
                        NombresApellidosEmpleado = String.Format("{0} {1}", s.Empleado.Persona.Nombres, s.Empleado.Persona.Apellidos),

                        SolicitudPermiso = new SolicitudPermiso
                        {
                            FechaAprobado = s.FechaAprobado,
                            FechaDesde = s.FechaDesde,
                            FechaHasta = s.FechaHasta,
                            FechaSolicitud = s.FechaSolicitud,
                            HoraDesde = s.HoraDesde,
                            HoraHasta = s.HoraHasta,
                            IdEmpleado = s.IdEmpleado,
                            IdSolicitudPermiso = s.IdSolicitudPermiso,
                            IdTipoPermiso = s.IdTipoPermiso,
                            Estado = s.Estado,
                            Motivo = s.Motivo,
                            Observacion = s.Observacion,
                        },

                        NombreTipoPermiso = s.TipoPermiso.Nombre,

                        NombreEstadoAprobacion = !String.IsNullOrEmpty(ConstantesEstadosAprobacionMovimientoInterno
                                      .ListaEstadosAprobacionMovimientoInterno.Where(cw => cw.ValorEstado == s.Estado)
                                      .FirstOrDefault().NombreEstado) ? ConstantesEstadosAprobacionMovimientoInterno
                                      .ListaEstadosAprobacionMovimientoInterno.Where(cw => cw.ValorEstado == s.Estado)
                                      .FirstOrDefault().NombreEstado : Mensaje.ErrorValorEstadoMovimientoInterno

                    }
               )
               .ToListAsync();

                return lista;
            }
            catch (Exception ex)
            {
                return new List<SolicitudPermisoViewModel>();
            }
        }


        // POST: api/SolicitudesPermisos
        [HttpPost]
        [Route("BorrarSolicitudPorId")]
        public async Task<Response> BorrarSolicitudPorId([FromBody]int id)
        {
            try
            {
                var modelo = await db.SolicitudPermiso
                    .Where(w => w.IdSolicitudPermiso == id)
                    .FirstOrDefaultAsync();

                if (modelo == null)
                {
                    return new Response {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado
                    };
                }

                db.SolicitudPermiso.Remove(modelo);
                await db.SaveChangesAsync();

                return new Response {
                    IsSuccess = true,
                    Message = Mensaje.BorradoSatisfactorio
                };

            }catch (Exception ex) {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.BorradoNoSatisfactorio
                };
            }
        }

    }
}