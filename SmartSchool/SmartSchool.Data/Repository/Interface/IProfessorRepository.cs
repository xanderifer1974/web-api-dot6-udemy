using SmartSchool.Data.Models;

namespace SmartSchool.Data.Repository.Interface
{
    public interface IProfessorRepository
    {

        Professor[] GetAllProfessores();
        Professor[] GetAllProfessoresByDisciplina();
        Professor GetProfessorById();
    }
}
