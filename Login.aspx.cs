using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using DataLayer;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public DataTable CheckUser(string userName)
    {
        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[1];

        sqlparam[0] = new SqlParameter("@Username", userName);
        dt = DBConn.Selection("Proc_GetResourceMaster", sqlparam, "");
        return dt;

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
      // bool output = validateUser(username.Value, password.Value);


       //if (output)
       // {
            Session["username"] = username.Value;
            //   Session["EmailId"]=tObjDataTable.Rows[0]["EmailId"].ToString();
            string department = string.Empty;

            DataTable dt=CheckUser(username.Value);
            if (dt.Rows.Count > 0)
            {
                Session["Role"] = dt.Rows[0]["Role"].ToString();
                Session["ResourceNo"] = dt.Rows[0]["ResourceNo"].ToString();
                //Session["resorcename"] = dt.Rows[0]["resorcename"].ToString();
                Session["Name"] = dt.Rows[0]["Name"].ToString();
                Session["Role"] = dt.Rows[0]["Role"].ToString();

            }
            else 
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('User is not exist in Datatbase');", true);
            }
            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "servpro");
            ////   UserPrincipal user = UserPrincipal.FindByIdentity(ctx, "servpro\\rashmi.shinde");// + Session["username"].ToString());
            //UserPrincipal user = UserPrincipal.FindByIdentity(ctx, Session["username"].ToString());// + Session["username"].ToString());

            Response.Redirect("Task_management.aspx");

            //DirectoryEntry directoryEntry = user.GetUnderlyingObject() as DirectoryEntry;

            //if (directoryEntry.Properties["department"].Value.ToString() == "SUP")
            //{
            //    Response.Redirect("Default.aspx");// ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('You do not have access to use the portal');", true);
            //}
            //if (directoryEntry.Properties["title"].Value.ToString().EndsWith("Manager") || directoryEntry.Properties["title"].Value.ToString().EndsWith("manager") || directoryEntry.Properties["title"].Value.ToString().EndsWith("HR") || directoryEntry.Properties["title"].Value.ToString().EndsWith("Officer") || directoryEntry.Properties["title"].Value.ToString().EndsWith("Executive Assistant") || directoryEntry.Properties["department"].Value.ToString().EndsWith("HR"))
            //{

            //    Response.Redirect("Default.aspx");

            //}
            //else
            //{
            //    Response.Redirect("Default.aspx");
            //    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('You do not have access to use the portal');", true);
            //}
        //}
        //else
        //{
        //   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Invalid credentials');", true);
        //}
    }




    public bool validateUser(string strUserName, string strOldPassword)
    {
        PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "servpro");
        bool isValid = ctx.ValidateCredentials(strUserName, strOldPassword);
        return isValid;
    }

}