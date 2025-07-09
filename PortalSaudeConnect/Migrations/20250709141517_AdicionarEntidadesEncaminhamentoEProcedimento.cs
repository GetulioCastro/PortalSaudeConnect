using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalSaudeConnect.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarEntidadesEncaminhamentoEProcedimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClinicaDestinoId",
                table: "Encaminhamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClinicaOrigemId",
                table: "Encaminhamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Encaminhamentos_ClinicaOrigemId",
                table: "Encaminhamentos",
                column: "ClinicaOrigemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Encaminhamentos_Clinicas_ClinicaOrigemId",
                table: "Encaminhamentos",
                column: "ClinicaOrigemId",
                principalTable: "Clinicas",
                principalColumn: "IdClinica",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Encaminhamentos_Clinicas_ClinicaOrigemId",
                table: "Encaminhamentos");

            migrationBuilder.DropIndex(
                name: "IX_Encaminhamentos_ClinicaOrigemId",
                table: "Encaminhamentos");

            migrationBuilder.DropColumn(
                name: "ClinicaDestinoId",
                table: "Encaminhamentos");

            migrationBuilder.DropColumn(
                name: "ClinicaOrigemId",
                table: "Encaminhamentos");
        }
    }
}
