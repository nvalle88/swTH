using bd.swth.entidades.Negocio;
using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class AccionPersonalViewModel
    {
        // Campos tabla accionPersonal
        public int IdAccionPersonal { get; set; }
        public int IdEmpleadoAfectado { get; set; }
        public int IdEmpleadoResponsableIngreso { get; set; }
        public int IdTipoAccionPersonal { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Solicitud { get; set; }
        public string Explicacion { get; set; }
        public DateTime FechaRige { get; set; }
        public DateTime? FechaRigeHasta { get; set; }
        public int TotalDias { get; set; }
        public int? DiasRestantes { get; set; }
        public int Estado { get; set; }
        public bool Bloquear { get; set; }
        public bool Ejecutado { get; set; }


        public TipoAccionesPersonalViewModel TipoAccionPersonalViewModel { get; set; }

        public DistributivoSituacionActual DistributivoSituacionActual { get; set; }

        /*
        // campos estados validación
        public string EstadoDirector { get; set; }
        public string EstadoValidacionTTHH { get; set; }


        public bool GeneraMovimientoPersonal { get; set; }
        
        
        
        public EmpleadoMovimiento EmpleadoMovimiento { get; set; }

        public List<IndiceOcupacionalModalidadPartida> ListaPuestosOcupados { get; set; }

        public string NombreUsuarioAprobador { get; set; }

        public bool ConfigurarPuesto { get; set; }

        */

    }
}
