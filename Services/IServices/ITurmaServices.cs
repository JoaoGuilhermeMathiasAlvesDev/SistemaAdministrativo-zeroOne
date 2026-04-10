using Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.IServices
{
    public interface ITurmaServices
    {

        Task <bool> DesativaTurma(Guid Turma);
        Task<bool> AtivaTurma(Guid Turma);
        Task <bool>  AlteraProfessor (Guid turma, Guid NovoProfessor);
        Task<bool> CriarTurma(AdicionarAtualizarTurmaModel model);

        Task<TurmaModel> ObterPorId(Guid id);

        Task<List<TurmaModel>> ObterTodas();
        Task<List<AlunoModel>> ListarAlunosDaTurma(Guid turmaId);
        Task<bool> AtualizarTurma(Guid turmaId,AdicionarAtualizarTurmaModel model);

        Task RemoverAlunoDaTurma(Guid alunoid, Guid turmaId);
        Task Remover(Guid Id);
    }
}
