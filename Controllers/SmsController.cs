﻿using EverythingSucks.Services;
using Microsoft.AspNetCore.Mvc;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace EverythingSucks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SmsController : ControllerBase
    {
        private readonly ITwilioRestClient _client;

        public SmsController(ITwilioRestClient client)
        {
            _client = client;
        }

        [HttpPost]
        [HttpPost("send-sms")]
        public async Task<IActionResult> SendSms(SmsMessage model)
        {
            var message = MessageResource.Create(
                to: new PhoneNumber(model.To),
                from: new PhoneNumber(model.From),
                body: model.Message,
                client: _client);
            return Ok("Success" + message.Sid);
        }
    }
}
