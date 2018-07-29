using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class ActividadesEsenciales
    {
        public ActividadesEsenciales()
        {
            EvaluacionActividadesPuestoTrabajo = new HashSet<EvaluacionActividadesPuestoTrabajo>();
            IndiceOcupacionalActividadesEsenciales = new HashSet<IndiceOcupacionalActividadesEsenciales>();
        }

        public int IdActividadesEsenciales { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<EvaluacionActividadesPuestoTrabajo> EvaluacionActividadesPuestoTrabajo { get; set; }
        public virtual ICollection<IndiceOcupacionalActividadesEsenciales> IndiceOcupacionalActividadesEsenciales { get; set; }
    }
}
