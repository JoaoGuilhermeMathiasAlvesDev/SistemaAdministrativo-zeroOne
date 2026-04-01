using Dominio.Entidades.EntityBase;
using Dominio.Entidades.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace Dominio.Entidades
{
    public class Turma : EntityBase.EntityBase
    {
        public string Nome { get; private set; }
        public string Horario { get; private set; }

        public Guid ProfessorId { get;private set; }

        [ForeignKey("ProfessorId")]
        public virtual Professor? Professor { get; private set; }
        public bool Ativo { get; private set; }

        public DiaDaSemana DiasDaAula { get; private set; }

        public Turma() { }
        
            
        

        public Turma(string nome, string horario, Guid professorId ,DiaDaSemana diaSemana)
        {
            InserirDados(nome, horario, professorId,diaSemana);
        }

        public void InserirDados(string nome, string horario, Guid professorId,DiaDaSemana diaSemana)
        {
            Nome = nome;
            Horario = horario;
            ProfessorId = professorId;
            DiasDaAula = diaSemana;
            Ativo = true;
        }

        public void AlteraProfessor(Guid professorId)
        {
            if (ProfessorId == Guid.Empty)
                throw new Exception("Precisamos do Id do Professor. ");

            ProfessorId = professorId;
        }

        public void Ativar() => Ativo = true;
        public void Desativar() => Ativo = false;

    }
}
