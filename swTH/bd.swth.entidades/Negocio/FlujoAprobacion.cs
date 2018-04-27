using System.ComponentModel.DataAnnotations;

namespace bd.swth.entidades.Negocio
{
    public class FlujoAprobacion
    {
        [Key]
        public int IdFlujoAprobacion { get; set; }

        public int IdTipoAccionPersonal { get; set; }
        public virtual TipoAccionPersonal TipoAccionPersonal { get; set; }

        public int IdEmpleado { get; set; }
        public virtual Empleado Empleado { get; set; }

        

    }
}
