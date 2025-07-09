//using Microsoft.EntityFrameworkCore;
//using PortalSaudeConnect.Models;

//namespace PortalSaudeConnect.Infrastructure.Data
//{
//    public class PortalContext : DbContext
//    {
//        public DbSet<PacienteModel> Pacientes { get; set; }
//        public DbSet<ProntuarioModel> Prontuarios { get; set; }
//        public DbSet<EncaminhamentoModel> Encaminhamentos { get; set; }
//        public DbSet<ProcedimentoModel> Procedimentos { get; set; }
//        public DbSet<ClinicaModel> Clinicas { get; set; }
//        public DbSet<UsuarioModel> Usuarios { get; set; }

//        public PortalContext(DbContextOptions<PortalContext> dbContextOptions): base(dbContextOptions)
//        {
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<ProntuarioModel>()
//                .HasOne(p => p.PacienteNome)
//                .WithMany(pn => pn.Prontuarios)
//                .HasForeignKey(p => p.PacienteId)
//                .OnDelete(DeleteBehavior.Restrict);

//            modelBuilder.Entity<ProntuarioModel>()
//                .HasMany(p => p.Encaminhamentos)
//                .WithOne(e => e.ProntuarioPaciente)
//                .HasForeignKey(e => e.ProntuarioId);

//            modelBuilder.Entity<EncaminhamentoModel>()
//                .HasOne(e => e.ProntuarioPaciente)
//                .WithMany(pp => pp.Encaminhamentos)
//                .HasForeignKey(e => e.ProntuarioId)
//                .OnDelete(DeleteBehavior.Restrict);

//            modelBuilder.Entity<EncaminhamentoModel>()
//                .HasMany(e => e.Procedimentos)
//                .WithOne(p => p.Encaminhamento)
//                .HasForeignKey(p => p.EncaminhamentoId);

//            modelBuilder.Entity<ProcedimentoModel>()
//                .HasOne(pm => pm.Encaminhamento)
//                .WithMany(e => e.Procedimentos)
//                .HasForeignKey(p => p.EncaminhamentoId)
//                .OnDelete(DeleteBehavior.Restrict);

//            modelBuilder.Entity<UsuarioModel>()
//                .Property(um => um.Senha)
//                .HasMaxLength(255);

//            modelBuilder.Entity<UsuarioModel>()
//                .HasOne(um => um.Clinica)
//                .WithMany(cm => cm.Usuarios)
//                .HasForeignKey(u => u.ClinicaId)
//                .OnDelete(DeleteBehavior.Restrict);

//            modelBuilder.Entity<ClinicaModel>()
//                .HasMany(cm => cm.Usuarios)
//                .WithOne(um => um.Clinica)
//                .HasForeignKey(u => u.ClinicaId)
//                .OnDelete(DeleteBehavior.Restrict);

//            modelBuilder.Entity<UsuarioModel>()
//                .Property(u => u.TipoAcessoPortal)
//                .HasColumnType("bit");


//            base.OnModelCreating(modelBuilder);
//        }
//    }
//}
using Microsoft.EntityFrameworkCore;
using PortalSaudeConnect.Models; // Certifique-se de que este using está correto

namespace PortalSaudeConnect.Infrastructure.Data // Certifique-se de que o namespace está correto
{
    public class PortalContext : DbContext
    {
        // Seus DbSets (coleções que mapeiam para as tabelas do banco de dados)
        public DbSet<PacienteModel> Pacientes { get; set; } = null!; // Adicionado PacienteModel
        public DbSet<ProntuarioModel> Prontuarios { get; set; } = null!;
        public DbSet<EncaminhamentoModel> Encaminhamentos { get; set; } = null!;
        public DbSet<ProcedimentoModel> Procedimentos { get; set; } = null!;
        public DbSet<ClinicaModel> Clinicas { get; set; } = null!;
        public DbSet<UsuarioModel> Usuarios { get; set; } = null!;

