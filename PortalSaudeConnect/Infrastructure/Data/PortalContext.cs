using Microsoft.EntityFrameworkCore;
using PortalSaudeConnect.Models;

namespace PortalSaudeConnect.Infrastructure.Data
{
    public class PortalContext : DbContext
    {
        public DbSet<PacienteModel> Pacientes { get; set; }
        public DbSet<ProntuarioModel> Prontuarios { get; set; }
        public DbSet<EncaminhamentoModel> Encaminhamentos { get; set; }
        public DbSet<ProcedimentoModel> Procedimentos { get; set; }
        public DbSet<ClinicaModel> Clinicas { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }

        public PortalContext(DbContextOptions<PortalContext> dbContextOptions): base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProntuarioModel>()
                .HasOne(p => p.PacienteNome)
                .WithMany(pn => pn.Prontuarios)
                .HasForeignKey(p => p.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EncaminhamentoModel>()
                .HasOne(e => e.ProntuarioPaciente)
                .WithMany(pp => pp.Encaminhamentos)
                .HasForeignKey(e => e.ProntuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProcedimentoModel>()
                .HasOne(pm => pm.Encaminhamento)
                .WithMany(e => e.Procedimentos)
                .HasForeignKey(p => p.EncaminhamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UsuarioModel>()
                .Property(um => um.Senha)
                .HasMaxLength(255);

            modelBuilder.Entity<UsuarioModel>()
                .HasOne(um => um.Clinica)
                .WithMany(cm => cm.Usuarios)
                .HasForeignKey(u => u.ClinicaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClinicaModel>()
                .HasMany(cm => cm.Usuarios)
                .WithOne(um => um.Clinica)
                .HasForeignKey(u => u.ClinicaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UsuarioModel>()
                .Property(u => u.TipoAcessoPortal)
                .HasColumnType("bit");


            base.OnModelCreating(modelBuilder);
        }
    }
}
