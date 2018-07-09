using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Approved : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection("server=servpro40;database=ConnectDB1;uid=demo;password=pro@1234");
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            try
            {
                GetResources();
                Bindgrid();
                BindgridAproved();
                BindgridPending();
                BindgridRejected();
            }
            catch (Exception ex) 
            {
            }
        }
        
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
            drp_resourcename.DataSource = dt;
            drp_resourcename.DataTextField = "Name";
            drp_resourcename.DataValueField = "ResourceNo";
            drp_resourcename.DataBind();
            drp_resourcename.Items.Insert(0, new ListItem("All", "All"));
           // drp_resourcename.Items.FindByText(Session["Name"].ToString()).Selected = true;
        }
    }
    public void Get_Reason()
    {
        //DBHelper DBConn = new DBHelper();
        //DataTable dt=new DataTable();
        //SqlParameter[] sqlparam;
        //sqlparam = new SqlParameter[0];
        //dt = DBConn.Selection("GET_ROLE", sqlparam, "");
        SqlCommand cmd = new SqlCommand("GET_ReasonsMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlReason.DataSource = dt;
            ddlReason.DataTextField = "RejectReasons";
            ddlReason.DataValueField = "RejectId";
            ddlReason.DataBind();
            ddlReason.Items.Insert(0, new ListItem("Select", string.Empty));
        }
    }
    public void Bindgrid()
    {
        DataTable ds = GetPlannings();
        // DataTable ds1 = GetPlannings1();
        DataView dv = new DataView();
        //string resourceno = Session["ResourceNo"].ToString();
        dv = ds.DefaultView;
        grdAll.DataSource = dv;
        grdAll.DataBind();        
    }
    public DataTable GetPlannings()
    {
        DBHelper DBConn = new DBHelper();
        DataTable dt = new DataTable();
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
        sqlparam[0] = new SqlParameter("@ResourceNo", drp_resourcename.SelectedValue);
        sqlparam[1] = new SqlParameter("@Fromdate", fromdate);
        sqlparam[2] = new SqlParameter("@ToDate", todate);
        dt = DBConn.Selection("Proc_GetPlanning1", sqlparam, "");


        return dt;
    }

    public void BindgridAproved()
    {
        grdpending.Visible = false;
        DataTable ds = GetApprovedPlannings();
        // DataTable ds1 = GetPlannings1();
        DataView dv = new DataView();
        //string resourceno = Session["ResourceNo"].ToString();
        dv = ds.DefaultView;
        grdApproved.DataSource = dv;
        grdApproved.DataBind();
    }
    public DataTable GetApprovedPlannings()
    {
        DBHelper DBConn = new DBHelper();
        DataTable dt = new DataTable();
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[5];
        string ResourceNames = string.Empty;

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
        sqlparam[0] = new SqlParameter("@ResourceNo", drp_resourcename.SelectedValue);
        sqlparam[1] = new SqlParameter("@Fromdate", fromdate);
        sqlparam[2] = new SqlParameter("@ToDate", todate);
        sqlparam[3] = new SqlParameter("@AssignedBy", Session["ResourceNo"]);
        sqlparam[4] = new SqlParameter("@Role", Session["Role"]);
        dt = DBConn.Selection("GET_ApprovedPlanningLines", sqlparam, "");
        if (Session["Role"].ToString() != "TL")
        {
            sqlparam[4] = new SqlParameter("@Role", drp_resourcename.SelectedValue);
            dt = DBConn.Selection("GET_ApprovedPlanningLines", sqlparam, "");
        }


        return dt;
    }
    public void BindgridPending()
    {
       
        DataTable ds = GetPendingPlannings();
        // DataTable ds1 = GetPlannings1();
        DataView dv = new DataView();
       // string resourceno = Session["ResourceNo"].ToString();
        dv = ds.DefaultView;
        grdpending.DataSource = dv;
        grdpending.DataBind();
       
        
    }
    public DataTable GetPendingPlannings()
    {
        DBHelper DBConn = new DBHelper();
        DataTable dt = new DataTable();
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[5];
        string ResourceNames = string.Empty;

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

        if (Session["Role"].ToString() != "TL")
        {
            sqlparam[4] = new SqlParameter("@Role", drp_resourcename.SelectedValue);
           
        }

        sqlparam[0] = new SqlParameter("@ResourceNo", drp_resourcename.SelectedValue);
        sqlparam[1] = new SqlParameter("@Fromdate", fromdate);
        sqlparam[2] = new SqlParameter("@ToDate", todate);
        sqlparam[3] = new SqlParameter("@AssignedBy", Session["ResourceNo"]);
        sqlparam[4]=new SqlParameter("@Role",Session["Role"]);
        dt = DBConn.Selection("GET_PendingPlanningLines", sqlparam, "");
        if (Session["Role"].ToString() != "TL")
        {
            sqlparam[4] = new SqlParameter("@Role", drp_resourcename.SelectedValue);
            dt = DBConn.Selection("GET_PendingPlanningLines", sqlparam, "");
        }

        return dt;
    }
    public void BindgridRejected()
    {       
        DataTable ds = GetRejectedPlannings();
        // DataTable ds1 = GetPlannings1();
        DataView dv = new DataView();
        //string resourceno = Session["ResourceNo"].ToString();
        dv = ds.DefaultView;
        grdRejected.DataSource = dv;
        grdRejected.DataBind();
    }
    public DataTable GetRejectedPlannings()
    {
        DBHelper DBConn = new DBHelper();
        DataTable dt = new DataTable();
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[5];
        string ResourceNames = string.Empty;

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
        sqlparam[0] = new SqlParameter("@ResourceNo", drp_resourcename.SelectedValue);
        sqlparam[1] = new SqlParameter("@Fromdate", fromdate);
        sqlparam[2] = new SqlParameter("@ToDate", todate);
        sqlparam[3] = new SqlParameter("@AssignedBy", Session["ResourceNo"]);
        sqlparam[4] = new SqlParameter("@Role", Session["Role"]);
        dt = DBConn.Selection("GET_RejectedPlanningLines", sqlparam, "");
        if (Session["Role"].ToString() != "TL")
        {
            sqlparam[4] = new SqlParameter("@Role", drp_resourcename.SelectedValue);
            //dt = DBConn.Selection("GET_RejectedPlanningLines", sqlparam, "");
            dt = DBConn.Selection("GET_RejectedPlanningLines", sqlparam, "");
        }
      
        return dt;
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

        //if (checktxt == "Applied")
        //{
            Label lblserial = (Label)(grv.FindControl("lblid"));

            sqlparam[0] = new SqlParameter("@Status", "Approved");
            sqlparam[1] = new SqlParameter("@id",lblserial.Text );
           
            int i = DBConn.Save("Proc_GetFlagUpdate", sqlparam, "");
            if (i > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Approved Successfully');", true);
                //string btnapproved = (grdAll.SelectedRow.FindControl("btnApprvoedFlag") as Label).Text;
                //string btnRejected = (grdAll.SelectedRow.FindControl("btnRejectedFlag") as Label).Text;
                //string lblstatus = (grdAll.SelectedRow.FindControl("lblstatus") as Label).Text;
 
                //Button btnapproved = (Button)FindControl("btnApprvoedFlag");
                //Button btnRejected = (Button)FindControl("btnRejectedFlag");
                //Label lblstatus = (Label)F indControl("lblstatus");
                
                //btnRejected.Visible = false;
                //lblstatus.Visible = true;
                BindgridPending();

            }
            
            
            //Bindgrid();
        }
        // if (checktxt == "Approved")
        //{
        //    Label lblserial = (Label)(grv.FindControl("lblid"));
         
        //    sqlparam[0] = new SqlParameter("@ApprovedFlag",'0');
        //    sqlparam[1] = new SqlParameter("@id", lblserial.Text);
        //    int i = DBConn.Save("Proc_GetFlagUpdate", sqlparam, "");
            
        //    if (i > 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Approve Successfully');", true);
               
        //    }
        //    Bindgrid();
           
        //}
   // }
    
    protected void Button1_Click(object sender, EventArgs e)
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
    
    protected void btnRejectedFlag_Click(object sender, EventArgs e)
    {
        Get_Reason();
        ModalPopupExtender1.Show();
        //DBHelper DBConn = new DBHelper();
        //DataTable dt;
        //SqlParameter[] sqlparam;
        //sqlparam = new SqlParameter[2];

        Button btnRejectflag = ((Button)sender);
        GridViewRow grv = ((GridViewRow)btnRejectflag.NamingContainer);
        Session["gvRejectedRecord"] = grv;
        ////Button btnApproveflag = (Button)(grv.FindControl("btnApproveflag"));
        //string checktxt = btnRejectflag.Text.ToString();

        //if (checktxt == "Reject")
        //{
        //    Label lblserial = (Label)(grv.FindControl("lblid"));

        //    sqlparam[0] = new SqlParameter("@RejectedFlag", '3');
        //    sqlparam[1] = new SqlParameter("@id", lblserial.Text);

        //    int i = DBConn.Save("Proc_GetRejectedFlagUpdate", sqlparam, "");
        //    if (i > 0)
        //    {
        //        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Rejecteded Successfully');", true);
        //        BindgridPending();
        //        Get_Reason();
        //        ModalPopupExtender1.Show();
        //    } 
        //    //BindgridPending();
        //    //ModalPopupExtender1.Show();
        //   // Bindgrid();
        //}
        ////if (checktxt == "Rejected")
        ////{
        ////    Label lblserial = (Label)(grv.FindControl("lblid"));

        ////    sqlparam[0] = new SqlParameter("@RejectedFlag", '2');
        ////    sqlparam[1] = new SqlParameter("@id", lblserial.Text);
        ////    int i = DBConn.Save("Proc_GetRejectedFlagUpdate", sqlparam, "");
        ////    if (i > 0)
        ////    {
        ////        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Reject Successfully');", true);

        ////    }
        ////    Bindgrid();
        ////}
    }

    //protected void btnALL_Click(object sender, EventArgs e)
    //{
    //    btnALL.Focus();
    //    btnALL.BackColor = System.Drawing.Color.White;
    //    btnALL.ForeColor = System.Drawing.Color.Orange;
    //    btnALL.BorderColor = System.Drawing.Color.Orange;
    //    clear();
    //    drp_resourcename.SelectedValue = Session["ResourceNo"].ToString();
    //    divall.Visible = true;
    //    btnapply.Visible = true;
    //    grdAll.Visible = true;

    //    btnapplyapproved.Visible = false;
    //    grdApproved.Visible = false;

    //    btnapplyPending.Visible = false;
    //    grdpending.Visible = false;

    //    btnapplyrejected.Visible = false;
    //    grdRejected.Visible = false;
    //}
    //protected void btnApproved_Click(object sender, EventArgs e)
    //{
        
    //    //btnApproved.Focus();
    //    //btnApproved.BackColor = System.Drawing.Color.White;
    //    //btnApproved.ForeColor = System.Drawing.Color.Orange;
    //    //btnApproved.BorderColor = System.Drawing.Color.Orange;

    //    clear();
    //    divall.Visible = true;
        
    //    btnapply.Visible = false;
    //    grdAll.Visible = false;

    //    drp_resourcename.SelectedValue = Session["ResourceNo"].ToString();
    //    btnapplyapproved.Visible = true;
    //    grdApproved.Visible = true;

    //    btnapplyPending.Visible = false;
    //    grdpending.Visible = false;

    //    btnapplyrejected.Visible = false;
    //    grdRejected.Visible = false;
    //}
    //protected void btnPending_Click(object sender, EventArgs e)
    //{
    //    clear();
    //    divall.Visible = true;
    //    btnapply.Visible = false;
    //    grdAll.Visible = false;

    //    btnapplyapproved.Visible = false;
    //    grdApproved.Visible = false;

    //    drp_resourcename.SelectedValue = Session["ResourceNo"].ToString();
    //    btnapplyPending.Visible = true;
    //    grdpending.Visible = true;

    //    btnapplyrejected.Visible = false;
    //    grdRejected.Visible = false;
    //}
    //protected void btnRejected_Click(object sender, EventArgs e)
    //{
    //    clear();
    //    divall.Visible = true;
    //    btnapply.Visible = false;
    //    grdAll.Visible = false;

    //    btnapplyapproved.Visible = false;
    //    grdApproved.Visible = false;

    //    btnapplyPending.Visible = false;
    //    grdpending.Visible = false;

    //    drp_resourcename.SelectedValue = Session["ResourceNo"].ToString();
    //    btnapplyrejected.Visible = true;
    //    grdRejected.Visible = true;
    //}

    //protected void btnapply_Click1(object sender, EventArgs e)
    //{
    //    divall.Visible = true;
    //    btnapply.Visible = true;
    //    grdAll.Visible = true;

    //    btnapplyapproved.Visible = false;
    //    grdApproved.Visible = false;

    //    btnapplyPending.Visible = false;
    //    grdpending.Visible = false;

    //    btnapplyrejected.Visible = false;
    //    grdRejected.Visible = false;

    //    Bindgrid();
    //}
    //protected void btnapplyapproved_Click(object sender, EventArgs e)
    //{
    //    divall.Visible = true;
    //    btnapply.Visible = false;
    //    grdAll.Visible = false;

    //    btnapplyapproved.Visible = true;
    //    grdApproved.Visible = true;

    //    btnapplyPending.Visible = false;
    //    grdpending.Visible = false;

    //    btnapplyrejected.Visible = false;
    //    grdRejected.Visible = false;
    //    BindgridAproved();
    //}
    //protected void btnapplyPending_Click(object sender, EventArgs e)
    //{
    //    divall.Visible = true;
    //    btnapply.Visible = false;
    //    grdAll.Visible = false;

    //    btnapplyapproved.Visible = false;
    //    grdApproved.Visible = false;

    //    btnapplyPending.Visible = true;
    //    grdpending.Visible = true;

    //    btnapplyrejected.Visible = false;
    //    grdRejected.Visible = false;
    //    BindgridPending();
    //}
    //protected void btnapplyrejected_Click(object sender, EventArgs e)
    //{
    //    divall.Visible = true;
    //    btnapply.Visible = false;
    //    grdAll.Visible = false;

    //    btnapplyapproved.Visible = false;
    //    grdApproved.Visible = false;

    //    btnapplyPending.Visible = false;
    //    grdpending.Visible = false;

    //    btnapplyrejected.Visible = true;
    //    grdRejected.Visible = true;
    //    BindgridRejected();
    //}
    void clear()
    {       
        txt_fromdate.Value = string.Empty;
        txt_todate.Value = string.Empty;
        ddlReason.SelectedIndex = 0;
    }

    protected void btnapply_Click(object sender, EventArgs e)
    {
        if (rdoStatus.SelectedValue.ToString() == "Pending")
        {
            grdpending.Visible = true;
            BindgridPending();
        }
        else if (rdoStatus.SelectedValue.ToString() == "Approved")
        {
            grdApproved.Visible = true;
            BindgridAproved();
        }
        else if (rdoStatus.SelectedValue.ToString() == "Rejected")
        {
            grdRejected.Visible = true;
            BindgridRejected();
        }
        else
        {
            grdAll.Visible = true;
            Bindgrid();
        }
    }
    protected void rdoStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoStatus.SelectedValue.ToString() == "Pending")
        {
            grdAll.Visible = false;
            grdApproved.Visible = false;
            grdRejected.Visible = false;
            //clear();
        }
        else if (rdoStatus.SelectedValue.ToString() == "Approved")
        {
            grdAll.Visible = false;
            grdpending.Visible = false;
            grdRejected.Visible = false;
            //clear();
        }
        else if (rdoStatus.SelectedValue.ToString() == "Rejected")
        {
            grdAll.Visible = false;
            grdpending.Visible = false;
            grdApproved.Visible = false;
            //clear();
        }
        else
        {
            grdApproved.Visible = false;
            grdpending.Visible = false;
            grdRejected.Visible = false;
            //clear();  
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        
        DBHelper DBConn = new DBHelper();
        DataTable dt;
        SqlParameter[] sqlparam;
        sqlparam = new SqlParameter[3];
        
        Label btnflag = sender as Label;
     
        GridViewRow row = ((GridViewRow)Session["gvRejectedRecord"]);


        Label ID1 = row.FindControl("lblid") as Label;
       // Label lblproject_name = row.FindControl("lblproject_name") as Label;
       // Label lblproject_number = row.FindControl("lblproject_number") as Label;
       // Label lblrescource_id = row.FindControl("lblrescource_id") as Label;
       //// Label lblcreated_date = row.FindControl("lblcreated_date") as Label;
       // Label lblresorcename = row.FindControl("lblresorcename") as Label;
       // Label lbltask_name = row.FindControl("lbltask_name") as Label;
       // Label lbltask_description = row.FindControl("lbltask_description") as Label;
       // Label lbladditional_descrption = row.FindControl("lbladditional_descrption") as Label;
       // Label lblsales_cycleNumber = row.FindControl("lblsales_cycleNumber") as Label;
       // Label lbltask_nameNumber = row.FindControl("lbltask_nameNumber") as Label;
       // Label lblsales_cycle = row.FindControl("lblsales_cycle") as Label;
       // Label lblstart_date = row.FindControl("lblstart_date") as Label;

       // DateTime dtstart_date = DateTime.ParseExact(lblstart_date.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
       // string strStarDate = dtstart_date.Date.Year + "-" + dtstart_date.Date.Month + "-" + dtstart_date.Date.Day;

       // Label lblstart_time = row.FindControl("lblstart_time") as Label;
       // Label lbl_h = row.FindControl("lbl_h") as Label;
       // Label lblend_date = row.FindControl("lblend_date") as Label;

       // DateTime dtEnd_date = DateTime.ParseExact(lblend_date.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
       // string strEndDate = dtEnd_date.Date.Year + "-" + dtEnd_date.Date.Month + "-" + dtEnd_date.Date.Day;

       // Label lblend_time = row.FindControl("lblend_time") as Label;
       // Label lblcreated_by = row.FindControl("lblcreated_by") as Label;
       // Label lblcreated_date = row.FindControl("lblcreated_date") as Label;

       // DateTime dtCreated_date = DateTime.ParseExact(lblcreated_date.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
       // string strCreatedDate = dtCreated_date.Date.Year + "-" + dtCreated_date.Date.Month + "-" + dtCreated_date.Date.Day;

       // Label lblassinname = row.FindControl("lblassinname") as Label;


        sqlparam[0] = new SqlParameter("@id", ID1.Text);
       // sqlparam[1] = new SqlParameter("@project_name", lblproject_name.Text);
       // sqlparam[2] = new SqlParameter("@project_number", lblproject_number.Text);
       // sqlparam[3] = new SqlParameter("@task_name", lbltask_name.Text);
       // sqlparam[4] = new SqlParameter("@start_date", strStarDate);
       // sqlparam[5] = new SqlParameter("@hours", lbl_h.Text);
       // sqlparam[6] = new SqlParameter("@task_description", lbltask_description.Text);
       // sqlparam[7] = new SqlParameter("@additional_descrption", lbladditional_descrption.Text);
       // sqlparam[8] = new SqlParameter("@rescource_id", lblrescource_id.Text);
       // sqlparam[9] = new SqlParameter("@created_date", strCreatedDate);
       // sqlparam[10] = new SqlParameter("@sales_cycleNumber", lblsales_cycleNumber.Text);
       // sqlparam[11] = new SqlParameter("@task_number", lbltask_nameNumber.Text);
       // sqlparam[12] = new SqlParameter("@created_by", lblcreated_by.Text);
       // sqlparam[13] = new SqlParameter("@resorcename", lblresorcename.Text);
       // sqlparam[14] = new SqlParameter("@end_date", strEndDate);
       // sqlparam[15] = new SqlParameter("@start_time", lblstart_time.Text);
       // sqlparam[16] = new SqlParameter("@end_time", lblend_time.Text);
       // sqlparam[17] = new SqlParameter("@sales_cycle", lblsales_cycle.Text);
       // sqlparam[18] = new SqlParameter("@location", "ST MAH");
       // sqlparam[19] = new SqlParameter("@contact_person", "");
       // sqlparam[20] = new SqlParameter("@modified_by", Session["Name"]);
       // sqlparam[21] = new SqlParameter("@AssignedBy", lblassinname.Text);
        sqlparam[1] = new SqlParameter("@RejectedBy", Session["Name"]);
        if (ddlReason.SelectedValue == "5")
        {
            sqlparam[2] = new SqlParameter("@RejectedReason", txtother.Text);
        }
        else
        {
            sqlparam[2] = new SqlParameter("@RejectedReason", ddlReason.SelectedItem.Text);
        }
        int i = DBConn.Save("InsertRejectedPlanning", sqlparam, "");
        if (i > 0)
        {
           
            //grdpending.EditIndex = -1;
            
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('Reject Successfully');", true);
            //grdpending.Visible = true;
            //BindgridPending();
  
        }
        BindgridPending();
        clear();
        ModalPopupExtender1.Hide();
    }
    protected void ddlReason_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReason.SelectedValue == "5")
        {
            //TextBox txtother = (TextBox)ModalPopupExtender1.FindControl("txtother") as TextBox ;
            lblother.Visible = true;
            txtother.Visible = true;
        }
        else
        {
            lblother.Visible = false;
            txtother.Visible = false;
        }
    }
}