using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SmartMilkSupply
{
    public partial class VDViewFMT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                MyConnection obj = new MyConnection();
                DataTable tab = new DataTable();
                string TableName = Session["UserId"].ToString() + "_" + Session["UserId"].ToString();
                tab = obj.GetCMP_Log(TableName);
                Table1.Controls.Clear();
                if (tab.Rows.Count > 0)
                {
                    lblMsg.Text = "";
                    TableRow hr = new TableRow();
                    TableHeaderCell hc1 = new TableHeaderCell();
                    TableHeaderCell hc2 = new TableHeaderCell();
                    TableHeaderCell hc3 = new TableHeaderCell();
                    TableHeaderCell hc4 = new TableHeaderCell();
                    TableHeaderCell hc5 = new TableHeaderCell();
                    TableHeaderCell hc6 = new TableHeaderCell();
                    TableHeaderCell hc7 = new TableHeaderCell();


                    hc1.Text = "Sl No";
                    hr.Cells.Add(hc1);
                    hc2.Text = "Qty";
                    hr.Cells.Add(hc2);
                    hc3.Text = "Log Date";
                    hr.Cells.Add(hc3);
                    hc4.Text = "Status";
                    hr.Cells.Add(hc4);
                    hc5.Text = "";
                    hr.Cells.Add(hc5);



                    Table1.Rows.Add(hr);
                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
                        TableRow row = new TableRow();

                        Label lblSlNo = new Label();
                        lblSlNo.Text = (i + 1).ToString();
                        TableCell SlNo = new TableCell();
                        SlNo.Controls.Add(lblSlNo);



                        Label lblQty = new Label();
                        lblQty.Text = tab.Rows[i]["ML"].ToString();
                        TableCell Qty = new TableCell();
                        Qty.Controls.Add(lblQty);

                        Label lbllogDate = new Label();
                        lbllogDate.Text = tab.Rows[i]["LogDate"].ToString();
                        TableCell logDate = new TableCell();
                        logDate.Controls.Add(lbllogDate);



                        LinkButton Recover = new LinkButton();
                        Recover.Text = "Recover";
                        Recover.ID = "lnkRecover" + i.ToString();
                        Recover.CommandArgument = tab.Rows[i]["SlNo"].ToString();
                        Recover.Click += new EventHandler(Recover_Click);

                        TableCell RecoverCell = new TableCell();
                        RecoverCell.Controls.Add(Recover);


                        MyConnection obj1 = new MyConnection();
                        int Sl_No = int.Parse(tab.Rows[i]["SlNo"].ToString());
                        string res = obj1.ChkVDMTTamper(Sl_No, TableName);
                        string imgpath = "";

                        if (res == "1")
                        {
                            imgpath = "~/images/Correct.jpg";
                            Recover.Enabled = false;
                        }
                        else
                        {
                            lblQty.ForeColor = System.Drawing.Color.Red;
                            lbllogDate.ForeColor = System.Drawing.Color.Red;
                            imgpath = "~/images/Tamper.jpg";
                        }
                        Image img = new Image();
                        img.ImageUrl = imgpath;
                        TableCell imgcell = new TableCell();
                        imgcell.Controls.Add(img);


                        row.Controls.Add(SlNo);
                        row.Controls.Add(Qty);
                        row.Controls.Add(logDate);
                        row.Controls.Add(imgcell);
                        row.Controls.Add(RecoverCell);
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

        void Recover_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();
            DataTable tab = new DataTable();
            string TableName = Session["UserId"].ToString() + "_" + Session["UserId"].ToString();
            LinkButton lnk = (LinkButton)sender;
            int SlNo = int.Parse(lnk.CommandArgument);
            string result = obj.VDMTRecover(SlNo, TableName);
            if (result == "1")
            {
                LoadData();

                lblMsg.Text = "Recover Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
        }
    }
}