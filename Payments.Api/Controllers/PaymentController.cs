using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payments.Api.Domain;
using Payments.Api.Services;

namespace Payments.Api.Controllers
{
    [Route("payment")]
    public class PaymentController : ControllerBase
    {
        IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> SavePaymentAsync([FromBody]NewPayment newPayment)
        {
            if (newPayment == null || !newPayment.IsValid)
            {
                return BadRequest();
            }

            var payment = await _paymentService.SavePaymetAsync(newPayment);
            return Ok(payment);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentsAsync()
        {
            var payments = await _paymentService.GetPaymentsAsync();
            return Ok(payments);
        }
    }
}