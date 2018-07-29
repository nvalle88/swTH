using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class EmpleadoMovimiento
    {
        public int IdEmpleadoMovimiento { get; set; }
        public int IdEmpleado { get; set; }
        public int? IdIndiceOcupacionalModalidadPartidaDesde { get; set; }
        public int? IdIndiceOcupacionalModalidadPartidaHasta { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public int IdAccionPersonal { get; set; }
        public int? IdIndiceOcupacional { get; set; }
        public int? IdFondoFinanciamiento { get; set; }
        public int? IdTipoNombramiento { get; set; }
        public decimal? SalarioReal { get; set; }
        public string CodigoContrato { get; set; }
        public string NumeroPartidaIndividual { get; set; }
        public int? IdModalidadPartida { get; set; }
        public bool EsJefe { get; set; }

        public virtual AccionPersonal IdAccionPersonalNavigation { get; set; }
        public virtual Empleado IdEmpleadoNavigation { get; set; }
        public virtual FondoFinanciamiento IdFondoFinanciamientoNavigation { get; set; }
        public virtual IndiceOcupacional IdIndiceOcupacionalNavigation { get; set; }
        public virtual IndiceOcupacionalModalidadPartida IdIndiceOcupacionalModalidadPartidaDesdeNavigation { get; set; }
        public virtual IndiceOcupacionalModalidadPartida IdIndiceOcupacionalModalidadPartidaHastaNavigation { get; set; }
        public virtual ModalidadPartida IdModalidadPartidaNavigation { get; set; }
        public virtual TipoNombramiento IdTipoNombramientoNavigation { get; set; }
    }
}
