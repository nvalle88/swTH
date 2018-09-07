namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
     
    public partial class TipoNombramiento
    {
        [Key]
        public int IdTipoNombramiento { get; set; }

        public string Nombre { get; set; }
        
        public int IdRelacionLaboral { get; set; }

        public virtual ICollection<DistributivoHistorico> DistributivoHistorico { get; set; }
        public virtual ICollection<DistributivoSituacionActual> DistributivoSituacionActual { get; set; }
        public virtual RelacionLaboral RelacionLaboral { get; set; }

    }
}
