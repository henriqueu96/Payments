using System;
using AutoFixture;
using FluentAssertions;
using Payments.Api.Domain;
using Xunit;

namespace Payments.Api.Tests.Domain
{
    public class PaymentTests : TestBase
    {
        private Payment Payment;

        public PaymentTests()
        {
            Payment = Fixture.Create<Payment>();            
        }

        [Fact]
        public void IsLate_ShouldReturnTrue_WhenPaymentDateIsAfterDueDate()
        {
            Payment.PaymentDate = DateTime.Now.AddDays(1);
            Payment.DueDate = DateTime.Now;            

            Payment.IsLate.Should().BeTrue();
        }

        [Fact]
        public void IsLate_ShouldReturnFalse_WhenPaymentDateIsEqualDueDate()
        {
            Payment.PaymentDate = DateTime.Now;
            Payment.DueDate = DateTime.Now;                     

            Payment.IsLate.Should().BeFalse();
        }

        [Fact]
        public void IsLate_ShouldReturnFalse_WhenPaymentDateIsBeforeDueDate()
        {
            Payment.PaymentDate = DateTime.Now;
            Payment.DueDate = DateTime.Now.AddDays(1);

            Payment.IsLate.Should().BeFalse();
        }

        [Fact]
        public void DelayInDays_ShouldReturn1()
        {
            Payment.DueDate = DateTime.Today;
            Payment.PaymentDate = DateTime.Today.AddHours(25);

            Payment.DelayInDays.Should().Be(1);
        }

        [Fact]
        public void DelayInDays_ShouldReturn2()
        {
            Payment.DueDate = DateTime.Today;
            Payment.PaymentDate = DateTime
                .Today
                .AddDays(1)
                .AddHours(25);

            Payment.DelayInDays.Should().Be(2);
        }
    }
}
