using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class TipoCalificacion
    {
        public TipoCalificacion()
        {
            Calificacion = new HashSet<Calificacion>();
        }

        public int IdTipoCalificacion { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Calificacion> Calificacion { get; set; }
    }
}
