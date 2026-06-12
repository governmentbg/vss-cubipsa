using Legalacts.Model.Entities;
using Legalacts.Utils.Communicators;
using Legalacts.Web.Captcha;
using Legalacts.Web.Models;
using Legalacts.Web.Utils;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Legalacts.Web.Controllers
{
    public partial class HomeController : Controller
    {
        [Inject]
        public IHelpDeskCommunicator _helpDeskCommunicator { get; set; }

        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public virtual ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public virtual ActionResult Feedback()
        {
            FeedbackVM model = new FeedbackVM();

            return View(model);
        }

        [HttpPost]
        [CaptchaValidation("Captcha")]
        public virtual ActionResult Feedback(FeedbackVM model, bool captchaValid)
        {
            if (!captchaValid)
                ModelState.AddModelError("Captcha", "Невалиден код за сигурност.");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string subjectText = String.Empty;
            switch (model.Type)
            {
                case "1":
                    subjectText = "Въпрос";
                    break;
                case "2":
                    subjectText = "Предложение";
                    break;
                case "3":
                    subjectText = "Технически проблем";
                    break;
                default:
                    break;
            }

            using (LegalactsContext _context = new LegalactsContext())
            {
                Message message = new Message();
                _context.Messages.Add(message);

                FeedbackTemplateVM feedbackTemplateVM = new FeedbackTemplateVM()
                {
                    Email = model.Email ?? String.Empty,
                    Name = model.Name ?? String.Empty,
                    Subject = subjectText,
                    Body = model.Body == null ? String.Empty : RazorRenderHtmlHelper.ConvertTextToHtml(model.Body)
                };

                message.Recipient = Statics.FeedbackEmails;
                message.Subject = String.Format(FeedbackTemplateVM.MessageSubjectPrefix + ": {0}", subjectText);
                message.Body = RazorRenderHtmlHelper.RenderHtml(feedbackTemplateVM, MVC.Home.Views.FeedbackMailTemplate);
                message.IsBodyHtml = true;

                _context.SaveChanges();

                Task.Run(() =>
                {
                    try
                    {
                        SendFeedbackToHelpDesk(subjectText, feedbackTemplateVM.Name, feedbackTemplateVM.Email, feedbackTemplateVM.Body);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(Legalacts.Utils.Helper.CreateExceptionString(ex));
                    }

                });
            }

            TempData["_feedbackSuccess"] = @"Вашето съобщение е прието и ще бъде обработено. 
                                                При необходимост ще се свържем с Вас на посочения адрес на електронна поща.";

            return RedirectToAction(ActionNames.Feedback);
        }

        [HttpGet]
        public virtual ActionResult Accessibility()
        {
            return View();
        }

        [NonAction]
        private void SendFeedbackToHelpDesk(string subject, string name, string email, string description)
        {
            var access_token = _helpDeskCommunicator.Login(
                ConfigurationManager.AppSettings["Legalacts.Web:HelpDeskDomain"],
                ConfigurationManager.AppSettings["Legalacts.Web:HelpDeskClientId"],
                ConfigurationManager.AppSettings["Legalacts.Web:HelpDeskSecret"],
                ConfigurationManager.AppSettings["Legalacts.Web:HelpDeskUsername"],
                ConfigurationManager.AppSettings["Legalacts.Web:HelpDeskPassword"]
                );

            _helpDeskCommunicator.Send(
                ConfigurationManager.AppSettings["Legalacts.Web:HelpDeskDomain"],
                access_token,
                subject,
                name,
                email,
                description
                );
        }

    }
}