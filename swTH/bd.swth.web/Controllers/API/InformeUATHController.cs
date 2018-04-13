using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bd.swth.datos;
using bd.swth.entidades.Constantes;
using bd.swth.entidades.Enumeradores;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.Utils;
using bd.swth.entidades.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/InformeUATH")]
    public class InformeUATHController : Controller
    {
        private readonly SwTHDbContext db;

        public InformeUATHController(SwTHDbContext db)
        {
            this.db = db;
        }
        [HttpPost]
        [Route("InsertarInforeUATH")]
        public async Task<Response> InsertarInforeUATH([FromBody] DocumentoFAOViewModel documentoFAOViewModel)
        {

            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {
                    int idpuesto = 0;
                   //var lista =  db.Empleado.Where(x => x.NombreUsuario == documentoFAOViewModel.NombreUsuario).FirstOrDefault();
                    if (documentoFAOViewModel.Revisar == false)
                    {
                        idpuesto = documentoFAOViewModel.IdManualPuestoActual;
                    }
                    else
                    {
                        idpuesto = documentoFAOViewModel.IdManualPuesto;
                    }
                    var informeInformeUATH = new InformeUATH
                        {
                            IdManualPuestoOrigen = documentoFAOViewModel.IdManualPuestoActual,
                            IdManualPuestoDestino = idpuesto,
                            Revisar = documentoFAOViewModel.Revisar,
                            IdAdministracionTalentoHumano = documentoFAOViewModel.IdAdministracionTalentoHumano

                        };
                    //1. Insertar informeInformeUATH 
                    var informeInformeUATHInsertar = await db.InformeUATH.AddAsync(informeInformeUATH);
                    

                    // cambia de estado en formulario Analisis Ocupacional
                    var formularioAnalisis = db.FormularioAnalisisOcupacional.Where(x=>x.IdEmpleado == documentoFAOViewModel.IdEmpleado && x.Anio == DateTime.Now.Year).FirstOrDefault();
                    var formuario  = db.FormularioAnalisisOcupacional.Find(formularioAnalisis.IdFormularioAnalisisOcupacional);
                    formuario.Estado = EstadosFAO.RealizadoJefeTH;
                    db.FormularioAnalisisOcupacional.Update(formuario);
                    await db.SaveChangesAsync();

                    transaction.Commit();

                        return new entidades.Utils.Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.Satisfactorio,
                            Resultado = formularioAnalisis
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

    }
}