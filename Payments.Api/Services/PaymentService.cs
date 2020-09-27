using System.Collections.Generic;
using System.Threading.Tasks;
using Payments.Api.Domain;
using Payments.Api.Repositories;

namespace Payments.Api.Services
{
    public class PaymentService : IPaymentService
    {
        const decimal FineForLessThanThreeDays = 0.02M;
        const decimal FineForLessThanFiveDays = 0.03M;
        const decimal FineForMoreThanFiveDays = 0.05M;
        const decimal InterestForLessThanThreeDays = 0.0001M;
        const decimal InterestForLessThanFiveDays = 0.0002M;
        const decimal InterestForMoreThanFiveDay = 0.0003M;

        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public Task<IEnumerable<Payment>> GetPaymentsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Payment> SavePaymetAsync(NewPayment newPayment)
        {            
            var adjustedValue = GetAdjustedValue(newPayment);

            var payment = new Payment(
            newPayment.Name,
            newPayment.Value.Value,
            adjustedValue,
            newPayment.PaymentDate.Value,
            newPayment.DueDate.Value);

            return _paymentRepository.SaveAsync(payment);
        }

        private decimal? GetAdjustedValue(NewPayment newPayment)
        {
            if (!newPayment.IsLate)
            {
                return null;
            }

            var interest = GetDailyInterest(newPayment);
            var fine = GetFine(newPayment);

            var value = newPayment.Value;
            return value + (value * (interest * newPayment.DelayInDays)) + (value * fine);
        }

        private decimal? GetFine(NewPayment newPayment)
        {
            if (!newPayment.IsLate)
            {
                return null;
            }

            if (newPayment.DelayInDays <= 3)
            {
                return FineForLessThanThreeDays;
            }
            else if (newPayment.DelayInDays <= 5)
            {
                return FineForLessThanFiveDays;
            }
            else
            {
                return FineForMoreThanFiveDays;
            }
        }

        private decimal? GetDailyInterest(NewPayment newPayment)
        {
            if (!newPayment.IsLate)
            {
                return null;
            }

            if (newPayment.DelayInDays <= 3)
            {
                return InterestForLessThanThreeDays;
            }
            else if (newPayment.DelayInDays <= 5)
            {
                return InterestForLessThanFiveDays;
            }
            else
            {
                return InterestForMoreThanFiveDay;
            }
        }
    }
}