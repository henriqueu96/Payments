using System;

namespace Payments.Api.Domain
{
    public class Payment
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Value { get; set; }        

        public decimal? Fine { get; set; }

        public decimal? Interest { get; set; }

        public DateTime PaymentDate { get; set; }

        public DateTime DueDate { get; set; }

        public Payment(string name, decimal value, DateTime paymentDate, DateTime dueDate, decimal? fine, decimal? interest)
        {
            Name = name;
            Value = value;
            PaymentDate = paymentDate;
            DueDate = dueDate;
            Fine = fine;
            Interest = interest;
        }
        
        public bool IsLate => DueDate.CompareTo(PaymentDate) < 0;

        public int DelayInDays => (PaymentDate - DueDate).Days;

        public decimal? GetAdjustedValue()
        {
            if (!IsLate)
            {
                return null;
            }
            
            return Value + (Value * (Interest * DelayInDays)) + (Value * Fine);
        }
    }
}