using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class LavadoActivoItemViewModel
    {
        public int IdLavadoActivoItem { get; set; }
        public string Descripcion { get; set; }
        public bool Valor { get; set; }
        public DateTime Fecha { get; set; }
    }
}
