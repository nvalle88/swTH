using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.Enumeradores;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.log.guardar.Enumeradores;
using bd.swth.entidades.Utils;
using bd.swth.entidades.ViewModels;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Candidatos")]
    public class CandidatosController : Controller
    {
        private readonly SwTHDbContext db;

        public CandidatosController(SwTHDbContext db)
        {
            this.db = db;
        }


        public async Task<bool> existeCandidato(string identificacion,Persona persona)
        {
            var existe = await db.Persona.Where(x => x.Identificacion == identificacion).FirstOrDefaultAsync();
            if (existe == null || existe.IdPersona == persona.IdPersona)
            {
                return false;

            }
            return true;
        }
        [HttpPost]
        [Route("ListarTrayectoriasLaborales")]
        public async Task<List<CandidatoTrayectoriaLaboral>> GetTrayectoriasLaborales([FromBody] ViewModelSeleccionPersonal viewModelSeleccionPersonal)
        {
            try
            {
                var a = await db.CandidatoTrayectoriaLaboral.Where(x => x.IdCandidato == viewModelSeleccionPersonal.IdCandidato).Include(x => x.Candidato).OrderBy(x => x.Institucion).ToListAsync();
                if (a != null)
                {
                    return a;
                }

                return new List<CandidatoTrayectoriaLaboral>(); ;
            }
            catch (Exception ex)
            {
                return new List<CandidatoTrayectoriaLaboral>();
            }
        }
        [HttpPost]
        [Route("InsertarTrayectoriaLaboral")]
        public async Task<Response> PostTrayectoriaLaboral([FromBody] CandidatoTrayectoriaLaboral candidatoTrayectoriaLaboral)
        {
            try
            {
                var respuesta = ExisteTrayectoria(candidatoTrayectoriaLaboral);
                if (!respuesta.IsSuccess)
                {
                    db.CandidatoTrayectoriaLaboral.Add(candidatoTrayectoriaLaboral);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio
                    };
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
        [Route("ListarEstudiosporCandidato")]
        public async Task<List<CandidatoEstudio>> ListarEstudiosporCandidato([FromBody] ViewModelSeleccionPersonal viewModel)
        {
            try
            {
                return await db.CandidatoEstudio.Where(x => x.IdCandidato == viewModel.IdCandidato).Include(x => x.Candidato).Include(x => x.Titulo.AreaConocimiento).Include(x => x.Titulo.Estudio).OrderBy(x => x.FechaGraduado).ToListAsync();

            }
            catch (Exception ex)
            {
                return new List<CandidatoEstudio>();
            }
        }
        [HttpPost]
        [Route("InsertarCandidatoEstudio")]
        public async Task<Response> PostPersonaEstudio([FromBody] CandidatoEstudio candidatoEstudio)
        {
            try
            {
                var respuesta = ExisteEstudio(candidatoEstudio);
                if (!respuesta.IsSuccess)
                {
                    db.CandidatoEstudio.Add(candidatoEstudio);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio
                    };
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
        [Route("EditarCandidato")]
        public async Task<Response> EditarCandidato([FromBody] FichaCandidatoViewModel viewModel)
        {
            try
            {
                    var persona = await db.Persona.Where(x => x.IdPersona == viewModel.IdPersona).FirstOrDefaultAsync();

                    if (persona != null)
                    {
                        var existe = await existeCandidato(viewModel.Identificacion,persona);
                        if (!existe)
                        {
                            persona.Apellidos = viewModel.Apellidos;
                            persona.Nombres = viewModel.Nombres;
                            persona.Identificacion = viewModel.Identificacion;
                            persona.TelefonoCasa = viewModel.TelefonoCasa;
                            persona.TelefonoPrivado = viewModel.TelefonoPrivado;
                            persona.CorreoPrivado = viewModel.CorreoPrivado;
                            db.Persona.Update(persona);
                            await db.SaveChangesAsync();
                            return new Response { IsSuccess = true, Resultado = persona };
                        }
                        return new Response { IsSuccess = false, Message = Mensaje.ExisteRegistro };
                    }


                return new Response { IsSuccess = false, Message = Mensaje.RegistroNoEncontrado };
            }
            catch (Exception ex)
            {
                return new Response { IsSuccess = false, Message = ex.Message };
            }
        }

        //[HttpPost]
        //[Route("ObtenerFichaCandidato")]
        //public async Task<Response> ObtenerFichaCandidato([FromBody] FichaCandidatoViewModel viewModel)
        //{
        //    try
        //    {
        //        var candidato = await db.Candidato.Where(x => x.IdCandidato == viewModel.IdCandidato).OrderBy(x => x.Persona.Nombres).ThenBy(x => x.Persona.Apellidos)
        //                            .Select(y => new FichaCandidatoViewModel
        //                            {
        //                                Apellidos = y.Persona.Apellidos,
        //                                Nombres = y.Persona.Nombres,
        //                                IdCandidato = y.IdCandidato,
        //                                Identificacion = y.Persona.Identificacion,
        //                                IdPersona = y.Persona.IdPersona,
        //                                TelefonoCasa = y.Persona.TelefonoCasa,
        //                                TelefonoPrivado = y.Persona.TelefonoPrivado,
        //                                CorreoPrivado = y.Persona.CorreoPrivado,
        //                            })
        //                            .FirstOrDefaultAsync();

        //        if (candidato != null)
        //        {
        //            return new Response { IsSuccess = true, Resultado = candidato };
        //        }

        //        return new Response { IsSuccess = false, Message = Mensaje.RegistroNoEncontrado };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response { IsSuccess = false, Message = ex.Message };
        //    }
        //}


        // GET: api/BasesDatos
        //[HttpGet]
        //[Route("ListarCandidatos")]
        //public async Task<List<FichaCandidatoViewModel>> ListarCandidatos()
        //{
        //    try
        //    {
        //        var lista = await db.Candidato.OrderBy(x => x.Persona.Nombres).ThenBy(x => x.Persona.Apellidos)
        //                            .Select(y => new FichaCandidatoViewModel
        //                            {
        //                                Apellidos = y.Persona.Apellidos,
        //                                Nombres = y.Persona.Nombres,
        //                                IdCandidato = y.IdCandidato,
        //                                Identificacion = y.Persona.Identificacion,
        //                                IdPersona = y.Persona.IdPersona,
        //                                TelefonoCasa = y.Persona.TelefonoCasa,
        //                                TelefonoPrivado = y.Persona.TelefonoPrivado,
        //                                CorreoPrivado = y.Persona.CorreoPrivado,
        //                            })
        //                            .ToListAsync();

        //        return lista;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new List<FichaCandidatoViewModel>();
        //    }
        //}

        // GET: api/Candidatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetCandidato([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ModeloInvalido,
                    };
                }

                var Candidato = await db.Candidato.SingleOrDefaultAsync(m => m.IdCandidato == id);

                if (Candidato == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                    Resultado = Candidato,
                };
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex.Message,
                    Message = Mensaje.Excepcion,
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }

        // PUT: api/Candidatos/5
        //[HttpPut("{id}")]
        //public async Task<Response> PutCandidato([FromRoute] int id, [FromBody] Candidato candidato)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = Mensaje.ModeloInvalido
        //            };
        //        }

        //        var existe = Existe(candidato);
        //        var CandidatoActualizar = (Candidato)existe.Resultado;
        //        if (existe.IsSuccess)
        //        {
        //            if (CandidatoActualizar.IdCandidato == candidato.IdCandidato)
        //            {
        //                return new Response
        //                {
        //                    IsSuccess = true,
        //                };
        //            }
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = Mensaje.ExisteRegistro,
        //            };
        //        }
        //        var Candidato = db.Candidato.Find(candidato.IdCandidato);

        //        Candidato.IdPersona = Candidato.IdPersona;

        //        db.Candidato.Update(Candidato);
        //        await db.SaveChangesAsync();

        //        return new Response
        //        {
        //            IsSuccess = true,
        //            Message = Mensaje.Satisfactorio,
        //        };

        //    }
        //    catch (Exception ex)
        //    {
        //        await GuardarLogService.SaveLogEntry(new LogEntryTranfer
        //        {
        //            ApplicationName = Convert.ToString(Aplicacion.SwTH),
        //            ExceptionTrace = ex.Message,
        //            Message = Mensaje.Excepcion,
        //            LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
        //            LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
        //            UserName = "",

        //        });

        //        return new Response
        //        {
        //            IsSuccess = true,
        //            Message = Mensaje.Excepcion,
        //        };
        //    }

        //}

        // POST: api/BasesDatos
        [HttpPost]
        [Route("InsertarCandidato")]
        public async Task<Response> PostCandidato([FromBody] Candidato Candidato)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = ""
                    };
                }

                //var respuesta = Existe(Candidato);
                //if (!respuesta.IsSuccess)
                //{
                //    db.Candidato.Add(Candidato);
                //    await db.SaveChangesAsync();
                //    return new Response
                //    {
                //        IsSuccess = true,
                //        Message = Mensaje.Satisfactorio
                //    };
                //}

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ExisteRegistro,
                };

            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex.Message,
                    Message = Mensaje.Excepcion,
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }

        // DELETE: api/BasesDatos/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteCandidato([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ModeloInvalido,
                    };
                }

                var respuesta = await db.Candidato.SingleOrDefaultAsync(m => m.IdCandidato == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.Candidato.Remove(respuesta);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                };
            }
            catch (Exception ex)
            {
                await GuardarLogService.SaveLogEntry(new LogEntryTranfer
                {
                    ApplicationName = Convert.ToString(Aplicacion.SwTH),
                    ExceptionTrace = ex.Message,
                    Message = Mensaje.Excepcion,
                    LogCategoryParametre = Convert.ToString(LogCategoryParameter.Critical),
                    LogLevelShortName = Convert.ToString(LogLevelParameter.ERR),
                    UserName = "",

                });
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }
        private Response ExisteEstudio(CandidatoEstudio candidatoEstudio)
        {
            var fechaGraduado = candidatoEstudio.FechaGraduado;
            var PersonaEstudiorespuesta = db.CandidatoEstudio.Where(p => p.FechaGraduado == fechaGraduado
            && p.IdCandidato == candidatoEstudio.IdCandidato
            && p.IdTitulo == candidatoEstudio.IdTitulo
            && p.Observaciones == candidatoEstudio.Observaciones
            && p.NoSenescyt == candidatoEstudio.NoSenescyt).FirstOrDefault();
            if (PersonaEstudiorespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = PersonaEstudiorespuesta,
                };

            }
            return new Response
            {
                IsSuccess = false,
                Resultado = PersonaEstudiorespuesta,
            };
        }
        private Response ExisteTrayectoria(CandidatoTrayectoriaLaboral candidatoTrayectoriaLaboral)
        {
            var fechaInicio = candidatoTrayectoriaLaboral.FechaInicio;
            var fechaFin = candidatoTrayectoriaLaboral.FechaFin;
            var Empresa = candidatoTrayectoriaLaboral.Institucion;
            var IdCandidato = candidatoTrayectoriaLaboral.IdCandidato;

            var TrayectoriaLaboralrespuesta = db.CandidatoTrayectoriaLaboral.Where(p => p.IdCandidato == IdCandidato
            && p.FechaFin == fechaFin
            && p.FechaInicio == fechaInicio
            && p.Institucion == Empresa).FirstOrDefault();

            if (TrayectoriaLaboralrespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = TrayectoriaLaboralrespuesta,
                };
            }

            return new Response
            {
                IsSuccess = false,
                Resultado = TrayectoriaLaboralrespuesta,
            };
        }
        //private Response Existe(Candidato Candidato)
        //{

        //    var Candidatorespuesta = db.Candidato.Where(p => p.IdPersona == Candidato.IdPersona).FirstOrDefault();
        //    if (Candidatorespuesta != null)
        //    {
        //        return new Response
        //        {
        //            IsSuccess = true,
        //            Message = Mensaje.ExisteRegistro,
        //            Resultado = Candidatorespuesta,
        //        };

        //    }

        //    return new Response
        //    {
        //        IsSuccess = false,
        //        Resultado = Candidatorespuesta,
        //    };
        //}
    }
}