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
    [Route("api/ActivacionesPersonalTalentoHumano")]
    public class ActivacionesPersonalTalentoHumanoController : Controller
    {
        private readonly SwTHDbContext db;

        public ActivacionesPersonalTalentoHumanoController(SwTHDbContext db)
        {
            this.db = db;
        }


        // MÉTODOS PÚBLICOS

        // GET: api/ActivacionesPersonalTalentoHumano
        [HttpGet]
        [Route("GetListDependenciasByFiscalYearActual")]
        public async Task<List<ActivarPersonalTalentoHumanoViewModel>> GetListDependenciasByFiscalYearActual()
        {

            List<ActivarPersonalTalentoHumanoViewModel> listaResultado = new List<ActivarPersonalTalentoHumanoViewModel>();



            try
            {
                var listaDependencias = await db.Dependencia.OrderBy(x => x.IdDependencia).ToListAsync();

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


                    // Validación de estado para los checkBox
                    for (int j = 0; j < listaDependenciasEnviadasCorreoThisYear.Count; j++)
                    {
                        idDependencia = listaDependenciasEnviadasCorreoThisYear.ElementAt(j).IdDependencia;

                        if (idDependencia == listaDependencias.ElementAt(i).IdDependencia)
                        {
                            
                            var estadoLista = listaDependenciasEnviadasCorreoThisYear.ElementAt(j).Estado; 

                            if (estadoLista == Convert.ToInt32(Constantes.ActivacionPersonalValorActivado)) {
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


        

        // GET: api/ActivacionesPersonalTalentoHumano
        [HttpGet]
        [Route("GetJefePorDependencia")]
        public Empleado GetJefePorDependencia(int idDependencia)
        {

            Empleado empleado = new Empleado();


            try
            {

                empleado = db.Empleado.Include(x => x.Dependencia).Include(x => x.Persona).Where(x =>
                       x.EsJefe == true
                       && x.Dependencia.IdDependencia == idDependencia
                    ).FirstOrDefault();

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
        public async Task<Response> InsertarActivacionesPersonalTalentoHumano([FromBody] ListaActivarPersonalTalentoHumanoViewModel listaRecibida)
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
                                estado = Convert.ToInt32( Constantes.ActivacionPersonalValorDesactivado );
                            }
                            else
                            {
                                estado = Convert.ToInt32(Constantes.ActivacionPersonalValorActivado);
                            }

                            activarPersonalTalentoHumano.Estado = estado;

                            Response response = Existe(activarPersonalTalentoHumano);



                            if (!response.IsSuccess && estado == Convert.ToInt32(Constantes.ActivacionPersonalValorActivado))
                            {
                                var empleado = GetJefePorDependencia(activarPersonalTalentoHumano.IdDependencia);

                                if (empleado != null)
                                {
                                    var correo = empleado.Persona.CorreoPrivado;
                                    correoResponse.Add(EnviarMailDesdeCorreoTalentohumano(correo));

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

                                    var empleado = GetJefePorDependencia(activarPersonalTalentoHumano.IdDependencia);

                                    var correo = empleado.Persona.CorreoPrivado;
                                    correoResponse.Add(EnviarMailDesdeCorreoTalentohumano(correo));

                                    db.ActivarPersonalTalentoHumano.Update(actualizar);
                                    await db.SaveChangesAsync();
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





        public Response EnviarMailDesdeCorreoTalentohumano(string correo)
        {
            try
            {

                //Static class MailConf 
                MailConfig.HostUri = Constantes.Smtp;
                MailConfig.PrimaryPort = Convert.ToInt32(Constantes.PrimaryPort);
                MailConfig.SecureSocketOptions = Convert.ToInt32(Constantes.SecureSocketOptions);

                //Class for submit the email 
                Mail mail = new Mail
                {
                    Password = Constantes.PasswordCorreo
                                     ,
                    Body = "My Body"
                                     ,
                    EmailFrom = Constantes.CorreoTTHH
                                     ,
                    EmailTo = correo
                                     ,
                    NameFrom = "Talento humano"
                                     ,
                    NameTo = "Name To"
                                     ,
                    Subject = "La plataforma de activación de requerimientos de personal se encuentra activada"
                };

                //execute the method Send Mail or SendMailAsync
                var a = Emails.SendEmailAsync(mail);


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




        // MÉTODOS PRIVADOS

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





    }
}