using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.Utils;
using bd.log.guardar.Servicios;
using bd.log.guardar.ObjectTranfer;
using bd.swth.entidades.Enumeradores;
using bd.log.guardar.Enumeradores;
using bd.swth.entidades.ViewModels;

namespace bd.swth.web.Controllers.API
{

    //public class DependenciaViewModel
    //{
    //    public int IdDependencia { get; set; }
    //    public string NombreDependencia { get; set; }
    //    public string NombreSucursal { get; set; }
    //    public string NombreDependenciaPadre { get; set; }
    //    public string NombreProceso  { get; set; }
    //}

    [Produces("application/json")]
    [Route("api/Dependencias")]
    public class DependenciasController : Controller
    {
        private readonly SwTHDbContext db;

        public DependenciasController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/Dependencias
        [HttpGet]
        [Route("ListarDependencias")]
        public async Task<List<DependenciaViewModel>> GetDependencia()
        {
            try
            {

                var listaDependencia = await db.Dependencia.Include(x=>x.DependenciaPadre).Include(x=>x.Sucursal).ToListAsync();

                var listaSalida = new List<DependenciaViewModel>();

                foreach (var item in listaDependencia)
                {

                    var padre = "";
                    if (item.DependenciaPadre == null)
                    {
                        padre = "No tiene dependencia padre";
                    }
                    else
                    {
                        padre = item.DependenciaPadre.Nombre;
                    }

                    var dependenciaSalida = new DependenciaViewModel
                    {
                        IdDependencia =item.IdDependencia,
                        NombreDependencia =item.Nombre,
                        NombreSucursal = item.Sucursal.Nombre,
                        NombreDependenciaPadre=padre
                        
                    };

                    var proceso =await db.Proceso.Where(x => x.IdProceso == item.IdProceso).FirstOrDefaultAsync();

                    dependenciaSalida.NombreProceso = proceso.Nombre;

                    listaSalida.Add(dependenciaSalida);

                }
                return  listaSalida;
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
                return new List<DependenciaViewModel>();
            }
        }


        [HttpPost]
        [Route("ListarDependenciaporSucursal")]
        public async Task<List<Dependencia>> GetDependenciabySucursal([FromBody] Sucursal sucursal)
        {
            try
            {
                return await db.Dependencia.Include(c => c.Sucursal).Where(x => x.IdSucursal == sucursal.IdSucursal).OrderBy(x => x.Nombre).ToListAsync();
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
                return new List<Dependencia>();
            }
        }

        [HttpPost]
        [Route("ListarDependenciaporSucursalPadreHijo")]
        public async Task<Dependencia> GetDependenciabySucursalPadreHijo([FromBody] Sucursal sucursal)
        {
            try
            {
                var listadependeciasporsucursal = await db.Dependencia.Include(c => c.Sucursal).Where(x => x.IdSucursal == sucursal.IdSucursal).OrderBy(x => x.Nombre).ToListAsync();
                var jefe = listadependeciasporsucursal.Where(x => x.IdDependenciaPadre == 0).FirstOrDefault();
                SetChildren(jefe, listadependeciasporsucursal);
                return jefe;
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
                return new Dependencia();
            }
        }

