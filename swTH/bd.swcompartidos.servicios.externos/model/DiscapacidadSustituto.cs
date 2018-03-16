using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class DiscapacidadSustituto
    {
        public int IdDiscapacidadSustituto { get; set; }
        public int IdTipoDiscapacidad { get; set; }
        public int PorcentajeDiscapacidad { get; set; }
        public string NumeroCarnet { get; set; }
        public int IdPersonaSustituto { get; set; }

        public virtual PersonaSustituto IdPersonaSustitutoNavigation { get; set; }
        public virtual TipoDiscapacidad IdTipoDiscapacidadNavigation { get; set; }
    }
}
