using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SmartMilkSupply
{
    public partial class VDCMFarmer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyConnection obj = new MyConnection();
                
                DataTable tab = new DataTable();
                tab = obj.GetFarmer(int.Parse(Session["UserId"].ToString()));
                ddlFarmer.DataSource = tab;
                ddlFarmer.DataTextField = "Name";
                ddlFarmer.DataValueField = "FarmerId";
                ddlFarmer.DataBind();
                ddlFarmer.Items.Insert(0, "--Select--");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();
            string TableName = Session["UserId"].ToString() + "_" + Session["UserId"].ToString();
            string phv = "";
            string res = obj.Check_BMF(TableName);
            string chv = "";
            string logdate = DateTime.Now.ToString();
            if (res == "3")
            {
                lblMsg.Text = "Previous Record Tampered";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                if (res == "0")
                {
                    phv = "000";
                    chv = SHAHashValue.ShaKeyGeneration(ddlFarmer.SelectedItem.Value, txtMilkML.Text,logdate);
                }
                else
                {
                    phv = res;
                    chv = SHAHashValue.ShaKeyGeneration(ddlFarmer.SelectedItem.Value,txtMilkML.Text, logdate, phv);
                }
                obj = new MyConnection();
                string result = obj.CreateTable_VDBMF(int.Parse(ddlFarmer.SelectedItem.Value),TableName, logdate, int.Parse(txtMilkML.Text), phv, chv);
                if (result == "1")
                {
                    ddlFarmer.SelectedIndex = 0;
                    txtMilkML.Text = "";
                    lblMsg.Text = "Farmer Milk Transaction Uploaded Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }

                else if (result == "0")
                {
                    ddlFarmer.SelectedIndex = 0;
                    txtMilkML.Text = "";
                    lblMsg.Text = "Farmer Milk Transaction Upload Error";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}