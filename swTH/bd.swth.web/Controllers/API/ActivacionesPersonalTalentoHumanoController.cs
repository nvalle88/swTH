using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.Utils;
using bd.swth.entidades.ViewModels;
using bd.swth.entidades.Constantes;
using SendMails.methods;
using EnviarCorreo;

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



        // GET: api/ActivacionesPersonalTalentoHumano
        [HttpGet]
        [Route("ListarDependencias")]
        public async Task<List<Dependencia>> ListarDependencias()
        {

            List<Dependencia> lista = new List<Dependencia>();


            try
            {

                lista = await db.Dependencia.OrderBy(x => x.IdDependencia).ToListAsync();

                return lista;
            }
            catch (Exception ex)
            {

                return new List<Dependencia>();
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

                empleado = db.Empleado.Include(x => x.Dependencia).Include( x => x.Persona).Where(x => 
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
        public async Task<Response> PostExamenComplementario([FromBody] ListaActivarPersonalTalentoHumanoViewModel listaRecibida)
        {
            using (var transaction = db.Database.BeginTransaction())
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

                    var lista = listaRecibida.listaAPTHVM;
                    var hoy = DateTime.Now.ToString("dd/MM/yyyy");
                    int estado = 0;

                    for (int i = 0; i < lista.Count; i++)
                    {
                        if (lista.ElementAt(i).Nombre != null)
                        {
                            var activarPersonalTalentoHumano = new ActivarPersonalTalentoHumano();

                            activarPersonalTalentoHumano.IdDependencia = lista.ElementAt(i).IdDependencia;
                            activarPersonalTalentoHumano.Fecha = DateTime.Parse(hoy);

                            if (lista.ElementAt(i).Estado == false)
                            {
                                estado = 0;
                            }
                            else
                            {
                                estado = 1;
                            }

                            activarPersonalTalentoHumano.Estado = estado;

                            Response response = Existe(activarPersonalTalentoHumano);



                            if (!response.IsSuccess && estado == 1)
                            {

                                EnviarCorreoElectronico(activarPersonalTalentoHumano);

                                db.ActivarPersonalTalentoHumano.Add(activarPersonalTalentoHumano);
                                await db.SaveChangesAsync();
                                transaction.Commit();
                            }
                            else
                            {
                                /*
                                activarPersonalTalentoHumano.IdActivarPersonalTalentoHumano = Convert.ToInt32(response.Resultado);
                                db.ActivarPersonalTalentoHumano.Update(activarPersonalTalentoHumano);
                                */
                            }


                        }

                    }


                    

                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.GuardadoSatisfactorio,
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


        private Response Existe(ActivarPersonalTalentoHumano activarPersonalTalentoHumano)
        {

            var Respuesta = db.ActivarPersonalTalentoHumano.Where(p =>

                    p.IdDependencia == activarPersonalTalentoHumano.IdDependencia
                    && p.Fecha == activarPersonalTalentoHumano.Fecha

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


        private Response EnviarCorreoElectronico(ActivarPersonalTalentoHumano activarPersonalTalentoHumano)
        {
            try
            {

                var empleado = GetJefePorDependencia(activarPersonalTalentoHumano.IdDependencia);
                var correo = empleado.Persona.CorreoPrivado;


                //Static class MailConf 
                MailConfig.HostUri = Constantes.Smtp; 
                MailConfig.PrimaryPort = Convert.ToInt32(Constantes.PrimaryPort);
                MailConfig.SecureSocketOptions = Convert.ToInt32(Constantes.SecureSocketOptions);

                //Class for submit the email 

                Mail enviar = new Mail
                {
                    Password = Constantes.PasswordCorreo
                                     ,
                    Body = "My Body"
                                     ,
                    EmailFrom = Constantes.CorreoTTHH
                                     ,
                    EmailTo = correo
                                     ,
                    NameFrom = "Name From"
                                     ,
                    NameTo = "Name To"
                                     ,
                    Subject = "La plataforma de activación de requerimientos de personal está activada"
                };
                //execute the method Send Mail or SendMailAsync
                var a = Emails.SendEmail(enviar);




                return new Response
                {
                    IsSuccess = true,
                    Resultado = Mensaje.MensajeSatisfactorio,
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Resultado = Mensaje.Excepcion,
                };
            }
        }


        


    }
}