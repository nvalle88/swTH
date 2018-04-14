namespace bd.swth.entidades.Negocio
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class SituacionPropuesta
    {
        
        public int IdSituacionPropuesta { get; set; }
        public int IdDependencia { get; set; }
        public int IdGrupoOcupacional { get; set; }
        public int IdProceso { get; set; }
        public int IdRolPuesto { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }

        public virtual Dependencia Dependencia { get; set; }
        public virtual GrupoOcupacional GrupoOcupacional { get; set; }
        public virtual Proceso Proceso { get; set; }
        public virtual RolPuesto RolPuesto { get; set; }


    }
}
