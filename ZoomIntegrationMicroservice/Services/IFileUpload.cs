using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZoomIntegrationMicroservice.Services
{
    public interface IFileUpload
    {
        Task<JsonResult> UploadFile(List<IFormFile> files);
    }
}
