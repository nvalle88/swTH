namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
     
     

    public partial class ManualPuesto
    {
        [Key]
        public int IdManualPuesto { get; set; }

        [Required(ErrorMessage = "Debe introducir  {0}")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe introducir  {0}")]
        public string Descripcion { get; set; }

        public string Mision { get; set; }
        public int? IdRelacionesInternasExternas { get; set; }

        public virtual ICollection<IndiceOcupacional> IndiceOcupacional { get; set; }
        public virtual ICollection<InformeUATH> InformeUATH { get; set; }

        public virtual ICollection<InformeUATH> InformeUATH1 { get; set; }

        public virtual RelacionesInternasExternas RelacionesInternasExternas { get; set; }
        public virtual ICollection<FlujoAprobacion> FlujoAprobacion { get; set; }
    }
}
