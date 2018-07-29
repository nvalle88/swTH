using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class TipoCesacionFuncion
    {
        public TipoCesacionFuncion()
        {
            CeseFuncion = new HashSet<CeseFuncion>();
        }

        public int IdTipoCesacionFuncion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<CeseFuncion> CeseFuncion { get; set; }
    }
}
