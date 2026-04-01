using Dominio.Entidades;
using Services.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Model
{
    public class AlunoModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Matricula { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int Idade { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
        public decimal ValorMensalidade { get; set; }
        public int DiaVencimento { get; set; }
        public Guid TurmaId { get; set; }
        public TurmaModel Turma{ get; set; }
        public string NomeTurma { get; set; }

        public string NomeProfessor { get; set; }
        public List<MensalidadeModel> Mensalidades { get; set; }

        public AlunoModel() { }


        public  AlunoModel ToModel(Aluno aluno)
        {
            if (aluno == null) return null;

            return new AlunoModel
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Email = aluno.Email,
                Matricula = aluno.Matricula,
                Telefone = aluno.Telefone,
                Idade = Helper.Helper.CalulcarIdade(aluno.DataNascimento),
                DataNascimento = aluno.DataNascimento,
                Ativo = aluno.Ativo,

                // Mapeia o nome longo do domínio para o nome curto da tela
                ValorMensalidade = aluno.ValorMensalidadeContratual,
                DiaVencimento = aluno.DiaVencimento,

                // Mapeia a lista de mensalidades caso ela tenha sido carregada (Include)
                Mensalidades = aluno.Mensalidades?.Select(m => new MensalidadeModel
                {
                    Id = m.Id,
                    Vencimento = m.DataVencimento,
                    Valor = m.ValorOriginal,
                    Status = (int)m.PagamentoStatus
                }).ToList() ?? new List<MensalidadeModel>()
            };
        }

    }
}
