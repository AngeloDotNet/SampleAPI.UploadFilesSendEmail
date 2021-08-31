using System;
using System.Threading.Tasks;
using API_UploadFiles_SendEmail.Models.InputModels;
using API_UploadFiles_SendEmail.Models.Services.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API_UploadFiles_SendEmail.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> logger;
        private readonly IEmailSenderService emailService;

        public EmailController(ILogger<EmailController> logger, IEmailSenderService emailService)
        {
            this.logger = logger;
            this.emailService = emailService;
        }

        [HttpGet("Welcome")]
        public IActionResult Welcome()
        {
            return Ok(string.Concat("Ciao sono le ore: ", DateTime.Now.ToLongTimeString()));
        }

        [HttpPost("InvioEmail")]
        public async Task<IActionResult> InvioEmail([FromForm] InputMailSender model)
        {
            try
            {
                await emailService.SendEmailAsync(model);
                return Ok();
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}