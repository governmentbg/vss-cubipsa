using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Legalacts.Model.Entities;
using System.ComponentModel.DataAnnotations;
using Legalacts.Utils.Managers;

namespace Legalacts.Web.Models
{
    public class SearchVM
    {
        public static string MessageCaptcha = "Невалиден код за сигурност.";

        #region Search parameters

        public string CourtId { get; set; }
        public string CaseKindId { get; set; }
        [RegularExpression(@"^\d{0,20}$", ErrorMessage = "Номер на дело може да съдържа само цифри.")]
        [MaxLength(9, ErrorMessage = "Номер на дело може да съдържа най-много 9 цифри.")]
        public string CaseNumber { get; set; }
        public string CaseYear { get; set; }
        public string Judge { get; set; }
        public string ActKindId { get; set; }
        public string StatusId { get; set; }
        [RegularExpression(@"^\d{0,20}$", ErrorMessage = "Номер на акт може да съдържа само цифри.")]
        [MaxLength(9, ErrorMessage = "Номер на акт може да съдържа най-много 9 цифри.")]
        public string ActNumber { get; set; }
        public string ActYear { get; set; }
        [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\.(0[13578]|1[02])\.((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\.(0[13456789]|1[012])\.((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\.02\.((1[6-9]|[2-9]\d)\d{2}))|(29\.02\.((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Дата на постановление от трябва да бъде във формат дд.мм.гггг.")]
        public string DateFrom { get; set; }
        [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\.(0[13578]|1[02])\.((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\.(0[13456789]|1[012])\.((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\.02\.((1[6-9]|[2-9]\d)\d{2}))|(29\.02\.((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Дата на постановление до трябва да бъде във формат дд.мм.гггг.")]
        public string DateTo { get; set; }
        public string ECLI { get; set; }
        public string KeyWord { get; set; }
        public bool ShowConnected { get; set; }
        public bool IsLuceneInUse { get; set; }
        public bool ShowResults { get; set; }
        public bool IsAdvanced { get; set; }

        #endregion

        #region Selects

        public IEnumerable<SelectListItem> Courts { get; set; }
        public IEnumerable<SelectListItem> CaseKinds { get; set; }
        public IEnumerable<SelectListItem> CaseYears { get; set; }
        public IEnumerable<SelectListItem> ActKinds { get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; }
        public IEnumerable<SelectListItem> ActYears { get; set; }

        #endregion

        #region SearchResults

        public PagedList.IPagedList<Act> SearchResults { get; set; } 

        #endregion

        #region Statics

        public static void EncryptProperties(SearchVM vm)
        {
            vm.CourtId = ConfigurationBasedStringEncrypter.Encrypt(vm.CourtId);
            vm.CaseKindId = ConfigurationBasedStringEncrypter.Encrypt(vm.CaseKindId);
            vm.CaseNumber = ConfigurationBasedStringEncrypter.Encrypt(vm.CaseNumber);
            vm.CaseYear = ConfigurationBasedStringEncrypter.Encrypt(vm.CaseYear);
            vm.Judge = ConfigurationBasedStringEncrypter.Encrypt(vm.Judge);
            vm.ActKindId = ConfigurationBasedStringEncrypter.Encrypt(vm.ActKindId);
            vm.StatusId = ConfigurationBasedStringEncrypter.Encrypt(vm.StatusId);
            vm.ActNumber = ConfigurationBasedStringEncrypter.Encrypt(vm.ActNumber);
            vm.ActYear = ConfigurationBasedStringEncrypter.Encrypt(vm.ActYear);
            vm.DateFrom = ConfigurationBasedStringEncrypter.Encrypt(vm.DateFrom);
            vm.DateTo = ConfigurationBasedStringEncrypter.Encrypt(vm.DateTo);
            vm.ECLI = ConfigurationBasedStringEncrypter.Encrypt(vm.ECLI);
            vm.KeyWord = ConfigurationBasedStringEncrypter.Encrypt(vm.KeyWord);
        }

        #endregion
    }
}