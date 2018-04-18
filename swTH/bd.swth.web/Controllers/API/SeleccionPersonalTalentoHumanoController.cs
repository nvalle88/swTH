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
                if (candidatoConcurso.Count() !=0 )
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

                        var fechas = db.TrayectoriaLaboral.Where(s => s.IdPersona == DatosBasicosIndiceOcupacional1.FirstOrDefault().Idcandidato).ToList();
                        if (fechas!=null)
                        {
                            var dia = fechas.FirstOrDefault().FechaFin - fechas.FirstOrDefault().FechaInicio;
                            DatosBasicosIndiceOcupacional1.FirstOrDefault().ExperienciaDias = Convert.ToString("Dias:"+dia.TotalDays);
                            DatosBasicosIndiceOcupacional1.FirstOrDefault().ExperienciaMesAno = Convert.ToString("Años: "+"Meses: "+"Dias: "+dia.Days);
                        }
                        //DatosBasicosIndiceOcupacional1.FirstOrDefault().ExperienciaDias = 
                        // FechaFin = db.TrayectoriaLaboral.Where(s => s.IdPersona == x.IdPersona).FirstOrDefault().FechaFin,
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




        //[HttpPost]
        //[Route("InsertarPostulante")]
        //public async Task<Response> InsertarPostulante([FromBody] ViewModelSeleccionPersonal viewModelSeleccionPersonal)
        //{
        //    using (var transaction = await db.Database.BeginTransactionAsync())
        //    {
        //        try
        //        {
        //            var respuesta = Existe(viewModelSeleccionPersonal);
        //            if (!respuesta.IsSuccess)
        //            {
        //                var persona = new Persona
        //                {
        //                    Identificacion = viewModelSeleccionPersonal.identificacion,
        //                    Nombres = viewModelSeleccionPersonal.nombres,
        //                    Apellidos = viewModelSeleccionPersonal.Apellidos

        //                };
        //                //1. Insertar Persona 
        //                var personaInsertarda = await db.Persona.AddAsync(persona);
        //                await db.SaveChangesAsync();

        //                //2. Insertar Empleado (Inicializado : IdPersona, IdDependencia)
        //                var empleadoinsertado = new Candidato
        //                {
        //                    IdPersona = personaInsertarda.Entity.IdPersona
        //                };
        //                var empleado = await db.Candidato.AddAsync(empleadoinsertado);
        //                await db.SaveChangesAsync();


        //                transaction.Commit();

        //                return new Response
        //                {
        //                    IsSuccess = true,
        //                    Message = Mensaje.Satisfactorio,
        //                    Resultado = empleado.Entity
        //                };
        //            }

        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = Mensaje.ExisteRegistro,
        //            };

        //        }
        //        catch (Exception ex)
        //        {

        //            transaction.Rollback();
        //            await GuardarLogService.SaveLogEntry(new LogEntryTranfer
        //            {
        //                ApplicationName = Convert.ToString(Aplicacion.SwTH),
        //                ExceptionTrace = ex.Message,
        //                Message = Mensaje.Excepcion,
        //                LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
        //                LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
        //                UserName = "",

        //            });
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = Mensaje.Error,
        //            };
        //        }
        //    }

       // }

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
    }
}