        public PortalContext(DbContextOptions<PortalContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacionamento entre Paciente e Prontuário (Um Paciente tem muitos Prontuários)
            modelBuilder.Entity<PacienteModel>()
                .HasMany(p => p.Prontuarios)
                .WithOne(pr => pr.Paciente) // 'Paciente' é a propriedade de navegação em ProntuarioModel
                .HasForeignKey(pr => pr.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento entre Clinica e Prontuário (Uma Clínica tem muitos Prontuários)
            modelBuilder.Entity<ClinicaModel>()
                .HasMany(c => c.Prontuarios)
                .WithOne(pr => pr.Clinica)
                .HasForeignKey(pr => pr.ClinicaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento entre Prontuário e Encaminhamento (Um Prontuário tem muitos Encaminhamentos)
            modelBuilder.Entity<ProntuarioModel>()
                .HasMany(p => p.Encaminhamentos)
                .WithOne(e => e.ProntuarioPaciente) // 'ProntuarioPaciente' é a propriedade de navegação em EncaminhamentoModel
                .HasForeignKey(e => e.ProntuarioId)
                .OnDelete(DeleteBehavior.Restrict); // Restringir exclusão de prontuário se tiver encaminhamentos

            // Relacionamento entre Encaminhamento e Procedimentos (Um Encaminhamento tem muitos Procedimentos)
            modelBuilder.Entity<EncaminhamentoModel>()
                .HasMany(e => e.Procedimentos)
                .WithOne(p => p.Encaminhamento) // 'Encaminhamento' é a propriedade de navegação em ProcedimentoModel
                .HasForeignKey(p => p.EncaminhamentoId)
                .OnDelete(DeleteBehavior.Cascade); // Excluir procedimentos se o encaminhamento for excluído

            // Relacionamento entre Encaminhamento e Clínica de Origem (Um Encaminhamento pertence a uma Clínica de Origem)
            modelBuilder.Entity<EncaminhamentoModel>()
                .HasOne(e => e.ClinicaOrigem)
                .WithMany() // Encaminhamento sabe sua clínica de origem, mas Clinica não tem uma coleção de "encaminhamentos enviados"
                .HasForeignKey(e => e.ClinicaOrigemId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento entre Encaminhamento e Clínica de Destino (Um Encaminhamento é para uma Clínica de Destino)
            modelBuilder.Entity<EncaminhamentoModel>()
                .HasOne(e => e.ClinicaDestino)
                .WithMany() // Encaminhamento sabe sua clínica de destino, mas Clinica não tem uma coleção de "encaminhamentos recebidos" por padrão aqui
                .HasForeignKey(e => e.ClinicaDestinoId)
                .OnDelete(DeleteBehavior.Restrict);
            // Relacionamento entre Usuário e Clínica (Um Usuário pertence a uma Clínica)
            // Relacionamento Clínica de Origem <-> Encaminhamento (Uma Clínica tem muitos Encaminhamentos ENVIADOS)
            modelBuilder.Entity<ClinicaModel>()
                .HasMany(c => c.EncaminhamentosEnviados) // Propriedade de navegação em ClinicaModel
                .WithOne(e => e.ClinicaOrigem)          // Propriedade de navegação em EncaminhamentoModel
                .HasForeignKey(e => e.ClinicaOrigemId)  // Chave estrangeira em EncaminhamentoModel
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Clínica de Destino <-> Encaminhamento (Uma Clínica tem muitos Encaminhamentos RECEBIDOS)
            modelBuilder.Entity<ClinicaModel>()
                .HasMany(c => c.EncaminhamentosRecebidos) // Propriedade de navegação em ClinicaModel
                .WithOne(e => e.ClinicaDestino)           // Propriedade de navegação em EncaminhamentoModel
                .HasForeignKey(e => e.ClinicaDestinoId)   // Chave estrangeira em EncaminhamentoModel
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento entre Usuário e Clínica (Uma Clínica tem muitos Usuários)
            modelBuilder.Entity<ClinicaModel>()
                .HasMany(cm => cm.Usuarios)
                .WithOne(um => um.Clinica)
                .HasForeignKey(u => u.ClinicaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuração da propriedade Senha do Usuário
            modelBuilder.Entity<UsuarioModel>()
                .Property(um => um.Senha)
                .HasMaxLength(255);

            // Configuração da propriedade TipoAcessoPortal do Usuário (se for bool, vira bit)
            modelBuilder.Entity<UsuarioModel>()
                .Property(u => u.TipoAcessoPortal)
                .HasColumnType("bit");

            // Garantir que a Base de dados seja criada/atualizada corretamente
            // modelBuilder.ApplyConfigurationsFromAssembly(typeof(PortalContext).Assembly);
        }
    }
}
