using Microsoft.AspNetCore.Mvc;
using Services.IServices;
using Services.Model;
using System.Security.AccessControl;

namespace SistemaAdministrativo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChamadaController : ControllerBase
    {
        private readonly IChamadaServices _chamadaService;

        public ChamadaController(IChamadaServices chamadaService)
        {
            _chamadaService = chamadaService;
        }

        [HttpPost] 
        public async Task<IActionResult> Registrar([FromBody] AdicionarChamadaModel model)
        {
            if (model == null) return BadRequest("Dados inválidos.");

            var sucesso = await _chamadaService.Registrar(model);

            if (!sucesso)
                return BadRequest(new { message = "Erro ao registrar a chamada." });

            return Ok(new { message = "Chamada registrada com sucesso!" });
        }

        [HttpPatch("{id:guid}/presencas")] 
        public async Task<IActionResult> AlterarPresenca(Guid id, List<AlunoPresencaModel> alunos)
        {
            if (alunos == null || !alunos.Any())
                return BadRequest(new { message = "A lista de alunos não pode estar vazia." });

            var sucesso = await _chamadaService.AlterarPresenca(id, alunos);

            if (!sucesso)
                return NotFound(new { message = "Chamada não encontrada ou sem itens para atualizar." });

            return Ok("Presenças atualizadas com sucesso!");
        }

        [HttpGet("{id:guid}")] 
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var resultado = await _chamadaService.ObterPorId(id);

            if (resultado == null)
                return NotFound("Chamada não encontrada.");

            return Ok(resultado);
        }

        [HttpGet("turma/{turmaId:guid}")] 
        public async Task<IActionResult> ObterPorTurma(Guid turmaId)
        {
            var resultado = await _chamadaService.ObterPorTurma(turmaId);

            if (resultado == null || !resultado.Any())
                return NotFound("Nenhuma chamada encontrada para esta turma.");

            return Ok(resultado);
        }


        [HttpGet("relatorio/turma/{turmaId:guid}")]
        public async Task<ActionResult<RelatorioTurmaModel>> ObterRelatorio(Guid turmaId)
        {
            // Aqui você usa o await para esperar o processamento do serviço
            var relatorio = await _chamadaService.GerarRelatorioConsolidado(turmaId);

            if (relatorio == null)
                return NotFound(new { message = "Nenhum dado encontrado para esta turma." });

            return Ok(relatorio);
        }

        [HttpGet("turma/{turmaId}/data/{data}")]
        public async Task<IActionResult> ObterPorData(Guid turmaId, DateTime data)
        {
            var chama = await _chamadaService.ObterChamadaPorData(turmaId, data);

            if (chama == null)
                return NotFound(new { message = "Nenhum dado encontrado para esta turma." });

            return Ok(chama);
        }
    }
}