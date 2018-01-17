using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.swth.entidades.Negocio
{
    public class TipoCalificacion
    {
        [Key]
        public int IdTipoCalificacion { get; set; }
        
        public string Nombre { get; set; }

        //Propiedades Virtuales Referencias a otras clases
        
        public virtual ICollection<Calificacion> Calificacion { get; set; }
    }
}
