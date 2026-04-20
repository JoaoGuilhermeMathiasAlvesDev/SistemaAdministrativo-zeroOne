using Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.IServices
{
    public interface IUsuarioServices
    {
        Task<LoginResponse?> Login(LoginRequest request);

        Task<List<UsuarioModel>> ObterTodos();
        Task<bool> Cadastrar(CriarUsuarioRequest request);
        Task<bool> EsqueciSenha(string usuario,string email);
        Task<bool> ResetarSenha(ResetSenhaRequest request);
    }
}
