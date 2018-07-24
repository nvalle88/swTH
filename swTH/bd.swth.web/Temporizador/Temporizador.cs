using System;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.IO;
using bd.swth.datos;
using bd.swth.web.Controllers.API;
using System.Linq;
using System.Collections.Generic;
using bd.swth.entidades.Negocio;

namespace bd.swth.web.Temporizador
{
    public class Temporizador
    {
        private static SwTHDbContext db;
        public static Timer RelojR0 { get; set; }

        
        private static SwTHDbContext CreateDbContext()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = new DbContextOptionsBuilder<SwTHDbContext>();

            var connectionString = configuration.GetConnectionString("SwTHConnection");

            builder.UseSqlServer(connectionString);

            builder.ConfigureWarnings(w =>
                w.Throw(RelationalEventId.QueryClientEvaluationWarning));

            return new SwTHDbContext(builder.Options);
        }


        public static void SetRelojR0(Timer timer, Action accion, TimeSpan tiempoEsperaFuncionCallBack, TimeSpan periodoEsperaFuncionCallBack)
        {
            db = db ?? CreateDbContext();
            RelojR0 = new Timer((c) => {
                accion();
            }, accion,  tiempoEsperaFuncionCallBack, periodoEsperaFuncionCallBack);
            
        }




        

        public static async Task AccionesRelojR0()
        {
            try
            {
                var horaActual = DateTime.Now.Hour;

                var horaControlDiarioMin = 0;//0
                var horaControlDiarioMax = 24;//3

                if (horaActual>horaControlDiarioMin && horaActual<horaControlDiarioMax) {

                    var ctl = new AccionesPersonalController(db);
                    await ctl.ActualizarDiasRestantesAccionPersonal();

                    var ctl1 = new SolicitudPlanificacionVacacionesController(db);
                    await ctl1.CrearRegistroVacacionesEmpleados();
                    
                }
            
            }
            catch (Exception)
            {
                throw;
            }
        }

        

        

        public static void InicializarRelojR0(TimeSpan inicioCiclo,TimeSpan intervaloCiclo)
        {
            SetRelojR0(RelojR0, async () => {
                await AccionesRelojR0();
            },  inicioCiclo, intervaloCiclo);
            
        }


    }
}
