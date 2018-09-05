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
using MoreLinq;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/ActivacionesPersonalTalentoHumano")]
    public class ActivacionesPersonalTalentoHumanoController : Controller
    {
        private readonly SwTHDbContext db;

        public ActivacionesPersonalTalentoHumanoController(SwTHDbContext db)
        {
            this.db = db;
        }


        // MÉTODOS PÚBLICOS

        // POST: api/ActivacionesPersonalTalentoHumano
        [HttpPost]
        [Route("GetListDependenciasByFiscalYearActual")]
        public async Task<List<ActivarPersonalTalentoHumanoViewModel>> GetListDependenciasByFiscalYearActual([FromBody]UsuarioViewModel usuario)
        {

            List<ActivarPersonalTalentoHumanoViewModel> listaResultado = new List<ActivarPersonalTalentoHumanoViewModel>();

            try
            {

                var empleadoActual = db.Empleado
                    .Include(d => d.Dependencia)
                    .Where(x => x.NombreUsuario == usuario.NombreUsuarioActual)
                    .FirstOrDefault()
                ;


                var listaDependencias = await db.Dependencia
                    .Include(i=>i.Sucursal)
                    .Where(w => w.IdSucursal == empleadoActual.Dependencia.IdSucursal)
                    .OrderBy(x => x.IdDependencia).ToListAsync();

                var listaDependenciasEnviadasCorreoThisYear = await ListarActivarPersonalTalentoHumanoYearActual();

                var idDependencia = 0;



                for (int i = 0; i < listaDependencias.Count; i++)
                {
                    ActivarPersonalTalentoHumanoViewModel model = new ActivarPersonalTalentoHumanoViewModel();

                    // Carga de datos del ViewModel
                    model.IdDependencia = listaDependencias.ElementAt(i).IdDependencia;
                    model.Fecha = DateTime.Now;
                    model.Estado = false;
                    model.Nombre = listaDependencias.ElementAt(i).Nombre;
                    model.IdSucursal = listaDependencias.ElementAt(i).Sucursal.IdSucursal;
                    model.SucursalNombre = listaDependencias.ElementAt(i).Sucursal.Nombre;

                    // Validación de estado para los checkBox
                    for (int j = 0; j < listaDependenciasEnviadasCorreoThisYear.Count; j++)
                    {
                        idDependencia = listaDependenciasEnviadasCorreoThisYear.ElementAt(j).IdDependencia;

                        if (idDependencia == listaDependencias.ElementAt(i).IdDependencia)
                        {

                            model.Existe = true; //Esta línea pone true cuando existe el registro (Este año)

                            var estadoLista = listaDependenciasEnviadasCorreoThisYear.ElementAt(j).Estado;

                            if (estadoLista == Convert.ToInt32(Constantes.ActivacionPersonalValorActivado))
                            {
                                model.Estado = true;

                            }

                            j = listaDependenciasEnviadasCorreoThisYear.Count + 1;
                        }
                    }

                    listaResultado.Add(model);
                }
                return listaResultado;

            }
            catch (Exception ex)
            {

                return listaResultado;
            }

        }



        /// <summary>
        /// Solo toma los jefes de la dependencia que están activos
        /// toma en cuenta los movimientos de subrogación, encargo y movimientos temporales
        /// </summary>
        /// <param name="idDependencia"></param>
        /// <returns></returns>
        // GET: api/ActivacionesPersonalTalentoHumano
        [HttpGet]
        [Route("GetJefePorDependencia")]
        public async Task<Empleado> GetJefePorDependencia(int idDependencia)
        {

            Empleado empleado = new Empleado();
            
            try
            {

                var empleadoActualIOMP = await db.IndiceOcupacionalModalidadPartida
                    .Include(i => i.Empleado)
                    .Include(i => i.Empleado.Dependencia)
                    .Include(i => i.Empleado.Persona)
                    .Include(i => i.IndiceOcupacional)
                    .Include(i => i.IndiceOcupacional.ManualPuesto)
                    .Where(w =>
                        w.Empleado.IdDependencia == idDependencia
                        && w.Empleado.Activo == true
                        && w.Empleado.EsJefe == true
                    )
                    .OrderByDescending(o => o.IdIndiceOcupacionalModalidadPartida)
                    .FirstOrDefaultAsync();

                /* 
                 * Obtiene el último movimiento temporal del empleado logueado que esté vigente en esta fecha
                 * y asi saber si subroga un cargo o está encargado del puesto
                */
                var empleadoPuestoEncargado = await db.EmpleadoMovimiento
                    .Include(i => i.IndiceOcupacional)
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
                        && w.IndiceOcupacional.IdDependencia == idDependencia
                        && w.EsJefe == true
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
                            empleado = empleadoActualIOMP.Empleado;
                        }
                        else
                        {


                            IndiceOcupacionalUsuarioActual = empleadoPuestoEncargado.IndiceOcupacional;
                            empleadoJefe = empleadoPuestoEncargado.EsJefe;
                            empleado = empleadoPuestoEncargado.Empleado;

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
                            empleado = empleadoActualIOMP.Empleado;
                        }
                        else
                        {


                            IndiceOcupacionalUsuarioActual = empleadoPuestoEncargado.IndiceOcupacional;
                            empleadoJefe = empleadoPuestoEncargado.EsJefe;
                            empleado = empleadoPuestoEncargado.Empleado;

                        }

                    }
                    else
                    {
                        IndiceOcupacionalUsuarioActual = empleadoPuestoEncargado.IndiceOcupacional;
                        empleadoJefe = empleadoPuestoEncargado.EsJefe;
                        empleado = empleadoPuestoEncargado.Empleado;
                    }

                }
                else
                {
                    IndiceOcupacionalUsuarioActual = empleadoActualIOMP.IndiceOcupacional;
                    empleadoJefe = empleadoActualIOMP.Empleado.EsJefe;
                    empleado = empleadoActualIOMP.Empleado;
                }



                return empleado;
            }
            catch (Exception ex)
            {

                return empleado;
            }

        }




        // POST: api/ActivacionesPersonalTalentoHumano
        [HttpPost]
        [Route("InsertarActivacionesPersonalTalentoHumano")]
        public async Task<Response> InsertarActivacionesPersonalTalentoHumano([FromBody]ListaActivarPersonalTalentoHumanoViewModel listaRecibida)
        {
            using (var transaction = db.Database.BeginTransaction())
            {

                List<Response> correoResponse = new List<Response>();

                try
                {

                    var resultadoMensaje = "";
                    var lista = listaRecibida.listaAPTHVM;
                    var hoy = DateTime.Now.ToString("dd/MM/yyyy");
                    int estado = 0;


                    if (!ModelState.IsValid)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = ""
                        };
                    }



                    for (int i = 0; i < lista.Count; i++)
                    {
                        if (lista.ElementAt(i).Nombre != null)
                        {
                            var activarPersonalTalentoHumano = new ActivarPersonalTalentoHumano();

                            activarPersonalTalentoHumano.IdDependencia = lista.ElementAt(i).IdDependencia;
                            activarPersonalTalentoHumano.Fecha = DateTime.Parse(hoy);

                            if (lista.ElementAt(i).Estado == false)
                            {
                                estado = Convert.ToInt32(Constantes.ActivacionPersonalValorDesactivado);
                            }
                            else
                            {
                                estado = Convert.ToInt32(Constantes.ActivacionPersonalValorActivado);
                            }

                            activarPersonalTalentoHumano.Estado = estado;

                            Response response = Existe(activarPersonalTalentoHumano);



                            if (!response.IsSuccess && estado == Convert.ToInt32(Constantes.ActivacionPersonalValorActivado))
                            {
                                var empleado = await GetJefePorDependencia(activarPersonalTalentoHumano.IdDependencia);

                                if (empleado != null && empleado.IdEmpleado > 0)
                                {
                                    var correo = empleado.Persona.CorreoPrivado;
                                    correoResponse.Add( await EnviarMailDesdeCorreoTalentohumano(correo, empleado.Dependencia.Nombre));

                                    db.ActivarPersonalTalentoHumano.Add(activarPersonalTalentoHumano);
                                    await db.SaveChangesAsync();
                                }
                                else
                                {
                                    correoResponse.Add(new Response
                                    {
                                        IsSuccess = false,
                                        Message = Mensaje.DependenciaSinJefe + " " + lista.ElementAt(i).Nombre
                                    }

                                    );
                                }


                            }
                            else
                            {

                                var actualizar = await db.ActivarPersonalTalentoHumano
                                        .Where(apt =>
                                            apt.IdActivarPersonalTalentoHumano == Convert.ToInt32(response.Resultado)
                                        ).FirstOrDefaultAsync();

                                if (
                                    response.IsSuccess
                                    && actualizar.Estado == Convert.ToInt32(Constantes.ActivacionPersonalValorDesactivado)
                                    && estado == Convert.ToInt32(Constantes.ActivacionPersonalValorActivado)
                                    )
                                {
                                    actualizar.Estado = Convert.ToInt32(Constantes.ActivacionPersonalValorActivado);

                                    var empleado = await GetJefePorDependencia(activarPersonalTalentoHumano.IdDependencia);


                                    if (empleado != null && empleado.IdEmpleado > 0)
                                    {

                                        var correo = empleado.Persona.CorreoPrivado;
                                        correoResponse.Add(await EnviarMailDesdeCorreoTalentohumano(correo, empleado.Dependencia.Nombre));

                                        db.ActivarPersonalTalentoHumano.Update(actualizar);
                                        await db.SaveChangesAsync();

                                    }
                                    else
                                    {
                                        correoResponse.Add(new Response
                                        {
                                            IsSuccess = false,
                                            Message = Mensaje.DependenciaSinJefe + " " + lista.ElementAt(i).Nombre
                                        }

                                        );
                                    }

                                }


                            }

                        }

                    }

                    for (int k = 0; k < correoResponse.Count; k++)
                    {
                        if (correoResponse.ElementAt(k).IsSuccess == false)
                        {

                            if (resultadoMensaje.Length < 1)
                            {
                                resultadoMensaje = Mensaje.ErrorCorreo + ", " + correoResponse.ElementAt(k).Message;
                            }
                            else
                            {
                                resultadoMensaje = resultadoMensaje + ", " + correoResponse.ElementAt(k).Message + "\n";
                            }


                        }
                    }


                    transaction.Commit();

                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.CorreoSatisfactorio,
                        Resultado = resultadoMensaje
                    };

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Response
                    {
                        IsSuccess = false,
                        Message = correoResponse.Last().Message,
                    };
                }
            }
        }
       
        public async Task<Response> EnviarMailDesdeCorreoTalentohumano(string correo,string dependenciaNombre)
        {
            try
            {
                string mensaje = ConstantesCorreo.MensajeCorreoSuperior;

                if (ConstantesCorreo.MensajeCorreoDependencia == "true")
                {
                    mensaje = mensaje + dependenciaNombre + "\n \n";
                }


                mensaje = mensaje +
                ConstantesCorreo.MensajeCorreoMedio +
                ConstantesCorreo.MensajeCorreoEnlace +
                ConstantesCorreo.MensajeCorreoInferior;

                //Class for submit the email 
                Mail mail = new Mail
                {
                 
                    Body = mensaje
                                     ,
                    EmailTo = correo
                                     ,
                    NameTo = "Name To"
                                     ,
                    Subject = ConstantesCorreo.Subject
                };

                //execute the method Send Mail or SendMailAsync
                var a = await Emails.SendEmailAsync(mail);


                return new Response
                {
                    IsSuccess = true,
                    Resultado = Mensaje.CorreoSatisfactorio,
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




        

        private async Task<List<ActivarPersonalTalentoHumano>> ListarActivarPersonalTalentoHumanoYearActual()
        {

            List<ActivarPersonalTalentoHumano> lista = new List<ActivarPersonalTalentoHumano>();


            try
            {

                lista = await db.ActivarPersonalTalentoHumano.Where(x => x.Fecha.Year == DateTime.Now.Year).OrderBy(x => x.IdActivarPersonalTalentoHumano).ToListAsync();

                return lista;
            }
            catch (Exception ex)
            {

                return new List<ActivarPersonalTalentoHumano>();
            }

        }


        private Response Existe(ActivarPersonalTalentoHumano activarPersonalTalentoHumano)
        {

            var Respuesta = db.ActivarPersonalTalentoHumano.Where(p =>

                    p.IdDependencia == activarPersonalTalentoHumano.IdDependencia
                    && p.Fecha.Year == activarPersonalTalentoHumano.Fecha.Year

                ).FirstOrDefault();

            if (Respuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = Respuesta.IdActivarPersonalTalentoHumano,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = Respuesta,
            };
        }


        [HttpGet]
        [Route("ObtenerSituacionActual")]
        public async Task<List<DistributivoViewModel>> ObtenerSituacionActual()
        {

            var lista = new List<DistributivoViewModel>();

            try
            {


                var modalidadPartida = await db.ModalidadPartida.ToListAsync();

                var iomp = await db.IndiceOcupacionalModalidadPartida
                    .Include(i => i.IndiceOcupacional)
                    .ToListAsync();

                lista = await db.IndiceOcupacional
                    .Select(s => new DistributivoViewModel
                    {

                        IdDependencia = (int)s.IdDependencia,
                        NombreDependencia = s.Dependencia.Nombre,

                        IdRolPuesto = (int)s.IdRolPuesto,
                        NombreRolPuesto = s.RolPuesto.Nombre,
                        /*
                        IdModalidadPartida = (s.IdModalidadPartida == null) ? 0 : (int)s.IdModalidadPartida,
                        NombreModalidadPartida = (s.IdModalidadPartida == null) ? "" : s.ModalidadPartida.Nombre,
                        */
                        GrupoOcupacional = s.EscalaGrados.GrupoOcupacional.TipoEscala,

                        RMU = s.EscalaGrados.Remuneracion,

                        Grado = (int)s.EscalaGrados.Grado,

                        CantidadEmpleados = iomp.Where(w =>

                                    //w.IndiceOcupacional.IdRolPuesto == s.IdRolPuesto
                                    //&& w.IndiceOcupacional.IdDependencia == s.IdDependencia


                                w.IdIndiceOcupacional == s.IdIndiceOcupacional
                                && w.IdEmpleado != null
                            ).Count()
                        ,

                        PartidaIndividual = "",

                        IdManualPuesto = (int)s.IdManualPuesto,
                        NombreManualPuesto = s.ManualPuesto.Nombre
                    }
                    )
                    .DistinctBy(d => new { d.IdDependencia, d.IdRolPuesto })
                    .ToAsyncEnumerable().ToList();


                return lista;


            }
            catch (Exception ex)
            {

                return lista;
            }

        }


    }
}