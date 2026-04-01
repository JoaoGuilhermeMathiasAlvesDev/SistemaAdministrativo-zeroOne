using Dominio.Entidades.EntityBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Entidades
{
    public class AlunoTurma : EntityBase.EntityBase
    {
        public Guid AlunoId { get;  set; }
        public Guid TurmaId {  get;  set; }


        [NotMapped]
        public Aluno Aluno { get;  set; }
        [NotMapped]
        public Turma Turma { get; set; } // O EF vai ignorar isso completamente

        public AlunoTurma()
        {
            
        }

        public void AdicionarAlunoNaTurma(Guid turmaId,Guid alunoId)
        {
            AlunoId = alunoId;
            TurmaId = turmaId;
        }

        public void AtualizarAlunoNaTurma(Guid turmaId, Guid alunoId)
        {
            AlunoId = alunoId;
            TurmaId = turmaId;
        }
    }
}
