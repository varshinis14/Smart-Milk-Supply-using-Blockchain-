using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SmartMilkSupply
{
    public partial class MBViewWallet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            try
            {
                MyConnection obj = new MyConnection();
                DataTable tab = new DataTable();
                tab = obj.GetMBWallet_Id(int.Parse(Session["UserId"].ToString()));
                if (tab.Rows.Count > 0)
                {
                    Table1.Controls.Clear();
                    TableRow hr = new TableRow();
                    lblMsg.Text = "Current A/c Balance Rs." + tab.Rows[0]["Balance"].ToString() + "/-";
                    lblMsg.ForeColor = System.Drawing.Color.Green;

                    TableHeaderCell hc1 = new TableHeaderCell();
                    hc1.Text = "Deposite Date";
                    TableHeaderCell hc2 = new TableHeaderCell();
                    hc2.Text = "Amount";
                    hr.Cells.Add(hc1);
                    hr.Cells.Add(hc2);

                    Table1.Rows.Add(hr);
                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
                        TableRow row = new TableRow();

                        Label lblDepositeDate = new Label();
                        lblDepositeDate.Text = tab.Rows[i]["DepositeDate"].ToString();

                        TableCell DepositeDate = new TableCell();
                        DepositeDate.Controls.Add(lblDepositeDate);

                        Label lblAmount = new Label();
                        lblAmount.Text = tab.Rows[i]["Amount"].ToString();

                        TableCell Amount = new TableCell();
                        Amount.Controls.Add(lblAmount);

                        row.Controls.Add(DepositeDate);
                        row.Controls.Add(Amount);

                        Table1.Controls.Add(row);
                    }
                }
            }
            catch
            {
            }
        }
    }
}