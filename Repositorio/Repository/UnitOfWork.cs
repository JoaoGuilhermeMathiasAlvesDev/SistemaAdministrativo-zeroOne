using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repositorio.Data;
using Repositorio.IRepository;
using Repositorio.IRepository.IRepositoryBase;
using Repositorio.Mapping;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Repositorio.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Contexto _contexto;
        private IDbContextTransaction _transaction;

        public IAlunoRepository Aluno { get; private set; }
        public IProfessoRepository Professo { get; private set; }

        public IChamadaRepositoy Chamada { get; private set; }

        public IChamadaItemRepository ChamadaItem { get; private set; }

        public ITurmaRepository Turma { get;private set; }
        public IMensalidadeRepository Mensalidade { get; private set; }

        public IDespesaRepository Despesa {  get; private set; }

        public IAlunoTurmaRepository AlunoTurma { get; private set; }

        public UnitOfWork(Contexto contexto)
        {
            _contexto = contexto;
            Aluno = new AlunoRepository(_contexto);
            Professo = new ProfessorRepository(_contexto);
            Turma = new TurmaRepository(_contexto);
            Mensalidade = new MensalidadeRepository(_contexto);
            Chamada = new ChamadaRepository(_contexto);
            ChamadaItem = new ChamadaItemRepository(_contexto);
            Despesa = new DespesaRepository(_contexto);
            AlunoTurma = new AlunoTurmaRepository(_contexto);
        }


        public async Task<bool> Commit()
        {
            try
            {
                var success = await _contexto.SaveChangesAsync() > 0;
                if (_transaction != null) await _transaction.CommitAsync();
                return success;
            }
            catch (Exception)
            {

                await Rollback();
                return false;
            }
        }

        public async Task Rollback()
        {
            if(_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
            {
                _transaction = await _contexto.Database.BeginTransactionAsync();
            }
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _contexto.SaveChangesAsync();

                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }
            }
            catch
            {
                Rollback();

                throw;
            }
            finally
            {
                DisposeTransaction();
            }
        }

        public async Task<int> CompleteAsync()
        {
            return await _contexto.SaveChangesAsync();
        }

        void IUnitOfWork.Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                DisposeTransaction();
            }
        }

        private void DisposeTransaction()
        {
            _transaction?.Dispose();
        }
    }
}
