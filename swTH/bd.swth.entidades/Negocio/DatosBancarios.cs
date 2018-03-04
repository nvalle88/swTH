namespace bd.swth.entidades.Negocio
{
    using System.ComponentModel.DataAnnotations;

    public partial class DatosBancarios
    {
        public int IdDatosBancarios { get; set; }
        public string NumeroCuenta { get; set; }
        public bool Ahorros { get; set; }
        public int IdEmpleado { get; set; }
        public virtual Empleado Empleado { get; set; }
        public int IdInstitucionFinanciera { get; set; }
        public virtual InstitucionFinanciera InstitucionFinanciera { get; set; }






    }
}
