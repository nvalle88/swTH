using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class IndiceOcupacionalModalidadPartida
    {
        public IndiceOcupacionalModalidadPartida()
        {
            EmpleadoMovimientoIdIndiceOcupacionalModalidadPartidaDesdeNavigation = new HashSet<EmpleadoMovimiento>();
            EmpleadoMovimientoIdIndiceOcupacionalModalidadPartidaHastaNavigation = new HashSet<EmpleadoMovimiento>();
        }

        public int IdIndiceOcupacionalModalidadPartida { get; set; }
        public int IdIndiceOcupacional { get; set; }
        public int? IdEmpleado { get; set; }
        public int? IdFondoFinanciamiento { get; set; }
        public int? IdTipoNombramiento { get; set; }
        public DateTime Fecha { get; set; }
        public decimal? SalarioReal { get; set; }
        public string CodigoContrato { get; set; }
        public string NumeroPartidaIndividual { get; set; }
        public int? IdModalidadPartida { get; set; }
        public DateTime? FechaFin { get; set; }

        public virtual ICollection<EmpleadoMovimiento> EmpleadoMovimientoIdIndiceOcupacionalModalidadPartidaDesdeNavigation { get; set; }
        public virtual ICollection<EmpleadoMovimiento> EmpleadoMovimientoIdIndiceOcupacionalModalidadPartidaHastaNavigation { get; set; }
        public virtual FondoFinanciamiento IdFondoFinanciamientoNavigation { get; set; }
        public virtual IndiceOcupacional IdIndiceOcupacionalNavigation { get; set; }
        public virtual ModalidadPartida IdModalidadPartidaNavigation { get; set; }
        public virtual TipoNombramiento IdTipoNombramientoNavigation { get; set; }
    }
}
