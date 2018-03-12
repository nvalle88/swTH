using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class FormularioAnalisisOcupacional
    {
        public FormularioAnalisisOcupacional()
        {
            ActividadesAnalisisOcupacional = new HashSet<ActividadesAnalisisOcupacional>();
            AdministracionTalentoHumano = new HashSet<AdministracionTalentoHumano>();
            ValidacionInmediatoSuperior = new HashSet<ValidacionInmediatoSuperior>();
        }

        public int IdFormularioAnalisisOcupacional { get; set; }
        public bool? InternoMismoProceso { get; set; }
        public bool? InternoOtroProceso { get; set; }
        public bool? ExternosCiudadania { get; set; }
        public bool? ExtPersJurídicasPubNivelNacional { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int Anio { get; set; }
        public string MisionPuesto { get; set; }

        public virtual ICollection<ActividadesAnalisisOcupacional> ActividadesAnalisisOcupacional { get; set; }
        public virtual ICollection<AdministracionTalentoHumano> AdministracionTalentoHumano { get; set; }
        public virtual ICollection<ValidacionInmediatoSuperior> ValidacionInmediatoSuperior { get; set; }
    }
}
