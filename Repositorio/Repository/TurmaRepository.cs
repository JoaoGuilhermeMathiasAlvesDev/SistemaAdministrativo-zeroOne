using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Repositorio.Data;
using Repositorio.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio.Repository
{
    public class TurmaRepository : RepositoryBase<Turma>, ITurmaRepository
    {
        private readonly Contexto _contexto;

        public TurmaRepository(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<Turma> ObterPorIdCompletoAsync(Guid id)
        {
            return await _context.Turmas
                .Include(t => t.Professor) 
                .Where(t => t.Id == id)
                .Select(t => new {
                    Turma = t,
                    
                    Alunos = _context.AlunoTurmas
                        .Where(at => at.TurmaId == t.Id)
                        .Select(at => at.Aluno)
                        .ToList()
                })
                .AsNoTracking()
                .Select(x => x.Turma)
                .FirstOrDefaultAsync();

        }
    }
}
