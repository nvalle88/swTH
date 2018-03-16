using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class DeclaracionPatrimonioPersonal
    {
        public DeclaracionPatrimonioPersonal()
        {
            OtroIngreso = new HashSet<OtroIngreso>();
        }

        public int IdDeclaracionPatrimonioPersonal { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime FechaDeclaracion { get; set; }
        public decimal? TotalEfectivo { get; set; }
        public decimal? TotalBienInmueble { get; set; }
        public decimal? TotalBienMueble { get; set; }
        public decimal? TotalPasivo { get; set; }
        public decimal TotalPatrimonio { get; set; }

        public virtual ICollection<OtroIngreso> OtroIngreso { get; set; }
    }
}
