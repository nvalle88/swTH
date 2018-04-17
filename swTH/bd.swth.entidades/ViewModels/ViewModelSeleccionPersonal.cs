using bd.swth.entidades.Negocio;
using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class ViewModelSeleccionPersonal
    {
        


        public int? iddependecia { get; set; }
        public int idescalagrados { get; set; }
        public string NumeroPartidaGeneral { get; set; }
        public string UnidadAdministrativa { get; set; }
        public string NumeroPartidaIndividual { get; set; }
        public string PuestoInstitucional { get; set; }
        public string grupoOcupacional { get; set; }
        public int NumeroPuesto { get; set; }
        public string Rol { get; set; }
        public decimal? Remuneracion { get; set; }
        public int OpcionMenu { get; set; }




        //Creacion Postulacion
        public string identificacion { get; set; }
        public string nombres { get; set; }
        public string Apellidos { get; set; }
        public int IdEstudio { get; set; }
        public string nivelIntruccion { get; set; }
        public int IdAreaConocimiento { get; set; }
        public string areaconocimiento { get; set; }
        public int IdTitulo { get; set; }
        public string Instituacion { get; set; }
        public string Cargo { get; set; }        
        public DateTime? fechainicio { get; set; }
        public DateTime? fechahasta { get; set; }

        //listas
        public List<PersonaEstudio> ListasPersonaEstudio { get; set; }
    }
}
