using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartMilkSupply
{
    public partial class AddMBWallet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();
            string result = obj.AddMBWallet(int.Parse(Session["UserId"].ToString()), int.Parse(txtAmt.Text));
            if (result == "1")
            {
                txtAmt.Text = "";

                lblMsg.Text = "Amount Deposited Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }

            else if (result == "0")
            {
                txtAmt.Text = "";

                lblMsg.Text = "Amount Deposition Error";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}