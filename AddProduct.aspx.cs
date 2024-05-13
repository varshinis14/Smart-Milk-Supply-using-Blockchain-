using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SmartMilkSupply
{
    public partial class AddProduct : System.Web.UI.Page
    {
        static int ProductId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyConnection obj = new MyConnection();
                

                if (Request.QueryString["ProductId"] != null)
                {
                    ProductId = int.Parse(Request.QueryString["ProductId"].ToString());
                    LoadData();
                }
            }
        }
        public void LoadData()
        {
            MyConnection obj = new MyConnection();
            DataTable tab = new DataTable();
            tab = obj.GetProduct_ProductId(ProductId);
            txtProductName.Text = tab.Rows[0]["ProductName"].ToString();
            txtDescription.Text = tab.Rows[0]["Description"].ToString();
            txtPrice.Text = tab.Rows[0]["Price"].ToString();
            btnSave.Text = "Update";
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();
            if (btnSave.Text == "Update")
            {

                string result = obj.UpdateProduct(ProductId, int.Parse(txtPrice.Text));
                if (result == "1")
                {
                    
                    txtProductName.Text = txtDescription.Text = txtPrice.Text = "";
                    btnSave.Text = "Save";
                    lblMsg.Text = "Product Details Updated Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
            }
            else
            {
                string result = obj.CreateProduct(txtProductName.Text, txtDescription.Text, int.Parse(txtPrice.Text));
                if (result == "1")
                {
                    
                    txtProductName.Text = txtDescription.Text = txtPrice.Text = "";
                    lblMsg.Text = "Product Added Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
                else if (result == "2")
                {
                    
                    txtProductName.Text = txtDescription.Text = txtPrice.Text = "";
                    lblMsg.Text = "Product Added Already";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else if (result == "0")
                {
                    
                    txtProductName.Text = txtDescription.Text = txtPrice.Text = "";
                    lblMsg.Text = "Product Creation Error";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}