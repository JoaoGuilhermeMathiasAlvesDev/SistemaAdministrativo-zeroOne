using Microsoft.Extensions.Configuration;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Services.Services
{
    public class WhatsAppServices : IWhatsAppServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public WhatsAppServices(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task EnviarEmail(string destinatario, string assunto, string mensagem)
        {

            var emailRemetente = _config["EmailSettings:Email"];
            var senhaApp = _config["EmailSettings:Senha"];
            var smtpHost = _config["EmailSettings:Smtp"];
            bool portaValida = int.TryParse(_config["EmailSettings:Porta"], out int porta);
            if (!portaValida) porta = 587;

            var smtpClient = new SmtpClient(smtpHost)
            {
                Port = porta,
                Credentials = new NetworkCredential(emailRemetente,senhaApp),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailRemetente),
                Subject = assunto,
                Body = mensagem,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(destinatario);
            await smtpClient.SendMailAsync(mailMessage);
        }

        public async Task<bool> EnviarMensagemAsync(string telefone, string mensagem)
        {
            
            var numeroLimpo = new string(telefone.Where(char.IsDigit).ToArray());

            
            var mensagemUrl = HttpUtility.UrlEncode(mensagem);


            var url = $"https://api.whatsapp.com/send?phone={numeroLimpo}&text={mensagemUrl}";

            var response = await _httpClient.PostAsync(url, null);
            return response.IsSuccessStatusCode ;
        }
    }
}
