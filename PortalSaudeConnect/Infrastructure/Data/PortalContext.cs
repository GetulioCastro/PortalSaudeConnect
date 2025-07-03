using Microsoft.EntityFrameworkCore;
using PortalSaudeConnect.Models;

namespace PortalSaudeConnect.Infrastructure.Data
{
    public class PortalContext : DbContext
    {
        public DbSet<PacienteModel> Pacientes { get; set; }
        public DbSet<ProntuarioModel> Prontuarios { get; set; }
        public DbSet<EncaminhamentoModel> Encaminhamentos { get; set; }

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

            base.OnModelCreating(modelBuilder);
        }


    }
}
