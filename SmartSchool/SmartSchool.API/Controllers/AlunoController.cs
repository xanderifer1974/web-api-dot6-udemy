using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {

        public List<Aluno>? Alunos = new List<Aluno>()
        {
            new Aluno()
            {
                Id = 1,
                Nome = "Marcos",
                Sobrenome= "Almeida",
                Telefone= "3874-7451"
            },
             new Aluno()
            {
                Id = 2,
                Nome = "João",
                Sobrenome="Barbosa",
                Telefone= "3874-7448"
            },
              new Aluno()
            {
                Id = 3,
                Nome = "Paulo",
                Sobrenome="Ferreira",
                Telefone= "3874-7435"
            }
        };

        public AlunoController() { }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Alunos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = Alunos.FirstOrDefault(a => a.Id == id );
            if (aluno == null)
                return BadRequest("O aluno não foi encontrado"); 

            return Ok(aluno);          
        }

    }
}
