using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SmartMilkSupply
{
    public partial class AddVillageDairy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyConnection obj = new MyConnection();
                DataTable tab = new DataTable();
                tab = obj.GetVillage();
                ddlvillage.DataSource = tab;
                ddlvillage.DataTextField = "VillageName";
                ddlvillage.DataValueField = "VillageId";
                ddlvillage.DataBind();
                ddlvillage.Items.Insert(0, "--Select--");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();
            Random rnd = new Random();
            int VDId = rnd.Next(100000, 999999);
            string pswd = rnd.Next(1000, 9999).ToString();
            int result = obj.AddVillageDairy(VDId, int.Parse(ddlvillage.SelectedItem.Value), txtName.Text, pswd, txtMobileNo.Text, txtAddress.Text);
            if (result == 1)
            {
                ddlvillage.SelectedIndex = 0;
                txtName.Text = txtMobileNo.Text = txtAddress.Text = "";
                lblMsg.Text = "Village Dairy Added Successfully & VillageDairy Id:" + VDId + " & Password:" + pswd;
                lblMsg.ForeColor = System.Drawing.Color.Green;
                lblMsg.Font.Bold = true;

            }
            else if (result == 2)
            {
                txtName.Text = txtMobileNo.Text = txtAddress.Text = "";
                lblMsg.Text = "Village Dairy Added Already!!!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Font.Bold = true;

            }
            else
            {
                txtName.Text = txtMobileNo.Text = txtAddress.Text = "";
                lblMsg.Text = "Village Dairy Add Error";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Font.Bold = true;
            }
        }
    }
}