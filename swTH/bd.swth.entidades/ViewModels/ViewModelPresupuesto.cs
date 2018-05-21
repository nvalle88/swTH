using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class ViewModelPresupuesto
    {
        public int IdPresupuesto { get; set; }
        public string NumeroPartidaPresupuestaria { get; set; }
        public double? Valor { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdSucursal { get; set; }
        public string NombreSucursal { get; set; }

    }
}
