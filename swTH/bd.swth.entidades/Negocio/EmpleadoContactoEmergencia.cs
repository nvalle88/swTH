namespace bd.swth.entidades.Negocio
{
    using System.ComponentModel.DataAnnotations;

    public partial class EmpleadoContactoEmergencia
    {
        public int IdEmpleadoContactoEmergencia { get; set; }
        
        public int IdPersona { get; set; }
        public virtual Persona Persona { get; set; }
        
        public int IdEmpleado { get; set; }
        public virtual Empleado Empleado { get; set; }
        
        public int IdParentesco { get; set; }
        public virtual Parentesco Parentesco { get; set; }
        
    }
}
