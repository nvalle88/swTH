using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class RequerimientoRolPorDependenciaViewModel
    {

        public int IdDependencia { get; set; }
        public string NombreDependencia { get; set; }

        public RequerimientoRolPorGrupoOcupacionalViewModel RolesNivelJerarquicoSuperior { get; set; }
        public RequerimientoRolPorGrupoOcupacionalViewModel RolesNivelOperativo { get; set; }
    }
}
