namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public partial class EspecificidadExperiencia
    {
        [Key]
        public int IdEspecificidadExperiencia { get; set; }

       
        public string Descripcion { get; set; }

        //Propiedades Virtuales Referencias a otras clases

        public virtual ICollection<ExperienciaLaboralRequerida> ExperienciaLaboralRequerida { get; set; }
    }
}
