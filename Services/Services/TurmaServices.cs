using Dominio.Entidades;
using Repositorio.IRepository.IRepositoryBase;
using Services.IServices;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public class TurmaServices : ITurmaServices
    {
        private readonly IUnitOfWork _UnitOfWork;

        public TurmaServices(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<bool> AlteraProfessor(Guid turma, Guid NovoProfessor)
        {
            var obterTurma = await _UnitOfWork.Turma.ObterPorId(turma);

            obterTurma.AlteraProfessor(NovoProfessor);

            _UnitOfWork.Turma.Atualizar(obterTurma);

            var salvo = await _UnitOfWork.CompleteAsync() > 0;

            await _UnitOfWork.CommitTransactionAsync();

            return salvo;
        }

        public async Task<bool> AtivaTurma(Guid Turma)
        {
            var obterTurma = await _UnitOfWork.Turma.ObterPorId(Turma);

            obterTurma.Ativar();

            _UnitOfWork.Turma.Atualizar(obterTurma);

            var salvo = await _UnitOfWork.CompleteAsync() > 0;
            await _UnitOfWork.CommitTransactionAsync();

            return salvo;

        }

        public async Task<bool> AtualizarTurma(Guid turmaId, AdicionarAtualizarTurmaModel model)
        {
            var obterTurma = await _UnitOfWork.Turma.ObterPorId(turmaId);

            var diaDaSemana = model.ObterDiasEnum();

            obterTurma.InserirDados(model.Nome, model.Horario, model.ProfessorId.Value, diaDaSemana);

            _UnitOfWork.Turma.Atualizar(obterTurma);

            var salvo = await _UnitOfWork.CompleteAsync() > 0;
            await _UnitOfWork.CommitTransactionAsync();

            return salvo;
        }

        public async Task<bool> CriarTurma(AdicionarAtualizarTurmaModel model)
        {
            var diaSemana = model.ObterDiasEnum();
            var criarTurma = new Turma(model.Nome, model.Horario, model.ProfessorId.Value, diaSemana);

            await _UnitOfWork.Turma.Adicionar(criarTurma);

            var salvo = await _UnitOfWork.CompleteAsync() > 0;
            await _UnitOfWork.CommitTransactionAsync();

            return salvo;
        }

        public async Task<bool> DesativaTurma(Guid Turma)
        {
            var obterTurma = await _UnitOfWork.Turma.ObterPorId(Turma);

            obterTurma.Desativar();

            _UnitOfWork.Turma.Atualizar(obterTurma);

            var salvo =  await _UnitOfWork.CompleteAsync() > 0;
            await _UnitOfWork.CommitTransactionAsync();

            return salvo;
        }

        public async Task<List<AlunoModel>> ListarAlunosDaTurma(Guid turmaId)
        {
           var resultado = await _UnitOfWork.AlunoTurma.ListarAlunosPorTurma(turmaId);

            if (resultado == null)
                return null;

            return resultado.Select(alunos => new AlunoModel().ToChama(alunos)).ToList();

        }

        public async Task<TurmaModel> ObterPorId(Guid id)
        {
            var obter = await _UnitOfWork.Turma.ObterPorIdCompletoAsync(id);

            if (obter == null)
                return null;

            return new TurmaModel().Response(obter);
        }

        public async Task<List<TurmaModel>> ObterTodas()
        {
            var obterTodos = await _UnitOfWork.Turma.ObterTodos();
            
            return obterTodos.Select(turma => new TurmaModel().Response(turma)).ToList();
        }

        public async Task Remover(Guid Id)
        {
            var existeTurma = await _UnitOfWork.Turma.ObterPorId(Id);

            if (existeTurma == null)
                throw new Exception("Turma não existe na base de dados.");

            _UnitOfWork.Turma.Remover(Id);

            await _UnitOfWork.CompleteAsync();

            await _UnitOfWork.CommitTransactionAsync();

        }

        public async Task RemoverAlunoDaTurma(Guid alunoid, Guid turmaId)
        {
            var obterAlunoTurma = await _UnitOfWork.AlunoTurma.ObterTurmaDoAluno(alunoid);

            if (obterAlunoTurma.TurmaId != turmaId)
                throw new Exception("Erro ao encontrar o Aluno");

            await _UnitOfWork.AlunoTurma.removerAlunoTurma(obterAlunoTurma);

            await _UnitOfWork.CompleteAsync();

            await _UnitOfWork.CommitTransactionAsync();
        }
    }
}
