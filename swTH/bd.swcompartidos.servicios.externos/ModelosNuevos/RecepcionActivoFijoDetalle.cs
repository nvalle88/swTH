using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class RecepcionActivoFijoDetalle
    {
        public RecepcionActivoFijoDetalle()
        {
            AltaActivoFijoDetalle = new HashSet<AltaActivoFijoDetalle>();
            BajaActivoFijoDetalle = new HashSet<BajaActivoFijoDetalle>();
            ComponenteActivoFijoIdRecepcionActivoFijoDetalleComponenteNavigation = new HashSet<ComponenteActivoFijo>();
            ComponenteActivoFijoIdRecepcionActivoFijoDetalleOrigenNavigation = new HashSet<ComponenteActivoFijo>();
            DepreciacionActivoFijo = new HashSet<DepreciacionActivoFijo>();
            DocumentoActivoFijo = new HashSet<DocumentoActivoFijo>();
            InventarioActivoFijoDetalle = new HashSet<InventarioActivoFijoDetalle>();
            MantenimientoActivoFijo = new HashSet<MantenimientoActivoFijo>();
            MovilizacionActivoFijoDetalle = new HashSet<MovilizacionActivoFijoDetalle>();
            ProcesoJudicialActivoFijo = new HashSet<ProcesoJudicialActivoFijo>();
            RevalorizacionActivoFijo = new HashSet<RevalorizacionActivoFijo>();
            TransferenciaActivoFijoDetalle = new HashSet<TransferenciaActivoFijoDetalle>();
            UbicacionActivoFijo = new HashSet<UbicacionActivoFijo>();
        }

        public int IdRecepcionActivoFijoDetalle { get; set; }
        public int IdRecepcionActivoFijo { get; set; }
        public int IdActivoFijo { get; set; }
        public int IdEstado { get; set; }
        public int IdCodigoActivoFijo { get; set; }
        public string Serie { get; set; }
        public string NumeroChasis { get; set; }
        public string NumeroMotor { get; set; }
        public string Placa { get; set; }
        public string NumeroClaveCatastral { get; set; }

        public virtual ICollection<AltaActivoFijoDetalle> AltaActivoFijoDetalle { get; set; }
        public virtual ICollection<BajaActivoFijoDetalle> BajaActivoFijoDetalle { get; set; }
        public virtual ICollection<ComponenteActivoFijo> ComponenteActivoFijoIdRecepcionActivoFijoDetalleComponenteNavigation { get; set; }
        public virtual ICollection<ComponenteActivoFijo> ComponenteActivoFijoIdRecepcionActivoFijoDetalleOrigenNavigation { get; set; }
        public virtual ICollection<DepreciacionActivoFijo> DepreciacionActivoFijo { get; set; }
        public virtual ICollection<DocumentoActivoFijo> DocumentoActivoFijo { get; set; }
        public virtual ICollection<InventarioActivoFijoDetalle> InventarioActivoFijoDetalle { get; set; }
        public virtual ICollection<MantenimientoActivoFijo> MantenimientoActivoFijo { get; set; }
        public virtual ICollection<MovilizacionActivoFijoDetalle> MovilizacionActivoFijoDetalle { get; set; }
        public virtual ICollection<ProcesoJudicialActivoFijo> ProcesoJudicialActivoFijo { get; set; }
        public virtual ICollection<RevalorizacionActivoFijo> RevalorizacionActivoFijo { get; set; }
        public virtual ICollection<TransferenciaActivoFijoDetalle> TransferenciaActivoFijoDetalle { get; set; }
        public virtual ICollection<UbicacionActivoFijo> UbicacionActivoFijo { get; set; }
        public virtual ActivoFijo IdActivoFijoNavigation { get; set; }
        public virtual CodigoActivoFijo IdCodigoActivoFijoNavigation { get; set; }
        public virtual Estado IdEstadoNavigation { get; set; }
        public virtual RecepcionActivoFijo IdRecepcionActivoFijoNavigation { get; set; }
    }
}
