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

            // Constantes de correo
            Constantes.PartidaVacante = Configuration.GetSection("PartidaVacante").Value;
            Constantes.Smtp = Configuration.GetSection("Smtp").Value;
            Constantes.PrimaryPort = Configuration.GetSection("PrimaryPort").Value;
            Constantes.SecureSocketOptions = Configuration.GetSection("SecureSocketOptions").Value;
            Constantes.CorreoTTHH = Configuration.GetSection("CorreoTTHH").Value;
            Constantes.PasswordCorreo = Configuration.GetSection("PasswordCorreo").Value;

            // Constante de estado para Documento Activacion Personal
            Constantes.ActivacionPersonalValorActivado = Configuration.GetSection("ActivacionPersonalValorActivado").Value;
            Constantes.ActivacionPersonalValorDesactivado = Configuration.GetSection("ActivacionPersonalValorDesactivado").Value;

            // Constantes GrupoOcupacional
            ConstantesGrupoOcupacional.GrupoOcupacionalNivelSuperior = Configuration.GetSection("GrupoOcupacionalNivelSuperior").Value;

            ConstantesGrupoOcupacional.GrupoOcupacionalNivelOperativo = Configuration.GetSection("GrupoOcupacionalNivelOperativo").Value;

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
