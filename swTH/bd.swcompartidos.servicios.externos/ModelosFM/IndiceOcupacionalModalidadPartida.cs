using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class IndiceOcupacionalModalidadPartida
    {
        public IndiceOcupacionalModalidadPartida()
        {
            EmpleadoMovimiento = new HashSet<EmpleadoMovimiento>();
            PartidasFase = new HashSet<PartidasFase>();
        }

        public int IdIndiceOcupacionalModalidadPartida { get; set; }
        public int IdIndiceOcupacional { get; set; }
        public int? IdEmpleado { get; set; }
        public int? IdFondoFinanciamiento { get; set; }
        public int? IdTipoNombramiento { get; set; }
        public DateTime Fecha { get; set; }
        public decimal SalarioReal { get; set; }

        public virtual ICollection<EmpleadoMovimiento> EmpleadoMovimiento { get; set; }
        public virtual ICollection<PartidasFase> PartidasFase { get; set; }
        public virtual FondoFinanciamiento IdFondoFinanciamientoNavigation { get; set; }
        public virtual IndiceOcupacional IdIndiceOcupacionalNavigation { get; set; }
        public virtual TipoNombramiento IdTipoNombramientoNavigation { get; set; }
    }
}
