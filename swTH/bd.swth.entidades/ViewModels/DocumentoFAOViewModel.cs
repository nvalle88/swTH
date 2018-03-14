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
        public string actividad1 { get; set; }
        public string actividad2 { get; set; }
        public string actividad3 { get; set; }
        public string actividad4 { get; set; }
        public string actividad5 { get; set; }
        public string actividad6 { get; set; }
        public string actividad7 { get; set; }
        public string actividad8 { get; set; }
        public string actividad9 { get; set; }
        public string actividad10 { get; set; }
        public int IdFormularioAnalisisOcupacional { get; set; }
        public DateTime FechaRegistro { get; set; }
        public List<ActividadesAnalisisOcupacional> ListaActividad { get; set; }
        public List<string> ListaActividads { get; set; }

    }
}
