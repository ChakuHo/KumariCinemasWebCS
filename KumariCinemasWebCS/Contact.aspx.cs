using System;

namespace KumariCinemasWebCS
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            lblMsg.CssClass = "text-success";
            lblMsg.Text = "Message submitted (demo).";
            txtName.Text = txtEmail.Text = txtMessage.Text = "";
        }
    }
}