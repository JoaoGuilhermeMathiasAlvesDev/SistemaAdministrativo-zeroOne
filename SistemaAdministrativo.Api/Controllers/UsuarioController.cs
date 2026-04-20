using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositorio.IRepository;
using Services.IServices;
using Services.Model;

namespace SistemaAdministrativo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioServices _usuarioService;

        public UsuarioController(IUsuarioServices usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [Authorize(Roles = "Admin,RH")]
        [HttpPost("Criar-Usuario")]
        public async Task<IActionResult> CriarUsuario(CriarUsuarioRequest usuario)
        {
            var criandoUsuario = await _usuarioService.Cadastrar(usuario);

            if(!criandoUsuario)
                return NotFound(new { message ="Correu um problema, Tente novamente ou Liga para Suporte." });

            return Ok(new  { message= "Usuario criado com Sucesso!" });
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest usuario)
        {
            var loginResult = await _usuarioService.Login(usuario);

            if (loginResult == null)
                return Unauthorized(new { message = "Sua Senha ou seu usuário está incorreto." });

            return loginResult != null ? Ok(loginResult) : Unauthorized(new { message = "Usuário ou senha incorretos." });
        }

        [AllowAnonymous]
        [HttpPost("esqueci-senha")]
        public async Task<IActionResult> EsqueciSenha([FromQuery] string usuario, string email)
        {
            var resultado = await _usuarioService.EsqueciSenha(usuario, email);

            return Ok(new { message = "Se o usuário existir, o mennsagem foi enviado." });
        }

        [AllowAnonymous]
        [HttpPost("resetar-senha")]
        public async Task<IActionResult>ResetarSenha([FromBody]ResetSenhaRequest usuario)
        {
            var successea = await _usuarioService.ResetarSenha(usuario);
            return successea ? Ok(new { message = "Senha Alterada !" }  ) : BadRequest(new { message = "Erro ao resetar." });
        }

        [AllowAnonymous]
        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterTodos()
        {
            var sucess = await _usuarioService.ObterTodos();
            if(sucess == null) 
                return Ok(new { message = "Não Usuario Cadastrado" });

            return Ok(sucess);
        }

    }
}
