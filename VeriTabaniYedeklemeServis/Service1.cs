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
            if (_timer == null)
            {
                _timer = new Timer();
            }
            _timer.Interval = 60_000;
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            string currentDay = DateTime.Now.ToString("dddd", new CultureInfo("tr-TR"));
            string currentTime = DateTime.Now.ToString("HH:mm");
            DataTable scheduleTable = SQLLiteConnection.GetDataFromSQLite("SELECT Days, BackUpTime FROM DbBackUpSettings LIMIT 1");
            foreach (DataRow row in scheduleTable.Rows)
            {
                string[] backupDay = row["Days"].ToString().Split(',');
                string backupTime = row["BackUpTime"].ToString().Substring(0, 5); ;
                for (byte i = 0; i < backupDay.Length; i++)
                {
                    if (backupDay[i]==currentDay && backupTime == currentTime)
                    {
                        _ = BackUp.BackupDatabasesToSqlServerServiceAsync(
                            SQLLiteConnection.GetDataFromSQLite("SELECT DbName FROM DbNameBackup"),
                            SQLServerConnection.ConnectionStringGet());
                        break;
                    }
                }
            }
            _timer.Start();
        }
        protected override void OnStop()
        {
            _timer?.Stop();
        }
    }
}