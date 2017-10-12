namespace bd.swth.entidades.Negocio
{
    using System.ComponentModel.DataAnnotations;

    public partial class EmpleadoNepotismo
    {
        [Key]
        public int IdEmpleadoNepotismo { get; set; }

        //Propiedades Virtuales Referencias a otras clases
        public int IdEmpleado { get; set; }
        public int IdEmpleadoFamiliar { get; set; }

        public virtual Empleado Empleado { get; set; }
        public virtual Empleado EmpleadoFamiliar { get; set; }

    }
}
