namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PartidasFase
    {
        [Key]
        public int IdPartidasFase { get; set; }
        public int IdIndiceOcupacional { get; set; }
        public int? IdTipoConcurso { get; set; }
        public int Vacantes { get; set; }
        public int Estado { get; set; }
        public bool Contrato { get; set; }

        public DateTime? Fecha { get; set; }

        public virtual ICollection<CandidatoConcurso> CandidatoConcurso { get; set; }
        public virtual TipoConcurso TipoConcurso { get; set; }
        public virtual IndiceOcupacional IndiceOcupacional { get; set; }




    }
}
