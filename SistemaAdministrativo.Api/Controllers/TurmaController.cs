using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;
using Services.Model;

namespace SistemaAdministrativo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurmaController : Controller
    {
        private readonly ITurmaServices _turmaServices;

        public TurmaController(ITurmaServices turmaServices)
        {
            _turmaServices = turmaServices;
        }

        [HttpPost]
        public async Task<IActionResult> CrearTurma(AdicionarAtualizarTurmaModel model)
        {
            if (model is null)
                return BadRequest(ModelState);

            var salvo = await _turmaServices.CriarTurma(model);

            if (!salvo)
                return NotFound("Correu um erro no cadastro");


            return Ok("Turma Criado com Sucesso!");
        }

        [HttpPut("{turmaId}")]
        public async Task<IActionResult> Atualuzar(Guid turmaId, AdicionarAtualizarTurmaModel model)
        {
            if (model is null)
                return BadRequest(ModelState);

            var salvo = await _turmaServices.AtualizarTurma(turmaId, model);

            if (!salvo)
                return NotFound("Correu um erro no Atualizar");

            return Ok("Atualização realizada com sucesso!");
        }

        [HttpPatch("{turmaId}/desativar")]
        public async Task<IActionResult> DesativaTurma(Guid turmaId)
        {
            var salvo = await _turmaServices.DesativaTurma(turmaId);

            if (!salvo) return NotFound("Ocorreu um Erro ao Desativa a Turma!");

            return Ok("Turma Desativada Com sucesso!");
        }

        [HttpPatch("{turmaId}/ativar")]
        public async Task<IActionResult> AtivarTurma(Guid turmaId)
        {
            var salvo = await _turmaServices.AtivaTurma(turmaId);

            if (!salvo) return NotFound("Ocorreu um Erro ao Ativar a Turma!");

            return Ok("Turma Ativar Com sucesso!");
        }

        [HttpPatch("{turmaId}/alterar-professor/{profesorId}")]
        public async Task<IActionResult> AlteraProfessor(Guid turmaId, Guid profesorId)
        {
            var salvo = await _turmaServices.AlteraProfessor(turmaId, profesorId);

            if (!salvo) return NotFound("Ocorreu um erro ao Altera Professor da Turma");

            return Ok("Altração realizada com Sucesso!");
        }


        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterTodasTurmas()
        {
            var resultado = await _turmaServices.ObterTodas();

            return Ok(resultado);
        }

        [HttpGet("ObterAlunosDaTurma/{id}")]
        public async Task<IActionResult> ObterAlunosDaTurma(Guid id)
        {
            var resultado = await _turmaServices.ListarAlunosDaTurma(id);

            return Ok(resultado);
        }


        [HttpGet("{turmaId}")]
        public async Task<IActionResult> ObterTurmaPorId(Guid turmaId)
        {
            var resultado = await _turmaServices.ObterPorId(turmaId);

            return Ok(resultado);
        }

        [HttpDelete("{turmaId}")]
        public async Task<IActionResult>RemoverTurma(Guid turmaId)
        {
            await _turmaServices.Remover(turmaId);
            return Ok("Removeu com Sucesso!");
        }

        [HttpDelete("removerAluno/{alunoId}/{turmaId}")]
        public async Task<IActionResult> RemoverAlunodaTurma(Guid alunoId,Guid turmaId)
        {
            await _turmaServices.RemoverAlunoDaTurma(alunoId,turmaId);
            return Ok("Removeu com Sucesso!");
        }
    }
}
