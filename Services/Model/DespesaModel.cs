using Dominio.Entidades.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Services.Model
{
    public class DespesaModel
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public bool Pago { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CategoriaDespesa Categoria { get; set; }
    }
}
