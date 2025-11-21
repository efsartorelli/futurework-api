using FutureWorkApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FutureWorkApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Professional> Professionals => Set<Professional>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<Career> Careers => Set<Career>();
    public DbSet<ProfessionalSkill> ProfessionalSkills => Set<ProfessionalSkill>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Many-to-many Professional <-> Skill (via ProfessionalSkill)
        modelBuilder.Entity<ProfessionalSkill>()
            .HasKey(ps => new { ps.ProfessionalId, ps.SkillId });

        modelBuilder.Entity<ProfessionalSkill>()
            .HasOne(ps => ps.Professional)
            .WithMany(p => p.ProfessionalSkills)
            .HasForeignKey(ps => ps.ProfessionalId);

        modelBuilder.Entity<ProfessionalSkill>()
            .HasOne(ps => ps.Skill)
            .WithMany(s => s.ProfessionalSkills)
            .HasForeignKey(ps => ps.SkillId);
    }
}
