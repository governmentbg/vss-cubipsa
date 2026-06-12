using Legalacts.Model.Entities;
using Legalacts.Model.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Legalacts.ECLI.Generator
{
    public partial class Generator : Form
    {
        private int progressMax = 0;
        private int progressValue = 0;
        private int SKIP = 500;

        private TaskScheduler ui;

        public Generator()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs ea)
        {
            ui = TaskScheduler.FromCurrentSynchronizationContext();

            btnStart.Enabled = false;

            var timmer = DateTime.Now.TimeOfDay;
            timer.Tick += (s, e) =>
            {
                lblTime.Text = (DateTime.Now.TimeOfDay - timmer).ToString(@"mm\:ss");
            };

            timer.Start();

            Task.Factory.StartNew(() =>
            {
                try
                {
                    var context = new LegalactsContext();
                    context.Configuration.ProxyCreationEnabled = false;
                    context.Configuration.LazyLoadingEnabled = false;

                    SetMode("Load data");
                    var acts = context.Acts.AsNoTracking()
                                        .Select(e => new EcliMetadata
                                        {
                                            ActId = e.ActId,
                                            CaseNumber = e.CaseNumber,
                                            CaseKindId = e.CaseKindId,
                                            CaseYear = e.CaseYear,
                                            CourtId = e.CourtId,
                                            StartDate = e.StartDate,
                                            EcliCode = e.EcliCode
                                        }).ToList();

                    progressMax = acts.Count();

                    if (progressMax == 0)
                    {
                        throw new ArgumentException("Missing acts.");
                    }

                    // ***************** generate ecli *****************
                    SetMode("Generate ECLI codes");

                    for (var i = 0; i < acts.Count(); i++)
                    {
                        acts[i].EcliCode = acts[i].Generate;

                        progressValue += 1;

                        if (i % SKIP == 0)
                        {
                            UpdateUI();
                        }
                    }

                    // ***************** update duplications *****************

                    progressValue = 0;
                    UpdateUI();

                    var dublicatedECLIs = acts.GroupBy(e => e.EcliCode).Where(g => g.Count() > 1).Select(e => new { ECLI = e.Key, Acts = e.Select(i => i).ToList(), Count = e.Count() }).ToList();
                    progressMax = dublicatedECLIs.Count();

                    SetMode("Update duplications");

                    for (var i = 0; i < dublicatedECLIs.Count(); i++)
                    {
                        var dublicatedActs = dublicatedECLIs[i].Acts;

                        for (int j = 0; j < dublicatedActs.Count(); j++)
                        {
                            dublicatedActs[j].EcliCode = dublicatedActs[j].EcliCode.Replace(".001", $".{(j + 1).ToString("D3")}");
                        }

                        progressValue += 1;

                        if ((i + 1) % SKIP / 20 == 0)
                        {
                            UpdateUI();
                        }
                    }

                    // ***************** update db *****************
                    progressValue = 0;
                    UpdateUI();

                    SetMode("Update DB");

                    List<EcliMetadata> bufferActs = new List<EcliMetadata>();
                    progressMax = acts.Count();

                    for (var i = 0; i < acts.Count(); i++)
                    {
                        bufferActs.Add(acts[i]);
                        progressValue += 1;

                        if ((i + 1) % SKIP == 0 || (i + 1) == acts.Count())
                        {
                            UpdateUI();

                            using (LegalactsContext db = new LegalactsContext())
                            {
                                db.Configuration.ProxyCreationEnabled = false;
                                db.Configuration.LazyLoadingEnabled = false;
                                db.Configuration.ValidateOnSaveEnabled = false;

                                var command = string.Empty;
                                var ids = bufferActs.Select(e => e.ActId).ToArray();
                                var willUpdate = db.Acts.Where(e => ids.Contains(e.ActId));

                                foreach (var da in bufferActs)
                                {
                                    command += $"UPDATE Acts SET EcliCode = '{da.EcliCode}' WHERE ActId = {da.ActId};";
                                }

                                db.Database.CommandTimeout = 0;
                                db.Database.ExecuteSqlCommand(command);
                                bufferActs = new List<EcliMetadata>();
                            }
                        }
                    }

                    context.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    timer.Stop();
                    UpdateUI(true);
                }

                Thread.Sleep(2000);
                if (MessageBox.Show("Do you want to close app generator?", "Exit",
                                     MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            });
        }

        private void UpdateUI(bool enableButton = false)
        {
            Task.Factory.StartNew(() =>
            {
                var percentage = (int)(100 * ((double)progressValue / progressMax));

                btnStart.Text = $"{percentage}%";
                pbLoader.Value = percentage;

                if (enableButton)
                {
                    btnStart.Text = "Finished";
                    lblMode.Text = btnStart.Text;
                }

            }, CancellationToken.None, TaskCreationOptions.None, ui);
        }

        private void SetMode(string message)
        {
            Task.Factory.StartNew(() => { lblMode.Text = $"Mode: {message}"; },
                CancellationToken.None, TaskCreationOptions.None, ui);
        }
    }
}
