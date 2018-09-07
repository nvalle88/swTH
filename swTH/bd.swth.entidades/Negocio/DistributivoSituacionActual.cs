using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace bd.swth.entidades.Negocio
{
    public partial class DistributivoSituacionActual
    {
        public int IdDistributivoSituacionActual { get; set; }
        public int IdIndiceOcupacionalModalidadPartida { get; set; }
        public int IdEmpleado { get; set; }
        public int IdFondoFinanciamiento { get; set; }
        public int IdTipoNombramiento { get; set; }
        public string Observacion { get; set; }
        public int? IdEmpleadoMovimiento { get; set; }

        public virtual Empleado Empleado { get; set; }
        public virtual EmpleadoMovimiento EmpleadoMovimiento { get; set; }
        public virtual FondoFinanciamiento FondoFinanciamiento { get; set; }
        public virtual IndiceOcupacionalModalidadPartida IndiceOcupacionalModalidadPartida { get; set; }
        public virtual TipoNombramiento TipoNombramiento { get; set; }
        

    }
}
