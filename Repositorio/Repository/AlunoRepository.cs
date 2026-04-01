using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Repositorio.Data;
using Repositorio.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio.Repository
{
    public class AlunoRepository : RepositoryBase<Aluno>, IAlunoRepository
    {
        private readonly Contexto _contexto;

        public AlunoRepository(Contexto context) : base(context)
        {
            _contexto = context;
        }

        public async Task<int> ObterProximoSequencial()
        {
            int anoAtual = DateTime.Now.Year;
            int inicioAno = anoAtual * 10000; // Ex: 20260000
            int fimAno = inicioAno + 9999;   // Ex: 20269999 (Agora o filtro abrange todas as matrículas do ano)

            var ultimaMatricula = await _context.Alunos
                .Where(m => m.Matricula >= inicioAno && m.Matricula <= fimAno)
                .Select(m => m.Matricula)
                .OrderByDescending(m => m)
                .FirstOrDefaultAsync();

            if (ultimaMatricula == 0)
                return 1;

            // Extrai o sequencial correto (os últimos 4 dígitos)
            int sequencialAtual = ultimaMatricula % 10000;

            return sequencialAtual + 1;
        }

        public async Task<Aluno> ObterTodasInformaçoaDeUmAluno(Guid id)
        {
            return await _context.Alunos
         .Include(a => a.Mensalidades) // Garante que as mensalidades venham junto
         .AsNoTracking()
         .FirstOrDefaultAsync(a => a.Id == id);
        }



        public async Task RemoverObjeto(Aluno aluno)
        {
            _context.Alunos.Remove(aluno);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Aluno>> ObterTodosComInformacoes()
        {
            return await _context.Alunos
         .Include(a => a.Mensalidades) // Traz as mensalidades junto com o aluno
         .AsNoTracking()
         .ToListAsync();
        }
    }
}
