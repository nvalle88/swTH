namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
     
     

    public partial class Persona
    {
        [Key]
        public int IdPersona { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Identificacion { get; set; }
        public string CallePrincipal { get; set; }
        public string CalleSecundaria { get; set; }
        public string Referencia { get; set; }
        public string Numero { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string TelefonoPrivado { get; set; }
        public string TelefonoCasa { get; set; }
        public string CorreoPrivado { get; set; }
        public string LugarTrabajo { get; set; }
        public string Ocupacion { get; set; }
        public int? IdSexo { get; set; }
        public virtual Sexo Sexo { get; set; }
        public int? IdNacionalidadIndigena { get; set; }
        public virtual NacionalidadIndigena NacionalidadIndigena { get; set; }
        public int? IdParroquia { get; set; }
        public virtual Parroquia Parroquia { get; set; }
        public int? IdGenero { get; set; }
        public virtual Genero Genero { get; set; }
        public int? IdTipoSangre { get; set; }
        public virtual TipoSangre TipoSangre { get; set; }
        public int? IdEtnia { get; set; }
        public virtual Etnia Etnia { get; set; }
        public int? IdEstadoCivil { get; set; }
        public virtual EstadoCivil EstadoCivil { get; set; }
        public int? IdTipoIdentificacion { get; set; }
        public virtual TipoIdentificacion TipoIdentificacion { get; set; }
        public virtual Nacionalidad Nacionalidad { get; set; }
        public int? IdNacionalidad { get; set; }
        public virtual ICollection<PersonaEnfermedad> PersonaEnfermedad { get; set; }
        public virtual ICollection<CandidatoConcurso> CandidatoConcurso { get; set; }
        public virtual ICollection<PersonaDiscapacidad> PersonaDiscapacidad { get; set; }
        public virtual ICollection<PersonaCapacitacion> PersonaCapacitacion { get; set; }
        public virtual ICollection<PersonaEstudio> PersonaEstudio { get; set; }
        public virtual ICollection<TrayectoriaLaboral> TrayectoriaLaboral { get; set; }
        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual ICollection<EmpleadoContactoEmergencia> EmpleadoContactoEmergencia { get; set; }
        public virtual ICollection<EmpleadoFamiliar> EmpleadoFamiliar { get; set; }
        public virtual PersonaSustituto PersonaSustitutoPersona { get; set; }
        public virtual PersonaSustituto PersonaPersonaSustituto { get; set; }

    }
}
