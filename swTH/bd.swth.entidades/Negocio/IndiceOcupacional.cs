namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class IndiceOcupacional
    {
        [Key]
        public int IdIndiceOcupacional { get; set; }
        public int? IdDependencia { get; set; }
        public int? IdManualPuesto { get; set; }
        public int? IdRolPuesto { get; set; }
        public int? IdEscalaGrados { get; set; }
        public int? IdPartidaGeneral { get; set; }
        public int? IdAmbito { get; set; }
        public string Nivel { get; set; }

        public virtual ICollection<IndiceOcupacionalActividadesEsenciales> IndiceOcupacionalActividadesEsenciales { get; set; }
        public virtual ICollection<IndiceOcupacionalAreaConocimiento> IndiceOcupacionalAreaConocimiento { get; set; }
        public virtual ICollection<IndiceOcupacionalCapacitaciones> IndiceOcupacionalCapacitaciones { get; set; }
        public virtual ICollection<IndiceOcupacionalComportamientoObservable> IndiceOcupacionalComportamientoObservable { get; set; }
        public virtual ICollection<IndiceOcupacionalConocimientosAdicionales> IndiceOcupacionalConocimientosAdicionales { get; set; }
        public virtual ICollection<IndiceOcupacionalEstudio> IndiceOcupacionalEstudio { get; set; }
        public virtual ICollection<IndiceOcupacionalExperienciaLaboralRequerida> IndiceOcupacionalExperienciaLaboralRequerida { get; set; }
        public virtual ICollection<IndiceOcupacionalModalidadPartida> IndiceOcupacionalModalidadPartida { get; set; }
        public virtual ICollection<PartidasFase> PartidasFase { get; set; }
        public virtual ICollection<PieFirma> PieFirma { get; set; }

        public virtual Ambito Ambito { get; set; }
        public virtual Dependencia Dependencia { get; set; }
        public virtual EscalaGrados EscalaGrados { get; set; }
        public virtual ManualPuesto ManualPuesto { get; set; }
        public virtual PartidaGeneral PartidaGeneral { get; set; }
        public virtual RolPuesto RolPuesto { get; set; }
        public virtual ICollection<EmpleadoMovimiento> EmpleadoMovimiento { get; set; }

        [NotMapped]
        public int IdRelacionLaboral { get; set; }

        [NotMapped]
        public int IdModalidadPartida { get; set; }

    }
}
