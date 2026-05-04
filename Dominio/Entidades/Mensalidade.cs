using Dominio.Entidades.EntityBase;
using Dominio.Entidades.Enum;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Dominio.Entidades
{
    public class Mensalidade : EntityBase.EntityBase
    {
        public Guid AlunoId { get; private set; }
        public decimal ValorOriginal { get; private set; }
        public decimal? ValorPago { get; private set; }
        public decimal Desconto { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public DateTime? DataPagamento { get; private set; }
        public PagamentoStatus PagamentoStatus { get; private set; }

        public Mensalidade()
        {
            
        }
        public Mensalidade(Guid alunoId, decimal valor, DateTime vencimento)
        {
           InserirDados(alunoId, valor, vencimento);
        }

        public void InserirDados (Guid alunoId, decimal valor, DateTime vencimento)
        {
            if (alunoId == Guid.Empty)
                throw new ArgumentException("Para Criar uma mensalidade precisa do Id do Aluno.");

            if (valor <= 0)
                throw new ArgumentException("A mensalidade o valor não pode ser zero ou menor que zero");

            if (vencimento < DateTime.Now.Date)
                throw new ArgumentException("A mensalidade data de vencimento não pode ser menor que data atual");


            AlunoId= alunoId;
            ValorOriginal = valor;
            DataVencimento = vencimento;
            PagamentoStatus= PagamentoStatus.Pendente ;
            
        }


        public void MudarStatus(int status)
        {
            if (!System.Enum.IsDefined(typeof(PagamentoStatus), status))
                throw new ArgumentException("Esse status não existe.");

            if (status == (int)PagamentoStatus.Pago)
            {
                DataPagamento = DateTime.Now;
            }
             
            PagamentoStatus = (PagamentoStatus)status;
        }

        public void AlteraDataVencimento(DateTime data)
        {

            if (data < DateTime.Now.Date)
                throw new ArgumentException("A mensalidade data de vencimento não pode ser menor que data atual");

            DataVencimento= data;
        }
    }
}
