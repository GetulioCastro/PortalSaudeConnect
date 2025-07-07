using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalSaudeConnect.Models
{
    public class UsuarioModel
    {

        [Key]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome completo não pode exceder 100 caracteres.")]
        public string? NomeCompleto { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        [StringLength(100, ErrorMessage = "O e-mail não pode exceder 100 caracteres.")]
        [Display(Name = "E-mail")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        [StringLength(70, MinimumLength = 6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        [Display(Name ="Senha")]
        public string? Senha { get; set; }

        [Required(ErrorMessage = "É necessário vincular o usuário a uma clínica.")]
        public int ClinicaId { get; set; }

        [ForeignKey("ClinicaId")]
        public ClinicaModel? Clinica { get; set; }

        [Required(ErrorMessage = "O tipo de acesso do usuário é obrigatório.")]
        [StringLength(20, ErrorMessage = "O tipo de acesso não pode exceder 20 caracteres.")]
        public bool TipoAcessoPortal { get; set; }

        // --- Auditoria ---
        [Required]
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
