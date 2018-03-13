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

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/AntecedentesLaborales")]
    public class AntecedentesLaboralesController : Controller
    {
        private readonly SwTHDbContext db;

        public AntecedentesLaboralesController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/AntecedentesLaborales
        [HttpGet]
        [Route("ListarAntecedentesLaborales")]
        public async Task<List<AntecedentesLaborales>> GetAntecedentesLaborales()
        {

            try
            {
                return await db.AntecedentesLaborales.OrderBy(x => x.IdAntecedentesLaborales).ToListAsync();
            }
            catch (Exception ex)
            {
               
                return new List<AntecedentesLaborales>();
            }
        }

        


        // Post: api/AntecedentesLaborales
        [HttpPost]
        [Route("ListarAntecedentesLaboralesPorFicha")]
        public async Task<Response> GetAntecedentesLaboralesPorFicha([FromBody] int idFicha)
        {

            Response response = new entidades.Utils.Response();

            try
            {
                var lista = await db.AntecedentesLaborales.Where( x => x.IdFichaMedica == idFicha ).OrderBy(x => x.IdAntecedentesLaborales).ToListAsync();


                return new Response { IsSuccess = true, Resultado = lista };

            }
            catch (Exception ex)
            {

                return new Response { IsSuccess = false, Message = Mensaje.Excepcion };
            }


        }



        // GET: api/AntecedentesLaborales/5
        [HttpGet("{id}")]
        public async Task<Response> GetAntecedentesLaborales([FromRoute] int id)
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

                var AntecedentesLaborales = await db.AntecedentesLaborales.SingleOrDefaultAsync(m => m.IdAntecedentesLaborales == id);


                if (AntecedentesLaborales == null)
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
                    Resultado = AntecedentesLaborales,
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

        // PUT: api/AntecedentesLaborales/5
        [HttpPut("{id}")]
        public async Task<Response> PutAntecedentesLaborales([FromRoute] int id, [FromBody] AntecedentesLaborales AntecedentesLaborales)
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

                var existe = Existe(AntecedentesLaborales);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var Actualizar = await db.AntecedentesLaborales.Where(x => x.IdAntecedentesLaborales == id).FirstOrDefaultAsync();
                if (Actualizar != null)
                {
                    try
                    {

                        Actualizar.Empresa = AntecedentesLaborales.Empresa;
                        Actualizar.Cargo = AntecedentesLaborales.Cargo;
                        Actualizar.TiempoTrabajo = AntecedentesLaborales.TiempoTrabajo;
                        Actualizar.DetalleRiesgosExpuesto = AntecedentesLaborales.DetalleRiesgosExpuesto;
                        Actualizar.EppUtilizados = AntecedentesLaborales.EppUtilizados;
                        Actualizar.IdFichaMedica = AntecedentesLaborales.IdFichaMedica;
                        

                        db.AntecedentesLaborales.Update(Actualizar);

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
                    Message = Mensaje.Excepcion
                };
            }

        }

        // POST: api/AntecedentesLaborales
        [HttpPost]
        [Route("InsertarAntecedentesLaborales")]
        public async Task<Response> PostAntecedentesLaborales([FromBody] AntecedentesLaborales AntecedentesLaborales)
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

                var respuesta = Existe(AntecedentesLaborales);
                if (!respuesta.IsSuccess)
                {
                    db.AntecedentesLaborales.Add(AntecedentesLaborales);
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

        // DELETE: api/AntecedentesLaborales/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteAntecedentesLaborales([FromRoute] int id)
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

                var respuesta = await db.AntecedentesLaborales.SingleOrDefaultAsync(m => m.IdAntecedentesLaborales == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.AntecedentesLaborales.Remove(respuesta);
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
                    Message = Mensaje.BorradoNoSatisfactorio,
                };
            }

        }




        private Response Existe(AntecedentesLaborales AntecedentesLaborales)
        {
            var ale = AntecedentesLaborales.Empresa.ToUpper().TrimEnd().TrimStart();
            var alc = AntecedentesLaborales.Cargo.ToUpper().TrimEnd().TrimStart();
            var alt = AntecedentesLaborales.TiempoTrabajo.ToUpper().TrimEnd().TrimStart();
            var ald = AntecedentesLaborales.DetalleRiesgosExpuesto.ToUpper().TrimEnd().TrimStart();
            var aleu = AntecedentesLaborales.EppUtilizados.ToUpper().TrimEnd().TrimStart();
            var ali = AntecedentesLaborales.IdFichaMedica;


            var Respuesta = db.AntecedentesLaborales.Where(

                    p => p.Empresa.ToUpper().TrimEnd().TrimStart() == ale
                    && p.Cargo.ToUpper().TrimEnd().TrimStart() == alc
                    && p.TiempoTrabajo.ToUpper().TrimEnd().TrimStart() == alt
                    && p.DetalleRiesgosExpuesto.ToUpper().TrimEnd().TrimStart() == ald
                    && p.EppUtilizados.ToUpper().TrimEnd().TrimStart() == aleu
                    && p.IdFichaMedica == ali

                ).FirstOrDefault();

            if (Respuesta != null)
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
                Resultado = Respuesta,
            };
        }
    }
}