using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.Data.Models;
using SmartSchool.Data.Repository.Interface;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly SmartContext _context;

        public AlunoController(SmartContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;
        }       
      

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Alunos);
        }

        //Aqui precisamos especificar os parâmetros, quando precisarmos fazer uma outra rota com query string, tendo o mesmo verbo.
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null)
                return BadRequest("O aluno não foi encontrado");

            return Ok(aluno);
        }

        //Aqui a pesquisa é via queryString
        [HttpGet("{byName}")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = _context.Alunos.FirstOrDefault(a =>
            a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome)
            );

            if (aluno == null)
                return BadRequest("O aluno não foi encontrado");

            return Ok(aluno);

        }

        [HttpPost()]
        public IActionResult Post(Aluno aluno)
        {
           _repository.Add(aluno);
            if (_repository.SaveChanges())
            {
                return Ok(aluno);
            }

            return BadRequest("Aluno não cadastrado.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id); // Para não travar o select e deixar atualizar
            if (alu == null) return BadRequest("Aluno não encontrado");

            _repository.Update(aluno);
            if (_repository.SaveChanges())
            {
                return Ok(aluno);
            }

            return BadRequest("Aluno não atualizado.");
        }


        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (alu == null) return BadRequest("Aluno não encontrado");

            _repository.Update(aluno);
            if (_repository.SaveChanges())
            {
                return Ok(aluno);
            }

            return BadRequest("Aluno não atualizado.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id); //No caso de delete, não precisa colocar o AsNoTracking
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _repository.Delete(aluno);
            if (_repository.SaveChanges())
            {
                return Ok("Aluno excluído");
            }

            return BadRequest("Aluno não excluído.");
        }

    }
}
