using bd.swth.entidades.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public partial class ActividadesGestionCambioViewModel
    {
        public string NombreUsuario { get; set; }

        public int IdActividadesGestionCambio { get; set; }
        public int IdDependencia { get; set; }
        public int IdEmpleado { get; set; }

        public string NombreDependencia { get; set; }
        public string NombreEmpleado { get; set; }
        
        public string Tarea { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Avance { get; set; }
        public string Observaciones { get; set; }

        public int ValorEstado { get; set; }
        public string Estado { get; set; }

    }
}
