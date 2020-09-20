using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZoomIntegrationMicroservice.Models;
using ZoomIntegrationMicroservice.Services;

namespace ZoomIntegrationMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailServiceController : ControllerBase
    {
        private readonly IMailService _email;

        public EmailServiceController(IMailService email)
        {
            _email = email;
        }
        [HttpPost]
        [Route("send-email")]
        public bool SendEmail(EmailModel email) {
            bool result = _email.SendEmail(email);
            return result;
        }

    }
}
