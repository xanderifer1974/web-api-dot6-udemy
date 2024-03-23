using SmartSchool.API.Data;
using SmartSchool.Data.Models;
using SmartSchool.Data.Repository.Interface;

namespace SmartSchool.Data.Repository
{
    public class AlunoRepository : BaseRepository, IAlunoRepository
    {
        public AlunoRepository(SmartContext context) : base(context)
        {
        }

        public Aluno[] GetAllAlunos()
        {
            throw new NotImplementedException();
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
