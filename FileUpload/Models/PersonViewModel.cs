using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileUpload.Models
{
    public class PersonViewModel
    {
        [Required]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$",
        ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public string FilePath { get; set; }

        [Required]
        public HttpPostedFileBase uploadFile { get; set; }

    } 
}