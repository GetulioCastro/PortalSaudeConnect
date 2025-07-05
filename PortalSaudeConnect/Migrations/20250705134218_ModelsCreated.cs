using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalSaudeConnect.Migrations
{
    /// <inheritdoc />
    public partial class ModelsCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Prontuarios",
                newName: "IdProntuario");

            migrationBuilder.RenameColumn(
                name: "ObservacoesInternasDestino",
                table: "Encaminhamentos",
                newName: "ObservacoesDaSolicitacao");

            migrationBuilder.CreateTable(
                name: "Clinicas",
                columns: table => new
                {
                    IdClinica = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeDaClinica = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    EnderecoLogradouro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EnderecoNumero = table.Column<int>(type: "int", nullable: false),
                    EnderecoComplemento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EnderecoBairro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EnderecoCidade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EnderecoEstado = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    EnderecoCep = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    TelefoneContato = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TelefoneWhatsapp = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmailContato = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TipoClinica = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioAdmId = table.Column<int>(type: "int", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinicas", x => x.IdClinica);
                });

            migrationBuilder.CreateTable(
                name: "Procedimentos",
                columns: table => new
                {
                    IdProcedimento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EncaminhamentoId = table.Column<int>(type: "int", nullable: false),
                    CodigoProcedimento = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    DescricaoProcedimento = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ObservacoesEspecificas = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NomeMedicoSolicitante = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RegistroMedicoSolicitante = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    StatusProcedimento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ResultadoDisponivel = table.Column<bool>(type: "bit", nullable: false),
                    DataResultadoDisponivel = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedimentos", x => x.IdProcedimento);
                    table.ForeignKey(
                        name: "FK_Procedimentos_Encaminhamentos_EncaminhamentoId",
                        column: x => x.EncaminhamentoId,
                        principalTable: "Encaminhamentos",
                        principalColumn: "IdEncaminhamento",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SenhaHash = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ClinicaId = table.Column<int>(type: "int", nullable: false),
                    TipoAcessoPortal = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Clinicas_ClinicaId",
                        column: x => x.ClinicaId,
                        principalTable: "Clinicas",
                        principalColumn: "IdClinica",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Procedimentos_EncaminhamentoId",
                table: "Procedimentos",
                column: "EncaminhamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_ClinicaId",
                table: "Usuarios",
                column: "ClinicaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Procedimentos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Clinicas");

            migrationBuilder.RenameColumn(
                name: "IdProntuario",
                table: "Prontuarios",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ObservacoesDaSolicitacao",
                table: "Encaminhamentos",
                newName: "ObservacoesInternasDestino");
        }
    }
}
