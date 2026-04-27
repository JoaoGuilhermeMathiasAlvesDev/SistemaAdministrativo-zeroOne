using Dominio.Entidades;
using Microsoft.EntityFrameworkCore.Storage;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.IServices
{
    public interface IFinanceiroServices
    {
        Task<bool> RegistrarDespesa(DespesaModel model);
        Task<BalanceteModel> GerarBalancete(int mes, int ano);
        Task<bool> BaixarMensalidade(Guid mensalidadeId);
        Task<List<DespesaModel>> ObterDispesar(int mes, int ano);

        Task<bool> RemoverDespesa(Guid id);
    }
}
