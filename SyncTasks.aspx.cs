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
using System.Web.Services;
using System.Globalization;
using ClosedXML;
using DocumentFormat;
using ClosedXML.Excel;
using System.IO;
using System.Web.Security;
using System.Data.OleDb;
using System.Text;
using DataLayer;
using Newtonsoft.Json.Linq;
using System.Configuration;

public partial class SyncTasks : System.Web.UI.Page
{
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (!IsPostBack)
        {
            try
            {
                
                BindgridTask();
                BindgridResource();
                BindgridSaleCycle();
                divjob.Visible = false;
               
                divTask.Visible = false;
                divResource.Visible = false;
                divSalesCycle.Visible = false;
                grdSalesCycle.Visible = false;
                Bindgrid();
                grdjob.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }
    }
    protected void Logout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        FormsAuthentication.SignOut();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1.0);
        Response.Expires = -1000;
        Response.CacheControl = "no-cache";
        Response.Redirect("Login.aspx");
    }
    public DataTable GetPlannings()
    {
        //  DBHelper DBConn = new DBHelper();
        DataTable dt = new DataTable();
        String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(strConnString);
        SqlCommand cmd = new SqlCommand();


        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Proc_GetProject";
        cmd.Connection = con;
        try
        {
            con.Open();
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.Fill(dt);
            //grdjob.EmptyDataText = "No Records Found";
            grdjob.DataSource = dt;
            grdjob.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

            con.Close();
            con.Dispose();
        }
        return dt;
    }
    public void Bindgrid()
    {
        divjob.Visible = true;
        grdjob.Visible = true;
        //divTask.Visible = false;
        //grdTask.Visible = false;
        //grdresource.Visible = false;
        //divResource.Visible = false;
        //divSalesCycle.Visible = false;
        GetPlannings();

    }
    public void BindgridTask()
    {
        //divjob.Visible = false;
        //grdjob.Visible = false;
        divTask.Visible = true;
        grdTask.Visible = true;
        //grdresource.Visible = false;
        //divResource.Visible = false;
        //divSalesCycle.Visible = false;
        GetPlanningsTask();

    }
    public void BindgridResource()
    {
        //divjob.Visible = false;
        //grdjob.Visible = false;
        //divTask.Visible = false;
        //grdTask.Visible = false;
        //divResource.Visible = true;
        grdresource.Visible = true;
        divSalesCycle.Visible = false;
        GetPlanningsResource();

    }
    public DataTable GetPlanningsTask()
    {
        //  DBHelper DBConn = new DBHelper();
        DataTable dtTask = new DataTable();
        String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(strConnString);
        SqlCommand cmd = new SqlCommand();


        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Proc_GetTask";
        cmd.Connection = con;
        try
        {
            con.Open();
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.Fill(dtTask);
            //grdjob.EmptyDataText = "No Records Found";
            grdTask.DataSource = dtTask;
            grdTask.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

            con.Close();
            con.Dispose();
        }
        return dtTask;
    }
    public void BindgridSaleCycle()
    {
        //divjob.Visible = false;
        //grdjob.Visible = false;
        //divTask.Visible = false;
        //grdTask.Visible = false;
        //divResource.Visible = false;
        //grdresource.Visible = false;
        divSalesCycle.Visible = true;
        grdSalesCycle.Visible = true;
        GetSaleCycle();

    }
    public DataTable GetSaleCycle()
    {
        //  DBHelper DBConn = new DBHelper();
        DataTable dtSalesCycle = new DataTable();
        String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(strConnString);
        SqlCommand cmd = new SqlCommand();


        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Proc_GetSalesCycleMaster";
        cmd.Connection = con;
        try
        {
            con.Open();
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.Fill(dtSalesCycle);
            //grdjob.EmptyDataText = "No Records Found";
            grdSalesCycle.DataSource = dtSalesCycle;
            grdSalesCycle.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

            con.Close();
            con.Dispose();
        }
        return dtSalesCycle;
    }
    public DataTable GetPlanningsResource()
    {
        //  DBHelper DBConn = new DBHelper();
        DataTable dtresource = new DataTable();
        String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(strConnString);
        SqlCommand cmd = new SqlCommand();


        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Proc_Resource";
        cmd.Connection = con;
        try
        {
            con.Open();
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.Fill(dtresource);
            //grdjob.EmptyDataText = "No Records Found";
            grdresource.DataSource = dtresource;
            grdresource.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

            con.Close();
            con.Dispose();
        }
        return dtresource;
    }
    protected void btnJob_Click(object sender, EventArgs e)
    {
        divjob.Visible = true;
        grdjob.Visible = true;
        divTask.Visible = false;
        grdTask.Visible = false;
        divResource.Visible = false;
        // divSalesCycle.Visible = false;
        Bindgrid();
        //InsertJobLines();
    }
    protected void btnTask_Click(object sender, EventArgs e)
    {
        divjob.Visible = false;
        grdjob.Visible = false;
        divTask.Visible = true;
        grdTask.Visible = true;
        divResource.Visible = false;
        //  divSalesCycle.Visible = false;
        BindgridTask();
        //InsertTaskList();
    }
    protected void btnResource_Click(object sender, EventArgs e)
    {
        divjob.Visible = false;
        grdjob.Visible = false;
        divTask.Visible = false;
        grdTask.Visible = false;
        divResource.Visible = true;
        // divSalesCycle.Visible = false;
        BindgridResource();
        //InsertResourceMAster();
    }
    protected void btncontactperson_Click(object sender, EventArgs e)
    {
        divjob.Visible = false;
        grdjob.Visible = false;
        divTask.Visible = false;
        grdTask.Visible = false;
        divResource.Visible = false;
        divSalesCycle.Visible = true;
        grdSalesCycle.Visible = false;
        // InsertSalesCycle();
    }

    public async void InsertJobLines()
    {
        GlobalFunction gb = new GlobalFunction();
        string url = ODataService.ENDPOINT + "Company('ServPro Technologies Pvt. Ltd.')/Job_List";
        var Conversionjson = await gb.GetPostData("", ODataService.Service_get, url, page, "");
        JArray parseJoblines = GlobalFunction.parseJsonJobLines(Conversionjson);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Sync Job Lines Successfully');", true);

    }
    public async void InsertTaskList()
    {
        GlobalFunction gb = new GlobalFunction();
        string url = ODataService.ENDPOINT + "Company('ServPro Technologies Pvt. Ltd.')/Job_Task_Lines";
        var Conversionjson = await gb.GetPostData("", ODataService.Service_get, url, page, "");
        JArray parseJoblines = GlobalFunction.parseJsonTaskLines(Conversionjson);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Sync Task Lines Successfully');", true);
    }
    public async void InsertResourceMAster()
    {
        GlobalFunction gb = new GlobalFunction();
        string url = ODataService.ENDPOINT + "Company('ServPro Technologies Pvt. Ltd.')/Resource_List_WS";
        var Conversionjson = await gb.GetPostData("", ODataService.Service_get, url, page, "");
        JArray parseJoblines = GlobalFunction.parseJsonResourceLines(Conversionjson);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Sync Resource List Successfully');", true);
    }
    public async void InsertSalesCycle()
    {
        GlobalFunction gb = new GlobalFunction();
        string url = ODataService.ENDPOINT + "Company('ServPro Technologies Pvt. Ltd.')/Sales_Cycle_WS";
        var Conversionjson = await gb.GetPostData("", ODataService.Service_get, url, page, "");
        JArray parseJoblines = GlobalFunction.parseJsonSalesCycleLines(Conversionjson);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Sync Sales Cylce Successfully');", true);
    }


    protected void grdjob_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdresource.Visible = false;
        divResource.Visible = false;
        divTask.Visible = false;
        grdTask.Visible = false;
        divSalesCycle.Visible = false;
        grdjob.PageIndex = e.NewPageIndex;
       
        grdjob.SelectedIndex = -1;
        grdjob.EditIndex = -1;

        Bindgrid();
    }
    void clear()
    {
        txtblock.Text = string.Empty;
        txtprojectname.Text = string.Empty;
        txtprojectNo.Text = string.Empty;
        txtcontactperson.Text = string.Empty;
        txtTaskName.Text = string.Empty;
        txtusername.Text = string.Empty;
        txtrole.Text = string.Empty;
        txtResourceno.Text = string.Empty;
        txtname.Text = string.Empty;
        txtJobno.Text = string.Empty;
        txtp_no.Text = string.Empty;
    }
    protected void btnADD_Click(object sender, EventArgs e)
    {

        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[3];

        //sqlparam[0] = new SqlParameter("@id", "0");
        sqlparam[0] = new SqlParameter("@project_number", txtprojectNo.Text);
        sqlparam[1] = new SqlParameter("@project_name", txtprojectname.Text);

        sqlparam[2] = new SqlParameter("@Contact_Person", txtcontactperson.Text);
        //sqlparam[3] = new SqlParameter("@Blocked", txtblock.Text);

        int i = DBConn.Save("InsertJob", sqlparam, "");
        if (i > 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Saved Successfully');", true);
            clear();
        }
        Bindgrid();
    }
    protected void btnTask_Click1(object sender, EventArgs e)
    {
        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[3];

        //sqlparam[0] = new SqlParameter("@id", "0");
        sqlparam[0] = new SqlParameter("@JobNo", txtJobno.Text);
        sqlparam[1] = new SqlParameter("@Project_Number", txtp_no.Text);

        sqlparam[2] = new SqlParameter("@TaskName", txtTaskName.Text);


        int i = DBConn.Save("InsertTask", sqlparam, "");
        if (i > 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Saved Successfully');", true);
            clear();
        }
        BindgridTask();

    }
    protected void grdTask_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        divjob.Visible = false;
        grdjob.Visible = false;
        grdresource.Visible = false;
        divResource.Visible = false;
        divSalesCycle.Visible = false;
        grdTask.PageIndex = e.NewPageIndex;
        
        BindgridTask();
    }
    protected void btnaddResource_Click(object sender, EventArgs e)
    {
        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[4];

        //sqlparam[0] = new SqlParameter("@id", "0");
        sqlparam[0] = new SqlParameter("@ResourceNo", txtResourceno.Text);
        sqlparam[1] = new SqlParameter("@UserName", txtusername.Text);

        sqlparam[2] = new SqlParameter("@Name", txtname.Text);
        sqlparam[3] = new SqlParameter("@Role", txtrole.Text);

        int i = DBConn.Save("InsertResoueces", sqlparam, "");
        if (i > 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Saved Successfully');", true);
            clear();
        }
        BindgridResource();
    }

    protected void grdresource_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        divjob.Visible = false;
        grdjob.Visible = false;
        divTask.Visible = false;
        grdTask.Visible = false;
        divSalesCycle.Visible = false;
        grdresource.PageIndex = e.NewPageIndex;
        grdresource.SelectedIndex = -1;
        grdresource.EditIndex = -1;
        BindgridResource();
    }

    protected void btnSalesCycle_Click(object sender, EventArgs e)
    {
        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[2];

        //sqlparam[0] = new SqlParameter("@id", "0");
        sqlparam[0] = new SqlParameter("@SalesCode", txtsalecode.Text);
        sqlparam[1] = new SqlParameter("@SalesDescription", txtSalesDescription.Text);

        int i = DBConn.Save("InsertSalesCycleMaster", sqlparam, "");
        if (i > 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Saved Successfully');", true);
            clear();
        }
        BindgridSaleCycle();
    }

    protected void grdSalesCycle_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        divjob.Visible = false;
        grdjob.Visible = false;
        divTask.Visible = false;
        grdTask.Visible = false;
        grdresource.Visible = false;
        divResource.Visible = false;
        // divSalesCycle.Visible = true;
         grdSalesCycle.PageIndex = e.NewPageIndex;
         
        BindgridSaleCycle();
    }
    protected void grdjob_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        //Bindgrid();
    }
    protected void grdjob_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = (GridViewRow)grdjob.Rows[e.RowIndex];
        Label id = grdjob.Rows[e.RowIndex].FindControl("lblID") as Label;
        TextBox projectnumber = (TextBox)row.FindControl("txtProject_Number") as TextBox;
        TextBox projectname = (TextBox)row.FindControl("txtproject_name") as TextBox;
        TextBox ContactPerson = (TextBox)row.FindControl("txtContact_Person") as TextBox;
        DropDownList ddlBlocked = (DropDownList)row.FindControl("ddlEditBlocked") as DropDownList;

        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[5];

        sqlparam[0] = new SqlParameter("@id", id.Text);
        sqlparam[1] = new SqlParameter("@project_number", projectnumber.Text);
        sqlparam[2] = new SqlParameter("@project_name", projectname.Text);
        sqlparam[3] = new SqlParameter("@Contact_Person", ContactPerson.Text);
        sqlparam[4] = new SqlParameter("@Blocked", ddlBlocked.SelectedItem.Text);

        int i = DBConn.Save("Upd_Job", sqlparam, "");
        {
            grdjob.EditIndex = -1;
            Bindgrid();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Updated Successfully');", true);
        }

    }
    protected void grdjob_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void grdjob_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdjob.EditIndex = e.NewEditIndex;
        Bindgrid();
    }
    protected void grdjob_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdjob.EditIndex = -1;
        Bindgrid();
    }
    protected void grdresource_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = (GridViewRow)grdresource.Rows[e.RowIndex];
        Label id = grdresource.Rows[e.RowIndex].FindControl("lblID") as Label;
        TextBox Resourcenumber = (TextBox)row.FindControl("txtResourceNo") as TextBox;
        TextBox Username = (TextBox)row.FindControl("txtUserName") as TextBox;
        TextBox Name = (TextBox)row.FindControl("txtName") as TextBox;
        DropDownList ddlRole = (DropDownList)row.FindControl("ddlEditRole") as DropDownList;

        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[5];

        sqlparam[0] = new SqlParameter("@id", id.Text);
        sqlparam[1] = new SqlParameter("@ResourceNo", Resourcenumber.Text);
        sqlparam[2] = new SqlParameter("@UserName", Username.Text);
        sqlparam[3] = new SqlParameter("@Name", Name.Text);
        sqlparam[4] = new SqlParameter("@Role", ddlRole.SelectedItem.Text);

        int i = DBConn.Save("Upd_ResourceMaster", sqlparam, "");
        {
            grdresource.EditIndex = -1;
            BindgridResource();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Updated Successfully');", true);
        }
    }
    protected void grdresource_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdresource.EditIndex = e.NewEditIndex;
        BindgridResource();
    }
    protected void grdresource_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdresource.EditIndex = -1;
        BindgridResource();
    }
    protected void rdoStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoStatus.SelectedValue.ToString() == "Job")
        {
            divjob.Visible = true;
            grdjob.Visible = true ;
            divTask.Visible = false;
            grdTask.Visible = false;
            divResource.Visible = false;
            divSalesCycle.Visible = false;
            divSalesCycle.Visible = false;
            clear();
        }
        else if (rdoStatus.SelectedValue.ToString() == "Task")
        {
            divjob.Visible = false;
            grdjob.Visible = false;
            divTask.Visible = true;
            grdTask.Visible = true;
            grdresource.Visible = false;
            divResource.Visible = false;
            divSalesCycle.Visible = false;

        }
        else if (rdoStatus.SelectedValue.ToString() == "Resource")
        {
            divjob.Visible = false;
            grdjob.Visible = false;
            divTask.Visible = false;
            grdTask.Visible = false;
            divResource.Visible = true;
            grdresource.Visible = true;
            divSalesCycle.Visible = false;
        }
        else
        {
            divjob.Visible = false;
            grdjob.Visible = false;
            divTask.Visible = false;
            grdTask.Visible = false;
            divResource.Visible = false;
            grdresource.Visible = false;
            divSalesCycle.Visible = true;
            grdSalesCycle.Visible = true;
        }
    }


    
}