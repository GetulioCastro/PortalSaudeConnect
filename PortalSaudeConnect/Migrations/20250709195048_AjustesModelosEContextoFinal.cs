using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalSaudeConnect.Migrations
{
    /// <inheritdoc />
    public partial class AjustesModelosEContextoFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Encaminhamentos_Clinicas_ClinicaOrigemId",
                table: "Encaminhamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedimentos_Encaminhamentos_EncaminhamentoId",
                table: "Procedimentos");

            migrationBuilder.DropColumn(
                name: "CRMSolicitante",
                table: "Prontuarios");

            migrationBuilder.DropColumn(
                name: "ClinicaDestino",
                table: "Prontuarios");

            migrationBuilder.DropColumn(
                name: "ClinicaOrigem",
                table: "Prontuarios");

            migrationBuilder.DropColumn(
                name: "DataEncaminhamento",
                table: "Prontuarios");

            migrationBuilder.DropColumn(
                name: "ExameSolicitado",
                table: "Prontuarios");

            migrationBuilder.DropColumn(
                name: "MotivoEncaminhamento",
                table: "Prontuarios");

            migrationBuilder.DropColumn(
                name: "ObservacoesEncaminhamento",
                table: "Prontuarios");

            migrationBuilder.DropColumn(
                name: "ProfissionalSolicitante",
                table: "Prontuarios");

            migrationBuilder.DropColumn(
                name: "StatusEncaminhamento",
                table: "Prontuarios");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "EnderecoBairro",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "EnderecoCep",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "EnderecoCidade",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "EnderecoComplemento",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "EnderecoEstado",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "EnderecoLogradouro",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "EnderecoNumero",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "TelefoneContato",
                table: "Pacientes");

            migrationBuilder.RenameColumn(
                name: "HistoricoConsulta",
                table: "Prontuarios",
                newName: "HistoricoClinicoResumido");

            migrationBuilder.RenameColumn(
                name: "NomePaciente",
                table: "Pacientes",
                newName: "NomeCompleto");

            migrationBuilder.AddColumn<int>(
                name: "ClinicaId",
                table: "Prontuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Sexo",
                table: "Pacientes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Pacientes",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldMaxLength: 14);

            migrationBuilder.AddColumn<string>(
                name: "TelefonePrincipal",
                table: "Pacientes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prontuarios_ClinicaId",
                table: "Prontuarios",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Encaminhamentos_ClinicaDestinoId",
                table: "Encaminhamentos",
                column: "ClinicaDestinoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Encaminhamentos_Clinicas_ClinicaDestinoId",
                table: "Encaminhamentos",
                column: "ClinicaDestinoId",
                principalTable: "Clinicas",
                principalColumn: "IdClinica",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Encaminhamentos_Clinicas_ClinicaOrigemId",
                table: "Encaminhamentos",
                column: "ClinicaOrigemId",
                principalTable: "Clinicas",
                principalColumn: "IdClinica",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedimentos_Encaminhamentos_EncaminhamentoId",
                table: "Procedimentos",
                column: "EncaminhamentoId",
                principalTable: "Encaminhamentos",
                principalColumn: "IdEncaminhamento",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prontuarios_Clinicas_ClinicaId",
                table: "Prontuarios",
                column: "ClinicaId",
                principalTable: "Clinicas",
                principalColumn: "IdClinica",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Encaminhamentos_Clinicas_ClinicaDestinoId",
                table: "Encaminhamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Encaminhamentos_Clinicas_ClinicaOrigemId",
                table: "Encaminhamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedimentos_Encaminhamentos_EncaminhamentoId",
                table: "Procedimentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Prontuarios_Clinicas_ClinicaId",
                table: "Prontuarios");

            migrationBuilder.DropIndex(
                name: "IX_Prontuarios_ClinicaId",
                table: "Prontuarios");

            migrationBuilder.DropIndex(
                name: "IX_Encaminhamentos_ClinicaDestinoId",
                table: "Encaminhamentos");

            migrationBuilder.DropColumn(
                name: "ClinicaId",
                table: "Prontuarios");

            migrationBuilder.DropColumn(
                name: "TelefonePrincipal",
                table: "Pacientes");

            migrationBuilder.RenameColumn(
                name: "HistoricoClinicoResumido",
                table: "Prontuarios",
                newName: "HistoricoConsulta");

            migrationBuilder.RenameColumn(
                name: "NomeCompleto",
                table: "Pacientes",
                newName: "NomePaciente");

            migrationBuilder.AddColumn<string>(
                name: "CRMSolicitante",
                table: "Prontuarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClinicaDestino",
                table: "Prontuarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClinicaOrigem",
                table: "Prontuarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataEncaminhamento",
                table: "Prontuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ExameSolicitado",
                table: "Prontuarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotivoEncaminhamento",
                table: "Prontuarios",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ObservacoesEncaminhamento",
                table: "Prontuarios",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfissionalSolicitante",
                table: "Prontuarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusEncaminhamento",
                table: "Prontuarios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Sexo",
                table: "Pacientes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Pacientes",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Pacientes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EnderecoBairro",
                table: "Pacientes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnderecoCep",
                table: "Pacientes",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnderecoCidade",
                table: "Pacientes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnderecoComplemento",
                table: "Pacientes",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnderecoEstado",
                table: "Pacientes",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnderecoLogradouro",
                table: "Pacientes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnderecoNumero",
                table: "Pacientes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelefoneContato",
                table: "Pacientes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Encaminhamentos_Clinicas_ClinicaOrigemId",
                table: "Encaminhamentos",
                column: "ClinicaOrigemId",
                principalTable: "Clinicas",
                principalColumn: "IdClinica",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedimentos_Encaminhamentos_EncaminhamentoId",
                table: "Procedimentos",
                column: "EncaminhamentoId",
                principalTable: "Encaminhamentos",
                principalColumn: "IdEncaminhamento",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
