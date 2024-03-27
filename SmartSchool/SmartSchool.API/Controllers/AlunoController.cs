using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Helpers;
using SmartSchool.Data.DTOs;
using SmartSchool.Data.Helpers;
using SmartSchool.Data.Models;
using SmartSchool.Data.Repository.Interface;

namespace SmartSchool.API.Controllers
{
    /// <summary>
    /// Controller com as informações de aluno
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Método construtor da controller Aluno
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public AlunoController(IAlunoRepository repository, IMapper mapper)
        {

            _repository = repository;
            _mapper = mapper;   
        }

        /// <summary>
        /// Método que obtem todos os alunos cadastrados
        /// </summary>
        /// <returns>Listas de Alunos</returns>
        [HttpGet]
        public async  Task<IActionResult> Get([FromQuery] PageParams pageParams)
        {

            var alunos = await _repository.GetAllAlunosAsync(pageParams, true);

            var alunosResult = _mapper.Map<IEnumerable<AlunoDto>>(alunos);

            Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPages);

            return Ok(alunosResult);
        }

        /// <summary>
        /// Método que pesquisa aluno por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repository.GetAlunoById(id, false);
            //var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null)
                return BadRequest("O aluno não foi encontrado");

            var alunoDTO = _mapper.Map<AlunoDto>(aluno);

            return Ok(alunoDTO) ;
        }

        /// <summary>
        /// Método para pesquisar aluno por nome e sobrenome.
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="sobrenome"></param>
        /// <returns></returns>
        [HttpGet("{byName}")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = _repository.GetAllAlunos(true).FirstOrDefault(a =>
            a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome)
            );            

            if (aluno == null)
                return BadRequest("O aluno não foi encontrado");

            var alunoDTO = _mapper.Map<AlunoDto>(aluno);

            return Ok(alunoDTO);

        }

        /// <summary>
        /// Método para gravarmos um aluno no banco de dados
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult Post(AlunoRecordDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repository.AdicionarAluno(aluno);
            if (_repository.SalvarAluno())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não cadastrado.");
        }

        /// <summary>
        /// Método para editarmos um aluno no banco de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRecordDto model)
        {
            var aluno = _repository.GetAlunoById(id, false); // Para não travar o select e deixar atualizar
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);

            _repository.AtualizarAluno(aluno);
            if (_repository.SalvarAluno())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não atualizado.");
        }

        /// <summary>
        /// Metodo para editarmos informações específicas de um aluno no banco de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRecordDto model)
        {
            var aluno = _repository.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);

            _repository.AtualizarAluno(aluno);
            if (_repository.SalvarAluno())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não atualizado.");
        }
        /// <summary>
        /// Métod para excluir um aluno no banco de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
