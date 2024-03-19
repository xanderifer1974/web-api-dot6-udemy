using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        public AlunoController() { }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Alunos: Paulo, Marta, Laura, Lucas");
        }
       
    }
}
