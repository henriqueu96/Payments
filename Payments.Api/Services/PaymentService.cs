using System.Collections.Generic;
using System.Threading.Tasks;
using Payments.Api.Domain;
using Payments.Api.Repositories;
using System.Linq;

namespace Payments.Api.Services
{
    public class PaymentService : IPaymentService
    {
        public const decimal FineForLessThanThreeDays = 0.02M;
        public const decimal FineForLessThanFiveDays = 0.03M;
        public const decimal FineForMoreThanFiveDays = 0.05M;
        public const decimal InterestForLessThanThreeDays = 0.001M;
        public const decimal InterestForLessThanFiveDays = 0.002M;
        public const decimal InterestForMoreThanFiveDay = 0.003M;

        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<PaymentVisualization>> GetPaymentsAsync()
        {
            var payments = await _paymentRepository.GetPaymentsAsync();
            return payments.Select(p => GetPaymentVisualization(p));
        }

        public async Task<PaymentVisualization> SavePaymetAsync(NewPayment newPayment)
        {
            var fine = GetFine(newPayment);
            var interest = GetInterest(newPayment);

            var payment = new Payment
            (
                newPayment.Name,
                newPayment.Value.Value,            
                newPayment.PaymentDate.Value,
                newPayment.DueDate.Value,
                fine,
                interest
            );

            await _paymentRepository.SaveAsync(payment);
            return GetPaymentVisualization(payment);
        }

        private PaymentVisualization GetPaymentVisualization(Payment payment)
        {
            return new PaymentVisualization()
            {
                Name = payment.Name,
                Value = payment.Value,
                AdjustedValue = payment.GetAdjustedValue(),
                DueDate = payment.DueDate,
                PaymentDate = payment.PaymentDate
            };
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

        private decimal? GetInterest(NewPayment newPayment)
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