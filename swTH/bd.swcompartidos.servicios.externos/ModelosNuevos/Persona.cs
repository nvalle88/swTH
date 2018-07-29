using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Persona
    {
        public Persona()
        {
            Empleado = new HashSet<Empleado>();
            EmpleadoContactoEmergencia = new HashSet<EmpleadoContactoEmergencia>();
            EmpleadoFamiliar = new HashSet<EmpleadoFamiliar>();
            FichaMedica = new HashSet<FichaMedica>();
            PersonaCapacitacion = new HashSet<PersonaCapacitacion>();
            PersonaDiscapacidad = new HashSet<PersonaDiscapacidad>();
            PersonaEnfermedad = new HashSet<PersonaEnfermedad>();
            PersonaEstudio = new HashSet<PersonaEstudio>();
            PersonaSustituto = new HashSet<PersonaSustituto>();
            TrayectoriaLaboral = new HashSet<TrayectoriaLaboral>();
        }

        public int IdPersona { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? IdSexo { get; set; }
        public int? IdTipoIdentificacion { get; set; }
        public int? IdEstadoCivil { get; set; }
        public int? IdGenero { get; set; }
        public int? IdNacionalidad { get; set; }
        public int? IdTipoSangre { get; set; }
        public int? IdEtnia { get; set; }
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string TelefonoPrivado { get; set; }
        public string TelefonoCasa { get; set; }
        public string CorreoPrivado { get; set; }
        public string LugarTrabajo { get; set; }
        public int? IdNacionalidadIndigena { get; set; }
        public string CallePrincipal { get; set; }
        public string CalleSecundaria { get; set; }
        public string Referencia { get; set; }
        public string Numero { get; set; }
        public int? IdParroquia { get; set; }
        public string Ocupacion { get; set; }

        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual ICollection<EmpleadoContactoEmergencia> EmpleadoContactoEmergencia { get; set; }
        public virtual ICollection<EmpleadoFamiliar> EmpleadoFamiliar { get; set; }
        public virtual ICollection<FichaMedica> FichaMedica { get; set; }
        public virtual ICollection<PersonaCapacitacion> PersonaCapacitacion { get; set; }
        public virtual ICollection<PersonaDiscapacidad> PersonaDiscapacidad { get; set; }
        public virtual ICollection<PersonaEnfermedad> PersonaEnfermedad { get; set; }
        public virtual ICollection<PersonaEstudio> PersonaEstudio { get; set; }
        public virtual ICollection<PersonaSustituto> PersonaSustituto { get; set; }
        public virtual ICollection<TrayectoriaLaboral> TrayectoriaLaboral { get; set; }
        public virtual EstadoCivil IdEstadoCivilNavigation { get; set; }
        public virtual Etnia IdEtniaNavigation { get; set; }
        public virtual Genero IdGeneroNavigation { get; set; }
        public virtual Nacionalidad IdNacionalidadNavigation { get; set; }
        public virtual NacionalidadIndigena IdNacionalidadIndigenaNavigation { get; set; }
        public virtual Parroquia IdParroquiaNavigation { get; set; }
        public virtual Sexo IdSexoNavigation { get; set; }
        public virtual TipoIdentificacion IdTipoIdentificacionNavigation { get; set; }
        public virtual TipoSangre IdTipoSangreNavigation { get; set; }
    }
}
