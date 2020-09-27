using Xunit;
using Payments.Api.Domain;
using FluentAssertions;
using AutoFixture;

namespace Payments.Api.Tests
{
    public class NewPaymentTests : TestBase
    {
        private NewPayment _newPayment;

        public NewPaymentTests()
        {
            _newPayment = Fixture.Create<NewPayment>();
        }

        [Fact]
        public void IsValid_ShouldBeTrue_WhenHasNoNullProperties()
        {            
            _newPayment.IsValid.Should().BeTrue();
        }

        [Fact]
        public void IsValid_ShouldBeFalse_WhenValueIsNull()
        {
            _newPayment.Value = null;
            _newPayment.IsValid.Should().BeFalse();
        }

        [Fact]
        public void IsValid_ShouldBeFalse_WhenNameIsNull()
        {
            _newPayment.Name = null;
            _newPayment.IsValid.Should().BeFalse();
        }

        [Fact]
        public void IsValid_ShouldBeFalse_WhenDueDateIsNull()
        {
            _newPayment.DueDate = null;
            _newPayment.IsValid.Should().BeFalse();
        }

        [Fact]
        public void IsValid_ShouldBeFalse_WhenPaymentDateIsNull()
        {
            _newPayment.PaymentDate = null;
            _newPayment.IsValid.Should().BeFalse();
        }        
    }
}
