using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SmartMilkSupply
{
    public partial class AddMilkBooth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyConnection obj = new MyConnection();
                DataTable tab = new DataTable();
                tab = obj.GetArea();
                ddlArea.DataSource = tab;
                ddlArea.DataTextField = "AreaName";
                ddlArea.DataValueField = "AreaId";
                ddlArea.DataBind();
                ddlArea.Items.Insert(0, "--Select--");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();
            Random rnd = new Random();
            int MBId = rnd.Next(100000, 999999);
            string pswd = rnd.Next(1000, 9999).ToString();
            int result = obj.AddMilkBooth(MBId, int.Parse(ddlArea.SelectedItem.Value), txtName.Text, pswd, txtMobileNo.Text, txtAddress.Text);
            if (result == 1)
            {
                ddlArea.SelectedIndex = 0;
                txtName.Text = txtMobileNo.Text = txtAddress.Text = "";
                lblMsg.Text = "Milk Booth Added Successfully & Milk Booth Id:" + MBId + " & Password:" + pswd;
                lblMsg.ForeColor = System.Drawing.Color.Green;
                lblMsg.Font.Bold = true;

            }
            else
            {
                txtName.Text = txtMobileNo.Text = txtAddress.Text = "";
                lblMsg.Text = "Milk Booth Add Error";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Font.Bold = true;
            }
        }
    }
}