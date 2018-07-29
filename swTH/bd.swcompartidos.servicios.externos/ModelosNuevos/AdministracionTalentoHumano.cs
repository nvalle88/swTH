using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class AdministracionTalentoHumano
    {
        public AdministracionTalentoHumano()
        {
            InformeUath = new HashSet<InformeUath>();
            RequisitosNoCumple = new HashSet<RequisitosNoCumple>();
        }

        public int IdAdministracionTalentoHumano { get; set; }
        public int IdRolPuesto { get; set; }
        public int IdFormularioAnalisisOcupacional { get; set; }
        public int EmpleadoResponsable { get; set; }
        public DateTime? Fecha { get; set; }
        public bool? SeAplicaraPolitica { get; set; }
        public string Descripcion { get; set; }
        public bool? Cumple { get; set; }

        public virtual ICollection<InformeUath> InformeUath { get; set; }
        public virtual ICollection<RequisitosNoCumple> RequisitosNoCumple { get; set; }
        public virtual FormularioAnalisisOcupacional IdFormularioAnalisisOcupacionalNavigation { get; set; }
        public virtual RolPuesto IdRolPuestoNavigation { get; set; }
    }
}
