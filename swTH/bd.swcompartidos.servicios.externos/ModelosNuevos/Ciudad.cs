using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Ciudad
    {
        public Ciudad()
        {
            DetalleReliquidacionViaticoIdCiudadDestinoNavigation = new HashSet<DetalleReliquidacionViatico>();
            DetalleReliquidacionViaticoIdCiudadOrigenNavigation = new HashSet<DetalleReliquidacionViatico>();
            Empleado = new HashSet<Empleado>();
            InformeViaticoIdCiudadDestinoNavigation = new HashSet<InformeViatico>();
            InformeViaticoIdCiudadOrigenNavigation = new HashSet<InformeViatico>();
            ItinerarioViaticoIdCiudadDestinoNavigation = new HashSet<ItinerarioViatico>();
            ItinerarioViaticoIdCiudadOrigenNavigation = new HashSet<ItinerarioViatico>();
            Parroquia = new HashSet<Parroquia>();
            PlanCapacitacion = new HashSet<PlanCapacitacion>();
            SolicitudViatico = new HashSet<SolicitudViatico>();
            Sucursal = new HashSet<Sucursal>();
        }

        public int IdCiudad { get; set; }
        public int IdProvincia { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<DetalleReliquidacionViatico> DetalleReliquidacionViaticoIdCiudadDestinoNavigation { get; set; }
        public virtual ICollection<DetalleReliquidacionViatico> DetalleReliquidacionViaticoIdCiudadOrigenNavigation { get; set; }
        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual ICollection<InformeViatico> InformeViaticoIdCiudadDestinoNavigation { get; set; }
        public virtual ICollection<InformeViatico> InformeViaticoIdCiudadOrigenNavigation { get; set; }
        public virtual ICollection<ItinerarioViatico> ItinerarioViaticoIdCiudadDestinoNavigation { get; set; }
        public virtual ICollection<ItinerarioViatico> ItinerarioViaticoIdCiudadOrigenNavigation { get; set; }
        public virtual ICollection<Parroquia> Parroquia { get; set; }
        public virtual ICollection<PlanCapacitacion> PlanCapacitacion { get; set; }
        public virtual ICollection<SolicitudViatico> SolicitudViatico { get; set; }
        public virtual ICollection<Sucursal> Sucursal { get; set; }
        public virtual Provincia IdProvinciaNavigation { get; set; }
    }
}
