using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class ActividadesAnalisisOcupacional
    {
        public int IdActividadesAnalisisOcupacional { get; set; }
        public int IdFormularioAnalisisOcupacional { get; set; }
        public string Actividades { get; set; }
        public bool? Cumple { get; set; }

        public virtual FormularioAnalisisOcupacional IdFormularioAnalisisOcupacionalNavigation { get; set; }
    }
}
