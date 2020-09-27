using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Payments.Api.Domain;

namespace Payments.Api.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentsDBContext _dbContext;

        public PaymentRepository(PaymentsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsAsync()
        {
            return await _dbContext.Payments.ToListAsync();
        }        

        public async Task SaveAsync(Payment payment)
        {
            await _dbContext.AddAsync(payment);
            await _dbContext.SaveChangesAsync();         
        }
    }
}
