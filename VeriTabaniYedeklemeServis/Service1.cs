using System;
using System.Data;
using System.Globalization;
using System.ServiceProcess;
using System.Timers;
using VeriTabaniYedeklemeServis.Classes;

namespace VeriTabaniYedeklemeServis
{
    public partial class Service1 : ServiceBase
    {
        private Timer _timer;

        public Service1()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            _timer = new Timer
            {
                Interval = 60_000,
                AutoReset = false 
            };
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }
        private async void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            try
            {
                string currentDay = DateTime.Now.ToString("dddd", new CultureInfo("tr-TR"));
                string currentTime = DateTime.Now.ToString("HH:mm");
                DataTable scheduleTable =
                    await SQLiteHelper.GetDataTableAsync("SELECT Days, BackUpTime FROM DbBackUpSettings LIMIT 1");
                if (scheduleTable.Rows.Count == 0 || string.IsNullOrEmpty(scheduleTable.Rows[0][0]?.ToString()) || string.IsNullOrEmpty(scheduleTable.Rows[0][1]?.ToString()))
                {
                    TextLog.TextLogging("Zamanlama verisi bulunamadı.", "Uyarı");
                    return;
                }
                foreach (DataRow row in scheduleTable.Rows)
                {
                    if (row["Days"] is DBNull || row["BackUpTime"] is DBNull)
                        return;
                    string[] backupDays = row["Days"].ToString().Split(',');
                    string backupTime = DateTime.Parse(row["BackUpTime"].ToString()).ToString("HH:mm");
                    foreach (string day in backupDays)
                    {
                        if (day.Trim().Equals(currentDay, StringComparison.OrdinalIgnoreCase) &&
                            backupTime == currentTime)
                        {
                            try
                            {
                                await BackUp.BackupDatabasesToSqlServerServiceAsync(
                                    await SQLiteHelper.GetDataTableAsync("SELECT DbName FROM DbNameBackup"),
                                    await SQLServerConnection.ConnectionStringGet());
                            }
                            catch (Exception backupEx)
                            {
                                TextLog.TextLogging("Yedekleme sırasında hata: " + backupEx, "Hatalı");
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TextLog.TextLogging("Zamanlayıcı genel hata: " + ex, "Hata");
            }
            finally
            {
                _timer.Start();
            }
        }
        protected override void OnStop()
        {
            _timer?.Stop();
        }
    }
}