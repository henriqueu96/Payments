
using System.Collections.Generic;
using System.Threading.Tasks;
using Payments.Api.Domain;

namespace Payments.Api.Services
{
    public interface IPaymentService
    {
        Task<PaymentVisualization> SavePaymetAsync(NewPayment newPaymentModel);
        Task<IEnumerable<PaymentVisualization>> GetPaymentsAsync();
    }
}