using SmartSchool.Data.Models;

namespace SmartSchool.Data.Repository.Interface
{
    public interface IAlunoRepository
    {
        Aluno[] GetAllAlunos();
        Aluno[] GetAllAlunosByDisciplina();
        Aluno GetAlunoById();
    }
}
