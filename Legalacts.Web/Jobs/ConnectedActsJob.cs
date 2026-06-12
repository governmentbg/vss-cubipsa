using Legalacts.Model.Entities;
using Legalacts.Utils;
using Legalacts.Web.Utils;
using NLog;
using System;
using System.Threading;

namespace Legalacts.Web.Jobs
{
    public class ConnectedActsJob : IJob
    {
        private readonly Timer timer;
        private readonly JobHost jobHost;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger(); 

        public ConnectedActsJob()
        {
            this.timer = new Timer(this.OnTimerElapsed);
            this.jobHost = new JobHost();
        }

        public void Start()
        {
            logger.Info("ConnectedActsJob Initializing.");

            this.timer.Change(TimeSpan.FromHours(1), TimeSpan.FromHours(Statics.ConnectedActsJobIntervalInHours));
        }

        public void Dispose()
        {
            this.timer.Dispose();

            logger.Info("ConnectedActsJob Disposed.");
        }

        private void OnTimerElapsed(object sender)
        {
            this.jobHost.DoAction(() =>
            {
                if (this.jobHost.IsShuttingDown)
                    return;

                logger.Info("ConnectedActsJob Started.");

                try
                {
                    using (LegalactsContext context = new LegalactsContext())
                    {
                        context.Database.ExecuteSqlCommand("spMergeConnectedActs");
                    }
                }
                catch (Exception e)
                {
                    logger.Error("General error: " + Helper.CreateExceptionString(e));
                }

                logger.Info("ConnectedActsJob Finished.");
            });
        }
    }
}