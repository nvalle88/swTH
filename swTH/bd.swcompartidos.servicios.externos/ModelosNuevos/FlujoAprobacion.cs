using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class FlujoAprobacion
    {
        public FlujoAprobacion()
        {
            AprobacionAccionPersonal = new HashSet<AprobacionAccionPersonal>();
        }

        public int IdFlujoAprobacion { get; set; }
        public int IdTipoAccionPersonal { get; set; }
        public int IdSucursal { get; set; }
        public int? IdManualPuesto { get; set; }
        public bool ApruebaJefe { get; set; }

        public virtual ICollection<AprobacionAccionPersonal> AprobacionAccionPersonal { get; set; }
        public virtual ManualPuesto IdManualPuestoNavigation { get; set; }
        public virtual Sucursal IdSucursalNavigation { get; set; }
        public virtual TipoAccionPersonal IdTipoAccionPersonalNavigation { get; set; }
    }
}
