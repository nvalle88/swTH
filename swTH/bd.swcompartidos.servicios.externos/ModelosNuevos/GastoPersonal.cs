using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class GastoPersonal
    {
        public int IdGastoPersonal { get; set; }
        public int IdTipoGastoPersonal { get; set; }
        public int IdEmpleado { get; set; }
        public double Valor { get; set; }
        public int Ano { get; set; }

        public virtual Empleado IdEmpleadoNavigation { get; set; }
        public virtual TipoDeGastoPersonal IdTipoGastoPersonalNavigation { get; set; }
    }
}
