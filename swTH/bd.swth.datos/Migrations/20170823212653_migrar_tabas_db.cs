using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace bd.swth.datos.Migrations
{
    public partial class migrar_tabas_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActividadesEsenciales",
                columns: table => new
                {
                    ActividadesEsencialesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActividadesEsenciales", x => x.ActividadesEsencialesId);
                });

            migrationBuilder.CreateTable(
                name: "AnoExperiencia",
                columns: table => new
                {
                    IdAnoExperiencia = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnoExperiencia", x => x.IdAnoExperiencia);
                });

            migrationBuilder.CreateTable(
                name: "AreaConocimiento",
                columns: table => new
                {
                    IdAreaConocimiento = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaConocimiento", x => x.IdAreaConocimiento);
                });

            migrationBuilder.CreateTable(
                name: "BrigadaSSO",
                columns: table => new
                {
                    IdBrigadaSSO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrigadaSSO", x => x.IdBrigadaSSO);
                });

            migrationBuilder.CreateTable(
                name: "Canditato",
                columns: table => new
                {
                    IdCanditato = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canditato", x => x.IdCanditato);
                });

            migrationBuilder.CreateTable(
                name: "Capacitacion",
                columns: table => new
                {
                    IdCapacitacion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capacitacion", x => x.IdCapacitacion);
                });

            migrationBuilder.CreateTable(
                name: "CapacitacionAreaConocimiento",
                columns: table => new
                {
                    IdCapacitacionAreaConocimiento = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapacitacionAreaConocimiento", x => x.IdCapacitacionAreaConocimiento);
                });

            migrationBuilder.CreateTable(
                name: "CapacitacionModalidad",
                columns: table => new
                {
                    IdCapacitacionModalidad = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapacitacionModalidad", x => x.IdCapacitacionModalidad);
                });

            migrationBuilder.CreateTable(
                name: "CapacitacionTipoPregunta",
                columns: table => new
                {
                    IdCapacitacionTipoPregunta = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapacitacionTipoPregunta", x => x.IdCapacitacionTipoPregunta);
                });

            migrationBuilder.CreateTable(
                name: "ConocimientosAdicionales",
                columns: table => new
                {
                    IdConocimientosAdicionales = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConocimientosAdicionales", x => x.IdConocimientosAdicionales);
                });

            migrationBuilder.CreateTable(
                name: "DenominacionCompetencia",
                columns: table => new
                {
                    IdDenominacionCompetencia = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompetenciaTecnica = table.Column<bool>(nullable: false),
                    Definicion = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DenominacionCompetencia", x => x.IdDenominacionCompetencia);
                });

            migrationBuilder.CreateTable(
                name: "Destreza",
                columns: table => new
                {
                    IdDestreza = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destreza", x => x.IdDestreza);
                });

            migrationBuilder.CreateTable(
                name: "EscalaEvaluacionTotal",
                columns: table => new
                {
                    IdEscalaEvaluacionTotal = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false),
                    PorcientoDesde = table.Column<decimal>(type: "decimal", nullable: false),
                    PorcientoHasta = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscalaEvaluacionTotal", x => x.IdEscalaEvaluacionTotal);
                });

            migrationBuilder.CreateTable(
                name: "EspecificidadExperiencia",
                columns: table => new
                {
                    IdEspecificidadExperiencia = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspecificidadExperiencia", x => x.IdEspecificidadExperiencia);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    IdEstado = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.IdEstado);
                });

            migrationBuilder.CreateTable(
                name: "EstadoCivil",
                columns: table => new
                {
                    IdEstadoCivil = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoCivil", x => x.IdEstadoCivil);
                });

            migrationBuilder.CreateTable(
                name: "Estudio",
                columns: table => new
                {
                    IdEstudio = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudio", x => x.IdEstudio);
                });

            migrationBuilder.CreateTable(
                name: "Etnia",
                columns: table => new
                {
                    IdEtnia = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etnia", x => x.IdEtnia);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionActividadesPuestoTrabajo",
                columns: table => new
                {
                    IdEvaluacionActividadesPuestoTrabajo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionActividadesPuestoTrabajo", x => x.IdEvaluacionActividadesPuestoTrabajo);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionCompetenciasTecnicasPuesto",
                columns: table => new
                {
                    IdEvaluacionCompetenciasTecnicasPuesto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionCompetenciasTecnicasPuesto", x => x.IdEvaluacionCompetenciasTecnicasPuesto);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionCompetenciasUniversales",
                columns: table => new
                {
                    IdEvaluacionCompetenciasUniversales = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionCompetenciasUniversales", x => x.IdEvaluacionCompetenciasUniversales);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionConocimiento",
                columns: table => new
                {
                    IdEvaluacionConocimiento = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionConocimiento", x => x.IdEvaluacionConocimiento);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionInducion",
                columns: table => new
                {
                    IdEvaluacionInduccion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaximoPuntos = table.Column<int>(nullable: false),
                    MinimoAprobar = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionInducion", x => x.IdEvaluacionInduccion);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionTrabajoEquipoIniciativaLiderazgo",
                columns: table => new
                {
                    IdEvaluacionTrabajoEquipoIniciativaLiderazgo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false),
                    ObservacionesJefeInmediato = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionTrabajoEquipoIniciativaLiderazgo", x => x.IdEvaluacionTrabajoEquipoIniciativaLiderazgo);
                });

            migrationBuilder.CreateTable(
                name: "Factor",
                columns: table => new
                {
                    IdFactor = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Porciento = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factor", x => x.IdFactor);
                });

            migrationBuilder.CreateTable(
                name: "FondoFinanciamiento",
                columns: table => new
                {
                    IdFondoFinanciamiento = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FondoFinanciamiento", x => x.IdFondoFinanciamiento);
                });

            migrationBuilder.CreateTable(
                name: "FormularioCapacitacion",
                columns: table => new
                {
                    IdFormularioCapacitacion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormularioCapacitacion", x => x.IdFormularioCapacitacion);
                });

            migrationBuilder.CreateTable(
                name: "FormulasRMU",
                columns: table => new
                {
                    IdFormulaRMU = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Formula = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormulasRMU", x => x.IdFormulaRMU);
                });

            migrationBuilder.CreateTable(
                name: "FrecuenciaAplicacion",
                columns: table => new
                {
                    IdFrecuenciaAplicacion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrecuenciaAplicacion", x => x.IdFrecuenciaAplicacion);
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    IdGenero = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.IdGenero);
                });

            migrationBuilder.CreateTable(
                name: "GrupoOcupacional",
                columns: table => new
                {
                    IdGrupoOcupacional = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoOcupacional", x => x.IdGrupoOcupacional);
                });

            migrationBuilder.CreateTable(
                name: "Indicador",
                columns: table => new
                {
                    IdIndicador = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indicador", x => x.IdIndicador);
                });

            migrationBuilder.CreateTable(
                name: "InstitucionFinanciera",
                columns: table => new
                {
                    IdInstitucionFinanciera = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 200, nullable: false),
                    SPI = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstitucionFinanciera", x => x.IdInstitucionFinanciera);
                });

            migrationBuilder.CreateTable(
                name: "ItemViatico",
                columns: table => new
                {
                    IdItemViatico = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descipcion = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemViatico", x => x.IdItemViatico);
                });

            migrationBuilder.CreateTable(
                name: "ManualPuesto",
                columns: table => new
                {
                    IdManualPuesto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManualPuesto", x => x.IdManualPuesto);
                });

            migrationBuilder.CreateTable(
                name: "Mision",
                columns: table => new
                {
                    IdMision = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mision", x => x.IdMision);
                });

            migrationBuilder.CreateTable(
                name: "ModosScializacion",
                columns: table => new
                {
                    IdModosScializacion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModosScializacion", x => x.IdModosScializacion);
                });

            migrationBuilder.CreateTable(
                name: "Nacionalidad",
                columns: table => new
                {
                    IdNacionalidad = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nacionalidad", x => x.IdNacionalidad);
                });

            migrationBuilder.CreateTable(
                name: "Nivel",
                columns: table => new
                {
                    IdNivel = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nivel", x => x.IdNivel);
                });

            migrationBuilder.CreateTable(
                name: "NivelConocimiento",
                columns: table => new
                {
                    IdNivelConocimiento = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NivelConocimiento", x => x.IdNivelConocimiento);
                });

            migrationBuilder.CreateTable(
                name: "NivelDesarrollo",
                columns: table => new
                {
                    IdNivelDesarrollo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NivelDesarrollo", x => x.IdNivelDesarrollo);
                });

            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    IdPais = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.IdPais);
                });

            migrationBuilder.CreateTable(
                name: "PaquetesInformaticos",
                columns: table => new
                {
                    IdPaquetesInformaticos = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 500, nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaquetesInformaticos", x => x.IdPaquetesInformaticos);
                });

            migrationBuilder.CreateTable(
                name: "Parentesco",
                columns: table => new
                {
                    IdParentesco = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parentesco", x => x.IdParentesco);
                });

            migrationBuilder.CreateTable(
                name: "Proceso",
                columns: table => new
                {
                    IdProceso = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proceso", x => x.IdProceso);
                });

            migrationBuilder.CreateTable(
                name: "RegimenLaboral",
                columns: table => new
                {
                    IdRegimenLaboral = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegimenLaboral", x => x.IdRegimenLaboral);
                });

            migrationBuilder.CreateTable(
                name: "RelacionesInternasExternas",
                columns: table => new
                {
                    IdRelacionesInternasExternas = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelacionesInternasExternas", x => x.IdRelacionesInternasExternas);
                });

            migrationBuilder.CreateTable(
                name: "Relevancia",
                columns: table => new
                {
                    IdRelevancia = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ComportamientoObservable = table.Column<string>(maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relevancia", x => x.IdRelevancia);
                });

            migrationBuilder.CreateTable(
                name: "Respuesta",
                columns: table => new
                {
                    IdRespuesta = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuesta", x => x.IdRespuesta);
                });

            migrationBuilder.CreateTable(
                name: "RolPuesto",
                columns: table => new
                {
                    IdRolPuesto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolPuesto", x => x.IdRolPuesto);
                });

            migrationBuilder.CreateTable(
                name: "Rubro",
                columns: table => new
                {
                    IdRubro = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false),
                    TasaPorcentualMaxima = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rubro", x => x.IdRubro);
                });

            migrationBuilder.CreateTable(
                name: "RubroLiquidacion",
                columns: table => new
                {
                    IdRubroLiquidacion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RubroLiquidacion", x => x.IdRubroLiquidacion);
                });

            migrationBuilder.CreateTable(
                name: "Sexo",
                columns: table => new
                {
                    IdSexo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sexo", x => x.IdSexo);
                });

            migrationBuilder.CreateTable(
                name: "TipoAccionPersonal",
                columns: table => new
                {
                    IdTipoAccionPersonal = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAccionPersonal", x => x.IdTipoAccionPersonal);
                });

            migrationBuilder.CreateTable(
                name: "TipoCertificado",
                columns: table => new
                {
                    IdTipoCertificado = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 500, nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCertificado", x => x.IdTipoCertificado);
                });

            migrationBuilder.CreateTable(
                name: "TipoConcurso",
                columns: table => new
                {
                    IdTipoConcurso = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 500, nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoConcurso", x => x.IdTipoConcurso);
                });

            migrationBuilder.CreateTable(
                name: "TipoDiscapacidad",
                columns: table => new
                {
                    IdTipoDiscapacidad = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDiscapacidad", x => x.IdTipoDiscapacidad);
                });

            migrationBuilder.CreateTable(
                name: "TipoEnfermedad",
                columns: table => new
                {
                    IdTipoEnfermedad = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEnfermedad", x => x.IdTipoEnfermedad);
                });

            migrationBuilder.CreateTable(
                name: "TipoIdentificacion",
                columns: table => new
                {
                    IdTipoIdentificacion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoIdentificacion", x => x.IdTipoIdentificacion);
                });

            migrationBuilder.CreateTable(
                name: "TipoMovimientoInterno",
                columns: table => new
                {
                    IdTipoMovimientoInterno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMovimientoInterno", x => x.IdTipoMovimientoInterno);
                });

            migrationBuilder.CreateTable(
                name: "TipoPermiso",
                columns: table => new
                {
                    IdTipoPermiso = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPermiso", x => x.IdTipoPermiso);
                });

            migrationBuilder.CreateTable(
                name: "TipoProvision",
                columns: table => new
                {
                    IdTipoProvision = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoProvision", x => x.IdTipoProvision);
                });

            migrationBuilder.CreateTable(
                name: "TipoRMU",
                columns: table => new
                {
                    IdTipoRMU = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoRMU", x => x.IdTipoRMU);
                });

            migrationBuilder.CreateTable(
                name: "TipoSangre",
                columns: table => new
                {
                    IdTipoSangre = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoSangre", x => x.IdTipoSangre);
                });

            migrationBuilder.CreateTable(
                name: "TipoTransporte",
                columns: table => new
                {
                    IdTipoTransporte = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoTransporte", x => x.IdTipoTransporte);
                });

            migrationBuilder.CreateTable(
                name: "TipoViatico",
                columns: table => new
                {
                    IdTipoViatico = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoViatico", x => x.IdTipoViatico);
                });

            migrationBuilder.CreateTable(
                name: "TrabajoEquipoIniciativaLiderazgo",
                columns: table => new
                {
                    IdTrabajoEquipoIniciativaLiderazgo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrabajoEquipoIniciativaLiderazgo", x => x.IdTrabajoEquipoIniciativaLiderazgo);
                });

            migrationBuilder.CreateTable(
                name: "BrigadaSSORol",
                columns: table => new
                {
                    IdBrigadaSSORol = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdBrigadaSSO = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrigadaSSORol", x => x.IdBrigadaSSORol);
                    table.ForeignKey(
                        name: "FK_BrigadaSSORol_BrigadaSSO_IdBrigadaSSO",
                        column: x => x.IdBrigadaSSO,
                        principalTable: "BrigadaSSO",
                        principalColumn: "IdBrigadaSSO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CapacitacionTemario",
                columns: table => new
                {
                    IdCapacitacionTemario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCapacitacionAreaConocimiento = table.Column<int>(nullable: false),
                    Tema = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapacitacionTemario", x => x.IdCapacitacionTemario);
                    table.ForeignKey(
                        name: "FK_CapacitacionTemario_CapacitacionAreaConocimiento_IdCapacitacionAreaConocimiento",
                        column: x => x.IdCapacitacionAreaConocimiento,
                        principalTable: "CapacitacionAreaConocimiento",
                        principalColumn: "IdCapacitacionAreaConocimiento",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Titulo",
                columns: table => new
                {
                    IdTitulo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEstudio = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titulo", x => x.IdTitulo);
                    table.ForeignKey(
                        name: "FK_Titulo_Estudio_IdEstudio",
                        column: x => x.IdEstudio,
                        principalTable: "Estudio",
                        principalColumn: "IdEstudio",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NacionalidadIndigena",
                columns: table => new
                {
                    IdNacionalidadIndigena = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEtnia = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NacionalidadIndigena", x => x.IdNacionalidadIndigena);
                    table.ForeignKey(
                        name: "FK_NacionalidadIndigena_Etnia_IdEtnia",
                        column: x => x.IdEtnia,
                        principalTable: "Etnia",
                        principalColumn: "IdEtnia",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pregunta",
                columns: table => new
                {
                    IdPregunta = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEvaluacionInduccion = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pregunta", x => x.IdPregunta);
                    table.ForeignKey(
                        name: "FK_Pregunta_EvaluacionInducion_IdEvaluacionInduccion",
                        column: x => x.IdEvaluacionInduccion,
                        principalTable: "EvaluacionInducion",
                        principalColumn: "IdEvaluacionInduccion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionActividadesPuestoTrabajoFactor",
                columns: table => new
                {
                    IdEvaluacionActividadesPuestoTrabajoFactor = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEvaluacionActividadesPuestoTrabajo = table.Column<int>(nullable: true),
                    IdFactor = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionActividadesPuestoTrabajoFactor", x => x.IdEvaluacionActividadesPuestoTrabajoFactor);
                    table.ForeignKey(
                        name: "FK_EvaluacionActividadesPuestoTrabajoFactor_EvaluacionActividadesPuestoTrabajo_IdEvaluacionActividadesPuestoTrabajo",
                        column: x => x.IdEvaluacionActividadesPuestoTrabajo,
                        principalTable: "EvaluacionActividadesPuestoTrabajo",
                        principalColumn: "IdEvaluacionActividadesPuestoTrabajo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluacionActividadesPuestoTrabajoFactor_Factor_IdFactor",
                        column: x => x.IdFactor,
                        principalTable: "Factor",
                        principalColumn: "IdFactor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionCompetenciasTecnicasPuestoFactor",
                columns: table => new
                {
                    IdEvaluacionCompetenciasTecnicasPuestoFactor = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEvaluacionCompetenciasTecnicasPuesto = table.Column<int>(nullable: true),
                    IdFactor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionCompetenciasTecnicasPuestoFactor", x => x.IdEvaluacionCompetenciasTecnicasPuestoFactor);
                    table.ForeignKey(
                        name: "FK_EvaluacionCompetenciasTecnicasPuestoFactor_EvaluacionCompetenciasTecnicasPuesto_IdEvaluacionCompetenciasTecnicasPuesto",
                        column: x => x.IdEvaluacionCompetenciasTecnicasPuesto,
                        principalTable: "EvaluacionCompetenciasTecnicasPuesto",
                        principalColumn: "IdEvaluacionCompetenciasTecnicasPuesto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluacionCompetenciasTecnicasPuestoFactor_Factor_IdFactor",
                        column: x => x.IdFactor,
                        principalTable: "Factor",
                        principalColumn: "IdFactor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionCompetenciasUniversalesFactor",
                columns: table => new
                {
                    IdEvaluacionCompetenciasUniversalesFactor = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEvaluacionCompetenciasUniversales = table.Column<int>(nullable: true),
                    IdFactor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionCompetenciasUniversalesFactor", x => x.IdEvaluacionCompetenciasUniversalesFactor);
                    table.ForeignKey(
                        name: "FK_EvaluacionCompetenciasUniversalesFactor_EvaluacionCompetenciasUniversales_IdEvaCompUnives",
                        column: x => x.IdEvaluacionCompetenciasUniversales,
                        principalTable: "EvaluacionCompetenciasUniversales",
                        principalColumn: "IdEvaluacionCompetenciasUniversales",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluacionCompetenciasUniversalesFactor_Factor_IdFactor",
                        column: x => x.IdFactor,
                        principalTable: "Factor",
                        principalColumn: "IdFactor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionConocimientoFactor",
                columns: table => new
                {
                    IdEvaluacionConocimientoFactor = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEvaluacionConocimiento = table.Column<int>(nullable: true),
                    IdFactor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionConocimientoFactor", x => x.IdEvaluacionConocimientoFactor);
                    table.ForeignKey(
                        name: "FK_EvaluacionConocimientoFactor_EvaluacionConocimiento_IdEvaluacionConocimiento",
                        column: x => x.IdEvaluacionConocimiento,
                        principalTable: "EvaluacionConocimiento",
                        principalColumn: "IdEvaluacionConocimiento",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluacionConocimientoFactor_Factor_IdFactor",
                        column: x => x.IdFactor,
                        principalTable: "Factor",
                        principalColumn: "IdFactor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionTrabajoEquipoIniciativaLiderazgoFactor",
                columns: table => new
                {
                    IdEvalTjoEquiInicLidFac = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEvaluacionTrabajoEquipoIniciativaLiderazgo = table.Column<int>(nullable: false),
                    IdFactor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionTrabajoEquipoIniciativaLiderazgoFactor", x => x.IdEvalTjoEquiInicLidFac);
                    table.ForeignKey(
                        name: "FK_EvalTrabEqIniLidFac_EvaTrabEquiIniLid_IdEvaluacionTrabajoEquipoIniciativaLiderazgo",
                        column: x => x.IdEvaluacionTrabajoEquipoIniciativaLiderazgo,
                        principalTable: "EvaluacionTrabajoEquipoIniciativaLiderazgo",
                        principalColumn: "IdEvaluacionTrabajoEquipoIniciativaLiderazgo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluacionTrabajoEquipoIniciativaLiderazgoFactor_Factor_IdFactor",
                        column: x => x.IdFactor,
                        principalTable: "Factor",
                        principalColumn: "IdFactor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IngresoEgresoRMU",
                columns: table => new
                {
                    IdIngresoEgresoRMU = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CuentaContable = table.Column<string>(maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 50, nullable: false),
                    IdFormulaRMU = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngresoEgresoRMU", x => x.IdIngresoEgresoRMU);
                    table.ForeignKey(
                        name: "FK_IngresoEgresoRMU_FormulasRMU_IdFormulaRMU",
                        column: x => x.IdFormulaRMU,
                        principalTable: "FormulasRMU",
                        principalColumn: "IdFormulaRMU",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EscalaGrados",
                columns: table => new
                {
                    IdEscalaGrados = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Grado = table.Column<int>(nullable: false),
                    IdGrupoOcupacional = table.Column<int>(nullable: false),
                    Remuneracion = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscalaGrados", x => x.IdEscalaGrados);
                    table.ForeignKey(
                        name: "FK_EscalaGrados_GrupoOcupacional_IdGrupoOcupacional",
                        column: x => x.IdGrupoOcupacional,
                        principalTable: "GrupoOcupacional",
                        principalColumn: "IdGrupoOcupacional",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionActividadesPuestoTrabajoDetalle",
                columns: table => new
                {
                    IdEvaluacionActividadesPuestoTrabajoDetalle = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActividadesCumplidas = table.Column<int>(nullable: false),
                    DescripcionActividad = table.Column<string>(maxLength: 50, nullable: false),
                    IdEvaluacionActividadesPuestoTrabajo = table.Column<int>(nullable: false),
                    IdIndicador = table.Column<int>(nullable: true),
                    MetaPeriodo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionActividadesPuestoTrabajoDetalle", x => x.IdEvaluacionActividadesPuestoTrabajoDetalle);
                    table.ForeignKey(
                        name: "FK_EvaluacionActividadesPuestoTrabajoDetalle_EvaluacionActividadesPuestoTrabajo_IdEvaluacionActividadesPuestoTrabajo",
                        column: x => x.IdEvaluacionActividadesPuestoTrabajo,
                        principalTable: "EvaluacionActividadesPuestoTrabajo",
                        principalColumn: "IdEvaluacionActividadesPuestoTrabajo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluacionActividadesPuestoTrabajoDetalle_Indicador_IdIndicador",
                        column: x => x.IdIndicador,
                        principalTable: "Indicador",
                        principalColumn: "IdIndicador",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComportamientoObservable",
                columns: table => new
                {
                    ComportamientoObservableId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DenominacionCompetenciaId = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(maxLength: 50, nullable: false),
                    NivelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComportamientoObservable", x => x.ComportamientoObservableId);
                    table.ForeignKey(
                        name: "FK_ComportamientoObservable_DenominacionCompetencia_DenominacionCompetenciaId",
                        column: x => x.DenominacionCompetenciaId,
                        principalTable: "DenominacionCompetencia",
                        principalColumn: "IdDenominacionCompetencia",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComportamientoObservable_Nivel_NivelId",
                        column: x => x.NivelId,
                        principalTable: "Nivel",
                        principalColumn: "IdNivel",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionConocimientoDetalle",
                columns: table => new
                {
                    IdEvaluacionConocimientoDetalle = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEvaluacionConocimiento = table.Column<int>(nullable: false),
                    IdNivelConocimiento = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionConocimientoDetalle", x => x.IdEvaluacionConocimientoDetalle);
                    table.ForeignKey(
                        name: "FK_EvaluacionConocimientoDetalle_EvaluacionConocimiento_IdEvaluacionConocimiento",
                        column: x => x.IdEvaluacionConocimiento,
                        principalTable: "EvaluacionConocimiento",
                        principalColumn: "IdEvaluacionConocimiento",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluacionConocimientoDetalle_NivelConocimiento_IdNivelConocimiento",
                        column: x => x.IdNivelConocimiento,
                        principalTable: "NivelConocimiento",
                        principalColumn: "IdNivelConocimiento",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Provincia",
                columns: table => new
                {
                    IdProvincia = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdPais = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincia", x => x.IdProvincia);
                    table.ForeignKey(
                        name: "FK_Provincia_Pais_IdPais",
                        column: x => x.IdPais,
                        principalTable: "Pais",
                        principalColumn: "IdPais",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RelacionLaboral",
                columns: table => new
                {
                    IdRelacionLaboral = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdRegimenLaboral = table.Column<int>(nullable: true),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelacionLaboral", x => x.IdRelacionLaboral);
                    table.ForeignKey(
                        name: "FK_RelacionLaboral_RegimenLaboral_IdRegimenLaboral",
                        column: x => x.IdRegimenLaboral,
                        principalTable: "RegimenLaboral",
                        principalColumn: "IdRegimenLaboral",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionCompetenciasTecnicasPuestoDetalle",
                columns: table => new
                {
                    IdEvaluacionCompetenciasTecnicasPuestoDetalle = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdDestreza = table.Column<int>(nullable: true),
                    IdEvaluacionCompetenciasTecnicasPuesto = table.Column<int>(nullable: false),
                    IdNivelDesarrollo = table.Column<int>(nullable: true),
                    IdRelevancia = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionCompetenciasTecnicasPuestoDetalle", x => x.IdEvaluacionCompetenciasTecnicasPuestoDetalle);
                    table.ForeignKey(
                        name: "FK_EvaluacionCompetenciasTecnicasPuestoDetalle_Destreza_IdDestreza",
                        column: x => x.IdDestreza,
                        principalTable: "Destreza",
                        principalColumn: "IdDestreza",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluacionCompetenciasTecnicasPuestoDetalle_EvaluacionCompetenciasTecnicasPuesto_IdEvaluacionCompetenciasTecnicasPuesto",
                        column: x => x.IdEvaluacionCompetenciasTecnicasPuesto,
                        principalTable: "EvaluacionCompetenciasTecnicasPuesto",
                        principalColumn: "IdEvaluacionCompetenciasTecnicasPuesto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluacionCompetenciasTecnicasPuestoDetalle_NivelDesarrollo_IdNivelDesarrollo",
                        column: x => x.IdNivelDesarrollo,
                        principalTable: "NivelDesarrollo",
                        principalColumn: "IdNivelDesarrollo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluacionCompetenciasTecnicasPuestoDetalle_Relevancia_IdRelevancia",
                        column: x => x.IdRelevancia,
                        principalTable: "Relevancia",
                        principalColumn: "IdRelevancia",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionCompetenciasUniversalesDetalle",
                columns: table => new
                {
                    IdEvaluacionCompetenciasUniversalesDetalle = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdDestreza = table.Column<int>(nullable: true),
                    IdEvaluacionCompetenciasUniversales = table.Column<int>(nullable: false),
                    IdFrecuenciaAplicacion = table.Column<int>(nullable: true),
                    IdRelevancia = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionCompetenciasUniversalesDetalle", x => x.IdEvaluacionCompetenciasUniversalesDetalle);
                    table.ForeignKey(
                        name: "FK_EvaluacionCompetenciasUniversalesDetalle_Destreza_IdDestreza",
                        column: x => x.IdDestreza,
                        principalTable: "Destreza",
                        principalColumn: "IdDestreza",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluacionCompUnivlesDetalle_EvaluacionCompsUni_IdEvalnCompsUniversales",
                        column: x => x.IdEvaluacionCompetenciasUniversales,
                        principalTable: "EvaluacionCompetenciasUniversales",
                        principalColumn: "IdEvaluacionCompetenciasUniversales",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluacionCompetenciasUniversalesDetalle_FrecuenciaAplicacion_IdFrecuenciaAplicacion",
                        column: x => x.IdFrecuenciaAplicacion,
                        principalTable: "FrecuenciaAplicacion",
                        principalColumn: "IdFrecuenciaAplicacion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluacionCompetenciasUniversalesDetalle_Relevancia_IdRelevancia",
                        column: x => x.IdRelevancia,
                        principalTable: "Relevancia",
                        principalColumn: "IdRelevancia",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FaseConcurso",
                columns: table => new
                {
                    IdFaseConcurso = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 500, nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    IdTipoConcurso = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaseConcurso", x => x.IdFaseConcurso);
                    table.ForeignKey(
                        name: "FK_FaseConcurso_TipoConcurso_IdTipoConcurso",
                        column: x => x.IdTipoConcurso,
                        principalTable: "TipoConcurso",
                        principalColumn: "IdTipoConcurso",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permiso",
                columns: table => new
                {
                    IdPermiso = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdTipoPermiso = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permiso", x => x.IdPermiso);
                    table.ForeignKey(
                        name: "FK_Permiso_TipoPermiso_IdTipoPermiso",
                        column: x => x.IdTipoPermiso,
                        principalTable: "TipoPermiso",
                        principalColumn: "IdTipoPermiso",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    IdPersona = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Apellidos = table.Column<string>(maxLength: 100, nullable: false),
                    CorreoPrivado = table.Column<string>(nullable: false),
                    FechaNacimiento = table.Column<DateTime>(nullable: false),
                    IdCanditato = table.Column<int>(nullable: true),
                    IdEstadoCivil = table.Column<int>(nullable: true),
                    IdEtnia = table.Column<int>(nullable: true),
                    IdGenero = table.Column<int>(nullable: false),
                    IdNacionalidad = table.Column<int>(nullable: true),
                    IdSexo = table.Column<int>(nullable: true),
                    IdTipoIdentificacion = table.Column<int>(nullable: true),
                    IdTipoSangre = table.Column<int>(nullable: true),
                    Identificacion = table.Column<string>(maxLength: 20, nullable: false),
                    LugarTrabajo = table.Column<string>(maxLength: 500, nullable: false),
                    Nombres = table.Column<string>(maxLength: 100, nullable: false),
                    TelefonoCasa = table.Column<string>(maxLength: 20, nullable: false),
                    TelefonoPrivado = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.IdPersona);
                    table.ForeignKey(
                        name: "FK_Persona_Canditato_IdCanditato",
                        column: x => x.IdCanditato,
                        principalTable: "Canditato",
                        principalColumn: "IdCanditato",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persona_EstadoCivil_IdEstadoCivil",
                        column: x => x.IdEstadoCivil,
                        principalTable: "EstadoCivil",
                        principalColumn: "IdEstadoCivil",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persona_Etnia_IdEtnia",
                        column: x => x.IdEtnia,
                        principalTable: "Etnia",
                        principalColumn: "IdEtnia",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persona_Genero_IdGenero",
                        column: x => x.IdGenero,
                        principalTable: "Genero",
                        principalColumn: "IdGenero",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persona_Nacionalidad_IdNacionalidad",
                        column: x => x.IdNacionalidad,
                        principalTable: "Nacionalidad",
                        principalColumn: "IdNacionalidad",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persona_Sexo_IdSexo",
                        column: x => x.IdSexo,
                        principalTable: "Sexo",
                        principalColumn: "IdSexo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persona_TipoIdentificacion_IdTipoIdentificacion",
                        column: x => x.IdTipoIdentificacion,
                        principalTable: "TipoIdentificacion",
                        principalColumn: "IdTipoIdentificacion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persona_TipoSangre_IdTipoSangre",
                        column: x => x.IdTipoSangre,
                        principalTable: "TipoSangre",
                        principalColumn: "IdTipoSangre",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle",
                columns: table => new
                {
                    IdEvaluacionTrabajoEquipoIniciativaLiderazgoDetalle = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEvaluacionTrabajoEquipoIniciativaLiderazgo = table.Column<int>(nullable: true),
                    IdFrecuenciaAplicacion = table.Column<int>(nullable: true),
                    IdRelevancia = table.Column<int>(nullable: true),
                    IdTrabajoEquipoIniciativaLiderazgo = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle", x => x.IdEvaluacionTrabajoEquipoIniciativaLiderazgoDetalle);
                    table.ForeignKey(
                        name: "FK_EvaluacionTrabajoEquIniLidDetall_EvalTrabEquiIniLid_IdEvaluacionTrabajoEquipoIniciativaLiderazgo",
                        column: x => x.IdEvaluacionTrabajoEquipoIniciativaLiderazgo,
                        principalTable: "EvaluacionTrabajoEquipoIniciativaLiderazgo",
                        principalColumn: "IdEvaluacionTrabajoEquipoIniciativaLiderazgo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle_FrecuenciaAplicacion_IdFrecuenciaAplicacion",
                        column: x => x.IdFrecuenciaAplicacion,
                        principalTable: "FrecuenciaAplicacion",
                        principalColumn: "IdFrecuenciaAplicacion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle_Relevancia_IdRelevancia",
                        column: x => x.IdRelevancia,
                        principalTable: "Relevancia",
                        principalColumn: "IdRelevancia",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle_TrabajoEquipoIniciativaLiderazgo_IdTrabajoEquipoIniciativaLiderazgo",
                        column: x => x.IdTrabajoEquipoIniciativaLiderazgo,
                        principalTable: "TrabajoEquipoIniciativaLiderazgo",
                        principalColumn: "IdTrabajoEquipoIniciativaLiderazgo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PreguntaRespuesta",
                columns: table => new
                {
                    IdPreguntaRespuesta = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdPregunta = table.Column<int>(nullable: false),
                    IdRespuesta = table.Column<int>(nullable: false),
                    Verdadero = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreguntaRespuesta", x => x.IdPreguntaRespuesta);
                    table.ForeignKey(
                        name: "FK_PreguntaRespuesta_Pregunta_IdPregunta",
                        column: x => x.IdPregunta,
                        principalTable: "Pregunta",
                        principalColumn: "IdPregunta",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreguntaRespuesta_Respuesta_IdRespuesta",
                        column: x => x.IdRespuesta,
                        principalTable: "Respuesta",
                        principalColumn: "IdRespuesta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ciudad",
                columns: table => new
                {
                    IdCiudad = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdProvincia = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudad", x => x.IdCiudad);
                    table.ForeignKey(
                        name: "FK_Ciudad_Provincia_IdProvincia",
                        column: x => x.IdProvincia,
                        principalTable: "Provincia",
                        principalColumn: "IdProvincia",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ModalidadPartida",
                columns: table => new
                {
                    IdModalidadPartida = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdRelacionLaboral = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModalidadPartida", x => x.IdModalidadPartida);
                    table.ForeignKey(
                        name: "FK_ModalidadPartida_RelacionLaboral_IdRelacionLaboral",
                        column: x => x.IdRelacionLaboral,
                        principalTable: "RelacionLaboral",
                        principalColumn: "IdRelacionLaboral",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoNombramiento",
                columns: table => new
                {
                    IdTipoNombramiento = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdRelacionLaboral = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoNombramiento", x => x.IdTipoNombramiento);
                    table.ForeignKey(
                        name: "FK_TipoNombramiento_RelacionLaboral_IdRelacionLaboral",
                        column: x => x.IdRelacionLaboral,
                        principalTable: "RelacionLaboral",
                        principalColumn: "IdRelacionLaboral",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonaCapacitacion",
                columns: table => new
                {
                    IdPersonaCapacitacion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    IdCapacitacion = table.Column<int>(nullable: false),
                    IdPersona = table.Column<int>(nullable: false),
                    Observaciones = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaCapacitacion", x => x.IdPersonaCapacitacion);
                    table.ForeignKey(
                        name: "FK_PersonaCapacitacion_Capacitacion_IdCapacitacion",
                        column: x => x.IdCapacitacion,
                        principalTable: "Capacitacion",
                        principalColumn: "IdCapacitacion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonaCapacitacion_Persona_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Persona",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonaDiscapacidad",
                columns: table => new
                {
                    IdPersonaDiscapacidad = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdPersona = table.Column<int>(nullable: true),
                    IdTipoDiscapacidad = table.Column<int>(nullable: true),
                    NumeroCarnet = table.Column<string>(maxLength: 20, nullable: false),
                    Porciento = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaDiscapacidad", x => x.IdPersonaDiscapacidad);
                    table.ForeignKey(
                        name: "FK_PersonaDiscapacidad_Persona_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Persona",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonaDiscapacidad_TipoDiscapacidad_IdTipoDiscapacidad",
                        column: x => x.IdTipoDiscapacidad,
                        principalTable: "TipoDiscapacidad",
                        principalColumn: "IdTipoDiscapacidad",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonaEnfermedad",
                columns: table => new
                {
                    IdPersonaEnfermedad = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdPersona = table.Column<int>(nullable: true),
                    IdTipoEnfermedad = table.Column<int>(nullable: true),
                    InstitucionEmite = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaEnfermedad", x => x.IdPersonaEnfermedad);
                    table.ForeignKey(
                        name: "FK_PersonaEnfermedad_Persona_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Persona",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonaEnfermedad_TipoEnfermedad_IdTipoEnfermedad",
                        column: x => x.IdTipoEnfermedad,
                        principalTable: "TipoEnfermedad",
                        principalColumn: "IdTipoEnfermedad",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonaEstudio",
                columns: table => new
                {
                    IdPersonaEstudio = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaGraduado = table.Column<DateTime>(nullable: false),
                    IdPersona = table.Column<int>(nullable: false),
                    IdTitulo = table.Column<int>(nullable: false),
                    Observaciones = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaEstudio", x => x.IdPersonaEstudio);
                    table.ForeignKey(
                        name: "FK_PersonaEstudio_Persona_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Persona",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonaEstudio_Titulo_IdTitulo",
                        column: x => x.IdTitulo,
                        principalTable: "Titulo",
                        principalColumn: "IdTitulo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrayectoriaLaboral",
                columns: table => new
                {
                    IdTrayectoriaLaboral = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DescripcionFunciones = table.Column<string>(nullable: true),
                    Empresa = table.Column<string>(maxLength: 100, nullable: true),
                    FechaFin = table.Column<DateTime>(nullable: true),
                    FechaInicio = table.Column<DateTime>(nullable: true),
                    IdPersona = table.Column<int>(nullable: false),
                    PuestoTrabajo = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrayectoriaLaboral", x => x.IdTrayectoriaLaboral);
                    table.ForeignKey(
                        name: "FK_TrayectoriaLaboral_Persona_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Persona",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parroquia",
                columns: table => new
                {
                    IdParroquia = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCiudad = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parroquia", x => x.IdParroquia);
                    table.ForeignKey(
                        name: "FK_Parroquia_Ciudad_IdCiudad",
                        column: x => x.IdCiudad,
                        principalTable: "Ciudad",
                        principalColumn: "IdCiudad",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sucursal",
                columns: table => new
                {
                    IdSucursal = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCiudad = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursal", x => x.IdSucursal);
                    table.ForeignKey(
                        name: "FK_Sucursal_Ciudad_IdCiudad",
                        column: x => x.IdCiudad,
                        principalTable: "Ciudad",
                        principalColumn: "IdCiudad",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dependencia",
                columns: table => new
                {
                    IdDependencia = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdDependenciaPadre = table.Column<int>(nullable: true),
                    IdSucursal = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependencia", x => x.IdDependencia);
                    table.ForeignKey(
                        name: "FK_Dependencia_Dependencia_IdDependenciaPadre",
                        column: x => x.IdDependenciaPadre,
                        principalTable: "Dependencia",
                        principalColumn: "IdDependencia",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dependencia_Sucursal_IdSucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursal",
                        principalColumn: "IdSucursal",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracionViatico",
                columns: table => new
                {
                    IdConfiguracionViatico = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdDependencia = table.Column<int>(nullable: false),
                    PorCientoAJustificar = table.Column<int>(nullable: false),
                    ValorEntregadoPorDia = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracionViatico", x => x.IdConfiguracionViatico);
                    table.ForeignKey(
                        name: "FK_ConfiguracionViatico_Dependencia_IdDependencia",
                        column: x => x.IdDependencia,
                        principalTable: "Dependencia",
                        principalColumn: "IdDependencia",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    IdEmpleado = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeclaracionJurada = table.Column<bool>(nullable: false),
                    DiasImposiciones = table.Column<int>(nullable: false),
                    FechaIngreso = table.Column<DateTime>(nullable: false),
                    FechaIngresoSectorPublico = table.Column<DateTime>(nullable: false),
                    IdCiudadLugarNacimiento = table.Column<int>(nullable: false),
                    IdDependencia = table.Column<int>(nullable: false),
                    IdPersona = table.Column<int>(nullable: false),
                    IdProvinciaLugarSufragio = table.Column<int>(nullable: false),
                    IngresosOtraActividad = table.Column<string>(maxLength: 20, nullable: false),
                    MesesImposiciones = table.Column<int>(nullable: false),
                    Nepotismo = table.Column<bool>(nullable: false),
                    TrabajoSuperintendenciaBanco = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.IdEmpleado);
                    table.ForeignKey(
                        name: "FK_Empleado_Ciudad_IdCiudadLugarNacimiento",
                        column: x => x.IdCiudadLugarNacimiento,
                        principalTable: "Ciudad",
                        principalColumn: "IdCiudad",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Empleado_Dependencia_IdDependencia",
                        column: x => x.IdDependencia,
                        principalTable: "Dependencia",
                        principalColumn: "IdDependencia",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Empleado_Persona_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Persona",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Empleado_Provincia_IdProvinciaLugarSufragio",
                        column: x => x.IdProvinciaLugarSufragio,
                        principalTable: "Provincia",
                        principalColumn: "IdProvincia",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IndiceOcupacional",
                columns: table => new
                {
                    IdIndiceOcupacional = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdDependencia = table.Column<int>(nullable: true),
                    IdEscalaGrados = table.Column<int>(nullable: true),
                    IdManualPuesto = table.Column<int>(nullable: true),
                    IdRolPuesto = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndiceOcupacional", x => x.IdIndiceOcupacional);
                    table.ForeignKey(
                        name: "FK_IndiceOcupacional_Dependencia_IdDependencia",
                        column: x => x.IdDependencia,
                        principalTable: "Dependencia",
                        principalColumn: "IdDependencia",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndiceOcupacional_EscalaGrados_IdEscalaGrados",
                        column: x => x.IdEscalaGrados,
                        principalTable: "EscalaGrados",
                        principalColumn: "IdEscalaGrados",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndiceOcupacional_ManualPuesto_IdManualPuesto",
                        column: x => x.IdManualPuesto,
                        principalTable: "ManualPuesto",
                        principalColumn: "IdManualPuesto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndiceOcupacional_RolPuesto_IdRolPuesto",
                        column: x => x.IdRolPuesto,
                        principalTable: "RolPuesto",
                        principalColumn: "IdRolPuesto",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcesoDetalle",
                columns: table => new
                {
                    IdProcesoDetalle = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdDependencia = table.Column<int>(nullable: true),
                    IdProceso = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcesoDetalle", x => x.IdProcesoDetalle);
                    table.ForeignKey(
                        name: "FK_ProcesoDetalle_Dependencia_IdDependencia",
                        column: x => x.IdDependencia,
                        principalTable: "Dependencia",
                        principalColumn: "IdDependencia",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProcesoDetalle_Proceso_IdProceso",
                        column: x => x.IdProceso,
                        principalTable: "Proceso",
                        principalColumn: "IdProceso",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SituacionPropuesta",
                columns: table => new
                {
                    IdSituacionPropuesta = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ano = table.Column<DateTime>(nullable: false),
                    Brecha = table.Column<int>(nullable: false),
                    IdDependencia = table.Column<int>(nullable: true),
                    NumeroPropuesta = table.Column<int>(nullable: false),
                    Observaciones = table.Column<string>(maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SituacionPropuesta", x => x.IdSituacionPropuesta);
                    table.ForeignKey(
                        name: "FK_SituacionPropuesta_Dependencia_IdDependencia",
                        column: x => x.IdDependencia,
                        principalTable: "Dependencia",
                        principalColumn: "IdDependencia",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccionPersonal",
                columns: table => new
                {
                    IdAccionPersonal = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Explicacion = table.Column<string>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    FechaRige = table.Column<DateTime>(nullable: false),
                    FechaRigeHasta = table.Column<DateTime>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IdTipoAccionPersonal = table.Column<int>(nullable: false),
                    NoDias = table.Column<int>(nullable: false),
                    Numero = table.Column<string>(maxLength: 20, nullable: false),
                    Solicitud = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccionPersonal", x => x.IdAccionPersonal);
                    table.ForeignKey(
                        name: "FK_AccionPersonal_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccionPersonal_TipoAccionPersonal_IdTipoAccionPersonal",
                        column: x => x.IdTipoAccionPersonal,
                        principalTable: "TipoAccionPersonal",
                        principalColumn: "IdTipoAccionPersonal",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CapacitacionPlanificacion",
                columns: table => new
                {
                    IdCapacitacionPlanificacion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    IdCapacitacionModalidad = table.Column<int>(nullable: false),
                    IdCapacitacionTemario = table.Column<int>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    NumeroHoras = table.Column<int>(nullable: false),
                    Presupuesto = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapacitacionPlanificacion", x => x.IdCapacitacionPlanificacion);
                    table.ForeignKey(
                        name: "FK_CapacitacionPlanificacion_CapacitacionModalidad_IdCapacitacionModalidad",
                        column: x => x.IdCapacitacionModalidad,
                        principalTable: "CapacitacionModalidad",
                        principalColumn: "IdCapacitacionModalidad",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CapacitacionPlanificacion_CapacitacionTemario_IdCapacitacionTemario",
                        column: x => x.IdCapacitacionTemario,
                        principalTable: "CapacitacionTemario",
                        principalColumn: "IdCapacitacionTemario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CapacitacionPlanificacion_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CapacitacionRecibida",
                columns: table => new
                {
                    IdCapacitacionRecibida = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCapacitacionTemario = table.Column<int>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapacitacionRecibida", x => x.IdCapacitacionRecibida);
                    table.ForeignKey(
                        name: "FK_CapacitacionRecibida_CapacitacionTemario_IdCapacitacionTemario",
                        column: x => x.IdCapacitacionTemario,
                        principalTable: "CapacitacionTemario",
                        principalColumn: "IdCapacitacionTemario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CapacitacionRecibida_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConfirmacionLectura",
                columns: table => new
                {
                    IdConfirmacionLectura = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEmpleado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfirmacionLectura", x => x.IdConfirmacionLectura);
                    table.ForeignKey(
                        name: "FK_ConfirmacionLectura_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DatosBancarios",
                columns: table => new
                {
                    IdDatosBancarios = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ahorros = table.Column<bool>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IdInstitucionFinanciera = table.Column<int>(nullable: false),
                    NumeroCuenta = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosBancarios", x => x.IdDatosBancarios);
                    table.ForeignKey(
                        name: "FK_DatosBancarios_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DatosBancarios_InstitucionFinanciera_IdInstitucionFinanciera",
                        column: x => x.IdInstitucionFinanciera,
                        principalTable: "InstitucionFinanciera",
                        principalColumn: "IdInstitucionFinanciera",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeclaracionPatrimonioPersonal",
                columns: table => new
                {
                    IdDeclaracionPatrimonioPersonal = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaDeclaracion = table.Column<DateTime>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    PromedioMensualIngresos = table.Column<decimal>(type: "decimal", nullable: false),
                    PromedioMensualOtrosIngresos = table.Column<decimal>(type: "decimal", nullable: false),
                    TotalActivosAnioActual = table.Column<decimal>(type: "decimal", nullable: false),
                    TotalActivosAnioAnterior = table.Column<decimal>(type: "decimal", nullable: false),
                    TotalPasivosAnioActual = table.Column<decimal>(type: "decimal", nullable: false),
                    TotalPasivosAnioAnterior = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclaracionPatrimonioPersonal", x => x.IdDeclaracionPatrimonioPersonal);
                    table.ForeignKey(
                        name: "FK_DeclaracionPatrimonioPersonal_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentosParentescodos",
                columns: table => new
                {
                    IdDocumentosSubidos = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Are = table.Column<string>(maxLength: 400, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 400, nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    FechaCaducidad = table.Column<DateTime>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    NombreArchivo = table.Column<string>(maxLength: 250, nullable: false),
                    Ubicacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentosParentescodos", x => x.IdDocumentosSubidos);
                    table.ForeignKey(
                        name: "FK_DocumentosParentescodos_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadoContactoEmergencia",
                columns: table => new
                {
                    IdEmpleadoContactoEmergencia = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IdParentesco = table.Column<int>(nullable: false),
                    IdPersona = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoContactoEmergencia", x => x.IdEmpleadoContactoEmergencia);
                    table.ForeignKey(
                        name: "FK_EmpleadoContactoEmergencia_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpleadoContactoEmergencia_Parentesco_IdParentesco",
                        column: x => x.IdParentesco,
                        principalTable: "Parentesco",
                        principalColumn: "IdParentesco",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpleadoContactoEmergencia_Persona_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Persona",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadoFamiliar",
                columns: table => new
                {
                    IdEmpleadoFamiliar = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IdParentesco = table.Column<int>(nullable: false),
                    IdPersona = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoFamiliar", x => x.IdEmpleadoFamiliar);
                    table.ForeignKey(
                        name: "FK_EmpleadoFamiliar_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpleadoFamiliar_Parentesco_IdParentesco",
                        column: x => x.IdParentesco,
                        principalTable: "Parentesco",
                        principalColumn: "IdParentesco",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpleadoFamiliar_Persona_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Persona",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadoFormularioCapacitacion",
                columns: table => new
                {
                    IdEmpleadoFormularioCapacitacion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEvento = table.Column<int>(nullable: true),
                    IdFormularioCapacitacion = table.Column<int>(nullable: false),
                    IdServidor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoFormularioCapacitacion", x => x.IdEmpleadoFormularioCapacitacion);
                    table.ForeignKey(
                        name: "FK_EmpleadoFormularioCapacitacion_FormularioCapacitacion_IdFormularioCapacitacion",
                        column: x => x.IdFormularioCapacitacion,
                        principalTable: "FormularioCapacitacion",
                        principalColumn: "IdFormularioCapacitacion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpleadoFormularioCapacitacion_Empleado_IdServidor",
                        column: x => x.IdServidor,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadoIE",
                columns: table => new
                {
                    IdEmpeladoIE = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    Fijo = table.Column<bool>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IdIngresoEgresoRMU = table.Column<int>(nullable: false),
                    Tipo = table.Column<string>(maxLength: 20, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoIE", x => x.IdEmpeladoIE);
                    table.ForeignKey(
                        name: "FK_EmpleadoIE_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpleadoIE_IngresoEgresoRMU_IdIngresoEgresoRMU",
                        column: x => x.IdIngresoEgresoRMU,
                        principalTable: "IngresoEgresoRMU",
                        principalColumn: "IdIngresoEgresoRMU",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadoImpuestoRenta",
                columns: table => new
                {
                    IdEmpleadoImpuestoRenta = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaDesde = table.Column<DateTime>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IngresoTotal = table.Column<decimal>(type: "decimal", nullable: false),
                    OtrosIngresos = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoImpuestoRenta", x => x.IdEmpleadoImpuestoRenta);
                    table.ForeignKey(
                        name: "FK_EmpleadoImpuestoRenta_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadoNepotismo",
                columns: table => new
                {
                    IdEmpleadoNepotismo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEmpleado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoNepotismo", x => x.IdEmpleadoNepotismo);
                    table.ForeignKey(
                        name: "FK_EmpleadoNepotismo_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadoSaldoVacaciones",
                columns: table => new
                {
                    IdEmpleadoSaldoVacaciones = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEmpleado = table.Column<int>(nullable: false),
                    SaldoDias = table.Column<decimal>(type: "decimal", nullable: false),
                    SaldoMonetario = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoSaldoVacaciones", x => x.IdEmpleadoSaldoVacaciones);
                    table.ForeignKey(
                        name: "FK_EmpleadoSaldoVacaciones_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Evaluador",
                columns: table => new
                {
                    IdEvaluador = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ano = table.Column<DateTime>(nullable: false),
                    IdDependencia = table.Column<int>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluador", x => x.IdEvaluador);
                    table.ForeignKey(
                        name: "FK_Evaluador_Dependencia_IdDependencia",
                        column: x => x.IdDependencia,
                        principalTable: "Dependencia",
                        principalColumn: "IdDependencia",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluador_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FormularioAnalisisOcupacional",
                columns: table => new
                {
                    IdFormularioAnalisisOcupacional = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Anio = table.Column<int>(nullable: false),
                    ExtPersJurídicasPubNivelNacional = table.Column<bool>(nullable: false),
                    ExternosCiudadania = table.Column<bool>(nullable: false),
                    FechaRegistro = table.Column<DateTime>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    InternoMismoProceso = table.Column<bool>(nullable: false),
                    InternoOtroProceso = table.Column<bool>(nullable: false),
                    MisionPuesto = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormularioAnalisisOcupacional", x => x.IdFormularioAnalisisOcupacional);
                    table.ForeignKey(
                        name: "FK_FormularioAnalisisOcupacional_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FormularioDevengacion",
                columns: table => new
                {
                    IdFormularioDevengacion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnalistaDesarrolloInstitucional = table.Column<int>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IdEvento = table.Column<int>(nullable: false),
                    ModoSocial = table.Column<string>(maxLength: 250, nullable: false),
                    ModosScializacionId = table.Column<int>(nullable: false),
                    ResponsableArea = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormularioDevengacion", x => x.IdFormularioDevengacion);
                    table.ForeignKey(
                        name: "FK_FormularioDevengacion_Empleado_AnalistaDesarrolloInstitucional",
                        column: x => x.AnalistaDesarrolloInstitucional,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormularioDevengacion_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormularioDevengacion_ModosScializacion_ModosScializacionId",
                        column: x => x.ModosScializacionId,
                        principalTable: "ModosScializacion",
                        principalColumn: "IdModosScializacion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormularioDevengacion_Empleado_ResponsableArea",
                        column: x => x.ResponsableArea,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Liquidacion",
                columns: table => new
                {
                    IdLiquidacion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IdRubroLiquidacion = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liquidacion", x => x.IdLiquidacion);
                    table.ForeignKey(
                        name: "FK_Liquidacion_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Liquidacion_RubroLiquidacion_IdRubroLiquidacion",
                        column: x => x.IdRubroLiquidacion,
                        principalTable: "RubroLiquidacion",
                        principalColumn: "IdRubroLiquidacion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonaPaquetesInformaticos",
                columns: table => new
                {
                    IdPersonaPaquetesInformaticos = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IdPaquetesInformaticos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaPaquetesInformaticos", x => x.IdPersonaPaquetesInformaticos);
                    table.ForeignKey(
                        name: "FK_PersonaPaquetesInformaticos_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonaPaquetesInformaticos_PaquetesInformaticos_IdPaquetesInformaticos",
                        column: x => x.IdPaquetesInformaticos,
                        principalTable: "PaquetesInformaticos",
                        principalColumn: "IdPaquetesInformaticos",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanGestionCambio",
                columns: table => new
                {
                    IdPlanGestionCambio = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AprobadoPor = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(maxLength: 500, nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    RealizadoPor = table.Column<int>(nullable: false),
                    Titulo = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanGestionCambio", x => x.IdPlanGestionCambio);
                    table.ForeignKey(
                        name: "FK_PlanGestionCambio_Empleado_AprobadoPor",
                        column: x => x.AprobadoPor,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanGestionCambio_Empleado_RealizadoPor",
                        column: x => x.RealizadoPor,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanificacionHE",
                columns: table => new
                {
                    IdPlanificacionHE = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estado = table.Column<bool>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    NoHoras = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanificacionHE", x => x.IdPlanificacionHE);
                    table.ForeignKey(
                        name: "FK_PlanificacionHE_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Provisiones",
                columns: table => new
                {
                    IdProvisiones = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IdTipoProvision = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provisiones", x => x.IdProvisiones);
                    table.ForeignKey(
                        name: "FK_Provisiones_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Provisiones_TipoProvision_IdTipoProvision",
                        column: x => x.IdTipoProvision,
                        principalTable: "TipoProvision",
                        principalColumn: "IdTipoProvision",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RealizaExamenInduccion",
                columns: table => new
                {
                    IdRealizaExamenInduccion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Calificacion = table.Column<decimal>(type: "decimal", nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IdEvaluacionInduccion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealizaExamenInduccion", x => x.IdRealizaExamenInduccion);
                    table.ForeignKey(
                        name: "FK_RealizaExamenInduccion_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RealizaExamenInduccion_EvaluacionInducion_IdEvaluacionInduccion",
                        column: x => x.IdEvaluacionInduccion,
                        principalTable: "EvaluacionInducion",
                        principalColumn: "IdEvaluacionInduccion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegistroEntradaSalida",
                columns: table => new
                {
                    IdRegistroEntradaSalida = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    HoraEntrada = table.Column<TimeSpan>(nullable: false),
                    HoraSalida = table.Column<TimeSpan>(nullable: false),
                    IdEmpleado = table.Column<int>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroEntradaSalida", x => x.IdRegistroEntradaSalida);
                    table.ForeignKey(
                        name: "FK_RegistroEntradaSalida_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolPagos",
                columns: table => new
                {
                    IdRolPagos = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    SaldoFinal = table.Column<decimal>(type: "decimal", nullable: false),
                    SaldoInicial = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolPagos", x => x.IdRolPagos);
                    table.ForeignKey(
                        name: "FK_RolPagos_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudAcumulacionDecimos",
                columns: table => new
                {
                    IdSolicitudAcumulacionDecimos = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AcumulaDecimoCuarto = table.Column<bool>(nullable: false),
                    AcumulaDecimoTercero = table.Column<bool>(nullable: false),
                    FechaSolicitud = table.Column<DateTime>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudAcumulacionDecimos", x => x.IdSolicitudAcumulacionDecimos);
                    table.ForeignKey(
                        name: "FK_SolicitudAcumulacionDecimos_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudAnticipo",
                columns: table => new
                {
                    IdSolicitudAnticipo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CantidadCancelada = table.Column<decimal>(type: "decimal", nullable: false),
                    CantidadSolicitada = table.Column<decimal>(type: "decimal", nullable: false),
                    FechaAprobado = table.Column<DateTime>(nullable: false),
                    FechaSolicitud = table.Column<DateTime>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IdEstado = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudAnticipo", x => x.IdSolicitudAnticipo);
                    table.ForeignKey(
                        name: "FK_SolicitudAnticipo_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudAnticipo_Estado_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudCertificadoPersonal",
                columns: table => new
                {
                    IdSolicitudCertificadoPersonal = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaSolicitud = table.Column<DateTime>(nullable: false),
                    IdEmpleadoDirigidoA = table.Column<int>(nullable: true),
                    IdEmpleadoSolicitante = table.Column<int>(nullable: true),
                    IdEstado = table.Column<int>(nullable: false),
                    IdTipoCertificado = table.Column<int>(nullable: false),
                    Observaciones = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudCertificadoPersonal", x => x.IdSolicitudCertificadoPersonal);
                    table.ForeignKey(
                        name: "FK_SolicitudCertificadoPersonal_Empleado_IdEmpleadoDirigidoA",
                        column: x => x.IdEmpleadoDirigidoA,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudCertificadoPersonal_Empleado_IdEmpleadoSolicitante",
                        column: x => x.IdEmpleadoSolicitante,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudCertificadoPersonal_Estado_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudCertificadoPersonal_TipoCertificado_IdTipoCertificado",
                        column: x => x.IdTipoCertificado,
                        principalTable: "TipoCertificado",
                        principalColumn: "IdTipoCertificado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudHorasExtras",
                columns: table => new
                {
                    IdSolicitudHorasExtras = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aprobado = table.Column<bool>(nullable: false),
                    CantidadHoras = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    FechaAprobado = table.Column<DateTime>(nullable: false),
                    FechaSolicitud = table.Column<DateTime>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    Observaciones = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudHorasExtras", x => x.IdSolicitudHorasExtras);
                    table.ForeignKey(
                        name: "FK_SolicitudHorasExtras_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudLiquidacionHaberes",
                columns: table => new
                {
                    IdSolicitudLiquidacionHaberes = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaSolicitud = table.Column<DateTime>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: true),
                    TotalDecimoCuarto = table.Column<decimal>(type: "decimal", nullable: false),
                    TotalDecimoTercero = table.Column<decimal>(type: "decimal", nullable: false),
                    TotalDesahucio = table.Column<decimal>(type: "decimal", nullable: false),
                    TotalDespidoIntempestivo = table.Column<decimal>(type: "decimal", nullable: false),
                    TotalFondoReserva = table.Column<decimal>(type: "decimal", nullable: false),
                    TotalVacaciones = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudLiquidacionHaberes", x => x.IdSolicitudLiquidacionHaberes);
                    table.ForeignKey(
                        name: "FK_SolicitudLiquidacionHaberes_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudModificacionFichaEmpleado",
                columns: table => new
                {
                    IdSolicitudModificacionFichaEmpleado = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Apellidos = table.Column<string>(nullable: true),
                    CapacitacionesRecibidas = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    ExperienciaLaboral = table.Column<string>(nullable: true),
                    FechaSolicitud = table.Column<DateTime>(nullable: false),
                    FormacionAcademica = table.Column<string>(nullable: true),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IdEstado = table.Column<int>(nullable: true),
                    Nombres = table.Column<string>(nullable: true),
                    ParentescosFamiliares = table.Column<string>(nullable: true),
                    TelefonoCasa = table.Column<string>(nullable: true),
                    TelefonoPrivado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudModificacionFichaEmpleado", x => x.IdSolicitudModificacionFichaEmpleado);
                    table.ForeignKey(
                        name: "FK_SolicitudModificacionFichaEmpleado_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudModificacionFichaEmpleado_Estado_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudPermiso",
                columns: table => new
                {
                    IdSolicitudPermiso = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaAprobado = table.Column<DateTime>(nullable: false),
                    FechaSolicitud = table.Column<DateTime>(nullable: false),
                    HoraDesde = table.Column<TimeSpan>(nullable: false),
                    HoraHasta = table.Column<TimeSpan>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IdEstado = table.Column<int>(nullable: true),
                    IdPermiso = table.Column<int>(nullable: true),
                    Motivo = table.Column<string>(maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudPermiso", x => x.IdSolicitudPermiso);
                    table.ForeignKey(
                        name: "FK_SolicitudPermiso_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudPermiso_Estado_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudPermiso_Permiso_IdPermiso",
                        column: x => x.IdPermiso,
                        principalTable: "Permiso",
                        principalColumn: "IdPermiso",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudPlanificacionVacaciones",
                columns: table => new
                {
                    IdSolicitudPlanificacionVacaciones = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaDesde = table.Column<DateTime>(nullable: false),
                    FechaHasta = table.Column<DateTime>(nullable: false),
                    FechaSolicitud = table.Column<DateTime>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IdEstado = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudPlanificacionVacaciones", x => x.IdSolicitudPlanificacionVacaciones);
                    table.ForeignKey(
                        name: "FK_SolicitudPlanificacionVacaciones_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudPlanificacionVacaciones_Estado_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudVacaciones",
                columns: table => new
                {
                    IdSolicitudVacaciones = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaAprobada = table.Column<DateTime>(nullable: false),
                    FechaDesde = table.Column<DateTime>(nullable: false),
                    FechaHasta = table.Column<DateTime>(nullable: false),
                    FechaSolicitud = table.Column<DateTime>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IdEstado = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudVacaciones", x => x.IdSolicitudVacaciones);
                    table.ForeignKey(
                        name: "FK_SolicitudVacaciones_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudVacaciones_Estado_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudViatico",
                columns: table => new
                {
                    IdSolicitudViatico = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 20, nullable: false),
                    FechaAprobado = table.Column<DateTime>(nullable: false),
                    FechaSolicitud = table.Column<DateTime>(nullable: false),
                    IdConfiguracionViatico = table.Column<int>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IdEstado = table.Column<int>(nullable: true),
                    IdTipoViatico = table.Column<int>(nullable: false),
                    ValorEstimado = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudViatico", x => x.IdSolicitudViatico);
                    table.ForeignKey(
                        name: "FK_SolicitudViatico_ConfiguracionViatico_IdConfiguracionViatico",
                        column: x => x.IdConfiguracionViatico,
                        principalTable: "ConfiguracionViatico",
                        principalColumn: "IdConfiguracionViatico",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudViatico_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudViatico_Estado_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudViatico_TipoViatico_IdTipoViatico",
                        column: x => x.IdTipoViatico,
                        principalTable: "TipoViatico",
                        principalColumn: "IdTipoViatico",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IndiceOcupacionalActividadesEsenciales",
                columns: table => new
                {
                    IdIndiceOcupacionalActividadesEsenciales = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdActividadesEsenciales = table.Column<int>(nullable: false),
                    IdIndiceOcupacional = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndiceOcupacionalActividadesEsenciales", x => x.IdIndiceOcupacionalActividadesEsenciales);
                    table.ForeignKey(
                        name: "FK_IndiceOcupacionalActividadesEsenciales_ActividadesEsenciales_IdActividadesEsenciales",
                        column: x => x.IdActividadesEsenciales,
                        principalTable: "ActividadesEsenciales",
                        principalColumn: "ActividadesEsencialesId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndiceOcupacionalActividadesEsenciales_IndiceOcupacional_IdIndiceOcupacional",
                        column: x => x.IdIndiceOcupacional,
                        principalTable: "IndiceOcupacional",
                        principalColumn: "IdIndiceOcupacional",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IndiceOcupacionalAreaConocimiento",
                columns: table => new
                {
                    IdIndiceOcupacionalAreaConocimiento = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdAreaConocimiento = table.Column<int>(nullable: false),
                    IdIndiceOcupacional = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndiceOcupacionalAreaConocimiento", x => x.IdIndiceOcupacionalAreaConocimiento);
                    table.ForeignKey(
                        name: "FK_IndiceOcupacionalAreaConocimiento_AreaConocimiento_IdAreaConocimiento",
                        column: x => x.IdAreaConocimiento,
                        principalTable: "AreaConocimiento",
                        principalColumn: "IdAreaConocimiento",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndiceOcupacionalAreaConocimiento_IndiceOcupacional_IdIndiceOcupacional",
                        column: x => x.IdIndiceOcupacional,
                        principalTable: "IndiceOcupacional",
                        principalColumn: "IdIndiceOcupacional",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IndiceOcupacionalCapacitaciones",
                columns: table => new
                {
                    IdIndiceOcupacionalCapacitaciones = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCapacitacion = table.Column<int>(nullable: false),
                    IdIndiceOcupacional = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndiceOcupacionalCapacitaciones", x => x.IdIndiceOcupacionalCapacitaciones);
                    table.ForeignKey(
                        name: "FK_IndiceOcupacionalCapacitaciones_Capacitacion_IdCapacitacion",
                        column: x => x.IdCapacitacion,
                        principalTable: "Capacitacion",
                        principalColumn: "IdCapacitacion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndiceOcupacionalCapacitaciones_IndiceOcupacional_IdIndiceOcupacional",
                        column: x => x.IdIndiceOcupacional,
                        principalTable: "IndiceOcupacional",
                        principalColumn: "IdIndiceOcupacional",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IndiceOcupacionalComportamientoObservable",
                columns: table => new
                {
                    IdIndiceOcupacionalComportamientoObservable = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdComportamientoObservable = table.Column<int>(nullable: false),
                    IdIndiceOcupacional = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndiceOcupacionalComportamientoObservable", x => x.IdIndiceOcupacionalComportamientoObservable);
                    table.ForeignKey(
                        name: "FK_IndiceOcupacionalComportamientoObservable_ComportamientoObservable_IdComportamientoObservable",
                        column: x => x.IdComportamientoObservable,
                        principalTable: "ComportamientoObservable",
                        principalColumn: "ComportamientoObservableId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndiceOcupacionalComportamientoObservable_IndiceOcupacional_IdIndiceOcupacional",
                        column: x => x.IdIndiceOcupacional,
                        principalTable: "IndiceOcupacional",
                        principalColumn: "IdIndiceOcupacional",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IndiceOcupacionalConocimientosAdicionales",
                columns: table => new
                {
                    IdIndiceOcupacionalConocimientosAdicionales = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdConocimientosAdicionales = table.Column<int>(nullable: false),
                    IdIndiceOcupacional = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndiceOcupacionalConocimientosAdicionales", x => x.IdIndiceOcupacionalConocimientosAdicionales);
                    table.ForeignKey(
                        name: "FK_IndiceOcupacionalConocimientosAdicionales_ConocimientosAdicionales_IdConocimientosAdicionales",
                        column: x => x.IdConocimientosAdicionales,
                        principalTable: "ConocimientosAdicionales",
                        principalColumn: "IdConocimientosAdicionales",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndiceOcupacionalConocimientosAdicionales_IndiceOcupacional_IdIndiceOcupacional",
                        column: x => x.IdIndiceOcupacional,
                        principalTable: "IndiceOcupacional",
                        principalColumn: "IdIndiceOcupacional",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IndiceOcupacionalEstudio",
                columns: table => new
                {
                    IdIndiceOcupacionalEstudio = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEstudio = table.Column<int>(nullable: false),
                    IdIndiceOcupacional = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndiceOcupacionalEstudio", x => x.IdIndiceOcupacionalEstudio);
                    table.ForeignKey(
                        name: "FK_IndiceOcupacionalEstudio_Estudio_IdEstudio",
                        column: x => x.IdEstudio,
                        principalTable: "Estudio",
                        principalColumn: "IdEstudio",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndiceOcupacionalEstudio_IndiceOcupacional_IdIndiceOcupacional",
                        column: x => x.IdIndiceOcupacional,
                        principalTable: "IndiceOcupacional",
                        principalColumn: "IdIndiceOcupacional",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IndiceOcupacionalModalidadPartida",
                columns: table => new
                {
                    IdIndiceOcupacionalModalidadPartida = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IdFondoFinanciamiento = table.Column<int>(nullable: true),
                    IdIndiceOcupacional = table.Column<int>(nullable: false),
                    IdModalidadPartida = table.Column<int>(nullable: true),
                    IdTipoNombramiento = table.Column<int>(nullable: true),
                    SalarioReal = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndiceOcupacionalModalidadPartida", x => x.IdIndiceOcupacionalModalidadPartida);
                    table.ForeignKey(
                        name: "FK_IndiceOcupacionalModalidadPartida_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndiceOcupacionalModalidadPartida_FondoFinanciamiento_IdFondoFinanciamiento",
                        column: x => x.IdFondoFinanciamiento,
                        principalTable: "FondoFinanciamiento",
                        principalColumn: "IdFondoFinanciamiento",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndiceOcupacionalModalidadPartida_IndiceOcupacional_IdIndiceOcupacional",
                        column: x => x.IdIndiceOcupacional,
                        principalTable: "IndiceOcupacional",
                        principalColumn: "IdIndiceOcupacional",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndiceOcupacionalModalidadPartida_ModalidadPartida_IdModalidadPartida",
                        column: x => x.IdModalidadPartida,
                        principalTable: "ModalidadPartida",
                        principalColumn: "IdModalidadPartida",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndiceOcupacionalModalidadPartida_TipoNombramiento_IdTipoNombramiento",
                        column: x => x.IdTipoNombramiento,
                        principalTable: "TipoNombramiento",
                        principalColumn: "IdTipoNombramiento",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MisionIndiceOcupacional",
                columns: table => new
                {
                    IdMisionIndiceOcupacional = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idIndiceOcupacional = table.Column<int>(nullable: false),
                    IdMision = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MisionIndiceOcupacional", x => x.IdMisionIndiceOcupacional);
                    table.ForeignKey(
                        name: "FK_MisionIndiceOcupacional_IndiceOcupacional_idIndiceOcupacional",
                        column: x => x.idIndiceOcupacional,
                        principalTable: "IndiceOcupacional",
                        principalColumn: "IdIndiceOcupacional",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MisionIndiceOcupacional_Mision_IdMision",
                        column: x => x.IdMision,
                        principalTable: "Mision",
                        principalColumn: "IdMision",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RelacionesInternasExternasIndiceOcupacional",
                columns: table => new
                {
                    IdRelacionesInternasExternasIndiceOcupacional = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdIndiceOcupacional = table.Column<int>(nullable: false),
                    RelacionesInternasExternasId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelacionesInternasExternasIndiceOcupacional", x => x.IdRelacionesInternasExternasIndiceOcupacional);
                    table.ForeignKey(
                        name: "FK_RelacionesInternasExternasIndiceOcupacional_IndiceOcupacional_IdIndiceOcupacional",
                        column: x => x.IdIndiceOcupacional,
                        principalTable: "IndiceOcupacional",
                        principalColumn: "IdIndiceOcupacional",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelacionesInternasExternasIndiceOcupacional_RelacionesInternasExternas_RelacionesInternasExternasId",
                        column: x => x.RelacionesInternasExternasId,
                        principalTable: "RelacionesInternasExternas",
                        principalColumn: "IdRelacionesInternasExternas",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CapacitacionEncuesta",
                columns: table => new
                {
                    IdCapacitacionEncuesta = table.Column<string>(maxLength: 450, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 20, nullable: false),
                    IdCapacitacionRecibida = table.Column<int>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapacitacionEncuesta", x => x.IdCapacitacionEncuesta);
                    table.ForeignKey(
                        name: "FK_CapacitacionEncuesta_CapacitacionRecibida_IdCapacitacionRecibida",
                        column: x => x.IdCapacitacionRecibida,
                        principalTable: "CapacitacionRecibida",
                        principalColumn: "IdCapacitacionRecibida",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CapacitacionEncuesta_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CapacitacionProveedor",
                columns: table => new
                {
                    IdCapacitacionProveedor = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Direccion = table.Column<string>(maxLength: 150, nullable: false),
                    IdCapacitacionRecibida = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 60, nullable: false),
                    Telefono = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapacitacionProveedor", x => x.IdCapacitacionProveedor);
                    table.ForeignKey(
                        name: "FK_CapacitacionProveedor_CapacitacionRecibida_IdCapacitacionRecibida",
                        column: x => x.IdCapacitacionRecibida,
                        principalTable: "CapacitacionRecibida",
                        principalColumn: "IdCapacitacionRecibida",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DependenciaDocumento",
                columns: table => new
                {
                    IdDependenciaDocumento = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdDependencia = table.Column<int>(nullable: false),
                    IdDocumentosSubidos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DependenciaDocumento", x => x.IdDependenciaDocumento);
                    table.ForeignKey(
                        name: "FK_DependenciaDocumento_Dependencia_IdDependencia",
                        column: x => x.IdDependencia,
                        principalTable: "Dependencia",
                        principalColumn: "IdDependencia",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DependenciaDocumento_DocumentosParentescodos_IdDocumentosSubidos",
                        column: x => x.IdDocumentosSubidos,
                        principalTable: "DocumentosParentescodos",
                        principalColumn: "IdDocumentosSubidos",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RMU",
                columns: table => new
                {
                    IdRMU = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    IdEmpeladoIE = table.Column<int>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IdTipoRMU = table.Column<int>(nullable: false),
                    Quincena = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RMU", x => x.IdRMU);
                    table.ForeignKey(
                        name: "FK_RMU_EmpleadoIE_IdEmpeladoIE",
                        column: x => x.IdEmpeladoIE,
                        principalTable: "EmpleadoIE",
                        principalColumn: "IdEmpeladoIE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RMU_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RMU_TipoRMU_IdTipoRMU",
                        column: x => x.IdTipoRMU,
                        principalTable: "TipoRMU",
                        principalColumn: "IdTipoRMU",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GastoRubro",
                columns: table => new
                {
                    IdGastoRubro = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GastoProyectado = table.Column<decimal>(type: "decimal", nullable: false),
                    IdEmpleadoImpuestoRenta = table.Column<int>(nullable: false),
                    IdRubro = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GastoRubro", x => x.IdGastoRubro);
                    table.ForeignKey(
                        name: "FK_GastoRubro_EmpleadoImpuestoRenta_IdEmpleadoImpuestoRenta",
                        column: x => x.IdEmpleadoImpuestoRenta,
                        principalTable: "EmpleadoImpuestoRenta",
                        principalColumn: "IdEmpleadoImpuestoRenta",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GastoRubro_Rubro_IdRubro",
                        column: x => x.IdRubro,
                        principalTable: "Rubro",
                        principalColumn: "IdRubro",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Eval001",
                columns: table => new
                {
                    IdEval001 = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EstaConforme = table.Column<bool>(nullable: false),
                    FechaEvaluacionDesde = table.Column<DateTime>(nullable: false),
                    FechaEvaluacionHasta = table.Column<DateTime>(nullable: false),
                    FechaRegistro = table.Column<DateTime>(nullable: false),
                    IdEmpleadoEvaluado = table.Column<int>(nullable: true),
                    IdEscalaEvaluacionTotal = table.Column<int>(nullable: true),
                    IdEvaluacionActividadesPuestoTrabajo = table.Column<int>(nullable: true),
                    IdEvaluacionCompetenciasTecnicasPuesto = table.Column<int>(nullable: true),
                    IdEvaluacionCompetenciasUniversales = table.Column<int>(nullable: true),
                    IdEvaluacionConocimiento = table.Column<int>(nullable: true),
                    IdEvaluacionTrabajoEquipoIniciativaLiderazgo = table.Column<int>(nullable: true),
                    IdEvaluador = table.Column<int>(nullable: false),
                    Observaciones = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eval001", x => x.IdEval001);
                    table.ForeignKey(
                        name: "FK_Eval001_Empleado_IdEmpleadoEvaluado",
                        column: x => x.IdEmpleadoEvaluado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Eval001_EscalaEvaluacionTotal_IdEscalaEvaluacionTotal",
                        column: x => x.IdEscalaEvaluacionTotal,
                        principalTable: "EscalaEvaluacionTotal",
                        principalColumn: "IdEscalaEvaluacionTotal",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Eval001_EvaluacionActividadesPuestoTrabajo_IdEvaluacionActividadesPuestoTrabajo",
                        column: x => x.IdEvaluacionActividadesPuestoTrabajo,
                        principalTable: "EvaluacionActividadesPuestoTrabajo",
                        principalColumn: "IdEvaluacionActividadesPuestoTrabajo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Eval001_EvaluacionCompetenciasTecnicasPuesto_IdEvaluacionCompetenciasTecnicasPuesto",
                        column: x => x.IdEvaluacionCompetenciasTecnicasPuesto,
                        principalTable: "EvaluacionCompetenciasTecnicasPuesto",
                        principalColumn: "IdEvaluacionCompetenciasTecnicasPuesto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Eval001_EvaluacionCompetenciasUniversales_IdEvaluacionCompetenciasUniversales",
                        column: x => x.IdEvaluacionCompetenciasUniversales,
                        principalTable: "EvaluacionCompetenciasUniversales",
                        principalColumn: "IdEvaluacionCompetenciasUniversales",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Eval001_EvaluacionConocimiento_IdEvaluacionConocimiento",
                        column: x => x.IdEvaluacionConocimiento,
                        principalTable: "EvaluacionConocimiento",
                        principalColumn: "IdEvaluacionConocimiento",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Eval001_EvaluacionTrabajoEquipoIniciativaLiderazgo_IdEvaluacionTrabajoEquipoIniciativaLiderazgo",
                        column: x => x.IdEvaluacionTrabajoEquipoIniciativaLiderazgo,
                        principalTable: "EvaluacionTrabajoEquipoIniciativaLiderazgo",
                        principalColumn: "IdEvaluacionTrabajoEquipoIniciativaLiderazgo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Eval001_Evaluador_IdEvaluador",
                        column: x => x.IdEvaluador,
                        principalTable: "Evaluador",
                        principalColumn: "IdEvaluador",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActividadesAnalisisOcupacional",
                columns: table => new
                {
                    IdActividadesAnalisisOcupacional = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Actividades = table.Column<string>(nullable: true),
                    Cumple = table.Column<bool>(nullable: false),
                    IdFormularioAnalisisOcupacional = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActividadesAnalisisOcupacional", x => x.IdActividadesAnalisisOcupacional);
                    table.ForeignKey(
                        name: "FK_ActividadesAnalisisOcupacional_FormularioAnalisisOcupacional_IdFormularioAnalisisOcupacional",
                        column: x => x.IdFormularioAnalisisOcupacional,
                        principalTable: "FormularioAnalisisOcupacional",
                        principalColumn: "IdFormularioAnalisisOcupacional",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdministracionTalentoHumano",
                columns: table => new
                {
                    IdAdministracionTalentoHumano = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmpleadoResponsable = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    idFormularioAnalisisOcupacional = table.Column<int>(nullable: false),
                    idRolPuesto = table.Column<int>(nullable: false),
                    SeAplicaraPolitica = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministracionTalentoHumano", x => x.IdAdministracionTalentoHumano);
                    table.ForeignKey(
                        name: "FK_AdministracionTalentoHumano_Empleado_EmpleadoResponsable",
                        column: x => x.EmpleadoResponsable,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdministracionTalentoHumano_FormularioAnalisisOcupacional_idFormularioAnalisisOcupacional",
                        column: x => x.idFormularioAnalisisOcupacional,
                        principalTable: "FormularioAnalisisOcupacional",
                        principalColumn: "IdFormularioAnalisisOcupacional",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdministracionTalentoHumano_RolPuesto_idRolPuesto",
                        column: x => x.idRolPuesto,
                        principalTable: "RolPuesto",
                        principalColumn: "IdRolPuesto",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ValidacionInmediatoSuperior",
                columns: table => new
                {
                    IdValidacionJefe = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: true),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IdFormularioAnalisisOcupacional = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidacionInmediatoSuperior", x => x.IdValidacionJefe);
                    table.ForeignKey(
                        name: "FK_ValidacionInmediatoSuperior_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ValidacionInmediatoSuperior_FormularioAnalisisOcupacional_IdFormularioAnalisisOcupacional",
                        column: x => x.IdFormularioAnalisisOcupacional,
                        principalTable: "FormularioAnalisisOcupacional",
                        principalColumn: "IdFormularioAnalisisOcupacional",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadosFormularioDevengacion",
                columns: table => new
                {
                    IdEmpleadosFormularioDevengacion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FormularioDevengacionId = table.Column<int>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadosFormularioDevengacion", x => x.IdEmpleadosFormularioDevengacion);
                    table.ForeignKey(
                        name: "FK_EmpleadosFormularioDevengacion_FormularioDevengacion_FormularioDevengacionId",
                        column: x => x.FormularioDevengacionId,
                        principalTable: "FormularioDevengacion",
                        principalColumn: "IdFormularioDevengacion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpleadosFormularioDevengacion_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialApoyo",
                columns: table => new
                {
                    IdMaterialApoyo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FormularioDevengacionId = table.Column<int>(nullable: false),
                    NombreDocumento = table.Column<string>(nullable: true),
                    Ubicacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialApoyo", x => x.IdMaterialApoyo);
                    table.ForeignKey(
                        name: "FK_MaterialApoyo_FormularioDevengacion_FormularioDevengacionId",
                        column: x => x.FormularioDevengacionId,
                        principalTable: "FormularioDevengacion",
                        principalColumn: "IdFormularioDevengacion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActividadesGestionCambio",
                columns: table => new
                {
                    IdActividadesGestionCambio = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    IdPlanGestionCambio = table.Column<int>(nullable: false),
                    Indicador = table.Column<int>(nullable: false),
                    Porciento = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActividadesGestionCambio", x => x.IdActividadesGestionCambio);
                    table.ForeignKey(
                        name: "FK_ActividadesGestionCambio_PlanGestionCambio_IdPlanGestionCambio",
                        column: x => x.IdPlanGestionCambio,
                        principalTable: "PlanGestionCambio",
                        principalColumn: "IdPlanGestionCambio",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetalleExamenInduccion",
                columns: table => new
                {
                    IdDetalleExamenInduccion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PreguntaId = table.Column<int>(nullable: true),
                    RealizaExamenInduccionId = table.Column<int>(nullable: false),
                    RespuestaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleExamenInduccion", x => x.IdDetalleExamenInduccion);
                    table.ForeignKey(
                        name: "FK_DetalleExamenInduccion_Pregunta_PreguntaId",
                        column: x => x.PreguntaId,
                        principalTable: "Pregunta",
                        principalColumn: "IdPregunta",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleExamenInduccion_RealizaExamenInduccion_RealizaExamenInduccionId",
                        column: x => x.RealizaExamenInduccionId,
                        principalTable: "RealizaExamenInduccion",
                        principalColumn: "IdRealizaExamenInduccion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleExamenInduccion_Respuesta_RespuestaId",
                        column: x => x.RespuestaId,
                        principalTable: "Respuesta",
                        principalColumn: "IdRespuesta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolPagoDetalle",
                columns: table => new
                {
                    IdRolPagoDetalle = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 100, nullable: false),
                    EsIngreso = table.Column<bool>(nullable: false),
                    IdRolPagos = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolPagoDetalle", x => x.IdRolPagoDetalle);
                    table.ForeignKey(
                        name: "FK_RolPagoDetalle_RolPagos_IdRolPagos",
                        column: x => x.IdRolPagos,
                        principalTable: "RolPagos",
                        principalColumn: "IdRolPagos",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AprobacionViatico",
                columns: table => new
                {
                    IdAprobacionViatico = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IdSolicitudViatico = table.Column<int>(nullable: false),
                    ValorADescontar = table.Column<decimal>(type: "decimal", nullable: false),
                    ValorJustificado = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AprobacionViatico", x => x.IdAprobacionViatico);
                    table.ForeignKey(
                        name: "FK_AprobacionViatico_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AprobacionViatico_SolicitudViatico_IdSolicitudViatico",
                        column: x => x.IdSolicitudViatico,
                        principalTable: "SolicitudViatico",
                        principalColumn: "IdSolicitudViatico",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItinerarioViatico",
                columns: table => new
                {
                    IdItinerarioViatico = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 500, nullable: false),
                    FechaDesde = table.Column<DateTime>(nullable: false),
                    FechaHasta = table.Column<DateTime>(nullable: false),
                    IdCiudad = table.Column<int>(nullable: false),
                    IdPais = table.Column<int>(nullable: false),
                    IdSolicitudViatico = table.Column<int>(nullable: false),
                    IdTipoTransporte = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItinerarioViatico", x => x.IdItinerarioViatico);
                    table.ForeignKey(
                        name: "FK_ItinerarioViatico_Ciudad_IdCiudad",
                        column: x => x.IdCiudad,
                        principalTable: "Ciudad",
                        principalColumn: "IdCiudad",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItinerarioViatico_Pais_IdPais",
                        column: x => x.IdPais,
                        principalTable: "Pais",
                        principalColumn: "IdPais",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItinerarioViatico_SolicitudViatico_IdSolicitudViatico",
                        column: x => x.IdSolicitudViatico,
                        principalTable: "SolicitudViatico",
                        principalColumn: "IdSolicitudViatico",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItinerarioViatico_TipoTransporte_IdTipoTransporte",
                        column: x => x.IdTipoTransporte,
                        principalTable: "TipoTransporte",
                        principalColumn: "IdTipoTransporte",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExperienciaLaboralRequerida",
                columns: table => new
                {
                    IdExperienciaLaboralRequerida = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdAnoExperiencia = table.Column<int>(nullable: false),
                    IdEspecificidadExperiencia = table.Column<int>(nullable: false),
                    IdEstudio = table.Column<int>(nullable: false),
                    IdIndiceOcupacionalCapacitaciones = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienciaLaboralRequerida", x => x.IdExperienciaLaboralRequerida);
                    table.ForeignKey(
                        name: "FK_ExperienciaLaboralRequerida_AnoExperiencia_IdAnoExperiencia",
                        column: x => x.IdAnoExperiencia,
                        principalTable: "AnoExperiencia",
                        principalColumn: "IdAnoExperiencia",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExperienciaLaboralRequerida_EspecificidadExperiencia_IdEspecificidadExperiencia",
                        column: x => x.IdEspecificidadExperiencia,
                        principalTable: "EspecificidadExperiencia",
                        principalColumn: "IdEspecificidadExperiencia",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExperienciaLaboralRequerida_Estudio_IdEstudio",
                        column: x => x.IdEstudio,
                        principalTable: "Estudio",
                        principalColumn: "IdEstudio",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExperienciaLaboralRequerida_IndiceOcupacionalCapacitaciones_IdIndiceOcupacionalCapacitaciones",
                        column: x => x.IdIndiceOcupacionalCapacitaciones,
                        principalTable: "IndiceOcupacionalCapacitaciones",
                        principalColumn: "IdIndiceOcupacionalCapacitaciones",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadoMovimiento",
                columns: table => new
                {
                    IdEmpleadoMovimiento = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaDesde = table.Column<DateTime>(nullable: false),
                    FechaHasta = table.Column<DateTime>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IdIndiceOcupacionalModalidadPartida = table.Column<int>(nullable: false),
                    IdTipoMovimientoInterno = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoMovimiento", x => x.IdEmpleadoMovimiento);
                    table.ForeignKey(
                        name: "FK_EmpleadoMovimiento_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpleadoMovimiento_IndiceOcupacionalModalidadPartida_IdIndiceOcupacionalModalidadPartida",
                        column: x => x.IdIndiceOcupacionalModalidadPartida,
                        principalTable: "IndiceOcupacionalModalidadPartida",
                        principalColumn: "IdIndiceOcupacionalModalidadPartida",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpleadoMovimiento_TipoMovimientoInterno_IdTipoMovimientoInterno",
                        column: x => x.IdTipoMovimientoInterno,
                        principalTable: "TipoMovimientoInterno",
                        principalColumn: "IdTipoMovimientoInterno",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartidasFase",
                columns: table => new
                {
                    IdPartidasFase = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ganador = table.Column<bool>(nullable: false),
                    IdFaseConcurso = table.Column<int>(nullable: false),
                    IdIndiceOcupacionalModalidadPartida = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartidasFase", x => x.IdPartidasFase);
                    table.ForeignKey(
                        name: "FK_PartidasFase_FaseConcurso_IdFaseConcurso",
                        column: x => x.IdFaseConcurso,
                        principalTable: "FaseConcurso",
                        principalColumn: "IdFaseConcurso",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartidasFase_IndiceOcupacionalModalidadPartida_IdIndiceOcupacionalModalidadPartida",
                        column: x => x.IdIndiceOcupacionalModalidadPartida,
                        principalTable: "IndiceOcupacionalModalidadPartida",
                        principalColumn: "IdIndiceOcupacionalModalidadPartida",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CapacitacionPregunta",
                columns: table => new
                {
                    IdCapacitacionPregunta = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 150, nullable: false),
                    IdCapacitacionEncuesta = table.Column<string>(maxLength: 450, nullable: true),
                    IdCapacitacionTipoPregunta = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapacitacionPregunta", x => x.IdCapacitacionPregunta);
                    table.ForeignKey(
                        name: "FK_CapacitacionPregunta_CapacitacionEncuesta_IdCapacitacionEncuesta",
                        column: x => x.IdCapacitacionEncuesta,
                        principalTable: "CapacitacionEncuesta",
                        principalColumn: "IdCapacitacionEncuesta",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CapacitacionPregunta_CapacitacionTipoPregunta_IdCapacitacionTipoPregunta",
                        column: x => x.IdCapacitacionTipoPregunta,
                        principalTable: "CapacitacionTipoPregunta",
                        principalColumn: "IdCapacitacionTipoPregunta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CapacitacionTemarioProveedor",
                columns: table => new
                {
                    IdCapacitacionTemarioProveedor = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Costo = table.Column<decimal>(type: "decimal", nullable: false),
                    IdCapacitacionModalidad = table.Column<int>(nullable: false),
                    IdCapacitacionProveedor = table.Column<int>(nullable: false),
                    IdCapacitacionTemario = table.Column<int>(nullable: false),
                    NumeroHoras = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapacitacionTemarioProveedor", x => x.IdCapacitacionTemarioProveedor);
                    table.ForeignKey(
                        name: "FK_CapacitacionTemarioProveedor_CapacitacionModalidad_IdCapacitacionModalidad",
                        column: x => x.IdCapacitacionModalidad,
                        principalTable: "CapacitacionModalidad",
                        principalColumn: "IdCapacitacionModalidad",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CapacitacionTemarioProveedor_CapacitacionProveedor_IdCapacitacionProveedor",
                        column: x => x.IdCapacitacionProveedor,
                        principalTable: "CapacitacionProveedor",
                        principalColumn: "IdCapacitacionProveedor",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CapacitacionTemarioProveedor_CapacitacionTemario_IdCapacitacionTemario",
                        column: x => x.IdCapacitacionTemario,
                        principalTable: "CapacitacionTemario",
                        principalColumn: "IdCapacitacionTemario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InformeUATH",
                columns: table => new
                {
                    IdInformeUATH = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdAdministracionTalentoHumano = table.Column<int>(nullable: false),
                    IdManualPuestoDestino = table.Column<int>(nullable: false),
                    IdManualPuestoOrigen = table.Column<int>(nullable: false),
                    Revizar = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformeUATH", x => x.IdInformeUATH);
                    table.ForeignKey(
                        name: "FK_InformeUATH_AdministracionTalentoHumano_IdAdministracionTalentoHumano",
                        column: x => x.IdAdministracionTalentoHumano,
                        principalTable: "AdministracionTalentoHumano",
                        principalColumn: "IdAdministracionTalentoHumano",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InformeUATH_ManualPuesto_IdManualPuestoDestino",
                        column: x => x.IdManualPuestoDestino,
                        principalTable: "ManualPuesto",
                        principalColumn: "IdManualPuesto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InformeUATH_ManualPuesto_IdManualPuestoOrigen",
                        column: x => x.IdManualPuestoOrigen,
                        principalTable: "ManualPuesto",
                        principalColumn: "IdManualPuesto",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequisitosNoCumple",
                columns: table => new
                {
                    IdRequisitosNoCumple = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdministracionTalentoHumanoId = table.Column<int>(nullable: false),
                    Detalle = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequisitosNoCumple", x => x.IdRequisitosNoCumple);
                    table.ForeignKey(
                        name: "FK_RequisitosNoCumple_AdministracionTalentoHumano_AdministracionTalentoHumanoId",
                        column: x => x.AdministracionTalentoHumanoId,
                        principalTable: "AdministracionTalentoHumano",
                        principalColumn: "IdAdministracionTalentoHumano",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exepciones",
                columns: table => new
                {
                    IdExepciones = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Detalle = table.Column<string>(maxLength: 500, nullable: false),
                    ValidacionJefeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exepciones", x => x.IdExepciones);
                    table.ForeignKey(
                        name: "FK_Exepciones_ValidacionInmediatoSuperior_ValidacionJefeId",
                        column: x => x.ValidacionJefeId,
                        principalTable: "ValidacionInmediatoSuperior",
                        principalColumn: "IdValidacionJefe",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AvanceGestionCambio",
                columns: table => new
                {
                    IdAvanceGestionCambio = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    IdActividadesGestionCambio = table.Column<int>(nullable: false),
                    Indicadorreal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvanceGestionCambio", x => x.IdAvanceGestionCambio);
                    table.ForeignKey(
                        name: "FK_AvanceGestionCambio_ActividadesGestionCambio_IdActividadesGestionCambio",
                        column: x => x.IdActividadesGestionCambio,
                        principalTable: "ActividadesGestionCambio",
                        principalColumn: "IdActividadesGestionCambio",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FacturaViatico",
                columns: table => new
                {
                    IdFacturaViatico = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AprobadoPor = table.Column<int>(nullable: false),
                    FechaAprobacion = table.Column<DateTime>(nullable: false),
                    FechaFactura = table.Column<DateTime>(nullable: false),
                    IdItemViatico = table.Column<int>(nullable: false),
                    ItinerarioViaticoId = table.Column<int>(nullable: false),
                    NumeroFactura = table.Column<string>(maxLength: 30, nullable: false),
                    Observaciones = table.Column<string>(nullable: false),
                    ValorTotalAprobacion = table.Column<decimal>(type: "decimal", nullable: false),
                    ValorTotalFactura = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturaViatico", x => x.IdFacturaViatico);
                    table.ForeignKey(
                        name: "FK_FacturaViatico_Empleado_AprobadoPor",
                        column: x => x.AprobadoPor,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FacturaViatico_ItemViatico_IdItemViatico",
                        column: x => x.IdItemViatico,
                        principalTable: "ItemViatico",
                        principalColumn: "IdItemViatico",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FacturaViatico_ItinerarioViatico_ItinerarioViaticoId",
                        column: x => x.ItinerarioViaticoId,
                        principalTable: "ItinerarioViatico",
                        principalColumn: "IdItinerarioViatico",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InformeViatico",
                columns: table => new
                {
                    IdInformeViatico = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 20, nullable: false),
                    IdItinerarioViatico = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformeViatico", x => x.IdInformeViatico);
                    table.ForeignKey(
                        name: "FK_InformeViatico_ItinerarioViatico_IdItinerarioViatico",
                        column: x => x.IdItinerarioViatico,
                        principalTable: "ItinerarioViatico",
                        principalColumn: "IdItinerarioViatico",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CandidatoConcurso",
                columns: table => new
                {
                    IdCandidatoConcurso = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CodigoSocioEmpleo = table.Column<string>(maxLength: 20, nullable: false),
                    Descartado = table.Column<bool>(nullable: false),
                    Ganador = table.Column<bool>(nullable: false),
                    IdCanditato = table.Column<int>(nullable: false),
                    IdPartidasFase = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidatoConcurso", x => x.IdCandidatoConcurso);
                    table.ForeignKey(
                        name: "FK_CandidatoConcurso_Canditato_IdCanditato",
                        column: x => x.IdCanditato,
                        principalTable: "Canditato",
                        principalColumn: "IdCanditato",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CandidatoConcurso_PartidasFase_IdPartidasFase",
                        column: x => x.IdPartidasFase,
                        principalTable: "PartidasFase",
                        principalColumn: "IdPartidasFase",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CapacitacionRespuesta",
                columns: table => new
                {
                    IdCapacitacionRespuesta = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 20, nullable: false),
                    EsCorrecta = table.Column<bool>(nullable: false),
                    IdCapacitacionPregunta = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapacitacionRespuesta", x => x.IdCapacitacionRespuesta);
                    table.ForeignKey(
                        name: "FK_CapacitacionRespuesta_CapacitacionPregunta_IdCapacitacionPregunta",
                        column: x => x.IdCapacitacionPregunta,
                        principalTable: "CapacitacionPregunta",
                        principalColumn: "IdCapacitacionPregunta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccionPersonal_IdEmpleado",
                table: "AccionPersonal",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_AccionPersonal_IdTipoAccionPersonal",
                table: "AccionPersonal",
                column: "IdTipoAccionPersonal");

            migrationBuilder.CreateIndex(
                name: "IX_ActividadesAnalisisOcupacional_IdFormularioAnalisisOcupacional",
                table: "ActividadesAnalisisOcupacional",
                column: "IdFormularioAnalisisOcupacional");

            migrationBuilder.CreateIndex(
                name: "IX_ActividadesGestionCambio_IdPlanGestionCambio",
                table: "ActividadesGestionCambio",
                column: "IdPlanGestionCambio");

            migrationBuilder.CreateIndex(
                name: "IX_AdministracionTalentoHumano_EmpleadoIdEmpleado",
                table: "AdministracionTalentoHumano",
                column: "EmpleadoResponsable");

            migrationBuilder.CreateIndex(
                name: "IX_AdministracionTalentoHumano_idFormularioAnalisisOcupacional",
                table: "AdministracionTalentoHumano",
                column: "idFormularioAnalisisOcupacional");

            migrationBuilder.CreateIndex(
                name: "IX_AdministracionTalentoHumano_idRolPuesto",
                table: "AdministracionTalentoHumano",
                column: "idRolPuesto");

            migrationBuilder.CreateIndex(
                name: "IX_AprobacionViatico_IdEmpleado",
                table: "AprobacionViatico",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_AprobacionViatico_IdSolicitudViatico",
                table: "AprobacionViatico",
                column: "IdSolicitudViatico");

            migrationBuilder.CreateIndex(
                name: "IX_AvanceGestionCambio_IdActividadesGestionCambio",
                table: "AvanceGestionCambio",
                column: "IdActividadesGestionCambio");

            migrationBuilder.CreateIndex(
                name: "IX_BrigadaSSORol_IdBrigadaSSO",
                table: "BrigadaSSORol",
                column: "IdBrigadaSSO");

            migrationBuilder.CreateIndex(
                name: "IX_CandidatoConcurso_IdCanditato",
                table: "CandidatoConcurso",
                column: "IdCanditato");

            migrationBuilder.CreateIndex(
                name: "IX_CandidatoConcurso_IdPartidasFase",
                table: "CandidatoConcurso",
                column: "IdPartidasFase");

            migrationBuilder.CreateIndex(
                name: "IX_CapacitacionEncuesta_IdCapacitacionRecibida",
                table: "CapacitacionEncuesta",
                column: "IdCapacitacionRecibida");

            migrationBuilder.CreateIndex(
                name: "IX_CapacitacionEncuesta_IdEmpleado",
                table: "CapacitacionEncuesta",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_CapacitacionPlanificacion_IdCapacitacionModalidad",
                table: "CapacitacionPlanificacion",
                column: "IdCapacitacionModalidad");

            migrationBuilder.CreateIndex(
                name: "IX_CapacitacionPlanificacion_IdCapacitacionTemario",
                table: "CapacitacionPlanificacion",
                column: "IdCapacitacionTemario");

            migrationBuilder.CreateIndex(
                name: "IX_CapacitacionPlanificacion_IdEmpleado",
                table: "CapacitacionPlanificacion",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_CapacitacionPregunta_IdCapacitacionEncuesta",
                table: "CapacitacionPregunta",
                column: "IdCapacitacionEncuesta");

            migrationBuilder.CreateIndex(
                name: "IX_CapacitacionPregunta_IdCapacitacionTipoPregunta",
                table: "CapacitacionPregunta",
                column: "IdCapacitacionTipoPregunta");

            migrationBuilder.CreateIndex(
                name: "IX_CapacitacionProveedor_IdCapacitacionRecibida",
                table: "CapacitacionProveedor",
                column: "IdCapacitacionRecibida");

            migrationBuilder.CreateIndex(
                name: "IX_CapacitacionRecibida_IdCapacitacionTemario",
                table: "CapacitacionRecibida",
                column: "IdCapacitacionTemario");

            migrationBuilder.CreateIndex(
                name: "IX_CapacitacionRecibida_IdEmpleado",
                table: "CapacitacionRecibida",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_CapacitacionRespuesta_IdCapacitacionPregunta",
                table: "CapacitacionRespuesta",
                column: "IdCapacitacionPregunta");

            migrationBuilder.CreateIndex(
                name: "IX_CapacitacionTemario_IdCapacitacionAreaConocimiento",
                table: "CapacitacionTemario",
                column: "IdCapacitacionAreaConocimiento");

            migrationBuilder.CreateIndex(
                name: "IX_CapacitacionTemarioProveedor_IdCapacitacionModalidad",
                table: "CapacitacionTemarioProveedor",
                column: "IdCapacitacionModalidad");

            migrationBuilder.CreateIndex(
                name: "IX_CapacitacionTemarioProveedor_IdCapacitacionProveedor",
                table: "CapacitacionTemarioProveedor",
                column: "IdCapacitacionProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_CapacitacionTemarioProveedor_IdCapacitacionTemario",
                table: "CapacitacionTemarioProveedor",
                column: "IdCapacitacionTemario");

            migrationBuilder.CreateIndex(
                name: "IX_Ciudad_IdProvincia",
                table: "Ciudad",
                column: "IdProvincia");

            migrationBuilder.CreateIndex(
                name: "IX_ComportamientoObservable_DenominacionCompetenciaId",
                table: "ComportamientoObservable",
                column: "DenominacionCompetenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComportamientoObservable_NivelId",
                table: "ComportamientoObservable",
                column: "NivelId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracionViatico_IdDependencia",
                table: "ConfiguracionViatico",
                column: "IdDependencia");

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmacionLectura_IdEmpleado",
                table: "ConfirmacionLectura",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_DatosBancarios_IdEmpleado",
                table: "DatosBancarios",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_DatosBancarios_IdInstitucionFinanciera",
                table: "DatosBancarios",
                column: "IdInstitucionFinanciera");

            migrationBuilder.CreateIndex(
                name: "IX_DeclaracionPatrimonioPersonal_IdEmpleado",
                table: "DeclaracionPatrimonioPersonal",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Dependencia_DependenciaPadreIdDependencia",
                table: "Dependencia",
                column: "IdDependenciaPadre");

            migrationBuilder.CreateIndex(
                name: "IX_Dependencia_IdSucursal",
                table: "Dependencia",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_DependenciaDocumento_IdDependencia",
                table: "DependenciaDocumento",
                column: "IdDependencia");

            migrationBuilder.CreateIndex(
                name: "IX_DependenciaDocumento_IdDocumentosSubidos",
                table: "DependenciaDocumento",
                column: "IdDocumentosSubidos");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleExamenInduccion_PreguntaId",
                table: "DetalleExamenInduccion",
                column: "PreguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleExamenInduccion_RealizaExamenInduccionId",
                table: "DetalleExamenInduccion",
                column: "RealizaExamenInduccionId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleExamenInduccion_RespuestaId",
                table: "DetalleExamenInduccion",
                column: "RespuestaId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosParentescodos_IdEmpleado",
                table: "DocumentosParentescodos",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_CiudadNacimientoIdCiudad",
                table: "Empleado",
                column: "IdCiudadLugarNacimiento");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_IdDependencia",
                table: "Empleado",
                column: "IdDependencia");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_IdPersona",
                table: "Empleado",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_ProvinciaSufragioIdProvincia",
                table: "Empleado",
                column: "IdProvinciaLugarSufragio");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoContactoEmergencia_IdEmpleado",
                table: "EmpleadoContactoEmergencia",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoContactoEmergencia_IdParentesco",
                table: "EmpleadoContactoEmergencia",
                column: "IdParentesco");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoContactoEmergencia_IdPersona",
                table: "EmpleadoContactoEmergencia",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoFamiliar_IdEmpleado",
                table: "EmpleadoFamiliar",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoFamiliar_IdParentesco",
                table: "EmpleadoFamiliar",
                column: "IdParentesco");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoFamiliar_IdPersona",
                table: "EmpleadoFamiliar",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoFormularioCapacitacion_IdFormularioCapacitacion",
                table: "EmpleadoFormularioCapacitacion",
                column: "IdFormularioCapacitacion");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoFormularioCapacitacion_ServidorIdEmpleado",
                table: "EmpleadoFormularioCapacitacion",
                column: "IdServidor");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoIE_IdEmpleado",
                table: "EmpleadoIE",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoIE_IdIngresoEgresoRMU",
                table: "EmpleadoIE",
                column: "IdIngresoEgresoRMU");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoImpuestoRenta_IdEmpleado",
                table: "EmpleadoImpuestoRenta",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoMovimiento_IdEmpleado",
                table: "EmpleadoMovimiento",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoMovimiento_IdIndiceOcupacionalModalidadPartida",
                table: "EmpleadoMovimiento",
                column: "IdIndiceOcupacionalModalidadPartida");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoMovimiento_IdTipoMovimientoInterno",
                table: "EmpleadoMovimiento",
                column: "IdTipoMovimientoInterno");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoNepotismo_IdEmpleado",
                table: "EmpleadoNepotismo",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoSaldoVacaciones_IdEmpleado",
                table: "EmpleadoSaldoVacaciones",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadosFormularioDevengacion_FormularioDevengacionId",
                table: "EmpleadosFormularioDevengacion",
                column: "FormularioDevengacionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadosFormularioDevengacion_IdEmpleado",
                table: "EmpleadosFormularioDevengacion",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_EscalaGrados_IdGrupoOcupacional",
                table: "EscalaGrados",
                column: "IdGrupoOcupacional");

            migrationBuilder.CreateIndex(
                name: "IX_Eval001_EmpleadoIdEmpleado",
                table: "Eval001",
                column: "IdEmpleadoEvaluado");

            migrationBuilder.CreateIndex(
                name: "IX_Eval001_IdEscalaEvaluacionTotal",
                table: "Eval001",
                column: "IdEscalaEvaluacionTotal");

            migrationBuilder.CreateIndex(
                name: "IX_Eval001_IdEvaluacionActividadesPuestoTrabajo",
                table: "Eval001",
                column: "IdEvaluacionActividadesPuestoTrabajo");

            migrationBuilder.CreateIndex(
                name: "IX_Eval001_IdEvaluacionCompetenciasTecnicasPuesto",
                table: "Eval001",
                column: "IdEvaluacionCompetenciasTecnicasPuesto");

            migrationBuilder.CreateIndex(
                name: "IX_Eval001_IdEvaluacionCompetenciasUniversales",
                table: "Eval001",
                column: "IdEvaluacionCompetenciasUniversales");

            migrationBuilder.CreateIndex(
                name: "IX_Eval001_IdEvaluacionConocimiento",
                table: "Eval001",
                column: "IdEvaluacionConocimiento");

            migrationBuilder.CreateIndex(
                name: "IX_Eval001_IdEvaluacionTrabajoEquipoIniciativaLiderazgo",
                table: "Eval001",
                column: "IdEvaluacionTrabajoEquipoIniciativaLiderazgo");

            migrationBuilder.CreateIndex(
                name: "IX_Eval001_IdEvaluador",
                table: "Eval001",
                column: "IdEvaluador");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionActividadesPuestoTrabajoDetalle_IdEvaluacionActividadesPuestoTrabajo",
                table: "EvaluacionActividadesPuestoTrabajoDetalle",
                column: "IdEvaluacionActividadesPuestoTrabajo");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionActividadesPuestoTrabajoDetalle_IdIndicador",
                table: "EvaluacionActividadesPuestoTrabajoDetalle",
                column: "IdIndicador");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionActividadesPuestoTrabajoFactor_IdEvaluacionActividadesPuestoTrabajo",
                table: "EvaluacionActividadesPuestoTrabajoFactor",
                column: "IdEvaluacionActividadesPuestoTrabajo");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionActividadesPuestoTrabajoFactor_IdFactor",
                table: "EvaluacionActividadesPuestoTrabajoFactor",
                column: "IdFactor");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionCompetenciasTecnicasPuestoDetalle_IdDestreza",
                table: "EvaluacionCompetenciasTecnicasPuestoDetalle",
                column: "IdDestreza");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionCompetenciasTecnicasPuestoDetalle_IdEvaluacionCompetenciasTecnicasPuesto",
                table: "EvaluacionCompetenciasTecnicasPuestoDetalle",
                column: "IdEvaluacionCompetenciasTecnicasPuesto");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionCompetenciasTecnicasPuestoDetalle_IdNivelDesarrollo",
                table: "EvaluacionCompetenciasTecnicasPuestoDetalle",
                column: "IdNivelDesarrollo");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionCompetenciasTecnicasPuestoDetalle_IdRelevancia",
                table: "EvaluacionCompetenciasTecnicasPuestoDetalle",
                column: "IdRelevancia");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionCompetenciasTecnicasPuestoFactor_IdEvaluacionCompetenciasTecnicasPuesto",
                table: "EvaluacionCompetenciasTecnicasPuestoFactor",
                column: "IdEvaluacionCompetenciasTecnicasPuesto");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionCompetenciasTecnicasPuestoFactor_IdFactor",
                table: "EvaluacionCompetenciasTecnicasPuestoFactor",
                column: "IdFactor");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionCompetenciasUniversalesDetalle_IdDestreza",
                table: "EvaluacionCompetenciasUniversalesDetalle",
                column: "IdDestreza");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionCompetenciasUniversalesDetalle_IdEvaluacionCompetenciasUniversales",
                table: "EvaluacionCompetenciasUniversalesDetalle",
                column: "IdEvaluacionCompetenciasUniversales");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionCompetenciasUniversalesDetalle_IdFrecuenciaAplicacion",
                table: "EvaluacionCompetenciasUniversalesDetalle",
                column: "IdFrecuenciaAplicacion");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionCompetenciasUniversalesDetalle_IdRelevancia",
                table: "EvaluacionCompetenciasUniversalesDetalle",
                column: "IdRelevancia");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionCompetenciasUniversalesFactor_IdEvaluacionCompetenciasUniversales",
                table: "EvaluacionCompetenciasUniversalesFactor",
                column: "IdEvaluacionCompetenciasUniversales");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionCompetenciasUniversalesFactor_IdFactor",
                table: "EvaluacionCompetenciasUniversalesFactor",
                column: "IdFactor");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionConocimientoDetalle_IdEvaluacionConocimiento",
                table: "EvaluacionConocimientoDetalle",
                column: "IdEvaluacionConocimiento");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionConocimientoDetalle_IdNivelConocimiento",
                table: "EvaluacionConocimientoDetalle",
                column: "IdNivelConocimiento");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionConocimientoFactor_IdEvaluacionConocimiento",
                table: "EvaluacionConocimientoFactor",
                column: "IdEvaluacionConocimiento");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionConocimientoFactor_IdFactor",
                table: "EvaluacionConocimientoFactor",
                column: "IdFactor");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle_IdEvaluacionTrabajoEquipoIniciativaLiderazgo",
                table: "EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle",
                column: "IdEvaluacionTrabajoEquipoIniciativaLiderazgo");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle_IdFrecuenciaAplicacion",
                table: "EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle",
                column: "IdFrecuenciaAplicacion");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle_IdRelevancia",
                table: "EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle",
                column: "IdRelevancia");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle_IdTrabajoEquipoIniciativaLiderazgo",
                table: "EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle",
                column: "IdTrabajoEquipoIniciativaLiderazgo");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionTrabajoEquipoIniciativaLiderazgoFactor_IdEvaluacionTrabajoEquipoIniciativaLiderazgo",
                table: "EvaluacionTrabajoEquipoIniciativaLiderazgoFactor",
                column: "IdEvaluacionTrabajoEquipoIniciativaLiderazgo");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionTrabajoEquipoIniciativaLiderazgoFactor_IdFactor",
                table: "EvaluacionTrabajoEquipoIniciativaLiderazgoFactor",
                column: "IdFactor");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluador_IdDependencia",
                table: "Evaluador",
                column: "IdDependencia");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluador_IdEmpleado",
                table: "Evaluador",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Exepciones_ValidacionInmediatoSuperiorIdValidacionJefe",
                table: "Exepciones",
                column: "ValidacionJefeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienciaLaboralRequerida_IdAnoExperiencia",
                table: "ExperienciaLaboralRequerida",
                column: "IdAnoExperiencia");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienciaLaboralRequerida_IdEspecificidadExperiencia",
                table: "ExperienciaLaboralRequerida",
                column: "IdEspecificidadExperiencia");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienciaLaboralRequerida_IdEstudio",
                table: "ExperienciaLaboralRequerida",
                column: "IdEstudio");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienciaLaboralRequerida_IdIndiceOcupacionalCapacitaciones",
                table: "ExperienciaLaboralRequerida",
                column: "IdIndiceOcupacionalCapacitaciones");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaViatico_EmpleadoIdEmpleado",
                table: "FacturaViatico",
                column: "AprobadoPor");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaViatico_IdItemViatico",
                table: "FacturaViatico",
                column: "IdItemViatico");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaViatico_ItinerarioViaticoId",
                table: "FacturaViatico",
                column: "ItinerarioViaticoId");

            migrationBuilder.CreateIndex(
                name: "IX_FaseConcurso_IdTipoConcurso",
                table: "FaseConcurso",
                column: "IdTipoConcurso");

            migrationBuilder.CreateIndex(
                name: "IX_FormularioAnalisisOcupacional_IdEmpleado",
                table: "FormularioAnalisisOcupacional",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_FormularioDevengacion_IdEmpleadoDesarrolloInstitucional",
                table: "FormularioDevengacion",
                column: "AnalistaDesarrolloInstitucional");

            migrationBuilder.CreateIndex(
                name: "IX_FormularioDevengacion_IdEmpleado",
                table: "FormularioDevengacion",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_FormularioDevengacion_ModosScializacionId",
                table: "FormularioDevengacion",
                column: "ModosScializacionId");

            migrationBuilder.CreateIndex(
                name: "IX_FormularioDevengacion_IdEmpleadoResponsableArea",
                table: "FormularioDevengacion",
                column: "ResponsableArea");

            migrationBuilder.CreateIndex(
                name: "IX_GastoRubro_IdEmpleadoImpuestoRenta",
                table: "GastoRubro",
                column: "IdEmpleadoImpuestoRenta");

            migrationBuilder.CreateIndex(
                name: "IX_GastoRubro_IdRubro",
                table: "GastoRubro",
                column: "IdRubro");

            migrationBuilder.CreateIndex(
                name: "IX_IndiceOcupacional_IdDependencia",
                table: "IndiceOcupacional",
                column: "IdDependencia");

            migrationBuilder.CreateIndex(
                name: "IX_IndiceOcupacional_IdEscalaGrados",
                table: "IndiceOcupacional",
                column: "IdEscalaGrados");

            migrationBuilder.CreateIndex(
                name: "IX_IndiceOcupacional_IdManualPuesto",
                table: "IndiceOcupacional",
                column: "IdManualPuesto");

            migrationBuilder.CreateIndex(
                name: "IX_IndiceOcupacional_IdRolPuesto",
                table: "IndiceOcupacional",
                column: "IdRolPuesto");

            migrationBuilder.CreateIndex(
                name: "IX_IndiceOcupacionalActividadesEsenciales_IdActividadesEsenciales",
                table: "IndiceOcupacionalActividadesEsenciales",
                column: "IdActividadesEsenciales");

            migrationBuilder.CreateIndex(
                name: "IX_IndiceOcupacionalActividadesEsenciales_IdIndiceOcupacional",
                table: "IndiceOcupacionalActividadesEsenciales",
                column: "IdIndiceOcupacional");

            migrationBuilder.CreateIndex(
                name: "IX_IndiceOcupacionalAreaConocimiento_IdAreaConocimiento",
                table: "IndiceOcupacionalAreaConocimiento",
                column: "IdAreaConocimiento");

            migrationBuilder.CreateIndex(
                name: "IX_IndiceOcupacionalAreaConocimiento_IdIndiceOcupacional",
                table: "IndiceOcupacionalAreaConocimiento",
                column: "IdIndiceOcupacional");

            migrationBuilder.CreateIndex(
                name: "IX_IndiceOcupacionalCapacitaciones_IdCapacitacion",
                table: "IndiceOcupacionalCapacitaciones",
                column: "IdCapacitacion");

            migrationBuilder.CreateIndex(
                name: "IX_IndiceOcupacionalCapacitaciones_IdIndiceOcupacional",
                table: "IndiceOcupacionalCapacitaciones",
                column: "IdIndiceOcupacional");

            migrationBuilder.CreateIndex(
                name: "IX_IndiceOcupacionalComportamientoObservable_ComportamientoObservableId",
                table: "IndiceOcupacionalComportamientoObservable",
                column: "IdComportamientoObservable");

            migrationBuilder.CreateIndex(
                name: "IX_IndiceOcupacionalComportamientoObservable_IdIndiceOcupacional",
                table: "IndiceOcupacionalComportamientoObservable",
                column: "IdIndiceOcupacional");

            migrationBuilder.CreateIndex(
                name: "IX_IndiceOcupacionalConocimientosAdicionales_IdConocimientosAdicionales",
                table: "IndiceOcupacionalConocimientosAdicionales",
                column: "IdConocimientosAdicionales");

            migrationBuilder.CreateIndex(
                name: "IX_IndiceOcupacionalConocimientosAdicionales_IdIndiceOcupacional",
                table: "IndiceOcupacionalConocimientosAdicionales",
                column: "IdIndiceOcupacional");

            migrationBuilder.CreateIndex(
                name: "IX_IndiceOcupacionalEstudio_IdEstudio",
                table: "IndiceOcupacionalEstudio",
                column: "IdEstudio");

            migrationBuilder.CreateIndex(
                name: "IX_IndiceOcupacionalEstudio_IdIndiceOcupacional",
                table: "IndiceOcupacionalEstudio",
                column: "IdIndiceOcupacional");

            migrationBuilder.CreateIndex(
                name: "IX_IndiceOcupacionalModalidadPartida_IdEmpleado",
                table: "IndiceOcupacionalModalidadPartida",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_IndiceOcupacionalModalidadPartida_IdFondoFinanciamiento",
                table: "IndiceOcupacionalModalidadPartida",
                column: "IdFondoFinanciamiento");

            migrationBuilder.CreateIndex(
                name: "IX_IndiceOcupacionalModalidadPartida_IdIndiceOcupacional",
                table: "IndiceOcupacionalModalidadPartida",
                column: "IdIndiceOcupacional");

            migrationBuilder.CreateIndex(
                name: "IX_IndiceOcupacionalModalidadPartida_IdModalidadPartida",
                table: "IndiceOcupacionalModalidadPartida",
                column: "IdModalidadPartida");

            migrationBuilder.CreateIndex(
                name: "IX_IndiceOcupacionalModalidadPartida_IdTipoNombramiento",
                table: "IndiceOcupacionalModalidadPartida",
                column: "IdTipoNombramiento");

            migrationBuilder.CreateIndex(
                name: "IX_InformeUATH_IdAdministracionTalentoHumano",
                table: "InformeUATH",
                column: "IdAdministracionTalentoHumano");

            migrationBuilder.CreateIndex(
                name: "IX_InformeUATH_IdManualPuestoDestino",
                table: "InformeUATH",
                column: "IdManualPuestoDestino");

            migrationBuilder.CreateIndex(
                name: "IX_InformeUATH_IdManualPuestoOrigen",
                table: "InformeUATH",
                column: "IdManualPuestoOrigen");

            migrationBuilder.CreateIndex(
                name: "IX_InformeViatico_IdItinerarioViatico",
                table: "InformeViatico",
                column: "IdItinerarioViatico");

            migrationBuilder.CreateIndex(
                name: "IX_IngresoEgresoRMU_FormulasRMUIdFormulaRMU",
                table: "IngresoEgresoRMU",
                column: "IdFormulaRMU");

            migrationBuilder.CreateIndex(
                name: "IX_ItinerarioViatico_IdCiudad",
                table: "ItinerarioViatico",
                column: "IdCiudad");

            migrationBuilder.CreateIndex(
                name: "IX_ItinerarioViatico_IdPais",
                table: "ItinerarioViatico",
                column: "IdPais");

            migrationBuilder.CreateIndex(
                name: "IX_ItinerarioViatico_IdSolicitudViatico",
                table: "ItinerarioViatico",
                column: "IdSolicitudViatico");

            migrationBuilder.CreateIndex(
                name: "IX_ItinerarioViatico_IdTipoTransporte",
                table: "ItinerarioViatico",
                column: "IdTipoTransporte");

            migrationBuilder.CreateIndex(
                name: "IX_Liquidacion_IdEmpleado",
                table: "Liquidacion",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Liquidacion_IdRubroLiquidacion",
                table: "Liquidacion",
                column: "IdRubroLiquidacion");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialApoyo_FormularioDevengacionId",
                table: "MaterialApoyo",
                column: "FormularioDevengacionId");

            migrationBuilder.CreateIndex(
                name: "IX_MisionIndiceOcupacional_idIndiceOcupacional",
                table: "MisionIndiceOcupacional",
                column: "idIndiceOcupacional");

            migrationBuilder.CreateIndex(
                name: "IX_MisionIndiceOcupacional_IdMision",
                table: "MisionIndiceOcupacional",
                column: "IdMision");

            migrationBuilder.CreateIndex(
                name: "IX_ModalidadPartida_IdRelacionLaboral",
                table: "ModalidadPartida",
                column: "IdRelacionLaboral");

            migrationBuilder.CreateIndex(
                name: "IX_NacionalidadIndigena_IdEtnia",
                table: "NacionalidadIndigena",
                column: "IdEtnia");

            migrationBuilder.CreateIndex(
                name: "IX_Parroquia_IdCiudad",
                table: "Parroquia",
                column: "IdCiudad");

            migrationBuilder.CreateIndex(
                name: "IX_PartidasFase_IdFaseConcurso",
                table: "PartidasFase",
                column: "IdFaseConcurso");

            migrationBuilder.CreateIndex(
                name: "IX_PartidasFase_IdIndiceOcupacionalModalidadPartida",
                table: "PartidasFase",
                column: "IdIndiceOcupacionalModalidadPartida");

            migrationBuilder.CreateIndex(
                name: "IX_Permiso_IdTipoPermiso",
                table: "Permiso",
                column: "IdTipoPermiso");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_IdCanditato",
                table: "Persona",
                column: "IdCanditato");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_IdEstadoCivil",
                table: "Persona",
                column: "IdEstadoCivil");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_IdEtnia",
                table: "Persona",
                column: "IdEtnia");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_IdGenero",
                table: "Persona",
                column: "IdGenero");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_IdNacionalidad",
                table: "Persona",
                column: "IdNacionalidad");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_IdSexo",
                table: "Persona",
                column: "IdSexo");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_IdTipoIdentificacion",
                table: "Persona",
                column: "IdTipoIdentificacion");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_IdTipoSangre",
                table: "Persona",
                column: "IdTipoSangre");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaCapacitacion_IdCapacitacion",
                table: "PersonaCapacitacion",
                column: "IdCapacitacion");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaCapacitacion_IdPersona",
                table: "PersonaCapacitacion",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaDiscapacidad_IdPersona",
                table: "PersonaDiscapacidad",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaDiscapacidad_IdTipoDiscapacidad",
                table: "PersonaDiscapacidad",
                column: "IdTipoDiscapacidad");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaEnfermedad_IdPersona",
                table: "PersonaEnfermedad",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaEnfermedad_IdTipoEnfermedad",
                table: "PersonaEnfermedad",
                column: "IdTipoEnfermedad");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaEstudio_IdPersona",
                table: "PersonaEstudio",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaEstudio_IdTitulo",
                table: "PersonaEstudio",
                column: "IdTitulo");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaPaquetesInformaticos_IdEmpleado",
                table: "PersonaPaquetesInformaticos",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaPaquetesInformaticos_IdPaquetesInformaticos",
                table: "PersonaPaquetesInformaticos",
                column: "IdPaquetesInformaticos");

            migrationBuilder.CreateIndex(
                name: "IX_PlanGestionCambio_EmpleadoIdAprobadoPor",
                table: "PlanGestionCambio",
                column: "AprobadoPor");

            migrationBuilder.CreateIndex(
                name: "IX_PlanGestionCambio_EmpleadoIdRealizadoPor",
                table: "PlanGestionCambio",
                column: "RealizadoPor");

            migrationBuilder.CreateIndex(
                name: "IX_PlanificacionHE_IdEmpleado",
                table: "PlanificacionHE",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Pregunta_IdEvaluacionInduccion",
                table: "Pregunta",
                column: "IdEvaluacionInduccion");

            migrationBuilder.CreateIndex(
                name: "IX_PreguntaRespuesta_IdPregunta",
                table: "PreguntaRespuesta",
                column: "IdPregunta");

            migrationBuilder.CreateIndex(
                name: "IX_PreguntaRespuesta_IdRespuesta",
                table: "PreguntaRespuesta",
                column: "IdRespuesta");

            migrationBuilder.CreateIndex(
                name: "IX_ProcesoDetalle_IdDependencia",
                table: "ProcesoDetalle",
                column: "IdDependencia");

            migrationBuilder.CreateIndex(
                name: "IX_ProcesoDetalle_IdProceso",
                table: "ProcesoDetalle",
                column: "IdProceso");

            migrationBuilder.CreateIndex(
                name: "IX_Provincia_IdPais",
                table: "Provincia",
                column: "IdPais");

            migrationBuilder.CreateIndex(
                name: "IX_Provisiones_IdEmpleado",
                table: "Provisiones",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Provisiones_IdTipoProvision",
                table: "Provisiones",
                column: "IdTipoProvision");

            migrationBuilder.CreateIndex(
                name: "IX_RealizaExamenInduccion_IdEmpleado",
                table: "RealizaExamenInduccion",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_RealizaExamenInduccion_IdEvaluacionInduccion",
                table: "RealizaExamenInduccion",
                column: "IdEvaluacionInduccion");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroEntradaSalida_IdEmpleado",
                table: "RegistroEntradaSalida",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_RelacionesInternasExternasIndiceOcupacional_IdIndiceOcupacional",
                table: "RelacionesInternasExternasIndiceOcupacional",
                column: "IdIndiceOcupacional");

            migrationBuilder.CreateIndex(
                name: "IX_RelacionesInternasExternasIndiceOcupacional_RelacionesInternasExternasId",
                table: "RelacionesInternasExternasIndiceOcupacional",
                column: "RelacionesInternasExternasId");

            migrationBuilder.CreateIndex(
                name: "IX_RelacionLaboral_IdRegimenLaboral",
                table: "RelacionLaboral",
                column: "IdRegimenLaboral");

            migrationBuilder.CreateIndex(
                name: "IX_RequisitosNoCumple_AdministracionTalentoHumanoId",
                table: "RequisitosNoCumple",
                column: "AdministracionTalentoHumanoId");

            migrationBuilder.CreateIndex(
                name: "IX_RMU_IdEmpeladoIE",
                table: "RMU",
                column: "IdEmpeladoIE");

            migrationBuilder.CreateIndex(
                name: "IX_RMU_IdEmpleado",
                table: "RMU",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_RMU_IdTipoRMU",
                table: "RMU",
                column: "IdTipoRMU");

            migrationBuilder.CreateIndex(
                name: "IX_RolPagoDetalle_IdRolPagos",
                table: "RolPagoDetalle",
                column: "IdRolPagos");

            migrationBuilder.CreateIndex(
                name: "IX_RolPagos_IdEmpleado",
                table: "RolPagos",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_SituacionPropuesta_IdDependencia",
                table: "SituacionPropuesta",
                column: "IdDependencia");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudAcumulacionDecimos_IdEmpleado",
                table: "SolicitudAcumulacionDecimos",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudAnticipo_IdEmpleado",
                table: "SolicitudAnticipo",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudAnticipo_IdEstado",
                table: "SolicitudAnticipo",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudCertificadoPersonal_EmpleadoSolicitanteIdEmpleadoDirigidoA",
                table: "SolicitudCertificadoPersonal",
                column: "IdEmpleadoDirigidoA");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudCertificadoPersonal_EmpleadoSolicitanteIdEmpleadoSolicitante",
                table: "SolicitudCertificadoPersonal",
                column: "IdEmpleadoSolicitante");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudCertificadoPersonal_IdEstado",
                table: "SolicitudCertificadoPersonal",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudCertificadoPersonal_IdTipoCertificado",
                table: "SolicitudCertificadoPersonal",
                column: "IdTipoCertificado");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudHorasExtras_IdEmpleado",
                table: "SolicitudHorasExtras",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudLiquidacionHaberes_IdEmpleado",
                table: "SolicitudLiquidacionHaberes",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudModificacionFichaEmpleado_IdEmpleado",
                table: "SolicitudModificacionFichaEmpleado",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudModificacionFichaEmpleado_IdEstado",
                table: "SolicitudModificacionFichaEmpleado",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudPermiso_IdEmpleado",
                table: "SolicitudPermiso",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudPermiso_IdEstado",
                table: "SolicitudPermiso",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudPermiso_IdPermiso",
                table: "SolicitudPermiso",
                column: "IdPermiso");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudPlanificacionVacaciones_IdEmpleado",
                table: "SolicitudPlanificacionVacaciones",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudPlanificacionVacaciones_IdEstado",
                table: "SolicitudPlanificacionVacaciones",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudVacaciones_IdEmpleado",
                table: "SolicitudVacaciones",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudVacaciones_IdEstado",
                table: "SolicitudVacaciones",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudViatico_IdConfiguracionViatico",
                table: "SolicitudViatico",
                column: "IdConfiguracionViatico");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudViatico_IdEmpleado",
                table: "SolicitudViatico",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudViatico_IdEstado",
                table: "SolicitudViatico",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudViatico_IdTipoViatico",
                table: "SolicitudViatico",
                column: "IdTipoViatico");

            migrationBuilder.CreateIndex(
                name: "IX_Sucursal_IdCiudad",
                table: "Sucursal",
                column: "IdCiudad");

            migrationBuilder.CreateIndex(
                name: "IX_TipoNombramiento_IdRelacionLaboral",
                table: "TipoNombramiento",
                column: "IdRelacionLaboral");

            migrationBuilder.CreateIndex(
                name: "IX_Titulo_IdEstudio",
                table: "Titulo",
                column: "IdEstudio");

            migrationBuilder.CreateIndex(
                name: "IX_TrayectoriaLaboral_IdPersona",
                table: "TrayectoriaLaboral",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_ValidacionInmediatoSuperior_IdEmpleado",
                table: "ValidacionInmediatoSuperior",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_ValidacionInmediatoSuperior_IdFormularioAnalisisOcupacional",
                table: "ValidacionInmediatoSuperior",
                column: "IdFormularioAnalisisOcupacional");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccionPersonal");

            migrationBuilder.DropTable(
                name: "ActividadesAnalisisOcupacional");

            migrationBuilder.DropTable(
                name: "AprobacionViatico");

            migrationBuilder.DropTable(
                name: "AvanceGestionCambio");

            migrationBuilder.DropTable(
                name: "BrigadaSSORol");

            migrationBuilder.DropTable(
                name: "CandidatoConcurso");

            migrationBuilder.DropTable(
                name: "CapacitacionPlanificacion");

            migrationBuilder.DropTable(
                name: "CapacitacionRespuesta");

            migrationBuilder.DropTable(
                name: "CapacitacionTemarioProveedor");

            migrationBuilder.DropTable(
                name: "ConfirmacionLectura");

            migrationBuilder.DropTable(
                name: "DatosBancarios");

            migrationBuilder.DropTable(
                name: "DeclaracionPatrimonioPersonal");

            migrationBuilder.DropTable(
                name: "DependenciaDocumento");

            migrationBuilder.DropTable(
                name: "DetalleExamenInduccion");

            migrationBuilder.DropTable(
                name: "EmpleadoContactoEmergencia");

            migrationBuilder.DropTable(
                name: "EmpleadoFamiliar");

            migrationBuilder.DropTable(
                name: "EmpleadoFormularioCapacitacion");

            migrationBuilder.DropTable(
                name: "EmpleadoMovimiento");

            migrationBuilder.DropTable(
                name: "EmpleadoNepotismo");

            migrationBuilder.DropTable(
                name: "EmpleadoSaldoVacaciones");

            migrationBuilder.DropTable(
                name: "EmpleadosFormularioDevengacion");

            migrationBuilder.DropTable(
                name: "Eval001");

            migrationBuilder.DropTable(
                name: "EvaluacionActividadesPuestoTrabajoDetalle");

            migrationBuilder.DropTable(
                name: "EvaluacionActividadesPuestoTrabajoFactor");

            migrationBuilder.DropTable(
                name: "EvaluacionCompetenciasTecnicasPuestoDetalle");

            migrationBuilder.DropTable(
                name: "EvaluacionCompetenciasTecnicasPuestoFactor");

            migrationBuilder.DropTable(
                name: "EvaluacionCompetenciasUniversalesDetalle");

            migrationBuilder.DropTable(
                name: "EvaluacionCompetenciasUniversalesFactor");

            migrationBuilder.DropTable(
                name: "EvaluacionConocimientoDetalle");

            migrationBuilder.DropTable(
                name: "EvaluacionConocimientoFactor");

            migrationBuilder.DropTable(
                name: "EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle");

            migrationBuilder.DropTable(
                name: "EvaluacionTrabajoEquipoIniciativaLiderazgoFactor");

            migrationBuilder.DropTable(
                name: "Exepciones");

            migrationBuilder.DropTable(
                name: "ExperienciaLaboralRequerida");

            migrationBuilder.DropTable(
                name: "FacturaViatico");

            migrationBuilder.DropTable(
                name: "GastoRubro");

            migrationBuilder.DropTable(
                name: "IndiceOcupacionalActividadesEsenciales");

            migrationBuilder.DropTable(
                name: "IndiceOcupacionalAreaConocimiento");

            migrationBuilder.DropTable(
                name: "IndiceOcupacionalComportamientoObservable");

            migrationBuilder.DropTable(
                name: "IndiceOcupacionalConocimientosAdicionales");

            migrationBuilder.DropTable(
                name: "IndiceOcupacionalEstudio");

            migrationBuilder.DropTable(
                name: "InformeUATH");

            migrationBuilder.DropTable(
                name: "InformeViatico");

            migrationBuilder.DropTable(
                name: "Liquidacion");

            migrationBuilder.DropTable(
                name: "MaterialApoyo");

            migrationBuilder.DropTable(
                name: "MisionIndiceOcupacional");

            migrationBuilder.DropTable(
                name: "NacionalidadIndigena");

            migrationBuilder.DropTable(
                name: "Parroquia");

            migrationBuilder.DropTable(
                name: "PersonaCapacitacion");

            migrationBuilder.DropTable(
                name: "PersonaDiscapacidad");

            migrationBuilder.DropTable(
                name: "PersonaEnfermedad");

            migrationBuilder.DropTable(
                name: "PersonaEstudio");

            migrationBuilder.DropTable(
                name: "PersonaPaquetesInformaticos");

            migrationBuilder.DropTable(
                name: "PlanificacionHE");

            migrationBuilder.DropTable(
                name: "PreguntaRespuesta");

            migrationBuilder.DropTable(
                name: "ProcesoDetalle");

            migrationBuilder.DropTable(
                name: "Provisiones");

            migrationBuilder.DropTable(
                name: "RegistroEntradaSalida");

            migrationBuilder.DropTable(
                name: "RelacionesInternasExternasIndiceOcupacional");

            migrationBuilder.DropTable(
                name: "RequisitosNoCumple");

            migrationBuilder.DropTable(
                name: "RMU");

            migrationBuilder.DropTable(
                name: "RolPagoDetalle");

            migrationBuilder.DropTable(
                name: "SituacionPropuesta");

            migrationBuilder.DropTable(
                name: "SolicitudAcumulacionDecimos");

            migrationBuilder.DropTable(
                name: "SolicitudAnticipo");

            migrationBuilder.DropTable(
                name: "SolicitudCertificadoPersonal");

            migrationBuilder.DropTable(
                name: "SolicitudHorasExtras");

            migrationBuilder.DropTable(
                name: "SolicitudLiquidacionHaberes");

            migrationBuilder.DropTable(
                name: "SolicitudModificacionFichaEmpleado");

            migrationBuilder.DropTable(
                name: "SolicitudPermiso");

            migrationBuilder.DropTable(
                name: "SolicitudPlanificacionVacaciones");

            migrationBuilder.DropTable(
                name: "SolicitudVacaciones");

            migrationBuilder.DropTable(
                name: "TrayectoriaLaboral");

            migrationBuilder.DropTable(
                name: "TipoAccionPersonal");

            migrationBuilder.DropTable(
                name: "ActividadesGestionCambio");

            migrationBuilder.DropTable(
                name: "BrigadaSSO");

            migrationBuilder.DropTable(
                name: "PartidasFase");

            migrationBuilder.DropTable(
                name: "CapacitacionPregunta");

            migrationBuilder.DropTable(
                name: "CapacitacionModalidad");

            migrationBuilder.DropTable(
                name: "CapacitacionProveedor");

            migrationBuilder.DropTable(
                name: "InstitucionFinanciera");

            migrationBuilder.DropTable(
                name: "DocumentosParentescodos");

            migrationBuilder.DropTable(
                name: "RealizaExamenInduccion");

            migrationBuilder.DropTable(
                name: "Parentesco");

            migrationBuilder.DropTable(
                name: "FormularioCapacitacion");

            migrationBuilder.DropTable(
                name: "TipoMovimientoInterno");

            migrationBuilder.DropTable(
                name: "EscalaEvaluacionTotal");

            migrationBuilder.DropTable(
                name: "Evaluador");

            migrationBuilder.DropTable(
                name: "Indicador");

            migrationBuilder.DropTable(
                name: "EvaluacionActividadesPuestoTrabajo");

            migrationBuilder.DropTable(
                name: "NivelDesarrollo");

            migrationBuilder.DropTable(
                name: "EvaluacionCompetenciasTecnicasPuesto");

            migrationBuilder.DropTable(
                name: "Destreza");

            migrationBuilder.DropTable(
                name: "EvaluacionCompetenciasUniversales");

            migrationBuilder.DropTable(
                name: "NivelConocimiento");

            migrationBuilder.DropTable(
                name: "EvaluacionConocimiento");

            migrationBuilder.DropTable(
                name: "FrecuenciaAplicacion");

            migrationBuilder.DropTable(
                name: "Relevancia");

            migrationBuilder.DropTable(
                name: "TrabajoEquipoIniciativaLiderazgo");

            migrationBuilder.DropTable(
                name: "EvaluacionTrabajoEquipoIniciativaLiderazgo");

            migrationBuilder.DropTable(
                name: "Factor");

            migrationBuilder.DropTable(
                name: "ValidacionInmediatoSuperior");

            migrationBuilder.DropTable(
                name: "AnoExperiencia");

            migrationBuilder.DropTable(
                name: "EspecificidadExperiencia");

            migrationBuilder.DropTable(
                name: "IndiceOcupacionalCapacitaciones");

            migrationBuilder.DropTable(
                name: "ItemViatico");

            migrationBuilder.DropTable(
                name: "EmpleadoImpuestoRenta");

            migrationBuilder.DropTable(
                name: "Rubro");

            migrationBuilder.DropTable(
                name: "ActividadesEsenciales");

            migrationBuilder.DropTable(
                name: "AreaConocimiento");

            migrationBuilder.DropTable(
                name: "ComportamientoObservable");

            migrationBuilder.DropTable(
                name: "ConocimientosAdicionales");

            migrationBuilder.DropTable(
                name: "ItinerarioViatico");

            migrationBuilder.DropTable(
                name: "RubroLiquidacion");

            migrationBuilder.DropTable(
                name: "FormularioDevengacion");

            migrationBuilder.DropTable(
                name: "Mision");

            migrationBuilder.DropTable(
                name: "TipoDiscapacidad");

            migrationBuilder.DropTable(
                name: "TipoEnfermedad");

            migrationBuilder.DropTable(
                name: "Titulo");

            migrationBuilder.DropTable(
                name: "PaquetesInformaticos");

            migrationBuilder.DropTable(
                name: "Pregunta");

            migrationBuilder.DropTable(
                name: "Respuesta");

            migrationBuilder.DropTable(
                name: "Proceso");

            migrationBuilder.DropTable(
                name: "TipoProvision");

            migrationBuilder.DropTable(
                name: "RelacionesInternasExternas");

            migrationBuilder.DropTable(
                name: "AdministracionTalentoHumano");

            migrationBuilder.DropTable(
                name: "EmpleadoIE");

            migrationBuilder.DropTable(
                name: "TipoRMU");

            migrationBuilder.DropTable(
                name: "RolPagos");

            migrationBuilder.DropTable(
                name: "TipoCertificado");

            migrationBuilder.DropTable(
                name: "Permiso");

            migrationBuilder.DropTable(
                name: "PlanGestionCambio");

            migrationBuilder.DropTable(
                name: "FaseConcurso");

            migrationBuilder.DropTable(
                name: "IndiceOcupacionalModalidadPartida");

            migrationBuilder.DropTable(
                name: "CapacitacionEncuesta");

            migrationBuilder.DropTable(
                name: "CapacitacionTipoPregunta");

            migrationBuilder.DropTable(
                name: "Capacitacion");

            migrationBuilder.DropTable(
                name: "DenominacionCompetencia");

            migrationBuilder.DropTable(
                name: "Nivel");

            migrationBuilder.DropTable(
                name: "SolicitudViatico");

            migrationBuilder.DropTable(
                name: "TipoTransporte");

            migrationBuilder.DropTable(
                name: "ModosScializacion");

            migrationBuilder.DropTable(
                name: "Estudio");

            migrationBuilder.DropTable(
                name: "EvaluacionInducion");

            migrationBuilder.DropTable(
                name: "FormularioAnalisisOcupacional");

            migrationBuilder.DropTable(
                name: "IngresoEgresoRMU");

            migrationBuilder.DropTable(
                name: "TipoPermiso");

            migrationBuilder.DropTable(
                name: "TipoConcurso");

            migrationBuilder.DropTable(
                name: "FondoFinanciamiento");

            migrationBuilder.DropTable(
                name: "IndiceOcupacional");

            migrationBuilder.DropTable(
                name: "ModalidadPartida");

            migrationBuilder.DropTable(
                name: "TipoNombramiento");

            migrationBuilder.DropTable(
                name: "CapacitacionRecibida");

            migrationBuilder.DropTable(
                name: "ConfiguracionViatico");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "TipoViatico");

            migrationBuilder.DropTable(
                name: "FormulasRMU");

            migrationBuilder.DropTable(
                name: "EscalaGrados");

            migrationBuilder.DropTable(
                name: "ManualPuesto");

            migrationBuilder.DropTable(
                name: "RolPuesto");

            migrationBuilder.DropTable(
                name: "RelacionLaboral");

            migrationBuilder.DropTable(
                name: "CapacitacionTemario");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "GrupoOcupacional");

            migrationBuilder.DropTable(
                name: "RegimenLaboral");

            migrationBuilder.DropTable(
                name: "CapacitacionAreaConocimiento");

            migrationBuilder.DropTable(
                name: "Dependencia");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "Sucursal");

            migrationBuilder.DropTable(
                name: "Canditato");

            migrationBuilder.DropTable(
                name: "EstadoCivil");

            migrationBuilder.DropTable(
                name: "Etnia");

            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropTable(
                name: "Nacionalidad");

            migrationBuilder.DropTable(
                name: "Sexo");

            migrationBuilder.DropTable(
                name: "TipoIdentificacion");

            migrationBuilder.DropTable(
                name: "TipoSangre");

            migrationBuilder.DropTable(
                name: "Ciudad");

            migrationBuilder.DropTable(
                name: "Provincia");

            migrationBuilder.DropTable(
                name: "Pais");
        }
    }
}
