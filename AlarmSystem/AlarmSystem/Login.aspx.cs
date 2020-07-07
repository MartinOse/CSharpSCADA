using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlarmSystem
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["username"] = null;
        }

        protected void btnLogin_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblMessage.Text = "Please check fields!";
                return;
            }
            // Check if entered credentials match any record from OPERATOR table
            string query = string.Format("select * from OPERATOR where OpUsername='{0}' and OpPassword='{1}'",txtUsername.Text, txtPassword.Text);
            DataTable table = Database.getTablefromDB(query);
            if(table.Rows.Count>0)
            {
                Session["username"] = txtUsername.Text;

                //string loginstr = "EXECUTE OperatorAction @OpUsername = '" + txtUsername.Text + "' @Action = 'Operator Login'";
                //Database.executeQuery(loginstr);

                Response.Redirect("Default.aspx");
            }
            else
            {
                lblMessage.Text = "Wrong credentials!";
                return;
            }
        }
    }
}