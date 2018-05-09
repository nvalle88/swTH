namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Ciudad
    {
        [Key]
        public int IdCiudad { get; set; }
        public int IdProvincia { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<InformeViatico> InformeViaticoIdCiudadDestino{ get; set; }
        public virtual ICollection<InformeViatico> InformeViaticoIdCiudadOrigen { get; set; }
        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual ICollection<Parroquia> Parroquia { get; set; }
        public virtual ICollection<SolicitudViatico> SolicitudViatico { get; set; }
        public virtual ICollection<Sucursal> Sucursal { get; set; }
        public virtual Provincia Provincia { get; set; }


    }
}
