using System;
using System.Windows.Forms;

namespace AsyenOtomatikYedekleme.Forms
{
    public partial class SplashScreenLoading : DevExpress.XtraEditors.XtraForm
    {
        public SplashScreenLoading()
        {
            InitializeComponent();
        }
        public string status = "";
        public bool IScancel = false;
        private void SplashScreenLoading_Load(object sender, EventArgs e)
        {
            labelCopyright.Text = $"Copyright© {DateTime.Now.Year} Asyen Bilişim Teknolojileri Ltd.Şti., Tüm Hakları Saklıdır V3.2";
        }
        private void SplashScreenLoading_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            timer1.Stop();
            timer1.Enabled = false;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbl_Status.Text=status;
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            IScancel = true;
        }
        private void btn_Cancel_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btn_Cancel,"Yedeklemeyi iptal et");
        }
    }
}