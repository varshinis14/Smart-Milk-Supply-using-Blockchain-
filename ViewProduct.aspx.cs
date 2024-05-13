using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SmartMilkSupply
{
    public partial class ViewProduct : System.Web.UI.Page
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
                tab = obj.GetProduct();
                if (tab.Rows.Count > 0)
                {
                    Table1.Controls.Clear();
                    TableRow hr = new TableRow();

                    TableHeaderCell hc1 = new TableHeaderCell();
                    hc1.Text = "Product Name";
                    TableHeaderCell hc2 = new TableHeaderCell();
                    hc2.Text = "Description";
                    TableHeaderCell hc3 = new TableHeaderCell();
                    hc3.Text = "Price";

                    TableHeaderCell hc4 = new TableHeaderCell();
                    hc4.Text = "";


                    hr.Cells.Add(hc1);
                    hr.Cells.Add(hc2);
                    hr.Cells.Add(hc3);
                    hr.Cells.Add(hc4);

                    Table1.Rows.Add(hr);
                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
                        TableRow row = new TableRow();

                        Label lblProductName = new Label();
                        lblProductName.Text = tab.Rows[i]["ProductName"].ToString();

                        TableCell ProductName = new TableCell();
                        ProductName.Controls.Add(lblProductName);

                        Label lblDescription = new Label();
                        lblDescription.Text = tab.Rows[i]["Description"].ToString();

                        TableCell Description = new TableCell();
                        Description.Controls.Add(lblDescription);

                        Label lblPrice = new Label();
                        lblPrice.Text = tab.Rows[i]["Price"].ToString();

                        TableCell Price = new TableCell();
                        Price.Controls.Add(lblPrice);

                        LinkButton Edit = new LinkButton();
                        Edit.ID = "lnkEdit" + i.ToString();
                        Edit.Text = "Edit";
                        Edit.CommandArgument = tab.Rows[i]["ProductId"].ToString();
                        Edit.Click += new EventHandler(Edit_Click);

                        TableCell Editcell = new TableCell();
                        Editcell.Controls.Add(Edit);

                        row.Controls.Add(ProductName);
                        row.Controls.Add(Description);
                        row.Controls.Add(Price);
                        row.Controls.Add(Editcell);

                        Table1.Controls.Add(row);
                    }
                }
            }
            catch
            {
            }
        }
        void Edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            Response.Redirect("AddProduct.aspx?ProductId=" + lnk.CommandArgument.ToString());
        }
    }
}