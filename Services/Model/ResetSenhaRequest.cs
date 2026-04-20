using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Model
{
    public record ResetSenhaRequest
    {
        public string username { get; set; }

        public string senha { get; set; }
    }
}
