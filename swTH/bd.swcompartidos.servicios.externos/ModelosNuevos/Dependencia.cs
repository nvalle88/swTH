using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Dependencia
    {
        public Dependencia()
        {
            ActivarPersonalTalentoHumano = new HashSet<ActivarPersonalTalentoHumano>();
            ActividadesGestionCambio = new HashSet<ActividadesGestionCambio>();
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
        public string Codigo { get; set; }
        public int? IdBodega { get; set; }

        public virtual ICollection<ActivarPersonalTalentoHumano> ActivarPersonalTalentoHumano { get; set; }
        public virtual ICollection<ActividadesGestionCambio> ActividadesGestionCambio { get; set; }
        public virtual ICollection<DependenciaDocumento> DependenciaDocumento { get; set; }
        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual ICollection<Evaluador> Evaluador { get; set; }
        public virtual ICollection<IndiceOcupacional> IndiceOcupacional { get; set; }
        public virtual ICollection<SituacionPropuesta> SituacionPropuesta { get; set; }
        public virtual Bodega IdBodegaNavigation { get; set; }
        public virtual Proceso IdProcesoNavigation { get; set; }
    }
}
