using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio.IRepository
{
    public interface IAlunoTurmaRepository : IRepositoryBase.IRepositoryBase<AlunoTurma>
    {
        Task<bool> AlunoEstaEmUmaTurma(Guid alunoid);

        Task<AlunoTurma> ObterTurmaDoAluno(Guid alunoid, Guid turmaId);

        Task removerAlunoTurma(AlunoTurma alunoTurma);
    }
}
