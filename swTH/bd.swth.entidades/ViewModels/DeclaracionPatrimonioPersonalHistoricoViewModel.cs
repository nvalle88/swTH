using System;
using System.Collections.Generic;
using System.Text;
using bd.swth.entidades.Negocio;

namespace bd.swth.entidades.ViewModels
{
    public class DeclaracionPatrimonioPersonalHistoricoViewModel
    {
        public int IdEmpleado { get; set; }

        public string NombrePersona { get; set; }
        public string ApellidoPersona { get; set; }

        public List<DeclaracionPatrimonioPersonal> ListaDeclaracionPatrimonioPersonal { get; set; }
    }
}
