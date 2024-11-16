using AsyenOtomatikYedekleme.Classes;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Windows.Forms;
namespace AsyenOtomatikYedekleme.Forms
{
    public partial class Home : XtraForm
    {
        public Home()
        {
            InitializeComponent();
        }
        private void btn_BackUp_Click(object sender, EventArgs e)
        {
            BackUp backup = new BackUp();
            _ = backup.BackupDatabasesToSqlServerAsync(this, SQLLiteConnection.GetDataFromSQLite("SELECT DbName FROM DbNameBackup"), SQLServerConnection.ConnectionStringGet());
        }
        private void Home_Load(object sender, EventArgs e)
        {
            // emailayarları lazım olduğunda bakıcaz
            eMailToolStripMenuItem.Visible = false;
            gridView1.OptionsSelection.MultiSelectMode =GridMultiSelectMode.RowSelect;
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false; 
            gridView1.OptionsBehavior.Editable = false;
            bool statusData = SQLLiteConnection.GetSqlConnectionControl("SELECT DbName 'Veritabani Adi' FROM DbNameBackup DbName");
            if (statusData)
            {
                SQLLiteConnection sQLLiteConnection = new SQLLiteConnection();
                sQLLiteConnection.LoadDataIntoGridView(gridControl1, "SELECT DbName 'Veritabani Adi' FROM DbNameBackup DbName");
            }
            else
            {
                SQLServerConnection connection = new SQLServerConnection();
                connection.LoadDataIntoGridView(gridControl1, "SELECT name 'Veritabani Adi' FROM sys.databases WHERE name NOT IN ('master','tempdb','model','msdb')");
            }
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }
        private void dbGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SQLServerConnection connection = new SQLServerConnection();
            connection.LoadDataIntoGridView(gridControl1, "SELECT name 'Veritabani Adi' FROM sys.databases WHERE name NOT IN ('master','tempdb','model','msdb')");
        }
        private void backUpSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackUpSettingsForms backUpSettingscs = new BackUpSettingsForms();
            backUpSettingscs.ShowDialog();
        }
        private void eMailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MailSettingsForm mailSettingsForm = new MailSettingsForm();
            mailSettingsForm.ShowDialog();
        }
        private void Home_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (gridView1.GetSelectedRows().Length > 0)
                gridView1.DeleteRow(gridView1.GetSelectedRows()[0]);
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            SQLLiteConnection.InserUpdateDelete($"DELETE FROM DbNameBackup");
            GridView view = gridView1;
            for (byte i = 0; i < view.RowCount; i++)
            {
                int rowHandle = view.GetRowHandle(i);
                DataRow row = view.GetDataRow(rowHandle);
                if (row == null)
                    continue;
                string column1Value = row["Veritabani Adi"].ToString();
                SQLLiteConnection.InserUpdateDelete($"INSERT INTO DbNameBackup (DbName) VALUES ('{column1Value}') ");
            }
        }
        private void btn_Update_Click(object sender, EventArgs e)
        {
            SQLServerConnection connection = new SQLServerConnection();
            connection.LoadDataIntoGridView(gridControl1, "SELECT name 'Veritabani Adi' FROM sys.databases WHERE name NOT IN ('master','tempdb','model','msdb')");
        }
    }
}