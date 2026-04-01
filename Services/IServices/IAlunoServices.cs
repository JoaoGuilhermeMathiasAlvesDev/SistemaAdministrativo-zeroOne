using Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.IServices
{
    public interface IAlunoServices
    {
        Task<bool> VinncularUmaTurma(Guid alunoid,Guid turmaid);
        Task<bool> CrearAluno(AlunoCriarModel model);
        Task<bool> AtulizarAluno(Guid id,AlunoAtualizarModel model);

        Task<AlunoModel> ObterPorId(Guid id);
        Task<List<AlunoModel>> ObterTodos();

        Task<bool> AlteraTurmaAluno(Guid alunoId,Guid turmaid);

        Task<bool> RemoverAlunoDaTurma(Guid alunoId, Guid turmaid);

        Task RemoverAluno(Guid id);
    }
}
