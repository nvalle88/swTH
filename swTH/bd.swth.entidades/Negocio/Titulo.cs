namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
     
     
    public partial class Titulo
    {
        [Key]
        public int IdTitulo { get; set; }
        public int? IdAreaConocimiento { get; set; }
        public int IdEstudio { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<CandidatoEstudio> CandidatoEstudio { get; set; }
        public virtual AreaConocimiento AreaConocimiento { get; set; }
        public virtual Estudio Estudio { get; set; }

    }
}
