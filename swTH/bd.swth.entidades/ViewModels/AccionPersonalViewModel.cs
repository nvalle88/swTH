using bd.swth.entidades.Negocio;
using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class AccionPersonalViewModel
    {
        // campos tabla accionPersonal
        public int IdAccionPersonal { get; set; }
        
        public DateTime Fecha { get; set; }
        
        public string Numero { get; set; }
        
        public string Solicitud { get; set; }
        
        public string Explicacion { get; set; }
        
        public DateTime FechaRige { get; set; }
        
        public DateTime? FechaRigeHasta { get; set; }
        
        public int Estado { get; set; }
        
        public int? NoDias { get; set; }

        public bool Bloquear { get; set; }
        public bool Ejecutado { get; set; }


        // campos estados validación
        public string EstadoDirector { get; set; }
        public string EstadoValidacionTTHH { get; set; }


        public bool GeneraMovimientoPersonal { get; set; }
        
        public TipoAccionesPersonalViewModel TipoAccionPersonalViewModel { get; set; }
        
        public EmpleadoMovimiento EmpleadoMovimiento { get; set; }

        public List<IndiceOcupacionalModalidadPartida> ListaPuestosOcupados { get; set; }

        public string NombreUsuarioAprobador { get; set; }

        public bool ConfigurarPuesto { get; set; }

    }
}
