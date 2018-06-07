﻿using Microsoft.AspNetCore.Builder;
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

            
            Constantes.PartidaVacante = Configuration.GetSection("PartidaVacante").Value;
            Constantes.PartidaOcupada = Configuration.GetSection("PartidaOcupada").Value;

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


            // Configuración Estados Capacitaciones Ingresado
            ConstantesCapacitacion.EstadoIngresado = Convert.ToInt32(Configuration.GetSection("Ingresado").Value);
            ConstantesCapacitacion.EstadoTerminado = Convert.ToInt32(Configuration.GetSection("Finalizado").Value);
            ConstantesCapacitacion.EstadoEvaluado = Convert.ToInt32(Configuration.GetSection("Calificado").Value);
            ConstantesCapacitacion.Desactivado = Convert.ToInt32(Configuration.GetSection("Desactivado").Value);



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
