namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Dependencia
    {
        [Key]
        public int IdDependencia { get; set; }
        public string Nombre { get; set; }
        public int IdSucursal { get; set; }
        public int? IdDependenciaPadre { get; set; }
        public int IdProceso { get; set; }
        public string Codigo { get; set; }


        
        public virtual Sucursal Sucursal { get; set; }
        public virtual Dependencia DependenciaPadre { get; set; }
        public virtual ICollection<Dependencia> DependenciasHijas { get; set; }
        public virtual ICollection<ActivarPersonalTalentoHumano> ActivarPersonalTalentoHumano { get; set; }
        public virtual ICollection<ActividadesGestionCambio> ActividadesGestionCambio { get; set; }
        public virtual ICollection<DependenciaDocumento> DependenciaDocumento { get; set; }
        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual ICollection<Evaluador> Evaluador { get; set; }
        public virtual ICollection<IndiceOcupacionalModalidadPartida> IndiceOcupacionalModalidadPartida { get; set; }
        public virtual ICollection<SituacionPropuesta> SituacionPropuesta { get; set; }
        public virtual ICollection<ProcesoDetalle> ProcesoDetalle { get; set; }

        public virtual Proceso Proceso { get; set; }


    }
}