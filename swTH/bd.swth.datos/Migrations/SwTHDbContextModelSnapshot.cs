using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using bd.swth.datos;

namespace bd.swth.datos.Migrations
{
    [DbContext(typeof(SwTHDbContext))]
    partial class SwTHDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("bd.swth.entidades.Negocio.AccionPersonal", b =>
                {
                    b.Property<int>("IdAccionPersonal")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Explicacion")
                        .IsRequired();

                    b.Property<DateTime>("Fecha");

                    b.Property<DateTime>("FechaRige");

                    b.Property<DateTime>("FechaRigeHasta");

                    b.Property<int>("IdEmpleado");

                    b.Property<int>("IdTipoAccionPersonal");

                    b.Property<int>("NoDias");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Solicitud")
                        .IsRequired();

                    b.HasKey("IdAccionPersonal")
                        .HasName("PK_AccionPersonal");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_AccionPersonal_IdEmpleado");

                    b.HasIndex("IdTipoAccionPersonal")
                        .HasName("IX_AccionPersonal_IdTipoAccionPersonal");

                    b.ToTable("AccionPersonal");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ActividadesAnalisisOcupacional", b =>
                {
                    b.Property<int>("IdActividadesAnalisisOcupacional")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Actividades");

                    b.Property<bool>("Cumple");

                    b.Property<int>("IdFormularioAnalisisOcupacional");

                    b.HasKey("IdActividadesAnalisisOcupacional")
                        .HasName("PK_ActividadesAnalisisOcupacional");

                    b.HasIndex("IdFormularioAnalisisOcupacional")
                        .HasName("IX_ActividadesAnalisisOcupacional_IdFormularioAnalisisOcupacional");

                    b.ToTable("ActividadesAnalisisOcupacional");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ActividadesEsenciales", b =>
                {
                    b.Property<int>("ActividadesEsencialesId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ActividadesEsencialesId");

                    b.ToTable("ActividadesEsenciales");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ActividadesGestionCambio", b =>
                {
                    b.Property<int>("IdActividadesGestionCambio")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired();

                    b.Property<DateTime>("FechaFin");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<int>("IdPlanGestionCambio");

                    b.Property<int>("Indicador");

                    b.Property<bool>("Porciento");

                    b.HasKey("IdActividadesGestionCambio")
                        .HasName("PK_ActividadesGestionCambio");

                    b.HasIndex("IdPlanGestionCambio")
                        .HasName("IX_ActividadesGestionCambio_IdPlanGestionCambio");

                    b.ToTable("ActividadesGestionCambio");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.AdministracionTalentoHumano", b =>
                {
                    b.Property<int>("IdAdministracionTalentoHumano")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EmpleadoResponsable");

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("IdFormularioAnalisisOcupacional")
                        .HasColumnName("idFormularioAnalisisOcupacional");

                    b.Property<int>("IdRolPuesto")
                        .HasColumnName("idRolPuesto");

                    b.Property<bool>("SeAplicaraPolitica");

                    b.HasKey("IdAdministracionTalentoHumano")
                        .HasName("PK_AdministracionTalentoHumano");

                    b.HasIndex("EmpleadoResponsable")
                        .HasName("IX_AdministracionTalentoHumano_EmpleadoIdEmpleado");

                    b.HasIndex("IdFormularioAnalisisOcupacional")
                        .HasName("IX_AdministracionTalentoHumano_idFormularioAnalisisOcupacional");

                    b.HasIndex("IdRolPuesto")
                        .HasName("IX_AdministracionTalentoHumano_idRolPuesto");

                    b.ToTable("AdministracionTalentoHumano");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.AnoExperiencia", b =>
                {
                    b.Property<int>("IdAnoExperiencia")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdAnoExperiencia")
                        .HasName("PK_AnoExperiencia");

                    b.ToTable("AnoExperiencia");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.AprobacionViatico", b =>
                {
                    b.Property<int>("IdAprobacionViatico")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdEmpleado");

                    b.Property<int>("IdSolicitudViatico");

                    b.Property<decimal>("ValorADescontar")
                        .HasColumnName("ValorADescontar")
                        .HasColumnType("decimal");

                    b.Property<decimal>("ValorJustificado")
                        .HasColumnType("decimal");

                    b.HasKey("IdAprobacionViatico")
                        .HasName("PK_AprobacionViatico");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_AprobacionViatico_IdEmpleado");

                    b.HasIndex("IdSolicitudViatico")
                        .HasName("IX_AprobacionViatico_IdSolicitudViatico");

                    b.ToTable("AprobacionViatico");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.AreaConocimiento", b =>
                {
                    b.Property<int>("IdAreaConocimiento")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("IdAreaConocimiento")
                        .HasName("PK_AreaConocimiento");

                    b.ToTable("AreaConocimiento");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.AvanceGestionCambio", b =>
                {
                    b.Property<int>("IdAvanceGestionCambio")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("IdActividadesGestionCambio");

                    b.Property<int>("Indicadorreal");

                    b.HasKey("IdAvanceGestionCambio")
                        .HasName("PK_AvanceGestionCambio");

                    b.HasIndex("IdActividadesGestionCambio")
                        .HasName("IX_AvanceGestionCambio_IdActividadesGestionCambio");

                    b.ToTable("AvanceGestionCambio");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.BrigadaSSO", b =>
                {
                    b.Property<int>("IdBrigadaSSO")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IdBrigadaSSO");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdBrigadaSSO")
                        .HasName("PK_BrigadaSSO");

                    b.ToTable("BrigadaSSO");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.BrigadaSSORol", b =>
                {
                    b.Property<int>("IdBrigadaSSORol")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IdBrigadaSSORol");

                    b.Property<int>("IdBrigadaSSO")
                        .HasColumnName("IdBrigadaSSO");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdBrigadaSSORol")
                        .HasName("PK_BrigadaSSORol");

                    b.HasIndex("IdBrigadaSSO")
                        .HasName("IX_BrigadaSSORol_IdBrigadaSSO");

                    b.ToTable("BrigadaSSORol");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.CandidatoConcurso", b =>
                {
                    b.Property<int>("IdCandidatoConcurso")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CodigoSocioEmpleo")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<bool>("Descartado");

                    b.Property<bool>("Ganador");

                    b.Property<int>("IdCanditato");

                    b.Property<int>("IdPartidasFase");

                    b.HasKey("IdCandidatoConcurso")
                        .HasName("PK_CandidatoConcurso");

                    b.HasIndex("IdCanditato")
                        .HasName("IX_CandidatoConcurso_IdCanditato");

                    b.HasIndex("IdPartidasFase")
                        .HasName("IX_CandidatoConcurso_IdPartidasFase");

                    b.ToTable("CandidatoConcurso");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Canditato", b =>
                {
                    b.Property<int>("IdCanditato")
                        .ValueGeneratedOnAdd();

                    b.HasKey("IdCanditato")
                        .HasName("PK_Canditato");

                    b.ToTable("Canditato");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Capacitacion", b =>
                {
                    b.Property<int>("IdCapacitacion")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdCapacitacion")
                        .HasName("PK_Capacitacion");

                    b.ToTable("Capacitacion");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.CapacitacionAreaConocimiento", b =>
                {
                    b.Property<int>("IdCapacitacionAreaConocimiento")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdCapacitacionAreaConocimiento")
                        .HasName("PK_CapacitacionAreaConocimiento");

                    b.ToTable("CapacitacionAreaConocimiento");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.CapacitacionEncuesta", b =>
                {
                    b.Property<string>("IdCapacitacionEncuesta")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("IdCapacitacionRecibida");

                    b.Property<int>("IdEmpleado");

                    b.HasKey("IdCapacitacionEncuesta")
                        .HasName("PK_CapacitacionEncuesta");

                    b.HasIndex("IdCapacitacionRecibida")
                        .HasName("IX_CapacitacionEncuesta_IdCapacitacionRecibida");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_CapacitacionEncuesta_IdEmpleado");

                    b.ToTable("CapacitacionEncuesta");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.CapacitacionModalidad", b =>
                {
                    b.Property<int>("IdCapacitacionModalidad")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdCapacitacionModalidad")
                        .HasName("PK_CapacitacionModalidad");

                    b.ToTable("CapacitacionModalidad");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.CapacitacionPlanificacion", b =>
                {
                    b.Property<int>("IdCapacitacionPlanificacion")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("IdCapacitacionModalidad");

                    b.Property<int>("IdCapacitacionTemario");

                    b.Property<int>("IdEmpleado");

                    b.Property<int>("NumeroHoras");

                    b.Property<decimal>("Presupuesto")
                        .HasColumnType("decimal");

                    b.HasKey("IdCapacitacionPlanificacion")
                        .HasName("PK_CapacitacionPlanificacion");

                    b.HasIndex("IdCapacitacionModalidad")
                        .HasName("IX_CapacitacionPlanificacion_IdCapacitacionModalidad");

                    b.HasIndex("IdCapacitacionTemario")
                        .HasName("IX_CapacitacionPlanificacion_IdCapacitacionTemario");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_CapacitacionPlanificacion_IdEmpleado");

                    b.ToTable("CapacitacionPlanificacion");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.CapacitacionPregunta", b =>
                {
                    b.Property<int>("IdCapacitacionPregunta")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("IdCapacitacionEncuesta")
                        .HasMaxLength(450);

                    b.Property<int>("IdCapacitacionTipoPregunta");

                    b.HasKey("IdCapacitacionPregunta")
                        .HasName("PK_CapacitacionPregunta");

                    b.HasIndex("IdCapacitacionEncuesta")
                        .HasName("IX_CapacitacionPregunta_IdCapacitacionEncuesta");

                    b.HasIndex("IdCapacitacionTipoPregunta")
                        .HasName("IX_CapacitacionPregunta_IdCapacitacionTipoPregunta");

                    b.ToTable("CapacitacionPregunta");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.CapacitacionProveedor", b =>
                {
                    b.Property<int>("IdCapacitacionProveedor")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<int>("IdCapacitacionRecibida");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.HasKey("IdCapacitacionProveedor")
                        .HasName("PK_CapacitacionProveedor");

                    b.HasIndex("IdCapacitacionRecibida")
                        .HasName("IX_CapacitacionProveedor_IdCapacitacionRecibida");

                    b.ToTable("CapacitacionProveedor");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.CapacitacionRecibida", b =>
                {
                    b.Property<int>("IdCapacitacionRecibida")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdCapacitacionTemario");

                    b.Property<int>("IdEmpleado");

                    b.HasKey("IdCapacitacionRecibida")
                        .HasName("PK_CapacitacionRecibida");

                    b.HasIndex("IdCapacitacionTemario")
                        .HasName("IX_CapacitacionRecibida_IdCapacitacionTemario");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_CapacitacionRecibida_IdEmpleado");

                    b.ToTable("CapacitacionRecibida");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.CapacitacionRespuesta", b =>
                {
                    b.Property<int>("IdCapacitacionRespuesta")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<bool>("EsCorrecta");

                    b.Property<int>("IdCapacitacionPregunta");

                    b.HasKey("IdCapacitacionRespuesta")
                        .HasName("PK_CapacitacionRespuesta");

                    b.HasIndex("IdCapacitacionPregunta")
                        .HasName("IX_CapacitacionRespuesta_IdCapacitacionPregunta");

                    b.ToTable("CapacitacionRespuesta");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.CapacitacionTemario", b =>
                {
                    b.Property<int>("IdCapacitacionTemario")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdCapacitacionAreaConocimiento");

                    b.Property<string>("Tema")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("IdCapacitacionTemario")
                        .HasName("PK_CapacitacionTemario");

                    b.HasIndex("IdCapacitacionAreaConocimiento")
                        .HasName("IX_CapacitacionTemario_IdCapacitacionAreaConocimiento");

                    b.ToTable("CapacitacionTemario");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.CapacitacionTemarioProveedor", b =>
                {
                    b.Property<int>("IdCapacitacionTemarioProveedor")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Costo")
                        .HasColumnType("decimal");

                    b.Property<int>("IdCapacitacionModalidad");

                    b.Property<int>("IdCapacitacionProveedor");

                    b.Property<int>("IdCapacitacionTemario");

                    b.Property<int>("NumeroHoras");

                    b.HasKey("IdCapacitacionTemarioProveedor")
                        .HasName("PK_CapacitacionTemarioProveedor");

                    b.HasIndex("IdCapacitacionModalidad")
                        .HasName("IX_CapacitacionTemarioProveedor_IdCapacitacionModalidad");

                    b.HasIndex("IdCapacitacionProveedor")
                        .HasName("IX_CapacitacionTemarioProveedor_IdCapacitacionProveedor");

                    b.HasIndex("IdCapacitacionTemario")
                        .HasName("IX_CapacitacionTemarioProveedor_IdCapacitacionTemario");

                    b.ToTable("CapacitacionTemarioProveedor");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.CapacitacionTipoPregunta", b =>
                {
                    b.Property<int>("IdCapacitacionTipoPregunta")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdCapacitacionTipoPregunta")
                        .HasName("PK_CapacitacionTipoPregunta");

                    b.ToTable("CapacitacionTipoPregunta");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Ciudad", b =>
                {
                    b.Property<int>("IdCiudad")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdProvincia");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasMaxLength(50);

                    b.HasKey("IdCiudad")
                        .HasName("PK_Ciudad");

                    b.HasIndex("IdProvincia")
                        .HasName("IX_Ciudad_IdProvincia");

                    b.ToTable("Ciudad");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ComportamientoObservable", b =>
                {
                    b.Property<int>("ComportamientoObservableId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DenominacionCompetenciaId");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("NivelId");

                    b.HasKey("ComportamientoObservableId");

                    b.HasIndex("DenominacionCompetenciaId")
                        .HasName("IX_ComportamientoObservable_DenominacionCompetenciaId");

                    b.HasIndex("NivelId")
                        .HasName("IX_ComportamientoObservable_NivelId");

                    b.ToTable("ComportamientoObservable");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ConfiguracionViatico", b =>
                {
                    b.Property<int>("IdConfiguracionViatico")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdDependencia");

                    b.Property<int>("PorCientoAJustificar")
                        .HasColumnName("PorCientoAJustificar");

                    b.Property<decimal>("ValorEntregadoPorDia")
                        .HasColumnType("decimal");

                    b.HasKey("IdConfiguracionViatico")
                        .HasName("PK_ConfiguracionViatico");

                    b.HasIndex("IdDependencia")
                        .HasName("IX_ConfiguracionViatico_IdDependencia");

                    b.ToTable("ConfiguracionViatico");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ConfirmacionLectura", b =>
                {
                    b.Property<int>("IdConfirmacionLectura")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdEmpleado");

                    b.HasKey("IdConfirmacionLectura")
                        .HasName("PK_ConfirmacionLectura");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_ConfirmacionLectura_IdEmpleado");

                    b.ToTable("ConfirmacionLectura");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ConocimientosAdicionales", b =>
                {
                    b.Property<int>("IdConocimientosAdicionales")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("IdConocimientosAdicionales")
                        .HasName("PK_ConocimientosAdicionales");

                    b.ToTable("ConocimientosAdicionales");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.DatosBancarios", b =>
                {
                    b.Property<int>("IdDatosBancarios")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ahorros");

                    b.Property<int>("IdEmpleado");

                    b.Property<int>("IdInstitucionFinanciera");

                    b.Property<string>("NumeroCuenta")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.HasKey("IdDatosBancarios")
                        .HasName("PK_DatosBancarios");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_DatosBancarios_IdEmpleado");

                    b.HasIndex("IdInstitucionFinanciera")
                        .HasName("IX_DatosBancarios_IdInstitucionFinanciera");

                    b.ToTable("DatosBancarios");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.DeclaracionPatrimonioPersonal", b =>
                {
                    b.Property<int>("IdDeclaracionPatrimonioPersonal")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("FechaDeclaracion");

                    b.Property<int>("IdEmpleado");

                    b.Property<decimal>("PromedioMensualIngresos")
                        .HasColumnType("decimal");

                    b.Property<decimal>("PromedioMensualOtrosIngresos")
                        .HasColumnType("decimal");

                    b.Property<decimal>("TotalActivosAnioActual")
                        .HasColumnType("decimal");

                    b.Property<decimal>("TotalActivosAnioAnterior")
                        .HasColumnType("decimal");

                    b.Property<decimal>("TotalPasivosAnioActual")
                        .HasColumnType("decimal");

                    b.Property<decimal>("TotalPasivosAnioAnterior")
                        .HasColumnType("decimal");

                    b.HasKey("IdDeclaracionPatrimonioPersonal")
                        .HasName("PK_DeclaracionPatrimonioPersonal");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_DeclaracionPatrimonioPersonal_IdEmpleado");

                    b.ToTable("DeclaracionPatrimonioPersonal");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.DenominacionCompetencia", b =>
                {
                    b.Property<int>("IdDenominacionCompetencia")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CompetenciaTecnica");

                    b.Property<string>("Definicion")
                        .IsRequired();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("IdDenominacionCompetencia")
                        .HasName("PK_DenominacionCompetencia");

                    b.ToTable("DenominacionCompetencia");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Dependencia", b =>
                {
                    b.Property<int>("IdDependencia")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("IdDependenciaPadre");

                    b.Property<int>("IdSucursal");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.HasKey("IdDependencia")
                        .HasName("PK_Dependencia");

                    b.HasIndex("IdDependenciaPadre")
                        .HasName("IX_Dependencia_DependenciaPadreIdDependencia");

                    b.HasIndex("IdSucursal")
                        .HasName("IX_Dependencia_IdSucursal");

                    b.ToTable("Dependencia");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.DependenciaDocumento", b =>
                {
                    b.Property<int>("IdDependenciaDocumento")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdDependencia");

                    b.Property<int>("IdDocumentosSubidos");

                    b.HasKey("IdDependenciaDocumento")
                        .HasName("PK_DependenciaDocumento");

                    b.HasIndex("IdDependencia")
                        .HasName("IX_DependenciaDocumento_IdDependencia");

                    b.HasIndex("IdDocumentosSubidos")
                        .HasName("IX_DependenciaDocumento_IdDocumentosSubidos");

                    b.ToTable("DependenciaDocumento");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Destreza", b =>
                {
                    b.Property<int>("IdDestreza")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdDestreza")
                        .HasName("PK_Destreza");

                    b.ToTable("Destreza");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.DetalleExamenInduccion", b =>
                {
                    b.Property<int>("IdDetalleExamenInduccion")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("PreguntaId");

                    b.Property<int>("RealizaExamenInduccionId");

                    b.Property<int>("RespuestaId");

                    b.HasKey("IdDetalleExamenInduccion")
                        .HasName("PK_DetalleExamenInduccion");

                    b.HasIndex("PreguntaId")
                        .HasName("IX_DetalleExamenInduccion_PreguntaId");

                    b.HasIndex("RealizaExamenInduccionId")
                        .HasName("IX_DetalleExamenInduccion_RealizaExamenInduccionId");

                    b.HasIndex("RespuestaId")
                        .HasName("IX_DetalleExamenInduccion_RespuestaId");

                    b.ToTable("DetalleExamenInduccion");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.DocumentoInformacionInstitucional", b =>
                {
                    b.Property<int>("IdDocumentoInformacionInstitucional")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("Url");

                    b.HasKey("IdDocumentoInformacionInstitucional");

                    b.ToTable("DocumentoInformacionInstitucional");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.DocumentosParentescodos", b =>
                {
                    b.Property<int>("IdDocumentosSubidos")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Are")
                        .IsRequired()
                        .HasMaxLength(400);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(400);

                    b.Property<DateTime>("Fecha");

                    b.Property<DateTime>("FechaCaducidad");

                    b.Property<int>("IdEmpleado");

                    b.Property<string>("NombreArchivo")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("Ubicacion")
                        .IsRequired();

                    b.HasKey("IdDocumentosSubidos")
                        .HasName("PK_DocumentosParentescodos");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_DocumentosParentescodos_IdEmpleado");

                    b.ToTable("DocumentosParentescodos");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Empleado", b =>
                {
                    b.Property<int>("IdEmpleado")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("DeclaracionJurada");

                    b.Property<int>("DiasImposiciones");

                    b.Property<DateTime>("FechaIngreso");

                    b.Property<DateTime?>("FechaIngresoSectorPublico")
                        .IsRequired();

                    b.Property<int>("IdCiudadLugarNacimiento");

                    b.Property<int>("IdDependencia");

                    b.Property<int>("IdPersona");

                    b.Property<int>("IdProvinciaLugarSufragio");

                    b.Property<string>("IngresosOtraActividad")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("MesesImposiciones");

                    b.Property<bool>("Nepotismo");

                    b.Property<bool>("TrabajoSuperintendenciaBanco");

                    b.HasKey("IdEmpleado")
                        .HasName("PK_Empleado");

                    b.HasIndex("IdCiudadLugarNacimiento")
                        .HasName("IX_Empleado_CiudadNacimientoIdCiudad");

                    b.HasIndex("IdDependencia")
                        .HasName("IX_Empleado_IdDependencia");

                    b.HasIndex("IdPersona")
                        .HasName("IX_Empleado_IdPersona");

                    b.HasIndex("IdProvinciaLugarSufragio")
                        .HasName("IX_Empleado_ProvinciaSufragioIdProvincia");

                    b.ToTable("Empleado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EmpleadoContactoEmergencia", b =>
                {
                    b.Property<int>("IdEmpleadoContactoEmergencia")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdEmpleado");

                    b.Property<int>("IdParentesco");

                    b.Property<int>("IdPersona");

                    b.HasKey("IdEmpleadoContactoEmergencia")
                        .HasName("PK_EmpleadoContactoEmergencia");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_EmpleadoContactoEmergencia_IdEmpleado");

                    b.HasIndex("IdParentesco")
                        .HasName("IX_EmpleadoContactoEmergencia_IdParentesco");

                    b.HasIndex("IdPersona")
                        .HasName("IX_EmpleadoContactoEmergencia_IdPersona");

                    b.ToTable("EmpleadoContactoEmergencia");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EmpleadoFamiliar", b =>
                {
                    b.Property<int>("IdEmpleadoFamiliar")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdEmpleado");

                    b.Property<int>("IdParentesco");

                    b.Property<int>("IdPersona");

                    b.HasKey("IdEmpleadoFamiliar")
                        .HasName("PK_EmpleadoFamiliar");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_EmpleadoFamiliar_IdEmpleado");

                    b.HasIndex("IdParentesco")
                        .HasName("IX_EmpleadoFamiliar_IdParentesco");

                    b.HasIndex("IdPersona")
                        .HasName("IX_EmpleadoFamiliar_IdPersona");

                    b.ToTable("EmpleadoFamiliar");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EmpleadoFormularioCapacitacion", b =>
                {
                    b.Property<int>("IdEmpleadoFormularioCapacitacion")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("IdEvento");

                    b.Property<int>("IdFormularioCapacitacion");

                    b.Property<int>("IdServidor");

                    b.HasKey("IdEmpleadoFormularioCapacitacion")
                        .HasName("PK_EmpleadoFormularioCapacitacion");

                    b.HasIndex("IdFormularioCapacitacion")
                        .HasName("IX_EmpleadoFormularioCapacitacion_IdFormularioCapacitacion");

                    b.HasIndex("IdServidor")
                        .HasName("IX_EmpleadoFormularioCapacitacion_ServidorIdEmpleado");

                    b.ToTable("EmpleadoFormularioCapacitacion");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EmpleadoIE", b =>
                {
                    b.Property<int>("IdEmpeladoIE")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IdEmpeladoIE");

                    b.Property<DateTime>("Fecha");

                    b.Property<DateTime>("FechaFin");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<bool>("Fijo");

                    b.Property<int>("IdEmpleado");

                    b.Property<int>("IdIngresoEgresoRMU")
                        .HasColumnName("IdIngresoEgresoRMU");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal");

                    b.HasKey("IdEmpeladoIE")
                        .HasName("PK_EmpleadoIE");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_EmpleadoIE_IdEmpleado");

                    b.HasIndex("IdIngresoEgresoRMU")
                        .HasName("IX_EmpleadoIE_IdIngresoEgresoRMU");

                    b.ToTable("EmpleadoIE");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EmpleadoImpuestoRenta", b =>
                {
                    b.Property<int>("IdEmpleadoImpuestoRenta")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("FechaDesde");

                    b.Property<int>("IdEmpleado");

                    b.Property<decimal>("IngresoTotal")
                        .HasColumnType("decimal");

                    b.Property<decimal>("OtrosIngresos")
                        .HasColumnType("decimal");

                    b.HasKey("IdEmpleadoImpuestoRenta")
                        .HasName("PK_EmpleadoImpuestoRenta");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_EmpleadoImpuestoRenta_IdEmpleado");

                    b.ToTable("EmpleadoImpuestoRenta");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EmpleadoMovimiento", b =>
                {
                    b.Property<int>("IdEmpleadoMovimiento")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("FechaDesde");

                    b.Property<DateTime?>("FechaHasta")
                        .IsRequired();

                    b.Property<int>("IdEmpleado");

                    b.Property<int>("IdIndiceOcupacionalModalidadPartida");

                    b.Property<int>("IdTipoMovimientoInterno");

                    b.HasKey("IdEmpleadoMovimiento")
                        .HasName("PK_EmpleadoMovimiento");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_EmpleadoMovimiento_IdEmpleado");

                    b.HasIndex("IdIndiceOcupacionalModalidadPartida")
                        .HasName("IX_EmpleadoMovimiento_IdIndiceOcupacionalModalidadPartida");

                    b.HasIndex("IdTipoMovimientoInterno")
                        .HasName("IX_EmpleadoMovimiento_IdTipoMovimientoInterno");

                    b.ToTable("EmpleadoMovimiento");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EmpleadoNepotismo", b =>
                {
                    b.Property<int>("IdEmpleadoNepotismo")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdEmpleado");

                    b.HasKey("IdEmpleadoNepotismo")
                        .HasName("PK_EmpleadoNepotismo");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_EmpleadoNepotismo_IdEmpleado");

                    b.ToTable("EmpleadoNepotismo");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EmpleadoSaldoVacaciones", b =>
                {
                    b.Property<int>("IdEmpleadoSaldoVacaciones")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdEmpleado");

                    b.Property<decimal>("SaldoDias")
                        .HasColumnType("decimal");

                    b.Property<decimal>("SaldoMonetario")
                        .HasColumnType("decimal");

                    b.HasKey("IdEmpleadoSaldoVacaciones")
                        .HasName("PK_EmpleadoSaldoVacaciones");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_EmpleadoSaldoVacaciones_IdEmpleado");

                    b.ToTable("EmpleadoSaldoVacaciones");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EmpleadosFormularioDevengacion", b =>
                {
                    b.Property<int>("IdEmpleadosFormularioDevengacion")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FormularioDevengacionId");

                    b.Property<int>("IdEmpleado");

                    b.HasKey("IdEmpleadosFormularioDevengacion")
                        .HasName("PK_EmpleadosFormularioDevengacion");

                    b.HasIndex("FormularioDevengacionId")
                        .HasName("IX_EmpleadosFormularioDevengacion_FormularioDevengacionId");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_EmpleadosFormularioDevengacion_IdEmpleado");

                    b.ToTable("EmpleadosFormularioDevengacion");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EscalaEvaluacionTotal", b =>
                {
                    b.Property<int>("IdEscalaEvaluacionTotal")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<decimal>("PorcientoDesde")
                        .HasColumnType("decimal");

                    b.Property<decimal>("PorcientoHasta")
                        .HasColumnType("decimal");

                    b.HasKey("IdEscalaEvaluacionTotal")
                        .HasName("PK_EscalaEvaluacionTotal");

                    b.ToTable("EscalaEvaluacionTotal");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EscalaGrados", b =>
                {
                    b.Property<int>("IdEscalaGrados")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Grado");

                    b.Property<int>("IdGrupoOcupacional");

                    b.Property<decimal>("Remuneracion")
                        .HasColumnType("decimal");

                    b.HasKey("IdEscalaGrados")
                        .HasName("PK_EscalaGrados");

                    b.HasIndex("IdGrupoOcupacional")
                        .HasName("IX_EscalaGrados_IdGrupoOcupacional");

                    b.ToTable("EscalaGrados");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EspecificidadExperiencia", b =>
                {
                    b.Property<int>("IdEspecificidadExperiencia")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdEspecificidadExperiencia")
                        .HasName("PK_EspecificidadExperiencia");

                    b.ToTable("EspecificidadExperiencia");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Estado", b =>
                {
                    b.Property<int>("IdEstado")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdEstado")
                        .HasName("PK_Estado");

                    b.ToTable("Estado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EstadoCivil", b =>
                {
                    b.Property<int>("IdEstadoCivil")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdEstadoCivil")
                        .HasName("PK_EstadoCivil");

                    b.ToTable("EstadoCivil");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Estudio", b =>
                {
                    b.Property<int>("IdEstudio")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdEstudio")
                        .HasName("PK_Estudio");

                    b.ToTable("Estudio");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Etnia", b =>
                {
                    b.Property<int>("IdEtnia")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdEtnia")
                        .HasName("PK_Etnia");

                    b.ToTable("Etnia");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Eval001", b =>
                {
                    b.Property<int>("IdEval001")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("EstaConforme")
                        .IsRequired();

                    b.Property<DateTime>("FechaEvaluacionDesde");

                    b.Property<DateTime>("FechaEvaluacionHasta");

                    b.Property<DateTime>("FechaRegistro");

                    b.Property<int?>("IdEmpleadoEvaluado");

                    b.Property<int?>("IdEscalaEvaluacionTotal");

                    b.Property<int?>("IdEvaluacionActividadesPuestoTrabajo");

                    b.Property<int?>("IdEvaluacionCompetenciasTecnicasPuesto");

                    b.Property<int?>("IdEvaluacionCompetenciasUniversales");

                    b.Property<int?>("IdEvaluacionConocimiento");

                    b.Property<int?>("IdEvaluacionTrabajoEquipoIniciativaLiderazgo");

                    b.Property<int>("IdEvaluador");

                    b.Property<string>("Observaciones");

                    b.HasKey("IdEval001")
                        .HasName("PK_Eval001");

                    b.HasIndex("IdEmpleadoEvaluado")
                        .HasName("IX_Eval001_EmpleadoIdEmpleado");

                    b.HasIndex("IdEscalaEvaluacionTotal")
                        .HasName("IX_Eval001_IdEscalaEvaluacionTotal");

                    b.HasIndex("IdEvaluacionActividadesPuestoTrabajo")
                        .HasName("IX_Eval001_IdEvaluacionActividadesPuestoTrabajo");

                    b.HasIndex("IdEvaluacionCompetenciasTecnicasPuesto")
                        .HasName("IX_Eval001_IdEvaluacionCompetenciasTecnicasPuesto");

                    b.HasIndex("IdEvaluacionCompetenciasUniversales")
                        .HasName("IX_Eval001_IdEvaluacionCompetenciasUniversales");

                    b.HasIndex("IdEvaluacionConocimiento")
                        .HasName("IX_Eval001_IdEvaluacionConocimiento");

                    b.HasIndex("IdEvaluacionTrabajoEquipoIniciativaLiderazgo")
                        .HasName("IX_Eval001_IdEvaluacionTrabajoEquipoIniciativaLiderazgo");

                    b.HasIndex("IdEvaluador")
                        .HasName("IX_Eval001_IdEvaluador");

                    b.ToTable("Eval001");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionActividadesPuestoTrabajo", b =>
                {
                    b.Property<int>("IdEvaluacionActividadesPuestoTrabajo")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdEvaluacionActividadesPuestoTrabajo")
                        .HasName("PK_EvaluacionActividadesPuestoTrabajo");

                    b.ToTable("EvaluacionActividadesPuestoTrabajo");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionActividadesPuestoTrabajoDetalle", b =>
                {
                    b.Property<int>("IdEvaluacionActividadesPuestoTrabajoDetalle")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActividadesCumplidas");

                    b.Property<string>("DescripcionActividad")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("IdEvaluacionActividadesPuestoTrabajo");

                    b.Property<int?>("IdIndicador");

                    b.Property<int>("MetaPeriodo");

                    b.HasKey("IdEvaluacionActividadesPuestoTrabajoDetalle")
                        .HasName("PK_EvaluacionActividadesPuestoTrabajoDetalle");

                    b.HasIndex("IdEvaluacionActividadesPuestoTrabajo")
                        .HasName("IX_EvaluacionActividadesPuestoTrabajoDetalle_IdEvaluacionActividadesPuestoTrabajo");

                    b.HasIndex("IdIndicador")
                        .HasName("IX_EvaluacionActividadesPuestoTrabajoDetalle_IdIndicador");

                    b.ToTable("EvaluacionActividadesPuestoTrabajoDetalle");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionActividadesPuestoTrabajoFactor", b =>
                {
                    b.Property<int>("IdEvaluacionActividadesPuestoTrabajoFactor")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("IdEvaluacionActividadesPuestoTrabajo");

                    b.Property<int?>("IdFactor");

                    b.HasKey("IdEvaluacionActividadesPuestoTrabajoFactor")
                        .HasName("PK_EvaluacionActividadesPuestoTrabajoFactor");

                    b.HasIndex("IdEvaluacionActividadesPuestoTrabajo")
                        .HasName("IX_EvaluacionActividadesPuestoTrabajoFactor_IdEvaluacionActividadesPuestoTrabajo");

                    b.HasIndex("IdFactor")
                        .HasName("IX_EvaluacionActividadesPuestoTrabajoFactor_IdFactor");

                    b.ToTable("EvaluacionActividadesPuestoTrabajoFactor");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionCompetenciasTecnicasPuesto", b =>
                {
                    b.Property<int>("IdEvaluacionCompetenciasTecnicasPuesto")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdEvaluacionCompetenciasTecnicasPuesto")
                        .HasName("PK_EvaluacionCompetenciasTecnicasPuesto");

                    b.ToTable("EvaluacionCompetenciasTecnicasPuesto");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionCompetenciasTecnicasPuestoDetalle", b =>
                {
                    b.Property<int>("IdEvaluacionCompetenciasTecnicasPuestoDetalle")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("IdDestreza");

                    b.Property<int>("IdEvaluacionCompetenciasTecnicasPuesto");

                    b.Property<int?>("IdNivelDesarrollo");

                    b.Property<int?>("IdRelevancia");

                    b.HasKey("IdEvaluacionCompetenciasTecnicasPuestoDetalle")
                        .HasName("PK_EvaluacionCompetenciasTecnicasPuestoDetalle");

                    b.HasIndex("IdDestreza")
                        .HasName("IX_EvaluacionCompetenciasTecnicasPuestoDetalle_IdDestreza");

                    b.HasIndex("IdEvaluacionCompetenciasTecnicasPuesto")
                        .HasName("IX_EvaluacionCompetenciasTecnicasPuestoDetalle_IdEvaluacionCompetenciasTecnicasPuesto");

                    b.HasIndex("IdNivelDesarrollo")
                        .HasName("IX_EvaluacionCompetenciasTecnicasPuestoDetalle_IdNivelDesarrollo");

                    b.HasIndex("IdRelevancia")
                        .HasName("IX_EvaluacionCompetenciasTecnicasPuestoDetalle_IdRelevancia");

                    b.ToTable("EvaluacionCompetenciasTecnicasPuestoDetalle");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionCompetenciasTecnicasPuestoFactor", b =>
                {
                    b.Property<int>("IdEvaluacionCompetenciasTecnicasPuestoFactor")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("IdEvaluacionCompetenciasTecnicasPuesto");

                    b.Property<int>("IdFactor");

                    b.HasKey("IdEvaluacionCompetenciasTecnicasPuestoFactor")
                        .HasName("PK_EvaluacionCompetenciasTecnicasPuestoFactor");

                    b.HasIndex("IdEvaluacionCompetenciasTecnicasPuesto")
                        .HasName("IX_EvaluacionCompetenciasTecnicasPuestoFactor_IdEvaluacionCompetenciasTecnicasPuesto");

                    b.HasIndex("IdFactor")
                        .HasName("IX_EvaluacionCompetenciasTecnicasPuestoFactor_IdFactor");

                    b.ToTable("EvaluacionCompetenciasTecnicasPuestoFactor");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionCompetenciasUniversales", b =>
                {
                    b.Property<int>("IdEvaluacionCompetenciasUniversales")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdEvaluacionCompetenciasUniversales")
                        .HasName("PK_EvaluacionCompetenciasUniversales");

                    b.ToTable("EvaluacionCompetenciasUniversales");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionCompetenciasUniversalesDetalle", b =>
                {
                    b.Property<int>("IdEvaluacionCompetenciasUniversalesDetalle")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("IdDestreza");

                    b.Property<int>("IdEvaluacionCompetenciasUniversales");

                    b.Property<int?>("IdFrecuenciaAplicacion");

                    b.Property<int?>("IdRelevancia");

                    b.HasKey("IdEvaluacionCompetenciasUniversalesDetalle")
                        .HasName("PK_EvaluacionCompetenciasUniversalesDetalle");

                    b.HasIndex("IdDestreza")
                        .HasName("IX_EvaluacionCompetenciasUniversalesDetalle_IdDestreza");

                    b.HasIndex("IdEvaluacionCompetenciasUniversales")
                        .HasName("IX_EvaluacionCompetenciasUniversalesDetalle_IdEvaluacionCompetenciasUniversales");

                    b.HasIndex("IdFrecuenciaAplicacion")
                        .HasName("IX_EvaluacionCompetenciasUniversalesDetalle_IdFrecuenciaAplicacion");

                    b.HasIndex("IdRelevancia")
                        .HasName("IX_EvaluacionCompetenciasUniversalesDetalle_IdRelevancia");

                    b.ToTable("EvaluacionCompetenciasUniversalesDetalle");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionCompetenciasUniversalesFactor", b =>
                {
                    b.Property<int>("IdEvaluacionCompetenciasUniversalesFactor")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("IdEvaluacionCompetenciasUniversales");

                    b.Property<int>("IdFactor");

                    b.HasKey("IdEvaluacionCompetenciasUniversalesFactor")
                        .HasName("PK_EvaluacionCompetenciasUniversalesFactor");

                    b.HasIndex("IdEvaluacionCompetenciasUniversales")
                        .HasName("IX_EvaluacionCompetenciasUniversalesFactor_IdEvaluacionCompetenciasUniversales");

                    b.HasIndex("IdFactor")
                        .HasName("IX_EvaluacionCompetenciasUniversalesFactor_IdFactor");

                    b.ToTable("EvaluacionCompetenciasUniversalesFactor");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionConocimiento", b =>
                {
                    b.Property<int>("IdEvaluacionConocimiento")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdEvaluacionConocimiento")
                        .HasName("PK_EvaluacionConocimiento");

                    b.ToTable("EvaluacionConocimiento");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionConocimientoDetalle", b =>
                {
                    b.Property<int>("IdEvaluacionConocimientoDetalle")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdEvaluacionConocimiento");

                    b.Property<int?>("IdNivelConocimiento");

                    b.HasKey("IdEvaluacionConocimientoDetalle")
                        .HasName("PK_EvaluacionConocimientoDetalle");

                    b.HasIndex("IdEvaluacionConocimiento")
                        .HasName("IX_EvaluacionConocimientoDetalle_IdEvaluacionConocimiento");

                    b.HasIndex("IdNivelConocimiento")
                        .HasName("IX_EvaluacionConocimientoDetalle_IdNivelConocimiento");

                    b.ToTable("EvaluacionConocimientoDetalle");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionConocimientoFactor", b =>
                {
                    b.Property<int>("IdEvaluacionConocimientoFactor")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("IdEvaluacionConocimiento");

                    b.Property<int>("IdFactor");

                    b.HasKey("IdEvaluacionConocimientoFactor")
                        .HasName("PK_EvaluacionConocimientoFactor");

                    b.HasIndex("IdEvaluacionConocimiento")
                        .HasName("IX_EvaluacionConocimientoFactor_IdEvaluacionConocimiento");

                    b.HasIndex("IdFactor")
                        .HasName("IX_EvaluacionConocimientoFactor_IdFactor");

                    b.ToTable("EvaluacionConocimientoFactor");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionInducion", b =>
                {
                    b.Property<int>("IdEvaluacionInduccion")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MaximoPuntos")
                        .HasColumnName("MaximoPuntos");

                    b.Property<int>("MinimoAprobar")
                        .HasColumnName("MinimoAprobar");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdEvaluacionInduccion")
                        .HasName("PK_EvaluacionInducion");

                    b.ToTable("EvaluacionInducion");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionTrabajoEquipoIniciativaLiderazgo", b =>
                {
                    b.Property<int>("IdEvaluacionTrabajoEquipoIniciativaLiderazgo")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("ObservacionesJefeInmediato")
                        .IsRequired();

                    b.HasKey("IdEvaluacionTrabajoEquipoIniciativaLiderazgo")
                        .HasName("PK_EvaluacionTrabajoEquipoIniciativaLiderazgo");

                    b.ToTable("EvaluacionTrabajoEquipoIniciativaLiderazgo");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle", b =>
                {
                    b.Property<int>("IdEvaluacionTrabajoEquipoIniciativaLiderazgoDetalle")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("IdEvaluacionTrabajoEquipoIniciativaLiderazgo");

                    b.Property<int?>("IdFrecuenciaAplicacion");

                    b.Property<int?>("IdRelevancia");

                    b.Property<int?>("IdTrabajoEquipoIniciativaLiderazgo");

                    b.HasKey("IdEvaluacionTrabajoEquipoIniciativaLiderazgoDetalle")
                        .HasName("PK_EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle");

                    b.HasIndex("IdEvaluacionTrabajoEquipoIniciativaLiderazgo")
                        .HasName("IX_EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle_IdEvaluacionTrabajoEquipoIniciativaLiderazgo");

                    b.HasIndex("IdFrecuenciaAplicacion")
                        .HasName("IX_EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle_IdFrecuenciaAplicacion");

                    b.HasIndex("IdRelevancia")
                        .HasName("IX_EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle_IdRelevancia");

                    b.HasIndex("IdTrabajoEquipoIniciativaLiderazgo")
                        .HasName("IX_EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle_IdTrabajoEquipoIniciativaLiderazgo");

                    b.ToTable("EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionTrabajoEquipoIniciativaLiderazgoFactor", b =>
                {
                    b.Property<int>("IdEvalTjoEquiInicLidFac")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdEvaluacionTrabajoEquipoIniciativaLiderazgo");

                    b.Property<int>("IdFactor");

                    b.HasKey("IdEvalTjoEquiInicLidFac")
                        .HasName("PK_EvaluacionTrabajoEquipoIniciativaLiderazgoFactor");

                    b.HasIndex("IdEvaluacionTrabajoEquipoIniciativaLiderazgo")
                        .HasName("IX_EvaluacionTrabajoEquipoIniciativaLiderazgoFactor_IdEvaluacionTrabajoEquipoIniciativaLiderazgo");

                    b.HasIndex("IdFactor")
                        .HasName("IX_EvaluacionTrabajoEquipoIniciativaLiderazgoFactor_IdFactor");

                    b.ToTable("EvaluacionTrabajoEquipoIniciativaLiderazgoFactor");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Evaluador", b =>
                {
                    b.Property<int>("IdEvaluador")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Ano");

                    b.Property<int>("IdDependencia");

                    b.Property<int>("IdEmpleado");

                    b.HasKey("IdEvaluador")
                        .HasName("PK_Evaluador");

                    b.HasIndex("IdDependencia")
                        .HasName("IX_Evaluador_IdDependencia");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_Evaluador_IdEmpleado");

                    b.ToTable("Evaluador");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Exepciones", b =>
                {
                    b.Property<int>("IdExepciones")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<int>("ValidacionJefeId");

                    b.HasKey("IdExepciones")
                        .HasName("PK_Exepciones");

                    b.HasIndex("ValidacionJefeId")
                        .HasName("IX_Exepciones_ValidacionInmediatoSuperiorIdValidacionJefe");

                    b.ToTable("Exepciones");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ExperienciaLaboralRequerida", b =>
                {
                    b.Property<int>("IdExperienciaLaboralRequerida")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdAnoExperiencia");

                    b.Property<int>("IdEspecificidadExperiencia");

                    b.Property<int>("IdEstudio");

                    b.Property<int>("IdIndiceOcupacionalCapacitaciones");

                    b.HasKey("IdExperienciaLaboralRequerida")
                        .HasName("PK_ExperienciaLaboralRequerida");

                    b.HasIndex("IdAnoExperiencia")
                        .HasName("IX_ExperienciaLaboralRequerida_IdAnoExperiencia");

                    b.HasIndex("IdEspecificidadExperiencia")
                        .HasName("IX_ExperienciaLaboralRequerida_IdEspecificidadExperiencia");

                    b.HasIndex("IdEstudio")
                        .HasName("IX_ExperienciaLaboralRequerida_IdEstudio");

                    b.HasIndex("IdIndiceOcupacionalCapacitaciones")
                        .HasName("IX_ExperienciaLaboralRequerida_IdIndiceOcupacionalCapacitaciones");

                    b.ToTable("ExperienciaLaboralRequerida");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Factor", b =>
                {
                    b.Property<int>("IdFactor")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal?>("Porciento")
                        .IsRequired()
                        .HasColumnType("decimal");

                    b.HasKey("IdFactor")
                        .HasName("PK_Factor");

                    b.ToTable("Factor");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.FacturaViatico", b =>
                {
                    b.Property<int>("IdFacturaViatico")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AprobadoPor");

                    b.Property<DateTime?>("FechaAprobacion")
                        .IsRequired();

                    b.Property<DateTime>("FechaFactura");

                    b.Property<int>("IdItemViatico");

                    b.Property<int>("ItinerarioViaticoId");

                    b.Property<string>("NumeroFactura")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Observaciones")
                        .IsRequired();

                    b.Property<decimal?>("ValorTotalAprobacion")
                        .IsRequired()
                        .HasColumnType("decimal");

                    b.Property<decimal>("ValorTotalFactura")
                        .HasColumnType("decimal");

                    b.HasKey("IdFacturaViatico")
                        .HasName("PK_FacturaViatico");

                    b.HasIndex("AprobadoPor")
                        .HasName("IX_FacturaViatico_EmpleadoIdEmpleado");

                    b.HasIndex("IdItemViatico")
                        .HasName("IX_FacturaViatico_IdItemViatico");

                    b.HasIndex("ItinerarioViaticoId")
                        .HasName("IX_FacturaViatico_ItinerarioViaticoId");

                    b.ToTable("FacturaViatico");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.FaseConcurso", b =>
                {
                    b.Property<int>("IdFaseConcurso")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTime?>("FechaFin")
                        .IsRequired();

                    b.Property<DateTime?>("FechaInicio")
                        .IsRequired();

                    b.Property<int>("IdTipoConcurso");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdFaseConcurso")
                        .HasName("PK_FaseConcurso");

                    b.HasIndex("IdTipoConcurso")
                        .HasName("IX_FaseConcurso_IdTipoConcurso");

                    b.ToTable("FaseConcurso");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.FondoFinanciamiento", b =>
                {
                    b.Property<int>("IdFondoFinanciamiento")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdFondoFinanciamiento")
                        .HasName("PK_FondoFinanciamiento");

                    b.ToTable("FondoFinanciamiento");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.FormularioAnalisisOcupacional", b =>
                {
                    b.Property<int>("IdFormularioAnalisisOcupacional")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Anio");

                    b.Property<bool?>("ExtPersJurídicasPubNivelNacional")
                        .IsRequired();

                    b.Property<bool?>("ExternosCiudadania")
                        .IsRequired();

                    b.Property<DateTime>("FechaRegistro");

                    b.Property<int>("IdEmpleado");

                    b.Property<bool?>("InternoMismoInstitucionFinanciera")
                        .IsRequired();

                    b.Property<bool?>("InternoOtroInstitucionFinanciera")
                        .IsRequired();

                    b.Property<string>("MisionPuesto")
                        .IsRequired();

                    b.HasKey("IdFormularioAnalisisOcupacional")
                        .HasName("PK_FormularioAnalisisOcupacional");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_FormularioAnalisisOcupacional_IdEmpleado");

                    b.ToTable("FormularioAnalisisOcupacional");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.FormularioCapacitacion", b =>
                {
                    b.Property<int>("IdFormularioCapacitacion")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdFormularioCapacitacion")
                        .HasName("PK_FormularioCapacitacion");

                    b.ToTable("FormularioCapacitacion");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.FormularioDevengacion", b =>
                {
                    b.Property<int>("IdFormularioDevengacion")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnalistaDesarrolloInstitucional");

                    b.Property<int>("IdEmpleado");

                    b.Property<int?>("IdEvento")
                        .IsRequired();

                    b.Property<string>("ModoSocial")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<int>("ModosScializacionId");

                    b.Property<int>("ResponsableArea");

                    b.HasKey("IdFormularioDevengacion")
                        .HasName("PK_FormularioDevengacion");

                    b.HasIndex("AnalistaDesarrolloInstitucional")
                        .HasName("IX_FormularioDevengacion_IdEmpleadoDesarrolloInstitucional");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_FormularioDevengacion_IdEmpleado");

                    b.HasIndex("ModosScializacionId")
                        .HasName("IX_FormularioDevengacion_ModosScializacionId");

                    b.HasIndex("ResponsableArea")
                        .HasName("IX_FormularioDevengacion_IdEmpleadoResponsableArea");

                    b.ToTable("FormularioDevengacion");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.FormulasRMU", b =>
                {
                    b.Property<int>("IdFormulaRMU")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IdFormulaRMU");

                    b.Property<string>("Formula")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdFormulaRMU")
                        .HasName("PK_FormulasRMU");

                    b.ToTable("FormulasRMU");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.FrecuenciaAplicacion", b =>
                {
                    b.Property<int>("IdFrecuenciaAplicacion")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdFrecuenciaAplicacion")
                        .HasName("PK_FrecuenciaAplicacion");

                    b.ToTable("FrecuenciaAplicacion");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.GastoRubro", b =>
                {
                    b.Property<int>("IdGastoRubro")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("GastoProyectado")
                        .HasColumnType("decimal");

                    b.Property<int>("IdEmpleadoImpuestoRenta");

                    b.Property<int?>("IdRubro");

                    b.HasKey("IdGastoRubro")
                        .HasName("PK_GastoRubro");

                    b.HasIndex("IdEmpleadoImpuestoRenta")
                        .HasName("IX_GastoRubro_IdEmpleadoImpuestoRenta");

                    b.HasIndex("IdRubro")
                        .HasName("IX_GastoRubro_IdRubro");

                    b.ToTable("GastoRubro");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Genero", b =>
                {
                    b.Property<int>("IdGenero")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdGenero")
                        .HasName("PK_Genero");

                    b.ToTable("Genero");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.GrupoOcupacional", b =>
                {
                    b.Property<int>("IdGrupoOcupacional")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdGrupoOcupacional")
                        .HasName("PK_GrupoOcupacional");

                    b.ToTable("GrupoOcupacional");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ImpuestoRentaParametros", b =>
                {
                    b.Property<int>("IdImpuestoRentaParametros")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("ExcesoHasta");

                    b.Property<decimal>("FraccionBasica");

                    b.Property<int?>("ImpuestoFraccionBasica")
                        .IsRequired();

                    b.Property<int>("PorcentajeImpuestoFraccionExcedente");

                    b.HasKey("IdImpuestoRentaParametros");

                    b.ToTable("ImpuestoRentaParametros");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Indicador", b =>
                {
                    b.Property<int>("IdIndicador")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdIndicador")
                        .HasName("PK_Indicador");

                    b.ToTable("Indicador");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.IndiceOcupacional", b =>
                {
                    b.Property<int>("IdIndiceOcupacional")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("IdDependencia");

                    b.Property<int?>("IdEscalaGrados");

                    b.Property<int?>("IdManualPuesto");

                    b.Property<int?>("IdRolPuesto");

                    b.HasKey("IdIndiceOcupacional")
                        .HasName("PK_IndiceOcupacional");

                    b.HasIndex("IdDependencia")
                        .HasName("IX_IndiceOcupacional_IdDependencia");

                    b.HasIndex("IdEscalaGrados")
                        .HasName("IX_IndiceOcupacional_IdEscalaGrados");

                    b.HasIndex("IdManualPuesto")
                        .HasName("IX_IndiceOcupacional_IdManualPuesto");

                    b.HasIndex("IdRolPuesto")
                        .HasName("IX_IndiceOcupacional_IdRolPuesto");

                    b.ToTable("IndiceOcupacional");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.IndiceOcupacionalActividadesEsenciales", b =>
                {
                    b.Property<int>("IdIndiceOcupacionalActividadesEsenciales")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdActividadesEsenciales");

                    b.Property<int>("IdIndiceOcupacional");

                    b.HasKey("IdIndiceOcupacionalActividadesEsenciales")
                        .HasName("PK_IndiceOcupacionalActividadesEsenciales");

                    b.HasIndex("IdActividadesEsenciales")
                        .HasName("IX_IndiceOcupacionalActividadesEsenciales_IdActividadesEsenciales");

                    b.HasIndex("IdIndiceOcupacional")
                        .HasName("IX_IndiceOcupacionalActividadesEsenciales_IdIndiceOcupacional");

                    b.ToTable("IndiceOcupacionalActividadesEsenciales");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.IndiceOcupacionalAreaConocimiento", b =>
                {
                    b.Property<int>("IdIndiceOcupacionalAreaConocimiento")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdAreaConocimiento");

                    b.Property<int>("IdIndiceOcupacional");

                    b.HasKey("IdIndiceOcupacionalAreaConocimiento")
                        .HasName("PK_IndiceOcupacionalAreaConocimiento");

                    b.HasIndex("IdAreaConocimiento")
                        .HasName("IX_IndiceOcupacionalAreaConocimiento_IdAreaConocimiento");

                    b.HasIndex("IdIndiceOcupacional")
                        .HasName("IX_IndiceOcupacionalAreaConocimiento_IdIndiceOcupacional");

                    b.ToTable("IndiceOcupacionalAreaConocimiento");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.IndiceOcupacionalCapacitaciones", b =>
                {
                    b.Property<int>("IdIndiceOcupacionalCapacitaciones")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdCapacitacion");

                    b.Property<int>("IdIndiceOcupacional");

                    b.HasKey("IdIndiceOcupacionalCapacitaciones")
                        .HasName("PK_IndiceOcupacionalCapacitaciones");

                    b.HasIndex("IdCapacitacion")
                        .HasName("IX_IndiceOcupacionalCapacitaciones_IdCapacitacion");

                    b.HasIndex("IdIndiceOcupacional")
                        .HasName("IX_IndiceOcupacionalCapacitaciones_IdIndiceOcupacional");

                    b.ToTable("IndiceOcupacionalCapacitaciones");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.IndiceOcupacionalComportamientoObservable", b =>
                {
                    b.Property<int>("IdIndiceOcupacionalComportamientoObservable")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdComportamientoObservable");

                    b.Property<int>("IdIndiceOcupacional");

                    b.HasKey("IdIndiceOcupacionalComportamientoObservable")
                        .HasName("PK_IndiceOcupacionalComportamientoObservable");

                    b.HasIndex("IdComportamientoObservable")
                        .HasName("IX_IndiceOcupacionalComportamientoObservable_ComportamientoObservableId");

                    b.HasIndex("IdIndiceOcupacional")
                        .HasName("IX_IndiceOcupacionalComportamientoObservable_IdIndiceOcupacional");

                    b.ToTable("IndiceOcupacionalComportamientoObservable");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.IndiceOcupacionalConocimientosAdicionales", b =>
                {
                    b.Property<int>("IdIndiceOcupacionalConocimientosAdicionales")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdConocimientosAdicionales");

                    b.Property<int>("IdIndiceOcupacional");

                    b.HasKey("IdIndiceOcupacionalConocimientosAdicionales")
                        .HasName("PK_IndiceOcupacionalConocimientosAdicionales");

                    b.HasIndex("IdConocimientosAdicionales")
                        .HasName("IX_IndiceOcupacionalConocimientosAdicionales_IdConocimientosAdicionales");

                    b.HasIndex("IdIndiceOcupacional")
                        .HasName("IX_IndiceOcupacionalConocimientosAdicionales_IdIndiceOcupacional");

                    b.ToTable("IndiceOcupacionalConocimientosAdicionales");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.IndiceOcupacionalEstudio", b =>
                {
                    b.Property<int>("IdIndiceOcupacionalEstudio")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdEstudio");

                    b.Property<int>("IdIndiceOcupacional");

                    b.HasKey("IdIndiceOcupacionalEstudio")
                        .HasName("PK_IndiceOcupacionalEstudio");

                    b.HasIndex("IdEstudio")
                        .HasName("IX_IndiceOcupacionalEstudio_IdEstudio");

                    b.HasIndex("IdIndiceOcupacional")
                        .HasName("IX_IndiceOcupacionalEstudio_IdIndiceOcupacional");

                    b.ToTable("IndiceOcupacionalEstudio");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.IndiceOcupacionalModalidadPartida", b =>
                {
                    b.Property<int>("IdIndiceOcupacionalModalidadPartida")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("IdEmpleado");

                    b.Property<int?>("IdFondoFinanciamiento");

                    b.Property<int>("IdIndiceOcupacional");

                    b.Property<int?>("IdModalidadPartida");

                    b.Property<int?>("IdTipoNombramiento");

                    b.Property<decimal?>("SalarioReal")
                        .IsRequired()
                        .HasColumnType("decimal");

                    b.HasKey("IdIndiceOcupacionalModalidadPartida")
                        .HasName("PK_IndiceOcupacionalModalidadPartida");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_IndiceOcupacionalModalidadPartida_IdEmpleado");

                    b.HasIndex("IdFondoFinanciamiento")
                        .HasName("IX_IndiceOcupacionalModalidadPartida_IdFondoFinanciamiento");

                    b.HasIndex("IdIndiceOcupacional")
                        .HasName("IX_IndiceOcupacionalModalidadPartida_IdIndiceOcupacional");

                    b.HasIndex("IdModalidadPartida")
                        .HasName("IX_IndiceOcupacionalModalidadPartida_IdModalidadPartida");

                    b.HasIndex("IdTipoNombramiento")
                        .HasName("IX_IndiceOcupacionalModalidadPartida_IdTipoNombramiento");

                    b.ToTable("IndiceOcupacionalModalidadPartida");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.InformeUATH", b =>
                {
                    b.Property<int>("IdInformeUATH")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IdInformeUATH");

                    b.Property<int>("IdAdministracionTalentoHumano");

                    b.Property<int>("IdManualPuestoDestino");

                    b.Property<int>("IdManualPuestoOrigen");

                    b.Property<bool?>("Revizar")
                        .IsRequired();

                    b.HasKey("IdInformeUATH")
                        .HasName("PK_InformeUATH");

                    b.HasIndex("IdAdministracionTalentoHumano")
                        .HasName("IX_InformeUATH_IdAdministracionTalentoHumano");

                    b.HasIndex("IdManualPuestoDestino")
                        .HasName("IX_InformeUATH_IdManualPuestoDestino");

                    b.HasIndex("IdManualPuestoOrigen")
                        .HasName("IX_InformeUATH_IdManualPuestoOrigen");

                    b.ToTable("InformeUATH");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.InformeViatico", b =>
                {
                    b.Property<int>("IdInformeViatico")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("IdItinerarioViatico");

                    b.HasKey("IdInformeViatico")
                        .HasName("PK_InformeViatico");

                    b.HasIndex("IdItinerarioViatico")
                        .HasName("IX_InformeViatico_IdItinerarioViatico");

                    b.ToTable("InformeViatico");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.IngresoEgresoRMU", b =>
                {
                    b.Property<int>("IdIngresoEgresoRMU")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IdIngresoEgresoRMU");

                    b.Property<string>("CuentaContable")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("IdFormulaRMU")
                        .HasColumnName("IdFormulaRMU");

                    b.HasKey("IdIngresoEgresoRMU")
                        .HasName("PK_IngresoEgresoRMU");

                    b.HasIndex("IdFormulaRMU")
                        .HasName("IX_IngresoEgresoRMU_FormulasRMUIdFormulaRMU");

                    b.ToTable("IngresoEgresoRMU");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.InstitucionFinanciera", b =>
                {
                    b.Property<int>("IdInstitucionFinanciera")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("SPI");

                    b.HasKey("IdInstitucionFinanciera")
                        .HasName("PK_InstitucionFinanciera");

                    b.ToTable("InstitucionFinanciera");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.InstruccionFormal", b =>
                {
                    b.Property<int>("IdInstruccionFormal")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdInstruccionFormal");

                    b.ToTable("InstruccionFormal");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ItemViatico", b =>
                {
                    b.Property<int>("IdItemViatico")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descipcion")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdItemViatico")
                        .HasName("PK_ItemViatico");

                    b.ToTable("ItemViatico");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ItinerarioViatico", b =>
                {
                    b.Property<int>("IdItinerarioViatico")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTime?>("FechaDesde")
                        .IsRequired();

                    b.Property<DateTime?>("FechaHasta")
                        .IsRequired();

                    b.Property<int>("IdCiudad");

                    b.Property<int>("IdPais");

                    b.Property<int>("IdSolicitudViatico");

                    b.Property<int>("IdTipoTransporte");

                    b.HasKey("IdItinerarioViatico")
                        .HasName("PK_ItinerarioViatico");

                    b.HasIndex("IdCiudad")
                        .HasName("IX_ItinerarioViatico_IdCiudad");

                    b.HasIndex("IdPais")
                        .HasName("IX_ItinerarioViatico_IdPais");

                    b.HasIndex("IdSolicitudViatico")
                        .HasName("IX_ItinerarioViatico_IdSolicitudViatico");

                    b.HasIndex("IdTipoTransporte")
                        .HasName("IX_ItinerarioViatico_IdTipoTransporte");

                    b.ToTable("ItinerarioViatico");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Liquidacion", b =>
                {
                    b.Property<int>("IdLiquidacion")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("IdEmpleado");

                    b.Property<int>("IdRubroLiquidacion");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal");

                    b.HasKey("IdLiquidacion")
                        .HasName("PK_Liquidacion");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_Liquidacion_IdEmpleado");

                    b.HasIndex("IdRubroLiquidacion")
                        .HasName("IX_Liquidacion_IdRubroLiquidacion");

                    b.ToTable("Liquidacion");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ManualPuesto", b =>
                {
                    b.Property<int>("IdManualPuesto")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdManualPuesto")
                        .HasName("PK_ManualPuesto");

                    b.ToTable("ManualPuesto");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.MaterialApoyo", b =>
                {
                    b.Property<int>("IdMaterialApoyo")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FormularioDevengacionId");

                    b.Property<string>("NombreDocumento");

                    b.Property<string>("Ubicacion");

                    b.HasKey("IdMaterialApoyo")
                        .HasName("PK_MaterialApoyo");

                    b.HasIndex("FormularioDevengacionId")
                        .HasName("IX_MaterialApoyo_FormularioDevengacionId");

                    b.ToTable("MaterialApoyo");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Mision", b =>
                {
                    b.Property<int>("IdMision")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired();

                    b.HasKey("IdMision")
                        .HasName("PK_Mision");

                    b.ToTable("Mision");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.MisionIndiceOcupacional", b =>
                {
                    b.Property<int>("IdMisionIndiceOcupacional")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdIndiceOcupacional")
                        .HasColumnName("idIndiceOcupacional");

                    b.Property<int>("IdMision");

                    b.HasKey("IdMisionIndiceOcupacional")
                        .HasName("PK_MisionIndiceOcupacional");

                    b.HasIndex("IdIndiceOcupacional")
                        .HasName("IX_MisionIndiceOcupacional_idIndiceOcupacional");

                    b.HasIndex("IdMision")
                        .HasName("IX_MisionIndiceOcupacional_IdMision");

                    b.ToTable("MisionIndiceOcupacional");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ModalidadPartida", b =>
                {
                    b.Property<int>("IdModalidadPartida")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdRelacionLaboral");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdModalidadPartida")
                        .HasName("PK_ModalidadPartida");

                    b.HasIndex("IdRelacionLaboral")
                        .HasName("IX_ModalidadPartida_IdRelacionLaboral");

                    b.ToTable("ModalidadPartida");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ModosScializacion", b =>
                {
                    b.Property<int>("IdModosScializacion")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdModosScializacion")
                        .HasName("PK_ModosScializacion");

                    b.ToTable("ModosScializacion");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Nacionalidad", b =>
                {
                    b.Property<int>("IdNacionalidad")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdNacionalidad")
                        .HasName("PK_Nacionalidad");

                    b.ToTable("Nacionalidad");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.NacionalidadIndigena", b =>
                {
                    b.Property<int>("IdNacionalidadIndigena")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdEtnia");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdNacionalidadIndigena")
                        .HasName("PK_NacionalidadIndigena");

                    b.HasIndex("IdEtnia")
                        .HasName("IX_NacionalidadIndigena_IdEtnia");

                    b.ToTable("NacionalidadIndigena");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Nivel", b =>
                {
                    b.Property<int>("IdNivel")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdNivel")
                        .HasName("PK_Nivel");

                    b.ToTable("Nivel");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.NivelConocimiento", b =>
                {
                    b.Property<int>("IdNivelConocimiento")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdNivelConocimiento")
                        .HasName("PK_NivelConocimiento");

                    b.ToTable("NivelConocimiento");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.NivelDesarrollo", b =>
                {
                    b.Property<int>("IdNivelDesarrollo")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdNivelDesarrollo")
                        .HasName("PK_NivelDesarrollo");

                    b.ToTable("NivelDesarrollo");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Pais", b =>
                {
                    b.Property<int>("IdPais")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdPais")
                        .HasName("PK_Pais");

                    b.ToTable("Pais");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.PaquetesInformaticos", b =>
                {
                    b.Property<int>("IdPaquetesInformaticos")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdPaquetesInformaticos")
                        .HasName("PK_PaquetesInformaticos");

                    b.ToTable("PaquetesInformaticos");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ParametrosGenerales", b =>
                {
                    b.Property<int>("IdParametrosGenerales")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre");

                    b.Property<string>("Valor");

                    b.HasKey("IdParametrosGenerales");

                    b.ToTable("ParametrosGenerales");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Parentesco", b =>
                {
                    b.Property<int>("IdParentesco")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdParentesco")
                        .HasName("PK_Parentesco");

                    b.ToTable("Parentesco");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Parroquia", b =>
                {
                    b.Property<int>("IdParroquia")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdCiudad");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdParroquia")
                        .HasName("PK_Parroquia");

                    b.HasIndex("IdCiudad")
                        .HasName("IX_Parroquia_IdCiudad");

                    b.ToTable("Parroquia");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.PartidasFase", b =>
                {
                    b.Property<int>("IdPartidasFase")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Ganador")
                        .IsRequired();

                    b.Property<int>("IdFaseConcurso");

                    b.Property<int>("IdIndiceOcupacionalModalidadPartida");

                    b.HasKey("IdPartidasFase")
                        .HasName("PK_PartidasFase");

                    b.HasIndex("IdFaseConcurso")
                        .HasName("IX_PartidasFase_IdFaseConcurso");

                    b.HasIndex("IdIndiceOcupacionalModalidadPartida")
                        .HasName("IX_PartidasFase_IdIndiceOcupacionalModalidadPartida");

                    b.ToTable("PartidasFase");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Permiso", b =>
                {
                    b.Property<int>("IdPermiso")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdTipoPermiso");

                    b.HasKey("IdPermiso")
                        .HasName("PK_Permiso");

                    b.HasIndex("IdTipoPermiso")
                        .HasName("IX_Permiso_IdTipoPermiso");

                    b.ToTable("Permiso");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Persona", b =>
                {
                    b.Property<int>("IdPersona")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("CorreoPrivado")
                        .IsRequired();

                    b.Property<DateTime?>("FechaNacimiento")
                        .IsRequired();

                    b.Property<int?>("IdCanditato");

                    b.Property<int?>("IdEstadoCivil");

                    b.Property<int?>("IdEtnia");

                    b.Property<int>("IdGenero");

                    b.Property<int?>("IdNacionalidad");

                    b.Property<int?>("IdSexo");

                    b.Property<int?>("IdTipoIdentificacion");

                    b.Property<int?>("IdTipoSangre");

                    b.Property<string>("Identificacion")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("LugarTrabajo")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("TelefonoCasa")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("TelefonoPrivado")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdPersona")
                        .HasName("PK_Persona");

                    b.HasIndex("IdCanditato")
                        .HasName("IX_Persona_IdCanditato");

                    b.HasIndex("IdEstadoCivil")
                        .HasName("IX_Persona_IdEstadoCivil");

                    b.HasIndex("IdEtnia")
                        .HasName("IX_Persona_IdEtnia");

                    b.HasIndex("IdGenero")
                        .HasName("IX_Persona_IdGenero");

                    b.HasIndex("IdNacionalidad")
                        .HasName("IX_Persona_IdNacionalidad");

                    b.HasIndex("IdSexo")
                        .HasName("IX_Persona_IdSexo");

                    b.HasIndex("IdTipoIdentificacion")
                        .HasName("IX_Persona_IdTipoIdentificacion");

                    b.HasIndex("IdTipoSangre")
                        .HasName("IX_Persona_IdTipoSangre");

                    b.ToTable("Persona");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.PersonaCapacitacion", b =>
                {
                    b.Property<int>("IdPersonaCapacitacion")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Fecha")
                        .IsRequired();

                    b.Property<int>("IdCapacitacion");

                    b.Property<int>("IdPersona");

                    b.Property<string>("Observaciones")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.HasKey("IdPersonaCapacitacion")
                        .HasName("PK_PersonaCapacitacion");

                    b.HasIndex("IdCapacitacion")
                        .HasName("IX_PersonaCapacitacion_IdCapacitacion");

                    b.HasIndex("IdPersona")
                        .HasName("IX_PersonaCapacitacion_IdPersona");

                    b.ToTable("PersonaCapacitacion");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.PersonaDiscapacidad", b =>
                {
                    b.Property<int>("IdPersonaDiscapacidad")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("IdPersona");

                    b.Property<int?>("IdTipoDiscapacidad");

                    b.Property<string>("NumeroCarnet")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("Porciento");

                    b.HasKey("IdPersonaDiscapacidad")
                        .HasName("PK_PersonaDiscapacidad");

                    b.HasIndex("IdPersona")
                        .HasName("IX_PersonaDiscapacidad_IdPersona");

                    b.HasIndex("IdTipoDiscapacidad")
                        .HasName("IX_PersonaDiscapacidad_IdTipoDiscapacidad");

                    b.ToTable("PersonaDiscapacidad");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.PersonaEnfermedad", b =>
                {
                    b.Property<int>("IdPersonaEnfermedad")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("IdPersona");

                    b.Property<int?>("IdTipoEnfermedad");

                    b.Property<string>("InstitucionEmite")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdPersonaEnfermedad")
                        .HasName("PK_PersonaEnfermedad");

                    b.HasIndex("IdPersona")
                        .HasName("IX_PersonaEnfermedad_IdPersona");

                    b.HasIndex("IdTipoEnfermedad")
                        .HasName("IX_PersonaEnfermedad_IdTipoEnfermedad");

                    b.ToTable("PersonaEnfermedad");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.PersonaEstudio", b =>
                {
                    b.Property<int>("IdPersonaEstudio")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("FechaGraduado")
                        .IsRequired();

                    b.Property<int>("IdPersona");

                    b.Property<int>("IdTitulo");

                    b.Property<string>("Observaciones")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.HasKey("IdPersonaEstudio")
                        .HasName("PK_PersonaEstudio");

                    b.HasIndex("IdPersona")
                        .HasName("IX_PersonaEstudio_IdPersona");

                    b.HasIndex("IdTitulo")
                        .HasName("IX_PersonaEstudio_IdTitulo");

                    b.ToTable("PersonaEstudio");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.PersonaPaquetesInformaticos", b =>
                {
                    b.Property<int>("IdPersonaPaquetesInformaticos")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdEmpleado");

                    b.Property<int>("IdPaquetesInformaticos");

                    b.HasKey("IdPersonaPaquetesInformaticos")
                        .HasName("PK_PersonaPaquetesInformaticos");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_PersonaPaquetesInformaticos_IdEmpleado");

                    b.HasIndex("IdPaquetesInformaticos")
                        .HasName("IX_PersonaPaquetesInformaticos_IdPaquetesInformaticos");

                    b.ToTable("PersonaPaquetesInformaticos");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.PlanGestionCambio", b =>
                {
                    b.Property<int>("IdPlanGestionCambio")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AprobadoPor");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTime?>("FechaFin")
                        .IsRequired();

                    b.Property<DateTime?>("FechaInicio")
                        .IsRequired();

                    b.Property<int>("RealizadoPor");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdPlanGestionCambio")
                        .HasName("PK_PlanGestionCambio");

                    b.HasIndex("AprobadoPor")
                        .HasName("IX_PlanGestionCambio_EmpleadoIdAprobadoPor");

                    b.HasIndex("RealizadoPor")
                        .HasName("IX_PlanGestionCambio_EmpleadoIdRealizadoPor");

                    b.ToTable("PlanGestionCambio");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.PlanificacionHE", b =>
                {
                    b.Property<int>("IdPlanificacionHE")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IdPlanificacionHE");

                    b.Property<bool?>("Estado")
                        .IsRequired();

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("IdEmpleado");

                    b.Property<decimal>("NoHoras")
                        .HasColumnType("decimal");

                    b.HasKey("IdPlanificacionHE")
                        .HasName("PK_PlanificacionHE");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_PlanificacionHE_IdEmpleado");

                    b.ToTable("PlanificacionHE");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Pregunta", b =>
                {
                    b.Property<int>("IdPregunta")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdEvaluacionInduccion");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("IdPregunta")
                        .HasName("PK_Pregunta");

                    b.HasIndex("IdEvaluacionInduccion")
                        .HasName("IX_Pregunta_IdEvaluacionInduccion");

                    b.ToTable("Pregunta");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.PreguntaRespuesta", b =>
                {
                    b.Property<int>("IdPreguntaRespuesta")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdPregunta");

                    b.Property<int>("IdRespuesta");

                    b.Property<bool?>("Verdadero")
                        .IsRequired();

                    b.HasKey("IdPreguntaRespuesta")
                        .HasName("PK_PreguntaRespuesta");

                    b.HasIndex("IdPregunta")
                        .HasName("IX_PreguntaRespuesta_IdPregunta");

                    b.HasIndex("IdRespuesta")
                        .HasName("IX_PreguntaRespuesta_IdRespuesta");

                    b.ToTable("PreguntaRespuesta");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Proceso", b =>
                {
                    b.Property<int>("IdProceso")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdProceso")
                        .HasName("PK_Proceso");

                    b.ToTable("Proceso");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ProcesoDetalle", b =>
                {
                    b.Property<int>("IdProcesoDetalle")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("IdDependencia");

                    b.Property<int>("IdProceso");

                    b.HasKey("IdProcesoDetalle")
                        .HasName("PK_ProcesoDetalle");

                    b.HasIndex("IdDependencia")
                        .HasName("IX_ProcesoDetalle_IdDependencia");

                    b.HasIndex("IdProceso")
                        .HasName("IX_ProcesoDetalle_IdProceso");

                    b.ToTable("ProcesoDetalle");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Provincia", b =>
                {
                    b.Property<int>("IdProvincia")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdPais");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdProvincia")
                        .HasName("PK_Provincia");

                    b.HasIndex("IdPais")
                        .HasName("IX_Provincia_IdPais");

                    b.ToTable("Provincia");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Provisiones", b =>
                {
                    b.Property<int>("IdProvisiones")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("IdEmpleado");

                    b.Property<int>("IdTipoProvision");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal");

                    b.HasKey("IdProvisiones")
                        .HasName("PK_Provisiones");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_Provisiones_IdEmpleado");

                    b.HasIndex("IdTipoProvision")
                        .HasName("IX_Provisiones_IdTipoProvision");

                    b.ToTable("Provisiones");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.RealizaExamenInduccion", b =>
                {
                    b.Property<int>("IdRealizaExamenInduccion")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal?>("Calificacion")
                        .IsRequired()
                        .HasColumnType("decimal");

                    b.Property<DateTime?>("Fecha")
                        .IsRequired();

                    b.Property<int>("IdEmpleado");

                    b.Property<int>("IdEvaluacionInduccion");

                    b.HasKey("IdRealizaExamenInduccion")
                        .HasName("PK_RealizaExamenInduccion");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_RealizaExamenInduccion_IdEmpleado");

                    b.HasIndex("IdEvaluacionInduccion")
                        .HasName("IX_RealizaExamenInduccion_IdEvaluacionInduccion");

                    b.ToTable("RealizaExamenInduccion");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.RegimenLaboral", b =>
                {
                    b.Property<int>("IdRegimenLaboral")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdRegimenLaboral")
                        .HasName("PK_RegimenLaboral");

                    b.ToTable("RegimenLaboral");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.RegistroEntradaSalida", b =>
                {
                    b.Property<int>("IdRegistroEntradaSalida")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Fecha");

                    b.Property<TimeSpan?>("HoraEntrada")
                        .IsRequired();

                    b.Property<TimeSpan?>("HoraSalida")
                        .IsRequired();

                    b.Property<int>("IdEmpleado")
                        .HasMaxLength(20);

                    b.HasKey("IdRegistroEntradaSalida")
                        .HasName("PK_RegistroEntradaSalida");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_RegistroEntradaSalida_IdEmpleado");

                    b.ToTable("RegistroEntradaSalida");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.RelacionesInternasExternas", b =>
                {
                    b.Property<int>("IdRelacionesInternasExternas")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RelacionesInternasExternasId");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdRelacionesInternasExternas")
                        .HasName("PK_RelacionesInternasExternas");

                    b.ToTable("RelacionesInternasExternas");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.RelacionesInternasExternasIndiceOcupacional", b =>
                {
                    b.Property<int>("IdRelacionesInternasExternasIndiceOcupacional")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdIndiceOcupacional");

                    b.Property<int>("RelacionesInternasExternasId");

                    b.HasKey("IdRelacionesInternasExternasIndiceOcupacional")
                        .HasName("PK_RelacionesInternasExternasIndiceOcupacional");

                    b.HasIndex("IdIndiceOcupacional")
                        .HasName("IX_RelacionesInternasExternasIndiceOcupacional_IdIndiceOcupacional");

                    b.HasIndex("RelacionesInternasExternasId")
                        .HasName("IX_RelacionesInternasExternasIndiceOcupacional_RelacionesInternasExternasId");

                    b.ToTable("RelacionesInternasExternasIndiceOcupacional");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.RelacionLaboral", b =>
                {
                    b.Property<int>("IdRelacionLaboral")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("IdRegimenLaboral");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdRelacionLaboral")
                        .HasName("PK_RelacionLaboral");

                    b.HasIndex("IdRegimenLaboral")
                        .HasName("IX_RelacionLaboral_IdRegimenLaboral");

                    b.ToTable("RelacionLaboral");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Relevancia", b =>
                {
                    b.Property<int>("IdRelevancia")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ComportamientoObservable")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdRelevancia")
                        .HasName("PK_Relevancia");

                    b.ToTable("Relevancia");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.RequisitosNoCumple", b =>
                {
                    b.Property<int>("IdRequisitosNoCumple")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdministracionTalentoHumanoId");

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("IdRequisitosNoCumple")
                        .HasName("PK_RequisitosNoCumple");

                    b.HasIndex("AdministracionTalentoHumanoId")
                        .HasName("IX_RequisitosNoCumple_AdministracionTalentoHumanoId");

                    b.ToTable("RequisitosNoCumple");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Respuesta", b =>
                {
                    b.Property<int>("IdRespuesta")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RespuestaId");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(400);

                    b.HasKey("IdRespuesta")
                        .HasName("PK_Respuesta");

                    b.ToTable("Respuesta");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.RMU", b =>
                {
                    b.Property<int>("IdRMU")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IdRMU");

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("IdEmpeladoIE")
                        .HasColumnName("IdEmpeladoIE");

                    b.Property<int>("IdEmpleado");

                    b.Property<int>("IdTipoRMU")
                        .HasColumnName("IdTipoRMU");

                    b.Property<int>("Quincena");

                    b.Property<decimal?>("Valor")
                        .IsRequired()
                        .HasColumnType("decimal");

                    b.HasKey("IdRMU")
                        .HasName("PK_RMU");

                    b.HasIndex("IdEmpeladoIE")
                        .HasName("IX_RMU_IdEmpeladoIE");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_RMU_IdEmpleado");

                    b.HasIndex("IdTipoRMU")
                        .HasName("IX_RMU_IdTipoRMU");

                    b.ToTable("RMU");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.RolPagoDetalle", b =>
                {
                    b.Property<int>("IdRolPagoDetalle")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("EsIngreso");

                    b.Property<int>("IdRolPagos");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal");

                    b.HasKey("IdRolPagoDetalle")
                        .HasName("PK_RolPagoDetalle");

                    b.HasIndex("IdRolPagos")
                        .HasName("IX_RolPagoDetalle_IdRolPagos");

                    b.ToTable("RolPagoDetalle");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.RolPagos", b =>
                {
                    b.Property<int>("IdRolPagos")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("IdEmpleado");

                    b.Property<decimal>("SaldoFinal")
                        .HasColumnType("decimal");

                    b.Property<decimal>("SaldoInicial")
                        .HasColumnType("decimal");

                    b.HasKey("IdRolPagos")
                        .HasName("PK_RolPagos");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_RolPagos_IdEmpleado");

                    b.ToTable("RolPagos");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.RolPuesto", b =>
                {
                    b.Property<int>("IdRolPuesto")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdRolPuesto")
                        .HasName("PK_RolPuesto");

                    b.ToTable("RolPuesto");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Rubro", b =>
                {
                    b.Property<int>("IdRubro")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<decimal?>("TasaPorcentualMaxima")
                        .IsRequired()
                        .HasColumnType("decimal");

                    b.HasKey("IdRubro")
                        .HasName("PK_Rubro");

                    b.ToTable("Rubro");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.RubroLiquidacion", b =>
                {
                    b.Property<int>("IdRubroLiquidacion")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdRubroLiquidacion")
                        .HasName("PK_RubroLiquidacion");

                    b.ToTable("RubroLiquidacion");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Sexo", b =>
                {
                    b.Property<int>("IdSexo")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdSexo")
                        .HasName("PK_Sexo");

                    b.ToTable("Sexo");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.SituacionPropuesta", b =>
                {
                    b.Property<int>("IdSituacionPropuesta")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Ano")
                        .IsRequired();

                    b.Property<int?>("Brecha")
                        .IsRequired();

                    b.Property<int?>("IdDependencia");

                    b.Property<int?>("NumeroPropuesta")
                        .IsRequired();

                    b.Property<string>("Observaciones")
                        .IsRequired()
                        .HasMaxLength(400);

                    b.HasKey("IdSituacionPropuesta")
                        .HasName("PK_SituacionPropuesta");

                    b.HasIndex("IdDependencia")
                        .HasName("IX_SituacionPropuesta_IdDependencia");

                    b.ToTable("SituacionPropuesta");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.SolicitudAcumulacionDecimos", b =>
                {
                    b.Property<int>("IdSolicitudAcumulacionDecimos")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AcumulaDecimoCuarto");

                    b.Property<bool>("AcumulaDecimoTercero");

                    b.Property<DateTime>("FechaSolicitud");

                    b.Property<int>("IdEmpleado");

                    b.HasKey("IdSolicitudAcumulacionDecimos")
                        .HasName("PK_SolicitudAcumulacionDecimos");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_SolicitudAcumulacionDecimos_IdEmpleado");

                    b.ToTable("SolicitudAcumulacionDecimos");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.SolicitudAnticipo", b =>
                {
                    b.Property<int>("IdSolicitudAnticipo")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("CantidadCancelada")
                        .HasColumnType("decimal");

                    b.Property<decimal>("CantidadSolicitada")
                        .HasColumnType("decimal");

                    b.Property<DateTime?>("FechaAprobado")
                        .IsRequired();

                    b.Property<DateTime?>("FechaSolicitud")
                        .IsRequired();

                    b.Property<int>("IdEmpleado");

                    b.Property<int?>("IdEstado");

                    b.HasKey("IdSolicitudAnticipo")
                        .HasName("PK_SolicitudAnticipo");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_SolicitudAnticipo_IdEmpleado");

                    b.HasIndex("IdEstado")
                        .HasName("IX_SolicitudAnticipo_IdEstado");

                    b.ToTable("SolicitudAnticipo");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.SolicitudCertificadoPersonal", b =>
                {
                    b.Property<int>("IdSolicitudCertificadoPersonal")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("FechaSolicitud");

                    b.Property<int?>("IdEmpleadoDirigidoA");

                    b.Property<int?>("IdEmpleadoSolicitante");

                    b.Property<int>("IdEstado");

                    b.Property<int>("IdTipoCertificado");

                    b.Property<string>("Observaciones")
                        .HasMaxLength(20);

                    b.HasKey("IdSolicitudCertificadoPersonal")
                        .HasName("PK_SolicitudCertificadoPersonal");

                    b.HasIndex("IdEmpleadoDirigidoA")
                        .HasName("IX_SolicitudCertificadoPersonal_EmpleadoSolicitanteIdEmpleadoDirigidoA");

                    b.HasIndex("IdEmpleadoSolicitante")
                        .HasName("IX_SolicitudCertificadoPersonal_EmpleadoSolicitanteIdEmpleadoSolicitante");

                    b.HasIndex("IdEstado")
                        .HasName("IX_SolicitudCertificadoPersonal_IdEstado");

                    b.HasIndex("IdTipoCertificado")
                        .HasName("IX_SolicitudCertificadoPersonal_IdTipoCertificado");

                    b.ToTable("SolicitudCertificadoPersonal");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.SolicitudHorasExtras", b =>
                {
                    b.Property<long>("IdSolicitudHorasExtras")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Aprobado")
                        .IsRequired();

                    b.Property<int?>("CantidadHoras")
                        .IsRequired();

                    b.Property<DateTime?>("Fecha")
                        .IsRequired();

                    b.Property<DateTime?>("FechaAprobado")
                        .IsRequired();

                    b.Property<DateTime?>("FechaSolicitud")
                        .IsRequired();

                    b.Property<int>("IdEmpleado");

                    b.Property<string>("Observaciones")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdSolicitudHorasExtras")
                        .HasName("PK_SolicitudHorasExtras");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_SolicitudHorasExtras_IdEmpleado");

                    b.ToTable("SolicitudHorasExtras");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.SolicitudLiquidacionHaberes", b =>
                {
                    b.Property<int>("IdSolicitudLiquidacionHaberes")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("FechaSolicitud");

                    b.Property<int?>("IdEmpleado");

                    b.Property<decimal>("TotalDecimoCuarto")
                        .HasColumnType("decimal");

                    b.Property<decimal>("TotalDecimoTercero")
                        .HasColumnType("decimal");

                    b.Property<decimal?>("TotalDesahucio")
                        .IsRequired()
                        .HasColumnType("decimal");

                    b.Property<decimal?>("TotalDespidoIntempestivo")
                        .IsRequired()
                        .HasColumnType("decimal");

                    b.Property<decimal?>("TotalFondoReserva")
                        .IsRequired()
                        .HasColumnType("decimal");

                    b.Property<decimal>("TotalVacaciones")
                        .HasColumnType("decimal");

                    b.HasKey("IdSolicitudLiquidacionHaberes")
                        .HasName("PK_SolicitudLiquidacionHaberes");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_SolicitudLiquidacionHaberes_IdEmpleado");

                    b.ToTable("SolicitudLiquidacionHaberes");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.SolicitudModificacionFichaEmpleado", b =>
                {
                    b.Property<int>("IdSolicitudModificacionFichaEmpleado")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Apellidos");

                    b.Property<string>("CapacitacionesRecibidas");

                    b.Property<string>("Direccion");

                    b.Property<string>("ExperienciaLaboral");

                    b.Property<DateTime>("FechaSolicitud");

                    b.Property<string>("FormacionAcademica");

                    b.Property<int>("IdEmpleado");

                    b.Property<int?>("IdEstado");

                    b.Property<string>("Nombres");

                    b.Property<string>("ParentescosFamiliares");

                    b.Property<string>("TelefonoCasa");

                    b.Property<string>("TelefonoPrivado");

                    b.HasKey("IdSolicitudModificacionFichaEmpleado")
                        .HasName("PK_SolicitudModificacionFichaEmpleado");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_SolicitudModificacionFichaEmpleado_IdEmpleado");

                    b.HasIndex("IdEstado")
                        .HasName("IX_SolicitudModificacionFichaEmpleado_IdEstado");

                    b.ToTable("SolicitudModificacionFichaEmpleado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.SolicitudPermiso", b =>
                {
                    b.Property<int>("IdSolicitudPermiso")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("FechaAprobado")
                        .IsRequired();

                    b.Property<DateTime>("FechaSolicitud");

                    b.Property<TimeSpan?>("HoraDesde")
                        .IsRequired();

                    b.Property<TimeSpan?>("HoraHasta")
                        .IsRequired();

                    b.Property<int>("IdEmpleado");

                    b.Property<int?>("IdEstado");

                    b.Property<int?>("IdPermiso");

                    b.Property<string>("Motivo")
                        .IsRequired()
                        .HasMaxLength(400);

                    b.HasKey("IdSolicitudPermiso")
                        .HasName("PK_SolicitudPermiso");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_SolicitudPermiso_IdEmpleado");

                    b.HasIndex("IdEstado")
                        .HasName("IX_SolicitudPermiso_IdEstado");

                    b.HasIndex("IdPermiso")
                        .HasName("IX_SolicitudPermiso_IdPermiso");

                    b.ToTable("SolicitudPermiso");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.SolicitudPlanificacionVacaciones", b =>
                {
                    b.Property<int>("IdSolicitudPlanificacionVacaciones")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("FechaDesde");

                    b.Property<DateTime>("FechaHasta");

                    b.Property<DateTime>("FechaSolicitud");

                    b.Property<int>("IdEmpleado");

                    b.Property<int?>("IdEstado");

                    b.HasKey("IdSolicitudPlanificacionVacaciones")
                        .HasName("PK_SolicitudPlanificacionVacaciones");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_SolicitudPlanificacionVacaciones_IdEmpleado");

                    b.HasIndex("IdEstado")
                        .HasName("IX_SolicitudPlanificacionVacaciones_IdEstado");

                    b.ToTable("SolicitudPlanificacionVacaciones");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.SolicitudVacaciones", b =>
                {
                    b.Property<int>("IdSolicitudVacaciones")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("FechaAprobada")
                        .IsRequired();

                    b.Property<DateTime>("FechaDesde");

                    b.Property<DateTime>("FechaHasta");

                    b.Property<DateTime>("FechaSolicitud");

                    b.Property<int>("IdEmpleado");

                    b.Property<int?>("IdEstado");

                    b.HasKey("IdSolicitudVacaciones")
                        .HasName("PK_SolicitudVacaciones");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_SolicitudVacaciones_IdEmpleado");

                    b.HasIndex("IdEstado")
                        .HasName("IX_SolicitudVacaciones_IdEstado");

                    b.ToTable("SolicitudVacaciones");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.SolicitudViatico", b =>
                {
                    b.Property<int>("IdSolicitudViatico")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime?>("FechaAprobado")
                        .IsRequired();

                    b.Property<DateTime>("FechaSolicitud");

                    b.Property<int>("IdConfiguracionViatico");

                    b.Property<int>("IdEmpleado");

                    b.Property<int?>("IdEstado");

                    b.Property<int>("IdTipoViatico");

                    b.Property<decimal?>("ValorEstimado")
                        .IsRequired()
                        .HasColumnType("decimal");

                    b.HasKey("IdSolicitudViatico")
                        .HasName("PK_SolicitudViatico");

                    b.HasIndex("IdConfiguracionViatico")
                        .HasName("IX_SolicitudViatico_IdConfiguracionViatico");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_SolicitudViatico_IdEmpleado");

                    b.HasIndex("IdEstado")
                        .HasName("IX_SolicitudViatico_IdEstado");

                    b.HasIndex("IdTipoViatico")
                        .HasName("IX_SolicitudViatico_IdTipoViatico");

                    b.ToTable("SolicitudViatico");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Sucursal", b =>
                {
                    b.Property<int>("IdSucursal")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdCiudad");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdSucursal")
                        .HasName("PK_Sucursal");

                    b.HasIndex("IdCiudad")
                        .HasName("IX_Sucursal_IdCiudad");

                    b.ToTable("Sucursal");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.TipoAccionPersonal", b =>
                {
                    b.Property<int>("IdTipoAccionPersonal")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("IdTipoAccionPersonal")
                        .HasName("PK_TipoAccionPersonal");

                    b.ToTable("TipoAccionPersonal");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.TipoCertificado", b =>
                {
                    b.Property<int>("IdTipoCertificado")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdTipoCertificado")
                        .HasName("PK_TipoCertificado");

                    b.ToTable("TipoCertificado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.TipoConcurso", b =>
                {
                    b.Property<int>("IdTipoConcurso")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdTipoConcurso")
                        .HasName("PK_TipoConcurso");

                    b.ToTable("TipoConcurso");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.TipoDiscapacidad", b =>
                {
                    b.Property<int>("IdTipoDiscapacidad")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdTipoDiscapacidad")
                        .HasName("PK_TipoDiscapacidad");

                    b.ToTable("TipoDiscapacidad");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.TipoDiscapacidadSustituto", b =>
                {
                    b.Property<int>("IdTipoDiscapacidadSustituto")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdTipoDiscapacidadSustituto");

                    b.ToTable("TipoDiscapacidadSustituto");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.TipoEnfermedad", b =>
                {
                    b.Property<int>("IdTipoEnfermedad")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdTipoEnfermedad")
                        .HasName("PK_TipoEnfermedad");

                    b.ToTable("TipoEnfermedad");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.TipoIdentificacion", b =>
                {
                    b.Property<int>("IdTipoIdentificacion")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdTipoIdentificacion")
                        .HasName("PK_TipoIdentificacion");

                    b.ToTable("TipoIdentificacion");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.TipoMovimientoInterno", b =>
                {
                    b.Property<int>("IdTipoMovimientoInterno")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdTipoMovimientoInterno")
                        .HasName("PK_TipoMovimientoInterno");

                    b.ToTable("TipoMovimientoInterno");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.TipoNombramiento", b =>
                {
                    b.Property<int>("IdTipoNombramiento")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdRelacionLaboral");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdTipoNombramiento")
                        .HasName("PK_TipoNombramiento");

                    b.HasIndex("IdRelacionLaboral")
                        .HasName("IX_TipoNombramiento_IdRelacionLaboral");

                    b.ToTable("TipoNombramiento");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.TipoPermiso", b =>
                {
                    b.Property<int>("IdTipoPermiso")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdTipoPermiso")
                        .HasName("PK_TipoPermiso");

                    b.ToTable("TipoPermiso");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.TipoProvision", b =>
                {
                    b.Property<int>("IdTipoProvision")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdTipoProvision")
                        .HasName("PK_TipoProvision");

                    b.ToTable("TipoProvision");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.TipoRMU", b =>
                {
                    b.Property<int>("IdTipoRMU")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IdTipoRMU");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdTipoRMU")
                        .HasName("PK_TipoRMU");

                    b.ToTable("TipoRMU");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.TipoSangre", b =>
                {
                    b.Property<int>("IdTipoSangre")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdTipoSangre")
                        .HasName("PK_TipoSangre");

                    b.ToTable("TipoSangre");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.TipoTransporte", b =>
                {
                    b.Property<int>("IdTipoTransporte")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("IdTipoTransporte")
                        .HasName("PK_TipoTransporte");

                    b.ToTable("TipoTransporte");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.TipoViatico", b =>
                {
                    b.Property<int>("IdTipoViatico")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdTipoViatico")
                        .HasName("PK_TipoViatico");

                    b.ToTable("TipoViatico");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Titulo", b =>
                {
                    b.Property<int>("IdTitulo")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdEstudio");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdTitulo")
                        .HasName("PK_Titulo");

                    b.HasIndex("IdEstudio")
                        .HasName("IX_Titulo_IdEstudio");

                    b.ToTable("Titulo");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.TrabajoEquipoIniciativaLiderazgo", b =>
                {
                    b.Property<int>("IdTrabajoEquipoIniciativaLiderazgo")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("IdTrabajoEquipoIniciativaLiderazgo")
                        .HasName("PK_TrabajoEquipoIniciativaLiderazgo");

                    b.ToTable("TrabajoEquipoIniciativaLiderazgo");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.TrayectoriaLaboral", b =>
                {
                    b.Property<int>("IdTrayectoriaLaboral")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DescripcionFunciones");

                    b.Property<string>("Empresa")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("FechaFin");

                    b.Property<DateTime?>("FechaInicio");

                    b.Property<int>("IdPersona");

                    b.Property<string>("PuestoTrabajo")
                        .HasMaxLength(250);

                    b.HasKey("IdTrayectoriaLaboral")
                        .HasName("PK_TrayectoriaLaboral");

                    b.HasIndex("IdPersona")
                        .HasName("IX_TrayectoriaLaboral_IdPersona");

                    b.ToTable("TrayectoriaLaboral");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ValidacionInmediatoSuperior", b =>
                {
                    b.Property<int>("IdValidacionJefe")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Fecha");

                    b.Property<int>("IdEmpleado");

                    b.Property<int>("IdFormularioAnalisisOcupacional");

                    b.HasKey("IdValidacionJefe")
                        .HasName("PK_ValidacionInmediatoSuperior");

                    b.HasIndex("IdEmpleado")
                        .HasName("IX_ValidacionInmediatoSuperior_IdEmpleado");

                    b.HasIndex("IdFormularioAnalisisOcupacional")
                        .HasName("IX_ValidacionInmediatoSuperior_IdFormularioAnalisisOcupacional");

                    b.ToTable("ValidacionInmediatoSuperior");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.AccionPersonal", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("AccionPersonal")
                        .HasForeignKey("IdEmpleado");

                    b.HasOne("bd.swth.entidades.Negocio.TipoAccionPersonal", "TipoAccionPersonal")
                        .WithMany("AccionPersonal")
                        .HasForeignKey("IdTipoAccionPersonal");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ActividadesAnalisisOcupacional", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.FormularioAnalisisOcupacional", "FormularioAnalisisOcupacional")
                        .WithMany("ActividadesAnalisisOcupacional")
                        .HasForeignKey("IdFormularioAnalisisOcupacional");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ActividadesGestionCambio", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.PlanGestionCambio", "PlanGestionCambio")
                        .WithMany("ActividadesGestionCambio")
                        .HasForeignKey("IdPlanGestionCambio");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.AdministracionTalentoHumano", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("AdministracionTalentoHumano")
                        .HasForeignKey("EmpleadoResponsable");

                    b.HasOne("bd.swth.entidades.Negocio.FormularioAnalisisOcupacional", "FormularioAnalisisOcupacional")
                        .WithMany("AdministracionTalentoHumano")
                        .HasForeignKey("IdFormularioAnalisisOcupacional");

                    b.HasOne("bd.swth.entidades.Negocio.RolPuesto", "RolPuesto")
                        .WithMany("AdministracionTalentoHumano")
                        .HasForeignKey("IdRolPuesto");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.AprobacionViatico", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("AprobacionViatico")
                        .HasForeignKey("IdEmpleado");

                    b.HasOne("bd.swth.entidades.Negocio.SolicitudViatico", "SolicitudViatico")
                        .WithMany("AprobacionViatico")
                        .HasForeignKey("IdSolicitudViatico");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.AvanceGestionCambio", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.ActividadesGestionCambio", "ActividadesGestionCambio")
                        .WithMany("AvanceGestionCambio")
                        .HasForeignKey("IdActividadesGestionCambio");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.BrigadaSSORol", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.BrigadaSSO", "BrigadaSSO")
                        .WithMany("BrigadaSSORol")
                        .HasForeignKey("IdBrigadaSSO");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.CandidatoConcurso", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Canditato", "Canditato")
                        .WithMany("CandidatoConcurso")
                        .HasForeignKey("IdCanditato");

                    b.HasOne("bd.swth.entidades.Negocio.PartidasFase", "PartidasFase")
                        .WithMany("CandidatoConcurso")
                        .HasForeignKey("IdPartidasFase");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.CapacitacionEncuesta", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.CapacitacionRecibida", "CapacitacionRecibida")
                        .WithMany("CapacitacionEncuesta")
                        .HasForeignKey("IdCapacitacionRecibida");

                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("CapacitacionEncuesta")
                        .HasForeignKey("IdEmpleado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.CapacitacionPlanificacion", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.CapacitacionModalidad", "CapacitacionModalidad")
                        .WithMany("CapacitacionPlanificacion")
                        .HasForeignKey("IdCapacitacionModalidad");

                    b.HasOne("bd.swth.entidades.Negocio.CapacitacionTemario", "CapacitacionTemario")
                        .WithMany("CapacitacionPlanificacion")
                        .HasForeignKey("IdCapacitacionTemario");

                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("CapacitacionPlanificacion")
                        .HasForeignKey("IdEmpleado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.CapacitacionPregunta", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.CapacitacionEncuesta", "CapacitacionEncuesta")
                        .WithMany("CapacitacionPregunta")
                        .HasForeignKey("IdCapacitacionEncuesta");

                    b.HasOne("bd.swth.entidades.Negocio.CapacitacionTipoPregunta", "CapacitacionTipoPregunta")
                        .WithMany("CapacitacionPregunta")
                        .HasForeignKey("IdCapacitacionTipoPregunta");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.CapacitacionProveedor", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.CapacitacionRecibida", "CapacitacionRecibida")
                        .WithMany("CapacitacionProveedor")
                        .HasForeignKey("IdCapacitacionRecibida");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.CapacitacionRecibida", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.CapacitacionTemario", "CapacitacionTemario")
                        .WithMany("CapacitacionRecibida")
                        .HasForeignKey("IdCapacitacionTemario");

                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("CapacitacionRecibida")
                        .HasForeignKey("IdEmpleado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.CapacitacionRespuesta", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.CapacitacionPregunta", "CapacitacionPregunta")
                        .WithMany("CapacitacionRespuesta")
                        .HasForeignKey("IdCapacitacionPregunta");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.CapacitacionTemario", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.CapacitacionAreaConocimiento", "CapacitacionAreaConocimiento")
                        .WithMany("CapacitacionTemario")
                        .HasForeignKey("IdCapacitacionAreaConocimiento");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.CapacitacionTemarioProveedor", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.CapacitacionModalidad", "CapacitacionModalidad")
                        .WithMany("CapacitacionTemarioProveedor")
                        .HasForeignKey("IdCapacitacionModalidad");

                    b.HasOne("bd.swth.entidades.Negocio.CapacitacionProveedor", "CapacitacionProveedor")
                        .WithMany("CapacitacionTemarioProveedor")
                        .HasForeignKey("IdCapacitacionProveedor");

                    b.HasOne("bd.swth.entidades.Negocio.CapacitacionTemario", "CapacitacionTemario")
                        .WithMany("CapacitacionTemarioProveedor")
                        .HasForeignKey("IdCapacitacionTemario");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Ciudad", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Provincia", "Provincia")
                        .WithMany("Ciudad")
                        .HasForeignKey("IdProvincia");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ComportamientoObservable", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.DenominacionCompetencia", "DenominacionCompetencia")
                        .WithMany("ComportamientoObservable")
                        .HasForeignKey("DenominacionCompetenciaId");

                    b.HasOne("bd.swth.entidades.Negocio.Nivel", "Nivel")
                        .WithMany("ComportamientoObservable")
                        .HasForeignKey("NivelId");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ConfiguracionViatico", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Dependencia", "Dependencia")
                        .WithMany("ConfiguracionViatico")
                        .HasForeignKey("IdDependencia");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ConfirmacionLectura", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("ConfirmacionLectura")
                        .HasForeignKey("IdEmpleado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.DatosBancarios", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("DatosBancarios")
                        .HasForeignKey("IdEmpleado");

                    b.HasOne("bd.swth.entidades.Negocio.InstitucionFinanciera", "InstitucionFinanciera")
                        .WithMany("DatosBancarios")
                        .HasForeignKey("IdInstitucionFinanciera");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.DeclaracionPatrimonioPersonal", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("DeclaracionPatrimonioPersonal")
                        .HasForeignKey("IdEmpleado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Dependencia", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Dependencia", "DependenciaPadre")
                        .WithMany("Dependencia1")
                        .HasForeignKey("IdDependenciaPadre");

                    b.HasOne("bd.swth.entidades.Negocio.Sucursal", "Sucursal")
                        .WithMany("Dependencia")
                        .HasForeignKey("IdSucursal");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.DependenciaDocumento", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Dependencia", "Dependencia")
                        .WithMany("DependenciaDocumento")
                        .HasForeignKey("IdDependencia");

                    b.HasOne("bd.swth.entidades.Negocio.DocumentosParentescodos", "DocumentosParentescodos")
                        .WithMany("DependenciaDocumento")
                        .HasForeignKey("IdDocumentosSubidos");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.DetalleExamenInduccion", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Pregunta", "Pregunta")
                        .WithMany("DetalleExamenInduccion")
                        .HasForeignKey("PreguntaId");

                    b.HasOne("bd.swth.entidades.Negocio.RealizaExamenInduccion", "RealizaExamenInduccion")
                        .WithMany("DetalleExamenInduccion")
                        .HasForeignKey("RealizaExamenInduccionId");

                    b.HasOne("bd.swth.entidades.Negocio.Respuesta", "Respuesta")
                        .WithMany("DetalleExamenInduccion")
                        .HasForeignKey("RespuestaId");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.DocumentosParentescodos", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("DocumentosParentescodos")
                        .HasForeignKey("IdEmpleado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Empleado", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Ciudad", "CiudadNacimiento")
                        .WithMany("Empleado")
                        .HasForeignKey("IdCiudadLugarNacimiento");

                    b.HasOne("bd.swth.entidades.Negocio.Dependencia", "Dependencia")
                        .WithMany("Empleado")
                        .HasForeignKey("IdDependencia");

                    b.HasOne("bd.swth.entidades.Negocio.Persona", "Persona")
                        .WithMany("Empleado")
                        .HasForeignKey("IdPersona");

                    b.HasOne("bd.swth.entidades.Negocio.Provincia", "ProvinciaSufragio")
                        .WithMany("Empleado")
                        .HasForeignKey("IdProvinciaLugarSufragio");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EmpleadoContactoEmergencia", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("EmpleadoContactoEmergencia")
                        .HasForeignKey("IdEmpleado");

                    b.HasOne("bd.swth.entidades.Negocio.Parentesco", "Parentesco")
                        .WithMany("EmpleadoContactoEmergencia")
                        .HasForeignKey("IdParentesco");

                    b.HasOne("bd.swth.entidades.Negocio.Persona", "Persona")
                        .WithMany("EmpleadoContactoEmergencia")
                        .HasForeignKey("IdPersona");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EmpleadoFamiliar", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("EmpleadoFamiliar")
                        .HasForeignKey("IdEmpleado");

                    b.HasOne("bd.swth.entidades.Negocio.Parentesco", "Parentesco")
                        .WithMany("EmpleadoFamiliar")
                        .HasForeignKey("IdParentesco");

                    b.HasOne("bd.swth.entidades.Negocio.Persona", "Persona")
                        .WithMany("EmpleadoFamiliar")
                        .HasForeignKey("IdPersona");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EmpleadoFormularioCapacitacion", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.FormularioCapacitacion", "FormularioCapacitacion")
                        .WithMany("EmpleadoFormularioCapacitacion")
                        .HasForeignKey("IdFormularioCapacitacion");

                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Servidor")
                        .WithMany("EmpleadoFormularioCapacitacion")
                        .HasForeignKey("IdServidor");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EmpleadoIE", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("EmpleadoIE")
                        .HasForeignKey("IdEmpleado");

                    b.HasOne("bd.swth.entidades.Negocio.IngresoEgresoRMU", "IngresoEgresoRMU")
                        .WithMany("EmpleadoIE")
                        .HasForeignKey("IdIngresoEgresoRMU");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EmpleadoImpuestoRenta", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("EmpleadoImpuestoRenta")
                        .HasForeignKey("IdEmpleado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EmpleadoMovimiento", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("EmpleadoMovimiento")
                        .HasForeignKey("IdEmpleado");

                    b.HasOne("bd.swth.entidades.Negocio.IndiceOcupacionalModalidadPartida", "IndiceOcupacionalModalidadPartida")
                        .WithMany("EmpleadoMovimiento")
                        .HasForeignKey("IdIndiceOcupacionalModalidadPartida");

                    b.HasOne("bd.swth.entidades.Negocio.TipoMovimientoInterno", "TipoMovimientoInterno")
                        .WithMany("EmpleadoMovimiento")
                        .HasForeignKey("IdTipoMovimientoInterno");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EmpleadoNepotismo", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("EmpleadoNepotismo")
                        .HasForeignKey("IdEmpleado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EmpleadoSaldoVacaciones", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("EmpleadoSaldoVacaciones")
                        .HasForeignKey("IdEmpleado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EmpleadosFormularioDevengacion", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.FormularioDevengacion", "FormularioDevengacion")
                        .WithMany("EmpleadosFormularioDevengacion")
                        .HasForeignKey("FormularioDevengacionId");

                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("EmpleadosFormularioDevengacion")
                        .HasForeignKey("IdEmpleado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EscalaGrados", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.GrupoOcupacional", "GrupoOcupacional")
                        .WithMany("EscalaGrados")
                        .HasForeignKey("IdGrupoOcupacional");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Eval001", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("Eval001")
                        .HasForeignKey("IdEmpleadoEvaluado");

                    b.HasOne("bd.swth.entidades.Negocio.EscalaEvaluacionTotal", "EscalaEvaluacionTotal")
                        .WithMany("Eval001")
                        .HasForeignKey("IdEscalaEvaluacionTotal");

                    b.HasOne("bd.swth.entidades.Negocio.EvaluacionActividadesPuestoTrabajo", "EvaluacionActividadesPuestoTrabajo")
                        .WithMany("Eval001")
                        .HasForeignKey("IdEvaluacionActividadesPuestoTrabajo");

                    b.HasOne("bd.swth.entidades.Negocio.EvaluacionCompetenciasTecnicasPuesto", "EvaluacionCompetenciasTecnicasPuesto")
                        .WithMany("Eval001")
                        .HasForeignKey("IdEvaluacionCompetenciasTecnicasPuesto");

                    b.HasOne("bd.swth.entidades.Negocio.EvaluacionCompetenciasUniversales", "EvaluacionCompetenciasUniversales")
                        .WithMany("Eval001")
                        .HasForeignKey("IdEvaluacionCompetenciasUniversales");

                    b.HasOne("bd.swth.entidades.Negocio.EvaluacionConocimiento", "EvaluacionConocimiento")
                        .WithMany("Eval001")
                        .HasForeignKey("IdEvaluacionConocimiento");

                    b.HasOne("bd.swth.entidades.Negocio.EvaluacionTrabajoEquipoIniciativaLiderazgo", "EvaluacionTrabajoEquipoIniciativaLiderazgo")
                        .WithMany("Eval001")
                        .HasForeignKey("IdEvaluacionTrabajoEquipoIniciativaLiderazgo");

                    b.HasOne("bd.swth.entidades.Negocio.Evaluador", "Evaluador")
                        .WithMany("Eval001")
                        .HasForeignKey("IdEvaluador");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionActividadesPuestoTrabajoDetalle", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.EvaluacionActividadesPuestoTrabajo", "EvaluacionActividadesPuestoTrabajo")
                        .WithMany("EvaluacionActividadesPuestoTrabajoDetalle")
                        .HasForeignKey("IdEvaluacionActividadesPuestoTrabajo");

                    b.HasOne("bd.swth.entidades.Negocio.Indicador", "Indicador")
                        .WithMany("EvaluacionActividadesPuestoTrabajoDetalle")
                        .HasForeignKey("IdIndicador");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionActividadesPuestoTrabajoFactor", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.EvaluacionActividadesPuestoTrabajo", "EvaluacionActividadesPuestoTrabajo")
                        .WithMany("EvaluacionActividadesPuestoTrabajoFactor")
                        .HasForeignKey("IdEvaluacionActividadesPuestoTrabajo");

                    b.HasOne("bd.swth.entidades.Negocio.Factor", "Factor")
                        .WithMany("EvaluacionActividadesPuestoTrabajoFactor")
                        .HasForeignKey("IdFactor");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionCompetenciasTecnicasPuestoDetalle", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Destreza", "Destreza")
                        .WithMany("EvaluacionCompetenciasTecnicasPuestoDetalle")
                        .HasForeignKey("IdDestreza");

                    b.HasOne("bd.swth.entidades.Negocio.EvaluacionCompetenciasTecnicasPuesto", "EvaluacionCompetenciasTecnicasPuesto")
                        .WithMany("EvaluacionCompetenciasTecnicasPuestoDetalle")
                        .HasForeignKey("IdEvaluacionCompetenciasTecnicasPuesto");

                    b.HasOne("bd.swth.entidades.Negocio.NivelDesarrollo", "NivelDesarrollo")
                        .WithMany("EvaluacionCompetenciasTecnicasPuestoDetalle")
                        .HasForeignKey("IdNivelDesarrollo");

                    b.HasOne("bd.swth.entidades.Negocio.Relevancia", "Relevancia")
                        .WithMany("EvaluacionCompetenciasTecnicasPuestoDetalle")
                        .HasForeignKey("IdRelevancia");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionCompetenciasTecnicasPuestoFactor", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.EvaluacionCompetenciasTecnicasPuesto", "EvaluacionCompetenciasTecnicasPuesto")
                        .WithMany("EvaluacionCompetenciasTecnicasPuestoFactor")
                        .HasForeignKey("IdEvaluacionCompetenciasTecnicasPuesto");

                    b.HasOne("bd.swth.entidades.Negocio.Factor", "Factor")
                        .WithMany("EvaluacionCompetenciasTecnicasPuestoFactor")
                        .HasForeignKey("IdFactor");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionCompetenciasUniversalesDetalle", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Destreza", "Destreza")
                        .WithMany("EvaluacionCompetenciasUniversalesDetalle")
                        .HasForeignKey("IdDestreza");

                    b.HasOne("bd.swth.entidades.Negocio.EvaluacionCompetenciasUniversales", "EvaluacionCompetenciasUniversales")
                        .WithMany("EvaluacionCompetenciasUniversalesDetalle")
                        .HasForeignKey("IdEvaluacionCompetenciasUniversales")
                        .HasConstraintName("FK_EvaluacionCompUnivlesDetalle_EvaluacionCompsUni_IdEvalnCompsUniversales");

                    b.HasOne("bd.swth.entidades.Negocio.FrecuenciaAplicacion", "FrecuenciaAplicacion")
                        .WithMany("EvaluacionCompetenciasUniversalesDetalle")
                        .HasForeignKey("IdFrecuenciaAplicacion");

                    b.HasOne("bd.swth.entidades.Negocio.Relevancia", "Relevancia")
                        .WithMany("EvaluacionCompetenciasUniversalesDetalle")
                        .HasForeignKey("IdRelevancia");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionCompetenciasUniversalesFactor", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.EvaluacionCompetenciasUniversales", "EvaluacionCompetenciasUniversales")
                        .WithMany("EvaluacionCompetenciasUniversalesFactor")
                        .HasForeignKey("IdEvaluacionCompetenciasUniversales")
                        .HasConstraintName("FK_EvaluacionCompetenciasUniversalesFactor_EvaluacionCompetenciasUniversales_IdEvaCompUnives");

                    b.HasOne("bd.swth.entidades.Negocio.Factor", "Factor")
                        .WithMany("EvaluacionCompetenciasUniversalesFactor")
                        .HasForeignKey("IdFactor");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionConocimientoDetalle", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.EvaluacionConocimiento", "EvaluacionConocimiento")
                        .WithMany("EvaluacionConocimientoDetalle")
                        .HasForeignKey("IdEvaluacionConocimiento");

                    b.HasOne("bd.swth.entidades.Negocio.NivelConocimiento", "NivelConocimiento")
                        .WithMany("EvaluacionConocimientoDetalle")
                        .HasForeignKey("IdNivelConocimiento");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionConocimientoFactor", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.EvaluacionConocimiento", "EvaluacionConocimiento")
                        .WithMany("EvaluacionConocimientoFactor")
                        .HasForeignKey("IdEvaluacionConocimiento");

                    b.HasOne("bd.swth.entidades.Negocio.Factor", "Factor")
                        .WithMany("EvaluacionConocimientoFactor")
                        .HasForeignKey("IdFactor");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.EvaluacionTrabajoEquipoIniciativaLiderazgo", "EvaluacionTrabajoEquipoIniciativaLiderazgo")
                        .WithMany("EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle")
                        .HasForeignKey("IdEvaluacionTrabajoEquipoIniciativaLiderazgo")
                        .HasConstraintName("FK_EvaluacionTrabajoEquIniLidDetall_EvalTrabEquiIniLid_IdEvaluacionTrabajoEquipoIniciativaLiderazgo");

                    b.HasOne("bd.swth.entidades.Negocio.FrecuenciaAplicacion", "FrecuenciaAplicacion")
                        .WithMany("EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle")
                        .HasForeignKey("IdFrecuenciaAplicacion");

                    b.HasOne("bd.swth.entidades.Negocio.Relevancia", "Relevancia")
                        .WithMany("EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle")
                        .HasForeignKey("IdRelevancia");

                    b.HasOne("bd.swth.entidades.Negocio.TrabajoEquipoIniciativaLiderazgo", "TrabajoEquipoIniciativaLiderazgo")
                        .WithMany("EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle")
                        .HasForeignKey("IdTrabajoEquipoIniciativaLiderazgo");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.EvaluacionTrabajoEquipoIniciativaLiderazgoFactor", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.EvaluacionTrabajoEquipoIniciativaLiderazgo", "EvaluacionTrabajoEquipoIniciativaLiderazgo")
                        .WithMany("EvaluacionTrabajoEquipoIniciativaLiderazgoFactor")
                        .HasForeignKey("IdEvaluacionTrabajoEquipoIniciativaLiderazgo")
                        .HasConstraintName("FK_EvalTrabEqIniLidFac_EvaTrabEquiIniLid_IdEvaluacionTrabajoEquipoIniciativaLiderazgo");

                    b.HasOne("bd.swth.entidades.Negocio.Factor", "Factor")
                        .WithMany("EvaluacionTrabajoEquipoIniciativaLiderazgoFactor")
                        .HasForeignKey("IdFactor");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Evaluador", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Dependencia", "Dependencia")
                        .WithMany("Evaluador")
                        .HasForeignKey("IdDependencia");

                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("Evaluador")
                        .HasForeignKey("IdEmpleado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Exepciones", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.ValidacionInmediatoSuperior", "ValidacionInmediatoSuperior")
                        .WithMany("Exepciones")
                        .HasForeignKey("ValidacionJefeId");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ExperienciaLaboralRequerida", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.AnoExperiencia", "AnoExperiencia")
                        .WithMany("ExperienciaLaboralRequerida")
                        .HasForeignKey("IdAnoExperiencia");

                    b.HasOne("bd.swth.entidades.Negocio.EspecificidadExperiencia", "EspecificidadExperiencia")
                        .WithMany("ExperienciaLaboralRequerida")
                        .HasForeignKey("IdEspecificidadExperiencia");

                    b.HasOne("bd.swth.entidades.Negocio.Estudio", "Estudio")
                        .WithMany("ExperienciaLaboralRequerida")
                        .HasForeignKey("IdEstudio");

                    b.HasOne("bd.swth.entidades.Negocio.IndiceOcupacionalCapacitaciones", "IndiceOcupacionalCapacitaciones")
                        .WithMany("ExperienciaLaboralRequerida")
                        .HasForeignKey("IdIndiceOcupacionalCapacitaciones");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.FacturaViatico", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("FacturaViatico")
                        .HasForeignKey("AprobadoPor");

                    b.HasOne("bd.swth.entidades.Negocio.ItemViatico", "ItemViatico")
                        .WithMany("FacturaViatico")
                        .HasForeignKey("IdItemViatico");

                    b.HasOne("bd.swth.entidades.Negocio.ItinerarioViatico", "ItinerarioViatico")
                        .WithMany("FacturaViatico")
                        .HasForeignKey("ItinerarioViaticoId");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.FaseConcurso", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.TipoConcurso", "TipoConcurso")
                        .WithMany("FaseConcurso")
                        .HasForeignKey("IdTipoConcurso");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.FormularioAnalisisOcupacional", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("FormularioAnalisisOcupacional")
                        .HasForeignKey("IdEmpleado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.FormularioDevengacion", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "EmpleadoDesarrolloInstitucional")
                        .WithMany("FormularioDevengacion1")
                        .HasForeignKey("AnalistaDesarrolloInstitucional");

                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("FormularioDevengacion")
                        .HasForeignKey("IdEmpleado");

                    b.HasOne("bd.swth.entidades.Negocio.ModosScializacion", "ModosScializacion")
                        .WithMany("FormularioDevengacion")
                        .HasForeignKey("ModosScializacionId");

                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "EmpleadoResponsableArea")
                        .WithMany("FormularioDevengacion2")
                        .HasForeignKey("ResponsableArea");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.GastoRubro", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.EmpleadoImpuestoRenta", "EmpleadoImpuestoRenta")
                        .WithMany("GastoRubro")
                        .HasForeignKey("IdEmpleadoImpuestoRenta");

                    b.HasOne("bd.swth.entidades.Negocio.Rubro", "Rubro")
                        .WithMany("GastoRubro")
                        .HasForeignKey("IdRubro");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.IndiceOcupacional", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Dependencia", "Dependencia")
                        .WithMany("IndiceOcupacional")
                        .HasForeignKey("IdDependencia");

                    b.HasOne("bd.swth.entidades.Negocio.EscalaGrados", "EscalaGrados")
                        .WithMany("IndiceOcupacional")
                        .HasForeignKey("IdEscalaGrados");

                    b.HasOne("bd.swth.entidades.Negocio.ManualPuesto", "ManualPuesto")
                        .WithMany("IndiceOcupacional")
                        .HasForeignKey("IdManualPuesto");

                    b.HasOne("bd.swth.entidades.Negocio.RolPuesto", "RolPuesto")
                        .WithMany("IndiceOcupacional")
                        .HasForeignKey("IdRolPuesto");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.IndiceOcupacionalActividadesEsenciales", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.ActividadesEsenciales", "ActividadesEsenciales")
                        .WithMany("IndiceOcupacionalActividadesEsenciales")
                        .HasForeignKey("IdActividadesEsenciales");

                    b.HasOne("bd.swth.entidades.Negocio.IndiceOcupacional", "IndiceOcupacional")
                        .WithMany("IndiceOcupacionalActividadesEsenciales")
                        .HasForeignKey("IdIndiceOcupacional");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.IndiceOcupacionalAreaConocimiento", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.AreaConocimiento", "AreaConocimiento")
                        .WithMany("IndiceOcupacionalAreaConocimiento")
                        .HasForeignKey("IdAreaConocimiento");

                    b.HasOne("bd.swth.entidades.Negocio.IndiceOcupacional", "IndiceOcupacional")
                        .WithMany("IndiceOcupacionalAreaConocimiento")
                        .HasForeignKey("IdIndiceOcupacional");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.IndiceOcupacionalCapacitaciones", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Capacitacion", "Capacitacion")
                        .WithMany("IndiceOcupacionalCapacitaciones")
                        .HasForeignKey("IdCapacitacion");

                    b.HasOne("bd.swth.entidades.Negocio.IndiceOcupacional", "IndiceOcupacional")
                        .WithMany("IndiceOcupacionalCapacitaciones")
                        .HasForeignKey("IdIndiceOcupacional");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.IndiceOcupacionalComportamientoObservable", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.ComportamientoObservable", "ComportamientoObservable")
                        .WithMany("IndiceOcupacionalComportamientoObservable")
                        .HasForeignKey("IdComportamientoObservable");

                    b.HasOne("bd.swth.entidades.Negocio.IndiceOcupacional", "IndiceOcupacional")
                        .WithMany("IndiceOcupacionalComportamientoObservable")
                        .HasForeignKey("IdIndiceOcupacional");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.IndiceOcupacionalConocimientosAdicionales", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.ConocimientosAdicionales", "ConocimientosAdicionales")
                        .WithMany("IndiceOcupacionalConocimientosAdicionales")
                        .HasForeignKey("IdConocimientosAdicionales");

                    b.HasOne("bd.swth.entidades.Negocio.IndiceOcupacional", "IndiceOcupacional")
                        .WithMany("IndiceOcupacionalConocimientosAdicionales")
                        .HasForeignKey("IdIndiceOcupacional");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.IndiceOcupacionalEstudio", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Estudio", "Estudio")
                        .WithMany("IndiceOcupacionalEstudio")
                        .HasForeignKey("IdEstudio");

                    b.HasOne("bd.swth.entidades.Negocio.IndiceOcupacional", "IndiceOcupacional")
                        .WithMany("IndiceOcupacionalEstudio")
                        .HasForeignKey("IdIndiceOcupacional");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.IndiceOcupacionalModalidadPartida", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("IndiceOcupacionalModalidadPartida")
                        .HasForeignKey("IdEmpleado");

                    b.HasOne("bd.swth.entidades.Negocio.FondoFinanciamiento", "FondoFinanciamiento")
                        .WithMany("IndiceOcupacionalModalidadPartida")
                        .HasForeignKey("IdFondoFinanciamiento");

                    b.HasOne("bd.swth.entidades.Negocio.IndiceOcupacional", "IndiceOcupacional")
                        .WithMany("IndiceOcupacionalModalidadPartida")
                        .HasForeignKey("IdIndiceOcupacional");

                    b.HasOne("bd.swth.entidades.Negocio.ModalidadPartida", "ModalidadPartida")
                        .WithMany("IndiceOcupacionalModalidadPartida")
                        .HasForeignKey("IdModalidadPartida");

                    b.HasOne("bd.swth.entidades.Negocio.TipoNombramiento", "TipoNombramiento")
                        .WithMany("IndiceOcupacionalModalidadPartida")
                        .HasForeignKey("IdTipoNombramiento");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.InformeUATH", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.AdministracionTalentoHumano", "AdministracionTalentoHumano")
                        .WithMany("InformeUATH")
                        .HasForeignKey("IdAdministracionTalentoHumano");

                    b.HasOne("bd.swth.entidades.Negocio.ManualPuesto", "ManualPuestoDestino")
                        .WithMany("InformeUATH")
                        .HasForeignKey("IdManualPuestoDestino");

                    b.HasOne("bd.swth.entidades.Negocio.ManualPuesto", "ManualPuestoOrigen")
                        .WithMany("InformeUATH1")
                        .HasForeignKey("IdManualPuestoOrigen");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.InformeViatico", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.ItinerarioViatico", "ItinerarioViatico")
                        .WithMany("InformeViatico")
                        .HasForeignKey("IdItinerarioViatico");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.IngresoEgresoRMU", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.FormulasRMU", "FormulasRMU")
                        .WithMany("IngresoEgresoRMU")
                        .HasForeignKey("IdFormulaRMU");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ItinerarioViatico", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Ciudad", "Ciudad")
                        .WithMany("ItinerarioViatico")
                        .HasForeignKey("IdCiudad");

                    b.HasOne("bd.swth.entidades.Negocio.Pais", "Pais")
                        .WithMany("ItinerarioViatico")
                        .HasForeignKey("IdPais");

                    b.HasOne("bd.swth.entidades.Negocio.SolicitudViatico", "SolicitudViatico")
                        .WithMany("ItinerarioViatico")
                        .HasForeignKey("IdSolicitudViatico");

                    b.HasOne("bd.swth.entidades.Negocio.TipoTransporte", "TipoTransporte")
                        .WithMany("ItinerarioViatico")
                        .HasForeignKey("IdTipoTransporte");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Liquidacion", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("Liquidacion")
                        .HasForeignKey("IdEmpleado");

                    b.HasOne("bd.swth.entidades.Negocio.RubroLiquidacion", "RubroLiquidacion")
                        .WithMany("Liquidacion")
                        .HasForeignKey("IdRubroLiquidacion");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.MaterialApoyo", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.FormularioDevengacion", "FormularioDevengacion")
                        .WithMany("MaterialApoyo")
                        .HasForeignKey("FormularioDevengacionId");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.MisionIndiceOcupacional", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.IndiceOcupacional", "IndiceOcupacional")
                        .WithMany("MisionIndiceOcupacional")
                        .HasForeignKey("IdIndiceOcupacional");

                    b.HasOne("bd.swth.entidades.Negocio.Mision", "Mision")
                        .WithMany("MisionIndiceOcupacional")
                        .HasForeignKey("IdMision");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ModalidadPartida", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.RelacionLaboral", "RelacionLaboral")
                        .WithMany("ModalidadPartida")
                        .HasForeignKey("IdRelacionLaboral");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.NacionalidadIndigena", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Etnia", "Etnia")
                        .WithMany("NacionalidadIndigena")
                        .HasForeignKey("IdEtnia");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Parroquia", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Ciudad", "Ciudad")
                        .WithMany("Parroquia")
                        .HasForeignKey("IdCiudad");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.PartidasFase", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.FaseConcurso", "FaseConcurso")
                        .WithMany("PartidasFase")
                        .HasForeignKey("IdFaseConcurso");

                    b.HasOne("bd.swth.entidades.Negocio.IndiceOcupacionalModalidadPartida", "IndiceOcupacionalModalidadPartida")
                        .WithMany("PartidasFase")
                        .HasForeignKey("IdIndiceOcupacionalModalidadPartida");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Permiso", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.TipoPermiso", "TipoPermiso")
                        .WithMany("Permiso")
                        .HasForeignKey("IdTipoPermiso");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Persona", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Canditato", "Canditato")
                        .WithMany("Persona")
                        .HasForeignKey("IdCanditato");

                    b.HasOne("bd.swth.entidades.Negocio.EstadoCivil", "EstadoCivil")
                        .WithMany("Persona")
                        .HasForeignKey("IdEstadoCivil");

                    b.HasOne("bd.swth.entidades.Negocio.Etnia", "Etnia")
                        .WithMany("Persona")
                        .HasForeignKey("IdEtnia");

                    b.HasOne("bd.swth.entidades.Negocio.Genero", "Genero")
                        .WithMany("Persona")
                        .HasForeignKey("IdGenero");

                    b.HasOne("bd.swth.entidades.Negocio.Nacionalidad", "Nacionalidad")
                        .WithMany("Persona")
                        .HasForeignKey("IdNacionalidad");

                    b.HasOne("bd.swth.entidades.Negocio.Sexo", "Sexo")
                        .WithMany("Persona")
                        .HasForeignKey("IdSexo");

                    b.HasOne("bd.swth.entidades.Negocio.TipoIdentificacion", "TipoIdentificacion")
                        .WithMany("Persona")
                        .HasForeignKey("IdTipoIdentificacion");

                    b.HasOne("bd.swth.entidades.Negocio.TipoSangre", "TipoSangre")
                        .WithMany("Persona")
                        .HasForeignKey("IdTipoSangre");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.PersonaCapacitacion", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Capacitacion", "Capacitacion")
                        .WithMany("PersonaCapacitacion")
                        .HasForeignKey("IdCapacitacion");

                    b.HasOne("bd.swth.entidades.Negocio.Persona", "Persona")
                        .WithMany("PersonaCapacitacion")
                        .HasForeignKey("IdPersona");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.PersonaDiscapacidad", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Persona", "Persona")
                        .WithMany("PersonaDiscapacidad")
                        .HasForeignKey("IdPersona");

                    b.HasOne("bd.swth.entidades.Negocio.TipoDiscapacidad", "TipoDiscapacidad")
                        .WithMany("PersonaDiscapacidad")
                        .HasForeignKey("IdTipoDiscapacidad");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.PersonaEnfermedad", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Persona", "Persona")
                        .WithMany("PersonaEnfermedad")
                        .HasForeignKey("IdPersona");

                    b.HasOne("bd.swth.entidades.Negocio.TipoEnfermedad", "TipoEnfermedad")
                        .WithMany("PersonaEnfermedad")
                        .HasForeignKey("IdTipoEnfermedad");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.PersonaEstudio", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Persona", "Persona")
                        .WithMany("PersonaEstudio")
                        .HasForeignKey("IdPersona");

                    b.HasOne("bd.swth.entidades.Negocio.Titulo", "Titulo")
                        .WithMany("PersonaEstudio")
                        .HasForeignKey("IdTitulo");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.PersonaPaquetesInformaticos", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("PersonaPaquetesInformaticos")
                        .HasForeignKey("IdEmpleado");

                    b.HasOne("bd.swth.entidades.Negocio.PaquetesInformaticos", "PaquetesInformaticos")
                        .WithMany("PersonaPaquetesInformaticos")
                        .HasForeignKey("IdPaquetesInformaticos");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.PlanGestionCambio", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "EmpleadoAprobadoPor")
                        .WithMany("PlanGestionCambio")
                        .HasForeignKey("AprobadoPor");

                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "EmpleadoRealizadoPor")
                        .WithMany("PlanGestionCambio1")
                        .HasForeignKey("RealizadoPor");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.PlanificacionHE", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("PlanificacionHE")
                        .HasForeignKey("IdEmpleado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Pregunta", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.EvaluacionInducion", "EvaluacionInducion")
                        .WithMany("Pregunta")
                        .HasForeignKey("IdEvaluacionInduccion");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.PreguntaRespuesta", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Pregunta", "Pregunta")
                        .WithMany("PreguntaRespuesta")
                        .HasForeignKey("IdPregunta");

                    b.HasOne("bd.swth.entidades.Negocio.Respuesta", "Respuesta")
                        .WithMany("PreguntaRespuesta")
                        .HasForeignKey("IdRespuesta");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ProcesoDetalle", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Dependencia", "Dependencia")
                        .WithMany("ProcesoDetalle")
                        .HasForeignKey("IdDependencia");

                    b.HasOne("bd.swth.entidades.Negocio.Proceso", "Proceso")
                        .WithMany("ProcesoDetalle")
                        .HasForeignKey("IdProceso");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Provincia", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Pais", "Pais")
                        .WithMany("Provincia")
                        .HasForeignKey("IdPais");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Provisiones", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("Provisiones")
                        .HasForeignKey("IdEmpleado");

                    b.HasOne("bd.swth.entidades.Negocio.TipoProvision", "TipoProvision")
                        .WithMany("Provisiones")
                        .HasForeignKey("IdTipoProvision");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.RealizaExamenInduccion", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("RealizaExamenInduccion")
                        .HasForeignKey("IdEmpleado");

                    b.HasOne("bd.swth.entidades.Negocio.EvaluacionInducion", "EvaluacionInducion")
                        .WithMany("RealizaExamenInduccion")
                        .HasForeignKey("IdEvaluacionInduccion");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.RegistroEntradaSalida", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("RegistroEntradaSalida")
                        .HasForeignKey("IdEmpleado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.RelacionesInternasExternasIndiceOcupacional", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.IndiceOcupacional", "IndiceOcupacional")
                        .WithMany("RelacionesInternasExternasIndiceOcupacional")
                        .HasForeignKey("IdIndiceOcupacional");

                    b.HasOne("bd.swth.entidades.Negocio.RelacionesInternasExternas", "RelacionesInternasExternas")
                        .WithMany("RelacionesInternasExternasIndiceOcupacional")
                        .HasForeignKey("RelacionesInternasExternasId");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.RelacionLaboral", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.RegimenLaboral", "RegimenLaboral")
                        .WithMany("RelacionLaboral")
                        .HasForeignKey("IdRegimenLaboral");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.RequisitosNoCumple", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.AdministracionTalentoHumano", "AdministracionTalentoHumano")
                        .WithMany("RequisitosNoCumple")
                        .HasForeignKey("AdministracionTalentoHumanoId");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.RMU", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.EmpleadoIE", "EmpleadoIE")
                        .WithMany("RMU")
                        .HasForeignKey("IdEmpeladoIE");

                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("RMU")
                        .HasForeignKey("IdEmpleado");

                    b.HasOne("bd.swth.entidades.Negocio.TipoRMU", "TipoRMU")
                        .WithMany("RMU")
                        .HasForeignKey("IdTipoRMU");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.RolPagoDetalle", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.RolPagos", "RolPagos")
                        .WithMany("RolPagoDetalle")
                        .HasForeignKey("IdRolPagos");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.RolPagos", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("RolPagos")
                        .HasForeignKey("IdEmpleado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.SituacionPropuesta", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Dependencia", "Dependencia")
                        .WithMany("SituacionPropuesta")
                        .HasForeignKey("IdDependencia");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.SolicitudAcumulacionDecimos", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("SolicitudAcumulacionDecimos")
                        .HasForeignKey("IdEmpleado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.SolicitudAnticipo", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("SolicitudAnticipo")
                        .HasForeignKey("IdEmpleado");

                    b.HasOne("bd.swth.entidades.Negocio.Estado", "Estado")
                        .WithMany("SolicitudAnticipo")
                        .HasForeignKey("IdEstado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.SolicitudCertificadoPersonal", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "EmpleadoDirigidoA")
                        .WithMany("SolicitudCertificadoPersonal1")
                        .HasForeignKey("IdEmpleadoDirigidoA");

                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "EmpleadoSolicitante")
                        .WithMany("SolicitudCertificadoPersonal")
                        .HasForeignKey("IdEmpleadoSolicitante");

                    b.HasOne("bd.swth.entidades.Negocio.Estado", "Estado")
                        .WithMany("SolicitudCertificadoPersonal")
                        .HasForeignKey("IdEstado");

                    b.HasOne("bd.swth.entidades.Negocio.TipoCertificado", "TipoCertificado")
                        .WithMany("SolicitudCertificadoPersonal")
                        .HasForeignKey("IdTipoCertificado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.SolicitudHorasExtras", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("SolicitudHorasExtras")
                        .HasForeignKey("IdEmpleado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.SolicitudLiquidacionHaberes", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("SolicitudLiquidacionHaberes")
                        .HasForeignKey("IdEmpleado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.SolicitudModificacionFichaEmpleado", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("SolicitudModificacionFichaEmpleado")
                        .HasForeignKey("IdEmpleado");

                    b.HasOne("bd.swth.entidades.Negocio.Estado", "Estado")
                        .WithMany("SolicitudModificacionFichaEmpleado")
                        .HasForeignKey("IdEstado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.SolicitudPermiso", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("SolicitudPermiso")
                        .HasForeignKey("IdEmpleado");

                    b.HasOne("bd.swth.entidades.Negocio.Estado", "Estado")
                        .WithMany("SolicitudPermiso")
                        .HasForeignKey("IdEstado");

                    b.HasOne("bd.swth.entidades.Negocio.Permiso", "Permiso")
                        .WithMany("SolicitudPermiso")
                        .HasForeignKey("IdPermiso");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.SolicitudPlanificacionVacaciones", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("SolicitudPlanificacionVacaciones")
                        .HasForeignKey("IdEmpleado");

                    b.HasOne("bd.swth.entidades.Negocio.Estado", "Estado")
                        .WithMany("SolicitudPlanificacionVacaciones")
                        .HasForeignKey("IdEstado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.SolicitudVacaciones", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("SolicitudVacaciones")
                        .HasForeignKey("IdEmpleado");

                    b.HasOne("bd.swth.entidades.Negocio.Estado", "Estado")
                        .WithMany("SolicitudVacaciones")
                        .HasForeignKey("IdEstado");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.SolicitudViatico", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.ConfiguracionViatico", "ConfiguracionViatico")
                        .WithMany("SolicitudViatico")
                        .HasForeignKey("IdConfiguracionViatico");

                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("SolicitudViatico")
                        .HasForeignKey("IdEmpleado");

                    b.HasOne("bd.swth.entidades.Negocio.Estado", "Estado")
                        .WithMany("SolicitudViatico")
                        .HasForeignKey("IdEstado");

                    b.HasOne("bd.swth.entidades.Negocio.TipoViatico", "TipoViatico")
                        .WithMany("SolicitudViatico")
                        .HasForeignKey("IdTipoViatico");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Sucursal", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Ciudad", "Ciudad")
                        .WithMany("Sucursal")
                        .HasForeignKey("IdCiudad");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.TipoNombramiento", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.RelacionLaboral", "RelacionLaboral")
                        .WithMany("TipoNombramiento")
                        .HasForeignKey("IdRelacionLaboral");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.Titulo", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Estudio", "Estudio")
                        .WithMany("Titulo")
                        .HasForeignKey("IdEstudio");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.TrayectoriaLaboral", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Persona", "Persona")
                        .WithMany("TrayectoriaLaboral")
                        .HasForeignKey("IdPersona");
                });

            modelBuilder.Entity("bd.swth.entidades.Negocio.ValidacionInmediatoSuperior", b =>
                {
                    b.HasOne("bd.swth.entidades.Negocio.Empleado", "Empleado")
                        .WithMany("ValidacionInmediatoSuperior")
                        .HasForeignKey("IdEmpleado");

                    b.HasOne("bd.swth.entidades.Negocio.FormularioAnalisisOcupacional", "FormularioAnalisisOcupacional")
                        .WithMany("ValidacionInmediatoSuperior")
                        .HasForeignKey("IdFormularioAnalisisOcupacional");
                });
        }
    }
}
