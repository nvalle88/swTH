namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Candidato
    {
        [Key]
        public int IdCandidato { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public virtual ICollection<CandidatoConcurso> CandidatoConcurso { get; set; }
        public virtual ICollection<CandidatoEstudio> CandidatoEstudio { get; set; }
        public virtual ICollection<CandidatoTrayectoriaLaboral> CandidatoTrayectoriaLaboral { get; set; }


    }
}
