namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ExperienciaLaboralRequerida
    {
        [Key]
        public int IdExperienciaLaboralRequerida { get; set; }
        
        public int IdEspecificidadExperiencia { get; set; }
        public int IdAnoExperiencia { get; set; }
        public int IdEstudio { get; set; }


        public virtual ICollection<IndiceOcupacionalExperienciaLaboralRequerida> IndiceOcupacionalExperienciaLaboralRequerida { get; set; }
        public virtual Estudio Estudio { get; set; }
        public virtual IndiceOcupacional IndiceOcupacional { get; set; }
        public virtual EspecificidadExperiencia EspecificidadExperiencia { get; set; }
        public virtual AnoExperiencia AnoExperiencia { get; set; }

    }
}
