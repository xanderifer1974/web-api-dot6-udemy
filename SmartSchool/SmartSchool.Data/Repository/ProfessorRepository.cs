using SmartSchool.API.Data;
using SmartSchool.Data.Models;
using SmartSchool.Data.Repository.Interface;

namespace SmartSchool.Data.Repository
{
    public class ProfessorRepository : BaseRepository, IProfessorRepository
    {
        public ProfessorRepository(SmartContext context) : base(context)
        {
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
