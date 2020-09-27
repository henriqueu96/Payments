using System.Threading.Tasks;
using Moq;
using Payments.Api.Domain;
using Payments.Api.Repositories;
using Payments.Api.Services;
using Xunit;
using AutoFixture;
using System;
using FluentAssertions;

namespace Payments.Api.Tests.Services
{
    public class PaymentServiceTests : TestBase
    {
        private Mock<IPaymentRepository> _paymentRepositoryMock;
        private PaymentService _paymentService;

        public PaymentServiceTests()
        {
            _paymentRepositoryMock = new Mock<IPaymentRepository>();
            _paymentService = new PaymentService(_paymentRepositoryMock.Object);
        }

        [Fact]
        public async Task SavePaymentAsync_ShouldCallPaymentRepositorySave()
        {
            var newPayment = Fixture.Create<NewPayment>();

            await _paymentService.SavePaymetAsync(newPayment);
            _paymentRepositoryMock.Verify(
                p => p.SaveAsync (
                    It.Is<Payment>(p=> p.Name == newPayment.Name)
               )
            );
        }

        [Fact]
        public void GetAjustedValue()
        {
            var newPayment = new NewPayment()
            {
                Name = "name",
                Value = 100,
                PaymentDate = DateTime.Now,
                DueDate = DateTime.Now.AddHours(25) 
            };

            var result = (decimal?) PrivateAccessHelper
                .InvokePrivateMethod("GetAdjustedValue", _paymentService, newPayment);

            result.Value.Should().Be(101M);
        }
    }
}
