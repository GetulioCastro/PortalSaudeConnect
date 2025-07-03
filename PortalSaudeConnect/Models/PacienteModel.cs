using System.ComponentModel.DataAnnotations;

namespace PortalSaudeConnect.Models
{
    public class PacienteModel
    {
        [Key]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "O nome do paciente deve conter entre 3 e 150 caracteres.")]
        public string? NomePaciente { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "Digite o número do CPF sem caracteres especiais.")]
        public string Cpf { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O sexo é obrigatório.")]
        public string? Sexo { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Phone(ErrorMessage = "Formato de telefone inválido.")]
        [StringLength(20, ErrorMessage = "O paciente precisa ser contatado, insira um telefone de contato.")]
        public string? TelefoneContato { get; set; }

        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        [StringLength(100, ErrorMessage = "O e-mail não pode exceder 100 caracteres.")]
        public string? Email { get; set; }

        [StringLength(150, ErrorMessage = "O endereço não pode exceder 150 caracteres.")]
        public string? EnderecoLogradouro { get; set; }

        [StringLength(10, ErrorMessage = "O número não pode exceder 10 caracteres.")]
        public string? EnderecoNumero { get; set; }

        [StringLength(30, ErrorMessage = "O complemento não pode exceder 30 caracteres.")]
        public string? EnderecoComplemento { get; set; }

        [StringLength(50, ErrorMessage = "O bairro não pode exceder 50 caracteres.")]
        public string? EnderecoBairro { get; set; }

        [StringLength(50, ErrorMessage = "A cidade não pode exceder 50 caracteres.")]
        public string? EnderecoCidade { get; set; }

        [StringLength(2, ErrorMessage = "O estado deve ter 2 caracteres.")]
        public string? EnderecoEstado { get; set; }

        [StringLength(9, ErrorMessage = "O CEP deve ter 9 caracteres.")]
        public string? EnderecoCep { get; set; }

        [Required]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public ICollection<ProntuarioModel> Prontuarios { get; set; } = new List<ProntuarioModel>();
    }
}
