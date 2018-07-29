using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Empleado
    {
        public Empleado()
        {
            AccionPersonal = new HashSet<AccionPersonal>();
            ActividadesGestionCambio = new HashSet<ActividadesGestionCambio>();
            AjusteInventarioArticulos = new HashSet<AjusteInventarioArticulos>();
            AprobacionAccionPersonal = new HashSet<AprobacionAccionPersonal>();
            Bodega = new HashSet<Bodega>();
            DatosBancarios = new HashSet<DatosBancarios>();
            EmpleadoMovimiento = new HashSet<EmpleadoMovimiento>();
            Eval001 = new HashSet<Eval001>();
            FormularioAnalisisOcupacional = new HashSet<FormularioAnalisisOcupacional>();
            GastoPersonal = new HashSet<GastoPersonal>();
            HorasExtrasNomina = new HashSet<HorasExtrasNomina>();
            LavadoActivoEmpleado = new HashSet<LavadoActivoEmpleado>();
            MantenimientoActivoFijo = new HashSet<MantenimientoActivoFijo>();
            MovilizacionActivoFijoIdEmpleadoAutorizadoNavigation = new HashSet<MovilizacionActivoFijo>();
            MovilizacionActivoFijoIdEmpleadoResponsableNavigation = new HashSet<MovilizacionActivoFijo>();
            MovilizacionActivoFijoIdEmpleadoSolicitaNavigation = new HashSet<MovilizacionActivoFijo>();
            OrdenCompraIdEmpleadoDevolucionNavigation = new HashSet<OrdenCompra>();
            OrdenCompraIdEmpleadoResponsableNavigation = new HashSet<OrdenCompra>();
            PersonaSustituto = new HashSet<PersonaSustituto>();
            Quejas = new HashSet<Quejas>();
            RealizaExamenInduccion = new HashSet<RealizaExamenInduccion>();
            RegistroEntradaSalida = new HashSet<RegistroEntradaSalida>();
            RequerimientoArticulos = new HashSet<RequerimientoArticulos>();
            Rmu = new HashSet<Rmu>();
            RolPagos = new HashSet<RolPagos>();
            SalidaArticulosIdEmpleadoDespachoNavigation = new HashSet<SalidaArticulos>();
            SalidaArticulosIdEmpleadoRealizaBajaNavigation = new HashSet<SalidaArticulos>();
            SolicitudViatico = new HashSet<SolicitudViatico>();
            TransferenciaActivoFijoIdEmpleadoResponsableEnvioNavigation = new HashSet<TransferenciaActivoFijo>();
            TransferenciaActivoFijoIdEmpleadoResponsableReciboNavigation = new HashSet<TransferenciaActivoFijo>();
            UbicacionActivoFijo = new HashSet<UbicacionActivoFijo>();
            VacacionesEmpleado = new HashSet<VacacionesEmpleado>();
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
        public bool? Nepotismo { get; set; }
        public bool? OtrosIngresos { get; set; }
        public string Detalle { get; set; }
        public string TipoRelacion { get; set; }
        public int? AnoDesvinculacion { get; set; }
        public string RelacionSuperintendencia { get; set; }

        public virtual ICollection<AccionPersonal> AccionPersonal { get; set; }
        public virtual ICollection<ActividadesGestionCambio> ActividadesGestionCambio { get; set; }
        public virtual ICollection<AjusteInventarioArticulos> AjusteInventarioArticulos { get; set; }
        public virtual ICollection<AprobacionAccionPersonal> AprobacionAccionPersonal { get; set; }
        public virtual ICollection<Bodega> Bodega { get; set; }
        public virtual ICollection<DatosBancarios> DatosBancarios { get; set; }
        public virtual ICollection<EmpleadoMovimiento> EmpleadoMovimiento { get; set; }
        public virtual ICollection<Eval001> Eval001 { get; set; }
        public virtual ICollection<FormularioAnalisisOcupacional> FormularioAnalisisOcupacional { get; set; }
        public virtual ICollection<GastoPersonal> GastoPersonal { get; set; }
        public virtual ICollection<HorasExtrasNomina> HorasExtrasNomina { get; set; }
        public virtual ICollection<LavadoActivoEmpleado> LavadoActivoEmpleado { get; set; }
        public virtual ICollection<MantenimientoActivoFijo> MantenimientoActivoFijo { get; set; }
        public virtual ICollection<MovilizacionActivoFijo> MovilizacionActivoFijoIdEmpleadoAutorizadoNavigation { get; set; }
        public virtual ICollection<MovilizacionActivoFijo> MovilizacionActivoFijoIdEmpleadoResponsableNavigation { get; set; }
        public virtual ICollection<MovilizacionActivoFijo> MovilizacionActivoFijoIdEmpleadoSolicitaNavigation { get; set; }
        public virtual ICollection<OrdenCompra> OrdenCompraIdEmpleadoDevolucionNavigation { get; set; }
        public virtual ICollection<OrdenCompra> OrdenCompraIdEmpleadoResponsableNavigation { get; set; }
        public virtual ICollection<PersonaSustituto> PersonaSustituto { get; set; }
        public virtual ICollection<Quejas> Quejas { get; set; }
        public virtual ICollection<RealizaExamenInduccion> RealizaExamenInduccion { get; set; }
        public virtual ICollection<RegistroEntradaSalida> RegistroEntradaSalida { get; set; }
        public virtual ICollection<RequerimientoArticulos> RequerimientoArticulos { get; set; }
        public virtual ICollection<Rmu> Rmu { get; set; }
        public virtual ICollection<RolPagos> RolPagos { get; set; }
        public virtual ICollection<SalidaArticulos> SalidaArticulosIdEmpleadoDespachoNavigation { get; set; }
        public virtual ICollection<SalidaArticulos> SalidaArticulosIdEmpleadoRealizaBajaNavigation { get; set; }
        public virtual ICollection<SolicitudViatico> SolicitudViatico { get; set; }
        public virtual ICollection<TransferenciaActivoFijo> TransferenciaActivoFijoIdEmpleadoResponsableEnvioNavigation { get; set; }
        public virtual ICollection<TransferenciaActivoFijo> TransferenciaActivoFijoIdEmpleadoResponsableReciboNavigation { get; set; }
        public virtual ICollection<UbicacionActivoFijo> UbicacionActivoFijo { get; set; }
        public virtual ICollection<VacacionesEmpleado> VacacionesEmpleado { get; set; }
        public virtual BrigadaSsorol IdBrigadaSsorolNavigation { get; set; }
        public virtual Ciudad IdCiudadLugarNacimientoNavigation { get; set; }
        public virtual Dependencia IdDependenciaNavigation { get; set; }
        public virtual Persona IdPersonaNavigation { get; set; }
        public virtual Provincia IdProvinciaLugarSufragioNavigation { get; set; }
    }
}
