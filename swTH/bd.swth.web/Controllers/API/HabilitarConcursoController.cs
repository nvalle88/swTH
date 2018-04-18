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
using MoreLinq;

namespace bd.swth.web.Controllers.API
{

    public class ejemplo
    {
        public string Nombre { get; set; }
        public string Escala { get; set; }
    }
    [Produces("application/json")]
    [Route("api/HabilitarConcurso")]
    public class HabilitarConcursoController : Controller
    {
        private readonly SwTHDbContext db;

        public HabilitarConcursoController(SwTHDbContext db)
        {
            this.db = db;
        }
        [Route("ListarPuestoVacantes")]
        public async Task<List<ViewModelPartidaFase>> ListarPuestoVacantes()
        {
            try
            {
                //var name = Constantes.PartidaVacante;
                var ModalidadPartida = await db.ModalidadPartida.Where(x => x.Nombre == Constantes.PartidaVacante).FirstOrDefaultAsync();
                var DatosBasicosIndiceOcupacional = await db.IndiceOcupacional.Where(x => x.IdModalidadPartida == ModalidadPartida.IdModalidadPartida)
                 .GroupBy(m => m.ManualPuesto.Nombre)
                 .Select(d => new ViewModelPartidaFase
                 {
                     idescalagrados = Convert.ToInt32(d.FirstOrDefault().IdEscalaGrados),
                     PuestoInstitucional = db.ManualPuesto.Where(s => s.IdManualPuesto == d.FirstOrDefault().IdManualPuesto).FirstOrDefault().Nombre,
                     grupoOcupacional = db.EscalaGrados.Where(s => s.IdEscalaGrados == d.FirstOrDefault().IdEscalaGrados).FirstOrDefault().Nombre,
                     Idindiceocupacional = d.FirstOrDefault().IdIndiceOcupacional

                 })
                 .ToListAsync();

                return DatosBasicosIndiceOcupacional;
            }
            catch (Exception ex)
            {
                return new List<ViewModelPartidaFase>();
            }
        }
        [Route("ListarConcursosVacantes")]
        public async Task<List<ViewModelPartidaFase>> ListarConcursosVacantes()
        {
            try
            {
                var ModalidadPartida = await db.ModalidadPartida.Where(x => x.Nombre == Constantes.PartidaVacante).FirstOrDefaultAsync();
                var DatosBasicosIndiceOcupacional = await db.IndiceOcupacional.Where(x => x.IdModalidadPartida == ModalidadPartida.IdModalidadPartida)
                 .GroupBy(n => new { grupoOcupacional = n.IdManualPuesto, PuestoInstitucional = n.IdEscalaGrados })
                 .Select(x => new ViewModelPartidaFase
                 {
                     Idindiceocupacional  = x.FirstOrDefault().IdIndiceOcupacional,
                     PuestoInstitucional = db.ManualPuesto.Where(s => s.IdManualPuesto == x.FirstOrDefault().IdManualPuesto).FirstOrDefault().Nombre,
                     grupoOcupacional = db.EscalaGrados.Where(s => s.IdEscalaGrados == x.FirstOrDefault().IdEscalaGrados).FirstOrDefault().Nombre,
                     Vacantes = x.Count()
                 })
                    .ToListAsync();

                foreach (var item in DatosBasicosIndiceOcupacional)
                {
                    var estado = db.PartidasFase.Where(s => s.IdIndiceOcupacional == item.Idindiceocupacional).ToList();
                    foreach (var item1 in estado)
                    {
                        if ( item.Idindiceocupacional == item1.IdIndiceOcupacional)
                        {
                            item.estado = item1.Estado;
                            item.VacantesCredo = item1.Vacantes;
                            
                            
                        } 
                    }
                }
                return DatosBasicosIndiceOcupacional;
            }
            catch (Exception ex)
            {
                return new List<ViewModelPartidaFase>();
                
            }
        }
        //public async Task<List<ViewModelPartidaFase>> ListarConcursosVacantes()
        //{
        //    try
        //    {
        //         var vacantes = await db.PartidasFase.Where(x=>x.Estado ==0)
        //            .Select(d => new ViewModelPartidaFase
        //            {
        //                idescalagrados = Convert.ToInt32(d.IndiceOcupacional.IdEscalaGrados),
        //                PuestoInstitucional = db.ManualPuesto.Where(s => s.IdManualPuesto == d.IndiceOcupacional.IdManualPuesto).FirstOrDefault().Nombre,
        //                grupoOcupacional = db.EscalaGrados.Where(s => s.IdEscalaGrados == d.IndiceOcupacional.IdEscalaGrados).FirstOrDefault().Nombre,
        //                Idindiceocupacional = d.IndiceOcupacional.IdIndiceOcupacional,
        //                IdPartidaFase = d.IdPartidasFase

        //            })
        //         .ToListAsync();

        //        return vacantes;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new List<ViewModelPartidaFase>();
        //    }
        //}

        [HttpPost]
        [Route("InsertarHabilitarConsurso")]
        public async Task<Response> InsertarHabilitarConsurso([FromBody] PartidasFase partidasFase)
        {
            try
            {
                var respuesta = Existe(partidasFase);
                if (!respuesta.IsSuccess)
                {
                    partidasFase.Fecha = DateTime.Now;
                    partidasFase.Estado = 1;
                    db.PartidasFase.Add(partidasFase);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.GuardadoSatisfactorio
                    };
                }

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ExisteRegistro
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }
        public Response Existe(PartidasFase partidasFase)
        {
            var bdd = partidasFase.IdIndiceOcupacional;
            var bdd2 = partidasFase.IdTipoConcurso;
            var loglevelrespuesta = db.PartidasFase.Where(p => p.IdIndiceOcupacional == bdd && p.IdTipoConcurso == bdd2).FirstOrDefault();
            if (loglevelrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = null,
                };

            }
            return new Response
            {
                IsSuccess = false,
                Resultado = loglevelrespuesta,
            };
        }


    }
}