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

        /*

        [Route("ListarPuestoVacantes")]
        public async Task<List<ViewModelPartidaFase>> ListarPuestoVacantes()
        {
            try
            {
                var DatosBasicosIndiceOcupacional = new List<ViewModelPartidaFase>();
                //var name = Constantes.PartidaVacante;
                var ModalidadPartida = await db.ModalidadPartida.Where(x => x.Nombre == Constantes.PartidaVacante).FirstOrDefaultAsync();
                var modalidad = await db.IndiceOcupacionalModalidadPartida.Where(x => x.IdModalidadPartida == ModalidadPartida.IdModalidadPartida).ToListAsync();
                //var DatosBasicosIndiceOcupacional == nu;
                foreach (var item in modalidad)
                {
                    DatosBasicosIndiceOcupacional = await db.IndiceOcupacional.Where(x => x.IdIndiceOcupacional == item.IdIndiceOcupacional)
                  .GroupBy(m => m.ManualPuesto.Nombre)
                  .Select(d => new ViewModelPartidaFase
                  {
                      idescalagrados = Convert.ToInt32(d.FirstOrDefault().IdEscalaGrados),
                      PuestoInstitucional = db.ManualPuesto.Where(s => s.IdManualPuesto == d.FirstOrDefault().IdManualPuesto).FirstOrDefault().Nombre,
                      grupoOcupacional = db.EscalaGrados.Where(s => s.IdEscalaGrados == d.FirstOrDefault().IdEscalaGrados).FirstOrDefault().Nombre,
                      Idindiceocupacional = d.FirstOrDefault().IdIndiceOcupacional

                  })
                  .ToListAsync();
                }

                return DatosBasicosIndiceOcupacional;
            }
            catch (Exception ex)
            {


                return new List<ViewModelPartidaFase>();
            }
        }


        /// <summary>
        /// Este método trae todos los vacantes por nombramiento
        /// </summary>
        /// <returns></returns>
        [Route("ListarConcursosVacantes")]
        public async Task<List<ViewModelPartidaFase>> ListarConcursosVacantes()
        {
            try
            {
                var DatosBasicosIndiceOcupacional = new List<ViewModelPartidaFase>();

                var partidaVacante = await db.ModalidadPartida
                    .Where(x => x.Nombre == Constantes.PartidaVacante)
                    .FirstOrDefaultAsync();

                var listaiomp = await db.IndiceOcupacionalModalidadPartida
                    .Include(i => i.IndiceOcupacional)
                    .Include(i => i.IndiceOcupacional.ManualPuesto)
                    .Include(i => i.IndiceOcupacional.EscalaGrados)
                    .Where(x =>
                        x.NumeroPartidaIndividual != null
                        && x.IdModalidadPartida == partidaVacante.IdModalidadPartida
                    )
                    .ToListAsync();


                DatosBasicosIndiceOcupacional = listaiomp
                    .GroupBy(g => g.IdIndiceOcupacional)
                    .Select(x => new ViewModelPartidaFase
                    {
                        Idindiceocupacional = x.FirstOrDefault().IdIndiceOcupacional,
                        PuestoInstitucional = x.FirstOrDefault().IndiceOcupacional.ManualPuesto.Nombre,
                        grupoOcupacional = x.FirstOrDefault().IndiceOcupacional.EscalaGrados.Nombre,
                        Vacantes = x.Count()
                    })
                    .ToList();



                foreach (var item in DatosBasicosIndiceOcupacional)
                { 
                    var estado = db.PartidasFase
                        .Where(s => 
                            s.IdIndiceOcupacional == item.Idindiceocupacional 
                            && s.Contrato==false
                        )
                        .ToList();

                    foreach (var item1 in estado)
                    {
                        if (item.Idindiceocupacional == item1.IdIndiceOcupacional)
                        {
                            item.IdPartidaFase = item1.IdPartidasFase;
                            item.estado = item1.Estado;
                            item.VacantesCreadas = item1.Vacantes;
                            
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




        /// <summary>
        ///   método para concurso por contrato
        /// </summary>
        /// <param name="viewModelPartidaFase"></param>
        /// <returns></returns>
        [Route("ListarPuestoVacantesContrato")]
        public async Task<List<ViewModelPartidaFase>> ListarPuestoVacantesContrato()
        {
            try
            {

                var DatosBasicosIndiceOcupacional = await db.IndiceOcupacional
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

        [Route("ListarConcursosVacantesContrato")]
        public async Task<List<ViewModelPartidaFase>> ListarConcursosVacantesContrato()
        {
            try
            {

                var DatosBasicosIndiceOcupacional = await db.IndiceOcupacional
                 //.GroupBy(n => new { grupoOcupacional = n.IdManualPuesto, PuestoInstitucional = n.IdEscalaGrados })
                 .Select(x => new ViewModelPartidaFase
                 {
                     Idindiceocupacional = x.IdIndiceOcupacional,

                     PuestoInstitucional = x.ManualPuesto.Nombre,

                     grupoOcupacional = x.EscalaGrados.Nombre,

                     Vacantes = 1

                 }).ToListAsync();
                

                foreach (var item in DatosBasicosIndiceOcupacional)
                {
                    var estado = db.PartidasFase
                        .Where(x => x.IdIndiceOcupacional == item.Idindiceocupacional && x.Contrato==true)
                        .ToList();
                    
                    foreach (var item1 in estado) 
                    {
                        if (item.Idindiceocupacional == item1.IdIndiceOcupacional)
                        {
                            item.IdPartidaFase = item1.IdPartidasFase;
                            item.estado = item1.Estado;
                            item.VacantesCreadas = item1.Vacantes;


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



        [HttpPost]
        [Route("Edit")]
        public async Task<Response> Edit([FromBody] ViewModelPartidaFase viewModelPartidaFase)
        {
            try
            {
                var vacantes = await db.PartidasFase
                    .Where(x => x.IdPartidasFase == viewModelPartidaFase.IdPartidaFase)
                   .Select(d => new ViewModelPartidaFase
                   {
                       IdTipoConcurso = Convert.ToInt32(d.IdTipoConcurso),
                       VacantesCreadas = d.Vacantes,
                       IdPartidaFase = d.IdPartidasFase,
                       Vacantes = viewModelPartidaFase.Vacantes

                   })
                .FirstOrDefaultAsync();

                return new Response
                {
                    IsSuccess = true,
                    Resultado = vacantes
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


        [HttpPost]
        [Route("Editar")]
        public async Task<Response> Editar([FromBody] PartidasFase partidasFase)
        {
            try
            {
                var modelo = await db.PartidasFase
                .Where(w => w.IdPartidasFase == partidasFase.IdPartidasFase)
                .FirstOrDefaultAsync();

                if (modelo == null) {

                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado
                    };
                }

                modelo.Fecha = DateTime.Now;
                modelo.IdTipoConcurso = null;
                modelo.Vacantes = partidasFase.Vacantes;

                db.PartidasFase.Update(modelo);
                await db.SaveChangesAsync();

                return new Response{
                    IsSuccess = true,
                    Message = Mensaje.GuardadoSatisfactorio
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion,
                };
            }
        }

        [HttpPost]
        [Route("InsertarHabilitarConcurso")]
        public async Task<Response> InsertarHabilitarConcurso([FromBody] PartidasFase partidasFase)
        {
            try
            {
                var respuesta = Existe(partidasFase);

                if (!respuesta.IsSuccess)
                {
                    partidasFase.Fecha = DateTime.Now;
                    partidasFase.Estado = 1;
                    partidasFase.IdTipoConcurso = null;

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
                    Message = Mensaje.Excepcion,
                };
            }
        }


        public Response Existe(PartidasFase partidasFase)
        {
            var bdd = partidasFase.IdIndiceOcupacional;
            var bdd2 = partidasFase.IdTipoConcurso;
            var bdd3 = partidasFase.Contrato;

            var modelo = db.PartidasFase
                .Where(p => p.IdIndiceOcupacional == bdd 
                && p.IdTipoConcurso == bdd2 
                && p.Contrato == bdd3)
                .FirstOrDefault();

            if (modelo != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = modelo,
                };

            }
            return new Response
            {
                IsSuccess = false,
            };
        }

    */
    }
}