using Microsoft.EntityFrameworkCore;
using SmartSchool.Data.Context;
using SmartSchool.Data.Helpers;
using SmartSchool.Data.Models;
using SmartSchool.Data.Repository.Interface;

namespace SmartSchool.Data.Repository
{
    public class AlunoRepository : BaseRepository, IAlunoRepository
    {
        private readonly SmartContext _context;

        public AlunoRepository(SmartContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool incluirProfessor)
        {
            IQueryable<Aluno> query =   _context.Alunos;

            if (incluirProfessor)
            {
                query = query.Include(a => a.AlunoDisciplinas)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);
         
            return await PageList<Aluno>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);

        }

        public Aluno[] GetAllAlunos(bool incluirProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (incluirProfessor)
            {
                query = query.Include(a => a.AlunoDisciplinas)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);
            return query.ToArray();
        }

        public Aluno[] GetAllAlunosByDisciplina(int disciplinaId, bool incluirProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (incluirProfessor)
            {
                query = query.Include(a => a.AlunoDisciplinas)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                .OrderBy(a => a.Id)
                .Where(aluno => aluno.AlunoDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId));

            return query.ToArray();
        }

        public Aluno GetAlunoById(int alunoId, bool incluirProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (incluirProfessor)
            {
                query = query.Include(a => a.AlunoDisciplinas)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                .OrderBy(a => a.Id)
                .Where(aluno => aluno.Id == alunoId);


            return query.FirstOrDefault();
        }

        public void AdicionarAluno(Aluno aluno)
        {
           base.Add(aluno);          
        }

        public void AtualizarAluno(Aluno aluno)
        {
           base.Update(aluno);
        }

        public void DeleteAluno(Aluno aluno)
        {
           base.Delete(aluno);
        }

        public bool SalvarAluno()
        {
           return base.SaveChanges();
        }
       
    }
}