        private void SetChildren(Dependencia dependencia, List<Dependencia> listadependencias)
        {
            var hijos = listadependencias.Where(x => x.IdDependenciaPadre == dependencia.IdDependencia).ToList();
            if (hijos.Count > 0)
            {
                foreach (var hijo in hijos)
                {
                    SetChildren(hijo, listadependencias);
                    dependencia.Dependencia1.Add(hijo);
                }
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetDependencia([FromRoute] int id)
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

                var Depedencia = await db.Dependencia.SingleOrDefaultAsync(m => m.IdDependencia == id);

                if (Depedencia == null)
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
                    Resultado = Depedencia,
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

        // PUT: api/Dependencias/5
        [HttpPut("{id}")]
        public async Task<Response> PutDependencia([FromRoute] int id, [FromBody] DependenciaViewModel dependenciaViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ModeloInvalido
                    };
                }

                var dependencia = new Dependencia
                {
                    IdDependencia= dependenciaViewModel.IdDependencia,
                    Nombre = dependenciaViewModel.NombreDependencia,
                    IdSucursal= dependenciaViewModel.IdSucursal,
                    IdDependenciaPadre = dependenciaViewModel.IdDependenciaPadre,
                    IdProceso = dependenciaViewModel.IdProceso
                };

                var existe = Existe(dependencia);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var dependenciaActualizar = await db.Dependencia.Where(x => x.IdDependencia == id).FirstOrDefaultAsync();

                if (dependenciaActualizar != null)
                {
                    try
                    {
                        if (dependencia.Nombre != dependenciaActualizar.Nombre
                            || dependencia.IdSucursal != dependenciaActualizar.IdSucursal
                            || dependencia.IdDependenciaPadre != dependenciaActualizar.IdDependenciaPadre
                            || dependencia.IdProceso != dependenciaActualizar.IdProceso)
                        {
                            dependenciaActualizar.Nombre = dependencia.Nombre;
                            dependenciaActualizar.IdSucursal = dependencia.IdSucursal;
                            dependenciaActualizar.IdDependenciaPadre = dependencia.IdDependenciaPadre;
                            dependenciaActualizar.IdProceso = dependencia.IdProceso;
                            await db.SaveChangesAsync();

                            return new Response
                            {
                                IsSuccess = true,
                                Message = Mensaje.Satisfactorio,
                            };
                        }
                      

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




                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ExisteRegistro
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };
            }
        }

        

             [HttpPost]
        [Route("ListarPadresPorSucursal")]
        public async Task<List<Dependencia>> ListarPadresPorSucursal([FromBody] Dependencia dependencia)
        {
            try
            {
                return await db.Dependencia.Where(x => x.IdSucursal == dependencia.IdSucursal).OrderBy(x => x.Nombre).ToListAsync();
                                                    
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
                return new List<Dependencia>();
            }
        }

        // POST: api/Dependencias
        [HttpPost]
        [Route("InsertarDependencia")]
        public async Task<Response> PostDependencia([FromBody] DependenciaViewModel dependenciaViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message =Mensaje.ModeloInvalido,
                    };
                }

                var dependencia = new Dependencia()
                {
                    Nombre = dependenciaViewModel.NombreDependencia,
                    IdSucursal = dependenciaViewModel.IdSucursal,
                    IdDependenciaPadre= dependenciaViewModel.IdDependenciaPadre,
                    IdProceso = dependenciaViewModel.IdProceso

                };

                var respuesta = Existe(dependencia);
                if (!respuesta.IsSuccess)
                {
                    db.Dependencia.Add(dependencia);
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
                    Message = Mensaje.ExisteRegistro
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

        // DELETE: api/Dependencias/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteDependencia([FromRoute] int id)
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

                var dependencia = await db.Dependencia.SingleOrDefaultAsync(m => m.IdDependencia == id);
                if (dependencia == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }

                db.Dependencia.Remove(dependencia);
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

        private Response Existe(Dependencia dependencia)
        {
            var nombre = dependencia.Nombre.ToUpper().TrimEnd().TrimStart();
            var dependenciarespuesta = db.Dependencia.Where(p => p.Nombre.ToUpper().TrimEnd().TrimStart()==nombre
                                                             && p.IdDependenciaPadre==dependencia.IdDependenciaPadre
                                                             && p.IdSucursal==dependencia.IdSucursal
                                                             && p.IdProceso == dependencia.IdProceso).FirstOrDefault();
            if (dependenciarespuesta != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.ExisteRegistro,
                    Resultado = dependenciarespuesta,
                };

            }

            return new Response
            {
                IsSuccess = false,
                Resultado = dependenciarespuesta,
            };
        }
    }
}