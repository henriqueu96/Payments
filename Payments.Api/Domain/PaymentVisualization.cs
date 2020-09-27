using System;
using Newtonsoft.Json;

namespace Payments.Api.Domain
{
    public class PaymentVisualization
    {
        [JsonProperty("nome")]
        public string Name { get; set; }
        [JsonProperty("valor")]
        public decimal Value { get; set; }
        [JsonProperty("valorCorrigido")]
        public decimal? AdjustedValue { get; set; }
        [JsonProperty("dataPagamento")]
        public DateTime PaymentDate { get; set; }
        [JsonProperty("dataVencimento")]
        public DateTime DueDate { get; set; }
    }
}
