using bd.swth.entidades.Negocio;
using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class EmpleadoEvaluadoViewModel
    {
        public int IdEmpleado { get; set; }

        public string  NombresApellidos { get; set; }

        public int IdPuesto { get; set; }
        public string NombrePuesto { get; set; }

        public List<TituloViewModel> ListaTituloProfesion { get; set; }

    }
}
