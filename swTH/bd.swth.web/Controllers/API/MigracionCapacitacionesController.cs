using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bd.swth.datos;
using bd.swth.entidades.Constantes;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.Utils;
using EnviarCorreo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendMails.methods;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/MigracionCapacitaciones")]
    public class MigracionCapacitacionesController : Controller
    {

        private readonly SwTHDbContext db;

        public MigracionCapacitacionesController(SwTHDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        [Route("VerificarExcel")]
        public async Task<List<PlanCapacitacion>> VerificarExcel([FromBody] List<PlanCapacitacion> lista)
        {
            try
            {
                var result = lista
                .GroupBy(x => x.NombreCiudad)
                .Select(g => new
                {
                    Nombreciudad = g.Key,
                    Total = g.Sum(x => x.PresupuestoIndividual)
                });
                foreach (var item in result)
                {
                    var a = await ExitePresupuesto(item.Nombreciudad, item.Total);

                }
                foreach (var item in lista)
                {

                    var empleado = await db.Empleado.Where(x => x.Activo == true && x.Persona.Identificacion == item.Cedula).FirstOrDefaultAsync();
                    if (empleado == null)
                    {
                        item.Valido = false;
                        item.MensajeError = Mensaje.EmpleadoNoExiste;
                    }
                    else
                    {
                        item.Valido = true;
                    }

                }
                return lista;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarGestionPlanCapacitaciones")]
        public async Task<List<GestionPlanCapacitacion>> ListarGestionPlanCapacitaciones()
        {
            try
            {
                return await db.GestionPlanCapacitacion.ToListAsync();
            }
            catch (Exception)
            {
                return new List<GestionPlanCapacitacion>();
            }
        }

        [HttpPost]
        [Route("LimpiarReportados")]
        public async Task<Response> LimpiarReportados([FromBody] GestionPlanCapacitacion gestionPlanCapacitacion)
        {
            try
            {
                var listadoBorrar = await db.PlanCapacitacion.Where(x => x.IdGestionPlanCapacitacion == gestionPlanCapacitacion.IdGestionPlanCapacitacion).ToListAsync();
                db.PlanCapacitacion.RemoveRange(listadoBorrar);
                await db.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                };
            }
        }
        [HttpGet]
        [HttpPost("ObtenerDatosEmpelado")]
        public async Task<Response> ObtenerDatosEmpelado([FromBody] PlanCapacitacion planCapacitacion)
        {
            try
            {

                var datos2 = await db.IndiceOcupacionalModalidadPartida.Where(x => x.IdEmpleado == planCapacitacion.IdEmpleado).OrderByDescending(x => x.Fecha).Select(y => new PlanCapacitacion
                {
                    IdEmpleado = (int)y.IdEmpleado,
                    Institucion = "BANCO DE DESARROLLO DEL ECUADOR B.P.",
                    Pais = y.IndiceOcupacional.Dependencia.Sucursal.Ciudad.Provincia.Pais.Nombre,
                    Provincia = y.IndiceOcupacional.Dependencia.Sucursal.Ciudad.Provincia.Nombre,
                    NombreCiudad = y.IndiceOcupacional.Dependencia.Sucursal.Ciudad.Nombre,
                    Cedula = y.Empleado.Persona.Identificacion,
                    ApellidoNombre = y.Empleado.Persona.Nombres + " " + y.Empleado.Persona.Apellidos,
                    Sexo = y.Empleado.Persona.Sexo.Nombre,
                    GrupoOcupacional = y.IndiceOcupacional.EscalaGrados.GrupoOcupacional.TipoEscala,
                    ModalidadLaboral = y.TipoNombramiento.Nombre,
                    RegimenLaboral = y.TipoNombramiento.RelacionLaboral.Nombre,
                    DenominacionPuesto = y.IndiceOcupacional.ManualPuesto.Nombre,
                    UnidadAdministrativa = y.IndiceOcupacional.Dependencia.Nombre,

                }).FirstOrDefaultAsync();
                if (datos2 == null)
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
                    Resultado = datos2,
                };

            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet]
        [HttpPost("ObtenerDatosPlanCapacitaciones")]
        public async Task<Response> ObtenerDatosPlanCapacitaciones([FromBody] PlanCapacitacion planCapacitacion)
        {
            try
            {
                var capacitacion = await db.PlanCapacitacion.Where(x => x.IdPlanCapacitacion == planCapacitacion.IdPlanCapacitacion).FirstOrDefaultAsync();
                var datos = await db.Empleado.Where(x => x.Persona.Identificacion == capacitacion.Cedula).FirstOrDefaultAsync();
                var presupuesto = await db.Presupuesto.Where(x => x.NumeroPartidaPresupuestaria == capacitacion.NumeroPartidaPresupuestaria).FirstOrDefaultAsync();

                if (presupuesto != null)
                {
                    var datos2 = await db.IndiceOcupacionalModalidadPartida.Where(x => x.IdEmpleado == datos.IdEmpleado).OrderByDescending(x => x.Fecha).Select(y => new PlanCapacitacion
                    {
                        IdPlanCapacitacion = capacitacion.IdPlanCapacitacion,
                        IdPresupuesto = presupuesto.IdPresupuesto,
                        IdGestionPlanCapacitacion = capacitacion.IdGestionPlanCapacitacion,
                        IdEmpleado = (int)y.IdEmpleado,
                        Institucion = capacitacion.Institucion,
                        NivelDesconcentracion = capacitacion.NivelDesconcentracion,
                        Pais = y.IndiceOcupacional.Dependencia.Sucursal.Ciudad.Provincia.Pais.Nombre,
                        Provincia = y.IndiceOcupacional.Dependencia.Sucursal.Ciudad.Provincia.Nombre,
                        NombreCiudad = y.IndiceOcupacional.Dependencia.Sucursal.Ciudad.Nombre,
                        Cedula = y.Empleado.Persona.Identificacion,
                        ApellidoNombre = y.Empleado.Persona.Nombres + " " + y.Empleado.Persona.Apellidos,
                        Sexo = y.Empleado.Persona.Sexo.Nombre,
                        GrupoOcupacional = y.IndiceOcupacional.EscalaGrados.GrupoOcupacional.TipoEscala,
                        ModalidadLaboral = y.TipoNombramiento.Nombre,
                        RegimenLaboral = y.TipoNombramiento.RelacionLaboral.Nombre,
                        DenominacionPuesto = y.IndiceOcupacional.ManualPuesto.Nombre,
                        ProductoFinal = capacitacion.ProductoFinal,
                        TemaCapacitacion = capacitacion.TemaCapacitacion,
                        ClasificacionTema = capacitacion.ClasificacionTema,
                        Modalidad = capacitacion.Modalidad,
                        Duracion = capacitacion.Duracion,
                        PresupuestoIndividual = capacitacion.PresupuestoIndividual,
                        FechaCapacitacionPlanificada = capacitacion.FechaCapacitacionPlanificada,
                        TipoCapacitacion = capacitacion.TipoCapacitacion,
                        EstadoEvento = capacitacion.EstadoEvento,
                        UnidadAdministrativa = y.IndiceOcupacional.Dependencia.Nombre,
                        AmbitoCapacitacion = capacitacion.AmbitoCapacitacion,
                        NombreEvento = capacitacion.NombreEvento,
                        TipoEvento = capacitacion.TipoEvento,
                        IdProveedorCapacitaciones = capacitacion.IdProveedorCapacitaciones,
                        DuracionEvento = capacitacion.DuracionEvento,
                        Anio = capacitacion.Anio,
                        FechaInicio = capacitacion.FechaInicio,
                        FechaFin = capacitacion.FechaFin,
                        ValorReal = capacitacion.ValorReal,
                        IdCiudad = capacitacion.IdCiudad,
                        TipoEvaluacion = capacitacion.TipoEvaluacion,
                        Ubicacion = capacitacion.Ubicacion,
                        Observacion = capacitacion.Observacion

                    }).FirstOrDefaultAsync();
                    if (datos2 == null)
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
                        Resultado = datos2,
                    };
                }

                return new Response
                {
                    IsSuccess = false,
                    Message = "Verificar Presupuesto no Existe",

                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }
        [HttpPost]
        [Route("InsertarReportadoPlanificacion")]
        public async Task<Response> InsertarReportadoNomina([FromBody] List<PlanCapacitacion> listaSalvar)
        {
            try
            {
                var listadoBorrar = await db.PlanCapacitacion.Where(x => x.IdGestionPlanCapacitacion == listaSalvar.FirstOrDefault().IdGestionPlanCapacitacion).ToListAsync();

                db.PlanCapacitacion.RemoveRange(listadoBorrar);
                await db.SaveChangesAsync();
                await db.PlanCapacitacion.AddRangeAsync(listaSalvar);
                await db.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                };
            }
        }
        [HttpPost]
        [Route("InsertarPlanCapacitacion")]
        public async Task<Response> InsertarPlanCapacitacion([FromBody] PlanCapacitacion planCapacitacion)
        {
            try
            {
                List<Response> correoResponse = new List<Response>();
                //if (await Existe(planCapacitacion))
                //{
                //    return new Response
                //    {
                //        IsSuccess = false,
                //        Message = Mensaje.ExisteRegistro,
                //    };
                //}

                var presupuesto = await db.Presupuesto.Where(x => x.IdPresupuesto == planCapacitacion.IdPresupuesto).FirstOrDefaultAsync();

                var datos2 = await db.IndiceOcupacionalModalidadPartida.Where(x => x.IdEmpleado == planCapacitacion.IdEmpleado).OrderByDescending(x => x.Fecha).Select(y => new PlanCapacitacion
                {
                    Institucion = "BANCO DE DESARROLLO DEL ECUADOR B.P.",
                    Pais = y.IndiceOcupacional.Dependencia.Sucursal.Ciudad.Provincia.Pais.Nombre,
                    Provincia = y.IndiceOcupacional.Dependencia.Sucursal.Ciudad.Provincia.Nombre,
                    NombreCiudad = y.IndiceOcupacional.Dependencia.Sucursal.Ciudad.Nombre,
                    Cedula = y.Empleado.Persona.Identificacion,
                    Correo = y.Empleado.Persona.CorreoPrivado,
                    ApellidoNombre = y.Empleado.Persona.Nombres + " " + y.Empleado.Persona.Apellidos,
                    Sexo = y.Empleado.Persona.Sexo.Nombre,
                    GrupoOcupacional = y.IndiceOcupacional.EscalaGrados.GrupoOcupacional.TipoEscala,
                    ModalidadLaboral = y.TipoNombramiento.Nombre,
                    RegimenLaboral = y.TipoNombramiento.RelacionLaboral.Nombre,
                    DenominacionPuesto = y.IndiceOcupacional.ManualPuesto.Nombre,
                    UnidadAdministrativa = y.IndiceOcupacional.Dependencia.Nombre,

                }).FirstOrDefaultAsync();
                var plan = new PlanCapacitacion
                {
                    IdGestionPlanCapacitacion = planCapacitacion.IdGestionPlanCapacitacion,
                    NumeroPartidaPresupuestaria = presupuesto.NumeroPartidaPresupuestaria,
                    Institucion = datos2.Institucion,
                    Pais = datos2.Pais,
                    Provincia = datos2.Provincia,
                    NombreCiudad = datos2.NombreCiudad,
                    NivelDesconcentracion = planCapacitacion.NivelDesconcentracion,

                    UnidadAdministrativa = datos2.UnidadAdministrativa,
                    Cedula = datos2.Cedula,
                    ApellidoNombre = datos2.ApellidoNombre,
                    Sexo = datos2.Sexo,
                    GrupoOcupacional = datos2.GrupoOcupacional,
                    DenominacionPuesto = datos2.DenominacionPuesto,

                    RegimenLaboral = datos2.RegimenLaboral,
                    ModalidadLaboral = datos2.ModalidadLaboral,
                    TemaCapacitacion = planCapacitacion.TemaCapacitacion,
                    ClasificacionTema = planCapacitacion.ClasificacionTema,
                    ProductoFinal = planCapacitacion.ProductoFinal,
                    Modalidad = planCapacitacion.Modalidad,
                    Duracion = planCapacitacion.Duracion,

                    PresupuestoIndividual = planCapacitacion.PresupuestoIndividual,
                    FechaCapacitacionPlanificada = planCapacitacion.FechaCapacitacionPlanificada,
                    TipoCapacitacion = planCapacitacion.TipoCapacitacion,
                    EstadoEvento = planCapacitacion.EstadoEvento,
                    AmbitoCapacitacion = planCapacitacion.AmbitoCapacitacion,
                    NombreEvento = planCapacitacion.NombreEvento,
                    TipoEvento = planCapacitacion.TipoEvento,

                    IdProveedorCapacitaciones = planCapacitacion.IdProveedorCapacitaciones,
                    DuracionEvento = planCapacitacion.DuracionEvento,
                    Anio = planCapacitacion.Anio,
                    FechaInicio = planCapacitacion.FechaInicio,
                    FechaFin = planCapacitacion.FechaFin,
                    ValorReal = planCapacitacion.ValorReal,
                    IdCiudad = planCapacitacion.IdCiudad,

                    TipoEvaluacion = planCapacitacion.TipoEvaluacion,
                    Ubicacion = planCapacitacion.Ubicacion,
                    Observacion = planCapacitacion.Observacion,
                    Estado = ConstantesCapacitacion.EstadoTerminado
            };

                db.PlanCapacitacion.Add(plan);
                await db.SaveChangesAsync();
                var correo = datos2.Correo;
                correoResponse.Add(EnviarMailDesdeCorreoTalentohumano(correo));

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
                    Message = Mensaje.Excepcion
                };
            }
        }
        // PUT: api/BasesDatos/5
        [HttpPost]
        [Route("EditarPlanCapacitacion")]
        public async Task<Response> EditarPlanCapacitacion([FromBody] PlanCapacitacion planCapacitacion)
        {
            try
            {
                List<Response> correoResponse = new List<Response>();
                var planCapacitacionActualizar = await db.PlanCapacitacion.Where(x => x.IdPlanCapacitacion == planCapacitacion.IdPlanCapacitacion).FirstOrDefaultAsync();
                var presupuesto = await db.Presupuesto.Where(x => x.NumeroPartidaPresupuestaria == planCapacitacionActualizar.NumeroPartidaPresupuestaria).FirstOrDefaultAsync();

                if (presupuesto != null)
                {
                    var datos2 = await db.IndiceOcupacionalModalidadPartida.Where(x => x.IdEmpleado == planCapacitacion.IdEmpleado).OrderByDescending(x => x.Fecha).Select(y => new PlanCapacitacion
                    {
                        Institucion = planCapacitacionActualizar.Institucion,
                        Pais = y.IndiceOcupacional.Dependencia.Sucursal.Ciudad.Provincia.Pais.Nombre,
                        Provincia = y.IndiceOcupacional.Dependencia.Sucursal.Ciudad.Provincia.Nombre,
                        NombreCiudad = y.IndiceOcupacional.Dependencia.Sucursal.Ciudad.Nombre,
                        Cedula = y.Empleado.Persona.Identificacion,
                        Correo= y.Empleado.Persona.CorreoPrivado,
                        ApellidoNombre = y.Empleado.Persona.Nombres + " " + y.Empleado.Persona.Apellidos,
                        Sexo = y.Empleado.Persona.Sexo.Nombre,
                        GrupoOcupacional = y.IndiceOcupacional.EscalaGrados.GrupoOcupacional.TipoEscala,
                        ModalidadLaboral = y.TipoNombramiento.Nombre,
                        RegimenLaboral = y.TipoNombramiento.RelacionLaboral.Nombre,
                        DenominacionPuesto = y.IndiceOcupacional.ManualPuesto.Nombre,
                        UnidadAdministrativa = y.IndiceOcupacional.Dependencia.Nombre,
                        
                    }).FirstOrDefaultAsync();

                    planCapacitacionActualizar.IdGestionPlanCapacitacion = planCapacitacion.IdGestionPlanCapacitacion;
                    planCapacitacionActualizar.NumeroPartidaPresupuestaria = presupuesto.NumeroPartidaPresupuestaria;
                    planCapacitacionActualizar.Institucion = datos2.Institucion;
                    planCapacitacionActualizar.Pais = datos2.Pais;
                    planCapacitacionActualizar.Provincia = datos2.Provincia;
                    planCapacitacionActualizar.NombreCiudad = datos2.NombreCiudad;
                    planCapacitacionActualizar.NivelDesconcentracion = planCapacitacion.NivelDesconcentracion;

                    planCapacitacionActualizar.UnidadAdministrativa = datos2.UnidadAdministrativa;
                    planCapacitacionActualizar.Cedula = datos2.Cedula;
                    planCapacitacionActualizar.ApellidoNombre = datos2.ApellidoNombre;
                    planCapacitacionActualizar.Sexo = datos2.Sexo;
                    planCapacitacionActualizar.GrupoOcupacional = datos2.GrupoOcupacional;
                    planCapacitacionActualizar.DenominacionPuesto = datos2.DenominacionPuesto;

                    planCapacitacionActualizar.RegimenLaboral = datos2.RegimenLaboral;
                    planCapacitacionActualizar.ModalidadLaboral = datos2.ModalidadLaboral;
                    planCapacitacionActualizar.TemaCapacitacion = planCapacitacion.TemaCapacitacion;
                    planCapacitacionActualizar.ClasificacionTema = planCapacitacion.ClasificacionTema;
                    planCapacitacionActualizar.ProductoFinal = planCapacitacion.ProductoFinal;
                    planCapacitacionActualizar.Modalidad = planCapacitacion.Modalidad;
                    planCapacitacionActualizar.Duracion = planCapacitacion.Duracion;

                    planCapacitacionActualizar.PresupuestoIndividual = planCapacitacion.PresupuestoIndividual;
                    planCapacitacionActualizar.FechaCapacitacionPlanificada = planCapacitacion.FechaCapacitacionPlanificada;
                    planCapacitacionActualizar.TipoCapacitacion = planCapacitacion.TipoCapacitacion;
                    planCapacitacionActualizar.EstadoEvento = planCapacitacion.EstadoEvento;
                    planCapacitacionActualizar.AmbitoCapacitacion = planCapacitacion.AmbitoCapacitacion;
                    planCapacitacionActualizar.NombreEvento = planCapacitacion.NombreEvento;
                    planCapacitacionActualizar.TipoEvento = planCapacitacion.TipoEvento;

                    planCapacitacionActualizar.IdProveedorCapacitaciones = planCapacitacion.IdProveedorCapacitaciones;
                    planCapacitacionActualizar.DuracionEvento = planCapacitacion.DuracionEvento;
                    planCapacitacionActualizar.Anio = planCapacitacion.Anio;
                    planCapacitacionActualizar.FechaInicio = planCapacitacion.FechaInicio;
                    planCapacitacionActualizar.FechaFin = planCapacitacion.FechaFin;
                    planCapacitacionActualizar.ValorReal = planCapacitacion.ValorReal;
                    planCapacitacionActualizar.IdCiudad = planCapacitacion.IdCiudad;

                    planCapacitacionActualizar.TipoEvaluacion = planCapacitacion.TipoEvaluacion;
                    planCapacitacionActualizar.Ubicacion = planCapacitacion.Ubicacion;
                    planCapacitacionActualizar.Observacion = planCapacitacion.Observacion;
                    planCapacitacionActualizar.Estado = ConstantesCapacitacion.EstadoTerminado;
                    db.PlanCapacitacion.Update(planCapacitacionActualizar);
                    await db.SaveChangesAsync();
                    var correo = datos2.Correo;
                    correoResponse.Add(EnviarMailDesdeCorreoTalentohumano(correo));                                     

                    return new Response
                    {
                        IsSuccess = true,
                        Resultado = planCapacitacionActualizar
                    };
                }
                return new Response
                {
                    IsSuccess = false,
                    Resultado = Mensaje.ErrorActualizarArchivo
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

        [HttpPost]
        [Route("ListarReportados")]
        public async Task<List<PlanCapacitacion>> ListarReportados([FromBody] GestionPlanCapacitacion gestionPlanCapacitacion)
        {
            try
            {
                var lista = await db.PlanCapacitacion.Where(x => x.IdGestionPlanCapacitacion == gestionPlanCapacitacion.IdGestionPlanCapacitacion).ToListAsync();
                return lista;
            }
            catch (Exception)
            {
                return new List<PlanCapacitacion>();
            }
        }
        [HttpPost]
        [Route("ListarReportadosucursal")]
        public async Task<List<PlanCapacitacion>> ListarReportadosucursal([FromBody] GestionPlanCapacitacion gestionPlanCapacitacion)
        {
            try
            {
                var empleado = await db.Empleado.Where(a => a.NombreUsuario == gestionPlanCapacitacion.NombreUsuario).FirstOrDefaultAsync();
                if (empleado != null)
                {
                    var ciudadDatos = db.Dependencia.Where(a => a.IdDependencia == empleado.IdDependencia).Select(y => new Dependencia
                    {
                        Nombre = y.Sucursal.Ciudad.Nombre
                    }
                    ).FirstOrDefault();
                    var lista = await db.PlanCapacitacion.Where(x => x.NombreCiudad == ciudadDatos.Nombre && x.IdGestionPlanCapacitacion == gestionPlanCapacitacion.IdGestionPlanCapacitacion).ToListAsync();
                    return lista;
                }
                return new List<PlanCapacitacion>();
            }
            catch (Exception ex)
            {
                return new List<PlanCapacitacion>();
            }
        }


        // DELETE: api/BasesDatos/5
        [HttpPost]
        [Route("Desactivar")]
        public async Task<Response> Desactivar([FromBody]PlanCapacitacion planCapacitacion)
        {
            try
            {
                var planCapacitacionActualizar = await db.PlanCapacitacion.Where(x => x.IdPlanCapacitacion == planCapacitacion.IdPlanCapacitacion).FirstOrDefaultAsync();
                if (planCapacitacionActualizar == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };

                }
                planCapacitacionActualizar.Estado = ConstantesCapacitacion.Desactivado;
                db.PlanCapacitacion.Update(planCapacitacionActualizar);
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
                    Message = Mensaje.BorradoNoSatisfactorio,
                };

            }
        }
        
        private async Task<bool> ExitePresupuesto(string ciudadresive, decimal? valorresive)
        {
            var datosenvia = new Presupuesto();
            var ciudad = await db.Ciudad.Where(x => x.Nombre == ciudadresive).FirstOrDefaultAsync();
            if (ciudad != null)
            {
                var sucursal = await db.Sucursal.Where(x => x.IdCiudad == ciudad.IdCiudad).FirstOrDefaultAsync();
                if (sucursal != null)
                {
                    var presupuesto = await db.Presupuesto.Where(x => x.IdSucursal == sucursal.IdSucursal).FirstOrDefaultAsync();
                    if (presupuesto != null)
                    {
                        var b = db.DetallePresupuesto.Where(x => x.IdPresupuesto == presupuesto.IdPresupuesto).ToListAsync().Result.Sum(x => x.Valor);
                        var valor = b + Convert.ToDouble(valorresive);
                        if (valor <= presupuesto.Valor)
                        {
                            return true;
                        }
                    }
                }

            }
            return false;
        }
        public Response EnviarMailDesdeCorreoTalentohumano(string correo)
        {
            try
            {

                //Static class MailConf 
                MailConfig.HostUri = ConstantesCorreo.Smtp;
                MailConfig.PrimaryPort = Convert.ToInt32(ConstantesCorreo.PrimaryPort);
                MailConfig.SecureSocketOptions = Convert.ToInt32(ConstantesCorreo.SecureSocketOptions);


                string mensaje = ConstantesCorreo.MensajeCorreoSuperior;

                if (ConstantesCorreo.MensajeCorreoDependencia == "true")
                {
                    mensaje = mensaje + "Talento Humano" + "\n \n";
                }                
                mensaje = mensaje +
                ConstantesCorreo.CorreoCabecera +
                ConstantesCorreo.CorreoEnlace +
                ConstantesCorreo.CorreoPie;

                //Class for submit the email 
                Mail mail = new Mail
                {
                    Body = mensaje
                                     ,
                    EmailTo = correo
                                     ,
                    NameTo = "Name To"
                                     ,
                    Subject = ConstantesCorreo.SubjectCapacitaciones
                };

                //execute the method Send Mail or SendMailAsync
                var a = Emails.SendEmailAsync(mail);               
                if (a.Result == Convert.ToString(true)) {
                    return new Response
                    {
                        IsSuccess = true,
                        Resultado = Mensaje.CorreoSatisfactorio,
                    };
                }

                return new Response
                {
                    IsSuccess = false,
                    Resultado = Mensaje.Error,
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Resultado = Mensaje.ErrorCorreo + " a: " + correo,
                };
            }
        }
    }
}