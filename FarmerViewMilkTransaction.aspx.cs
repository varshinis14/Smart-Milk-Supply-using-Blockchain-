using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SmartMilkSupply
{
    public partial class FarmerViewMilkTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();
            DataTable tab = new DataTable();
            tab = obj.GetMilkTransaction_FarmerId(int.Parse(Session["UserId"].ToString()));
            if (tab.Rows.Count > 0)
            {
                lblMsg.Text = "";

                Table1.Controls.Clear();

                TableRow hr = new TableRow();

                TableHeaderCell hc1 = new TableHeaderCell();
                TableHeaderCell hc2 = new TableHeaderCell();
                TableHeaderCell hc3 = new TableHeaderCell();

                hc1.Text = "Village Dairy Name";
                hr.Cells.Add(hc1);
                hc2.Text = "Transaction Date";
                hr.Cells.Add(hc2);
                hc3.Text = "Milk (mlt)";
                hr.Cells.Add(hc3);

                Table1.Rows.Add(hr);

                for (int i = 0; i < tab.Rows.Count; i++)
                {

                    TableRow row = new TableRow();

                    Label lblName = new Label();
                    lblName.Text = tab.Rows[i]["Name"].ToString();
                    TableCell MBName = new TableCell();
                    MBName.Controls.Add(lblName);

                    Label lblTDate = new Label();
                    lblTDate.Text = tab.Rows[i]["LogDate"].ToString();
                    TableCell TDate = new TableCell();
                    TDate.Controls.Add(lblTDate);

                    Label lblMilkML = new Label();
                    lblMilkML.Text = tab.Rows[i]["ML"].ToString();
                    TableCell MilkML = new TableCell();
                    MilkML.Controls.Add(lblMilkML);

                    row.Controls.Add(MBName);
                    row.Controls.Add(TDate);
                    row.Controls.Add(MilkML);

                    Table1.Controls.Add(row);

                }


            }
            else
            {
                lblMsg.Text = "No Record Found";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

            DataTable tabpay = new DataTable();
            tabpay = obj.GetMilkPayment_FarmerId(int.Parse(Session["UserId"].ToString()));
            if (tabpay.Rows.Count > 0)
            {
                DataTable tabbalance = obj.GetBalance_FarmerId(int.Parse(Session["UserId"].ToString()));

                lblPayment.Text = "Total Amount Rs." + tabbalance.Rows[0]["Balance"].ToString() + "/-";
                lblPayment.ForeColor = System.Drawing.Color.Green;

                Table2.Controls.Clear();

                TableRow hr = new TableRow();

                TableHeaderCell hc1 = new TableHeaderCell();
                TableHeaderCell hc2 = new TableHeaderCell();
                TableHeaderCell hc3 = new TableHeaderCell();

                hc1.Text = "Deposit Date";
                hr.Cells.Add(hc1);
                hc2.Text = "Amount";
                hr.Cells.Add(hc2);

                Table2.Rows.Add(hr);

                for (int i = 0; i < tabpay.Rows.Count; i++)
                {

                    TableRow row = new TableRow();

                    Label lblDDate = new Label();
                    lblDDate.Text = tabpay.Rows[i]["DDate"].ToString();
                    TableCell DDate = new TableCell();
                    DDate.Controls.Add(lblDDate);

                    Label lblAmount = new Label();
                    lblAmount.Text = tabpay.Rows[i]["Amount"].ToString();
                    TableCell Amount = new TableCell();
                    Amount.Controls.Add(lblAmount);

                    row.Controls.Add(DDate);
                    row.Controls.Add(Amount);

                    Table2.Controls.Add(row);

                }


            }
            else
            {
                lblPayment.Text = "No Record Found";
                lblPayment.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}