using SmartSchool.Data.Models;

namespace SmartSchool.Data.Repository.Interface
{
    public interface IProfessorRepository
    {

        Professor[] GetAllProfessores(bool incluirAluno = false);
        Professor[] GetAllProfessoresByDisciplina(int disciplinaId, bool includeAluno = false);
        Professor GetProfessorById(int professorId, bool includeAluno = false);

        void AdicionarProfessor(Professor professor);
        void AtualizarProfessor(Professor professor);
        void DeletarProfessoer(Professor professor);
        bool SalvarProfessor();
    }
}
