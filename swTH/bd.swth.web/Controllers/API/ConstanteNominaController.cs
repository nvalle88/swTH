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
    [Route("api/ConstanteNomina")]
    public class ConstanteNominaController : Controller
    {
        private readonly SwTHDbContext db;

        public ConstanteNominaController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarConstanteNomina")]
        public async Task<List<ConstanteNomina>> ListarConstanteNomina()
        {
            try
            {
                return await db.ConstanteNomina.OrderBy(x => x.Constante).ToListAsync();
            }
            catch (Exception)
            {
               
                return new List<ConstanteNomina>();
            }
        }
    }
}