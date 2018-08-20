namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class DeclaracionPatrimonioPersonal
    {
        [Key]
        public int IdDeclaracionPatrimonioPersonal { get; set; }

        public DateTime FechaDeclaracion { get; set; }

    
        public decimal? TotalEfectivo { get; set; }

   
        public decimal? TotalBienInmueble { get; set; }

        
        public decimal? TotalBienMueble { get; set; }

        
        public decimal? TotalPasivo { get; set; }


        public decimal TotalPatrimonio { get; set; }


        public int IdEmpleado { get; set; }

        [NotMapped]
        public decimal? Comparativo { get; set; }

        public virtual ICollection<OtroIngreso> OtroIngreso { get; set; }
        public virtual Empleado Empleado { get; set; }
    }
}
