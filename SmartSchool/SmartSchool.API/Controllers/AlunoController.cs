using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.Data.DTOs;
using SmartSchool.Data.Models;
using SmartSchool.Data.Repository.Interface;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoRepository _repository;
        private readonly IMapper _mapper;

        public AlunoController(IAlunoRepository repository, IMapper mapper)
        {

            _repository = repository;
            _mapper = mapper;   
        }


        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repository.GetAllAlunos(true);         
            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos)); 
        }

        //Aqui precisamos especificar os parâmetros, quando precisarmos fazer uma outra rota com query string, tendo o mesmo verbo.
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repository.GetAlunoById(id, false);
            //var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null)
                return BadRequest("O aluno não foi encontrado");

            return Ok(aluno);
        }

        //Aqui a pesquisa é via queryString
        [HttpGet("{byName}")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = _repository.GetAllAlunos(true).FirstOrDefault(a =>
            a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome)
            );

            // var aluno = _context.Alunos.FirstOrDefault(a =>
            //a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome)
            //);

            if (aluno == null)
                return BadRequest("O aluno não foi encontrado");

            return Ok(aluno);

        }

        [HttpPost()]
        public IActionResult Post(Aluno aluno)
        {
            _repository.AdicionarAluno(aluno);
            if (_repository.SalvarAluno())
            {
                return Ok(aluno);
            }

            return BadRequest("Aluno não cadastrado.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _repository.GetAlunoById(id, false); // Para não travar o select e deixar atualizar
            if (alu == null) return BadRequest("Aluno não encontrado");

            _repository.AtualizarAluno(aluno);
            if (_repository.SalvarAluno())
            {
                return Ok(aluno);
            }

            return BadRequest("Aluno não atualizado.");
        }


        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _repository.GetAlunoById(id, false);
            if (alu == null) return BadRequest("Aluno não encontrado");

            _repository.AtualizarAluno(aluno);
            if (_repository.SalvarAluno())
            {
                return Ok(aluno);
            }

            return BadRequest("Aluno não atualizado.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repository.GetAlunoById(id, false); //No caso de delete, não precisa colocar o AsNoTracking
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _repository.DeleteAluno(aluno);
            if (_repository.SalvarAluno())
            {
                return Ok("Aluno excluído");
            }

            return BadRequest("Aluno não excluído.");
        }

    }
}
