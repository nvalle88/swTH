namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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

        public virtual ICollection<OtroIngreso> OtroIngreso { get; set; }
        public virtual Empleado Empleado { get; set; }
    }
}
