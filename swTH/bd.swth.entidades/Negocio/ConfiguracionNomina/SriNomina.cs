using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.swth.entidades.Negocio
{
   public class SriNomina
    {
        [Key]
        public int IdSri { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public bool Abierto { get; set; }

        public virtual List<SriDetalle> SriDetalle { get; set; }
    }
}
