using System;
using Newtonsoft.Json;

namespace Payments.Api.Domain
{
    public class NewPayment
    {
        [JsonProperty("nome")]
        public string Name { get; set; }

        [JsonProperty("valorOriginal")]
        public decimal? Value { get; set; }

        [JsonProperty("dataPagamento")]
        public DateTime? PaymentDate { get; set; }

        [JsonProperty("dataVencimento")]
        public DateTime? DueDate { get; set; }

        public NewPayment()
        {
        }

        public int DelayInDays => (PaymentDate - DueDate)?.Days ?? 0;

        public bool IsLate => DueDate?.CompareTo(PaymentDate) < 0;

        public bool IsValid =>
                !string.IsNullOrEmpty(Name) &&
                Value != null &&
                PaymentDate != null &&
                DueDate != null;
    }
}