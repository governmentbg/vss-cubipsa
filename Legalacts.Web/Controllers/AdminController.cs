using Legalacts.Model.Entities;
using Legalacts.Web.Captcha;
using Legalacts.Web.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;
using Legalacts.Web.Utils;

namespace Legalacts.Web.Controllers
{
    [Authorize]
    public partial class AdminController : Controller
    {
        [Inject]
        public Legalacts.Model.Repositories.INomenclatureRepository _nomenclatureRepository { get; set; }

        [Inject]
        public Legalacts.Model.Repositories.ILegalactsRepository _legalactsRepository { get; set; }

        #region Account

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult admin()
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                FormsAuthentication.SetAuthCookie("admin", false);
                return RedirectToAction(ActionNames.Search);
            }
            else
            {
                return RedirectToAction(ActionNames.LogOn);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [CaptchaValidation("Captcha")]
        public virtual ActionResult LogOn(LogonVM vm, string ReturnUrl, bool? captchaValid)
        {
            if (captchaValid.HasValue && !captchaValid.Value)
            {
                ModelState.AddModelError("Captcha", LogonVM.MessageCaptcha);
            }

            if (ModelState.IsValid && !Membership.Provider.ValidateUser(vm.UserName, vm.Password))
            {
                ModelState.AddModelError("_FORM", LogonVM.MessageUserExists);
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            FormsAuthentication.SetAuthCookie(vm.UserName, false);

            return RedirectToAction(ActionNames.Search);
        }

        [HttpGet]
        public virtual ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            return RedirectToAction(MVC.Search.ActionNames.Search, MVC.Search.Name);
        }

        [HttpGet]
        public virtual ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult ChangePassword(ChangePasswordVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            string currentUsername = ControllerContext.HttpContext.User.Identity.Name;

            if (Membership.Provider.ChangePassword(currentUsername, vm.CurrentPassword, vm.NewPassword))
            {
                return RedirectToAction(ActionNames.Search);
            }
            else
            {
                ModelState.AddModelError("currentPassword", ChangePasswordVM.MessageWrongCurrentPassword);
                return View();
            }
        }

        #endregion


        [HttpGet]
        public virtual ActionResult Search(
                string courtId = "",
                string actionLogTypeId = "",
                string dateFrom = "",
                string dateTo = "",
                bool showResults = false,
                int page = 1
                )
        {
            SearchLogVM vm = new SearchLogVM()
            {
                CourtId = courtId,
                ActionLogTypeId = actionLogTypeId,
                DateFrom = dateFrom,
                DateTo = dateTo,
                ShowResults = showResults
            };

            FillSelectListItems(ref vm);

            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // TODO ADD INDEXES FOR ALL SEARCH COLUMNS
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            if (vm.ShowResults)
            {
                var logs = _legalactsRepository.GetAllLogs();

                if (!string.IsNullOrWhiteSpace(courtId))
                {
                    int pCourtId = 0;

                    if (int.TryParse(courtId, out pCourtId))
                    {
                        logs = logs.Where(e => e.CourtId == pCourtId);
                    }
                }

                if (!string.IsNullOrWhiteSpace(actionLogTypeId))
                {
                    int pActionLogTypeId = 0;

                    if (int.TryParse(actionLogTypeId, out pActionLogTypeId))
                    {
                        logs = logs.Where(e => e.ActionLogTypeId == pActionLogTypeId);
                    }
                }

                if (!string.IsNullOrWhiteSpace(dateFrom))
                {
                    DateTime pDateFrom = DateTime.MinValue;

                    if (DateTime.TryParse(dateFrom, out pDateFrom))
                    {
                        logs = logs.Where(e => e.DatetimeOfAction >= pDateFrom);
                    }
                }

                if (!string.IsNullOrWhiteSpace(dateTo))
                {
                    DateTime pDateTo = DateTime.MaxValue;

                    if (DateTime.TryParse(dateTo, out pDateTo))
                    {
                        logs = logs.Where(e => e.DatetimeOfAction <= pDateTo);
                    }
                }
                vm.SearchResults = logs.Take(Statics.MaxLogItems).ToList().ToPagedList(page, Statics.MaxLogItemsPerPage);
            }

            return View(vm);
        }

        [HttpPost]
        public virtual ActionResult Search(SearchLogVM vm)
        {
            if (!ModelState.IsValid)
            {
                FillSelectListItems(ref vm);

                return View(vm);
            }

            vm.ShowResults = true;
            return RedirectToAction(MVC.Search.ActionNames.Search, vm);
        }

        [HttpGet]
        public virtual ActionResult Nomenclatures()
        {
            NomenclaturesVM vm = new NomenclaturesVM()
            {
                ActKinds = _nomenclatureRepository.GetAllActKinds().ToList(),
                CaseKinds = _nomenclatureRepository.GetAllCaseKinds().ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public virtual ActionResult EditActKinds(NomenclaturesVM vm)
        {
            if (vm != null && vm.ActKinds != null)
            {
                foreach (var kind in vm.ActKinds)
                {
                    var kindFromDb = _nomenclatureRepository.GetActKindById(kind.ActKindId);
                    kindFromDb.IsActive = kind.IsActive;
                }
                _nomenclatureRepository.UnitOfWork.Save();
            }

            return RedirectToAction(ActionNames.Nomenclatures);
        }

        [HttpPost]
        public virtual ActionResult EditCaseKinds(NomenclaturesVM vm)
        {
            if (vm != null && vm.CaseKinds != null)
            {
                foreach (var kind in vm.CaseKinds)
                {
                    var kindFromDb = _nomenclatureRepository.GetCaseKindById(kind.CaseKindId);
                    kindFromDb.IsActive = kind.IsActive;
                }
                _nomenclatureRepository.UnitOfWork.Save();
            }

            return RedirectToAction(ActionNames.Nomenclatures);
        }

        [HttpGet]
        public virtual ActionResult Acts(int? year)
        {
            ActsVM vm = new ActsVM();
            if (year.HasValue)
            {
                vm.Year = year;
                vm.Courts = _legalactsRepository.GetAllActs().Where(e => e.ActYear.HasValue && year.Value.Equals(e.ActYear.Value))
                    .GroupBy(e => e.CourtId)
                    .Select(group => new CourtInfo() { ActsCount = group.Count(), CourtId = group.Key }).ToList();

                vm.TotalCount = _legalactsRepository.GetAllActs().Where(e => e.ActYear.HasValue && year.Value.Equals(e.ActYear.Value)).Count();
            }
            else
            {
                vm.TotalCount = _legalactsRepository.GetAllActs().Count();
                vm.Courts = _legalactsRepository.GetAllActs().GroupBy(e => e.CourtId)
                    .Select(group => new CourtInfo() { ActsCount = group.Count(), CourtId = group.Key }).ToList();
            }

            foreach (var court in _nomenclatureRepository.GetAllActiveCourts())
            {
                if (!vm.Courts.Any(e => e.CourtId == court.CourtId))
                {
                    vm.Courts.Add(new CourtInfo() { CourtId = court.CourtId, ActsCount = 0 });
                }

                vm.Courts.Single(e => e.CourtId == court.CourtId).CourtName = court.Name;
            }

            vm.Courts = vm.Courts.OrderBy(e => e.CourtName).ToList();
            vm.Years = Enumerable.Range(DateTime.Now.Year - 30, 31).OrderByDescending(e => e).Select(e => new SelectListItem() { Value = e.ToString(), Text = e.ToString() });

            return View(vm);
        }

        [HttpPost]
        public virtual ActionResult Acts(string year)
        {
            int value;
            if (Int32.TryParse(year, out value))
                return RedirectToAction(ActionNames.Acts, new { year = value });
            else
                return RedirectToAction(ActionNames.Acts);
        }

        #region Private

        private void FillSelectListItems(ref SearchLogVM vm)
        {
            if (vm == null)
                vm = new SearchLogVM();

            vm.Courts = _nomenclatureRepository.GetAllActiveCourts().OrderBy(e => e.Name).Select(e => new SelectListItem() { Value = e.CourtId.ToString(), Text = e.Name });
            vm.ActionLogTypes = _nomenclatureRepository.GetAllActiveActionLogTypes().Select(e => new SelectListItem() { Value = e.ActionLogTypeId.ToString(), Text = e.Name });
            ViewData["TotalActsCount"] = _legalactsRepository.GetAllActs().Count();
        }

        #endregion

    }
}