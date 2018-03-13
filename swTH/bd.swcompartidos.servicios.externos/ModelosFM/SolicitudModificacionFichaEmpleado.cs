using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class SolicitudModificacionFichaEmpleado
    {
        public int IdSolicitudModificacionFichaEmpleado { get; set; }
        public int IdEmpleado { get; set; }
        public int? IdEstado { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string TelefonoPrivado { get; set; }
        public string TelefonoCasa { get; set; }
        public string Direccion { get; set; }
        public string CapacitacionesRecibidas { get; set; }
        public string ExperienciaLaboral { get; set; }
        public string FormacionAcademica { get; set; }
        public string CargasFamiliares { get; set; }

        public virtual Estado IdEstadoNavigation { get; set; }
    }
}
