using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq; // Garante que o .Select e .Any funcionem
using System.Text;

namespace Services.Model
{
    public class TurmaModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Horario { get; set; }
        public bool Ativo { get; set; }

        // --- Adicionado para refletir a mudança na Entidade ---
        public int DiasDaAula { get; set; }

        public Guid? ProfessorId { get; set; }
        public string NomeProfessor { get; set; }
        public ProfessorModel Professor { get; set; }

        public List<AlunoModel> Alunos { get; set; } = new List<AlunoModel>();

        public TurmaModel() { }

        public TurmaModel Response(Turma turma)
        {
            this.Id = turma.Id;
            this.Nome = turma.Nome;
            this.Horario = turma.Horario;
            this.Ativo = turma.Ativo;
            this.ProfessorId = turma.ProfessorId;

            // Mapeia o Enum para int para facilitar o transporte dos dados
            this.DiasDaAula = (int)turma.DiasDaAula;

            if (turma.Professor != null)
            {
                this.NomeProfessor = turma.Professor.Nome;
                this.Professor = new ProfessorModel
                {
                    Id = turma.Professor.Id,
                    Nome = turma.Professor.Nome
                };
            }
            else
            {
                this.NomeProfessor = "Sem Professor Atribuído";
            }

            //if (turma.Alunos != null && turma.Alunos.Any())
            //{
            //    this.Alunos = turma.Alunos.Select(aluno => new AlunoModel
            //    {
            //        Id = aluno.Id,
            //        Nome = aluno.Nome,
            //        Matricula = aluno.Matricula,
            //        Email = aluno.Email,
            //        Ativo = aluno.Ativo
            //    }).ToList();
            //}

            return this;
        }
    }
}