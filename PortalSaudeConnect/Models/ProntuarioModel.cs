using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PortalSaudeConnect.Models
{
    public class ProntuarioModel
    {
    
        [Key]
        public int IdProntuario { get; set; }

        [Required(ErrorMessage = "O ID do paciente é obrigatório.")]
        public int PacienteId { get; set; }

        [ForeignKey("PacienteId")]
        public PacienteModel? PacienteNome { get; set; }

        [Required(ErrorMessage = "A data do encaminhamento é obrigatória.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataEncaminhamento { get; set; }

        [Required(ErrorMessage = "A clínica de origem é obrigatória.")]
        [StringLength(100, ErrorMessage = "O nome da clínica de origem não pode exceder 100 caracteres.")]
        public string? ClinicaOrigem { get; set; }

        [Required(ErrorMessage = "A clínica de destino é obrigatória.")]
        [StringLength(100, ErrorMessage = "O nome da clínica de destino não pode exceder 100 caracteres.")]
        public string? ClinicaDestino { get; set; }

        [Required(ErrorMessage = "O motivo do encaminhamento é obrigatório.")]
        [StringLength(500, ErrorMessage = "O motivo do encaminhamento não pode exceder 500 caracteres.")]
        public string? MotivoEncaminhamento { get; set; }

        [StringLength(100, ErrorMessage = "O exame solicitado não pode exceder 100 caracteres.")]
        public string? ExameSolicitado { get; set; }

        [StringLength(500, ErrorMessage = "As observações do encaminhamento não podem exceder 500 caracteres.")]
        public string? ObservacoesEncaminhamento { get; set; }

        [Required(ErrorMessage = "O status do encaminhamento é obrigatório.")]
        [StringLength(50, ErrorMessage = "O status não pode exceder 50 caracteres.")]
        public string StatusEncaminhamento { get; set; } = "Pendente"; // Ex: "Pendente", "Agendado", "Realizado", "Cancelado"

        // --- Informações adicionais do Prontuário (histórico) ---

        [DataType(DataType.MultilineText)]
        [StringLength(2000, ErrorMessage = "O histórico da consulta não pode exceder 2000 caracteres.")]
        public string? HistoricoConsulta { get; set; } // Descrição da consulta na clínica de origem

        [StringLength(100, ErrorMessage = "O nome do médico/profissional de origem não pode exceder 100 caracteres.")]
        public string? ProfissionalSolicitante { get; set; }

        [StringLength(20, ErrorMessage = "O CRM/Registro do profissional de origem não pode exceder 20 caracteres.")]
        public string? CRMSolicitante { get; set; }

        // --- Dados de Auditoria ---
        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public ICollection<EncaminhamentoModel> Encaminhamentos { get; set; } = new List<EncaminhamentoModel>();
    }
}
