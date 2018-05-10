namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class Quejas
    {
        public int IdQueja { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Descripcion { get; set; }
        public string NumeroFormulario { get; set; }
        public bool? AplicaDescuento { get; set; }
        public int? IdEmpleado { get; set; }
        public int? IdEval001 { get; set; }

        public virtual Empleado Empleado { get; set; }
        public virtual Eval001 Eval001 { get; set; }
    }
}
