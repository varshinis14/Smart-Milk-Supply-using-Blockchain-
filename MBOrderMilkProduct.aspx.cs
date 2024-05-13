using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SmartMilkSupply
{
    public partial class MBOrderMilkProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                MyConnection obj = new MyConnection();
                DataTable tab = new DataTable();
                tab = obj.GetProduct();
                ddlProduct.DataSource = tab;
                ddlProduct.DataTextField = "ProductName";
                ddlProduct.DataValueField = "ProductId";
                ddlProduct.DataBind();
                ddlProduct.Items.Insert(0, "--Select--");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();
            string result = obj.MBOrderMP(int.Parse(Session["UserId"].ToString()),int.Parse(ddlProduct.SelectedItem.Value), int.Parse(txtQty.Text));
            if (result == "1")
            {
                txtQty.Text = "";
                ddlProduct.SelectedIndex = 0;
                lblMsg.Text = "Milk Product Order Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }

            else if (result == "0")
            {
                txtQty.Text = "";
                ddlProduct.SelectedIndex = 0;
                lblMsg.Text = "Milk Product Order Error";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}