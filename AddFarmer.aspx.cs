using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartMilkSupply
{
    public partial class AddFarmer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();
            Random rnd = new Random();
            int FarmerId = (rnd.Next(100000, 999999) + DateTime.Now.Second);
            string Password = (rnd.Next(1000, 9999) + DateTime.Now.Second).ToString();

            int result = obj.AddFarmer(FarmerId,int.Parse(Session["UserId"].ToString()), txtFarmerName.Text, Password, txtMobileNo.Text, txtAddress.Text);
            if (result == 1)
            {

                string Message = "Login Credentials Farmer Id:" + FarmerId + " & Password:" + Password;

                txtFarmerName.Text = txtMobileNo.Text = txtAddress.Text = "";
                lblMsg.Text = "Farmer Account Created Successfully & " + Message;
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }

            else if (result == 0)
            {

                txtFarmerName.Text = txtMobileNo.Text = txtAddress.Text = "";
                lblMsg.Text = "Farmer Account Creation Error";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}