using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Repositorio.Data;
using Repositorio.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio.Repository
{
    public class AlunoTurmaRepository : RepositoryBase<AlunoTurma>, IAlunoTurmaRepository
    {
        private readonly Contexto _contexto;

        public AlunoTurmaRepository(Contexto contexto) : base(contexto) {
        
            _contexto = contexto;
        }

        public async Task<bool> AlunoEstaEmUmaTurma(Guid alunoid)
        {
           return await _context.AlunoTurmas
                .AsNoTracking()
                .AnyAsync(a => a.AlunoId == alunoid);
        }

        public async Task<AlunoTurma> ObterTurmaDoAluno(Guid alunoid, Guid turmaId)
        {
            return await _context.AlunoTurmas
                .AsNoTracking().
                FirstOrDefaultAsync(a => a.TurmaId == turmaId && a.AlunoId == alunoid);
        }

        public Task removerAlunoTurma(AlunoTurma alunoTurma)
        {
            _context.Remove(alunoTurma);

            return Task.CompletedTask;
        }
    }
}
