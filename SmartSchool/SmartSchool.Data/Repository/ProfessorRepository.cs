using Microsoft.EntityFrameworkCore;
using SmartSchool.Data.Context;
using SmartSchool.Data.Models;
using SmartSchool.Data.Repository.Interface;

namespace SmartSchool.Data.Repository
{
    public class ProfessorRepository : BaseRepository, IProfessorRepository
    {
        private readonly SmartContext _context;
        public ProfessorRepository(SmartContext context) : base(context)
        {
            _context = context;
        }



        public Professor[] GetAllProfessores(bool incluirAlunos = false)
        {

            IQueryable<Professor> query = _context.Professores;

            if (incluirAlunos)
            {
                query = query.Include(p => p.Disciplinas)
                    .ThenInclude(d => d.AlunoDisciplinas)
                    .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id);
            return query.ToArray();
        }

        public Professor[] GetAllProfessoresByDisciplina(int disciplinaId, bool includeAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAluno)
            {
                query = query.Include(p => p.Disciplinas)
                    .ThenInclude(d => d.AlunoDisciplinas)
                    .ThenInclude(a => a.Aluno);
            }

            query = query.AsNoTracking()
                .OrderBy(aluno => aluno.Id)
                .Where(aluno => aluno.Disciplinas.Any(
                    d => d.AlunoDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId)));


            return query.ToArray();
        }

        public Professor GetProfessorById(int professorId, bool includeAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAluno)
            {
                query = query.Include(p => p.Disciplinas)
                    .ThenInclude(d => d.AlunoDisciplinas)
                    .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking()
                .OrderBy(a => a.Id)
                .Where(professor => professor.Id == professorId);


            return query.FirstOrDefault();
        }


        public void AdicionarProfessor(Professor professor)
        {
            base.Add(professor);
        }

        public void AtualizarProfessor(Professor professor)
        {
            base.Update(professor);
        }

        public void DeletarProfessoer(Professor professor)
        {
            base.Delete(professor);
        }
        public bool SalvarProfessor()
        {
            return base.SaveChanges();
        }

    }
}
