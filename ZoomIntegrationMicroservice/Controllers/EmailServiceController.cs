using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public readonly IFileUpload _file;

        public EmailServiceController(IMailService email,IFileUpload file)
        {
            _email = email;
            _file = file;
        }
        [HttpPost]
        [Route("send-email")]
        public bool SendEmail(EmailModel email) {
            bool result = _email.SendEmail(email);
            return result;
        }
        #region FileUploadFunction
        [HttpPost]
        [Route("file-upload")]
        public async Task<JsonResult> FileUpload(List<IFormFile> files)
        {
            var result = await _file.UploadFile(files);
            return result;
        }
        #endregion
    }
}
