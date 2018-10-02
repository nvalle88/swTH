using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
  public  class IndiceOcupacionalViewModel
    {

        // Campos del índice ocupacional

        public int IdIndiceOcupacional { get; set; }
        public string DenominacionPuesto { get; set; }
        public string UnidadAdministrativa { get; set; }
        public int? IdRolPuesto { get; set; }
        public int? IdEscalaGrados { get; set; }
        public int? IdPartidaGeneral { get; set; }
        public int? IdAmbito { get; set; }
        public string Nivel { get; set; }
        public string Mision { get; set; }
        public string RelacionesInternasExternas { get; set; }
        public bool SinClasificar { get; set; }
        public decimal? RmusinClasificar { get; set; }
        public bool Activo { get; set; }
        

        public string NombreRolPuesto { get; set; }

        public string NombreEscalaGrados { get; set; }
        public decimal? Remuneracion { get; set; }
        public int? Grado { get; set; }

        public string NombreAmbito { get; set; }

        /*
        // Campos extra agregar aquí debajo
        public string NombreDependencia { get; set; }
        public string CodigoDependencia { get; set; }

        

        public string NombreManualPuesto { get; set; }
        public string DescripcionManualPuesto { get; set; }
        public string MisionManualPuesto { get; set; }
        public int? IdRelacionesInternasExternas { get; set; }
        public string NombreRelacionesInternasExternas { get; set; }
        public string DescripcionRelacionesInternasExternas { get; set; }

        

        

        public string NumeroPartidaGeneral { get; set; }
        
        */
    }
}
