namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Provincia
    {
        [Key]
        public int IdProvincia { get; set; }
        public int IdPais { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Ciudad> Ciudad { get; set; }
        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual ICollection<SolicitudViatico> SolicitudViatico { get; set; }
        public virtual Pais Pais { get; set; }

    }
}
