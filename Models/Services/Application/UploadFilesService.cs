using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using API_UploadFiles_SendEmail.Models.InputModels;
using API_UploadFiles_SendEmail.Models.Services.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API_UploadFiles_SendEmail.Models.Services.Application
{
    public class UploadFilesService : IUploadFilesService
    {
        private readonly ILogger<UploadFilesService> logger;
        
        public UploadFilesService(ILogger<UploadFilesService> logger)
        {
            this.logger = logger;
        }

        public async Task UploadFileAsync([FromForm] InputUploadFile model, [FromServices] IWebHostEnvironment env)
        {

            List<IFormFile> documenti = model.documenti;

            string fileFolder = Path.Combine(Path.Combine(env.ContentRootPath, "upload"), Path.Combine(DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM")));
            
            if (documenti.Count != 0)
            {
                if (!Directory.Exists(fileFolder))
                {
                    Directory.CreateDirectory(fileFolder);
                }

                foreach (IFormFile docs in documenti)
                {
                    double fileSizeKB = Math.Round((double) docs.Length / 1024);

                    string pathSaveDoc = Path.Combine(fileFolder, docs.FileName);   
                    
                    string fileName = docs.FileName;                                
                    string fileSize = string.Join(" ", fileSizeKB.ToString(), "KB");
                    string fileDesc = model.descrizione;                            

                    using var fileStream = System.IO.File.OpenWrite(pathSaveDoc);

                    await docs.CopyToAsync(fileStream);
                }
            }
        }
    }
}