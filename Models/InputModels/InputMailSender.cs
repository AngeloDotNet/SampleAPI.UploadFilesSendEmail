using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace API_UploadFiles_SendEmail.Models.InputModels
{
    public class InputMailSender
    {
        [Required(ErrorMessage = "L'indirizzo email è obbligatorio"), EmailAddress, Display(Name = "Destinatario")]
        public string recipientEmail { get; set; }

        [Required(ErrorMessage = "L'oggetto è obbligatorio"), Display(Name = "Oggetto")]
        public string subject { get; set; }

        [Required(ErrorMessage = "Il messaggio è obbligatorio"), Display(Name = "Messaggio")]
        public string htmlMessage { get; set; }

        [Display(Name = "Allegato/i")]
        public List<IFormFile> attachments { get; set; }
    }
}