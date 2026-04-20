using Azure.Core;
using BCrypt.Net;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using Repositorio.IRepository;
using Services.IServices;
using Services.Model;
using Services.Services.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly IUsuarioRepository _repository;
        private readonly IWhatsAppServices _whatsAppServices;
        private readonly IConfiguration _config;
        private readonly AuthService _authService;
        private readonly GerarToken _gerarToken;

        public UsuarioServices(IUsuarioRepository repository, IConfiguration config, AuthService authService, GerarToken gerarToken, IWhatsAppServices whatsAppServices)
        {
            _repository = repository;
            _config = config;
            _authService = authService;
            _gerarToken = gerarToken;
            _whatsAppServices = whatsAppServices;
        }

        public async Task<bool> Cadastrar(CriarUsuarioRequest request)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(request.Senha);

            var novoUsuario = new Usuario(request.Username, hash, request.Role);

            await _repository.Adicionar(novoUsuario);

            return await _repository.SalvarAlteracoes();
        }

        public async Task<bool> EsqueciSenha(string usuario, string email)
        {
            var user = await _repository.ObterPorUsername(usuario);
            var senharnova = 123456;

            var hash = BCrypt.Net.BCrypt.HashPassword(senharnova.ToString());

            user.AltearSenha(hash);

            _repository.Atualizar(user);
            var salvo = await _repository.SalvarAlteracoes();

            var assunto = "Altração de Senha na Plataforma Zero One!";
            var mensagem = $"*ZeroOne - Recuperação de Acesso*\n\n" +
                       $"Olá {user.Username}, sua nova senha é: *{senharnova}*\n\n" +
                       $"_Por favor, altere sua senha assim que realizar o login._";

            await _whatsAppServices.EnviarEmail(email,assunto, mensagem);

            return salvo;
        }

        public async Task<LoginResponse?> Login(LoginRequest request)
        {
            var user = await _repository.ObterPorUsername(request.Username);

            if (user == null || !_authService.VerificarSenha(request.Senha, user.SenhaHash))
                return null;

            var usuario = new LoginResponse
            {
                Username = request.Username,
                Role = ((int)user.Role),
                Token = _gerarToken.GerarTokenJwt(user)
            };
            return usuario;
        }

        public async Task<List<UsuarioModel>> ObterTodos()
        {
            var resultado = await _repository.ObterTodos();

            var retorno = resultado.Select(u => new UsuarioModel
            {
                Id = u.Id,
                Username = u.Username,
                Role = u.Role.ToString(),
            }).ToList();

            return retorno;
        }

        public async Task<bool> ResetarSenha(ResetSenhaRequest request)
        {
            var user = await _repository.ObterPorUsername(request.username);

            var hash = BCrypt.Net.BCrypt.HashPassword(request.senha.ToString());

            user.AltearSenha(hash);

            _repository.Atualizar(user);

            return await _repository.SalvarAlteracoes();
        }
    }
}
