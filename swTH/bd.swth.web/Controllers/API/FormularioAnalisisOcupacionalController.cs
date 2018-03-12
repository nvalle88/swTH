using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bd.swth.datos;
using bd.swth.entidades.Enumeradores;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/FormularioAnalisisOcupacional")]
    public class FormularioAnalisisOcupacionalController : Controller
    {
        private readonly SwTHDbContext db;

        public FormularioAnalisisOcupacionalController(SwTHDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        [Route("InsertarFormularioAnalisisOcupacional")]
        public async Task<Response> PostFormularioAnalisisOcupacional([FromBody] FormularioAnalisisOcupacional formularioAnalisisOcupacional)
        {
            try
            {
                int fecha = DateTime.Now.Year;
                if (ModelState.IsValid)
                {
                    var empleado =  db.FormularioAnalisisOcupacional.Where(x => x.IdEmpleado == formularioAnalisisOcupacional.IdEmpleado && fecha == formularioAnalisisOcupacional.Anio).FirstOrDefault();
                    
                    if (empleado == null)
                    {
                        db.FormularioAnalisisOcupacional.Add(formularioAnalisisOcupacional);
                        await db.SaveChangesAsync();
                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.Satisfactorio
                        };
                    }
                    else
                    {
                        formularioAnalisisOcupacional.IdFormularioAnalisisOcupacional = empleado.IdFormularioAnalisisOcupacional;
                        formularioAnalisisOcupacional.IdEmpleado = empleado.IdEmpleado;
                        formularioAnalisisOcupacional.MisionPuesto = empleado.MisionPuesto;
                        formularioAnalisisOcupacional.Estado = empleado.Estado;
                        db.FormularioAnalisisOcupacional.Update(formularioAnalisisOcupacional);
                        await db.SaveChangesAsync();
                        return new Response
                        {
                            IsSuccess = true,
                            Message = Mensaje.Satisfactorio
                        };
                    }
                    
                }
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
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
    }
}