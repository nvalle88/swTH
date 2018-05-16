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
    [Route("api/FuncionNomina")]
    public class FuncionNominaController : Controller
    {
        private readonly SwTHDbContext db;

        public FuncionNominaController(SwTHDbContext db)
        {
            this.db = db;
        }

        // GET: api/BasesDatos
        [HttpGet]
        [Route("ListarFuncionNomina")]
        public async Task<List<FuncionNomina>> ListarConstanteNomina()
        {
            try
            {
                return await db.FuncionNomina.OrderBy(x => x.Descripcion).ToListAsync();
            }
            catch (Exception)
            {

                return new List<FuncionNomina>();
            }
        }
    }
}