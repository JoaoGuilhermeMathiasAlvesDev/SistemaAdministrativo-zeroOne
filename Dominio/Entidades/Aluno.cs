using Dominio.Entidades.EntityBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Entidades
{
    public class Aluno : EntityBase.EntityBase
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public int Matricula { get; private set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; private set; }
        public decimal ValorMensalidadeContratual { get; private set; }
        public int DiaVencimento { get; private set; }
      
        public bool Ativo { get; private set; } = true;
        public DateTime DataMatricula { get; private set; } = DateTime.Now;


        public virtual ICollection<Mensalidade> Mensalidades { get; private set; } = new List<Mensalidade>();

        public Aluno()
        {

        }

        public Aluno(string nome, string email, DateTime dataNascimento, string telefone)
        {
            AdiconarEAtualizarAluno(nome, email, dataNascimento, telefone);
        }

        public void AdiconarEAtualizarAluno(string nome, string email, DateTime datanascimento, string telefone)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentNullException("Por gentileza digite uma nome.");
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("por favor gentileza digite email");

            Telefone = validarNumero(telefone);
            Nome = nome;
            Email = email;
            DataNascimento = datanascimento;
        }


        public void valorContratualAdicionar(decimal valor, int vencimento)
        {
            if (valor < 0)
                throw new ArgumentException("O valor da mensalidade não pode ser negativo.");

            if (vencimento < 1 || vencimento > 31)
                throw new ArgumentException("O dia de vencimento deve ser entre 1 e 31.");

            ValorMensalidadeContratual = valor;
            DiaVencimento = vencimento;
        }
        public List<Mensalidade> GerarCarneAnual()
        {
            var mensalidades = new List<Mensalidade>();
           
            DateTime primeiroDiaMesAtual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            for (int i = 1; i <= 12; i++) 
            {
                
                DateTime dataAux = primeiroDiaMesAtual.AddMonths(i);

                int ultimoDiaDoMes = DateTime.DaysInMonth(dataAux.Year, dataAux.Month);

                int diaFinal = Math.Min(this.DiaVencimento, ultimoDiaDoMes);

                var vencimento = new DateTime(dataAux.Year, dataAux.Month, diaFinal);

                var novaMensalidade = new Mensalidade(this.Id, this.ValorMensalidadeContratual, vencimento);
                mensalidades.Add(novaMensalidade);
            }

            return mensalidades;
        }

        private string validarNumero(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
                throw new ArgumentException("O telefone não pode estar vazio.");

            string apenasNumeros = new string(numero.Where(char.IsDigit).ToArray());

            if (apenasNumeros.Length < 10 || apenasNumeros.Length > 11)
            {
                throw new ArgumentException("O telefone deve ter 10 ou 11 dígitos (incluindo o DDD).");
            }

            return apenasNumeros;
        }

        public void DefinirMatricula(int proximoSequencial)
        {
            if (proximoSequencial <= 0)
                throw new ArgumentException("O sequencial da matrícula deve ser maior que zero.");

            int anoAtual = DateTime.Now.Year;

            this.Matricula = (anoAtual * 10000) + proximoSequencial; ;
        }

    }
}
