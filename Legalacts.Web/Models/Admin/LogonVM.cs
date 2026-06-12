using System.ComponentModel.DataAnnotations;

namespace Legalacts.Web.Models
{
    public class LogonVM
    {
        public static string MessageCaptcha = "Невалиден код за сигурност.";
        public static string MessageUserExists = "В системата няма регистриран потребител с посоченото потребителско име и/или парола.";

        [Required(ErrorMessage = "Полето \"Потребителско име\" е задължително.")]
        [StringLength(200, ErrorMessage = "Полето \"Потребителско име\" не може да надвишава 200 символа.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Полето \"Парола\" е задължително.")]
        [StringLength(200, ErrorMessage = "Полето \"Парола\" не може да надвишава 200 символа.")]
        public string Password { get; set; }
        public string Phone { get; set; }
    }
}
