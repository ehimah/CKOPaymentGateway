using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CheckoutPaymentGateway.Service;
using CheckoutPaymentGateway.Service.Models;
using CheckOutPaymentGateway.API.Dto;
using Microsoft.AspNetCore.Mvc;


namespace CheckOutPaymentGateway.API.Controllers
{
    

    [ApiController]
    [Route("api/payment")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService paymentService;
        public readonly IMapper mapper;

        public PaymentController(IPaymentService paymentService, IMapper mapper)
        {
            this.paymentService = paymentService;
            this.mapper = mapper;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = new { Id = id };

            return StatusCode((int)HttpStatusCode.OK, response);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentRequestDto paymentRequestDto)
        {
            try
            {
                // check if we've tried to process same request before
                var existingPayment = await paymentService.GetPaymentInfo(paymentRequestDto.Id);


                if(existingPayment != null)
                {
                    // we've seen this request before, return a conflict response
                    return StatusCode((int)HttpStatusCode.Conflict);
                }
                var paymentRequet = this.mapper.Map<PaymentRequest>(paymentRequestDto);

                var response = await paymentService.ProcessPayment(paymentRequet);

                if(response == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (Exception ex)
            {
                // process the exception and return HTTP 500
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            
        }
    }
}
