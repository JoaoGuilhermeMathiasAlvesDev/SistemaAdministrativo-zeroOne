using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Model
{
    public class AdicionarChamadaModel
    {
        public Guid TurmaId { get; set; }
        public DateTime DataAula { get; set; }

        public List<AlunoPresencaEnvioModel> Alunos { get; set; } = new();

    }
}
