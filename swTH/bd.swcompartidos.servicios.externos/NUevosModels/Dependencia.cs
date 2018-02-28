using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class Dependencia
    {
        public Dependencia()
        {
            DependenciaDocumento = new HashSet<DependenciaDocumento>();
            Empleado = new HashSet<Empleado>();
            Evaluador = new HashSet<Evaluador>();
            IndiceOcupacional = new HashSet<IndiceOcupacional>();
            SituacionPropuesta = new HashSet<SituacionPropuesta>();
        }

        public int IdDependencia { get; set; }
        public string Nombre { get; set; }
        public int IdSucursal { get; set; }
        public int? IdDependenciaPadre { get; set; }
        public int IdProceso { get; set; }

        public virtual ICollection<DependenciaDocumento> DependenciaDocumento { get; set; }
        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual ICollection<Evaluador> Evaluador { get; set; }
        public virtual ICollection<IndiceOcupacional> IndiceOcupacional { get; set; }
        public virtual ICollection<SituacionPropuesta> SituacionPropuesta { get; set; }
        public virtual Proceso IdProcesoNavigation { get; set; }
    }
}
