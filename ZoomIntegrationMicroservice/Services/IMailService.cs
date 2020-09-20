using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZoomIntegrationMicroservice.Models;

namespace ZoomIntegrationMicroservice.Services
{
    public interface IMailService
    {
        bool SendEmail(EmailModel email);
    }
}
