using System;

namespace Payments.Api.Domain
{
    public class Payment
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Value { get; set; }

        public decimal AdjustedValue { get; set; }

        public DateTime PaymentDate { get; set; }

        public DateTime DueDate { get; set; }

        public Payment(string name, decimal value, decimal? adjustedValue, DateTime paymentDate, DateTime dueDate)
        {
            Name = name;
            Value = value;
            PaymentDate = paymentDate;
            DueDate = dueDate;
        }
        
        public bool IsLate => DueDate.CompareTo(PaymentDate) < 0;

        public int DelayInDays => (PaymentDate - DueDate).Days;                
    }
}