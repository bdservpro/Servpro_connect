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
public partial class LoginPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public DataTable CheckUser(string userName, string password)
    {
        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[2];

        sqlparam[0] = new SqlParameter("@loginName", userName);
        sqlparam[1] = new SqlParameter("@passWord", password);

        dt = DBConn.Selection("ProcCheckLoginDetails", sqlparam, "");
        return dt;

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        bool output = validateUser(txtUserName.Text, txtPassword.Text);


        if (output)
        {
            Session["username"] = txtUserName.Text;
            //   Session["EmailId"]=tObjDataTable.Rows[0]["EmailId"].ToString();
            string department = string.Empty;

            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "servpro");
            //   UserPrincipal user = UserPrincipal.FindByIdentity(ctx, "servpro\\rashmi.shinde");// + Session["username"].ToString());
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, Session["username"].ToString());// + Session["username"].ToString());

            DirectoryEntry directoryEntry = user.GetUnderlyingObject() as DirectoryEntry;

            if (directoryEntry.Properties["department"].Value.ToString() == "SUP")
            {
                Response.Redirect("Default.aspx");// ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('You do not have access to use the portal');", true);
            }
            if (directoryEntry.Properties["title"].Value.ToString().EndsWith("Manager") || directoryEntry.Properties["title"].Value.ToString().EndsWith("manager") || directoryEntry.Properties["title"].Value.ToString().EndsWith("HR") || directoryEntry.Properties["title"].Value.ToString().EndsWith("Officer") || directoryEntry.Properties["title"].Value.ToString().EndsWith("Executive Assistant") || directoryEntry.Properties["department"].Value.ToString().EndsWith("HR"))
            {
               
                    Response.Redirect("Default.aspx");
               
            }
            else
            {
                Response.Redirect("Default.aspx");
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('You do not have access to use the portal');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Invalid credentials');", true);
        }
    }
        
             
        

    public bool validateUser(string strUserName, string strOldPassword)
    {
        PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "servpro");
        bool isValid = ctx.ValidateCredentials(strUserName, strOldPassword);
        return isValid;
    }

}