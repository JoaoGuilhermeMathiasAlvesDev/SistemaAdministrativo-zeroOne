using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Model
{
    public class ChamadaModel
    {
        public Guid IdChamada { get; set; }
        public Guid TurmaId { get; set; }
        public string NomeTurma { get; set; }
        public DateTime DataAula { get; set; }

        public List<ChamadaItemModel> Alunos { get; set; } = new List<ChamadaItemModel>();

        public ChamadaModel() { }

        public ChamadaModel Response(Chamada chamada)
        {
            if (chamada == null) return null;

            this.IdChamada = chamada.Id;
            this.TurmaId = chamada.TurmaId;
            this.DataAula = chamada.DataAula;

            this.NomeTurma = chamada.Turma?.Nome ?? "Turma não carregada";

            if (chamada.AlunosPresenca != null && chamada.AlunosPresenca.Any())
            {
                this.Alunos = chamada.AlunosPresenca.Select(item => new ChamadaItemModel
                {
                    Id = item.Id,
                    AlunoId = item.AlunoId,
                    NomeAluno = item.Aluno?.Nome ?? "Aluno não carregado",
                    Presente = item.Presente,
                    Observacao = item.Observacao
                }).ToList();
            }

            return this;
        }
    }
}
