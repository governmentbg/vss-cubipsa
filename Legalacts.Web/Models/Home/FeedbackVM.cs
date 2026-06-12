using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Legalacts.Web.Models
{
    public class FeedbackVM
    {
        [Required(ErrorMessage = "Избора на поле „Съобщение за“ е задължителен.")]
        public string Type { get; set; }

        [RegularExpression(@"^[а-яА-Яa-zA-Z ]*$", ErrorMessage = "Име и фамилия трябва да съдържат само букви.")]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "Електронната поща не трябва да надвишава 100 символа.")]
        [RegularExpression(@"^[\w\-!#$%&'*+/=?^`{|}~.""]+@([\w]+[.-]?)+[\w]\.[\w]+$", ErrorMessage = "Невалидна електронна поща.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Текстът в полето „Описание“ е задължителен.")]
        public string Body { get; set; }

        public string Captcha { get; set; }
    }
}