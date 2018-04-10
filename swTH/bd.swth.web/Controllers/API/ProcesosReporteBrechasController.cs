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
    [Route("api/ReportesProcesosBrechas")]
    public class ReportesProcesosBrechasController : Controller
    {
        private readonly SwTHDbContext db;

        public ReportesProcesosBrechasController(SwTHDbContext db)
        {
            this.db = db;
        }
        
        /// <summary>
        /// Se necesita enviar IdDependencia
        /// </summary>
        /// <param name="brechasPorDependenciaViewModel"></param>
        /// <returns></returns>
        // GET: api/ReportesProcesosBrechas
        [HttpPost]
        [Route("CrearRolesParaAsignarBrechas")]
        public async Task<BrechasPorDependenciaViewModel> CrearRolesParaAsignarBrechas([FromBody] BrechasPorDependenciaViewModel brechasPorDependenciaViewModel)
        {
            var brechaPorDependencia = new BrechasPorDependenciaViewModel();

            try
            {
                var ListaPuestos = await db.RolPuesto.Select(
                    x=> new ReporteBrechasViewModel{
                        
                        IdRol = x.IdRolPuesto,
                        NombreRol= x.Nombre
                    }
                    
                
                ).ToListAsync();

                brechaPorDependencia.ListaReporteBrechasViewModels = ListaPuestos;
                brechaPorDependencia.IdDependencia = brechasPorDependenciaViewModel.IdDependencia;

                return brechaPorDependencia;

            }
            catch (Exception ex)
            {

                return brechaPorDependencia;
            }

        }

        

        // GET: api/ReportesProcesosBrechas
        [HttpGet]
        [Route("ListarDependencias")]
        public async Task<List<Dependencia>> ListarDependencias()
        {
            var listaDependencias = new List<Dependencia>();

            try
            {
                listaDependencias = await db.Dependencia.OrderBy(x => x.IdDependencia).ToListAsync();

                return listaDependencias;

            }
            catch (Exception ex)
            {

                return listaDependencias;
            }

        }


        
        // GET: api/ReportesProcesosBrechas
        [HttpPost]
        [Route("InsertarBrechasParaDependencia")]
        public async Task<Response> InsertarBrechasParaDependencia([FromBody] BrechasPorDependenciaViewModel brechasPorDependenciaViewModel)
        {
            try
            {

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.GuardadoSatisfactorio
                };


            } catch (Exception ex) {

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };

            }

        }

    }
}