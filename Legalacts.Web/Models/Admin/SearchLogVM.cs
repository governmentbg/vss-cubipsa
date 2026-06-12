using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Legalacts.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace Legalacts.Web.Models
{
    public class SearchLogVM
    {
        #region Search parameters

        public string CourtId { get; set; }
        public string ActionLogTypeId { get; set; }
        [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\.(0[13578]|1[02])\.((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\.(0[13456789]|1[012])\.((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\.02\.((1[6-9]|[2-9]\d)\d{2}))|(29\.02\.((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Дата на постановление от трябва да бъде във формат дд.мм.гггг.")]
        public string DateFrom { get; set; }
        [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\.(0[13578]|1[02])\.((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\.(0[13456789]|1[012])\.((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\.02\.((1[6-9]|[2-9]\d)\d{2}))|(29\.02\.((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Дата на постановление до трябва да бъде във формат дд.мм.гггг.")]
        public string DateTo { get; set; }
        public bool ShowResults { get; set; }

        #endregion

        #region Selects

        public IEnumerable<SelectListItem> Courts { get; set; }
        public IEnumerable<SelectListItem> ActionLogTypes { get; set; }

        #endregion

        #region SearchResults

        public PagedList.IPagedList<Log> SearchResults { get; set; } 

        #endregion
    }
}