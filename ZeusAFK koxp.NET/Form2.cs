using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZeusAFK_koxp.NET
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            //try
            //{
            //    this.Text = WebContent.Url.ToString();
            //}
            //catch { this.Text = "ZeusAFK Blog"; }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            browserDonate.Visible = false;
            this.Left = 0;
            this.Top = 0;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            browserDonate.DocumentText = generate_donate();
            Application.DoEvents();
        }

        bool first = true;

        public string generate_donate()
        {
            StringBuilder html = new StringBuilder();
            html.Append("<html>");
            html.Append("<head>");
            html.Append("<title>ZeusAFK</title>");
            html.Append("</head>");
            html.Append("<body bgcolor=#090000>");
            html.Append("<form align=\"center\" action=\"https://www.paypal.com/cgi-bin/webscr\" method=\"post\">");
            html.Append("<input type=\"hidden\" name=\"cmd\" value=\"_donations\">");
            html.Append("<input type=\"hidden\" name=\"business\" value=\"zeusafk@gmail.com\">");
            html.Append("<input type=\"hidden\" name=\"lc\" value=\"BO\">");
            html.Append("<input type=\"hidden\" name=\"item_name\" value=\"Open ZeusAFk Tools\">");
            html.Append("<input type=\"hidden\" name=\"no_note\" value=\"0\">");
            html.Append("<input type=\"hidden\" name=\"currency_code\" value=\"USD\">");
            html.Append("<input type=\"hidden\" name=\"bn\" value=\"PP-DonationsBF:btn_\");donate_LG.gif:NonHostedGuest\">");
            html.Append("<input type=\"image\" src=\"https://www.paypalobjects.com/en_US/i/btn/btn_donate_LG.gif\" border=\"0\" name=\"submit\" target=\"_blank\" alt=\"PayPal - The safer, easier way to pay online!\">");
            html.Append("<img alt=\"\" border=\"0\" src=\"https://www.paypalobjects.com/es_XC/i/scr/pixel.gif\" width=\"1\" height=\"1\">");
            html.Append("</form>");
            html.Append("</body>");
            html.Append("</html>");
            return html.ToString();
        }

        private void browserDonate_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (first)
            {
                HtmlElement submit = browserDonate.Document.GetElementById("submit");
                submit.InvokeMember("click");
                first = false;
                toolStripTextBox1.Width = this.Width - 100;
            }
            else
            {
                browserDonate.Visible = true;
                toolText.Text = " Completo.";
                toolStripTextBox1.Text = browserDonate.Url.ToString();
            }
        }
    }
}
