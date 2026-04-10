using Microsoft.AspNetCore.Mvc;
using Services.IServices;
using Services.Model;

namespace SistemaAdministrativo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorServices _professorServices;

        public ProfessorController(IProfessorServices professorServices)
        {
            _professorServices = professorServices;
        }

        [HttpPost]
        public async Task<IActionResult>CreateProfessor(AdicionarAtualizaProfessorModel model)
        {
            var salvo = await _professorServices.CrearProfessor(model);

            if (!salvo)
                return NotFound(new { message = "Erro ao Criar Professor." });

            return Ok(new { message = "Professor Cadastrado com Sucesso!" });
        }

        [HttpPut]
        public async Task<IActionResult>AtualizarProfessor(AdicionarAtualizaProfessorModel model)
        {
            var salvo = await _professorServices.AtulizarProfessor(model);

            if (!salvo)
                return NotFound(new { message = "Erro ao Atualizar o Professor." });

            return Ok(new { message = "Professor atualizado com Sucesso!" });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorIdProfessor (Guid id)
        {
            var resultado =  await _professorServices.ObterPorId(id);

            return Ok(resultado);
        }

        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterTodosProfessores()
        {
            var resultado = await _professorServices.ObterTodos();

            return Ok(resultado);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult>RemoverProfessor(Guid id)
        {
            await _professorServices.RemoverProfessor(id);

            return Ok(new { mensagem = "Removeu Com Sucesso!" });
        }
    }
}
