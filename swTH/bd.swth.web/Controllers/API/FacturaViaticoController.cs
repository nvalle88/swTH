using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.Utils;
using bd.swth.entidades.ViewModels;
using bd.swth.servicios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/FacturaViatico")]
    public class FacturaViaticoController : Controller
    {
        private readonly IUploadFileService uploadFileService;
        private readonly SwTHDbContext db;

        public FacturaViaticoController(SwTHDbContext db, IUploadFileService uploadFileService)
        {
            this.uploadFileService = uploadFileService;
            this.db = db;
        }

        [HttpPost]
        [Route("ListarFacturas")]
        public async Task<List<FacturaViatico>> ListarFacturas([FromBody]FacturaViatico facturaViatico)
        {
            //Persona persona = new Persona();
            try
            {

                return await db.FacturaViatico.Where(x => x.IdItinerarioViatico == facturaViatico.IdItinerarioViatico).ToListAsync();

            }
            catch (Exception ex)
            {
                
                return new List<FacturaViatico>();
            }
        }

        // GET: api/BasesDatos/5
        [HttpGet("{id}")]
        public async Task<Response> GetFacturaViatico([FromRoute] int id)
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

                var facturavia = await db.FacturaViatico.SingleOrDefaultAsync(m => m.IdFacturaViatico == id);

                if (facturavia == null)
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
                    Resultado = facturavia,
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

        //PUT: api/BasesDatos/5
        [HttpPut("{id}")]
        public async Task<Response> PutFacturaViatico([FromRoute] int id, [FromBody] FacturaViatico facturaViatico)
        {
            try
            {
                var existe = Existe(facturaViatico);
                if (existe.IsSuccess)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ExisteRegistro,
                    };
                }

                var facturaViaticoActualizar = await db.FacturaViatico.Where(x => x.IdFacturaViatico == id).FirstOrDefaultAsync();

                if (facturaViaticoActualizar != null)
                {
                    try
                    {
                        facturaViaticoActualizar.NumeroFactura = facturaViatico.NumeroFactura;
                        facturaViaticoActualizar.FechaFactura = facturaViatico.FechaFactura;
                        facturaViaticoActualizar.ValorTotalFactura = facturaViatico.ValorTotalFactura;
                        facturaViaticoActualizar.FechaFactura = facturaViatico.FechaFactura;
                        facturaViaticoActualizar.Observaciones = facturaViatico.Observaciones;
                        facturaViaticoActualizar.IdItemViatico = facturaViatico.IdItemViatico;
                        facturaViaticoActualizar.IdItinerarioViatico = facturaViatico.IdItinerarioViatico;
                        db.FacturaViatico.Update(facturaViaticoActualizar);
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
        [Route("InsertarFacturas")]
        public async Task<Response> InsertarFacturas([FromBody] ViewModelFacturaViatico viewModelFacturaViatico)
        {

            try
            {
                var documenttransfer = new FacturaViatico
                {
                    NumeroFactura = viewModelFacturaViatico.NumeroFactura,
                    IdItinerarioViatico = viewModelFacturaViatico.IdItinerarioViatico,
                    IdItemViatico = viewModelFacturaViatico.IdItemViatico,
                    FechaFactura = viewModelFacturaViatico.FechaFactura,
                    ValorTotalFactura = viewModelFacturaViatico.ValorTotalFactura,
                    Observaciones = viewModelFacturaViatico.Observaciones,
                    Url = viewModelFacturaViatico.Url,
                    

                };

                var respuesta = Existe(documenttransfer);
                if (!respuesta.IsSuccess)
                {
                    db.FacturaViatico.Add(documenttransfer);
                    await db.SaveChangesAsync();

                    var id = documenttransfer.IdFacturaViatico;

                    await uploadFileService.UploadFile(viewModelFacturaViatico.Fichero, "FacturaViaticos", Convert.ToString(id), ".pdf");


                    var seleccionado = db.FacturaViatico.Find(documenttransfer.IdFacturaViatico);
                    seleccionado.Url = string.Format("{0}/{1}.{2}", "FacturaViaticos", Convert.ToString(id), "pdf");
                    db.FacturaViatico.Update(seleccionado);
                    db.SaveChanges();
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
        // POST: api/BasesDatos
        

        // DELETE: api/BasesDatos/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteFacturaViatico([FromRoute] int id)
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

                var respuesta = await db.FacturaViatico.SingleOrDefaultAsync(m => m.IdFacturaViatico == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                db.FacturaViatico.Remove(respuesta);
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

        private Response Existe(FacturaViatico facturaViatico)
        {
            var bdd1 = facturaViatico.IdFacturaViatico;
            var bdd2 = facturaViatico.NumeroFactura;
            var bdd3 = facturaViatico.ValorTotalFactura;
            var bdd4 = facturaViatico.FechaFactura;
            var facturaViaticorespuesta = db.FacturaViatico.Where(p => p.IdFacturaViatico == bdd1
            && p.NumeroFactura == bdd2
            && p.ValorTotalFactura == bdd3
            && p.FechaFactura == bdd4
            //&& p.FechaHasta == bdd5
            //&& p.Valor == bdd6
            //&& p.HoraSalida == bdd7
            //&& p.HoraLlegada == bdd8
            ).FirstOrDefault();
            if (facturaViaticorespuesta != null)
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
                Resultado = facturaViaticorespuesta,
            };
        }
        [HttpPost]
        [Route("GetFile")]
        public async Task<Response> GetFile([FromBody] ViewModelFacturaViatico viewModelFacturaViatico)
        {

            try
            {
                var respuestaFile = uploadFileService.GetFile("FacturaViaticos", Convert.ToString(viewModelFacturaViatico.IdFacturaViatico), ".pdf");

                var dato = await db.FacturaViatico.Where(x => x.IdFacturaViatico == viewModelFacturaViatico.IdFacturaViatico).FirstOrDefaultAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = "Factura #" + dato.IdFacturaViatico + ", archivo adjunto",
                    Resultado = respuestaFile,
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