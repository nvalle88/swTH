using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class MaterialApoyo
    {
        public int IdMaterialApoyo { get; set; }
        public string NombreDocumento { get; set; }
        public string Ubicacion { get; set; }
        public int IdFormularioDevengacion { get; set; }

        public virtual FormularioDevengacion IdFormularioDevengacionNavigation { get; set; }
    }
}
