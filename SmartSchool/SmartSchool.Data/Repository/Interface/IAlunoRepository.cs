using SmartSchool.Data.Helpers;
using SmartSchool.Data.Models;

namespace SmartSchool.Data.Repository.Interface
{
    public interface IAlunoRepository
    {
        Task<PageList<Aluno>> GetAllAlunosAsync(bool incluirProfessor);
        Aluno[] GetAllAlunos(bool incluirProfessor);
        Aluno[] GetAllAlunosByDisciplina(int disciplinaId, bool incluirProfessor);
        Aluno GetAlunoById(int alunoId, bool incluirProfessor);

        void AdicionarAluno(Aluno aluno);
        void AtualizarAluno(Aluno aluno);
        void DeleteAluno(Aluno aluno);
        bool SalvarAluno();
    }
}
