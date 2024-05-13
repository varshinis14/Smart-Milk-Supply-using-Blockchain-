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
    public partial class MilkProductSales : System.Web.UI.Page
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
            LoadSPData();
        }

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadSPData();
            }
            catch
            {
            }
        }

        private void LoadSPData()
        {
            try
            {
                MyConnection obj = new MyConnection();
                DataTable tab = new DataTable();
                tab = obj.GetMBMPStock(int.Parse(ddlProduct.SelectedItem.Value), int.Parse(Session["UserId"].ToString()));

                Table1.Controls.Clear();
                if (tab.Rows.Count > 0)
                {
                    TableRow hr = new TableRow();
                    TableHeaderCell hc1 = new TableHeaderCell();
                    TableHeaderCell hc2 = new TableHeaderCell();
                    TableHeaderCell hc3 = new TableHeaderCell();
                    TableHeaderCell hc4 = new TableHeaderCell();

                    hc1.Text = "Series Id";
                    hr.Cells.Add(hc1);
                    hc2.Text = "Balance";
                    hr.Cells.Add(hc2);
                    hc3.Text = "";
                    hr.Cells.Add(hc3);


                    Table1.Rows.Add(hr);
                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
                        TableRow row = new TableRow();


                        Label lblSeriesId = new Label();
                        lblSeriesId.Text = tab.Rows[i]["SeriesId"].ToString();
                        TableCell SeriesId = new TableCell();
                        SeriesId.Controls.Add(lblSeriesId);

                        Label lblBalance = new Label();
                        lblBalance.Text = tab.Rows[i]["Balance"].ToString();
                        TableCell Balance = new TableCell();
                        Balance.Controls.Add(lblBalance);

                        LinkButton SPSales = new LinkButton();
                        SPSales.Text = "SPSales";
                        SPSales.ID = "lnkSPSales" + i.ToString();
                        SPSales.CommandArgument = tab.Rows[i]["SeriesId"].ToString() + "," + tab.Rows[i]["Balance"].ToString() + "," + tab.Rows[i]["SMBMPId"].ToString();
                        SPSales.Click += new EventHandler(SPSales_Click);

                        TableCell ApproveSPCell = new TableCell();
                        ApproveSPCell.Controls.Add(SPSales);


                        row.Controls.Add(SeriesId);
                        row.Controls.Add(Balance);
                        row.Controls.Add(ApproveSPCell);
                        Table1.Controls.Add(row);

                    }
                }
                else
                {
                    //lblMsg.Text = "No Record Found";
                }
            }
            catch
            {

            }
        }

        void SPSales_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();
            LinkButton lnkm = (LinkButton)sender;
            string SeriesId = lnkm.CommandArgument.Split(',')[0];
            byte[] passBytes = Encoding.UTF8.GetBytes("MPS");
            string keyvalue = string.Join("", passBytes);
            int SMBMPId = int.Parse(lnkm.CommandArgument.Split(',')[2]);
            if (int.Parse(lnkm.CommandArgument.Split(',')[1]) >= int.Parse(txtQty.Text))
            {
                string TableName = Session["UserId"].ToString() + "_" + keyvalue;
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
                        chv = SHAHashValue.ShaKeyGeneration(ddlProduct.SelectedItem.Value, SeriesId, txtQty.Text, logdate);
                    }
                    else
                    {
                        phv = res;
                        chv = SHAHashValue.ShaKeyGeneration(ddlProduct.SelectedItem.Value, SeriesId, txtQty.Text, logdate, phv);
                    }
                    obj = new MyConnection();
                    string result = obj.CreateTable_MBPSales(int.Parse(ddlProduct.SelectedItem.Value), int.Parse(SeriesId), TableName, logdate, int.Parse(txtQty.Text), phv, chv, SMBMPId);
                    if (result == "1")
                    {
                        ddlProduct.SelectedIndex = 0;
                        txtQty.Text = "";
                        Response.Redirect("MilkProductSales.aspx");
                    }

                    else if (result == "0")
                    {
                        ddlProduct.SelectedIndex = 0;
                        lblMsg.Text = "Milk Product Sales Error";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }

                }

            }
            else
            {
                lblMsg.Text = "Snacks Product Insufficient Balance";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}