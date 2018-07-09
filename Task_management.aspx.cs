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

public partial class Task_management : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection("server=servpro40;database=ConnectDB1;uid=demo;password=pro@1234");
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Name"] == null || Session["Name"] == string.Empty)
        {
            Response.Redirect("Login.aspx", true);

        }

       
        if (!IsPostBack)
        {
            try
            {

               
                GetSalesCycleMaster();

                GetResources();
                Get_ddl();
                Bindgrid();
            }
            catch (Exception ex)
            {
            }
        }

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
    public void GetResources()
    {
        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[0];
        dt = DBConn.Selection("Proc_GetResources", sqlparam, "");

        if (dt.Rows.Count > 0)
        {
            string Role = Session["Role"].ToString();
            if (Role == "PMO")
            {
                drp_resourcename.Enabled = true;
                drp_resourcename.DataSource = dt;
                drp_resourcename.DataTextField = "Name";
                drp_resourcename.DataValueField = "ResourceNo";
                drp_resourcename.DataBind();
                drp_resourcename.Items.Insert(0, new ListItem("All", "All"));
                drp_resourcename.Items.FindByText(Session["Name"].ToString()).Selected = true;

                drpresource_Admin.Visible = true;
                drpresource_Admin.DataSource = dt;
                drpresource_Admin.DataTextField = "Name";
                drpresource_Admin.DataValueField = "ResourceNo";
                drpresource_Admin.DataBind();
                drpresource_Admin.Items.Insert(0, new ListItem("-Select-", ""));
                drpresource_Admin.Items.FindByText(Session["Name"].ToString()).Selected = true;
                lblsyncnav.Visible = true;
                updatediv.Visible = true;
                lblQA.Visible = true;
                lblApproved.Visible = true;
            }
            else  if (Role == "TL")
            {
                drp_resourcename.Enabled = true;
                drp_resourcename.DataSource = dt;
                drp_resourcename.DataTextField = "Name";
                drp_resourcename.DataValueField = "ResourceNo";
                drp_resourcename.DataBind();
                drp_resourcename.Items.Insert(0, new ListItem("All", "All"));
                drp_resourcename.Items.FindByText(Session["Name"].ToString()).Selected = true;

                drpresource_Admin.Visible = true;
                drpresource_Admin.DataSource = dt;
                drpresource_Admin.DataTextField = "Name";
                drpresource_Admin.DataValueField = "ResourceNo";
                drpresource_Admin.DataBind();
                drpresource_Admin.Items.Insert(0, new ListItem("-Select-", ""));
                drpresource_Admin.Items.FindByText(Session["Name"].ToString()).Selected = true;
                lblsyncnav.Visible = true;
                updatediv.Visible = true;
                lblQA.Visible = true;
                lblApproved.Visible = true;
            }
            else
            {
                drpresource_Admin.Visible = false;
                ResourceName_Row.Visible = false;
                drp_resourcename.DataSource = dt;
                drp_resourcename.DataTextField = "Name";
                drp_resourcename.DataValueField = "ResourceNo";
                drp_resourcename.DataBind();
                drp_resourcename.Items.FindByText(Session["Name"].ToString()).Selected = true;
                drp_resourcename.Enabled = false;
                lblsyncnav.Visible = false;
                updatediv.Visible = false;
                lblQA.Visible = false;
                lblApproved.Visible = false;
            }


        }


    }
    public DataTable GetPlannings()
    {
        DBHelper DBConn = new DBHelper();
        DataTable dt = new DataTable();
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[3];
        string ResourceNames = string.Empty;
        string AssignBy = string.Empty;
        if (string.IsNullOrEmpty(ResourceNames))
        {
            ResourceNames = "All";
        }
        else
        {
            ResourceNames = drp_resourcename.SelectedValue;
        }

        

        //  DateTime date = DateTime.ParseExact(DateTime.Now.ToShortDateString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
        string fromdate = string.Empty;
        string todate = string.Empty;
        if (!string.IsNullOrEmpty(txt_fromdate.Value))
        {
            string[] arr = txt_fromdate.Value.Split('-');
            fromdate = arr[2] + "-" + arr[1] + "-" + arr[0];

        }
        else
        {
            ////  fromdate = date.Day + "-" + date.Month + "-" + date.Year;
            // fromdate = date.ToString().Replace(" 00:00:00", "");

        }
        if (!string.IsNullOrEmpty(txt_todate.Value))
        {
            string[] arr = txt_todate.Value.Split('-');
            todate = arr[2] + "-" + arr[1] + "-" + arr[0];
        }
        else
        {
            //// todate = date.Day + "-" + date.Month + "-" + date.Year;
            // todate = date.ToString().Replace(" 00:00:00", "");

        }

        //lblshowdate.Text = txt_fromdate.Value + "||" + txt_todate.Value;
        sqlparam[0] = new SqlParameter("@ResourceNo", drp_resourcename.SelectedValue);
        sqlparam[1] = new SqlParameter("@Fromdate", fromdate);
        sqlparam[2] = new SqlParameter("@ToDate", todate);

       
          
       // sqlparam[3] = new SqlParameter("@Role", );

        //if (Session["Role"].ToString() != "TL")
        //{   
        //    sqlparam[3] = new SqlParameter("@Role",drp_resourcename.SelectedValue);
        //    dt = DBConn.Selection("Proc_GetPlanning1", sqlparam, "");
        //}
        //else
        //{
              //sqlparam[3] = new SqlParameter("@Role","TL" );
              dt = DBConn.Selection("Proc_GetPlanning1", sqlparam, "");
        //}


        return dt;
    }
    public DataTable GetPlannings1()
    {
        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[2];
        string ResourceNames = string.Empty;
        if (string.IsNullOrEmpty(ResourceNames))
        {
            ResourceNames = "All";
        }
        else
        {
            ResourceNames = drp_resourcename.SelectedValue;
        }

        string updatedate = string.Empty;
        string time = string.Empty;
        if (!string.IsNullOrEmpty(txt_updated.Value))
        {
            string[] arr = txt_updated.Value.Split('-');
            updatedate = arr[2] + "-" + arr[1] + "-" + arr[0];

        }
        else
        {
        }
        if (!string.IsNullOrEmpty(timepicker2.Value))
        {
            string[] arr1 = timepicker2.Value.Split(':');
            time = txt_updated.Value + " " + arr1[0] + ":" + arr1[1];
        }

        sqlparam[0] = new SqlParameter("@ResourceNo", drp_resourcename.SelectedValue);
        sqlparam[1] = new SqlParameter("@created_date", time);
       // sqlparam[2] = new SqlParameter("@Role", drp_resourcename.SelectedValue);

        dt = DBConn.Selection("Proc_GetPlanningUpdate", sqlparam, "");
       
        //if (Session["Role"].ToString() != "PMO")
        //{
        //    //sqlparam[2] = new SqlParameter("@Role", drp_resourcename.SelectedValue);
        //    dt = DBConn.Selection("Proc_GetPlanningUpdate", sqlparam, "");
        //}
        //else
        //{
        //    sqlparam[2] = new SqlParameter("@Role", "PMO");
        //    dt = DBConn.Selection("Proc_GetPlanningUpdatePMO", sqlparam, "");
        //}

        return dt;
    }

    public DataTable GetPlanningsExport()
    {
        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[3];
        string ResourceNames = string.Empty;
        if (string.IsNullOrEmpty(ResourceNames))
        {
            ResourceNames = "All";
        }
        else
        {
            ResourceNames = drp_resourcename.SelectedValue;
        }


        string fromdate = string.Empty;
        string todate = string.Empty;
        if (!string.IsNullOrEmpty(txt_fromdate.Value))
        {
            string[] arr = txt_fromdate.Value.Split('-');
            fromdate = arr[2] + "-" + arr[1] + "-" + arr[0];

        }
        else
        {
            // fromdate = date.ToString().Replace(" 00:00:00", "");

        }
        if (!string.IsNullOrEmpty(txt_todate.Value))
        {
            string[] arr = txt_todate.Value.Split('-');
            todate = arr[2] + "-" + arr[1] + "-" + arr[0];
        }
        else
        {
            // todate = date.ToString().Replace(" 00:00:00", "");

        }


        sqlparam[0] = new SqlParameter("@ResourceNo", drp_resourcename.SelectedValue);
        sqlparam[1] = new SqlParameter("@Fromdate", fromdate);
        sqlparam[2] = new SqlParameter("@ToDate", todate);


        dt = DBConn.Selection("Proc_GetPlanningExport", sqlparam, "");

        return dt;
    }


    public void Get_ddl()
    {
        //DBHelper DBConn = new DBHelper();
        //DataTable dt=new DataTable();
        //SqlParameter[] sqlparam;
        //sqlparam = new SqlParameter[0];
        //dt = DBConn.Selection("GET_ROLE", sqlparam, "");
        SqlCommand cmd = new SqlCommand("GET_ROLE", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlAssign.DataSource = dt;
            ddlAssign.DataTextField = "Name";
            ddlAssign.DataValueField = "ResourceNo";
            ddlAssign.DataBind();
            ddlAssign.Items.Insert(0, new ListItem("Select", string.Empty));
        }
    }
    public void Bindgrid()
     {
        DataTable ds = GetPlannings();
       // DataTable ds1 = GetPlannings1();
        DataView dv = new DataView();
        string resourceno = Session["ResourceNo"].ToString();
        dv = ds.DefaultView;  
        gvDetails.DataSource = dv;
        gvDetails.DataBind();
        gvUpdate.Visible = false;
        gvDetails.Visible = true;
      
    }
    public void Bindgrid1()
    {
        //DataTable ds = GetPlannings();
        DataTable ds1 = GetPlannings1();
        gvUpdate.DataSource = ds1;
        gvUpdate.DataBind();
        gvDetails.Visible = false;
        gvUpdate.Visible = true;
    }
    public void ExportToexcel()
    {

        DataTable ds = GetPlanningsExport();

        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(ds, "Planning");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Servpro-Connect-PlanningLines-" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
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

    public void GetSalesCycleMaster()
    {
        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[0];
        dt = DBConn.Selection("Proc_GetSalesCycleMaster1", sqlparam, "");

        if (dt.Rows.Count > 0)
        {
            drp_salescyles.DataSource = dt;
            drp_salescyles.DataTextField = "SalesDescription";
            drp_salescyles.DataValueField = "SalesCode";
            drp_salescyles.DataBind();
            drp_salescyles.Items.Insert(0, new ListItem("Select", string.Empty));
        }


    }

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


    protected void txt_projectname_TextChanged(object sender, EventArgs e)
    {
        lblprojectNumber.Text = hfProjectid.Value;
        if (!string.IsNullOrEmpty(lblprojectNumber.Text))
            drp_taskname.Enabled = true;
        else
            drp_taskname.Enabled = false;
        GetTasks(lblprojectNumber.Text);
    }
    protected void Save_Click(object sender, EventArgs e)
    {

        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[21];

        int O_hours = Convert.ToInt32(drp_hours.Value);
        int O_minutes = Convert.ToInt32(drp_minutes.Value);
        string starttime = timepicker1.Value;
        string[] arr = starttime.Split(':');
        int S_hours = Convert.ToInt32(arr[0]);
        string startimehours = string.Empty;
        if (arr[0].Length == 1)
        {
            startimehours = "0" + arr[0];
        }
        else
        {
            startimehours = arr[0];
        }
        int S_minutes = Convert.ToInt32(arr[1].Replace(" AM", ""));
        int O_HS = O_hours + S_hours;
        int S_HS = O_minutes + S_minutes;
        string end_time = Convert.ToString(O_HS + ":" + S_HS);



        TimeSpan t1 = TimeSpan.Parse(drp_hours.Value + ":" + drp_minutes.Value);
        TimeSpan t2 = TimeSpan.Parse(arr[0] + ":" + arr[1]);
        TimeSpan t3 = t1.Add(t2);




        string ResourceNo = string.Empty;
        string ResourceName = string.Empty;
        if (drpresource_Admin.SelectedValue == "")
        {
            ResourceNo = Session["ResourceNo"].ToString();
            ResourceName = Session["Name"].ToString();
        }
        else
        {
            ResourceNo = drpresource_Admin.SelectedValue;
            ResourceName = drpresource_Admin.SelectedItem.Text;
        }



        sqlparam[0] = new SqlParameter("@id", "0");
        sqlparam[1] = new SqlParameter("@project_name", txt_projectname.Text);
        sqlparam[2] = new SqlParameter("@project_number", lblprojectNumber.Text);
        sqlparam[3] = new SqlParameter("@task_name", drp_taskname.SelectedItem.Text);
        sqlparam[4] = new SqlParameter("@start_date", txt_startdate.Value);
        sqlparam[5] = new SqlParameter("@start_time", startimehours + ":" + arr[1]);
        sqlparam[6] = new SqlParameter("@hours", drp_hours.Value + ":" + drp_minutes.Value);
        sqlparam[7] = new SqlParameter("@task_description", txt_description.Text);
        sqlparam[8] = new SqlParameter("@additional_descrption", txt_additonaldescription.Value);
        sqlparam[9] = new SqlParameter("@rescource_id", ResourceNo);
        sqlparam[10] = new SqlParameter("@sales_cycleNumber", drp_salescyles.SelectedValue);
        sqlparam[11] = new SqlParameter("@task_number", drp_taskname.SelectedValue);
        sqlparam[12] = new SqlParameter("@created_by", ResourceName);
        sqlparam[13] = new SqlParameter("@resorcename", ResourceName);
        sqlparam[14] = new SqlParameter("@end_date", txt_startdate.Value);
        sqlparam[15] = new SqlParameter("@end_time", t3);
        sqlparam[16] = new SqlParameter("@sales_cycle", drp_salescyles.SelectedItem.Text);
        sqlparam[17] = new SqlParameter("@location", "ST MAH");
        sqlparam[18] = new SqlParameter("@contact_person", "");
        sqlparam[19] = new SqlParameter("@modified_by", ResourceName);
        sqlparam[20] = new SqlParameter("@AssignedBy",ddlAssign.SelectedValue);
        int i = DBConn.Save("InsertPlanning", sqlparam, "");
        if (i > 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Saved Successfully');", true);
            clear();
        }
        Bindgrid();
        //serviceRef();
    }
    void clear()
    {
        txt_projectname.Text = string.Empty;
        lblprojectNumber.Text = string.Empty;
        drp_taskname.SelectedValue = string.Empty;
        txt_startdate.Value = string.Empty;
        timepicker1.Value = string.Empty;
        drp_hours.Value = string.Empty;
        drp_minutes.Value = string.Empty;
        drp_salescyles.SelectedValue = string.Empty;
        txt_description.Text = string.Empty;
        txt_additonaldescription.Value = string.Empty;
        lblprojectNumber.Text = string.Empty;
        lbltasknumber.Text = string.Empty;
        lblcyclenumber.Text = string.Empty;
        ddlAssign.SelectedValue=string.Empty;
    }

    protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvDetails.EditIndex = e.NewEditIndex;
        Bindgrid();
    }
    protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = (GridViewRow)gvDetails.Rows[e.RowIndex];
        int id = Int32.Parse(gvDetails.DataKeys[e.RowIndex].Value.ToString());
        TextBox project_name = (TextBox)row.FindControl("txtproject_name");
        Label project_number = (Label)row.FindControl("lblproject_number");
        Label resoruce_id = (Label)row.FindControl("txtrescource_id");
        Label resource_name = (Label)row.FindControl("lblresorcename");
        DropDownList drpsales_cycles = (DropDownList)row.FindControl("drpsales_cycle");
        DropDownList task_name = (DropDownList)row.FindControl("drptask_name");
        DropDownList hours = (DropDownList)row.FindControl("drphours");
        DropDownList minutes = (DropDownList)row.FindControl("drpminutes");
        TextBox task_description = (TextBox)row.FindControl("txttask_description");
        TextBox addtional_description = (TextBox)row.FindControl("txtadditional_descrption");
        TextBox start_date = (TextBox)row.FindControl("txtstart_date");
        TextBox start_time = (TextBox)row.FindControl("txtstart_time");
        TextBox end_date = (TextBox)row.FindControl("txtend_date");
        TextBox end_time = (TextBox)row.FindControl("txtend_time");
        DropDownList drpasignname = (DropDownList)row.FindControl("drpasignname");
        Label lblasignname = (Label)row.FindControl("lblasignname");

        int O_hours = Convert.ToInt32(hours.SelectedValue);
        int O_minutes = Convert.ToInt32(minutes.SelectedValue);
        string starttime = timepicker1.Value;
        string[] arr = start_time.Text.Split(':');
        int S_hours = Convert.ToInt32(arr[0]);
        int S_minutes = Convert.ToInt32(arr[1].Replace(" AM", ""));
        int O_HS = O_hours + S_hours;
        int S_HS = O_minutes + S_minutes;
        string end_time1 = Convert.ToString(O_HS + ":" + S_HS);

        TimeSpan t1 = TimeSpan.Parse(hours.SelectedValue + ":" + minutes.SelectedValue);
        TimeSpan t2 = TimeSpan.Parse(arr[0] + ":" + arr[1]);
        TimeSpan t3 = t1.Add(t2);

        if (start_date.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Please enter start date');", true);

        }
        else if (task_description.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Please enter Task Description');", true);

        }
        else if (project_name.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Please enter Project Name');", true);

        }
        else if (start_time.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Please enter Start Time');", true);

        }
        else
        {

            DBHelper DBConn = new DBHelper();
            DataTable dt;
            SqlParameter[] sqlparam;
            sqlparam = new SqlParameter[21];

            sqlparam[0] = new SqlParameter("@id", id);
            sqlparam[1] = new SqlParameter("@project_name", project_name.Text);
            sqlparam[2] = new SqlParameter("@project_number", project_number.Text);
            sqlparam[3] = new SqlParameter("@task_name", task_name.SelectedItem.Text);
            sqlparam[4] = new SqlParameter("@start_date", start_date.Text);
            sqlparam[5] = new SqlParameter("@start_time", start_time.Text);
            sqlparam[6] = new SqlParameter("@hours", O_hours + ":" + O_minutes);
            sqlparam[7] = new SqlParameter("@task_description", task_description.Text);
            sqlparam[8] = new SqlParameter("@additional_descrption", addtional_description.Text);
            sqlparam[9] = new SqlParameter("@rescource_id", Session["ResourceNo"]);
            sqlparam[10] = new SqlParameter("@sales_cycleNumber", drpsales_cycles.SelectedValue);
            sqlparam[11] = new SqlParameter("@task_number", task_name.SelectedValue);
            sqlparam[12] = new SqlParameter("@created_by", Session["Name"]);
            sqlparam[13] = new SqlParameter("@resorcename", resource_name.Text);
            sqlparam[14] = new SqlParameter("@end_date", start_date.Text);
            sqlparam[15] = new SqlParameter("@end_time", t3);
            sqlparam[16] = new SqlParameter("@sales_cycle", drpsales_cycles.SelectedItem.Text);
            sqlparam[17] = new SqlParameter("@location", "ST MAH");
            sqlparam[18] = new SqlParameter("@contact_person", "");
            sqlparam[19] = new SqlParameter("@modified_by", Session["Name"]);
            sqlparam[20] = new SqlParameter("@AssignedBy", drpasignname.SelectedValue);

            int i = DBConn.Save("InsertPlanning", sqlparam, "");
            {
                gvDetails.EditIndex = -1;
                Bindgrid();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Updated Successfully');", true);
            }

        }

    }
    protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

        gvDetails.EditIndex = -1;
        Bindgrid();
    }


    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        Bindgrid();
    }
    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //Button btnflag = (Button)e.Row.FindControl("btnApprvoedFlag");
            //if (Session["Role"].ToString().Equals(""))
            //{
            //    btnflag.Visible = false;

            //    gvDetails.Columns[19].Visible=false;
            //}
            Label lblst = (Label)e.Row.FindControl("lblstatus");
            string st = lblst.Text.Trim();
            LinkButton lb = (LinkButton)e.Row.Cells[24].Controls[0];
            string llb = lb.Text.Trim();
            if (st == "Approved")
            {
                lb.Enabled = false;
            }

        }
        if (e.Row.RowType == DataControlRowType.DataRow && gvDetails.EditIndex == e.Row.RowIndex)
        {

            
            DropDownList drpsales_cycle = (DropDownList)e.Row.FindControl("drpsales_cycle");
            
            Label lblproject_number = (Label)e.Row.FindControl("lblproject_number");
            Label labelhours = (Label)e.Row.FindControl("lbl_hours12");
            DropDownList drptask_name = (DropDownList)e.Row.FindControl("drptask_name");
            DropDownList drphours = (DropDownList)e.Row.FindControl("drphours");
            DropDownList drpminutes = (DropDownList)e.Row.FindControl("drpminutes");
            Label lblsales_cycleNumber = (Label)e.Row.FindControl("lblsales_cycleNumber");
            Label lbltask_nameNumber = (Label)e.Row.FindControl("lbltask_nameNumber");

            DropDownList drpasignname = (DropDownList)e.Row.FindControl("drpasignname");
            Label lblasignname = (Label)e.Row.FindControl("lblasignname");

            string[] arr = labelhours.Text.Split(':');


            DBHelper DBConn = new DBHelper();
            DataTable dt;
            SqlParameter[] sqlparam;
            DataTable dt1;
            SqlParameter[] sqlparam1;
            sqlparam = new SqlParameter[0];
            sqlparam1 = new SqlParameter[1];
            dt = DBConn.Selection("Proc_GetSalesCycleMaster1", sqlparam, "");


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
            drptask_name.Items.FindByValue((e.Row.FindControl("lbltask_nameNumber") as Label).Text).Selected = true;



            SqlCommand cmd = new SqlCommand("GET_ROLE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dtRole = new DataTable();
            sda.Fill(dtRole);
            if (dtRole.Rows.Count > 0)
            {
                drpasignname.DataSource = dtRole;
                drpasignname.DataTextField = "Name";
                drpasignname.DataValueField = "ResourceNo";
                drpasignname.DataBind();
                drpasignname.Items.Insert(0, new ListItem("Select", string.Empty));
            }
            //drpasignname.Items.FindByValue((e.Row.FindControl("lblasignname") as Label).Text).Selected = true;
            drpasignname.Items.FindByText((e.Row.FindControl("lblasignname") as Label).Text).Selected = true;
            drphours.Items.FindByValue(arr[0]).Selected = true;


            if (arr[1] == "0")
            {
                drpminutes.Items.FindByValue("0" + arr[1]).Selected = true;
            }
            else
            {
                drpminutes.Items.FindByValue(arr[1]).Selected = true;
            }
           
        }
    }

    protected void txtproject_name_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)txt.Parent.Parent;

        Label lblproject_number = (Label)gvRow.FindControl("lblproject_number");
        DropDownList drptask_name = (DropDownList)gvRow.FindControl("drptask_name");

        DBHelper DBConn = new DBHelper();

        DataTable dt1;
        SqlParameter[] sqlparam1;

        sqlparam1 = new SqlParameter[1];
        lblproject_number.Text = hfgridProjectid.Value;
        sqlparam1[0] = new SqlParameter("@JobNumber", lblproject_number.Text);
        dt1 = DBConn.Selection("Proc_GetTaskMaster", sqlparam1, "");

        drptask_name.DataSource = dt1;
        drptask_name.DataTextField = "TaskName";
        drptask_name.DataValueField = "Project_Number";
        drptask_name.DataBind();
    }
    protected void Apply_Click(object sender, EventArgs e)
    {
        
        Bindgrid();

    }

    protected void Download_employe_Click(object sender, EventArgs e)
    {
        ExportToexcel();
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "confirm('Delete this Record?');", true);
        GridViewRow row = (GridViewRow)gvDetails.Rows[e.RowIndex];
        int id = Int32.Parse(gvDetails.DataKeys[e.RowIndex].Value.ToString());

        DBHelper DBConn = new DBHelper();
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[1];

        sqlparam[0] = new SqlParameter("@id", id);
        int i = DBConn.Save("Proc_deleteplanning", sqlparam, "");
        if (i > 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Deleted Successfully');", true);
            Bindgrid();
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
    public void UploadTasks()
    {
        try
        {
            if (fpExcelUpload.HasFile)
            {
                int i;
                int RowCounter = 0;
                string FileName = Path.GetFileName(fpExcelUpload.PostedFile.FileName);
                string Extension = Path.GetExtension(fpExcelUpload.PostedFile.FileName);
                string FilePath1 = Server.MapPath(FileName);
                fpExcelUpload.SaveAs(FilePath1);
                string ConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=1\";", FilePath1);
                System.Text.StringBuilder stbQuery = new StringBuilder();
                stbQuery.Append("Select * FROM [Sheet1$]");
                OleDbDataAdapter adp = new OleDbDataAdapter(stbQuery.ToString(), ConnectionString);
                System.Data.DataTable dtInvoiceExcel = new System.Data.DataTable();
                adp.Fill(dtInvoiceExcel);

                DataTable dtInvoiceExcelClone = dtInvoiceExcel.Clone();
                dtInvoiceExcelClone.Columns.Add("STATUS", typeof(System.String));


                foreach (DataRow r in dtInvoiceExcel.Rows)
                {
                    if ((string.IsNullOrEmpty(r["Resource ID"].ToString())) ||
                        (string.IsNullOrEmpty(r["Project Number"].ToString())) ||
                        (string.IsNullOrEmpty(r["Task ID"].ToString())) ||
                        (string.IsNullOrEmpty(r["Sales Cycle ID"].ToString())) ||
                        (string.IsNullOrEmpty(r["Start Date"].ToString())) ||
                        (string.IsNullOrEmpty(r["Start Time"].ToString())) ||
                        (string.IsNullOrEmpty(r["Efforts (Hours)"].ToString())) ||
                        (string.IsNullOrEmpty(r["Efforts (Min)"].ToString())) ||
                        (string.IsNullOrEmpty(r["Task Description"].ToString()))||
                        (string.IsNullOrEmpty(r["AssignedBy"].ToString())))
                    {
                        dtInvoiceExcelClone.ImportRow(r);
                        dtInvoiceExcelClone.Rows[RowCounter]["STATUS"] = "fail";
                    }
                    else
                    {

                        int O_hours = Convert.ToInt32(r["Efforts (Hours)"].ToString());
                        int O_minutes = Convert.ToInt32(r["Efforts (Min)"].ToString());
                        string starttime = timepicker1.Value;
                        string s = r["Start Time"].ToString();
                        string[] arr = r["Start Time"].ToString().Split(':');
                        int S_hours = Convert.ToInt32(arr[0]);
                        int S_minutes = Convert.ToInt32(arr[1]);
                        int O_HS = O_hours + S_hours;
                        int S_HS = O_minutes + S_minutes;
                        string end_time1 = Convert.ToString(O_HS + ":" + S_HS);

                        TimeSpan t1 = TimeSpan.Parse(r["Efforts (Hours)"].ToString() + ":" + r["Efforts (Min)"].ToString());
                        TimeSpan t2 = TimeSpan.Parse(arr[0] + ":" + arr[1]);
                        TimeSpan t3 = t1.Add(t2);

                        SqlParameter[] param1 = new SqlParameter[21];
                        param1[0] = new SqlParameter("@id", "0");
                        param1[1] = new SqlParameter("@rescource_id", r["Resource ID"].ToString());
                        param1[2] = new SqlParameter("@project_number", r["Project Number"].ToString());
                        param1[3] = new SqlParameter("@task_number", r["Task ID"].ToString());
                        param1[4] = new SqlParameter("@sales_cycleNumber", r["Sales Cycle ID"].ToString());
                        param1[5] = new SqlParameter("@start_date", r["Start Date"].ToString());
                        param1[6] = new SqlParameter("@start_time", s);
                        param1[7] = new SqlParameter("@hours", O_hours + ":" + O_minutes);
                        param1[8] = new SqlParameter("@end_date", r["Start Date"].ToString());
                        param1[9] = new SqlParameter("@end_time", t3);
                        param1[10] = new SqlParameter("@task_description", r["Task Description"].ToString());
                        param1[11] = new SqlParameter("@additional_descrption", r["Additonal Description"].ToString());
                        param1[12] = new SqlParameter("@location", "ST MAH");
                        param1[13] = new SqlParameter("@created_by", Session["Name"]);
                        param1[14] = new SqlParameter("@resorcename", "");
                        param1[15] = new SqlParameter("@contact_person", "");
                        param1[16] = new SqlParameter("@modified_by", Session["Name"]);
                        param1[17] = new SqlParameter("@task_name", r["Task ID"].ToString());
                        param1[18] = new SqlParameter("@project_name", r["Project Number"].ToString());
                        param1[19] = new SqlParameter("@sales_cycle", r["Sales Cycle ID"].ToString());
                        param1[20] = new SqlParameter("@AssignedBy", r["AssignedBy"].ToString());

                        DBHelper DBConn1 = new DBHelper();
                        int a = DBConn1.Save("InsertPlanningImport", param1, "");
                        if (a > 0)
                        {
                            dtInvoiceExcelClone.ImportRow(r);
                            dtInvoiceExcelClone.Rows[RowCounter]["STATUS"] = "Success";

                        }
                        goto Outer;
                    }
                    RowCounter += 1;
                Outer:
                    continue;
                }

                string message = "Tasks uploaded sucessfully";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Task Upload", "alert('" + message + "');", true);
                //lblTotal.Text = RowCounter.ToString();
                //grdUploadData.DataSource = dtInvoiceExcelClone;
                //grdUploadData.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void Upload_Click(object sender, EventArgs e)
    {
        UploadTasks();
    }
    protected void drp_taskname_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbltasknumber.Text = drp_taskname.SelectedValue;
    }
    protected void drp_salescyles_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblcyclenumber.Text = drp_salescyles.SelectedValue;
    }
    protected void lnkTemplate_Click(object sender, EventArgs e)
    {
        string url = System.Configuration.ConfigurationManager.AppSettings["UploadStyleTemplate"].ToString();
        OpenNewWindow(url);

    }


    private void OpenNewWindow(string url)
    {
        string script = null;
        try
        {
            script = "window.open(\"" + url + "\",'','height=500px,width=1000px,scrollbars=1,resizable=1,top=00,left=30')";
            if (!Page.ClientScript.IsClientScriptBlockRegistered("NewWindow"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
        }

    }
   
    protected void btndownload_Click(object sender, EventArgs e)
    {
        ExportToexcel1();
    }
    public void ExportToexcel1()
    {

        DataTable ds = GetPlanningsExport1();

        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(ds, "Planning");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Servpro-Connect-PlanningLines-" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
    public DataTable GetPlanningsExport1()
    {
        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[2];
        string ResourceNames = string.Empty;
        if (string.IsNullOrEmpty(ResourceNames))
        {
            ResourceNames = "All";
        }
        else
        {
            ResourceNames = drp_resourcename.SelectedValue;
        }

        string updatedate = string.Empty;
        string time = string.Empty;
        if (!string.IsNullOrEmpty(txt_updated.Value))
        {
            string[] arr = txt_updated.Value.Split('-');
            updatedate = arr[2] + "-" + arr[1] + "-" + arr[0];

        }
        else
        {
        }
        if (!string.IsNullOrEmpty(timepicker2.Value))
        {
            string[] arr1 = timepicker2.Value.Split(':');
            time =txt_updated.Value+" "+ arr1[0] + ":" + arr1[1];
        }
        //string fromdate = string.Empty;
        //string todate = string.Empty;
        //if (!string.IsNullOrEmpty(txt_fromdate.Value))
        //{
        //    string[] arr = txt_fromdate.Value.Split('-');
        //    fromdate = arr[2] + "-" + arr[1] + "-" + arr[0];
        //}
        //else
        //{
        //    // fromdate = date.ToString().Replace(" 00:00:00", "");
        //}
        //if (!string.IsNullOrEmpty(txt_todate.Value))
        //{
        //    string[] arr = txt_todate.Value.Split('-');
        //    todate = arr[2] + "-" + arr[1] + "-" + arr[0];
        //}
        //else
        //{
        //    // todate = date.ToString().Replace(" 00:00:00", "");

        //}
        //sqlparam[0] = new SqlParameter("@ResourceNo", drp_resourcename.SelectedValue);
        //sqlparam[1] = new SqlParameter("@Fromdate", fromdate);
        //sqlparam[2] = new SqlParameter("@ToDate", todate);

        sqlparam[0] = new SqlParameter("@ResourceNo", drp_resourcename.SelectedValue);
        sqlparam[1] = new SqlParameter("@created_date", time);
        
        dt = DBConn.Selection("Proc_GetPlanningUpdate", sqlparam, "");

        return dt;
    }
    protected void btnapply_Click(object sender, EventArgs e)
    {
      
        Bindgrid1();
    }
    protected void btnApprvoedFlag_Click(object sender, EventArgs e)
    {   
        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[2];

        Button btnflag = ((Button)sender);
        GridViewRow grv = ((GridViewRow)btnflag.NamingContainer);
        //Button btnApproveflag = (Button)(grv.FindControl("btnApproveflag"));
        string checktxt = btnflag.Text.ToString();

        if (checktxt == "Approve")
        {
            Label lblserial = (Label)(grv.FindControl("lblid"));

            sqlparam[0] = new SqlParameter("@ApprovedFlag",'1');
            sqlparam[1] = new SqlParameter("@id",lblserial.Text );
           
            int i = DBConn.Save("Proc_GetFlagUpdate", sqlparam, "");
            if (i > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Approved Successfully');", true);
            }
            Bindgrid();
        }
         if (checktxt == "Approved")
        {
            Label lblserial = (Label)(grv.FindControl("lblid"));
         
            sqlparam[0] = new SqlParameter("@ApprovedFlag",'0');
            sqlparam[1] = new SqlParameter("@id", lblserial.Text);
            int i = DBConn.Save("Proc_GetFlagUpdate", sqlparam, "");
            if (i > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Approve Successfully');", true);
               
            }
            Bindgrid();
           
        }
    }

}