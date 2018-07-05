using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bd.swth.entidades.Negocio
{
    public partial class Presupuesto
    {
        [Key]
        public int IdPresupuesto { get; set; }
        public string NumeroPartidaPresupuestaria { get; set; }
        public double? Valor { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdSucursal { get; set; }

        public virtual ICollection<DetallePresupuesto> DetallePresupuesto { get; set; }
        public virtual Sucursal Sucursal { get; set; }
        public virtual ICollection<ReliquidacionViatico> ReliquidacionViatico { get; set; }
    }
}
