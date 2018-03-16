using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class FichaMedica
    {
        public FichaMedica()
        {
            AntecedentesFamiliares = new HashSet<AntecedentesFamiliares>();
            AntecedentesLaborales = new HashSet<AntecedentesLaborales>();
            ExamenComplementario = new HashSet<ExamenComplementario>();
        }

        public int IdFichaMedica { get; set; }
        public int IdPersona { get; set; }
        public DateTime FechaFichaMedica { get; set; }
        public string AntecedenteMedico { get; set; }
        public string AntecedenteQuirurgico { get; set; }
        public string Alergias { get; set; }
        public string UsoMedicinaDiaria { get; set; }
        public string Vacunas { get; set; }
        public DateTime? FechaUltimaDosis { get; set; }
        public DateTime? PrimeraMenstruacion { get; set; }
        public DateTime? UltimaMenstruacion { get; set; }
        public string CicloMenstrual { get; set; }
        public int? Gestas { get; set; }
        public int? Partos { get; set; }
        public int? Cesarias { get; set; }
        public int? Abortos { get; set; }
        public int? HijosVivos { get; set; }
        public DateTime? UltimoPapTest { get; set; }
        public DateTime? UltimaMamografia { get; set; }
        public string Anticoncepcion { get; set; }
        public bool Cigarrillo { get; set; }
        public string FrecuenciaCigarrillo { get; set; }
        public string CigarrilloDesde { get; set; }
        public string CigarrilloHasta { get; set; }
        public bool Licor { get; set; }
        public string LicorFrecuencia { get; set; }
        public string LicorDesde { get; set; }
        public string LicorHasta { get; set; }
        public bool Drogas { get; set; }
        public string FrecuenciaDrogas { get; set; }
        public string DrogasDesde { get; set; }
        public string DrogasHasta { get; set; }
        public bool Ejercicios { get; set; }
        public string EjerciciosFrecuencia { get; set; }
        public string EjerciciosTipo { get; set; }
        public string TensionArterial { get; set; }
        public string FrecuenciaCardiaca { get; set; }
        public string FrecuenciaRespiratoria { get; set; }
        public string Talla { get; set; }
        public string Peso { get; set; }
        public int LateralidadDominante { get; set; }
        public int Interpretacion { get; set; }
        public bool Cabeza { get; set; }
        public bool Ojos { get; set; }
        public bool Oidos { get; set; }
        public bool Nariz { get; set; }
        public bool Boca { get; set; }
        public bool FaringeAmigdalas { get; set; }
        public bool Cuello { get; set; }
        public bool Corazon { get; set; }
        public bool Pulmones { get; set; }
        public bool Abdomen { get; set; }
        public bool Hernias { get; set; }
        public bool Genitales { get; set; }
        public bool ExtremidadesSuperiores { get; set; }
        public bool ExtremidadesInferiores { get; set; }
        public bool Varices { get; set; }
        public bool SistemaNerviosoCentral { get; set; }
        public bool Piel { get; set; }
        public string Diagnostico { get; set; }
        public bool SospechaEnfermedadLaboral { get; set; }
        public string DetalleEnfermedad { get; set; }
        public bool AptoCargo { get; set; }
        public string Recomendaciones { get; set; }
        public bool AccidenteTrabajo { get; set; }
        public DateTime? FechaAccidente { get; set; }
        public string EmpresaAccidente { get; set; }
        public bool EnfermedadProfesional { get; set; }
        public DateTime? FechaDiagnostico { get; set; }
        public string EmpresaEnfermedad { get; set; }
        public string DetalleAccidenteEnfermedadOcupacional { get; set; }
        public string HabitosObservaciones { get; set; }
        public string CabezaHallazgos { get; set; }
        public string OjosHallazgos { get; set; }
        public string OidosHallazgos { get; set; }
        public string NarizHallazgos { get; set; }
        public string BocaHallazgos { get; set; }
        public string FaringeAmigdalasHallazgos { get; set; }
        public string CuelloHallazgos { get; set; }
        public string CorazonHallazgos { get; set; }
        public string PulmonesHallazgos { get; set; }
        public string AbdomenHallazgos { get; set; }
        public string HerniasHallazgos { get; set; }
        public string GenitalesHallazgos { get; set; }
        public string ExtremidadesSuperioresHallazgos { get; set; }
        public string ExtremidadesInferioresHallazgos { get; set; }
        public string VaricesHallazgos { get; set; }
        public string SistemaNerviosoHallazgos { get; set; }
        public string PielHallazgos { get; set; }
        public int Estado { get; set; }

        public virtual ICollection<AntecedentesFamiliares> AntecedentesFamiliares { get; set; }
        public virtual ICollection<AntecedentesLaborales> AntecedentesLaborales { get; set; }
        public virtual ICollection<ExamenComplementario> ExamenComplementario { get; set; }
        public virtual Persona IdPersonaNavigation { get; set; }
    }
}
