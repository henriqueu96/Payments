using System;
using AutoFixture;
using FluentAssertions;
using Payments.Api.Domain;
using Payments.Api.Services;
using Xunit;

namespace Payments.Api.Tests.Domain
{
    public class PaymentTests : TestBase
    {
        private Payment _payment;

        public PaymentTests()
        {
            _payment = Fixture.Create<Payment>();            
        }

        [Fact]
        public void IsLate_ShouldReturnTrue_WhenPaymentDateIsAfterDueDate()
        {
            _payment.PaymentDate = DateTime.Now.AddDays(1);
            _payment.DueDate = DateTime.Now;            

            _payment.IsLate.Should().BeTrue();
        }

        [Fact]
        public void IsLate_ShouldReturnFalse_WhenPaymentDateIsEqualDueDate()
        {
            _payment.PaymentDate = DateTime.Now;
            _payment.DueDate = DateTime.Now;                     

            _payment.IsLate.Should().BeFalse();
        }

        [Fact]
        public void IsLate_ShouldReturnFalse_WhenPaymentDateIsBeforeDueDate()
        {
            _payment.PaymentDate = DateTime.Now;
            _payment.DueDate = DateTime.Now.AddDays(1);

            _payment.IsLate.Should().BeFalse();
        }

        [Fact]
        public void DelayInDays_ShouldReturn1()
        {
            _payment.DueDate = DateTime.Today;
            _payment.PaymentDate = DateTime.Today.AddHours(25);

            _payment.DelayInDays.Should().Be(1);
        }

        [Fact]
        public void DelayInDays_ShouldReturn2()
        {
            _payment.DueDate = DateTime.Today;
            _payment.PaymentDate = DateTime
                .Today
                .AddDays(1)
                .AddHours(25);

            _payment.DelayInDays.Should().Be(2);
        }

        [Fact]
        public void GetAdjustedValue_ShouldReturn102_1()
        {
            _payment.Value = 100;
            _payment.DueDate = DateTime.Today;
            _payment.PaymentDate = DateTime.Today.AddDays(1);
            _payment.Interest = PaymentService.InterestForLessThanThreeDays;
            _payment.Fine = PaymentService.FineForLessThanThreeDays;

            var adjustedValue = _payment.GetAdjustedValue();
            adjustedValue.Should().Be(102.1M);
        }

        [Fact]
        public void GetAdjustedValue_ShouldReturn103_8()
        {
            _payment.Value = 100;
            _payment.DueDate = DateTime.Today;
            _payment.PaymentDate = DateTime.Today.AddDays(4);
            _payment.Interest = PaymentService.InterestForLessThanFiveDays;
            _payment.Fine = PaymentService.FineForLessThanFiveDays;

            var adjustedValue = _payment.GetAdjustedValue();
            adjustedValue.Should().Be(103.8M);
        }

        [Fact]
        public void GetAdjustedValue_ShouldReturn106_8()
        {
            _payment.Value = 100;
            _payment.DueDate = DateTime.Today;
            _payment.PaymentDate = DateTime.Today.AddDays(6);
            _payment.Interest = PaymentService.InterestForMoreThanFiveDay;
            _payment.Fine = PaymentService.FineForMoreThanFiveDays;

            var adjustedValue = _payment.GetAdjustedValue();
            adjustedValue.Should().Be(106.8M);
        }
    }
}
