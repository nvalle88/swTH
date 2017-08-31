namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
     
     

    public partial class EvaluacionInducion
    {
        [Key]
        public int IdEvaluacionInduccion { get; set; }

        
        public int MinimoAprobar { get; set; }

       
        public int MaximoPuntos { get; set; }

      
        public string Nombre { get; set; }

        //Propiedades Virtuales Referencias a otras clases

        public virtual ICollection<Pregunta> Pregunta { get; set; }

        public virtual ICollection<RealizaExamenInduccion> RealizaExamenInduccion { get; set; }
    }
}
