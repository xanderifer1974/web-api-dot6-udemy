using SmartSchool.Data.Models;

namespace SmartSchool.Data.Repository.Interface
{
    public interface IAlunoRepository
    {
        Aluno[] GetAllAlunos(bool incluirProfessor);
        Aluno[] GetAllAlunosByDisciplina(int disciplinaId, bool incluirProfessor);
        Aluno GetAlunoById(int alunoId, bool incluirProfessor);
    }
}
