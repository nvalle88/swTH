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
using bd.swth.entidades.Constantes;

namespace bd.swth.web.Temporizador
{
    public class TemporizadorFondoReserva
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


        private static int DiasDiferencia(DateTime fechaIngreso, DateTime fechaActual)
        {
            TimeSpan dias = fechaActual - fechaIngreso;
            return dias.Days;
        }

        

        public static async Task AccionesRelojR0()
        {
            try
            {
                    var listaEmpleados =await db.Empleado.Where(x => x.Activo == true && x.DerechoFondoReserva==false).Select(x => new Empleado { IdEmpleado = x.IdEmpleado, FechaIngreso = x.FechaIngreso}).ToListAsync();
                    foreach (var item in listaEmpleados)
                    {
                        var fechaIngresoActual = Convert.ToDateTime(item.FechaIngreso == null ? DateTime.Today : item.FechaIngreso);
                        var diasDiferencia=DiasDiferencia(fechaIngresoActual, DateTime.Today);

                        if (diasDiferencia>= ConstantesFondosReserva.DiasMinimoDerechoFondoReserva)
                        {
                            var empleadoActualizar = await db.Empleado.Where(x=>x.IdEmpleado==item.IdEmpleado).FirstOrDefaultAsync();
                            empleadoActualizar.DerechoFondoReserva = true;
                            db.Empleado.Update(empleadoActualizar);
                            await db.SaveChangesAsync();

                        }


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
