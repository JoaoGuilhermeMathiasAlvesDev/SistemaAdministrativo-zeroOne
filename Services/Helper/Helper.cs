using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Helper
{
    public static class Helper
    {
        public static int CalulcarIdade(DateTime dataNascimento)
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - dataNascimento.Year;

            if (dataNascimento.Date > hoje.AddYears(-idade))
            {
                idade--;
            }

            return idade;
        }
    }
}
