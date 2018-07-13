using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class FichaEmpleadoViewModel:DatosBasicosEmpleadoViewModel
    {
       
        public DateTime? FechaIngresoSectorPublico { get; set; }

        public int? ExtencionTelefonica { get; set; }

        public string Telefono { get; set; }

        public int MesesImposiciones { get; set; }

        public int DiasImposiciones { get; set; }

        public bool FondosReservas { get; set; }

        public int? IdBrigadaSSORol { get; set; }

        public int? IdBrigadaSSO { get; set; }

        public bool TrabajoSuperintendenciaBanco { get; set; }

        public bool DeclaracionJurada { get; set; }

        public string NombreUsuario { get; set; }

        public bool EsJefe { get; set; }

        public bool? Nepotismo { get; set; }

        public bool? OtrosIngresos { get; set; }

        public string IngresosOtraActividad { get; set; }

        public string Detalle { get; set; }

        public int? AnoDesvinculacion { get; set; }

        public string TipoRelacion { get; set; }
        

    }
}
