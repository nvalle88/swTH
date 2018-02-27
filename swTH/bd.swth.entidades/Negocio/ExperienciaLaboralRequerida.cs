namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ExperienciaLaboralRequerida
    {
        [Key]
        public int IdExperienciaLaboralRequerida { get; set; }

        public int IdEspecificidadExperiencia { get; set; }
        public int AnoExperiencia { get; set; }
        public int MesExperiencia { get; set; }
        public int IdEstudio { get; set; }

        public virtual ICollection<IndiceOcupacionalExperienciaLaboralRequerida> IndiceOcupacionalExperienciaLaboralRequerida { get; set; }
        public virtual EspecificidadExperiencia EspecificidadExperiencia { get; set; }
        public virtual Estudio Estudio { get; set; }

    }
}
