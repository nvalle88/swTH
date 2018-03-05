using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bd.swth.datos;
using bd.swth.entidades.Negocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bd.swth.web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Ambitos")]
    public class AmbitosController : Controller
    {

        private readonly SwTHDbContext db;

        public AmbitosController(SwTHDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("ListarAmbitos")]
        public async Task<List<Ambito>> ListarAmbitos()
        {
            try
            {
                var lista = await db.Ambito.OrderBy(x=>x.Nombre).ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                return new List<Ambito>();
            }
        }
    }
}