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

                //var listaDependencia = await db.Dependencia.Include(x=>x.DependenciaPadre).Include(x=>x.Sucursal).Include(x=>x.Sucursal.Nombre).ToListAsync();
                var listaDependencia = await db.Dependencia.Select(x => new DependenciaViewModel
                {
                    IdDependencia = x.IdDependencia,
                    NombreDependencia = x.Nombre,
                    NombreSucursal = x.Sucursal.Nombre,
                    Codigo = x.Codigo,
                    Ciudad = x.Sucursal.Ciudad.Nombre,
                    NombreDependenciaPadre =x.DependenciaPadre.Nombre,
                    IdProceso = x.IdProceso
                    
                    
                }).OrderByDescending(x=>x.Codigo).ToListAsync();

                var listaSalida = new List<DependenciaViewModel>();

                foreach (var item in listaDependencia)
                {

                    var padre = "";
                    if (item.NombreDependenciaPadre == null)
                    {
                        padre = "No tiene dependencia padre";
                    }
                    else
                    {
                        padre = item.NombreDependenciaPadre;
                    }

                    var dependenciaSalida = new DependenciaViewModel
                    {
                        IdDependencia = item.IdDependencia,
                        NombreDependencia = item.NombreDependencia,
                        NombreSucursal = item.NombreSucursal,
                        NombreDependenciaPadre = padre,
                        Codigo = item.Codigo,
                        Ciudad = item.Ciudad

                    };

                    var proceso = await db.Proceso.Where(x => x.IdProceso == item.IdProceso).FirstOrDefaultAsync();

                    dependenciaSalida.NombreProceso = proceso.Nombre;

                    listaSalida.Add(dependenciaSalida);

                }
                return listaSalida;
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
                return await db.Dependencia
                    .Include(c => c.Sucursal)
                    .Where(w => w.IdSucursal == sucursal.IdSucursal)
                    .OrderBy(o => o.Nombre)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                
                return new List<Dependencia>();
            }
        }

        [HttpPost]
        [Route("ListarDependenciaporSucursalPadreHijo")]
        public async Task<Dependencia> GetDependenciabySucursalPadreHijo([FromBody] Sucursal sucursal)
        {
            try
            {
                var listaDependencias = await db.Dependencia.Where(x => x.IdSucursal == sucursal.IdSucursal).OrderBy(x => x.Nombre).ToListAsync();

                // ** Buscar dependencia padre
                var padre = listaDependencias.Where(w => w.IdDependenciaPadre == 0).FirstOrDefault();

                // ** Cada item se convierte en padre y se buscan sus hijos (así se llenan todos los padres)
                foreach (var item in listaDependencias)
                {

                    var listaHijos = listaDependencias.Where(w => w.IdDependenciaPadre == item.IdDependencia).ToList();
                    item.Dependencia1 = new List<Dependencia>(listaHijos);
                }

                // ** árbol de dependencias **

                // añadir el padre al árbol
                var arbol = padre;

                return arbol;
            }
            catch (Exception ex)
            {
                return new Dependencia();
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
                    IdDependencia = dependenciaViewModel.IdDependencia,
                    Nombre = dependenciaViewModel.NombreDependencia,
                    IdSucursal = dependenciaViewModel.IdSucursal,
                    IdDependenciaPadre = dependenciaViewModel.IdDependenciaPadre,
                    IdProceso = dependenciaViewModel.IdProceso,
                    Codigo = dependenciaViewModel.Codigo
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
                            || dependencia.IdProceso != dependenciaActualizar.IdProceso
                            || dependencia.Codigo != dependenciaActualizar.Codigo)
                        {
                            dependenciaActualizar.Nombre = dependencia.Nombre;
                            dependenciaActualizar.IdSucursal = dependencia.IdSucursal;
                            dependenciaActualizar.IdDependenciaPadre = dependencia.IdDependenciaPadre;
                            dependenciaActualizar.IdProceso = dependencia.IdProceso;
                            dependenciaActualizar.Codigo = dependencia.Codigo;
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
                        Message = Mensaje.ModeloInvalido,
                    };
                }

                var dependencia = new Dependencia()
                {
                    Nombre = dependenciaViewModel.NombreDependencia,
                    IdSucursal = dependenciaViewModel.IdSucursal,
                    IdDependenciaPadre = dependenciaViewModel.IdDependenciaPadre,
                    IdProceso = dependenciaViewModel.IdProceso,
                    Codigo = dependenciaViewModel.Codigo

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
            var dependenciarespuesta = db.Dependencia.Where(p =>(p.Codigo==dependencia.Codigo) || (p.Nombre.ToUpper().TrimEnd().TrimStart() == nombre
                                                             && p.IdDependenciaPadre == dependencia.IdDependenciaPadre
                                                             && p.IdSucursal == dependencia.IdSucursal
                                                             && p.IdProceso == dependencia.IdProceso
                                                             && p.Codigo == dependencia.Codigo)).FirstOrDefault();
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


        /// <summary>
        ///  Obtiene la dependencia actual, y las dependencias hijas (DependenciaDatosViewModel)
        ///  a partir del NombreUsuario
        /// </summary>
        /// <param name="idFiltrosViewModel"></param>
        /// <returns></returns>
        // Post: api/Dependencias
        [HttpPost]
        [Route("ObtenerDependenciaDatosViewModelPorUsuarioActual")]
        public async Task<DependenciaDatosViewModel> ObtenerDependenciaDatosViewModelPorUsuarioActual([FromBody]IdFiltrosViewModel idFiltrosViewModel)
        {
            try
            {

                var empleado = await db.Empleado
                    .Where(w => w.NombreUsuario == idFiltrosViewModel.NombreUsuario).FirstOrDefaultAsync();

                var dependencia = await db.Dependencia
                    .Include(i => i.Sucursal)
                    .Where(w => w.IdDependencia == empleado.IdDependencia).FirstOrDefaultAsync();

                var modelo = new DependenciaDatosViewModel
                {

                    IdDependencia = dependencia.IdDependencia,
                    IdSucursal = dependencia.IdSucursal,
                    NombreDependencia = dependencia.Nombre,
                    NombreSucursal = dependencia.Sucursal.Nombre,

                    DatosBasicosEmpleadoJefeViewModel = db.Empleado
                        .Where(we =>
                            we.IdDependencia == dependencia.IdDependencia
                            && we.EsJefe == true
                            && we.Activo == true
                        )
                        .Select(se => new DatosBasicosEmpleadoViewModel
                        {
                            IdEmpleado = se.IdEmpleado,
                            IdPersona = se.IdPersona,
                            Nombres = se.Persona.Nombres,
                            Apellidos = se.Persona.Apellidos

                        }

                        ).FirstOrDefault()
                    ,


                    ListaDependenciasHijas = await ObtenerDependenciasHijas(dependencia.IdDependencia),

                    ListaEmpleadosDependencia = await db.Empleado
                    .Where(wl =>
                    wl.Activo == true
                    && wl.IdDependencia == dependencia.IdDependencia
                    && wl.EsJefe == false
                    )
                    .Select(s1 => new DatosBasicosEmpleadoViewModel
                    {

                        IdEmpleado = s1.IdEmpleado,
                        IdPersona = s1.IdPersona,
                        Nombres = s1.Persona.Nombres,
                        Apellidos = s1.Persona.Apellidos,
                        Identificacion = s1.Persona.Identificacion
                    }
                    )
                    .ToListAsync()

                };


                return modelo;

            }
            catch (Exception ex)
            {

                return new DependenciaDatosViewModel();
            }
        }


        /// <summary>
        ///  Obtiene el DependenciaDatosViewModel a partir del idDependencia
        ///  Necesario: idDependencia
        /// </summary>
        /// <param name="idFiltrosViewModel"></param>
        /// <returns></returns>
        // Post: api/Dependencias
        [HttpPost]
        [Route("ObtenerDependenciaDatosViewModelPorIdDependencia")]
        public async Task<DependenciaDatosViewModel> ObtenerDependenciaDatosViewModelPorIdDependencia([FromBody]IdFiltrosViewModel idFiltrosViewModel)
        {
            try
            {

                var dependencia = await db.Dependencia
                    .Include(i => i.Sucursal)
                    .Where(w => w.IdDependencia == idFiltrosViewModel.IdDependencia).FirstOrDefaultAsync();

                var modelo = new DependenciaDatosViewModel
                {

                    IdDependencia = dependencia.IdDependencia,
                    IdSucursal = dependencia.IdSucursal,
                    NombreDependencia = dependencia.Nombre,
                    NombreSucursal = dependencia.Sucursal.Nombre,

                    DatosBasicosEmpleadoJefeViewModel = db.Empleado
                        .Where(we =>
                            we.IdDependencia == dependencia.IdDependencia
                            && we.EsJefe == true
                            && we.Activo == true
                        )
                        .Select(se => new DatosBasicosEmpleadoViewModel
                        {
                            IdEmpleado = se.IdEmpleado,
                            IdPersona = se.IdPersona,
                            Nombres = se.Persona.Nombres,
                            Apellidos = se.Persona.Apellidos

                        }

                        ).FirstOrDefault()
                    ,


                    ListaDependenciasHijas = await ObtenerDependenciasHijas(dependencia.IdDependencia),

                    ListaEmpleadosDependencia = await db.Empleado
                    .Where(wl =>
                    wl.Activo == true
                    && wl.IdDependencia == dependencia.IdDependencia
                    && wl.EsJefe == false
                    )
                    .Select(s1 => new DatosBasicosEmpleadoViewModel
                    {

                        IdEmpleado = s1.IdEmpleado,
                        IdPersona = s1.IdPersona,
                        Nombres = s1.Persona.Nombres,
                        Apellidos = s1.Persona.Apellidos,
                        Identificacion = s1.Persona.Identificacion
                    }
                    )
                    .ToListAsync()

                };


                return modelo;

            }
            catch (Exception ex)
            {

                return new DependenciaDatosViewModel();
            }
        }


        public async Task<List<DependenciaDatosViewModel>> ObtenerDependenciasHijas(int idDependencia)
        {

            try
            {
                var lista = await db.Dependencia.Where(w => w.IdDependenciaPadre == idDependencia)
                    .Select(s => new DependenciaDatosViewModel
                    {
                        IdDependencia = s.IdDependencia,
                        IdSucursal = s.IdSucursal,
                        NombreDependencia = s.Nombre,
                        NombreSucursal = s.Sucursal.Nombre,

                        DatosBasicosEmpleadoJefeViewModel = db.Empleado
                        .Where(we =>
                            we.IdDependencia == s.IdDependencia
                            && we.EsJefe == true
                            && we.Activo == true
                        )
                        .Select(se => new DatosBasicosEmpleadoViewModel
                        {
                            IdEmpleado = se.IdEmpleado,
                            IdPersona = se.IdPersona,
                            Nombres = se.Persona.Nombres,
                            Apellidos = se.Persona.Apellidos

                        }

                        ).FirstOrDefault()

                    }
                    )
                    .ToListAsync();

                return lista;
            }
            catch (Exception)
            {

                return new List<DependenciaDatosViewModel>();
            }

        }

    }
}