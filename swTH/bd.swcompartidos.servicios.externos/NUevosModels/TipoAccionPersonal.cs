using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class TipoAccionPersonal
    {
        public TipoAccionPersonal()
        {
            AccionPersonal = new HashSet<AccionPersonal>();
            FlujoAprobacion = new HashSet<FlujoAprobacion>();
            PieFirma = new HashSet<PieFirma>();
        }

        public int IdTipoAccionPersonal { get; set; }
        public string Nombre { get; set; }
        public int? NdiasMinimo { get; set; }
        public int? NdiasMaximo { get; set; }
        public int? NhorasMinimo { get; set; }
        public int? NhorasMaximo { get; set; }
        public bool? DiasHabiles { get; set; }
        public bool? ImputableVacaciones { get; set; }
        public bool? ProcesoNomina { get; set; }
        public bool? EsResponsableTh { get; set; }
        public string Matriz { get; set; }
        public string Descripcion { get; set; }
        public bool? GeneraAccionPersonal { get; set; }
        public bool? ModificaDistributivo { get; set; }
        public int? IdEstadoTipoAccionPersonal { get; set; }

        public virtual ICollection<AccionPersonal> AccionPersonal { get; set; }
        public virtual ICollection<FlujoAprobacion> FlujoAprobacion { get; set; }
        public virtual ICollection<PieFirma> PieFirma { get; set; }
        public virtual EstadoTipoAccionPersonal IdEstadoTipoAccionPersonalNavigation { get; set; }
    }
}
