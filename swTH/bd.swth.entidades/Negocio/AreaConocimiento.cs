namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class AreaConocimiento
    {

        [Key]
        public int IdAreaConocimiento { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<EvaluacionConocimiento> EvaluacionConocimiento { get; set; }
        public virtual ICollection<IndiceOcupacionalAreaConocimiento> IndiceOcupacionalAreaConocimiento { get; set; }
        public virtual ICollection<Titulo> Titulo { get; set; }

    }
}
