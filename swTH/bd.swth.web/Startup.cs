using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using Microsoft.AspNetCore.Http;
using bd.swth.servicios.Interfaces;
using bd.swth.servicios.Servicios;
using bd.swth.entidades.Constantes;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using bd.swth.entidades.ViewModels;
using EnviarCorreo;
using SendMails.methods;

namespace bd.swth.web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<SwTHDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("SwTHConnection")));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IUploadFileService, UploadFileService>();

            // Configuración Constantes tipo relacion
            ConstantesTipoRelacion.Nombramiento = Configuration.GetSection("Nombramiento").Value;
            ConstantesTipoRelacion.Contrato = Configuration.GetSection("Contrato").Value;

            Constantes.PartidaVacante = Configuration.GetSection("PartidaVacante").Value;
            Constantes.PartidaOcupada = Configuration.GetSection("PartidaOcupada").Value;
            Constantes.PartidaSuprimida = Configuration.GetSection("PartidaSuprimida").Value;


            //Configuración del correo

            MailConfig.HostUri = Configuration.GetSection("Smtp").Value;
            MailConfig.PrimaryPort = Convert.ToInt32(Configuration.GetSection("PrimaryPort").Value);
            MailConfig.SecureSocketOptions = Convert.ToInt32(Configuration.GetSection("SecureSocketOptions").Value);
            MailConfig.RequireAuthentication = Convert.ToBoolean(Configuration.GetSection("RequireAuthentication").Value);
            MailConfig.UserName = Configuration.GetSection("UsuarioCorreo").Value;
            MailConfig.Password = Configuration.GetSection("PasswordCorreo").Value;

            MailConfig.EmailFrom = Configuration.GetSection("EmailFrom").Value;
            MailConfig.NameFrom = Configuration.GetSection("NameFrom").Value;

            // Constantes de correo
            ConstantesCorreo.Smtp = Configuration.GetSection("Smtp").Value;
            ConstantesCorreo.PrimaryPort = Configuration.GetSection("PrimaryPort").Value;
            ConstantesCorreo.SecureSocketOptions = Configuration.GetSection("SecureSocketOptions").Value;
            ConstantesCorreo.CorreoTTHH = Configuration.GetSection("CorreoTTHH").Value;
            ConstantesCorreo.PasswordCorreo = Configuration.GetSection("PasswordCorreo").Value; 
            ConstantesCorreo.NameFrom = Configuration.GetSection("NameFrom").Value;
            ConstantesCorreo.Subject = Configuration.GetSection("Subject").Value;
            ConstantesCorreo.MensajeCorreoSuperior = Configuration.GetSection("MensajeCorreoSuperior").Value;
            ConstantesCorreo.MensajeCorreoDependencia = Configuration.GetSection("MensajeCorreoDependencia").Value;
            ConstantesCorreo.MensajeCorreoMedio = Configuration.GetSection("MensajeCorreoMedio").Value;
            ConstantesCorreo.MensajeCorreoEnlace = Configuration.GetSection("MensajeCorreoEnlace").Value;
            ConstantesCorreo.MensajeCorreoInferior = Configuration.GetSection("MensajeCorreoInferior").Value;
            //Correo planificacion evaluacion evento
            ConstantesCorreo.SubjectCapacitaciones= Configuration.GetSection("SubjectCapacitaciones").Value;
            ConstantesCorreo.CorreoCabecera = Configuration.GetSection("CorreoCabecera").Value;
            ConstantesCorreo.CorreoEnlace = Configuration.GetSection("CorreoEnlace").Value;
            ConstantesCorreo.CorreoPie = Configuration.GetSection("CorreoPie").Value;

            //Constantes Estados del FAO
            EstadosFAO.Asignado = Convert.ToInt32(Configuration.GetSection("Asignado").Value);
            EstadosFAO.RealizadoEmpleado = Convert.ToInt32(Configuration.GetSection("RealizadoEmpleado").Value);
            EstadosFAO.RealizadoJefe = Convert.ToInt32(Configuration.GetSection("RealizadoJefe").Value);
            EstadosFAO.RealizadoEspecialistaTH = Convert.ToInt32(Configuration.GetSection("RealizadoEspecialistaTH").Value);
            EstadosFAO.RealizadoJefeTH = Convert.ToInt32(Configuration.GetSection("RealizadoJefeTH").Value);

            //Constantes Evaluacion Desempeño
            EvaluacionDesempeño.Sobresaliente = Configuration.GetSection("Sobresaliente").Value;
            EvaluacionDesempeño.MuyBueno = Configuration.GetSection("MuyBueno").Value;
            EvaluacionDesempeño.Bueno = Configuration.GetSection("Bueno").Value;
            EvaluacionDesempeño.Regular = Configuration.GetSection("Regular").Value;
            EvaluacionDesempeño.Insuficiente = Configuration.GetSection("Insuficiente").Value;

            EvaluacionDesempeño.AltamenteDesarrollada = Configuration.GetSection("AltamenteDesarrollada").Value;
            EvaluacionDesempeño.Desarrollada = Configuration.GetSection("Desarrollada").Value;
            EvaluacionDesempeño.MedianamenteDesarrollada = Configuration.GetSection("MedianamenteDesarrollada").Value;
            EvaluacionDesempeño.PocoDesarrollada = Configuration.GetSection("PocoDesarrollada").Value;
            EvaluacionDesempeño.NoDesarrollada = Configuration.GetSection("NoDesarrollada").Value;

            EvaluacionDesempeño.Siempre = Configuration.GetSection("Siempre").Value;
            EvaluacionDesempeño.Frecuentemente = Configuration.GetSection("Frecuentemente").Value;
            EvaluacionDesempeño.Algunavez = Configuration.GetSection("Algunavez").Value;
            EvaluacionDesempeño.Raravez = Configuration.GetSection("Raravez").Value;
            EvaluacionDesempeño.Nunca = Configuration.GetSection("Nunca").Value;
            
            EvaluacionDesempeño.TrabajoEnEquipo = Configuration.GetSection("TrabajoEnEquipo").Value;
            EvaluacionDesempeño.Iniciativa = Configuration.GetSection("Iniciativa").Value;
            EvaluacionDesempeño.Liderazgo = Configuration.GetSection("Liderazgo").Value;


            // Constante de estado para Documento Activacion Personal
            Constantes.ActivacionPersonalValorActivado = Configuration.GetSection("ActivacionPersonalValorActivado").Value;
            Constantes.ActivacionPersonalValorDesactivado = Configuration.GetSection("ActivacionPersonalValorDesactivado").Value;

            // Constantes GrupoOcupacional
            ConstantesGrupoOcupacional.GrupoOcupacionalNivelSuperior = Configuration.GetSection("GrupoOcupacionalNivelSuperior").Value;

            ConstantesGrupoOcupacional.GrupoOcupacionalNivelOperativo = Configuration.GetSection("GrupoOcupacionalNivelOperativo").Value;

            // Configuración Constantes Estado Inducción
            ConstantesEstadoInduccion.InduccionFinalizada = Configuration.GetSection("InduccionFinalizada").Value;
            ConstantesEstadoInduccion.InduccionNoFinalizada = Configuration.GetSection("InduccionNoFinalizada").Value;

            // Configuración Constantes Estado Gestión Cambio
            Constantes.ListaEstadosGestionCambio = JsonConvert.DeserializeObject<List<EstadoActividadGestionCambioViewModel>>(Configuration.GetSection("ListaEstadosGestionCambio").Value);

            // Configuración Estados Aprobación Movimiento Interno
            ConstantesEstadosAprobacionMovimientoInterno.ListaEstadosAprobacionMovimientoInterno = JsonConvert.DeserializeObject<List<AprobacionMovimientoInternoViewModel>>(Configuration.GetSection("ListaEstadosAprobacionMovimientoInterno").Value);

            // Configuración Estados Vacaciones
           ConstantesEstadosVacaciones.ListaEstadosVacaciones = JsonConvert.DeserializeObject<List<EstadoVacacionesViewModel>>(Configuration.GetSection("ListaEstadosVacaciones").Value);


            // Configuración constantes Fondo reserva Ingresado
            ConstantesFondosReserva.DiasMinimoDerechoFondoReserva = Convert.ToInt32(Configuration.GetSection("DiasMinimoDerechoFondoReserva").Value);
            ConstantesFondosReserva.IntervaloTemporizadorHorasFondoReserva = Convert.ToInt32(Configuration.GetSection("IntervaloTemporizadorHorasFondoReserva").Value);
            ConstantesFondosReserva.IntervaloTemporizadorMinutosFondoReserva = Convert.ToInt32(Configuration.GetSection("IntervaloTemporizadorMinutosFondoReserva").Value);
            ConstantesFondosReserva.IntervaloTemporizadorSegundosFondoReserva = Convert.ToInt32(Configuration.GetSection("IntervaloTemporizadorSegundosFondoReserva").Value);


            ConstantesFondosReserva.inicioCicloHorasFondoReserva = Convert.ToInt32(Configuration.GetSection("inicioCicloHorasFondoReserva").Value);
            ConstantesFondosReserva.inicioCicloMinutosFondoReserva = Convert.ToInt32(Configuration.GetSection("inicioCicloMinutosFondoReserva").Value);
            ConstantesFondosReserva.inicioCicloSegundosFondoReserva = Convert.ToInt32(Configuration.GetSection("inicioCicloSegundosFondoReserva").Value);




            // Configuración Estados Capacitaciones Ingresado
            ConstantesCapacitacion.EstadoIngresado = Convert.ToInt32(Configuration.GetSection("Ingresado").Value);
            ConstantesCapacitacion.EstadoTerminado = Convert.ToInt32(Configuration.GetSection("Finalizado").Value);
            ConstantesCapacitacion.EstadoEvaluado = Convert.ToInt32(Configuration.GetSection("Calificado").Value);
            ConstantesCapacitacion.Desactivado = Convert.ToInt32(Configuration.GetSection("Desactivado").Value);


            //Valores viaticos
            ConstantesViaticos.Operativo = Configuration.GetSection("Operativo").Value;
            ConstantesViaticos.Jefe = Configuration.GetSection("Jefe").Value;


            //valor de PLanificacionCapacitaciones
            PlanificacionCapacitacion.TipoCapacitacion = Configuration.GetSection("TipoCapacitacion").Value;
            PlanificacionCapacitacion.EstadoEvento= Configuration.GetSection("EstadoEvento").Value;
            PlanificacionCapacitacion.Ambitovento = Configuration.GetSection("Ambitovento").Value;
            PlanificacionCapacitacion.NombreEvento = Configuration.GetSection("NombreEvento").Value;
            PlanificacionCapacitacion.TipoEvento = Configuration.GetSection("TipoEvento").Value;
            PlanificacionCapacitacion.TipoEvaluacion = Configuration.GetSection("TipoEvaluacion").Value;

            // Nombre dependencia desconcentrados
            Constantes.NombreDesconcentrados = Configuration.GetSection("NombreDesconcentrados").Value;

            // constantesAccionPersonal
            ConstantesAccionPersonal.TerminacionEncargo = Configuration.GetSection("TerminacionEncargo").Value;
            ConstantesAccionPersonal.TerminacionSubrogacion = Configuration.GetSection("TerminacionSubrogacion").Value;
            ConstantesAccionPersonal.Encargo = Configuration.GetSection("Encargo").Value;
            ConstantesAccionPersonal.Subrogacion = Configuration.GetSection("Subrogacion").Value;

            // constante nivel jerárquico superior
            Constantes.NombreNivelJerarquicoSuperior = Configuration.GetSection("NombreNivelJerarquicoSuperior").Value;


            /// <summary>
            /// Se lee el fichero appsetting.json según las etiquetas expuestas en este.
            /// Ejemplo:IntervaloTemporizadorHoras Horas que tendra de vida la token.
            /// IntervaloTemporizadorMinutos Minutos que tendra de vida la token
            ///  IntervaloTemporizadorSegundos Segundos que tendra de vida la token.
            /// </summary>
            var IntervaloCicloHoras = Configuration.GetSection("IntervaloTemporizadorHoras").Value;
            var IntervaloCicloMinutos = Configuration.GetSection("IntervaloTemporizadorMinutos").Value;
            var IntervaloCicloSegundos = Configuration.GetSection("IntervaloTemporizadorSegundos").Value;

            /// <summary>
            /// Se lee el fichero appsetting.json según las etiquetas expuestas en este.
            /// Ejemplo:inicioCicloHoras Horas que comensará a ejecutarse una vez iniciada la aplicación.
            /// inicioCicloMinutos Minutos que comensará a ejecutarse una vez iniciada la aplicación.
            ///  inicioCicloSegundos Segundos que comensará a ejecutarse una vez iniciada la aplicación.
            /// </summary>
            var inicioCicloHoras = Configuration.GetSection("inicioCicloHoras").Value;
            var inicioCicloMinutos = Configuration.GetSection("inicioCicloMinutos").Value;
            var inicioCicloSegundos = Configuration.GetSection("inicioCicloSegundos").Value;


            /// <summary>
            /// Se inicializa el temporizador: RelojR0, el método para inicializarlo se llama InicializarRelojR0
            /// </summary>
            Temporizador.Temporizador.InicializarRelojR0
                (
                  new TimeSpan(
                        Convert.ToInt32(inicioCicloHoras), 
                        Convert.ToInt32(inicioCicloMinutos),
                        Convert.ToInt32(inicioCicloSegundos)
                        )
                , new TimeSpan(
                        Convert.ToInt32(IntervaloCicloHoras), 
                        Convert.ToInt32(IntervaloCicloMinutos), 
                        Convert.ToInt32(IntervaloCicloSegundos)
                        )
                );


            Temporizador.TemporizadorFondoReserva.InicializarRelojR0
               (
                 new TimeSpan(
                      ConstantesFondosReserva.inicioCicloHorasFondoReserva,
                      ConstantesFondosReserva.inicioCicloMinutosFondoReserva,
                      ConstantesFondosReserva.inicioCicloSegundosFondoReserva
                       )
               , new TimeSpan(
                      ConstantesFondosReserva.IntervaloTemporizadorHorasFondoReserva,
                       ConstantesFondosReserva.IntervaloTemporizadorMinutosFondoReserva,
                       ConstantesFondosReserva.IntervaloTemporizadorSegundosFondoReserva
                       )
               );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();




            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();

                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
                {
                    //serviceScope.ServiceProvider.GetService<SwTHDbContext>()
                    //         .Database.Migrate();

                    //serviceScope.ServiceProvider.GetService<SwCompartidoDbContext>().EnsureSeedData();
                }

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
