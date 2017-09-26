namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
     
     

    public partial class EvaluacionInducion
    {
        [Key]
        public int IdEvaluacionInduccion { get; set; }

        [Range(1, 100, ErrorMessage = "El {0} debe ser mayor que {1} ")]
        public int MinimoAprobar { get; set; }

        [Range(1, 100, ErrorMessage = "El {0} debe ser menor que {2} ")]
        public int MaximoPuntos { get; set; }

      
        public string Nombre { get; set; }

        //Propiedades Virtuales Referencias a otras clases

        public virtual ICollection<Pregunta> Pregunta { get; set; }

        public virtual ICollection<RealizaExamenInduccion> RealizaExamenInduccion { get; set; }
    }
}
