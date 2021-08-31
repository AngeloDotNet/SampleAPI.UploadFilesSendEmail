using System;
using System.Threading.Tasks;
using API_UploadFiles_SendEmail.Models.InputModels;
using API_UploadFiles_SendEmail.Models.Services.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API_UploadFiles_SendEmail.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly ILogger<UploadController> logger;
        private readonly IUploadFilesService filesService;
        private readonly IWebHostEnvironment env;

        public UploadController(ILogger<UploadController> logger, IUploadFilesService filesService, IWebHostEnvironment env)
        {
            this.logger = logger;
            this.filesService = filesService;
            this.env = env;
        }
        
        [HttpGet("Welcome")]
        public IActionResult Welcome()
        {
            return Ok(string.Concat("Ciao sono le ore: ", DateTime.Now.ToLongTimeString()));
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> UploadFiles([FromForm] InputUploadFile model)
        {
            try
            {
                await filesService.UploadFileAsync(model, env);
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}