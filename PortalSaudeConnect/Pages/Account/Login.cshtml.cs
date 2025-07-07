using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PortalSaudeConnect.Infrastructure.Data;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using BCrypt.Net;

namespace PortalSaudeConnect.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly PortalContext _context;

        public LoginModel(PortalContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "O e-mail é obrigatório.")]
            [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
            [StringLength(100, ErrorMessage = "O e-mail não pode exceder 100 caracteres.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "A senha é obrigatória.")]
            [DataType(DataType.Password)]
            [StringLength(20, MinimumLength = 6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Selecione o tipo de acesso.")]
            public bool? TipoAcessoPortal { get; set; }
        }

        public IActionResult OnGet()
        {
            Input = new InputModel();
            Input.TipoAcessoPortal = true;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var usuario = await _context.Usuarios
                                        .Include(u => u.Clinica)
                                        .FirstOrDefaultAsync(u => u.Email == Input.Email);

            if (usuario == null)
            {
                ViewData["ErrorMessage"] = "Credenciais inválidas.";
                return Page();
            }

            if (!BCrypt.Net.BCrypt.Verify(Input.Password, usuario.Senha))
            {
                ViewData["ErrorMessage"] = "Credenciais inválidas.";
                return Page();
            }

            if (usuario.TipoAcessoPortal != Input.TipoAcessoPortal)
            {
                string tipoCorreto = usuario.TipoAcessoPortal ? "Clínica de Origem" : "Clínica de Destino";
                ViewData["ErrorMessage"] = $"Acesso negado. Seu usuário pertence a uma {tipoCorreto}. Por favor, selecione a opção correta.";
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.NomeCompleto), 
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim("ClinicaId", usuario.ClinicaId.ToString()),
                new Claim("TipoAcessoPortal", usuario.TipoAcessoPortal ? "Origem" : "Destino")
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            if (usuario.TipoAcessoPortal)
            {
                return RedirectToPage("/Index", new { Area = "ClinicaOrigem" });
            }
            else
            {
                return RedirectToPage("/Index", new { Area = "ClinicaDestino" });
            }
        }
    }
}
