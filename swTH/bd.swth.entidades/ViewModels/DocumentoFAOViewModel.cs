using bd.swth.entidades.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class DocumentoFAOViewModel
    {
        public int IdEmpleado { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string Identificacion { get; set; }
        public int OpcionMenu { get; set; }
        public string NombreUsuario { get; set; }
        public int idDependencia { get; set; }
        public int idsucursal { get; set; }
        public int? estado { get; set; }
        public string Institucion { get; set; }
        public string UnidadAdministrativa { get; set; }
        public string Puesto { get; set; }
        public string NombreApellido { get; set; }
        public string LugarTrabajo { get; set; }
        public string Mision { get; set; }
        public int Anio { get; set; }
        public bool InternoMismoProceso { get; set; }
        public bool InternoOtroProceso { get; set; }
        public bool ExternosCiudadania { get; set; }
        public bool ExtPersJuridicasPubNivelNacional { get; set; }
        public string actividad { get; set; }
        public int IdFormularioAnalisisOcupacional { get; set; }
        public DateTime FechaRegistro { get; set; }
        public List<ActividadesAnalisisOcupacional> ListaActividad { get; set; }
        public List<Exepciones> ListaExepcion { get; set; }
        public List<string> ListaActividads { get; set; }
        public List<string> ListaExepciones { get; set; }
        public List<string> ListActividades { get; set; }
        public List<RolPuesto> ListasRolPUestos { get; set; }
        public List<ManualPuesto> ListasManualPuesto { get; set; }
        public List<string> ListaRolPUesto { get; set; }
        public bool aplicapolitica { get; set; }
        public string Descripcionpuesto { get; set; }
        public bool Cumple { get; set; }
        public bool Revisar { get; set; }
        public int IdManualPuesto { get; set; }
        public int IdAdministracionTalentoHumano { get; set; }
        public int IdManualPuestoActual { get; set; }

    }
}
