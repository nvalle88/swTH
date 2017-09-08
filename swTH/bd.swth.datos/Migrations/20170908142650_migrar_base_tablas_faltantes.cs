using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace bd.swth.datos.Migrations
{
    public partial class migrar_base_tablas_faltantes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdRespuesta",
                table: "Respuesta",
                newName: "RespuestaId");

            migrationBuilder.RenameColumn(
                name: "IdRelacionesInternasExternas",
                table: "RelacionesInternasExternas",
                newName: "RelacionesInternasExternasId");

            migrationBuilder.RenameColumn(
                name: "InternoOtroProceso",
                table: "FormularioAnalisisOcupacional",
                newName: "InternoOtroInstitucionFinanciera");

            migrationBuilder.RenameColumn(
                name: "InternoMismoProceso",
                table: "FormularioAnalisisOcupacional",
                newName: "InternoMismoInstitucionFinanciera");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Ciudad",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "ImpuestoRentaParametros",
                columns: table => new
                {
                    IdImpuestoRentaParametros = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExcesoHasta = table.Column<decimal>(nullable: false),
                    FraccionBasica = table.Column<decimal>(nullable: false),
                    ImpuestoFraccionBasica = table.Column<int>(nullable: false),
                    PorcentajeImpuestoFraccionExcedente = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImpuestoRentaParametros", x => x.IdImpuestoRentaParametros);
                });

            migrationBuilder.CreateTable(
                name: "InstruccionFormal",
                columns: table => new
                {
                    IdInstruccionFormal = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstruccionFormal", x => x.IdInstruccionFormal);
                });

            migrationBuilder.CreateTable(
                name: "ParametrosGenerales",
                columns: table => new
                {
                    IdParametrosGenerales = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Valor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametrosGenerales", x => x.IdParametrosGenerales);
                });

            migrationBuilder.CreateTable(
                name: "TipoDiscapacidadSustituto",
                columns: table => new
                {
                    IdTipoDiscapacidadSustituto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDiscapacidadSustituto", x => x.IdTipoDiscapacidadSustituto);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImpuestoRentaParametros");

            migrationBuilder.DropTable(
                name: "InstruccionFormal");

            migrationBuilder.DropTable(
                name: "ParametrosGenerales");

            migrationBuilder.DropTable(
                name: "TipoDiscapacidadSustituto");

            migrationBuilder.RenameColumn(
                name: "RespuestaId",
                table: "Respuesta",
                newName: "IdRespuesta");

            migrationBuilder.RenameColumn(
                name: "RelacionesInternasExternasId",
                table: "RelacionesInternasExternas",
                newName: "IdRelacionesInternasExternas");

            migrationBuilder.RenameColumn(
                name: "InternoOtroInstitucionFinanciera",
                table: "FormularioAnalisisOcupacional",
                newName: "InternoOtroProceso");

            migrationBuilder.RenameColumn(
                name: "InternoMismoInstitucionFinanciera",
                table: "FormularioAnalisisOcupacional",
                newName: "InternoMismoProceso");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Ciudad",
                newName: "Nombre");
        }
    }
}
