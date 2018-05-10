using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using bd.swth.entidades.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/InformeViaticos")]
    public class InformeViaticosController : Controller
    {
        private readonly SwTHDbContext db;

        public InformeViaticosController(SwTHDbContext db)
        {
            this.db = db;
        }
        [HttpPost]
        [Route("ListarInformeViaticos")]
        public async Task<List<InformeViatico>> ListarInformeViaticos([FromBody] InformeViatico informeViatico)
        {

            try
            {
                return await db.InformeViatico.Where(x=>x.IdItinerarioViatico==informeViatico.IdItinerarioViatico).ToListAsync();
            }
            catch (Exception ex)
            {                
                return new List<InformeViatico>();
            }
        }
        [HttpPost]
        [Route("InsertarInformeViatico")]
        public async Task<Response> InsertarInformeViatico([FromBody] InformeViatico informeViatico)
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
                else
                {
                    db.InformeViatico.Add(informeViatico);
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Mensaje.Satisfactorio
                    };
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
    }
}