using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class BrechasPorDependenciaViewModel
    {
        public int IdDependencia { get; set; }

        public List<ReporteBrechasViewModel> ListaReporteBrechasViewModels { get; set; }

    }
}
