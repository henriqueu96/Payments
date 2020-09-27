using System.Collections.Generic;
using System.Threading.Tasks;
using Payments.Api.Domain;

namespace Payments.Api.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> SaveAsync(Payment payment);
        Task<IEnumerable<Payment>> GetPaymentsAsync();
    }
}
