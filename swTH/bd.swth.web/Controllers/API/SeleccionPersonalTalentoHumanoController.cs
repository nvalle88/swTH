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
using Itenso.TimePeriod;
using bd.swth.entidades.ObjectTransfer;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/SeleccionPersonalTalentoHumano")]
    public class SeleccionPersonalTalentoHumanoController : Controller
    {
        private readonly SwTHDbContext db;

        public SeleccionPersonalTalentoHumanoController(SwTHDbContext db)
        {
            this.db = db;
        }


        // MÉTODOS PÚBLICOS

        // GET: api
        [Route("ListarPuestoVacantesSeleccionPersonal")]
        public async Task<List<ViewModelSeleccionPersonal>> ListarPuestoVacantesSeleccionPersonal()
        {
            try
            {
                //var name = Constantes.PartidaVacante;
                var ModalidadPartida = await db.ModalidadPartida.Where(x => x.Nombre == Constantes.PartidaVacante).FirstOrDefaultAsync();
                var DatosBasicosIndiceOcupacional = await db.IndiceOcupacional.Where(x => x.IdModalidadPartida == ModalidadPartida.IdModalidadPartida)
                 .GroupBy(n => new { grupoOcupacional = n.IdManualPuesto, PuestoInstitucional = n.IdEscalaGrados })
                 .Select(x => new ViewModelSeleccionPersonal
                 {
                     idIndiceOcupacional = x.FirstOrDefault().IdIndiceOcupacional,
                     iddependecia = x.FirstOrDefault().IdDependencia,
                     NumeroPartidaGeneral = db.PartidaGeneral.Where(s => s.IdPartidaGeneral == x.FirstOrDefault().IdPartidaGeneral).FirstOrDefault().NumeroPartida,
                     UnidadAdministrativa = db.Dependencia.Where(s => s.IdDependencia == x.FirstOrDefault().IdDependencia).FirstOrDefault().Nombre,
                     NumeroPartidaIndividual = Convert.ToString(x.FirstOrDefault().NumeroPartidaIndividual),
                     PuestoInstitucional = db.ManualPuesto.Where(s => s.IdManualPuesto == x.FirstOrDefault().IdManualPuesto).FirstOrDefault().Nombre,
                     grupoOcupacional = db.EscalaGrados.Where(s => s.IdEscalaGrados == x.FirstOrDefault().IdEscalaGrados).FirstOrDefault().Nombre,
                     Rol = db.RolPuesto.Where(s => s.IdRolPuesto == x.FirstOrDefault().IdRolPuesto).FirstOrDefault().Nombre,
                     Remuneracion = db.EscalaGrados.Where(s => s.IdEscalaGrados == x.FirstOrDefault().IdEscalaGrados).FirstOrDefault().Remuneracion
                     //NumeroPuesto = x.Count()

                 }).ToListAsync();
                foreach (var item in DatosBasicosIndiceOcupacional)
                {
                    var estado = db.PartidasFase.Where(s => s.IdIndiceOcupacional == item.idIndiceOcupacional).ToList();
                    foreach (var item1 in estado)
                    {
                        if (item.idIndiceOcupacional == item1.IdIndiceOcupacional)
                        {
                            item.NumeroPuesto = item1.Vacantes;
                            item.IdPrtidaFase = item1.IdPartidasFase;

                        }
                    }
                }


                return DatosBasicosIndiceOcupacional;
            }
            catch (Exception ex)
            {
                return new List<ViewModelSeleccionPersonal>();
            }
        }

        [HttpPost]
        [Route("ObtenerEncabezadopostulante")]
        public async Task<ViewModelSeleccionPersonal> ObtenerEncabezadopostulante([FromBody] ViewModelSeleccionPersonal viewModelSeleccionPersonal)
        {
            try
            {
                var DatosBasicosIndiceOcupacional1 = new List<ViewModelCandidatoExperiencia>();
                var DatosBasicosIndiceOcupacional2 = new List<ViewModelCandidatoExperiencia>();
                var DatosBasicosIndiceOcupacional = await db.IndiceOcupacional.Where(x => x.IdDependencia == viewModelSeleccionPersonal.iddependecia)
                 .Select(x => new ViewModelSeleccionPersonal
                 {
                     iddependecia = x.IdDependencia,
                     UnidadAdministrativa = x.Dependencia.Nombre,
                     PuestoInstitucional = x.ManualPuesto.Nombre

                 }).FirstOrDefaultAsync();
                var candidatoConcurso = await db.CandidatoConcurso.Where(x => x.IdPartidasFase == viewModelSeleccionPersonal.IdPrtidaFase).ToListAsync();
                if (candidatoConcurso.Count() != 0)
                {
                    foreach (var item in candidatoConcurso)
                    {
                        DatosBasicosIndiceOcupacional1 = await db.Candidato.Where(x => x.IdCandidato == item.IdCandidato)
                        .Select(x => new ViewModelCandidatoExperiencia
                        {
                            Idcandidato = x.IdPersona,
                            Nombres = x.Persona.Nombres + " " + x.Persona.Apellidos,
                            Cedula = x.Persona.Identificacion
                        }).ToListAsync();

                       var dia= await aExperiencia(DatosBasicosIndiceOcupacional1.FirstOrDefault().Idcandidato);
                        DatosBasicosIndiceOcupacional1.FirstOrDefault().ExperienciaDias = Convert.ToString(dia.dia);
                        DatosBasicosIndiceOcupacional1.FirstOrDefault().ExperienciaMesAno = dia.Experiencia;
                        DatosBasicosIndiceOcupacional2.AddRange(DatosBasicosIndiceOcupacional1);
                    }
                }
                //var listacandidatos = await db.CandidatoConcurso

                DatosBasicosIndiceOcupacional.ListasCanditadoExperiencia = DatosBasicosIndiceOcupacional2;
                return DatosBasicosIndiceOcupacional;

            }
            catch (Exception ex)
            {
                ex.ToString();
                return new ViewModelSeleccionPersonal();
            }
        }

        [HttpPost]
        [Route("ListaCanditadoApuesto")]
        public async Task<ViewModelCandidatoExperiencia> ListaCanditadoApuesto([FromBody] ViewModelSeleccionPersonal viewModelSeleccionPersonal)
        {
            try
            {
                //var DatosBasicosIndiceOcupacional = await db.IndiceOcupacional.Where(x => x.IdDependencia == viewModelSeleccionPersonal.iddependecia)
                // .Select(x => new ViewModelCandidatoExperiencia
                // {

                //     //iddependecia = x.IdDependencia,
                //     //UnidadAdministrativa = x.Dependencia.Nombre,
                //     //PuestoInstitucional = x.ManualPuesto.Nombre,

                // }).FirstOrDefaultAsync();
                //return DatosBasicosIndiceOcupacional.ListasCanditadoExperiencia;
                return new ViewModelCandidatoExperiencia();

            }
            catch (Exception ex)
            {
                ex.ToString();
                return new ViewModelCandidatoExperiencia();
            }
        }

        private Response Existe(ViewModelSeleccionPersonal viewModelSeleccionPersonal)
        {
            var identificacion = viewModelSeleccionPersonal.identificacion.ToUpper().TrimEnd().TrimStart();
            var Empleadorespuesta = db.Persona.Where(p => p.Identificacion == identificacion).FirstOrDefault();

            if (Empleadorespuesta != null)
            {
                if (viewModelSeleccionPersonal.identificacion == Empleadorespuesta.Identificacion)
                {
                    return new Response
                    {
                        IsSuccess = false,
                    };
                }
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                };

            }
            return new Response
            {
                IsSuccess = false,
            };
        }
        public async Task<DateRequest> aExperiencia(int candidato)
        {
            DateRequest fecha = new DateRequest();
            var resultado = "";
           
            var fechas = db.TrayectoriaLaboral.Where(s => s.IdPersona == candidato).ToList();
            if (fechas.Count != 0)
            {
                foreach (var item in fechas)
                {
                    DateDiff periodo = new DateDiff
                        (
                        item.FechaInicio, item.FechaFin
                        );
                    resultado = "Años: " + periodo.Years + " Mes: " + periodo.Months + " Dia: " + periodo.Days;
                    fecha.dia = periodo.Days;
                    fecha.Experiencia = resultado;                }
            }
            return fecha;
        }
    }
}