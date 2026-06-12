using System.ComponentModel.DataAnnotations;

namespace Legalacts.Web.Models
{
    public class ChangePasswordVM
    {
        public static string MessageChangeSuccess = "Вашата парола е сменена.";
        public static string MessageWrongCurrentPassword = "Грешна настояща парола.";

        [Required(ErrorMessage = "Полето \"Текуща парола\" е задължително.")]
        [StringLength(200, ErrorMessage = "Полето \"Текуща парола\" не може да надвишава 200 символа.")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Полето \"Нова парола\" е задължително.")]
        [MinLength(6, ErrorMessage = "Минималната дължина на полето \"Нова парола\" е 6 символа.")]
        [StringLength(200, ErrorMessage = "Полето \"Нова парола\" не може да надвишава 200 символа.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Полето \"Повтори новата парола\" е задължително.")]
        [StringLength(200, ErrorMessage = "Полето \"Повтори новата парола\" не може да надвишава 200 символа.")]
        [Compare("NewPassword", ErrorMessage = "Полетата \"Нова парола\" и \"Повтори новата парола\" не съвпадат.")]
        public string ConfirmPassword { get; set; }
    }
}
