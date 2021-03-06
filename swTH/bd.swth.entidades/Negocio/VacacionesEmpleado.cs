﻿using System;
using System.Collections.Generic;

namespace bd.swth.entidades.Negocio
{
    public partial class VacacionesEmpleado
    {
        public int IdVacaciones { get; set; }
        public int PeriodoFiscal { get; set; }
        public int IdEmpleado { get; set; }
        public decimal VacacionesGozadas { get; set; }
        public decimal VacacionesNoGozadas { get; set; }

        public virtual Empleado Empleado{ get; set; }
    }
}
