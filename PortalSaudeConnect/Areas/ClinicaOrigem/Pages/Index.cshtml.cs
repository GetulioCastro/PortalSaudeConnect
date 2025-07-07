using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace PortalSaudeConnect.Areas.ClinicaOrigem.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["NomeUsuarioLogado"] = User.FindFirst(ClaimTypes.Name)?.Value;
            }
            else
            {
                ViewData["NomeUsuarioLogado"] = "Não Autenticado";
            }
        }
    }
}
