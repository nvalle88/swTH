using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class ViewModelSeleccionPersonal
    {
        public int ? iddependecia { get; set; }
        public string NumeroPartidaGeneral { get; set; }
        public string UnidadAdministrativa { get; set; }
        public string NumeroPartidaIndividual { get; set; }
        public string PuestoInstitucional { get; set; }
        public string grupoOcupacional { get; set; }
        public int NumeroPuesto { get; set; }
        public string Rol { get; set; }
        public decimal Remuneracion { get; set; }
    }
}
