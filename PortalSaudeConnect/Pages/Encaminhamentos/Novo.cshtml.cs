using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PortalSaudeConnect.Infrastructure.Data;
using PortalSaudeConnect.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims; // Necessário para ClaimsPrincipal e ClaimTypes

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
        [Required(ErrorMessage = "A clínica de destino é obrigatória.")]
        public int ClinicaDestinoId { get; set; } // Para selecionar a clínica de destino

        public SelectList ClinicasDestino { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Carregar apenas as clínicas de destino (TipoClinica = false / 0)
            var clinicas = await _context.Clinicas
                                         .Where(c => c.TipoClinica == false) // 'false' representa clínica de destino
                                         .OrderBy(c => c.NomeDaClinica)
                                         .ToListAsync();

            ClinicasDestino = new SelectList(clinicas, "IdClinica", "NomeDaClinica");

            // Configurar valores padrão para campos ocultos
            Encaminhamento.DataRecebimentoDestino = DateTime.Now;
            Encaminhamento.StatusAtual = "Recebido";
            Encaminhamento.ExameProcedimentoRealizado = false;
            Encaminhamento.LaudoParecerEnviado = false;
            Encaminhamento.DataCriacaoRegistro = DateTime.Now; // Garante que a data é definida no OnGet

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validações básicas do modelo
            if (!ModelState.IsValid)
            {
                // Se houver erros, recarrega a lista de clínicas de destino e retorna a página
                var clinicas = await _context.Clinicas
                                             .Where(c => c.TipoClinica == false)
                                             .OrderBy(c => c.NomeDaClinica)
                                             .ToListAsync();
                ClinicasDestino = new SelectList(clinicas, "IdClinica", "NomeDaClinica");
                return Page();
            }

            // Atribui a clínica de origem (o usuário logado) ao encaminhamento
            // Precisamos obter o IdClinica do usuário logado
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                ModelState.AddModelError(string.Empty, "Não foi possível identificar o usuário logado ou sua clínica de origem.");
                return Page();
            }

            var usuarioLogado = await _context.Usuarios
                                              .Include(u => u.Clinica)
                                              .FirstOrDefaultAsync(u => u.Email == userIdString); // Supondo que NameIdentifier seja o Email

            if (usuarioLogado == null || !usuarioLogado.TipoAcessoPortal) // Apenas clínicas de origem podem criar encaminhamentos
            {
                ModelState.AddModelError(string.Empty, "Somente usuários de clínicas de origem podem criar encaminhamentos.");
                return Page();
            }

            // O encaminhamento DEVE ter a clínica de origem de quem o criou
            // Adicionar esta coluna em EncaminhamentoModel e rodar migração
            Encaminhamento.ClinicaOrigemId = usuarioLogado.ClinicaId; // <<-- REQUER NOVA COLUNA NA MODEL E MIGRAÇÃO!

            // Atribui a clínica de destino selecionada no formulário
            Encaminhamento.ClinicaDestinoId = ClinicaDestinoId; // <<-- REQUER NOVA COLUNA NA MODEL E MIGRAÇÃO!


            // Preenche campos de auditoria e status inicial (se não estiverem vindo do formulário)
            Encaminhamento.DataRecebimentoDestino = DateTime.Now; // Será atualizado para a data de recebimento real
            Encaminhamento.StatusAtual = "Recebido"; // Status inicial
            Encaminhamento.DataCriacaoRegistro = DateTime.Now;


            _context.Encaminhamentos.Add(Encaminhamento);

            // Adiciona os procedimentos vinculados ao encaminhamento (observações específicas = descrição do exame)
            foreach (var proc in Procedimentos)
            {
                if (!string.IsNullOrWhiteSpace(proc.ObservacoesEspecificas))
                {
                    Encaminhamento.Procedimentos.Add(proc);
                }
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Encaminhamento enviado com sucesso!";
            return RedirectToPage("/Encaminhamentos/ConfirmacaoEnvio"); // Página de confirmação
        }
    }
}
