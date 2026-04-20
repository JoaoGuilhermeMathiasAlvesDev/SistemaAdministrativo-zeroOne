using System;
using System.Collections.Generic;
using System.Text;

namespace Services.IServices
{
    public interface IWhatsAppServices
    {
        Task<bool> EnviarMensagemAsync(string telefone, string mensagem);
        Task EnviarEmail(string destinatario, string assunto, string mensagem);
    }
}
