using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class SituacionPropuesta
    {
        public int IdSituacionPropuesta { get; set; }
        public int IdDependencia { get; set; }
        public int IdGrupoOcupacional { get; set; }
        public int IdProceso { get; set; }
        public int IdRolPuesto { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }

        public virtual Dependencia IdDependenciaNavigation { get; set; }
        public virtual GrupoOcupacional IdGrupoOcupacionalNavigation { get; set; }
        public virtual Proceso IdProcesoNavigation { get; set; }
        public virtual RolPuesto IdRolPuestoNavigation { get; set; }
    }
}
