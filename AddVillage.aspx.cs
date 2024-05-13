using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartMilkSupply
{
    public partial class AddVillage : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();

            int result = obj.AddVillage(txtName.Text);
            if (result == 1)
            {
                txtName.Text = "";
                lblMsg.Text = "Village Added Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                lblMsg.Font.Bold = true;

            }
            else if (result == 2)
            {
                txtName.Text = "";
                lblMsg.Text = "Village Added Already!!!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Font.Bold = true;

            }
            else
            {
                txtName.Text = "";
                lblMsg.Text = "Village Add Error";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Font.Bold = true;
            }

        }
    }
}