using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
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

        public Aluno[] GetAllAlunos()
        {
            IQueryable<Aluno> query = _context.Alunos;

            query = query.AsNoTracking().OrderBy(a => a.Id);
            return query.ToArray();
        }

        public Aluno[] GetAllAlunosByDisciplina()
        {
            throw new NotImplementedException();
        }

        public Aluno GetAlunoById()
        {
            throw new NotImplementedException();
        }
    }
}
