using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class Empleado
    {
        public Empleado()
        {
            AccionPersonal = new HashSet<AccionPersonal>();
            RealizaExamenInduccion = new HashSet<RealizaExamenInduccion>();
            RecepcionActivoFijo = new HashSet<RecepcionActivoFijo>();
            RecepcionArticulos = new HashSet<RecepcionArticulos>();
            RegistroEntradaSalida = new HashSet<RegistroEntradaSalida>();
            Rmu = new HashSet<Rmu>();
            RolPagos = new HashSet<RolPagos>();
        }

        public int IdEmpleado { get; set; }
        public int IdPersona { get; set; }
        public int IdCiudadLugarNacimiento { get; set; }
        public int IdProvinciaLugarSufragio { get; set; }
        public int? IdDependencia { get; set; }
        public int? IdBrigadaSsorol { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaIngresoSectorPublico { get; set; }
        public bool? EsJefe { get; set; }
        public string Foto { get; set; }
        public bool? TrabajoSuperintendenciaBanco { get; set; }
        public bool? DeclaracionJurada { get; set; }
        public string IngresosOtraActividad { get; set; }
        public int? MesesImposiciones { get; set; }
        public int? DiasImposiciones { get; set; }
        public bool? FondosReservas { get; set; }
        public string NombreUsuario { get; set; }
        public bool? Activo { get; set; }
        public string Telefono { get; set; }
        public int? Extension { get; set; }

        public virtual ICollection<AccionPersonal> AccionPersonal { get; set; }
        public virtual ICollection<RealizaExamenInduccion> RealizaExamenInduccion { get; set; }
        public virtual ICollection<RecepcionActivoFijo> RecepcionActivoFijo { get; set; }
        public virtual ICollection<RecepcionArticulos> RecepcionArticulos { get; set; }
        public virtual ICollection<RegistroEntradaSalida> RegistroEntradaSalida { get; set; }
        public virtual ICollection<Rmu> Rmu { get; set; }
        public virtual ICollection<RolPagos> RolPagos { get; set; }
        public virtual BrigadaSsorol IdBrigadaSsorolNavigation { get; set; }
        public virtual Ciudad IdCiudadLugarNacimientoNavigation { get; set; }
        public virtual Dependencia IdDependenciaNavigation { get; set; }
        public virtual Persona IdPersonaNavigation { get; set; }
        public virtual Provincia IdProvinciaLugarSufragioNavigation { get; set; }
    }
}
