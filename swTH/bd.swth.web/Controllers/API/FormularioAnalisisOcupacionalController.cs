using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bd.swth.datos;
using bd.swth.entidades.Enumeradores;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.Utils;
using bd.swth.entidades.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/FormularioAnalisisOcupacional")]
    public class FormularioAnalisisOcupacionalController : Controller
    {
        private readonly SwTHDbContext db;

        public FormularioAnalisisOcupacionalController(SwTHDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        [Route("InsertarFormularioAnalisisOcupacional")]
        public async Task<Response> PostFormularioAnalisisOcupacional([FromBody] FormularioAnalisisOcupacional formularioAnalisisOcupacional)
        {
            try
            {
                int fecha = DateTime.Now.Year;
                if (ModelState.IsValid)
                {
                    var empleado = db.FormularioAnalisisOcupacional.Where(x => x.IdEmpleado == formularioAnalisisOcupacional.IdEmpleado && fecha == formularioAnalisisOcupacional.Anio).FirstOrDefault();

                    if (empleado == null)
                    {
                        db.FormularioAnalisisOcupacional.Add(formularioAnalisisOcupacional);
                        await db.SaveChangesAsync();
                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.Satisfactorio
                        };
                    }                  

                }
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
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
        /// 
        /// </summary>
        /// <param name="documentoFAOViewModel"></param>
        /// genera las lista de la actividades
        /// actualiza la tbala de FormularioIndiceOCupacional
        /// Inserta en la tabla Validacion Jefe Superior
        /// <returns></returns>

        [HttpPost]
        [Route("ActualizarActividades")]
        public async Task<Response> ActualizarActividades([FromBody] DocumentoFAOViewModel documentoFAOViewModel)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    //BUscar el id de empleado logueado
                    var empleado = await db.Empleado.Include(x => x.Persona).Where(x => x.NombreUsuario == documentoFAOViewModel.NombreUsuario).FirstOrDefaultAsync();

                    var activarAnalisiOcupacional = new ActividadesAnalisisOcupacional();
                    foreach (var item in documentoFAOViewModel.ListaActividads)
                    {
                        activarAnalisiOcupacional = db.ActividadesAnalisisOcupacional.Where(x => x.IdActividadesAnalisisOcupacional == Convert.ToInt32(item)).FirstOrDefault();
                        activarAnalisiOcupacional.Cumple = true;
                        db.ActividadesAnalisisOcupacional.Update(activarAnalisiOcupacional);
                    }

                    await db.SaveChangesAsync();


                    //cambia de estado en el formulario en FormularioAnalisis Ocupacional
                    var formularioAnalisisOcupacional = db.FormularioAnalisisOcupacional.Where(x => x.IdFormularioAnalisisOcupacional == activarAnalisiOcupacional.IdFormularioAnalisisOcupacional).FirstOrDefault();
                    formularioAnalisisOcupacional.Estado = 1;
                    db.FormularioAnalisisOcupacional.Update(formularioAnalisisOcupacional);
                    await db.SaveChangesAsync();
                    //Insercion en la tabla validadrimediato superior
                    var validacionSuperior = new ValidacionInmediatoSuperior();
                    validacionSuperior.IdFormularioAnalisisOcupacional = activarAnalisiOcupacional.IdFormularioAnalisisOcupacional;
                    validacionSuperior.IdEmpleado = empleado.IdEmpleado;
                    validacionSuperior.Fecha = DateTime.Now;
                    db.ValidacionInmediatoSuperior.Add(validacionSuperior);
                    await db.SaveChangesAsync();
                    // inserta la exepciones si lo tuviera

                    //var validacionjefesuperior = await db.ValidacionInmediatoSuperior.Where(x=> x.Fecha == DateTime.Now.Date).FirstOrDefaultAsync();


                    foreach (var item in documentoFAOViewModel.ListaExepciones)
                    {
                        if (item != null)
                        {
                            var exepcion = new Exepciones();
                            exepcion.IdValidacionJefe = validacionSuperior.IdValidacionJefe;
                            exepcion.Detalle = item;
                            db.Exepciones.Add(exepcion);
                        }

                    }
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio
                    };
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


        [HttpPost]
        [Route("ActualizarFormularioAnalisisOcupacional")]
        public async Task<Response> ActualizarFormularioAnalisisOcupacional([FromBody] DocumentoFAOViewModel formularioAnalisisOcupacional)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    int fecha = DateTime.Now.Year;
                    if (ModelState.IsValid)
                    {

                        var empleado = db.FormularioAnalisisOcupacional.Where(x => x.IdEmpleado == formularioAnalisisOcupacional.IdEmpleado && x.Anio == fecha).FirstOrDefault();

                        if (empleado != null)
                        {
                            empleado.MisionPuesto = formularioAnalisisOcupacional.Mision;
                            empleado.Estado = 0;
                            empleado.InternoMismoProceso = formularioAnalisisOcupacional.InternoMismoProceso;
                            empleado.InternoOtroProceso = formularioAnalisisOcupacional.InternoOtroProceso;
                            empleado.ExternosCiudadania = formularioAnalisisOcupacional.ExternosCiudadania;
                            empleado.ExtPersJuridicasPubNivelNacional = formularioAnalisisOcupacional.ExtPersJuridicasPubNivelNacional;
                            db.FormularioAnalisisOcupacional.Update(empleado);
                            await db.SaveChangesAsync();

                            foreach (var item in formularioAnalisisOcupacional.ListActividades)
                            {
                                if (item != null)
                                {
                                    var actividad = new ActividadesAnalisisOcupacional();
                                    actividad.Actividades = item;
                                    actividad.IdFormularioAnalisisOcupacional = empleado.IdFormularioAnalisisOcupacional;
                                    db.ActividadesAnalisisOcupacional.Add(actividad);
                                }

                            }
                            await db.SaveChangesAsync();
                            transaction.Commit();
                            return new Response
                            {
                                IsSuccess = true,
                                Message = Mensaje.Satisfactorio
                            };
                        }
                    }
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.Error
                    };

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

        [HttpPost]
        [Route("InsertarAdministracionTalentoHumano")]
        public async Task<Response> InsertarAdministracionTalentoHumano([FromBody] DocumentoFAOViewModel documentoFAOViewModel)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    int fecha = DateTime.Now.Year;
                    if (ModelState.IsValid)
                    {

                        var empleado = await db.Empleado.Include(x => x.Persona).Where(x => x.NombreUsuario == documentoFAOViewModel.NombreUsuario).FirstOrDefaultAsync();
                        var idformualario = db.FormularioAnalisisOcupacional.Where(x => x.IdEmpleado == documentoFAOViewModel.IdEmpleado && x.Anio == fecha).FirstOrDefault();

                        var administradortalento = new AdministracionTalentoHumano();
                        if (empleado != null)
                        { 
                            foreach (var item in documentoFAOViewModel.ListaRolPUesto)
                            {
                                if (item != null)
                                {
                                    administradortalento.IdRolPuesto = Convert.ToInt32(item);
                                    administradortalento.IdFormularioAnalisisOcupacional = idformualario.IdFormularioAnalisisOcupacional;
                                    administradortalento.EmpleadoResponsable = empleado.IdEmpleado;
                                    administradortalento.Fecha = DateTime.Now;
                                    administradortalento.SeAplicaraPolitica = documentoFAOViewModel.Cumple;
                                    administradortalento.Descripcion = documentoFAOViewModel.Descripcionpuesto;
                                    administradortalento.SeAplicaraPolitica = documentoFAOViewModel.aplicapolitica;
                                    db.AdministracionTalentoHumano.Add(administradortalento);
                                }

                            }
                            await db.SaveChangesAsync();

                            //cambia de estado
                            var formularioAnalisisOcupacional = db.FormularioAnalisisOcupacional.Where(x => x.IdFormularioAnalisisOcupacional == idformualario.IdFormularioAnalisisOcupacional).FirstOrDefault();
                            formularioAnalisisOcupacional.Estado = 2;
                            db.FormularioAnalisisOcupacional.Update(formularioAnalisisOcupacional);
                            await db.SaveChangesAsync();
                            transaction.Commit();
                            return new Response
                            {
                                IsSuccess = true,
                                Message = Mensaje.Satisfactorio
                            };
                        }
                    }
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.Error
                    };

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


        [HttpDelete("{id}")]
        public async Task<Response> Delete([FromRoute] int id)
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

                var respuesta = await db.FormularioAnalisisOcupacional.SingleOrDefaultAsync(m => m.IdEmpleado == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.FormularioAnalisisOcupacional.Remove(respuesta);
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
    }
}