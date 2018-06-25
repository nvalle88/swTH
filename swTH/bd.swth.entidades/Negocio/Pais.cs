namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
     
     

    public partial class Pais
    {

        [Key]
        public int IdPais { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<CapacitacionProveedor> CapacitacionProveedor { get; set; }
        public virtual ICollection<Provincia> Provincia { get; set; }
    }
}
