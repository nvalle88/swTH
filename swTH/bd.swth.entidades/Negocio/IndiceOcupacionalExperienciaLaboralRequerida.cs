using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bd.swth.entidades.Negocio
{
    public partial class IndiceOcupacionalExperienciaLaboralRequerida
    {
        [Key]
        public int IdIndiceOcupacionalExperienciaLaboralRequerida { get; set; }
        public int IdIndiceOcupacional { get; set; }
        public int IdExperienciaLaboralRequerida { get; set; }

        public virtual ExperienciaLaboralRequerida ExperienciaLaboralRequerida { get; set; }
        public virtual IndiceOcupacional IndiceOcupacional { get; set; }
    }
}
