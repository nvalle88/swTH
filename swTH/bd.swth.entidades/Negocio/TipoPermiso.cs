namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
     
    public partial class TipoPermiso
    {
        public TipoPermiso()
        {
            SolicitudPermiso = new HashSet<SolicitudPermiso>();
        }

        public int IdTipoPermiso { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<SolicitudPermiso> SolicitudPermiso { get; set; }
    }
}
