using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace bd.sw.externos.ModelosFM
{
    public partial class zeusbdeContext : DbContext
    {
        public virtual DbSet<AccionPersonal> AccionPersonal { get; set; }
        public virtual DbSet<ActividadesAnalisisOcupacional> ActividadesAnalisisOcupacional { get; set; }
        public virtual DbSet<ActividadesEsenciales> ActividadesEsenciales { get; set; }
        public virtual DbSet<ActividadesGestionCambio> ActividadesGestionCambio { get; set; }
        public virtual DbSet<ActivoFijo> ActivoFijo { get; set; }
        public virtual DbSet<ActivoFijoMotivoBaja> ActivoFijoMotivoBaja { get; set; }
        public virtual DbSet<ActivosFijosAlta> ActivosFijosAlta { get; set; }
        public virtual DbSet<ActivosFijosBaja> ActivosFijosBaja { get; set; }
        public virtual DbSet<AdministracionTalentoHumano> AdministracionTalentoHumano { get; set; }
        public virtual DbSet<Ambito> Ambito { get; set; }
        public virtual DbSet<AntecedentesFamiliares> AntecedentesFamiliares { get; set; }
        public virtual DbSet<AntecedentesLaborales> AntecedentesLaborales { get; set; }
        public virtual DbSet<AprobacionViatico> AprobacionViatico { get; set; }
        public virtual DbSet<AreaConocimiento> AreaConocimiento { get; set; }
        public virtual DbSet<Articulo> Articulo { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AvanceGestionCambio> AvanceGestionCambio { get; set; }
        public virtual DbSet<BrigadaSso> BrigadaSso { get; set; }
        public virtual DbSet<BrigadaSsorol> BrigadaSsorol { get; set; }
        public virtual DbSet<Calificacion> Calificacion { get; set; }
        public virtual DbSet<Candidato> Candidato { get; set; }
        public virtual DbSet<CandidatoConcurso> CandidatoConcurso { get; set; }
        public virtual DbSet<Capacitacion> Capacitacion { get; set; }
        public virtual DbSet<CapacitacionAreaConocimiento> CapacitacionAreaConocimiento { get; set; }
        public virtual DbSet<CapacitacionEncuesta> CapacitacionEncuesta { get; set; }
        public virtual DbSet<CapacitacionModalidad> CapacitacionModalidad { get; set; }
        public virtual DbSet<CapacitacionPlanificacion> CapacitacionPlanificacion { get; set; }
        public virtual DbSet<CapacitacionPregunta> CapacitacionPregunta { get; set; }
        public virtual DbSet<CapacitacionProveedor> CapacitacionProveedor { get; set; }
        public virtual DbSet<CapacitacionRecibida> CapacitacionRecibida { get; set; }
        public virtual DbSet<CapacitacionRespuesta> CapacitacionRespuesta { get; set; }
        public virtual DbSet<CapacitacionTemario> CapacitacionTemario { get; set; }
        public virtual DbSet<CapacitacionTemarioProveedor> CapacitacionTemarioProveedor { get; set; }
        public virtual DbSet<CapacitacionTipoPregunta> CapacitacionTipoPregunta { get; set; }
        public virtual DbSet<CatalogoCuenta> CatalogoCuenta { get; set; }
        public virtual DbSet<CeseFuncion> CeseFuncion { get; set; }
        public virtual DbSet<Ciudad> Ciudad { get; set; }
        public virtual DbSet<ClaseActivoFijo> ClaseActivoFijo { get; set; }
        public virtual DbSet<ClaseArticulo> ClaseArticulo { get; set; }
        public virtual DbSet<CodigoActivoFijo> CodigoActivoFijo { get; set; }
        public virtual DbSet<Complain> Complain { get; set; }
        public virtual DbSet<ComportamientoObservable> ComportamientoObservable { get; set; }
        public virtual DbSet<ConfiguracionContabilidad> ConfiguracionContabilidad { get; set; }
        public virtual DbSet<ConfiguracionFeriados> ConfiguracionFeriados { get; set; }
        public virtual DbSet<ConfiguracionViatico> ConfiguracionViatico { get; set; }
        public virtual DbSet<ConfirmacionLectura> ConfirmacionLectura { get; set; }
        public virtual DbSet<ConocimientosAdicionales> ConocimientosAdicionales { get; set; }
        public virtual DbSet<DatosBancarios> DatosBancarios { get; set; }
        public virtual DbSet<DeclaracionPatrimonioPersonal> DeclaracionPatrimonioPersonal { get; set; }
        public virtual DbSet<DenominacionCompetencia> DenominacionCompetencia { get; set; }
        public virtual DbSet<Dependencia> Dependencia { get; set; }
        public virtual DbSet<DependenciaDocumento> DependenciaDocumento { get; set; }
        public virtual DbSet<DepreciacionActivoFijo> DepreciacionActivoFijo { get; set; }
        public virtual DbSet<Destreza> Destreza { get; set; }
        public virtual DbSet<DetalleExamenInduccion> DetalleExamenInduccion { get; set; }
        public virtual DbSet<DetalleFactura> DetalleFactura { get; set; }
        public virtual DbSet<DiscapacidadSustituto> DiscapacidadSustituto { get; set; }
        public virtual DbSet<DocumentoInformacionInstitucional> DocumentoInformacionInstitucional { get; set; }
        public virtual DbSet<DocumentosCargados> DocumentosCargados { get; set; }
        public virtual DbSet<DocumentosIngreso> DocumentosIngreso { get; set; }
        public virtual DbSet<DocumentosIngresoEmpleado> DocumentosIngresoEmpleado { get; set; }
        public virtual DbSet<Ejemplo> Ejemplo { get; set; }
        public virtual DbSet<Ejemplo1> Ejemplo1 { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<EmpleadoActivoFijo> EmpleadoActivoFijo { get; set; }
        public virtual DbSet<EmpleadoContactoEmergencia> EmpleadoContactoEmergencia { get; set; }
        public virtual DbSet<EmpleadoFamiliar> EmpleadoFamiliar { get; set; }
        public virtual DbSet<EmpleadoFormularioCapacitacion> EmpleadoFormularioCapacitacion { get; set; }
        public virtual DbSet<EmpleadoIe> EmpleadoIe { get; set; }
        public virtual DbSet<EmpleadoImpuestoRenta> EmpleadoImpuestoRenta { get; set; }
        public virtual DbSet<EmpleadoMovimiento> EmpleadoMovimiento { get; set; }
        public virtual DbSet<EmpleadoNepotismo> EmpleadoNepotismo { get; set; }
        public virtual DbSet<EmpleadoSaldoVacaciones> EmpleadoSaldoVacaciones { get; set; }
        public virtual DbSet<EmpleadosFormularioDevengacion> EmpleadosFormularioDevengacion { get; set; }
        public virtual DbSet<EnfermedadSustituto> EnfermedadSustituto { get; set; }
        public virtual DbSet<EscalaEvaluacionTotal> EscalaEvaluacionTotal { get; set; }
        public virtual DbSet<EscalaGrados> EscalaGrados { get; set; }
        public virtual DbSet<EspecificidadExperiencia> EspecificidadExperiencia { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<EstadoCivil> EstadoCivil { get; set; }
        public virtual DbSet<EstadoTipoAccionPersonal> EstadoTipoAccionPersonal { get; set; }
        public virtual DbSet<Estudio> Estudio { get; set; }
        public virtual DbSet<Etnia> Etnia { get; set; }
        public virtual DbSet<Eval001> Eval001 { get; set; }
        public virtual DbSet<EvaluacionActividadesPuestoTrabajo> EvaluacionActividadesPuestoTrabajo { get; set; }
        public virtual DbSet<EvaluacionActividadesPuestoTrabajoDetalle> EvaluacionActividadesPuestoTrabajoDetalle { get; set; }
        public virtual DbSet<EvaluacionActividadesPuestoTrabajoFactor> EvaluacionActividadesPuestoTrabajoFactor { get; set; }
        public virtual DbSet<EvaluacionCompetenciasTecnicasPuesto> EvaluacionCompetenciasTecnicasPuesto { get; set; }
        public virtual DbSet<EvaluacionCompetenciasTecnicasPuestoDetalle> EvaluacionCompetenciasTecnicasPuestoDetalle { get; set; }
        public virtual DbSet<EvaluacionCompetenciasTecnicasPuestoFactor> EvaluacionCompetenciasTecnicasPuestoFactor { get; set; }
        public virtual DbSet<EvaluacionCompetenciasUniversales> EvaluacionCompetenciasUniversales { get; set; }
        public virtual DbSet<EvaluacionCompetenciasUniversalesDetalle> EvaluacionCompetenciasUniversalesDetalle { get; set; }
        public virtual DbSet<EvaluacionCompetenciasUniversalesFactor> EvaluacionCompetenciasUniversalesFactor { get; set; }
        public virtual DbSet<EvaluacionConocimiento> EvaluacionConocimiento { get; set; }
        public virtual DbSet<EvaluacionConocimientoDetalle> EvaluacionConocimientoDetalle { get; set; }
        public virtual DbSet<EvaluacionConocimientoFactor> EvaluacionConocimientoFactor { get; set; }
        public virtual DbSet<EvaluacionInducion> EvaluacionInducion { get; set; }
        public virtual DbSet<EvaluacionTrabajoEquipoIniciativaLiderazgo> EvaluacionTrabajoEquipoIniciativaLiderazgo { get; set; }
        public virtual DbSet<EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle> EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle { get; set; }
        public virtual DbSet<EvaluacionTrabajoEquipoIniciativaLiderazgoFactor> EvaluacionTrabajoEquipoIniciativaLiderazgoFactor { get; set; }
        public virtual DbSet<Evaluador> Evaluador { get; set; }
        public virtual DbSet<ExamenComplementario> ExamenComplementario { get; set; }
        public virtual DbSet<Exepciones> Exepciones { get; set; }
        public virtual DbSet<ExperienciaLaboralRequerida> ExperienciaLaboralRequerida { get; set; }
        public virtual DbSet<Factor> Factor { get; set; }
        public virtual DbSet<Factura> Factura { get; set; }
        public virtual DbSet<FacturaViatico> FacturaViatico { get; set; }
        public virtual DbSet<FichaMedica> FichaMedica { get; set; }
        public virtual DbSet<FlujoAprobacion> FlujoAprobacion { get; set; }
        public virtual DbSet<FondoFinanciamiento> FondoFinanciamiento { get; set; }
        public virtual DbSet<FormularioAnalisisOcupacional> FormularioAnalisisOcupacional { get; set; }
        public virtual DbSet<FormularioCapacitacion> FormularioCapacitacion { get; set; }
        public virtual DbSet<FormularioDevengacion> FormularioDevengacion { get; set; }
        public virtual DbSet<FormulasRmu> FormulasRmu { get; set; }
        public virtual DbSet<FrecuenciaAplicacion> FrecuenciaAplicacion { get; set; }
        public virtual DbSet<GastoRubro> GastoRubro { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<GrupoOcupacional> GrupoOcupacional { get; set; }
        public virtual DbSet<ImpuestoRentaParametros> ImpuestoRentaParametros { get; set; }
        public virtual DbSet<Indicador> Indicador { get; set; }
        public virtual DbSet<IndiceOcupacional> IndiceOcupacional { get; set; }
        public virtual DbSet<IndiceOcupacionalActividadesEsenciales> IndiceOcupacionalActividadesEsenciales { get; set; }
        public virtual DbSet<IndiceOcupacionalAreaConocimiento> IndiceOcupacionalAreaConocimiento { get; set; }
        public virtual DbSet<IndiceOcupacionalCapacitaciones> IndiceOcupacionalCapacitaciones { get; set; }
        public virtual DbSet<IndiceOcupacionalComportamientoObservable> IndiceOcupacionalComportamientoObservable { get; set; }
        public virtual DbSet<IndiceOcupacionalConocimientosAdicionales> IndiceOcupacionalConocimientosAdicionales { get; set; }
        public virtual DbSet<IndiceOcupacionalEstudio> IndiceOcupacionalEstudio { get; set; }
        public virtual DbSet<IndiceOcupacionalExperienciaLaboralRequerida> IndiceOcupacionalExperienciaLaboralRequerida { get; set; }
        public virtual DbSet<IndiceOcupacionalModalidadPartida> IndiceOcupacionalModalidadPartida { get; set; }
        public virtual DbSet<Induccion> Induccion { get; set; }
        public virtual DbSet<InformeUath> InformeUath { get; set; }
        public virtual DbSet<InformeViatico> InformeViatico { get; set; }
        public virtual DbSet<IngresoEgresoRmu> IngresoEgresoRmu { get; set; }
        public virtual DbSet<InstitucionFinanciera> InstitucionFinanciera { get; set; }
        public virtual DbSet<Institution> Institution { get; set; }
        public virtual DbSet<InstruccionFormal> InstruccionFormal { get; set; }
        public virtual DbSet<ItemViatico> ItemViatico { get; set; }
        public virtual DbSet<ItinerarioViatico> ItinerarioViatico { get; set; }
        public virtual DbSet<LibroActivoFijo> LibroActivoFijo { get; set; }
        public virtual DbSet<Libros> Libros { get; set; }
        public virtual DbSet<Liquidacion> Liquidacion { get; set; }
        public virtual DbSet<MaestroArticuloSucursal> MaestroArticuloSucursal { get; set; }
        public virtual DbSet<MaestroDetalleArticulo> MaestroDetalleArticulo { get; set; }
        public virtual DbSet<MantenimientoActivoFijo> MantenimientoActivoFijo { get; set; }
        public virtual DbSet<ManualPuesto> ManualPuesto { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<MaterialApoyo> MaterialApoyo { get; set; }
        public virtual DbSet<MaterialInduccion> MaterialInduccion { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<ModalidadPartida> ModalidadPartida { get; set; }
        public virtual DbSet<Modelo> Modelo { get; set; }
        public virtual DbSet<ModosScializacion> ModosScializacion { get; set; }
        public virtual DbSet<MotivoAsiento> MotivoAsiento { get; set; }
        public virtual DbSet<MotivoRecepcion> MotivoRecepcion { get; set; }
        public virtual DbSet<Nacionalidad> Nacionalidad { get; set; }
        public virtual DbSet<NacionalidadIndigena> NacionalidadIndigena { get; set; }
        public virtual DbSet<Nivel> Nivel { get; set; }
        public virtual DbSet<NivelConocimiento> NivelConocimiento { get; set; }
        public virtual DbSet<NivelDesarrollo> NivelDesarrollo { get; set; }
        public virtual DbSet<Noticia> Noticia { get; set; }
        public virtual DbSet<OtroIngreso> OtroIngreso { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<PaquetesInformaticos> PaquetesInformaticos { get; set; }
        public virtual DbSet<ParametrosGenerales> ParametrosGenerales { get; set; }
        public virtual DbSet<Parentesco> Parentesco { get; set; }
        public virtual DbSet<Parroquia> Parroquia { get; set; }
        public virtual DbSet<PartidaGeneral> PartidaGeneral { get; set; }
        public virtual DbSet<PartidasFase> PartidasFase { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<PersonaCapacitacion> PersonaCapacitacion { get; set; }
        public virtual DbSet<PersonaDiscapacidad> PersonaDiscapacidad { get; set; }
        public virtual DbSet<PersonaEnfermedad> PersonaEnfermedad { get; set; }
        public virtual DbSet<PersonaEstudio> PersonaEstudio { get; set; }
        public virtual DbSet<PersonaPaquetesInformaticos> PersonaPaquetesInformaticos { get; set; }
        public virtual DbSet<PersonaSustituto> PersonaSustituto { get; set; }
        public virtual DbSet<PieFirma> PieFirma { get; set; }
        public virtual DbSet<PlanGestionCambio> PlanGestionCambio { get; set; }
        public virtual DbSet<PlanificacionHe> PlanificacionHe { get; set; }
        public virtual DbSet<Pregunta> Pregunta { get; set; }
        public virtual DbSet<PreguntaRespuesta> PreguntaRespuesta { get; set; }
        public virtual DbSet<Proceso> Proceso { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<Provincia> Provincia { get; set; }
        public virtual DbSet<Provisiones> Provisiones { get; set; }
        public virtual DbSet<RealizaExamenInduccion> RealizaExamenInduccion { get; set; }
        public virtual DbSet<RecepcionActivoFijo> RecepcionActivoFijo { get; set; }
        public virtual DbSet<RecepcionActivoFijoDetalle> RecepcionActivoFijoDetalle { get; set; }
        public virtual DbSet<RecepcionArticulos> RecepcionArticulos { get; set; }
        public virtual DbSet<RegimenLaboral> RegimenLaboral { get; set; }
        public virtual DbSet<RegistroEntradaSalida> RegistroEntradaSalida { get; set; }
        public virtual DbSet<RelacionLaboral> RelacionLaboral { get; set; }
        public virtual DbSet<RelacionesInternasExternas> RelacionesInternasExternas { get; set; }
        public virtual DbSet<Relevancia> Relevancia { get; set; }
        public virtual DbSet<RequisitosNoCumple> RequisitosNoCumple { get; set; }
        public virtual DbSet<Respuesta> Respuesta { get; set; }
        public virtual DbSet<Rmu> Rmu { get; set; }
        public virtual DbSet<RolPagoDetalle> RolPagoDetalle { get; set; }
        public virtual DbSet<RolPagos> RolPagos { get; set; }
        public virtual DbSet<RolPuesto> RolPuesto { get; set; }
        public virtual DbSet<Rubro> Rubro { get; set; }
        public virtual DbSet<RubroLiquidacion> RubroLiquidacion { get; set; }
        public virtual DbSet<Sexo> Sexo { get; set; }
        public virtual DbSet<SituacionPropuesta> SituacionPropuesta { get; set; }
        public virtual DbSet<SolicitudAcumulacionDecimos> SolicitudAcumulacionDecimos { get; set; }
        public virtual DbSet<SolicitudAnticipo> SolicitudAnticipo { get; set; }
        public virtual DbSet<SolicitudCertificadoPersonal> SolicitudCertificadoPersonal { get; set; }
        public virtual DbSet<SolicitudHorasExtras> SolicitudHorasExtras { get; set; }
        public virtual DbSet<SolicitudLiquidacionHaberes> SolicitudLiquidacionHaberes { get; set; }
        public virtual DbSet<SolicitudModificacionFichaEmpleado> SolicitudModificacionFichaEmpleado { get; set; }
        public virtual DbSet<SolicitudPermiso> SolicitudPermiso { get; set; }
        public virtual DbSet<SolicitudPlanificacionVacaciones> SolicitudPlanificacionVacaciones { get; set; }
        public virtual DbSet<SolicitudProveeduria> SolicitudProveeduria { get; set; }
        public virtual DbSet<SolicitudProveeduriaDetalle> SolicitudProveeduriaDetalle { get; set; }
        public virtual DbSet<SolicitudTipoViatico> SolicitudTipoViatico { get; set; }
        public virtual DbSet<SolicitudVacaciones> SolicitudVacaciones { get; set; }
        public virtual DbSet<SolicitudViatico> SolicitudViatico { get; set; }
        public virtual DbSet<SubClaseActivoFijo> SubClaseActivoFijo { get; set; }
        public virtual DbSet<SubClaseArticulo> SubClaseArticulo { get; set; }
        public virtual DbSet<SubcategoryInstitution> SubcategoryInstitution { get; set; }
        public virtual DbSet<Sucursal> Sucursal { get; set; }
        public virtual DbSet<TablaDepreciacion> TablaDepreciacion { get; set; }
        public virtual DbSet<Temporal> Temporal { get; set; }
        public virtual DbSet<TipoAccionPersonal> TipoAccionPersonal { get; set; }
        public virtual DbSet<TipoActivoFijo> TipoActivoFijo { get; set; }
        public virtual DbSet<TipoArticulo> TipoArticulo { get; set; }
        public virtual DbSet<TipoCalificacion> TipoCalificacion { get; set; }
        public virtual DbSet<TipoCertificado> TipoCertificado { get; set; }
        public virtual DbSet<TipoCesacionFuncion> TipoCesacionFuncion { get; set; }
        public virtual DbSet<TipoConcurso> TipoConcurso { get; set; }
        public virtual DbSet<TipoDiscapacidad> TipoDiscapacidad { get; set; }
        public virtual DbSet<TipoDiscapacidadSustituto> TipoDiscapacidadSustituto { get; set; }
        public virtual DbSet<TipoEnfermedad> TipoEnfermedad { get; set; }
        public virtual DbSet<TipoExamenComplementario> TipoExamenComplementario { get; set; }
        public virtual DbSet<TipoIdentificacion> TipoIdentificacion { get; set; }
        public virtual DbSet<TipoMovimientoInterno> TipoMovimientoInterno { get; set; }
        public virtual DbSet<TipoNombramiento> TipoNombramiento { get; set; }
        public virtual DbSet<TipoPermiso> TipoPermiso { get; set; }
        public virtual DbSet<TipoProvision> TipoProvision { get; set; }
        public virtual DbSet<TipoRmu> TipoRmu { get; set; }
        public virtual DbSet<TipoSangre> TipoSangre { get; set; }
        public virtual DbSet<TipoTransporte> TipoTransporte { get; set; }
        public virtual DbSet<TipoViatico> TipoViatico { get; set; }
        public virtual DbSet<Titulo> Titulo { get; set; }
        public virtual DbSet<TrabajoEquipoIniciativaLiderazgo> TrabajoEquipoIniciativaLiderazgo { get; set; }
        public virtual DbSet<TransferenciaActivoFijo> TransferenciaActivoFijo { get; set; }
        public virtual DbSet<TransferenciaActivoFijoDetalle> TransferenciaActivoFijoDetalle { get; set; }
        public virtual DbSet<TransferenciaArticulo> TransferenciaArticulo { get; set; }
        public virtual DbSet<TrayectoriaLaboral> TrayectoriaLaboral { get; set; }
        public virtual DbSet<UnidadMedida> UnidadMedida { get; set; }
        public virtual DbSet<ValidacionInmediatoSuperior> ValidacionInmediatoSuperior { get; set; }

        // Unable to generate entity type for table 'dbo.CATEGORY'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.COMMENT'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.SUBCATEGORY'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.dbo.EstadoSolicitudVacacion'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=developmentds.eastus.cloudapp.azure.com;Database=zeusbde;User Id=sa;password=Digital_2018;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccionPersonal>(entity =>
            {
                entity.HasKey(e => e.IdAccionPersonal)
                    .HasName("PK188");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15463");

                entity.HasIndex(e => e.IdTipoAccionPersonal)
                    .HasName("Ref305462");

                entity.Property(e => e.Explicacion).HasColumnType("text");

                entity.Property(e => e.Numero).HasColumnType("varchar(20)");

                entity.Property(e => e.Solicitud).HasColumnType("text");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.AccionPersonal)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEmpleado463");

                entity.HasOne(d => d.IdTipoAccionPersonalNavigation)
                    .WithMany(p => p.AccionPersonal)
                    .HasForeignKey(d => d.IdTipoAccionPersonal)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefTipoAccionPersonal462");
            });

            modelBuilder.Entity<ActividadesAnalisisOcupacional>(entity =>
            {
                entity.HasKey(e => e.IdActividadesAnalisisOcupacional)
                    .HasName("PK192");

                entity.HasIndex(e => e.IdFormularioAnalisisOcupacional)
                    .HasName("Ref107307");

                entity.Property(e => e.Actividades).HasColumnType("varchar(250)");

                entity.HasOne(d => d.IdFormularioAnalisisOcupacionalNavigation)
                    .WithMany(p => p.ActividadesAnalisisOcupacional)
                    .HasForeignKey(d => d.IdFormularioAnalisisOcupacional)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefFormularioAnalisisOcupacional307");
            });

            modelBuilder.Entity<ActividadesEsenciales>(entity =>
            {
                entity.HasKey(e => e.IdActividadesEsenciales)
                    .HasName("PK221");

                entity.Property(e => e.Descripcion).HasMaxLength(100);
            });

            modelBuilder.Entity<ActividadesGestionCambio>(entity =>
            {
                entity.HasKey(e => e.IdActividadesGestionCambio)
                    .HasName("PK260");

                entity.HasIndex(e => e.IdPlanGestionCambio)
                    .HasName("Ref262401");

                entity.Property(e => e.Descripcion).HasMaxLength(200);

                entity.HasOne(d => d.IdPlanGestionCambioNavigation)
                    .WithMany(p => p.ActividadesGestionCambio)
                    .HasForeignKey(d => d.IdPlanGestionCambio)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefPlanGestionCambio401");
            });

            modelBuilder.Entity<ActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdActivoFijo)
                    .HasName("PK_ActivoFijo");

                entity.HasIndex(e => e.IdCiudad)
                    .HasName("IX_ActivoFijo_IdCiudad");

                entity.HasIndex(e => e.IdCodigoActivoFijo)
                    .HasName("IX_ActivoFijo_IdCodigoActivoFijo");

                entity.HasIndex(e => e.IdLibroActivoFijo)
                    .HasName("IX_ActivoFijo_IdLibroActivoFijo");

                entity.HasIndex(e => e.IdModelo)
                    .HasName("IX_ActivoFijo_IdModelo");

                entity.HasIndex(e => e.IdSubClaseActivoFijo)
                    .HasName("IX_ActivoFijo_IdSubClaseActivoFijo");

                entity.HasIndex(e => e.IdUnidadMedida)
                    .HasName("IX_ActivoFijo_IdUnidadMedida");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Serie)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ubicacion)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ValorCompra).HasColumnType("decimal");

                entity.HasOne(d => d.IdCiudadNavigation)
                    .WithMany(p => p.ActivoFijo)
                    .HasForeignKey(d => d.IdCiudad)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IdCodigoActivoFijoNavigation)
                    .WithMany(p => p.ActivoFijo)
                    .HasForeignKey(d => d.IdCodigoActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IdLibroActivoFijoNavigation)
                    .WithMany(p => p.ActivoFijo)
                    .HasForeignKey(d => d.IdLibroActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IdModeloNavigation)
                    .WithMany(p => p.ActivoFijo)
                    .HasForeignKey(d => d.IdModelo)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IdSubClaseActivoFijoNavigation)
                    .WithMany(p => p.ActivoFijo)
                    .HasForeignKey(d => d.IdSubClaseActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IdUnidadMedidaNavigation)
                    .WithMany(p => p.ActivoFijo)
                    .HasForeignKey(d => d.IdUnidadMedida)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ActivoFijoMotivoBaja>(entity =>
            {
                entity.HasKey(e => e.IdActivoFijoMotivoBaja)
                    .HasName("PK_ActivoFijoMotivoBaja");

                entity.Property(e => e.Nombre).HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<ActivosFijosAlta>(entity =>
            {
                entity.HasKey(e => e.IdActivoFijo)
                    .HasName("PK_ActivosFijosAlta_1");

                entity.Property(e => e.IdActivoFijo).ValueGeneratedNever();

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.HasOne(d => d.IdActivoFijoNavigation)
                    .WithOne(p => p.ActivosFijosAlta)
                    .HasForeignKey<ActivosFijosAlta>(d => d.IdActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ActivosFijosAlta_ActivoFijo");

                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithMany(p => p.ActivosFijosAlta)
                    .HasForeignKey(d => d.IdFactura)
                    .HasConstraintName("FK_ActivosFijosAlta_Factura");
            });

            modelBuilder.Entity<ActivosFijosBaja>(entity =>
            {
                entity.HasKey(e => e.IdBaja)
                    .HasName("PK_ActivosFijosBaja");

                entity.Property(e => e.FechaBaja).HasColumnType("datetime");

                entity.HasOne(d => d.IdActivoNavigation)
                    .WithMany(p => p.ActivosFijosBaja)
                    .HasForeignKey(d => d.IdActivo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ActivosFijosBaja_ActivoFijo");

                entity.HasOne(d => d.IdMotivoBajaNavigation)
                    .WithMany(p => p.ActivosFijosBaja)
                    .HasForeignKey(d => d.IdMotivoBaja)
                    .HasConstraintName("FK_ActivosFijosBaja_ActivoFijoMotivoBaja");
            });

            modelBuilder.Entity<AdministracionTalentoHumano>(entity =>
            {
                entity.HasKey(e => e.IdAdministracionTalentoHumano)
                    .HasName("PK198");

                entity.HasIndex(e => e.EmpleadoResponsable)
                    .HasName("Ref15323");

                entity.HasIndex(e => e.IdFormularioAnalisisOcupacional)
                    .HasName("Ref107317");

                entity.HasIndex(e => e.IdRolPuesto)
                    .HasName("Ref63315");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.HasOne(d => d.IdFormularioAnalisisOcupacionalNavigation)
                    .WithMany(p => p.AdministracionTalentoHumano)
                    .HasForeignKey(d => d.IdFormularioAnalisisOcupacional)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefFormularioAnalisisOcupacional317");

                entity.HasOne(d => d.IdRolPuestoNavigation)
                    .WithMany(p => p.AdministracionTalentoHumano)
                    .HasForeignKey(d => d.IdRolPuesto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefRolPuesto315");
            });

            modelBuilder.Entity<Ambito>(entity =>
            {
                entity.HasKey(e => e.IdAmbito)
                    .HasName("PK_Ambito");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<AntecedentesFamiliares>(entity =>
            {
                entity.HasKey(e => e.IdAntecedentesFamiliares)
                    .HasName("PK_AntecedentesFamiliares");

                entity.Property(e => e.Enfermedad)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.Observaciones)
                    .IsRequired()
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.Parentesco)
                    .IsRequired()
                    .HasColumnName("parentesco")
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdFichaMedicaNavigation)
                    .WithMany(p => p.AntecedentesFamiliares)
                    .HasForeignKey(d => d.IdFichaMedica)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AntecedentesFamiliares_AntecedentesFamiliares");
            });

            modelBuilder.Entity<AntecedentesLaborales>(entity =>
            {
                entity.HasKey(e => e.IdAntecedentesLaborales)
                    .HasName("PK_AntecedentesLaborales");

                entity.Property(e => e.Cargo)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.DetalleRiesgosExpuesto)
                    .IsRequired()
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.Empresa)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.EppUtilizados).HasColumnType("varchar(250)");

                entity.Property(e => e.TiempoTrabajo)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.HasOne(d => d.IdFichaMedicaNavigation)
                    .WithMany(p => p.AntecedentesLaborales)
                    .HasForeignKey(d => d.IdFichaMedica)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AntecedentesLaborales_FichaMedica");
            });

            modelBuilder.Entity<AprobacionViatico>(entity =>
            {
                entity.HasKey(e => e.IdAprobacionViatico)
                    .HasName("PK254");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15392");

                entity.HasIndex(e => e.IdSolicitudViatico)
                    .HasName("Ref77390");

                entity.Property(e => e.ValorAdescontar)
                    .HasColumnName("ValorADescontar")
                    .HasColumnType("char(10)");

                entity.Property(e => e.ValorJustificado).HasColumnType("decimal");

                entity.HasOne(d => d.IdSolicitudViaticoNavigation)
                    .WithMany(p => p.AprobacionViatico)
                    .HasForeignKey(d => d.IdSolicitudViatico)
                    .HasConstraintName("RefSolicitudViatico390");
            });

            modelBuilder.Entity<AreaConocimiento>(entity =>
            {
                entity.HasKey(e => e.IdAreaConocimiento)
                    .HasName("PK219");

                entity.Property(e => e.Descripcion).HasMaxLength(100);
            });

            modelBuilder.Entity<Articulo>(entity =>
            {
                entity.HasKey(e => e.IdArticulo)
                    .HasName("PK_Articulo");

                entity.HasIndex(e => e.IdModelo)
                    .HasName("IX_Articulo_IdModelo");

                entity.HasIndex(e => e.IdSubClaseArticulo)
                    .HasName("IX_Articulo_IdSubClaseArticulo");

                entity.HasIndex(e => e.IdUnidadMedida)
                    .HasName("IX_Articulo_IdUnidadMedida");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdModeloNavigation)
                    .WithMany(p => p.Articulo)
                    .HasForeignKey(d => d.IdModelo);

                entity.HasOne(d => d.IdSubClaseArticuloNavigation)
                    .WithMany(p => p.Articulo)
                    .HasForeignKey(d => d.IdSubClaseArticulo)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IdUnidadMedidaNavigation)
                    .WithMany(p => p.Articulo)
                    .HasForeignKey(d => d.IdUnidadMedida);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId })
                    .HasName("PK_dbo.AspNetUserLogins");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_dbo.AspNetUserRoles");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_RoleId");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.RoleId).HasMaxLength(128);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.UserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AvanceGestionCambio>(entity =>
            {
                entity.HasKey(e => e.IdAvanceGestionCambio)
                    .HasName("PK263");

                entity.HasIndex(e => e.IdActividadesGestionCambio)
                    .HasName("Ref260402");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.HasOne(d => d.IdActividadesGestionCambioNavigation)
                    .WithMany(p => p.AvanceGestionCambio)
                    .HasForeignKey(d => d.IdActividadesGestionCambio)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefActividadesGestionCambio402");
            });

            modelBuilder.Entity<BrigadaSso>(entity =>
            {
                entity.HasKey(e => e.IdBrigadaSso)
                    .HasName("PK66");

                entity.ToTable("BrigadaSSO");

                entity.Property(e => e.IdBrigadaSso).HasColumnName("IdBrigadaSSO");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<BrigadaSsorol>(entity =>
            {
                entity.HasKey(e => e.IdBrigadaSsorol)
                    .HasName("PK67");

                entity.ToTable("BrigadaSSORol");

                entity.HasIndex(e => e.IdBrigadaSso)
                    .HasName("Ref6696");

                entity.Property(e => e.IdBrigadaSsorol).HasColumnName("IdBrigadaSSORol");

                entity.Property(e => e.IdBrigadaSso).HasColumnName("IdBrigadaSSO");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdBrigadaSsoNavigation)
                    .WithMany(p => p.BrigadaSsorol)
                    .HasForeignKey(d => d.IdBrigadaSso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefBrigadaSSO96");
            });

            modelBuilder.Entity<Calificacion>(entity =>
            {
                entity.HasKey(e => e.IdCalificacion)
                    .HasName("PK_Calificacion");

                entity.HasOne(d => d.IdCandidatoConcursoNavigation)
                    .WithMany(p => p.Calificacion)
                    .HasForeignKey(d => d.IdCandidatoConcurso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Calificacion_CandidatoConcurso");

                entity.HasOne(d => d.IdTipoCalificacionNavigation)
                    .WithMany(p => p.Calificacion)
                    .HasForeignKey(d => d.IdTipoCalificacion)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Calificacion_TipoCalificacion");
            });

            modelBuilder.Entity<Candidato>(entity =>
            {
                entity.HasKey(e => e.IdCandidato)
                    .HasName("PK_Canditato");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Candidato)
                    .HasForeignKey(d => d.IdPersona)
                    .HasConstraintName("FK_Candidato_Persona");
            });

            modelBuilder.Entity<CandidatoConcurso>(entity =>
            {
                entity.HasKey(e => e.IdCandidatoConcurso)
                    .HasName("PK270");

                entity.HasIndex(e => e.IdCandidato)
                    .HasName("Ref267419");

                entity.HasIndex(e => e.IdPartidasFase)
                    .HasName("Ref273430");

                entity.Property(e => e.CodigoSocioEmpleo).HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdCandidatoNavigation)
                    .WithMany(p => p.CandidatoConcurso)
                    .HasForeignKey(d => d.IdCandidato)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefCanditato419");

                entity.HasOne(d => d.IdPartidasFaseNavigation)
                    .WithMany(p => p.CandidatoConcurso)
                    .HasForeignKey(d => d.IdPartidasFase)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefPartidasFase430");
            });

            modelBuilder.Entity<Capacitacion>(entity =>
            {
                entity.HasKey(e => e.IdCapacitacion)
                    .HasName("PK12_1");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<CapacitacionAreaConocimiento>(entity =>
            {
                entity.HasKey(e => e.IdCapacitacionAreaConocimiento)
                    .HasName("PK169");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<CapacitacionEncuesta>(entity =>
            {
                entity.HasKey(e => e.IdCapacitacionEncuesta)
                    .HasName("PK171");

                entity.HasIndex(e => e.IdCapacitacionRecibida)
                    .HasName("Ref315470");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15485");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.IdCapacitacionRecibidaNavigation)
                    .WithMany(p => p.CapacitacionEncuesta)
                    .HasForeignKey(d => d.IdCapacitacionRecibida)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefCapacitacionRecibida470");
            });

            modelBuilder.Entity<CapacitacionModalidad>(entity =>
            {
                entity.HasKey(e => e.IdCapacitacionModalidad)
                    .HasName("PK178");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<CapacitacionPlanificacion>(entity =>
            {
                entity.HasKey(e => e.IdCapacitacionPlanificacion)
                    .HasName("PK167");

                entity.HasIndex(e => e.IdCapacitacionModalidad)
                    .HasName("Ref322478");

                entity.HasIndex(e => e.IdCapacitacionTemario)
                    .HasName("Ref313479");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15484");

                entity.Property(e => e.Presupuesto).HasColumnType("decimal");

                entity.HasOne(d => d.IdCapacitacionModalidadNavigation)
                    .WithMany(p => p.CapacitacionPlanificacion)
                    .HasForeignKey(d => d.IdCapacitacionModalidad)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefCapacitacionModalidad478");

                entity.HasOne(d => d.IdCapacitacionTemarioNavigation)
                    .WithMany(p => p.CapacitacionPlanificacion)
                    .HasForeignKey(d => d.IdCapacitacionTemario)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefCapacitacionTemario479");
            });

            modelBuilder.Entity<CapacitacionPregunta>(entity =>
            {
                entity.HasKey(e => e.IdCapacitacionPregunta)
                    .HasName("PK172");

                entity.HasIndex(e => e.IdCapacitacionTipoPregunta)
                    .HasName("Ref319473");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.IdCapacitacionEncuestaNavigation)
                    .WithMany(p => p.CapacitacionPregunta)
                    .HasForeignKey(d => d.IdCapacitacionEncuesta)
                    .HasConstraintName("FK_CapacitacionPregunta_CapacitacionEncuesta");

                entity.HasOne(d => d.IdCapacitacionRespuestaNavigation)
                    .WithMany(p => p.CapacitacionPregunta)
                    .HasForeignKey(d => d.IdCapacitacionRespuesta)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CapacitacionPregunta_CapacitacionRespuesta");

                entity.HasOne(d => d.IdCapacitacionTipoPreguntaNavigation)
                    .WithMany(p => p.CapacitacionPregunta)
                    .HasForeignKey(d => d.IdCapacitacionTipoPregunta)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefCapacitacionTipoPregunta473");
            });

            modelBuilder.Entity<CapacitacionProveedor>(entity =>
            {
                entity.HasKey(e => e.IdCapacitacionProveedor)
                    .HasName("PK175");

                entity.Property(e => e.Direccion).HasColumnType("varchar(40)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Telefono).HasColumnType("varchar(20)");

                entity.HasOne(d => d.IdPaisNavigation)
                    .WithMany(p => p.CapacitacionProveedor)
                    .HasForeignKey(d => d.IdPais)
                    .HasConstraintName("FK_CapacitacionProveedor_Pais");
            });

            modelBuilder.Entity<CapacitacionRecibida>(entity =>
            {
                entity.HasKey(e => e.IdCapacitacionRecibida)
                    .HasName("PK170");

                entity.HasIndex(e => e.CostoReal)
                    .HasName("Ref313481");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15483");

                entity.Property(e => e.CostoReal).HasColumnType("decimal");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.IdCapacitacionTemarioProveedorNavigation)
                    .WithMany(p => p.CapacitacionRecibida)
                    .HasForeignKey(d => d.IdCapacitacionTemarioProveedor)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CapacitacionRecibida_CapacitacionTemarioProveedor");
            });

            modelBuilder.Entity<CapacitacionRespuesta>(entity =>
            {
                entity.HasKey(e => e.IdCapacitacionRespuesta)
                    .HasName("PK173");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(60)");
            });

            modelBuilder.Entity<CapacitacionTemario>(entity =>
            {
                entity.HasKey(e => e.IdCapacitacionTemario)
                    .HasName("PK168");

                entity.HasIndex(e => e.IdCapacitacionAreaConocimiento)
                    .HasName("Ref314480");

                entity.Property(e => e.Tema)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.IdCapacitacionAreaConocimientoNavigation)
                    .WithMany(p => p.CapacitacionTemario)
                    .HasForeignKey(d => d.IdCapacitacionAreaConocimiento)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefCapacitacionAreaConocimiento480");
            });

            modelBuilder.Entity<CapacitacionTemarioProveedor>(entity =>
            {
                entity.HasKey(e => e.IdCapacitacionTemarioProveedor)
                    .HasName("PK177");

                entity.HasIndex(e => e.IdCapacitacionModalidad)
                    .HasName("Ref322477");

                entity.HasIndex(e => e.IdCapacitacionProveedor)
                    .HasName("Ref320475");

                entity.HasIndex(e => e.IdCapacitacionTemario)
                    .HasName("Ref313474");

                entity.Property(e => e.Costo).HasColumnType("decimal");

                entity.HasOne(d => d.IdCapacitacionModalidadNavigation)
                    .WithMany(p => p.CapacitacionTemarioProveedor)
                    .HasForeignKey(d => d.IdCapacitacionModalidad)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefCapacitacionModalidad477");

                entity.HasOne(d => d.IdCapacitacionProveedorNavigation)
                    .WithMany(p => p.CapacitacionTemarioProveedor)
                    .HasForeignKey(d => d.IdCapacitacionProveedor)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CapacitacionTemarioProveedor_CapacitacionProveedor");

                entity.HasOne(d => d.IdCapacitacionTemarioNavigation)
                    .WithMany(p => p.CapacitacionTemarioProveedor)
                    .HasForeignKey(d => d.IdCapacitacionTemario)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CapacitacionTemarioProveedor_CapacitacionTemario");
            });

            modelBuilder.Entity<CapacitacionTipoPregunta>(entity =>
            {
                entity.HasKey(e => e.IdCapacitacionTipoPregunta)
                    .HasName("PK174");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<CatalogoCuenta>(entity =>
            {
                entity.HasKey(e => e.IdCatalogoCuenta)
                    .HasName("PK163");

                entity.HasIndex(e => e.IdCatalogoCuentaHijo)
                    .HasName("Ref310466");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnType("char(10)");

                entity.HasOne(d => d.IdCatalogoCuentaHijoNavigation)
                    .WithMany(p => p.InverseIdCatalogoCuentaHijoNavigation)
                    .HasForeignKey(d => d.IdCatalogoCuentaHijo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefCatalogoCuenta466");
            });

            modelBuilder.Entity<CeseFuncion>(entity =>
            {
                entity.HasKey(e => e.IdCeseFuncion)
                    .HasName("PK_CeseFuncion");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.FechaNotificacion).HasColumnType("date");

                entity.Property(e => e.Observacion).HasMaxLength(250);

                entity.HasOne(d => d.IdTipoCesacionFuncionNavigation)
                    .WithMany(p => p.CeseFuncion)
                    .HasForeignKey(d => d.IdTipoCesacionFuncion)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CeseFuncion_CeseFuncion");
            });

            modelBuilder.Entity<Ciudad>(entity =>
            {
                entity.HasKey(e => e.IdCiudad)
                    .HasName("PK_Ciudad");

                entity.HasIndex(e => e.IdProvincia)
                    .HasName("IX_Ciudad_IdProvincia");

                entity.Property(e => e.Nombre).HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdProvinciaNavigation)
                    .WithMany(p => p.Ciudad)
                    .HasForeignKey(d => d.IdProvincia)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ClaseActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdClaseActivoFijo)
                    .HasName("PK_ClaseActivoFijo");

                entity.HasIndex(e => e.IdTablaDepreciacion)
                    .HasName("IX_ClaseActivoFijo_IdTablaDepreciacion");

                entity.HasIndex(e => e.IdTipoActivoFijo)
                    .HasName("IX_ClaseActivoFijo_IdTipoActivoFijo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdTablaDepreciacionNavigation)
                    .WithMany(p => p.ClaseActivoFijo)
                    .HasForeignKey(d => d.IdTablaDepreciacion)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IdTipoActivoFijoNavigation)
                    .WithMany(p => p.ClaseActivoFijo)
                    .HasForeignKey(d => d.IdTipoActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ClaseArticulo>(entity =>
            {
                entity.HasKey(e => e.IdClaseArticulo)
                    .HasName("PK_ClaseArticulo");

                entity.HasIndex(e => e.IdTipoArticulo)
                    .HasName("IX_ClaseArticulo_IdTipoArticulo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdTipoArticuloNavigation)
                    .WithMany(p => p.ClaseArticulo)
                    .HasForeignKey(d => d.IdTipoArticulo)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CodigoActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdCodigoActivoFijo)
                    .HasName("PK_CodigoActivoFijo");

                entity.Property(e => e.CodigoBarras)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Codigosecuencial)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Complain>(entity =>
            {
                entity.HasKey(e => e.IdComplain)
                    .HasName("PK8");

                entity.ToTable("COMPLAIN");

                entity.HasIndex(e => e.IdSubcategory)
                    .HasName("Ref137");

                entity.HasIndex(e => e.IdUser)
                    .HasName("Ref42");

                entity.Property(e => e.IdComplain)
                    .HasColumnName("idComplain")
                    .HasColumnType("char(10)");

                entity.Property(e => e.Description).HasColumnType("varchar(256)");

                entity.Property(e => e.IdSubcategory)
                    .HasColumnName("idSubcategory")
                    .HasColumnType("char(10)");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Photo).HasColumnType("varchar(20)");

                entity.Property(e => e.Title).HasColumnType("varchar(12)");
            });

            modelBuilder.Entity<ComportamientoObservable>(entity =>
            {
                entity.HasKey(e => e.IdComportamientoObservable)
                    .HasName("PK204");

                entity.HasIndex(e => e.IdDenominacionCompetencia)
                    .HasName("Ref203326");

                entity.HasIndex(e => e.IdNivel)
                    .HasName("Ref205325");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(150)");

                entity.HasOne(d => d.IdDenominacionCompetenciaNavigation)
                    .WithMany(p => p.ComportamientoObservable)
                    .HasForeignKey(d => d.IdDenominacionCompetencia)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefDenominacionCompetencia326");

                entity.HasOne(d => d.IdNivelNavigation)
                    .WithMany(p => p.ComportamientoObservable)
                    .HasForeignKey(d => d.IdNivel)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefNivel325");
            });

            modelBuilder.Entity<ConfiguracionContabilidad>(entity =>
            {
                entity.HasKey(e => e.IdConfiguracionContabilidad)
                    .HasName("PK162");

                entity.HasIndex(e => e.IdCatalogoCuentaD)
                    .HasName("Ref310467");

                entity.HasIndex(e => e.IdCatalogoCuentaH)
                    .HasName("Ref310468");

                entity.Property(e => e.ValorD).HasColumnType("decimal");

                entity.Property(e => e.ValorH).HasColumnType("decimal");

                entity.HasOne(d => d.IdCatalogoCuentaDNavigation)
                    .WithMany(p => p.ConfiguracionContabilidadIdCatalogoCuentaDNavigation)
                    .HasForeignKey(d => d.IdCatalogoCuentaD)
                    .HasConstraintName("RefCatalogoCuenta467");

                entity.HasOne(d => d.IdCatalogoCuentaHNavigation)
                    .WithMany(p => p.ConfiguracionContabilidadIdCatalogoCuentaHNavigation)
                    .HasForeignKey(d => d.IdCatalogoCuentaH)
                    .HasConstraintName("RefCatalogoCuenta468");
            });

            modelBuilder.Entity<ConfiguracionFeriados>(entity =>
            {
                entity.HasKey(e => e.IdConfiguracionFeriado)
                    .HasName("PK_ConfiguracionFeriados");

                entity.Property(e => e.Descripcion).HasMaxLength(250);

                entity.Property(e => e.FechaDesde).HasColumnType("date");

                entity.Property(e => e.FechaHasta).HasColumnType("date");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ConfiguracionViatico>(entity =>
            {
                entity.HasKey(e => e.IdConfiguracionViatico)
                    .HasName("PK255");

                entity.Property(e => e.PorCientoAjustificar)
                    .HasColumnName("PorCientoAJustificar")
                    .HasColumnType("char(10)");

                entity.Property(e => e.ValorEntregadoPorDia).HasColumnType("decimal");

                entity.HasOne(d => d.IdRolPuestoNavigation)
                    .WithMany(p => p.ConfiguracionViatico)
                    .HasForeignKey(d => d.IdRolPuesto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ConfiguracionViatico_RolPuesto");
            });

            modelBuilder.Entity<ConfirmacionLectura>(entity =>
            {
                entity.HasKey(e => e.IdConfirmacionLectura)
                    .HasName("PK237");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15363");
            });

            modelBuilder.Entity<ConocimientosAdicionales>(entity =>
            {
                entity.HasKey(e => e.IdConocimientosAdicionales)
                    .HasName("PK222");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<DatosBancarios>(entity =>
            {
                entity.HasKey(e => e.IdDatosBancarios)
                    .HasName("PK24");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref1534");

                entity.HasIndex(e => e.IdInstitucionFinanciera)
                    .HasName("Ref2333");

                entity.Property(e => e.NumeroCuenta)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.DatosBancarios)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DatosBancarios_Empleado");

                entity.HasOne(d => d.IdInstitucionFinancieraNavigation)
                    .WithMany(p => p.DatosBancarios)
                    .HasForeignKey(d => d.IdInstitucionFinanciera)
                    .HasConstraintName("RefInstitucionFinanciera33");
            });

            modelBuilder.Entity<DeclaracionPatrimonioPersonal>(entity =>
            {
                entity.HasKey(e => e.IdDeclaracionPatrimonioPersonal)
                    .HasName("PK106");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15152");

                entity.Property(e => e.FechaDeclaracion).HasColumnType("date");

                entity.Property(e => e.TotalBienInmueble).HasColumnType("decimal");

                entity.Property(e => e.TotalBienMueble).HasColumnType("decimal");

                entity.Property(e => e.TotalEfectivo).HasColumnType("decimal");

                entity.Property(e => e.TotalPasivo).HasColumnType("decimal");

                entity.Property(e => e.TotalPatrimonio).HasColumnType("decimal");
            });

            modelBuilder.Entity<DenominacionCompetencia>(entity =>
            {
                entity.HasKey(e => e.IdDenominacionCompetencia)
                    .HasName("PK203");

                entity.Property(e => e.Definicion).HasColumnType("varchar(1000)");

                entity.Property(e => e.Nombre).HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<Dependencia>(entity =>
            {
                entity.HasKey(e => e.IdDependencia)
                    .HasName("PK_Dependencia");

                entity.HasIndex(e => e.IdSucursal)
                    .HasName("IX_Dependencia_IdSucursal");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.HasOne(d => d.IdProcesoNavigation)
                    .WithMany(p => p.Dependencia)
                    .HasForeignKey(d => d.IdProceso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Dependencia_Proceso");
            });

            modelBuilder.Entity<DependenciaDocumento>(entity =>
            {
                entity.HasKey(e => e.IdDependenciaDocumento)
                    .HasName("PK244");

                entity.HasIndex(e => e.DocumentosSubidos)
                    .HasName("Ref243372");

                entity.HasIndex(e => e.IdDependencia)
                    .HasName("Ref51373");

                entity.HasOne(d => d.DocumentosSubidosNavigation)
                    .WithMany(p => p.DependenciaDocumento)
                    .HasForeignKey(d => d.DocumentosSubidos)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefDocumentosCargados372");

                entity.HasOne(d => d.IdDependenciaNavigation)
                    .WithMany(p => p.DependenciaDocumento)
                    .HasForeignKey(d => d.IdDependencia)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefDependencia373");
            });

            modelBuilder.Entity<DepreciacionActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdDepreciacionActivoFijo)
                    .HasName("PK_DepreciacionActivoFijo");

                entity.HasIndex(e => e.IdActivoFijo)
                    .HasName("IX_DepreciacionActivoFijo_IdActivoFijo");

                entity.Property(e => e.DepreciacionAcumulada).HasColumnType("decimal");

                entity.Property(e => e.ValorResidual).HasColumnType("decimal");

                entity.HasOne(d => d.IdActivoFijoNavigation)
                    .WithMany(p => p.DepreciacionActivoFijo)
                    .HasForeignKey(d => d.IdActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Destreza>(entity =>
            {
                entity.HasKey(e => e.IdDestreza)
                    .HasName("PK40");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<DetalleExamenInduccion>(entity =>
            {
                entity.HasKey(e => e.IdDetalleExamenInduccion)
                    .HasName("PK236");

                entity.HasIndex(e => e.IdPregunta)
                    .HasName("Ref232360");

                entity.HasIndex(e => e.IdRealizaExamenInduccion)
                    .HasName("Ref235358");

                entity.HasIndex(e => e.IdRespuesta)
                    .HasName("Ref233361");

                entity.HasOne(d => d.IdPreguntaNavigation)
                    .WithMany(p => p.DetalleExamenInduccion)
                    .HasForeignKey(d => d.IdPregunta)
                    .HasConstraintName("RefPregunta360");

                entity.HasOne(d => d.IdRealizaExamenInduccionNavigation)
                    .WithMany(p => p.DetalleExamenInduccion)
                    .HasForeignKey(d => d.IdRealizaExamenInduccion)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefRealizaExamenInduccion358");

                entity.HasOne(d => d.IdRespuestaNavigation)
                    .WithMany(p => p.DetalleExamenInduccion)
                    .HasForeignKey(d => d.IdRespuesta)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefRespuesta361");
            });

            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.HasKey(e => e.IdDetalleFactura)
                    .HasName("PK_DetalleFactura");

                entity.HasIndex(e => e.IdArticulo)
                    .HasName("IX_DetalleFactura_IdArticulo");

                entity.HasIndex(e => e.IdFactura)
                    .HasName("IX_DetalleFactura_IdFactura");

                entity.Property(e => e.Precio).HasColumnType("decimal");

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.DetalleFactura)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithMany(p => p.DetalleFactura)
                    .HasForeignKey(d => d.IdFactura)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DiscapacidadSustituto>(entity =>
            {
                entity.HasKey(e => e.IdDiscapacidadSustituto)
                    .HasName("PK_DiscapacidadSustituto");

                entity.Property(e => e.NumeroCarnet)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdPersonaSustitutoNavigation)
                    .WithMany(p => p.DiscapacidadSustituto)
                    .HasForeignKey(d => d.IdPersonaSustituto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DiscapacidadSustituto_PersonaSustituto");

                entity.HasOne(d => d.IdTipoDiscapacidadNavigation)
                    .WithMany(p => p.DiscapacidadSustituto)
                    .HasForeignKey(d => d.IdTipoDiscapacidad)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DiscapacidadSustituto_TipoDiscapacidad");
            });

            modelBuilder.Entity<DocumentoInformacionInstitucional>(entity =>
            {
                entity.HasKey(e => e.IdDocumentoInformacionInstitucional)
                    .HasName("PK105");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Url).HasColumnType("varchar(1024)");
            });

            modelBuilder.Entity<DocumentosCargados>(entity =>
            {
                entity.HasKey(e => e.DocumentosSubidos)
                    .HasName("PK243");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15371");

                entity.Property(e => e.Are).HasColumnType("char(10)");

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.FechaCaducidad).HasColumnType("date");

                entity.Property(e => e.NombreArchivo).HasColumnType("text");

                entity.Property(e => e.Ubicacion).HasColumnType("text");
            });

            modelBuilder.Entity<DocumentosIngreso>(entity =>
            {
                entity.HasKey(e => e.IdDocumentosIngreso)
                    .HasName("PK_DocumentosIngreso");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(500)");
            });

            modelBuilder.Entity<DocumentosIngresoEmpleado>(entity =>
            {
                entity.HasKey(e => e.IdDocumentosIngresoEmpleado)
                    .HasName("PK_DocumentosIngresoEmpleado");

                entity.HasOne(d => d.IdDocumentosIngresoNavigation)
                    .WithMany(p => p.DocumentosIngresoEmpleado)
                    .HasForeignKey(d => d.IdDocumentosIngreso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DocumentosIngresoEmpleado_DocumentosIngreso");
            });

            modelBuilder.Entity<Ejemplo>(entity =>
            {
                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Ejemplo1>(entity =>
            {
                entity.Property(e => e.Nombre).HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdEjemploNavigation)
                    .WithMany(p => p.Ejemplo1)
                    .HasForeignKey(d => d.IdEjemplo)
                    .HasConstraintName("FK_Ejemplo1_Ejemplo");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado)
                    .HasName("PK_Empleado");

                entity.HasIndex(e => e.IdCiudadLugarNacimiento)
                    .HasName("IX_Empleado_CiudadNacimientoIdCiudad");

                entity.HasIndex(e => e.IdDependencia)
                    .HasName("IX_Empleado_IdDependencia");

                entity.HasIndex(e => e.IdPersona)
                    .HasName("IX_Empleado_IdPersona");

                entity.HasIndex(e => e.IdProvinciaLugarSufragio)
                    .HasName("IX_Empleado_ProvinciaSufragioIdProvincia");

                entity.Property(e => e.Foto).HasColumnType("text");

                entity.Property(e => e.IdBrigadaSsorol).HasColumnName("IdBrigadaSSORol");

                entity.Property(e => e.IngresosOtraActividad).HasMaxLength(150);

                entity.Property(e => e.NombreUsuario).HasMaxLength(50);

                entity.Property(e => e.Telefono).HasColumnType("varchar(20)");

                entity.HasOne(d => d.IdBrigadaSsorolNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.IdBrigadaSsorol)
                    .HasConstraintName("FK_Empleado_BrigadaSSORol1");

                entity.HasOne(d => d.IdCiudadLugarNacimientoNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.IdCiudadLugarNacimiento)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Empleado_Ciudad_CiudadNacimientoIdCiudad");

                entity.HasOne(d => d.IdDependenciaNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.IdDependencia);

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IdProvinciaLugarSufragioNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.IdProvinciaLugarSufragio)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Empleado_Provincia_ProvinciaSufragioIdProvincia");
            });

            modelBuilder.Entity<EmpleadoActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdEmpleadoActivoFijo)
                    .HasName("PK_EmpleadoActivoFijo");

                entity.HasIndex(e => e.IdActivoFijo)
                    .HasName("IX_EmpleadoActivoFijo_IdActivoFijo");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_EmpleadoActivoFijo_IdEmpleado");

                entity.HasOne(d => d.IdActivoFijoNavigation)
                    .WithMany(p => p.EmpleadoActivoFijo)
                    .HasForeignKey(d => d.IdActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<EmpleadoContactoEmergencia>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpleadoContactoEmergencia, e.IdPersona })
                    .HasName("PK25");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref1537");

                entity.HasIndex(e => e.IdParentesco)
                    .HasName("Ref2038");

                entity.HasIndex(e => e.IdPersona)
                    .HasName("Ref1735");

                entity.Property(e => e.IdEmpleadoContactoEmergencia).ValueGeneratedOnAdd();

                entity.HasOne(d => d.IdParentescoNavigation)
                    .WithMany(p => p.EmpleadoContactoEmergencia)
                    .HasForeignKey(d => d.IdParentesco)
                    .HasConstraintName("RefParentesco38");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.EmpleadoContactoEmergencia)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefPersona35");
            });

            modelBuilder.Entity<EmpleadoFamiliar>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpleadoFamiliar, e.IdPersona })
                    .HasName("PK19");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref1521");

                entity.HasIndex(e => e.IdParentesco)
                    .HasName("Ref2036");

                entity.HasIndex(e => e.IdPersona)
                    .HasName("Ref1720");

                entity.Property(e => e.IdEmpleadoFamiliar).ValueGeneratedOnAdd();

                entity.HasOne(d => d.IdParentescoNavigation)
                    .WithMany(p => p.EmpleadoFamiliar)
                    .HasForeignKey(d => d.IdParentesco)
                    .HasConstraintName("RefParentesco36");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.EmpleadoFamiliar)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefPersona20");
            });

            modelBuilder.Entity<EmpleadoFormularioCapacitacion>(entity =>
            {
                entity.HasKey(e => e.IdEmpleadoFormularioCapacitacion)
                    .HasName("PK239");

                entity.HasIndex(e => e.AporbadoPor)
                    .HasName("Ref15369");

                entity.HasIndex(e => e.IdFormularioCapacitacion)
                    .HasName("Ref238365");

                entity.HasIndex(e => e.IdServidor)
                    .HasName("Ref15366");

                entity.HasIndex(e => e.ResponsableArea)
                    .HasName("Ref15367");

                entity.HasIndex(e => e.RevisadoPor)
                    .HasName("Ref15368");

                entity.HasOne(d => d.IdFormularioCapacitacionNavigation)
                    .WithMany(p => p.EmpleadoFormularioCapacitacion)
                    .HasForeignKey(d => d.IdFormularioCapacitacion)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefFormularioCapacitacion365");
            });

            modelBuilder.Entity<EmpleadoIe>(entity =>
            {
                entity.HasKey(e => e.IdEmpeladoIe)
                    .HasName("PK153");

                entity.ToTable("EmpleadoIE");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15439");

                entity.HasIndex(e => e.IdIngresoEgresoRmu)
                    .HasName("Ref282434");

                entity.Property(e => e.IdEmpeladoIe).HasColumnName("IdEmpeladoIE");

                entity.Property(e => e.IdIngresoEgresoRmu).HasColumnName("IdIngresoEgresoRMU");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Valor).HasColumnType("decimal");

                entity.HasOne(d => d.IdIngresoEgresoRmuNavigation)
                    .WithMany(p => p.EmpleadoIe)
                    .HasForeignKey(d => d.IdIngresoEgresoRmu)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefIngresoEgresoRMU434");
            });

            modelBuilder.Entity<EmpleadoImpuestoRenta>(entity =>
            {
                entity.HasKey(e => e.IdEmpleadoImpuestoRenta)
                    .HasName("PK81");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15120");

                entity.Property(e => e.FechaDesde).HasColumnType("date");

                entity.Property(e => e.IngresoTotal).HasColumnType("decimal");

                entity.Property(e => e.OtrosIngresos).HasColumnType("decimal");
            });

            modelBuilder.Entity<EmpleadoMovimiento>(entity =>
            {
                entity.HasKey(e => e.IdEmpleadoMovimiento)
                    .HasName("PK126");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15414");

                entity.HasIndex(e => e.IdIndiceOcupacionalModalidadPartida)
                    .HasName("Ref71195");

                entity.HasIndex(e => e.IdTipoMovimientoInterno)
                    .HasName("Ref127193");

                entity.Property(e => e.FechaDesde).HasColumnType("date");

                entity.Property(e => e.FechaHasta).HasColumnType("date");

                entity.HasOne(d => d.IdIndiceOcupacionalModalidadPartidaNavigation)
                    .WithMany(p => p.EmpleadoMovimiento)
                    .HasForeignKey(d => d.IdIndiceOcupacionalModalidadPartida)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefIndiceOcupacionalModalidadPartida195");

                entity.HasOne(d => d.IdTipoMovimientoInternoNavigation)
                    .WithMany(p => p.EmpleadoMovimiento)
                    .HasForeignKey(d => d.IdTipoMovimientoInterno)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefTipoMovimientoInterno193");
            });

            modelBuilder.Entity<EmpleadoNepotismo>(entity =>
            {
                entity.HasKey(e => e.IdEmpleadoNepotismo)
                    .HasName("PK123");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15176");

                entity.HasIndex(e => e.IdEmpleadoFamiliar)
                    .HasName("Ref15177");
            });

            modelBuilder.Entity<EmpleadoSaldoVacaciones>(entity =>
            {
                entity.HasKey(e => e.IdEmpleadoSaldoVacaciones)
                    .HasName("PK74");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15112");

                entity.Property(e => e.SaldoDias).HasColumnType("decimal");

                entity.Property(e => e.SaldoMonetario).HasColumnType("decimal");
            });

            modelBuilder.Entity<EmpleadosFormularioDevengacion>(entity =>
            {
                entity.HasKey(e => e.IdEmpleadosFormularioDevengacion)
                    .HasName("PK246");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15377");

                entity.HasIndex(e => e.IdFormularioDevengacion)
                    .HasName("Ref245376");

                entity.HasOne(d => d.IdFormularioDevengacionNavigation)
                    .WithMany(p => p.EmpleadosFormularioDevengacion)
                    .HasForeignKey(d => d.IdFormularioDevengacion)
                    .HasConstraintName("RefFormularioDevengacion376");
            });

            modelBuilder.Entity<EnfermedadSustituto>(entity =>
            {
                entity.HasKey(e => e.IdEnfermedadSustituto)
                    .HasName("PK_EnfermedadSustituto");

                entity.Property(e => e.InstitucionEmite).HasColumnType("varchar(100)");

                entity.HasOne(d => d.IdPersonaSustitutoNavigation)
                    .WithMany(p => p.EnfermedadSustituto)
                    .HasForeignKey(d => d.IdPersonaSustituto)
                    .HasConstraintName("FK_EnfermedadSustituto_PersonaSustituto");

                entity.HasOne(d => d.IdTipoEnfermedadNavigation)
                    .WithMany(p => p.EnfermedadSustituto)
                    .HasForeignKey(d => d.IdTipoEnfermedad)
                    .HasConstraintName("FK_EnfermedadSustituto_TipoEnfermedad");
            });

            modelBuilder.Entity<EscalaEvaluacionTotal>(entity =>
            {
                entity.HasKey(e => e.IdEscalaEvaluacionTotal)
                    .HasName("PK50");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(1024)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.PorcientoDesde).HasColumnType("decimal");

                entity.Property(e => e.PorcientoHasta).HasColumnType("decimal");
            });

            modelBuilder.Entity<EscalaGrados>(entity =>
            {
                entity.HasKey(e => e.IdEscalaGrados)
                    .HasName("PK60");

                entity.HasIndex(e => e.IdGrupoOcupacional)
                    .HasName("Ref6195");

                entity.Property(e => e.Nombre).HasColumnType("varchar(150)");

                entity.Property(e => e.Remuneracion).HasColumnType("decimal");

                entity.HasOne(d => d.IdGrupoOcupacionalNavigation)
                    .WithMany(p => p.EscalaGrados)
                    .HasForeignKey(d => d.IdGrupoOcupacional)
                    .HasConstraintName("RefGrupoOcupacional95");
            });

            modelBuilder.Entity<EspecificidadExperiencia>(entity =>
            {
                entity.HasKey(e => e.IdEspecificidadExperiencia)
                    .HasName("PK229");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("PK_Estado");

                entity.HasIndex(e => new { e.IdEstado, e.IdSolicitudCertificadoPersonal })
                    .HasName("Ref103220");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<EstadoCivil>(entity =>
            {
                entity.HasKey(e => e.IdEstadoCivil)
                    .HasName("PK_EstadoCivil");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<EstadoTipoAccionPersonal>(entity =>
            {
                entity.HasKey(e => e.IdEstadoTipoAccionPersonal)
                    .HasName("PK_EstadoTipoAccionPersonal");

                entity.Property(e => e.Nombre).HasMaxLength(200);
            });

            modelBuilder.Entity<Estudio>(entity =>
            {
                entity.HasKey(e => e.IdEstudio)
                    .HasName("PK16_1");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Etnia>(entity =>
            {
                entity.HasKey(e => e.IdEtnia)
                    .HasName("PK_Etnia");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Eval001>(entity =>
            {
                entity.HasKey(e => e.IdEval001)
                    .HasName("PK49");

                entity.HasIndex(e => e.IdEmpleadoEvaluado)
                    .HasName("Ref1576");

                entity.HasIndex(e => e.IdEscalaEvaluacionTotal)
                    .HasName("Ref5078");

                entity.HasIndex(e => e.IdEvaluacionActividadesPuestoTrabajo)
                    .HasName("Ref3379");

                entity.HasIndex(e => e.IdEvaluacionCompetenciasTecnicasPuesto)
                    .HasName("Ref3881");

                entity.HasIndex(e => e.IdEvaluacionCompetenciasUniversales)
                    .HasName("Ref4382");

                entity.HasIndex(e => e.IdEvaluacionConocimiento)
                    .HasName("Ref3580");

                entity.HasIndex(e => e.IdEvaluacionTrabajoEquipoIniciativaLiderazgo)
                    .HasName("Ref4683");

                entity.HasIndex(e => e.IdEvaluador)
                    .HasName("Ref182301");

                entity.Property(e => e.FechaEvaluacionDesde).HasColumnType("date");

                entity.Property(e => e.FechaEvaluacionHasta).HasColumnType("date");

                entity.Property(e => e.FechaRegistro).HasColumnType("date");

                entity.Property(e => e.Observaciones).HasColumnType("text");

                entity.HasOne(d => d.IdEscalaEvaluacionTotalNavigation)
                    .WithMany(p => p.Eval001)
                    .HasForeignKey(d => d.IdEscalaEvaluacionTotal)
                    .HasConstraintName("RefEscalaEvaluacionTotal78");

                entity.HasOne(d => d.IdEvaluacionActividadesPuestoTrabajoNavigation)
                    .WithMany(p => p.Eval001)
                    .HasForeignKey(d => d.IdEvaluacionActividadesPuestoTrabajo)
                    .HasConstraintName("RefEvaluacionActividadesPuestoTrabajo79");

                entity.HasOne(d => d.IdEvaluacionCompetenciasTecnicasPuestoNavigation)
                    .WithMany(p => p.Eval001)
                    .HasForeignKey(d => d.IdEvaluacionCompetenciasTecnicasPuesto)
                    .HasConstraintName("RefEvaluacionCompetenciasTecnicasPuesto81");

                entity.HasOne(d => d.IdEvaluacionCompetenciasUniversalesNavigation)
                    .WithMany(p => p.Eval001)
                    .HasForeignKey(d => d.IdEvaluacionCompetenciasUniversales)
                    .HasConstraintName("RefEvaluacionCompetenciasUniversales82");

                entity.HasOne(d => d.IdEvaluacionConocimientoNavigation)
                    .WithMany(p => p.Eval001)
                    .HasForeignKey(d => d.IdEvaluacionConocimiento)
                    .HasConstraintName("RefEvaluacionConocimiento80");

                entity.HasOne(d => d.IdEvaluacionTrabajoEquipoIniciativaLiderazgoNavigation)
                    .WithMany(p => p.Eval001)
                    .HasForeignKey(d => d.IdEvaluacionTrabajoEquipoIniciativaLiderazgo)
                    .HasConstraintName("RefEvaluacionTrabajoEquipoIniciativaLiderazgo83");

                entity.HasOne(d => d.IdEvaluadorNavigation)
                    .WithMany(p => p.Eval001)
                    .HasForeignKey(d => d.IdEvaluador)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEvaluador301");
            });

            modelBuilder.Entity<EvaluacionActividadesPuestoTrabajo>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionActividadesPuestoTrabajo)
                    .HasName("PK33");

                entity.Property(e => e.Nombre).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<EvaluacionActividadesPuestoTrabajoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionActividadesPuestoTrabajoDetalle)
                    .HasName("PK34");

                entity.HasIndex(e => e.IdEvaluacionActividadesPuestoTrabajo)
                    .HasName("Ref3356");

                entity.HasIndex(e => e.IdIndicador)
                    .HasName("Ref3157");

                entity.Property(e => e.DescripcionActividad)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdEvaluacionActividadesPuestoTrabajoNavigation)
                    .WithMany(p => p.EvaluacionActividadesPuestoTrabajoDetalle)
                    .HasForeignKey(d => d.IdEvaluacionActividadesPuestoTrabajo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEvaluacionActividadesPuestoTrabajo56");

                entity.HasOne(d => d.IdIndicadorNavigation)
                    .WithMany(p => p.EvaluacionActividadesPuestoTrabajoDetalle)
                    .HasForeignKey(d => d.IdIndicador)
                    .HasConstraintName("RefIndicador57");
            });

            modelBuilder.Entity<EvaluacionActividadesPuestoTrabajoFactor>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionActividadesPuestoTrabajoFactor)
                    .HasName("PK128");

                entity.HasIndex(e => e.IdEvaluacionActividadesPuestoTrabajo)
                    .HasName("Ref33207");

                entity.HasIndex(e => e.IdFactor)
                    .HasName("Ref32206");

                entity.HasOne(d => d.IdEvaluacionActividadesPuestoTrabajoNavigation)
                    .WithMany(p => p.EvaluacionActividadesPuestoTrabajoFactor)
                    .HasForeignKey(d => d.IdEvaluacionActividadesPuestoTrabajo)
                    .HasConstraintName("RefEvaluacionActividadesPuestoTrabajo207");

                entity.HasOne(d => d.IdFactorNavigation)
                    .WithMany(p => p.EvaluacionActividadesPuestoTrabajoFactor)
                    .HasForeignKey(d => d.IdFactor)
                    .HasConstraintName("RefFactor206");
            });

            modelBuilder.Entity<EvaluacionCompetenciasTecnicasPuesto>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionCompetenciasTecnicasPuesto)
                    .HasName("PK38");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<EvaluacionCompetenciasTecnicasPuestoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionCompetenciasTecnicasPuestoDetalle)
                    .HasName("PK39");

                entity.HasIndex(e => e.IdDestreza)
                    .HasName("Ref4063");

                entity.HasIndex(e => e.IdEvaluacionCompetenciasTecnicasPuesto)
                    .HasName("Ref3862");

                entity.HasIndex(e => e.IdNivelDesarrollo)
                    .HasName("Ref4265");

                entity.HasIndex(e => e.IdRelevancia)
                    .HasName("Ref4164");

                entity.HasOne(d => d.IdDestrezaNavigation)
                    .WithMany(p => p.EvaluacionCompetenciasTecnicasPuestoDetalle)
                    .HasForeignKey(d => d.IdDestreza)
                    .HasConstraintName("RefDestreza63");

                entity.HasOne(d => d.IdEvaluacionCompetenciasTecnicasPuestoNavigation)
                    .WithMany(p => p.EvaluacionCompetenciasTecnicasPuestoDetalle)
                    .HasForeignKey(d => d.IdEvaluacionCompetenciasTecnicasPuesto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEvaluacionCompetenciasTecnicasPuesto62");

                entity.HasOne(d => d.IdNivelDesarrolloNavigation)
                    .WithMany(p => p.EvaluacionCompetenciasTecnicasPuestoDetalle)
                    .HasForeignKey(d => d.IdNivelDesarrollo)
                    .HasConstraintName("RefNivelDesarrollo65");

                entity.HasOne(d => d.IdRelevanciaNavigation)
                    .WithMany(p => p.EvaluacionCompetenciasTecnicasPuestoDetalle)
                    .HasForeignKey(d => d.IdRelevancia)
                    .HasConstraintName("RefRelevancia64");
            });

            modelBuilder.Entity<EvaluacionCompetenciasTecnicasPuestoFactor>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionCompetenciasTecnicasPuestoFactor)
                    .HasName("PK130");

                entity.HasIndex(e => e.IdEvaluacionCompetenciasTecnicasPuesto)
                    .HasName("Ref38209");

                entity.HasIndex(e => e.IdFactor)
                    .HasName("Ref32201");

                entity.HasOne(d => d.IdEvaluacionCompetenciasTecnicasPuestoNavigation)
                    .WithMany(p => p.EvaluacionCompetenciasTecnicasPuestoFactor)
                    .HasForeignKey(d => d.IdEvaluacionCompetenciasTecnicasPuesto)
                    .HasConstraintName("RefEvaluacionCompetenciasTecnicasPuesto209");

                entity.HasOne(d => d.IdFactorNavigation)
                    .WithMany(p => p.EvaluacionCompetenciasTecnicasPuestoFactor)
                    .HasForeignKey(d => d.IdFactor)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefFactor201");
            });

            modelBuilder.Entity<EvaluacionCompetenciasUniversales>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionCompetenciasUniversales)
                    .HasName("PK43");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<EvaluacionCompetenciasUniversalesDetalle>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionCompetenciasUniversalesDetalle)
                    .HasName("PK44");

                entity.HasIndex(e => e.IdDestreza)
                    .HasName("Ref4068");

                entity.HasIndex(e => e.IdEvaluacionCompetenciasUniversales)
                    .HasName("Ref4367");

                entity.HasIndex(e => e.IdFrecuenciaAplicacion)
                    .HasName("Ref4570");

                entity.HasIndex(e => e.IdRelevancia)
                    .HasName("Ref4169");

                entity.HasOne(d => d.IdDestrezaNavigation)
                    .WithMany(p => p.EvaluacionCompetenciasUniversalesDetalle)
                    .HasForeignKey(d => d.IdDestreza)
                    .HasConstraintName("RefDestreza68");

                entity.HasOne(d => d.IdEvaluacionCompetenciasUniversalesNavigation)
                    .WithMany(p => p.EvaluacionCompetenciasUniversalesDetalle)
                    .HasForeignKey(d => d.IdEvaluacionCompetenciasUniversales)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEvaluacionCompetenciasUniversales67");

                entity.HasOne(d => d.IdFrecuenciaAplicacionNavigation)
                    .WithMany(p => p.EvaluacionCompetenciasUniversalesDetalle)
                    .HasForeignKey(d => d.IdFrecuenciaAplicacion)
                    .HasConstraintName("RefFrecuenciaAplicacion70");

                entity.HasOne(d => d.IdRelevanciaNavigation)
                    .WithMany(p => p.EvaluacionCompetenciasUniversalesDetalle)
                    .HasForeignKey(d => d.IdRelevancia)
                    .HasConstraintName("RefRelevancia69");
            });

            modelBuilder.Entity<EvaluacionCompetenciasUniversalesFactor>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionCompetenciasUniversalesFactor)
                    .HasName("PK131");

                entity.HasIndex(e => e.IdEvaluacionCompetenciasUniversales)
                    .HasName("Ref43210");

                entity.HasIndex(e => e.IdFactor)
                    .HasName("Ref32203");

                entity.HasOne(d => d.IdEvaluacionCompetenciasUniversalesNavigation)
                    .WithMany(p => p.EvaluacionCompetenciasUniversalesFactor)
                    .HasForeignKey(d => d.IdEvaluacionCompetenciasUniversales)
                    .HasConstraintName("RefEvaluacionCompetenciasUniversales210");

                entity.HasOne(d => d.IdFactorNavigation)
                    .WithMany(p => p.EvaluacionCompetenciasUniversalesFactor)
                    .HasForeignKey(d => d.IdFactor)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefFactor203");
            });

            modelBuilder.Entity<EvaluacionConocimiento>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionConocimiento)
                    .HasName("PK35");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<EvaluacionConocimientoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionConocimientoDetalle)
                    .HasName("PK36");

                entity.HasIndex(e => e.IdEvaluacionConocimiento)
                    .HasName("Ref3558");

                entity.HasIndex(e => e.IdNivelConocimiento)
                    .HasName("Ref3759");

                entity.HasOne(d => d.IdEvaluacionConocimientoNavigation)
                    .WithMany(p => p.EvaluacionConocimientoDetalle)
                    .HasForeignKey(d => d.IdEvaluacionConocimiento)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEvaluacionConocimiento58");

                entity.HasOne(d => d.IdNivelConocimientoNavigation)
                    .WithMany(p => p.EvaluacionConocimientoDetalle)
                    .HasForeignKey(d => d.IdNivelConocimiento)
                    .HasConstraintName("RefNivelConocimiento59");
            });

            modelBuilder.Entity<EvaluacionConocimientoFactor>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionConocimientoFactor)
                    .HasName("PK129");

                entity.HasIndex(e => e.IdEvaluacionConocimiento)
                    .HasName("Ref35208");

                entity.HasIndex(e => e.IdFactor)
                    .HasName("Ref32199");

                entity.HasOne(d => d.IdEvaluacionConocimientoNavigation)
                    .WithMany(p => p.EvaluacionConocimientoFactor)
                    .HasForeignKey(d => d.IdEvaluacionConocimiento)
                    .HasConstraintName("RefEvaluacionConocimiento208");

                entity.HasOne(d => d.IdFactorNavigation)
                    .WithMany(p => p.EvaluacionConocimientoFactor)
                    .HasForeignKey(d => d.IdFactor)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefFactor199");
            });

            modelBuilder.Entity<EvaluacionInducion>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionInduccion)
                    .HasName("PK231");

                entity.Property(e => e.MinimoAprobar).HasColumnName("MInimoAprobar");

                entity.Property(e => e.Nombre).HasColumnType("char(10)");
            });

            modelBuilder.Entity<EvaluacionTrabajoEquipoIniciativaLiderazgo>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionTrabajoEquipoIniciativaLiderazgo)
                    .HasName("PK46");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.ObservacionesJefeInmediato).HasColumnType("text");
            });

            modelBuilder.Entity<EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionTrabajoEquipoIniciativaLiderazgoDetalle)
                    .HasName("PK47");

                entity.HasIndex(e => e.IdEvaluacionTrabajoEquipoIniciativaLiderazgo)
                    .HasName("Ref4671");

                entity.HasIndex(e => e.IdFrecuenciaAplicacion)
                    .HasName("Ref4574");

                entity.HasIndex(e => e.IdRelevancia)
                    .HasName("Ref4173");

                entity.HasIndex(e => e.IdTrabajoEquipoIniciativaLiderazgo)
                    .HasName("Ref4872");

                entity.HasOne(d => d.IdEvaluacionTrabajoEquipoIniciativaLiderazgoNavigation)
                    .WithMany(p => p.EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle)
                    .HasForeignKey(d => d.IdEvaluacionTrabajoEquipoIniciativaLiderazgo)
                    .HasConstraintName("RefEvaluacionTrabajoEquipoIniciativaLiderazgo71");

                entity.HasOne(d => d.IdFrecuenciaAplicacionNavigation)
                    .WithMany(p => p.EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle)
                    .HasForeignKey(d => d.IdFrecuenciaAplicacion)
                    .HasConstraintName("RefFrecuenciaAplicacion74");

                entity.HasOne(d => d.IdRelevanciaNavigation)
                    .WithMany(p => p.EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle)
                    .HasForeignKey(d => d.IdRelevancia)
                    .HasConstraintName("RefRelevancia73");

                entity.HasOne(d => d.IdTrabajoEquipoIniciativaLiderazgoNavigation)
                    .WithMany(p => p.EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle)
                    .HasForeignKey(d => d.IdTrabajoEquipoIniciativaLiderazgo)
                    .HasConstraintName("RefTrabajoEquipoIniciativaLiderazgo72");
            });

            modelBuilder.Entity<EvaluacionTrabajoEquipoIniciativaLiderazgoFactor>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionTrabajoEquipoIniciativaLiderazgoFactor)
                    .HasName("PK132");

                entity.HasIndex(e => e.IdEvaluacionTrabajoEquipoIniciativaLiderazgo)
                    .HasName("Ref46211");

                entity.HasIndex(e => e.IdFactor)
                    .HasName("Ref32205");

                entity.HasOne(d => d.IdEvaluacionTrabajoEquipoIniciativaLiderazgoNavigation)
                    .WithMany(p => p.EvaluacionTrabajoEquipoIniciativaLiderazgoFactor)
                    .HasForeignKey(d => d.IdEvaluacionTrabajoEquipoIniciativaLiderazgo)
                    .HasConstraintName("RefEvaluacionTrabajoEquipoIniciativaLiderazgo211");

                entity.HasOne(d => d.IdFactorNavigation)
                    .WithMany(p => p.EvaluacionTrabajoEquipoIniciativaLiderazgoFactor)
                    .HasForeignKey(d => d.IdFactor)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefFactor205");
            });

            modelBuilder.Entity<Evaluador>(entity =>
            {
                entity.HasKey(e => e.IdEvaluador)
                    .HasName("PK182");

                entity.HasIndex(e => e.IdDependencia)
                    .HasName("Ref51299");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15300");

                entity.Property(e => e.IdEvaluador).ValueGeneratedNever();

                entity.Property(e => e.Ano).HasColumnType("date");

                entity.HasOne(d => d.IdDependenciaNavigation)
                    .WithMany(p => p.Evaluador)
                    .HasForeignKey(d => d.IdDependencia)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefDependencia299");
            });

            modelBuilder.Entity<ExamenComplementario>(entity =>
            {
                entity.HasKey(e => e.IdExamenComplementario)
                    .HasName("PK_ExamenComplementario");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Resultado)
                    .IsRequired()
                    .HasColumnType("varchar(500)");

                entity.HasOne(d => d.IdFichaMedicaNavigation)
                    .WithMany(p => p.ExamenComplementario)
                    .HasForeignKey(d => d.IdFichaMedica)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ExamenComplementario_FichaMedica");

                entity.HasOne(d => d.IdTipoExamenComplementarioNavigation)
                    .WithMany(p => p.ExamenComplementario)
                    .HasForeignKey(d => d.IdTipoExamenComplementario)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ExamenComplementario_ExamenComplementario");
            });

            modelBuilder.Entity<Exepciones>(entity =>
            {
                entity.HasKey(e => e.IdExepciones)
                    .HasName("PK202");

                entity.HasIndex(e => e.IdValidacionJefe)
                    .HasName("Ref193324");

                entity.Property(e => e.Detalle).HasColumnType("varchar(250)");

                entity.HasOne(d => d.IdValidacionJefeNavigation)
                    .WithMany(p => p.Exepciones)
                    .HasForeignKey(d => d.IdValidacionJefe)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefValidacionInmediatoSuperior324");
            });

            modelBuilder.Entity<ExperienciaLaboralRequerida>(entity =>
            {
                entity.HasKey(e => e.IdExperienciaLaboralRequerida)
                    .HasName("PK230");

                entity.HasIndex(e => e.IdEspecificidadExperiencia)
                    .HasName("Ref229350");

                entity.HasIndex(e => e.IdEstudio)
                    .HasName("Ref214352");

                entity.HasOne(d => d.IdEspecificidadExperienciaNavigation)
                    .WithMany(p => p.ExperienciaLaboralRequerida)
                    .HasForeignKey(d => d.IdEspecificidadExperiencia)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEspecificidadExperiencia350");

                entity.HasOne(d => d.IdEstudioNavigation)
                    .WithMany(p => p.ExperienciaLaboralRequerida)
                    .HasForeignKey(d => d.IdEstudio)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEstudio352");
            });

            modelBuilder.Entity<Factor>(entity =>
            {
                entity.HasKey(e => e.IdFactor)
                    .HasName("PK32");

                entity.Property(e => e.Porciento).HasColumnType("decimal");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.IdFactura)
                    .HasName("PK_Factura");

                entity.HasIndex(e => e.IdMaestroArticuloSucursal)
                    .HasName("IX_Factura_IdMaestroArticuloSucursal");

                entity.HasIndex(e => e.IdProveedor)
                    .HasName("IX_Factura_IdProveedor");

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.IdMaestroArticuloSucursalNavigation)
                    .WithMany(p => p.Factura)
                    .HasForeignKey(d => d.IdMaestroArticuloSucursal)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Factura)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<FacturaViatico>(entity =>
            {
                entity.HasKey(e => e.IdFacturaViatico)
                    .HasName("PK253");

                entity.HasIndex(e => e.AprobadoPor)
                    .HasName("Ref15397");

                entity.HasIndex(e => e.IdItemViatico)
                    .HasName("Ref252388");

                entity.HasIndex(e => e.IdItinerarioViatico)
                    .HasName("Ref251389");

                entity.Property(e => e.FechaAprobacion).HasColumnType("date");

                entity.Property(e => e.FechaFactura).HasColumnType("date");

                entity.Property(e => e.NumeroFactura)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Observaciones).HasColumnType("text");

                entity.Property(e => e.ValorTotalAprobacion).HasColumnType("date");

                entity.Property(e => e.ValorTotalFactura).HasColumnType("decimal");

                entity.HasOne(d => d.IdItemViaticoNavigation)
                    .WithMany(p => p.FacturaViatico)
                    .HasForeignKey(d => d.IdItemViatico)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefItemViatico388");

                entity.HasOne(d => d.IdItinerarioViaticoNavigation)
                    .WithMany(p => p.FacturaViatico)
                    .HasForeignKey(d => d.IdItinerarioViatico)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefItinerarioViatico389");
            });

            modelBuilder.Entity<FichaMedica>(entity =>
            {
                entity.HasKey(e => e.IdFichaMedica)
                    .HasName("PK_FichaMedica");

                entity.Property(e => e.AbdomenHallazgos).HasColumnType("varchar(250)");

                entity.Property(e => e.Alergias).HasColumnType("varchar(250)");

                entity.Property(e => e.AntecedenteMedico).HasColumnType("varchar(500)");

                entity.Property(e => e.AntecedenteQuirurgico).HasColumnType("varchar(500)");

                entity.Property(e => e.Anticoncepcion).HasColumnType("varchar(250)");

                entity.Property(e => e.BocaHallazgos).HasColumnType("varchar(250)");

                entity.Property(e => e.CabezaHallazgos).HasColumnType("varchar(250)");

                entity.Property(e => e.CicloMenstrual).HasColumnType("varchar(50)");

                entity.Property(e => e.CigarrilloDesde).HasColumnType("varchar(50)");

                entity.Property(e => e.CigarrilloHasta).HasColumnType("varchar(50)");

                entity.Property(e => e.CorazonHallazgos).HasColumnType("varchar(250)");

                entity.Property(e => e.CuelloHallazgos).HasColumnType("varchar(250)");

                entity.Property(e => e.DetalleAccidenteEnfermedadOcupacional).HasColumnType("varchar(500)");

                entity.Property(e => e.DetalleEnfermedad).HasColumnType("varchar(500)");

                entity.Property(e => e.Diagnostico)
                    .IsRequired()
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.DrogasDesde).HasColumnType("varchar(50)");

                entity.Property(e => e.DrogasHasta).HasColumnType("varchar(50)");

                entity.Property(e => e.EjerciciosFrecuencia).HasColumnType("varchar(250)");

                entity.Property(e => e.EjerciciosTipo).HasColumnType("varchar(500)");

                entity.Property(e => e.EmpresaAccidente).HasColumnType("varchar(250)");

                entity.Property(e => e.EmpresaEnfermedad).HasColumnType("varchar(250)");

                entity.Property(e => e.ExtremidadesInferioresHallazgos).HasColumnType("varchar(250)");

                entity.Property(e => e.ExtremidadesSuperioresHallazgos).HasColumnType("varchar(250)");

                entity.Property(e => e.FaringeAmigdalasHallazgos).HasColumnType("varchar(250)");

                entity.Property(e => e.FechaAccidente).HasColumnType("date");

                entity.Property(e => e.FechaDiagnostico).HasColumnType("date");

                entity.Property(e => e.FechaFichaMedica).HasColumnType("date");

                entity.Property(e => e.FechaUltimaDosis).HasColumnType("date");

                entity.Property(e => e.FrecuenciaCardiaca).HasColumnType("varchar(100)");

                entity.Property(e => e.FrecuenciaCigarrillo).HasColumnType("varchar(250)");

                entity.Property(e => e.FrecuenciaDrogas).HasColumnType("varchar(250)");

                entity.Property(e => e.FrecuenciaRespiratoria).HasColumnType("varchar(100)");

                entity.Property(e => e.GenitalesHallazgos).HasColumnType("varchar(250)");

                entity.Property(e => e.HabitosObservaciones).HasColumnType("varchar(500)");

                entity.Property(e => e.HerniasHallazgos).HasColumnType("varchar(250)");

                entity.Property(e => e.LicorDesde).HasColumnType("varchar(50)");

                entity.Property(e => e.LicorFrecuencia)
                    .HasColumnName("LIcorFrecuencia")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.LicorHasta)
                    .HasColumnName("LIcorHasta")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.NarizHallazgos).HasColumnType("varchar(250)");

                entity.Property(e => e.OidosHallazgos).HasColumnType("varchar(250)");

                entity.Property(e => e.OjosHallazgos).HasColumnType("varchar(250)");

                entity.Property(e => e.Peso).HasColumnType("varchar(50)");

                entity.Property(e => e.PielHallazgos).HasColumnType("varchar(250)");

                entity.Property(e => e.PrimeraMenstruacion).HasColumnType("date");

                entity.Property(e => e.PulmonesHallazgos).HasColumnType("varchar(250)");

                entity.Property(e => e.Recomendaciones).HasColumnType("varchar(500)");

                entity.Property(e => e.SistemaNerviosoHallazgos).HasColumnType("varchar(250)");

                entity.Property(e => e.Talla).HasColumnType("varchar(50)");

                entity.Property(e => e.TensionArterial).HasColumnType("varchar(100)");

                entity.Property(e => e.UltimaMamografia).HasColumnType("date");

                entity.Property(e => e.UltimaMenstruacion).HasColumnType("date");

                entity.Property(e => e.UltimoPapTest).HasColumnType("date");

                entity.Property(e => e.UsoMedicinaDiaria).HasColumnType("varchar(250)");

                entity.Property(e => e.Vacunas).HasColumnType("varchar(250)");

                entity.Property(e => e.VaricesHallazgos).HasColumnType("varchar(250)");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.FichaMedica)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_FichaMedica_Persona");
            });

            modelBuilder.Entity<FlujoAprobacion>(entity =>
            {
                entity.HasKey(e => e.IdFlujoAprobacion)
                    .HasName("PK_FlujoAprobacion");

                entity.HasOne(d => d.IdTipoAccionPersonalNavigation)
                    .WithMany(p => p.FlujoAprobacion)
                    .HasForeignKey(d => d.IdTipoAccionPersonal)
                    .HasConstraintName("FK_FlujoAprobacion_TipoAccionPersonal");
            });

            modelBuilder.Entity<FondoFinanciamiento>(entity =>
            {
                entity.HasKey(e => e.IdFondoFinanciamiento)
                    .HasName("PK_FondoFinanciamiento");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<FormularioAnalisisOcupacional>(entity =>
            {
                entity.HasKey(e => e.IdFormularioAnalisisOcupacional)
                    .HasName("PK107");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15153");

                entity.Property(e => e.FechaRegistro).HasColumnType("date");

                entity.Property(e => e.MisionPuesto)
                    .IsRequired()
                    .HasColumnType("text");
            });

            modelBuilder.Entity<FormularioCapacitacion>(entity =>
            {
                entity.HasKey(e => e.IdFormularioCapacitacion)
                    .HasName("PK238");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<FormularioDevengacion>(entity =>
            {
                entity.HasKey(e => e.IdFormularioDevengacion)
                    .HasName("PK245");

                entity.HasIndex(e => e.AnalistaDesarrolloInstitucional)
                    .HasName("Ref15381");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15379");

                entity.HasIndex(e => e.IdModosScializacion)
                    .HasName("Ref247375");

                entity.HasIndex(e => e.ResponsableArea)
                    .HasName("Ref15380");

                entity.Property(e => e.ModoSocial).HasColumnType("char(10)");

                entity.HasOne(d => d.IdModosScializacionNavigation)
                    .WithMany(p => p.FormularioDevengacion)
                    .HasForeignKey(d => d.IdModosScializacion)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefModosScializacion375");
            });

            modelBuilder.Entity<FormulasRmu>(entity =>
            {
                entity.HasKey(e => e.IdFormulaRmu)
                    .HasName("PK180");

                entity.ToTable("FormulasRMU");

                entity.Property(e => e.IdFormulaRmu).HasColumnName("IdFormulaRMU");

                entity.Property(e => e.Formula).HasColumnType("varchar(80)");
            });

            modelBuilder.Entity<FrecuenciaAplicacion>(entity =>
            {
                entity.HasKey(e => e.IdFrecuenciaAplicacion)
                    .HasName("PK45");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<GastoRubro>(entity =>
            {
                entity.HasKey(e => e.IdGastoRubro)
                    .HasName("PK82");

                entity.HasIndex(e => e.IdEmpleadoImpuestoRenta)
                    .HasName("Ref81121");

                entity.HasIndex(e => e.IdRubro)
                    .HasName("Ref83122");

                entity.Property(e => e.GastoProyectado).HasColumnType("decimal");

                entity.HasOne(d => d.IdEmpleadoImpuestoRentaNavigation)
                    .WithMany(p => p.GastoRubro)
                    .HasForeignKey(d => d.IdEmpleadoImpuestoRenta)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEmpleadoImpuestoRenta121");

                entity.HasOne(d => d.IdRubroNavigation)
                    .WithMany(p => p.GastoRubro)
                    .HasForeignKey(d => d.IdRubro)
                    .HasConstraintName("RefRubro122");
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.HasKey(e => e.IdGenero)
                    .HasName("PK_Genero");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<GrupoOcupacional>(entity =>
            {
                entity.HasKey(e => e.IdGrupoOcupacional)
                    .HasName("PK61");

                entity.Property(e => e.TipoEscala)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<ImpuestoRentaParametros>(entity =>
            {
                entity.HasKey(e => e.IdImpuestoRentaParametros)
                    .HasName("PK80");

                entity.Property(e => e.ExcesoHasta).HasColumnType("decimal");

                entity.Property(e => e.FraccionBasica).HasColumnType("decimal");
            });

            modelBuilder.Entity<Indicador>(entity =>
            {
                entity.HasKey(e => e.IdIndicador)
                    .HasName("PK31");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<IndiceOcupacional>(entity =>
            {
                entity.HasKey(e => e.IdIndiceOcupacional)
                    .HasName("PK69");

                entity.HasIndex(e => e.IdDependencia)
                    .HasName("Ref5198");

                entity.HasIndex(e => e.IdEscalaGrados)
                    .HasName("Ref60101");

                entity.HasIndex(e => e.IdManualPuesto)
                    .HasName("Ref7099");

                entity.HasIndex(e => e.IdRolPuesto)
                    .HasName("Ref63100");

                entity.Property(e => e.Nivel).HasColumnType("varchar(50)");

                entity.Property(e => e.NumeroPartidaIndividual).HasMaxLength(50);

                entity.HasOne(d => d.IdAmbitoNavigation)
                    .WithMany(p => p.IndiceOcupacional)
                    .HasForeignKey(d => d.IdAmbito)
                    .HasConstraintName("FK_IndiceOcupacional_Ambito");

                entity.HasOne(d => d.IdDependenciaNavigation)
                    .WithMany(p => p.IndiceOcupacional)
                    .HasForeignKey(d => d.IdDependencia)
                    .HasConstraintName("RefDependencia98");

                entity.HasOne(d => d.IdEscalaGradosNavigation)
                    .WithMany(p => p.IndiceOcupacional)
                    .HasForeignKey(d => d.IdEscalaGrados)
                    .HasConstraintName("RefEscalaGrados101");

                entity.HasOne(d => d.IdManualPuestoNavigation)
                    .WithMany(p => p.IndiceOcupacional)
                    .HasForeignKey(d => d.IdManualPuesto)
                    .HasConstraintName("RefManualPuesto99");

                entity.HasOne(d => d.IdModalidadPartidaNavigation)
                    .WithMany(p => p.IndiceOcupacional)
                    .HasForeignKey(d => d.IdModalidadPartida)
                    .HasConstraintName("FK_IndiceOcupacional_ModalidadPartida");

                entity.HasOne(d => d.IdPartidaGeneralNavigation)
                    .WithMany(p => p.IndiceOcupacional)
                    .HasForeignKey(d => d.IdPartidaGeneral)
                    .HasConstraintName("FK_IndiceOcupacional_PartidaGeneral");

                entity.HasOne(d => d.IdRolPuestoNavigation)
                    .WithMany(p => p.IndiceOcupacional)
                    .HasForeignKey(d => d.IdRolPuesto)
                    .HasConstraintName("RefRolPuesto100");
            });

            modelBuilder.Entity<IndiceOcupacionalActividadesEsenciales>(entity =>
            {
                entity.HasKey(e => e.IdIndiceOcupacionalActividadesEsenciales)
                    .HasName("PK223");

                entity.HasIndex(e => e.IdActividadesEsenciales)
                    .HasName("Ref221341");

                entity.HasIndex(e => e.IdIndiceOcupacional)
                    .HasName("Ref69344");

                entity.HasOne(d => d.IdActividadesEsencialesNavigation)
                    .WithMany(p => p.IndiceOcupacionalActividadesEsenciales)
                    .HasForeignKey(d => d.IdActividadesEsenciales)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefActividadesEsenciales341");

                entity.HasOne(d => d.IdIndiceOcupacionalNavigation)
                    .WithMany(p => p.IndiceOcupacionalActividadesEsenciales)
                    .HasForeignKey(d => d.IdIndiceOcupacional)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefIndiceOcupacional344");
            });

            modelBuilder.Entity<IndiceOcupacionalAreaConocimiento>(entity =>
            {
                entity.HasKey(e => e.IdIndiceOcupacionalAreaConocimiento)
                    .HasName("PK220");

                entity.HasIndex(e => e.IdAreaConocimiento)
                    .HasName("Ref219339");

                entity.HasIndex(e => e.IdIndiceOcupacional)
                    .HasName("Ref69340");

                entity.HasOne(d => d.IdAreaConocimientoNavigation)
                    .WithMany(p => p.IndiceOcupacionalAreaConocimiento)
                    .HasForeignKey(d => d.IdAreaConocimiento)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefAreaConocimiento339");

                entity.HasOne(d => d.IdIndiceOcupacionalNavigation)
                    .WithMany(p => p.IndiceOcupacionalAreaConocimiento)
                    .HasForeignKey(d => d.IdIndiceOcupacional)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefIndiceOcupacional340");
            });

            modelBuilder.Entity<IndiceOcupacionalCapacitaciones>(entity =>
            {
                entity.HasKey(e => e.IdIndiceOcupacionalCapacitaciones)
                    .HasName("PK226");

                entity.HasIndex(e => e.IdCapacitacion)
                    .HasName("Ref213346");

                entity.HasIndex(e => e.IdIndiceOcupacional)
                    .HasName("Ref69345");

                entity.HasOne(d => d.IdCapacitacionNavigation)
                    .WithMany(p => p.IndiceOcupacionalCapacitaciones)
                    .HasForeignKey(d => d.IdCapacitacion)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefCapacitacion346");

                entity.HasOne(d => d.IdIndiceOcupacionalNavigation)
                    .WithMany(p => p.IndiceOcupacionalCapacitaciones)
                    .HasForeignKey(d => d.IdIndiceOcupacional)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefIndiceOcupacional345");
            });

            modelBuilder.Entity<IndiceOcupacionalComportamientoObservable>(entity =>
            {
                entity.HasKey(e => e.IdIndiceOcupacionalComportamientoObservable)
                    .HasName("PK227");

                entity.HasIndex(e => e.IdComportamientoObservable)
                    .HasName("Ref204347");

                entity.HasIndex(e => e.IdIndiceOcupacional)
                    .HasName("Ref69348");

                entity.HasOne(d => d.IdComportamientoObservableNavigation)
                    .WithMany(p => p.IndiceOcupacionalComportamientoObservable)
                    .HasForeignKey(d => d.IdComportamientoObservable)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefComportamientoObservable347");

                entity.HasOne(d => d.IdIndiceOcupacionalNavigation)
                    .WithMany(p => p.IndiceOcupacionalComportamientoObservable)
                    .HasForeignKey(d => d.IdIndiceOcupacional)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefIndiceOcupacional348");
            });

            modelBuilder.Entity<IndiceOcupacionalConocimientosAdicionales>(entity =>
            {
                entity.HasKey(e => e.IdIndiceOcupacionalConocimientosAdicionales)
                    .HasName("PK224");

                entity.HasIndex(e => e.IdConocimientosAdicionales)
                    .HasName("Ref222342");

                entity.HasIndex(e => e.IdIndiceOcupacional)
                    .HasName("Ref69343");

                entity.HasOne(d => d.IdConocimientosAdicionalesNavigation)
                    .WithMany(p => p.IndiceOcupacionalConocimientosAdicionales)
                    .HasForeignKey(d => d.IdConocimientosAdicionales)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefConocimientosAdicionales342");

                entity.HasOne(d => d.IdIndiceOcupacionalNavigation)
                    .WithMany(p => p.IndiceOcupacionalConocimientosAdicionales)
                    .HasForeignKey(d => d.IdIndiceOcupacional)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefIndiceOcupacional343");
            });

            modelBuilder.Entity<IndiceOcupacionalEstudio>(entity =>
            {
                entity.HasKey(e => e.IdIndiceOcupacionalEstudio)
                    .HasName("PK218");

                entity.HasIndex(e => e.IdEstudio)
                    .HasName("Ref214338");

                entity.HasIndex(e => e.IdIndiceOcupacional)
                    .HasName("Ref69337");

                entity.HasOne(d => d.IdEstudioNavigation)
                    .WithMany(p => p.IndiceOcupacionalEstudio)
                    .HasForeignKey(d => d.IdEstudio)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEstudio338");

                entity.HasOne(d => d.IdIndiceOcupacionalNavigation)
                    .WithMany(p => p.IndiceOcupacionalEstudio)
                    .HasForeignKey(d => d.IdIndiceOcupacional)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefIndiceOcupacional337");
            });

            modelBuilder.Entity<IndiceOcupacionalExperienciaLaboralRequerida>(entity =>
            {
                entity.HasKey(e => e.IdIndiceOcupacionalExperienciaLaboralRequerida)
                    .HasName("PK_IndiceOcupacionalExperienciaLaboralRequerida");

                entity.HasOne(d => d.IdExperienciaLaboralRequeridaNavigation)
                    .WithMany(p => p.IndiceOcupacionalExperienciaLaboralRequerida)
                    .HasForeignKey(d => d.IdExperienciaLaboralRequerida)
                    .HasConstraintName("FK_IndiceOcupacionalExperienciaLaboralRequerida_ExperienciaLaboralRequerida");

                entity.HasOne(d => d.IdIndiceOcupacionalNavigation)
                    .WithMany(p => p.IndiceOcupacionalExperienciaLaboralRequerida)
                    .HasForeignKey(d => d.IdIndiceOcupacional)
                    .HasConstraintName("FK_IndiceOcupacionalExperienciaLaboralRequerida_IndiceOcupacional");
            });

            modelBuilder.Entity<IndiceOcupacionalModalidadPartida>(entity =>
            {
                entity.HasKey(e => e.IdIndiceOcupacionalModalidadPartida)
                    .HasName("PK71");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15189");

                entity.HasIndex(e => e.IdFondoFinanciamiento)
                    .HasName("Ref59104");

                entity.HasIndex(e => e.IdIndiceOcupacional)
                    .HasName("Ref69103");

                entity.HasIndex(e => e.IdTipoNombramiento)
                    .HasName("Ref58106");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.SalarioReal).HasColumnType("decimal");

                entity.HasOne(d => d.IdFondoFinanciamientoNavigation)
                    .WithMany(p => p.IndiceOcupacionalModalidadPartida)
                    .HasForeignKey(d => d.IdFondoFinanciamiento)
                    .HasConstraintName("RefFondoFinanciamiento104");

                entity.HasOne(d => d.IdIndiceOcupacionalNavigation)
                    .WithMany(p => p.IndiceOcupacionalModalidadPartida)
                    .HasForeignKey(d => d.IdIndiceOcupacional)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefIndiceOcupacional103");

                entity.HasOne(d => d.IdTipoNombramientoNavigation)
                    .WithMany(p => p.IndiceOcupacionalModalidadPartida)
                    .HasForeignKey(d => d.IdTipoNombramiento)
                    .HasConstraintName("RefTipoNombramiento106");
            });

            modelBuilder.Entity<Induccion>(entity =>
            {
                entity.HasKey(e => e.IdInduccion)
                    .HasName("PK_Induccion");

                entity.Property(e => e.Fecha).HasColumnType("date");
            });

            modelBuilder.Entity<InformeUath>(entity =>
            {
                entity.HasKey(e => e.IdInformeUath)
                    .HasName("PK201");

                entity.ToTable("InformeUATH");

                entity.HasIndex(e => e.IdAdministracionTalentoHumano)
                    .HasName("Ref198321");

                entity.HasIndex(e => e.ManualPuestoDestino)
                    .HasName("Ref70319");

                entity.HasIndex(e => e.ManualPuestoOrigen)
                    .HasName("Ref70318");

                entity.Property(e => e.IdInformeUath).HasColumnName("IdInformeUATH");

                entity.HasOne(d => d.IdAdministracionTalentoHumanoNavigation)
                    .WithMany(p => p.InformeUath)
                    .HasForeignKey(d => d.IdAdministracionTalentoHumano)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefAdministracionTalentoHumano321");

                entity.HasOne(d => d.ManualPuestoDestinoNavigation)
                    .WithMany(p => p.InformeUathManualPuestoDestinoNavigation)
                    .HasForeignKey(d => d.ManualPuestoDestino)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefManualPuesto319");

                entity.HasOne(d => d.ManualPuestoOrigenNavigation)
                    .WithMany(p => p.InformeUathManualPuestoOrigenNavigation)
                    .HasForeignKey(d => d.ManualPuestoOrigen)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefManualPuesto318");
            });

            modelBuilder.Entity<InformeViatico>(entity =>
            {
                entity.HasKey(e => e.IdInformeViatico)
                    .HasName("PK257");

                entity.HasIndex(e => e.IdItinerarioViatico)
                    .HasName("Ref251396");

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.HasOne(d => d.IdItinerarioViaticoNavigation)
                    .WithMany(p => p.InformeViatico)
                    .HasForeignKey(d => d.IdItinerarioViatico)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefItinerarioViatico396");
            });

            modelBuilder.Entity<IngresoEgresoRmu>(entity =>
            {
                entity.HasKey(e => e.IdIngresoEgresoRmu)
                    .HasName("PK152");

                entity.ToTable("IngresoEgresoRMU");

                entity.HasIndex(e => e.IdFormulaRmu)
                    .HasName("Ref280437");

                entity.Property(e => e.IdIngresoEgresoRmu).HasColumnName("IdIngresoEgresoRMU");

                entity.Property(e => e.CuentaContable)
                    .IsRequired()
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.IdFormulaRmu).HasColumnName("IdFormulaRMU");

                entity.HasOne(d => d.IdFormulaRmuNavigation)
                    .WithMany(p => p.IngresoEgresoRmu)
                    .HasForeignKey(d => d.IdFormulaRmu)
                    .HasConstraintName("RefFormulasRMU437");
            });

            modelBuilder.Entity<InstitucionFinanciera>(entity =>
            {
                entity.HasKey(e => e.IdInstitucionFinanciera)
                    .HasName("PK23");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Spi).HasColumnName("SPI");
            });

            modelBuilder.Entity<Institution>(entity =>
            {
                entity.HasKey(e => e.IdInstitution)
                    .HasName("PK15");

                entity.ToTable("INSTITUTION");

                entity.Property(e => e.IdInstitution)
                    .HasColumnName("idInstitution")
                    .HasColumnType("char(10)");

                entity.Property(e => e.LinkFacebook).HasColumnType("varchar(12)");

                entity.Property(e => e.LinkTwitter).HasColumnType("varchar(12)");

                entity.Property(e => e.Name).HasColumnType("varchar(12)");
            });

            modelBuilder.Entity<InstruccionFormal>(entity =>
            {
                entity.HasKey(e => e.IdInstruccionFormal)
                    .HasName("PK64");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<ItemViatico>(entity =>
            {
                entity.HasKey(e => e.IdItemViatico)
                    .HasName("PK252");

                entity.Property(e => e.Descripcion).HasMaxLength(100);
            });

            modelBuilder.Entity<ItinerarioViatico>(entity =>
            {
                entity.HasKey(e => e.IdItinerarioViatico)
                    .HasName("PK251");

                entity.HasIndex(e => e.IdSolicitudViatico)
                    .HasName("Ref77384");

                entity.HasIndex(e => e.IdTipoTransporte)
                    .HasName("Ref249385");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(300)");

                entity.Property(e => e.Valor).HasColumnType("decimal");

                entity.HasOne(d => d.IdSolicitudViaticoNavigation)
                    .WithMany(p => p.ItinerarioViatico)
                    .HasForeignKey(d => d.IdSolicitudViatico)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefSolicitudViatico384");

                entity.HasOne(d => d.IdTipoTransporteNavigation)
                    .WithMany(p => p.ItinerarioViatico)
                    .HasForeignKey(d => d.IdTipoTransporte)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefTipoTransporte385");
            });

            modelBuilder.Entity<LibroActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdLibroActivoFijo)
                    .HasName("PK_LibroActivoFijo");

                entity.HasIndex(e => e.IdSucursal)
                    .HasName("IX_LibroActivoFijo_IdSucursal");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.LibroActivoFijo)
                    .HasForeignKey(d => d.IdSucursal);
            });

            modelBuilder.Entity<Libros>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__libros__40F9A2076955C6B1");

                entity.ToTable("libros");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Autor)
                    .HasColumnName("autor")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.Editorial)
                    .HasColumnName("editorial")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Precio)
                    .HasColumnName("precio")
                    .HasColumnType("decimal");

                entity.Property(e => e.Titulo)
                    .HasColumnName("titulo")
                    .HasColumnType("varchar(40)");
            });

            modelBuilder.Entity<Liquidacion>(entity =>
            {
                entity.HasKey(e => e.IdLiquidacion)
                    .HasName("PK182_1");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15465");

                entity.HasIndex(e => e.IdRubroLiquidacion)
                    .HasName("Ref308464");

                entity.Property(e => e.IdLiquidacion).ValueGeneratedNever();

                entity.Property(e => e.Valor).HasColumnType("decimal");

                entity.HasOne(d => d.IdRubroLiquidacionNavigation)
                    .WithMany(p => p.Liquidacion)
                    .HasForeignKey(d => d.IdRubroLiquidacion)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefRubroLiquidacion464");
            });

            modelBuilder.Entity<MaestroArticuloSucursal>(entity =>
            {
                entity.HasKey(e => e.IdMaestroArticuloSucursal)
                    .HasName("PK_MaestroArticuloSucursal");

                entity.HasIndex(e => e.IdSucursal)
                    .HasName("IX_MaestroArticuloSucursal_IdSucursal");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.MaestroArticuloSucursal)
                    .HasForeignKey(d => d.IdSucursal)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<MaestroDetalleArticulo>(entity =>
            {
                entity.HasKey(e => e.IdMaestroDetalleArticulo)
                    .HasName("PK_MaestroDetalleArticulo");

                entity.HasIndex(e => e.IdArticulo)
                    .HasName("IX_MaestroDetalleArticulo_IdArticulo");

                entity.HasIndex(e => e.IdMaestroArticuloSucursal)
                    .HasName("IX_MaestroDetalleArticulo_IdMaestroArticuloSucursal");

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.MaestroDetalleArticulo)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IdMaestroArticuloSucursalNavigation)
                    .WithMany(p => p.MaestroDetalleArticulo)
                    .HasForeignKey(d => d.IdMaestroArticuloSucursal)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<MantenimientoActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdMantenimientoActivoFijo)
                    .HasName("PK_MantenimientoActivoFijo");

                entity.HasIndex(e => e.IdActivoFijo)
                    .HasName("IX_MantenimientoActivoFijo_IdActivoFijo");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_MantenimientoActivoFijo_IdEmpleado");

                entity.Property(e => e.Observaciones)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Valor).HasColumnType("decimal");

                entity.HasOne(d => d.IdActivoFijoNavigation)
                    .WithMany(p => p.MantenimientoActivoFijo)
                    .HasForeignKey(d => d.IdActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ManualPuesto>(entity =>
            {
                entity.HasKey(e => e.IdManualPuesto)
                    .HasName("PK70");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.Mision).HasColumnType("varchar(1000)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(150)");

                entity.HasOne(d => d.IdRelacionesInternasExternasNavigation)
                    .WithMany(p => p.ManualPuesto)
                    .HasForeignKey(d => d.IdRelacionesInternasExternas)
                    .HasConstraintName("FK_ManualPuesto_RelacionesInternasExternas");
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.HasKey(e => e.IdMarca)
                    .HasName("PK_Marca");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MaterialApoyo>(entity =>
            {
                entity.HasKey(e => e.IdMaterialApoyo)
                    .HasName("PK248");

                entity.HasIndex(e => e.IdFormularioDevengacion)
                    .HasName("Ref245378");

                entity.Property(e => e.NombreDocumento).HasColumnType("varchar(70)");

                entity.Property(e => e.Ubicacion).HasColumnType("varchar(70)");

                entity.HasOne(d => d.IdFormularioDevengacionNavigation)
                    .WithMany(p => p.MaterialApoyo)
                    .HasForeignKey(d => d.IdFormularioDevengacion)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefFormularioDevengacion378");
            });

            modelBuilder.Entity<MaterialInduccion>(entity =>
            {
                entity.HasKey(e => e.IdMaterialInduccion)
                    .HasName("PK_MaterialInduccion");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.Url).HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<ModalidadPartida>(entity =>
            {
                entity.HasKey(e => e.IdModalidadPartida)
                    .HasName("PK62");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Modelo>(entity =>
            {
                entity.HasKey(e => e.IdModelo)
                    .HasName("PK_Modelo");

                entity.HasIndex(e => e.IdMarca)
                    .HasName("IX_Modelo_IdMarca");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdMarcaNavigation)
                    .WithMany(p => p.Modelo)
                    .HasForeignKey(d => d.IdMarca)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ModosScializacion>(entity =>
            {
                entity.HasKey(e => e.IdModosScializacion)
                    .HasName("PK247");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<MotivoAsiento>(entity =>
            {
                entity.HasKey(e => e.IdMotivoAsiento)
                    .HasName("PK165");

                entity.HasIndex(e => e.IdConfiguracionContabilidad)
                    .HasName("Ref309469");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.IdConfiguracionContabilidadNavigation)
                    .WithMany(p => p.MotivoAsiento)
                    .HasForeignKey(d => d.IdConfiguracionContabilidad)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefConfiguracionContabilidad469");
            });

            modelBuilder.Entity<MotivoRecepcion>(entity =>
            {
                entity.HasKey(e => e.IdMotivoRecepcion)
                    .HasName("PK_MotivoRecepcion");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Nacionalidad>(entity =>
            {
                entity.HasKey(e => e.IdNacionalidad)
                    .HasName("PK_Nacionalidad");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<NacionalidadIndigena>(entity =>
            {
                entity.HasKey(e => e.IdNacionalidadIndigena)
                    .HasName("PK11");

                entity.HasIndex(e => e.IdEtnia)
                    .HasName("Ref106");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdEtniaNavigation)
                    .WithMany(p => p.NacionalidadIndigena)
                    .HasForeignKey(d => d.IdEtnia)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEtnia6");
            });

            modelBuilder.Entity<Nivel>(entity =>
            {
                entity.HasKey(e => e.IdNivel)
                    .HasName("PK205");

                entity.Property(e => e.Nombre).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<NivelConocimiento>(entity =>
            {
                entity.HasKey(e => e.IdNivelConocimiento)
                    .HasName("PK37");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<NivelDesarrollo>(entity =>
            {
                entity.HasKey(e => e.IdNivelDesarrollo)
                    .HasName("PK42");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Noticia>(entity =>
            {
                entity.HasKey(e => e.IdNoticia)
                    .HasName("PK_Noticias");

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Foto).HasMaxLength(1000);

                entity.Property(e => e.Titulo).HasMaxLength(300);
            });

            modelBuilder.Entity<OtroIngreso>(entity =>
            {
                entity.HasKey(e => e.IdOtroIngreso)
                    .HasName("PK_OtroIngreso");

                entity.Property(e => e.DescripcionOtros).HasMaxLength(250);

                entity.Property(e => e.IngresoArriendos).HasColumnType("decimal");

                entity.Property(e => e.IngresoConyuge).HasColumnType("decimal");

                entity.Property(e => e.IngresoNegocioParticular).HasColumnType("decimal");

                entity.Property(e => e.IngresoRentasFinancieras).HasColumnType("decimal");

                entity.Property(e => e.OtrosIngresos).HasColumnType("decimal");

                entity.Property(e => e.Total).HasColumnType("decimal");

                entity.HasOne(d => d.IdDeclaracionPatrimonioPersonalNavigation)
                    .WithMany(p => p.OtroIngreso)
                    .HasForeignKey(d => d.IdDeclaracionPatrimonioPersonal)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_OtroIngreso_DeclaracionPatrimonioPersonal");
            });

            modelBuilder.Entity<Pais>(entity =>
            {
                entity.HasKey(e => e.IdPais)
                    .HasName("PK_Pais");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PaquetesInformaticos>(entity =>
            {
                entity.HasKey(e => e.IdPaquetesInformaticos)
                    .HasName("PK195");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(150)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<ParametrosGenerales>(entity =>
            {
                entity.HasKey(e => e.IdParametrosGenerales)
                    .HasName("PK119");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Valor)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Parentesco>(entity =>
            {
                entity.HasKey(e => e.IdParentesco)
                    .HasName("PK20");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Parroquia>(entity =>
            {
                entity.HasKey(e => e.IdParroquia)
                    .HasName("PK_Parroquia");

                entity.HasIndex(e => e.IdCiudad)
                    .HasName("IX_Parroquia_IdCiudad");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdCiudadNavigation)
                    .WithMany(p => p.Parroquia)
                    .HasForeignKey(d => d.IdCiudad)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PartidaGeneral>(entity =>
            {
                entity.HasKey(e => e.IdPartidaGeneral)
                    .HasName("PK_PartidaGeneral");

                entity.Property(e => e.NumeroPartida)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<PartidasFase>(entity =>
            {
                entity.HasKey(e => e.IdPartidasFase)
                    .HasName("PK273");

                entity.HasIndex(e => e.IdIndiceOcupacionalModalidadPartida)
                    .HasName("Ref71424");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.HasOne(d => d.IdIndiceOcupacionalModalidadPartidaNavigation)
                    .WithMany(p => p.PartidasFase)
                    .HasForeignKey(d => d.IdIndiceOcupacionalModalidadPartida)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefIndiceOcupacionalModalidadPartida424");

                entity.HasOne(d => d.IdTipoConcursoNavigation)
                    .WithMany(p => p.PartidasFase)
                    .HasForeignKey(d => d.IdTipoConcurso)
                    .HasConstraintName("FK_PartidasFase_TipoConcurso");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona)
                    .HasName("PK_Persona");

                entity.HasIndex(e => e.IdEstadoCivil)
                    .HasName("IX_Persona_IdEstadoCivil");

                entity.HasIndex(e => e.IdEtnia)
                    .HasName("IX_Persona_IdEtnia");

                entity.HasIndex(e => e.IdGenero)
                    .HasName("IX_Persona_IdGenero");

                entity.HasIndex(e => e.IdNacionalidad)
                    .HasName("IX_Persona_IdNacionalidad");

                entity.HasIndex(e => e.IdSexo)
                    .HasName("IX_Persona_IdSexo");

                entity.HasIndex(e => e.IdTipoIdentificacion)
                    .HasName("IX_Persona_IdTipoIdentificacion");

                entity.HasIndex(e => e.IdTipoSangre)
                    .HasName("IX_Persona_IdTipoSangre");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CallePrincipal).HasMaxLength(150);

                entity.Property(e => e.CalleSecundaria).HasMaxLength(50);

                entity.Property(e => e.Identificacion).HasMaxLength(20);

                entity.Property(e => e.LugarTrabajo).HasMaxLength(500);

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Numero).HasMaxLength(50);

                entity.Property(e => e.Ocupacion).HasMaxLength(50);

                entity.Property(e => e.Referencia).HasMaxLength(50);

                entity.Property(e => e.TelefonoCasa).HasMaxLength(20);

                entity.Property(e => e.TelefonoPrivado).HasMaxLength(20);

                entity.HasOne(d => d.IdEstadoCivilNavigation)
                    .WithMany(p => p.Persona)
                    .HasForeignKey(d => d.IdEstadoCivil);

                entity.HasOne(d => d.IdEtniaNavigation)
                    .WithMany(p => p.Persona)
                    .HasForeignKey(d => d.IdEtnia);

                entity.HasOne(d => d.IdGeneroNavigation)
                    .WithMany(p => p.Persona)
                    .HasForeignKey(d => d.IdGenero);

                entity.HasOne(d => d.IdNacionalidadNavigation)
                    .WithMany(p => p.Persona)
                    .HasForeignKey(d => d.IdNacionalidad);

                entity.HasOne(d => d.IdNacionalidadIndigenaNavigation)
                    .WithMany(p => p.Persona)
                    .HasForeignKey(d => d.IdNacionalidadIndigena)
                    .HasConstraintName("FK_Persona_NacionalidadIndigena");

                entity.HasOne(d => d.IdParroquiaNavigation)
                    .WithMany(p => p.Persona)
                    .HasForeignKey(d => d.IdParroquia)
                    .HasConstraintName("FK_Persona_Parroquia");

                entity.HasOne(d => d.IdSexoNavigation)
                    .WithMany(p => p.Persona)
                    .HasForeignKey(d => d.IdSexo);

                entity.HasOne(d => d.IdTipoIdentificacionNavigation)
                    .WithMany(p => p.Persona)
                    .HasForeignKey(d => d.IdTipoIdentificacion);

                entity.HasOne(d => d.IdTipoSangreNavigation)
                    .WithMany(p => p.Persona)
                    .HasForeignKey(d => d.IdTipoSangre);
            });

            modelBuilder.Entity<PersonaCapacitacion>(entity =>
            {
                entity.HasKey(e => e.IdPersonaCapacitacion)
                    .HasName("PK148");

                entity.HasIndex(e => e.IdCapacitacion)
                    .HasName("Ref213333");

                entity.HasIndex(e => e.IdPersona)
                    .HasName("Ref17334");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Observaciones).HasColumnType("varchar(100)");

                entity.HasOne(d => d.IdCapacitacionNavigation)
                    .WithMany(p => p.PersonaCapacitacion)
                    .HasForeignKey(d => d.IdCapacitacion)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefCapacitacion333");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.PersonaCapacitacion)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefPersona334");
            });

            modelBuilder.Entity<PersonaDiscapacidad>(entity =>
            {
                entity.HasKey(e => e.IdPersonaDiscapacidad)
                    .HasName("PK22");

                entity.HasIndex(e => e.IdPersona)
                    .HasName("Ref17186");

                entity.HasIndex(e => e.IdTipoDiscapacidad)
                    .HasName("Ref1332");

                entity.Property(e => e.NumeroCarnet)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.PersonaDiscapacidad)
                    .HasForeignKey(d => d.IdPersona)
                    .HasConstraintName("RefPersona186");

                entity.HasOne(d => d.IdTipoDiscapacidadNavigation)
                    .WithMany(p => p.PersonaDiscapacidad)
                    .HasForeignKey(d => d.IdTipoDiscapacidad)
                    .HasConstraintName("RefTipoDiscapacidad32");
            });

            modelBuilder.Entity<PersonaEnfermedad>(entity =>
            {
                entity.HasKey(e => e.IdPersonaEnfermedad)
                    .HasName("PK21");

                entity.HasIndex(e => e.IdPersona)
                    .HasName("Ref17185");

                entity.HasIndex(e => e.IdTipoEnfermedad)
                    .HasName("Ref1431");

                entity.Property(e => e.InstitucionEmite)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.PersonaEnfermedad)
                    .HasForeignKey(d => d.IdPersona)
                    .HasConstraintName("RefPersona185");

                entity.HasOne(d => d.IdTipoEnfermedadNavigation)
                    .WithMany(p => p.PersonaEnfermedad)
                    .HasForeignKey(d => d.IdTipoEnfermedad)
                    .HasConstraintName("RefTipoEnfermedad31");
            });

            modelBuilder.Entity<PersonaEstudio>(entity =>
            {
                entity.HasKey(e => e.IdPersonaEstudio)
                    .HasName("PK147");

                entity.HasIndex(e => e.IdPersona)
                    .HasName("Ref17335");

                entity.HasIndex(e => e.IdTitulo)
                    .HasName("Ref217332");

                entity.Property(e => e.FechaGraduado).HasColumnType("date");

                entity.Property(e => e.NoSenescyt).HasColumnType("varchar(50)");

                entity.Property(e => e.Observaciones).HasColumnType("varchar(300)");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.PersonaEstudio)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefPersona335");
            });

            modelBuilder.Entity<PersonaPaquetesInformaticos>(entity =>
            {
                entity.HasKey(e => e.IdPersonaPaquetesInformaticos)
                    .HasName("PK196");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15314");

                entity.HasIndex(e => e.IdPaquetesInformaticos)
                    .HasName("Ref195310");

                entity.HasOne(d => d.IdPaquetesInformaticosNavigation)
                    .WithMany(p => p.PersonaPaquetesInformaticos)
                    .HasForeignKey(d => d.IdPaquetesInformaticos)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefPaquetesInformaticos310");
            });

            modelBuilder.Entity<PersonaSustituto>(entity =>
            {
                entity.HasKey(e => e.IdPersonaSustituto)
                    .HasName("PK_PersonaSustituto");

                entity.HasIndex(e => e.IdPersona)
                    .HasName("IX_PersonaSustituto")
                    .IsUnique();

                entity.HasOne(d => d.IdParentescoNavigation)
                    .WithMany(p => p.PersonaSustituto)
                    .HasForeignKey(d => d.IdParentesco)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PersonaSustituto_Parentesco");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithOne(p => p.PersonaSustituto)
                    .HasForeignKey<PersonaSustituto>(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PersonaSustituto_Persona");

                entity.HasOne(d => d.IdPersonaDiscapacidadNavigation)
                    .WithMany(p => p.PersonaSustituto)
                    .HasForeignKey(d => d.IdPersonaDiscapacidad)
                    .HasConstraintName("FK_PersonaSustituto_PersonaDiscapacidad");
            });

            modelBuilder.Entity<PieFirma>(entity =>
            {
                entity.HasKey(e => e.IdPieFirma)
                    .HasName("PK_PieFirma");

                entity.HasOne(d => d.IdIndiceOcupacionalNavigation)
                    .WithMany(p => p.PieFirma)
                    .HasForeignKey(d => d.IdIndiceOcupacional)
                    .HasConstraintName("FK_PieFirma_IndiceOcupacional");

                entity.HasOne(d => d.IdTipoAccionPersonalNavigation)
                    .WithMany(p => p.PieFirma)
                    .HasForeignKey(d => d.IdTipoAccionPersonal)
                    .HasConstraintName("FK_PieFirma_TipoAccionPersonal");
            });

            modelBuilder.Entity<PlanGestionCambio>(entity =>
            {
                entity.HasKey(e => e.IdPlanGestionCambio)
                    .HasName("PK262");

                entity.HasIndex(e => e.AprobadoPor)
                    .HasName("Ref15404");

                entity.HasIndex(e => e.RealizadoPor)
                    .HasName("Ref15403");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(400)");

                entity.Property(e => e.Titulo).HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<PlanificacionHe>(entity =>
            {
                entity.HasKey(e => e.IdPlanificacionHe)
                    .HasName("PK179");

                entity.ToTable("PlanificacionHE");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15443");

                entity.HasIndex(e => e.IdEmpleadoAprueba)
                    .HasName("Ref15445");

                entity.Property(e => e.IdPlanificacionHe).HasColumnName("IdPlanificacionHE");

                entity.Property(e => e.NoHoras).HasColumnType("decimal");
            });

            modelBuilder.Entity<Pregunta>(entity =>
            {
                entity.HasKey(e => e.IdPregunta)
                    .HasName("PK232");

                entity.HasIndex(e => e.IdEvaluacionInduccion)
                    .HasName("Ref231353");

                entity.Property(e => e.Nombre).HasColumnType("varchar(500)");

                entity.HasOne(d => d.IdEvaluacionInduccionNavigation)
                    .WithMany(p => p.Pregunta)
                    .HasForeignKey(d => d.IdEvaluacionInduccion)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEvaluacionInducion353");
            });

            modelBuilder.Entity<PreguntaRespuesta>(entity =>
            {
                entity.HasKey(e => e.IdPreguntaRespuesta)
                    .HasName("PK234");

                entity.HasIndex(e => e.IdPregunta)
                    .HasName("Ref232354");

                entity.HasIndex(e => e.IdRespuesta)
                    .HasName("Ref233355");

                entity.HasOne(d => d.IdPreguntaNavigation)
                    .WithMany(p => p.PreguntaRespuesta)
                    .HasForeignKey(d => d.IdPregunta)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefPregunta354");

                entity.HasOne(d => d.IdRespuestaNavigation)
                    .WithMany(p => p.PreguntaRespuesta)
                    .HasForeignKey(d => d.IdRespuesta)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefRespuesta355");
            });

            modelBuilder.Entity<Proceso>(entity =>
            {
                entity.HasKey(e => e.IdProceso)
                    .HasName("PK54");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor)
                    .HasName("PK_Proveedor");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Identificacion)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Provincia>(entity =>
            {
                entity.HasKey(e => e.IdProvincia)
                    .HasName("PK_Provincia");

                entity.HasIndex(e => e.IdPais)
                    .HasName("IX_Provincia_IdPais");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdPaisNavigation)
                    .WithMany(p => p.Provincia)
                    .HasForeignKey(d => d.IdPais)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Provisiones>(entity =>
            {
                entity.HasKey(e => e.IdProvisiones)
                    .HasName("PK160");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15444");

                entity.HasIndex(e => e.IdTipoProvision)
                    .HasName("Ref287441");

                entity.Property(e => e.Valor).HasColumnType("decimal");

                entity.HasOne(d => d.IdTipoProvisionNavigation)
                    .WithMany(p => p.Provisiones)
                    .HasForeignKey(d => d.IdTipoProvision)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefTipoProvision441");
            });

            modelBuilder.Entity<RealizaExamenInduccion>(entity =>
            {
                entity.HasKey(e => e.IdRealizaExamenInduccion)
                    .HasName("PK235");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15357");

                entity.HasIndex(e => e.IdEvaluacionInduccion)
                    .HasName("Ref231356");

                entity.Property(e => e.Calificacion).HasColumnType("decimal");

                entity.Property(e => e.Fecha).HasColumnType("char(10)");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.RealizaExamenInduccion)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEmpleado357");

                entity.HasOne(d => d.IdEvaluacionInduccionNavigation)
                    .WithMany(p => p.RealizaExamenInduccion)
                    .HasForeignKey(d => d.IdEvaluacionInduccion)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEvaluacionInducion356");
            });

            modelBuilder.Entity<RecepcionActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdRecepcionActivoFijo)
                    .HasName("PK_RecepcionActivoFijo");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_RecepcionActivoFijo_IdEmpleado");

                entity.HasIndex(e => e.IdLibroActivoFijo)
                    .HasName("IX_RecepcionActivoFijo_IdLibroActivoFijo");

                entity.HasIndex(e => e.IdMotivoRecepcion)
                    .HasName("IX_RecepcionActivoFijo_IdMotivoRecepcion");

                entity.HasIndex(e => e.IdProveedor)
                    .HasName("IX_RecepcionActivoFijo_IdProveedor");

                entity.HasIndex(e => e.IdSubClaseActivoFijo)
                    .HasName("IX_RecepcionActivoFijo_IdSubClaseActivoFijo");

                entity.Property(e => e.Cantidad).HasColumnType("decimal");

                entity.Property(e => e.Fondo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.OrdenCompra)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.RecepcionActivoFijo)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IdLibroActivoFijoNavigation)
                    .WithMany(p => p.RecepcionActivoFijo)
                    .HasForeignKey(d => d.IdLibroActivoFijo);

                entity.HasOne(d => d.IdMotivoRecepcionNavigation)
                    .WithMany(p => p.RecepcionActivoFijo)
                    .HasForeignKey(d => d.IdMotivoRecepcion);

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.RecepcionActivoFijo)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IdSubClaseActivoFijoNavigation)
                    .WithMany(p => p.RecepcionActivoFijo)
                    .HasForeignKey(d => d.IdSubClaseActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RecepcionActivoFijoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdRecepcionActivoFijoDetalle)
                    .HasName("PK_RecepcionActivoFijoDetalle");

                entity.HasIndex(e => e.IdActivoFijo)
                    .HasName("IX_RecepcionActivoFijoDetalle_IdActivoFijo");

                entity.HasIndex(e => e.IdEstado)
                    .HasName("IX_RecepcionActivoFijoDetalle_IdEstado");

                entity.HasIndex(e => e.IdRecepcionActivoFijo)
                    .HasName("IX_RecepcionActivoFijoDetalle_IdRecepcionActivoFijo");

                entity.Property(e => e.NumeroPoliza)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.RecepcionActivoFijoDetalle)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RecepcionArticulos>(entity =>
            {
                entity.HasKey(e => e.IdRecepcionArticulos)
                    .HasName("PK_RecepcionArticulos");

                entity.HasIndex(e => e.IdArticulo)
                    .HasName("IX_RecepcionArticulos_IdArticulo");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_RecepcionArticulos_IdEmpleado");

                entity.HasIndex(e => e.IdMaestroArticuloSucursal)
                    .HasName("IX_RecepcionArticulos_IdMaestroArticuloSucursal");

                entity.HasIndex(e => e.IdProveedor)
                    .HasName("IX_RecepcionArticulos_IdProveedor");

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.RecepcionArticulos)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.RecepcionArticulos)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IdMaestroArticuloSucursalNavigation)
                    .WithMany(p => p.RecepcionArticulos)
                    .HasForeignKey(d => d.IdMaestroArticuloSucursal)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.RecepcionArticulos)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RegimenLaboral>(entity =>
            {
                entity.HasKey(e => e.IdRegimenLaboral)
                    .HasName("PK56");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<RegistroEntradaSalida>(entity =>
            {
                entity.HasKey(e => e.IdRegistroEntradaSalida)
                    .HasName("PK187");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15440");

                entity.Property(e => e.IdRegistroEntradaSalida).ValueGeneratedNever();

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.RegistroEntradaSalida)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEmpleado440");
            });

            modelBuilder.Entity<RelacionLaboral>(entity =>
            {
                entity.HasKey(e => e.IdRelacionLaboral)
                    .HasName("PK57");

                entity.HasIndex(e => e.IdRegimenLaboral)
                    .HasName("Ref5693");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdRegimenLaboralNavigation)
                    .WithMany(p => p.RelacionLaboral)
                    .HasForeignKey(d => d.IdRegimenLaboral)
                    .HasConstraintName("RefRegimenLaboral93");
            });

            modelBuilder.Entity<RelacionesInternasExternas>(entity =>
            {
                entity.HasKey(e => e.IdRelacionesInternasExternas)
                    .HasName("PK211");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(3000)");

                entity.Property(e => e.Nombre).HasColumnType("varchar(150)");
            });

            modelBuilder.Entity<Relevancia>(entity =>
            {
                entity.HasKey(e => e.IdRelevancia)
                    .HasName("PK41");

                entity.Property(e => e.ComportamientoObservable)
                    .IsRequired()
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<RequisitosNoCumple>(entity =>
            {
                entity.HasKey(e => e.IdRequisitosNoCumple)
                    .HasName("PK199");

                entity.HasIndex(e => e.IdAdministracionTalentoHumano)
                    .HasName("Ref198316");

                entity.Property(e => e.Detalle).HasColumnType("varchar(250)");

                entity.HasOne(d => d.IdAdministracionTalentoHumanoNavigation)
                    .WithMany(p => p.RequisitosNoCumple)
                    .HasForeignKey(d => d.IdAdministracionTalentoHumano)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefAdministracionTalentoHumano316");
            });

            modelBuilder.Entity<Respuesta>(entity =>
            {
                entity.HasKey(e => e.IdRespuesta)
                    .HasName("PK233");

                entity.Property(e => e.Nombre).HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<Rmu>(entity =>
            {
                entity.HasKey(e => e.IdRmu)
                    .HasName("PK150");

                entity.ToTable("RMU");

                entity.HasIndex(e => e.IdEmpeladoIe)
                    .HasName("Ref283435");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15438");

                entity.HasIndex(e => e.IdTipoRmu)
                    .HasName("Ref284436");

                entity.Property(e => e.IdRmu).HasColumnName("IdRMU");

                entity.Property(e => e.IdEmpeladoIe).HasColumnName("IdEmpeladoIE");

                entity.Property(e => e.IdTipoRmu).HasColumnName("IdTipoRMU");

                entity.Property(e => e.Valor).HasColumnType("decimal");

                entity.HasOne(d => d.IdEmpeladoIeNavigation)
                    .WithMany(p => p.Rmu)
                    .HasForeignKey(d => d.IdEmpeladoIe)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEmpleadoIE435");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Rmu)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEmpleado438");

                entity.HasOne(d => d.IdTipoRmuNavigation)
                    .WithMany(p => p.Rmu)
                    .HasForeignKey(d => d.IdTipoRmu)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefTipoRMU436");
            });

            modelBuilder.Entity<RolPagoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdRolPagoDetalle)
                    .HasName("PK79");

                entity.HasIndex(e => e.IdRolPagos)
                    .HasName("Ref78119");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Valor).HasColumnType("decimal");

                entity.HasOne(d => d.IdRolPagosNavigation)
                    .WithMany(p => p.RolPagoDetalle)
                    .HasForeignKey(d => d.IdRolPagos)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefRolPagos119");
            });

            modelBuilder.Entity<RolPagos>(entity =>
            {
                entity.HasKey(e => e.IdRolPagos)
                    .HasName("PK78");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15118");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.SaldoFinal).HasColumnType("decimal");

                entity.Property(e => e.SaldoInicial).HasColumnType("decimal");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.RolPagos)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEmpleado118");
            });

            modelBuilder.Entity<RolPuesto>(entity =>
            {
                entity.HasKey(e => e.IdRolPuesto)
                    .HasName("PK63");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Rubro>(entity =>
            {
                entity.HasKey(e => e.IdRubro)
                    .HasName("PK83");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.TasaPorcentualMaxima).HasColumnType("decimal");
            });

            modelBuilder.Entity<RubroLiquidacion>(entity =>
            {
                entity.HasKey(e => e.IdRubroLiquidacion)
                    .HasName("PK183");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<Sexo>(entity =>
            {
                entity.HasKey(e => e.IdSexo)
                    .HasName("PK_Sexo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SituacionPropuesta>(entity =>
            {
                entity.HasKey(e => e.IdSituacionPropuesta)
                    .HasName("PK258");

                entity.HasIndex(e => e.IdDependencia)
                    .HasName("Ref51400");

                entity.Property(e => e.Ano).HasColumnType("date");

                entity.Property(e => e.Observaciones).HasColumnType("text");

                entity.HasOne(d => d.IdDependenciaNavigation)
                    .WithMany(p => p.SituacionPropuesta)
                    .HasForeignKey(d => d.IdDependencia)
                    .HasConstraintName("RefDependencia400");
            });

            modelBuilder.Entity<SolicitudAcumulacionDecimos>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudAcumulacionDecimos)
                    .HasName("PK110");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15158");

                entity.Property(e => e.FechaSolicitud).HasColumnType("date");
            });

            modelBuilder.Entity<SolicitudAnticipo>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudAnticipo)
                    .HasName("PK109");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15156");

                entity.HasIndex(e => e.IdEstado)
                    .HasName("Ref73157");

                entity.Property(e => e.CantidadCancelada).HasColumnType("decimal");

                entity.Property(e => e.CantidadSolicitada).HasColumnType("decimal");

                entity.Property(e => e.FechaSolicitud).HasColumnType("date");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.SolicitudAnticipo)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("RefEstado157");
            });

            modelBuilder.Entity<SolicitudCertificadoPersonal>(entity =>
            {
                entity.HasKey(e => new { e.IdSolicitudCertificadoPersonal, e.IdEstado })
                    .HasName("PK103");

                entity.HasIndex(e => e.IdEmpleadoDirigidoA)
                    .HasName("Ref15151");

                entity.HasIndex(e => e.IdEmpleadoSolicitante)
                    .HasName("Ref15150");

                entity.HasIndex(e => e.IdEstado)
                    .HasName("Ref73223");

                entity.HasIndex(e => e.IdTipoCertificado)
                    .HasName("Ref104149");

                entity.Property(e => e.IdSolicitudCertificadoPersonal).ValueGeneratedOnAdd();

                entity.Property(e => e.FechaSolicitud).HasColumnType("date");

                entity.Property(e => e.Observaciones).HasColumnType("text");

                entity.HasOne(d => d.IdTipoCertificadoNavigation)
                    .WithMany(p => p.SolicitudCertificadoPersonal)
                    .HasForeignKey(d => d.IdTipoCertificado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefTipoCertificado149");
            });

            modelBuilder.Entity<SolicitudHorasExtras>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudHorasExtras)
                    .HasName("PK190");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15306");

                entity.Property(e => e.Estado).HasDefaultValueSql("0");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.FechaAprobado).HasColumnType("date");

                entity.Property(e => e.FechaSolicitud).HasColumnType("date");

                entity.Property(e => e.Observaciones).HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<SolicitudLiquidacionHaberes>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudLiquidacionHaberes)
                    .HasName("PK111");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15159");

                entity.Property(e => e.FechaSolicitud).HasColumnType("date");

                entity.Property(e => e.TotalDecimoCuarto).HasColumnType("decimal");

                entity.Property(e => e.TotalDecimoTercero).HasColumnType("decimal");

                entity.Property(e => e.TotalDesahucio).HasColumnType("decimal");

                entity.Property(e => e.TotalDespidoIntempestivo).HasColumnType("decimal");

                entity.Property(e => e.TotalFondoReserva).HasColumnType("decimal");

                entity.Property(e => e.TotalVacaciones).HasColumnType("decimal");
            });

            modelBuilder.Entity<SolicitudModificacionFichaEmpleado>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudModificacionFichaEmpleado)
                    .HasName("PK108");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15154");

                entity.HasIndex(e => e.IdEstado)
                    .HasName("Ref73155");

                entity.Property(e => e.Apellidos).HasColumnType("varchar(50)");

                entity.Property(e => e.CapacitacionesRecibidas).HasColumnType("text");

                entity.Property(e => e.CargasFamiliares).HasColumnType("text");

                entity.Property(e => e.Direccion).HasColumnType("varchar(1024)");

                entity.Property(e => e.ExperienciaLaboral).HasColumnType("text");

                entity.Property(e => e.FechaSolicitud).HasColumnType("date");

                entity.Property(e => e.FormacionAcademica).HasColumnType("text");

                entity.Property(e => e.Nombres).HasColumnType("varchar(50)");

                entity.Property(e => e.TelefonoCasa).HasColumnType("varchar(15)");

                entity.Property(e => e.TelefonoPrivado).HasColumnType("varchar(20)");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.SolicitudModificacionFichaEmpleado)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("RefEstado155");
            });

            modelBuilder.Entity<SolicitudPermiso>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudPermiso)
                    .HasName("PK76");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15113");

                entity.Property(e => e.Estado).HasDefaultValueSql("0");

                entity.Property(e => e.FechaAprobado).HasColumnType("datetime");

                entity.Property(e => e.FechaDesde).HasColumnType("datetime");

                entity.Property(e => e.FechaHasta).HasColumnType("datetime");

                entity.Property(e => e.FechaSolicitud).HasColumnType("date");

                entity.Property(e => e.Motivo).HasColumnType("text");

                entity.Property(e => e.Observacion).HasColumnType("text");

                entity.HasOne(d => d.IdTipoPermisoNavigation)
                    .WithMany(p => p.SolicitudPermiso)
                    .HasForeignKey(d => d.IdTipoPermiso)
                    .HasConstraintName("FK_SolicitudPermiso_TipoPermiso");
            });

            modelBuilder.Entity<SolicitudPlanificacionVacaciones>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudPlanificacionVacaciones)
                    .HasName("PK72");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15110");

                entity.Property(e => e.FechaDesde).HasColumnType("date");

                entity.Property(e => e.FechaHasta).HasColumnType("date");

                entity.Property(e => e.FechaSolicitud).HasColumnType("date");

                entity.Property(e => e.Observaciones).HasMaxLength(150);
            });

            modelBuilder.Entity<SolicitudProveeduria>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudProveeduria)
                    .HasName("PK_SolicitudProveduria");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_SolicitudProveduria_IdEmpleado");
            });

            modelBuilder.Entity<SolicitudProveeduriaDetalle>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudProveeduriaDetalle)
                    .HasName("PK_SolicitudProveduriaDetalle");

                entity.HasIndex(e => e.IdArticulo)
                    .HasName("IX_SolicitudProveduriaDetalle_IdArticulo");

                entity.HasIndex(e => e.IdEstado)
                    .HasName("IX_SolicitudProveduriaDetalle_IdEstado");

                entity.HasIndex(e => e.IdMaestroArticuloSucursal)
                    .HasName("IX_SolicitudProveduriaDetalle_IdMaestroArticuloSucursal");

                entity.HasIndex(e => e.IdSolicitudProveduria)
                    .HasName("IX_SolicitudProveduriaDetalle_IdSolicitudProveduria");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.SolicitudProveeduriaDetalle)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SolicitudProveduriaDetalle_Estado_IdEstado");
            });

            modelBuilder.Entity<SolicitudTipoViatico>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudTipoViatico)
                    .HasName("PK_SolicitudTipoViatico");

                entity.HasOne(d => d.IdSolicitudViaticoNavigation)
                    .WithMany(p => p.SolicitudTipoViatico)
                    .HasForeignKey(d => d.IdSolicitudViatico)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SolicitudTipoViatico_SolicitudViatico");

                entity.HasOne(d => d.IdTipoViaticoNavigation)
                    .WithMany(p => p.SolicitudTipoViatico)
                    .HasForeignKey(d => d.IdTipoViatico)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SolicitudTipoViatico_TipoViatico");
            });

            modelBuilder.Entity<SolicitudVacaciones>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudVacaciones)
                    .HasName("PK75");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15111");

                entity.Property(e => e.FechaDesde).HasColumnType("date");

                entity.Property(e => e.FechaHasta).HasColumnType("date");

                entity.Property(e => e.FechaRespuesta).HasColumnType("date");

                entity.Property(e => e.FechaSolicitud).HasColumnType("date");

                entity.Property(e => e.Observaciones).HasMaxLength(150);
            });

            modelBuilder.Entity<SolicitudViatico>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudViatico)
                    .HasName("PK77");

                entity.HasIndex(e => e.IdConfiguracionViatico)
                    .HasName("Ref255393");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15117");

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.FechaLlegada).HasColumnType("date");

                entity.Property(e => e.FechaSalida).HasColumnType("date");

                entity.Property(e => e.FechaSolicitud).HasColumnType("date");

                entity.Property(e => e.Observacion).HasMaxLength(250);

                entity.Property(e => e.ValorEstimado).HasColumnType("decimal");
            });

            modelBuilder.Entity<SubClaseActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdSubClaseActivoFijo)
                    .HasName("PK_SubClaseActivoFijo");

                entity.HasIndex(e => e.IdClaseActivoFijo)
                    .HasName("IX_SubClaseActivoFijo_IdClaseActivoFijo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdClaseActivoFijoNavigation)
                    .WithMany(p => p.SubClaseActivoFijo)
                    .HasForeignKey(d => d.IdClaseActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SubClaseArticulo>(entity =>
            {
                entity.HasKey(e => e.IdSubClaseArticulo)
                    .HasName("PK_SubClaseArticulo");

                entity.HasIndex(e => e.IdClaseArticulo)
                    .HasName("IX_SubClaseArticulo_IdClaseArticulo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdClaseArticuloNavigation)
                    .WithMany(p => p.SubClaseArticulo)
                    .HasForeignKey(d => d.IdClaseArticulo)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SubcategoryInstitution>(entity =>
            {
                entity.HasKey(e => e.IdSubCategoryInstitution)
                    .HasName("PK16");

                entity.ToTable("SUBCATEGORY_INSTITUTION");

                entity.HasIndex(e => e.IdInstitution)
                    .HasName("Ref1512");

                entity.HasIndex(e => e.IdSubcategory)
                    .HasName("Ref1311");

                entity.Property(e => e.IdSubCategoryInstitution)
                    .HasColumnName("idSubCategoryInstitution")
                    .HasColumnType("char(10)");

                entity.Property(e => e.IdInstitution)
                    .IsRequired()
                    .HasColumnName("idInstitution")
                    .HasColumnType("char(10)");

                entity.Property(e => e.IdSubcategory)
                    .IsRequired()
                    .HasColumnName("idSubcategory")
                    .HasColumnType("char(10)");

                entity.HasOne(d => d.IdInstitutionNavigation)
                    .WithMany(p => p.SubcategoryInstitution)
                    .HasForeignKey(d => d.IdInstitution)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefINSTITUTION12");
            });

            modelBuilder.Entity<Sucursal>(entity =>
            {
                entity.HasKey(e => e.IdSucursal)
                    .HasName("PK_Sucursal");

                entity.HasIndex(e => e.IdCiudad)
                    .HasName("IX_Sucursal_IdCiudad");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdCiudadNavigation)
                    .WithMany(p => p.Sucursal)
                    .HasForeignKey(d => d.IdCiudad)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TablaDepreciacion>(entity =>
            {
                entity.HasKey(e => e.IdTablaDepreciacion)
                    .HasName("PK_TablaDepreciacion");

                entity.Property(e => e.IndiceDepreciacion).HasColumnType("decimal");
            });

            modelBuilder.Entity<Temporal>(entity =>
            {
                entity.HasKey(e => e.IdTemporal)
                    .HasName("PK65");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<TipoAccionPersonal>(entity =>
            {
                entity.HasKey(e => e.IdTipoAccionPersonal)
                    .HasName("PK247_1");

                entity.Property(e => e.Descripcion).HasMaxLength(300);

                entity.Property(e => e.EsResponsableTh).HasColumnName("EsResponsableTH");

                entity.Property(e => e.Matriz).HasMaxLength(100);

                entity.Property(e => e.NdiasMaximo).HasColumnName("NDiasMaximo");

                entity.Property(e => e.NdiasMinimo).HasColumnName("NDiasMinimo");

                entity.Property(e => e.NhorasMaximo).HasColumnName("NHorasMaximo");

                entity.Property(e => e.NhorasMinimo).HasColumnName("NHorasMinimo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdEstadoTipoAccionPersonalNavigation)
                    .WithMany(p => p.TipoAccionPersonal)
                    .HasForeignKey(d => d.IdEstadoTipoAccionPersonal)
                    .HasConstraintName("FK_TipoAccionPersonal_EstadoTipoAccionPersonal");
            });

            modelBuilder.Entity<TipoActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdTipoActivoFijo)
                    .HasName("PK_TipoActivoFijo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TipoArticulo>(entity =>
            {
                entity.HasKey(e => e.IdTipoArticulo)
                    .HasName("PK_TipoArticulo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TipoCalificacion>(entity =>
            {
                entity.HasKey(e => e.IdTipoCalificacion)
                    .HasName("PK_TipoCalificacion");

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<TipoCertificado>(entity =>
            {
                entity.HasKey(e => e.IdTipoCertificado)
                    .HasName("PK104");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(1024)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<TipoCesacionFuncion>(entity =>
            {
                entity.HasKey(e => e.IdTipoCesacionFuncion)
                    .HasName("PK_TipoCesacionFuncion");

                entity.Property(e => e.Descripcion).HasMaxLength(150);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TipoConcurso>(entity =>
            {
                entity.HasKey(e => e.IdTipoConcurso)
                    .HasName("PK268");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(250)");

                entity.Property(e => e.Nombre).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<TipoDiscapacidad>(entity =>
            {
                entity.HasKey(e => e.IdTipoDiscapacidad)
                    .HasName("PK13");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<TipoDiscapacidadSustituto>(entity =>
            {
                entity.HasKey(e => e.IdTipoDiscapacidadSustituto)
                    .HasName("PK68");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<TipoEnfermedad>(entity =>
            {
                entity.HasKey(e => e.IdTipoEnfermedad)
                    .HasName("PK14");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<TipoExamenComplementario>(entity =>
            {
                entity.HasKey(e => e.IdTipoExamenComplementario)
                    .HasName("PK_TipoExamenComplementario");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<TipoIdentificacion>(entity =>
            {
                entity.HasKey(e => e.IdTipoIdentificacion)
                    .HasName("PK_TipoIdentificacion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TipoMovimientoInterno>(entity =>
            {
                entity.HasKey(e => e.IdTipoMovimientoInterno)
                    .HasName("PK127");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<TipoNombramiento>(entity =>
            {
                entity.HasKey(e => e.IdTipoNombramiento)
                    .HasName("PK58");

                entity.HasIndex(e => e.IdRelacionLaboral)
                    .HasName("Ref5794");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdRelacionLaboralNavigation)
                    .WithMany(p => p.TipoNombramiento)
                    .HasForeignKey(d => d.IdRelacionLaboral)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefRelacionLaboral94");
            });

            modelBuilder.Entity<TipoPermiso>(entity =>
            {
                entity.HasKey(e => e.IdTipoPermiso)
                    .HasName("PK133");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<TipoProvision>(entity =>
            {
                entity.HasKey(e => e.IdTipoProvision)
                    .HasName("PK161");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<TipoRmu>(entity =>
            {
                entity.HasKey(e => e.IdTipoRmu)
                    .HasName("PK155");

                entity.ToTable("TipoRMU");

                entity.Property(e => e.IdTipoRmu).HasColumnName("IdTipoRMU");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("char(10)");
            });

            modelBuilder.Entity<TipoSangre>(entity =>
            {
                entity.HasKey(e => e.IdTipoSangre)
                    .HasName("PK_TipoSangre");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TipoTransporte>(entity =>
            {
                entity.HasKey(e => e.IdTipoTransporte)
                    .HasName("PK249");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<TipoViatico>(entity =>
            {
                entity.HasKey(e => e.IdTipoViatico)
                    .HasName("PK250");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Titulo>(entity =>
            {
                entity.HasKey(e => e.IdTitulo)
                    .HasName("PK149");

                entity.HasIndex(e => e.IdEstudio)
                    .HasName("Ref214331");

                entity.Property(e => e.Nombre).HasMaxLength(80);

                entity.HasOne(d => d.IdAreaConocimientoNavigation)
                    .WithMany(p => p.Titulo)
                    .HasForeignKey(d => d.IdAreaConocimiento)
                    .HasConstraintName("FK_Titulo_AreaConocimiento");

                entity.HasOne(d => d.IdEstudioNavigation)
                    .WithMany(p => p.Titulo)
                    .HasForeignKey(d => d.IdEstudio)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEstudio331");
            });

            modelBuilder.Entity<TrabajoEquipoIniciativaLiderazgo>(entity =>
            {
                entity.HasKey(e => e.IdTrabajoEquipoIniciativaLiderazgo)
                    .HasName("PK48");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<TransferenciaActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdTransferenciaActivoFijo)
                    .HasName("PK_TransferenciaActivoFijo");

                entity.HasIndex(e => e.IdEmpleadoRegistra)
                    .HasName("Ref15169");

                entity.HasIndex(e => e.IdEmpleadoResponsableEnvio)
                    .HasName("IX_TransferenciaActivoFijo_IdEmpleado");

                entity.HasIndex(e => e.IdEmpleadoResponsableRecibo)
                    .HasName("Ref15171");

                entity.Property(e => e.Destino)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IdMotivoTransferencia).HasColumnName("idMotivoTransferencia");

                entity.Property(e => e.Observaciones).HasColumnType("text");

                entity.Property(e => e.Origen).HasMaxLength(50);
            });

            modelBuilder.Entity<TransferenciaActivoFijoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdTransferenciaActivoFijoDetalle)
                    .HasName("PK_TransferenciaActivoFijoDetalle");

                entity.HasIndex(e => e.IdActivoFijo)
                    .HasName("IX_TransferenciaActivoFijoDetalle_IdActivoFijo");

                entity.HasIndex(e => e.IdTransferenciaActivoFijo)
                    .HasName("IX_TransferenciaActivoFijoDetalle_IdTransferenciaActivoFijo");

                entity.HasOne(d => d.IdActivoFijoNavigation)
                    .WithMany(p => p.TransferenciaActivoFijoDetalle)
                    .HasForeignKey(d => d.IdActivoFijo);

                entity.HasOne(d => d.IdTransferenciaActivoFijoNavigation)
                    .WithMany(p => p.TransferenciaActivoFijoDetalle)
                    .HasForeignKey(d => d.IdTransferenciaActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TransferenciaArticulo>(entity =>
            {
                entity.HasKey(e => e.IdTransferenciaArticulo)
                    .HasName("PK_TranferenciaArticulo");

                entity.HasIndex(e => e.IdArticulo)
                    .HasName("Ref99258");

                entity.HasIndex(e => e.IdEmpleadoEnvia)
                    .HasName("IX_TranferenciaArticulo_EmpleadoIdEmpleado");

                entity.HasIndex(e => e.IdMaestroArticuloEnvia)
                    .HasName("Ref100257");

                entity.HasIndex(e => e.IdMaestroRecibe)
                    .HasName("Ref100262");

                entity.HasIndex(e => e.IdempleadoRecibe)
                    .HasName("IX_TranferenciaArticulo_IdArticulo");

                entity.Property(e => e.Fecha).HasMaxLength(10);

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.TransferenciaArticulo)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TranferenciaArticulo_Articulo_IdArticulo");

                entity.HasOne(d => d.IdMaestroArticuloEnviaNavigation)
                    .WithMany(p => p.TransferenciaArticuloIdMaestroArticuloEnviaNavigation)
                    .HasForeignKey(d => d.IdMaestroArticuloEnvia)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TranferenciaArticulo_MaestroArticuloSucursal_MaestroArticuloSucursalIdMaestroArticuloSucursal");

                entity.HasOne(d => d.IdMaestroRecibeNavigation)
                    .WithMany(p => p.TransferenciaArticuloIdMaestroRecibeNavigation)
                    .HasForeignKey(d => d.IdMaestroRecibe)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefMaestroArticuloSucursal262");
            });

            modelBuilder.Entity<TrayectoriaLaboral>(entity =>
            {
                entity.HasKey(e => e.IdTrayectoriaLaboral)
                    .HasName("PK271");

                entity.HasIndex(e => e.IdPersona)
                    .HasName("Ref17422");

                entity.Property(e => e.DescripcionFunciones).HasColumnType("varchar(250)");

                entity.Property(e => e.Empresa).HasColumnType("varchar(100)");

                entity.Property(e => e.FechaFin).HasColumnType("date");

                entity.Property(e => e.FechaInicio).HasColumnType("date");

                entity.Property(e => e.PuestoTrabajo).HasColumnType("varchar(250)");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.TrayectoriaLaboral)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefPersona422");
            });

            modelBuilder.Entity<UnidadMedida>(entity =>
            {
                entity.HasKey(e => e.IdUnidadMedida)
                    .HasName("PK_UnidadMedida");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ValidacionInmediatoSuperior>(entity =>
            {
                entity.HasKey(e => e.IdValidacionJefe)
                    .HasName("PK193");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15309");

                entity.HasIndex(e => e.IdFormularioAnalisisOcupacional)
                    .HasName("Ref107308");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.HasOne(d => d.IdFormularioAnalisisOcupacionalNavigation)
                    .WithMany(p => p.ValidacionInmediatoSuperior)
                    .HasForeignKey(d => d.IdFormularioAnalisisOcupacional)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefFormularioAnalisisOcupacional308");
            });
        }
    }
}