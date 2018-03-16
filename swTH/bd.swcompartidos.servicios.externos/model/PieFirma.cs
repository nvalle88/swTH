using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class PieFirma
    {
        public int IdPieFirma { get; set; }
        public int? IdTipoAccionPersonal { get; set; }
        public int? IdIndiceOcupacional { get; set; }
        public int? Nivel { get; set; }

        public virtual IndiceOcupacional IdIndiceOcupacionalNavigation { get; set; }
        public virtual TipoAccionPersonal IdTipoAccionPersonalNavigation { get; set; }
    }
}
