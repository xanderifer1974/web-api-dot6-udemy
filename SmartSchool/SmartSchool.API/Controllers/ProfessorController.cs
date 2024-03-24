using Microsoft.AspNetCore.Mvc;
using SmartSchool.Data.Models;
using SmartSchool.Data.Repository.Interface;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorRepository _repository;

        public ProfessorController(IProfessorRepository repository)
        {
          _repository = repository;

        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetAllProfessores(true));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var professor = _repository.GetProfessorById(id,true);
            if (professor == null)
                return BadRequest("O professor não foi encontrado");

            return Ok(professor);
        }

        [HttpGet("{byName}")]
        public IActionResult GetByName(string nome)
        {
            var professor = _repository.GetAllProfessores(true).FirstOrDefault(a =>a.Nome.Contains(nome));

            if (professor == null)
                return BadRequest("O professor não foi encontrado");

            return Ok(professor);
        }

        [HttpPost()]
        public IActionResult Post(Professor professor)
        {
            _repository.AdicionarProfessor(professor);
            if (_repository.SalvarProfessor())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não cadastrado.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = _repository.GetProfessorById(id, false); // Para não travar o select e deixar atualizar
            if (prof == null) return BadRequest("Professor não encontrado");

            _repository.AtualizarProfessor(professor);
            if (_repository.SalvarProfessor())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não atualizado.");
        }


        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = _repository.GetProfessorById(id, false); // Para não travar o select e deixar atualizar
            if (prof == null) return BadRequest("Professor não encontrado");

            _repository.AtualizarProfessor(professor);
            if (_repository.SalvarProfessor())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não atualizado.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var prof = _repository.GetProfessorById(id, false); //No caso de delete, não precisa colocar o AsNoTracking
            if (prof == null) return BadRequest("Professor não encontrado");

            _repository.DeletarProfessoer(prof);
            if (_repository.SalvarProfessor())
            {
                return Ok("Professor excluído");
            }

            return BadRequest("Professor não excluído.");
        }

    }
}
