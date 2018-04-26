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

            //Constantes Estados del FAO
            EstadosFAO.Asignado = Convert.ToInt32(Configuration.GetSection("Asignado").Value);
            EstadosFAO.RealizadoEmpleado = Convert.ToInt32(Configuration.GetSection("RealizadoEmpleado").Value);
            EstadosFAO.RealizadoJefe = Convert.ToInt32(Configuration.GetSection("RealizadoJefe").Value);
            EstadosFAO.RealizadoEspecialistaTH = Convert.ToInt32(Configuration.GetSection("RealizadoEspecialistaTH").Value);
            EstadosFAO.RealizadoJefeTH = Convert.ToInt32(Configuration.GetSection("RealizadoJefeTH").Value);



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
