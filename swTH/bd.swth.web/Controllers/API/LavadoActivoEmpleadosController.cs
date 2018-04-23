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
    [Route("api/LavadoActivoEmpleados")]
    public class LavadoActivoEmpleadosController : Controller
    {
        private readonly SwTHDbContext db;

        public LavadoActivoEmpleadosController(SwTHDbContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Necesita NombreUsuario, devuelve datos para crear el formulario
        /// </summary>
        /// <param name="lavadoActivoEmpleadoViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CrearLavadoActivoEmpleado")]
        public async Task<Response> CrearLavadoActivoEmpleado ([FromBody] LavadoActivoEmpleadoViewModel lavadoActivoEmpleadoViewModel)
        {

            try
            {
                var empleado = await db.Empleado.Include(a=>a.Persona).Where(x => x.NombreUsuario == lavadoActivoEmpleadoViewModel.NombreUsuario).ToListAsync();

                if (empleado.Count > 0) {

                    lavadoActivoEmpleadoViewModel.DatosBasicosEmpleadoViewModel = new
                    DatosBasicosEmpleadoViewModel {
                        IdEmpleado = empleado.FirstOrDefault().IdEmpleado,

                        Nombres = empleado.FirstOrDefault().Persona.Nombres +" "+
                        empleado.FirstOrDefault().Persona.Apellidos
                        
                    };

                    var query1 = db.LavadoActivoEmpleado;


                    lavadoActivoEmpleadoViewModel.ListaLavadoActivoItemViewModel = await db.LavadoActivoItem
                    .Select(x => new LavadoActivoItemViewModel
                    {
                        IdLavadoActivoItem = x.IdLavadoActivoItem,
                        Descripcion = x.Descripcion,
                        Valor = (query1.Where(la =>
                                    la.IdEmpleado == lavadoActivoEmpleadoViewModel.DatosBasicosEmpleadoViewModel.IdEmpleado &&
                                    la.IdLavadoActivoItem == x.IdLavadoActivoItem
                                )
                            .FirstOrDefault() != null
                            ) ? query1.Where(la =>
                                      la.IdEmpleado == lavadoActivoEmpleadoViewModel.DatosBasicosEmpleadoViewModel.IdEmpleado &&
                                      la.IdLavadoActivoItem == x.IdLavadoActivoItem
                                )
                            .FirstOrDefault().Valor: false,
                    }
                    )
                    .ToListAsync();

                    return new Response
                    {
                        IsSuccess = true,
                        Resultado = lavadoActivoEmpleadoViewModel
                    };
                }

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.AccesoNoAutorizado
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


        [HttpPost]
        [Route("InsertarLavadoActivosEmpleados")]
        public async Task<Response> InsertarLavadoActivosEmpleados([FromBody] LavadoActivoEmpleadoViewModel lavadoActivoEmpleadoViewModel) {

            try
            {
                foreach (var item in lavadoActivoEmpleadoViewModel.ListaLavadoActivoItemViewModel) {

                    var registro = new LavadoActivoEmpleado
                    {
                        IdLavadoActivoItem = item.IdLavadoActivoItem,
                        IdEmpleado = lavadoActivoEmpleadoViewModel.DatosBasicosEmpleadoViewModel.IdEmpleado,
                        Valor = item.Valor,
                        Fecha = DateTime.Now
                         
                    };

                    Response existe = await Existe(registro);

                    if (!existe.IsSuccess && existe.Message != null)
                    {
                        db.LavadoActivoEmpleado.Add(registro);
                    }
                    else
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = Mensaje.ErrorEditarDatos,

                        };
                    }
                    
                }

                
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.GuardadoSatisfactorio,
                    
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

        public async Task<Response> Existe(LavadoActivoEmpleado lavadoActivoEmpleado) {

            try
            {
                var lista = await db.LavadoActivoEmpleado
                    .Where(w=>
                        w.IdLavadoActivoItem == lavadoActivoEmpleado.IdLavadoActivoItem &&
                        w.IdEmpleado == lavadoActivoEmpleado.IdEmpleado
                     ).ToListAsync();

                if (lista != null && lista.Count > 0) {

                    return new Response
                    {
                        IsSuccess = true
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
                    Message = Mensaje.Excepcion
                };
            }
        }



        /// <summary>
        /// Solo requiere el IdEmpleado
        /// </summary>
        /// <param name="lavadoActivoEmpleadoViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ExisteLavadoActivosPorIdEmpleado")]
        public async Task<Response> ExisteLavadoActivosPorIdEmpleado([FromBody] LavadoActivoEmpleadoViewModel lavadoActivoEmpleadoViewModel)
        {

            try
            {
                var lista = await db.LavadoActivoEmpleado
                    .Where(w =>
                        w.IdEmpleado == lavadoActivoEmpleadoViewModel.DatosBasicosEmpleadoViewModel.IdEmpleado
                     ).ToListAsync();

                if (lista != null && lista.Count > 0)
                {

                    return new Response
                    {
                        IsSuccess = true
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

                };
            }

        }



    }
}