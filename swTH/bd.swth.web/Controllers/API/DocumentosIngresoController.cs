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
using bd.swth.entidades.ViewModels;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/DocumentosIngreso")]
    public class DocumentosIngresoController : Controller
    {
        private readonly SwTHDbContext db;

        public DocumentosIngresoController(SwTHDbContext db)
        {
            this.db = db;
        }

        // Controlador corregido nuevo sistema
        // última actualización: 17/09/2018

        #region Métodos para mantenimiento de DocumentosIngreso

        // GET: api/DocumentosIngreso
        [HttpGet]
        [Route("ListarDocumentosIngreso")]
        public async Task<List<DocumentosIngreso>> ListarDocumentosIngreso()
        {
            try
            {
                return await db.DocumentosIngreso
                    .Select(s=>new DocumentosIngreso {
                        IdDocumentosIngreso = s.IdDocumentosIngreso ,
                        Descripcion = s.Descripcion
                    })
                    .OrderBy(x => x.Descripcion)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<DocumentosIngreso>();
            }
        }


        /// <summary>
        ///  Obtiene una lista de los documentos ingresados,
        ///  requiere IdEmpleado
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        // POST: api/DocumentosIngreso
        [HttpPost]
        [Route("ListarDocumentosIngresoEmpleado")]
        public async Task<List<DocumentosIngresoEmpleado>> ListarDocumentosIngresoEmpleado([FromBody] Empleado empleado)
        {
            try
            {
                return await db.DocumentosIngresoEmpleado
                    .Where(x => x.IdEmpleado == empleado.IdEmpleado)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<DocumentosIngresoEmpleado>();
            }
        }


        /// <summary>
        /// Permite guardar un nuevo registro de Documentos ingreso
        /// </summary>
        /// <param name="DocumentosIngreso"></param>
        /// <returns></returns>
        // POST: api/DocumentosIngreso
        [HttpPost]
        [Route("InsertarDocumentosIngreso")]
        public async Task<Response> InsertarDocumentosIngreso([FromBody] DocumentosIngreso DocumentosIngreso)
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

                var respuesta = Existe(DocumentosIngreso);
                if (!respuesta.IsSuccess)
                {
                    DocumentosIngreso.Descripcion = DocumentosIngreso.Descripcion.ToString().ToUpper();

                    db.DocumentosIngreso.Add(DocumentosIngreso);
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
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }


        /// <summary>
        /// Obtiene 1 registro de DocumentosIngreso,
        /// según IdDocumentosIngreso
        /// </summary>
        /// <param name="documentosIngreso"></param>
        /// <returns></returns>
        // POST: api/DocumentosIngreso
        [HttpPost]
        [Route("ObtenerDocumentosIngresoPorId")]
        public async Task<Response> ObtenerDocumentosIngresoPorId([FromBody] DocumentosIngreso DocumentosIngreso)
        {

            try 
            {

                var modelo = await db.DocumentosIngreso
                    .Where(m => m.IdDocumentosIngreso == DocumentosIngreso.IdDocumentosIngreso)
                    .FirstOrDefaultAsync();

                if (DocumentosIngreso == null)
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
                    Resultado = modelo,
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


        /// <summary>
        /// Permite la edición de 1 registro de DocumentosIngreso,
        /// Necesario: IdDocumentosIngreso a editar
        /// </summary>
        /// <param name="documentosIngreso"></param>
        /// <returns></returns>
        // POST: api/DocumentosIngreso
        [HttpPost]
        [Route("EditarDocumentosIngreso")]
        public async Task<Response> EditarDocumentosIngreso([FromBody] DocumentosIngreso documentosIngreso)
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

                var existe = await db.DocumentosIngreso
                    .Where(w => w.IdDocumentosIngreso == documentosIngreso.IdDocumentosIngreso)
                    .FirstOrDefaultAsync();

                if (existe != null)
                {

                    var existeRegistroDiferente = await db.DocumentosIngreso
                        .Where(
                            w => w.Descripcion.ToString().ToUpper() == documentosIngreso.Descripcion.ToString().ToUpper()
                            && w.IdDocumentosIngreso != existe.IdDocumentosIngreso
                         )
                        .FirstOrDefaultAsync();

                    if (existeRegistroDiferente != null)
                    {

                        return new Response
                        {
                            IsSuccess = false,
                            Message = Mensaje.ExisteRegistro,
                        };
                    }

                    existe.Descripcion = documentosIngreso.Descripcion.ToString().ToUpper();
                    await db.SaveChangesAsync();

                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio,
                    };
                }

                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.RegistroNoEncontrado,
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


        private Response Existe(DocumentosIngreso DocumentosIngreso)
        {

            var documentoingresorespuesta = db.DocumentosIngreso
                .Where(p =>
                    p.Descripcion.ToUpper().TrimStart().TrimEnd() == DocumentosIngreso.Descripcion.ToUpper().TrimEnd().TrimStart()
                 )
                .FirstOrDefault();

            if (documentoingresorespuesta != null)
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
                Resultado = documentoingresorespuesta,
            };
        }


        /// <summary>
        /// Elimina 1 registro de documentosIngreso
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: api/DocumentosIngreso
        [HttpPost]
        [Route("EliminarDocumentosIngreso")]
        public async Task<Response> EliminarDocumentosIngreso([FromBody] DocumentosIngreso documentosIngreso)
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

                var modelo = await db.DocumentosIngreso
                    .Where(m => m.IdDocumentosIngreso == documentosIngreso.IdDocumentosIngreso)
                    .FirstOrDefaultAsync();

                if (modelo == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }

                db.DocumentosIngreso.Remove(modelo);
                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
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

        #endregion


        #region Métodos usados para mantenimiento de los documentos que ha ingresado el empleado

        // PUT: api/BasesDatos/5
        [HttpPost]
        [Route("ActualizarDocumentosEntregados")]
        public async Task<Response> ActualizarDocumentosEntregados([FromBody] ViewModelDocumentoIngresoEmpleado 
            viewModelDocumentoIngresoEmpleado)
        {

            
            try
            {

                var listaDocumentosExistentesEmpleado = await db.DocumentosIngresoEmpleado
                    .Where(w => w.IdEmpleado == viewModelDocumentoIngresoEmpleado.Distributivo.IdEmpleado)
                    .ToListAsync();

                var documentosSeleccionados = viewModelDocumentoIngresoEmpleado.DocumentosSeleccionados;

                var listaDocumentos = await db.DocumentosIngreso.ToListAsync();

                foreach (var item in listaDocumentos) {

                    var documento = listaDocumentosExistentesEmpleado
                        .Where(w => w.IdDocumentosIngreso == item.IdDocumentosIngreso)
                        .FirstOrDefault();

                    if (documento == null)
                    {
                        // se debe agregar el documento al empleado

                        var modelo = new DocumentosIngresoEmpleado
                        {
                            IdDocumentosIngreso = item.IdDocumentosIngreso,
                            Entregado = false,
                            IdEmpleado = viewModelDocumentoIngresoEmpleado.Distributivo.IdEmpleado
                        };

                        // se revisa si el documento fue seleccionado

                        var seleccionado = documentosSeleccionados
                            .Where(w=>w.ToString()== item.IdDocumentosIngreso.ToString())
                            .FirstOrDefault();

                        if (seleccionado != null) {
                            modelo.Entregado = true;
                        }

                        await db.DocumentosIngresoEmpleado.AddAsync(modelo);

                    }
                    else {
                        // se debe editar el estado del documento

                        documento.Entregado = false;

                        if (documentosSeleccionados != null) {

                            var seleccionado = documentosSeleccionados
                            .Where(w => w.ToString() == item.IdDocumentosIngreso.ToString())
                            .FirstOrDefault();

                            if (seleccionado != null)
                            {
                                documento.Entregado = true;
                            }
                        }

                        

                        db.Update(documento);
                    }

                    
                }

                await db.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.GuardadoSatisfactorio
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Excepcion
                };
            }

            
        }


        #endregion

        
    }
}