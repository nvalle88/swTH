using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class IndiceOcupacional
    {
        public IndiceOcupacional()
        {
            IndiceOcupacionalActividadesEsenciales = new HashSet<IndiceOcupacionalActividadesEsenciales>();
            IndiceOcupacionalAreaConocimiento = new HashSet<IndiceOcupacionalAreaConocimiento>();
            IndiceOcupacionalCapacitaciones = new HashSet<IndiceOcupacionalCapacitaciones>();
            IndiceOcupacionalComportamientoObservable = new HashSet<IndiceOcupacionalComportamientoObservable>();
            IndiceOcupacionalConocimientosAdicionales = new HashSet<IndiceOcupacionalConocimientosAdicionales>();
            IndiceOcupacionalEstudio = new HashSet<IndiceOcupacionalEstudio>();
            IndiceOcupacionalExperienciaLaboralRequerida = new HashSet<IndiceOcupacionalExperienciaLaboralRequerida>();
            IndiceOcupacionalModalidadPartida = new HashSet<IndiceOcupacionalModalidadPartida>();
            PieFirma = new HashSet<PieFirma>();
        }

        public int IdIndiceOcupacional { get; set; }
        public int? IdDependencia { get; set; }
        public int? IdManualPuesto { get; set; }
        public int? IdRolPuesto { get; set; }
        public int? IdEscalaGrados { get; set; }
        public int? IdModalidadPartida { get; set; }
        public int? IdPartidaGeneral { get; set; }
        public string NumeroPartidaIndividual { get; set; }
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
        public virtual ICollection<PieFirma> PieFirma { get; set; }
        public virtual Ambito IdAmbitoNavigation { get; set; }
        public virtual Dependencia IdDependenciaNavigation { get; set; }
        public virtual EscalaGrados IdEscalaGradosNavigation { get; set; }
        public virtual ManualPuesto IdManualPuestoNavigation { get; set; }
        public virtual ModalidadPartida IdModalidadPartidaNavigation { get; set; }
        public virtual PartidaGeneral IdPartidaGeneralNavigation { get; set; }
        public virtual RolPuesto IdRolPuestoNavigation { get; set; }
    }
}
