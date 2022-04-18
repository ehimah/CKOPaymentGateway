﻿using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;


namespace CheckOutPaymentGateway.API.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : Controller
    {

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var response = new { Id = id };
            return StatusCode((int)HttpStatusCode.OK, response);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] string value)
        {
            return StatusCode((int)HttpStatusCode.OK, value);
        }
    }
}
