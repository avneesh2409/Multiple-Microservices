using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ZoomIntegrationMicroservice.Services
{
    public class FileUploadRepo:IFileUpload
    {
        public async Task<JsonResult> UploadFile(List<IFormFile> files) {
            if (files.Count > 0)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload");
                Dictionary<string, string> filesPath = new Dictionary<string, string>();
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                foreach (IFormFile file in files)
                {
                    string filename = Guid.NewGuid().ToString() + '_' + file.FileName;
                    string filePath = Path.Combine(path, filename);
                    filesPath.Add(file.FileName, "/upload/" + filename);
                    FileStream fs = File.Create(filePath);
                    await file.CopyToAsync(fs);
                }
                return new JsonResult(new { status = true, filepath = filesPath });
            }
            else
            {
                return new JsonResult(new { status = false, error = "unable to upload" });
            }
        }
        
    }
}
