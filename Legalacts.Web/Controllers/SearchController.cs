using System.Data.Entity;
using System.Web.Helpers;
using Legalacts.Model.Entities;
using Legalacts.Utils.Managers.Pdf;
using Legalacts.Web.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Text;
using Legalacts.Web.Utils;
using Legalacts.Model.Utils;
using Legalacts.Utils.Managers;
using Legalacts.Web.Captcha;

namespace Legalacts.Web.Controllers
{
    public partial class SearchController : Controller
    {
        [Inject]
        public Legalacts.Model.Repositories.INomenclatureRepository _nomenclatureRepository { get; set; }

        [Inject]
        public Legalacts.Model.Repositories.ILegalactsRepository _legalactsRepository { get; set; }

        [HttpGet]
        [DecryptParametersAttribute(IdsParamName = 
            new string[] 
            { 
                    "courtId",
                    "caseKindId",
                    "caseNumber",
                    "caseYear",
                    "judge",
                    "actKindId",
                    "statusId",
                    "actNumber",
                    "actYear",
                    "dateFrom",
                    "dateTo",
                    "ecli",
                    "keyWord",
                    "page"
            })]
        public virtual ActionResult Search(
                string courtId = "",
                string caseKindId = "",
                string caseNumber = "",
                string caseYear = "",
                string judge = "",
                string actKindId = "",
                string statusId = "",
                string actNumber = "",
                string actYear = "",
                string dateFrom = "",
                string dateTo = "",
                string ecli = "",
                string keyWord = "",
                bool showConnected = false,
                bool isLuceneInUse = true,
                bool showResults = false,
                bool isAdvanced = false,
                string page = ""
                )
        {
            ModelState.Clear();
            
            SearchVM vm = new SearchVM()
            {
                CourtId = courtId,
                CaseKindId = caseKindId,
                CaseNumber = caseNumber,
                CaseYear = caseYear,
                Judge = judge,
                ActKindId = actKindId,
                StatusId = statusId,
                ActNumber = actNumber,
                ActYear = actYear,
                DateFrom = dateFrom,
                DateTo = dateTo,
                ECLI = ecli,
                KeyWord = keyWord,
                ShowConnected = showConnected,
                IsLuceneInUse = isLuceneInUse,
                ShowResults = showResults,
                IsAdvanced = isAdvanced
            };

            FillSelectListItems(ref vm);

            if (vm.ShowResults)
            {
                bool isLastCondition = true;

                var acts = _legalactsRepository.GetAllActs();

                if (!string.IsNullOrWhiteSpace(courtId))
                {
                    int pCourtId = 0;

                    if (int.TryParse(courtId, out pCourtId))
                    {
                        acts = acts.Where(e => e.CourtId == pCourtId);
                    }

                    isLastCondition = false;
                }

                if (!string.IsNullOrWhiteSpace(caseKindId))
                {
                    int pCaseKindId = 0;

                    if (int.TryParse(caseKindId, out pCaseKindId))
                    {
                        acts = acts.Where(e => e.CaseKindId == pCaseKindId);
                    }

                    isLastCondition = false;
                }

                if (!string.IsNullOrWhiteSpace(caseNumber))
                {
                    int pCaseNumber = 0;

                    if (int.TryParse(caseNumber, out pCaseNumber))
                    {
                        acts = acts.Where(e => e.CaseNumber == pCaseNumber);
                    }

                    isLastCondition = false;
                }

                if (!string.IsNullOrWhiteSpace(caseYear))
                {
                    int pCaseYear = 0;

                    if (int.TryParse(caseYear, out pCaseYear))
                    {
                        acts = acts.Where(e => e.CaseYear == pCaseYear);
                    }

                    isLastCondition = false;
                }

                if (!string.IsNullOrWhiteSpace(ecli))
                {
                    acts = acts.Where(e => DbFunctions.Like(e.EcliCode, ecli + "%") || DbFunctions.Like(e.PreviousEcliCode, ecli + "%"));

                    isLastCondition = false;
                }

                if (isAdvanced)
                {
                    if (!string.IsNullOrWhiteSpace(judge))
                    {
                        acts = acts.Where(e => e.Judge.ToUpper().Contains(judge.ToUpper()));

                        isLastCondition = false;
                    }

                    if (!string.IsNullOrWhiteSpace(actKindId))
                    {
                        int pActKindId = 0;

                        if (int.TryParse(actKindId, out pActKindId))
                        {
                            acts = acts.Where(e => e.ActKindId == pActKindId);
                        }

                        isLastCondition = false;
                    }

                    if (!string.IsNullOrWhiteSpace(statusId))
                    {
                        int pStatusId = 0;

                        if (int.TryParse(statusId, out pStatusId))
                        {
                            acts = acts.Where(e => e.StatusId == pStatusId);
                        }

                        isLastCondition = false;
                    }

                    if (!string.IsNullOrWhiteSpace(actNumber))
                    {
                        int pActNumber = 0;

                        if (int.TryParse(actNumber, out pActNumber))
                        {
                            acts = acts.Where(e => e.ActNumber == pActNumber);
                        }

                        isLastCondition = false;
                    }

                    if (!string.IsNullOrWhiteSpace(actYear))
                    {
                        int pActYear = 0;

                        if (int.TryParse(actYear, out pActYear))
                        {
                            acts = acts.Where(e => e.ActYear == pActYear);
                        }

                        isLastCondition = false;
                    }

                    if (!string.IsNullOrWhiteSpace(dateFrom))
                    {
                        DateTime pDateFrom = DateTime.MinValue;

                        if (DateTime.TryParse(dateFrom, out pDateFrom))
                        {
                            acts = acts.Where(e => e.StartDate.Value >= pDateFrom);
                        }

                        isLastCondition = false;
                    }

                    if (!string.IsNullOrWhiteSpace(dateTo))
                    {
                        DateTime pDateTo = DateTime.MaxValue;

                        if (DateTime.TryParse(dateTo, out pDateTo))
                        {
                            acts = acts.Where(e => e.StartDate.Value <= pDateTo);
                        }

                        isLastCondition = false;
                    }

                }

                int innerPage = string.IsNullOrEmpty(page) ? 1 : int.Parse(page);

                // RESTRICT MAX RETRIEVED ACTS
                acts = acts.Take(Statics.MaxActItems + 1);

                if (!string.IsNullOrWhiteSpace(keyWord))
                {
                    var sqlCommands = acts.ExtractSqlParameters<Act>();

                    var actsWithFullTextSearch = _legalactsRepository.GetActsByKeywords(keyWord, acts.ToString(), sqlCommands, isLuceneInUse, isLastCondition);

                    vm.SearchResults = actsWithFullTextSearch
                        .OrderBy(e => e.CourtId)
                        .OrderByDescending(e => e.StartDate)
                        .OrderByDescending(e => e.ActNumber)
                        .ToPagedList(innerPage, Statics.MaxActItemsPerPage);
                }
                else
                {
                    vm.SearchResults = acts
                        .OrderBy(e => e.CourtId)
                        .OrderByDescending(e => e.StartDate)
                        .OrderByDescending(e => e.ActNumber)
                        .ToList().ToPagedList(innerPage, Statics.MaxActItemsPerPage);
                }

                if (vm.SearchResults.TotalItemCount > Statics.MaxActItems)
                {
                    ModelState.AddModelError("_FORM", "Броят на намерените актове надвишава " + Statics.MaxActItems + ". Моля въведете повече критерии.");
                    vm.SearchResults = null;
                }
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CaptchaValidation("Captcha")]
        public virtual ActionResult Search(SearchVM vm, bool? captchaValid)
        {
            if (captchaValid.HasValue && !captchaValid.Value)
            {
                ModelState.AddModelError("Captcha", SearchVM.MessageCaptcha);
            }

            if (!ModelState.IsValid)
            {
                FillSelectListItems(ref vm);

                return View(vm);
            }

            vm.ShowResults = true;

            SearchVM.EncryptProperties(vm);

            return RedirectToAction(MVC.Search.ActionNames.Search, vm);
        }

        [HttpGet]
        public virtual ActionResult UidDetails(string uid)
        {
            Act act = _legalactsRepository.GetActByUID(uid);

            if (act == null)
            {
                return RedirectToAction(ActionNames.Search);
            }

            return RedirectToAction(ActionNames.Details, new { actId = ConfigurationBasedStringEncrypter.Encrypt(act.ActId.ToString()), isAdmin = true });
        }

        [HttpGet]
        [DecryptParameter(IdParamName = "actId")]
        public virtual ActionResult Details(string actId)
        {
            Act act = _legalactsRepository.GetActById(int.Parse(actId));

            if (act == null)
            {
                return RedirectToAction(ActionNames.Search);
            }

            act.ResultOfAppealDescription = GetResultOfAppealDescription(act.CourtId, act.ResultOfAppeal, act.CaseKindId);

            return View(act);
        }

        [Route("~/{ecli:regex(^ECLI:BG:[A-Z]{2}[0-9]{3}:[0-9]{4}:[0-9]{11}.[0-9]{3}$)}")]
        public virtual ActionResult DetailsByEcli(string ecli)
        {
            Act act = _legalactsRepository.GetActByEcli(ecli);

            if (act == null)
            {
                return RedirectToAction(ActionNames.Search);
            }

            act.ResultOfAppealDescription = GetResultOfAppealDescription(act.CourtId, act.ResultOfAppeal, act.CaseKindId);

            return View(Views.Details, act);
        }

        [Route("~/GetActContent/{ecli:regex(^ECLI:BG:[A-Z]{2}[0-9]{3}:[0-9]{4}:[0-9]{11}.[0-9]{3}$)}")]
        public virtual ActionResult GetActContentByEcli(string ecli)
        {
            Act act = _legalactsRepository.GetActByEcli(ecli);

            if (act == null)
            {
                return RedirectToAction(ActionNames.Search);
            }

            return GetActContent(act.ActId);
        }

        [Route("~/GetMotiveContent/{ecli:regex(^ECLI:BG:[A-Z]{2}[0-9]{3}:[0-9]{4}:[0-9]{11}.[0-9]{3}$)}")]
        public virtual ActionResult GetMotiveContentByEcli(string ecli)
        {
            Act act = _legalactsRepository.GetActByEcli(ecli);

            if (act == null)
            {
                return RedirectToAction(ActionNames.Search);
            }

            return GetMotiveContent(act.ActId);
        }

        private string GetResultOfAppealDescription(int courtId, string resultOfAppeal, int caseKindId)
        {
            string description = String.Empty;

            if (!String.IsNullOrEmpty(resultOfAppeal))
            {
                ResultsOfAppeal result = null;
                if (Legalacts.Model.Utils.Validator.IsAdministrativeCourt(courtId))
                {
                    result = _nomenclatureRepository.GetAllActiveResultsOfAppeals().FirstOrDefault(r => r.Code == resultOfAppeal && r.ResultsOfAppealId >= 400);

                }
                else
                {
                    if (Legalacts.Model.Utils.Validator.IsCriminalCase(caseKindId))
                    {
                        result = _nomenclatureRepository.GetAllActiveResultsOfAppeals().FirstOrDefault(r => r.Code == resultOfAppeal && r.ResultsOfAppealId >= 300 && r.ResultsOfAppealId < 400);
                    }
                    else
                    {
                        result = _nomenclatureRepository.GetAllActiveResultsOfAppeals().FirstOrDefault(r => r.Code == resultOfAppeal && r.ResultsOfAppealId < 300);
                    }
                }

                if (result != null)
                    description = result.Description;
                else
                    description = resultOfAppeal;
            }

            return description;
        }

        [DecryptParameter(IdParamName = "actId")]
        public virtual FileResult GetActContentByActId(string actId)
        {
            return GetActContent(int.Parse(actId));
        }

        [DecryptParameter(IdParamName = "actId")]
        public virtual FileResult GetMotiveContentByActId(string actId)
        {
            return GetMotiveContent(int.Parse(actId));
        }

        #region Private

        private FileResult GetActContent(int actId)
        {
            var act = _legalactsRepository.GetActById(actId);

            if (act.ActDocument == null || act.ActDocument.Content == null || String.IsNullOrWhiteSpace(act.ActDocument.MimeType))
            {
                return null;
            }
            else
            {
                byte[] fileContent = null;
                var mimeType = act.ActDocument.MimeType;
                var decompressed = Legalacts.Utils.Managers.ZipManager.Decompress(act.ActDocument.Content);

                if (Statics.EnablePdfConverting)
                {
                    fileContent = PdfConvertManager.Convert(decompressed, ref mimeType);
                }
                else
                {
                    fileContent = decompressed;
                }

                string fileName = "Act";
                switch (mimeType)
                {
                    case "text/html":
                        fileName += ".htm";
                        break;
                    case "application/msword":
                        fileName += ".doc";
                        break;
                    case "text/plain":
                        fileName += ".txt";
                        break;
                    case "application/pdf":
                        fileName += ".pdf";
                        break;
                    default:
                        break;
                }

                Response.AppendHeader("Content-Disposition", "inline;filename=" + fileName);

                if (mimeType.Contains("text"))
                {
                    Response.Charset = "windows-1251";
                }

                return File(fileContent, mimeType);
            }
        }

        private FileResult GetMotiveContent(int actId)
        {
            var act = _legalactsRepository.GetActById(actId);

            if (act.MotiveDocument == null || act.MotiveDocument.Content == null || String.IsNullOrWhiteSpace(act.MotiveDocument.MimeType))
            {
                return null;
            }
            else
            {
                byte[] fileContent = null;
                var mimeType = act.MotiveDocument.MimeType;
                var decompressed = Legalacts.Utils.Managers.ZipManager.Decompress(act.MotiveDocument.Content);

                if (Statics.EnablePdfConverting)
                {
                    fileContent = PdfConvertManager.Convert(decompressed, ref mimeType);
                }
                else
                {
                    fileContent = decompressed;
                }

                string fileName = "Motive";
                switch (mimeType)
                {
                    case "text/html":
                        fileName += ".htm";
                        break;
                    case "application/msword":
                        fileName += ".doc";
                        break;
                    case "text/plain":
                        fileName += ".txt";
                        break;
                    case "application/pdf":
                        fileName += ".pdf";
                        break;
                    default:
                        break;
                }

                Response.AppendHeader("Content-Disposition", "inline;filename=" + fileName);

                if (mimeType.Contains("text"))
                {
                    Response.Charset = "windows-1251";
                }

                return File(fileContent, mimeType);
            }
        }

        private void FillSelectListItems(ref SearchVM vm)
        {
            if (vm == null)
                vm = new SearchVM();

            vm.Courts = _nomenclatureRepository.GetAllActiveCourts().OrderBy(e => e.Name).Select(e => new SelectListItem() { Value = e.CourtId.ToString(), Text = e.Name });
            vm.CaseKinds = _nomenclatureRepository.GetAllActiveCaseKinds().OrderBy(e => e.Name).Select(e => new SelectListItem() { Value = e.CaseKindId.ToString(), Text = e.Name });
            vm.CaseYears = _years;
            vm.ActKinds = _nomenclatureRepository.GetAllActiveActKinds().OrderBy(e => e.Name).Select(e => new SelectListItem() { Value = e.ActKindId.ToString(), Text = e.Name });
            vm.Statuses = _nomenclatureRepository.GetAllActiveStatuses().OrderBy(e => e.Name).Select(e => new SelectListItem() { Value = e.StatusId.ToString(), Text = e.Name });
            vm.ActYears = _years;
        }

        private IEnumerable<SelectListItem> _years = Enumerable.Range(DateTime.Now.Year - 30, 31).OrderByDescending(e => e).Select(e => new SelectListItem() { Value = e.ToString(), Text = e.ToString() });

        #endregion
    }
}