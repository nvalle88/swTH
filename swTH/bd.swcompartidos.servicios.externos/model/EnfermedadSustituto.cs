using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class EnfermedadSustituto
    {
        public int IdEnfermedadSustituto { get; set; }
        public int? IdTipoEnfermedad { get; set; }
        public string InstitucionEmite { get; set; }
        public int? IdPersonaSustituto { get; set; }

        public virtual PersonaSustituto IdPersonaSustitutoNavigation { get; set; }
        public virtual TipoEnfermedad IdTipoEnfermedadNavigation { get; set; }
    }
}
