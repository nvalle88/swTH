﻿using System;
using System.Collections.Generic;

namespace bd.swth.entidades.Negocio
{
    public partial class VacacionRelacionLaboral
    {
        public int IdVacacionRelacionLaboral { get; set; }
        public int IdRelacionLaboral { get; set; }
        public int MinAcumulacionDias { get; set; }
        public int MaxAcumulacionDias { get; set; }
        public int IncrementoDiasPorPeriodoFiscal { get; set; }
        public int IncrementoApartirPeriodoFiscal { get; set; }

        public virtual RelacionLaboral RelacionLaboral { get; set; }
    }
}
