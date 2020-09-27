using System.Threading.Tasks;
using Moq;
using Payments.Api.Domain;
using Payments.Api.Repositories;
using Payments.Api.Services;
using Xunit;
using AutoFixture;
using System;

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
                p => p.SaveAsync(
                    It.Is<Payment>(p => p.Name == newPayment.Name)
               )
            );
        }

        [Fact]
        public async Task SavePayment_ShouldSetPaymentFineT0_02()
        {
            var newPayment = new NewPayment
            {
                Name = "Teste",
                Value = 100,
                DueDate = DateTime.Today,
                PaymentDate = DateTime.Today.AddDays(1)
            };

            await _paymentService.SavePaymetAsync(newPayment);
            _paymentRepositoryMock.Verify(
                p => p.SaveAsync(
                    It.Is<Payment>(p => p.Fine == PaymentService.FineForLessThanThreeDays)
               )
            );
        }

        [Fact]
        public async Task SavePayment_ShouldSetPaymentFineT0_03()
        {
            var newPayment = new NewPayment
            {
                Name = "Teste",
                Value = 100,
                DueDate = DateTime.Today,
                PaymentDate = DateTime.Today.AddDays(5)
            };

            await _paymentService.SavePaymetAsync(newPayment);
            _paymentRepositoryMock.Verify(
                p => p.SaveAsync(
                    It.Is<Payment>(p => p.Fine == PaymentService.FineForLessThanFiveDays)
               )
            );
        }

        [Fact]
        public async Task SavePayment_ShouldSetPaymentFineT0_05()
        {
            var newPayment = new NewPayment
            {
                Name = "Teste",
                Value = 100,
                DueDate = DateTime.Today,
                PaymentDate = DateTime.Today.AddDays(6)
            };

            await _paymentService.SavePaymetAsync(newPayment);
            _paymentRepositoryMock.Verify(
                p => p.SaveAsync(
                    It.Is<Payment>(p => p.Fine == PaymentService.FineForMoreThanFiveDays)
               )
            );
        }

        [Fact]
        public async Task SavePayment_ShouldSetPaymentInterestTo0_001()
        {
            var newPayment = new NewPayment
            {
                Name = "Teste",
                Value = 100,
                DueDate = DateTime.Today,
                PaymentDate = DateTime.Today.AddDays(1)
            };

            await _paymentService.SavePaymetAsync(newPayment);
            _paymentRepositoryMock.Verify(
                p => p.SaveAsync(
                    It.Is<Payment>(p => p.Interest == PaymentService.InterestForLessThanThreeDays)
               )
            );
        }

        [Fact]
        public async Task SavePayment_ShouldSetPaymentInterestTo0_002()
        {
            var newPayment = new NewPayment
            {
                Name = "Teste",
                Value = 100,
                DueDate = DateTime.Today,
                PaymentDate = DateTime.Today.AddDays(4)
            };

            await _paymentService.SavePaymetAsync(newPayment);
            _paymentRepositoryMock.Verify(
                p => p.SaveAsync(
                    It.Is<Payment>(p => p.Interest == PaymentService.InterestForLessThanFiveDays)
               )
            );
        }

        [Fact]
        public async Task SavePayment_ShouldSetPaymentInterestTo0_003()
        {
            var newPayment = new NewPayment
            {
                Name = "Teste",
                Value = 100,
                DueDate = DateTime.Today,
                PaymentDate = DateTime.Today.AddDays(6)
            };

            await _paymentService.SavePaymetAsync(newPayment);
            _paymentRepositoryMock.Verify(
                p => p.SaveAsync(
                    It.Is<Payment>(p => p.Interest == PaymentService.InterestForMoreThanFiveDay)
               )
            );
        }
    }
}