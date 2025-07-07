using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalSaudeConnect.Models
{
    public class ClinicaModel
    {

        [Key]
        public int IdClinica { get; set; }

        [Required(ErrorMessage = "O nome da clínica é obrigatório.")]
        [StringLength(150, ErrorMessage = "O nome da clínica não pode exceder 150 caracteres.")]
        public string? NomeDaClinica { get; set; }

        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        [StringLength(18, MinimumLength = 14, ErrorMessage = "O CNPJ deve ter entre 14 e 18 caracteres.")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "O logradouro é obrigatório.")]
        [StringLength(100, ErrorMessage = "O logradouro não pode exceder 100 caracteres.")]
        public string? EnderecoLogradouro { get; set; }

        public int EnderecoNumero { get; set; }

        [StringLength(50, ErrorMessage = "O complemento não pode exceder 50 caracteres.")]
        public string? EnderecoComplemento { get; set; }

        [Required(ErrorMessage = "O bairro é obrigatório.")]
        [StringLength(50, ErrorMessage = "O bairro não pode exceder 50 caracteres.")]
        public string? EnderecoBairro { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [StringLength(50, ErrorMessage = "A cidade não pode exceder 50 caracteres.")]
        public string? EnderecoCidade { get; set; }

        [Required(ErrorMessage = "O estado (UF) é obrigatório.")]
        [StringLength(2, ErrorMessage = "O estado deve ter 2 caracteres.", MinimumLength = 2)]
        public string? EnderecoEstado { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [StringLength(9, ErrorMessage = "O CEP deve ter 9 caracteres.", MinimumLength = 8)]
        public string? EnderecoCep { get; set; }

        [Phone(ErrorMessage = "Formato de telefone inválido.")]
        [StringLength(20, ErrorMessage = "O telefone não pode exceder 20 caracteres.")]
        public string? TelefoneContato { get; set; }

        [Required(ErrorMessage = "O telefone de whatsapp é obrigatório.")]
        [Phone(ErrorMessage = "Formato de telefone inválido para WhatsApp.")]
        [StringLength(20, ErrorMessage = "O telefone WhatsApp não pode exceder 20 caracteres.")]
        public string? TelefoneWhatsapp { get; set; }

        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        [StringLength(100, ErrorMessage = "O e-mail não pode exceder 100 caracteres.")]
        public string? EmailContato { get; set; }

        [Required(ErrorMessage = "O tipo da clínica é obrigatório.")]
        public bool TipoClinica { get; set; }

        public int? UsuarioAdmId { get; set; }

        // --- Auditoria ---
        [Required]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public ICollection<UsuarioModel> Usuarios { get; set; } = new List<UsuarioModel>();
    }
}

