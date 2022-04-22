using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CheckoutPaymentGateway.Service;
using CheckoutPaymentGateway.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace CheckOutPaymentGateway.API.Controllers
{

    /// <summary>
    /// API controller that exposes endpoints for a merchant to accept payments from their customers
    /// </summary>
    [ApiController]
    [Route("api/payment")]
    [Produces("application/json")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService paymentService;
        public readonly IMapper mapper;

        public PaymentController(IPaymentService paymentService, IMapper mapper)
        {
            this.paymentService = paymentService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Retreive the details of a previously made payment
        /// </summary>
        /// <param name="id">The unique identifier of the payment</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PaymentResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Authorize]
        public async Task<IActionResult> Get(Guid id)
        {
            // fetch the payment from the service
            var existingPayment = await paymentService.GetPaymentInfo(id);

            // if payment not found
            if (existingPayment == null)
            {
                // return a not found error
                return StatusCode((int)HttpStatusCode.NotFound);
            }

            // map from payment to payment response
            var response = mapper.Map<PaymentResponse>(existingPayment);

            return StatusCode((int)HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Process a merchant payment transaction
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="paymentRequest">The payment request details</param>
        /// <response code="201">Returns the newly processed payment item</response>
        /// <response code="400">If the payment request contains invalid properties</response>
        /// <response code="409">If the request id is a duplicate of a previous request</response>
        /// <response code="500">An unknown server error occurred</response>
        /// <returns>The transaction response along with the status</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] PaymentRequest paymentRequest)
        {
            try
            {
                // validate that input model state
                if (!ModelState.IsValid)
                {
                    // we've seen this request before, return a conflict response
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

                // check if we've tried to process same request before
                var existingPayment = await paymentService.GetPaymentInfo(paymentRequest.Id);


                if(existingPayment != null)
                {
                    // we've seen this request before, return a conflict response
                    return StatusCode(StatusCodes.Status409Conflict);
                }

                var processedPayment = await paymentService.ProcessPayment(paymentRequest);

                // if the transaction didn't succeed, then report errors
                if(processedPayment.Status != TransactionStatus.Accepted)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, processedPayment.ExternalComment);
                }
                var response = mapper.Map<PaymentResponse>(processedPayment);

                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (Exception ex)
            {
                // process the exception and return HTTP 500
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
    }
}
