using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PortalSaudeConnect.Infrastructure.Data;
using PortalSaudeConnect.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims; // Necess�rio para ClaimsPrincipal e ClaimTypes

namespace PortalSaudeConnect.Pages.Encaminhamentos
{
    [Authorize(Policy = "SomenteClinicaOrigem")]
    public class NovoModel : PageModel
    {
        private readonly PortalContext _context;

        public NovoModel(PortalContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EncaminhamentoModel Encaminhamento { get; set; } = new EncaminhamentoModel();

        [BindProperty]
        public List<ProcedimentoModel> Procedimentos { get; set; } = new List<ProcedimentoModel>();

        [BindProperty]
        [Required(ErrorMessage = "A cl�nica de destino � obrigat�ria.")]
        public int ClinicaDestinoId { get; set; } // Para selecionar a cl�nica de destino

        public SelectList ClinicasDestino { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Carregar apenas as cl�nicas de destino (TipoClinica = false / 0)
            var clinicas = await _context.Clinicas
                                         .Where(c => c.TipoClinica == false) // 'false' representa cl�nica de destino
                                         .OrderBy(c => c.NomeDaClinica)
                                         .ToListAsync();

            ClinicasDestino = new SelectList(clinicas, "IdClinica", "NomeDaClinica");

            // Configurar valores padr�o para campos ocultos
            Encaminhamento.DataRecebimentoDestino = DateTime.Now;
            Encaminhamento.StatusAtual = "Recebido";
            Encaminhamento.ExameProcedimentoRealizado = false;
            Encaminhamento.LaudoParecerEnviado = false;
            Encaminhamento.DataCriacaoRegistro = DateTime.Now; // Garante que a data � definida no OnGet

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Valida��es b�sicas do modelo
            if (!ModelState.IsValid)
            {
                // Se houver erros, recarrega a lista de cl�nicas de destino e retorna a p�gina
                var clinicas = await _context.Clinicas
                                             .Where(c => c.TipoClinica == false)
                                             .OrderBy(c => c.NomeDaClinica)
                                             .ToListAsync();
                ClinicasDestino = new SelectList(clinicas, "IdClinica", "NomeDaClinica");
                return Page();
            }

            // Atribui a cl�nica de origem (o usu�rio logado) ao encaminhamento
            // Precisamos obter o IdClinica do usu�rio logado
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                ModelState.AddModelError(string.Empty, "N�o foi poss�vel identificar o usu�rio logado ou sua cl�nica de origem.");
                return Page();
            }

            var usuarioLogado = await _context.Usuarios
                                              .Include(u => u.Clinica)
                                              .FirstOrDefaultAsync(u => u.Email == userIdString); // Supondo que NameIdentifier seja o Email

            if (usuarioLogado == null || !usuarioLogado.TipoAcessoPortal) // Apenas cl�nicas de origem podem criar encaminhamentos
            {
                ModelState.AddModelError(string.Empty, "Somente usu�rios de cl�nicas de origem podem criar encaminhamentos.");
                return Page();
            }

            // O encaminhamento DEVE ter a cl�nica de origem de quem o criou
            // Adicionar esta coluna em EncaminhamentoModel e rodar migra��o
            Encaminhamento.ClinicaOrigemId = usuarioLogado.ClinicaId; // <<-- REQUER NOVA COLUNA NA MODEL E MIGRA��O!

            // Atribui a cl�nica de destino selecionada no formul�rio
            Encaminhamento.ClinicaDestinoId = ClinicaDestinoId; // <<-- REQUER NOVA COLUNA NA MODEL E MIGRA��O!


            // Preenche campos de auditoria e status inicial (se n�o estiverem vindo do formul�rio)
            Encaminhamento.DataRecebimentoDestino = DateTime.Now; // Ser� atualizado para a data de recebimento real
            Encaminhamento.StatusAtual = "Recebido"; // Status inicial
            Encaminhamento.DataCriacaoRegistro = DateTime.Now;


            _context.Encaminhamentos.Add(Encaminhamento);

            // Adiciona os procedimentos vinculados ao encaminhamento (observa��es espec�ficas = descri��o do exame)
            foreach (var proc in Procedimentos)
            {
                if (!string.IsNullOrWhiteSpace(proc.ObservacoesEspecificas))
                {
                    Encaminhamento.Procedimentos.Add(proc);
                }
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Encaminhamento enviado com sucesso!";
            return RedirectToPage("/Encaminhamentos/ConfirmacaoEnvio"); // P�gina de confirma��o
        }
    }
}
