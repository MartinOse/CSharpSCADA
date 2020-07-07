using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;

namespace AlarmSystem
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            lblOperator.Text = "Operator: "+ Session["username"].ToString();
        }

        protected void btnSingout_Clicked(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        [WebMethod]
        public static string GetAllAlarms()
        {
            string query = "SELECT AlarmId, TagId, AlarmType, DataId, ActivationTime, AcknowledgeTime FROM AlarmSystem";
            return CommonMethods.SerializereObject(new { Records = Database.getTablefromDB(query) });
        }
		
		[WebMethod]
        public void chartUpdate()
        {
            SqlTemp.Update();
        }

        [WebMethod]
        public static string AcknowledgeAlarm(int id)
        {
            // SQL datetime format is: yyyy-MM-dd HH:mm:ss.fff !
            string query = "UPDATE ALARM_DATA SET AcknowledgeTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") +"' WHERE AlarmId="+id;
            try
            {
                Database.executeQuery(query);
                return CommonMethods.SerializereObject(new { Success = true });
            }
            catch(Exception ex)
            {
                return CommonMethods.SerializereObject(new { Success = false, Message=ex.Message });
            }
        }

        
        [WebMethod]
        protected void Button1_Click(object sender, EventArgs e)
        {
           string query1 = "EXECUTE SETPOINT_CHANGE " + Math.Round(double.Parse(TextBox1.Text), 2, MidpointRounding.AwayFromZero);
           string query2 = "EXECUTE OperatorAction '" + Session["username"] + "', 'Setpoint change'";

           if (double.Parse(TextBox1.Text) > 20 || double.Parse(TextBox1.Text) < 50)
           {
                Database.executeQuery(query1);
                Database.executeQuery(query2);
           }
        }

        

        /*
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        */
    }
}