using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class ActividadesGestionCambio
    {
        public ActividadesGestionCambio()
        {
            AvanceGestionCambio = new HashSet<AvanceGestionCambio>();
        }

        public int IdActividadesGestionCambio { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? Indicador { get; set; }
        public bool? Porciento { get; set; }
        public string Descripcion { get; set; }
        public int IdPlanGestionCambio { get; set; }

        public virtual ICollection<AvanceGestionCambio> AvanceGestionCambio { get; set; }
        public virtual PlanGestionCambio IdPlanGestionCambioNavigation { get; set; }
    }
}
