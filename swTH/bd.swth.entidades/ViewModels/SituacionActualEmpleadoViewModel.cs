using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class SituacionActualEmpleadoViewModel
    {
        public int IdEmpleado { get; set; }

        public int IdSucursal { get; set; }
        public string NombreSucursal { get; set; }

        public int IdDependencia { get; set; }
        public string NombreDependencia { get; set; }

        public int IdCargo { get; set; }
        public string NombreCargo { get; set; }

        public decimal Remuneracion { get; set; }

        public int IdIndiceOcupacionalModalidadPartida { get; set; }

    }
}
