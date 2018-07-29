using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Calificacion
    {
        public int IdCalificacion { get; set; }
        public int IdTipoCalificacion { get; set; }
        public int? Puntaje { get; set; }

        public virtual TipoCalificacion IdTipoCalificacionNavigation { get; set; }
    }
}
