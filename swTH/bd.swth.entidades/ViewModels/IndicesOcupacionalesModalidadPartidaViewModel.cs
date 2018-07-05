using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class IndicesOcupacionalesModalidadPartidaViewModel
    {
        public int IdIndiceOcupacionalModalidadPartida { get; set; }
        public DateTime Fecha { get; set; }
        public decimal? SalarioActual { get; set; }
        public int IdIndiceOcupacional { get; set; }
        public int? IdEmpleado { get; set; }
        public int? IdFondoFinanciamiento { get; set; }
        public int? IdTipoNombramiento { get; set; }
        public string CodigoContrato { get; set; }
        public int? IdModalidadPartida { get; set; }
        public string NumeroPartidaIndividual { get; set; }
        public DateTime? FechaFin { get; set; }

        // Datos extra poner aquí debajo
        public IndiceOcupacionalViewModel IndiceOcupacionalViewModel { get; set; }

        public string NombreFondoFinanciamiento { get; set; }

        public string NombreTipoNombramiento { get; set; }
        public int? IdRelacionLaboral { get; set; }
        public string NombreRelacionLaboral { get; set; }

        public string NombreModalidadPartida { get; set; }

    }
}
