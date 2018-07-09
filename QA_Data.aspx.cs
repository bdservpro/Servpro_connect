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

public partial class QA_Data : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection("server=servpro40;database=ConnectDB1;uid=demo;password=pro@1234");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
            GetSalesCycleMaster();
            //Get_ddl();
        }
    }
    public void BindGrid()
    {
        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[0];
        dt = DBConn.Selection("Get_qa_Data", sqlparam, "");

        if (dt.Rows.Count > 0)
        {
            //dt.Columns.Add("Action");
            gvQaDate.DataSource = dt;
            gvQaDate.DataBind();
        }

    }
    public void GetSalesCycleMaster()
    {
        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[0];
        dt = DBConn.Selection("Proc_GetSalesCycleMaster", sqlparam, "");

        if (dt.Rows.Count > 0)
        {
            drp_salescyles.DataSource = dt;
            drp_salescyles.DataTextField = "SalesDescription";
            drp_salescyles.DataValueField = "SalesCode";
            drp_salescyles.DataBind();
            drp_salescyles.Items.Insert(0, new ListItem("Select", string.Empty));
        }
    }
    void clear()
    {

        lblprojectNumber.Text = null;
        txt_projectname.Text = null;
        lbltasknumber.Text = null;
        drp_taskname.SelectedItem.Value = "Select";
        //drp_salescyles.SelectedIndex=1;
        //drp_salescyles.SelectedItem.Value = null;
        txtApkIos.Text = null;
        txtDescription.Text = null;
        txtRetesting_status.Text = null;
        txtAditionalDescription.Text = null;
        txtSVNInfo.Text = null;
        // lblcyclenumber.Text = null;
        // drp_taskname.SelectedItem.Text = "";
        // drp_taskname.SelectedItem.Value = null;
        //drp_taskname.SelectedValue = "";
        Label1.Text = "";
        ReleaseDate.Value = string.Empty;

        txt_projectname.Text = string.Empty;
        lblprojectNumber.Text = string.Empty;
        drp_taskname.SelectedValue = string.Empty;

        drp_salescyles.SelectedValue = string.Empty;

        lblprojectNumber.Text = string.Empty;
        lbltasknumber.Text = string.Empty;
        lblcyclenumber.Text = string.Empty;
    }

    //public void Get_ddl()
    //{
        
    //    SqlCommand cmd = new SqlCommand("GET_ROLE", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    sda.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {

    //        //ddlAssign.DataSource = dt;
    //        //ddlAssign.DataTextField = "Name";
    //        //ddlAssign.DataValueField = "ResourceNo";
    //        //ddlAssign.DataBind();
    //        //ddlAssign.Items.Insert(0, new ListItem("Select", string.Empty));
    //    }
    //}
    [WebMethod]
    public static List<projects> GetProjectList(string projectName)
    {
        List<projects> projects = new List<projects>();

        DataTable dt = GetProjects(projectName);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                projects.Add(new projects { Project_Name = dt.Rows[i]["Project_Name"].ToString(), Project_Number = dt.Rows[i]["Project_Number"].ToString() });
            }
        }
        return projects;


    }
    public void GetTasks(string projectName)
    {
        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[1];

        sqlparam[0] = new SqlParameter("@JobNumber", projectName);
        dt = DBConn.Selection("Proc_GetTaskMaster", sqlparam, "");
        if (dt.Rows.Count > 0)
        {
            drp_taskname.DataSource = dt;
            drp_taskname.DataTextField = "TaskName";
            drp_taskname.DataValueField = "Project_Number";
            drp_taskname.DataBind();
            drp_taskname.Items.Insert(0, new ListItem("Select", string.Empty));
        }
    }
    public class projects
    {
        public projects()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string Project_Name { get; set; }
        public string Project_Number { get; set; }
    }
    public static DataTable GetProjects(string projectName)
    {
        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[1];

        sqlparam[0] = new SqlParameter("@project_Name", projectName);
        dt = DBConn.Selection("Proc_GetProjectMaster", sqlparam, "");
        return dt;

    }

    protected void txt_projectname_TextChanged(object sender, EventArgs e)
    {
        lblprojectNumber.Text = hfProjectid.Value;

        if (!string.IsNullOrEmpty(lblprojectNumber.Text))
            drp_taskname.Enabled = true;
        else
            drp_taskname.Enabled = false;
        GetTasks(lblprojectNumber.Text);
    }
    protected void drp_taskname_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbltasknumber.Text = drp_taskname.SelectedValue;
    }
    protected void drp_salescyles_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblcyclenumber.Text = drp_salescyles.SelectedValue;
    }
    protected void gvQaDate_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void gvQaDate_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvQaDate.EditIndex = e.NewEditIndex;
        BindGrid();
    }
    protected void gvQaDate_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = (GridViewRow)gvQaDate.Rows[e.RowIndex];
        int id = Int32.Parse(gvQaDate.DataKeys[e.RowIndex].Value.ToString());
        Label project_number = (Label)row.FindControl("lblproject_number");
        Label project_name = (Label)row.FindControl("lblproject_name");
        Label task_number = (Label)row.FindControl("lblTaskNumber");
        DropDownList task_name = (DropDownList)row.FindControl("drptask_name");
        Label cycle_number = (Label)row.FindControl("lblsales_cycleNumber");
        DropDownList drpsales_cycles = (DropDownList)row.FindControl("drpsales_cycle");
        TextBox Release_date = (TextBox)row.FindControl("txtRelease_date");
        TextBox Apk_IOS = (TextBox)row.FindControl("txtApk_IOS");
        TextBox task_description = (TextBox)row.FindControl("txttask_description");
        TextBox addtional_description = (TextBox)row.FindControl("txtadditional_descrption");
        TextBox retesting_status = (TextBox)row.FindControl("txtRetesting_status");
        TextBox SVN_Information = (TextBox)row.FindControl("txtSVN_Information");


        //if (start_date.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Please enter start date');", true);

        //}
        //else if (task_description.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Please enter Task Description');", true);

        //}
        //else if (project_name.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Please enter Project Name');", true);

        //}
        //else if (start_time.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Please enter Start Time');", true);

        //}
        //else
        //{

        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[17];

        string ResourceNo = string.Empty;
        string ResourceName = string.Empty;

        ResourceNo = Session["ResourceNo"].ToString();
        ResourceName = Session["Name"].ToString();

        sqlparam[0] = new SqlParameter("@Project_Number", project_number.Text);
        sqlparam[1] = new SqlParameter("@Project_Name", project_name.Text);
        sqlparam[2] = new SqlParameter("@Task_Number", task_number.Text);
        sqlparam[3] = new SqlParameter("@Task_Name", task_name.SelectedItem.Text);
        sqlparam[4] = new SqlParameter("@Sales_Cycle", drp_salescyles.SelectedItem.Text);
        sqlparam[5] = new SqlParameter("@Sale_cycle_number", drp_salescyles.SelectedItem.Value);
        sqlparam[6] = new SqlParameter("@Date_ofRelease", Release_date.Text);
        sqlparam[7] = new SqlParameter("@Apk_IOS", Apk_IOS.Text);
        sqlparam[8] = new SqlParameter("@Description", task_description.Text);
        sqlparam[9] = new SqlParameter("@Retesting_status", retesting_status.Text);
        sqlparam[10] = new SqlParameter("@Additional_Description", addtional_description.Text);
        sqlparam[11] = new SqlParameter("@SVN_Information", SVN_Information.Text);
        sqlparam[12] = new SqlParameter("@Created_By", ResourceName);
        sqlparam[13] = new SqlParameter("@Created_Date", DateTime.Now);
        sqlparam[14] = new SqlParameter("@modify_By", "dhiraj");
        sqlparam[15] = new SqlParameter("@Modify_Date", "");
        sqlparam[16] = new SqlParameter("@id", id);

        int i = DBConn.Save("qa_Data_Update", sqlparam, "");
        {
            gvQaDate.EditIndex = -1;
            BindGrid();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Updated Successfully');", true);
        }

    }
    protected void gvQaDate_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

        gvQaDate.EditIndex = -1;
        BindGrid();
        
    }
    protected void gvQaDate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && gvQaDate.EditIndex == e.Row.RowIndex)
        {


            DropDownList drpsales_cycle = (DropDownList)e.Row.FindControl("drpsales_cycle");
            Label lblproject_number = (Label)e.Row.FindControl("lblproject_number");
            Label labelhours = (Label)e.Row.FindControl("lbl_hours12");
            DropDownList drptask_name = (DropDownList)e.Row.FindControl("drptask_name");
            DropDownList drphours = (DropDownList)e.Row.FindControl("drphours");
            DropDownList drpminutes = (DropDownList)e.Row.FindControl("drpminutes");
            Label lblsales_cycleNumber = (Label)e.Row.FindControl("lblsales_cycleNumber");
            Label lbltask_nameNumber = (Label)e.Row.FindControl("lbltask_nameNumber");




            DBHelper DBConn = new DBHelper();
            DataTable dt;
            SqlParameter[] sqlparam;
            DataTable dt1;
            SqlParameter[] sqlparam1;
            sqlparam = new SqlParameter[0];
            sqlparam1 = new SqlParameter[1];
            dt = DBConn.Selection("Proc_GetSalesCycleMaster", sqlparam, "");


            drpsales_cycle.DataSource = dt;
            drpsales_cycle.DataTextField = "SalesDescription";
            drpsales_cycle.DataValueField = "SalesCode";
            drpsales_cycle.DataBind();


            drpsales_cycle.Items.FindByValue((e.Row.FindControl("lblsales_cycleNumber") as Label).Text).Selected = true;

            sqlparam1[0] = new SqlParameter("@JobNumber", lblproject_number.Text);
            dt1 = DBConn.Selection("Proc_GetTaskMaster", sqlparam1, "");

            drptask_name.DataSource = dt1;
            drptask_name.DataTextField = "TaskName";
            drptask_name.DataValueField = "Project_Number";
            drptask_name.DataBind();
            drptask_name.Items.FindByValue((e.Row.FindControl("lblTaskNumber") as Label).Text).Selected = true;





        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        InsertPlanning();
    }
    protected void InsertPlanning()
    {
        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[16];

        //int O_hours = Convert.ToInt32(drp_hours.Value);
        //int O_minutes = Convert.ToInt32(drp_minutes.Value);
        //string starttime = timepicker1.Value;
        //string[] arr = starttime.Split('.');
        //int S_hours = Convert.ToInt32(arr[0]);
        //string startimehours = string.Empty;
        //if (arr[0].Length == 1)
        //{
        //    startimehours = "0" + arr[0];
        //}
        //else
        //{
        //    startimehours = arr[0];
        //}
        //int S_minutes = Convert.ToInt32(arr[1].Replace(" AM", ""));
        //int O_HS = O_hours + S_hours;
        //int S_HS = O_minutes + S_minutes;
        //string end_time = Convert.ToString(O_HS + ":" + S_HS);

        //TimeSpan t1 = TimeSpan.Parse(drp_hours.Value + ":" + drp_minutes.Value);
        //TimeSpan t2 = TimeSpan.Parse(arr[0] + ":" + arr[1]);
        //TimeSpan t3 = t1.Add(t2);
        string ResourceNo = string.Empty;
        string ResourceName = string.Empty;
        //if (drpresource_Admin.SelectedValue == "")
        //{
        ResourceNo = Session["ResourceNo"].ToString();
        ResourceName = Session["Name"].ToString();
        //}
        //else
        //{
        //    ResourceNo = drpresource_Admin.SelectedValue;
        //    ResourceName = drpresource_Admin.SelectedItem.Text;
        //}

        sqlparam[0] = new SqlParameter("@Project_Number", lblprojectNumber.Text);
        sqlparam[1] = new SqlParameter("@Project_Name", txt_projectname.Text);
        sqlparam[2] = new SqlParameter("@Task_Number", lbltasknumber.Text);
        sqlparam[3] = new SqlParameter("@Task_Name", drp_taskname.SelectedItem.Text);
        sqlparam[4] = new SqlParameter("@Sales_Cycle", drp_salescyles.SelectedItem.Text);
        sqlparam[5] = new SqlParameter("@Sale_cycle_number", drp_salescyles.SelectedItem.Value);
        sqlparam[6] = new SqlParameter("@Date_ofRelease", DateTime.Now);
        sqlparam[7] = new SqlParameter("@Apk_IOS", txtApkIos.Text);
        sqlparam[8] = new SqlParameter("@Description", txtDescription.Text);
        sqlparam[9] = new SqlParameter("@Retesting_status", txtRetesting_status.Text);
        sqlparam[10] = new SqlParameter("@Additional_Description", txtAditionalDescription.Text);
        sqlparam[11] = new SqlParameter("@SVN_Information", txtSVNInfo.Text);
        sqlparam[12] = new SqlParameter("@Created_By", ResourceName);
        sqlparam[13] = new SqlParameter("@Created_Date", DateTime.Now);
        sqlparam[14] = new SqlParameter("@modify_By", "");
        sqlparam[15] = new SqlParameter("@Modify_Date", "");



        int i = DBConn.Save("qa_Data_Insert", sqlparam, "");
        if (i > 0)
        {

            BindGrid();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Saved Successfully');", true);
            clear();
        }



        //serviceRef();

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

    protected void gvQaDate_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvQaDate.PageIndex = e.NewPageIndex;
        gvQaDate.SelectedIndex = -1;
        gvQaDate.EditIndex = -1;
        BindGrid();
    }
}