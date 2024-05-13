using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

namespace SmartMilkSupply
{
    public partial class MilkProductProduction : System.Web.UI.Page
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
            Random rnd = new Random();
            string seriesId = rnd.Next(10000, 99999).ToString();
            byte[] passBytes = Encoding.UTF8.GetBytes("MDMP");
            string keyvalue = string.Join("", passBytes);
            string TableName = keyvalue + "_" + keyvalue;
            string phv = "";
            string res = obj.Check_CMP(TableName);
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
                    chv = SHAHashValue.ShaKeyGeneration(ddlProduct.SelectedItem.Value, seriesId, txtQty.Text, logdate);
                }
                else
                {
                    phv = res;
                    chv = SHAHashValue.ShaKeyGeneration(ddlProduct.SelectedItem.Value, seriesId, txtQty.Text, logdate, phv);
                }
                obj = new MyConnection();
                string result = obj.CreateTable_MDMP(int.Parse(ddlProduct.SelectedItem.Value), int.Parse(seriesId), TableName, logdate, int.Parse(txtQty.Text), phv, chv);
                if (result == "1")
                {
                    ddlProduct.SelectedIndex = 0;
                    txtQty.Text = "";
                    lblMsg.Text = "Milk Production Uploaded Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }

                else if (result == "0")
                {
                    ddlProduct.SelectedIndex = 0;
                    txtQty.Text = "";
                    lblMsg.Text = "Milk Production Error";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }

            }
        }
    }
}