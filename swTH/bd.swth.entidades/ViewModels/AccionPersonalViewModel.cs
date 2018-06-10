using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class AccionPersonalViewModel
    {
        // campos de tabla Acción Personal
        public int IdAccionPersonal { get; set; }
        
        public DateTime Fecha { get; set; }
        
        public string Numero { get; set; }
        
        public string Solicitud { get; set; }
        
        public string Explicacion { get; set; }
        
        public DateTime FechaRige { get; set; }
        
        public DateTime FechaRigeHasta { get; set; }
        
        public int Estado { get; set; }
        
        public int NoDias { get; set; }

        // Campos que no pertenecen a la tabla
        public string EstadoDirector { get; set; }
        public string EstadoValidacionTTHH { get; set; }


        public bool GeneraMovimientoPersonal { get; set; }

        //Referencias a tablas
        public DatosBasicosEmpleadoViewModel DatosBasicosEmpleadoViewModel { get; set; }
        
        public TipoAccionesPersonalViewModel TipoAccionPersonalViewModel { get; set; }

        public SituacionActualEmpleadoViewModel SituacionActualEmpleadoViewModel { get; set; }
        public SituacionActualEmpleadoViewModel SituacionPropuestaEmpleadoViewModel { get; set; }

        public int IdIndiceOcupacionalModalidadPartidaPropuesta { get; set; }

        public List<IndicesOcupacionalesModalidadPartidaViewModel> ListaIndicesOcupacionalesModalidadPartida { get; set; }

        public string NombreUsuarioAprobador { get; set; }
    }
}
