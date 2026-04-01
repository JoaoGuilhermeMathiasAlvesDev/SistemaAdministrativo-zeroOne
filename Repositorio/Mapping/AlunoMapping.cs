using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositorio.Mapping
{
    public class AlunoMapping : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("Alunos");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome).HasColumnType("varchar(150)").IsRequired();
            builder.Property(a => a.Email).HasColumnType("varchar(150)").IsRequired();
            builder.Property(a => a.Telefone).HasColumnType("varchar(11)").IsRequired();
            builder.Property(a => a.Matricula).IsRequired();
            builder.HasIndex(a => a.Matricula).IsUnique();

            builder.Property(a => a.ValorMensalidadeContratual).HasPrecision(18, 2).IsRequired();


            builder.HasMany(a => a.Mensalidades)
                .WithOne()
                .HasForeignKey(m => m.AlunoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}