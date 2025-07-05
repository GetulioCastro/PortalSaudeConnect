using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalSaudeConnect.Models
{
    public class ProcedimentoModel
    {
        [Key]
        public int IdProcedimento { get; set; }

        [Required(ErrorMessage = "O código do encaminhamento é obrigatório para este procedimento.")]
        public int EncaminhamentoId { get; set; }

        [ForeignKey("EncaminhamentoId")]
        public EncaminhamentoModel? Encaminhamento { get; set; }

        [Required(ErrorMessage = "O código do procedimento/exame é obrigatório.")]
        [StringLength(8, ErrorMessage = "O código do procedimento/exame não pode exceder 8 caracteres.")]
        public string? CodigoProcedimento { get; set; }

        [Required(ErrorMessage = "A descrição do procedimento/exame é obrigatória.")]
        [StringLength(200, ErrorMessage = "A descrição do procedimento/exame não pode exceder 200 caracteres.")]
        public string? DescricaoProcedimento { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "As observações específicas do procedimento não podem exceder 500 caracteres.")]
        public string? ObservacoesEspecificas { get; set; }

        [Required(ErrorMessage = "O nome do médico solicitante é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do médico solicitante não pode exceder 100 caracteres.")]
        public string? NomeMedicoSolicitante { get; set; }

        [Required(ErrorMessage = "O registro do médico solicitante (CRM/outro) é obrigatório.")]
        [StringLength(15, ErrorMessage = "O registro do médico solicitante não pode exceder 15 caracteres.")]
        public string? RegistroMedicoSolicitante { get; set; } // Ex: CRM/UF 123456

        // --- Status do Procedimento Individual ---
        [Required(ErrorMessage = "O status do procedimento é obrigatório.")]
        [StringLength(50, ErrorMessage = "O status do procedimento não pode exceder 50 caracteres.")]
        // Exemplos: "Aguardando Realização", "Em Execução", "Realizado", "Cancelado"
        public string StatusProcedimento { get; set; } = "Aguardando Realização";

        // --- Campos de Conclusão Específica do Procedimento ---
        // Embora o EncaminhamentoModel tenha um status geral, aqui podemos ter detalhes se um encaminhamento
        // tiver múltiplos procedimentos, e alguns forem concluídos antes de outros.

        [Display(Name = "Resultado Disponível")]
        public bool ResultadoDisponivel { get; set; } = false; // Indica se o resultado deste procedimento está pronto

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DataResultadoDisponivel { get; set; } // Data em que o resultado ficou pronto

        // --- Auditoria ---
        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.Now; // Quando este registro de procedimento foi criado
    }
}
