namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Candidato
    {
        [Key]
        public int IdCandidato { get; set; }

        //Propiedades Virtuales Referencias a otras clases
        public virtual ICollection<CandidatoConcurso> CandidatoConcurso { get; set; }
        
        [Display(Name = "Persona:")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar el {0} ")]
        public int IdPersona { get; set; }
        public virtual Persona Persona { get; set; }

        
    }
}
