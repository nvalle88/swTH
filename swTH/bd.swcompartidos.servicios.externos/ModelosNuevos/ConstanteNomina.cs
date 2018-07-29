using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class ConstanteNomina
    {
        public int IdConstante { get; set; }
        public string Constante { get; set; }
        public double? Valor { get; set; }
        public string Descripcion { get; set; }
    }
}
