using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace SmartMilkSupply
{
    public partial class VDSupplyMMD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyConnection obj = new MyConnection();
                DataTable tab = new DataTable();
                tab = obj.GetVDTotal_ML(int.Parse(Session["UserId"].ToString()));
                txtMilkML.Text = tab.Rows[0]["MilkML"].ToString();
                txtMilkML.ReadOnly = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtMilkML.Text) >= 500)
            {
                MyConnection obj = new MyConnection();
                byte[] passBytes = Encoding.UTF8.GetBytes("MDMT");
                string keyvalue = string.Join("", passBytes);

                string TableName = keyvalue + "_" + keyvalue;
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
                        chv = SHAHashValue.ShaKeyGeneration(Session["UserId"].ToString(), txtMilkML.Text, logdate);
                    }
                    else
                    {
                        phv = res;
                        chv = SHAHashValue.ShaKeyGeneration(Session["UserId"].ToString(), txtMilkML.Text, logdate, phv);
                    }
                    obj = new MyConnection();
                    string result = obj.CreateTable_VDSMDT(int.Parse(Session["UserId"].ToString()), TableName, logdate, int.Parse(txtMilkML.Text), phv, chv);
                    if (result == "1")
                    {
                       
                        txtMilkML.Text = "";
                        lblMsg.Text = "Main Milk Dairy, Milk Supply Successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                    }

                    else if (result == "0")
                    {
                       
                        txtMilkML.Text = "";
                        lblMsg.Text = "Main Milk Dairy, Milk Transaction Upload Error";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            else
            {
                lblMsg.Text = "Insufficient Balance Transfer Milk";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}