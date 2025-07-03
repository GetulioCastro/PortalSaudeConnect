using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalSaudeConnect.Migrations
{
    /// <inheritdoc />
    public partial class CreateDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    IdPaciente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomePaciente = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelefoneContato = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EnderecoLogradouro = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    EnderecoNumero = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    EnderecoComplemento = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    EnderecoBairro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EnderecoCidade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EnderecoEstado = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    EnderecoCep = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.IdPaciente);
                });

            migrationBuilder.CreateTable(
                name: "Prontuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    DataEncaminhamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClinicaOrigem = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClinicaDestino = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MotivoEncaminhamento = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ExameSolicitado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ObservacoesEncaminhamento = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StatusEncaminhamento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HistoricoConsulta = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ProfissionalSolicitante = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CRMSolicitante = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prontuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prontuarios_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "IdPaciente",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Encaminhamentos",
                columns: table => new
                {
                    IdEncaminhamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProntuarioId = table.Column<int>(type: "int", nullable: false),
                    DataRecebimentoDestino = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusAtual = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ObservacoesInternasDestino = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ExameProcedimentoRealizado = table.Column<bool>(type: "bit", nullable: false),
                    DataRealizacaoExame = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LaudoParecerEnviado = table.Column<bool>(type: "bit", nullable: false),
                    DataEnvioLaudo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CaminhoLaudoDigital = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DataCriacaoRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encaminhamentos", x => x.IdEncaminhamento);
                    table.ForeignKey(
                        name: "FK_Encaminhamentos_Prontuarios_ProntuarioId",
                        column: x => x.ProntuarioId,
                        principalTable: "Prontuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Encaminhamentos_ProntuarioId",
                table: "Encaminhamentos",
                column: "ProntuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Prontuarios_PacienteId",
                table: "Prontuarios",
                column: "PacienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Encaminhamentos");

            migrationBuilder.DropTable(
                name: "Prontuarios");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
