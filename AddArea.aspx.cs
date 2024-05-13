﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartMilkSupply
{
    public partial class AddArea : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();

            int result = obj.AddArea(txtName.Text);
            if (result == 1)
            {
                txtName.Text = "";
                lblMsg.Text = "Area Added Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                lblMsg.Font.Bold = true;

            }
            else if (result == 2)
            {
                txtName.Text = "";
                lblMsg.Text = "Area Added Already!!!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Font.Bold = true;

            }
            else
            {
                txtName.Text = "";
                lblMsg.Text = "Area Add Error";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Font.Bold = true;
            }
        }
    }
}