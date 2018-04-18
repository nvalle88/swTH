namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class TipoConcurso
    {
        [Key]
        public int IdTipoConcurso { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        //Propiedades Virtuales Referencias a otras clases

        public virtual ICollection<PartidasFase> PartidasFase { get; set; }




    }
}
