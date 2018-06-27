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

        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual ICollection<InformeViatico> InformeViaticoCiudadDestino { get; set; }
        public virtual ICollection<InformeViatico> InformeViaticoCiudadOrigen { get; set; }
        public virtual ICollection<ItinerarioViatico> ItinerarioViaticoCiudadDestino { get; set; }
        public virtual ICollection<ItinerarioViatico> ItinerarioViaticoCiudadOrigen { get; set; }
        public virtual ICollection<Parroquia> Parroquia { get; set; }
        public virtual ICollection<PlanCapacitacion> PlanCapacitacion { get; set; }
        public virtual ICollection<ReliquidacionViatico> ReliquidacionViaticoCiudadDestino { get; set; }
        public virtual ICollection<ReliquidacionViatico> ReliquidacionViaticoCiudadOrigen{ get; set; }
        public virtual ICollection<SolicitudViatico> SolicitudViatico { get; set; }
        public virtual ICollection<Sucursal> Sucursal { get; set; }
        public virtual Provincia Provincia { get; set; }     


    }
}
