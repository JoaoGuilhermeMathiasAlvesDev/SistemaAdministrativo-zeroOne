using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio.Mapping
{
    public class TurmaMapping : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.ToTable("Turmas");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Nome)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(t => t.Horario)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(t => t.Ativo)

                .HasDefaultValue(true);
            builder.Property(t => t.DiasDaAula)
                .IsRequired()
                .HasColumnType("int");

            builder.HasOne(t => t.Professor)
                .WithMany(p => p.Turmas)
                .HasForeignKey(t => t.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
