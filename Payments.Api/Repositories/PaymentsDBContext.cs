using System;
using Microsoft.EntityFrameworkCore;
using Payments.Api.Domain;

namespace Payments.Api.Repositories
{
    public class PaymentsDBContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }

        public PaymentsDBContext
            (DbContextOptions<PaymentsDBContext> options)
            : base(options)
        {           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Payment>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        }
    }
}
