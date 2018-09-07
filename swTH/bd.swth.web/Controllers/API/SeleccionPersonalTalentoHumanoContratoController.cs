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
    [Route("api/SeleccionPersonalTalentoHumanoContrato")]
    public class SeleccionPersonalTalentoHumanoContratoController : Controller
    {
        private readonly SwTHDbContext db;

        public SeleccionPersonalTalentoHumanoContratoController(SwTHDbContext db)
        {
            this.db = db;
        }

        /*

        [HttpPost]
        [Route("InsertarCandidato")]
        public async Task<Response> InsertarCandidato([FromBody] ViewModelSeleccionPersonal viewModelSeleccionPersonal)
        {
            try
            {
                var respuesta = Existe(viewModelSeleccionPersonal);
                if (!respuesta.IsSuccess)
                {

                    using (var transaction = await db.Database.BeginTransactionAsync())
                    {
                        var candidato = new Candidato
                        {
                            Identificacion = viewModelSeleccionPersonal.identificacion,
                            Nombre = viewModelSeleccionPersonal.nombres.ToString().ToUpper(),
                            Apellido = viewModelSeleccionPersonal.Apellidos.ToString().ToUpper()

                        };
                        await db.Candidato.AddAsync(candidato);
                        //await db.SaveChangesAsync();

                        var candidatoConcurso = new CandidatoConcurso
                        {
                            IdCandidato = candidato.IdCandidato,
                            IdPartidasFase = viewModelSeleccionPersonal.IdPartidaFase
                        };

                        await db.CandidatoConcurso.AddAsync(candidatoConcurso);
                        await db.SaveChangesAsync();

                        transaction.Commit();

                        viewModelSeleccionPersonal.IdCandidato = candidato.IdCandidato;
                        viewModelSeleccionPersonal.IdCandidatoConcurso = candidatoConcurso.IdCandidatoConcurso;

                        

                        return new Response
                        {
                            IsSuccess = true,
                            Resultado = viewModelSeleccionPersonal,
                            Message = Mensaje.GuardadoSatisfactorio
                        };
                        

                    }
                    
                }

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ExisteRegistro,
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
        [Route("InsertarCandidatoConcurso")]
        public async Task<Response> InsertarCandidatoConcurso([FromBody] ViewModelSeleccionPersonal viewModelSeleccionPersonal)
        {
            try
            {
                var a = await db.CandidatoConcurso
                    .Where(x => 
                        x.IdCandidato == viewModelSeleccionPersonal.IdCandidato 
                        && x.IdPartidasFase == viewModelSeleccionPersonal.IdPartidaFase
                    )
                    .FirstOrDefaultAsync();

                if (a != null)
                {

                    return new Response { IsSuccess = true };
                }
                else
                {
                    var candidatoConcurso = new CandidatoConcurso
                    {
                        IdCandidato = viewModelSeleccionPersonal.IdCandidato,
                        IdPartidasFase = viewModelSeleccionPersonal.IdPartidaFase,
                        Estado = 0
                    };
                    db.CandidatoConcurso.Add(candidatoConcurso);
                    await db.SaveChangesAsync();
                    return new Response { IsSuccess = true };
                }
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

        //// MÉTODOS PÚBLICOS

        // GET: api
        [Route("ListarPuestoVacantesSeleccionPersonal")]
        public async Task<List<ViewModelSeleccionPersonal>> ListarPuestoVacantesSeleccionPersonal()
        {
            try
            {

                var listaIO = await db.PartidasFase
                    .Where(w=>w.Contrato == true)
                    .Select(x => new ViewModelSeleccionPersonal
                    {
                        idIndiceOcupacional = x.IdIndiceOcupacional,
                        iddependecia = x.IndiceOcupacional.IdDependencia,
                        NumeroPartidaGeneral = x.IndiceOcupacional.PartidaGeneral.NumeroPartida,
                        UnidadAdministrativa = x.IndiceOcupacional.Dependencia.Nombre,
                        NumeroPartidaIndividual = "",
                        PuestoInstitucional = x.IndiceOcupacional.ManualPuesto.Nombre,
                        grupoOcupacional = x.IndiceOcupacional.EscalaGrados.Nombre,
                        Rol = x.IndiceOcupacional.RolPuesto.Nombre,
                        Remuneracion = x.IndiceOcupacional.EscalaGrados.Remuneracion,

                        NumeroPuesto = x.Vacantes,
                        IdPartidaFase = x.IdPartidasFase

                    })
                    .ToListAsync();
                

                return listaIO;

            }
            catch (Exception ex)
            {
                return new List<ViewModelSeleccionPersonal>();
            }
        }


        [HttpPost]
        [Route("ObtenerViewModelSeleccionPersonal")]
        public async Task<ViewModelSeleccionPersonal> ObtenerViewModelSeleccionPersonal([FromBody] ViewModelSeleccionPersonal viewModelSeleccionPersonal)
        {
            try
            {
                var modelo = await db.PartidasFase
                    .Where(w => w.IdPartidasFase == viewModelSeleccionPersonal.IdPartidaFase)
                    .Select(x => new ViewModelSeleccionPersonal
                    {
                        iddependecia = x.IndiceOcupacional.IdDependencia,
                        UnidadAdministrativa = x.IndiceOcupacional.Dependencia.Nombre,
                        PuestoInstitucional = x.IndiceOcupacional.ManualPuesto.Nombre,

                        IdPartidaFase = x.IdPartidasFase,
                        IdCandidato = viewModelSeleccionPersonal.IdCandidato,
                        IdCandidatoConcurso = viewModelSeleccionPersonal.IdCandidatoConcurso,

                    })
                    .FirstOrDefaultAsync();


                return modelo;

            }
            catch (Exception ex)
            {
                ex.ToString();
                return new ViewModelSeleccionPersonal();
            }
        }

        [HttpPost]
        [Route("ListaCanditadoPostulados")]
        public async Task<ViewModelSeleccionPersonal> ListaCanditadoPostulados([FromBody] ViewModelSeleccionPersonal viewModelSeleccionPersonal)
        {
            try
            {
                var DatosBasicosIndiceOcupacional = new ViewModelSeleccionPersonal();
                var DatosBasicosIndiceOcupacional1 = new List<ViewModelCandidatoExperiencia>();
                var DatosBasicosIndiceOcupacional2 = new List<ViewModelCandidatoExperiencia>();
                var candidatoConcurso = await db.CandidatoConcurso.Where(x => x.IdPartidasFase == viewModelSeleccionPersonal.IdPartidaFase).ToListAsync();
                if (candidatoConcurso.Count() != 0)
                {
                    foreach (var item in candidatoConcurso)
                    {
                        DatosBasicosIndiceOcupacional1 = await db.Candidato.Where(x => x.IdCandidato == item.IdCandidato)
                        .Select(x => new ViewModelCandidatoExperiencia
                        {
                            Idcandidato = x.IdCandidato,
                            Nombres = x.Nombre + " " + x.Apellido,
                            Cedula = x.Identificacion
                        }).ToListAsync();

                        var dia = await aExperiencia(DatosBasicosIndiceOcupacional1.FirstOrDefault().Idcandidato);
                        DatosBasicosIndiceOcupacional1.FirstOrDefault().ExperienciaDias = Convert.ToString(dia.dia);
                        DatosBasicosIndiceOcupacional1.FirstOrDefault().ExperienciaMesAno = dia.Experiencia;
                        DatosBasicosIndiceOcupacional1.FirstOrDefault().Estado = item.Estado;
                        DatosBasicosIndiceOcupacional2.AddRange(DatosBasicosIndiceOcupacional1);
                    }
                }
                DatosBasicosIndiceOcupacional.IdPartidaFase = viewModelSeleccionPersonal.IdPartidaFase;
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
        [Route("CandidatoEvaluar")]
        public async Task<ViewModelEvaluarCandidato> CandidatoEvaluar([FromBody] ViewModelEvaluarCandidato viewModelEvaluarCandidato)
        {
            try
            {
                var DatosBasicos = new ViewModelEvaluarCandidato();
                var ListaEstudioCandidato = new List<ViewModelEstudioCandidato>();
                var ListaExperienciaCandidato = new List<ViewModelCandidatoTrayectoriaLaboral>();
                var candidatoConcurso = await db.Candidato.Where(x => x.IdCandidato == viewModelEvaluarCandidato.IdCandidato)
                 .Select(x => new ViewModelEvaluarCandidato
                 {
                     IdPartidaFase = viewModelEvaluarCandidato.IdPartidaFase,
                     IdCandidato = x.IdCandidato,
                     Nombres = x.Nombre + " " + x.Apellido,
                     Identificacion = x.Identificacion
                 }).FirstOrDefaultAsync();
                if (candidatoConcurso != null)
                {
                    ListaEstudioCandidato = await db.CandidatoEstudio.Where(x => x.IdCandidato == candidatoConcurso.IdCandidato)
                        .Select(x => new ViewModelEstudioCandidato
                        {
                            IdCandidato = x.IdCandidato,
                            Titulo = x.Titulo.Nombre,
                            NoSenecity = x.NoSenescyt
                        }).ToListAsync();

                    ListaExperienciaCandidato = await db.CandidatoTrayectoriaLaboral.Where(x => x.IdCandidato == candidatoConcurso.IdCandidato)
                        .Select(x => new ViewModelCandidatoTrayectoriaLaboral
                        {
                            IdCandidato = x.IdCandidato,
                            Institucion = x.Institucion,
                            FechaInicio = x.FechaInicio,
                            FechaFin = x.FechaFin
                        }).ToListAsync();
                    foreach (var item in ListaExperienciaCandidato)
                    {
                        var dia = await aExperiencia(candidatoConcurso.IdCandidato);
                        item.ExperienciaDias = Convert.ToString(dia.dia);
                        item.ExperienciaMesAno = dia.Experiencia;

                    }
                    DatosBasicos.IdCandidato = candidatoConcurso.IdCandidato;
                    DatosBasicos.Identificacion = candidatoConcurso.Identificacion;
                    DatosBasicos.Nombres = candidatoConcurso.Nombres;
                    DatosBasicos.IdPartidaFase = candidatoConcurso.IdPartidaFase;
                    DatosBasicos.ListasPersonaEstudio = ListaEstudioCandidato;
                    DatosBasicos.ListasCanditadoExperiencia = ListaExperienciaCandidato;

                }
                return DatosBasicos;

            }
            catch (Exception ex)
            {
                ex.ToString();
                return new ViewModelEvaluarCandidato();
            }
        }
        [HttpPost]
        [Route("ObtenerCandidato")]
        public async Task<Candidato> ObtenerCandidato([FromBody] Candidato candidato)
        {
            try
            {
                var a = await db.Candidato.Where(x => x.Identificacion == candidato.Identificacion).FirstOrDefaultAsync();
                return a;

            }
            catch (Exception ex)
            {
                ex.ToString();
                return new Candidato();
            }
        }
        [HttpPost]
        [Route("DetalleCandidatoEvaluar")]
        public async Task<ViewModelEvaluarCandidato> DetalleCandidatoEvaluar([FromBody] ViewModelEvaluarCandidato viewModelEvaluarCandidato)
        {
            try
            {
                var DatosBasicos = new ViewModelEvaluarCandidato();
                var ListaEstudioCandidato = new List<ViewModelEstudioCandidato>();
                var ListaExperienciaCandidato = new List<ViewModelCandidatoTrayectoriaLaboral>();
                var candidatoConcurso = await db.Candidato.Where(x => x.IdCandidato == viewModelEvaluarCandidato.IdCandidato)
                 .Select(x => new ViewModelEvaluarCandidato
                 {
                     IdCandidato = x.IdCandidato,
                     Nombres = x.Nombre + " " + x.Apellido,
                     Identificacion = x.Identificacion,
                     Elegido = x.CandidatoConcurso.FirstOrDefault().Preseleccionado,
                     ApliInstruccion = x.CandidatoConcurso.FirstOrDefault().CumpleInstruccion,
                     ApliExperiencia = x.CandidatoConcurso.FirstOrDefault().CumpleExperiencia,
                     PorInstruccion = x.CandidatoConcurso.FirstOrDefault().PorcentajeInstruccion,
                     PorExperiencia = x.CandidatoConcurso.FirstOrDefault().PorcentajeExperiencia,
                     Descripcion = x.CandidatoConcurso.FirstOrDefault().Observacion,


                 }).FirstOrDefaultAsync();
                if (candidatoConcurso != null)
                {
                    ListaEstudioCandidato = await db.CandidatoEstudio.Where(x => x.IdCandidato == candidatoConcurso.IdCandidato)
                        .Select(x => new ViewModelEstudioCandidato
                        {
                            IdCandidato = x.IdCandidato,
                            Titulo = x.Titulo.Nombre,
                            NoSenecity = x.NoSenescyt
                        }).ToListAsync();

                    ListaExperienciaCandidato = await db.CandidatoTrayectoriaLaboral.Where(x => x.IdCandidato == candidatoConcurso.IdCandidato)
                        .Select(x => new ViewModelCandidatoTrayectoriaLaboral
                        {
                            IdCandidato = x.IdCandidato,
                            Institucion = x.Institucion,
                            FechaInicio = x.FechaInicio,
                            FechaFin = x.FechaFin
                        }).ToListAsync();
                    foreach (var item in ListaExperienciaCandidato)
                    {
                        var dia = await aExperiencia(candidatoConcurso.IdCandidato);
                        item.ExperienciaDias = Convert.ToString(dia.dia);
                        item.ExperienciaMesAno = dia.Experiencia;

                    }
                    DatosBasicos.IdCandidato = candidatoConcurso.IdCandidato;
                    DatosBasicos.Identificacion = candidatoConcurso.Identificacion;
                    DatosBasicos.Nombres = candidatoConcurso.Nombres;
                    DatosBasicos.ListasPersonaEstudio = ListaEstudioCandidato;
                    DatosBasicos.ListasCanditadoExperiencia = ListaExperienciaCandidato;
                    DatosBasicos.ApliInstruccion = candidatoConcurso.ApliInstruccion;
                    DatosBasicos.ApliExperiencia = candidatoConcurso.ApliExperiencia;
                    DatosBasicos.PorInstruccion = candidatoConcurso.PorInstruccion;
                    DatosBasicos.PorExperiencia = candidatoConcurso.PorExperiencia;
                    DatosBasicos.Elegido = candidatoConcurso.Elegido;
                    DatosBasicos.Descripcion = candidatoConcurso.Descripcion;
                    DatosBasicos.Total = Convert.ToInt32(candidatoConcurso.PorExperiencia + candidatoConcurso.PorInstruccion);

                }
                return DatosBasicos;

            }
            catch (Exception ex)
            {
                ex.ToString();
                return new ViewModelEvaluarCandidato();
            }
        }

        [HttpPost]
        [Route("InsertarEvaluacion")]
        public async Task<Response> InsertarEvaluacion([FromBody] ViewModelEvaluarCandidato viewModelEvaluarCandidato)
        {
            var a = await db.CandidatoConcurso.Where(x => x.IdCandidato == viewModelEvaluarCandidato.IdCandidato && x.IdPartidasFase == viewModelEvaluarCandidato.IdPartidaFase).FirstOrDefaultAsync();
            try
            {
                a.IdCandidato = viewModelEvaluarCandidato.IdCandidato;
                a.CumpleInstruccion = viewModelEvaluarCandidato.ApliInstruccion;
                a.CumpleExperiencia = viewModelEvaluarCandidato.ApliExperiencia;
                a.PorcentajeInstruccion = viewModelEvaluarCandidato.PorInstruccion;
                a.PorcentajeExperiencia = viewModelEvaluarCandidato.PorExperiencia;
                a.Preseleccionado = viewModelEvaluarCandidato.Elegido;
                a.Observacion = viewModelEvaluarCandidato.Descripcion;
                a.Estado = 1;

                db.CandidatoConcurso.Update(a);
                await db.SaveChangesAsync();
                return new Response { IsSuccess = true };
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

        private Response Existe(ViewModelSeleccionPersonal viewModelSeleccionPersonal)
        {
            var identificacion = viewModelSeleccionPersonal.identificacion.ToUpper().TrimEnd().TrimStart();
            var Empleadorespuesta = db.Candidato.Where(p => p.Identificacion == identificacion).FirstOrDefault();

            if (Empleadorespuesta != null)
            {
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
            var mes = 0;
            var dia = 0;

            var fechas = db.CandidatoTrayectoriaLaboral.Where(s => s.IdCandidato == candidato).ToList();
            if (fechas.Count != 0)
            {
                foreach (var item in fechas)
                {
                    DateDiff periodo = new DateDiff
                        (
                        item.FechaInicio,
                        item.FechaFin
                        );
                    if (periodo.Months > 12)
                    {
                        mes = periodo.Months / 12;
                    }
                    else
                    {
                        mes = periodo.Months;
                    }
                    if (periodo.Days > 365)
                    {
                        dia = periodo.Days / 365;
                    }
                    else
                    {
                        dia = periodo.Days;
                    }
                    resultado = "Años: " + periodo.Years + " Mes: " + mes + " Dia: " + dia;
                    fecha.dia = periodo.Days;
                    fecha.Experiencia = resultado;
                }
            }
            return fecha;
        }

        */

    }
}