using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace API_UploadFiles_SendEmail.Models.InputModels
{
    public class InputUploadFile
    {
        [Required(ErrorMessage = "La descrizione è obbligatoria"), Display(Name = "Descrizione")]
        public string descrizione { get; set; }

        [Required(ErrorMessage = "Aggiungere almeno un documento"), Display(Name = "Documenti")]
        public List<IFormFile> documenti { get; set; }
    }
}