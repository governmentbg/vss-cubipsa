using System.Linq;
using System.Linq.Expressions;
using Legalacts.Model.Entities;
using Legalacts.Utils;
using Legalacts.Web.Utils;
using NLog;
using System;
using System.Threading;
using System.Net.Mail;
using System.Net;

namespace Legalacts.Web.Jobs
{
    public class MailSenderJob : IJob
    {
        private readonly Timer timer;
        private readonly JobHost jobHost;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public MailSenderJob()
        {
            this.timer = new Timer(this.OnTimerElapsed);
            this.jobHost = new JobHost();
        }

        public void Start()
        {
            logger.Info("MailSenderJob Initializing.");

            this.timer.Change(TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(Statics.MailSenderJobIntervalInMinutes));
        }

        public void Dispose()
        {
            this.timer.Dispose();

            logger.Info("MailSenderJob Disposed.");
        }

        private void OnTimerElapsed(object sender)
        {
            this.jobHost.DoAction(() =>
            {
                if (this.jobHost.IsShuttingDown)
                    return;

                logger.Info("MailSenderJob Started.");

                try
                {
                    using (LegalactsContext _context = new LegalactsContext())
                    {
                        var messages = _context.Messages.Where(e => e.SentDate == null).ToList();

                        foreach (var message in messages)
                        {
                            if (_sendMail(message))
                            {
                                message.SentDate = DateTime.Now;

                                _context.SaveChanges();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    logger.Error("General error: " + Helper.CreateExceptionString(e));
                }

                logger.Info("MailSenderJob Finished.");
            });
        }

        private bool _sendMail(Message message)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient
                {
                    Host = "host",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("mail", "password")
                })
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        MailAddress from = new MailAddress("mail");

                        mailMessage.From = from;

                        //if (message.Recipient.Contains(','))
                        //{
                        //    var mails = message.Recipient.Split(',');
                        //    for (int i = 0; i < mails.Count(); i++)
                        //    {
                        //        if (i == 0)
                        //        {
                        //            mailMessage.To.Add(new MailAddress(mails[i]));
                        //        }
                        //        else
                        //        {
                        //            mailMessage.Bcc.Add(new MailAddress(mails[i]));
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    mailMessage.To.Add(message.Recipient);
                        //}

                        mailMessage.Bcc.Add(message.Recipient);
                        mailMessage.Subject = message.Subject;
                        mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                        mailMessage.Body = message.Body;
                        mailMessage.IsBodyHtml = message.IsBodyHtml;
                        mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                        
                        smtpClient.Send(mailMessage);
                    }
                }
            }
            catch { return false; }

            return true;
        }
    }
}