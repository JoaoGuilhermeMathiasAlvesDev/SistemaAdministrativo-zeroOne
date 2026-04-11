using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Model
{
    public class AlunoPresencaEnvioModel
    {
        public Guid AlunoId { get; set; }
        public bool Presente { get; set; }
        public string Observacao { get; set; } = string.Empty;
    }
}
