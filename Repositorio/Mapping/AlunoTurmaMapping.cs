using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AlunoTurmaMapping : IEntityTypeConfiguration<AlunoTurma>
{
    public void Configure(EntityTypeBuilder<AlunoTurma> builder)
    {
        builder.ToTable("AlunosTurmas");
        builder.HasKey(at => at.Id);

        // Configura o relacionamento com Aluno
        builder.HasOne(at => at.Aluno)
               .WithMany() // Se quiser a lista no Aluno, use .WithMany(a => a.AlunosTurmas)
               .HasForeignKey(at => at.AlunoId)
               .OnDelete(DeleteBehavior.Cascade);

        // Configura o relacionamento com Turma
        builder.HasOne(at => at.Turma)
               .WithMany() // Se quiser a lista na Turma, use .WithMany(t => t.AlunosTurmas)
               .HasForeignKey(at => at.TurmaId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}