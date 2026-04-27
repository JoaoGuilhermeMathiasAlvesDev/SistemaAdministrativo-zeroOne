using Microsoft.AspNetCore.Mvc;
using Services.IServices;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAdministrativo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinanceiroController : ControllerBase
    {
        private readonly IFinanceiroServices _financeiroService;
        private readonly IMensalidadeServices _mensalidadeService;

        public FinanceiroController(IFinanceiroServices financeiroService, IMensalidadeServices mensalidadeService)
        {
            _financeiroService = financeiroService;
            _mensalidadeService = mensalidadeService;
        }

        #region Balancete e Despesas

        [HttpGet("balancete")]
        public async Task<ActionResult<BalanceteModel>> GetBalancete([FromQuery] int mes, [FromQuery] int ano)
        {
            var balancete = await _financeiroService.GerarBalancete(mes, ano);
            return Ok(balancete);
        }

        [HttpPost("despesa")]
        public async Task<IActionResult> CriarDespesa([FromBody] DespesaModel model)
        {
            var sucesso = await _financeiroService.RegistrarDespesa(model);
            if (!sucesso) return BadRequest("Não foi possível registrar a despesa.");
            return Ok();
        }

        #endregion

        #region Gestão de Mensalidades (Receitas)

        [HttpGet("mensalidades")]
        public async Task<ActionResult<IEnumerable<MensalidadeModel>>> ListarTodasMensalidades()
        {
            return Ok(await _mensalidadeService.ObterTodos());
        }

        [HttpGet("mensalidades/aluno/{alunoId:guid}")]
        public async Task<ActionResult<IEnumerable<MensalidadeModel>>> ListarPorAluno(Guid alunoId)
        {
            var resultado = (await _mensalidadeService.ListarPorAluno(alunoId));

            if (resultado == null) return NotFound("Nenhuma mensalidade encontrada para este aluno.");

            return Ok(resultado);
        }

        [HttpGet("mensalidades/vencidas")]
        public async Task<ActionResult<IEnumerable<MensalidadeModel>>> ListarVencidas()
        {
            return Ok(await _mensalidadeService.ListarVencidas());
        }

        [HttpPost("mensalidade/gerar")]
        public async Task<ActionResult<MensalidadeAtualizarAdicionarModel>> GerarMensalidade(Guid alunoId, decimal valor, DateTime vencimento)
        {
            var resultado = await _mensalidadeService.GerarMensalidade(alunoId, valor, vencimento);
            return Ok(resultado);
        }

        [HttpPatch("mensalidade/{id:guid}/pagar")]
        public async Task<IActionResult> PagarMensalidade(Guid id)
        {
            // Aqui você pode usar o FinanceiroService (para baixar com lógica contábil)
            // ou o MensalidadeService (para apenas mudar o status).
            var sucesso = await _financeiroService.BaixarMensalidade(id);
            if (!sucesso) return BadRequest("Erro ao processar pagamento.");
            return Ok();
        }

        [HttpPatch("mensalidade/{id:guid}/prorrogar")]
        public async Task<IActionResult> ProrrogarVencimento(Guid id, [FromBody] DateTime novaData)
        {
            await _mensalidadeService.ProrrogarVencimento(id, novaData);
            return Ok();
        }

        [HttpDelete("mensalidade/{id:guid}")]
        public async Task<IActionResult> ExcluirMensalidade(Guid id)
        {
            var sucesso = await _mensalidadeService.ExcluirMensalidade(id);
            return sucesso ? Ok() : BadRequest("Erro ao excluir mensalidade.");
        }

        [HttpGet("Obter-Despesa")]
        public async Task<IActionResult> ObterDispesa([FromQuery] int mes, [FromQuery] int ano)
        {
            var resultado = await _financeiroService.ObterDispesar(mes, ano);
            return Ok(resultado);
        }

        [HttpPatch("MudarStatus-Mensalida/{id:guid}")]
        public async Task<IActionResult> MudarStatus(Guid id, int status)
        {
            if (id == Guid.Empty)
                return BadRequest(new { message = "Precisa da uma Parcela para alter o Status." });

            await _mensalidadeService.MudarStatusPagamento(id, status);

            return Ok(new { message = "Status da Mensalidade Atualizado" });
        }

        [HttpDelete("despesas/{id:Guid}")]
        public async Task<IActionResult> RemoverDespesa(Guid id)
        {
           var salvo = await _financeiroService.RemoverDespesa(id);

            if (salvo == false)
                return BadRequest(new { menssagem = "Erro ao remove uma dispesa" });
            return Ok(new { menssagem = "Removido com Sucesso!" });
        }
        #endregion
    }
}