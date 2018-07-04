using bd.swth.entidades.Negocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;

namespace bd.swth.datos
{
    public class SwTHDbContext : DbContext
    {

        public SwTHDbContext(DbContextOptions<SwTHDbContext> options)
            : base(options)
        {

        }

        /// <summary>
        /// Db Set de Configuración de la nómina
        /// </summary>
        /// 
        public virtual DbSet<ConceptoConjuntoNomina> ConceptoConjuntoNomina { get; set; }
        public virtual DbSet<ConceptoNomina> ConceptoNomina { get; set; }
        public virtual DbSet<ConjuntoNomina> ConjuntoNomina { get; set; }
        public virtual DbSet<ProcesoNomina> ProcesoNomina { get; set; }
        public virtual DbSet<TeconceptoNomina> TeconceptoNomina { get; set; }
        public virtual DbSet<TipoConjuntoNomina> TipoConjuntoNomina { get; set; }
        public virtual DbSet<PeriodoNomina> PeriodoNomina { get; set; }
        public virtual DbSet<SriNomina> SriNomina { get; set; }
        public virtual DbSet<SriDetalle> SriDetalle { get; set; }
        public virtual DbSet<TipoDeGastoPersonal> TipoDeGastoPersonal { get; set; }
        public virtual DbSet<GastoPersonal> GastoPersonal { get; set; }
        public virtual DbSet<CalculoNomina> CalculoNomina { get; set; }
        public virtual DbSet<ReportadoNomina> ReportadoNomina { get; set; }
        public virtual DbSet<CabeceraNomina> CabeceraNomina { get; set; }
        public virtual DbSet<DetalleNomina> DetalleNomina { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.ConstanteNomina> ConstanteNomina { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.FuncionNomina> FuncionNomina { get; set; }





        public virtual DbSet<bd.swth.entidades.Negocio.TipoExamenComplementario> TipoExamenComplementario { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.FichaMedica> FichaMedica { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.AntecedentesFamiliares> AntecedentesFamiliares { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.AntecedentesLaborales> AntecedentesLaborales { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.ExamenComplementario> ExamenComplementario { get; set; }

        public virtual DbSet<bd.swth.entidades.Negocio.ActivarPersonalTalentoHumano> ActivarPersonalTalentoHumano { get; set; }

        public virtual DbSet<bd.swth.entidades.Negocio.LavadoActivoEmpleado> LavadoActivoEmpleado { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.LavadoActivoItem> LavadoActivoItem { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.Empleado> Empleado { get; set; }

        public virtual DbSet<bd.swth.entidades.Negocio.AccionPersonal> AccionPersonal { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.DocumentoInformacionInstitucional> DocumentoInformacionInstitucional { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.CapacitacionProveedor> CapacitacionProveedor { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.ExperienciaLaboralRequerida> ExperienciaLaboralRequerida { get; set; }
        public virtual DbSet<ActividadesAnalisisOcupacional> ActividadesAnalisisOcupacional { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.ActividadesEsenciales> ActividadesEsenciales { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.ComportamientoObservable> ComportamientoObservable { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.IndiceOcupacionalExperienciaLaboralRequerida> IndiceOcupacionalExperienciaLaboralRequerida { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.ActividadesGestionCambio> ActividadesGestionCambio { get; set; }
        public virtual DbSet<AdministracionTalentoHumano> AdministracionTalentoHumano { get; set; }
        public virtual DbSet<AprobacionViatico> AprobacionViatico { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.Ambito> Ambito { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.AreaConocimiento> AreaConocimiento { get; set; }

        public virtual DbSet<bd.swth.entidades.Negocio.BrigadaSSO> BrigadaSSO { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.BrigadaSSORol> BrigadaSsorol { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.Calificacion> Calificacion { get; set; }
        public virtual DbSet<CandidatoConcurso> CandidatoConcurso { get; set; }
        public virtual DbSet<Candidato> Candidato { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.Capacitacion> Capacitacion { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.CapacitacionAreaConocimiento> CapacitacionAreaConocimiento { get; set; }
        public virtual DbSet<CapacitacionEncuesta> CapacitacionEncuesta { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.CapacitacionModalidad> CapacitacionModalidad { get; set; }
        public virtual DbSet<CapacitacionPlanificacion> CapacitacionPlanificacion { get; set; }
        public virtual DbSet<CapacitacionPregunta> CapacitacionPregunta { get; set; }
        public virtual DbSet<CapacitacionRecibida> CapacitacionRecibida { get; set; }
        public virtual DbSet<CapacitacionRespuesta> CapacitacionRespuesta { get; set; }
        public virtual DbSet<CandidatoEstudio> CandidatoEstudio { get; set; }
        public virtual DbSet<ReliquidacionViatico> ReliquidacionViatico { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.CapacitacionTemario> CapacitacionTemario { get; set; }
        public virtual DbSet<CapacitacionTemarioProveedor> CapacitacionTemarioProveedor { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.CapacitacionTipoPregunta> CapacitacionTipoPregunta { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.CeseFuncion> CeseFuncion { get; set; }
        public virtual DbSet<Ciudad> Ciudad { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.ConfiguracionFeriados> ConfiguracionFeriados { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.ConfiguracionViatico> ConfiguracionViatico { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.ConfirmacionLectura> ConfirmacionLectura { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.ConocimientosAdicionales> ConocimientosAdicionales { get; set; }
        public virtual DbSet<DatosBancarios> DatosBancarios { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.DeclaracionPatrimonioPersonal> DeclaracionPatrimonioPersonal { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.DenominacionCompetencia> DenominacionCompetencia { get; set; }
        public virtual DbSet<Dependencia> Dependencia { get; set; }
        public virtual DbSet<DependenciaDocumento> DependenciaDocumento { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.Destreza> Destreza { get; set; }
        public virtual DbSet<DetalleExamenInduccion> DetalleExamenInduccion { get; set; }
        public virtual DbSet<DetallePresupuesto> DetallePresupuesto { get; set; }
        public virtual DbSet<DetalleEvaluacionEvento> DetalleEvaluacionEvento { get; set; }
        public virtual DbSet<DetalleReliquidacionViatico> DetalleReliquidacionViatico { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.DiscapacidadSustituto> DiscapacidadSustituto { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.DocumentosIngreso> DocumentosIngreso { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.DocumentosIngresoEmpleado> DocumentosIngresoEmpleado { get; set; }
        public virtual DbSet<DocumentosParentescodos> DocumentosParentescodos { get; set; }
        public virtual DbSet<EmpleadoContactoEmergencia> EmpleadoContactoEmergencia { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.EmpleadoFamiliar> EmpleadoFamiliar { get; set; }
        public virtual DbSet<EmpleadoFormularioCapacitacion> EmpleadoFormularioCapacitacion { get; set; }
        public virtual DbSet<EmpleadoIE> EmpleadoIE { get; set; }
        public virtual DbSet<CandidatoTrayectoriaLaboral> CandidatoTrayectoriaLaboral { get; set; }
        public virtual DbSet<EmpleadoImpuestoRenta> EmpleadoImpuestoRenta { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.ImpuestoRentaParametros> ImpuestoRentaParametros { get; set; }
        public virtual DbSet<EmpleadoMovimiento> EmpleadoMovimiento { get; set; }
        public virtual DbSet<EmpleadoNepotismo> EmpleadoNepotismo { get; set; }
        public virtual DbSet<EmpleadoSaldoVacaciones> EmpleadoSaldoVacaciones { get; set; }
        public virtual DbSet<EmpleadosFormularioDevengacion> EmpleadosFormularioDevengacion { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.EnfermedadSustituto> EnfermedadSustituto { get; set; }
        public virtual DbSet<EscalaEvaluacionTotal> EscalaEvaluacionTotal { get; set; }
        public virtual DbSet<EscalaGrados> EscalaGrados { get; set; }
        public virtual DbSet<EspecificidadExperiencia> EspecificidadExperiencia { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }

        public virtual DbSet<bd.swth.entidades.Negocio.EstadoCivil> EstadoCivil { get; set; }
        public virtual DbSet<Estudio> Estudio { get; set; }
        public virtual DbSet<Etnia> Etnia { get; set; }
        public virtual DbSet<Eval001> Eval001 { get; set; }
        public virtual DbSet<EvaluacionActividadesPuestoTrabajo> EvaluacionActividadesPuestoTrabajo { get; set; }
        public virtual DbSet<EvaluacionCompetenciasTecnicasPuesto> EvaluacionCompetenciasTecnicasPuesto { get; set; }
        public virtual DbSet<EvaluacionCompetenciasUniversales> EvaluacionCompetenciasUniversales { get; set; }
        public virtual DbSet<EvaluacionConocimiento> EvaluacionConocimiento { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.EvaluacionInducion> EvaluacionInducion { get; set; }
        public virtual DbSet<EvaluacionTrabajoEquipoIniciativaLiderazgo> EvaluacionTrabajoEquipoIniciativaLiderazgo { get; set; }
        public virtual DbSet<Evaluador> Evaluador { get; set; }
        public virtual DbSet<EvaluacionEvento> EvaluacionEvento { get; set; }
        public virtual DbSet<Exepciones> Exepciones { get; set; }
        public virtual DbSet<Factor> Factor { get; set; }
        public virtual DbSet<FacturaViatico> FacturaViatico { get; set; }
        public virtual DbSet<FondoFinanciamiento> FondoFinanciamiento { get; set; }
        public virtual DbSet<FormularioAnalisisOcupacional> FormularioAnalisisOcupacional { get; set; }
        public virtual DbSet<FormularioCapacitacion> FormularioCapacitacion { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.FormularioDevengacion> FormularioDevengacion { get; set; }
        public virtual DbSet<FormulasRMU> FormulasRMU { get; set; }
        public virtual DbSet<FrecuenciaAplicacion> FrecuenciaAplicacion { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.FlujoAprobacion> FlujoAprobacion { get; set; }

        public virtual DbSet<GastoRubro> GastoRubro { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<GestionPlanCapacitacion> GestionPlanCapacitacion { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.GrupoOcupacional> GrupoOcupacional { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.InstruccionFormal> InstruccionFormal { get; set; }
        public virtual DbSet<Indicador> Indicador { get; set; }
        public virtual DbSet<IndiceOcupacional> IndiceOcupacional { get; set; }
        public virtual DbSet<IndiceOcupacionalActividadesEsenciales> IndiceOcupacionalActividadesEsenciales { get; set; }
        public virtual DbSet<IndiceOcupacionalAreaConocimiento> IndiceOcupacionalAreaConocimiento { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.IndiceOcupacionalCapacitaciones> IndiceOcupacionalCapacitaciones { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.IndiceOcupacionalComportamientoObservable> IndiceOcupacionalComportamientoObservable { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.IndiceOcupacionalConocimientosAdicionales> IndiceOcupacionalConocimientosAdicionales { get; set; }
        public virtual DbSet<IndiceOcupacionalEstudio> IndiceOcupacionalEstudio { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.IndiceOcupacionalModalidadPartida> IndiceOcupacionalModalidadPartida { get; set; }
        public virtual DbSet<Induccion> Induccion { get; set; }
        public virtual DbSet<InformeUATH> InformeUATH { get; set; }
        public virtual DbSet<InformeViatico> InformeViatico { get; set; }
        public virtual DbSet<IngresoEgresoRMU> IngresoEgresoRMU { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.InstitucionFinanciera> InstitucionFinanciera { get; set; }
        public virtual DbSet<ItemViatico> ItemViatico { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.ItinerarioViatico> ItinerarioViatico { get; set; }
        public virtual DbSet<Liquidacion> Liquidacion { get; set; }
        public virtual DbSet<ManualPuesto> ManualPuesto { get; set; }
        public virtual DbSet<MaterialApoyo> MaterialApoyo { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.MaterialInduccion> MaterialInduccion { get; set; }
        public virtual DbSet<ModalidadPartida> ModalidadPartida { get; set; }
        public virtual DbSet<ModosScializacion> ModosScializacion { get; set; }
        public virtual DbSet<Nacionalidad> Nacionalidad { get; set; }
        public virtual DbSet<NacionalidadIndigena> NacionalidadIndigena { get; set; }
        public virtual DbSet<Nivel> Nivel { get; set; }
        public virtual DbSet<NivelConocimiento> NivelConocimiento { get; set; }
        public virtual DbSet<OtroIngreso> OtroIngreso { get; set; }
        public virtual DbSet<NivelDesarrollo> NivelDesarrollo { get; set; }
        public virtual DbSet<Noticia> Noticia { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<PaquetesInformaticos> PaquetesInformaticos { get; set; }
        public virtual DbSet<ParametrosGenerales> ParametrosGenerales { get; set; }
        public virtual DbSet<TipoDiscapacidadSustituto> TipoDiscapacidadSustituto { get; set; }
        public virtual DbSet<Parentesco> Parentesco { get; set; }
        public virtual DbSet<Parroquia> Parroquia { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.PartidaGeneral> PartidaGeneral { get; set; }
        public virtual DbSet<PartidasFase> PartidasFase { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<PersonaCapacitacion> PersonaCapacitacion { get; set; }
        public virtual DbSet<PersonaDiscapacidad> PersonaDiscapacidad { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.PersonaEnfermedad> PersonaEnfermedad { get; set; }
        public virtual DbSet<PersonaEstudio> PersonaEstudio { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.PersonaSustituto> PersonaSustituto { get; set; }
        public virtual DbSet<PersonaPaquetesInformaticos> PersonaPaquetesInformaticos { get; set; }
        public virtual DbSet<PieFirma> PieFirma { get; set; }
        public virtual DbSet<PlanCapacitacion> PlanCapacitacion { get; set; }
        public virtual DbSet<PlanificacionHE> PlanificacionHE { get; set; }
        public virtual DbSet<Pregunta> Pregunta { get; set; }
        public virtual DbSet<PreguntaRespuesta> PreguntaRespuesta { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.Proceso> Proceso { get; set; }
        public virtual DbSet<ProcesoDetalle> ProcesoDetalle { get; set; }
        public virtual DbSet<Presupuesto> Presupuesto { get; set; }
        public virtual DbSet<PreguntaEvaluacionEvento> PreguntaEvaluacionEvento { get; set; }
        public virtual DbSet<Provincia> Provincia { get; set; }
        public virtual DbSet<Quejas> Quejas { get; set; }
        public virtual DbSet<Provisiones> Provisiones { get; set; }
        public virtual DbSet<RealizaExamenInduccion> RealizaExamenInduccion { get; set; }
        public virtual DbSet<RecepcionActivoFijoDetalle> RecepcionActivoFijoDetalle { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.RegimenLaboral> RegimenLaboral { get; set; }
        public virtual DbSet<RegistroEntradaSalida> RegistroEntradaSalida { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.RelacionLaboral> RelacionLaboral { get; set; }
        public virtual DbSet<RelacionesInternasExternas> RelacionesInternasExternas { get; set; }
        public virtual DbSet<Relevancia> Relevancia { get; set; }
        public virtual DbSet<RequisitosNoCumple> RequisitosNoCumple { get; set; }
        public virtual DbSet<Respuesta> Respuesta { get; set; }
        public virtual DbSet<RMU> RMU { get; set; }
        public virtual DbSet<RolPagoDetalle> RolPagoDetalle { get; set; }
        public virtual DbSet<RolPagos> RolPagos { get; set; }
        public virtual DbSet<RolPuesto> RolPuesto { get; set; }
        public virtual DbSet<Rubro> Rubro { get; set; }
        public virtual DbSet<InformeActividadViatico> InformeActividadViatico { get; set; }
        public virtual DbSet<RubroLiquidacion> RubroLiquidacion { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.Sexo> Sexo { get; set; }
        public virtual DbSet<SituacionPropuesta> SituacionPropuesta { get; set; }
        public virtual DbSet<SolicitudAcumulacionDecimos> SolicitudAcumulacionDecimos { get; set; }
        public virtual DbSet<SolicitudAnticipo> SolicitudAnticipo { get; set; }
        public virtual DbSet<SolicitudCertificadoPersonal> SolicitudCertificadoPersonal { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.SolicitudHorasExtras> SolicitudHorasExtras { get; set; }
        public virtual DbSet<SolicitudLiquidacionHaberes> SolicitudLiquidacionHaberes { get; set; }
        public virtual DbSet<SolicitudModificacionFichaEmpleado> SolicitudModificacionFichaEmpleado { get; set; }
        public virtual DbSet<SolicitudPermiso> SolicitudPermiso { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.SolicitudTipoViatico> SolicitudTipoViatico { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.SolicitudPlanificacionVacaciones> SolicitudPlanificacionVacaciones { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.SolicitudVacaciones> SolicitudVacaciones { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.SolicitudViatico> SolicitudViatico { get; set; }
        public virtual DbSet<Sucursal> Sucursal { get; set; }
        public virtual DbSet<TipoAccionPersonal> TipoAccionPersonal { get; set; }
        public virtual DbSet<TipoCertificado> TipoCertificado { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.TipoCesacionFuncion> TipoCesacionFuncion { get; set; }
        public virtual DbSet<TipoCalificacion> TipoCalificacion { get; set; }
        public virtual DbSet<TipoConcurso> TipoConcurso { get; set; }
        public virtual DbSet<TipoDiscapacidad> TipoDiscapacidad { get; set; }
        public virtual DbSet<TipoEnfermedad> TipoEnfermedad { get; set; }
        public virtual DbSet<TipoIdentificacion> TipoIdentificacion { get; set; }
        public virtual DbSet<TipoMovimientoInterno> TipoMovimientoInterno { get; set; }
        public virtual DbSet<TipoNombramiento> TipoNombramiento { get; set; }
        public virtual DbSet<TipoPermiso> TipoPermiso { get; set; }
        public virtual DbSet<TipoProvision> TipoProvision { get; set; }
        public virtual DbSet<TipoRMU> TipoRMU { get; set; }
        public virtual DbSet<TipoSangre> TipoSangre { get; set; }
        public virtual DbSet<TipoTransporte> TipoTransporte { get; set; }
        public virtual DbSet<TipoViatico> TipoViatico { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.Titulo> Titulo { get; set; }
        public virtual DbSet<TrabajoEquipoIniciativaLiderazgo> TrabajoEquipoIniciativaLiderazgo { get; set; }
        public virtual DbSet<TrayectoriaLaboral> TrayectoriaLaboral { get; set; }
        public virtual DbSet<VacacionesEmpleado> VacacionesEmpleado { get; set; }
        public virtual DbSet<bd.swth.entidades.Negocio.VacacionRelacionLaboral> VacacionRelacionLaboral { get; set; }
        public virtual DbSet<ValidacionInmediatoSuperior> ValidacionInmediatoSuperior { get; set; }
        public virtual DbSet<AprobacionAccionPersonal> AprobacionAccionPersonal { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AprobacionAccionPersonal>(entity =>
            {
                entity.HasKey(e => e.IdAprobacionAccionPersonal)
                    .HasName("PK_AprobacionAccionPersonal");

                entity.Property(e => e.FechaAprobacion).HasColumnType("datetime");

                entity.HasOne(d => d.AccionPersonal)
                    .WithMany(p => p.AprobacionAccionPersonal)
                    .HasForeignKey(d => d.IdAccionPersonal)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AprobacionAccionPersonal_AccionPersonal");

                entity.HasOne(d => d.EmpleadoAprobador)
                    .WithMany(p => p.AprobacionAccionPersonal)
                    .HasForeignKey(d => d.IdEmpleadoAprobador)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AprobacionAccionPersonal_Empleado");

                entity.HasOne(d => d.FlujoAprobacion)
                    .WithMany(p => p.AprobacionAccionPersonal)
                    .HasForeignKey(d => d.IdFlujoAprobacion)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AprobacionAccionPersonal_FlujoAprobacion");
            });

            modelBuilder.Entity<FlujoAprobacion>(entity =>
            {
                entity.HasKey(e => e.IdFlujoAprobacion)
                    .HasName("PK_FlujoAprobacion");

                entity.HasOne(d => d.ManualPuesto)
                    .WithMany(p => p.FlujoAprobacion)
                    .HasForeignKey(d => d.IdManualPuesto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_FlujoAprobacion_ManualPuesto");

                entity.HasOne(d => d.Sucursal)
                    .WithMany(p => p.FlujoAprobacion)
                    .HasForeignKey(d => d.IdSucursal)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_FlujoAprobacion_Sucursal");

                entity.HasOne(d => d.TipoAccionPersonal)
                    .WithMany(p => p.FlujoAprobacion)
                    .HasForeignKey(d => d.IdTipoAccionPersonal)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_FlujoAprobacion_TipoAccionPersonal");
            });

            modelBuilder.Entity<VacacionRelacionLaboral>(entity =>
            {
                entity.HasKey(e => e.IdVacacionRelacionLaboral)
                    .HasName("PK_VacacionRelacionLaboral");

                entity.Property(e => e.IncrementoApartirPeriodoFiscal).HasColumnName("IncrementoAPartirPeriodoFiscal");

                entity.HasOne(d => d.RegimenLaboral)
                    .WithMany(p => p.VacacionRelacionLaboral)
                    .HasForeignKey(d => d.IdRegimenLaboral)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_VacacionRelacionLaboral_RelacionLaboral");
            });

            modelBuilder.Entity<VacacionesEmpleado>(entity =>
            {
                entity.HasKey(e => e.IdVacaciones)
                    .HasName("PK_VacacionesEmpleado");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.VacacionesEmpleado)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_VacacionesEmpleado_Empleado");
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

                entity.Property(e => e.IngresosOtraActividad)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.CiudadNacimiento)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.IdCiudadLugarNacimiento);

                entity.HasOne(d => d.Dependencia)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.IdDependencia)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ProvinciaSufragio)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.IdProvinciaLugarSufragio);

                entity.HasOne(d => d.BrigadaSSORol)
                   .WithMany(p => p.Empleado)
                   .HasForeignKey(d => d.IdBrigadaSSORol);
            });

            modelBuilder.Entity<TipoConjuntoNomina>(entity =>
            {
                entity.HasKey(e => e.IdTipoConjunto)
                    .HasName("PK6");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<TeconceptoNomina>(entity =>
            {
                entity.HasKey(e => e.IdTeconcepto)
                    .HasName("PK5");

                entity.ToTable("TEConceptoNomina");

                entity.HasIndex(e => e.IdConcepto)
                    .HasName("Ref210");

                entity.Property(e => e.IdTeconcepto).HasColumnName("IdTEConcepto");

                entity.HasOne(d => d.ConceptoNomina)
                    .WithMany(p => p.TeconceptoNomina)
                    .HasForeignKey(d => d.IdConcepto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefConceptoNomina10");
            });

            modelBuilder.Entity<ConjuntoNomina>(entity =>
            {
                entity.HasKey(e => e.IdConjunto)
                    .HasName("PK3");

                entity.HasIndex(e => e.IdTipoConjunto)
                    .HasName("Ref65");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.HasOne(d => d.TipoConjuntoNomina)
                    .WithMany(p => p.ConjuntoNomina)
                    .HasForeignKey(d => d.IdTipoConjunto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefTipoConjuntoNomina5");
            });

            modelBuilder.Entity<ProcesoNomina>(entity =>
            {
                entity.HasKey(e => e.IdProceso)
                    .HasName("PK1");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<ConceptoConjuntoNomina>(entity =>
            {
                entity.HasKey(e => e.IdConceptoConjunto)
                    .HasName("PK4");

                entity.HasIndex(e => e.IdConcepto)
                    .HasName("Ref27");

                entity.HasIndex(e => e.IdConjunto)
                    .HasName("Ref36");

                entity.HasOne(d => d.ConceptoNomina)
                    .WithMany(p => p.ConceptoConjuntoNomina)
                    .HasForeignKey(d => d.IdConcepto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefConceptoNomina7");

                entity.HasOne(d => d.ConjuntoNomina)
                    .WithMany(p => p.ConceptoConjuntoNomina)
                    .HasForeignKey(d => d.IdConjunto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefConjuntoNomina6");
            });

            modelBuilder.Entity<ConceptoNomina>(entity =>
            {
                entity.HasKey(e => e.IdConcepto)
                    .HasName("PK2");

                entity.HasIndex(e => e.IdProceso)
                    .HasName("Ref18");

                entity.Property(e => e.Abreviatura)
                    .IsRequired()
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Estatus)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.FormulaCalculo).HasColumnType("varchar(500)");

                entity.Property(e => e.NivelAcumulacion)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.RegistroEn)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.TipoCalculo)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.TipoConcepto)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.HasOne(d => d.ProcesoNomina)
                    .WithMany(p => p.ConceptoNomina)
                    .HasForeignKey(d => d.IdProceso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefProcesoNomina8");
            });

            modelBuilder.Entity<LavadoActivoItem>(entity =>
            {
                entity.HasKey(e => e.IdLavadoActivoItem)
                    .HasName("PK_LavadoActivoItem");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(700)");
            });

            modelBuilder.Entity<ActivarPersonalTalentoHumano>(entity =>
            {
                entity.HasKey(e => e.IdActivarPersonalTalentoHumano)
                    .HasName("PK_ActivarPersonalTalentoHumano");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.Dependencia)
                    .WithMany(p => p.ActivarPersonalTalentoHumano)
                    .HasForeignKey(d => d.IdDependencia)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ActivarPersonalTalentoHumano_Dependencia");
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

                entity.HasOne(d => d.FichaMedica)
                    .WithMany(p => p.AntecedentesFamiliares)
                    .HasForeignKey(d => d.IdFichaMedica)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AntecedentesFamiliares_AntecedentesFamiliares");
            });

            modelBuilder.Entity<TipoExamenComplementario>(entity =>
            {
                entity.HasKey(e => e.IdTipoExamenComplementario)
                    .HasName("PK_TipoExamenComplementario");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
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

                entity.HasOne(d => d.FichaMedica)
                    .WithMany(p => p.AntecedentesLaborales)
                    .HasForeignKey(d => d.IdFichaMedica)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AntecedentesLaborales_FichaMedica");
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

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.FichaMedica)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_FichaMedica_Persona");
            });

            modelBuilder.Entity<ExamenComplementario>(entity =>
            {
                entity.HasKey(e => e.IdExamenComplementario)
                    .HasName("PK_ExamenComplementario");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Resultado)
                    .IsRequired()
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.Url).HasColumnType("varchar(1024)");

                entity.HasOne(d => d.FichaMedica)
                    .WithMany(p => p.ExamenComplementario)
                    .HasForeignKey(d => d.IdFichaMedica)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ExamenComplementario_FichaMedica");

                entity.HasOne(d => d.TipoExamenComplementario)
                    .WithMany(p => p.ExamenComplementario)
                    .HasForeignKey(d => d.IdTipoExamenComplementario)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ExamenComplementario_ExamenComplementario");
            });

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

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.AccionPersonal)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEmpleado463");

                entity.HasOne(d => d.TipoAccionPersonal)
                    .WithMany(p => p.AccionPersonal)
                    .HasForeignKey(d => d.IdTipoAccionPersonal)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefTipoAccionPersonal462");
            });



            modelBuilder.Entity<ActividadesAnalisisOcupacional>(entity =>
            {
                entity.HasKey(e => e.IdActividadesAnalisisOcupacional)
                    .HasName("PK_ActividadesAnalisisOcupacional");

                entity.HasIndex(e => e.IdFormularioAnalisisOcupacional)
                    .HasName("IX_ActividadesAnalisisOcupacional_IdFormularioAnalisisOcupacional");

                entity.HasOne(d => d.FormularioAnalisisOcupacional)
                    .WithMany(p => p.ActividadesAnalisisOcupacional)
                    .HasForeignKey(d => d.IdFormularioAnalisisOcupacional)
                    .OnDelete(DeleteBehavior.Restrict);
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

                entity.Property(e => e.Observaciones)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Tarea)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Dependencia)
                    .WithMany(p => p.ActividadesGestionCambio)
                    .HasForeignKey(d => d.IdDependencia)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ActividadesGestionCambio_Dependencia");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.ActividadesGestionCambio)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ActividadesGestionCambio_Empleado");

            });


            modelBuilder.Entity<AdministracionTalentoHumano>(entity =>
            {
                entity.HasKey(e => e.IdAdministracionTalentoHumano)
                    .HasName("PK_AdministracionTalentoHumano");

                entity.HasIndex(e => e.EmpleadoResponsable)
                    .HasName("IX_AdministracionTalentoHumano_EmpleadoIdEmpleado");

                entity.HasIndex(e => e.IdFormularioAnalisisOcupacional)
                    .HasName("IX_AdministracionTalentoHumano_idFormularioAnalisisOcupacional");

                entity.HasIndex(e => e.IdRolPuesto)
                    .HasName("IX_AdministracionTalentoHumano_idRolPuesto");

                entity.Property(e => e.IdFormularioAnalisisOcupacional).HasColumnName("idFormularioAnalisisOcupacional");

                entity.Property(e => e.IdRolPuesto).HasColumnName("idRolPuesto");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.AdministracionTalentoHumano)
                    .HasForeignKey(d => d.EmpleadoResponsable);

                entity.HasOne(d => d.FormularioAnalisisOcupacional)
                    .WithMany(p => p.AdministracionTalentoHumano)
                    .HasForeignKey(d => d.IdFormularioAnalisisOcupacional)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.RolPuesto)
                    .WithMany(p => p.AdministracionTalentoHumano)
                    .HasForeignKey(d => d.IdRolPuesto)
                    .OnDelete(DeleteBehavior.Restrict);
            });



            modelBuilder.Entity<AprobacionViatico>(entity =>
            {
                entity.HasKey(e => e.IdAprobacionViatico)
                    .HasName("PK_AprobacionViatico");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_AprobacionViatico_IdEmpleado");

                entity.HasIndex(e => e.IdSolicitudViatico)
                    .HasName("IX_AprobacionViatico_IdSolicitudViatico");

                entity.Property(e => e.ValorADescontar)
                    .HasColumnName("ValorADescontar")
                    .HasColumnType("decimal");

                entity.Property(e => e.ValorJustificado).HasColumnType("decimal");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.AprobacionViatico)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.SolicitudViatico)
                    .WithMany(p => p.AprobacionViatico)
                    .HasForeignKey(d => d.IdSolicitudViatico)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<AreaConocimiento>(entity =>
            {
                entity.HasKey(e => e.IdAreaConocimiento)
                    .HasName("PK219");

                entity.Property(e => e.Descripcion).HasMaxLength(100);
            });


            modelBuilder.Entity<BrigadaSSO>(entity =>
            {
                entity.HasKey(e => e.IdBrigadaSSO)
                    .HasName("PK_BrigadaSSO");

                entity.ToTable("BrigadaSSO");

                entity.Property(e => e.IdBrigadaSSO).HasColumnName("IdBrigadaSSO");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BrigadaSSORol>(entity =>
            {
                entity.HasKey(e => e.IdBrigadaSSORol)
                    .HasName("PK_BrigadaSSORol");

                entity.ToTable("BrigadaSSORol");

                entity.HasIndex(e => e.IdBrigadaSSO)
                    .HasName("IX_BrigadaSSORol_IdBrigadaSSO");

                entity.Property(e => e.IdBrigadaSSORol).HasColumnName("IdBrigadaSSORol");

                entity.Property(e => e.IdBrigadaSSO).HasColumnName("IdBrigadaSSO");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.BrigadaSSO)
                    .WithMany(p => p.BrigadaSSORol)
                    .HasForeignKey(d => d.IdBrigadaSSO)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CandidatoConcurso>(entity =>
            {
                entity.HasKey(e => e.IdCandidatoConcurso)
                    .HasName("PK_CandidatoConcurso");

                entity.HasIndex(e => e.IdCandidato)
                    .HasName("IX_CandidatoConcurso_IdCandidato");

                entity.HasIndex(e => e.IdPartidasFase)
                    .HasName("IX_CandidatoConcurso_IdPartidasFase");

                entity.Property(e => e.CodigoSocioEmpleo)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Candidato)
                    .WithMany(p => p.CandidatoConcurso)
                    .HasForeignKey(d => d.IdCandidato)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.PartidasFase)
                    .WithMany(p => p.CandidatoConcurso)
                    .HasForeignKey(d => d.IdPartidasFase)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Calificacion>(entity =>
            {
                entity.HasKey(e => e.IdCalificacion)
                    .HasName("PK_Calificacion");

                entity.HasOne(d => d.CandidatoConcurso)
                    .WithMany(p => p.Calificacion)
                    .HasForeignKey(d => d.IdCandidatoConcurso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Calificacion_CandidatoConcurso");

                entity.HasOne(d => d.TipoCalificacion)
                    .WithMany(p => p.Calificacion)
                    .HasForeignKey(d => d.IdTipoCalificacion)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Calificacion_TipoCalificacion");
            });
            modelBuilder.Entity<Candidato>(entity =>
            {
                entity.HasKey(e => e.IdCandidato)
                    .HasName("PK_Canditato");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Identificacion)
                    .IsRequired()
                    .HasColumnType("varchar(13)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
            });
            modelBuilder.Entity<CandidatoEstudio>(entity =>
            {
                entity.HasKey(e => e.IdCandidatoEstudio)
                    .HasName("PK_CandidatoEstudio");

                entity.Property(e => e.FechaGraduado).HasColumnType("date");

                entity.Property(e => e.NoSenescyt).HasColumnType("varchar(50)");

                entity.Property(e => e.Observaciones).HasColumnType("varchar(300)");

                entity.HasOne(d => d.Candidato)
                    .WithMany(p => p.CandidatoEstudio)
                    .HasForeignKey(d => d.IdCandidato)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CandidatoEstudio_Candidato");

                entity.HasOne(d => d.Titulo)
                    .WithMany(p => p.CandidatoEstudio)
                    .HasForeignKey(d => d.IdTitulo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CandidatoEstudio_Titulo");
            });
            modelBuilder.Entity<CandidatoTrayectoriaLaboral>(entity =>
            {
                entity.HasKey(e => e.IdCandidatoTrayectoriaLaboral)
                    .HasName("PK_CandidatoTrayectoriaLaboral");

                entity.Property(e => e.FechaFin).HasColumnType("date");

                entity.Property(e => e.FechaInicio).HasColumnType("date");

                entity.Property(e => e.Institucion).HasColumnType("varchar(100)");

                entity.HasOne(d => d.Candidato)
                    .WithMany(p => p.CandidatoTrayectoriaLaboral)
                    .HasForeignKey(d => d.IdCandidato)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CandidatoTrayectoriaLaboral_Candidato");
            });
            modelBuilder.Entity<Capacitacion>(entity =>
            {
                entity.HasKey(e => e.IdCapacitacion)
                    .HasName("PK_Capacitacion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<CapacitacionAreaConocimiento>(entity =>
            {
                entity.HasKey(e => e.IdCapacitacionAreaConocimiento)
                    .HasName("PK_CapacitacionAreaConocimiento");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(20);
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

                entity.HasOne(d => d.CapacitacionRecibida)
                    .WithMany(p => p.CapacitacionEncuesta)
                    .HasForeignKey(d => d.IdCapacitacionRecibida)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefCapacitacionRecibida470");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.CapacitacionEncuesta)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEmpleado485");
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
                    .HasName("PK_CapacitacionPlanificacion");

                entity.HasIndex(e => e.IdCapacitacionModalidad)
                    .HasName("IX_CapacitacionPlanificacion_IdCapacitacionModalidad");

                entity.HasIndex(e => e.IdCapacitacionTemario)
                    .HasName("IX_CapacitacionPlanificacion_IdCapacitacionTemario");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_CapacitacionPlanificacion_IdEmpleado");

                entity.Property(e => e.Presupuesto).HasColumnType("decimal");

                entity.HasOne(d => d.CapacitacionModalidad)
                    .WithMany(p => p.CapacitacionPlanificacion)
                    .HasForeignKey(d => d.IdCapacitacionModalidad)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.CapacitacionTemario)
                    .WithMany(p => p.CapacitacionPlanificacion)
                    .HasForeignKey(d => d.IdCapacitacionTemario)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.CapacitacionPlanificacion)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);
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

                entity.HasOne(d => d.CapacitacionEncuesta)
                    .WithMany(p => p.CapacitacionPregunta)
                    .HasForeignKey(d => d.IdCapacitacionEncuesta)
                    .HasConstraintName("FK_CapacitacionPregunta_CapacitacionEncuesta");

                entity.HasOne(d => d.CapacitacionRespuesta)
                    .WithMany(p => p.CapacitacionPregunta)
                    .HasForeignKey(d => d.IdCapacitacionRespuesta)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CapacitacionPregunta_CapacitacionRespuesta");

                entity.HasOne(d => d.CapacitacionTipoPregunta)
                    .WithMany(p => p.CapacitacionPregunta)
                    .HasForeignKey(d => d.IdCapacitacionTipoPregunta)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefCapacitacionTipoPregunta473");
            });

            modelBuilder.Entity<CapacitacionProveedor>(entity =>
            {
                entity.HasKey(e => e.IdCapacitacionProveedor)
                    .HasName("PK175");

                //entity.HasIndex(e => e.IdCapacitacionRecibida)
                //    .HasName("Ref315476");

                entity.Property(e => e.Direccion).HasColumnType("varchar(40)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Telefono).HasColumnType("varchar(20)");

                entity.HasOne(d => d.Pais)
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

                entity.HasOne(d => d.CapacitacionTemarioProveedor)
                    .WithMany(p => p.CapacitacionRecibida)
                    .HasForeignKey(d => d.IdCapacitacionTemarioProveedor)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CapacitacionRecibida_CapacitacionTemarioProveedor");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.CapacitacionRecibida)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEmpleado483");
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
                    .HasName("PK_CapacitacionTemario");

                entity.HasIndex(e => e.IdCapacitacionAreaConocimiento)
                    .HasName("IX_CapacitacionTemario_IdCapacitacionAreaConocimiento");

                entity.Property(e => e.Tema)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.CapacitacionAreaConocimiento)
                    .WithMany(p => p.CapacitacionTemario)
                    .HasForeignKey(d => d.IdCapacitacionAreaConocimiento)
                    .OnDelete(DeleteBehavior.Restrict);
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

                entity.HasOne(d => d.CapacitacionModalidad)
                    .WithMany(p => p.CapacitacionTemarioProveedor)
                    .HasForeignKey(d => d.IdCapacitacionModalidad)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefCapacitacionModalidad477");

                entity.HasOne(d => d.CapacitacionProveedor)
                    .WithMany(p => p.CapacitacionTemarioProveedor)
                    .HasForeignKey(d => d.IdCapacitacionProveedor)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CapacitacionTemarioProveedor_CapacitacionProveedor");

                entity.HasOne(d => d.CapacitacionTemario)
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

            modelBuilder.Entity<CeseFuncion>(entity =>
      {
          entity.HasKey(e => e.IdCeseFuncion)
              .HasName("PK_CeseFuncion");

          entity.Property(e => e.Fecha).HasColumnType("date");

          entity.Property(e => e.Observacion).HasMaxLength(250);

          entity.HasOne(d => d.IdEmpleadoNavigation)
              .WithMany(p => p.CeseFuncion)
              .HasForeignKey(d => d.IdEmpleado)
              .OnDelete(DeleteBehavior.Restrict)
              .HasConstraintName("FK_CeseFuncion_Empleado");

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

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Provincia)
                    .WithMany(p => p.Ciudad)
                    .HasForeignKey(d => d.IdProvincia)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ComportamientoObservable>(entity =>
            {
                entity.HasIndex(e => e.IdDenominacionCompetencia)
                    .HasName("IX_ComportamientoObservable_DenominacionCompetenciaId");

                entity.HasIndex(e => e.IdNivel)
                    .HasName("IX_ComportamientoObservable_NivelId");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.DenominacionCompetencia)
                    .WithMany(p => p.ComportamientoObservable)
                    .HasForeignKey(d => d.IdDenominacionCompetencia)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Nivel)
                    .WithMany(p => p.ComportamientoObservable)
                    .HasForeignKey(d => d.IdNivel)
                    .OnDelete(DeleteBehavior.Restrict);
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

                entity.HasOne(d => d.RolPuesto)
                    .WithMany(p => p.ConfiguracionViatico)
                    .HasForeignKey(d => d.IdRolPuesto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ConfiguracionViatico_RolPuesto");
            });

            modelBuilder.Entity<ConfirmacionLectura>(entity =>
            {
                entity.HasKey(e => e.IdConfirmacionLectura)
                    .HasName("PK_ConfirmacionLectura");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_ConfirmacionLectura_IdEmpleado");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.ConfirmacionLectura)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ConocimientosAdicionales>(entity =>
            {
                entity.HasKey(e => e.IdConocimientosAdicionales)
                    .HasName("PK_ConocimientosAdicionales");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<DatosBancarios>(entity =>
            {
                entity.HasKey(e => e.IdDatosBancarios)
                    .HasName("PK_DatosBancarios");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_DatosBancarios_IdEmpleado");

                entity.HasIndex(e => e.IdInstitucionFinanciera)
                    .HasName("IX_DatosBancarios_IdInstitucionFinanciera");

                entity.Property(e => e.NumeroCuenta)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.DatosBancarios)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.InstitucionFinanciera)
                    .WithMany(p => p.DatosBancarios)
                    .HasForeignKey(d => d.IdInstitucionFinanciera)
                    .OnDelete(DeleteBehavior.Restrict);
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

                entity.HasOne(d => d.DocumentosIngreso)
                    .WithMany(p => p.DocumentosIngresoEmpleado)
                    .HasForeignKey(d => d.IdDocumentosIngreso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DocumentosIngresoEmpleado_DocumentosIngreso");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.DocumentosIngresoEmpleado)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DocumentosIngresoEmpleado_Empleado");
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

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.DeclaracionPatrimonioPersonal)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEmpleado152");
            });

            modelBuilder.Entity<DenominacionCompetencia>(entity =>
            {
                entity.HasKey(e => e.IdDenominacionCompetencia)
                    .HasName("PK_DenominacionCompetencia");

                entity.Property(e => e.Definicion).IsRequired();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Dependencia>(entity =>
            {
                entity.HasKey(e => e.IdDependencia)
                    .HasName("PK_Dependencia");

                entity.HasIndex(e => e.IdDependenciaPadre)
                    .HasName("IX_Dependencia_DependenciaPadreIdDependencia");

                entity.HasIndex(e => e.IdSucursal)
                    .HasName("IX_Dependencia_IdSucursal");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.HasOne(d => d.Proceso)
                    .WithMany(p => p.Dependencia)
                    .HasForeignKey(d => d.IdProceso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Dependencia_Proceso");

                entity.HasOne(d => d.DependenciaPadre)
                    .WithMany(p => p.Dependencia1)
                    .HasForeignKey(d => d.IdDependenciaPadre);

                entity.HasOne(d => d.Sucursal)
                    .WithMany(p => p.Dependencia)
                    .HasForeignKey(d => d.IdSucursal)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DependenciaDocumento>(entity =>
            {
                entity.HasKey(e => e.IdDependenciaDocumento)
                    .HasName("PK_DependenciaDocumento");

                entity.HasIndex(e => e.IdDependencia)
                    .HasName("IX_DependenciaDocumento_IdDependencia");

                entity.HasIndex(e => e.IdDocumentosSubidos)
                    .HasName("IX_DependenciaDocumento_IdDocumentosSubidos");

                entity.HasOne(d => d.Dependencia)
                    .WithMany(p => p.DependenciaDocumento)
                    .HasForeignKey(d => d.IdDependencia)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.DocumentosParentescodos)
                    .WithMany(p => p.DependenciaDocumento)
                    .HasForeignKey(d => d.IdDocumentosSubidos)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<Destreza>(entity =>
            {
                entity.HasKey(e => e.IdDestreza)
                    .HasName("PK_Destreza");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);
            });
            modelBuilder.Entity<DetalleEvaluacionEvento>(entity =>
            {
                entity.HasKey(e => e.IdDetalleEvaluacionEvento)
                    .HasName("PK_DetalleEvaluacionEvento");

                entity.Property(e => e.IdDetalleEvaluacionEvento);

                entity.HasOne(d => d.EvaluacionEvento)
                    .WithMany(p => p.DetalleEvaluacionEvento)
                    .HasForeignKey(d => d.IdEvaluacionEvento)
                    .HasConstraintName("FK_DetalleEvaluacionEvento_EvaluacionEvento");

                entity.HasOne(d => d.PreguntasEvaluacionEvento)
                    .WithMany(p => p.DetalleEvaluacionEvento)
                    .HasForeignKey(d => d.IdPreguntasEvaluacionEvento)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DetalleEvaluacionEvento_PreguntaEvaluacionEvento");
            });

            modelBuilder.Entity<DetalleExamenInduccion>(entity =>
            {
                entity.HasKey(e => e.IdDetalleExamenInduccion)
                    .HasName("PK_DetalleExamenInduccion");

                entity.HasIndex(e => e.IdPregunta)
                    .HasName("IX_DetalleExamenInduccion_PreguntaId");

                entity.HasIndex(e => e.IdRealizaExamenInduccion)
                    .HasName("IX_DetalleExamenInduccion_RealizaExamenInduccionId");

                entity.HasIndex(e => e.IdRespuesta)
                    .HasName("IX_DetalleExamenInduccion_RespuestaId");

                entity.HasOne(d => d.Pregunta)
                    .WithMany(p => p.DetalleExamenInduccion)
                    .HasForeignKey(d => d.IdPregunta);

                entity.HasOne(d => d.RealizaExamenInduccion)
                    .WithMany(p => p.DetalleExamenInduccion)
                    .HasForeignKey(d => d.IdRealizaExamenInduccion)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Respuesta)
                    .WithMany(p => p.DetalleExamenInduccion)
                    .HasForeignKey(d => d.IdRespuesta)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<DocumentosParentescodos>(entity =>
            {
                entity.HasKey(e => e.IdDocumentosSubidos)
                    .HasName("PK_DocumentosParentescodos");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_DocumentosParentescodos_IdEmpleado");

                entity.Property(e => e.Are)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.NombreArchivo)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Ubicacion).IsRequired();

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.DocumentosParentescodos)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);
            });




            modelBuilder.Entity<EmpleadoContactoEmergencia>(entity =>
            {
                entity.HasKey(e => e.IdEmpleadoContactoEmergencia)
                    .HasName("PK_EmpleadoContactoEmergencia");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_EmpleadoContactoEmergencia_IdEmpleado");

                entity.HasIndex(e => e.IdParentesco)
                    .HasName("IX_EmpleadoContactoEmergencia_IdParentesco");

                entity.HasIndex(e => e.IdPersona)
                    .HasName("IX_EmpleadoContactoEmergencia_IdPersona");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.EmpleadoContactoEmergencia)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Parentesco)
                    .WithMany(p => p.EmpleadoContactoEmergencia)
                    .HasForeignKey(d => d.IdParentesco)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.EmpleadoContactoEmergencia)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<EmpleadoFamiliar>(entity =>
            {
                entity.HasKey(e => e.IdEmpleadoFamiliar)
                    .HasName("PK_EmpleadoFamiliar");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_EmpleadoFamiliar_IdEmpleado");

                entity.HasIndex(e => e.IdParentesco)
                    .HasName("IX_EmpleadoFamiliar_IdParentesco");

                entity.HasIndex(e => e.IdPersona)
                    .HasName("IX_EmpleadoFamiliar_IdPersona");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.EmpleadoFamiliar)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Parentesco)
                    .WithMany(p => p.EmpleadoFamiliar)
                    .HasForeignKey(d => d.IdParentesco)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.EmpleadoFamiliar)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<EmpleadoFormularioCapacitacion>(entity =>
            {
                entity.HasKey(e => e.IdEmpleadoFormularioCapacitacion)
                    .HasName("PK_EmpleadoFormularioCapacitacion");

                entity.HasIndex(e => e.IdFormularioCapacitacion)
                    .HasName("IX_EmpleadoFormularioCapacitacion_IdFormularioCapacitacion");

                entity.HasIndex(e => e.IdServidor)
                    .HasName("IX_EmpleadoFormularioCapacitacion_ServidorIdEmpleado");

                entity.HasOne(d => d.FormularioCapacitacion)
                    .WithMany(p => p.EmpleadoFormularioCapacitacion)
                    .HasForeignKey(d => d.IdFormularioCapacitacion)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Servidor)
                    .WithMany(p => p.EmpleadoFormularioCapacitacion)
                    .HasForeignKey(d => d.IdServidor);
            });

            modelBuilder.Entity<EmpleadoIE>(entity =>
            {
                entity.HasKey(e => e.IdEmpeladoIE)
                    .HasName("PK_EmpleadoIE");

                entity.ToTable("EmpleadoIE");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_EmpleadoIE_IdEmpleado");

                entity.HasIndex(e => e.IdIngresoEgresoRMU)
                    .HasName("IX_EmpleadoIE_IdIngresoEgresoRMU");

                entity.Property(e => e.IdEmpeladoIE).HasColumnName("IdEmpeladoIE");

                entity.Property(e => e.IdIngresoEgresoRMU).HasColumnName("IdIngresoEgresoRMU");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Valor).HasColumnType("decimal");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.EmpleadoIE)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IngresoEgresoRMU)
                    .WithMany(p => p.EmpleadoIE)
                    .HasForeignKey(d => d.IdIngresoEgresoRMU)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PersonaSustituto>(entity =>
            {
                entity.HasKey(e => e.IdPersonaSustituto)
                    .HasName("PK_PersonaSustituto");

                entity.HasIndex(e => e.IdPersona)
                    .HasName("IX_PersonaSustituto")
                    .IsUnique();

                entity.HasOne(d => d.Parentesco)
                    .WithMany(p => p.PersonaSustituto)
                    .HasForeignKey(d => d.IdParentesco)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PersonaSustituto_Parentesco");

                //entity.HasOne(d => d.Persona)
                //    .WithOne(p => p.PersonaSustitutoPersona)
                //    .HasForeignKey<PersonaSustituto>(d => d.IdPersona)
                //    .OnDelete(DeleteBehavior.Restrict)
                //    .HasConstraintName("FK_PersonaSustituto_Persona");


            });

            modelBuilder.Entity<EnfermedadSustituto>(entity =>
            {
                entity.HasKey(e => e.IdEnfermedadSustituto)
                    .HasName("PK_EnfermedadSustituto");

                entity.Property(e => e.InstitucionEmite).HasColumnType("varchar(100)");

                entity.HasOne(d => d.PersonaSustituto)
                    .WithMany(p => p.EnfermedadSustituto)
                    .HasForeignKey(d => d.IdPersonaSustituto)
                    .HasConstraintName("FK_EnfermedadSustituto_PersonaSustituto");

                entity.HasOne(d => d.TipoEnfermedad)
                    .WithMany(p => p.EnfermedadSustituto)
                    .HasForeignKey(d => d.IdTipoEnfermedad)
                    .HasConstraintName("FK_EnfermedadSustituto_TipoEnfermedad");
            });
            modelBuilder.Entity<DetallePresupuesto>(entity =>
            {
                entity.HasKey(e => e.IdDetallePresupuesto)
                    .HasName("PK_DetallePresupuesto");

                entity.Property(e => e.IdDetallePresupuesto);

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.HasOne(d => d.Presupuesto)
                    .WithMany(p => p.DetallePresupuesto)
                    .HasForeignKey(d => d.IdPresupuesto)
                    .HasConstraintName("FK_DetallePresupuesto_Presupuesto");

                entity.HasOne(d => d.SolicitudViatico)
                    .WithMany(p => p.DetallePresupuesto)
                    .HasForeignKey(d => d.IdSolicitudViatico)
                    .HasConstraintName("FK_DetallePresupuesto_SolicitudViatico");
            });

            modelBuilder.Entity<DetalleReliquidacionViatico>(entity =>
            {
                entity.HasKey(e => e.IdDetalleReliquidacionViatico)
                    .HasName("PK_DetalleRequlidacionViatico");

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.FechaLlegada).HasColumnType("date");

                entity.Property(e => e.FechaSalida).HasColumnType("date");

                entity.Property(e => e.HoraLlegada).HasColumnName("HoraLLegada");

                entity.Property(e => e.NombreTransporte).HasColumnType("varchar(250)");

                entity.Property(e => e.ValorEstimado).HasColumnType("decimal");

                entity.HasOne(d => d.CiudadDestino)
                    .WithMany(p => p.DetalleReliquidacionViaticoCiudadDestino)
                    .HasForeignKey(d => d.IdCiudadDestino)
                    .HasConstraintName("FK_DetalleReliquidacionViatico_Ciudad");

                entity.HasOne(d => d.CiudadOrigen)
                    .WithMany(p => p.DetalleReliquidacionViaticoCiudadOrigen)
                    .HasForeignKey(d => d.IdCiudadOrigen)
                    .HasConstraintName("FK_DetalleReliquidacionViatico_Ciudad1");

                entity.HasOne(d => d.ItemViatico)
                    .WithMany(p => p.DetalleReliquidacionViatico)
                    .HasForeignKey(d => d.IdItemViatico)
                    .HasConstraintName("FK_DetalleReliquidacionViatico_ItemViatico");

                entity.HasOne(d => d.ReliquidacionViatico)
                    .WithMany(p => p.DetalleReliquidacionViatico)
                    .HasForeignKey(d => d.IdReliquidacionViatico)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DetalleReliquidacionViatico_SolicitudViatico");

                entity.HasOne(d => d.TipoTransporte)
                    .WithMany(p => p.DetalleReliquidacionViatico)
                    .HasForeignKey(d => d.IdTipoTransporte)
                    .HasConstraintName("FK_DetalleReliquidacionViatico_TipoTransporte");
            });

            modelBuilder.Entity<DiscapacidadSustituto>(entity =>
            {
                entity.HasKey(e => e.IdDiscapacidadSustituto)
                    .HasName("PK_DiscapacidadSustituto");

                entity.Property(e => e.NumeroCarnet)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.PersonaSustituto)
                    .WithMany(p => p.DiscapacidadSustituto)
                    .HasForeignKey(d => d.IdPersonaSustituto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DiscapacidadSustituto_PersonaSustituto");

                entity.HasOne(d => d.TipoDiscapacidad)
                    .WithMany(p => p.DiscapacidadSustituto)
                    .HasForeignKey(d => d.IdTipoDiscapacidad)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DiscapacidadSustituto_TipoDiscapacidad");
            });

            modelBuilder.Entity<EmpleadoImpuestoRenta>(entity =>
            {
                entity.HasKey(e => e.IdEmpleadoImpuestoRenta)
                    .HasName("PK_EmpleadoImpuestoRenta");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_EmpleadoImpuestoRenta_IdEmpleado");

                entity.Property(e => e.IngresoTotal).HasColumnType("decimal");

                entity.Property(e => e.OtrosIngresos).HasColumnType("decimal");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.EmpleadoImpuestoRenta)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<EmpleadoMovimiento>(entity =>
            {
                entity.HasKey(e => e.IdEmpleadoMovimiento)
                    .HasName("PK126");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("Ref15414");

                entity.HasIndex(e => e.IdIndiceOcupacionalModalidadPartidaDesde)
                    .HasName("Ref71195");

                entity.Property(e => e.FechaDesde).HasColumnType("date");

                entity.Property(e => e.FechaHasta).HasColumnType("date");

                entity.HasOne(d => d.AccionPersonal)
                    .WithMany(p => p.EmpleadoMovimiento)
                    .HasForeignKey(d => d.IdAccionPersonal)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_EmpleadoMovimiento_AccionPersonal");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.EmpleadoMovimiento)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_EmpleadoMovimiento_Empleado");

                entity.HasOne(d => d.IndiceOcupacionalModalidadPartidaDesde)
                    .WithMany(p => p.EmpleadoMovimientoIdIndiceOcupacionalModalidadPartidaDesde)
                    .HasForeignKey(d => d.IdIndiceOcupacionalModalidadPartidaDesde)
                    .HasConstraintName("RefIndiceOcupacionalModalidadPartida195");

                entity.HasOne(d => d.IndiceOcupacionalModalidadPartidaHasta)
                    .WithMany(p => p.EmpleadoMovimientoIdIndiceOcupacionalModalidadPartidaHasta)
                    .HasForeignKey(d => d.IdIndiceOcupacionalModalidadPartidaHasta)
                    .HasConstraintName("FK_EmpleadoMovimiento_IndiceOcupacionalModalidadPartida");
            });

            modelBuilder.Entity<EmpleadoNepotismo>(entity =>
            {
                entity.HasKey(e => e.IdEmpleadoNepotismo)
                    .HasName("PK_EmpleadoNepotismo");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_EmpleadoNepotismo_IdEmpleado");

                entity.HasIndex(e => e.IdEmpleadoFamiliar);

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.EmpleadoNepotismoEmpleado)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.EmpleadoFamiliar)
                    .WithMany(p => p.EmpleadoNepotismoFamiliar)
                    .HasForeignKey(d => d.IdEmpleadoFamiliar)
                    .OnDelete(DeleteBehavior.Restrict);






            });

            modelBuilder.Entity<EmpleadoSaldoVacaciones>(entity =>
            {
                entity.HasKey(e => e.IdEmpleadoSaldoVacaciones)
                    .HasName("PK_EmpleadoSaldoVacaciones");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_EmpleadoSaldoVacaciones_IdEmpleado");

                entity.Property(e => e.SaldoDias).HasColumnType("decimal");

                entity.Property(e => e.SaldoMonetario).HasColumnType("decimal");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.EmpleadoSaldoVacaciones)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<EmpleadosFormularioDevengacion>(entity =>
            {
                entity.HasKey(e => e.IdEmpleadosFormularioDevengacion)
                    .HasName("PK_EmpleadosFormularioDevengacion");

                entity.HasIndex(e => e.IdFormularioDevengacion)
                    .HasName("IX_EmpleadosFormularioDevengacion_FormularioDevengacionId");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_EmpleadosFormularioDevengacion_IdEmpleado");

                entity.HasOne(d => d.FormularioDevengacion)
                    .WithMany(p => p.EmpleadosFormularioDevengacion)
                    .HasForeignKey(d => d.IdFormularioDevengacion)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.EmpleadosFormularioDevengacion)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<EscalaEvaluacionTotal>(entity =>
            {
                entity.HasKey(e => e.IdEscalaEvaluacionTotal)
                    .HasName("PK_EscalaEvaluacionTotal");

                entity.Property(e => e.Descripcion).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PorcientoDesde).HasColumnType("decimal");

                entity.Property(e => e.PorcientoHasta).HasColumnType("decimal");
            });

            modelBuilder.Entity<EscalaGrados>(entity =>
            {
                entity.HasKey(e => e.IdEscalaGrados)
                    .HasName("PK_EscalaGrados");

                entity.HasIndex(e => e.IdGrupoOcupacional)
                    .HasName("IX_EscalaGrados_IdGrupoOcupacional");

                entity.Property(e => e.Remuneracion).HasColumnType("decimal");

                entity.HasOne(d => d.GrupoOcupacional)
                    .WithMany(p => p.EscalaGrados)
                    .HasForeignKey(d => d.IdGrupoOcupacional)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<EspecificidadExperiencia>(entity =>
            {
                entity.HasKey(e => e.IdEspecificidadExperiencia)
                    .HasName("PK_EspecificidadExperiencia");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(200);
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
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Estudio>(entity =>
            {
                entity.HasKey(e => e.IdEstudio)
                    .HasName("PK_Estudio");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);
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

                entity.HasIndex(e => e.IdEvaluador)
                    .HasName("Ref182301");

                entity.Property(e => e.FechaEvaluacionDesde).HasColumnType("date");

                entity.Property(e => e.FechaEvaluacionHasta).HasColumnType("date");

                entity.Property(e => e.FechaRegistro).HasColumnType("date");

                entity.Property(e => e.Observaciones).HasColumnType("text");

                entity.HasOne(d => d.EmpleadoEvaluado)
                    .WithMany(p => p.Eval001)
                    .HasForeignKey(d => d.IdEmpleadoEvaluado)
                    .HasConstraintName("FK_Eval001_Empleado");

                entity.HasOne(d => d.EscalaEvaluacionTotal)
                    .WithMany(p => p.Eval001)
                    .HasForeignKey(d => d.IdEscalaEvaluacionTotal)
                    .HasConstraintName("RefEscalaEvaluacionTotal78");

                entity.HasOne(d => d.Evaluador)
                    .WithMany(p => p.Eval001)
                    .HasForeignKey(d => d.IdEvaluador)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEvaluador301");
            });

            modelBuilder.Entity<EvaluacionActividadesPuestoTrabajo>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionActividadesPuestoTrabajo)
                    .HasName("PK34");

                entity.HasIndex(e => e.IdIndicador)
                    .HasName("Ref3157");

                entity.Property(e => e.DescripcionActividad)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.ActividadesEsenciales)
                    .WithMany(p => p.EvaluacionActividadesPuestoTrabajo)
                    .HasForeignKey(d => d.IdActividadesEsenciales)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_EvaluacionActividadesPuestoTrabajo_ActividadesEsenciales");

                entity.HasOne(d => d.Eval001)
                    .WithMany(p => p.EvaluacionActividadesPuestoTrabajo)
                    .HasForeignKey(d => d.IdEval001)
                    .HasConstraintName("FK_EvaluacionActividadesPuestoTrabajo_Eval001");

                entity.HasOne(d => d.Indicador)
                    .WithMany(p => p.EvaluacionActividadesPuestoTrabajo)
                    .HasForeignKey(d => d.IdIndicador)
                    .HasConstraintName("RefIndicador57");
            });


            modelBuilder.Entity<EvaluacionCompetenciasTecnicasPuesto>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionCompetenciasTecnicasPuesto)
                    .HasName("PK39");

                entity.HasIndex(e => e.IdNivelDesarrollo)
                    .HasName("Ref4265");

                entity.HasOne(d => d.ComportamientoObservable)
                    .WithMany(p => p.EvaluacionCompetenciasTecnicasPuesto)
                    .HasForeignKey(d => d.IdComportamientoObservable)
                    .HasConstraintName("FK_EvaluacionCompetenciasTecnicasPuesto_ComportamientoObservable");

                entity.HasOne(d => d.Eval001)
                    .WithMany(p => p.EvaluacionCompetenciasTecnicasPuesto)
                    .HasForeignKey(d => d.IdEval001)
                    .HasConstraintName("FK_EvaluacionCompetenciasTecnicasPuesto_Eval001");

                entity.HasOne(d => d.NivelDesarrollo)
                    .WithMany(p => p.EvaluacionCompetenciasTecnicasPuesto)
                    .HasForeignKey(d => d.IdNivelDesarrollo)
                    .HasConstraintName("RefNivelDesarrollo65");
            });

            modelBuilder.Entity<EvaluacionCompetenciasUniversales>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionCompetenciasUniversales)
                    .HasName("PK44");

                entity.HasIndex(e => e.IdFrecuenciaAplicacion)
                    .HasName("Ref4570");

                entity.HasOne(d => d.ComportamientoObservable)
                    .WithMany(p => p.EvaluacionCompetenciasUniversales)
                    .HasForeignKey(d => d.IdComportamientoObservable)
                    .HasConstraintName("FK_EvaluacionCompetenciasUniversales_ComportamientoObservable");

                entity.HasOne(d => d.Eval001)
                    .WithMany(p => p.EvaluacionCompetenciasUniversales)
                    .HasForeignKey(d => d.IdEval001)
                    .HasConstraintName("FK_EvaluacionCompetenciasUniversales_Eval001");

                entity.HasOne(d => d.FrecuenciaAplicacion)
                    .WithMany(p => p.EvaluacionCompetenciasUniversales)
                    .HasForeignKey(d => d.IdFrecuenciaAplicacion)
                    .HasConstraintName("RefFrecuenciaAplicacion70");
            });



            modelBuilder.Entity<EvaluacionConocimiento>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionConocimiento)
                    .HasName("PK36");

                entity.HasIndex(e => e.IdNivelConocimiento)
                    .HasName("Ref3759");

                entity.HasOne(d => d.AreaConocimiento)
                    .WithMany(p => p.EvaluacionConocimiento)
                    .HasForeignKey(d => d.IdAreaConocimiento)
                    .HasConstraintName("FK_EvaluacionConocimiento_AreaConocimiento");

                entity.HasOne(d => d.Eval001)
                    .WithMany(p => p.EvaluacionConocimiento)
                    .HasForeignKey(d => d.IdEval001)
                    .HasConstraintName("FK_EvaluacionConocimiento_Eval001");

                entity.HasOne(d => d.NivelConocimiento)
                    .WithMany(p => p.EvaluacionConocimiento)
                    .HasForeignKey(d => d.IdNivelConocimiento)
                    .HasConstraintName("RefNivelConocimiento59");
            });
            modelBuilder.Entity<EvaluacionEvento>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionEvento)
                    .HasName("PK_EvaluacionEvento");

                entity.Property(e => e.IdEvaluacionEvento);

                entity.Property(e => e.Sugerencias).HasColumnType("text");

                entity.HasOne(d => d.PlanCapacitacion)
                    .WithMany(p => p.EvaluacionEvento)
                    .HasForeignKey(d => d.IdPlanCapacitacion)
                    .HasConstraintName("FK_EvaluacionEvento_PlanCapacitacion");
            });

            modelBuilder.Entity<EvaluacionInducion>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionInduccion)
                    .HasName("PK_EvaluacionInducion");

                entity.Property(e => e.MinimoAprobar).HasColumnName("MinimoAprobar");
                entity.Property(e => e.MaximoPuntos).HasColumnName("MaximoPuntos");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });


            modelBuilder.Entity<TipoDiscapacidadSustituto>(entity =>
            {
                entity.HasKey(e => e.IdTipoDiscapacidadSustituto)
                    .HasName("PK68");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<EvaluacionTrabajoEquipoIniciativaLiderazgo>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionTrabajoEquipoIniciativaLiderazgo)
                    .HasName("PK47");

                entity.HasIndex(e => e.IdComportamientoObservable)
                    .HasName("Ref4173");

                entity.HasIndex(e => e.IdFrecuenciaAplicacion)
                    .HasName("Ref4574");

                entity.HasOne(d => d.ComportamientoObservable)
                    .WithMany(p => p.EvaluacionTrabajoEquipoIniciativaLiderazgo)
                    .HasForeignKey(d => d.IdComportamientoObservable)
                    .HasConstraintName("FK_EvaluacionTrabajoEquipoIniciativaLiderazgo_ComportamientoObservable");

                entity.HasOne(d => d.Eval001)
                    .WithMany(p => p.EvaluacionTrabajoEquipoIniciativaLiderazgo)
                    .HasForeignKey(d => d.IdEval001)
                    .HasConstraintName("FK_EvaluacionTrabajoEquipoIniciativaLiderazgo_Eval001");

                entity.HasOne(d => d.FrecuenciaAplicacion)
                    .WithMany(p => p.EvaluacionTrabajoEquipoIniciativaLiderazgo)
                    .HasForeignKey(d => d.IdFrecuenciaAplicacion)
                    .HasConstraintName("RefFrecuenciaAplicacion74");

            });

            modelBuilder.Entity<Evaluador>(entity =>
            {
                entity.HasKey(e => e.IdEvaluador)
                    .HasName("PK_Evaluador");

                entity.HasIndex(e => e.IdDependencia)
                    .HasName("IX_Evaluador_IdDependencia");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_Evaluador_IdEmpleado");

                entity.HasOne(d => d.Dependencia)
                    .WithMany(p => p.Evaluador)
                    .HasForeignKey(d => d.IdDependencia)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.Evaluador)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Exepciones>(entity =>
            {
                entity.HasKey(e => e.IdExepciones)
                    .HasName("PK_Exepciones");

                entity.HasIndex(e => e.IdValidacionJefe)
                    .HasName("IX_Exepciones_ValidacionInmediatoSuperiorIdValidacionJefe");

                entity.Property(e => e.Detalle)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.ValidacionInmediatoSuperior)
                    .WithMany(p => p.Exepciones)
                    .HasForeignKey(d => d.IdValidacionJefe);
            });


            modelBuilder.Entity<ExperienciaLaboralRequerida>(entity =>
            {
                entity.HasKey(e => e.IdExperienciaLaboralRequerida)
                    .HasName("PK230");

                entity.HasIndex(e => e.IdEspecificidadExperiencia)
                    .HasName("Ref229350");

                entity.HasIndex(e => e.IdEstudio)
                    .HasName("Ref214352");

                entity.HasOne(d => d.EspecificidadExperiencia)
                    .WithMany(p => p.ExperienciaLaboralRequerida)
                    .HasForeignKey(d => d.IdEspecificidadExperiencia)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEspecificidadExperiencia350");

                entity.HasOne(d => d.Estudio)
                    .WithMany(p => p.ExperienciaLaboralRequerida)
                    .HasForeignKey(d => d.IdEstudio)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEstudio352");
            });




            modelBuilder.Entity<IndiceOcupacionalExperienciaLaboralRequerida>(entity =>
            {
                entity.HasKey(e => e.IdIndiceOcupacionalExperienciaLaboralRequerida)
                    .HasName("PK_IndiceOcupacionalExperienciaLaboralRequerida");

                entity.HasOne(d => d.ExperienciaLaboralRequerida)
                    .WithMany(p => p.IndiceOcupacionalExperienciaLaboralRequerida)
                    .HasForeignKey(d => d.IdExperienciaLaboralRequerida)
                    .HasConstraintName("FK_IndiceOcupacionalExperienciaLaboralRequerida_ExperienciaLaboralRequerida");

                entity.HasOne(d => d.IndiceOcupacional)
                    .WithMany(p => p.IndiceOcupacionalExperienciaLaboralRequerida)
                    .HasForeignKey(d => d.IdIndiceOcupacional)
                    .HasConstraintName("FK_IndiceOcupacionalExperienciaLaboralRequerida_IndiceOcupacional");
            });

            modelBuilder.Entity<Factor>(entity =>
            {
                entity.HasKey(e => e.IdFactor)
                    .HasName("PK_Factor");

                entity.Property(e => e.Porciento).HasColumnType("decimal");
            });


            modelBuilder.Entity<FacturaViatico>(entity =>
            {
                entity.HasKey(e => e.IdFacturaViatico)
                    .HasName("PK253");

                entity.HasIndex(e => e.AprobadoPor)
                    .HasName("Ref15397");

                entity.HasIndex(e => e.IdItemViatico)
                    .HasName("Ref252388");

                entity.HasIndex(e => e.IdSolicitudViatico)
                    .HasName("Ref251389");

                entity.Property(e => e.FechaAprobacion).HasColumnType("date");

                entity.Property(e => e.FechaFactura).HasColumnType("date");

                entity.Property(e => e.NumeroFactura)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Observaciones).HasColumnType("text");

                entity.Property(e => e.Url).HasColumnType("varchar(1024)");

                entity.Property(e => e.ValorTotalAprobacion).HasColumnType("date");

                entity.Property(e => e.ValorTotalFactura).HasColumnType("decimal");

                entity.HasOne(d => d.ItemViatico)
                    .WithMany(p => p.FacturaViatico)
                    .HasForeignKey(d => d.IdItemViatico)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefItemViatico388");

                entity.HasOne(d => d.SolicitudViatico)
                    .WithMany(p => p.FacturaViatico)
                    .HasForeignKey(d => d.IdSolicitudViatico)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_FacturaViatico_SolicitudViatico");
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
                    .HasName("PK_FormularioAnalisisOcupacional");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_FormularioAnalisisOcupacional_IdEmpleado");

                entity.Property(e => e.MisionPuesto).IsRequired();

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.FormularioAnalisisOcupacional)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<FormularioCapacitacion>(entity =>
            {
                entity.HasKey(e => e.IdFormularioCapacitacion)
                    .HasName("PK_FormularioCapacitacion");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<FormularioDevengacion>(entity =>
            {
                entity.HasKey(e => e.IdFormularioDevengacion)
                    .HasName("PK_FormularioDevengacion");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_FormularioDevengacion_IdEmpleado");


                entity.HasIndex(e => e.AnalistaDesarrolloInstitucional)
                    .HasName("IX_FormularioDevengacion_IdEmpleadoDesarrolloInstitucional");

                entity.HasIndex(e => e.ResponsableArea)
                    .HasName("IX_FormularioDevengacion_IdEmpleadoResponsableArea");

                entity.HasIndex(e => e.IdModosScializacion)
                    .HasName("IX_FormularioDevengacion_IdModosScializacion");

                entity.Property(e => e.ModoSocial)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.FormularioDevengacion)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.EmpleadoDesarrolloInstitucional)
                    .WithMany(p => p.FormularioDevengacion1)
                    .HasForeignKey(d => d.AnalistaDesarrolloInstitucional)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.EmpleadoResponsableArea)
                   .WithMany(p => p.FormularioDevengacion2)
                   .HasForeignKey(d => d.ResponsableArea)
                   .OnDelete(DeleteBehavior.Restrict);


                entity.HasOne(d => d.ModosScializacion)
                    .WithMany(p => p.FormularioDevengacion)
                    .HasForeignKey(d => d.IdModosScializacion)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<FormulasRMU>(entity =>
            {
                entity.HasKey(e => e.IdFormulaRMU)
                    .HasName("PK_FormulasRMU");

                entity.ToTable("FormulasRMU");

                entity.Property(e => e.IdFormulaRMU).HasColumnName("IdFormulaRMU");

                entity.Property(e => e.Formula)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<FrecuenciaAplicacion>(entity =>
            {
                entity.HasKey(e => e.IdFrecuenciaAplicacion)
                    .HasName("PK_FrecuenciaAplicacion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<GastoRubro>(entity =>
            {
                entity.HasKey(e => e.IdGastoRubro)
                    .HasName("PK_GastoRubro");

                entity.HasIndex(e => e.IdEmpleadoImpuestoRenta)
                    .HasName("IX_GastoRubro_IdEmpleadoImpuestoRenta");

                entity.HasIndex(e => e.IdRubro)
                    .HasName("IX_GastoRubro_IdRubro");

                entity.Property(e => e.GastoProyectado).HasColumnType("decimal");

                entity.HasOne(d => d.EmpleadoImpuestoRenta)
                    .WithMany(p => p.GastoRubro)
                    .HasForeignKey(d => d.IdEmpleadoImpuestoRenta)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Rubro)
                    .WithMany(p => p.GastoRubro)
                    .HasForeignKey(d => d.IdRubro);
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.HasKey(e => e.IdGenero)
                    .HasName("PK_Genero");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);
            });
            modelBuilder.Entity<GestionPlanCapacitacion>(entity =>
            {
                entity.HasKey(e => e.IdGestionPlanCapacitacion)
                    .HasName("PK_GestionPlanCapacitacion");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(250)");

                entity.Property(e => e.Nombre).HasColumnType("varchar(250)");
            });
            modelBuilder.Entity<GrupoOcupacional>(entity =>
            {
                entity.HasKey(e => e.IdGrupoOcupacional)
                    .HasName("PK_GrupoOcupacional");

                entity.Property(e => e.TipoEscala)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Indicador>(entity =>
            {
                entity.HasKey(e => e.IdIndicador)
                    .HasName("PK_Indicador");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Ambito>(entity =>
            {
                entity.HasKey(e => e.IdAmbito)
                    .HasName("PK_Ambito");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<IndiceOcupacional>(entity =>
            {
                entity.HasKey(e => e.IdIndiceOcupacional)
                    .HasName("PK69");

                entity.Property(e => e.Nivel).HasColumnType("varchar(50)");

                entity.HasOne(d => d.Ambito)
                    .WithMany(p => p.IndiceOcupacional)
                    .HasForeignKey(d => d.IdAmbito)
                    .HasConstraintName("FK_IndiceOcupacional_Ambito");

                entity.HasOne(d => d.Dependencia)
                    .WithMany(p => p.IndiceOcupacional)
                    .HasForeignKey(d => d.IdDependencia)
                    .HasConstraintName("RefDependencia98");

                entity.HasOne(d => d.EscalaGrados)
                    .WithMany(p => p.IndiceOcupacional)
                    .HasForeignKey(d => d.IdEscalaGrados)
                    .HasConstraintName("RefEscalaGrados101");

                entity.HasOne(d => d.ManualPuesto)
                    .WithMany(p => p.IndiceOcupacional)
                    .HasForeignKey(d => d.IdManualPuesto)
                    .HasConstraintName("RefManualPuesto99");

                entity.HasOne(d => d.PartidaGeneral)
                    .WithMany(p => p.IndiceOcupacional)
                    .HasForeignKey(d => d.IdPartidaGeneral)
                    .HasConstraintName("FK_IndiceOcupacional_PartidaGeneral");

                entity.HasOne(d => d.RolPuesto)
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

                entity.HasOne(d => d.ActividadesEsenciales)
                    .WithMany(p => p.IndiceOcupacionalActividadesEsenciales)
                    .HasForeignKey(d => d.IdActividadesEsenciales)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefActividadesEsenciales341");

                entity.HasOne(d => d.IndiceOcupacional)
                    .WithMany(p => p.IndiceOcupacionalActividadesEsenciales)
                    .HasForeignKey(d => d.IdIndiceOcupacional)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefIndiceOcupacional344");
            });

            modelBuilder.Entity<IndiceOcupacionalAreaConocimiento>(entity =>
            {
                entity.HasKey(e => e.IdIndiceOcupacionalAreaConocimiento)
                    .HasName("PK_IndiceOcupacionalAreaConocimiento");

                entity.HasIndex(e => e.IdAreaConocimiento)
                    .HasName("IX_IndiceOcupacionalAreaConocimiento_IdAreaConocimiento");

                entity.HasIndex(e => e.IdIndiceOcupacional)
                    .HasName("IX_IndiceOcupacionalAreaConocimiento_IdIndiceOcupacional");

                entity.HasOne(d => d.AreaConocimiento)
                    .WithMany(p => p.IndiceOcupacionalAreaConocimiento)
                    .HasForeignKey(d => d.IdAreaConocimiento)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IndiceOcupacional)
                    .WithMany(p => p.IndiceOcupacionalAreaConocimiento)
                    .HasForeignKey(d => d.IdIndiceOcupacional)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<IndiceOcupacionalCapacitaciones>(entity =>
            {
                entity.HasKey(e => e.IdIndiceOcupacionalCapacitaciones)
                    .HasName("PK_IndiceOcupacionalCapacitaciones");

                entity.HasIndex(e => e.IdCapacitacion)
                    .HasName("IX_IndiceOcupacionalCapacitaciones_IdCapacitacion");

                entity.HasIndex(e => e.IdIndiceOcupacional)
                    .HasName("IX_IndiceOcupacionalCapacitaciones_IdIndiceOcupacional");

                entity.HasOne(d => d.Capacitacion)
                    .WithMany(p => p.IndiceOcupacionalCapacitaciones)
                    .HasForeignKey(d => d.IdCapacitacion)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IndiceOcupacional)
                    .WithMany(p => p.IndiceOcupacionalCapacitaciones)
                    .HasForeignKey(d => d.IdIndiceOcupacional)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<IndiceOcupacionalComportamientoObservable>(entity =>
            {
                entity.HasKey(e => e.IdIndiceOcupacionalComportamientoObservable)
                    .HasName("PK_IndiceOcupacionalComportamientoObservable");

                entity.HasIndex(e => e.IdComportamientoObservable)
                    .HasName("IX_IndiceOcupacionalComportamientoObservable_ComportamientoObservableId");

                entity.HasIndex(e => e.IdIndiceOcupacional)
                    .HasName("IX_IndiceOcupacionalComportamientoObservable_IdIndiceOcupacional");

                entity.HasOne(d => d.ComportamientoObservable)
                    .WithMany(p => p.IndiceOcupacionalComportamientoObservable)
                    .HasForeignKey(d => d.IdComportamientoObservable);

                entity.HasOne(d => d.IndiceOcupacional)
                    .WithMany(p => p.IndiceOcupacionalComportamientoObservable)
                    .HasForeignKey(d => d.IdIndiceOcupacional)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<IndiceOcupacionalConocimientosAdicionales>(entity =>
            {
                entity.HasKey(e => e.IdIndiceOcupacionalConocimientosAdicionales)
                    .HasName("PK_IndiceOcupacionalConocimientosAdicionales");

                entity.HasIndex(e => e.IdConocimientosAdicionales)
                    .HasName("IX_IndiceOcupacionalConocimientosAdicionales_IdConocimientosAdicionales");

                entity.HasIndex(e => e.IdIndiceOcupacional)
                    .HasName("IX_IndiceOcupacionalConocimientosAdicionales_IdIndiceOcupacional");

                entity.HasOne(d => d.ConocimientosAdicionales)
                    .WithMany(p => p.IndiceOcupacionalConocimientosAdicionales)
                    .HasForeignKey(d => d.IdConocimientosAdicionales)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IndiceOcupacional)
                    .WithMany(p => p.IndiceOcupacionalConocimientosAdicionales)
                    .HasForeignKey(d => d.IdIndiceOcupacional)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<IndiceOcupacionalEstudio>(entity =>
            {
                entity.HasKey(e => e.IdIndiceOcupacionalEstudio)
                    .HasName("PK_IndiceOcupacionalEstudio");

                entity.HasIndex(e => e.IdEstudio)
                    .HasName("IX_IndiceOcupacionalEstudio_IdEstudio");

                entity.HasIndex(e => e.IdIndiceOcupacional)
                    .HasName("IX_IndiceOcupacionalEstudio_IdIndiceOcupacional");

                entity.HasOne(d => d.Estudio)
                    .WithMany(p => p.IndiceOcupacionalEstudio)
                    .HasForeignKey(d => d.IdEstudio)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IndiceOcupacional)
                    .WithMany(p => p.IndiceOcupacionalEstudio)
                    .HasForeignKey(d => d.IdIndiceOcupacional)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<IndiceOcupacionalModalidadPartida>(entity =>
            {
                entity.HasKey(e => e.IdIndiceOcupacionalModalidadPartida)
                    .HasName("PK71");

                entity.Property(e => e.CodigoContrato).HasColumnType("varchar(50)");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.FechaFin).HasColumnType("date");

                entity.Property(e => e.NumeroPartidaIndividual).HasColumnType("varchar(50)");

                entity.Property(e => e.SalarioReal).HasColumnType("decimal");

                entity.HasOne(d => d.FondoFinanciamiento)
                    .WithMany(p => p.IndiceOcupacionalModalidadPartida)
                    .HasForeignKey(d => d.IdFondoFinanciamiento)
                    .HasConstraintName("RefFondoFinanciamiento104");

                entity.HasOne(d => d.IndiceOcupacional)
                    .WithMany(p => p.IndiceOcupacionalModalidadPartida)
                    .HasForeignKey(d => d.IdIndiceOcupacional)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefIndiceOcupacional103");

                entity.HasOne(d => d.ModalidadPartida)
                    .WithMany(p => p.IndiceOcupacionalModalidadPartida)
                    .HasForeignKey(d => d.IdModalidadPartida)
                    .HasConstraintName("FK_IndiceOcupacionalModalidadPartida_ModalidadPartida");

                entity.HasOne(d => d.TipoNombramiento)
                    .WithMany(p => p.IndiceOcupacionalModalidadPartida)
                    .HasForeignKey(d => d.IdTipoNombramiento)
                    .HasConstraintName("RefTipoNombramiento106");
            });

            modelBuilder.Entity<Induccion>(entity =>
            {
                entity.HasKey(e => e.IdInduccion)
                    .HasName("PK_Induccion");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Induccion)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Induccion_Empleado");
            });
            modelBuilder.Entity<InformeActividadViatico>(entity =>
            {
                entity.HasKey(e => e.IdInformeActividad)
                    .HasName("PK_InformeActividadViatico");

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.Observacion).HasColumnType("text");

                entity.HasOne(d => d.SolicitudViatico)
                    .WithMany(p => p.InformeActividadViatico)
                    .HasForeignKey(d => d.IdSolicitudViatico)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_InformeActividadViatico_SolicitudViatico");
            });

            modelBuilder.Entity<InformeUATH>(entity =>
            {
                entity.HasKey(e => e.IdInformeUATH)
                    .HasName("PK_InformeUATH");

                entity.ToTable("InformeUATH");

                entity.HasIndex(e => e.IdAdministracionTalentoHumano)
                    .HasName("IX_InformeUATH_IdAdministracionTalentoHumano");

                entity.HasIndex(e => e.IdManualPuestoOrigen)
                    .HasName("IX_InformeUATH_IdManualPuestoOrigen");

                entity.HasIndex(e => e.IdManualPuestoDestino)
                   .HasName("IX_InformeUATH_IdManualPuestoDestino");

                entity.Property(e => e.IdInformeUATH).HasColumnName("IdInformeUATH");

                entity.HasOne(d => d.AdministracionTalentoHumano)
                    .WithMany(p => p.InformeUATH)
                    .HasForeignKey(d => d.IdAdministracionTalentoHumano)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ManualPuestoOrigen)
                    .WithMany(p => p.InformeUATH1)
                    .HasForeignKey(d => d.IdManualPuestoOrigen)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ManualPuestoDestino)
                   .WithMany(p => p.InformeUATH)
                   .HasForeignKey(d => d.IdManualPuestoDestino)
                   .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<InformeViatico>(entity =>
            {
                entity.HasKey(e => e.IdInformeViatico)
                    .HasName("PK257");

                entity.HasIndex(e => e.IdSolicitudViatico)
                    .HasName("Ref251396");

                entity.Property(e => e.FechaLlegada).HasColumnType("date");

                entity.Property(e => e.FechaSalida).HasColumnType("date");

                entity.Property(e => e.HoraLlegada).HasColumnName("HoraLLegada");

                entity.Property(e => e.NombreTransporte).HasColumnType("varchar(250)");

                entity.Property(e => e.ValorEstimado).HasColumnType("decimal");

                entity.HasOne(d => d.CiudadDestino)
                    .WithMany(p => p.InformeViaticoCiudadDestino)
                    .HasForeignKey(d => d.IdCiudadDestino)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_InformeViatico_Ciudad");

                entity.HasOne(d => d.CiudadOrigen)
                    .WithMany(p => p.InformeViaticoCiudadOrigen)
                    .HasForeignKey(d => d.IdCiudadOrigen)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_InformeViatico_Ciudad1");

                entity.HasOne(d => d.SolicitudViatico)
                    .WithMany(p => p.InformeViatico)
                    .HasForeignKey(d => d.IdSolicitudViatico)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefItinerarioViatico396");

                entity.HasOne(d => d.TipoTransporte)
                    .WithMany(p => p.InformeViatico)
                    .HasForeignKey(d => d.IdTipoTransporte)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_InformeViatico_TipoTransporte");
            });

            modelBuilder.Entity<IngresoEgresoRMU>(entity =>
            {
                entity.HasKey(e => e.IdIngresoEgresoRMU)
                    .HasName("PK_IngresoEgresoRMU");

                entity.ToTable("IngresoEgresoRMU");

                entity.HasIndex(e => e.IdFormulaRMU)
                    .HasName("IX_IngresoEgresoRMU_FormulasRMUIdFormulaRMU");

                entity.Property(e => e.IdIngresoEgresoRMU).HasColumnName("IdIngresoEgresoRMU");

                entity.Property(e => e.CuentaContable)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);



                entity.Property(e => e.IdFormulaRMU).HasColumnName("IdFormulaRMU");

                entity.HasOne(d => d.FormulasRMU)
                    .WithMany(p => p.IngresoEgresoRMU)
                    .HasForeignKey(d => d.IdFormulaRMU);
            });

            modelBuilder.Entity<InstitucionFinanciera>(entity =>
            {
                entity.HasKey(e => e.IdInstitucionFinanciera)
                    .HasName("PK_InstitucionFinanciera");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.SPI)
                    .IsRequired();
            });

            modelBuilder.Entity<ItemViatico>(entity =>
            {
                entity.HasKey(e => e.IdItemViatico)
                    .HasName("PK_ItemViatico");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<ItinerarioViatico>(entity =>
            {
                entity.HasKey(e => e.IdItinerarioViatico)
                    .HasName("PK251");

                entity.HasIndex(e => e.IdSolicitudViatico)
                    .HasName("Ref77384");

                entity.HasIndex(e => e.IdTipoTransporte)
                    .HasName("Ref249385");

                entity.HasOne(d => d.CiudadDestino)
                    .WithMany(p => p.ItinerarioViaticoCiudadDestino)
                    .HasForeignKey(d => d.IdCiudadDestino)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ItinerarioViatico_Ciudad1");

                entity.HasOne(d => d.CiudadOrigen)
                    .WithMany(p => p.ItinerarioViaticoCiudadOrigen)
                    .HasForeignKey(d => d.IdCiudadOrigen)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ItinerarioViatico_Ciudad");

                entity.HasOne(d => d.SolicitudViatico)
                    .WithMany(p => p.ItinerarioViatico)
                    .HasForeignKey(d => d.IdSolicitudViatico)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefSolicitudViatico384");

                entity.HasOne(d => d.TipoTransporte)
                    .WithMany(p => p.ItinerarioViatico)
                    .HasForeignKey(d => d.IdTipoTransporte)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefTipoTransporte385");
            });


            modelBuilder.Entity<Liquidacion>(entity =>
            {
                entity.HasKey(e => e.IdLiquidacion)
                    .HasName("PK_Liquidacion");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_Liquidacion_IdEmpleado");

                entity.HasIndex(e => e.IdRubroLiquidacion)
                    .HasName("IX_Liquidacion_IdRubroLiquidacion");

                entity.Property(e => e.Valor).HasColumnType("decimal");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.Liquidacion)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.RubroLiquidacion)
                    .WithMany(p => p.Liquidacion)
                    .HasForeignKey(d => d.IdRubroLiquidacion)
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

                entity.HasOne(d => d.RelacionesInternasExternas)
                    .WithMany(p => p.ManualPuesto)
                    .HasForeignKey(d => d.IdRelacionesInternasExternas)
                    .HasConstraintName("FK_ManualPuesto_RelacionesInternasExternas");
            });


            modelBuilder.Entity<MaterialApoyo>(entity =>
            {
                entity.HasKey(e => e.IdMaterialApoyo)
                    .HasName("PK_MaterialApoyo");

                entity.HasIndex(e => e.IdFormularioDevengacion)
                    .HasName("IX_MaterialApoyo_FormularioDevengacionId");

                entity.HasOne(d => d.FormularioDevengacion)
                    .WithMany(p => p.MaterialApoyo)
                    .HasForeignKey(d => d.IdFormularioDevengacion)
                    .OnDelete(DeleteBehavior.Restrict);
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

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnType("varchar(250)");
            });



            modelBuilder.Entity<ModalidadPartida>(entity =>
            {
                entity.HasKey(e => e.IdModalidadPartida)
                    .HasName("PK62");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });


            modelBuilder.Entity<ModosScializacion>(entity =>
            {
                entity.HasKey(e => e.IdModosScializacion)
                    .HasName("PK_ModosScializacion");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Nacionalidad>(entity =>
            {
                entity.HasKey(e => e.IdNacionalidad)
                    .HasName("PK_Nacionalidad");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<NacionalidadIndigena>(entity =>
            {
                entity.HasKey(e => e.IdNacionalidadIndigena)
                    .HasName("PK_NacionalidadIndigena");

                entity.HasIndex(e => e.IdEtnia)
                    .HasName("IX_NacionalidadIndigena_IdEtnia");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Etnia)
                    .WithMany(p => p.NacionalidadIndigena)
                    .HasForeignKey(d => d.IdEtnia)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Nivel>(entity =>
            {
                entity.HasKey(e => e.IdNivel)
                    .HasName("PK_Nivel");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<NivelConocimiento>(entity =>
            {
                entity.HasKey(e => e.IdNivelConocimiento)
                    .HasName("PK_NivelConocimiento");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<NivelDesarrollo>(entity =>
            {
                entity.HasKey(e => e.IdNivelDesarrollo)
                    .HasName("PK_NivelDesarrollo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);
            });


            modelBuilder.Entity<OtroIngreso>(entity =>
            {
                entity.HasKey(e => e.IdOtroIngreso)
                    .HasName("PK_OtroIngreso");

                entity.Property(e => e.DescripcionOtros).HasColumnType("nchar(10)");

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
            modelBuilder.Entity<Presupuesto>(entity =>
            {
                entity.HasKey(e => e.IdPresupuesto)
                    .HasName("PK_Presupuesto");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.NumeroPartidaPresupuestaria).HasColumnType("varchar(50)");

                entity.HasOne(d => d.Sucursal)
                    .WithMany(p => p.Presupuesto)
                    .HasForeignKey(d => d.IdSucursal)
                    .HasConstraintName("FK_Presupuesto_Sucursal");
            });
            modelBuilder.Entity<PaquetesInformaticos>(entity =>
            {
                entity.HasKey(e => e.IdPaquetesInformaticos)
                    .HasName("PK_PaquetesInformaticos");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Parentesco>(entity =>
            {
                entity.HasKey(e => e.IdParentesco)
                    .HasName("PK_Parentesco");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);
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

                entity.HasOne(d => d.Ciudad)
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

                entity.HasIndex(e => e.IdIndiceOcupacional)
                    .HasName("Ref71424");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.HasOne(d => d.IndiceOcupacional)
                    .WithMany(p => p.PartidasFase)
                    .HasForeignKey(d => d.IdIndiceOcupacional)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PartidasFase_IndiceOcupacional");

                entity.HasOne(d => d.TipoConcurso)
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

                entity.HasIndex(e => e.IdNacionalidadIndigena);

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

                entity.Property(e => e.CorreoPrivado).IsRequired();

                entity.Property(e => e.CallePrincipal).HasMaxLength(150);
                entity.Property(e => e.CalleSecundaria).HasMaxLength(150);
                entity.Property(e => e.Referencia).HasMaxLength(150);
                entity.Property(e => e.Numero).HasMaxLength(50);

                entity.Property(e => e.Identificacion)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.LugarTrabajo)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TelefonoCasa)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.TelefonoPrivado)
                    .IsRequired()
                    .HasMaxLength(20);


                entity.HasOne(d => d.EstadoCivil)
                    .WithMany(p => p.Persona)
                    .HasForeignKey(d => d.IdEstadoCivil);

                entity.HasOne(d => d.Etnia)
                    .WithMany(p => p.Persona)
                    .HasForeignKey(d => d.IdEtnia);

                entity.HasOne(d => d.NacionalidadIndigena)
                    .WithMany(p => p.Persona)
                    .HasForeignKey(d => d.IdNacionalidadIndigena);

                entity.HasOne(d => d.Genero)
                    .WithMany(p => p.Persona)
                    .HasForeignKey(d => d.IdGenero);

                entity.HasOne(d => d.Nacionalidad)
                    .WithMany(p => p.Persona)
                    .HasForeignKey(d => d.IdNacionalidad);

                entity.HasOne(d => d.Sexo)
                    .WithMany(p => p.Persona)
                    .HasForeignKey(d => d.IdSexo);

                entity.HasOne(d => d.TipoIdentificacion)
                    .WithMany(p => p.Persona)
                    .HasForeignKey(d => d.IdTipoIdentificacion);

                entity.HasOne(d => d.TipoSangre)
                    .WithMany(p => p.Persona)
                    .HasForeignKey(d => d.IdTipoSangre);
            });

            modelBuilder.Entity<PersonaCapacitacion>(entity =>
            {
                entity.HasKey(e => e.IdPersonaCapacitacion)
                    .HasName("PK_PersonaCapacitacion");

                entity.HasIndex(e => e.IdCapacitacion)
                    .HasName("IX_PersonaCapacitacion_IdCapacitacion");

                entity.HasIndex(e => e.IdPersona)
                    .HasName("IX_PersonaCapacitacion_IdPersona");

                entity.Property(e => e.Observaciones)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Capacitacion)
                    .WithMany(p => p.PersonaCapacitacion)
                    .HasForeignKey(d => d.IdCapacitacion)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.PersonaCapacitacion)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PersonaDiscapacidad>(entity =>
            {
                entity.HasKey(e => e.IdPersonaDiscapacidad)
                    .HasName("PK_PersonaDiscapacidad");

                entity.HasIndex(e => e.IdPersona)
                    .HasName("IX_PersonaDiscapacidad_IdPersona");

                entity.HasIndex(e => e.IdTipoDiscapacidad)
                    .HasName("IX_PersonaDiscapacidad_IdTipoDiscapacidad");

                entity.Property(e => e.NumeroCarnet)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.PersonaDiscapacidad)
                    .HasForeignKey(d => d.IdPersona);

                entity.HasOne(d => d.TipoDiscapacidad)
                    .WithMany(p => p.PersonaDiscapacidad)
                    .HasForeignKey(d => d.IdTipoDiscapacidad);
            });

            modelBuilder.Entity<PersonaEnfermedad>(entity =>
            {
                entity.HasKey(e => e.IdPersonaEnfermedad)
                    .HasName("PK_PersonaEnfermedad");

                entity.HasIndex(e => e.IdPersona)
                    .HasName("IX_PersonaEnfermedad_IdPersona");

                entity.HasIndex(e => e.IdTipoEnfermedad)
                    .HasName("IX_PersonaEnfermedad_IdTipoEnfermedad");

                entity.Property(e => e.InstitucionEmite)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.PersonaEnfermedad)
                    .HasForeignKey(d => d.IdPersona);

                entity.HasOne(d => d.TipoEnfermedad)
                    .WithMany(p => p.PersonaEnfermedad)
                    .HasForeignKey(d => d.IdTipoEnfermedad);
            });

            modelBuilder.Entity<PersonaEstudio>(entity =>
            {
                entity.HasKey(e => e.IdPersonaEstudio)
                    .HasName("PK_PersonaEstudio");

                entity.HasIndex(e => e.IdPersona)
                    .HasName("IX_PersonaEstudio_IdPersona");

                entity.HasIndex(e => e.IdTitulo)
                    .HasName("IX_PersonaEstudio_IdTitulo");

                entity.Property(e => e.Observaciones)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.NoSenescyt)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.PersonaEstudio)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Restrict);

                //entity.HasOne(d => d.Titulo)
                //    .WithMany(p => p.PersonaEstudio)
                //    .HasForeignKey(d => d.IdTitulo)
                //    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PersonaPaquetesInformaticos>(entity =>
            {
                entity.HasKey(e => e.IdPersonaPaquetesInformaticos)
                    .HasName("PK_PersonaPaquetesInformaticos");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_PersonaPaquetesInformaticos_IdEmpleado");

                entity.HasIndex(e => e.IdPaquetesInformaticos)
                    .HasName("IX_PersonaPaquetesInformaticos_IdPaquetesInformaticos");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.PersonaPaquetesInformaticos)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.PaquetesInformaticos)
                    .WithMany(p => p.PersonaPaquetesInformaticos)
                    .HasForeignKey(d => d.IdPaquetesInformaticos)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<PlanCapacitacion>(entity =>
            {
                entity.HasKey(e => e.IdPlanCapacitacion)
                    .HasName("PK_PlanCapacitacion");

                entity.Property(e => e.AmbitoCapacitacion).HasColumnType("varchar(250)");

                entity.Property(e => e.ApellidoNombre).HasColumnType("varchar(250)");

                entity.Property(e => e.Cedula).HasColumnType("varchar(10)");

                entity.Property(e => e.NombreCiudad).HasColumnType("varchar(50)");

                entity.Property(e => e.ClasificacionTema).HasColumnType("varchar(250)");

                entity.Property(e => e.DenominacionPuesto).HasColumnType("varchar(50)");

                entity.Property(e => e.EstadoEvento).HasColumnType("varchar(254)");

                entity.Property(e => e.FechaCapacitacionPlanificada).HasColumnType("date");

                entity.Property(e => e.FechaFin).HasColumnType("date");

                entity.Property(e => e.FechaInicio).HasColumnType("date");

                entity.Property(e => e.GrupoOcupacional).HasColumnType("varchar(50)");

                entity.Property(e => e.Institucion).HasColumnType("varchar(50)");

                entity.Property(e => e.Modalidad).HasColumnType("varchar(50)");

                entity.Property(e => e.ModalidadLaboral).HasColumnType("varchar(50)");

                entity.Property(e => e.NivelDesconcentracion).HasColumnType("varchar(50)");

                entity.Property(e => e.NombreEvento).HasColumnType("varchar(1000)");

                entity.Property(e => e.NumeroPartidaPresupuestaria).HasColumnType("varchar(50)");

                entity.Property(e => e.Observacion).HasColumnType("text");

                entity.Property(e => e.Pais).HasColumnType("varchar(50)");

                entity.Property(e => e.PresupuestoIndividual).HasColumnType("decimal");

                entity.Property(e => e.ProductoFinal).HasColumnType("varchar(50)");

                entity.Property(e => e.Provincia).HasColumnType("varchar(50)");

                entity.Property(e => e.RegimenLaboral).HasColumnType("varchar(50)");

                entity.Property(e => e.Sexo).HasColumnType("varchar(50)");

                entity.Property(e => e.TemaCapacitacion).HasColumnType("text");

                entity.Property(e => e.TipoCapacitacion).HasColumnType("varchar(50)");

                entity.Property(e => e.TipoEvaluacion).HasColumnType("varchar(250)");

                entity.Property(e => e.TipoEvento).HasColumnType("varchar(250)");

                entity.Property(e => e.Ubicacion).HasColumnType("varchar(100)");

                entity.Property(e => e.UnidadAdministrativa).HasColumnType("varchar(50)");

                entity.Property(e => e.ValorReal).HasColumnType("decimal");

                entity.HasOne(d => d.Ciudad)
                    .WithMany(p => p.PlanCapacitacion)
                    .HasForeignKey(d => d.IdCiudad)
                    .HasConstraintName("FK_PlanCapacitacion_Ciudad");

                entity.HasOne(d => d.GestionPlanCapacitacion)
                    .WithMany(p => p.PlanCapacitacion)
                    .HasForeignKey(d => d.IdGestionPlanCapacitacion)
                    .HasConstraintName("FK_PlanCapacitacion_PlanCapacitacion1");

                entity.HasOne(d => d.ProveedorCapacitaciones)
                    .WithMany(p => p.PlanCapacitacion)
                    .HasForeignKey(d => d.IdProveedorCapacitaciones)
                    .HasConstraintName("FK_PlanCapacitacion_CapacitacionProveedor");
            });

            modelBuilder.Entity<PlanificacionHE>(entity =>
            {
                entity.HasKey(e => e.IdPlanificacionHE)
                    .HasName("PK_PlanificacionHE");

                entity.ToTable("PlanificacionHE");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_PlanificacionHE_IdEmpleado");

                entity.Property(e => e.IdPlanificacionHE).HasColumnName("IdPlanificacionHE");

                entity.Property(e => e.NoHoras).HasColumnType("decimal");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.PlanificacionHE)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Pregunta>(entity =>
            {
                entity.HasKey(e => e.IdPregunta)
                    .HasName("PK_Pregunta");

                entity.HasIndex(e => e.IdEvaluacionInduccion)
                    .HasName("IX_Pregunta_IdEvaluacionInduccion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.EvaluacionInducion)
                    .WithMany(p => p.Pregunta)
                    .HasForeignKey(d => d.IdEvaluacionInduccion)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<PreguntaEvaluacionEvento>(entity =>
            {
                entity.HasKey(e => e.IdPreguntaEvaluacionEvento)
                    .HasName("PK_PreguntaEvaluacionEvento");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(500)");
            });

            modelBuilder.Entity<PreguntaRespuesta>(entity =>
            {
                entity.HasKey(e => e.IdPreguntaRespuesta)
                    .HasName("PK_PreguntaRespuesta");

                entity.HasIndex(e => e.IdPregunta)
                    .HasName("IX_PreguntaRespuesta_IdPregunta");

                entity.HasIndex(e => e.IdRespuesta)
                    .HasName("IX_PreguntaRespuesta_IdRespuesta");

                entity.HasOne(d => d.Pregunta)
                    .WithMany(p => p.PreguntaRespuesta)
                    .HasForeignKey(d => d.IdPregunta)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Respuesta)
                    .WithMany(p => p.PreguntaRespuesta)
                    .HasForeignKey(d => d.IdRespuesta)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Proceso>(entity =>
            {
                entity.HasKey(e => e.IdProceso)
                    .HasName("PK_Proceso");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ProcesoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdProcesoDetalle)
                    .HasName("PK_ProcesoDetalle");

                entity.HasIndex(e => e.IdDependencia)
                    .HasName("IX_ProcesoDetalle_IdDependencia");

                entity.HasIndex(e => e.IdProceso)
                    .HasName("IX_ProcesoDetalle_IdProceso");

                entity.HasOne(d => d.Dependencia)
                    .WithMany(p => p.ProcesoDetalle)
                    .HasForeignKey(d => d.IdDependencia);

                entity.HasOne(d => d.Proceso)
                    .WithMany(p => p.ProcesoDetalle)
                    .HasForeignKey(d => d.IdProceso)
                    .OnDelete(DeleteBehavior.Restrict);
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

                entity.HasOne(d => d.Pais)
                    .WithMany(p => p.Provincia)
                    .HasForeignKey(d => d.IdPais)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Provisiones>(entity =>
            {
                entity.HasKey(e => e.IdProvisiones)
                    .HasName("PK_Provisiones");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_Provisiones_IdEmpleado");

                entity.HasIndex(e => e.IdTipoProvision)
                    .HasName("IX_Provisiones_IdTipoProvision");

                entity.Property(e => e.Valor).HasColumnType("decimal");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.Provisiones)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.TipoProvision)
                    .WithMany(p => p.Provisiones)
                    .HasForeignKey(d => d.IdTipoProvision)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<Quejas>(entity =>
            {
                entity.HasKey(e => e.IdQueja)
                    .HasName("PK_Quejas");

                entity.Property(e => e.Apellido).HasColumnType("varchar(100)");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(1000)");

                entity.Property(e => e.Nombre).HasColumnType("varchar(100)");

                entity.Property(e => e.NumeroFormulario).HasColumnType("varchar(50)");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.Quejas)
                    .HasForeignKey(d => d.IdEmpleado)
                    .HasConstraintName("FK_Quejas_Empleado");

                entity.HasOne(d => d.Eval001)
                    .WithMany(p => p.Quejas)
                    .HasForeignKey(d => d.IdEval001)
                    .HasConstraintName("FK_Quejas_Eval001");
            });

            modelBuilder.Entity<RealizaExamenInduccion>(entity =>
            {
                entity.HasKey(e => e.IdRealizaExamenInduccion)
                    .HasName("PK_RealizaExamenInduccion");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_RealizaExamenInduccion_IdEmpleado");

                entity.HasIndex(e => e.IdEvaluacionInduccion)
                    .HasName("IX_RealizaExamenInduccion_IdEvaluacionInduccion");

                entity.Property(e => e.Calificacion).HasColumnType("decimal");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.RealizaExamenInduccion)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.EvaluacionInducion)
                    .WithMany(p => p.RealizaExamenInduccion)
                    .HasForeignKey(d => d.IdEvaluacionInduccion)
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

            modelBuilder.Entity<RegimenLaboral>(entity =>
            {
                entity.HasKey(e => e.IdRegimenLaboral)
                    .HasName("PK_RegimenLaboral");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<RegistroEntradaSalida>(entity =>
            {
                entity.HasKey(e => e.IdRegistroEntradaSalida)
                    .HasName("PK_RegistroEntradaSalida");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_RegistroEntradaSalida_IdEmpleado");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.RegistroEntradaSalida)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RelacionLaboral>(entity =>
            {
                entity.HasKey(e => e.IdRelacionLaboral)
                    .HasName("PK_RelacionLaboral");

                entity.HasIndex(e => e.IdRegimenLaboral)
                    .HasName("IX_RelacionLaboral_IdRegimenLaboral");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.RegimenLaboral)
                    .WithMany(p => p.RelacionLaboral)
                    .HasForeignKey(d => d.IdRegimenLaboral)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RelacionesInternasExternas>(entity =>
            {
                entity.HasKey(e => e.IdRelacionesInternasExternas)
                    .HasName("PK_RelacionesInternasExternas");

                entity.Property(e => e.IdRelacionesInternasExternas).HasColumnName("IdRelacionesInternasExternas");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Relevancia>(entity =>
            {
                entity.HasKey(e => e.IdRelevancia)
                    .HasName("PK_Relevancia");

                entity.Property(e => e.ComportamientoObservable)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RequisitosNoCumple>(entity =>
            {
                entity.HasKey(e => e.IdRequisitosNoCumple)
                    .HasName("PK_RequisitosNoCumple");

                entity.HasIndex(e => e.IdAdministracionTalentoHumano)
                    .HasName("IX_RequisitosNoCumple_AdministracionTalentoHumanoId");

                entity.Property(e => e.Detalle)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.AdministracionTalentoHumano)
                    .WithMany(p => p.RequisitosNoCumple)
                    .HasForeignKey(d => d.IdAdministracionTalentoHumano)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ReliquidacionViatico>(entity =>
            {
                entity.HasKey(e => e.IdReliquidacionViatico)
                    .HasName("PK_RequlidacionViatico");

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.ValorEstimado).HasColumnType("decimal");

                entity.HasOne(d => d.Presupuesto)
                    .WithMany(p => p.ReliquidacionViatico)
                    .HasForeignKey(d => d.IdPresupuesto)
                    .HasConstraintName("FK_ReliquidacionViatico_Presupuesto");

                entity.HasOne(d => d.SolicitudViatico)
                    .WithMany(p => p.ReliquidacionViatico)
                    .HasForeignKey(d => d.IdSolicitudViatico)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ReliquidacionViatico_SolicitudViatico");
            });

            modelBuilder.Entity<Respuesta>(entity =>
            {
                entity.HasKey(e => e.IdRespuesta)
                    .HasName("PK_Respuesta");

                entity.Property(c => c.IdRespuesta).HasColumnName("IdRespuesta");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(400);
            });

            modelBuilder.Entity<RMU>(entity =>
            {
                entity.HasKey(e => e.IdRMU)
                    .HasName("PK_RMU");

                entity.ToTable("RMU");

                entity.HasIndex(e => e.IdEmpeladoIE)
                    .HasName("IX_RMU_IdEmpeladoIE");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_RMU_IdEmpleado");

                entity.HasIndex(e => e.IdTipoRMU)
                    .HasName("IX_RMU_IdTipoRMU");

                entity.Property(e => e.IdRMU).HasColumnName("IdRMU");

                entity.Property(e => e.IdEmpeladoIE).HasColumnName("IdEmpeladoIE");

                entity.Property(e => e.IdTipoRMU).HasColumnName("IdTipoRMU");

                entity.Property(e => e.Valor).HasColumnType("decimal");

                entity.HasOne(d => d.EmpleadoIE)
                    .WithMany(p => p.RMU)
                    .HasForeignKey(d => d.IdEmpeladoIE)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.RMU)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.TipoRMU)
                    .WithMany(p => p.RMU)
                    .HasForeignKey(d => d.IdTipoRMU)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RolPagoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdRolPagoDetalle)
                    .HasName("PK_RolPagoDetalle");

                entity.HasIndex(e => e.IdRolPagos)
                    .HasName("IX_RolPagoDetalle_IdRolPagos");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Valor).HasColumnType("decimal");

                entity.HasOne(d => d.RolPagos)
                    .WithMany(p => p.RolPagoDetalle)
                    .HasForeignKey(d => d.IdRolPagos)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RolPagos>(entity =>
            {
                entity.HasKey(e => e.IdRolPagos)
                    .HasName("PK_RolPagos");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_RolPagos_IdEmpleado");

                entity.Property(e => e.SaldoFinal).HasColumnType("decimal");

                entity.Property(e => e.SaldoInicial).HasColumnType("decimal");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.RolPagos)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RolPuesto>(entity =>
            {
                entity.HasKey(e => e.IdRolPuesto)
                    .HasName("PK_RolPuesto");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Rubro>(entity =>
            {
                entity.HasKey(e => e.IdRubro)
                    .HasName("PK_Rubro");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TasaPorcentualMaxima).HasColumnType("decimal");
            });

            modelBuilder.Entity<RubroLiquidacion>(entity =>
            {
                entity.HasKey(e => e.IdRubroLiquidacion)
                    .HasName("PK_RubroLiquidacion");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Sexo>(entity =>
            {
                entity.HasKey(e => e.IdSexo)
                    .HasName("PK_Sexo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<SituacionPropuesta>(entity =>
            {
                entity.HasKey(e => e.IdSituacionPropuesta)
                    .HasName("PK258");

                entity.HasIndex(e => e.IdDependencia)
                    .HasName("Ref51400");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(500)");

                entity.HasOne(d => d.Dependencia)
                    .WithMany(p => p.SituacionPropuesta)
                    .HasForeignKey(d => d.IdDependencia)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SituacionPropuesta_Dependencia");

                entity.HasOne(d => d.GrupoOcupacional)
                    .WithMany(p => p.SituacionPropuesta)
                    .HasForeignKey(d => d.IdGrupoOcupacional)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SituacionPropuesta_GrupoOcupacional");

                entity.HasOne(d => d.Proceso)
                    .WithMany(p => p.SituacionPropuesta)
                    .HasForeignKey(d => d.IdProceso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SituacionPropuesta_Proceso");

                entity.HasOne(d => d.RolPuesto)
                    .WithMany(p => p.SituacionPropuesta)
                    .HasForeignKey(d => d.IdRolPuesto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SituacionPropuesta_RolPuesto");
            });

            modelBuilder.Entity<SolicitudAcumulacionDecimos>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudAcumulacionDecimos)
                    .HasName("PK_SolicitudAcumulacionDecimos");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_SolicitudAcumulacionDecimos_IdEmpleado");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.SolicitudAcumulacionDecimos)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SolicitudAnticipo>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudAnticipo)
                    .HasName("PK_SolicitudAnticipo");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_SolicitudAnticipo_IdEmpleado");

                entity.HasIndex(e => e.IdEstado)
                    .HasName("IX_SolicitudAnticipo_IdEstado");

                entity.Property(e => e.CantidadCancelada).HasColumnType("decimal");

                entity.Property(e => e.CantidadSolicitada).HasColumnType("decimal");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.SolicitudAnticipo)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.SolicitudAnticipo)
                    .HasForeignKey(d => d.IdEstado);
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

                entity.HasOne(d => d.IdEmpleadoDirigidoANavigation)
                    .WithMany(p => p.SolicitudCertificadoPersonal)
                    .HasForeignKey(d => d.IdEmpleadoDirigidoA)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEmpleado151");

                entity.HasOne(d => d.IdEmpleadoSolicitanteNavigation)
                    .WithMany(p => p.SolicitudCertificadoPersonal1)
                    .HasForeignKey(d => d.IdEmpleadoSolicitante)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEmpleado150");

                entity.HasOne(d => d.IdTipoCertificadoNavigation)
                    .WithMany(p => p.SolicitudCertificadoPersonal)
                    .HasForeignKey(d => d.IdTipoCertificado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefTipoCertificado149");
            });

            modelBuilder.Entity<SolicitudHorasExtras>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudHorasExtras)
                    .HasName("PK_SolicitudHorasExtras");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_SolicitudHorasExtras_IdEmpleado");

                entity.Property(e => e.Observaciones)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.SolicitudHorasExtras)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SolicitudLiquidacionHaberes>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudLiquidacionHaberes)
                    .HasName("PK_SolicitudLiquidacionHaberes");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_SolicitudLiquidacionHaberes_IdEmpleado");

                entity.Property(e => e.TotalDecimoCuarto).HasColumnType("decimal");

                entity.Property(e => e.TotalDecimoTercero).HasColumnType("decimal");

                entity.Property(e => e.TotalDesahucio).HasColumnType("decimal");

                entity.Property(e => e.TotalDespidoIntempestivo).HasColumnType("decimal");

                entity.Property(e => e.TotalFondoReserva).HasColumnType("decimal");

                entity.Property(e => e.TotalVacaciones).HasColumnType("decimal");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.SolicitudLiquidacionHaberes)
                    .HasForeignKey(d => d.IdEmpleado);
            });

            modelBuilder.Entity<SolicitudModificacionFichaEmpleado>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudModificacionFichaEmpleado)
                    .HasName("PK_SolicitudModificacionFichaEmpleado");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_SolicitudModificacionFichaEmpleado_IdEmpleado");

                entity.HasIndex(e => e.IdEstado)
                    .HasName("IX_SolicitudModificacionFichaEmpleado_IdEstado");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.SolicitudModificacionFichaEmpleado)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.SolicitudModificacionFichaEmpleado)
                    .HasForeignKey(d => d.IdEstado);
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


                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.SolicitudPermiso)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SolicitudPermiso_Empleado");

                entity.HasOne(d => d.TipoPermiso)
                    .WithMany(p => p.SolicitudPermiso)
                    .HasForeignKey(d => d.IdTipoPermiso)
                    .HasConstraintName("FK_SolicitudPermiso_TipoPermiso");
            });

            modelBuilder.Entity<SolicitudPlanificacionVacaciones>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudPlanificacionVacaciones)
                    .HasName("PK_SolicitudPlanificacionVacaciones");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_SolicitudPlanificacionVacaciones_IdEmpleado");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.SolicitudPlanificacionVacaciones)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<SolicitudTipoViatico>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudTipoViatico)
                    .HasName("PK_SolicitudTipoViatico");

                entity.HasOne(d => d.SolicitudViatico)
                    .WithMany(p => p.SolicitudTipoViatico)
                    .HasForeignKey(d => d.IdSolicitudViatico)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SolicitudTipoViatico_SolicitudViatico");

                entity.HasOne(d => d.TipoViatico)
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

                entity.Property(e => e.RazonNoPlanificado).HasMaxLength(700);
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

                entity.HasOne(d => d.Ciudad)
                    .WithMany(p => p.SolicitudViatico)
                    .HasForeignKey(d => d.IdCiudad)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SolicitudViatico_Ciudad");

                entity.HasOne(d => d.ConfiguracionViatico)
                    .WithMany(p => p.SolicitudViatico)
                    .HasForeignKey(d => d.IdConfiguracionViatico)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SolicitudViatico_ConfiguracionViatico");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.SolicitudViatico)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SolicitudViatico_Empleado");

                entity.HasOne(d => d.FondoFinanciamiento)
                    .WithMany(p => p.SolicitudViatico)
                    .HasForeignKey(d => d.IdFondoFinanciamiento)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SolicitudViatico_FondoFinanciamiento");
            });


            modelBuilder.Entity<Sucursal>(entity =>
            {
                entity.HasKey(e => e.IdSucursal)
                    .HasName("PK_Sucursal");

                entity.HasIndex(e => e.IdCiudad)
                    .HasName("IX_Sucursal_IdCiudad");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Ciudad)
                    .WithMany(p => p.Sucursal)
                    .HasForeignKey(d => d.IdCiudad)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TipoAccionPersonal>(entity =>
            {
                entity.HasKey(e => e.IdTipoAccionPersonal)
                    .HasName("PK247_1");

                entity.Property(e => e.Descripcion).HasMaxLength(300);

                entity.Property(e => e.EsResponsableTH).HasColumnName("EsResponsableTH");

                entity.Property(e => e.Matriz).HasMaxLength(100);

                entity.Property(e => e.NDiasMaximo).HasColumnName("NDiasMaximo");

                entity.Property(e => e.NDiasMinimo).HasColumnName("NDiasMinimo");

                entity.Property(e => e.NHorasMaximo).HasColumnName("NHorasMaximo");

                entity.Property(e => e.NHorasMinimo).HasColumnName("NHorasMinimo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);
            });


            modelBuilder.Entity<TipoCertificado>(entity =>
            {
                entity.HasKey(e => e.IdTipoCertificado)
                    .HasName("PK_TipoCertificado");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
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
                    .HasName("PK_TipoDiscapacidad");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TipoEnfermedad>(entity =>
            {
                entity.HasKey(e => e.IdTipoEnfermedad)
                    .HasName("PK_TipoEnfermedad");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<TipoIdentificacion>(entity =>
            {
                entity.HasKey(e => e.IdTipoIdentificacion)
                    .HasName("PK_TipoIdentificacion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<TipoMovimientoInterno>(entity =>
            {
                entity.HasKey(e => e.IdTipoMovimientoInterno)
                    .HasName("PK_TipoMovimientoInterno");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TipoNombramiento>(entity =>
            {
                entity.HasKey(e => e.IdTipoNombramiento)
                    .HasName("PK_TipoNombramiento");

                entity.HasIndex(e => e.IdRelacionLaboral)
                    .HasName("IX_TipoNombramiento_IdRelacionLaboral");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.RelacionLaboral)
                    .WithMany(p => p.TipoNombramiento)
                    .HasForeignKey(d => d.IdRelacionLaboral)
                    .OnDelete(DeleteBehavior.Restrict);
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
                    .HasName("PK_TipoProvision");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<TipoRMU>(entity =>
            {
                entity.HasKey(e => e.IdTipoRMU)
                    .HasName("PK_TipoRMU");

                entity.ToTable("TipoRMU");

                entity.Property(e => e.IdTipoRMU).HasColumnName("IdTipoRMU");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<TipoSangre>(entity =>
            {
                entity.HasKey(e => e.IdTipoSangre)
                    .HasName("PK_TipoSangre");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<TipoTransporte>(entity =>
            {
                entity.HasKey(e => e.IdTipoTransporte)
                    .HasName("PK_TipoTransporte");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<TipoViatico>(entity =>
            {
                entity.HasKey(e => e.IdTipoViatico)
                    .HasName("PK_TipoViatico");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Titulo>(entity =>
            {
                entity.HasKey(e => e.IdTitulo)
                    .HasName("PK_Titulo");

                entity.HasIndex(e => e.IdEstudio)
                    .HasName("IX_Titulo_IdEstudio");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Estudio)
                    .WithMany(p => p.Titulo)
                    .HasForeignKey(d => d.IdEstudio)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TrabajoEquipoIniciativaLiderazgo>(entity =>
            {
                entity.HasKey(e => e.IdTrabajoEquipoIniciativaLiderazgo)
                    .HasName("PK_TrabajoEquipoIniciativaLiderazgo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);
            });


            modelBuilder.Entity<TrayectoriaLaboral>(entity =>
            {
                entity.HasKey(e => e.IdTrayectoriaLaboral)
                    .HasName("PK_TrayectoriaLaboral");

                entity.HasIndex(e => e.IdPersona)
                    .HasName("IX_TrayectoriaLaboral_IdPersona");

                entity.Property(e => e.Empresa).HasMaxLength(100);

                entity.Property(e => e.PuestoTrabajo).HasMaxLength(250);

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.TrayectoriaLaboral)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<ValidacionInmediatoSuperior>(entity =>
            {
                entity.HasKey(e => e.IdValidacionJefe)
                    .HasName("PK_ValidacionInmediatoSuperior");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_ValidacionInmediatoSuperior_IdEmpleado");

                entity.HasIndex(e => e.IdFormularioAnalisisOcupacional)
                    .HasName("IX_ValidacionInmediatoSuperior_IdFormularioAnalisisOcupacional");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.ValidacionInmediatoSuperior)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.FormularioAnalisisOcupacional)
                    .WithMany(p => p.ValidacionInmediatoSuperior)
                    .HasForeignKey(d => d.IdFormularioAnalisisOcupacional)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{

        //    foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        //    {
        //        relationship.DeleteBehavior = DeleteBehavior.Restrict;
        //    }

        //    base.OnModelCreating(builder);

        //    // Customize the ASP.NET Identity model and override the defaults if needed.
        //    // For example, you can rename the ASP.NET Identity table names and more.
        //    // Add your customizations after calling base.OnModelCreating(builder);
        //}


        public void EnsureSeedData()
        {

        }


    }

}






