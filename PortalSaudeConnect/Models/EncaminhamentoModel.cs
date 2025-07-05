using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalSaudeConnect.Models
{
    public class EncaminhamentoModel
    {
        [Key]
        public int IdEncaminhamento { get; set; }

        [Required(ErrorMessage = "O ID do prontuário é obrigatório para o encaminhamento.")]
        public int ProntuarioId { get; set; }

        [ForeignKey("ProntuarioId")]
        public ProntuarioModel? ProntuarioPaciente { get; set; }

        // --- Dados do Processo na Clínica de Destino ---

        [Required(ErrorMessage = "A data de recebimento do encaminhamento pela clínica de destino é obrigatória.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataRecebimentoDestino { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "O status atual do encaminhamento é obrigatório.")]
        [StringLength(50, ErrorMessage = "O status não pode exceder 50 caracteres.")]
        // Exemplos: "Recebido", "Agendado", "Em Realização", "Realizado", "Laudo Emitido", "Finalizado", "Cancelado"
        public string StatusAtual { get; set; } = "Recebido";

        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "As observações internas não podem exceder 500 caracteres.")]
        public string? ObservacoesDaSolicitacao { get; set; } // Observações à cerca da solcitação, entenda o pedido.

        // --- Campos de Conclusão do Exame/Procedimento ---
        // Estes são os campos que você mencionou para o feedback da Clínica de Destino.

        [Display(Name = "Exame/Procedimento Realizado")]
        public bool ExameProcedimentoRealizado { get; set; } = false;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        // Nullable DateTime, pois só será preenchido quando o exame for realizado
        public DateTime? DataRealizacaoExame { get; set; }

        [Display(Name = "Laudo/Parecer Enviado")]
        public bool LaudoParecerEnviado { get; set; } = false;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        // Nullable DateTime, só será preenchido quando o laudo for enviado
        public DateTime? DataEnvioLaudo { get; set; }

        // Opcional: Caminho ou URL para o arquivo do laudo/diagnóstico
        [StringLength(255, ErrorMessage = "O caminho do arquivo não pode exceder 255 caracteres.")]
        public string? CaminhoLaudoDigital { get; set; }

        // --- Auditoria ---
        [Required]
        public DateTime DataCriacaoRegistro { get; set; } = DateTime.Now;
        public ICollection<ProcedimentoModel> Procedimentos { get; set; } = new List<ProcedimentoModel>();

    }
}

