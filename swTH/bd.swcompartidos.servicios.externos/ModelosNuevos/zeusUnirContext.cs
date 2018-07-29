using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class zeusUnirContext : DbContext
    {
        public virtual DbSet<AccionPersonal> AccionPersonal { get; set; }
        public virtual DbSet<ActivarPersonalTalentoHumano> ActivarPersonalTalentoHumano { get; set; }
        public virtual DbSet<ActividadesAnalisisOcupacional> ActividadesAnalisisOcupacional { get; set; }
        public virtual DbSet<ActividadesEsenciales> ActividadesEsenciales { get; set; }
        public virtual DbSet<ActividadesGestionCambio> ActividadesGestionCambio { get; set; }
        public virtual DbSet<ActivoFijo> ActivoFijo { get; set; }
        public virtual DbSet<AdministracionTalentoHumano> AdministracionTalentoHumano { get; set; }
        public virtual DbSet<AjusteInventarioArticulos> AjusteInventarioArticulos { get; set; }
        public virtual DbSet<AltaActivoFijo> AltaActivoFijo { get; set; }
        public virtual DbSet<AltaActivoFijoDetalle> AltaActivoFijoDetalle { get; set; }
        public virtual DbSet<Ambito> Ambito { get; set; }
        public virtual DbSet<AntecedentesFamiliares> AntecedentesFamiliares { get; set; }
        public virtual DbSet<AntecedentesLaborales> AntecedentesLaborales { get; set; }
        public virtual DbSet<AprobacionAccionPersonal> AprobacionAccionPersonal { get; set; }
        public virtual DbSet<AprobacionViatico> AprobacionViatico { get; set; }
        public virtual DbSet<AreaConocimiento> AreaConocimiento { get; set; }
        public virtual DbSet<Articulo> Articulo { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<BajaActivoFijo> BajaActivoFijo { get; set; }
        public virtual DbSet<BajaActivoFijoDetalle> BajaActivoFijoDetalle { get; set; }
        public virtual DbSet<Bodega> Bodega { get; set; }
        public virtual DbSet<BrigadaSso> BrigadaSso { get; set; }
        public virtual DbSet<BrigadaSsorol> BrigadaSsorol { get; set; }
        public virtual DbSet<CabeceraNomina> CabeceraNomina { get; set; }
        public virtual DbSet<CalculoNomina> CalculoNomina { get; set; }
        public virtual DbSet<Calificacion> Calificacion { get; set; }
        public virtual DbSet<Candidato> Candidato { get; set; }
        public virtual DbSet<CandidatoConcurso> CandidatoConcurso { get; set; }
        public virtual DbSet<CandidatoEstudio> CandidatoEstudio { get; set; }
        public virtual DbSet<CandidatoTrayectoriaLaboral> CandidatoTrayectoriaLaboral { get; set; }
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
        public virtual DbSet<CategoriaActivoFijo> CategoriaActivoFijo { get; set; }
        public virtual DbSet<CeseFuncion> CeseFuncion { get; set; }
        public virtual DbSet<Ciudad> Ciudad { get; set; }
        public virtual DbSet<ClaseActivoFijo> ClaseActivoFijo { get; set; }
        public virtual DbSet<ClaseArticulo> ClaseArticulo { get; set; }
        public virtual DbSet<CodigoActivoFijo> CodigoActivoFijo { get; set; }
        public virtual DbSet<CompaniaSeguro> CompaniaSeguro { get; set; }
        public virtual DbSet<Complain> Complain { get; set; }
        public virtual DbSet<ComponenteActivoFijo> ComponenteActivoFijo { get; set; }
        public virtual DbSet<ComportamientoObservable> ComportamientoObservable { get; set; }
        public virtual DbSet<ConceptoConjuntoNomina> ConceptoConjuntoNomina { get; set; }
        public virtual DbSet<ConceptoNomina> ConceptoNomina { get; set; }
        public virtual DbSet<ConfiguracionContabilidad> ConfiguracionContabilidad { get; set; }
        public virtual DbSet<ConfiguracionFeriados> ConfiguracionFeriados { get; set; }
        public virtual DbSet<ConfiguracionViatico> ConfiguracionViatico { get; set; }
        public virtual DbSet<ConfirmacionLectura> ConfirmacionLectura { get; set; }
        public virtual DbSet<ConjuntoNomina> ConjuntoNomina { get; set; }
        public virtual DbSet<ConocimientosAdicionales> ConocimientosAdicionales { get; set; }
        public virtual DbSet<ConstanteNomina> ConstanteNomina { get; set; }
        public virtual DbSet<DatosBancarios> DatosBancarios { get; set; }
        public virtual DbSet<DeclaracionPatrimonioPersonal> DeclaracionPatrimonioPersonal { get; set; }
        public virtual DbSet<DenominacionCompetencia> DenominacionCompetencia { get; set; }
        public virtual DbSet<Dependencia> Dependencia { get; set; }
        public virtual DbSet<DependenciaDocumento> DependenciaDocumento { get; set; }
        public virtual DbSet<DepreciacionActivoFijo> DepreciacionActivoFijo { get; set; }
        public virtual DbSet<Destreza> Destreza { get; set; }
        public virtual DbSet<DetalleEvaluacionEvento> DetalleEvaluacionEvento { get; set; }
        public virtual DbSet<DetalleExamenInduccion> DetalleExamenInduccion { get; set; }
        public virtual DbSet<DetalleNomina> DetalleNomina { get; set; }
        public virtual DbSet<DetallePresupuesto> DetallePresupuesto { get; set; }
        public virtual DbSet<DetalleReliquidacionViatico> DetalleReliquidacionViatico { get; set; }
        public virtual DbSet<DiscapacidadSustituto> DiscapacidadSustituto { get; set; }
        public virtual DbSet<DocumentoActivoFijo> DocumentoActivoFijo { get; set; }
        public virtual DbSet<DocumentoInformacionInstitucional> DocumentoInformacionInstitucional { get; set; }
        public virtual DbSet<DocumentosCargados> DocumentosCargados { get; set; }
        public virtual DbSet<DocumentosIngreso> DocumentosIngreso { get; set; }
        public virtual DbSet<DocumentosIngresoEmpleado> DocumentosIngresoEmpleado { get; set; }
        public virtual DbSet<Ejemplo> Ejemplo { get; set; }
        public virtual DbSet<Ejemplo1> Ejemplo1 { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
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
        public virtual DbSet<Estudio> Estudio { get; set; }
        public virtual DbSet<Etnia> Etnia { get; set; }
        public virtual DbSet<Eval001> Eval001 { get; set; }
        public virtual DbSet<EvaluacionActividadesPuestoTrabajo> EvaluacionActividadesPuestoTrabajo { get; set; }
        public virtual DbSet<EvaluacionCompetenciasTecnicasPuesto> EvaluacionCompetenciasTecnicasPuesto { get; set; }
        public virtual DbSet<EvaluacionCompetenciasUniversales> EvaluacionCompetenciasUniversales { get; set; }
        public virtual DbSet<EvaluacionConocimiento> EvaluacionConocimiento { get; set; }
        public virtual DbSet<EvaluacionEvento> EvaluacionEvento { get; set; }
        public virtual DbSet<EvaluacionInducion> EvaluacionInducion { get; set; }
        public virtual DbSet<EvaluacionTrabajoEquipoIniciativaLiderazgo> EvaluacionTrabajoEquipoIniciativaLiderazgo { get; set; }
        public virtual DbSet<Evaluador> Evaluador { get; set; }
        public virtual DbSet<ExamenComplementario> ExamenComplementario { get; set; }
        public virtual DbSet<Exepciones> Exepciones { get; set; }
        public virtual DbSet<ExperienciaLaboralRequerida> ExperienciaLaboralRequerida { get; set; }
        public virtual DbSet<Factor> Factor { get; set; }
        public virtual DbSet<FacturaActivoFijo> FacturaActivoFijo { get; set; }
        public virtual DbSet<FacturaViatico> FacturaViatico { get; set; }
        public virtual DbSet<FichaMedica> FichaMedica { get; set; }
        public virtual DbSet<FlujoAprobacion> FlujoAprobacion { get; set; }
        public virtual DbSet<FondoFinanciamiento> FondoFinanciamiento { get; set; }
        public virtual DbSet<FormularioAnalisisOcupacional> FormularioAnalisisOcupacional { get; set; }
        public virtual DbSet<FormularioCapacitacion> FormularioCapacitacion { get; set; }
        public virtual DbSet<FormularioDevengacion> FormularioDevengacion { get; set; }
        public virtual DbSet<FormulasRmu> FormulasRmu { get; set; }
        public virtual DbSet<FrecuenciaAplicacion> FrecuenciaAplicacion { get; set; }
        public virtual DbSet<FuncionNomina> FuncionNomina { get; set; }
        public virtual DbSet<GastoPersonal> GastoPersonal { get; set; }
        public virtual DbSet<GastoRubro> GastoRubro { get; set; }
        public virtual DbSet<GeneralCapacitacion> GeneralCapacitacion { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<GestionPlanCapacitacion> GestionPlanCapacitacion { get; set; }
        public virtual DbSet<GrupoOcupacional> GrupoOcupacional { get; set; }
        public virtual DbSet<HorasExtrasNomina> HorasExtrasNomina { get; set; }
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
        public virtual DbSet<InformeActividadViatico> InformeActividadViatico { get; set; }
        public virtual DbSet<InformeUath> InformeUath { get; set; }
        public virtual DbSet<InformeViatico> InformeViatico { get; set; }
        public virtual DbSet<IngresoEgresoRmu> IngresoEgresoRmu { get; set; }
        public virtual DbSet<InstitucionFinanciera> InstitucionFinanciera { get; set; }
        public virtual DbSet<Institution> Institution { get; set; }
        public virtual DbSet<InstruccionFormal> InstruccionFormal { get; set; }
        public virtual DbSet<InventarioActivoFijo> InventarioActivoFijo { get; set; }
        public virtual DbSet<InventarioActivoFijoDetalle> InventarioActivoFijoDetalle { get; set; }
        public virtual DbSet<InventarioArticulos> InventarioArticulos { get; set; }
        public virtual DbSet<ItemViatico> ItemViatico { get; set; }
        public virtual DbSet<ItinerarioViatico> ItinerarioViatico { get; set; }
        public virtual DbSet<LavadoActivoEmpleado> LavadoActivoEmpleado { get; set; }
        public virtual DbSet<LavadoActivoItem> LavadoActivoItem { get; set; }
        public virtual DbSet<LibroActivoFijo> LibroActivoFijo { get; set; }
        public virtual DbSet<Libros> Libros { get; set; }
        public virtual DbSet<LineaServicio> LineaServicio { get; set; }
        public virtual DbSet<Liquidacion> Liquidacion { get; set; }
        public virtual DbSet<MaestroArticuloSucursal> MaestroArticuloSucursal { get; set; }
        public virtual DbSet<MantenimientoActivoFijo> MantenimientoActivoFijo { get; set; }
        public virtual DbSet<ManualPuesto> ManualPuesto { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<MaterialApoyo> MaterialApoyo { get; set; }
        public virtual DbSet<MaterialInduccion> MaterialInduccion { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<ModalidadPartida> ModalidadPartida { get; set; }
        public virtual DbSet<Modelo> Modelo { get; set; }
        public virtual DbSet<ModosScializacion> ModosScializacion { get; set; }
        public virtual DbSet<MotivoAlta> MotivoAlta { get; set; }
        public virtual DbSet<MotivoAsiento> MotivoAsiento { get; set; }
        public virtual DbSet<MotivoBaja> MotivoBaja { get; set; }
        public virtual DbSet<MotivoRecepcionArticulos> MotivoRecepcionArticulos { get; set; }
        public virtual DbSet<MotivoSalidaArticulos> MotivoSalidaArticulos { get; set; }
        public virtual DbSet<MotivoTransferencia> MotivoTransferencia { get; set; }
        public virtual DbSet<MotivoTraslado> MotivoTraslado { get; set; }
        public virtual DbSet<MovilizacionActivoFijo> MovilizacionActivoFijo { get; set; }
        public virtual DbSet<MovilizacionActivoFijoDetalle> MovilizacionActivoFijoDetalle { get; set; }
        public virtual DbSet<Nacionalidad> Nacionalidad { get; set; }
        public virtual DbSet<NacionalidadIndigena> NacionalidadIndigena { get; set; }
        public virtual DbSet<Nivel> Nivel { get; set; }
        public virtual DbSet<NivelConocimiento> NivelConocimiento { get; set; }
        public virtual DbSet<NivelDesarrollo> NivelDesarrollo { get; set; }
        public virtual DbSet<Noticia> Noticia { get; set; }
        public virtual DbSet<OrdenCompra> OrdenCompra { get; set; }
        public virtual DbSet<OrdenCompraDetalles> OrdenCompraDetalles { get; set; }
        public virtual DbSet<OtroIngreso> OtroIngreso { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<PaquetesInformaticos> PaquetesInformaticos { get; set; }
        public virtual DbSet<ParametrosGenerales> ParametrosGenerales { get; set; }
        public virtual DbSet<Parentesco> Parentesco { get; set; }
        public virtual DbSet<Parroquia> Parroquia { get; set; }
        public virtual DbSet<PartidaGeneral> PartidaGeneral { get; set; }
        public virtual DbSet<PartidasFase> PartidasFase { get; set; }
        public virtual DbSet<PeriodoNomina> PeriodoNomina { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<PersonaCapacitacion> PersonaCapacitacion { get; set; }
        public virtual DbSet<PersonaDiscapacidad> PersonaDiscapacidad { get; set; }
        public virtual DbSet<PersonaEnfermedad> PersonaEnfermedad { get; set; }
        public virtual DbSet<PersonaEstudio> PersonaEstudio { get; set; }
        public virtual DbSet<PersonaPaquetesInformaticos> PersonaPaquetesInformaticos { get; set; }
        public virtual DbSet<PersonaSustituto> PersonaSustituto { get; set; }
        public virtual DbSet<PieFirma> PieFirma { get; set; }
        public virtual DbSet<PlanCapacitacion> PlanCapacitacion { get; set; }
        public virtual DbSet<PlanificacionHe> PlanificacionHe { get; set; }
        public virtual DbSet<PolizaSeguroActivoFijo> PolizaSeguroActivoFijo { get; set; }
        public virtual DbSet<Pregunta> Pregunta { get; set; }
        public virtual DbSet<PreguntaEvaluacionEvento> PreguntaEvaluacionEvento { get; set; }
        public virtual DbSet<PreguntaRespuesta> PreguntaRespuesta { get; set; }
        public virtual DbSet<Presupuesto> Presupuesto { get; set; }
        public virtual DbSet<Proceso> Proceso { get; set; }
        public virtual DbSet<ProcesoJudicialActivoFijo> ProcesoJudicialActivoFijo { get; set; }
        public virtual DbSet<ProcesoNomina> ProcesoNomina { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<Provincia> Provincia { get; set; }
        public virtual DbSet<Provisiones> Provisiones { get; set; }
        public virtual DbSet<Quejas> Quejas { get; set; }
        public virtual DbSet<Ramo> Ramo { get; set; }
        public virtual DbSet<RealizaExamenInduccion> RealizaExamenInduccion { get; set; }
        public virtual DbSet<RecepcionActivoFijo> RecepcionActivoFijo { get; set; }
        public virtual DbSet<RecepcionActivoFijoDetalle> RecepcionActivoFijoDetalle { get; set; }
        public virtual DbSet<RegimenLaboral> RegimenLaboral { get; set; }
        public virtual DbSet<RegistroEntradaSalida> RegistroEntradaSalida { get; set; }
        public virtual DbSet<RelacionLaboral> RelacionLaboral { get; set; }
        public virtual DbSet<RelacionesInternasExternas> RelacionesInternasExternas { get; set; }
        public virtual DbSet<Relevancia> Relevancia { get; set; }
        public virtual DbSet<ReliquidacionViatico> ReliquidacionViatico { get; set; }
        public virtual DbSet<ReportadoNomina> ReportadoNomina { get; set; }
        public virtual DbSet<RequerimientoArticulos> RequerimientoArticulos { get; set; }
        public virtual DbSet<RequerimientosArticulosDetalles> RequerimientosArticulosDetalles { get; set; }
        public virtual DbSet<RequisitosNoCumple> RequisitosNoCumple { get; set; }
        public virtual DbSet<Respuesta> Respuesta { get; set; }
        public virtual DbSet<RevalorizacionActivoFijo> RevalorizacionActivoFijo { get; set; }
        public virtual DbSet<Rmu> Rmu { get; set; }
        public virtual DbSet<RolPagoDetalle> RolPagoDetalle { get; set; }
        public virtual DbSet<RolPagos> RolPagos { get; set; }
        public virtual DbSet<RolPuesto> RolPuesto { get; set; }
        public virtual DbSet<Rubro> Rubro { get; set; }
        public virtual DbSet<RubroLiquidacion> RubroLiquidacion { get; set; }
        public virtual DbSet<SalidaArticulos> SalidaArticulos { get; set; }
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
        public virtual DbSet<SolicitudTipoViatico> SolicitudTipoViatico { get; set; }
        public virtual DbSet<SolicitudVacaciones> SolicitudVacaciones { get; set; }
        public virtual DbSet<SolicitudViatico> SolicitudViatico { get; set; }
        public virtual DbSet<SriDetalle> SriDetalle { get; set; }
        public virtual DbSet<SriNomina> SriNomina { get; set; }
        public virtual DbSet<SubClaseActivoFijo> SubClaseActivoFijo { get; set; }
        public virtual DbSet<SubClaseArticulo> SubClaseArticulo { get; set; }
        public virtual DbSet<SubcategoryInstitution> SubcategoryInstitution { get; set; }
        public virtual DbSet<Subramo> Subramo { get; set; }
        public virtual DbSet<Sucursal> Sucursal { get; set; }
        public virtual DbSet<TeconceptoNomina> TeconceptoNomina { get; set; }
        public virtual DbSet<Temporal> Temporal { get; set; }
        public virtual DbSet<TipoAccionPersonal> TipoAccionPersonal { get; set; }
        public virtual DbSet<TipoActivoFijo> TipoActivoFijo { get; set; }
        public virtual DbSet<TipoArticulo> TipoArticulo { get; set; }
        public virtual DbSet<TipoCalificacion> TipoCalificacion { get; set; }
        public virtual DbSet<TipoCertificado> TipoCertificado { get; set; }
        public virtual DbSet<TipoCesacionFuncion> TipoCesacionFuncion { get; set; }
        public virtual DbSet<TipoConcurso> TipoConcurso { get; set; }
        public virtual DbSet<TipoConjuntoNomina> TipoConjuntoNomina { get; set; }
        public virtual DbSet<TipoDeGastoPersonal> TipoDeGastoPersonal { get; set; }
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
        public virtual DbSet<TipoUtilizacionAlta> TipoUtilizacionAlta { get; set; }
        public virtual DbSet<TipoViatico> TipoViatico { get; set; }
        public virtual DbSet<Titulo> Titulo { get; set; }
        public virtual DbSet<TrabajoEquipoIniciativaLiderazgo> TrabajoEquipoIniciativaLiderazgo { get; set; }
        public virtual DbSet<TransferenciaActivoFijo> TransferenciaActivoFijo { get; set; }
        public virtual DbSet<TransferenciaActivoFijoDetalle> TransferenciaActivoFijoDetalle { get; set; }
        public virtual DbSet<TrayectoriaLaboral> TrayectoriaLaboral { get; set; }
        public virtual DbSet<UbicacionActivoFijo> UbicacionActivoFijo { get; set; }
        public virtual DbSet<UnidadMedida> UnidadMedida { get; set; }
        public virtual DbSet<VacacionRelacionLaboral> VacacionRelacionLaboral { get; set; }
        public virtual DbSet<VacacionesEmpleado> VacacionesEmpleado { get; set; }
        public virtual DbSet<ValidacionInmediatoSuperior> ValidacionInmediatoSuperior { get; set; }

        // Unable to generate entity type for table 'dbo.COMMENT'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.dbo.EstadoSolicitudVacacion'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tmpConjuntos'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tmpSumaConceptos'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.SUBCATEGORY'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.CATEGORY'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=developmentds.eastus.cloudapp.azure.com;Database=zeusUnir;User Id=sa;password=Digital_2018;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccionPersonal>(entity =>
            {
                entity.HasKey(e => e.IdAccionPersonal)
                    .HasName("PK188");

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

            modelBuilder.Entity<ActivarPersonalTalentoHumano>(entity =>
            {
                entity.HasKey(e => e.IdActivarPersonalTalentoHumano)
                    .HasName("PK_ActivarPersonalTalentoHumano");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.IdDependenciaNavigation)
                    .WithMany(p => p.ActivarPersonalTalentoHumano)
                    .HasForeignKey(d => d.IdDependencia)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ActivarPersonalTalentoHumano_Dependencia");
            });

            modelBuilder.Entity<ActividadesAnalisisOcupacional>(entity =>
            {
                entity.HasKey(e => e.IdActividadesAnalisisOcupacional)
                    .HasName("PK192");

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

                entity.Property(e => e.Descripcion).HasMaxLength(500);
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

                entity.HasOne(d => d.IdDependenciaNavigation)
                    .WithMany(p => p.ActividadesGestionCambio)
                    .HasForeignKey(d => d.IdDependencia)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ActividadesGestionCambio_Dependencia");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.ActividadesGestionCambio)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ActividadesGestionCambio_Empleado");
            });

            modelBuilder.Entity<ActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdActivoFijo)
                    .HasName("PK_ActivoFijo");

                entity.HasIndex(e => e.IdModelo)
                    .HasName("IX_ActivoFijo_IdModelo");

                entity.HasIndex(e => e.IdSubClaseActivoFijo)
                    .HasName("IX_ActivoFijo_IdSubClaseActivoFijo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ValorCompra).HasColumnType("decimal");

                entity.HasOne(d => d.IdModeloNavigation)
                    .WithMany(p => p.ActivoFijo)
                    .HasForeignKey(d => d.IdModelo)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IdSubClaseActivoFijoNavigation)
                    .WithMany(p => p.ActivoFijo)
                    .HasForeignKey(d => d.IdSubClaseActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<AdministracionTalentoHumano>(entity =>
            {
                entity.HasKey(e => e.IdAdministracionTalentoHumano)
                    .HasName("PK198");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(1000)");

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

            modelBuilder.Entity<AjusteInventarioArticulos>(entity =>
            {
                entity.HasKey(e => e.IdAjusteInventario)
                    .HasName("PK_AjusteInventarioArticulos");

                entity.Property(e => e.Motivo).HasMaxLength(500);

                entity.HasOne(d => d.IdEmpleadoAutorizaNavigation)
                    .WithMany(p => p.AjusteInventarioArticulos)
                    .HasForeignKey(d => d.IdEmpleadoAutoriza)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AjusteInventarioArticulos_Empleado");
            });

            modelBuilder.Entity<AltaActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdAltaActivoFijo)
                    .HasName("PK_ActivosFijosAlta_1");

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.HasOne(d => d.IdFacturaActivoFijoNavigation)
                    .WithMany(p => p.AltaActivoFijo)
                    .HasForeignKey(d => d.IdFacturaActivoFijo)
                    .HasConstraintName("FK_AltaActivoFijo_FacturaActivoFijo");

                entity.HasOne(d => d.IdMotivoAltaNavigation)
                    .WithMany(p => p.AltaActivoFijo)
                    .HasForeignKey(d => d.IdMotivoAlta)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AltaActivoFijo_MotivoAlta");
            });

            modelBuilder.Entity<AltaActivoFijoDetalle>(entity =>
            {
                entity.HasKey(e => new { e.IdRecepcionActivoFijoDetalle, e.IdAltaActivoFijo })
                    .HasName("PK_RecepcionActivoFijoDetalleAltaActivoFijo");

                entity.HasOne(d => d.IdAltaActivoFijoNavigation)
                    .WithMany(p => p.AltaActivoFijoDetalle)
                    .HasForeignKey(d => d.IdAltaActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RecepcionActivoFijoDetalleAltaActivoFijo_AltaActivoFijo");

                entity.HasOne(d => d.IdRecepcionActivoFijoDetalleNavigation)
                    .WithMany(p => p.AltaActivoFijoDetalle)
                    .HasForeignKey(d => d.IdRecepcionActivoFijoDetalle)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RecepcionActivoFijoDetalleAltaActivoFijo_RecepcionActivoFijoDetalle");

                entity.HasOne(d => d.IdTipoUtilizacionAltaNavigation)
                    .WithMany(p => p.AltaActivoFijoDetalle)
                    .HasForeignKey(d => d.IdTipoUtilizacionAlta)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RecepcionActivoFijoDetalleAltaActivoFijo_TipoUtilizacionAlta");

                entity.HasOne(d => d.IdUbicacionActivoFijoNavigation)
                    .WithMany(p => p.AltaActivoFijoDetalle)
                    .HasForeignKey(d => d.IdUbicacionActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RecepcionActivoFijoDetalleAltaActivoFijo_UbicacionActivoFijo");
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

            modelBuilder.Entity<AprobacionAccionPersonal>(entity =>
            {
                entity.HasKey(e => e.IdAprobacionAccionPersonal)
                    .HasName("PK_AprobacionAccionPersonal");

                entity.Property(e => e.FechaAprobacion).HasColumnType("datetime");

                entity.HasOne(d => d.IdAccionPersonalNavigation)
                    .WithMany(p => p.AprobacionAccionPersonal)
                    .HasForeignKey(d => d.IdAccionPersonal)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AprobacionAccionPersonal_AccionPersonal");

                entity.HasOne(d => d.IdEmpleadoAprobadorNavigation)
                    .WithMany(p => p.AprobacionAccionPersonal)
                    .HasForeignKey(d => d.IdEmpleadoAprobador)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AprobacionAccionPersonal_Empleado");

                entity.HasOne(d => d.IdFlujoAprobacionNavigation)
                    .WithMany(p => p.AprobacionAccionPersonal)
                    .HasForeignKey(d => d.IdFlujoAprobacion)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AprobacionAccionPersonal_FlujoAprobacion");
            });

            modelBuilder.Entity<AprobacionViatico>(entity =>
            {
                entity.HasKey(e => e.IdAprobacionViatico)
                    .HasName("PK254");

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
                    .HasColumnType("varchar(200)");

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
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
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
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<BajaActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdBajaActivoFijo)
                    .HasName("PK_ActivosFijosBaja");

                entity.Property(e => e.FechaBaja).HasColumnType("datetime");

                entity.Property(e => e.MemoOficioResolucion)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.IdMotivoBajaNavigation)
                    .WithMany(p => p.BajaActivoFijo)
                    .HasForeignKey(d => d.IdMotivoBaja)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ActivosFijosBaja_ActivoFijoMotivoBaja");
            });

            modelBuilder.Entity<BajaActivoFijoDetalle>(entity =>
            {
                entity.HasKey(e => new { e.IdRecepcionActivoFijoDetalle, e.IdBajaActivoFijo })
                    .HasName("PK_RecepcionActivoFijoDetalleBajaActivoFijo");

                entity.HasOne(d => d.IdBajaActivoFijoNavigation)
                    .WithMany(p => p.BajaActivoFijoDetalle)
                    .HasForeignKey(d => d.IdBajaActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RecepcionActivoFijoDetalleBajaActivoFijo_BajaActivoFijo");

                entity.HasOne(d => d.IdRecepcionActivoFijoDetalleNavigation)
                    .WithMany(p => p.BajaActivoFijoDetalle)
                    .HasForeignKey(d => d.IdRecepcionActivoFijoDetalle)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RecepcionActivoFijoDetalleBajaActivoFijo_RecepcionActivoFijoDetalle");
            });

            modelBuilder.Entity<Bodega>(entity =>
            {
                entity.HasKey(e => e.IdBodega)
                    .HasName("PK_Bodega");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.IdEmpleadoResponsableNavigation)
                    .WithMany(p => p.Bodega)
                    .HasForeignKey(d => d.IdEmpleadoResponsable)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Bodega_Empleado");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.Bodega)
                    .HasForeignKey(d => d.IdSucursal)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Bodega_Sucursal");
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

            modelBuilder.Entity<CabeceraNomina>(entity =>
            {
                entity.HasKey(e => e.IdCabeceraNomina)
                    .HasName("PK_CabeceraNomina");

                entity.Property(e => e.Identificacion).HasColumnType("varchar(20)");

                entity.HasOne(d => d.IdCalculoNominaNavigation)
                    .WithMany(p => p.CabeceraNomina)
                    .HasForeignKey(d => d.IdCalculoNomina)
                    .HasConstraintName("FK_CabeceraNomina_CalculoNomina");
            });

            modelBuilder.Entity<CalculoNomina>(entity =>
            {
                entity.HasKey(e => e.IdCalculoNomina)
                    .HasName("PK_CalculoNomina");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(250)");

                entity.HasOne(d => d.IdPeriodoNavigation)
                    .WithMany(p => p.CalculoNomina)
                    .HasForeignKey(d => d.IdPeriodo)
                    .HasConstraintName("FK_CalculoNomina_PeriodoNomina");

                entity.HasOne(d => d.IdProcesoNavigation)
                    .WithMany(p => p.CalculoNomina)
                    .HasForeignKey(d => d.IdProceso)
                    .HasConstraintName("FK_CalculoNomina_CalculoNomina");
            });

            modelBuilder.Entity<Calificacion>(entity =>
            {
                entity.HasKey(e => e.IdCalificacion)
                    .HasName("PK_Calificacion");

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

            modelBuilder.Entity<CandidatoConcurso>(entity =>
            {
                entity.HasKey(e => e.IdCandidatoConcurso)
                    .HasName("PK270");

                entity.Property(e => e.CodigoSocioEmpleo).HasColumnType("varchar(50)");

                entity.Property(e => e.Observacion).HasColumnType("varchar(1000)");

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

            modelBuilder.Entity<CandidatoEstudio>(entity =>
            {
                entity.HasKey(e => e.IdCandidatoEstudio)
                    .HasName("PK_CandidatoEstudio");

                entity.Property(e => e.FechaGraduado).HasColumnType("date");

                entity.Property(e => e.NoSenescyt).HasColumnType("varchar(50)");

                entity.Property(e => e.Observaciones).HasColumnType("varchar(300)");

                entity.HasOne(d => d.IdCandidatoNavigation)
                    .WithMany(p => p.CandidatoEstudio)
                    .HasForeignKey(d => d.IdCandidato)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CandidatoEstudio_Candidato");

                entity.HasOne(d => d.IdTituloNavigation)
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

                entity.HasOne(d => d.IdCandidatoNavigation)
                    .WithMany(p => p.CandidatoTrayectoriaLaboral)
                    .HasForeignKey(d => d.IdCandidato)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CandidatoTrayectoriaLaboral_Candidato");
            });

            modelBuilder.Entity<Capacitacion>(entity =>
            {
                entity.HasKey(e => e.IdCapacitacion)
                    .HasName("PK12_1");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
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

                entity.Property(e => e.Especialidad)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

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
                    .HasConstraintName("RefCatalogoCuenta466");
            });

            modelBuilder.Entity<CategoriaActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdCategoriaActivoFijo)
                    .HasName("PK_CategoriaActivoFijo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PorCientoDepreciacionAnual).HasColumnType("decimal");
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

                entity.HasIndex(e => e.IdCategoriaActivoFijo)
                    .HasName("IX_ClaseActivoFijo_IdTablaDepreciacion");

                entity.HasIndex(e => e.IdTipoActivoFijo)
                    .HasName("IX_ClaseActivoFijo_IdTipoActivoFijo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(200)");

                entity.HasOne(d => d.IdCategoriaActivoFijoNavigation)
                    .WithMany(p => p.ClaseActivoFijo)
                    .HasForeignKey(d => d.IdCategoriaActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ClaseActivoFijo_CategoriaActivoFijo");

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
                    .HasColumnType("varchar(200)");

                entity.HasOne(d => d.IdTipoArticuloNavigation)
                    .WithMany(p => p.ClaseArticulo)
                    .HasForeignKey(d => d.IdTipoArticulo)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CodigoActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdCodigoActivoFijo)
                    .HasName("PK_CodigoActivoFijo");

                entity.Property(e => e.Codigosecuencial)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<CompaniaSeguro>(entity =>
            {
                entity.HasKey(e => e.IdCompaniaSeguro)
                    .HasName("PK_CompaniaSeguro");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Complain>(entity =>
            {
                entity.HasKey(e => e.IdComplain)
                    .HasName("PK8");

                entity.ToTable("COMPLAIN");

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

            modelBuilder.Entity<ComponenteActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdComponenteActivoFijo)
                    .HasName("PK_ActivoFijoComponentes_1");

                entity.HasOne(d => d.IdRecepcionActivoFijoDetalleComponenteNavigation)
                    .WithMany(p => p.ComponenteActivoFijoIdRecepcionActivoFijoDetalleComponenteNavigation)
                    .HasForeignKey(d => d.IdRecepcionActivoFijoDetalleComponente)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ComponenteActivoFijo_RecepcionActivoFijoDetalle1");

                entity.HasOne(d => d.IdRecepcionActivoFijoDetalleOrigenNavigation)
                    .WithMany(p => p.ComponenteActivoFijoIdRecepcionActivoFijoDetalleOrigenNavigation)
                    .HasForeignKey(d => d.IdRecepcionActivoFijoDetalleOrigen)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ComponenteActivoFijo_RecepcionActivoFijoDetalle");
            });

            modelBuilder.Entity<ComportamientoObservable>(entity =>
            {
                entity.HasKey(e => e.IdComportamientoObservable)
                    .HasName("PK204");

                entity.Property(e => e.Descripcion).HasColumnType("text");

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

            modelBuilder.Entity<ConceptoConjuntoNomina>(entity =>
            {
                entity.HasKey(e => e.IdConceptoConjunto)
                    .HasName("PK4");

                entity.HasOne(d => d.IdConceptoNavigation)
                    .WithMany(p => p.ConceptoConjuntoNomina)
                    .HasForeignKey(d => d.IdConcepto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefConceptoNomina7");

                entity.HasOne(d => d.IdConjuntoNavigation)
                    .WithMany(p => p.ConceptoConjuntoNomina)
                    .HasForeignKey(d => d.IdConjunto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefConjuntoNomina6");
            });

            modelBuilder.Entity<ConceptoNomina>(entity =>
            {
                entity.HasKey(e => e.IdConcepto)
                    .HasName("PK2");

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

                entity.HasOne(d => d.IdProcesoNavigation)
                    .WithMany(p => p.ConceptoNomina)
                    .HasForeignKey(d => d.IdProceso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefProcesoNomina8");
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
            });

            modelBuilder.Entity<ConjuntoNomina>(entity =>
            {
                entity.HasKey(e => e.IdConjunto)
                    .HasName("PK3");

                entity.Property(e => e.AliasConcepto).HasColumnType("varchar(100)");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.HasOne(d => d.IdTipoConjuntoNavigation)
                    .WithMany(p => p.ConjuntoNomina)
                    .HasForeignKey(d => d.IdTipoConjunto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefTipoConjuntoNomina5");
            });

            modelBuilder.Entity<ConocimientosAdicionales>(entity =>
            {
                entity.HasKey(e => e.IdConocimientosAdicionales)
                    .HasName("PK222");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<ConstanteNomina>(entity =>
            {
                entity.HasKey(e => e.IdConstante)
                    .HasName("PK_ConstanteNomina");

                entity.Property(e => e.Constante).HasColumnType("varchar(20)");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<DatosBancarios>(entity =>
            {
                entity.HasKey(e => e.IdDatosBancarios)
                    .HasName("PK24");

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

                entity.Property(e => e.Codigo).HasColumnType("varchar(50)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.Dependencia)
                    .HasForeignKey(d => d.IdBodega)
                    .HasConstraintName("FK_Dependencia_Bodega");

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

                entity.Property(e => e.DepreciacionAcumulada).HasColumnType("decimal");

                entity.Property(e => e.ValorCompra).HasColumnType("decimal");

                entity.Property(e => e.ValorResidual).HasColumnType("decimal");

                entity.HasOne(d => d.IdRecepcionActivoFijoDetalleNavigation)
                    .WithMany(p => p.DepreciacionActivoFijo)
                    .HasForeignKey(d => d.IdRecepcionActivoFijoDetalle)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DepreciacionActivoFijo_RecepcionActivoFijoDetalle");
            });

            modelBuilder.Entity<Destreza>(entity =>
            {
                entity.HasKey(e => e.IdDestreza)
                    .HasName("PK40");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<DetalleEvaluacionEvento>(entity =>
            {
                entity.HasKey(e => e.IdDetalleEvaluacionEvento)
                    .HasName("PK_DetalleEvaluacionEvento");

                entity.HasOne(d => d.IdEvaluacionEventoNavigation)
                    .WithMany(p => p.DetalleEvaluacionEvento)
                    .HasForeignKey(d => d.IdEvaluacionEvento)
                    .HasConstraintName("FK_DetalleEvaluacionEvento_EvaluacionEvento");

                entity.HasOne(d => d.IdPreguntasEvaluacionEventoNavigation)
                    .WithMany(p => p.DetalleEvaluacionEvento)
                    .HasForeignKey(d => d.IdPreguntasEvaluacionEvento)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DetalleEvaluacionEvento_PreguntaEvaluacionEvento");
            });

            modelBuilder.Entity<DetalleExamenInduccion>(entity =>
            {
                entity.HasKey(e => e.IdDetalleExamenInduccion)
                    .HasName("PK236");

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

            modelBuilder.Entity<DetalleNomina>(entity =>
            {
                entity.HasKey(e => e.IdDetalleNomina)
                    .HasName("PK_DetalleNomina");

                entity.Property(e => e.CodigoConceptoNomina).HasColumnType("varchar(10)");

                entity.Property(e => e.Valor).HasColumnType("numeric");

                entity.HasOne(d => d.IdCabeceraNominaNavigation)
                    .WithMany(p => p.DetalleNomina)
                    .HasForeignKey(d => d.IdCabeceraNomina)
                    .HasConstraintName("FK_DetalleNomina_CabeceraNomina");
            });

            modelBuilder.Entity<DetallePresupuesto>(entity =>
            {
                entity.HasKey(e => e.IdDetallePresupuesto)
                    .HasName("PK_DetallePresupuesto");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.HasOne(d => d.IdPresupuestoNavigation)
                    .WithMany(p => p.DetallePresupuesto)
                    .HasForeignKey(d => d.IdPresupuesto)
                    .HasConstraintName("FK_DetallePresupuesto_Presupuesto");

                entity.HasOne(d => d.IdSolicitudViaticoNavigation)
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

                entity.HasOne(d => d.IdCiudadDestinoNavigation)
                    .WithMany(p => p.DetalleReliquidacionViaticoIdCiudadDestinoNavigation)
                    .HasForeignKey(d => d.IdCiudadDestino)
                    .HasConstraintName("FK_DetalleReliquidacionViatico_Ciudad");

                entity.HasOne(d => d.IdCiudadOrigenNavigation)
                    .WithMany(p => p.DetalleReliquidacionViaticoIdCiudadOrigenNavigation)
                    .HasForeignKey(d => d.IdCiudadOrigen)
                    .HasConstraintName("FK_DetalleReliquidacionViatico_Ciudad1");

                entity.HasOne(d => d.IdItemViaticoNavigation)
                    .WithMany(p => p.DetalleReliquidacionViatico)
                    .HasForeignKey(d => d.IdItemViatico)
                    .HasConstraintName("FK_DetalleReliquidacionViatico_ItemViatico");

                entity.HasOne(d => d.IdReliquidacionViaticoNavigation)
                    .WithMany(p => p.DetalleReliquidacionViatico)
                    .HasForeignKey(d => d.IdReliquidacionViatico)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DetalleReliquidacionViatico_SolicitudViatico");

                entity.HasOne(d => d.IdTipoTransporteNavigation)
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

            modelBuilder.Entity<DocumentoActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdDocumentoActivoFijo)
                    .HasName("PK_ActivoFijoDocumento");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Url).HasColumnType("varchar(1024)");

                entity.HasOne(d => d.IdActivoFijoNavigation)
                    .WithMany(p => p.DocumentoActivoFijo)
                    .HasForeignKey(d => d.IdActivoFijo)
                    .HasConstraintName("FK_ActivoFijoDocumento_ActivoFijo");

                entity.HasOne(d => d.IdAltaActivoFijoNavigation)
                    .WithMany(p => p.DocumentoActivoFijo)
                    .HasForeignKey(d => d.IdAltaActivoFijo)
                    .HasConstraintName("FK_DocumentoActivoFijo_AltaActivoFijo");

                entity.HasOne(d => d.IdFacturaActivoFijoNavigation)
                    .WithMany(p => p.DocumentoActivoFijo)
                    .HasForeignKey(d => d.IdFacturaActivoFijo)
                    .HasConstraintName("FK_DocumentoActivoFijo_FacturaActivoFijo");

                entity.HasOne(d => d.IdProcesoJudicialActivoFijoNavigation)
                    .WithMany(p => p.DocumentoActivoFijo)
                    .HasForeignKey(d => d.IdProcesoJudicialActivoFijo)
                    .HasConstraintName("FK_DocumentoActivoFijo_ProcesoJudicialActivoFijo");

                entity.HasOne(d => d.IdRecepcionActivoFijoDetalleNavigation)
                    .WithMany(p => p.DocumentoActivoFijo)
                    .HasForeignKey(d => d.IdRecepcionActivoFijoDetalle)
                    .HasConstraintName("FK_DocumentoActivoFijo_RecepcionActivoFijoDetalle");
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

                entity.Property(e => e.Detalle).HasColumnType("varchar(2000)");

                entity.Property(e => e.FechaIngreso).HasColumnType("datetime");

                entity.Property(e => e.FechaIngresoSectorPublico).HasColumnType("datetime");

                entity.Property(e => e.Foto).HasColumnType("text");

                entity.Property(e => e.IdBrigadaSsorol).HasColumnName("IdBrigadaSSORol");

                entity.Property(e => e.IngresosOtraActividad).HasMaxLength(150);

                entity.Property(e => e.NombreUsuario).HasMaxLength(50);

                entity.Property(e => e.RelacionSuperintendencia).HasColumnType("varchar(200)");

                entity.Property(e => e.Telefono).HasColumnType("varchar(20)");

                entity.Property(e => e.TipoRelacion).HasColumnType("varchar(50)");

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

            modelBuilder.Entity<EmpleadoContactoEmergencia>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpleadoContactoEmergencia, e.IdPersona })
                    .HasName("PK25");

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

                entity.Property(e => e.FechaDesde).HasColumnType("date");

                entity.Property(e => e.IngresoTotal).HasColumnType("decimal");

                entity.Property(e => e.OtrosIngresos).HasColumnType("decimal");
            });

            modelBuilder.Entity<EmpleadoMovimiento>(entity =>
            {
                entity.HasKey(e => e.IdEmpleadoMovimiento)
                    .HasName("PK126");

                entity.Property(e => e.CodigoContrato).HasColumnType("varchar(50)");

                entity.Property(e => e.FechaDesde).HasColumnType("date");

                entity.Property(e => e.FechaHasta).HasColumnType("date");

                entity.Property(e => e.NumeroPartidaIndividual).HasColumnType("varchar(50)");

                entity.Property(e => e.SalarioReal).HasColumnType("decimal");

                entity.HasOne(d => d.IdAccionPersonalNavigation)
                    .WithMany(p => p.EmpleadoMovimiento)
                    .HasForeignKey(d => d.IdAccionPersonal)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_EmpleadoMovimiento_AccionPersonal");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.EmpleadoMovimiento)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_EmpleadoMovimiento_Empleado");

                entity.HasOne(d => d.IdFondoFinanciamientoNavigation)
                    .WithMany(p => p.EmpleadoMovimiento)
                    .HasForeignKey(d => d.IdFondoFinanciamiento)
                    .HasConstraintName("FK_EmpleadoMovimiento_FondoFinanciamiento");

                entity.HasOne(d => d.IdIndiceOcupacionalNavigation)
                    .WithMany(p => p.EmpleadoMovimiento)
                    .HasForeignKey(d => d.IdIndiceOcupacional)
                    .HasConstraintName("FK_EmpleadoMovimiento_IndiceOcupacional");

                entity.HasOne(d => d.IdIndiceOcupacionalModalidadPartidaDesdeNavigation)
                    .WithMany(p => p.EmpleadoMovimientoIdIndiceOcupacionalModalidadPartidaDesdeNavigation)
                    .HasForeignKey(d => d.IdIndiceOcupacionalModalidadPartidaDesde)
                    .HasConstraintName("RefIndiceOcupacionalModalidadPartida195");

                entity.HasOne(d => d.IdIndiceOcupacionalModalidadPartidaHastaNavigation)
                    .WithMany(p => p.EmpleadoMovimientoIdIndiceOcupacionalModalidadPartidaHastaNavigation)
                    .HasForeignKey(d => d.IdIndiceOcupacionalModalidadPartidaHasta)
                    .HasConstraintName("FK_EmpleadoMovimiento_IndiceOcupacionalModalidadPartida");

                entity.HasOne(d => d.IdModalidadPartidaNavigation)
                    .WithMany(p => p.EmpleadoMovimiento)
                    .HasForeignKey(d => d.IdModalidadPartida)
                    .HasConstraintName("FK_EmpleadoMovimiento_ModalidadPartida");

                entity.HasOne(d => d.IdTipoNombramientoNavigation)
                    .WithMany(p => p.EmpleadoMovimiento)
                    .HasForeignKey(d => d.IdTipoNombramiento)
                    .HasConstraintName("FK_EmpleadoMovimiento_TipoNombramiento");
            });

            modelBuilder.Entity<EmpleadoNepotismo>(entity =>
            {
                entity.HasKey(e => e.IdEmpleadoNepotismo)
                    .HasName("PK123");
            });

            modelBuilder.Entity<EmpleadoSaldoVacaciones>(entity =>
            {
                entity.HasKey(e => e.IdEmpleadoSaldoVacaciones)
                    .HasName("PK74");

                entity.Property(e => e.SaldoDias).HasColumnType("decimal");

                entity.Property(e => e.SaldoMonetario).HasColumnType("decimal");
            });

            modelBuilder.Entity<EmpleadosFormularioDevengacion>(entity =>
            {
                entity.HasKey(e => e.IdEmpleadosFormularioDevengacion)
                    .HasName("PK246");

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

                entity.Property(e => e.FechaEvaluacionDesde).HasColumnType("date");

                entity.Property(e => e.FechaEvaluacionHasta).HasColumnType("date");

                entity.Property(e => e.FechaRegistro).HasColumnType("date");

                entity.Property(e => e.Observaciones).HasColumnType("text");

                entity.HasOne(d => d.IdEmpleadoEvaluadoNavigation)
                    .WithMany(p => p.Eval001)
                    .HasForeignKey(d => d.IdEmpleadoEvaluado)
                    .HasConstraintName("FK_Eval001_Empleado");

                entity.HasOne(d => d.IdEscalaEvaluacionTotalNavigation)
                    .WithMany(p => p.Eval001)
                    .HasForeignKey(d => d.IdEscalaEvaluacionTotal)
                    .HasConstraintName("RefEscalaEvaluacionTotal78");

                entity.HasOne(d => d.IdEvaluadorNavigation)
                    .WithMany(p => p.Eval001)
                    .HasForeignKey(d => d.IdEvaluador)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEvaluador301");
            });

            modelBuilder.Entity<EvaluacionActividadesPuestoTrabajo>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionActividadesPuestoTrabajo)
                    .HasName("PK34");

                entity.Property(e => e.DescripcionActividad)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdActividadesEsencialesNavigation)
                    .WithMany(p => p.EvaluacionActividadesPuestoTrabajo)
                    .HasForeignKey(d => d.IdActividadesEsenciales)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_EvaluacionActividadesPuestoTrabajo_ActividadesEsenciales");

                entity.HasOne(d => d.IdEval001Navigation)
                    .WithMany(p => p.EvaluacionActividadesPuestoTrabajo)
                    .HasForeignKey(d => d.IdEval001)
                    .HasConstraintName("FK_EvaluacionActividadesPuestoTrabajo_Eval001");

                entity.HasOne(d => d.IdIndicadorNavigation)
                    .WithMany(p => p.EvaluacionActividadesPuestoTrabajo)
                    .HasForeignKey(d => d.IdIndicador)
                    .HasConstraintName("RefIndicador57");
            });

            modelBuilder.Entity<EvaluacionCompetenciasTecnicasPuesto>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionCompetenciasTecnicasPuesto)
                    .HasName("PK39");

                entity.HasOne(d => d.IdComportamientoObservableNavigation)
                    .WithMany(p => p.EvaluacionCompetenciasTecnicasPuesto)
                    .HasForeignKey(d => d.IdComportamientoObservable)
                    .HasConstraintName("FK_EvaluacionCompetenciasTecnicasPuesto_ComportamientoObservable");

                entity.HasOne(d => d.IdEval001Navigation)
                    .WithMany(p => p.EvaluacionCompetenciasTecnicasPuesto)
                    .HasForeignKey(d => d.IdEval001)
                    .HasConstraintName("FK_EvaluacionCompetenciasTecnicasPuesto_Eval001");

                entity.HasOne(d => d.IdNivelDesarrolloNavigation)
                    .WithMany(p => p.EvaluacionCompetenciasTecnicasPuesto)
                    .HasForeignKey(d => d.IdNivelDesarrollo)
                    .HasConstraintName("RefNivelDesarrollo65");
            });

            modelBuilder.Entity<EvaluacionCompetenciasUniversales>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionCompetenciasUniversales)
                    .HasName("PK44");

                entity.HasOne(d => d.IdComportamientoObservableNavigation)
                    .WithMany(p => p.EvaluacionCompetenciasUniversales)
                    .HasForeignKey(d => d.IdComportamientoObservable)
                    .HasConstraintName("FK_EvaluacionCompetenciasUniversales_ComportamientoObservable");

                entity.HasOne(d => d.IdEval001Navigation)
                    .WithMany(p => p.EvaluacionCompetenciasUniversales)
                    .HasForeignKey(d => d.IdEval001)
                    .HasConstraintName("FK_EvaluacionCompetenciasUniversales_Eval001");

                entity.HasOne(d => d.IdFrecuenciaAplicacionNavigation)
                    .WithMany(p => p.EvaluacionCompetenciasUniversales)
                    .HasForeignKey(d => d.IdFrecuenciaAplicacion)
                    .HasConstraintName("RefFrecuenciaAplicacion70");
            });

            modelBuilder.Entity<EvaluacionConocimiento>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionConocimiento)
                    .HasName("PK36");

                entity.HasOne(d => d.IdAreaConocimientoNavigation)
                    .WithMany(p => p.EvaluacionConocimiento)
                    .HasForeignKey(d => d.IdAreaConocimiento)
                    .HasConstraintName("FK_EvaluacionConocimiento_AreaConocimiento");

                entity.HasOne(d => d.IdEval001Navigation)
                    .WithMany(p => p.EvaluacionConocimiento)
                    .HasForeignKey(d => d.IdEval001)
                    .HasConstraintName("FK_EvaluacionConocimiento_Eval001");

                entity.HasOne(d => d.IdNivelConocimientoNavigation)
                    .WithMany(p => p.EvaluacionConocimiento)
                    .HasForeignKey(d => d.IdNivelConocimiento)
                    .HasConstraintName("RefNivelConocimiento59");
            });

            modelBuilder.Entity<EvaluacionEvento>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacionEvento)
                    .HasName("PK_EvaluacionEvento");

                entity.Property(e => e.Sugerencias).HasColumnType("text");

                entity.HasOne(d => d.IdPlanCapacitacionNavigation)
                    .WithMany(p => p.EvaluacionEvento)
                    .HasForeignKey(d => d.IdPlanCapacitacion)
                    .HasConstraintName("FK_EvaluacionEvento_PlanCapacitacion");
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
                    .HasName("PK47");

                entity.HasOne(d => d.IdComportamientoObservableNavigation)
                    .WithMany(p => p.EvaluacionTrabajoEquipoIniciativaLiderazgo)
                    .HasForeignKey(d => d.IdComportamientoObservable)
                    .HasConstraintName("FK_EvaluacionTrabajoEquipoIniciativaLiderazgo_ComportamientoObservable");

                entity.HasOne(d => d.IdEval001Navigation)
                    .WithMany(p => p.EvaluacionTrabajoEquipoIniciativaLiderazgo)
                    .HasForeignKey(d => d.IdEval001)
                    .HasConstraintName("FK_EvaluacionTrabajoEquipoIniciativaLiderazgo_Eval001");

                entity.HasOne(d => d.IdFrecuenciaAplicacionNavigation)
                    .WithMany(p => p.EvaluacionTrabajoEquipoIniciativaLiderazgo)
                    .HasForeignKey(d => d.IdFrecuenciaAplicacion)
                    .HasConstraintName("RefFrecuenciaAplicacion74");
            });

            modelBuilder.Entity<Evaluador>(entity =>
            {
                entity.HasKey(e => e.IdEvaluador)
                    .HasName("PK182");

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

                entity.Property(e => e.Url).HasColumnType("varchar(1024)");

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

            modelBuilder.Entity<FacturaActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdFacturaActivoFijo)
                    .HasName("PK_FacturaActivoFijo");

                entity.Property(e => e.NumeroFactura)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<FacturaViatico>(entity =>
            {
                entity.HasKey(e => e.IdFacturaViatico)
                    .HasName("PK253");

                entity.Property(e => e.FechaAprobacion).HasColumnType("date");

                entity.Property(e => e.FechaFactura).HasColumnType("date");

                entity.Property(e => e.NumeroFactura)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Observaciones).HasColumnType("text");

                entity.Property(e => e.Url).HasColumnType("varchar(1024)");

                entity.Property(e => e.ValorTotalAprobacion).HasColumnType("date");

                entity.Property(e => e.ValorTotalFactura).HasColumnType("decimal");

                entity.HasOne(d => d.IdItemViaticoNavigation)
                    .WithMany(p => p.FacturaViatico)
                    .HasForeignKey(d => d.IdItemViatico)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefItemViatico388");

                entity.HasOne(d => d.IdSolicitudViaticoNavigation)
                    .WithMany(p => p.FacturaViatico)
                    .HasForeignKey(d => d.IdSolicitudViatico)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_FacturaViatico_SolicitudViatico");
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

                entity.HasOne(d => d.IdManualPuestoNavigation)
                    .WithMany(p => p.FlujoAprobacion)
                    .HasForeignKey(d => d.IdManualPuesto)
                    .HasConstraintName("FK_FlujoAprobacion_ManualPuesto");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.FlujoAprobacion)
                    .HasForeignKey(d => d.IdSucursal)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_FlujoAprobacion_Sucursal");

                entity.HasOne(d => d.IdTipoAccionPersonalNavigation)
                    .WithMany(p => p.FlujoAprobacion)
                    .HasForeignKey(d => d.IdTipoAccionPersonal)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_FlujoAprobacion_TipoAccionPersonal");
            });

            modelBuilder.Entity<FondoFinanciamiento>(entity =>
            {
                entity.HasKey(e => e.IdFondoFinanciamiento)
                    .HasName("PK_FondoFinanciamiento");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<FormularioAnalisisOcupacional>(entity =>
            {
                entity.HasKey(e => e.IdFormularioAnalisisOcupacional)
                    .HasName("PK107");

                entity.Property(e => e.FechaRegistro).HasColumnType("date");

                entity.Property(e => e.MisionPuesto)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.FormularioAnalisisOcupacional)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_FormularioAnalisisOcupacional_Empleado");
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

            modelBuilder.Entity<FuncionNomina>(entity =>
            {
                entity.HasKey(e => e.IdFuncion)
                    .HasName("PK_FuncionNomina");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(100)");

                entity.Property(e => e.ProcedimientoAlmacenado).HasColumnType("varchar(100)");

                entity.Property(e => e.Variable).HasColumnType("varchar(3)");
            });

            modelBuilder.Entity<GastoPersonal>(entity =>
            {
                entity.HasKey(e => e.IdGastoPersonal)
                    .HasName("PK_GastoPersonal");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.GastoPersonal)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_GastoPersonal_Empleado");

                entity.HasOne(d => d.IdTipoGastoPersonalNavigation)
                    .WithMany(p => p.GastoPersonal)
                    .HasForeignKey(d => d.IdTipoGastoPersonal)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_GastoPersonal_TipoDeGastoPersonal");
            });

            modelBuilder.Entity<GastoRubro>(entity =>
            {
                entity.HasKey(e => e.IdGastoRubro)
                    .HasName("PK82");

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

            modelBuilder.Entity<GeneralCapacitacion>(entity =>
            {
                entity.HasKey(e => e.IdGeneralCapacitacion)
                    .HasName("PK_GeneralCapacitacion");

                entity.Property(e => e.Nombre).HasColumnType("varchar(100)");

                entity.Property(e => e.Tipo).HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.HasKey(e => e.IdGenero)
                    .HasName("PK_Genero");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
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
                    .HasName("PK61");

                entity.Property(e => e.TipoEscala)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<HorasExtrasNomina>(entity =>
            {
                entity.HasKey(e => e.IdHorasExtrasNomina)
                    .HasName("PK_HorasExtrasNomina");

                entity.Property(e => e.IdentificacionEmpleado).HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdCalculoNominaNavigation)
                    .WithMany(p => p.HorasExtrasNomina)
                    .HasForeignKey(d => d.IdCalculoNomina)
                    .HasConstraintName("FK_HorasExtrasNomina_CalculoNomina");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.HorasExtrasNomina)
                    .HasForeignKey(d => d.IdEmpleado)
                    .HasConstraintName("FK_HorasExtrasNomina_Empleado");
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

                entity.Property(e => e.Nivel).HasColumnType("varchar(50)");

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

                entity.Property(e => e.CodigoContrato).HasColumnType("varchar(50)");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.FechaFin).HasColumnType("date");

                entity.Property(e => e.NumeroPartidaIndividual).HasColumnType("varchar(50)");

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

                entity.HasOne(d => d.IdModalidadPartidaNavigation)
                    .WithMany(p => p.IndiceOcupacionalModalidadPartida)
                    .HasForeignKey(d => d.IdModalidadPartida)
                    .HasConstraintName("FK_IndiceOcupacionalModalidadPartida_ModalidadPartida");

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

            modelBuilder.Entity<InformeActividadViatico>(entity =>
            {
                entity.HasKey(e => e.IdInformeActividad)
                    .HasName("PK_InformeActividadViatico");

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.Observacion).HasColumnType("text");

                entity.HasOne(d => d.IdSolicitudViaticoNavigation)
                    .WithMany(p => p.InformeActividadViatico)
                    .HasForeignKey(d => d.IdSolicitudViatico)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_InformeActividadViatico_SolicitudViatico");
            });

            modelBuilder.Entity<InformeUath>(entity =>
            {
                entity.HasKey(e => e.IdInformeUath)
                    .HasName("PK201");

                entity.ToTable("InformeUATH");

                entity.Property(e => e.IdInformeUath).HasColumnName("IdInformeUATH");

                entity.HasOne(d => d.IdAdministracionTalentoHumanoNavigation)
                    .WithMany(p => p.InformeUath)
                    .HasForeignKey(d => d.IdAdministracionTalentoHumano)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefAdministracionTalentoHumano321");

                entity.HasOne(d => d.IdManualPuestoDestinoNavigation)
                    .WithMany(p => p.InformeUathIdManualPuestoDestinoNavigation)
                    .HasForeignKey(d => d.IdManualPuestoDestino)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefManualPuesto319");

                entity.HasOne(d => d.IdManualPuestoOrigenNavigation)
                    .WithMany(p => p.InformeUathIdManualPuestoOrigenNavigation)
                    .HasForeignKey(d => d.IdManualPuestoOrigen)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefManualPuesto318");
            });

            modelBuilder.Entity<InformeViatico>(entity =>
            {
                entity.HasKey(e => e.IdInformeViatico)
                    .HasName("PK257");

                entity.Property(e => e.FechaLlegada).HasColumnType("date");

                entity.Property(e => e.FechaSalida).HasColumnType("date");

                entity.Property(e => e.HoraLlegada).HasColumnName("HoraLLegada");

                entity.Property(e => e.NombreTransporte).HasColumnType("varchar(250)");

                entity.Property(e => e.ValorEstimado).HasColumnType("decimal");

                entity.HasOne(d => d.IdCiudadDestinoNavigation)
                    .WithMany(p => p.InformeViaticoIdCiudadDestinoNavigation)
                    .HasForeignKey(d => d.IdCiudadDestino)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_InformeViatico_Ciudad");

                entity.HasOne(d => d.IdCiudadOrigenNavigation)
                    .WithMany(p => p.InformeViaticoIdCiudadOrigenNavigation)
                    .HasForeignKey(d => d.IdCiudadOrigen)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_InformeViatico_Ciudad1");

                entity.HasOne(d => d.IdSolicitudViaticoNavigation)
                    .WithMany(p => p.InformeViatico)
                    .HasForeignKey(d => d.IdSolicitudViatico)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefItinerarioViatico396");

                entity.HasOne(d => d.IdTipoTransporteNavigation)
                    .WithMany(p => p.InformeViatico)
                    .HasForeignKey(d => d.IdTipoTransporte)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_InformeViatico_TipoTransporte");
            });

            modelBuilder.Entity<IngresoEgresoRmu>(entity =>
            {
                entity.HasKey(e => e.IdIngresoEgresoRmu)
                    .HasName("PK152");

                entity.ToTable("IngresoEgresoRMU");

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

            modelBuilder.Entity<InventarioActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdInventarioActivoFijo)
                    .HasName("PK_InventarioActivoFijo");

                entity.Property(e => e.NumeroInforme)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.InventarioActivoFijo)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_InventarioActivoFijo_Estado");
            });

            modelBuilder.Entity<InventarioActivoFijoDetalle>(entity =>
            {
                entity.HasKey(e => new { e.IdRecepcionActivoFijoDetalle, e.IdInventarioActivoFijo })
                    .HasName("PK_InventarioActivoFijoDetalle");

                entity.HasOne(d => d.IdInventarioActivoFijoNavigation)
                    .WithMany(p => p.InventarioActivoFijoDetalle)
                    .HasForeignKey(d => d.IdInventarioActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_InventarioActivoFijoDetalle_InventarioActivoFijo");

                entity.HasOne(d => d.IdRecepcionActivoFijoDetalleNavigation)
                    .WithMany(p => p.InventarioActivoFijoDetalle)
                    .HasForeignKey(d => d.IdRecepcionActivoFijoDetalle)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_InventarioActivoFijoDetalle_RecepcionActivoFijoDetalle");
            });

            modelBuilder.Entity<InventarioArticulos>(entity =>
            {
                entity.HasKey(e => e.IdInventarioArticulos)
                    .HasName("PK_InventarioArticulos_1");

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.InventarioArticulos)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_InventarioArticulos_Articulo");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.InventarioArticulos)
                    .HasForeignKey(d => d.IdBodega)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_InventarioArticulos_Bodega1");
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

                entity.HasOne(d => d.IdCiudadDestinoNavigation)
                    .WithMany(p => p.ItinerarioViaticoIdCiudadDestinoNavigation)
                    .HasForeignKey(d => d.IdCiudadDestino)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ItinerarioViatico_Ciudad1");

                entity.HasOne(d => d.IdCiudadOrigenNavigation)
                    .WithMany(p => p.ItinerarioViaticoIdCiudadOrigenNavigation)
                    .HasForeignKey(d => d.IdCiudadOrigen)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ItinerarioViatico_Ciudad");

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

            modelBuilder.Entity<LavadoActivoEmpleado>(entity =>
            {
                entity.HasKey(e => e.IdLavadoActivoEmpleado)
                    .HasName("PK_LavadoActivoEmpleado");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.LavadoActivoEmpleado)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_LavadoActivoEmpleado_Empleado");

                entity.HasOne(d => d.IdLavadoActivoItemNavigation)
                    .WithMany(p => p.LavadoActivoEmpleado)
                    .HasForeignKey(d => d.IdLavadoActivoItem)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_LavadoActivoEmpleado_LavadoActivoItem");
            });

            modelBuilder.Entity<LavadoActivoItem>(entity =>
            {
                entity.HasKey(e => e.IdLavadoActivoItem)
                    .HasName("PK_LavadoActivoItem");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(700)");
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
                    .HasName("PK__libros__40F9A2076969E3DB");

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

            modelBuilder.Entity<LineaServicio>(entity =>
            {
                entity.HasKey(e => e.IdLineaServicio)
                    .HasName("PK_LineaServicio");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Liquidacion>(entity =>
            {
                entity.HasKey(e => e.IdLiquidacion)
                    .HasName("PK182_1");

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

                entity.Property(e => e.CodigoArticulo)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.MaestroArticuloSucursal)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_MaestroArticuloSucursal_Articulo");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.MaestroArticuloSucursal)
                    .HasForeignKey(d => d.IdSucursal)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<MantenimientoActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdMantenimientoActivoFijo)
                    .HasName("PK_MantenimientoActivoFijo");

                entity.HasIndex(e => e.IdEmpleado)
                    .HasName("IX_MantenimientoActivoFijo_IdEmpleado");

                entity.Property(e => e.Observaciones)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Valor).HasColumnType("decimal");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.MantenimientoActivoFijo)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_MantenimientoActivoFijo_Empleado");

                entity.HasOne(d => d.IdRecepcionActivoFijoDetalleNavigation)
                    .WithMany(p => p.MantenimientoActivoFijo)
                    .HasForeignKey(d => d.IdRecepcionActivoFijoDetalle)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_MantenimientoActivoFijo_RecepcionActivoFijoDetalle");
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
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<MaterialApoyo>(entity =>
            {
                entity.HasKey(e => e.IdMaterialApoyo)
                    .HasName("PK248");

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
                    .HasMaxLength(200);

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

            modelBuilder.Entity<MotivoAlta>(entity =>
            {
                entity.HasKey(e => e.IdMotivoAlta)
                    .HasName("PK_MotivoAlta");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<MotivoAsiento>(entity =>
            {
                entity.HasKey(e => e.IdMotivoAsiento)
                    .HasName("PK165");

                entity.HasIndex(e => e.IdConfiguracionContabilidad)
                    .HasName("Ref309469");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(200)");

                entity.HasOne(d => d.IdConfiguracionContabilidadNavigation)
                    .WithMany(p => p.MotivoAsiento)
                    .HasForeignKey(d => d.IdConfiguracionContabilidad)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefConfiguracionContabilidad469");
            });

            modelBuilder.Entity<MotivoBaja>(entity =>
            {
                entity.HasKey(e => e.IdMotivoBaja)
                    .HasName("PK_ActivoFijoMotivoBaja");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<MotivoRecepcionArticulos>(entity =>
            {
                entity.HasKey(e => e.IdMotivoRecepcionArticulos)
                    .HasName("PK_MotivoRecepcionArticulos");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<MotivoSalidaArticulos>(entity =>
            {
                entity.HasKey(e => e.IdMotivoSalidaArticulos)
                    .HasName("PK_MotivoSalidaArticulos");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<MotivoTransferencia>(entity =>
            {
                entity.HasKey(e => e.IdMotivoTransferencia)
                    .HasName("PK_MotivoTransferencia");

                entity.Property(e => e.MotivoTransferencia1)
                    .IsRequired()
                    .HasColumnName("Motivo_Transferencia")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<MotivoTraslado>(entity =>
            {
                entity.HasKey(e => e.IdMotivoTraslado)
                    .HasName("PK_MotivoTraslado");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<MovilizacionActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdMovilizacionActivoFijo)
                    .HasName("PK_MovilizacionActivoFijo");

                entity.HasOne(d => d.IdEmpleadoAutorizadoNavigation)
                    .WithMany(p => p.MovilizacionActivoFijoIdEmpleadoAutorizadoNavigation)
                    .HasForeignKey(d => d.IdEmpleadoAutorizado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_MovilizacionActivoFijo_Empleado2");

                entity.HasOne(d => d.IdEmpleadoResponsableNavigation)
                    .WithMany(p => p.MovilizacionActivoFijoIdEmpleadoResponsableNavigation)
                    .HasForeignKey(d => d.IdEmpleadoResponsable)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_MovilizacionActivoFijo_Empleado1");

                entity.HasOne(d => d.IdEmpleadoSolicitaNavigation)
                    .WithMany(p => p.MovilizacionActivoFijoIdEmpleadoSolicitaNavigation)
                    .HasForeignKey(d => d.IdEmpleadoSolicita)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_MovilizacionActivoFijo_Empleado");

                entity.HasOne(d => d.IdMotivoTrasladoNavigation)
                    .WithMany(p => p.MovilizacionActivoFijo)
                    .HasForeignKey(d => d.IdMotivoTraslado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_MovilizacionActivoFijo_MotivoTraslado");
            });

            modelBuilder.Entity<MovilizacionActivoFijoDetalle>(entity =>
            {
                entity.HasKey(e => new { e.IdRecepcionActivoFijoDetalle, e.IdMovilizacionActivoFijo })
                    .HasName("PK_MovilizacionActivoFijoDetalle");

                entity.Property(e => e.Observaciones).HasMaxLength(500);

                entity.HasOne(d => d.IdMovilizacionActivoFijoNavigation)
                    .WithMany(p => p.MovilizacionActivoFijoDetalle)
                    .HasForeignKey(d => d.IdMovilizacionActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_MovilizacionActivoFijoDetalle_MovilizacionActivoFijo");

                entity.HasOne(d => d.IdRecepcionActivoFijoDetalleNavigation)
                    .WithMany(p => p.MovilizacionActivoFijoDetalle)
                    .HasForeignKey(d => d.IdRecepcionActivoFijoDetalle)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_MovilizacionActivoFijoDetalle_RecepcionActivoFijoDetalle");
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

            modelBuilder.Entity<OrdenCompra>(entity =>
            {
                entity.HasKey(e => e.IdOrdenCompra)
                    .HasName("PK_OrdenCompra");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.OrdenCompra)
                    .HasForeignKey(d => d.IdBodega)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_OrdenCompra_Bodega");

                entity.HasOne(d => d.IdEmpleadoDevolucionNavigation)
                    .WithMany(p => p.OrdenCompraIdEmpleadoDevolucionNavigation)
                    .HasForeignKey(d => d.IdEmpleadoDevolucion)
                    .HasConstraintName("FK_OrdenCompra_Empleado1");

                entity.HasOne(d => d.IdEmpleadoResponsableNavigation)
                    .WithMany(p => p.OrdenCompraIdEmpleadoResponsableNavigation)
                    .HasForeignKey(d => d.IdEmpleadoResponsable)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_OrdenCompra_Empleado");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.OrdenCompra)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_OrdenCompra_Estado");

                entity.HasOne(d => d.IdFacturaActivoFijoNavigation)
                    .WithMany(p => p.OrdenCompra)
                    .HasForeignKey(d => d.IdFacturaActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_OrdenCompra_FacturaActivoFijo");

                entity.HasOne(d => d.IdMotivoRecepcionArticulosNavigation)
                    .WithMany(p => p.OrdenCompra)
                    .HasForeignKey(d => d.IdMotivoRecepcionArticulos)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_OrdenCompra_MotivoRecepcionArticulos");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.OrdenCompra)
                    .HasForeignKey(d => d.IdProveedor)
                    .HasConstraintName("FK_OrdenCompra_Proveedor");
            });

            modelBuilder.Entity<OrdenCompraDetalles>(entity =>
            {
                entity.HasKey(e => new { e.IdOrdenCompra, e.IdMaestroArticuloSucursal })
                    .HasName("PK_OrdenCompraDetalles_1");

                entity.Property(e => e.ValorUnitario).HasColumnType("decimal");

                entity.HasOne(d => d.IdMaestroArticuloSucursalNavigation)
                    .WithMany(p => p.OrdenCompraDetalles)
                    .HasForeignKey(d => d.IdMaestroArticuloSucursal)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_OrdenCompraDetalles_MaestroArticuloSucursal");

                entity.HasOne(d => d.IdOrdenCompraNavigation)
                    .WithMany(p => p.OrdenCompraDetalles)
                    .HasForeignKey(d => d.IdOrdenCompra)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_OrdenCompraDetalles_OrdenCompra");
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

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.HasOne(d => d.IdIndiceOcupacionalNavigation)
                    .WithMany(p => p.PartidasFase)
                    .HasForeignKey(d => d.IdIndiceOcupacional)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PartidasFase_IndiceOcupacional");

                entity.HasOne(d => d.IdTipoConcursoNavigation)
                    .WithMany(p => p.PartidasFase)
                    .HasForeignKey(d => d.IdTipoConcurso)
                    .HasConstraintName("FK_PartidasFase_TipoConcurso");
            });

            modelBuilder.Entity<PeriodoNomina>(entity =>
            {
                entity.HasKey(e => e.IdPeriodo)
                    .HasName("PK_PeriodoNomina");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.FechaFin).HasColumnType("date");

                entity.Property(e => e.FechaInicio).HasColumnType("date");
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

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Observaciones).HasColumnType("varchar(100)");

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

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.PersonaSustituto)
                    .HasForeignKey(d => d.IdEmpleado)
                    .HasConstraintName("FK_PersonaSustituto_Empleado");

                entity.HasOne(d => d.IdParentescoNavigation)
                    .WithMany(p => p.PersonaSustituto)
                    .HasForeignKey(d => d.IdParentesco)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PersonaSustituto_Parentesco");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.PersonaSustituto)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PersonaSustituto_Persona");
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

            modelBuilder.Entity<PlanCapacitacion>(entity =>
            {
                entity.HasKey(e => e.IdPlanCapacitacion)
                    .HasName("PK_PlanCapacitacion");

                entity.Property(e => e.ApellidoNombre).HasColumnType("varchar(250)");

                entity.Property(e => e.Cedula).HasColumnType("varchar(10)");

                entity.Property(e => e.ClasificacionTema).HasColumnType("varchar(250)");

                entity.Property(e => e.DenominacionPuesto).HasColumnType("varchar(50)");

                entity.Property(e => e.FechaCapacitacionPlanificada).HasColumnType("date");

                entity.Property(e => e.FechaFin).HasColumnType("date");

                entity.Property(e => e.FechaInicio).HasColumnType("date");

                entity.Property(e => e.GrupoOcupacional).HasColumnType("varchar(50)");

                entity.Property(e => e.Institucion).HasColumnType("varchar(50)");

                entity.Property(e => e.Modalidad).HasColumnType("varchar(50)");

                entity.Property(e => e.ModalidadLaboral).HasColumnType("varchar(50)");

                entity.Property(e => e.NivelDesconcentracion).HasColumnType("varchar(50)");

                entity.Property(e => e.NombreCiudad).HasColumnType("varchar(50)");

                entity.Property(e => e.NumeroPartidaPresupuestaria).HasColumnType("varchar(50)");

                entity.Property(e => e.Observacion).HasColumnType("text");

                entity.Property(e => e.Pais).HasColumnType("varchar(50)");

                entity.Property(e => e.PresupuestoIndividual).HasColumnType("decimal");

                entity.Property(e => e.ProductoFinal).HasColumnType("varchar(50)");

                entity.Property(e => e.Provincia).HasColumnType("varchar(50)");

                entity.Property(e => e.RegimenLaboral).HasColumnType("varchar(50)");

                entity.Property(e => e.Sexo).HasColumnType("varchar(50)");

                entity.Property(e => e.TemaCapacitacion).HasColumnType("text");

                entity.Property(e => e.Ubicacion).HasColumnType("varchar(100)");

                entity.Property(e => e.UnidadAdministrativa).HasColumnType("varchar(50)");

                entity.Property(e => e.ValorReal).HasColumnType("decimal");

                entity.HasOne(d => d.AmbitoCapacitacionNavigation)
                    .WithMany(p => p.PlanCapacitacionAmbitoCapacitacionNavigation)
                    .HasForeignKey(d => d.AmbitoCapacitacion)
                    .HasConstraintName("FK_PlanCapacitacion_GeneralCapacitacion2");

                entity.HasOne(d => d.EstadoEventoNavigation)
                    .WithMany(p => p.PlanCapacitacionEstadoEventoNavigation)
                    .HasForeignKey(d => d.EstadoEvento)
                    .HasConstraintName("FK_PlanCapacitacion_GeneralCapacitacion1");

                entity.HasOne(d => d.IdCiudadNavigation)
                    .WithMany(p => p.PlanCapacitacion)
                    .HasForeignKey(d => d.IdCiudad)
                    .HasConstraintName("FK_PlanCapacitacion_Ciudad");

                entity.HasOne(d => d.IdGestionPlanCapacitacionNavigation)
                    .WithMany(p => p.PlanCapacitacion)
                    .HasForeignKey(d => d.IdGestionPlanCapacitacion)
                    .HasConstraintName("FK_PlanCapacitacion_PlanCapacitacion1");

                entity.HasOne(d => d.IdProveedorCapacitacionesNavigation)
                    .WithMany(p => p.PlanCapacitacion)
                    .HasForeignKey(d => d.IdProveedorCapacitaciones)
                    .HasConstraintName("FK_PlanCapacitacion_CapacitacionProveedor");

                entity.HasOne(d => d.NombreEventoNavigation)
                    .WithMany(p => p.PlanCapacitacionNombreEventoNavigation)
                    .HasForeignKey(d => d.NombreEvento)
                    .HasConstraintName("FK_PlanCapacitacion_GeneralCapacitacion5");

                entity.HasOne(d => d.TipoCapacitacionNavigation)
                    .WithMany(p => p.PlanCapacitacionTipoCapacitacionNavigation)
                    .HasForeignKey(d => d.TipoCapacitacion)
                    .HasConstraintName("FK_PlanCapacitacion_GeneralCapacitacion");

                entity.HasOne(d => d.TipoEvaluacionNavigation)
                    .WithMany(p => p.PlanCapacitacionTipoEvaluacionNavigation)
                    .HasForeignKey(d => d.TipoEvaluacion)
                    .HasConstraintName("FK_PlanCapacitacion_GeneralCapacitacion4");

                entity.HasOne(d => d.TipoEventoNavigation)
                    .WithMany(p => p.PlanCapacitacionTipoEventoNavigation)
                    .HasForeignKey(d => d.TipoEvento)
                    .HasConstraintName("FK_PlanCapacitacion_GeneralCapacitacion3");
            });

            modelBuilder.Entity<PlanificacionHe>(entity =>
            {
                entity.HasKey(e => e.IdPlanificacionHe)
                    .HasName("PK179");

                entity.ToTable("PlanificacionHE");

                entity.Property(e => e.IdPlanificacionHe).HasColumnName("IdPlanificacionHE");

                entity.Property(e => e.NoHoras).HasColumnType("decimal");
            });

            modelBuilder.Entity<PolizaSeguroActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdActivo)
                    .HasName("PK_PolizaSeguroActivFijo");

                entity.Property(e => e.IdActivo).ValueGeneratedNever();

                entity.Property(e => e.NumeroPoliza).HasMaxLength(200);

                entity.HasOne(d => d.IdActivoNavigation)
                    .WithOne(p => p.PolizaSeguroActivoFijo)
                    .HasForeignKey<PolizaSeguroActivoFijo>(d => d.IdActivo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PolizaSeguroActivoFijo_ActivoFijo");

                entity.HasOne(d => d.IdCompaniaSeguroNavigation)
                    .WithMany(p => p.PolizaSeguroActivoFijo)
                    .HasForeignKey(d => d.IdCompaniaSeguro)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PolizaSeguroActivoFijo_CompaniaSeguro");

                entity.HasOne(d => d.IdSubramoNavigation)
                    .WithMany(p => p.PolizaSeguroActivoFijo)
                    .HasForeignKey(d => d.IdSubramo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PolizaSeguroActivoFijo_Subramo");
            });

            modelBuilder.Entity<Pregunta>(entity =>
            {
                entity.HasKey(e => e.IdPregunta)
                    .HasName("PK232");

                entity.Property(e => e.Nombre).HasColumnType("varchar(500)");

                entity.HasOne(d => d.IdEvaluacionInduccionNavigation)
                    .WithMany(p => p.Pregunta)
                    .HasForeignKey(d => d.IdEvaluacionInduccion)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefEvaluacionInducion353");
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
                    .HasName("PK234");

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

            modelBuilder.Entity<Presupuesto>(entity =>
            {
                entity.HasKey(e => e.IdPresupuesto)
                    .HasName("PK_Presupuesto");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.NumeroPartidaPresupuestaria).HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.Presupuesto)
                    .HasForeignKey(d => d.IdSucursal)
                    .HasConstraintName("FK_Presupuesto_Sucursal");
            });

            modelBuilder.Entity<Proceso>(entity =>
            {
                entity.HasKey(e => e.IdProceso)
                    .HasName("PK54");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<ProcesoJudicialActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdProcesoJudicialActivoFijo)
                    .HasName("PK_ProcesoJudicialActivoFijo");

                entity.Property(e => e.NumeroDenuncia)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.IdRecepcionActivoFijoDetalleNavigation)
                    .WithMany(p => p.ProcesoJudicialActivoFijo)
                    .HasForeignKey(d => d.IdRecepcionActivoFijoDetalle)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ProcesoJudicialActivoFijo_RecepcionActivoFijoDetalle");
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

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor)
                    .HasName("PK_Proveedor");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Cargo).HasMaxLength(200);

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Identificacion)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Observaciones).HasMaxLength(200);

                entity.Property(e => e.RazonSocial)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Telefono).HasMaxLength(200);

                entity.HasOne(d => d.IdLineaServicioNavigation)
                    .WithMany(p => p.Proveedor)
                    .HasForeignKey(d => d.IdLineaServicio)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Proveedor_LineaServicio");
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

                entity.Property(e => e.Valor).HasColumnType("decimal");

                entity.HasOne(d => d.IdTipoProvisionNavigation)
                    .WithMany(p => p.Provisiones)
                    .HasForeignKey(d => d.IdTipoProvision)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefTipoProvision441");
            });

            modelBuilder.Entity<Quejas>(entity =>
            {
                entity.HasKey(e => e.IdQueja)
                    .HasName("PK_Quejas");

                entity.Property(e => e.Apellido).HasColumnType("varchar(100)");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(1000)");

                entity.Property(e => e.Nombre).HasColumnType("varchar(100)");

                entity.Property(e => e.NumeroFormulario).HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Quejas)
                    .HasForeignKey(d => d.IdEmpleado)
                    .HasConstraintName("FK_Quejas_Empleado");

                entity.HasOne(d => d.IdEval001Navigation)
                    .WithMany(p => p.Quejas)
                    .HasForeignKey(d => d.IdEval001)
                    .HasConstraintName("FK_Quejas_Eval001");
            });

            modelBuilder.Entity<Ramo>(entity =>
            {
                entity.HasKey(e => e.IdRamo)
                    .HasName("PK_Ramo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<RealizaExamenInduccion>(entity =>
            {
                entity.HasKey(e => e.IdRealizaExamenInduccion)
                    .HasName("PK235");

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

                entity.HasIndex(e => e.IdProveedor)
                    .HasName("IX_RecepcionActivoFijo_IdProveedor");

                entity.Property(e => e.OrdenCompra)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.IdFondoFinanciamientoNavigation)
                    .WithMany(p => p.RecepcionActivoFijo)
                    .HasForeignKey(d => d.IdFondoFinanciamiento)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RecepcionActivoFijo_FondoFinanciamiento");

                entity.HasOne(d => d.IdMotivoAltaNavigation)
                    .WithMany(p => p.RecepcionActivoFijo)
                    .HasForeignKey(d => d.IdMotivoAlta)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RecepcionActivoFijo_MotivoAlta");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.RecepcionActivoFijo)
                    .HasForeignKey(d => d.IdProveedor)
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

                entity.Property(e => e.NumeroChasis).HasMaxLength(200);

                entity.Property(e => e.NumeroClaveCatastral).HasMaxLength(200);

                entity.Property(e => e.NumeroMotor).HasMaxLength(200);

                entity.Property(e => e.Placa).HasMaxLength(200);

                entity.Property(e => e.Serie).HasMaxLength(200);

                entity.HasOne(d => d.IdActivoFijoNavigation)
                    .WithMany(p => p.RecepcionActivoFijoDetalle)
                    .HasForeignKey(d => d.IdActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RecepcionActivoFijoDetalle_ActivoFijo");

                entity.HasOne(d => d.IdCodigoActivoFijoNavigation)
                    .WithMany(p => p.RecepcionActivoFijoDetalle)
                    .HasForeignKey(d => d.IdCodigoActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RecepcionActivoFijoDetalle_CodigoActivoFijo");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.RecepcionActivoFijoDetalle)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IdRecepcionActivoFijoNavigation)
                    .WithMany(p => p.RecepcionActivoFijoDetalle)
                    .HasForeignKey(d => d.IdRecepcionActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RecepcionActivoFijoDetalle_RecepcionActivoFijo");
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

            modelBuilder.Entity<ReliquidacionViatico>(entity =>
            {
                entity.HasKey(e => e.IdReliquidacionViatico)
                    .HasName("PK_RequlidacionViatico");

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.ValorEstimado).HasColumnType("decimal");

                entity.HasOne(d => d.IdPresupuestoNavigation)
                    .WithMany(p => p.ReliquidacionViatico)
                    .HasForeignKey(d => d.IdPresupuesto)
                    .HasConstraintName("FK_ReliquidacionViatico_Presupuesto");

                entity.HasOne(d => d.IdSolicitudViaticoNavigation)
                    .WithMany(p => p.ReliquidacionViatico)
                    .HasForeignKey(d => d.IdSolicitudViatico)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ReliquidacionViatico_SolicitudViatico");
            });

            modelBuilder.Entity<ReportadoNomina>(entity =>
            {
                entity.HasKey(e => e.IdReportadoNomina)
                    .HasName("PK_ReportadoNomina");

                entity.Property(e => e.CodigoConcepto).HasColumnType("varchar(10)");

                entity.Property(e => e.IdentificacionEmpleado).HasColumnType("varchar(20)");

                entity.Property(e => e.NombreEmpleado).HasColumnType("varchar(200)");

                entity.HasOne(d => d.IdCalculoNominaNavigation)
                    .WithMany(p => p.ReportadoNomina)
                    .HasForeignKey(d => d.IdCalculoNomina)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ReportadoNomina_CalculoNomina");
            });

            modelBuilder.Entity<RequerimientoArticulos>(entity =>
            {
                entity.HasKey(e => e.IdRequerimientoArticulos)
                    .HasName("PK_RequerimientoArticulos");

                entity.Property(e => e.Observaciones).HasMaxLength(500);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.RequerimientoArticulos)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RequerimientoArticulos_Estado");

                entity.HasOne(d => d.IdFuncionarioSolicitanteNavigation)
                    .WithMany(p => p.RequerimientoArticulos)
                    .HasForeignKey(d => d.IdFuncionarioSolicitante)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RequerimientoArticulos_Empleado");
            });

            modelBuilder.Entity<RequerimientosArticulosDetalles>(entity =>
            {
                entity.HasKey(e => new { e.IdRequerimientosArticulos, e.IdMaestroArticuloSucursal })
                    .HasName("PK_RequerimientosArticulosDetalles_1");

                entity.HasOne(d => d.IdMaestroArticuloSucursalNavigation)
                    .WithMany(p => p.RequerimientosArticulosDetalles)
                    .HasForeignKey(d => d.IdMaestroArticuloSucursal)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RequerimientosArticulosDetalles_MaestroArticuloSucursal");

                entity.HasOne(d => d.IdRequerimientosArticulosNavigation)
                    .WithMany(p => p.RequerimientosArticulosDetalles)
                    .HasForeignKey(d => d.IdRequerimientosArticulos)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RequerimientosArticulosDetalles_RequerimientoArticulos");
            });

            modelBuilder.Entity<RequisitosNoCumple>(entity =>
            {
                entity.HasKey(e => e.IdRequisitosNoCumple)
                    .HasName("PK199");

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

            modelBuilder.Entity<RevalorizacionActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdRevalorizacionActivoFijo)
                    .HasName("PK_RevalorizacionActivoFijo");

                entity.Property(e => e.ValorCompra).HasColumnType("decimal");

                entity.HasOne(d => d.IdRecepcionActivoFijoDetalleNavigation)
                    .WithMany(p => p.RevalorizacionActivoFijo)
                    .HasForeignKey(d => d.IdRecepcionActivoFijoDetalle)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RevalorizacionActivoFijo_RecepcionActivoFijoDetalle");
            });

            modelBuilder.Entity<Rmu>(entity =>
            {
                entity.HasKey(e => e.IdRmu)
                    .HasName("PK150");

                entity.ToTable("RMU");

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

            modelBuilder.Entity<SalidaArticulos>(entity =>
            {
                entity.HasKey(e => e.IdSalidaArticulos)
                    .HasName("PK_SalidaArticulos");

                entity.Property(e => e.DescripcionMotivo).HasMaxLength(500);

                entity.HasOne(d => d.IdEmpleadoDespachoNavigation)
                    .WithMany(p => p.SalidaArticulosIdEmpleadoDespachoNavigation)
                    .HasForeignKey(d => d.IdEmpleadoDespacho)
                    .HasConstraintName("FK_SalidaArticulos_Empleado1");

                entity.HasOne(d => d.IdEmpleadoRealizaBajaNavigation)
                    .WithMany(p => p.SalidaArticulosIdEmpleadoRealizaBajaNavigation)
                    .HasForeignKey(d => d.IdEmpleadoRealizaBaja)
                    .HasConstraintName("FK_SalidaArticulos_Empleado");

                entity.HasOne(d => d.IdMotivoSalidaArticulosNavigation)
                    .WithMany(p => p.SalidaArticulos)
                    .HasForeignKey(d => d.IdMotivoSalidaArticulos)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SalidaArticulos_MotivoSalidaArticulos");

                entity.HasOne(d => d.IdProveedorDevolucionNavigation)
                    .WithMany(p => p.SalidaArticulos)
                    .HasForeignKey(d => d.IdProveedorDevolucion)
                    .HasConstraintName("FK_SalidaArticulos_Proveedor");

                entity.HasOne(d => d.IdRequerimientoArticulosNavigation)
                    .WithMany(p => p.SalidaArticulos)
                    .HasForeignKey(d => d.IdRequerimientoArticulos)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SalidaArticulos_RequerimientoArticulos");
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

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(500)");

                entity.HasOne(d => d.IdDependenciaNavigation)
                    .WithMany(p => p.SituacionPropuesta)
                    .HasForeignKey(d => d.IdDependencia)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SituacionPropuesta_Dependencia");

                entity.HasOne(d => d.IdGrupoOcupacionalNavigation)
                    .WithMany(p => p.SituacionPropuesta)
                    .HasForeignKey(d => d.IdGrupoOcupacional)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SituacionPropuesta_GrupoOcupacional");

                entity.HasOne(d => d.IdProcesoNavigation)
                    .WithMany(p => p.SituacionPropuesta)
                    .HasForeignKey(d => d.IdProceso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SituacionPropuesta_Proceso");

                entity.HasOne(d => d.IdRolPuestoNavigation)
                    .WithMany(p => p.SituacionPropuesta)
                    .HasForeignKey(d => d.IdRolPuesto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SituacionPropuesta_RolPuesto");
            });

            modelBuilder.Entity<SolicitudAcumulacionDecimos>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudAcumulacionDecimos)
                    .HasName("PK110");

                entity.Property(e => e.FechaSolicitud).HasColumnType("date");
            });

            modelBuilder.Entity<SolicitudAnticipo>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudAnticipo)
                    .HasName("PK109");

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

                entity.Property(e => e.Estado).HasDefaultValueSql("0");

                entity.Property(e => e.FechaAprobado).HasColumnType("datetime");

                entity.Property(e => e.FechaDesde).HasColumnType("datetime");

                entity.Property(e => e.FechaHasta).HasColumnType("datetime");

                entity.Property(e => e.FechaSolicitud).HasColumnType("date");

                entity.Property(e => e.Motivo).HasColumnType("varchar(1000)");

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

                entity.Property(e => e.FechaDesde).HasColumnType("date");

                entity.Property(e => e.FechaHasta).HasColumnType("date");

                entity.Property(e => e.FechaSolicitud).HasColumnType("date");

                entity.Property(e => e.Observaciones).HasMaxLength(150);
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

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.FechaLlegada).HasColumnType("date");

                entity.Property(e => e.FechaSalida).HasColumnType("date");

                entity.Property(e => e.FechaSolicitud).HasColumnType("date");

                entity.Property(e => e.Observacion).HasMaxLength(250);

                entity.Property(e => e.ValorEstimado).HasColumnType("decimal");

                entity.HasOne(d => d.IdCiudadNavigation)
                    .WithMany(p => p.SolicitudViatico)
                    .HasForeignKey(d => d.IdCiudad)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SolicitudViatico_Ciudad");

                entity.HasOne(d => d.IdConfiguracionViaticoNavigation)
                    .WithMany(p => p.SolicitudViatico)
                    .HasForeignKey(d => d.IdConfiguracionViatico)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SolicitudViatico_ConfiguracionViatico");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.SolicitudViatico)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SolicitudViatico_Empleado");

                entity.HasOne(d => d.IdFondoFinanciamientoNavigation)
                    .WithMany(p => p.SolicitudViatico)
                    .HasForeignKey(d => d.IdFondoFinanciamiento)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SolicitudViatico_FondoFinanciamiento");
            });

            modelBuilder.Entity<SriDetalle>(entity =>
            {
                entity.HasKey(e => e.IdSriDetalle)
                    .HasName("PK_SriDetalle");

                entity.HasOne(d => d.IdSriNavigation)
                    .WithMany(p => p.SriDetalle)
                    .HasForeignKey(d => d.IdSri)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SriDetalle_SriNomina");
            });

            modelBuilder.Entity<SriNomina>(entity =>
            {
                entity.HasKey(e => e.IdSri)
                    .HasName("PK_SriNomina");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<SubClaseActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdSubClaseActivoFijo)
                    .HasName("PK_SubClaseActivoFijo");

                entity.HasIndex(e => e.IdClaseActivoFijo)
                    .HasName("IX_SubClaseActivoFijo_IdClaseActivoFijo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);

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
                    .HasColumnType("varchar(200)");

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

            modelBuilder.Entity<Subramo>(entity =>
            {
                entity.HasKey(e => e.IdSubramo)
                    .HasName("PK_Subramo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.IdRamoNavigation)
                    .WithMany(p => p.Subramo)
                    .HasForeignKey(d => d.IdRamo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Subramo_Ramo");
            });

            modelBuilder.Entity<Sucursal>(entity =>
            {
                entity.HasKey(e => e.IdSucursal)
                    .HasName("PK_Sucursal");

                entity.HasIndex(e => e.IdCiudad)
                    .HasName("IX_Sucursal_IdCiudad");

                entity.Property(e => e.Direccion).HasColumnType("varchar(100)");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdCiudadNavigation)
                    .WithMany(p => p.Sucursal)
                    .HasForeignKey(d => d.IdCiudad)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TeconceptoNomina>(entity =>
            {
                entity.HasKey(e => e.IdTeconcepto)
                    .HasName("PK5");

                entity.ToTable("TEConceptoNomina");

                entity.Property(e => e.IdTeconcepto).HasColumnName("IdTEConcepto");

                entity.HasOne(d => d.IdConceptoNavigation)
                    .WithMany(p => p.TeconceptoNomina)
                    .HasForeignKey(d => d.IdConcepto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefConceptoNomina10");
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
            });

            modelBuilder.Entity<TipoActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdTipoActivoFijo)
                    .HasName("PK_TipoActivoFijo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<TipoArticulo>(entity =>
            {
                entity.HasKey(e => e.IdTipoArticulo)
                    .HasName("PK_TipoArticulo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);
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

            modelBuilder.Entity<TipoDeGastoPersonal>(entity =>
            {
                entity.HasKey(e => e.IdTipoGastoPersonal)
                    .HasName("PK_TipoDeGastoPersonal");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.NombreConstante).HasColumnType("varchar(50)");
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
                    .HasColumnType("varchar(255)");
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

            modelBuilder.Entity<TipoUtilizacionAlta>(entity =>
            {
                entity.HasKey(e => e.IdTipoUtilizacionAlta)
                    .HasName("PK_TipoUtilizacionAlta");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);
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

                entity.HasIndex(e => e.IdEmpleadoResponsableEnvio)
                    .HasName("IX_TransferenciaActivoFijo_IdEmpleado");

                entity.HasIndex(e => e.IdEmpleadoResponsableRecibo)
                    .HasName("Ref15171");

                entity.Property(e => e.IdMotivoTransferencia).HasColumnName("idMotivoTransferencia");

                entity.Property(e => e.Observaciones).HasColumnType("text");

                entity.HasOne(d => d.IdEmpleadoResponsableEnvioNavigation)
                    .WithMany(p => p.TransferenciaActivoFijoIdEmpleadoResponsableEnvioNavigation)
                    .HasForeignKey(d => d.IdEmpleadoResponsableEnvio)
                    .HasConstraintName("FK_TransferenciaActivoFijo_Empleado1");

                entity.HasOne(d => d.IdEmpleadoResponsableReciboNavigation)
                    .WithMany(p => p.TransferenciaActivoFijoIdEmpleadoResponsableReciboNavigation)
                    .HasForeignKey(d => d.IdEmpleadoResponsableRecibo)
                    .HasConstraintName("FK_TransferenciaActivoFijo_Empleado2");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.TransferenciaActivoFijo)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TransferenciaActivoFijo_Estado");

                entity.HasOne(d => d.IdMotivoTransferenciaNavigation)
                    .WithMany(p => p.TransferenciaActivoFijo)
                    .HasForeignKey(d => d.IdMotivoTransferencia)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TransferenciaActivoFijo_MotivoTransferencia");
            });

            modelBuilder.Entity<TransferenciaActivoFijoDetalle>(entity =>
            {
                entity.HasKey(e => new { e.IdRecepcionActivoFijoDetalle, e.IdTransferenciaActivoFijo })
                    .HasName("PK_TransferenciaActivoFijoDetalle");

                entity.HasOne(d => d.IdCodigoActivoFijoNavigation)
                    .WithMany(p => p.TransferenciaActivoFijoDetalle)
                    .HasForeignKey(d => d.IdCodigoActivoFijo)
                    .HasConstraintName("FK_TransferenciaActivoFijoDetalle_CodigoActivoFijo");

                entity.HasOne(d => d.IdRecepcionActivoFijoDetalleNavigation)
                    .WithMany(p => p.TransferenciaActivoFijoDetalle)
                    .HasForeignKey(d => d.IdRecepcionActivoFijoDetalle)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TransferenciaActivoFijoDetalle_RecepcionActivoFijoDetalle");

                entity.HasOne(d => d.IdTransferenciaActivoFijoNavigation)
                    .WithMany(p => p.TransferenciaActivoFijoDetalle)
                    .HasForeignKey(d => d.IdTransferenciaActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TransferenciaActivoFijoDetalle_TransferenciaActivoFijo");

                entity.HasOne(d => d.IdUbicacionActivoFijoDestinoNavigation)
                    .WithMany(p => p.TransferenciaActivoFijoDetalleIdUbicacionActivoFijoDestinoNavigation)
                    .HasForeignKey(d => d.IdUbicacionActivoFijoDestino)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TransferenciaActivoFijoDetalle_UbicacionActivoFijo1");

                entity.HasOne(d => d.IdUbicacionActivoFijoOrigenNavigation)
                    .WithMany(p => p.TransferenciaActivoFijoDetalleIdUbicacionActivoFijoOrigenNavigation)
                    .HasForeignKey(d => d.IdUbicacionActivoFijoOrigen)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TransferenciaActivoFijoDetalle_UbicacionActivoFijo");
            });

            modelBuilder.Entity<TrayectoriaLaboral>(entity =>
            {
                entity.HasKey(e => e.IdTrayectoriaLaboral)
                    .HasName("PK271");

                entity.Property(e => e.AreaAsignada).HasColumnType("varchar(100)");

                entity.Property(e => e.DescripcionFunciones).HasColumnType("varchar(250)");

                entity.Property(e => e.Empresa).HasColumnType("varchar(100)");

                entity.Property(e => e.FechaFin).HasColumnType("date");

                entity.Property(e => e.FechaInicio).HasColumnType("date");

                entity.Property(e => e.FormaIngreso).HasColumnType("varchar(100)");

                entity.Property(e => e.MotivoSalida).HasColumnType("varchar(1000)");

                entity.Property(e => e.PuestoTrabajo).HasColumnType("varchar(250)");

                entity.Property(e => e.TipoInstitucion).HasColumnType("varchar(100)");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.TrayectoriaLaboral)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("RefPersona422");
            });

            modelBuilder.Entity<UbicacionActivoFijo>(entity =>
            {
                entity.HasKey(e => e.IdUbicacionActivoFijo)
                    .HasName("PK_UbicacionActivoFijo");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.UbicacionActivoFijo)
                    .HasForeignKey(d => d.IdBodega)
                    .HasConstraintName("FK_UbicacionActivoFijo_Bodega");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.UbicacionActivoFijo)
                    .HasForeignKey(d => d.IdEmpleado)
                    .HasConstraintName("FK_UbicacionActivoFijo_Empleado");

                entity.HasOne(d => d.IdLibroActivoFijoNavigation)
                    .WithMany(p => p.UbicacionActivoFijo)
                    .HasForeignKey(d => d.IdLibroActivoFijo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UbicacionActivoFijo_LibroActivoFijo");

                entity.HasOne(d => d.IdRecepcionActivoFijoDetalleNavigation)
                    .WithMany(p => p.UbicacionActivoFijo)
                    .HasForeignKey(d => d.IdRecepcionActivoFijoDetalle)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UbicacionActivoFijo_RecepcionActivoFijoDetalle");
            });

            modelBuilder.Entity<UnidadMedida>(entity =>
            {
                entity.HasKey(e => e.IdUnidadMedida)
                    .HasName("PK_UnidadMedida");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<VacacionRelacionLaboral>(entity =>
            {
                entity.HasKey(e => e.IdVacacionRelacionLaboral)
                    .HasName("PK_VacacionRelacionLaboral");

                entity.Property(e => e.IncrementoApartirPeriodoFiscal).HasColumnName("IncrementoAPartirPeriodoFiscal");

                entity.HasOne(d => d.IdRegimenLaboralNavigation)
                    .WithMany(p => p.VacacionRelacionLaboral)
                    .HasForeignKey(d => d.IdRegimenLaboral)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_VacacionRelacionLaboral_RelacionLaboral");
            });

            modelBuilder.Entity<VacacionesEmpleado>(entity =>
            {
                entity.HasKey(e => e.IdVacaciones)
                    .HasName("PK_VacacionesEmpleado");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.VacacionesEmpleado)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_VacacionesEmpleado_Empleado");
            });

            modelBuilder.Entity<ValidacionInmediatoSuperior>(entity =>
            {
                entity.HasKey(e => e.IdValidacionJefe)
                    .HasName("PK193");

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