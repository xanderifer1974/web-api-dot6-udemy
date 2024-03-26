using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.Data.DTOs;
using SmartSchool.Data.Models;
using SmartSchool.Data.Repository.Interface;

namespace SmartSchool.API.Controllers
{
    /// <summary>
    /// Controller Professor
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]    
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor da controller professor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public ProfessorController(IProfessorRepository repository, IMapper mapper)
        {
          _repository = repository;
           _mapper = mapper;

        }
        /// <summary>
        /// Método que retorna todos os professores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var Professor = _repository.GetAllProfessores(true);
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(Professor));
        }

        /// <summary>
        /// Método que retorna professor por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var Professor = _repository.GetProfessorById(id, true);
            if (Professor == null) return BadRequest("O Professor não foi encontrado");

            var professorDto = _mapper.Map<ProfessorDto>(Professor);

            return Ok(professorDto);
        }

        /// <summary>
        /// Método que pesquisa professor por nome
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        [HttpGet("{byName}")]
        public IActionResult GetByName(string nome)
        {
            var professor = _repository.GetAllProfessores(true).FirstOrDefault(a =>a.Nome.Contains(nome));

            if (professor == null)
                return BadRequest("O professor não foi encontrado");

            var professorDto = _mapper.Map<ProfessorDto>(professor);

            return Ok(professorDto);
        }

        /// <summary>
        /// Método para gravar um professor no banco de dados
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult Post(ProfessorRecordDto model)
        {
            var prof = _mapper.Map<Professor>(model);
            _repository.AdicionarProfessor(prof);

            if (_repository.SalvarProfessor())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(prof));
            }

            return BadRequest("Professor não cadastrado.");
        }

        /// <summary>
        /// Método para editar um professor no banco de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRecordDto model)
        {

            var prof = _repository.GetProfessorById(id, false); // Para não travar o select e deixar atualizar
            if (prof == null) return BadRequest("Professor não encontrado");

            _mapper.Map(model, prof);

            _repository.AtualizarProfessor(prof);
            if (_repository.SalvarProfessor())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(prof));
            }

            return BadRequest("Professor não atualizado.");
        }

        /// <summary>
        /// Método para editar informações específicas do professor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRecordDto model)
        {
            var prof = _repository.GetProfessorById(id, false); // Para não travar o select e deixar atualizar
            if (prof == null) return BadRequest("Professor não encontrado");

            _mapper.Map(model, prof);

            _repository.AtualizarProfessor(prof);
            if (_repository.SalvarProfessor())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(prof));
            }

            return BadRequest("Professor não atualizado.");
        }

        /// <summary>
        /// Método para excluir um professor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
