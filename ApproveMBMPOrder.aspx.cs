using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SmartMilkSupply
{
    public partial class ApproveMBMPOrder : System.Web.UI.Page
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
                Panel1.Visible = false;
            }
            LoadData();
            LoadProductData();
        }

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch
            {
            }
        }
        private void LoadData()
        {
            try
            {
                MyConnection obj = new MyConnection();
                DataTable tab = new DataTable();
                tab = obj.GetMBMPO_P(int.Parse(ddlProduct.SelectedItem.Value));

                Table1.Controls.Clear();
                if (tab.Rows.Count > 0)
                {
                    TableRow hr = new TableRow();
                    TableHeaderCell hc1 = new TableHeaderCell();
                    TableHeaderCell hc2 = new TableHeaderCell();
                    TableHeaderCell hc3 = new TableHeaderCell();
                    TableHeaderCell hc4 = new TableHeaderCell();

                    hc1.Text = "Milk Booth Name";
                    hr.Cells.Add(hc1);
                    hc2.Text = "Order Date";
                    hr.Cells.Add(hc2);
                    hc3.Text = "Qty";
                    hr.Cells.Add(hc3);
                    hc4.Text = "";
                    hr.Cells.Add(hc4);


                    Table1.Rows.Add(hr);
                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
                        TableRow row = new TableRow();


                        Label lblDName = new Label();
                        lblDName.Text = tab.Rows[i]["Name"].ToString();
                        TableCell DName = new TableCell();
                        DName.Controls.Add(lblDName);

                        Label lblOrderDate = new Label();
                        lblOrderDate.Text = tab.Rows[i]["OrderDate"].ToString();
                        TableCell OrderDate = new TableCell();
                        OrderDate.Controls.Add(lblOrderDate);

                        Label lblQty = new Label();
                        lblQty.Text = tab.Rows[i]["Qty"].ToString();
                        TableCell Qty = new TableCell();
                        Qty.Controls.Add(lblQty);

                        LinkButton Approve = new LinkButton();
                        Approve.Text = "Approve";
                        Approve.ID = "lnkApprove" + i.ToString();
                        Approve.CommandArgument = tab.Rows[i]["MBMPOId"].ToString() + "," + tab.Rows[i]["MBId"].ToString() + "," + tab.Rows[i]["Qty"].ToString();
                        Approve.Click += new EventHandler(Approve_Click);

                        TableCell ApproveCell = new TableCell();
                        ApproveCell.Controls.Add(Approve);

                        row.Controls.Add(DName);
                        row.Controls.Add(OrderDate);
                        row.Controls.Add(Qty);
                        row.Controls.Add(ApproveCell);
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

        void Approve_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();
            DataTable tab = new DataTable();
            LinkButton lnk = (LinkButton)sender;
            Session["MBMPOId_MBId"] = lnk.CommandArgument;
            Panel1.Visible = true;
            LoadProductData();
        }
        private void LoadProductData()
        {
            try
            {
                MyConnection obj = new MyConnection();
                DataTable tab = new DataTable();
                tab = obj.GetMPStock(int.Parse(ddlProduct.SelectedItem.Value));

                Table2.Controls.Clear();
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


                    Table2.Rows.Add(hr);
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

                        LinkButton ApproveProduct = new LinkButton();
                        ApproveProduct.Text = "ApproveProduct";
                        ApproveProduct.ID = "lnkApproveProduct" + i.ToString();
                        ApproveProduct.CommandArgument = tab.Rows[i]["SeriesId"].ToString() + "," + tab.Rows[i]["Balance"].ToString();
                        ApproveProduct.Click += new EventHandler(ApproveProduct_Click);

                        TableCell ApproveProductCell = new TableCell();
                        ApproveProductCell.Controls.Add(ApproveProduct);


                        row.Controls.Add(SeriesId);
                        row.Controls.Add(Balance);
                        row.Controls.Add(ApproveProductCell);
                        Table2.Controls.Add(row);

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

       
        void ApproveProduct_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();
            LinkButton lnkm = (LinkButton)sender;
            string SeriesId = lnkm.CommandArgument.Split(',')[0];
            if (int.Parse(lnkm.CommandArgument.Split(',')[1]) >= int.Parse(Session["MBMPOId_MBId"].ToString().Split(',')[2]))
            {
                string TableName = Session["MBMPOId_MBId"].ToString().Split(',')[1] + "_" + Session["MBMPOId_MBId"].ToString().Split(',')[1];
                string phv = "";
                string res = obj.Check_MBMPT(TableName);
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
                        chv = SHAHashValue.ShaKeyGeneration(ddlProduct.SelectedItem.Value, SeriesId, Session["MBMPOId_MBId"].ToString().Split(',')[2], logdate);
                    }
                    else
                    {
                        phv = res;
                        chv = SHAHashValue.ShaKeyGeneration(ddlProduct.SelectedItem.Value, SeriesId, Session["MBMPOId_MBId"].ToString().Split(',')[2], logdate, phv);
                    }
                    obj = new MyConnection();
                    string result = obj.CreateTable_MBMPOS(int.Parse(Session["MBMPOId_MBId"].ToString().Split(',')[0]), int.Parse(ddlProduct.SelectedItem.Value), int.Parse(SeriesId), TableName, logdate, int.Parse(Session["MBMPOId_MBId"].ToString().Split(',')[2]), phv, chv);
                    if (result == "1")
                    {
                        ddlProduct.SelectedIndex = 0;

                        Response.Redirect("ApproveMBMPOrder.aspx");
                    }

                    else if (result == "0")
                    {
                        ddlProduct.SelectedIndex = 0;
                        lblMsg.Text = "Milk Product Supply Milk Booth Error";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }

                }

            }
            else
            {
                lblMsg.Text = "Milk Product Insufficient Balance";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

        }
    }
}