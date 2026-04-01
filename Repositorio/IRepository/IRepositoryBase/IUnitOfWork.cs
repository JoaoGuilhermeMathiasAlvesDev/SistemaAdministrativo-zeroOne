using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio.IRepository.IRepositoryBase
{
    public interface IUnitOfWork
    {
        IAlunoRepository Aluno { get; }
        IProfessoRepository Professo { get; }
        ITurmaRepository Turma { get; }
        IMensalidadeRepository Mensalidade { get; }
        IChamadaRepositoy Chamada { get; }
        IChamadaItemRepository ChamadaItem { get; }
        IAlunoTurmaRepository AlunoTurma { get; }
        IDespesaRepository Despesa { get; }

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task<int> CompleteAsync();
        void Rollback();
    }
}
