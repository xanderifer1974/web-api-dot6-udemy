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

        public Professor[] GetAllProfessores()
        {
            throw new NotImplementedException();
        }

        public Professor[] GetAllProfessoresByDisciplina()
        {
            throw new NotImplementedException();
        }

        public Professor GetProfessorById()
        {
            throw new NotImplementedException();
        }
    }
}
