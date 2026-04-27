using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Model
{
    public class BalanceteModel
    {
        public string Periodo { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }

        public decimal TotalMensalidadesRecebidas { get; set; }
        public int QuantidadePagamentosRealizados { get; set; }

        public decimal TotalSalariosProfessores { get; set; }
        public decimal TotalGastosCurso { get; set; }
        public decimal TotalGeralDespesas => TotalSalariosProfessores + TotalGastosCurso;

        public decimal SaldoLiquido { get; set; }
        public string StatusFinanceiro => SaldoLiquido >= 0 ? "Lucro" : "Prejuízo";

        public decimal ValorPendenteReceber { get; set; }
        public int QuantidadeAlunosInadimplentes { get; set; }

        public BalanceteModel Response(
            int mes,
            int ano,
            IEnumerable<Mensalidade> mensalidades,
            IEnumerable<Despesa> despesas,
            IEnumerable<Professor> professores)
        {
            Mes = mes;
            Ano = ano;
            Periodo = $"{mes:D2}/{ano}";

            var mensalidadesPagasNoMes = mensalidades
                .Where(m => m.PagamentoStatus == 0 &&
                            m.DataPagamento.HasValue &&
                            m.DataPagamento.Value.Month == mes &&
                            m.DataPagamento.Value.Year == ano)
                .ToList();

            TotalMensalidadesRecebidas = mensalidadesPagasNoMes.Sum(m => m.ValorPago ?? 0m);
            QuantidadePagamentosRealizados = mensalidadesPagasNoMes.Count;

            TotalSalariosProfessores = professores.Sum(p => p.Salario);

            TotalGastosCurso = despesas
                .Where(d => d.DataVencimento.Month == mes && d.DataVencimento.Year == ano)
                .Sum(d => d.Valor);

            SaldoLiquido = TotalMensalidadesRecebidas - TotalGeralDespesas;

            var dataReferencia = new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes));

            var pendentes = mensalidades
                .Where(m => !m.DataPagamento.HasValue &&
                            m.DataVencimento <= dataReferencia)
                .ToList();

            ValorPendenteReceber = pendentes.Sum(m => m.ValorOriginal);
            QuantidadeAlunosInadimplentes = pendentes.Select(m => m.AlunoId).Distinct().Count();

            return this;
        }
    }
}