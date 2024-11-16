using System;
using System.Diagnostics;
namespace AsyenOtomatikYedekleme.Forms
{
    public partial class AboutForm : DevExpress.XtraEditors.XtraForm
    {
        public AboutForm()
        {
            InitializeComponent();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.asyen.com.tr",
                UseShellExecute = true
            });
        }
        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBox1, "Asyen Web Sitesine Git");
        }
    }
}