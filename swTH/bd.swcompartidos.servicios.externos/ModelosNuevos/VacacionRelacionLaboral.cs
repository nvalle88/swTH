using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class VacacionRelacionLaboral
    {
        public int IdVacacionRelacionLaboral { get; set; }
        public int IdRegimenLaboral { get; set; }
        public int MinAcumulacionDias { get; set; }
        public int MaxAcumulacionDias { get; set; }
        public int IncrementoDiasPorPeriodoFiscal { get; set; }
        public int IncrementoApartirPeriodoFiscal { get; set; }

        public virtual RegimenLaboral IdRegimenLaboralNavigation { get; set; }
    }
}
