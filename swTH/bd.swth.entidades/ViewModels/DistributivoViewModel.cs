using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class DistributivoViewModel
    {
        public int IdDependencia { get; set; }
        public string NombreDependencia { get; set; }

        public int IdRolPuesto { get; set; }
        public string NombreRolPuesto { get; set; }

        public int IdModalidadPartida { get; set; }
        public string NombreModalidadPartida { get; set; }
        public string GrupoOcupacional { get; set; }
        
        public decimal? RMU { get; set; }

        public int Grado { get; set; }

        public int CantidadEmpleados { get; set; }
    }
}
