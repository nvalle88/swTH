using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bd.swth.entidades.Negocio
{
    public class FlujoAprobacion
    {
        [Key]
        public int IdFlujoAprobacion { get; set; }
        public int IdTipoAccionPersonal { get; set; }
        public int IdSucursal { get; set; }
        public int IdManualPuesto { get; set; }

        public virtual ManualPuesto ManualPuesto{ get; set; }
        public virtual Sucursal Sucursal { get; set; }
        public virtual TipoAccionPersonal TipoAccionPersonal { get; set; }
        public virtual ICollection<AprobacionAccionPersonal> AprobacionAccionPersonal { get; set; }


    }
}
