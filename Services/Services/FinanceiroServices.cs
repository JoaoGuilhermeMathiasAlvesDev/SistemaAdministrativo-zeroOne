using Dominio.Entidades;
using Repositorio.IRepository.IRepositoryBase;
using Services.IServices;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public class FinanceiroServices : IFinanceiroServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public FinanceiroServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> BaixarMensalidade(Guid mensalidadeId)
        {
            var mensalidade = await _unitOfWork.Mensalidade.ObterPorId(mensalidadeId);

            if (mensalidade == null) return false;

            mensalidade.MudarStatus(0);

            _unitOfWork.Mensalidade.Atualizar(mensalidade);
            var salvo = await _unitOfWork.CompleteAsync() > 0;

            await _unitOfWork.CommitTransactionAsync();
            return salvo;

        }

        public async Task<BalanceteModel> GerarBalancete(int mes, int ano)
        {
            var despesasDoMes = await _unitOfWork.Despesa.ObterPorPeriodo(mes, ano);

            var mensalidades = await _unitOfWork.Mensalidade.ObterTodos();

            var professores = await _unitOfWork.Professo.ObterTodos();

            return new BalanceteModel().Response(mes, ano, mensalidades, despesasDoMes, professores);
        }

        public async Task<List<DespesaModel>> ObterDispesar(int mes, int ano)
        {

            var despesa = await _unitOfWork.Despesa.ObterPorPeriodo(mes, ano);
            var professor = await _unitOfWork.Professo.ObterTodos();
            var resultado = despesa.Select(d =>  new DespesaModel{ 
                Id = d.Id,
                DataPagamento = d.DataPagamento,
                Descricao = d.Descricao,
                Pago = d.Pago,
                Valor = d.Valor,
                Categoria = d.Categoria,
            }).ToList();

            DateTime hoje = DateTime.Now;
            DateTime vencimentoProximoMes = new DateTime(hoje.Year, hoje.Month, 10).AddMonths(1);
            var professorResultado = professor.Select(p=> new DespesaModel
            {
               Id = p.Id,
               DataPagamento = vencimentoProximoMes,
               Descricao = $"Salario do(a) Professor {p.Nome}",
               Valor = p.Salario,
               Pago = true,
               Categoria = Dominio.Entidades.Enum.CategoriaDespesa.Salário,
               DataVencimento = vencimentoProximoMes,
               
            }).ToList();    
             
            resultado.AddRange(professorResultado);

            return resultado;
        }

        public async Task<bool> RegistrarDespesa(DespesaModel model)
        {
            var novaDespesa = new Despesa(
                                         model.Descricao,
                                         model.Valor,
                                         model.DataVencimento,
                                         model.DataPagamento, 
                                         model.Pago,
                                         model.Categoria
                                     );

            await _unitOfWork.Despesa.Adicionar(novaDespesa);
            var salvo = await _unitOfWork.CompleteAsync() > 0;

            await _unitOfWork.CommitTransactionAsync();

            return salvo;
        }

        public async Task<bool> RemoverDespesa(Guid id)
        {
            _unitOfWork.Despesa.Remover(id);

           var salvo = await _unitOfWork.CompleteAsync() > 0 ;

            await _unitOfWork.CommitTransactionAsync();

            return salvo;
        }
    }
}
