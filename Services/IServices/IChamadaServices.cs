using Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.IServices
{
    public interface IChamadaServices
    {
        Task<bool> Registrar(AdicionarChamadaModel model);
        Task<bool> AlterarPresenca(Guid chamadaId, List<AlunoPresencaModel> alunos);
        Task<ChamadaModel> ObterPorId(Guid id);
        Task<IEnumerable<ChamadaModel>> ObterPorTurma(Guid turmaId);
        Task<RelatorioTurmaModel> GerarRelatorioConsolidado(Guid turmaId);
        Task<AdicionarChamadaModel?> ObterChamadaPorData(Guid turmaId, DateTime data);

    }
}
