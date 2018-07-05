using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
  public  class IndiceOcupacionalViewModel
    {
        
        // Campos del índice ocupacional

        public int IdIndiceOcupacional { get; set; }
        public int? IdDependencia { get; set; }
        public int? IdManualPuesto { get; set; }
        public int? IdRolPuesto { get; set; }
        public int? IdEscalaGrados { get; set; }
        public int? IdPartidaGeneral { get; set; }
        public int? IdAmbito { get; set; }
        public string Nivel { get; set; }

        // Campos extra agregar aquí debajo
        public string NombreDependencia { get; set; }
        public string CodigoDependencia { get; set; }

        public int IdSucursal { get; set; }
        public string NombreSucursal { get; set; }

        public string NombreManualPuesto { get; set; }
        public string DescripcionManualPuesto { get; set; }
        public string MisionManualPuesto { get; set; }
        public int? IdRelacionesInternasExternas { get; set; }
        public string NombreRelacionesInternasExternas { get; set; }
        public string DescripcionRelacionesInternasExternas { get; set; }

        public string NombreRolPuesto { get; set; }

        public string NombreEscalaGrados { get; set; }
        public decimal? Remuneracion { get; set; }
        public int? Grado { get; set; }

        public string NumeroPartidaGeneral { get; set; }
        public string  NombreAmbito { get; set; }

    }
}
