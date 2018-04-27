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
using bd.swth.entidades.Constantes;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/ActividadesGestionCambio")]
    public class ActividadesGestionCambioController : Controller
    {
        private readonly SwTHDbContext db;

        public ActividadesGestionCambioController(SwTHDbContext db)
        {
            this.db = db;
        }

        // Post: api/ActividadesGestionCambio
        [HttpPost]
        [Route("ListarActividadesGestionCambio")]
        public async Task<List<ActividadesGestionCambioViewModel>> ListarActividadesGestionCambio([FromBody] ActividadesGestionCambioViewModel actividadesGestionCambioViewModel)
        {
            
            try
            {

                var empleado = db.Empleado.Include(d=>d.Dependencia)
                    .Where(x=>x.NombreUsuario == actividadesGestionCambioViewModel.NombreUsuario)
                    .FirstOrDefault()
                ;
                
                
                
                return await db.ActividadesGestionCambio.Include(d=>d.Dependencia).Include(e=>e.Empleado).ThenInclude(p=>p.Persona)
                    .Where(w=>w.Dependencia.IdSucursal == empleado.Dependencia.IdSucursal)
                    .Select(x=>new ActividadesGestionCambioViewModel
                        {   
                            IdActividadesGestionCambio = x.IdActividadesGestionCambio,

                            IdDependencia = x.IdDependencia,
                            NombreDependencia = x.Dependencia.Nombre,

                            IdEmpleado = x.IdEmpleado,
                            NombreEmpleado = x.Empleado.Persona.Nombres+ " "+x.Empleado.Persona.Apellidos,

                            Avance = x.Avance,
                            FechaInicio = x.FechaInicio,
                            FechaFin = x.FechaFin,
                            Observaciones = x.Observaciones,
                            Tarea = x.Tarea,
                            
                            ValorEstado = x.EstadoActividadesGestionCambio,
                            Estado = Constantes.ListaEstadosGestionCambio.Where(a=>a.Valor == x.EstadoActividadesGestionCambio).FirstOrDefault().Nombre
                    }
                    )
                    .ToListAsync();
                    
            }
            catch (Exception ex)
            {
                return new List<ActividadesGestionCambioViewModel>();
            }
        }



        // POST: api/ActividadesGestionCambio
        [HttpPost]
        [Route("CrearActividadesGestionCambio")]
        public async Task<CrearActividadesGestionCambioViewModel> CrearActividadesGestionCambio([FromBody] ActividadesGestionCambioViewModel actividadesGestionCambioViewModel)
        {
            var modelo = new CrearActividadesGestionCambioViewModel();

            modelo.actividadesGestionCambioViewModel = new ActividadesGestionCambioViewModel {
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now,
                Observaciones = ""
            };

            modelo.ListaDatosBasicosEmpleadoViewModel = new List<DatosBasicosEmpleadoViewModel>();
            modelo.ListaEstadoActividadGestionCambioViewModel = new List<EstadoActividadGestionCambioViewModel>();
            modelo.ListaDependenciasViewModel = new List<DependenciaViewModel>();

            try
            {
                var empleado = db.Empleado.Include(d => d.Dependencia)
                    .Where(x => x.NombreUsuario == actividadesGestionCambioViewModel.NombreUsuario)
                    .FirstOrDefault()
                ;
                

                // Obtención de estados desde las constantes
                modelo.ListaEstadoActividadGestionCambioViewModel = Constantes.ListaEstadosGestionCambio;

                // Obtención de los empleados por sucursal
                modelo.ListaDatosBasicosEmpleadoViewModel = await db.Empleado
                    .Where(w=>
                        w.Dependencia.IdSucursal == empleado.Dependencia.IdSucursal
                    )
                    .Select(s=> new DatosBasicosEmpleadoViewModel
                        {
                            IdEmpleado = s.IdEmpleado,
                            Nombres = s.Persona.Nombres + " "+ s.Persona.Apellidos
                        }                    
                    )
                    .ToListAsync();

                // Obtención de dependencias por sucursal
                modelo.ListaDependenciasViewModel = await db.Dependencia
                    .Where(w=>
                        w.IdSucursal == empleado.Dependencia.IdSucursal
                    )
                    .Select(s=> new DependenciaViewModel
                        {
                            IdDependencia = s.IdDependencia,
                            NombreDependencia = s.Nombre,
                            IdSucursal = s.IdSucursal
                        }
                    )
                    .ToListAsync();


                return modelo;
            }
            catch (Exception)
            {
                return modelo;
            }
        }


        // POST: api/ActividadesGestionCambio
        [HttpPost]
        [Route("InsertarActividadesGestionCambio")]
        public async Task<Response> InsertarActividadesGestionCambio([FromBody] ActividadesGestionCambioViewModel actividadesGestionCambioViewModel)
        {
            try {

                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ModeloInvalido,
                    };
                }

                var modelo = new ActividadesGestionCambio{
                        IdDependencia = actividadesGestionCambioViewModel.IdDependencia,
                        IdEmpleado = actividadesGestionCambioViewModel.IdEmpleado,

                        Avance = actividadesGestionCambioViewModel.Avance,
                        EstadoActividadesGestionCambio = actividadesGestionCambioViewModel.ValorEstado,
                        Tarea = actividadesGestionCambioViewModel.Tarea,
                        FechaInicio = actividadesGestionCambioViewModel.FechaInicio,
                        FechaFin = actividadesGestionCambioViewModel.FechaFin,
                        Observaciones = (String.IsNullOrEmpty(actividadesGestionCambioViewModel.Observaciones)) ? "" : actividadesGestionCambioViewModel.Observaciones
                };

                

                if (Existe(modelo).Result.IsSuccess) {

                    return new Response {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro
                    };
                }

                db.ActividadesGestionCambio.Add(modelo);
                await db.SaveChangesAsync();


                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.GuardadoSatisfactorio
                };


            }
            catch (Exception ex)
            {
                return new Response {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };
            }
        }



        /// <summary>
        /// se necesita el IdActividadGestionCambio y el nombreUsuario
        /// </summary>
        /// <param name="actividadesGestionCambioViewModel"></param>
        /// <returns></returns>
        // Post: api/ActividadesGestionCambio
        [HttpPost]
        [Route("ObtenerActividadesGestionCambioPorId")]
        public async Task<CrearActividadesGestionCambioViewModel> ObtenerActividadesGestionCambioPorId([FromBody] ActividadesGestionCambioViewModel actividadesGestionCambioViewModel)
        {

            var modelo = new CrearActividadesGestionCambioViewModel();

            modelo.actividadesGestionCambioViewModel = new ActividadesGestionCambioViewModel
            {
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now
            };

            modelo.ListaDatosBasicosEmpleadoViewModel = new List<DatosBasicosEmpleadoViewModel>();
            modelo.ListaEstadoActividadGestionCambioViewModel = new List<EstadoActividadGestionCambioViewModel>();
            modelo.ListaDependenciasViewModel = new List<DependenciaViewModel>();

            try
            {
                var empleado = db.Empleado.Include(d => d.Dependencia)
                    .Where(x => x.NombreUsuario == actividadesGestionCambioViewModel.NombreUsuario)
                    .FirstOrDefault()
                ;

                var idSucursal = empleado.Dependencia.IdSucursal;

                // Obtención de estados desde las constantes
                modelo.ListaEstadoActividadGestionCambioViewModel = Constantes.ListaEstadosGestionCambio;

                // Obtención de los empleados por sucursal
                modelo.ListaDatosBasicosEmpleadoViewModel = await db.Empleado
                    .Where(w =>
                        w.Dependencia.IdSucursal == idSucursal
                    )
                    .Select(s => new DatosBasicosEmpleadoViewModel
                    {
                        IdEmpleado = s.IdEmpleado,
                        Nombres = s.Persona.Nombres + " " + s.Persona.Apellidos
                    }
                    )
                    .ToListAsync();

                // Obtención de dependencias por sucursal
                modelo.ListaDependenciasViewModel = await db.Dependencia
                    .Where(w =>
                        w.IdSucursal == empleado.Dependencia.IdSucursal
                    )
                    .Select(s => new DependenciaViewModel
                    {
                        IdDependencia = s.IdDependencia,
                        NombreDependencia = s.Nombre,
                        IdSucursal = s.IdSucursal
                    }
                    )
                    .ToListAsync();

                //Obtención del registro
                modelo.actividadesGestionCambioViewModel = await db.ActividadesGestionCambio.Include(d => d.Dependencia).Include(e => e.Empleado).ThenInclude(p => p.Persona)
                    .Where(w => 
                    w.IdActividadesGestionCambio == actividadesGestionCambioViewModel.IdActividadesGestionCambio
                    )
                    .Select(x => new ActividadesGestionCambioViewModel
                    {
                        IdActividadesGestionCambio = x.IdActividadesGestionCambio,

                        IdDependencia = x.IdDependencia,
                        NombreDependencia = x.Dependencia.Nombre,

                        IdEmpleado = x.IdEmpleado,
                        NombreEmpleado = x.Empleado.Persona.Nombres + " " + x.Empleado.Persona.Apellidos,

                        Avance = x.Avance,
                        FechaInicio = x.FechaInicio,
                        FechaFin = x.FechaFin,
                        Observaciones = x.Observaciones,
                        Tarea = x.Tarea,

                        ValorEstado = x.EstadoActividadesGestionCambio,
                        Estado = Constantes.ListaEstadosGestionCambio.Where(a => a.Valor == x.EstadoActividadesGestionCambio).FirstOrDefault().Nombre
                    }
                    )
                    .FirstOrDefaultAsync();

                return modelo;
            }
            catch (Exception ex)
            {
                return modelo;
            }
        }

        
        
        /// <summary>
        /// Necesita: NombreUsuario e IdDependencia
        /// </summary>
        /// <param name="actividadesGestionCambioViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ObtenerEmpleadosPorSucursalYDependencia")]
        public async Task<List<DatosBasicosEmpleadoViewModel>> ObtenerEmpleadosPorSucursalYDependencia([FromBody] ActividadesGestionCambioViewModel actividadesGestionCambioViewModel)
        {
            try
            {
                var empleado = db.Empleado.Include(d => d.Dependencia)
                    .Where(x => x.NombreUsuario == actividadesGestionCambioViewModel.NombreUsuario)
                    .FirstOrDefault()
                ;
                
                // Obtención de los empleados por sucursal
                var modelo = await db.Empleado
                    .Where(w =>
                        w.Dependencia.IdSucursal == empleado.Dependencia.IdSucursal
                        && w.IdDependencia == actividadesGestionCambioViewModel.IdDependencia
                    )
                    .Select(s => new DatosBasicosEmpleadoViewModel
                    {
                        IdEmpleado = s.IdEmpleado,
                        Nombres = s.Persona.Nombres + " " + s.Persona.Apellidos
                    }
                    )
                    .ToListAsync();

               
                return modelo;
            }
            catch (Exception ex)
            {
                return new List<DatosBasicosEmpleadoViewModel>();
            }
        }
        

        // POST: api/ActividadesGestionCambio
        [HttpPost]
        [Route("EditarActividadesGestionCambio")]
        public async Task<Response> EditarActividadesGestionCambio([FromBody] ActividadesGestionCambioViewModel actividadesGestionCambioViewModel)
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

                var modelo = new ActividadesGestionCambio
                {
                    IdActividadesGestionCambio = actividadesGestionCambioViewModel.IdActividadesGestionCambio,
                    IdDependencia = actividadesGestionCambioViewModel.IdDependencia,
                    IdEmpleado = actividadesGestionCambioViewModel.IdEmpleado,

                    Avance = actividadesGestionCambioViewModel.Avance,
                    EstadoActividadesGestionCambio = actividadesGestionCambioViewModel.ValorEstado,
                    Tarea = actividadesGestionCambioViewModel.Tarea,
                    FechaInicio = actividadesGestionCambioViewModel.FechaInicio,
                    FechaFin = actividadesGestionCambioViewModel.FechaFin,
                    Observaciones = actividadesGestionCambioViewModel.Observaciones
                };



                if (Existe(modelo).Result.IsSuccess)
                {

                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro
                    };
                }

                var actualizar = await db.ActividadesGestionCambio
                    .Where(x=>
                        x.IdActividadesGestionCambio == actividadesGestionCambioViewModel.IdActividadesGestionCambio
                    ).FirstOrDefaultAsync();

                actualizar.IdDependencia = actividadesGestionCambioViewModel.IdDependencia;
                actualizar.IdEmpleado = actividadesGestionCambioViewModel.IdEmpleado;
                actualizar.Avance = actividadesGestionCambioViewModel.Avance;
                actualizar.EstadoActividadesGestionCambio = actividadesGestionCambioViewModel.ValorEstado;
                actualizar.Tarea = actividadesGestionCambioViewModel.Tarea;
                actualizar.FechaInicio = actividadesGestionCambioViewModel.FechaInicio;
                actualizar.FechaFin = actividadesGestionCambioViewModel.FechaFin;
                actualizar.Observaciones = (String.IsNullOrEmpty(actividadesGestionCambioViewModel.Observaciones))?"": actividadesGestionCambioViewModel.Observaciones;
                

                db.ActividadesGestionCambio.Update(actualizar);
                await db.SaveChangesAsync();


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

        private async Task<Response> Existe( ActividadesGestionCambio modelo) {

            try
            {
                var lista = await db.ActividadesGestionCambio
                    .Where(x=>
                        x.Tarea.ToUpper().TrimEnd().TrimStart() == modelo.Tarea.ToUpper().TrimEnd().TrimStart()
                        //&& x.FechaInicio.ToString("dd-mm-yyyy") == modelo.FechaInicio.ToString("dd-mm-yyyy")
                        //&& x.FechaFin.ToString("dd-mm-yyyy") == modelo.FechaFin.ToString("dd-mm-yyyy")
                        && x.IdDependencia == modelo.IdDependencia
                        && x.IdEmpleado == modelo.IdEmpleado
                    )
                    .ToListAsync();

                if (
                    lista != null 
                    && lista.Count > 0 
                    && modelo.IdActividadesGestionCambio != lista.FirstOrDefault().IdActividadesGestionCambio
                    )
                {

                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.ExisteRegistro
                    };
                }

                return new Response
                {
                    IsSuccess = false
                };

            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };
            }

        }
        
    }
}