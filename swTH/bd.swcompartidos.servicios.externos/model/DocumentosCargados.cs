using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class DocumentosCargados
    {
        public DocumentosCargados()
        {
            DependenciaDocumento = new HashSet<DependenciaDocumento>();
        }

        public int DocumentosSubidos { get; set; }
        public string NombreArchivo { get; set; }
        public DateTime? Fecha { get; set; }
        public string Ubicacion { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaCaducidad { get; set; }
        public string Are { get; set; }
        public int IdEmpleado { get; set; }

        public virtual ICollection<DependenciaDocumento> DependenciaDocumento { get; set; }
    }
}
