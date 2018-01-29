using bd.swth.entidades.Negocio;
using bd.swth.entidades.ObjectTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class ViewModelDocumentoIngresoEmpleado
    {
        public ListaEmpleadoViewModel empleadoViewModel { get; set; }
        public List<DocumentosIngreso> listadocumentosingreso { get; set; }
        public List<DocumentosIngresoEmpleado> listadocumentosingresoentregado { get; set; }
        public List<String> DocumentosSeleccionados { get; set; }
    }
}
