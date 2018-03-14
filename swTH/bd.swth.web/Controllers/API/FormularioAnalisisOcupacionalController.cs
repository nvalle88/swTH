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
                    else
                    {
                        formularioAnalisisOcupacional.IdFormularioAnalisisOcupacional = empleado.IdFormularioAnalisisOcupacional;
                        formularioAnalisisOcupacional.IdEmpleado = empleado.IdEmpleado;
                        formularioAnalisisOcupacional.MisionPuesto = empleado.MisionPuesto;
                        formularioAnalisisOcupacional.Estado = empleado.Estado;
                        db.FormularioAnalisisOcupacional.Update(formularioAnalisisOcupacional);
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

        [HttpPost]
        [Route("ActualizarActividades")]
        public async Task<Response> ActualizarActividades([FromBody] DocumentoFAOViewModel documentoFAOViewModel)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var activarAnalisiOcupacional = new ActividadesAnalisisOcupacional();
                    foreach (var item in documentoFAOViewModel.ListaActividads)
                    {
                        activarAnalisiOcupacional = db.ActividadesAnalisisOcupacional.Where(x => x.IdActividadesAnalisisOcupacional == Convert.ToInt32(item)).FirstOrDefault();
                        activarAnalisiOcupacional.Cumple = true;
                        db.ActividadesAnalisisOcupacional.Update(activarAnalisiOcupacional);
                    }
                    
                    await db.SaveChangesAsync();


                    //cambia de estado en el formulario en FormularioAnalisis Ocupacional
                    var formularioAnalisisOcupacional = db.FormularioAnalisisOcupacional.Where(x=> x.IdFormularioAnalisisOcupacional==activarAnalisiOcupacional.IdFormularioAnalisisOcupacional).FirstOrDefault();
                    formularioAnalisisOcupacional.Estado = 1;
                    db.FormularioAnalisisOcupacional.Update(formularioAnalisisOcupacional);
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

                            if (formularioAnalisisOcupacional.actividad1 != null)
                            {
                                await db.ActividadesAnalisisOcupacional.AddAsync(new ActividadesAnalisisOcupacional
                                {
                                    Actividades = formularioAnalisisOcupacional.actividad1,
                                    IdFormularioAnalisisOcupacional = empleado.IdFormularioAnalisisOcupacional
                                });
                            }
                            if (formularioAnalisisOcupacional.actividad2 != null)
                            {
                                await db.ActividadesAnalisisOcupacional.AddAsync(new ActividadesAnalisisOcupacional
                                {
                                    IdFormularioAnalisisOcupacional = empleado.IdFormularioAnalisisOcupacional,
                                    Actividades = formularioAnalisisOcupacional.actividad2
                                });
                            }
                            if (formularioAnalisisOcupacional.actividad3 != null)
                            {
                                await db.ActividadesAnalisisOcupacional.AddAsync(new ActividadesAnalisisOcupacional
                                {
                                    IdFormularioAnalisisOcupacional = empleado.IdFormularioAnalisisOcupacional,
                                    Actividades = formularioAnalisisOcupacional.actividad3
                                });
                            }
                                if (formularioAnalisisOcupacional.actividad4 != null)
                                {
                                    await db.ActividadesAnalisisOcupacional.AddAsync(new ActividadesAnalisisOcupacional
                                    {
                                        IdFormularioAnalisisOcupacional = empleado.IdFormularioAnalisisOcupacional,
                                        Actividades = formularioAnalisisOcupacional.actividad4
                                    });
                                }
                                if (formularioAnalisisOcupacional.actividad5 != null)
                                {
                                    await db.ActividadesAnalisisOcupacional.AddAsync(new ActividadesAnalisisOcupacional
                                    {
                                        IdFormularioAnalisisOcupacional = empleado.IdFormularioAnalisisOcupacional,
                                        Actividades = formularioAnalisisOcupacional.actividad5
                                    });
                                }
                                if (formularioAnalisisOcupacional.actividad6 != null)
                                {
                                    await db.ActividadesAnalisisOcupacional.AddAsync(new ActividadesAnalisisOcupacional
                                    {
                                        IdFormularioAnalisisOcupacional = empleado.IdFormularioAnalisisOcupacional,
                                        Actividades = formularioAnalisisOcupacional.actividad6
                                    });
                                }
                                if (formularioAnalisisOcupacional.actividad7 != null)
                                {
                                    await db.ActividadesAnalisisOcupacional.AddAsync(new ActividadesAnalisisOcupacional
                                    {
                                        IdFormularioAnalisisOcupacional = empleado.IdFormularioAnalisisOcupacional,
                                        Actividades = formularioAnalisisOcupacional.actividad7
                                    });
                                }
                                if (formularioAnalisisOcupacional.actividad8 != null)
                                {
                                    await db.ActividadesAnalisisOcupacional.AddAsync(new ActividadesAnalisisOcupacional
                                    {
                                        IdFormularioAnalisisOcupacional = empleado.IdFormularioAnalisisOcupacional,
                                        Actividades = formularioAnalisisOcupacional.actividad8
                                    });
                                }
                                if (formularioAnalisisOcupacional.actividad9 != null)
                                {
                                    await db.ActividadesAnalisisOcupacional.AddAsync(new ActividadesAnalisisOcupacional
                                    {
                                        IdFormularioAnalisisOcupacional = empleado.IdFormularioAnalisisOcupacional,
                                        Actividades = formularioAnalisisOcupacional.actividad9
                                    });
                                }
                                if (formularioAnalisisOcupacional.actividad10 != null)
                                {
                                    await db.ActividadesAnalisisOcupacional.AddAsync(new ActividadesAnalisisOcupacional
                                    {
                                        IdFormularioAnalisisOcupacional = empleado.IdFormularioAnalisisOcupacional,
                                        Actividades = formularioAnalisisOcupacional.actividad10
                                    });
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
    }
}