using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class PlanGestionCambio
    {
        public PlanGestionCambio()
        {
            ActividadesGestionCambio = new HashSet<ActividadesGestionCambio>();
        }

        public int IdPlanGestionCambio { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int RealizadoPor { get; set; }
        public int AprobadoPor { get; set; }

        public virtual ICollection<ActividadesGestionCambio> ActividadesGestionCambio { get; set; }
    }
}
