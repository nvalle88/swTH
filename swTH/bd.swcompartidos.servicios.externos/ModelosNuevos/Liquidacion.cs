using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Liquidacion
    {
        public int IdLiquidacion { get; set; }
        public int IdRubroLiquidacion { get; set; }
        public int IdEmpleado { get; set; }
        public decimal Valor { get; set; }
        public DateTime Fecha { get; set; }

        public virtual RubroLiquidacion IdRubroLiquidacionNavigation { get; set; }
    }
}
