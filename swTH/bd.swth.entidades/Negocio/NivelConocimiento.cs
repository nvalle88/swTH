namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;



    public partial class NivelConocimiento
    {
        [Key]
        public int IdNivelConocimiento { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<EvaluacionConocimiento> EvaluacionConocimiento { get; set; }
    }
}
